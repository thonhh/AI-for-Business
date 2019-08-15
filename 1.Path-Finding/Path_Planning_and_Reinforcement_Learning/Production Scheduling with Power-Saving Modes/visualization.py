# ======================================================================================================================
# Functions for visualization of the solution (drawing Gantt charts, graphs, etc.)
# ======================================================================================================================
import graphviz as gv
import svgwrite
from colour import Color
from solution import PatternWrapper

def draw_graph(instance, filename):
    """
    Draw a simple graph of modes for the given instance
    :param instance: Problem instance
    :param filename: Name of the image file to be saved
    :type instance: Instance
    :type str
    :return: Graph
    :rtype: nx.Graph
    """
    # Create a new graph
    G = gv.Digraph(format='svg')

    # Add nodes
    for i in range(instance.V):
        # The first argument is index of the node, the second argument is label of node
        # which consists of mode index, t_min, t_max and C
        G.node(str(i), ''.join(['Mode ', str(i),
                                '\n----------',
                                '\nt_min = ', str(instance.vertices[i]["t_min"]),
                                '\nt_max = ', str(instance.vertices[i]["t_max"]),
                                '\nC = ', str(instance.vertices[i]["C"])]))

    # Add edges
    for i in range(instance.V):
        for j in instance.edges[i]:
            G.edge(str(i), str(j["to"]), label="".join(["t = ", str(j["t"]), "\nC = ", str(j["C"])]))

    # Draw graph
    G.render(filename)

    # Return graph
    return G


def visualize_heuristics(instance, path):
    raise NotImplementedError("This function has to be implemented.")


def visualize_complex_solution(model, width, inst, path):
    """
    Visualize solution of full modes as a Gantt chart
    :param model: RelativeModel of the whole problem (solved)
    :param width: width of Gantt chart
    :param inst: instance
    :param path: path where to save the output
    :return: None
    """
    gantt = GanttChart(width, inst.N, inst.J, inst.V, inst.L, path)

    modes = model.get_modes()
    start_modes = model.get_start_modes()
    processing_modes = model.get_processing_modes()
    tasks_assign = model.get_tasks_assign()
    tasks_process = model.get_tasks_process()
    tasks_start = model.get_tasks_start()

    for n in range(inst.N):
        for i in range(model.I):
            gantt.add_mode(n, modes[n][i], start_modes[n][i], processing_modes[n][i], (True if modes[n][i] >= inst.V
                                                                                       else False))

    for j in range(inst.J):
        gantt.add_task(tasks_assign[j][0], j, tasks_start[j], tasks_process[j])

    gantt.save_image()


def visualize_solution(p_wrap, width, inst, path):
    """
    Visualize solution stored in PatternWrapper p_wrap
    :param p_wrap: pattern wrapper containing solution
    :param width: width of image in pixels
    :param inst: instance
    :param path: path where the image will be saved
    :return: None
    :type p_wrap: PatternWrapper
    """
    gantt = GanttChart(width, inst.N, inst.J, inst.V, inst.L, path)

    for n in range(len(p_wrap.patterns)):  # pattern <-> assignment of tasks to resource
        sol = p_wrap.solutions[n]  # solution for monoprocessor
        profile = inst.profiles[sol.profile_idx]
        assert sol is not None

        for j in p_wrap.patterns[n]:
            s_j = sol.get_s_j(j)
            p_j = sol.get_p_j(j)
            gantt.add_task(n, j, s_j, p_j)  # draw this task in gantt

        for i in range(len(profile)):
            s_m = sol.get_s_i(i)
            p_m = sol.get_p_i(i)
            gantt.add_mode(n, profile[i], s_m, p_m, False)

            if i > 0:  # add transition
                s_t = sol.get_s_i(i-1)
                p_t = sol.get_p_i(i-1)
                gantt.add_mode(n, profile[i], s_t + p_t, inst.V_distances[profile[i-1]][profile[i]]["time"], True)

    gantt.save_image()


class GanttChart:
    # SPACING/PADDING CONSTANTS
    PADDING = 10  # Padding around the whole image
    H_BOX = 40  # Height of space for plotting boxes (for modes/tasks)
    W_DESC = 120  # Width of resource description
    W_DESC_BOX = 20  # Width of colored box describing resource
    P_BETWEEN = 80  # Height of the space between resources
    P_SPACE = 5  # Small space
    P_BOX = 10  # Padding around plotted box (should be < than H_BOX/2)
    M_LEFT = 160  # Margin on the left side of the central area; should be > W_DESC + W_DESC_BOX
    M_RIGHT = 150  # Margin on the right side of the central area (some additional info)
    FONT_SIZE = 20  # Size of the font
    FONT_SMALL = 10  # Size of the smaller font
    # COLORS
    CLR_RES_FROM = "#edf8b1"
    CLR_RES_TO = "#2c7fb8"
    CLR_TASK_FROM = "#E1F5FE"
    CLR_TASK_TO = "#01579B"
    CLR_MODE_FROM = "#ffeda0"
    CLR_MODE_TO = "#f03b20"
    CLR_PADDING = "#ffffff"  # color around whole chart
    CLR_BACK = "#f5f5f5"  # color behind gant chart
    CLR_BACK_RES = "#D3D3D3"  # color under scheduled boxes

    def __init__(self, w, n, j, v, l, f_name):
        """
        Initialize this Gantt chart
        :param w: width in pixels (height is computed accordingly to number of resources)
        :param n: number of resources
        :param j: number of tasks
        :param v: number of modes
        :param l: length of planning horizon
        :param f_name: file where to save the image
        """
        assert w > 2*self.PADDING + self.M_LEFT + self.M_RIGHT + 1

        self.w = w
        self.h = 2*self.PADDING + (2*self.H_BOX+1)*n + (n+1)*self.P_BETWEEN
        self.n = n
        self.l = l
        self.canvas = svgwrite.Drawing(size=(self.w, self.h), filename=f_name)
        self.res_clrs = list(Color(self.CLR_RES_FROM).range_to(Color(self.CLR_RES_TO), n))
        self.task_clrs = list(Color(self.CLR_TASK_FROM).range_to(Color(self.CLR_TASK_TO), j))
        self.mode_clrs = list(Color(self.CLR_MODE_FROM).range_to(Color(self.CLR_MODE_TO), v))

        # Create file if not exists
        # try:
        #     fh = open(f_name, 'r')
        #     fh.close()
        # except:
        #     # if file does not exist, create it
        #     fh = open(f_name, 'w')
        #     fh.close()

        self.init_canvas()

    def init_canvas(self):
        # - background (padding)
        self.canvas.add(self.canvas.rect((0, 0), (self.w, self.h), fill=self.CLR_PADDING))
        # - background
        self.canvas.add(self.canvas.rect((self.PADDING, self.PADDING), (self.w-2*self.PADDING, self.h-2*self.PADDING),
                                         fill=self.CLR_BACK))

        # - left resource line
        o_from = (self.PADDING+self.M_LEFT, self.PADDING + self.P_BETWEEN)
        o_to = (o_from[0], o_from[1]+self.n*(2*self.H_BOX+1)+(self.n-1)*self.P_BETWEEN)
        self.canvas.add(self.canvas.line(start=o_from, end=o_to, stroke='black'))

        # - resources
        for i in range(self.n):
            # - resource background
            o_from = (self.PADDING + self.M_LEFT, self.PADDING + i*(2*self.H_BOX + 1 + self.P_BETWEEN) + self.P_BETWEEN)
            o_size = (self.get_chart_w(), 2*self.H_BOX + 1)
            self.canvas.add(self.canvas.rect(o_from, o_size, fill=self.CLR_BACK_RES))

            # - horizontal resource lines
            o_from = (self.PADDING + self.M_LEFT, self.PADDING + (i+1)*self.H_BOX + i*(1+self.H_BOX+self.P_BETWEEN) + self.P_BETWEEN)
            o_to = (self.w - self.PADDING - self.M_RIGHT, o_from[1])
            self.canvas.add(self.canvas.line(start=o_from, end=o_to, stroke='black'))

            # - resource descriptions
            # -> text (p_i)
            p_left = self.PADDING + self.M_LEFT - self.W_DESC + self.W_DESC_BOX + self.P_SPACE  # +5 not to be too close to box
            p_top = int(self.PADDING + i*(2*self.H_BOX+1 + self.P_BETWEEN) + self.H_BOX + self.FONT_SIZE/2 + self.P_BETWEEN)
            self.canvas.add(self.canvas.text(''.join(["P", str(i + 1)]), insert=(p_left, p_top),
                                             style=''.join(["font-size:", str(self.FONT_SIZE), "px"])))
            # -> text (tasks, modes)
            p_left = self.PADDING + self.M_LEFT - self.W_DESC + self.W_DESC_BOX + self.FONT_SIZE*2
            p_top = int(self.PADDING + i*(2*self.H_BOX+1 + self.P_BETWEEN) + self.H_BOX/2 + self.FONT_SMALL/2 + self.P_BETWEEN)
            self.canvas.add(self.canvas.text("TASKS", insert=(p_left, p_top),
                                             style=''.join(["font-size:", str(int(self.FONT_SMALL)), "px"])))
            self.canvas.add(self.canvas.text("MODES", insert=(p_left, p_top+self.H_BOX+1),
                                             style=''.join(["font-size:", str(int(self.FONT_SMALL)), "px"])))
            # -> colorized rectangles (identifying resource)
            p_left = self.PADDING + self.M_LEFT - self.W_DESC
            p_top = self.PADDING + i*(2*self.H_BOX+1 + self.P_BETWEEN) + self.H_BOX - int(self.W_DESC_BOX/2) + self.P_BETWEEN + 3
            self.canvas.add(self.canvas.rect((p_left, p_top), (self.W_DESC_BOX, self.W_DESC_BOX),
                                              fill=self.res_clrs[i].hex, stroke='black', style="stroke-width:2px"))

    def add_task(self, n, j, s, p):
        """
        Add a single task to this Gantt chart
        :param n: index of resource
        :param j: index of task
        :param s: start time
        :param p: processing time
        :return: None
        """
        left_shift = self.PADDING + self.M_LEFT + s * (self.get_chart_w() / self.l)
        top_shift = self.PADDING + n*(2*self.H_BOX + 1 + self.P_BETWEEN) + self.P_BETWEEN
        processing_len = p * (self.get_chart_w() / self.l)

        # rectangle
        self.canvas.add(self.canvas.rect((left_shift, top_shift), (processing_len, self.H_BOX),
                                          fill=self.task_clrs[j].hex))

        # lines
        self.canvas.add(self.canvas.line(start=(left_shift, top_shift - 10),
                                         end=(left_shift, top_shift + self.H_BOX),
                                        stroke='black'))
        # start time
        self.canvas.add(self.canvas.text(str(int(round(s, 0))),
                                         insert=(left_shift + self.FONT_SMALL/4, top_shift - 13),
                                         transform="".join(["rotate(-80,", str(left_shift + self.FONT_SMALL / 4), ",",
                                                           str(top_shift - 13), ")"]),
                                         style=''.join(["font-size:", str(int(self.FONT_SMALL)), "px"])))

        # task ID
        self.canvas.add(self.canvas.text(str(j),
                                         insert=(left_shift + int(processing_len/2), top_shift + int(self.H_BOX/2 + self.FONT_SMALL/2)),
                                         style=''.join(["font-size:", str(int(self.FONT_SMALL)), "px"])))

    def add_mode(self, n, m, s, p, trans):
        """
        Add a single mode to this Gantt
        :param n: id of resource
        :param m: id of mode
        :param s: start time
        :param p: processing time
        :param trans: indicates if this mode is transition mode
        :return: None
        """
        left_shift = self.PADDING + self.M_LEFT + s * (self.get_chart_w() / self.l)
        top_shift = self.PADDING + n*(2*self.H_BOX + 1 + self.P_BETWEEN) + self.P_BETWEEN + 1 + self.H_BOX
        processing_len = p * (self.get_chart_w() / self.l)
        height = self.H_BOX

        if trans:
            top_shift += self.P_BOX
            height -= 2*self.P_BOX
            clr = "#000000"
        else:
            clr = self.mode_clrs[m].hex

        # rectangle
        self.canvas.add(self.canvas.rect((left_shift, top_shift), (processing_len, height),
                                          fill=clr))

        # recompute - line and text for trans are same as for not trans
        left_shift = self.PADDING + self.M_LEFT + s * (self.get_chart_w() / self.l)
        top_shift = self.PADDING + n * (2 * self.H_BOX + 1 + self.P_BETWEEN) + self.P_BETWEEN + 1 + self.H_BOX
        # line
        self.canvas.add(self.canvas.line(start=(left_shift, top_shift),
                                         end=(left_shift, top_shift + self.H_BOX + 10),
                                         stroke='black'))
        # start time
        self.canvas.add(self.canvas.text(str(int(round(s, 0))),
                                         insert=(left_shift + self.FONT_SMALL/4, top_shift + self.H_BOX + 13),
                                         #transform="".join(["rotate(-80,", str(left_shift + self.FONT_SMALL / 4), ",",
                                         #                  str(top_shift - 13), ")"]),
                                         style=''.join(["font-size:", str(int(self.FONT_SMALL)), "px"])))

        if not trans:
            self.canvas.add(self.canvas.text(str(m),
                                             insert=(left_shift + int(processing_len / 2),
                                                     top_shift + int(self.H_BOX / 2 + self.FONT_SMALL / 2)),
                                             style=''.join(["font-size:", str(int(self.FONT_SMALL)), "px"])))

    def get_chart_w(self):
        """ Returns width of area, where tasks/modes are displayed """
        return self.w - 2*self.PADDING - self.M_LEFT - self.M_RIGHT

    def save_image(self):
        self.canvas.save()

