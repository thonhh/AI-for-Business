using System;

namespace Tri_tue_nhan_tao
{
	/// <summary>
	/// Summary description for Class_Queue.
	/// </summary>
	public class Class_Queue
	{
		private int[] Q;
		private int First,Last;
		public Class_Queue()
		{
			//
			// TODO: Add constructor logic here
			//
			Q=new int[Class_MienDoThi.dinh_Max+1];//0->dinhMax
			First=Last=0;
		}
		public bool Is_empty()
		{
			return (First==0);
		}
		public int Take()
		{
			if (First>0)
			{
				int temp=Q[First];
				if (First==Last)
					First=Last=0;
				else
				{
					if (First<Class_MienDoThi.dinh_Max)
						First++;
					else
						First=1;
				}
				return temp;
			}
			else
				return 0;
		}
		public bool Append(int a)
		{
			int temp;
			if (Last<Class_MienDoThi.dinh_Max)
				temp=Last+1;
			else
				temp=1;
			if (temp==First)
				return false;
			else
			{
				Last=temp;
				Q[Last]=a;
				if (First==0)
					First=1;
				return true;
			}
		}
		public bool In_list(int a)
		{
			bool kt=false;
			int i=First;
			while  (i!=Last)
			{
				if (Q[i]==a)
				{
					kt=true;
					break;
				}
				if (++i>Class_MienDoThi_VH.dinh_Max)
					i=1;
			}
			return (kt||(Q[Last]==a));
		}
		public string List_Queue()
		{
			if (First!=0)
			{
				string temp="";
				if (First<=Last)
				{
					for (int i=First;i<=Last;i++)
						temp+=Q[i].ToString()+" ";
				}
				else
				{
					for (int i=First;i<=Class_MienDoThi.dinh_Max;i++)
						temp+=Q[i].ToString()+" ";
					for (int i=1;i<=Last;i++)
						temp+=Q[i].ToString()+" ";
				}
				return temp;
			}
			else
				return "Rỗng...";
		}
	}
}
