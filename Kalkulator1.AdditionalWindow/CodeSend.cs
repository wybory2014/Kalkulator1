using Kalkulator1.AdditionalClass;
using Kalkulator1.ResponseClass;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Web;
using System.Windows.Forms;
namespace Kalkulator1.AdditionalWindow
{
	public class CodeSend : Form
	{
		private string postWarning;
		private string postWarning2;
		private string licensePath;
		private string xml;
		private EnteringCodes f;
		private Connection con;
		private System.ComponentModel.IContainer components = null;
		private Panel passwordPanel;
		private Label label6;
		private Button log;
		private TextBox password;
		private Label label4;
		public CodeSend(string post, string xml, string post2, string licensePath, EnteringCodes f)
		{
			this.InitializeComponent();
			this.postWarning = post;
			this.postWarning2 = post2;
			this.con = new Connection();
			this.licensePath = licensePath;
			this.f = f;
			this.xml = xml;
		}
		private void log_Click(object sender, System.EventArgs e)
		{
			if (this.con.IsAvailableNetworkActive())
			{
				Certificate cert = new Certificate();
				bool logged = cert.readKey(this.licensePath, this.password.Text);
				if (logged)
				{
					WaitPanel panelWait = new WaitPanel("Wait_04", base.Size.Width, base.Size.Height);
					panelWait.setWaitPanel("Trwa wysyłanie prośby o przyznanie kodu odblokowującego przez pełnomocnika.", "Proszę czekać");
					panelWait.setSize(this.passwordPanel.Size);
					panelWait.setLocation(this.passwordPanel.Location);
					base.Controls.Add(panelWait.getPanel());
					base.Controls[panelWait.getName()].BringToFront();
					panelWait.setVisible(true);
					this.label6.Visible = false;
					try
					{
						cert.SignXmlText(this.xml, System.IO.Path.GetTempPath() + "KBW\\tmp\\eksportCode.xml", this.password.Text, this.licensePath);
						System.IO.StreamReader sr = new System.IO.StreamReader(System.IO.Path.GetTempPath() + "KBW\\tmp\\eksportCode.xml");
						this.xml = sr.ReadToEnd();
						sr.Close();
					}
					catch (System.Exception ex)
					{
						MessageBox.Show("Wprowadzanie kodu: " + ex.Message, "Error");
					}
					string post2 = this.postWarning + this.postWarning2 + "&flag=export&flag2=akcept&xml=" + HttpUtility.UrlEncode(this.xml);
					string uri = "protocols/export";
					ResponseData cod = this.con.postSendCode(uri, post2, 0);
					if (cod.getCode().getcode() == 0)
					{
						WaitPanel p = new WaitPanel("Wait_04", base.Size.Width, base.Size.Height);
						p.setWaitPanel("Trwa sprawdzanie czy ostrzeżenie/a twarde zostały zaakceptowane - oczekiwanie na przyznanie kodu odblokowującego przez pełnomocnika.", "Proszę czekać");
						p.setSize(this.passwordPanel.Size);
						p.setLocation(this.passwordPanel.Location);
						base.Controls.Add(p.getPanel());
						base.Controls[p.getName()].BringToFront();
						p.setVisible(true);
						bool response = false;
						int count = 0;
						while (!response)
						{
							count++;
							string post3 = "flag=export&flag2=check&" + this.postWarning2 + "&xml=" + HttpUtility.UrlEncode(this.xml);
							ResponseData cod2 = this.con.postSendCode(uri, post3, 0);
							if (cod2.getCode().getcode() == 10)
							{
								this.f.codeField.Text = cod2.getXml();
								response = true;
								base.Close();
							}
							else
							{
								if (cod2.getCode().getcode() == 9)
								{
									p.setWaitPanel("Protokół z ostrzeżeniami twardymi został odrzucony", "Proszę czekać");
									response = true;
								}
								else
								{
									if (cod2.getCode().getcode() != 11)
									{
										if (MessageBox.Show(cod2.getCode().getText() + " Spróbować jeszcze raz?", "Oczekiwanie na odpowiedź", MessageBoxButtons.YesNo) == DialogResult.No)
										{
											response = true;
											base.Close();
										}
									}
								}
							}
							if (count % 11 == 0)
							{
								if (MessageBox.Show("Protokół ciągle oczekuje na akceptacje. Kontynuować sprawdzanie?", "Oczekiwanie na odpowiedź", MessageBoxButtons.YesNo) == DialogResult.No)
								{
									response = true;
									base.Close();
								}
							}
						}
						base.Close();
					}
					else
					{
						MessageBox.Show(cod.getCode().getText(), "Komuniakt");
						base.Close();
					}
					panelWait.setVisible(true);
				}
				else
				{
					this.label6.Visible = true;
				}
			}
			else
			{
				MessageBox.Show("Nie masz połaczenia z internetem! Podłącz internet i spróbuj jeszcze raz.", "Uwaga");
				base.Close();
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
			this.passwordPanel = new Panel();
			this.label6 = new Label();
			this.log = new Button();
			this.password = new TextBox();
			this.label4 = new Label();
			this.passwordPanel.SuspendLayout();
			base.SuspendLayout();
			this.passwordPanel.Controls.Add(this.label6);
			this.passwordPanel.Controls.Add(this.log);
			this.passwordPanel.Controls.Add(this.password);
			this.passwordPanel.Controls.Add(this.label4);
			this.passwordPanel.Location = new System.Drawing.Point(8, 8);
			this.passwordPanel.Margin = new Padding(6, 6, 6, 6);
			this.passwordPanel.Name = "passwordPanel";
			this.passwordPanel.Size = new System.Drawing.Size(806, 356);
			this.passwordPanel.TabIndex = 8;
			this.label6.AutoSize = true;
			this.label6.ForeColor = System.Drawing.Color.Red;
			this.label6.Location = new System.Drawing.Point(48, 321);
			this.label6.Margin = new Padding(6, 0, 6, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(258, 26);
			this.label6.TabIndex = 6;
			this.label6.Text = "Hasło jest nieprawidłowe.";
			this.label6.Visible = false;
			this.log.Location = new System.Drawing.Point(54, 242);
			this.log.Margin = new Padding(6, 6, 6, 6);
			this.log.Name = "log";
			this.log.Size = new System.Drawing.Size(668, 44);
			this.log.TabIndex = 4;
			this.log.Text = "Zaloguj";
			this.log.UseVisualStyleBackColor = true;
			this.log.Click += new System.EventHandler(this.log_Click);
			this.password.Location = new System.Drawing.Point(146, 110);
			this.password.Margin = new Padding(6, 6, 6, 6);
			this.password.Name = "password";
			this.password.PasswordChar = '*';
			this.password.Size = new System.Drawing.Size(572, 31);
			this.password.TabIndex = 3;
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			this.label4.Location = new System.Drawing.Point(50, 113);
			this.label4.Margin = new Padding(6, 0, 6, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(84, 29);
			this.label4.TabIndex = 0;
			this.label4.Text = "Hasło:";
			base.AutoScaleDimensions = new System.Drawing.SizeF(12f, 25f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(820, 371);
			base.Controls.Add(this.passwordPanel);
			base.Margin = new Padding(6, 6, 6, 6);
			base.Name = "CodeSend";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Logowanie";
			this.passwordPanel.ResumeLayout(false);
			this.passwordPanel.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
