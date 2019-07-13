using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

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
		public static Point d1,d2;
		//public
		//private
		private bool lan_dau=true;
		private Point Diem_dau,Diem_cuoi;
		private string ten_dinh;
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
		public static void Binh_thuong_DThi()
		{
			int i=0;
			if (Class_DinhDoThi.chiso_1!=0)
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
				Class_DinhDoThi.chiso_1=Class_DinhDoThi.chiso_2=Class_DinhDoThi.dem_qua_2_dinh=0;
            }
		}
		private void Them_dinh_vao_do_thi(int X,int Y)
		{
			Class_DinhDoThi.Cao=this.Height;
			Class_DinhDoThi.Rong=this.Width;
			Class_DinhDoThi.ctrl=this;
			++dinh_Max;
			string path=Application.StartupPath+"\\Images";
			Class_DinhDoThi obj=new Class_DinhDoThi(dinh_Max,X,Y,path);
			DS_Dinh.Add(obj);
			this.Controls.Add(obj.MyPic);
		}
		public bool Kiem_tra_dinh_thuoc_DThi(int chso)
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
		private void Class_MienDoThi_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ((e.Button==MouseButtons.Left)&& cho_phep_tao_dinh)
			{
				Them_dinh_vao_do_thi(e.X,e.Y);
			}
		}

		private void Class_MienDoThi_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.Clear(Color.White);
			dsach_cung.Draw(e.Graphics);
			
		}
		public bool Kiem_tra_cung_thuoc_DThi(int x,int y)
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
		public Class_Cung Cung(int x,int y)
		{
			Class_Cung c=new Class_Cung(0,0,0,0,0,0,0);
			foreach (Class_Cung obj in dsach_cung.DS_Cung)
				if (((obj.dinh1==x)&&(obj.dinh2==y))||((obj.dinh1==y)&&(obj.dinh2==x)))
				{
					c=new Class_Cung(obj.startPoint.X,obj.startPoint.Y,obj.endPoint.X,obj.endPoint.Y,obj.dinh1,obj.dinh2,obj.trong_so);
					break;
				}
			return c;
		}
	}
}
