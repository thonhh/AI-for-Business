using System;

namespace Tri_tue_nhan_tao
{
	/// <summary>
	/// Summary description for Class_Stack.
	/// </summary>
	public class Class_Stack
	{
		public byte[] S;
		public byte Top;
		public Class_Stack()
		{
			//
			// TODO: Add constructor logic here
			//
			S=new byte[21];
			Top=0;
		}
		public bool Is_empty()
		{
			return (Top==0);
		}
		public byte Pop()
		{
			if (Top>0)
				return (S[Top--]);
			else
				return (0);
		}
		public bool Push(byte a)
		{
			if (Top<20)
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
			for (byte i=Top;i>0;i--)
			{
				temp+=S[i].ToString()+" ";
			}
			return (temp);
		}
		public bool In_Stack(byte a)
		{
			byte i;
			for (i=1;i<=Top;i++)
			{
				if (S[i]==a)
					break;
			}
			return (i<=Top);
		}

	}
}
