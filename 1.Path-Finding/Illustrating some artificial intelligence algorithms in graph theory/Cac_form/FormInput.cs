using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Tri_tue_nhan_tao
{
	/// <summary>
	/// Summary description for FormInput.
	/// </summary>
	public class FormInput : System.Windows.Forms.Form
	{
		public static int gia_tri=0;
		public static bool OK=false;
		private string ten;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox_Value;
		private System.Windows.Forms.Button button_OK;
		private System.Windows.Forms.Button button_Cancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormInput(string s)
		{
			//
			// Required for Windows Form Designer support
			//
			ten=s;
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.textBox_Value = new System.Windows.Forms.TextBox();
			this.button_OK = new System.Windows.Forms.Button();
			this.button_Cancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(136, 16);
			this.label1.TabIndex = 0;
			// 
			// textBox_Value
			// 
			this.textBox_Value.Location = new System.Drawing.Point(8, 32);
			this.textBox_Value.Name = "textBox_Value";
			this.textBox_Value.Size = new System.Drawing.Size(168, 20);
			this.textBox_Value.TabIndex = 1;
			this.textBox_Value.Text = "";
			this.textBox_Value.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_Value_KeyDown);
			// 
			// button_OK
			// 
			this.button_OK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button_OK.Location = new System.Drawing.Point(8, 64);
			this.button_OK.Name = "button_OK";
			this.button_OK.TabIndex = 2;
			this.button_OK.Text = "OK";
			this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
			// 
			// button_Cancel
			// 
			this.button_Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button_Cancel.Location = new System.Drawing.Point(101, 64);
			this.button_Cancel.Name = "button_Cancel";
			this.button_Cancel.TabIndex = 3;
			this.button_Cancel.Text = "Cancel";
			this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
			// 
			// FormInput
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(184, 110);
			this.ControlBox = false;
			this.Controls.Add(this.button_Cancel);
			this.Controls.Add(this.button_OK);
			this.Controls.Add(this.textBox_Value);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormInput";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Nhap gia tri";
			this.Load += new System.EventHandler(this.FormInput_Load);
			this.ResumeLayout(false);

		}
		#endregion

		public static bool kiem_tra(string s)
		{
			bool kt=true;
			for (int i=0;i<s.Length;i++)
				if ((s[i].CompareTo('0')<0)||(s[i].CompareTo('9')>0))
				{
					kt=false;
					break;
				}
			return kt;
		}
		private void button_OK_Click(object sender, System.EventArgs e)
		{
			if ((textBox_Value.Text.Trim().Length>0)&& kiem_tra(textBox_Value.Text.Trim()))
				gia_tri=Convert.ToInt16(textBox_Value.Text,10);
			else
				gia_tri=0;
			OK=true;
			this.Close();
		}

		private void button_Cancel_Click(object sender, System.EventArgs e)
		{
			OK=false;
			this.Close();
		}

		private void FormInput_Load(object sender, System.EventArgs e)
		{
			label1.Text=ten;
		}

		private void textBox_Value_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode==Keys.Enter)
				button_OK_Click(sender,e);
		}
	}
}
