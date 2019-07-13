using System;
using System.Windows.Forms;
using System.Drawing;
namespace Tri_tue_nhan_tao
{
	/// <summary>
	/// Summary description for Class_DinhDoThi.
	/// </summary>
	public class Class_DinhDoThi
	{
		//static
		public static Class_MienDoThi ctrl=new Class_MienDoThi();//ctrl làm vùng vẽ đồ thị 
		public static bool cho_phep_keo=true,cho_phep_click=false,dang_xoa_dinh=false,dang_xoa_cung=false,dang_tao_cung=true;
		public static bool dang_doi_trong_so=false;
		public static int Rong,Cao;//chiều rộng và cao của vùng vẽ đồ thị
		public static int dem_qua_2_dinh=0;//mỗi lần chỉ cho phép chọn 2 đỉnh
		public static int chiso_1=0,chiso_2=0,trso=0;//chỉ số 2 đỉnh được chọn, trọng số sẽ nhập cho cung
		public static int trong_so=0;
		public static Point dinh_chon_1=new Point(0,0),dinh_chon_2=new Point(0,0);//tạo độ 2 đỉnh đang chọn
		//public
		public System.Windows.Forms.PictureBox MyPic;
		public int chiso,Toa_doX,Toa_doY;
		public bool da_click=false;//đối tượng đỉnh đã được click hay chọn
		//private
		private string path;//đường dẫn file ảnh đỉnh
		private int khoa=0;
		private bool lan_dau=true;
		private System.Drawing.Point Diem_dau;
		private System.Windows.Forms.Label MyLab;
		public Class_DinhDoThi()
		{}
		public Class_DinhDoThi(int chso,int X,int Y,string v_path)
		{
			//
			// TODO: Add constructor logic here
			//
			Khoi_tao(chso,X,Y,v_path);
		}
		private void Khoi_tao(int chso,int X,int Y,string v_path)
		{
			path=v_path;
			Toa_doX=X-8;//tren trai cua mypic
			Toa_doY=Y-8;
			chiso=chso;
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
					if (khoa==3)
					{
						MyPic.Location=new Point(x,y);
						int n=Class_MienDoThi.dsach_cung.DS_Cung.Count;
						for (int i=n-1;i>=0;i--)
						{
							Class_Cung obj;
							obj=(Class_Cung)Class_MienDoThi.dsach_cung.DS_Cung[i];
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
					if (chiso==chiso_1)
						dinh_chon_1=new Point(Toa_doX,Toa_doY);
					else if (chiso==chiso_2)
						dinh_chon_2=new Point(Toa_doX,Toa_doY);
					Diem_dau=new Point(e.X,e.Y);
					khoa++;
					
				}
			}
			else
				lan_dau=true;
		}
		public void Tro_ve_binh_thuong()
		{
			MyPic.Image=System.Drawing.Image.FromFile(@path+@"\dinh.bmp",false);
			Bitmap bmp=new Bitmap(@path+@"\dinh.bmp"); 
			Class_Tao_Control_Anh.Tao_DK_AnhBmp(MyPic,bmp);
			MyLab.ForeColor=System.Drawing.Color.White;
			MyLab.Left=6;
		}
		private void Bien_dang()
		{
			MyPic.Image=System.Drawing.Image.FromFile(@path+@"\dinh2.bmp",false);
			Bitmap bmp=new Bitmap(@path+@"\dinh2.bmp"); 
			Class_Tao_Control_Anh.Tao_DK_AnhBmp(MyPic,bmp);
			MyLab.ForeColor=System.Drawing.Color.Black;
			MyLab.Left=5;
		}
		private void MyPic_MouseDown(object sender,MouseEventArgs e)
		{
			if (cho_phep_click)
			{
				if (dang_tao_cung||dang_xoa_cung)
				{
					if (da_click)
					{
						dem_qua_2_dinh--;
						da_click=false;
						Tro_ve_binh_thuong();
						if (chiso_1==chiso)
						{
							chiso_1=chiso_2;
							chiso_2=0;
							dinh_chon_1=new Point(dinh_chon_2.X,dinh_chon_2.Y);
							dinh_chon_2=new Point(0,0);
						}
						else 
						{
							chiso_2=0;
							dinh_chon_2=new Point(0,0);
						}
					}
					else if (dem_qua_2_dinh<2)
					{
						dem_qua_2_dinh++;
						da_click=true;
						Bien_dang();
						if (chiso_1==0)
						{
							chiso_1=chiso;
							dinh_chon_1=new Point(Toa_doX,Toa_doY);
						}
						else
						{
							chiso_2=chiso;
							dinh_chon_2=new Point(Toa_doX,Toa_doY);
							Class_Cung cung=new Class_Cung(dinh_chon_1.X+8,dinh_chon_1.Y+8,dinh_chon_2.X+8,dinh_chon_2.Y+8,chiso_1,chiso_2,trso); 
							if (dang_tao_cung && !ctrl.Kiem_tra_cung_thuoc_DThi(chiso_1,chiso_2))
								Class_MienDoThi.dsach_cung.Add(cung);
							else if ((dang_xoa_cung)&&(ctrl.Kiem_tra_cung_thuoc_DThi(chiso_1,chiso_2))&&(MessageBox.Show("Bạn có chắc chắn xóa cung nối "+chiso_1.ToString()+" với "+ chiso_2.ToString()+ " không ?","...",MessageBoxButtons.YesNo)==DialogResult.Yes))
							{
								Class_MienDoThi.dsach_cung.ReMove(chiso_1,chiso_2);
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
						if ((obj.dinh1==chiso)||(obj.dinh2==chiso))
							Class_MienDoThi.dsach_cung.ReMove(obj.dinh1,obj.dinh2);
					}
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
