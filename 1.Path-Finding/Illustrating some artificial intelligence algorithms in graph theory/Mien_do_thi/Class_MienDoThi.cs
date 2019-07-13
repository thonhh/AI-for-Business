using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO; 

namespace Tri_tue_nhan_tao
{
	/// <summary>
	/// Summary description for Class_MienDoThi.
	/// </summary>
	public class Class_MienDoThi : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		/// 
		//static
		public static ArrayList DS_Dinh=new ArrayList();
		public static Class_DanhSachCung dsach_cung=new Class_DanhSachCung();
		public static bool cho_phep_tao_dinh=true;
		public static int dinh_Max=0;
		public static int dem_qua_2_dinh=0;//m?i l?n ch? cho phép ch?n 2 d?nh
		public static int chiso_1=0,chiso_2=0;
		public static string path="";
		//public
		//private
		private bool lan_dau=true;
		private Point Diem_dau,Diem_cuoi;
		private System.ComponentModel.Container components = null;
		public Class_MienDoThi()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// Class_MienDoThi
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Cursor = System.Windows.Forms.Cursors.Hand;
			this.Name = "Class_MienDoThi";
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Class_MienDoThi_Paint);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Class_MienDoThi_MouseDown);

		}
		#endregion
		public static void Giai_phong_Static()
		{
			foreach (Class_DinhDoThi obj in DS_Dinh)
				Class_DinhDoThi.ctrl.Controls.Remove(obj.MyPic);
			DS_Dinh.Clear();
			dsach_cung.DS_Cung.Clear();
			dinh_Max=0;
			Class_DinhDoThi.ctrl.Refresh();
		}
		public static void Binh_thuong_DThi()
		{
			int i=0;
			if (chiso_1!=0)
			{
				foreach(Class_DinhDoThi obj in DS_Dinh)
				{
					if (obj.da_click)
					{
						obj.Tro_ve_binh_thuong();
						obj.da_click=false;
						i++;
					}
					if (i==2)
						break;
				}
				chiso_1=chiso_2=dem_qua_2_dinh=0;
            }
		}
		public void Them_dinh_vao_do_thi(int chiso,int X,int Y,int h)
		{
			Class_DinhDoThi.Cao=this.Height;
			Class_DinhDoThi.Rong=this.Width;
			Class_DinhDoThi.ctrl=this;
			path=Application.StartupPath+"\\Images";
			Class_DinhDoThi obj=new Class_DinhDoThi(chiso,h,X,Y,path);
			DS_Dinh.Add(obj);
			this.Controls.Add(obj.MyPic);
		}
		public static void Them_Cung_vao_do_thi(int chiso1,int chiso2, int trongso)
		{
			int x1=0,y1=0,x2=0,y2=0,kt=0;
			foreach (Class_DinhDoThi obj in DS_Dinh)
			{
				if (obj.chiso==chiso1)
				{
					x1=obj.Toa_doX+8;
					y1=obj.Toa_doY+8;
					kt++;
				}
				if (obj.chiso==chiso2)
				{
					x2=obj.Toa_doX+8;
					y2=obj.Toa_doY+8;
					kt++;
				}
				if (kt==2)
					break;
			}
			Class_Cung cung=new Class_Cung(x1,y1,x2,y2,chiso1,chiso2,trongso); 
			dsach_cung.Add(cung);
		}
		public static bool Kiem_tra_dinh_thuoc_DThi(int chso)
		{
			bool kt=false;
			foreach (Class_DinhDoThi obj in DS_Dinh) 
				if (obj.chiso==chso)
				{
					kt=true;
					break;
				}
			return kt;
		}
		public void Class_MienDoThi_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ((e.Button==MouseButtons.Left)&& cho_phep_tao_dinh)
			{
				dinh_Max++;
				Them_dinh_vao_do_thi(dinh_Max,e.X,e.Y,0);
			}
		}

		public void Class_MienDoThi_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			dsach_cung.Draw(e.Graphics);
		}
		public static bool Kiem_tra_cung_thuoc_DThi(int x,int y)
		{
			bool kt=false;
			foreach(Class_Cung obj in dsach_cung.DS_Cung)
				if (((obj.dinh1==x)&&(obj.dinh2==y))||((obj.dinh1==y)&&(obj.dinh2==x)))
				{
					kt=true;
					break;
				}
			return kt;
		}
		public static int Kiem_tra_cung_thuoc_DThi1(int x,int y)
		{
			int kt=0;
			
			foreach(Class_Cung obj in dsach_cung.DS_Cung)
				if (((obj.dinh1==x)&&(obj.dinh2==y))||((obj.dinh1==y)&&(obj.dinh2==x)))
				{
					kt=obj.trong_so;
					break;
				}
			return kt;
		}
		public static int Get_Trong_so_Cung(int x,int y)
		{
			int ts=0;
			foreach (Class_Cung obj in dsach_cung.DS_Cung)
				if (((obj.dinh1==x)&&(obj.dinh2==y))||((obj.dinh1==y)&&(obj.dinh2==x)))
				{
					ts=obj.trong_so;
					break;
				}
			return ts;
		}
		public static void Set_Trong_so_Cung(int x,int y,int ts)
		{
			foreach (Class_Cung obj in dsach_cung.DS_Cung)
				if (((obj.dinh1==x)&&(obj.dinh2==y))||((obj.dinh1==y)&&(obj.dinh2==x)))
				{
					obj.trong_so=ts;
					break;
				}
		}
		public static int Get_Heuristic(int i)
		{
			int temp=0;
			foreach (Class_DinhDoThi obj in Class_MienDoThi.DS_Dinh)
				if (obj.chiso==i)
				{
					temp=obj.heuristic;
					break;
				}
			return temp;
		}
		public static bool Kiem_tra_Cung_da_qua(int x,int y)
		{
			bool kt=false;
			foreach (Class_Cung obj in dsach_cung.DS_Cung)
				if (((obj.dinh1==x)&&(obj.dinh2==y))||((obj.dinh1==y)&&(obj.dinh2==x)))
				{
					kt=obj.da_di_qua;
					break;
				}
			return kt;
		}
		public static bool Kiem_tra_Cung_di_qua_dinh(Class_Cung obj, int x)
		{
			return ((obj.dinh1==x)||(obj.dinh2==x));
		}
		public static bool Chon_dinh_ke_tot_nhat(int n,ref int i)
		{
			int Max=-1;
			bool kt=false;
			foreach (Class_Cung obj in dsach_cung.DS_Cung)
				if (Kiem_tra_Cung_di_qua_dinh(obj,n) && !obj.da_di_qua && (obj.trong_so>Max))
				{
					Max=obj.trong_so;
					if (obj.dinh1==n)
						i=obj.dinh2;
					else
						i=obj.dinh1;
					kt=true;
				}
			foreach (Class_Cung obj in dsach_cung.DS_Cung)
				if (Kiem_tra_Cung_di_qua_dinh(obj,n) && !obj.da_di_qua &&(obj.trong_so==Max))
					obj.da_di_qua=true;
			return kt;
		}
		public static void Bo_di_qua_cac_Cung()
		{
			foreach (Class_Cung obj in dsach_cung.DS_Cung)
				obj.da_di_qua=false;
		}
		public static void Dat_dinh_Va(int i)
		{
			foreach (Class_DinhDoThi obj in Class_MienDoThi.DS_Dinh)
				if (obj.chiso==i)
				{
					obj.dinh_Va=true;
					obj.Bien_dang(@"\dinh3.bmp",Color.White);
					break;
				}
		}
		public static void Dat_dinh_So_cap(int i)
		{
			foreach (Class_DinhDoThi obj in Class_MienDoThi.DS_Dinh)
				if (obj.chiso==i)
				{
					obj.giai_duoc=obj.luu_giai_duoc=true;
					obj.khong_giai_duoc=obj.luu_khong_giai_duoc=false;
					obj.Bien_dang(@"\dinh4.bmp",Color.Red);
					break;
				}
		}
		public static void Dat_lai_kha_nang_Giai_duoc()
		{
			foreach (Class_DinhDoThi obj in Class_MienDoThi.DS_Dinh)
			{
				obj.giai_duoc=obj.luu_giai_duoc;
				obj.khong_giai_duoc=obj.luu_khong_giai_duoc;
			}
		}
	}
}
