using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO; 

namespace Tri_tue_nhan_tao
{
	/// <summary>
	/// Summary description for Class_MienDoThi_VH.
	/// </summary>
	public class Class_MienDoThi_VH : Class_MienDoThi
	{
		public Class_MienDoThi_VH()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public static void Dat_dinh_Va(int i)
		{
			foreach (Class_DinhDoThi obj in DS_Dinh)
				if (obj.chiso==i)
				{
					obj.dinh_Va=true;
					obj.Bien_dang(@"\dinh3.bmp",Color.White);
					break;
				}
		}
		public static void Dat_dinh_So_cap(int i)
		{
			foreach (Class_DinhDoThi obj in DS_Dinh)
				if (obj.chiso==i)
				{
					obj.giai_duoc=true;
					obj.Bien_dang(@"\dinh4.bmp",Color.Red);
					break;
				}
		}
		public static bool Giai_duoc(int n)
		{
			bool kt=false;
			foreach (Class_DinhDoThi obj in DS_Dinh)
			{
				if (obj.chiso==n)
				{
					if (obj.giai_duoc)
						kt=true;
					else if (!obj.Co_dinh_ke())
					{
						kt=false;
					}
					else if (obj.dinh_Va)
					{	
						kt=true;
						foreach(Class_DinhDoThi obj1 in DS_Dinh)
							if (obj.Thuoc_DS_con(obj1.chiso)&& !obj1.giai_duoc)
							{
								kt=false;
								break;
							}
						if (kt)
							obj.giai_duoc=true;
					}
					else if (!obj.dinh_Va)
					{
						kt=false;
						foreach(Class_DinhDoThi obj1 in DS_Dinh)
							if (obj.Thuoc_DS_con(obj1.chiso)&& obj1.giai_duoc)
							{
								kt=true;
								break;
							}
						if (kt)
							obj.giai_duoc=true;
					}
				}
		
			}
			return kt;
		}
		public bool Khong_giai_duoc(int n)
		{
			bool kt=false;
			foreach (Class_DinhDoThi obj in DS_Dinh)
			{
				if (obj.chiso==n)
				{
					if (obj.giai_duoc)
						kt=false;
					else if (!obj.Co_dinh_ke())
					{
						kt=true;
					}
					else if (obj.dinh_Va)
					{	
						kt=false;
						foreach(Class_DinhDoThi obj1 in DS_Dinh)
							if (obj.Thuoc_DS_con(obj1.chiso)&& !obj1.giai_duoc)
							{
								kt=true;
								break;
							}
					}
					else if (!obj.dinh_Va)
					{
						kt=true;
						foreach(Class_DinhDoThi obj1 in DS_Dinh)
							if (obj.Thuoc_DS_con(obj1.chiso)&& obj1.giai_duoc)
							{
								kt=false;
								break;
							}
					}
				}
		
			}
			return kt;
		}
    }
}
