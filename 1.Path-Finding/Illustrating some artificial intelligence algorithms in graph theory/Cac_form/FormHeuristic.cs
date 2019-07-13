using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Tri_tue_nhan_tao
{
	/// <summary>
	/// Summary description for FormHueristic.
	/// </summary>
	public class FormHeuristic : System.Windows.Forms.Form
	{
		public static string s="";
		private System.Windows.Forms.RichTextBox richTextBox_Dinh;
		private System.Windows.Forms.RichTextBox richTextBox_H;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button button_OK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormHeuristic()
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
			this.richTextBox_Dinh = new System.Windows.Forms.RichTextBox();
			this.richTextBox_H = new System.Windows.Forms.RichTextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.button_OK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// richTextBox_Dinh
			// 
			this.richTextBox_Dinh.Location = new System.Drawing.Point(24, 56);
			this.richTextBox_Dinh.Name = "richTextBox_Dinh";
			this.richTextBox_Dinh.Size = new System.Drawing.Size(48, 272);
			this.richTextBox_Dinh.TabIndex = 0;
			this.richTextBox_Dinh.Text = "";
			// 
			// richTextBox_H
			// 
			this.richTextBox_H.Location = new System.Drawing.Point(72, 56);
			this.richTextBox_H.Name = "richTextBox_H";
			this.richTextBox_H.Size = new System.Drawing.Size(48, 272);
			this.richTextBox_H.TabIndex = 1;
			this.richTextBox_H.Text = "";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Blue;
			this.label1.Location = new System.Drawing.Point(3, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(138, 16);
			this.label1.TabIndex = 2;
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(32, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Đỉnh";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(82, 32);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(28, 16);
			this.label3.TabIndex = 4;
			this.label3.Text = "H(i)";
			// 
			// button_OK
			// 
			this.button_OK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button_OK.Location = new System.Drawing.Point(35, 336);
			this.button_OK.Name = "button_OK";
			this.button_OK.TabIndex = 0;
			this.button_OK.Text = "OK";
			this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
			// 
			// FormHeuristic
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(144, 366);
			this.ControlBox = false;
			this.Controls.Add(this.button_OK);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.richTextBox_H);
			this.Controls.Add(this.richTextBox_Dinh);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "FormHeuristic";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Display";
			this.Load += new System.EventHandler(this.FormHueristic_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void button_OK_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void FormHueristic_Load(object sender, System.EventArgs e)
		{
			this.label1.Text=s;
			for (int i=1;i<=Class_MienDoThi.dinh_Max;i++)
				if (Class_MienDoThi.Kiem_tra_dinh_thuoc_DThi(i))
				{
					richTextBox_Dinh.Text+=i.ToString()+"\n";
					richTextBox_H.Text+=Class_MienDoThi.Get_Heuristic(i).ToString()+"\n";
				}
		}
	}
}
