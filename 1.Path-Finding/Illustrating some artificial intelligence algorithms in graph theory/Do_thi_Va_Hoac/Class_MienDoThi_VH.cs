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
					obj.Dinh_la=true;
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
					{
						kt=true;
						foreach(Class_DinhDoThi obj2 in DS_Dinh)
						{
							if (obj.Thuoc_DS_con(obj2.chiso,n))
								Giai_duoc(obj2.chiso);
						}
					}
					else if (obj.DS_Dinh_con.Count==0)
					{
						kt=false;
					}
					else if (obj.dinh_Va)
					{	
						kt=true;
						foreach(Class_DinhDoThi obj1 in DS_Dinh)
							if (obj.Thuoc_DS_con(n,obj1.chiso)&& !obj1.giai_duoc)
							{
								kt=false;
								break;
							}
						if (kt)
						{
							obj.giai_duoc=true;
							foreach(Class_DinhDoThi obj2 in DS_Dinh)
							{
								if (obj.Thuoc_DS_con(obj2.chiso,n))
									Giai_duoc(obj2.chiso);
							}
						}
					}
					else if (!obj.dinh_Va)
					{
						kt=false;
						foreach(Class_DinhDoThi obj1 in DS_Dinh)
							if (obj.Thuoc_DS_con(n,obj1.chiso)&& obj1.giai_duoc)
							{
								kt=true;
								break;
							}
						if (kt)
						{
							obj.giai_duoc=true;
							foreach(Class_DinhDoThi obj2 in DS_Dinh)
							{
								if (obj.Thuoc_DS_con(obj2.chiso,n))
									Giai_duoc(obj2.chiso);
							}
						}
					}
					break;	
				}
			}
			return kt;
		}
		public static bool Khong_giai_duoc(int n)
		{
			bool kt=false;
			foreach (Class_DinhDoThi obj in DS_Dinh)
			{
				if (obj.chiso==n)
				{
					if (obj.giai_duoc)
						kt=false;
					else if (obj.DS_Dinh_con.Count==0)
					{
						kt=true;
						obj.khong_giai_duoc=true;
						foreach(Class_DinhDoThi obj1 in DS_Dinh)
						{
							if (obj.Thuoc_DS_con(obj1.chiso,n))
								Khong_giai_duoc(obj1.chiso);
						}
					}
					else if (obj.dinh_Va)
					{	
						kt=false;
						foreach(Class_DinhDoThi obj1 in DS_Dinh)
							if (obj.Thuoc_DS_con(n,obj1.chiso)&& obj1.khong_giai_duoc)
							{
								kt=true;
								break;
							}
						if (kt)
						{
							obj.giai_duoc=false;
							obj.khong_giai_duoc=true;
							foreach(Class_DinhDoThi obj2 in DS_Dinh)
							{
								if (obj.Thuoc_DS_con(obj2.chiso,n))
									Khong_giai_duoc(obj2.chiso);
							}
						}
					}
					else if (!obj.dinh_Va)
					{
						kt=true;
						foreach(Class_DinhDoThi obj1 in DS_Dinh)
							if (obj.Thuoc_DS_con(n,obj1.chiso)&& obj1.giai_duoc)
							{
								kt=false;
								break;
							}
						if (kt)
						{
							obj.giai_duoc=false;
							obj.khong_giai_duoc=true;
							foreach(Class_DinhDoThi obj2 in DS_Dinh)
							{
								if (obj.Thuoc_DS_con(obj2.chiso,n))
									Khong_giai_duoc(obj2.chiso);
							}
						}
					}
				}
		
			}
			return kt;
		}
    }
}
