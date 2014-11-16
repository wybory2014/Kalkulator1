using Kalkulator1.AdditionalWindow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Web;
using System.Windows.Forms;
using System.Xml;
namespace Kalkulator1
{
	public class EnteringCodes : Form
	{
		private System.Collections.Generic.List<string> posts;
		private string protocol;
		private string savePath;
		private string OU;
		private string licensePath;
		private ProtocolForm f;
		private System.ComponentModel.IContainer components = null;
		private Label label1;
		private Label label2;
		private Panel codesPanel;
		private Panel webPanel;
		private Label label4;
		private Label label3;
		private Button send;
		private ToolTip toolTip1;
		private Button akcept;
		private Label label5;
		public TextBox codeField;
		private Button show;
		private Label errorsList;
		public EnteringCodes()
		{
			this.InitializeComponent();
			this.posts = new System.Collections.Generic.List<string>();
			this.protocol = "";
			this.OU = "";
			this.show.Visible = false;
		}
		public EnteringCodes(string protocolName, string savePath, string OU, string licensePath, ProtocolForm form)
		{
			this.InitializeComponent();
			this.posts = new System.Collections.Generic.List<string>();
			this.savePath = savePath;
			this.protocol = protocolName;
			this.OU = OU;
			this.f = form;
			this.licensePath = licensePath;
			this.errorsList.Visible = false;
		}
		private string generetedCode(string errors, string controlSum)
		{
			ClassMd5 i = new ClassMd5();
			return i.CreateMD5Hash2(errors + this.OU + controlSum);
		}
		private bool check(string code, string errors, string controlSum)
		{
			string genereted = this.generetedCode(errors, controlSum);
			return genereted == code;
		}
		private void send_Click(object sender, System.EventArgs e)
		{
			XmlDocument save = new XmlDocument();
			save.Load(this.savePath);
			try
			{
				XmlNode header = save.SelectSingleNode("/save/header");
				XmlNode step = save.SelectSingleNode("/save/step1");
				XmlNode step2 = save.SelectSingleNode("/save/step2");
				XmlNode step3 = save.SelectSingleNode("/save/step3");
				XmlNode hardWarning = save.SelectSingleNode("/save/hardWarning");
				string xml = save.InnerXml;
				string controlSum = "";
				if (header != null)
				{
					controlSum += header.OuterXml;
				}
				if (step != null)
				{
					controlSum += step.OuterXml;
				}
				if (step2 != null)
				{
					controlSum += step2.OuterXml;
				}
				if (step3 != null)
				{
					controlSum += step3.OuterXml;
				}
				ClassMd5 i = new ClassMd5();
				string hash = i.CreateMD5Hash2(controlSum);
				string errors = "";
				if (hardWarning != null)
				{
					errors = hardWarning.OuterXml;
				}
				string post = "controlSum=" + HttpUtility.UrlEncode(hash) + "&errors=" + HttpUtility.UrlEncode(errors);
				string[] name = this.savePath.Split(new char[]
				{
					'\\'
				});
				string[] param = name[name.Length - 1].Split(new char[]
				{
					'-'
				});
				string post2 = "&fileName=" + HttpUtility.UrlEncode(name[name.Length - 1]) + "&jns=" + HttpUtility.UrlEncode(param[1]);
				CodeSend ws = new CodeSend(post, xml, post2, this.licensePath, this);
				ws.ShowDialog();
				bool corect = this.check(this.codeField.Text, errors, hash);
				if (corect)
				{
					string xml2 = save.InnerXml;
					string code = "<hardWarningCode>" + this.codeField.Text + "</hardWarningCode>";
					xml2.Replace("</save>", code + "</save>");
					System.IO.StreamWriter sw = new System.IO.StreamWriter(this.savePath, false);
					sw.Write(xml2);
					sw.Close();
					this.f.goodcertificate = true;
					base.Close();
				}
			}
			catch (XmlException)
			{
			}
			catch (System.NullReferenceException)
			{
			}
		}
		private void akcept_Click(object sender, System.EventArgs e)
		{
			XmlDocument save = new XmlDocument();
			save.Load(this.savePath);
			string pass2 = this.checkPhone();
			if (pass2 == this.codeField.Text)
			{
				string xml2 = save.InnerXml;
				string code = "<hardWarningCode>" + this.codeField.Text + "</hardWarningCode>";
				xml2.Replace("</save>", code + "</save>");
				System.IO.StreamWriter sw = new System.IO.StreamWriter(this.savePath, false);
				sw.Write(xml2);
				sw.Close();
				this.f.goodcertificate = true;
				base.Close();
			}
			else
			{
				MessageBox.Show("Wpisany kod jest nie poprawny, spróbuj jeszcze raz", "Kounikat");
			}
		}
		private string checkPhone()
		{
			XmlDocument save = new XmlDocument();
			save.Load(this.savePath);
			string result;
			try
			{
				int day = System.DateTime.Now.Day;
				int month = System.DateTime.Now.Month;
				int year = System.DateTime.Now.Year;
				XmlNode obw = save.SelectSingleNode("/save/header/nrObwodu");
				System.Collections.Generic.List<string> errors = new System.Collections.Generic.List<string>();
				XmlNode hardWarning = save.SelectSingleNode("/save/hardWarning");
				if (hardWarning != null)
				{
					foreach (XmlNode field in hardWarning)
					{
						for (int i = 0; i < field.ChildNodes.Count; i++)
						{
							bool can = true;
							for (int j = 0; j < errors.Count; j++)
							{
								if (field.ChildNodes[i].InnerText == errors[j])
								{
									can = false;
									break;
								}
							}
							if (can)
							{
								errors.Add(field.ChildNodes[i].InnerText);
							}
						}
					}
				}
				errors.Sort();
				string text = "";
				for (int j = 0; j < errors.Count; j++)
				{
					text += errors[j];
				}
				if (obw != null)
				{
					text += obw.InnerText;
				}
				text += day.ToString();
				text += month.ToString();
				text += year.ToString();
				ClassMd5 k = new ClassMd5();
				string hash = k.CreateMD5Hash2(text);
				result = hash.Substring(0, 10);
				return result;
			}
			catch (XmlException)
			{
			}
			catch (System.NullReferenceException)
			{
			}
			result = "";
			return result;
		}
		private void show_Click(object sender, System.EventArgs e)
		{
			this.codesPanel.AutoSize = true;
			this.codesPanel.MaximumSize = new System.Drawing.Size(640, 0);
			this.codesPanel.Visible = true;
			this.show.Text = "Showaj błędy";
			this.show.Click -= new System.EventHandler(this.show_Click);
			this.show.Click += new System.EventHandler(this.hide_Click);
			this.errorsList.Visible = true;
			string errors = "";
			XmlDocument save = new XmlDocument();
			save.Load(this.savePath);
			try
			{
				XmlNode hardWarning = save.SelectSingleNode("/save/hardWarning");
				System.Collections.Generic.List<string> errorsL = new System.Collections.Generic.List<string>();
				hardWarning = save.SelectSingleNode("/save/hardWarning");
				if (hardWarning != null)
				{
					foreach (XmlNode field in hardWarning)
					{
						for (int i = 0; i < field.ChildNodes.Count; i++)
						{
							bool can = true;
							for (int j = 0; j < errorsL.Count; j++)
							{
								if (field.ChildNodes[i].InnerText == errorsL[j])
								{
									can = false;
									break;
								}
							}
							if (can)
							{
								errorsL.Add(field.ChildNodes[i].InnerText);
							}
						}
					}
				}
				errorsL.Sort();
				errors = "";
				for (int j = 0; j < errorsL.Count; j++)
				{
					errors = errors + errorsL[j] + " ";
				}
			}
			catch (XmlException)
			{
			}
			catch (System.NullReferenceException)
			{
			}
			this.errorsList.Text = errors;
			this.webPanel.Location = new System.Drawing.Point(10, this.codesPanel.Location.Y + this.codesPanel.Height);
		}
		private void hide_Click(object sender, System.EventArgs e)
		{
			this.codesPanel.AutoSize = false;
			this.codesPanel.MaximumSize = new System.Drawing.Size(640, 80);
			this.codesPanel.Size = new System.Drawing.Size(640, 80);
			this.codesPanel.Visible = true;
			this.show.Text = "Pokaż błędy";
			this.show.Click += new System.EventHandler(this.show_Click);
			this.show.Click -= new System.EventHandler(this.hide_Click);
			this.errorsList.Visible = false;
			this.errorsList.Text = "bledy";
			this.webPanel.Location = new System.Drawing.Point(10, 174);
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
			this.components = new System.ComponentModel.Container();
			this.label1 = new Label();
			this.label2 = new Label();
			this.codesPanel = new Panel();
			this.akcept = new Button();
			this.codeField = new TextBox();
			this.label5 = new Label();
			this.webPanel = new Panel();
			this.label4 = new Label();
			this.label3 = new Label();
			this.send = new Button();
			this.toolTip1 = new ToolTip(this.components);
			this.show = new Button();
			this.errorsList = new Label();
			this.codesPanel.SuspendLayout();
			this.webPanel.SuspendLayout();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
			this.label1.Location = new System.Drawing.Point(10, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Uwaga!";
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			this.label2.Location = new System.Drawing.Point(10, 42);
			this.label2.MaximumSize = new System.Drawing.Size(640, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(613, 30);
			this.label2.TabIndex = 1;
			this.label2.Text = "Protokół zawiera ostrzeżenia twarde. Aby przejść dalej musisz wpisać kod, bądź wysłać prośbę o potwierdzenie możliwości zatwierdzenia protokołu z ostrzeżeniami twardymi.";
			this.codesPanel.AutoSize = true;
			this.codesPanel.Controls.Add(this.errorsList);
			this.codesPanel.Controls.Add(this.show);
			this.codesPanel.Controls.Add(this.akcept);
			this.codesPanel.Controls.Add(this.codeField);
			this.codesPanel.Controls.Add(this.label5);
			this.codesPanel.Location = new System.Drawing.Point(10, 88);
			this.codesPanel.Name = "codesPanel";
			this.codesPanel.Size = new System.Drawing.Size(640, 80);
			this.codesPanel.TabIndex = 2;
			this.akcept.Location = new System.Drawing.Point(535, 14);
			this.akcept.Name = "akcept";
			this.akcept.Size = new System.Drawing.Size(75, 23);
			this.akcept.TabIndex = 2;
			this.akcept.Text = "zatwierdź";
			this.akcept.UseVisualStyleBackColor = true;
			this.akcept.Click += new System.EventHandler(this.akcept_Click);
			this.codeField.Location = new System.Drawing.Point(78, 19);
			this.codeField.Name = "codeField";
			this.codeField.Size = new System.Drawing.Size(347, 20);
			this.codeField.TabIndex = 1;
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(3, 19);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(57, 13);
			this.label5.TabIndex = 0;
			this.label5.Text = "Wpisz kod";
			this.webPanel.AutoSize = true;
			this.webPanel.Controls.Add(this.label4);
			this.webPanel.Controls.Add(this.label3);
			this.webPanel.Controls.Add(this.send);
			this.webPanel.Location = new System.Drawing.Point(10, 174);
			this.webPanel.Name = "webPanel";
			this.webPanel.Size = new System.Drawing.Size(640, 68);
			this.webPanel.TabIndex = 3;
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			this.label4.Location = new System.Drawing.Point(3, 7);
			this.label4.MaximumSize = new System.Drawing.Size(640, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(24, 15);
			this.label4.TabIndex = 5;
			this.label4.Text = "lub";
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			this.label3.Location = new System.Drawing.Point(3, 35);
			this.label3.MaximumSize = new System.Drawing.Size(640, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(422, 15);
			this.label3.TabIndex = 4;
			this.label3.Text = "wyślij prośbę o możliwość zatwierdzenia protokołu z ostrzeżeniami twardymi";
			this.send.Location = new System.Drawing.Point(535, 27);
			this.send.Name = "send";
			this.send.Size = new System.Drawing.Size(80, 23);
			this.send.TabIndex = 0;
			this.send.Text = "wyślij";
			this.send.UseVisualStyleBackColor = true;
			this.send.Click += new System.EventHandler(this.send_Click);
			this.show.Location = new System.Drawing.Point(535, 54);
			this.show.Name = "show";
			this.show.Size = new System.Drawing.Size(75, 23);
			this.show.TabIndex = 3;
			this.show.Text = "Pokaż błędy";
			this.show.UseVisualStyleBackColor = true;
			this.show.Click += new System.EventHandler(this.show_Click);
			this.errorsList.AutoSize = true;
			this.errorsList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
			this.errorsList.ForeColor = System.Drawing.Color.DodgerBlue;
			this.errorsList.Location = new System.Drawing.Point(9, 59);
			this.errorsList.MaximumSize = new System.Drawing.Size(500, 0);
			this.errorsList.Name = "errorsList";
			this.errorsList.Size = new System.Drawing.Size(37, 13);
			this.errorsList.TabIndex = 4;
			this.errorsList.Text = "bledy";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.AutoScroll = true;
			this.AutoSize = true;
			base.ClientSize = new System.Drawing.Size(664, 252);
			base.Controls.Add(this.webPanel);
			base.Controls.Add(this.codesPanel);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Name = "EnteringCodes";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Wprowadzanie kodu";
			this.codesPanel.ResumeLayout(false);
			this.codesPanel.PerformLayout();
			this.webPanel.ResumeLayout(false);
			this.webPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
