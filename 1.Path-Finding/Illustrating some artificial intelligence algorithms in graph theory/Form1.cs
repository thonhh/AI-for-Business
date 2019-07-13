using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Drawing2D;

namespace Tri_tue_nhan_tao
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class FormKhoidau : System.Windows.Forms.Form
	{
		private System.Windows.Forms.StatusBarPanel panel1;
		private System.Windows.Forms.Timer timer1;
		private string path;
		private System.Windows.Forms.MainMenu mainMenuKhoidau;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuThoat;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuDothi;
		private System.Windows.Forms.MenuItem menuDothiVH;
		private System.Windows.Forms.MenuItem menuLogic;
		private System.Windows.Forms.MenuItem menuSuydien;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuNoidung;
		private System.Windows.Forms.MenuItem menuTacgia;
		private System.Windows.Forms.PictureBox pictureBox;
		private System.ComponentModel.IContainer components;

		public FormKhoidau()
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
				if (components != null) 
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormKhoidau));
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.mainMenuKhoidau = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuThoat = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuDothi = new System.Windows.Forms.MenuItem();
			this.menuDothiVH = new System.Windows.Forms.MenuItem();
			this.menuLogic = new System.Windows.Forms.MenuItem();
			this.menuSuydien = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuNoidung = new System.Windows.Forms.MenuItem();
			this.menuTacgia = new System.Windows.Forms.MenuItem();
			this.pictureBox = new System.Windows.Forms.PictureBox();
			this.SuspendLayout();
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 150;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// mainMenuKhoidau
			// 
			this.mainMenuKhoidau.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							this.menuItem1,
																							this.menuItem3,
																							this.menuItem8});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuThoat});
			this.menuItem1.Text = "File";
			// 
			// menuThoat
			// 
			this.menuThoat.Index = 0;
			this.menuThoat.Text = "Exit";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuDothi,
																					  this.menuDothiVH,
																					  this.menuLogic,
																					  this.menuSuydien});
			this.menuItem3.Text = "Problems";
			// 
			// menuDothi
			// 
			this.menuDothi.Index = 0;
			this.menuDothi.Text = "Tìm kiếm đường đi trên đồ thị";
			this.menuDothi.Click += new System.EventHandler(this.menuDothi_Click);
			// 
			// menuDothiVH
			// 
			this.menuDothiVH.Index = 1;
			this.menuDothiVH.Text = "Tìm kiếm lời giải trên đồ thị Và / Hoặc";
			// 
			// menuLogic
			// 
			this.menuLogic.Index = 2;
			this.menuLogic.Text = "Chứng minh bài toán biểu diễn bằng Logic hình thức";
			// 
			// menuSuydien
			// 
			this.menuSuydien.Index = 3;
			this.menuSuydien.Text = "Suy diễn trên luật sản xuất";
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 2;
			this.menuItem8.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuNoidung,
																					  this.menuTacgia});
			this.menuItem8.Text = "Help";
			// 
			// menuNoidung
			// 
			this.menuNoidung.Index = 0;
			this.menuNoidung.Text = "Nội dung";
			// 
			// menuTacgia
			// 
			this.menuTacgia.Index = 1;
			this.menuTacgia.Text = "Tác giả";
			// 
			// pictureBox
			// 
			this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox.Image")));
			this.pictureBox.Location = new System.Drawing.Point(0, 0);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new System.Drawing.Size(576, 372);
			this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			// 
			// FormKhoidau
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
			this.CausesValidation = false;
			this.ClientSize = new System.Drawing.Size(576, 372);
			this.Controls.Add(this.pictureBox);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Menu = this.mainMenuKhoidau;
			this.Name = "FormKhoidau";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Tri tue nhan tao";
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new FormKhoidau());
		}

		private void FormMain_Load(object sender, System.EventArgs e)
		{
			path= Application.StartupPath; 
			pictureBox.Image= Image.FromFile(path+"\\Images\\COMPUTER.bmp",false);
			CreateMyStatusBar();
		}
		private void CreateMyStatusBar()
		{
			StatusBar statusBar = new StatusBar();
			panel1 = new StatusBarPanel();
			panel1.BorderStyle = StatusBarPanelBorderStyle.Sunken;
			panel1.Text = "Trên con đường thành công không có dấu chân của kẻ lười biếng.           *   *   *   *   *   *          ";
			panel1.AutoSize = StatusBarPanelAutoSize.Spring;
			statusBar.ShowPanels = true;
			statusBar.Panels.Add(panel1);
			this.Controls.Add(statusBar);
		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{
			string temp=panel1.Text.Substring(0,1);
			panel1.Text=panel1.Text.Remove(0,1);
			panel1.Text+=temp;
		}

		private void menuDothi_Click(object sender, System.EventArgs e)
		{
			FormDothi form=new FormDothi();
			form.ShowDialog();
		}

	}
}
