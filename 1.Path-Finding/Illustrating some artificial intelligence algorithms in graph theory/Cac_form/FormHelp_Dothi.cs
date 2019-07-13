using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Tri_tue_nhan_tao
{
	/// <summary>
	/// Summary description for FormHelp_Dothi.
	/// </summary>
	public class FormHelp_Dothi : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button_OK;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormHelp_Dothi()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormHelp_Dothi));
			this.button_OK = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// button_OK
			// 
			this.button_OK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button_OK.ForeColor = System.Drawing.Color.Blue;
			this.button_OK.Location = new System.Drawing.Point(132, 368);
			this.button_OK.Name = "button_OK";
			this.button_OK.Size = new System.Drawing.Size(96, 32);
			this.button_OK.TabIndex = 1;
			this.button_OK.Text = "OK";
			this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
			// 
			// label1
			// 
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(344, 344);
			this.label1.TabIndex = 2;
			this.label1.Text = "label1";
			// 
			// FormHelp_Dothi
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(360, 414);
			this.ControlBox = false;
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button_OK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormHelp_Dothi";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Huong dan su dung";
			this.Load += new System.EventHandler(this.FormHelp_Dothi_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormHelp_Dothi_Load(object sender, System.EventArgs e)
		{
			label1.Image=Image.FromFile(Application.StartupPath+@"\Images\3.gif",false);
			label1.Text="			Form nay hướng dẫn cách sử dụng trong Phần tìm kiếm trên đồ thị:"+"\n"+
				"1.Tạo 1 đỉnh: \n"+
				"_Chọn Thêm đỉnh trên mục Công cụ. \n"+
				"_Click chuột lên Vùng đồ thị. \n"+
				"2.Xóa 1 đỉnh: \n"+
				"_Chọn Xóa đỉnh trên mục Công cụ.\n"+
				"_Click chuột lên đỉnh cần xóa.\n"+
				"3. Tạo 1 cung:\n"+
				"_Chọn Tạo cung  trên mục Công cụ.\n"+
				"_Nhập trước trọng số cho cung sắp tạo trên mục Nhập trọng số.\n"+
				"_Click chuột lên 2 đỉnh cần nối cung.\n"+
				"4.Xóa 1 cung:\n"+
				"_Chọn Xóa cung  trên mục Công cụ.\n"+
				"_Click chuột lên 2 đỉnh cần xóa cung.\n"+
				"5.Nhập Heuristic:\n"+
				"_Chọn Chọn đỉnh trên mục Công cụ.\n"+
				"_Đánh dấu Check vào mục Nhập Heuristic.\n"+
				"_Nhập giá trị Heuristic trên mục Nhập Heuristic.\n"+
				"_Click chuột lên đỉnh cần nhập Heuristic.\n"+
				"6.Đổi trọng số cung:\n"+
				"_Chọn Đổi trọng số trên mục Công cụ.\n"+
				"_Click chuột lên 2 đỉnh cần đổi trọng số cung.\n"+
				"7.Thực hiện thuật toán:\n"+
				"_Chọn thuật toán ở Menu Algorithm\\…\n"+
				"_Chọn Chọn đỉnh trên mục Công cụ.\n"+
				"_Click chuột lên đỉnh xuất phát và đỉnh đích.\n"+
				"_Click Menu Tool\\Thực hiện.\n";
		}

		private void button_OK_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
