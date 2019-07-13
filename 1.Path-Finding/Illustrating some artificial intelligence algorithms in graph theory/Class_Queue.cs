using System;

namespace Tri_tue_nhan_tao
{
	/// <summary>
	/// Summary description for Class_Queue.
	/// </summary>
	public class Class_Queue
	{
		private byte[] Q;
		private int First,Last;
		public Class_Queue()
		{
			//
			// TODO: Add constructor logic here
			//
			Q=new byte[21];
			First=Last=0;
		}
		public bool Is_empty()
		{
			return (First==0);
		}
		public byte Take()
		{
			if (First>0)
			{
				byte temp=Q[First];
				if (First==Last)
					First=Last=0;
				else
				{
					if (First<20)
						First++;
					else
						First=1;
				}
				return temp;
			}
			else
				return 0;
		}
		public bool Append(byte a)
		{
			int temp;
			if (Last<20)
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
		public bool In_list(byte a)
		{
			byte i;
			for (i=1;i<=20;i++)
			{
				if (Q[i]==a)
					break;
			}
			return (i<=20);
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
					for (int i=First;i<=20;i++)
						temp+=Q[i].ToString()+" ";
					for (int i=1;i<=Last;i++)
						temp+=Q[i].ToString()+" ";
				}
				/*for (byte i=1;i<=20;i++)
					temp+=Q[i].ToString()+" ";*/
				return temp;
			}
			else
				return "Rỗng...";
		}
	}
}
