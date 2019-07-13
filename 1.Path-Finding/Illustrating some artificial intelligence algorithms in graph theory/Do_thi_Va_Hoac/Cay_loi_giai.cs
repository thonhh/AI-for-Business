using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Tri_tue_nhan_tao
{
	/// <summary>
	/// Summary description for Cay_loi_giai.
	/// </summary>
	public class Cay_loi_giai : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		/// 
		public static ArrayList DS_Dinh=new ArrayList();
		public static Class_DanhSachCung dsach_cung=new Class_DanhSachCung();
		private System.ComponentModel.Container components = null;

		public Cay_loi_giai()
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
			// Cay_loi_giai
			// 
			this.Name = "Cay_loi_giai";
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Cay_loi_giai_Paint);

		}
		#endregion
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
		public void Them_dinh_vao_do_thi(int chiso,int X,int Y,int h)
		{
			Class_DinhDoThi.Cao=this.Height;
			Class_DinhDoThi.Rong=this.Width;
			Class_DinhDoThi.ctrl=this;
			string path=Application.StartupPath+"\\Images";
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
		public static void Giai_phong_Static()
		{
			foreach (Class_DinhDoThi obj in DS_Dinh)
				Class_DinhDoThi.ctrl.Controls.Remove(obj.MyPic);
			DS_Dinh.Clear();
			dsach_cung.DS_Cung.Clear();
			Class_DinhDoThi.ctrl.Refresh();
		}
		private void Cay_loi_giai_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			dsach_cung.Draw(e.Graphics);
		}
	}
}
