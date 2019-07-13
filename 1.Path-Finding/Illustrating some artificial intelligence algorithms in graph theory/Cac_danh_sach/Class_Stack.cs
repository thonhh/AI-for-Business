using System;

namespace Tri_tue_nhan_tao
{
	/// <summary>
	/// Summary description for Class_Stack.
	/// </summary>
	public class Class_Stack
	{
		public int[] S;
		public int Top;
		public Class_Stack()
		{
			//
			// TODO: Add constructor logic here
			//
			S=new int[Class_MienDoThi.dinh_Max+1];
			Top=0;
		}
		public bool Is_empty()
		{
			return (Top==0);
		}
		public int Pop()
		{
			if (Top>0)
				return (S[Top--]);
			else
				return (0);
		}
		public bool Push(int a)
		{
			if (Top<Class_MienDoThi.dinh_Max+1)
			{
				S[++Top]=a;
				return true;
			}
			else
				return false;
		}
		public string List_Stack()
		{
			string temp="";
			for (int i=Top;i>0;i--)
			{
				temp+=S[i].ToString()+" ";
			}
			return (temp);
		}
		public bool In_Stack(int a)
		{
			int i;
			for (i=1;i<=Top;i++)
			{
				if (S[i]==a)
					break;
			}
			return (i<=Top);
		}
	}
}
