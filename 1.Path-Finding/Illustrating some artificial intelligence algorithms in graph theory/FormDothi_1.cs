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
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.Label label_i;
		private System.Windows.Forms.Label label_j;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Button button_OK1;
		private System.Windows.Forms.Button button_OK2;
		private System.Windows.Forms.TextBox textBox_Cj;
		private System.Windows.Forms.TextBox textBox_Ci;
		private System.Windows.Forms.TextBox textBox_Chiphi;
		private System.Windows.Forms.TextBox textBox_Dinhi;
		private System.Windows.Forms.TextBox textBox_Hi;
		private System.Windows.Forms.RichTextBox richTextBox_n;
		private System.Windows.Forms.RichTextBox richTextBox_T;
		private System.Windows.Forms.RichTextBox richTextBox_Open;
		private System.Windows.Forms.RichTextBox richTextBox_Close;
		private System.ComponentModel.IContainer components;
		//
		//My var
		private System.Windows.Forms.Button button_Vecung;
		
		private System.Windows.Forms.RichTextBox richTextBox_DDi;
		
		private System.Windows.Forms.RadioButton radioButton_Taodinh;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.RadioButton radioButton_Vecung;
		
	
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormDothi));
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label_i = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label_j = new System.Windows.Forms.Label();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.textBox_Cj = new System.Windows.Forms.TextBox();
			this.button_OK1 = new System.Windows.Forms.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.textBox_Ci = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textBox_Chiphi = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.textBox_Dinhi = new System.Windows.Forms.TextBox();
			this.textBox_Hi = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.button_OK2 = new System.Windows.Forms.Button();
			this.richTextBox_n = new System.Windows.Forms.RichTextBox();
			this.richTextBox_T = new System.Windows.Forms.RichTextBox();
			this.richTextBox_Open = new System.Windows.Forms.RichTextBox();
			this.richTextBox_Close = new System.Windows.Forms.RichTextBox();
			this.button_Vecung = new System.Windows.Forms.Button();
			this.richTextBox_DDi = new System.Windows.Forms.RichTextBox();
			this.radioButton_Taodinh = new System.Windows.Forms.RadioButton();
			this.radioButton_Vecung = new System.Windows.Forms.RadioButton();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(792, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "Các phương pháp tìm kiếm trên đồ thị                                        ";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.Red;
			this.label3.Location = new System.Drawing.Point(318, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(14, 16);
			this.label3.TabIndex = 6;
			this.label3.Text = "n";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.ForeColor = System.Drawing.Color.Red;
			this.label4.Location = new System.Drawing.Point(401, 48);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(32, 16);
			this.label4.TabIndex = 7;
			this.label4.Text = "T(n)";
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label5.ForeColor = System.Drawing.Color.Red;
			this.label5.Location = new System.Drawing.Point(528, 48);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(48, 16);
			this.label5.TabIndex = 8;
			this.label5.Text = "Open()";
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label6.ForeColor = System.Drawing.Color.Red;
			this.label6.Location = new System.Drawing.Point(680, 48);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(48, 16);
			this.label6.TabIndex = 9;
			this.label6.Text = "Close()";
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label7.ForeColor = System.Drawing.Color.DarkGreen;
			this.label7.Location = new System.Drawing.Point(104, 40);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100, 24);
			this.label7.TabIndex = 10;
			this.label7.Text = "Nhập đồ thị";
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label8.ForeColor = System.Drawing.Color.DodgerBlue;
			this.label8.Location = new System.Drawing.Point(307, 406);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(132, 22);
			this.label8.TabIndex = 11;
			this.label8.Text = "Nhập chi phí C(i,j)";
			// 
			// label_i
			// 
			this.label_i.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label_i.Location = new System.Drawing.Point(224, 448);
			this.label_i.Name = "label_i";
			this.label_i.Size = new System.Drawing.Size(40, 16);
			this.label_i.TabIndex = 12;
			this.label_i.Text = "Đỉnh";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(312, 450);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(16, 16);
			this.pictureBox1.TabIndex = 13;
			this.pictureBox1.TabStop = false;
			// 
			// label_j
			// 
			this.label_j.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label_j.Location = new System.Drawing.Point(360, 448);
			this.label_j.Name = "label_j";
			this.label_j.Size = new System.Drawing.Size(40, 16);
			this.label_j.TabIndex = 14;
			this.label_j.Text = "Đỉnh";
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
			this.pictureBox2.Location = new System.Drawing.Point(336, 450);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(16, 16);
			this.pictureBox2.TabIndex = 15;
			this.pictureBox2.TabStop = false;
			// 
			// textBox_Cj
			// 
			this.textBox_Cj.Enabled = false;
			this.textBox_Cj.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textBox_Cj.Location = new System.Drawing.Point(400, 447);
			this.textBox_Cj.Name = "textBox_Cj";
			this.textBox_Cj.Size = new System.Drawing.Size(40, 23);
			this.textBox_Cj.TabIndex = 16;
			this.textBox_Cj.Text = "";
			// 
			// button_OK1
			// 
			this.button_OK1.Enabled = false;
			this.button_OK1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button_OK1.Location = new System.Drawing.Point(339, 480);
			this.button_OK1.Name = "button_OK1";
			this.button_OK1.TabIndex = 17;
			this.button_OK1.Text = "OK";
			
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label9.ForeColor = System.Drawing.Color.RoyalBlue;
			this.label9.Location = new System.Drawing.Point(556, 408);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(217, 16);
			this.label9.TabIndex = 18;
			this.label9.Text = "Nhập hàm đánh giá Heuristic H(i)";
			// 
			// textBox_Ci
			// 
			this.textBox_Ci.Enabled = false;
			this.textBox_Ci.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textBox_Ci.Location = new System.Drawing.Point(264, 447);
			this.textBox_Ci.Name = "textBox_Ci";
			this.textBox_Ci.Size = new System.Drawing.Size(40, 23);
			this.textBox_Ci.TabIndex = 19;
			this.textBox_Ci.Text = "";
			// 
			// label10
			// 
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label10.Location = new System.Drawing.Point(448, 448);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(24, 16);
			this.label10.TabIndex = 20;
			this.label10.Text = "là: ";
			// 
			// textBox_Chiphi
			// 
			this.textBox_Chiphi.Enabled = false;
			this.textBox_Chiphi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textBox_Chiphi.Location = new System.Drawing.Point(488, 447);
			this.textBox_Chiphi.Name = "textBox_Chiphi";
			this.textBox_Chiphi.Size = new System.Drawing.Size(40, 23);
			this.textBox_Chiphi.TabIndex = 21;
			this.textBox_Chiphi.Text = "";
			// 
			// label11
			// 
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label11.Location = new System.Drawing.Point(592, 449);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(16, 16);
			this.label11.TabIndex = 22;
			this.label11.Text = "H";
			// 
			// textBox_Dinhi
			// 
			this.textBox_Dinhi.Enabled = false;
			this.textBox_Dinhi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textBox_Dinhi.Location = new System.Drawing.Point(616, 448);
			this.textBox_Dinhi.Name = "textBox_Dinhi";
			this.textBox_Dinhi.Size = new System.Drawing.Size(40, 23);
			this.textBox_Dinhi.TabIndex = 23;
			this.textBox_Dinhi.Text = "";
			// 
			// textBox_Hi
			// 
			this.textBox_Hi.Enabled = false;
			this.textBox_Hi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textBox_Hi.Location = new System.Drawing.Point(704, 448);
			this.textBox_Hi.Name = "textBox_Hi";
			this.textBox_Hi.Size = new System.Drawing.Size(40, 23);
			this.textBox_Hi.TabIndex = 24;
			this.textBox_Hi.Text = "";
			// 
			// label12
			// 
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label12.Location = new System.Drawing.Point(672, 448);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(16, 16);
			this.label12.TabIndex = 25;
			this.label12.Text = "=";
			// 
			// button_OK2
			// 
			this.button_OK2.Enabled = false;
			this.button_OK2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button_OK2.Location = new System.Drawing.Point(627, 480);
			this.button_OK2.Name = "button_OK2";
			this.button_OK2.TabIndex = 26;
			this.button_OK2.Text = "OK";
			
			// 
			// richTextBox_n
			// 
			this.richTextBox_n.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.richTextBox_n.Location = new System.Drawing.Point(300, 72);
			this.richTextBox_n.Name = "richTextBox_n";
			this.richTextBox_n.Size = new System.Drawing.Size(50, 296);
			this.richTextBox_n.TabIndex = 27;
			this.richTextBox_n.Text = "";
			// 
			// richTextBox_T
			// 
			this.richTextBox_T.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.richTextBox_T.Location = new System.Drawing.Point(352, 72);
			this.richTextBox_T.Name = "richTextBox_T";
			this.richTextBox_T.Size = new System.Drawing.Size(130, 296);
			this.richTextBox_T.TabIndex = 28;
			this.richTextBox_T.Text = "";
			// 
			// richTextBox_Open
			// 
			this.richTextBox_Open.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.richTextBox_Open.Location = new System.Drawing.Point(484, 72);
			this.richTextBox_Open.Name = "richTextBox_Open";
			this.richTextBox_Open.Size = new System.Drawing.Size(148, 296);
			this.richTextBox_Open.TabIndex = 29;
			this.richTextBox_Open.Text = "";
			// 
			// richTextBox_Close
			// 
			this.richTextBox_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.richTextBox_Close.Location = new System.Drawing.Point(632, 72);
			this.richTextBox_Close.Name = "richTextBox_Close";
			this.richTextBox_Close.Size = new System.Drawing.Size(152, 296);
			this.richTextBox_Close.TabIndex = 30;
			this.richTextBox_Close.Text = "";
			// 
			// button_Vecung
			// 
			
			// richTextBox_DDi
			// 
			this.richTextBox_DDi.Location = new System.Drawing.Point(300, 370);
			this.richTextBox_DDi.Name = "richTextBox_DDi";
			this.richTextBox_DDi.Size = new System.Drawing.Size(484, 22);
			this.richTextBox_DDi.TabIndex = 32;
			this.richTextBox_DDi.Text = "";
			// 
			// radioButton_Taodinh
			// 
			this.radioButton_Taodinh.Checked = true;
			this.radioButton_Taodinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.radioButton_Taodinh.Location = new System.Drawing.Point(16, 432);
			this.radioButton_Taodinh.Name = "radioButton_Taodinh";
			this.radioButton_Taodinh.Size = new System.Drawing.Size(72, 24);
			this.radioButton_Taodinh.TabIndex = 33;
			this.radioButton_Taodinh.TabStop = true;
			this.radioButton_Taodinh.Text = "Tạo đỉnh";
			
			// 
			// radioButton_Vecung
			// 
			this.radioButton_Vecung.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.radioButton_Vecung.Location = new System.Drawing.Point(16, 478);
			this.radioButton_Vecung.Name = "radioButton_Vecung";
			this.radioButton_Vecung.Size = new System.Drawing.Size(72, 24);
			this.radioButton_Vecung.TabIndex = 34;
			this.radioButton_Vecung.Text = "Vẽ cung";
			this.radioButton_Vecung.CheckedChanged += new System.EventHandler(this.radioButton_Vecung_CheckedChanged);
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Location = new System.Drawing.Point(216, 416);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(320, 96);
			this.panel1.TabIndex = 35;
			// 
			// panel2
			// 
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Location = new System.Drawing.Point(544, 416);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(240, 96);
			this.panel2.TabIndex = 36;
			// 
			// panel3
			// 
			this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel3.Location = new System.Drawing.Point(8, 416);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(200, 96);
			this.panel3.TabIndex = 37;
			// 
			// FormDothi
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(792, 545);
			this.Controls.Add(this.radioButton_Vecung);
			this.Controls.Add(this.radioButton_Taodinh);
			this.Controls.Add(this.richTextBox_DDi);
			this.Controls.Add(this.button_Vecung);
			this.Controls.Add(this.richTextBox_Close);
			this.Controls.Add(this.richTextBox_Open);
			this.Controls.Add(this.richTextBox_T);
			this.Controls.Add(this.richTextBox_n);
			this.Controls.Add(this.button_OK2);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.textBox_Hi);
			this.Controls.Add(this.textBox_Dinhi);
			this.Controls.Add(this.textBox_Chiphi);
			this.Controls.Add(this.textBox_Ci);
			this.Controls.Add(this.textBox_Cj);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.button_OK1);
			this.Controls.Add(this.pictureBox2);
			this.Controls.Add(this.label_j);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label_i);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel2);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormDothi";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Tri tue nhan tao";
			
			
			this.ResumeLayout(false);

		}
		#endregion

		private void khoi_tao()
		{
		}

	
		private void Tim_kiem_theo_chieu_rong()
		{
			tim_duoc=false;
			for (int i=0;i<=sodinh;i++)
				truoc[i]=0;
			n0=Class_Graph.chiso1;
			n1=Class_Graph.chiso2;
			byte n;
			Class_Queue Open,Close;
			Open=new Class_Queue();
			Close=new Class_Queue();
			Open.Append(n0);
			richTextBox_Open.Text+=Open.List_Queue()+"\n";
			richTextBox_Close.Text+="\n";
			richTextBox_n.Text+="\n";
			richTextBox_T.Text+="\n";
			while (!Open.Is_empty())
			{
				n=Open.Take();
				richTextBox_n.Text+=n.ToString()+"\n";
				if (n==n1)
				{
					richTextBox_T.Text+="Đến đích -> dừng"+"\n";
					tim_duoc=true;
					break;	
				}
				Close.Append(n);
				richTextBox_Close.Text+=Close.List_Queue()+"\n";
				for (byte i=1;i<=sodinh;i++)
				{
					if (mt_ke[n,i]==1)
					{
						richTextBox_T.Text+=i.ToString()+" ";
						if ((!Open.In_list(i))&&(!Close.In_list(i)))
						{
							Open.Append(i);
							truoc[i]=n;
						}
					}
				}
				richTextBox_T.Text+="\n";
				richTextBox_Open.Text+=Open.List_Queue()+"\n";
			}
			if (tim_duoc)
			{
				richTextBox_DDi.Text="Đường đi theo BrFS : ";
				inkq(n1);
			}
			else
				richTextBox_DDi.Text="Không tìm thấy đường đi! ";
		}
		private void Tim_kiem_theo_chieu_sau()
		{
			tim_duoc=false;
			for (int i=0;i<=sodinh;i++)
				truoc[i]=0;
			n0=Class_Graph.chiso1;
			n1=Class_Graph.chiso2;
			byte n;
			Class_Stack Open,Close;
			Open=new Class_Stack();
			Close=new Class_Stack();
			Open.Push(n0);
			richTextBox_Open.Text+=Open.List_Stack()+"\n";
			richTextBox_Close.Text+="\n";
			richTextBox_n.Text+="\n";
			richTextBox_T.Text+="\n";
			while (!Open.Is_empty())
			{
				n=Open.Pop();
				richTextBox_n.Text+=n.ToString()+"\n";
				if (n==n1)
				{
					richTextBox_T.Text+="Đến đích -> dừng"+"\n";
					tim_duoc=true;
					break;	
				}
				Close.Push(n);
				richTextBox_Close.Text+=Close.List_Stack()+"\n";
				for (byte i=1;i<=sodinh;i++)
				{
					if (mt_ke[n,i]==1)
					{
						richTextBox_T.Text+=i.ToString()+" ";
						if ((!Open.In_Stack(i))&&(!Close.In_Stack(i)))
						{
							Open.Push(i);
							truoc[i]=n;
						}
					}
				}
				richTextBox_T.Text+="\n";
				richTextBox_Open.Text+=Open.List_Stack()+"\n";
			}
			if (tim_duoc)
			{
				richTextBox_DDi.Text="Đường đi theo DFS : ";
				inkq(n1);
			}
			else
				richTextBox_DDi.Text="Không tìm thấy đường đi! ";
		}
		private void inkq(byte n)
		{
			if (n!=n0)
				inkq(truoc[n]);
			if (n!=n1)
				richTextBox_DDi.Text+=n.ToString()+"->";
			else
				richTextBox_DDi.Text+=n.ToString();
		}

	
		private bool Tim_kiem_co_gioi_han_do_sau(byte Start,byte Goal,int do_sau)
		{
			tim_duoc=false;
			for (int i=0;i<=sodinh;i++)
				truoc[i]=0;
			Class_Stack Open=new Class_Stack(),Close=new Class_Stack();
			int[] Mang_do_sau=new int[21];
			n0=Start;
			n1=Goal;
			Mang_do_sau[Start]=0;
			Open.Push(n0);
			byte n;
			richTextBox_Open.Text+=Open.List_Stack()+ "Độ sâu "+do_sau.ToString()+"\n";
			richTextBox_Close.Text+="\n";
			richTextBox_n.Text+="\n";
			richTextBox_T.Text+="\n";
			while (!Open.Is_empty())
			{
				n=Open.Pop();
				richTextBox_n.Text+=n.ToString()+"\n";
				if (n==n1)
				{
					richTextBox_T.Text+="Đến đích -> dừng"+"\n";
					tim_duoc=true;
					break;
				}
				Close.Push(n);
				richTextBox_Close.Text+=Close.List_Stack()+"\n";
				if (Mang_do_sau[n]<do_sau)
				{
					for (byte i=1;i<=sodinh;i++)
					{
						if (mt_ke[n,i]==1)
						{
							richTextBox_T.Text+=i.ToString()+" ";
							if ((!Open.In_Stack(i))&&(!Close.In_Stack(i)))
							{
								Open.Push(i);
								truoc[i]=n;
								Mang_do_sau[i]=Mang_do_sau[n]+1;
							}
						}
					}
					richTextBox_T.Text+="\n";
					richTextBox_Open.Text+=Open.List_Stack()+"\n"; 
				}
				else
				{
					richTextBox_T.Text+=" Quá độ sâu ("+ do_sau.ToString()+")" +"\n";
					richTextBox_Open.Text+=Open.List_Stack()+"\n";
				}
			}
			if (tim_duoc)
			{
				richTextBox_DDi.Text="Đường đi theo DDS : ";
				inkq(n1);
				return true;
			}
			else
			{
				return false;
			}
		}
		private void Tim_kiem_sau_dan()
		{
			bool ktra=false;
			for (int i=1;i<=15;i++)
				if (Tim_kiem_co_gioi_han_do_sau(Class_Graph.chiso1,Class_Graph.chiso2,i))
				{
					ktra=true;
					break;
				}
			if (!ktra)
				richTextBox_DDi.Text="Không tìm thấy đường đi!";
		}

		
		
		private void Tim_kiem_tot_nhat_dau_tien()
		{
			tim_duoc=false;
			for (int i=0;i<=sodinh;i++)
				truoc[i]=0;
			n0=Class_Graph.chiso1;
			n1=Class_Graph.chiso2;
			byte n;
			Class_Stack Open,Close;
			Open=new Class_Stack();
			Close=new Class_Stack();
			Open.Push(n0);
			richTextBox_Open.Text+=Open.List_Stack()+"\n";
			richTextBox_Close.Text+="\n";
			richTextBox_n.Text+="\n";
			richTextBox_T.Text+="\n";
			while (!Open.Is_empty())
			{
				n=Open.Pop();
				richTextBox_n.Text+=n.ToString()+"\n";
				if (n==n1)
				{
					richTextBox_T.Text+="Đến đích -> dừng"+"\n";
					tim_duoc=true;
					break;	
				}
				Close.Push(n);
				richTextBox_Close.Text+=Close.List_Stack()+"\n";
				for (byte i=1;i<=sodinh;i++)
				{
					if (mt_ke[n,i]==1)
					{
						richTextBox_T.Text+=i.ToString()+" ";
						if ((!Open.In_Stack(i))&&(!Close.In_Stack(i)))
						{
							Open.Push(i);
							truoc[i]=n;
						}
					}
				}
				int i1,j,k;
				for (i1=1;i1<Open.Top;i1++)
				{
					j=i1;
					for (k=i1+1;k<=Open.Top;k++)
						if (h[Open.S[k]]<h[Open.S[j]])
							j=k;
					byte temp=Open.S[i1];
					Open.S[i1]=Open.S[j];
					Open.S[j]=temp;
				}
				richTextBox_T.Text+="\n";
				richTextBox_Open.Text+=Open.List_Stack()+"\n";
			}
			if (tim_duoc)
			{
				richTextBox_DDi.Text="Đường đi theo BFS : ";
				inkq(n1);
			}
			else
				richTextBox_DDi.Text="Không tìm thấy đường đi! ";
			
		}
		private byte Chi_so_Min_STACK(Class_Stack St, bool k)
		{
			byte temp=St.Top;
			for (byte i=St.Top;i>=1;i--)
				if (k)
				{
					if (g[St.S[temp]]>g[St.S[i]])
						temp=i;
				}
				else
				{
					if (f[St.S[temp]]>f[St.S[i]])
						temp=i;
				}
			return temp;
		}
		private void Tim_kiem_theo_AT()
		{
			n0=Class_Graph.chiso1;
			n1=Class_Graph.chiso2;
			Class_Stack Open=new Class_Stack(),Close=new Class_Stack();
			byte n;
			g[n0]=0;
			Open.Push(n0);
			while (!Open.Is_empty())
			{
				byte min=Chi_so_Min_STACK(Open,true);
				byte temp=Open.S[Open.Top];
				Open.S[Open.Top]=Open.S[min];
				Open.S[min]=temp;
				richTextBox_Open.Text+=Open.List_Stack()+ "<"+ Open.S[Open.Top].ToString()+">\n";
				richTextBox_Close.Text+="\n";
				richTextBox_n.Text+="\n";
				richTextBox_T.Text+="\n";
				n=Open.Pop();
				richTextBox_n.Text+=n.ToString();
				if (n==n1)
				{
					richTextBox_T.Text+="Đến đích -> dừng"+"\n";
					tim_duoc=true;
					break;
				}
				Close.Push(n);
				richTextBox_Close.Text+=Close.List_Stack();
				for (byte i=1;i<=sodinh;i++)
				{
					if (mt_ke[n,i]==1)
					{
						richTextBox_T.Text+=i.ToString()+" ";
						if ((!Open.In_Stack(i))&&(!Close.In_Stack(i)))
						{
							Open.Push(i);
							g[i]=g[n]+mt_trongso[n,i];
							truoc[i]=n;
						}
						else if ((!Close.In_Stack(i))&&(g[i]>g[n]+mt_trongso[n,i]))
						{
							g[i]=g[n]+mt_trongso[n,i];
							truoc[i]=n;	
						}
					}
				}
	 		}
			if (tim_duoc)
			{
				richTextBox_DDi.Text="Đường đi ngắn nhất (theo AT) : ";
				inkq(n1);
				richTextBox_DDi.Text+=" chi phí g= "+g[n1].ToString();
			}
			else
				richTextBox_DDi.Text="Không tìm thấy đường đi! ";
		}
		private void khoi_tao_bien()
		{
			mt_trongso=new byte[21,21];
			g=new int[21];
			f=new int[21];
			da_tao_bien=true;
		}

	

		private void radioButton_Vecung_CheckedChanged(object sender, System.EventArgs e)
		{
			duoc_kich=false;
			Class_Graph.cho_phep_click=true;
			button_Vecung.Enabled=true;
		}

		
		private void Tim_kiem_theo_A_sao()
		{
			n0=Class_Graph.chiso1;
			n1=Class_Graph.chiso2;
			Class_Stack Open=new Class_Stack(),Close=new Class_Stack();
			byte n;
			f[n0]=0;
			g[n0]=0;
			Open.Push(n0);
			while (!Open.Is_empty())
			{
				byte min=Chi_so_Min_STACK(Open,false);
				byte temp=Open.S[Open.Top];
				Open.S[Open.Top]=Open.S[min];
				Open.S[min]=temp;
				richTextBox_Open.Text+=Open.List_Stack()+ "<"+ Open.S[Open.Top].ToString()+">\n";
				richTextBox_Close.Text+="\n";
				richTextBox_n.Text+="\n";
				richTextBox_T.Text+="\n";
				n=Open.Pop();
				richTextBox_n.Text+=n.ToString();
				if (n==n1)
				{
					richTextBox_T.Text+="Đến đích -> dừng"+"\n";
					tim_duoc=true;
					break;
				}
				Close.Push(n);
				richTextBox_Close.Text+=Close.List_Stack();
				for (byte i=1;i<=sodinh;i++)
				{
					if (mt_ke[n,i]==1)
					{
						richTextBox_T.Text+=i.ToString()+" ";
						if ((!Open.In_Stack(i))&&(!Close.In_Stack(i)))
						{
							Open.Push(i);
							g[i]=g[n]+mt_trongso[n,i];
							f[i]=h[i]+g[i];
							truoc[i]=n;
						}
						else if ((!Close.In_Stack(i))&&(f[i]>h[i]+g[n]+mt_trongso[n,i]))
						{
							g[i]=g[n]+mt_trongso[n,i];
							f[i]=h[i]+g[i];
							truoc[i]=n;	
						}
					}
				}
			}
			if (tim_duoc)
			{
				richTextBox_DDi.Text="Đường đi ngắn nhất (theo A*) : ";
				inkq(n1);
				richTextBox_DDi.Text+=" chi phí f= "+f[n1].ToString();
			}
			else
				richTextBox_DDi.Text="Không tìm thấy đường đi! ";
		}
		private void Tim_kiem_leo_doi()
		{
			tim_duoc=false;
			for (int i=0;i<=sodinh;i++)
				truoc[i]=0;
			n0=Class_Graph.chiso1;
			n1=Class_Graph.chiso2;
			byte n;
			byte[] L=new byte[sodinh];
			Class_Stack Open,Close;
			Open=new Class_Stack();
			Close=new Class_Stack();
			Open.Push(n0);
			richTextBox_Open.Text+=Open.List_Stack()+"\n";
			richTextBox_Close.Text+="\n";
			richTextBox_n.Text+="\n";
			richTextBox_T.Text+="\n";
			string st="";
			while (!Open.Is_empty())
			{
				n=Open.Pop();
				richTextBox_n.Text+=n.ToString()+"\n";
				if (n==n1)
				{
					richTextBox_T.Text+="Đến đích -> dừng"+"\n";
					tim_duoc=true;
					break;
				}
				Close.Push(n);
				richTextBox_Close.Text+=Close.List_Stack()+"\n";
				int chso=0;
				for (byte i=1;i<=sodinh;i++)
				{
					if (mt_ke[n,i]==1)
					{
						richTextBox_T.Text+=i.ToString()+" ";
						if ((!Open.In_Stack(i))&&(!Close.In_Stack(i)))
						{
							truoc[i]=n;
							L[++chso]=i;//đưa các đỉnh kề chưa chọn vào L
						}
					}
				}
				bool thuoc_dich=false;
				for (byte i=1;i<=chso;i++)
					if (L[i]==n1)
					{
						Open.Top=0;
						Open.Push(L[i]);
						thuoc_dich=true;//chọn ngay đỉnh đích cho vào Stack
						st=" ->Chọn đỉnh đích.";
						break;
					}
				if (!thuoc_dich)//không có đỉnh kề nào là đỉnh đích
				{
					for(int i=1;i<chso;i++)
					{
						int j=i;
						for (int k=i+1;k<=chso;k++)
							if(h[L[j]]>h[L[k]])
								j=k;
						byte temp;
						temp=L[i];
						L[i]=L[j];
						L[j]=temp;
						//sắp xếp lại L theo h[i] tăng dần	
					}
					for(int i=1;i<=chso;i++)
					{
						Open.Push(L[i]);
					}
				}
				richTextBox_T.Text+="\n";
				richTextBox_Open.Text+=Open.List_Stack()+st+"\n";
			}
			if (tim_duoc)
			{
				richTextBox_DDi.Text="Đường đi theo Climbing-Hill : ";
				inkq(n1);
			}
			else
				richTextBox_DDi.Text="Không tìm thấy đường đi! ";
		}

	}
}
