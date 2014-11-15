using Kalkulator1.AdditionalClass;
using Kalkulator1.ResponseClass;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;
namespace Kalkulator1
{
	public class Login : Form
	{
		private string initialDirectory;
		private string user;
		private string ou;
		private string token;
		private string path;
		private int step;
		private Connection con;
		private System.ComponentModel.IContainer components = null;
		private Panel panelNet;
		private Button LoginNext;
		private TextBox password;
		private TextBox username;
		private Label label3;
		private Label label2;
		private Panel getLicensePanel;
		private Button sendPetition;
		private Label loggedText;
		private Label label1;
		private Panel passwordPanel;
		private Button step2;
		private TextBox passwordCert2;
		private TextBox passwordCert1;
		private Label label5;
		private Label label4;
		private Label label6;
		public Login()
		{
			this.path = System.IO.Path.GetTempPath() + "KBW";
			this.InitializeComponent();
			this.initialDirectory = this.path + "\\Licenses";
			this.getLicensePanel.Visible = false;
			this.panelNet.Visible = true;
			this.passwordPanel.Visible = false;
			this.user = "";
			this.ou = "";
			this.token = "";
			this.con = new Connection();
			this.step = 0;
			this.LoginNext.KeyDown += new KeyEventHandler(this.Login_KeyDown);
			this.sendPetition.KeyDown += new KeyEventHandler(this.Login_KeyDown);
		}
		private void LoginNext_Click(object sender, System.EventArgs e)
		{
			if (this.con.IsAvailableNetworkActive())
			{
				WaitPanel p = new WaitPanel("Wait_04", base.Size.Width, base.Size.Height);
				p.setWaitPanel("Trwa autoryzacja użytkownika", "Proszę czekać");
				p.setSize(this.passwordPanel.Size);
				p.setLocation(this.passwordPanel.Location);
				base.Controls.Add(p.getPanel());
				base.Controls[p.getName()].BringToFront();
				p.setVisible(true);
				string postData = "flag=kalkulator&login=" + HttpUtility.UrlEncode(this.username.Text) + "&password=" + HttpUtility.UrlEncode(this.password.Text);
				Logged logData = this.con.postLog(postData, 0);
				p.setVisible(false);
				if (logData.getToken() != "")
				{
					this.user = logData.getUser();
					this.ou = string.Concat(new string[]
					{
						logData.getElections(),
						"-",
						logData.getJns(),
						"-",
						logData.getRola()
					});
					if (logData.getRola() == "P" || logData.getRola() == "Z")
					{
						this.ou = this.ou + "-" + logData.getObwodu();
					}
					this.token = logData.getToken();
					this.step1();
				}
				else
				{
					MessageBox.Show("Podano błędny login lub hasło", "Uwaga");
					base.Close();
				}
			}
			else
			{
				MessageBox.Show("Nie masz połaczenia z internetem!", "Uwaga");
				base.Close();
			}
		}
		private void step1()
		{
			this.step = 1;
			this.getLicensePanel.Visible = true;
			this.panelNet.Visible = false;
			this.getLicensePanel.Location = this.panelNet.Location;
			this.loggedText.Text = "Zalogowałeś się jako " + this.user;
		}
		private void step2_Click(object sender, System.EventArgs e)
		{
			this.step = 2;
			this.getLicensePanel.Visible = false;
			this.panelNet.Visible = false;
			this.passwordPanel.Visible = true;
			this.Text = "Ustal hasło do licencji";
		}
		private void sendPetition_Click(object sender, System.EventArgs e)
		{
			WaitPanel p = new WaitPanel("Wait_04", base.Size.Width, base.Size.Height);
			p.setWaitPanel("Trwa generowanie wniosku", "Proszę czekać");
			p.setSize(this.passwordPanel.Size);
			p.setLocation(this.passwordPanel.Location);
			base.Controls.Add(p.getPanel());
			base.Controls[p.getName()].BringToFront();
			p.setVisible(true);
			string uri = "certificates/csrwrite";
			Certificate certificate = new Certificate();
			certificate.generateCSR(this.ou, this.user, this.passwordCert1.Text);
			string postData = "csr=" + HttpUtility.UrlEncode(certificate.getCSR()) + "&token=" + HttpUtility.UrlEncode(this.token);
			p.setWaitPanel("Trwa wysyłanie wniosku", "Proszę czekać");
			Code res = this.con.postReq(uri, postData, 0);
			if (res.getcode() == 0)
			{
				MessageBox.Show("Wniosek o licencje został wysłany", "Uwaga");
			}
			else
			{
				MessageBox.Show(res.getText(), "Uwaga");
			}
			p.setVisible(false);
			base.Close();
		}
		private void step3_Click(object sender, System.EventArgs e)
		{
			bool spr = System.Text.RegularExpressions.Regex.IsMatch(this.passwordCert1.Text, "[!@#$%^&*()\\\\\\-_+=<>?]+");
			if (this.passwordCert1.Text.Length >= 8 && this.passwordCert1.Text.Length <= 40)
			{
				if (System.Text.RegularExpressions.Regex.IsMatch(this.passwordCert1.Text, "[A-ZĄĆĘŁŃÓŚŹŻ]+") && System.Text.RegularExpressions.Regex.IsMatch(this.passwordCert1.Text, "[a-ząćęłńóśźż]+") && System.Text.RegularExpressions.Regex.IsMatch(this.passwordCert1.Text, "[0-9]+") && System.Text.RegularExpressions.Regex.IsMatch(this.passwordCert1.Text, "[!@#$%^&*()\\\\\\-_+=<>?]+"))
				{
					if (!(this.passwordCert1.Text == this.passwordCert2.Text))
					{
						this.label6.Visible = true;
					}
					else
					{
						this.label6.Text = "Hasła nie są takie same.";
						this.label6.Visible = false;
						this.sendPetition_Click(sender, e);
					}
				}
				else
				{
					this.label6.Text = "Hasło powinno się zkładać z minimum 8 znaków, a maksymalnie z 40. Powinno również zawierać jedną wielką literę, jedną mała, cyfrę oraz znak specjalny (! @ # $ % ^ & * ( ) \\ - _ + = < > ? )";
					this.label6.Visible = true;
				}
			}
			else
			{
				this.label6.Text = "Hasło powinno się zkładać z minimum 8 znaków, a maksymalnie z 40. Powinno również zawierać jedną wielką literę, jedną mała, cyfrę oraz znak specjalny (! @ # $ % ^ & * ( ) \\ - _ + = < > ? )";
				this.label6.Visible = true;
			}
		}
		private void Login_KeyDown(object sender, KeyEventArgs e)
		{
			if (this.step == 0)
			{
				this.LoginNext_Click(sender, e);
			}
			if (this.step == 1)
			{
				this.step2_Click(sender, e);
			}
			if (this.step == 2)
			{
				this.step3_Click(sender, e);
			}
		}
		private void passwordCert1_KeyDown(object sender, KeyEventArgs e)
		{
			Control ctl = (Control)sender;
			base.ActiveControl = ctl;
			if (e.KeyCode == Keys.Return)
			{
				ctl.SelectNextControl(base.ActiveControl, true, true, true, true);
			}
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
			this.LoginNext = new Button();
			this.password = new TextBox();
			this.username = new TextBox();
			this.label3 = new Label();
			this.label2 = new Label();
			this.getLicensePanel = new Panel();
			this.label1 = new Label();
			this.sendPetition = new Button();
			this.loggedText = new Label();
			this.passwordPanel = new Panel();
			this.label6 = new Label();
			this.step2 = new Button();
			this.passwordCert2 = new TextBox();
			this.passwordCert1 = new TextBox();
			this.label5 = new Label();
			this.label4 = new Label();
			this.panelNet.SuspendLayout();
			this.getLicensePanel.SuspendLayout();
			this.passwordPanel.SuspendLayout();
			base.SuspendLayout();
			this.panelNet.Controls.Add(this.LoginNext);
			this.panelNet.Controls.Add(this.password);
			this.panelNet.Controls.Add(this.username);
			this.panelNet.Controls.Add(this.label3);
			this.panelNet.Controls.Add(this.label2);
			this.panelNet.Location = new System.Drawing.Point(1, 4);
			this.panelNet.Name = "panelNet";
			this.panelNet.Size = new System.Drawing.Size(403, 185);
			this.panelNet.TabIndex = 2;
			this.LoginNext.Location = new System.Drawing.Point(16, 126);
			this.LoginNext.Name = "LoginNext";
			this.LoginNext.Size = new System.Drawing.Size(363, 33);
			this.LoginNext.TabIndex = 4;
			this.LoginNext.Text = "Zaloguj";
			this.LoginNext.UseVisualStyleBackColor = true;
			this.LoginNext.Click += new System.EventHandler(this.LoginNext_Click);
			this.password.Location = new System.Drawing.Point(101, 82);
			this.password.Name = "password";
			this.password.PasswordChar = '*';
			this.password.Size = new System.Drawing.Size(278, 20);
			this.password.TabIndex = 3;
			this.username.Location = new System.Drawing.Point(101, 32);
			this.username.Name = "username";
			this.username.Size = new System.Drawing.Size(278, 20);
			this.username.TabIndex = 2;
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(13, 85);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(36, 13);
			this.label3.TabIndex = 1;
			this.label3.Text = "Hasło";
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 35);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(33, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Login";
			this.getLicensePanel.Controls.Add(this.label1);
			this.getLicensePanel.Controls.Add(this.sendPetition);
			this.getLicensePanel.Controls.Add(this.loggedText);
			this.getLicensePanel.Location = new System.Drawing.Point(1, 4);
			this.getLicensePanel.Name = "getLicensePanel";
			this.getLicensePanel.Size = new System.Drawing.Size(403, 185);
			this.getLicensePanel.TabIndex = 5;
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			this.label1.Location = new System.Drawing.Point(41, 107);
			this.label1.MaximumSize = new System.Drawing.Size(320, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(320, 60);
			this.label1.TabIndex = 5;
			this.label1.Text = "Licencja będzie dostępna w serwisie zaraz po utworzeniu jej przez pracownika KBW. Licencja będzie dostępna na stronie głównej kalkulatora. Pobraną licencję należy zainstalować.";
			this.sendPetition.Location = new System.Drawing.Point(58, 50);
			this.sendPetition.Name = "sendPetition";
			this.sendPetition.Size = new System.Drawing.Size(271, 33);
			this.sendPetition.TabIndex = 4;
			this.sendPetition.Text = "wyślij wniosek o licencję";
			this.sendPetition.UseVisualStyleBackColor = true;
			this.sendPetition.Click += new System.EventHandler(this.step2_Click);
			this.loggedText.AutoSize = true;
			this.loggedText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 238);
			this.loggedText.Location = new System.Drawing.Point(55, 21);
			this.loggedText.Name = "loggedText";
			this.loggedText.Size = new System.Drawing.Size(274, 17);
			this.loggedText.TabIndex = 0;
			this.loggedText.Text = "Zalogowałeś się jako Imię i Nazwisko";
			this.passwordPanel.AutoSize = true;
			this.passwordPanel.Controls.Add(this.label6);
			this.passwordPanel.Controls.Add(this.step2);
			this.passwordPanel.Controls.Add(this.passwordCert2);
			this.passwordPanel.Controls.Add(this.passwordCert1);
			this.passwordPanel.Controls.Add(this.label5);
			this.passwordPanel.Controls.Add(this.label4);
			this.passwordPanel.Location = new System.Drawing.Point(0, 5);
			this.passwordPanel.Name = "passwordPanel";
			this.passwordPanel.Size = new System.Drawing.Size(403, 185);
			this.passwordPanel.TabIndex = 6;
			this.label6.AutoSize = true;
			this.label6.ForeColor = System.Drawing.Color.Red;
			this.label6.Location = new System.Drawing.Point(24, 163);
			this.label6.MaximumSize = new System.Drawing.Size(334, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(124, 13);
			this.label6.TabIndex = 5;
			this.label6.Text = "Hasła nie są takie same.";
			this.label6.Visible = false;
			this.step2.Location = new System.Drawing.Point(27, 126);
			this.step2.Name = "step2";
			this.step2.Size = new System.Drawing.Size(334, 23);
			this.step2.TabIndex = 4;
			this.step2.Text = "Dalej";
			this.step2.UseVisualStyleBackColor = true;
			this.step2.Click += new System.EventHandler(this.step3_Click);
			this.passwordCert2.Location = new System.Drawing.Point(118, 77);
			this.passwordCert2.Name = "passwordCert2";
			this.passwordCert2.PasswordChar = '*';
			this.passwordCert2.Size = new System.Drawing.Size(243, 20);
			this.passwordCert2.TabIndex = 3;
			this.passwordCert2.KeyDown += new KeyEventHandler(this.passwordCert1_KeyDown);
			this.passwordCert1.Location = new System.Drawing.Point(118, 27);
			this.passwordCert1.Name = "passwordCert1";
			this.passwordCert1.PasswordChar = '*';
			this.passwordCert1.Size = new System.Drawing.Size(243, 20);
			this.passwordCert1.TabIndex = 2;
			this.passwordCert1.KeyDown += new KeyEventHandler(this.passwordCert1_KeyDown);
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			this.label5.Location = new System.Drawing.Point(24, 82);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(87, 15);
			this.label5.TabIndex = 1;
			this.label5.Text = "Powtórz hasło:";
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			this.label4.Location = new System.Drawing.Point(69, 32);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(42, 15);
			this.label4.TabIndex = 0;
			this.label4.Text = "Hasło:";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.AutoSize = true;
			base.ClientSize = new System.Drawing.Size(404, 194);
			base.Controls.Add(this.passwordPanel);
			base.Controls.Add(this.getLicensePanel);
			base.Controls.Add(this.panelNet);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "Login";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Wnioskowanie o licencje";
			base.KeyDown += new KeyEventHandler(this.passwordCert1_KeyDown);
			this.panelNet.ResumeLayout(false);
			this.panelNet.PerformLayout();
			this.getLicensePanel.ResumeLayout(false);
			this.getLicensePanel.PerformLayout();
			this.passwordPanel.ResumeLayout(false);
			this.passwordPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
