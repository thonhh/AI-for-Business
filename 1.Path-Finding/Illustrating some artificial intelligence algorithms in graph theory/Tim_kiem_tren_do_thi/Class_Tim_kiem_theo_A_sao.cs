using System;

namespace Tri_tue_nhan_tao
{
	/// <summary>
	/// Summary description for Class_Tim_kiem_theo_A_sao.
	/// </summary>
	public class Class_Tim_kiem_theo_A_sao
	{
		public System.Windows.Forms.RichTextBox richTextBox_n;
		public System.Windows.Forms.RichTextBox richTextBox_Tn;
		public System.Windows.Forms.RichTextBox richTextBox_Open;
		public System.Windows.Forms.RichTextBox richTextBox_Close;
		public System.Windows.Forms.RichTextBox richTextBox_Kq;
		public System.Windows.Forms.Label label_n;
		public System.Windows.Forms.Label label_T;
		public System.Windows.Forms.Label label_O;
		public System.Windows.Forms.Label label_C;
		private int[] truoc,g,f;
		private int n0,n1;
		public Class_Tim_kiem_theo_A_sao()
		{
			//
			// TODO: Add constructor logic here
			//
			InitializeComponent();
		}
		public void Dispose()
		{
			label_C.Dispose();
			label_n.Dispose();
			label_O.Dispose();
			label_T.Dispose();
			richTextBox_Close.Dispose();
			richTextBox_Kq.Dispose();
			richTextBox_Open.Dispose();
			richTextBox_n.Dispose();
			richTextBox_Tn.Dispose();
		}
		private void InitializeComponent()
		{
			this.richTextBox_n = new System.Windows.Forms.RichTextBox();
			this.richTextBox_Tn = new System.Windows.Forms.RichTextBox();
			this.richTextBox_Open = new System.Windows.Forms.RichTextBox();
			this.richTextBox_Close = new System.Windows.Forms.RichTextBox();
			this.richTextBox_Kq = new System.Windows.Forms.RichTextBox();
			this.label_n = new System.Windows.Forms.Label();
			this.label_T = new System.Windows.Forms.Label();
			this.label_O = new System.Windows.Forms.Label();
			this.label_C = new System.Windows.Forms.Label();
			// richTextBox_n
			// 
			this.richTextBox_n.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.richTextBox_n.Location = new System.Drawing.Point(300, 32);
			this.richTextBox_n.Name = "richTextBox_n";
			this.richTextBox_n.Size = new System.Drawing.Size(30, 376);
			this.richTextBox_n.TabIndex = 1;
			this.richTextBox_n.Text = "";
			// 
			// richTextBox_Tn
			// 
			this.richTextBox_Tn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.richTextBox_Tn.Location = new System.Drawing.Point(330, 32);
			this.richTextBox_Tn.Name = "richTextBox_Tn";
			this.richTextBox_Tn.Size = new System.Drawing.Size(138, 376);
			this.richTextBox_Tn.TabIndex = 2;
			this.richTextBox_Tn.Text = "";
			// 
			// richTextBox_Open
			// 
			this.richTextBox_Open.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.richTextBox_Open.Location = new System.Drawing.Point(469, 32);
			this.richTextBox_Open.Name = "richTextBox_Open";
			this.richTextBox_Open.Size = new System.Drawing.Size(155, 376);
			this.richTextBox_Open.TabIndex = 3;
			this.richTextBox_Open.Text = "";
			// 
			// richTextBox_Close
			// 
			this.richTextBox_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.richTextBox_Close.Location = new System.Drawing.Point(624, 32);
			this.richTextBox_Close.Name = "richTextBox_Close";
			this.richTextBox_Close.Size = new System.Drawing.Size(160, 376);
			this.richTextBox_Close.TabIndex = 4;
			this.richTextBox_Close.Text = "";
			// 
			// richTextBox_Kq
			// 
			this.richTextBox_Kq.ForeColor = System.Drawing.Color.Blue;
			this.richTextBox_Kq.Location = new System.Drawing.Point(300, 409);
			this.richTextBox_Kq.MaxLength = 200;
			this.richTextBox_Kq.Multiline = false;
			this.richTextBox_Kq.Name = "richTextBox_Kq";
			this.richTextBox_Kq.Size = new System.Drawing.Size(484, 23);
			this.richTextBox_Kq.TabIndex = 6;
			this.richTextBox_Kq.Text = "";
			// 
			// label_n
			// 
			this.label_n.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label_n.ForeColor = System.Drawing.Color.Blue;
			this.label_n.Location = new System.Drawing.Point(296, 16);
			this.label_n.Name = "label_n";
			this.label_n.Size = new System.Drawing.Size(40, 15);
			this.label_n.TabIndex = 9;
			this.label_n.Text = "Đỉnh n";
			// 
			// label_T
			// 
			this.label_T.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label_T.ForeColor = System.Drawing.Color.Blue;
			this.label_T.Location = new System.Drawing.Point(354, 16);
			this.label_T.Name = "label_T";
			this.label_T.Size = new System.Drawing.Size(96, 15);
			this.label_T.TabIndex = 10;
			this.label_T.Text = "Tập đỉnh kề T(n)";
			// 
			// label_O
			// 
			this.label_O.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label_O.ForeColor = System.Drawing.Color.Blue;
			this.label_O.Location = new System.Drawing.Point(495, 16);
			this.label_O.Name = "label_O";
			this.label_O.Size = new System.Drawing.Size(104, 15);
			this.label_O.TabIndex = 11;
			this.label_O.Text = "Danh sách Open( )";
			// 
			// label_C
			// 
			this.label_C.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label_C.ForeColor = System.Drawing.Color.Blue;
			this.label_C.Location = new System.Drawing.Point(648, 16);
			this.label_C.Name = "label_C";
			this.label_C.Size = new System.Drawing.Size(112, 15);
			this.label_C.TabIndex = 12;
			this.label_C.Text = "Danh sách Close( )";
			// 
		}
		public void Khoi_tao()
		{
			richTextBox_n.Text="";
			richTextBox_Tn.Text="";
			richTextBox_Open.Text="";
			richTextBox_Close.Text="";
			richTextBox_Kq.Text="";
		}
		private void inkq(int n)
		{
			if (n!=n0)
				inkq(truoc[n]);
			if (n!=n1)
				richTextBox_Kq.Text+=n.ToString()+"->";
			else
				richTextBox_Kq.Text+=n.ToString();
		}
		private int Chi_so_Min_STACK(Class_Stack St)
		{
			int temp=St.Top;
			for (int i=St.Top;i>=1;i--)
				if (f[St.S[temp]]>f[St.S[i]])
					temp=i;
			return temp;
		}
		public void Tim_kiem_theo_A_sao()
		{
			Khoi_tao();
			bool tim_duoc=false;
			truoc=new int[Class_MienDoThi.dinh_Max+1];
			g=new int[Class_MienDoThi.dinh_Max+1];
			f=new int[Class_MienDoThi.dinh_Max+1];
			for (int i=0;i<=Class_MienDoThi.dinh_Max;i++)
				truoc[i]=0;
			n0=Class_MienDoThi.chiso_1;
			n1=Class_MienDoThi.chiso_2;
			int n;
			f[n0]=0;
			g[n0]=0;
			Class_Stack Open=new Class_Stack(),Close=new Class_Stack();
			Open.Push(n0);
			while (!Open.Is_empty())
			{
				int min=Chi_so_Min_STACK(Open);
				int temp=Open.S[Open.Top];
				Open.S[Open.Top]=Open.S[min];
				Open.S[min]=temp;
				richTextBox_Open.Text+=Open.List_Stack()+ "<"+ Open.S[Open.Top].ToString()+">\n";
				richTextBox_Close.Text+="\n";
				richTextBox_n.Text+="\n";
				richTextBox_Tn.Text+="\n";
				n=Open.Pop();
				richTextBox_n.Text+=n.ToString();
				if (n==n1)
				{
					richTextBox_Tn.Text+="Đến đích -> dừng"+"\n";
					tim_duoc=true;
					break;
				}
				Close.Push(n);
				richTextBox_Close.Text+=Close.List_Stack();
				for (int i=1;i<=Class_MienDoThi.dinh_Max;i++)
				{
					if (Class_MienDoThi.Kiem_tra_dinh_thuoc_DThi(i) && (Class_MienDoThi.Kiem_tra_cung_thuoc_DThi(n,i)))
					{
						richTextBox_Tn.Text+=i.ToString()+" ";
						if ((!Open.In_Stack(i))&&(!Close.In_Stack(i)))
						{
							Open.Push(i);
							g[i]=g[n]+Class_MienDoThi.Get_Trong_so_Cung(n,i);
							f[i]=Class_MienDoThi.Get_Heuristic(i)+g[i];
							truoc[i]=n;
						}
						else if ((!Close.In_Stack(i))&&(f[i]>Class_MienDoThi.Get_Heuristic(i)+g[n]+Class_MienDoThi.Get_Trong_so_Cung(n,i)))
						{
							g[i]=g[n]+Class_MienDoThi.Get_Trong_so_Cung(n,i);
							f[i]=Class_MienDoThi.Get_Heuristic(i)+g[i];
							truoc[i]=n;	
						}
					}
				}
			}
			if (tim_duoc)
			{
				richTextBox_Kq.Text="Đường đi ngắn nhất (theo A*) : ";
				inkq(n1);
				richTextBox_Kq.Text+=" chi phí f= "+f[n1].ToString();
			}
			else
				richTextBox_Kq.Text="Không tìm thấy đường đi! ";
		}

	}
}
