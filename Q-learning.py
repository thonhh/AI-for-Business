import numpy as np

gamma = 0.75
alpha = 0.9

# PART 1 - DEFINING THE ENVIRONMENT - Định nghĩa môi trường
#  Defining the states - Định nghĩa các trạng thái
location_to_state = {'A':0,
                     'B':1,
                     'C':2,
                     'D':3,
                     'E':4,
                     'F':5,
                     'G': 6,
                     'H': 7,
                     'I': 8,
                     'J': 9,
                     'K': 10,
                     'L': 11
                    }

# Defining the actions - Định nghĩa các hành động
actions = [0,1,2,3,4,5,6,7,8,9,10,11]
# Defining the rewards
R = np.array([  [0,1,0,0,0,0,0,0,0,0,0,0],
                [1,0,1,0,0,1,0,0,0,0,0,0],
                [0,1,0,0,0,0,1,0,0,0,0,0],
                [0,0,0,0,0,0,0,1,0,0,0,0],
                [0,0,0,0,0,0,0,0,1,0,0,0],
                [0,1,0,0,0,0,0,0,0,1,0,0],
                [0,0,1,0,0,0,1000,1,0,0,0,0],
                [0,0,0,1,0,0,1,0,0,0,0,1],
                [0,0,0,0,1,0,0,0,0,1,0,0],
                [0,0,0,0,0,1,0,0,1,0,1,0],
                [0,0,0,0,0,0,0,0,0,1,0,1],
                [0,0,0,0,0,0,0,1,0,0,1,0]])

# PART 2 - BUILDING THE AI SOLUTION WITH Q-LEARNING - Thuật toán Q-learning
# Initializing the Q-Values - khởi tạo mảng Q
Q = np.array(np.zeros([12,12]))
# Implementing the Q-Learning process - thực hiện xử lý Q
for i in range(1000):
    curent_state =  np.random.randint(0,12)
    playable_action = []
    #Duyệt qua tất cả các node (12 đỉnh) -> Nếu R (reward) >0 có nghĩa là có đường đi từ node hiện tại đến node đó
    #bổ sung vào danh sách các node có thể đi tới (action=hành động đi tới)
    for j in range(12):
        if R[curent_state,j]>0:
            playable_action.append(j)
    #chọn ngẫu nghiên 1 node để đi tới-next_state = action = a
    next_state = np.random.choice(playable_action)
    # γ * max/a(Q(s/t+1 , a))->1. duyệt các giá trị a: tìm giá trị lớn nhất của tại state: t+1: | np.argmax(Q[next_state,]) |  ->   2. gamma * giá trị Q tại state t+1 và a đó
    TD = R[curent_state,next_state]+gamma*Q[next_state,np.argmax(Q[next_state,])] - Q[curent_state,next_state]
    #Q[curent_state, next_state] mới  = Q[curent_state,next_state] cũ + alpha*TD
    Q[curent_state, next_state] = Q[curent_state,next_state] + alpha*TD

print("Q-Values:")
print(Q.astype(int))

# Making a mapping from the states to the locations
state_to_location = {state: location for location, state in location_to_state.items()}


# Making the final function that will return the optimal route
def route(starting_location, ending_location):
    route = [starting_location]
    next_location = starting_location
    while (next_location != ending_location):
        #Đổi chữ thành số
        starting_state = location_to_state[starting_location]
        #Lấy index của phần tử có giá trị lớn nhất
        next_state = np.argmax(Q[starting_state,])
        #Đổi số thành chữ
        next_location = state_to_location[next_state]
        #Bổ sung vào tuyến đường
        route.append(next_location)
        #Đặt lại vị trí bắt đầu là vị trí vừa tìm được
        starting_location = next_location
    return route

print('Route:')
route('E','G')
