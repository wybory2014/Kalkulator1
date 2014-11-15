using Kalkulator1.AdditionalClass;
using Kalkulator1.ResponseClass;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Web;
using System.Windows.Forms;
using System.Xml;
namespace Kalkulator1.AdditionalWindow
{
	public class Import : Form
	{
		private Connection con;
		private Certificate c;
		private string name;
		private ProtocolForm form2;
		private string licensePath;
		private string okregId;
		private string instId;
		private string obwod;
		private string inst;
		private string okreg;
		private System.ComponentModel.IContainer components = null;
		private Panel passwordPanel;
		private Label label6;
		private Button log;
		private TextBox password;
		private Label label4;
		public Import()
		{
			this.InitializeComponent();
			this.con = new Connection();
			this.name = "";
			this.form2 = new ProtocolForm();
			this.licensePath = "";
			this.c = new Certificate();
		}
		public Import(string fileName, string licensePath, ProtocolForm pf, XmlDocument header)
		{
			this.InitializeComponent();
			this.con = new Connection();
			this.name = fileName;
			this.form2 = pf;
			this.licensePath = licensePath;
			this.c = new Certificate();
			string[] filenameTab = this.name.Split(new char[]
			{
				'\\'
			});
			string filename = filenameTab[filenameTab.Length - 1];
			string post = "flag=import&filename=" + HttpUtility.UrlEncode(filename);
			string[] item = filename.Split(new char[]
			{
				'-'
			});
			post = post + "&akcja=" + HttpUtility.UrlEncode(item[0].Replace('_', '/'));
			post = post + "&jsn=" + item[1];
			this.obwod = item[2];
			this.inst = item[3];
			string[] okr = item[4].Replace(".xml", "").Split(new char[]
			{
				' '
			});
			this.okreg = okr[0];
			XmlNode headerRoot = header.SelectSingleNode("/akcja_wyborcza/jns");
			foreach (XmlNode xObwod in headerRoot)
			{
				if (xObwod.Attributes["nr"].InnerText == this.obwod)
				{
					foreach (XmlNode xInst in xObwod)
					{
						if (xInst.Attributes["kod"].InnerText == this.inst)
						{
							this.instId = xInst.Attributes["id"].InnerText;
							foreach (XmlNode xOkreg in xInst)
							{
								if (xOkreg.Attributes["nr"].InnerText == this.okreg)
								{
									this.okregId = xOkreg.Attributes["id"].InnerText;
								}
							}
						}
					}
				}
			}
		}
		private void log_Click(object sender, System.EventArgs e)
		{
			if (this.con.IsAvailableNetworkActive())
			{
				bool logged = this.c.readKey(this.licensePath, this.password.Text);
				if (logged)
				{
					WaitPanel p = new WaitPanel("Wait_04", base.Size.Width, base.Size.Height);
					p.setWaitPanel("Trwa importowanie danych", "Proszę czekać");
					p.setSize(this.passwordPanel.Size);
					p.setLocation(this.passwordPanel.Location);
					base.Controls.Add(p.getPanel());
					base.Controls[p.getName()].BringToFront();
					p.setVisible(true);
					string[] filenameTab = this.name.Split(new char[]
					{
						'\\'
					});
					string filename = filenameTab[filenameTab.Length - 1];
					string uri = "protocols/import";
					string post = "flag=import&filename=" + HttpUtility.UrlEncode(filename);
					string[] item = filename.Split(new char[]
					{
						'-'
					});
					post = post + "&akcja=" + HttpUtility.UrlEncode(item[0].Replace('_', '/'));
					post = post + "&jsn=" + item[1];
					post = post + "&obw=" + item[2];
					post = post + "&inst=" + item[3];
					string[] okr = item[4].Replace(".xml", "").Split(new char[]
					{
						' '
					});
					post = post + "&okr=" + okr[0];
					string xml = string.Concat(new string[]
					{
						"<import><header><jns_kod>",
						item[1],
						"</jns_kod><nrObwodu>",
						item[2],
						"</nrObwodu>"
					});
					xml = xml + "<id_intytucji>" + this.instId + "</id_intytucji>";
					xml = xml + "<id_okregu>" + this.okregId + "</id_okregu></header></import>";
					try
					{
						Certificate cer = new Certificate();
						cer.SignXmlText(xml, System.IO.Path.GetTempPath() + "KBW\\tmp\\import.xml", this.password.Text, this.licensePath);
						System.IO.StreamReader sr = new System.IO.StreamReader(System.IO.Path.GetTempPath() + "KBW\\tmp\\import.xml");
						xml = sr.ReadToEnd();
						sr.Close();
					}
					catch (System.Exception ex)
					{
						MessageBox.Show("Import protokołu: " + ex.Message, "Error");
					}
					post = post + "&xmlImport=" + HttpUtility.UrlEncode(xml);
					ResponseData res = this.con.postImport(uri, post, 0);
					if (res.getCode().getcode() == 0)
					{
						System.IO.StreamWriter sw = new System.IO.StreamWriter(this.name, false);
						sw.Write(res.getXml());
						sw.Close();
						this.form2.imported = true;
						base.Close();
					}
					else
					{
						MessageBox.Show(res.getCode().getText(), "Import");
						this.form2.imported = false;
					}
					p.setVisible(true);
				}
				else
				{
					this.label6.Visible = true;
					this.form2.imported = false;
				}
			}
			else
			{
				MessageBox.Show("Nie masz połaczenia z internetem!", "Uwaga");
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
			this.passwordPanel.Location = new System.Drawing.Point(4, 4);
			this.passwordPanel.Name = "passwordPanel";
			this.passwordPanel.Size = new System.Drawing.Size(403, 185);
			this.passwordPanel.TabIndex = 8;
			this.label6.AutoSize = true;
			this.label6.ForeColor = System.Drawing.Color.Red;
			this.label6.Location = new System.Drawing.Point(24, 167);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(111, 13);
			this.label6.TabIndex = 6;
			this.label6.Text = "Nieprawidłowe hasło.";
			this.label6.Visible = false;
			this.log.Location = new System.Drawing.Point(27, 126);
			this.log.Name = "log";
			this.log.Size = new System.Drawing.Size(334, 23);
			this.log.TabIndex = 4;
			this.log.Text = "Zaloguj";
			this.log.UseVisualStyleBackColor = true;
			this.log.Click += new System.EventHandler(this.log_Click);
			this.password.Location = new System.Drawing.Point(164, 59);
			this.password.Name = "password";
			this.password.PasswordChar = '*';
			this.password.Size = new System.Drawing.Size(197, 20);
			this.password.TabIndex = 3;
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			this.label4.Location = new System.Drawing.Point(25, 60);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(133, 15);
			this.label4.TabIndex = 0;
			this.label4.Text = "Podaj hasło do licencji:";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(409, 192);
			base.Controls.Add(this.passwordPanel);
			base.Name = "Import";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Import z sieci";
			this.passwordPanel.ResumeLayout(false);
			this.passwordPanel.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
