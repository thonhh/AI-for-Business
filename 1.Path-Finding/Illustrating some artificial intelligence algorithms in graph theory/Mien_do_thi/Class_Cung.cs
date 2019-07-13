using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Globalization;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;

namespace Tri_tue_nhan_tao
{
	/// <summary>
	/// Line graphic object
	/// </summary>
	public class Class_Cung 
	{
		public Color  color=Color.Blue;
		public int dinh1,dinh2;
        public Point startPoint;
        public Point endPoint;
		public bool da_di_qua=false;
		private int Trong_so;
		public Class_Cung(int x1, int y1, int x2, int y2,int d1,int d2,int trso)
        {
            startPoint.X = x1;
            startPoint.Y = y1;
            endPoint.X = x2;
            endPoint.Y = y2;
			dinh1=d1;
			dinh2=d2;
			Trong_so=trso;	
         }
        public void Draw(Graphics g)
        {
			Font MyFont=new Font("Arial",9.0f);
			SolidBrush MyBrush=new SolidBrush(Color.Red);
            g.SmoothingMode = SmoothingMode.AntiAlias;
			Pen pen = new Pen(color,1);
            g.DrawLine(pen, startPoint.X, startPoint.Y, endPoint.X, endPoint.Y);
			g.DrawString(Trong_so.ToString(),MyFont,MyBrush,(startPoint.X+endPoint.X)/2,(startPoint.Y+endPoint.Y)/2);
			pen.Dispose();
			MyBrush.Dispose();
        }
		public int trong_so
		{
			get{return Trong_so;}
			set{Trong_so=value;}
		}
        public void ChangeStart(int x, int y)
        {
			startPoint.X = x;
			startPoint.Y = y;
		}
		public void ChangeEnd(int x, int y)
		{
			endPoint.X = x;
			endPoint.Y = y;
		}
      
     }
}
