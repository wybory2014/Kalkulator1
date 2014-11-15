using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
namespace Kalkulator1
{
	public class Commit : Form
	{
		private string filepath;
		private Start form;
		private ProtocolForm form2;
		private SendProtocol form3;
		private Attendance form4;
		private bool loggin;
		private System.ComponentModel.IContainer components = null;
		private Panel panelNet;
		private Button LoginNext;
		private TextBox password;
		private Label label3;
		private Label error;
		private Label label1;
		private Label label2;
		private Label code;
		private Label codeBarLabel;
		public Commit()
		{
			this.InitializeComponent();
			this.filepath = "";
			this.loggin = false;
			string[] filepathpart = this.filepath.Split(new char[]
			{
				'\\'
			});
			this.label1.Text = "Licencja " + filepathpart[filepathpart.Length - 1].Replace(".pem", "");
			this.codeBarLabel.Visible = false;
		}
		public Commit(string filepath, Start start)
		{
			this.InitializeComponent();
			this.filepath = filepath;
			this.form = start;
			this.loggin = false;
			string[] filepathpart = filepath.Split(new char[]
			{
				'\\'
			});
			this.label1.Text = filepathpart[filepathpart.Length - 1].Replace(".pem", "");
			this.form.logged = false;
			this.codeBarLabel.Visible = false;
		}
		public Commit(string filepath, ProtocolForm pf)
		{
			this.InitializeComponent();
			this.filepath = filepath;
			this.form2 = pf;
			this.LoginNext.Click -= new System.EventHandler(this.LoginNext_Click);
			this.LoginNext.Click += new System.EventHandler(this.LoginNext2_Click);
			this.loggin = false;
			string[] filepathpart = filepath.Split(new char[]
			{
				'\\'
			});
			this.label1.Text = filepathpart[filepathpart.Length - 1].Replace(".pem", "");
			this.form2.goodcertificate = false;
			this.codeBarLabel.Visible = false;
		}
		public Commit(string filepath, ProtocolForm pf, string xml)
		{
			this.InitializeComponent();
			this.filepath = filepath;
			this.form2 = pf;
			this.LoginNext.Click -= new System.EventHandler(this.LoginNext_Click);
			this.LoginNext.Click += new System.EventHandler(this.LoginNext2_Click);
			this.loggin = false;
			string[] filepathpart = filepath.Split(new char[]
			{
				'\\'
			});
			this.label1.Text = filepathpart[filepathpart.Length - 1].Replace(".pem", "");
			this.form2.goodcertificate = false;
			string docXml = "";
			XmlDocument save = new XmlDocument();
			save.LoadXml(xml);
			XmlNode header = save.SelectSingleNode("/save/header");
			if (header != null)
			{
				docXml += header.OuterXml;
			}
			XmlNode step = save.SelectSingleNode("/save/step");
			if (step != null)
			{
				docXml += step.OuterXml;
			}
			XmlNode form = save.SelectSingleNode("/save/form");
			if (form != null)
			{
				docXml += form.OuterXml;
			}
			XmlNode komisja_sklad = save.SelectSingleNode("/save/komisja_sklad");
			if (komisja_sklad != null)
			{
				docXml += komisja_sklad.OuterXml;
			}
			XmlNode hardWarningCode = save.SelectSingleNode("/save/hardWarningCode");
			if (hardWarningCode != null)
			{
				docXml += hardWarningCode.OuterXml;
			}
			XmlNode softError = save.SelectSingleNode("/save/softError");
			if (softError != null)
			{
				docXml += softError.OuterXml;
			}
			XmlNode hardError = save.SelectSingleNode("/save/hardError");
			if (hardError != null)
			{
				docXml += hardError.OuterXml;
			}
			XmlNode hardWarning = save.SelectSingleNode("/save/hardWarning");
			if (hardWarning != null)
			{
				docXml += hardWarning.OuterXml;
			}
			ClassMd5 i = new ClassMd5();
			string controlSum = i.CreateMD5Hash(docXml);
			codeBar code = new codeBar();
			code.generateCode(controlSum);
			this.codeBarLabel.Text = "Podpisywanie protokołu o kodzie kreskowym: " + '\n'.ToString() + code.getTextReadable();
			this.codeBarLabel.Visible = true;
		}
		public Commit(string filepath, SendProtocol pf)
		{
			this.InitializeComponent();
			this.filepath = filepath;
			this.form3 = pf;
			this.loggin = false;
			this.LoginNext.Click -= new System.EventHandler(this.LoginNext_Click);
			this.LoginNext.Click -= new System.EventHandler(this.LoginNext2_Click);
			this.LoginNext.Click += new System.EventHandler(this.LoginNext3_Click);
			string[] filepathpart = filepath.Split(new char[]
			{
				'\\'
			});
			this.label1.Text = filepathpart[filepathpart.Length - 1].Replace(".pem", "");
			this.form3.goodcertificate = false;
			this.codeBarLabel.Visible = false;
		}
		public Commit(string filepath, Attendance pf)
		{
			this.InitializeComponent();
			this.filepath = filepath;
			this.form4 = pf;
			this.loggin = false;
			this.LoginNext.Click -= new System.EventHandler(this.LoginNext_Click);
			this.LoginNext.Click -= new System.EventHandler(this.LoginNext2_Click);
			this.LoginNext.Click -= new System.EventHandler(this.LoginNext3_Click);
			this.LoginNext.Click += new System.EventHandler(this.LoginNext4_Click);
			string[] filepathpart = filepath.Split(new char[]
			{
				'\\'
			});
			this.label1.Text = filepathpart[filepathpart.Length - 1].Replace(".pem", "");
			this.form4.logged = false;
			this.codeBarLabel.Visible = false;
		}
		private void LoginNext_Click(object sender, System.EventArgs e)
		{
			this.error.Visible = false;
			Certificate cert = new Certificate();
			this.form.logged = cert.readKey(this.filepath, this.password.Text);
			if (this.form.logged)
			{
				this.loggin = true;
				this.error.Visible = false;
				base.Close();
			}
			else
			{
				this.error.Visible = true;
			}
		}
		private void LoginNext2_Click(object sender, System.EventArgs e)
		{
			this.error.Visible = false;
			Certificate cert = new Certificate();
			this.form2.goodcertificate = cert.readKey(this.filepath, this.password.Text);
			if (this.form2.goodcertificate)
			{
				this.error.Visible = false;
				this.loggin = true;
				this.form2.password = this.password.Text;
				base.Close();
			}
			else
			{
				this.error.Visible = true;
				this.loggin = false;
			}
		}
		private void LoginNext3_Click(object sender, System.EventArgs e)
		{
			this.error.Visible = false;
			Certificate cert = new Certificate();
			this.form3.goodcertificate = cert.readKey(this.filepath, this.password.Text);
			if (this.form3.goodcertificate)
			{
				this.loggin = true;
				this.form3.password = this.password.Text;
				this.error.Visible = false;
				base.Close();
			}
			else
			{
				this.error.Visible = true;
			}
		}
		private void LoginNext4_Click(object sender, System.EventArgs e)
		{
			this.error.Visible = false;
			Certificate cert = new Certificate();
			this.form4.logged = cert.readKey(this.filepath, this.password.Text);
			if (this.form4.logged)
			{
				this.loggin = true;
				this.form4.password = this.password.Text;
				this.error.Visible = false;
				base.Close();
			}
			else
			{
				this.error.Visible = true;
			}
		}
		private void Commit_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!this.loggin)
			{
				if (this.form != null)
				{
					this.form.logged = false;
				}
				if (this.form2 != null)
				{
					this.form2.goodcertificate = false;
				}
				if (this.form3 != null)
				{
					this.form3.goodcertificate = false;
				}
				if (this.form4 != null)
				{
					this.form4.logged = false;
				}
			}
		}
		private void Commit_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				if (this.form != null)
				{
					this.form.logged = false;
					this.LoginNext_Click(sender, e);
				}
				if (this.form2 != null)
				{
					this.form2.goodcertificate = false;
					this.LoginNext2_Click(sender, e);
				}
				if (this.form3 != null)
				{
					this.form3.goodcertificate = false;
					this.LoginNext3_Click(sender, e);
				}
				if (this.form4 != null)
				{
					this.form4.logged = false;
					this.LoginNext4_Click(sender, e);
				}
			}
		}
		private void password_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				if (this.form != null)
				{
					this.form.logged = false;
					this.LoginNext_Click(sender, e);
				}
				if (this.form2 != null)
				{
					this.form2.goodcertificate = false;
					this.LoginNext2_Click(sender, e);
				}
				if (this.form3 != null)
				{
					this.form3.goodcertificate = false;
					this.LoginNext3_Click(sender, e);
				}
				if (this.form4 != null)
				{
					this.form4.logged = false;
					this.LoginNext4_Click(sender, e);
				}
			}
		}
		private void error_Click(object sender, System.EventArgs e)
		{
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		private void InitializeComponent()
		{
			this.panelNet = new Panel();
			this.code = new Label();
			this.label2 = new Label();
			this.label1 = new Label();
			this.error = new Label();
			this.LoginNext = new Button();
			this.password = new TextBox();
			this.label3 = new Label();
			this.codeBarLabel = new Label();
			this.panelNet.SuspendLayout();
			base.SuspendLayout();
			this.panelNet.Controls.Add(this.codeBarLabel);
			this.panelNet.Controls.Add(this.code);
			this.panelNet.Controls.Add(this.label2);
			this.panelNet.Controls.Add(this.label1);
			this.panelNet.Controls.Add(this.error);
			this.panelNet.Controls.Add(this.LoginNext);
			this.panelNet.Controls.Add(this.password);
			this.panelNet.Controls.Add(this.label3);
			this.panelNet.Location = new System.Drawing.Point(3, 6);
			this.panelNet.Name = "panelNet";
			this.panelNet.Size = new System.Drawing.Size(403, 197);
			this.panelNet.TabIndex = 3;
			this.code.AutoSize = true;
			this.code.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
			this.code.Location = new System.Drawing.Point(98, 98);
			this.code.MaximumSize = new System.Drawing.Size(370, 0);
			this.code.Name = "code";
			this.code.Size = new System.Drawing.Size(0, 15);
			this.code.TabIndex = 8;
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
			this.label2.Location = new System.Drawing.Point(160, 3);
			this.label2.MaximumSize = new System.Drawing.Size(370, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(61, 15);
			this.label2.TabIndex = 7;
			this.label2.Text = "Licencja";
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
			this.label1.Location = new System.Drawing.Point(16, 18);
			this.label1.MaximumSize = new System.Drawing.Size(370, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 15);
			this.label1.TabIndex = 6;
			this.label1.Text = "Hasło";
			this.error.AutoSize = true;
			this.error.ForeColor = System.Drawing.Color.Red;
			this.error.Location = new System.Drawing.Point(16, 174);
			this.error.Name = "error";
			this.error.RightToLeft = RightToLeft.No;
			this.error.Size = new System.Drawing.Size(108, 13);
			this.error.TabIndex = 5;
			this.error.Text = "Nieprawidłowe hasło";
			this.error.Visible = false;
			this.error.Click += new System.EventHandler(this.error_Click);
			this.LoginNext.Location = new System.Drawing.Point(16, 138);
			this.LoginNext.Name = "LoginNext";
			this.LoginNext.Size = new System.Drawing.Size(363, 33);
			this.LoginNext.TabIndex = 4;
			this.LoginNext.Text = "Zaloguj";
			this.LoginNext.UseVisualStyleBackColor = true;
			this.LoginNext.Click += new System.EventHandler(this.LoginNext_Click);
			this.password.Location = new System.Drawing.Point(101, 71);
			this.password.Name = "password";
			this.password.PasswordChar = '*';
			this.password.Size = new System.Drawing.Size(278, 20);
			this.password.TabIndex = 3;
			this.password.KeyDown += new KeyEventHandler(this.password_KeyDown);
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(16, 74);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(36, 13);
			this.label3.TabIndex = 1;
			this.label3.Text = "Hasło";
			this.codeBarLabel.AutoSize = true;
			this.codeBarLabel.Location = new System.Drawing.Point(16, 100);
			this.codeBarLabel.Name = "codeBarLabel";
			this.codeBarLabel.Size = new System.Drawing.Size(36, 13);
			this.codeBarLabel.TabIndex = 9;
			this.codeBarLabel.Text = "Hasło";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(411, 207);
			base.Controls.Add(this.panelNet);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "Commit";
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Podaj hasło do licencji";
			base.FormClosing += new FormClosingEventHandler(this.Commit_FormClosing);
			base.KeyDown += new KeyEventHandler(this.Commit_KeyDown);
			this.panelNet.ResumeLayout(false);
			this.panelNet.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
