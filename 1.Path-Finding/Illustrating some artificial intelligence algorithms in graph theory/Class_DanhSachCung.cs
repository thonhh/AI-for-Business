using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;


namespace Tri_tue_nhan_tao
{
	public class Class_DanhSachCung 
    {
        public ArrayList DS_Cung;
        public Class_DanhSachCung()
        {
            DS_Cung = new ArrayList();
        }
        public void Add(Class_Cung obj)
        {
           DS_Cung.Insert(0, obj);
        }
        public void ReMove(int d1,int d2)
        {
			foreach (Class_Cung obj in DS_Cung)
				if (((obj.dinh1==d1)&&(obj.dinh2==d2))||((obj.dinh1==d2)&&(obj.dinh2==d1)))
				{
					DS_Cung.Remove(obj);
					break;
				}
		}
		public void Draw(Graphics g)
		{
			int n = DS_Cung.Count;
			Class_Cung o;
			for (int i = n - 1; i >= 0; i-- )
			{
				o = (Class_Cung)DS_Cung[i];
				o.Draw(g);

			}
		}
	}
}
