using System;

namespace Tri_tue_nhan_tao
{
	/// <summary>
	/// Summary description for Class_Tim_kiem_leo_doi.
	/// </summary>
	public class Class_Tim_kiem_leo_doi
	{
		public System.Windows.Forms.RichTextBox richTextBox_n;
		public System.Windows.Forms.RichTextBox richTextBox_Tn;
		public System.Windows.Forms.RichTextBox richTextBox_Open;
		public System.Windows.Forms.RichTextBox richTextBox_Kq;
		public System.Windows.Forms.Label label_n;
		public System.Windows.Forms.Label label_T;
		public System.Windows.Forms.Label label_O;
		private bool tim_duoc=false;
		private Class_Stack Open;
		public Class_Tim_kiem_leo_doi()
		{
			//
			// TODO: Add constructor logic here
			//
			InitializeComponent();
		}
		public void Dispose()
		{
			label_n.Dispose();
			label_O.Dispose();
			label_T.Dispose();
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
			this.richTextBox_Kq = new System.Windows.Forms.RichTextBox();
			this.label_n = new System.Windows.Forms.Label();
			this.label_T = new System.Windows.Forms.Label();
			this.label_O = new System.Windows.Forms.Label();
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
			this.richTextBox_Tn.Size = new System.Drawing.Size(209, 376);
			this.richTextBox_Tn.TabIndex = 2;
			this.richTextBox_Tn.Text = "";
			// 
			// richTextBox_Open
			// 
			this.richTextBox_Open.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.richTextBox_Open.Location = new System.Drawing.Point(539, 32);
			this.richTextBox_Open.Name = "richTextBox_Open";
			this.richTextBox_Open.Size = new System.Drawing.Size(243, 376);
			this.richTextBox_Open.TabIndex = 3;
			this.richTextBox_Open.Text = "";
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
			this.label_T.Location = new System.Drawing.Point(378, 16);
			this.label_T.Name = "label_T";
			this.label_T.Size = new System.Drawing.Size(96, 15);
			this.label_T.TabIndex = 10;
			this.label_T.Text = "Tập đỉnh kề T(n)";
			// 
			// label_O
			// 
			this.label_O.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label_O.ForeColor = System.Drawing.Color.Blue;
			this.label_O.Location = new System.Drawing.Point(592, 16);
			this.label_O.Name = "label_O";
			this.label_O.Size = new System.Drawing.Size(115, 15);
			this.label_O.TabIndex = 11;
			this.label_O.Text = "Trạng thái của Stack";
			// 
		}
		public void Khoi_tao()
		{
			richTextBox_n.Text="";
			richTextBox_Tn.Text="";
			richTextBox_Open.Text="";
			richTextBox_Kq.Text="";
			Open=new Class_Stack();
			Class_MienDoThi.Bo_di_qua_cac_Cung();
		}
		private void inkq()
		{
			for (int i=1;i<=Open.Top;i++)
				richTextBox_Kq.Text+=Open.S[i].ToString()+"->";
			richTextBox_Kq.Text+=Class_MienDoThi.chiso_2.ToString();
		}
		private bool Den_dich(int n,int n1,ref int i)
		{
			bool kt=false;
			for (int j=1;j<=Class_MienDoThi.dinh_Max;j++)
				if (Class_MienDoThi.Kiem_tra_dinh_thuoc_DThi(j) && Class_MienDoThi.Kiem_tra_cung_thuoc_DThi(n,j) && j==n1)
				{
					kt=true;
					i=j;
					break;
				}
			return kt;
		}
		public void Tim_kiem_leo_doi(int n0,int n1)
		{
			richTextBox_n.Text+=n0.ToString()+"\n";
			if (n0==n1)
			{
				richTextBox_Tn.Text+="Đến đích -> dừng"+"\n";
				tim_duoc=true;
			}
			else
			{
				for (int j=1;j<=Class_MienDoThi.dinh_Max;j++)
					if (Class_MienDoThi.Kiem_tra_dinh_thuoc_DThi(j) && Class_MienDoThi.Kiem_tra_cung_thuoc_DThi(n0,j))
						richTextBox_Tn.Text+=j.ToString()+" ";
				richTextBox_Tn.Text+="\n";
				int i=0;
				if (Den_dich(n0,n1,ref i))
				{
					Open.Push(n0);
					richTextBox_Open.Text+=Open.List_Stack()+"\n";
					Tim_kiem_leo_doi(i,n1);
				}
				else if (Class_MienDoThi.Chon_dinh_ke_tot_nhat(n0,ref i))
				{
					Open.Push(n0);
					richTextBox_Open.Text+=Open.List_Stack()+"\n";
					Tim_kiem_leo_doi(i,n1);
				}
				else
				{
					if (!Open.Is_empty())
					{
						richTextBox_Open.Text+=Open.List_Stack()+"\n";
						i=Open.Pop();
						Tim_kiem_leo_doi(i,n1);
					}
				}
			}
			if (tim_duoc)
			{
				richTextBox_Kq.Text="Đường đi theo Climbing-Hill : ";
				inkq();
			}
			else
				richTextBox_Kq.Text="Không tìm thấy đường đi! ";
		}
	}
}
