using System;

namespace Tri_tue_nhan_tao
{
	/// <summary>
	/// Summary description for Class_VH_Theo_chieu_sau.
	/// </summary>
	public class Class_VH_Theo_chieu_sau
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
		private int n0;
		public Class_VH_Theo_chieu_sau()
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
		public void Tim_theo_chieu_sau()
		{
			Khoi_tao();
			n0=Class_MienDoThi_VH.chiso_1;//dinh xuat phat
			int n;
			Class_Stack Open=new Class_Stack(),Close=new Class_Stack();//Open va Close
			Open.Push(n0);//dat n0 vao Open
			richTextBox_Open.Text+=Open.List_Stack()+"\n";
			richTextBox_n.Text+="\n";
			richTextBox_Tn.Text+="\n";
			richTextBox_Close.Text+="\n";
			//Xuong 1 dong
			bool thoat_while=false,chung_minh_duoc=false;

			while (!Open.Is_empty()&& !thoat_while)//of Open con khac rong va chua thoat khoi vong while
			{
				n=Open.Pop();//lay 1 phan tu trong Open
				richTextBox_n.Text+=n.ToString()+"\n";//ghi vao cot n
				Close.Push(n);//dat phan tu do vao Close
				richTextBox_Close.Text+=Close.List_Stack()+"\n";//liet ke danh sach va cac dinh trong stack ra cot Close---Close.List_Stack tra ve 1 xau cac phan tu trong Stack
				bool kt=false;
				foreach (Class_DinhDoThi obj in Class_MienDoThi_VH.DS_Dinh)//voi moi dinh trong danh sach dinh
				{
					if (obj.chiso==n)//dinh co chi so = dinh dang xet
					{
						if (obj.DS_Dinh_con.Count>0)//if so luong cac dinh con trong danh sach con >0
						{
							foreach(Class_DinhDoThi obj1 in Class_MienDoThi_VH.DS_Dinh)
								if (obj.Thuoc_DS_con(n,obj1.chiso))
								{
									richTextBox_Tn.Text+=obj1.chiso.ToString()+" ";
									if (!Open.In_Stack(obj1.chiso)&& !Close.In_Stack(obj1.chiso))
										Open.Push(obj1.chiso);
								}
						}
						else
						{
							if (Class_MienDoThi_VH.Giai_duoc(obj.chiso))
							{
								richTextBox_Tn.Text+="Gđ";
								if (Class_MienDoThi_VH.Giai_duoc(n0))
								{
									richTextBox_Tn.Text+="->Gđ "+n0.ToString()+"\n";
									chung_minh_duoc=true;
									thoat_while=true;
								}
							}
							else
							{
								if (Class_MienDoThi_VH.Khong_giai_duoc(obj.chiso))
								{
									richTextBox_Tn.Text+="Kgđ";
									if (Class_MienDoThi_VH.Khong_giai_duoc(n0))
									{
										richTextBox_Tn.Text+="-> Không gđ "+n0.ToString()+"\n";
										chung_minh_duoc=false;
										thoat_while=true;
									}
								}
							}
						}
						break;
					}
				}
				richTextBox_Open.Text+=Open.List_Stack()+"\n";
				richTextBox_Tn.Text+="\n";
			}
			if (chung_minh_duoc)
			{
				richTextBox_Kq.Text="Kết luận: Chứng minh được "+n0.ToString();
				Cay_loi_giai.Giai_phong_Static();
				FormCayLoiGiai formCay=new FormCayLoiGiai();
				Open=new Class_Stack();
				Open.Push(n0);
				foreach(Class_DinhDoThi obj in Class_MienDoThi_VH.DS_Dinh)
					if (obj.chiso==n0)
						formCay.cay_loi_giai1.Them_dinh_vao_do_thi(n0,obj.Toa_doX+8,obj.Toa_doY+8,obj.heuristic);
				while (!Open.Is_empty())
				{
					n=Open.Pop();
					foreach(Class_DinhDoThi obj in Class_MienDoThi_VH.DS_Dinh)
						if (obj.chiso==n)
						{
							if (obj.dinh_Va)
							{
								foreach(Class_DinhDoThi obj1 in Class_MienDoThi_VH.DS_Dinh)
									if (obj1.Thuoc_DS_con(obj.chiso,obj1.chiso))
									{	
										formCay.cay_loi_giai1.Them_dinh_vao_do_thi(obj1.chiso,obj1.Toa_doX+8,obj1.Toa_doY+8,obj1.heuristic);
										Cay_loi_giai.Them_Cung_vao_do_thi(obj.chiso,obj1.chiso,Class_MienDoThi_VH.Get_Trong_so_Cung(obj.chiso,obj1.chiso));
										Open.Push(obj1.chiso);
									}
							}
							else
							{
								foreach(Class_DinhDoThi obj1 in Class_MienDoThi_VH.DS_Dinh)
									if (obj1.Thuoc_DS_con(obj.chiso,obj1.chiso)&& obj1.giai_duoc)
									{
										formCay.cay_loi_giai1.Them_dinh_vao_do_thi(obj1.chiso,obj1.Toa_doX+8,obj1.Toa_doY+8,obj1.heuristic);
										Cay_loi_giai.Them_Cung_vao_do_thi(obj.chiso,obj1.chiso,Class_MienDoThi_VH.Get_Trong_so_Cung(obj.chiso,obj1.chiso));
										Open.Push(obj1.chiso);
										break;
									}
							}
							break;
						}
				}
				formCay.Show();
			}
			else
				richTextBox_Kq.Text="Kết luận: Không chứng minh được "+n0.ToString();
		}
	}
}
