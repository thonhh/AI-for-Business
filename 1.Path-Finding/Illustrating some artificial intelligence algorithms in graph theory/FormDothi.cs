using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Tri_tue_nhan_tao
{
	/// <summary>
	/// Summary description for FormDothi.
	/// </summary>
	public class FormDothi : System.Windows.Forms.Form
	{
		private Tri_tue_nhan_tao.Class_MienDoThi class_MienDoThi1;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.MenuItem menuItem13;
		private System.Windows.Forms.MenuItem menuItem14;
		private System.Windows.Forms.MenuItem menuItem15;
		private System.Windows.Forms.MenuItem menuItem16;
		private System.Windows.Forms.MenuItem menuItem17;
		private System.Windows.Forms.MenuItem menuItem18;
		private System.Windows.Forms.RichTextBox richTextBox_n;
		private System.Windows.Forms.RichTextBox richTextBox_Tn;
		private System.Windows.Forms.RichTextBox richTextBox_Open;
		private System.Windows.Forms.RichTextBox richTextBox_Close;
		private System.Windows.Forms.RichTextBox richTextBox_Kq;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioButton_ThemDinh;
		private System.Windows.Forms.RadioButton radioButton_XoaDinh;
		private System.Windows.Forms.RadioButton radioButton_TaoCung;
		private System.Windows.Forms.RadioButton radioButton_XoaCung;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Button button1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormDothi()
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
			this.class_MienDoThi1 = new Tri_tue_nhan_tao.Class_MienDoThi();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this.menuItem12 = new System.Windows.Forms.MenuItem();
			this.menuItem13 = new System.Windows.Forms.MenuItem();
			this.menuItem14 = new System.Windows.Forms.MenuItem();
			this.menuItem15 = new System.Windows.Forms.MenuItem();
			this.menuItem16 = new System.Windows.Forms.MenuItem();
			this.menuItem17 = new System.Windows.Forms.MenuItem();
			this.menuItem18 = new System.Windows.Forms.MenuItem();
			this.richTextBox_n = new System.Windows.Forms.RichTextBox();
			this.richTextBox_Tn = new System.Windows.Forms.RichTextBox();
			this.richTextBox_Open = new System.Windows.Forms.RichTextBox();
			this.richTextBox_Close = new System.Windows.Forms.RichTextBox();
			this.richTextBox_Kq = new System.Windows.Forms.RichTextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioButton_XoaCung = new System.Windows.Forms.RadioButton();
			this.radioButton_TaoCung = new System.Windows.Forms.RadioButton();
			this.radioButton_XoaDinh = new System.Windows.Forms.RadioButton();
			this.radioButton_ThemDinh = new System.Windows.Forms.RadioButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// class_MienDoThi1
			// 
			this.class_MienDoThi1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.class_MienDoThi1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.class_MienDoThi1.Location = new System.Drawing.Point(8, 32);
			this.class_MienDoThi1.Name = "class_MienDoThi1";
			this.class_MienDoThi1.Size = new System.Drawing.Size(283, 400);
			this.class_MienDoThi1.TabIndex = 0;
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem9,
																					  this.menuItem17});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2,
																					  this.menuItem3,
																					  this.menuItem4,
																					  this.menuItem5,
																					  this.menuItem6,
																					  this.menuItem7,
																					  this.menuItem8});
			this.menuItem1.Text = "File";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "New";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "Open";
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 2;
			this.menuItem4.Text = "-";
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 3;
			this.menuItem5.Text = "Save";
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 4;
			this.menuItem6.Text = "Save as";
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 5;
			this.menuItem7.Text = "-";
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 6;
			this.menuItem8.Text = "Exit";
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 1;
			this.menuItem9.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem10,
																					  this.menuItem11,
																					  this.menuItem12,
																					  this.menuItem13,
																					  this.menuItem14,
																					  this.menuItem15,
																					  this.menuItem16});
			this.menuItem9.Text = "Algorithm";
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 0;
			this.menuItem10.Text = "Thuật toán Tìm kiếm theo chiều rộng";
			// 
			// menuItem11
			// 
			this.menuItem11.Index = 1;
			this.menuItem11.Text = "Thuật toán Tìm kiếm theo chiều sâu";
			// 
			// menuItem12
			// 
			this.menuItem12.Index = 2;
			this.menuItem12.Text = "Thuật toán Tìm kiếm sâu dần";
			// 
			// menuItem13
			// 
			this.menuItem13.Index = 3;
			this.menuItem13.Text = "Thuật toán Tìm kiếm tốt nhất đầu tiên";
			// 
			// menuItem14
			// 
			this.menuItem14.Index = 4;
			this.menuItem14.Text = "Thuật toán Tìm kiếm đường đi ngắn nhất";
			// 
			// menuItem15
			// 
			this.menuItem15.Index = 5;
			this.menuItem15.Text = "Thuật toán A*";
			// 
			// menuItem16
			// 
			this.menuItem16.Index = 6;
			this.menuItem16.Text = "Thuật toán Leo đồi";
			// 
			// menuItem17
			// 
			this.menuItem17.Index = 2;
			this.menuItem17.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.menuItem18});
			this.menuItem17.Text = "Tool";
			// 
			// menuItem18
			// 
			this.menuItem18.Index = 0;
			this.menuItem18.Text = "Thực hiện";
			// 
			// richTextBox_n
			// 
			this.richTextBox_n.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.richTextBox_n.Location = new System.Drawing.Point(300, 32);
			this.richTextBox_n.Name = "richTextBox_n";
			this.richTextBox_n.Size = new System.Drawing.Size(40, 376);
			this.richTextBox_n.TabIndex = 1;
			this.richTextBox_n.Text = "";
			// 
			// richTextBox_Tn
			// 
			this.richTextBox_Tn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.richTextBox_Tn.Location = new System.Drawing.Point(328, 32);
			this.richTextBox_Tn.Name = "richTextBox_Tn";
			this.richTextBox_Tn.Size = new System.Drawing.Size(138, 376);
			this.richTextBox_Tn.TabIndex = 2;
			this.richTextBox_Tn.Text = "";
			// 
			// richTextBox_Open
			// 
			this.richTextBox_Open.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.richTextBox_Open.Location = new System.Drawing.Point(472, 32);
			this.richTextBox_Open.Name = "richTextBox_Open";
			this.richTextBox_Open.Size = new System.Drawing.Size(145, 376);
			this.richTextBox_Open.TabIndex = 3;
			this.richTextBox_Open.Text = "";
			// 
			// richTextBox_Close
			// 
			this.richTextBox_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.richTextBox_Close.Location = new System.Drawing.Point(624, 32);
			this.richTextBox_Close.Name = "richTextBox_Close";
			this.richTextBox_Close.Size = new System.Drawing.Size(160, 376);
			this.richTextBox_Close.TabIndex = 4;
			this.richTextBox_Close.Text = "";
			// 
			// richTextBox_Kq
			// 
			this.richTextBox_Kq.ForeColor = System.Drawing.Color.Blue;
			this.richTextBox_Kq.Location = new System.Drawing.Point(300, 409);
			this.richTextBox_Kq.MaxLength = 200;
			this.richTextBox_Kq.Multiline = false;
			this.richTextBox_Kq.Name = "richTextBox_Kq";
			this.richTextBox_Kq.Size = new System.Drawing.Size(484, 23);
			this.richTextBox_Kq.TabIndex = 6;
			this.richTextBox_Kq.Text = "";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioButton_XoaCung);
			this.groupBox1.Controls.Add(this.radioButton_TaoCung);
			this.groupBox1.Controls.Add(this.radioButton_XoaDinh);
			this.groupBox1.Controls.Add(this.radioButton_ThemDinh);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox1.ForeColor = System.Drawing.Color.Red;
			this.groupBox1.Location = new System.Drawing.Point(8, 434);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(283, 80);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Công cụ";
			// 
			// radioButton_XoaCung
			// 
			this.radioButton_XoaCung.ForeColor = System.Drawing.Color.Black;
			this.radioButton_XoaCung.Location = new System.Drawing.Point(176, 48);
			this.radioButton_XoaCung.Name = "radioButton_XoaCung";
			this.radioButton_XoaCung.Size = new System.Drawing.Size(80, 24);
			this.radioButton_XoaCung.TabIndex = 3;
			this.radioButton_XoaCung.Text = "Xóa cung";
			this.radioButton_XoaCung.CheckedChanged += new System.EventHandler(this.radioButton_XoaCung_CheckedChanged);
			// 
			// radioButton_TaoCung
			// 
			this.radioButton_TaoCung.ForeColor = System.Drawing.Color.Black;
			this.radioButton_TaoCung.Location = new System.Drawing.Point(176, 16);
			this.radioButton_TaoCung.Name = "radioButton_TaoCung";
			this.radioButton_TaoCung.Size = new System.Drawing.Size(80, 24);
			this.radioButton_TaoCung.TabIndex = 2;
			this.radioButton_TaoCung.Text = "Tạo cung";
			this.radioButton_TaoCung.CheckedChanged += new System.EventHandler(this.radioButton_TaoCung_CheckedChanged);
			// 
			// radioButton_XoaDinh
			// 
			this.radioButton_XoaDinh.ForeColor = System.Drawing.Color.Black;
			this.radioButton_XoaDinh.Location = new System.Drawing.Point(32, 48);
			this.radioButton_XoaDinh.Name = "radioButton_XoaDinh";
			this.radioButton_XoaDinh.Size = new System.Drawing.Size(80, 24);
			this.radioButton_XoaDinh.TabIndex = 1;
			this.radioButton_XoaDinh.Text = "Xóa đỉnh";
			this.radioButton_XoaDinh.CheckedChanged += new System.EventHandler(this.radioButton_XoaDinh_CheckedChanged);
			// 
			// radioButton_ThemDinh
			// 
			this.radioButton_ThemDinh.Checked = true;
			this.radioButton_ThemDinh.ForeColor = System.Drawing.Color.Black;
			this.radioButton_ThemDinh.Location = new System.Drawing.Point(32, 16);
			this.radioButton_ThemDinh.Name = "radioButton_ThemDinh";
			this.radioButton_ThemDinh.Size = new System.Drawing.Size(80, 24);
			this.radioButton_ThemDinh.TabIndex = 0;
			this.radioButton_ThemDinh.TabStop = true;
			this.radioButton_ThemDinh.Text = "Thêm đỉnh";
			this.radioButton_ThemDinh.CheckedChanged += new System.EventHandler(this.radioButton_ThemDinh_CheckedChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.textBox3);
			this.groupBox2.Controls.Add(this.textBox2);
			this.groupBox2.Controls.Add(this.textBox1);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox2.ForeColor = System.Drawing.Color.Red;
			this.groupBox2.Location = new System.Drawing.Point(300, 434);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(283, 80);
			this.groupBox2.TabIndex = 9;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Bảng giá trị";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(216, 24);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(40, 20);
			this.textBox3.TabIndex = 5;
			this.textBox3.Text = "textBox3";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(80, 48);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(40, 20);
			this.textBox2.TabIndex = 4;
			this.textBox2.Text = "textBox2";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(80, 21);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(40, 20);
			this.textBox1.TabIndex = 3;
			this.textBox1.Text = "textBox1";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(128, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(70, 14);
			this.label3.TabIndex = 2;
			this.label3.Text = "Trọng số";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(70, 14);
			this.label2.TabIndex = 1;
			this.label2.Text = "Đỉnh cuối";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(70, 14);
			this.label1.TabIndex = 0;
			this.label1.Text = "Đỉnh đầu";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(640, 456);
			this.button1.Name = "button1";
			this.button1.TabIndex = 10;
			this.button1.Text = "button1";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// FormDothi
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(792, 526);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.richTextBox_Kq);
			this.Controls.Add(this.richTextBox_Close);
			this.Controls.Add(this.richTextBox_Open);
			this.Controls.Add(this.richTextBox_Tn);
			this.Controls.Add(this.richTextBox_n);
			this.Controls.Add(this.class_MienDoThi1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Menu = this.mainMenu1;
			this.Name = "FormDothi";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Tim kiem tren do thi";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void dat_Check_false()
		{
			Class_MienDoThi.cho_phep_tao_dinh=false;
			Class_DinhDoThi.cho_phep_click=false;
			Class_DinhDoThi.cho_phep_keo=false;
			Class_DinhDoThi.dang_tao_cung=false;
			Class_DinhDoThi.dang_xoa_cung=false;
			Class_DinhDoThi.dang_xoa_dinh=false;
			Class_MienDoThi.Binh_thuong_DThi();
		}
		private void radioButton_ThemDinh_CheckedChanged(object sender, System.EventArgs e)
		{
			dat_Check_false();
			Class_MienDoThi.cho_phep_tao_dinh=true;
			Class_DinhDoThi.cho_phep_keo=true;
		}

		private void radioButton_XoaDinh_CheckedChanged(object sender, System.EventArgs e)
		{
			dat_Check_false();
			Class_DinhDoThi.cho_phep_click=true;
			Class_DinhDoThi.dang_xoa_dinh=true;
		}

		private void radioButton_TaoCung_CheckedChanged(object sender, System.EventArgs e)
		{
			dat_Check_false();
			Class_DinhDoThi.cho_phep_click=true;
			Class_DinhDoThi.dang_tao_cung=true;
			Class_DinhDoThi.cho_phep_keo=true;
		}

		private void radioButton_XoaCung_CheckedChanged(object sender, System.EventArgs e)
		{
			dat_Check_false();
			Class_DinhDoThi.cho_phep_click=true;
			Class_DinhDoThi.dang_xoa_cung=true;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			textBox3.Text=Class_DinhDoThi.chiso_1.ToString();
			textBox2.Text=Class_DinhDoThi.chiso_2.ToString();
		}


	}
}
