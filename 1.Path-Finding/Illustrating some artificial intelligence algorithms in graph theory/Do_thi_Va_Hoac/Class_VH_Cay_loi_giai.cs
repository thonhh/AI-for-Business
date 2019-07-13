using System;
using System.Collections;

namespace Tri_tue_nhan_tao
{
	/// <summary>
	/// Summary description for Class_VH_Cay_loi_giai.
	/// </summary>
	public class Class_VH_Cay_loi_giai
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
		// cac thanh phan danh cho thuat toan
		public ArrayList DS_DinhG=new ArrayList();
		public int DinhBatDau;
		public ArrayList Open=new ArrayList();
		public Stack Close=new Stack();
		bool[] kt=new bool[100];
		bool Chiso_max=true;
		bool Chiso_tong=false;
		//for(int k=0;k<100;k++)kt[k]=true;
		
		/// <summary>
		/// Cac thanh phan co ban
		/// </summary>
		public Class_VH_Cay_loi_giai()
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
			this.label_n.Text = "Ðỉnh n";
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
		
        /// <summary>
        /// Bat dau thuat toan tim kiem theo cay loi giai cuc tieu
        /// </summary>

		
		


		bool Dk_Thoat()
		{
			bool kt1=false;
			foreach(int i in DS_DinhG)
				if(kt[i])
				{
					kt1=true;
					break;
				}
			return kt1;
		}
		public void Xay_Dung_cay_loi_giai_G(int chiso)
		{  
			DS_DinhG.Clear();
			DinhBatDau=chiso;
			DS_DinhG.Add(DinhBatDau);
			bool Thoat_while=true;
			int min=10000;
			int k=0;
			for( k=0;k<50;k++)kt[k]=true;
			int l=0;
			int dinhg;
			while (Thoat_while)
			{   
				//foreach(int dinhg in DS_DinhG)//Voi moi dinh thuoc G`
				while (l<DS_DinhG.Count)
				 
				{	
					   dinhg=(int)DS_DinhG[l];
						l++;	
					   if(kt[dinhg])
					   foreach(Class_DinhDoThi dinh in Class_MienDoThi_VH.DS_Dinh)//tim dinh trong G co chi so = chiso cua G`
						   if(dinh.chiso==dinhg)
						   {//chondinh
							    if(dinh.dinh_Va)//if dinhg la dinh va
									foreach(int i in dinh.DS_Dinh_con)//voi moi dinh i ke voi dinhg
										if(Open.Contains(i)||Close.Contains(i))//if i thuoc Open & Close
											 DS_DinhG.Add(i);
							   if(!dinh.dinh_Va)
							   {  //dinh hoac
								   bool gt=true;
								   foreach(int i in dinh.DS_Dinh_con)
									   if(Open.Contains(i)||Close.Contains(i))
									   {  gt=false;
										   foreach(Class_DinhDoThi dinh1 in Class_MienDoThi_VH.DS_Dinh)//tim dinh trong G co chi so = chiso cua G`
											   if(dinh1.chiso==i) 
											   {
												   if(min>dinh1.heuristic+Class_MienDoThi.Kiem_tra_cung_thuoc_DThi1(dinhg,i))
												   {
													   min=dinh1.heuristic+Class_MienDoThi.Kiem_tra_cung_thuoc_DThi1(dinhg,i);
													   k=i;
												   }
												   break;
											   }
									   }
								   if(!gt) DS_DinhG.Add(k);
							   }// dinh hoac

							   /*foreach(int i in dinh.DS_Dinh_con)//voi moi dinh i ke voi dinhg
							   {
								   if(Open.Contains(i)||Close.Contains(i))//if i thuoc Open & Close
								   {
									   if(dinh.dinh_Va)//if dinhg la dinh va
									   {  
										   DS_DinhG.Add(i);
									   }
									   else //if dinhg la dinh hoac
									   {  
										   foreach(Class_DinhDoThi dinh1 in Class_MienDoThi_VH.DS_Dinh)//tim dinh trong G co chi so = chiso cua G`
											   if(dinh1.chiso==i) 
											   {
												   if(min>dinh1.heuristic+Class_MienDoThi.Kiem_tra_cung_thuoc_DThi1(dinhg,i))
												   {
													   min=dinh1.heuristic+Class_MienDoThi.Kiem_tra_cung_thuoc_DThi1(dinhg,i);
													   k=i;
												   }
												   break;
											   }
									   }
								   }
							   }*/
							   kt[dinhg]=false;//danh dau dinh do dinhg da duoc chon
							  //  if(!dinh.dinh_Va) DS_DinhG.Add(k);//them dinhg la hoac vao ds_dinhG
							   break;
						   }//chon dinh
					if(Dk_Thoat()==false)Thoat_while=false;
				}	
			}
	}










		public void Xac_Dinh_lai_HoI(int i)
		{
			int vc=10000;
			int max=0;
			int min=vc;
			int tong=0;
			bool gt;
			foreach(Class_DinhDoThi dinh in Class_MienDoThi_VH.DS_Dinh)
				if(dinh.chiso==i)
				{
					if(dinh.DS_Dinh_con.Count==0)
					{
						if(dinh.giai_duoc) dinh.heuristic=0;
						else if(dinh.khong_giai_duoc)dinh.heuristic=vc;
					}
					else
					{
						gt=true;
						foreach(int j in dinh.DS_Dinh_con)
						{ 
							foreach(Class_DinhDoThi dinh1 in Class_MienDoThi_VH.DS_Dinh)
								if(dinh1.chiso==j)
								{//kiem tra thoc danh sach dinh
									
									if(Open.Contains(j)||Close.Contains(j))
									{//kiem tra thuoc
										gt=false;
										if(dinh.dinh_Va)
										{//dinh va
											if(Chiso_max)
											{
												if(dinh1.heuristic+Class_MienDoThi.Kiem_tra_cung_thuoc_DThi1(i,j)>max)
												{
													max=dinh1.heuristic+Class_MienDoThi.Kiem_tra_cung_thuoc_DThi1(i,j);
												}
											}
											else
												if(Chiso_tong)
											{
												tong+=dinh1.heuristic+Class_MienDoThi.Kiem_tra_cung_thuoc_DThi1(i,j);
											}
										}//dinh va
										else
											if(!dinh.dinh_Va)
										{
											if(dinh1.heuristic+Class_MienDoThi.Kiem_tra_cung_thuoc_DThi1(i,j)<min)
												min=dinh1.heuristic+Class_MienDoThi.Kiem_tra_cung_thuoc_DThi1(i,j);
										}
									}//kiem tra thuoc
									break;
								}//kiem tra thoc danh sach dinh
						}
						if(!gt)
						{
							if(dinh.dinh_Va) 
							{
								if(Chiso_max) dinh.heuristic=max;
								else
									if(Chiso_tong) dinh.heuristic=tong;
							}
							else
								if(!dinh.dinh_Va) dinh.heuristic=min;
						}
					}//for
					break;
				}				
		}
	    
		public void Danh_sach_la_cua_G(ArrayList ds)
		{
			ds.Clear();
			foreach(int i in DS_DinhG)
			{
				foreach(Class_DinhDoThi dinh in Class_MienDoThi_VH.DS_Dinh)
				{
					if(dinh.chiso==i)
					{  
						bool kt=true;//kiem tra xem 1 dinh co fai la la hay khong,gia dinh i la` la'
						foreach(int j in DS_DinhG)//xet tat ca cac dinh trong G if co 1 dinh chua trong tap con cua dinh i thi i  khong la la
							if(dinh.DS_Dinh_con.Contains(j)) 
							{
								kt=false;
								break;
							}
						if(kt) ds.Add(i);//if la` la' thi` cho vao` danh sa'ch la'
						break;//thoat khoi vong lap khi tim duoc 1 dinh co chi so la dinh trong cay tiem tang
					}
				}
			}
		}
		public void Tim_kiem_cay_loi_giai_cuc_tieu()
		{
			Khoi_tao();
			int n=Class_MienDoThi_VH.chiso_1;
			Open.Clear();
			Close.Clear();
			Open.Add(n);
			ArrayList Ds_la=new ArrayList();
			int i=0;
			bool Co_loi_giai=false;
			while(Open.Count>0)
			{//while
				Xay_Dung_cay_loi_giai_G(n);
				Ds_la.Clear();
				Danh_sach_la_cua_G(Ds_la);//??????????????????????????
				foreach(int j in Ds_la)//moi doi tuong thuoc danh sach la if co trong danh sach open thi xoa no khoi open
					if(Open.Contains(j))
					{  
						i=j;
						Open.RemoveAt(Open.IndexOf(j));//xoa la khoi open
						break;
					}
				//foreach(Class_DinhDoThi dinh in Class_MienDoThi_VH.DS_Dinh)
				
				foreach(Class_DinhDoThi dinh in Class_MienDoThi_VH.DS_Dinh)
					if(dinh.chiso==i)
					{//for of break*/
						if(dinh.DS_Dinh_con.Count>0)
							foreach(int j in dinh.DS_Dinh_con)
							{
								Open.Add(j);
								//	Xac_Dinh_lai_HoI(j);
							}
						if(Class_MienDoThi_VH.Giai_duoc(i))
							if(Class_MienDoThi_VH.Giai_duoc(n))
							{
								Co_loi_giai=true;
							}
						if(Class_MienDoThi_VH.Khong_giai_duoc(i))
							if(Class_MienDoThi_VH.Khong_giai_duoc(n))
							{
								Co_loi_giai=false;
							}
						Close.Push(i);//dat no vao Close
						foreach(int j in Close)
							Xac_Dinh_lai_HoI(j);
						break;
					}//for of break*/
			}//while
		}

		///Ket thuc tim  cay toi u
	}
}
