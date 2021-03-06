using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace Tri_tue_nhan_tao
{
	/// <summary>
	/// Summary description for FormDo_thi_VH.
	/// </summary>
	public class FormDo_thi_VH : System.Windows.Forms.Form
	{
        private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menu_New;
		private System.Windows.Forms.MenuItem menu_Open;
		private System.Windows.Forms.MenuItem menu_Save;
		private System.Windows.Forms.MenuItem menu_Exit;
		private System.Windows.Forms.MenuItem menu_Theo_Rong;
		private System.Windows.Forms.MenuItem menu_Theo_Sau;
		private System.Windows.Forms.MenuItem menu_Theo_Cay_min;
		private System.Windows.Forms.MenuItem menuItem13;
		private System.Windows.Forms.MenuItem menu_Thuchien;
		private Tri_tue_nhan_tao.Class_MienDoThi_VH class_MienDoThi1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button button_HT;
		private System.Windows.Forms.TextBox textBox_H;
		private System.Windows.Forms.TextBox textBox_DinhH;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox textBox_Dinh2;
		private System.Windows.Forms.TextBox textBox_Dinh1;
		private System.Windows.Forms.TextBox textBox_Trongso;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioButton_Doitrongso;
		private System.Windows.Forms.RadioButton radioButton_Chondinh;
		private System.Windows.Forms.RadioButton radioButton_XoaCung;
		private System.Windows.Forms.RadioButton radioButton_TaoCung;
		private System.Windows.Forms.RadioButton radioButton_XoaDinh;
		private System.Windows.Forms.RadioButton radioButton_ThemDinh;
		private System.Windows.Forms.Button button_Dinh_Va;
		private string path_temp="";
		/// <summary>
		/// Required designer variable.
		/// </summary>
		/// 
		public enum Cac_Thuat_toan
		{
			TheoChieuRong,
			TheoChieuSau,
			CayLoiGiai
		}
		private Cac_Thuat_toan thuattoan;
		private bool kt_Rong=false,kt_Sau=false,kt_Cay=false;
		private Class_VH_Theo_chieu_rong obj_Rong;
		private Class_VH_Theo_chieu_sau obj_Sau;
		private Class_VH_Cay_loi_giai obj_Cay;
		private System.Windows.Forms.Button button_Dinh_So_cap;
		private System.Windows.Forms.RadioButton radioButton_Themcon;
		private System.Windows.Forms.RadioButton radioButton_Xoacon;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.ComponentModel.Container components = null;
		
		public FormDo_thi_VH()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormDo_thi_VH));
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menu_New = new System.Windows.Forms.MenuItem();
			this.menu_Open = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menu_Save = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menu_Exit = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menu_Theo_Rong = new System.Windows.Forms.MenuItem();
			this.menu_Theo_Sau = new System.Windows.Forms.MenuItem();
			this.menu_Theo_Cay_min = new System.Windows.Forms.MenuItem();
			this.menuItem13 = new System.Windows.Forms.MenuItem();
			this.menu_Thuchien = new System.Windows.Forms.MenuItem();
			this.class_MienDoThi1 = new Tri_tue_nhan_tao.Class_MienDoThi_VH();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.button_HT = new System.Windows.Forms.Button();
			this.textBox_H = new System.Windows.Forms.TextBox();
			this.textBox_DinhH = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textBox_Dinh2 = new System.Windows.Forms.TextBox();
			this.textBox_Dinh1 = new System.Windows.Forms.TextBox();
			this.textBox_Trongso = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioButton_Xoacon = new System.Windows.Forms.RadioButton();
			this.radioButton_Themcon = new System.Windows.Forms.RadioButton();
			this.button_Dinh_So_cap = new System.Windows.Forms.Button();
			this.button_Dinh_Va = new System.Windows.Forms.Button();
			this.radioButton_Doitrongso = new System.Windows.Forms.RadioButton();
			this.radioButton_Chondinh = new System.Windows.Forms.RadioButton();
			this.radioButton_XoaCung = new System.Windows.Forms.RadioButton();
			this.radioButton_TaoCung = new System.Windows.Forms.RadioButton();
			this.radioButton_XoaDinh = new System.Windows.Forms.RadioButton();
			this.radioButton_ThemDinh = new System.Windows.Forms.RadioButton();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem9,
																					  this.menuItem13,
																					  this.menuItem2});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menu_New,
																					  this.menu_Open,
																					  this.menuItem4,
																					  this.menu_Save,
																					  this.menuItem7,
																					  this.menu_Exit});
			this.menuItem1.Text = "File";
			// 
			// menu_New
			// 
			this.menu_New.Index = 0;
			this.menu_New.Text = "New";
			this.menu_New.Click += new System.EventHandler(this.menu_New_Click);
			// 
			// menu_Open
			// 
			this.menu_Open.Index = 1;
			this.menu_Open.Text = "Open...";
			this.menu_Open.Click += new System.EventHandler(this.menu_Open_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 2;
			this.menuItem4.Text = "-";
			// 
			// menu_Save
			// 
			this.menu_Save.Index = 3;
			this.menu_Save.Text = "Save...";
			this.menu_Save.Click += new System.EventHandler(this.menu_Save_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 4;
			this.menuItem7.Text = "-";
			// 
			// menu_Exit
			// 
			this.menu_Exit.Index = 5;
			this.menu_Exit.Text = "Exit";
			this.menu_Exit.Click += new System.EventHandler(this.menu_Exit_Click);
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 1;
			this.menuItem9.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menu_Theo_Rong,
																					  this.menu_Theo_Sau,
																					  this.menu_Theo_Cay_min});
			this.menuItem9.Text = "Algorithm";
			// 
			// menu_Theo_Rong
			// 
			this.menu_Theo_Rong.Index = 0;
			this.menu_Theo_Rong.Text = "Tìm kiếm theo chiều rộng";
			this.menu_Theo_Rong.Click += new System.EventHandler(this.menu_Theo_Rong_Click);
			// 
			// menu_Theo_Sau
			// 
			this.menu_Theo_Sau.Index = 1;
			this.menu_Theo_Sau.Text = "Tìm kiếm theo chiều sâu";
			this.menu_Theo_Sau.Click += new System.EventHandler(this.menu_Theo_Sau_Click);
			// 
			// menu_Theo_Cay_min
			// 
			this.menu_Theo_Cay_min.Index = 2;
			this.menu_Theo_Cay_min.Text = "Tìm cây lời giải cực tiểu";
			this.menu_Theo_Cay_min.Click += new System.EventHandler(this.menu_Theo_Cay_min_Click);
			// 
			// menuItem13
			// 
			this.menuItem13.Index = 2;
			this.menuItem13.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.menu_Thuchien});
			this.menuItem13.Text = "Tool";
			// 
			// menu_Thuchien
			// 
			this.menu_Thuchien.Enabled = false;
			this.menu_Thuchien.Index = 0;
			this.menu_Thuchien.Text = "Thực hiện";
			this.menu_Thuchien.Click += new System.EventHandler(this.menu_Thuchien_Click);
			// 
			// class_MienDoThi1
			// 
			this.class_MienDoThi1.BackColor = System.Drawing.Color.White;
			this.class_MienDoThi1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.class_MienDoThi1.Location = new System.Drawing.Point(8, 32);
			this.class_MienDoThi1.Name = "class_MienDoThi1";
			this.class_MienDoThi1.Size = new System.Drawing.Size(285, 400);
			this.class_MienDoThi1.TabIndex = 0;
			this.class_MienDoThi1.Paint += new System.Windows.Forms.PaintEventHandler(this.class_MienDoThi1_Paint);
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label6.ForeColor = System.Drawing.Color.Blue;
			this.label6.Location = new System.Drawing.Point(100, 14);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(80, 16);
			this.label6.TabIndex = 12;
			this.label6.Text = "Vùng đồ thị";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.button_HT);
			this.groupBox3.Controls.Add(this.textBox_H);
			this.groupBox3.Controls.Add(this.textBox_DinhH);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox3.ForeColor = System.Drawing.Color.Red;
			this.groupBox3.Location = new System.Drawing.Point(616, 434);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(168, 80);
			this.groupBox3.TabIndex = 15;
			this.groupBox3.TabStop = false;
			// 
			// button_HT
			// 
			this.button_HT.ForeColor = System.Drawing.Color.Black;
			this.button_HT.Location = new System.Drawing.Point(96, 40);
			this.button_HT.Name = "button_HT";
			this.button_HT.Size = new System.Drawing.Size(64, 23);
			this.button_HT.TabIndex = 4;
			this.button_HT.Text = "Hiển thị";
			this.button_HT.Click += new System.EventHandler(this.button_HT_Click);
			// 
			// textBox_H
			// 
			this.textBox_H.Location = new System.Drawing.Point(48, 48);
			this.textBox_H.MaxLength = 3;
			this.textBox_H.Name = "textBox_H";
			this.textBox_H.Size = new System.Drawing.Size(40, 20);
			this.textBox_H.TabIndex = 3;
			this.textBox_H.Text = "";
			this.textBox_H.TextChanged += new System.EventHandler(this.textBox_H_TextChanged);
			// 
			// textBox_DinhH
			// 
			this.textBox_DinhH.Location = new System.Drawing.Point(48, 24);
			this.textBox_DinhH.MaxLength = 3;
			this.textBox_DinhH.Name = "textBox_DinhH";
			this.textBox_DinhH.ReadOnly = true;
			this.textBox_DinhH.Size = new System.Drawing.Size(40, 20);
			this.textBox_DinhH.TabIndex = 2;
			this.textBox_DinhH.Text = "";
			// 
			// label5
			// 
			this.label5.ForeColor = System.Drawing.Color.Black;
			this.label5.Location = new System.Drawing.Point(8, 50);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(32, 16);
			this.label5.TabIndex = 1;
			this.label5.Text = "H(i)";
			// 
			// label4
			// 
			this.label4.ForeColor = System.Drawing.Color.Black;
			this.label4.Location = new System.Drawing.Point(8, 26);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(32, 16);
			this.label4.TabIndex = 0;
			this.label4.Text = "Đỉnh";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.textBox_Dinh2);
			this.groupBox2.Controls.Add(this.textBox_Dinh1);
			this.groupBox2.Controls.Add(this.textBox_Trongso);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox2.ForeColor = System.Drawing.Color.Red;
			this.groupBox2.Location = new System.Drawing.Point(424, 434);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(192, 80);
			this.groupBox2.TabIndex = 14;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Nhập trọng số";
			// 
			// textBox_Dinh2
			// 
			this.textBox_Dinh2.Location = new System.Drawing.Point(72, 48);
			this.textBox_Dinh2.MaxLength = 3;
			this.textBox_Dinh2.Name = "textBox_Dinh2";
			this.textBox_Dinh2.ReadOnly = true;
			this.textBox_Dinh2.Size = new System.Drawing.Size(40, 20);
			this.textBox_Dinh2.TabIndex = 5;
			this.textBox_Dinh2.Text = "";
			// 
			// textBox_Dinh1
			// 
			this.textBox_Dinh1.Location = new System.Drawing.Point(72, 24);
			this.textBox_Dinh1.MaxLength = 3;
			this.textBox_Dinh1.Name = "textBox_Dinh1";
			this.textBox_Dinh1.ReadOnly = true;
			this.textBox_Dinh1.Size = new System.Drawing.Size(40, 20);
			this.textBox_Dinh1.TabIndex = 4;
			this.textBox_Dinh1.Text = "";
			// 
			// textBox_Trongso
			// 
			this.textBox_Trongso.Location = new System.Drawing.Point(138, 48);
			this.textBox_Trongso.MaxLength = 3;
			this.textBox_Trongso.Name = "textBox_Trongso";
			this.textBox_Trongso.Size = new System.Drawing.Size(40, 20);
			this.textBox_Trongso.TabIndex = 3;
			this.textBox_Trongso.Text = "";
			this.textBox_Trongso.TextChanged += new System.EventHandler(this.textBox_Trongso_TextChanged);
			// 
			// label3
			// 
			this.label3.ForeColor = System.Drawing.Color.Black;
			this.label3.Location = new System.Drawing.Point(8, 51);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 15);
			this.label3.TabIndex = 2;
			this.label3.Text = "Đỉnh cuối";
			// 
			// label2
			// 
			this.label2.ForeColor = System.Drawing.Color.Black;
			this.label2.Location = new System.Drawing.Point(8, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 15);
			this.label2.TabIndex = 1;
			this.label2.Text = "Đỉnh đầu";
			// 
			// label1
			// 
			this.label1.ForeColor = System.Drawing.Color.Black;
			this.label1.Location = new System.Drawing.Point(128, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Trọng số";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioButton_Xoacon);
			this.groupBox1.Controls.Add(this.radioButton_Themcon);
			this.groupBox1.Controls.Add(this.button_Dinh_So_cap);
			this.groupBox1.Controls.Add(this.button_Dinh_Va);
			this.groupBox1.Controls.Add(this.radioButton_Doitrongso);
			this.groupBox1.Controls.Add(this.radioButton_Chondinh);
			this.groupBox1.Controls.Add(this.radioButton_XoaCung);
			this.groupBox1.Controls.Add(this.radioButton_TaoCung);
			this.groupBox1.Controls.Add(this.radioButton_XoaDinh);
			this.groupBox1.Controls.Add(this.radioButton_ThemDinh);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox1.ForeColor = System.Drawing.Color.Red;
			this.groupBox1.Location = new System.Drawing.Point(8, 434);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(416, 80);
			this.groupBox1.TabIndex = 13;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Công cụ";
			// 
			// radioButton_Xoacon
			// 
			this.radioButton_Xoacon.ForeColor = System.Drawing.Color.Black;
			this.radioButton_Xoacon.Location = new System.Drawing.Point(255, 48);
			this.radioButton_Xoacon.Name = "radioButton_Xoacon";
			this.radioButton_Xoacon.Size = new System.Drawing.Size(72, 24);
			this.radioButton_Xoacon.TabIndex = 9;
			this.radioButton_Xoacon.Text = "Xóa con";
			this.radioButton_Xoacon.CheckedChanged += new System.EventHandler(this.radioButton_Xoacon_CheckedChanged);
			// 
			// radioButton_Themcon
			// 
			this.radioButton_Themcon.ForeColor = System.Drawing.Color.Black;
			this.radioButton_Themcon.Location = new System.Drawing.Point(255, 16);
			this.radioButton_Themcon.Name = "radioButton_Themcon";
			this.radioButton_Themcon.Size = new System.Drawing.Size(64, 24);
			this.radioButton_Themcon.TabIndex = 8;
			this.radioButton_Themcon.Text = "Tạo con";
			this.radioButton_Themcon.CheckedChanged += new System.EventHandler(this.radioButton_Themcon_CheckedChanged);
			// 
			// button_Dinh_So_cap
			// 
			this.button_Dinh_So_cap.Cursor = System.Windows.Forms.Cursors.Hand;
			this.button_Dinh_So_cap.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button_Dinh_So_cap.Location = new System.Drawing.Point(325, 48);
			this.button_Dinh_So_cap.Name = "button_Dinh_So_cap";
			this.button_Dinh_So_cap.Size = new System.Drawing.Size(83, 23);
			this.button_Dinh_So_cap.TabIndex = 7;
			this.button_Dinh_So_cap.Text = "Đỉnh kết thúc";
			this.button_Dinh_So_cap.Click += new System.EventHandler(this.button_Dinh_So_cap_Click);
			// 
			// button_Dinh_Va
			// 
			this.button_Dinh_Va.Cursor = System.Windows.Forms.Cursors.Hand;
			this.button_Dinh_Va.ForeColor = System.Drawing.Color.White;
			this.button_Dinh_Va.Location = new System.Drawing.Point(325, 16);
			this.button_Dinh_Va.Name = "button_Dinh_Va";
			this.button_Dinh_Va.Size = new System.Drawing.Size(83, 23);
			this.button_Dinh_Va.TabIndex = 6;
			this.button_Dinh_Va.Text = "Đỉnh Và";
			this.button_Dinh_Va.Click += new System.EventHandler(this.button_Dinh_Va_Click);
			// 
			// radioButton_Doitrongso
			// 
			this.radioButton_Doitrongso.ForeColor = System.Drawing.Color.Black;
			this.radioButton_Doitrongso.Location = new System.Drawing.Point(165, 48);
			this.radioButton_Doitrongso.Name = "radioButton_Doitrongso";
			this.radioButton_Doitrongso.Size = new System.Drawing.Size(88, 24);
			this.radioButton_Doitrongso.TabIndex = 5;
			this.radioButton_Doitrongso.Text = "Đổi trọng số";
			this.radioButton_Doitrongso.CheckedChanged += new System.EventHandler(this.radioButton_Doitrongso_CheckedChanged);
			// 
			// radioButton_Chondinh
			// 
			this.radioButton_Chondinh.ForeColor = System.Drawing.Color.Black;
			this.radioButton_Chondinh.Location = new System.Drawing.Point(165, 16);
			this.radioButton_Chondinh.Name = "radioButton_Chondinh";
			this.radioButton_Chondinh.Size = new System.Drawing.Size(78, 24);
			this.radioButton_Chondinh.TabIndex = 4;
			this.radioButton_Chondinh.Text = "Chọn đỉnh";
			this.radioButton_Chondinh.CheckedChanged += new System.EventHandler(this.radioButton_Chondinh_CheckedChanged);
			// 
			// radioButton_XoaCung
			// 
			this.radioButton_XoaCung.ForeColor = System.Drawing.Color.Black;
			this.radioButton_XoaCung.Location = new System.Drawing.Point(89, 48);
			this.radioButton_XoaCung.Name = "radioButton_XoaCung";
			this.radioButton_XoaCung.Size = new System.Drawing.Size(73, 24);
			this.radioButton_XoaCung.TabIndex = 3;
			this.radioButton_XoaCung.Text = "Xóa cung";
			this.radioButton_XoaCung.CheckedChanged += new System.EventHandler(this.radioButton_XoaCung_CheckedChanged);
			// 
			// radioButton_TaoCung
			// 
			this.radioButton_TaoCung.ForeColor = System.Drawing.Color.Black;
			this.radioButton_TaoCung.Location = new System.Drawing.Point(89, 16);
			this.radioButton_TaoCung.Name = "radioButton_TaoCung";
			this.radioButton_TaoCung.Size = new System.Drawing.Size(73, 24);
			this.radioButton_TaoCung.TabIndex = 2;
			this.radioButton_TaoCung.Text = "Tạo cung";
			this.radioButton_TaoCung.CheckedChanged += new System.EventHandler(this.radioButton_TaoCung_CheckedChanged);
			// 
			// radioButton_XoaDinh
			// 
			this.radioButton_XoaDinh.ForeColor = System.Drawing.Color.Black;
			this.radioButton_XoaDinh.Location = new System.Drawing.Point(8, 48);
			this.radioButton_XoaDinh.Name = "radioButton_XoaDinh";
			this.radioButton_XoaDinh.Size = new System.Drawing.Size(70, 24);
			this.radioButton_XoaDinh.TabIndex = 1;
			this.radioButton_XoaDinh.Text = "Xóa đỉnh";
			this.radioButton_XoaDinh.CheckedChanged += new System.EventHandler(this.radioButton_XoaDinh_CheckedChanged);
			// 
			// radioButton_ThemDinh
			// 
			this.radioButton_ThemDinh.Checked = true;
			this.radioButton_ThemDinh.ForeColor = System.Drawing.Color.Black;
			this.radioButton_ThemDinh.Location = new System.Drawing.Point(8, 16);
			this.radioButton_ThemDinh.Name = "radioButton_ThemDinh";
			this.radioButton_ThemDinh.Size = new System.Drawing.Size(80, 24);
			this.radioButton_ThemDinh.TabIndex = 0;
			this.radioButton_ThemDinh.TabStop = true;
			this.radioButton_ThemDinh.Text = "Thêm đỉnh";
			this.radioButton_ThemDinh.CheckedChanged += new System.EventHandler(this.radioButton_ThemDinh_CheckedChanged);
			// 
			// checkBox1
			// 
			this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.checkBox1.ForeColor = System.Drawing.Color.Red;
			this.checkBox1.Location = new System.Drawing.Point(621, 432);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(91, 16);
			this.checkBox1.TabIndex = 18;
			this.checkBox1.Text = "Nhập chi phí";
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 3;
			this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem3});
			this.menuItem2.Text = "Help";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 0;
			this.menuItem3.Text = "Cách sử dụng";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// FormDo_thi_VH
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(790, 524);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.class_MienDoThi1);
			this.Controls.Add(this.groupBox3);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Menu = this.mainMenu1;
			this.Name = "FormDo_thi_VH";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Tim kiem loi giai tren do thi Va / Hoac";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormDo_thi_VH_Closing);
			this.Load += new System.EventHandler(this.FormDo_thi_VH_Load);
			this.groupBox3.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormDo_thi_VH_Load(object sender, System.EventArgs e)
		{
			string path=Application.StartupPath;
			Bitmap bmp=new Bitmap(path+@"\Images\nut1.bmp");
			Class_Tao_Control_Anh.Tao_DK_AnhBmp(button_Dinh_Va,bmp);
			bmp=new Bitmap(path+@"\Images\nut2.bmp");
			Class_Tao_Control_Anh.Tao_DK_AnhBmp(button_Dinh_So_cap,bmp);
			
		}
		private void dat_Check_false()
		{
			Class_MienDoThi.cho_phep_tao_dinh=false;
			Class_DinhDoThi.cho_phep_click=false;
			Class_DinhDoThi.cho_phep_keo=false;
			Class_DinhDoThi.dang_tao_cung=false;
			Class_DinhDoThi.dang_xoa_cung=false;
			Class_DinhDoThi.dang_xoa_dinh=false;
			Class_DinhDoThi.dang_chon_dinh=false;
			Class_DinhDoThi.dang_doi_trong_so=false;
			Class_DinhDoThi.dang_bot_con=false;
			Class_DinhDoThi.dang_them_con=false;
			Class_MienDoThi.Binh_thuong_DThi();
		}
		private void Khoi_tao_moi()
		{
			Class_MienDoThi_VH.Giai_phong_Static();
			radioButton_ThemDinh.Checked=true; 
		}
		private void dat_Menu_check_false()
		{
			if(kt_Rong)
				obj_Rong.Dispose();
			if (kt_Sau)
				obj_Sau.Dispose();
			if (kt_Cay)
				obj_Cay.Dispose();
			kt_Rong=false;
			kt_Sau=false;
			kt_Cay=false;
			menu_Theo_Rong.Checked=false;
			menu_Theo_Sau.Checked=false;
			menu_Theo_Cay_min.Checked=false;
			menu_Thuchien.Enabled=true;
		}
		private void button_Dinh_Va_Click(object sender, System.EventArgs e)
		{
			Class_MienDoThi.Dat_dinh_Va(Class_MienDoThi.chiso_1);
			Class_MienDoThi.Binh_thuong_DThi();
		}

		private void radioButton_ThemDinh_CheckedChanged(object sender, System.EventArgs e)
		{
			dat_Check_false();
			Class_DinhDoThi.cho_phep_click=true;
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

		private void radioButton_Chondinh_CheckedChanged(object sender, System.EventArgs e)
		{
			dat_Check_false();
			Class_DinhDoThi.cho_phep_click=true;
			Class_DinhDoThi.dang_chon_dinh=true;
		}

		private void radioButton_Doitrongso_CheckedChanged(object sender, System.EventArgs e)
		{
			dat_Check_false();
			Class_DinhDoThi.cho_phep_click=true;
			Class_DinhDoThi.dang_doi_trong_so=true;
		}

		private void button_Dinh_So_cap_Click(object sender, System.EventArgs e)
		{
			Class_MienDoThi.Dat_dinh_So_cap(Class_MienDoThi.chiso_1);
			Class_MienDoThi.Binh_thuong_DThi();
		}

		private void menu_New_Click(object sender, System.EventArgs e)
		{
			Class_DinhDoThi.ctrl=class_MienDoThi1;
			Khoi_tao_moi();
			if(kt_Rong)
				obj_Rong.Khoi_tao();
			if (kt_Sau)
				obj_Sau.Khoi_tao();
			if (kt_Sau)
				obj_Sau.Khoi_tao();
			if (kt_Cay)
				obj_Cay.Khoi_tao();
		}

		private void menu_Exit_Click(object sender, System.EventArgs e)
		{
			Khoi_tao_moi();
			this.Close();
		}
		private void menu_Open_Click(object sender, System.EventArgs e)
		{
			menu_New_Click(sender,e);
			OpenFileDialog openFile=new OpenFileDialog();
			openFile.DefaultExt="AOG";
			openFile.Filter="Diagram file (*.AOG)|*.AOG";
			openFile.ShowDialog();
			string path=openFile.FileName;
			if (path.Length>0)
			{
				try
				{
					openFile.OpenFile();
					string line;
					int so1,so2,so3;
					StreamReader strRead= new StreamReader(path); 
					line=strRead.ReadLine();
					int i=line.IndexOf(' ',0);
					so1=Convert.ToInt16(line.Substring(0,i),10);
					so2=Convert.ToInt16(line.Substring(i+1,line.Length-i-1),10);
					Class_MienDoThi.dinh_Max=so2;
					for (int j=1;j<=so1;j++)
					{
						int s1,s2,s3,s4,s5,s6,s7,s8,pos,pos1;
						line=strRead.ReadLine();
						pos=line.IndexOf(' ',0);
						s1=Convert.ToInt16(line.Substring(0,pos),10);
						pos1=line.IndexOf(' ',pos+1);
						s2=Convert.ToInt16(line.Substring(pos+1,pos1-pos-1),10);
						pos=line.IndexOf(' ',pos1+1);
						s3=Convert.ToInt16(line.Substring(pos1+1,pos-pos1-1),10);
						pos1=line.IndexOf(' ',pos+1);
						s4=Convert.ToInt16(line.Substring(pos+1,pos1-pos-1),10);
						pos=line.IndexOf(' ',pos1+1);
						s5=Convert.ToInt16(line.Substring(pos1+1,pos-pos1-1),10);
						pos1=line.IndexOf(' ',pos+1);
						s6=Convert.ToInt16(line.Substring(pos+1,pos1-pos-1),10);
						pos=line.IndexOf(' ',pos1+1);
						s7=Convert.ToInt16(line.Substring(pos1+1,pos-pos1-1),10);
						class_MienDoThi1.Them_dinh_vao_do_thi(s1,s2,s3,s4);
						if (s5==1)
							Class_MienDoThi.Dat_dinh_Va(s1);
						if (s6==1)
							Class_MienDoThi.Dat_dinh_So_cap(s1);
						foreach(Class_DinhDoThi obj in Class_MienDoThi_VH.DS_Dinh)
							if (obj.chiso==s1)
							{
								for (int k=1;k<=s7;k++)
								{
									pos1=line.IndexOf(' ',pos+1);
									s8=Convert.ToInt16(line.Substring(pos+1,pos1-pos-1),10);
									obj.Them_con(s8);
									pos=pos1;
								}
							}

					}
					line=strRead.ReadLine();
					so3=Convert.ToInt16(line,10);
					for (int j=1;j<=so3;j++)
					{
						int s1,s2,s3,pos,pos1;
						line=strRead.ReadLine();
						pos=line.IndexOf(' ',0);
						s1=Convert.ToInt16(line.Substring(0,pos),10);
						pos1=line.IndexOf(' ',pos+1);
						s2=Convert.ToInt16(line.Substring(pos+1,pos1-pos-1),10);
						s3=Convert.ToInt16(line.Substring(pos1+1,line.Length-pos1-1),10);
						Class_MienDoThi.Them_Cung_vao_do_thi(s1,s2,s3);
						class_MienDoThi1.Refresh();
					}
					strRead.Close();
				}
				catch (Exception a)
				{
					MessageBox.Show("Lỗi đọc file...","Thông báo..."); 
				}
			}
		}

		private void FormDo_thi_VH_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Khoi_tao_moi();
		}
		private void menu_Save_Click(object sender, System.EventArgs e)
		{
			SaveFileDialog saveFile=new SaveFileDialog();
			saveFile.DefaultExt="AOG";
			saveFile.Filter="Diagram file (*.AOG)|*.AOG";
			saveFile.OverwritePrompt=true;
			saveFile.ShowDialog();
			string path=saveFile.FileName;
			if (path.Trim().Length>0)
			{
				try
				{
					StreamWriter strWrite=new StreamWriter(path);
					strWrite.WriteLine(Class_MienDoThi.DS_Dinh.Count.ToString()+" "+Class_MienDoThi.dinh_Max.ToString());
					foreach (Class_DinhDoThi obj in Class_MienDoThi.DS_Dinh)
					{
						int i=0,j=0;
						if (obj.dinh_Va)
							i=1;
						if (obj.giai_duoc)
							j=1;
						strWrite.Write(obj.chiso.ToString()+" "+(obj.Toa_doX+8).ToString()+" "+(obj.Toa_doY+8).ToString()+" "+obj.heuristic.ToString()+" "+i.ToString()+" "+j.ToString()+" "+ obj.DS_Dinh_con.Count.ToString()+" ");
						for(int k=0; k<obj.DS_Dinh_con.Count;k++)
							strWrite.Write(obj.DS_Dinh_con[k].ToString()+" ");
						strWrite.WriteLine("");
					}
					strWrite.WriteLine(Class_MienDoThi.dsach_cung.DS_Cung.Count.ToString());
					foreach(Class_Cung obj in Class_MienDoThi.dsach_cung.DS_Cung)
					{
						strWrite.WriteLine(obj.dinh1.ToString()+" "+obj.dinh2.ToString()+" "+obj.trong_so.ToString());
					}
					strWrite.Close();
				}
				catch
				{
					MessageBox.Show("Lỗi lưu file...");
				}
			}
		}

		private void class_MienDoThi1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			textBox_Dinh1.Text=Class_MienDoThi.chiso_1.ToString();
			textBox_Dinh2.Text=Class_MienDoThi.chiso_2.ToString();
			textBox_DinhH.Text=Class_MienDoThi.chiso_1.ToString();
			if (Class_MienDoThi.Kiem_tra_cung_thuoc_DThi(Class_MienDoThi.chiso_1,Class_MienDoThi.chiso_2))
				textBox_Trongso.Text=Class_MienDoThi.Get_Trong_so_Cung(Class_MienDoThi.chiso_1,Class_MienDoThi.chiso_2).ToString();
		}

		private void textBox_Trongso_TextChanged(object sender, System.EventArgs e)
		{
			if ((textBox_Trongso.Text.Trim().Length>0)&&(FormInput.kiem_tra(textBox_Trongso.Text.Trim())))
				Class_DinhDoThi.trso=Convert.ToInt16(textBox_Trongso.Text,10);
			else
				Class_DinhDoThi.trso=0;
		}

		private void textBox_H_TextChanged(object sender, System.EventArgs e)
		{
			if (textBox_H.Text.Trim().Length==0)
				Class_DinhDoThi.h=Class_MienDoThi_VH.Get_Heuristic(Class_MienDoThi_VH.chiso_1);
			else if (FormInput.kiem_tra(textBox_H.Text.Trim()))
				Class_DinhDoThi.h=Convert.ToInt16(textBox_H.Text,10);
			else
				Class_DinhDoThi.h=Class_MienDoThi_VH.Get_Heuristic(Class_MienDoThi_VH.chiso_1);
		}

		private void button_HT_Click(object sender, System.EventArgs e)
		{
			FormHeuristic form1=new FormHeuristic();
			FormHeuristic.s="Bảng chi phí";
			form1.Show();
		}

		private void radioButton_Themcon_CheckedChanged(object sender, System.EventArgs e)
		{
			dat_Check_false();
			Class_DinhDoThi.cho_phep_click=true;
			Class_DinhDoThi.dang_them_con=true;
		}

		private void radioButton_Xoacon_CheckedChanged(object sender, System.EventArgs e)
		{
			dat_Check_false();
			Class_DinhDoThi.cho_phep_click=true;
			Class_DinhDoThi.dang_bot_con=true;
		}

		private void menu_Theo_Rong_Click(object sender, System.EventArgs e)
		{
			dat_Menu_check_false();
			thuattoan=Cac_Thuat_toan.TheoChieuRong;
			menu_Theo_Rong.Checked=true;
			obj_Rong=new Class_VH_Theo_chieu_rong();
			kt_Rong=true;
			this.Controls.Add(obj_Rong.richTextBox_Close);
			this.Controls.Add(obj_Rong.richTextBox_Kq);
			this.Controls.Add(obj_Rong.richTextBox_n);
			this.Controls.Add(obj_Rong.richTextBox_Open);
			this.Controls.Add(obj_Rong.richTextBox_Tn);
			this.Controls.Add(obj_Rong.label_C);
			this.Controls.Add(obj_Rong.label_n);
			this.Controls.Add(obj_Rong.label_O);
			this.Controls.Add(obj_Rong.label_T);
		}

		private void menu_Theo_Sau_Click(object sender, System.EventArgs e)
		{
			dat_Menu_check_false();
			thuattoan=Cac_Thuat_toan.TheoChieuSau;
			menu_Theo_Sau.Checked=true;
			obj_Sau=new Class_VH_Theo_chieu_sau();
			kt_Sau=true;
			this.Controls.Add(obj_Sau.richTextBox_Close);
			this.Controls.Add(obj_Sau.richTextBox_Kq);
			this.Controls.Add(obj_Sau.richTextBox_n);
			this.Controls.Add(obj_Sau.richTextBox_Open);
			this.Controls.Add(obj_Sau.richTextBox_Tn);
			this.Controls.Add(obj_Sau.label_C);
			this.Controls.Add(obj_Sau.label_n);
			this.Controls.Add(obj_Sau.label_O);
			this.Controls.Add(obj_Sau.label_T);
		}

		private void menu_Theo_Cay_min_Click(object sender, System.EventArgs e)
		{
			dat_Menu_check_false();
			thuattoan=Cac_Thuat_toan.CayLoiGiai;
			menu_Theo_Cay_min.Checked=true;
			obj_Cay=new Class_VH_Cay_loi_giai();
			kt_Cay=true;
			this.Controls.Add(obj_Cay.richTextBox_Close);
			this.Controls.Add(obj_Cay.richTextBox_Kq);
			this.Controls.Add(obj_Cay.richTextBox_n);
			this.Controls.Add(obj_Cay.richTextBox_Open);
			this.Controls.Add(obj_Cay.richTextBox_Tn);
			this.Controls.Add(obj_Cay.label_C);
			this.Controls.Add(obj_Cay.label_n);
			this.Controls.Add(obj_Cay.label_O);
			this.Controls.Add(obj_Cay.label_T);
		}

		private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
		{
			Class_DinhDoThi.cho_tao_heurstic=checkBox1.Checked;
		}

		private void menu_Thuchien_Click(object sender, System.EventArgs e)
		{
			switch (thuattoan)
			{
				case Cac_Thuat_toan.TheoChieuRong:
					obj_Rong.Tim_theo_chieu_rong();
					break;
				case Cac_Thuat_toan.TheoChieuSau:
					obj_Sau.Tim_theo_chieu_sau();
					break;
				case Cac_Thuat_toan.CayLoiGiai:
					obj_Cay.Tim_kiem_cay_loi_giai_cuc_tieu();
					break;
				/*case Cac_Thuat_toan.TotNhatDauTien:
					obj_Dau.Tim_kiem_tot_nhat_dau_tien();
					break;
				case Cac_Thuat_toan.NganNhat:
					obj_Ngannhat.Tim_kiem_theo_AT();
					break;
				case Cac_Thuat_toan.ASao:
					obj_A.Tim_kiem_theo_A_sao();
					break;
				case Cac_Thuat_toan.LeoDoi:
					obj_LeoDoi.Khoi_tao();
					obj_LeoDoi.Tim_kiem_leo_doi(Class_MienDoThi.chiso_1,Class_MienDoThi.chiso_2);
					break;*/
			}
			Class_MienDoThi_VH.Dat_lai_kha_nang_Giai_duoc();
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			Form_Dothi_VH_Help obj=new Form_Dothi_VH_Help();
			obj.Show();
		}
	}
}
