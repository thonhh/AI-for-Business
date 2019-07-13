using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Tri_tue_nhan_tao
{
	/// <summary>
	/// Summary description for FormCayLoiGiai.
	/// </summary>
	public class FormCayLoiGiai : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		public System.Windows.Forms.Label label_DoThi;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormCayLoiGiai()
		{
			//
			// Required for Windows Form Designer support
			//
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
			this.label_DoThi = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label_DoThi
			// 
			this.label_DoThi.BackColor = System.Drawing.Color.White;
			this.label_DoThi.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label_DoThi.Location = new System.Drawing.Point(22, 8);
			this.label_DoThi.Name = "label_DoThi";
			this.label_DoThi.Size = new System.Drawing.Size(285, 400);
			this.label_DoThi.TabIndex = 0;
			this.label_DoThi.Paint += new System.Windows.Forms.PaintEventHandler(this.label_DoThi_Paint);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button1.Location = new System.Drawing.Point(127, 416);
			this.button1.Name = "button1";
			this.button1.TabIndex = 1;
			this.button1.Text = "OK";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// FormCayLoiGiai
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(328, 446);
			this.ControlBox = false;
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label_DoThi);
			this.Name = "FormCayLoiGiai";
			this.Text = "FormCayLoiGiai";
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void label_DoThi_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Pen p=new Pen(Color.Blue,1);
			e.Graphics.SmoothingMode=SmoothingMode.AntiAlias;
			e.Graphics.DrawPath(p,Class_VH_Theo_chieu_rong.grpth);
			p.Dispose();
		}
	}
}
