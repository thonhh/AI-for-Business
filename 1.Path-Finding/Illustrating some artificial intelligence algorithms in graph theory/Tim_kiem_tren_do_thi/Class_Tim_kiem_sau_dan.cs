using System;
using System.Windows.Forms;

namespace Tri_tue_nhan_tao
{
	/// <summary>
	/// Summary description for Class_Tim_kiem_sau_dan.
	/// </summary>
	public class Class_Tim_kiem_sau_dan
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
		private int[] truoc;
		private int n0,n1;
		private int do_sau_Max=10;
		public Class_Tim_kiem_sau_dan()
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
		private bool Tim_kiem_co_gioi_han_do_sau(int Start,int Goal,int do_sau)
		{
			truoc=new int[Class_MienDoThi.dinh_Max+1];
			bool tim_duoc=false;
			for (int i=0;i<=Class_MienDoThi.dinh_Max;i++)
				truoc[i]=0;
			Class_Stack Open=new Class_Stack(),Close=new Class_Stack();
			int[] Mang_do_sau=new int[Class_MienDoThi.dinh_Max+1];
			n0=Start;
			n1=Goal;
			Mang_do_sau[Start]=0;
			Open.Push(n0);
			int n;
			richTextBox_Open.Text+=Open.List_Stack()+ "Độ sâu "+do_sau.ToString()+"\n";
			richTextBox_Close.Text+="\n";
			richTextBox_n.Text+="\n";
			richTextBox_Tn.Text+="\n";
			while (!Open.Is_empty())
			{
				n=Open.Pop();
				richTextBox_n.Text+=n.ToString()+"\n";
				if (n==n1)
				{
					richTextBox_Tn.Text+="Đến đích -> dừng"+"\n";
					tim_duoc=true;
					break;
				}
				Close.Push(n);
				richTextBox_Close.Text+=Close.List_Stack()+"\n";
				if (Mang_do_sau[n]<do_sau)
				{
					for (int i=1;i<=Class_MienDoThi.dinh_Max;i++)
					{
						if (Class_MienDoThi.Kiem_tra_dinh_thuoc_DThi(i)&& Class_MienDoThi.Kiem_tra_cung_thuoc_DThi(n,i))
						{
							richTextBox_Tn.Text+=i.ToString()+" ";
							if ((!Open.In_Stack(i))&&(!Close.In_Stack(i)))
							{
								Open.Push(i);
								truoc[i]=n;
								Mang_do_sau[i]=Mang_do_sau[n]+1;
							}
						}
					}
					richTextBox_Tn.Text+="\n";
					richTextBox_Open.Text+=Open.List_Stack()+"\n"; 
				}
				else
				{
					richTextBox_Tn.Text+=" Quá độ sâu ("+ do_sau.ToString()+")" +"\n";
					richTextBox_Open.Text+=Open.List_Stack()+"\n";
				}
			}
			if (tim_duoc)
			{
				richTextBox_Kq.Text="Đường đi theo DDS : ";
				inkq(n1);
				return true;
			}
			else
			{
				return false;
			}
		}
		public void Tim_kiem_sau_dan()
		{
			Khoi_tao();
			FormInput form=new FormInput("Nhập độ sâu tối đa");
			form.ShowDialog();
			if (FormInput.OK)
				do_sau_Max=FormInput.gia_tri;
			else
				MessageBox.Show("Mặc định độ sâu tối đa là 10");
			bool ktra=false;
			for (int i=1;i<=do_sau_Max;i++)
				if (Tim_kiem_co_gioi_han_do_sau(Class_MienDoThi.chiso_1,Class_MienDoThi.chiso_2,i))
				{
					ktra=true;
					break;
				}
			if (!ktra)
				richTextBox_Kq.Text="Không tìm thấy đường đi!";
		}
	}
}
