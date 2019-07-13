using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
namespace Tri_tue_nhan_tao
{
	/// <summary>
	/// Summary description for Class_DinhDoThi.
	/// </summary>
	public class Class_DinhDoThi
	{
		//static
		public static UserControl ctrl=new Class_MienDoThi();//ctrl làm vùng vẽ đồ thị 
		public static bool cho_phep_keo=true,cho_phep_click=false,dang_xoa_dinh=false,dang_xoa_cung=false,dang_tao_cung=false,dang_chon_dinh=false,dang_them_con=false,dang_bot_con=false,cho_tao_heurstic=false;
		public static bool dang_doi_trong_so=false;
		public static int Rong,Cao;//chiều rộng và cao của vùng vẽ đồ thị
		public static int trso=0,h=0;//trọng số sẽ nhập cho cung
		//public static Point dinh_chon_1=new Point(0,0),dinh_chon_2=new Point(0,0);//tạo độ 2 đỉnh đang chọn
		//public
		public ArrayList DS_Dinh_con=new ArrayList();
		public System.Windows.Forms.PictureBox MyPic;
		public int chiso,Toa_doX,Toa_doY,heuristic=0;
		public bool da_click=false;//đối tượng đỉnh đã được click hay chọn
		public bool giai_duoc=false,dinh_Va=false,khong_giai_duoc=false,luu_giai_duoc=false,luu_khong_giai_duoc=false;
		public bool Dinh_la=false;
		//private
		//private FormInput obj=new FormInput();
		private string path;//đường dẫn file ảnh đỉnh
		private int khoa=0;
		private bool lan_dau=true;
		private System.Drawing.Point Diem_dau;
		private System.Windows.Forms.Label MyLab;
		public Class_DinhDoThi()
		{}
		public Class_DinhDoThi(int chso,int h,int X,int Y,string v_path)
		{
			//
			// TODO: Add constructor logic here
			//
			Khoi_tao(chso,h,X,Y,v_path);
		}
		private void Khoi_tao(int chso,int h,int X,int Y,string v_path)
		{
			path=v_path;
			Toa_doX=X-8;//tren trai cua mypic
			Toa_doY=Y-8;
			chiso=chso;
			heuristic=h;
			//Tao cac thuoc tinh cho MyPic
			MyPic=new System.Windows.Forms.PictureBox();
			MyPic.Location=new System.Drawing.Point(Toa_doX,Toa_doY);
			MyPic.Image=System.Drawing.Image.FromFile(@path+@"\dinh.bmp",false);
			MyPic.Width=18;
			MyPic.Height=18;
			//Tao MyPic dung dang cua anh bmp
			Bitmap bmp=new Bitmap(@path+@"\dinh.bmp"); 
			Class_Tao_Control_Anh.Tao_DK_AnhBmp(MyPic,bmp);
			MyPic.MouseDown +=new System.Windows.Forms.MouseEventHandler(MyPic_MouseDown);
			MyPic.MouseMove += new MouseEventHandler(MyPic_MouseMove);
			//Tao thuoc tinh cho MyLab
			MyLab=new System.Windows.Forms.Label();
			MyLab.MouseDown +=new MouseEventHandler(MyLab_MouseDown);
			MyLab.MouseMove +=new MouseEventHandler(MyLab_MouseMove);
			MyLab.Text=chiso.ToString();
			MyLab.TextAlign=ContentAlignment.MiddleCenter;
			MyLab.Font= new System.Drawing.Font("Arial", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(2))); 
			MyLab.ForeColor=System.Drawing.Color.White;
			MyLab.BackColor=System.Drawing.Color.Transparent;
			MyLab.Height=10;
			MyLab.Width=12;
			MyLab.Top=6;
			MyLab.Left=6;
			MyPic.Controls.Add(MyLab);
		}
		private void MyPic_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ((e.Button==MouseButtons.Left)&& cho_phep_keo)
			{
				if (lan_dau)
				{
					Diem_dau=new Point(e.X,e.Y);
					lan_dau=false;
				}
				else
				{
					int x=MyPic.Location.X+e.X-Diem_dau.X,y=MyPic.Location.Y+e.Y-Diem_dau.Y;
					if (MyPic.Location.X+e.X-Diem_dau.X<0)
						x=0;
					if (MyPic.Location.X+e.X-Diem_dau.X>Rong-25)
						x=Rong-25;
					if (MyPic.Location.Y+e.Y-Diem_dau.Y<0)
						y=0;
					if (MyPic.Location.Y+e.Y-Diem_dau.Y>Cao-25)
						y=Cao-25;
					if (khoa==2)
					{
						MyPic.Location=new Point(x,y);
						foreach (Class_Cung obj in Class_MienDoThi.dsach_cung.DS_Cung)
						{
							if (obj.dinh1==chiso)
								obj.ChangeStart(x+8,y+8);
							if (obj.dinh2==chiso)
								obj.ChangeEnd(x+8,y+8);
						}
						ctrl.Refresh();
						khoa=0;
					}
					Toa_doX=MyPic.Location.X;
					Toa_doY=MyPic.Location.Y;
					khoa++;
					
				}
			}
			else
				lan_dau=true;
		}
		/*public bool Co_dinh_ke()
		{
			bool kt=false;
			foreach (Class_DinhDoThi obj in Class_MienDoThi.DS_Dinh)
				if (Class_MienDoThi.Kiem_tra_cung_thuoc_DThi(this.chiso,obj.chiso))
				{
					kt=true;
					break;
				}
			return kt;
		}*/
		public void Tro_ve_binh_thuong()
		{
			if (!this.dinh_Va && !this.giai_duoc)
			{
				MyPic.Image=System.Drawing.Image.FromFile(@path+@"\dinh.bmp",false);
				Bitmap bmp=new Bitmap(@path+@"\dinh.bmp"); 
				Class_Tao_Control_Anh.Tao_DK_AnhBmp(MyPic,bmp);
				MyLab.ForeColor=System.Drawing.Color.White;
				MyLab.Left=6;
			}
			else if (this.giai_duoc)
			{
				MyPic.Image=System.Drawing.Image.FromFile(@path+@"\dinh4.bmp",false);
				Bitmap bmp=new Bitmap(@path+@"\dinh4.bmp"); 
				Class_Tao_Control_Anh.Tao_DK_AnhBmp(MyPic,bmp);
				MyLab.ForeColor=System.Drawing.Color.Red;
				MyLab.Left=6;
			}
			else
			{
				MyPic.Image=System.Drawing.Image.FromFile(@path+@"\dinh3.bmp",false);
				Bitmap bmp=new Bitmap(@path+@"\dinh3.bmp"); 
				Class_Tao_Control_Anh.Tao_DK_AnhBmp(MyPic,bmp);
				MyLab.ForeColor=System.Drawing.Color.White;
				MyLab.Left=6;
			}
		}
		public void Bien_dang(string st,Color color)
		{
			MyPic.Image=System.Drawing.Image.FromFile(@path+@st,false);
			Bitmap bmp=new Bitmap(@path+@st); 
			Class_Tao_Control_Anh.Tao_DK_AnhBmp(MyPic,bmp);
			MyLab.ForeColor=color;
			MyLab.Left=5;
		}
		public void Them_con(int x)
		{
			bool kt=true;
			foreach (int i in DS_Dinh_con)
				if (i==x)
				{
					kt=false;
					break;
				}
			if (kt)
				DS_Dinh_con.Add(x);
		}
		public void Bot_con(int x)
		{
			foreach (int i in DS_Dinh_con)
				if (i==x)
				{
					DS_Dinh_con.Remove(x);
					break;
				}
		}
		public bool Thuoc_DS_con(int x,int y)
		{
			bool kt=false;
			foreach (Class_DinhDoThi obj in 
				Class_MienDoThi.DS_Dinh)
				if (obj.chiso==x)
				{
					foreach (int i in obj.DS_Dinh_con)
						if (i==y)
						{
							kt=true;
							break;
						}
					break;
				}
			return kt;
		}
		private void MyPic_MouseDown(object sender,MouseEventArgs e)
		{
			if (cho_phep_click)
			{
				if (dang_tao_cung||dang_xoa_cung||dang_chon_dinh||dang_doi_trong_so||dang_bot_con||dang_them_con)
				{
					if (da_click)
					{
						Class_MienDoThi.dem_qua_2_dinh--;
						da_click=false;
						Tro_ve_binh_thuong();
						if (Class_MienDoThi.chiso_1==chiso)
						{
							Class_MienDoThi.chiso_1=Class_MienDoThi.chiso_2;
							Class_MienDoThi.chiso_2=0;
						}
						else 
						{
							Class_MienDoThi.chiso_2=0;
						}
					}
					else if (Class_MienDoThi.dem_qua_2_dinh<2)
					{
						Class_MienDoThi.dem_qua_2_dinh++;
						da_click=true;
						string s=@"\dinh2.bmp";
						Bien_dang(s,Color.Black);
						if (Class_MienDoThi.chiso_1==0)
						{
							Class_MienDoThi.chiso_1=chiso;
							if (cho_tao_heurstic)
								heuristic=h;
						}
						else
						{
							Class_MienDoThi.chiso_2=chiso;
							if (dang_tao_cung && !Class_MienDoThi.Kiem_tra_cung_thuoc_DThi(Class_MienDoThi.chiso_1,Class_MienDoThi.chiso_2))
							{
								Class_MienDoThi.Them_Cung_vao_do_thi(Class_MienDoThi.chiso_1,Class_MienDoThi.chiso_2,trso);
							}
							else if ((dang_xoa_cung)&&(Class_MienDoThi.Kiem_tra_cung_thuoc_DThi(Class_MienDoThi.chiso_1,Class_MienDoThi.chiso_2))&&(MessageBox.Show("Bạn có chắc chắn xóa cung nối "+Class_MienDoThi.chiso_1.ToString()+" với "+ Class_MienDoThi.chiso_2.ToString()+ " không ?","...",MessageBoxButtons.YesNo)==DialogResult.Yes))
							{
								Class_MienDoThi.dsach_cung.ReMove(Class_MienDoThi.chiso_1,Class_MienDoThi.chiso_2);
							}
							else if (dang_doi_trong_so && Class_MienDoThi.Kiem_tra_cung_thuoc_DThi(Class_MienDoThi.chiso_1,Class_MienDoThi.chiso_2))
							{
								FormInput form1=new FormInput("Nhập trọng số mới");
								form1.ShowDialog();
								if (FormInput.OK)
								{
									Class_MienDoThi.Set_Trong_so_Cung(Class_MienDoThi.chiso_1,Class_MienDoThi.chiso_2,FormInput.gia_tri);
								}
							}
							else if (dang_them_con)
							{
								foreach (Class_DinhDoThi obj in Class_MienDoThi_VH.DS_Dinh)
									if (obj.chiso==Class_MienDoThi_VH.chiso_1)
									{
										obj.Them_con(chiso);
										break;
									}
							}
							else if (dang_bot_con)
							{
								foreach (Class_DinhDoThi obj in Class_MienDoThi_VH.DS_Dinh)
									if (obj.chiso==Class_MienDoThi_VH.chiso_1)
									{
										obj.Bot_con(chiso);
										break;
									}
							}
							ctrl.Refresh();
							
						}
					}
				}
				else if ((dang_xoa_dinh)&& MessageBox.Show("Bạn có chắc chắn xóa đỉnh "+chiso.ToString()+" không ?","...",MessageBoxButtons.YesNo)==DialogResult.Yes)
				{
					int n=Class_MienDoThi.dsach_cung.DS_Cung.Count;
					for (int i=n-1;i>=0;i--)
					{
						Class_Cung obj;
						obj=(Class_Cung)Class_MienDoThi.dsach_cung.DS_Cung[i];
						if (Class_MienDoThi.Kiem_tra_Cung_di_qua_dinh(obj,chiso))
							Class_MienDoThi.dsach_cung.ReMove(obj.dinh1,obj.dinh2);
					}
					foreach (Class_DinhDoThi obj in Class_MienDoThi.DS_Dinh)
						obj.Bot_con(this.chiso);
					Class_MienDoThi.DS_Dinh.Remove(this);
					ctrl.Controls.Remove(this.MyPic);
					ctrl.Refresh();
				}
			}
		}
		private void MyLab_MouseMove(Object sender,MouseEventArgs e)
		{
			MyPic_MouseMove(sender,e);
		}
		private void MyLab_MouseDown(Object sender,MouseEventArgs e)
		{
			MyPic_MouseDown(sender,e);
		}
	}
}
