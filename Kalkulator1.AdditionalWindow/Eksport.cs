using Kalkulator1.ResponseClass;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Web;
using System.Windows.Forms;
namespace Kalkulator1.AdditionalWindow
{
	public class Eksport : Form
	{
		private Connection con;
		private ProtocolForm p;
		private ProtocolsList p2;
		private SendProtocol p1;
		private Certificate c;
		private string pass;
		private string path;
		private string savePath;
		private string licensePath;
		private System.ComponentModel.IContainer components = null;
		private Panel passwordPanel;
		private Button log;
		private TextBox password;
		private Label label4;
		private Label label6;
		public Eksport()
		{
			this.InitializeComponent();
			this.con = new Connection();
			this.c = new Certificate();
			this.savePath = "";
			this.licensePath = "";
			this.pass = "";
		}
		public Eksport(string savePath, string licensePath)
		{
			this.InitializeComponent();
			this.con = new Connection();
			this.c = new Certificate();
			this.savePath = savePath;
			this.Text = "Eksport";
			this.licensePath = licensePath;
			this.pass = "";
			this.path = (this.path = System.IO.Path.GetTempPath() + "KBW");
		}
		public Eksport(string savePath, bool send, ProtocolForm p, string licensePath)
		{
			this.p = p;
			this.InitializeComponent();
			this.licensePath = licensePath;
			this.con = new Connection();
			this.c = new Certificate();
			this.savePath = savePath;
			if (send)
			{
				this.Text = "Wysyłanie protokołu na serwer";
				this.log.Click -= new System.EventHandler(this.log_Click);
				this.log.Click += new System.EventHandler(this.send_Click);
			}
			else
			{
				this.Text = "Eksport";
				this.log.Click += new System.EventHandler(this.log_Click);
				this.log.Click -= new System.EventHandler(this.send_Click);
			}
			this.pass = "";
			this.path = (this.path = System.IO.Path.GetTempPath() + "KBW");
		}
		public Eksport(string savePath, bool send, ProtocolForm p, string licensePath, string password)
		{
			this.p = p;
			this.InitializeComponent();
			this.licensePath = licensePath;
			this.con = new Connection();
			this.c = new Certificate();
			this.savePath = savePath;
			if (send)
			{
				this.Text = "Wysyłanie protokołu na serwer";
				this.log.Click -= new System.EventHandler(this.log_Click);
				this.log.Click += new System.EventHandler(this.send_Click);
			}
			else
			{
				this.Text = "Eksport";
				this.log.Click += new System.EventHandler(this.log_Click);
				this.log.Click -= new System.EventHandler(this.send_Click);
			}
			this.pass = password;
			this.path = (this.path = System.IO.Path.GetTempPath() + "KBW");
			this.send();
		}
		public Eksport(string xml, bool send, ProtocolsList p)
		{
			this.p2 = p;
			this.InitializeComponent();
			this.con = new Connection();
			this.c = new Certificate();
			this.savePath = xml;
			if (send)
			{
				this.Text = "Wysyłanie protokołu na serwer";
			}
			else
			{
				this.Text = "Eksport";
			}
			this.path = (this.path = System.IO.Path.GetTempPath() + "KBW");
			this.label4.Text = "Trwa wysyłanie protokołu";
			this.password.Visible = false;
			this.log.Visible = false;
			this.send2_Click();
			base.Close();
		}
		public Eksport(string savePath, bool send, SendProtocol p, string licensePath, string password)
		{
			this.p1 = p;
			this.InitializeComponent();
			this.licensePath = licensePath;
			this.con = new Connection();
			this.c = new Certificate();
			this.savePath = savePath;
			if (send)
			{
				this.Text = "Wysyłanie protokołu na serwer";
				this.log.Click -= new System.EventHandler(this.log_Click);
				this.log.Click += new System.EventHandler(this.send_Click);
			}
			else
			{
				this.Text = "Wysyłanie protokołu";
				this.log.Click += new System.EventHandler(this.log_Click);
				this.log.Click -= new System.EventHandler(this.send_Click);
			}
			this.pass = password;
			this.path = (this.path = System.IO.Path.GetTempPath() + "KBW");
			this.send();
		}
		private void log_Click(object sender, System.EventArgs e)
		{
			if (this.con.IsAvailableNetworkActive())
			{
				if (this.pass != "")
				{
					this.password.Text = this.pass;
				}
				bool logged = this.c.readKey(this.licensePath, this.password.Text);
				if (logged)
				{
					this.label6.Visible = false;
					try
					{
						this.c.SignXml(this.savePath, this.path + "\\tmp\\eksport.xml", this.password.Text, this.licensePath);
					}
					catch (System.Exception ex)
					{
						MessageBox.Show("Eksport: " + ex.Message, "Error");
					}
					string post2 = "flag=export";
					string[] name = this.savePath.Split(new char[]
					{
						'\\'
					});
					string[] param = name[name.Length - 1].Split(new char[]
					{
						'-'
					});
					string xml = "";
					try
					{
						try
						{
							System.IO.StreamReader sr = new System.IO.StreamReader(this.path + "\\tmp\\eksport.xml");
							xml = sr.ReadToEnd();
							sr.Close();
						}
						catch (System.Exception ex)
						{
							MessageBox.Show("Eksport: " + ex.Message, "Error");
						}
						post2 = post2 + "&xml=" + HttpUtility.UrlEncode(xml);
						string uri = "protocols/export";
						Code cod = this.con.postReq(uri, post2, 0);
						if (cod.getcode() == 1)
						{
							if (MessageBox.Show("Plik już istnieje na serwerze. Czy nadpisać zmiany?", "Eksport", MessageBoxButtons.YesNo) == DialogResult.Yes)
							{
								post2 += "&save=overwrite";
								Code cod2 = this.con.postReq(uri, post2, 0);
								MessageBox.Show(cod2.getText(), "Eksport");
							}
							base.Close();
						}
						else
						{
							MessageBox.Show(cod.getText(), "Eksport");
						}
					}
					catch (System.ArgumentNullException)
					{
						MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
					}
					catch (System.ArgumentException)
					{
						MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
					}
					catch (System.IO.FileNotFoundException)
					{
						MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
					}
					catch (System.IO.DirectoryNotFoundException)
					{
						MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
					}
					catch (System.IO.IOException)
					{
						MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
					}
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
		private void send_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (this.pass != "")
				{
					this.password.Text = this.pass;
				}
				bool logged = this.c.readKey(this.licensePath, this.password.Text);
				if (logged)
				{
					this.label6.Visible = false;
					string post2 = "flag=export";
					string[] name = this.savePath.Split(new char[]
					{
						'\\'
					});
					string[] param = name[name.Length - 1].Split(new char[]
					{
						'-'
					});
					string xml = "";
					try
					{
						try
						{
							System.IO.StreamReader sr = new System.IO.StreamReader(this.savePath);
							xml = sr.ReadToEnd();
							sr.Close();
						}
						catch (System.Exception ex)
						{
							MessageBox.Show("Wysyłanie protokołu: " + ex.Message, "Error");
						}
						post2 = post2 + "&xml=" + HttpUtility.UrlEncode(xml);
						string uri = "protocols/export";
						Code cod = this.con.postReq(uri, post2, 0);
						if (cod.getcode() == 0)
						{
							if (this.p != null)
							{
								this.p.komSend = "Protokół został wysłany na serwer";
								this.p.goodcertificate = true;
							}
							if (this.p1 != null)
							{
								this.p1.komSend = "Protokół został wysłany na serwer";
								this.p1.goodcertificate = true;
							}
							base.Close();
						}
						else
						{
							MessageBox.Show(cod.getText(), "Wysyłanie protokołu na serwer");
						}
					}
					catch (System.ArgumentNullException)
					{
						MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
					}
					catch (System.ArgumentException)
					{
						MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
					}
					catch (System.IO.FileNotFoundException)
					{
						MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
					}
					catch (System.IO.DirectoryNotFoundException)
					{
						MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
					}
					catch (System.IO.IOException)
					{
						MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
					}
				}
				else
				{
					this.label6.Visible = true;
				}
			}
			catch (System.Exception)
			{
			}
		}
		private void send()
		{
			try
			{
				if (this.pass != "")
				{
					this.password.Text = this.pass;
				}
				bool logged = this.c.readKey(this.licensePath, this.password.Text);
				if (logged)
				{
					this.label6.Visible = false;
					string post2 = "flag=export";
					string[] name = this.savePath.Split(new char[]
					{
						'\\'
					});
					string[] param = name[name.Length - 1].Split(new char[]
					{
						'-'
					});
					string xml = "";
					try
					{
						try
						{
							System.IO.StreamReader sr = new System.IO.StreamReader(this.savePath);
							xml = sr.ReadToEnd();
							sr.Close();
						}
						catch (System.Exception ex)
						{
							MessageBox.Show("Wysyłanie protokołu: " + ex.Message, "Error");
						}
						post2 = post2 + "&xml=" + HttpUtility.UrlEncode(xml);
						string uri = "protocols/export";
						Code cod = this.con.postReq(uri, post2, 0);
						if (cod.getcode() == 0)
						{
							if (this.p != null)
							{
								this.p.komSend = "Protokół został wysłany na serwer";
								this.p.goodcertificate = true;
							}
							if (this.p1 != null)
							{
								this.p1.komSend = "Protokół został wysłany na serwer";
								this.p1.goodcertificate = true;
							}
							base.Close();
						}
						if (cod.getcode() == 15)
						{
							if (this.p != null)
							{
								this.p.komSend = "Protokół już jest na serwerze";
								this.p.goodcertificate = true;
							}
							if (this.p1 != null)
							{
								this.p1.komSend = "Protokół już jest na serwerze";
								this.p1.goodcertificate = true;
							}
							MessageBox.Show(cod.getText(), "Wysyłanie protokołu na serwer");
							base.Close();
						}
						else
						{
							MessageBox.Show(cod.getText(), "Wysyłanie protokołu na serwer");
						}
					}
					catch (System.ArgumentNullException)
					{
						MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
					}
					catch (System.ArgumentException)
					{
						MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
					}
					catch (System.IO.FileNotFoundException)
					{
						MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
					}
					catch (System.IO.DirectoryNotFoundException)
					{
						MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
					}
					catch (System.IO.IOException)
					{
						MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
					}
				}
				else
				{
					this.label6.Visible = true;
				}
			}
			catch (System.Exception)
			{
			}
		}
		private void send2_Click()
		{
			try
			{
				this.label6.Visible = false;
				string post2 = "flag=export";
				string[] name = this.savePath.Split(new char[]
				{
					'\\'
				});
				string[] param = name[name.Length - 1].Split(new char[]
				{
					'-'
				});
				try
				{
					post2 = post2 + "&xml=" + HttpUtility.UrlEncode(this.savePath);
					string uri = "protocols/export";
					Code cod = this.con.postReq(uri, post2, 0);
					if (cod.getcode() == 0)
					{
						if (this.p != null)
						{
							this.p.komSend = "Protokół został wysłany na serwer";
							this.p.goodcertificate = true;
						}
						if (this.p1 != null)
						{
							this.p1.komSend = "Protokół został wysłany na serwer";
							this.p1.goodcertificate = true;
						}
						if (this.p2 != null)
						{
							this.p2.send = true;
						}
						base.Close();
					}
					else
					{
						MessageBox.Show(cod.getText(), "Wysyłanie protokołu na serwer");
					}
				}
				catch (System.ArgumentNullException)
				{
					MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
				}
				catch (System.ArgumentException)
				{
					MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
				}
				catch (System.IO.FileNotFoundException)
				{
					MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
				}
				catch (System.IO.DirectoryNotFoundException)
				{
					MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
				}
				catch (System.IO.IOException)
				{
					MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
				}
			}
			catch (System.Exception)
			{
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
			this.passwordPanel.Location = new System.Drawing.Point(6, 6);
			this.passwordPanel.Name = "passwordPanel";
			this.passwordPanel.Size = new System.Drawing.Size(403, 185);
			this.passwordPanel.TabIndex = 7;
			this.label6.AutoSize = true;
			this.label6.ForeColor = System.Drawing.Color.Red;
			this.label6.Location = new System.Drawing.Point(24, 167);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(111, 13);
			this.label6.TabIndex = 6;
			this.label6.Text = "Nieprawidłowe hasło.";
			this.label6.Visible = false;
			this.log.Location = new System.Drawing.Point(27, 124);
			this.log.Name = "log";
			this.log.Size = new System.Drawing.Size(334, 23);
			this.log.TabIndex = 4;
			this.log.Text = "Zaloguj";
			this.log.UseVisualStyleBackColor = true;
			this.log.Click += new System.EventHandler(this.log_Click);
			this.password.Location = new System.Drawing.Point(157, 57);
			this.password.Name = "password";
			this.password.PasswordChar = '*';
			this.password.Size = new System.Drawing.Size(204, 20);
			this.password.TabIndex = 3;
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			this.label4.Location = new System.Drawing.Point(25, 58);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(133, 15);
			this.label4.TabIndex = 0;
			this.label4.Text = "Podaj hasło do licencji:";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(415, 195);
			base.Controls.Add(this.passwordPanel);
			base.Name = "Eksport";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Eksport do sieci";
			this.passwordPanel.ResumeLayout(false);
			this.passwordPanel.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
