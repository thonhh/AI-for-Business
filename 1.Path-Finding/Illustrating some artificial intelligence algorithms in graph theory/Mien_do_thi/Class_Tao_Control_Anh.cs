using System;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;
namespace Tri_tue_nhan_tao
{
	/// <summary>
	/// Summary description for Class_GraphicsRegion.
	/// </summary>
	public class Class_Tao_Control_Anh
	{
		public Class_Tao_Control_Anh()
		{}
		//
		// TODO: Add constructor logic here
		//
		public static void Tao_DK_AnhBmp(Control ctrl,Bitmap bmp)
		{
			if ((ctrl==null)||(bmp==null))
				return;
			ctrl.Width=bmp.Width;
			ctrl.Height=bmp.Height;
			if (ctrl is PictureBox)
			{
				PictureBox pict=(PictureBox)ctrl;
				pict.Image=bmp;
				GraphicsPath grpp=Tinh_anh_DK(bmp);
				pict.Region=new Region(grpp);
			}
			else if (ctrl is Button)
			{
				Button btt=(Button)ctrl;
				btt.BackgroundImage=bmp;
				GraphicsPath grpp=Tinh_anh_DK(bmp);
				btt.Region=new Region(grpp);
			}
			else if (ctrl is Form)
			{
				Form form1=(Form)ctrl;
				form1.BackgroundImage=bmp;
				GraphicsPath grpp=Tinh_anh_DK(bmp);
				form1.Region=new Region(grpp);
			}
		}
		private static GraphicsPath Tinh_anh_DK(Bitmap bmp)
		{
			GraphicsPath grpp=new GraphicsPath();
			Color Mau_trong_suot=bmp.GetPixel(0,0);
			int Cot_khong_trong_suot=0;
			for (int i=0;i<bmp.Height;i++)
			{
				Cot_khong_trong_suot=0;
				for(int j=0;j<bmp.Width;j++)
				{
					if (bmp.GetPixel(j,i)!=Mau_trong_suot)
					{
						Cot_khong_trong_suot=j;
						int Cot_trong_suot=j;
						for (Cot_trong_suot=j;Cot_trong_suot<bmp.Width;Cot_trong_suot++)
							if (bmp.GetPixel(Cot_trong_suot,i)==Mau_trong_suot)
								break;
						grpp.AddRectangle(new Rectangle(Cot_khong_trong_suot,i,Cot_trong_suot-Cot_khong_trong_suot,1));
						j=Cot_trong_suot;
					}
				}
			}
			return grpp;
		}
	}
}

