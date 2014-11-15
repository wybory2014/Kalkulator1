using Kalkulator1.AdditionalClass;
using Kalkulator1.instalClass;
using Kalkulator1.ResponseClass;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;
using System.Xml;
namespace Kalkulator1
{
	public class Attendance : Form
	{
		private string licensePath;
		public string password;
		public bool logged;
		private string role;
		private string path;
		private XmlDocument header;
		private System.ComponentModel.IContainer components = null;
		private Panel FormPanel;
		private Label label1;
		private ComboBox attendanceHour;
		private TextBox attendanceValue;
		private Label label2;
		private Button send;
		private Label label6;
		private Label jns;
		private Label label5;
		private Label okreg;
		private Label label3;
		private Label errorValue;
		private Label errorHour;
		private Label label7;
		private Label obwod;
		private ComboBox obwodList;
		private TextBox currentLwyb;
		private Label errorOBW;
		private Label errorCurrentLwyb;
		private Label klkLwyb;
		public Attendance()
		{
			this.licensePath = "";
			this.InitializeComponent();
			this.errorValue.Visible = false;
			this.errorHour.Visible = false;
			this.path = System.IO.Path.GetTempPath() + "KBW";
		}
		public Attendance(string licensepath, XmlDocument header, string jns, string role, string circuit, string electoralEampaignSave)
		{
			this.licensePath = licensepath;
			string[] electoralEampaignParts = electoralEampaignSave.Split(new char[]
			{
				'_'
			});
			this.password = "";
			this.logged = false;
			this.InitializeComponent();
			this.errorValue.Visible = false;
			this.errorHour.Visible = false;
			this.errorCurrentLwyb.Visible = false;
			this.errorOBW.Visible = false;
			this.okreg.Text = electoralEampaignParts[electoralEampaignParts.Length - 1];
			this.jns.Text = jns;
			this.role = role;
			if (this.role == "P" && circuit != null)
			{
				this.obwod.Text = circuit;
				this.obwodList.Visible = false;
			}
			else
			{
				this.obwodList.Visible = true;
			}
			this.path = System.IO.Path.GetTempPath() + "KBW";
			this.header = header;
			this.setComboBoxObwod(header);
			this.setComboBoxHour(electoralEampaignSave);
			if ((this.obwodList.DataSource as System.Collections.ArrayList).Count <= 1)
			{
				MessageBox.Show("Nie ma obwodu, dla którego można by wysłać frekwencję.", "Komunikat");
				base.Close();
			}
		}
		private void send_Click(object sender, System.EventArgs e)
		{
			this.obwodList_Validated(this.obwodList, e);
			this.currentLwyb_Validated(this.currentLwyb, e);
			this.attendanceHour_Validated(this.attendanceHour, e);
			this.number_Validated(this.attendanceValue, e);
			if ((!this.errorValue.Visible && !this.errorHour.Visible && !this.errorOBW.Visible && !this.errorCurrentLwyb.Visible) || (!this.errorValue.Visible && !this.errorHour.Visible && !this.errorOBW.Visible && this.errorCurrentLwyb.Visible && this.errorCurrentLwyb.Text == "Liczba wyborców uprawnionych do głosowania jest mniejsza od 110% i większa od 90% szacowanej liczby wyborców (" + (this.obwodList.SelectedItem as AttendanceOBWItem).getLwyb().ToString() + ")."))
			{
				WaitPanel p = new WaitPanel("Wait_04", base.Size.Width, base.Size.Height);
				p.setWaitPanel("Trwa importowanie danych", "Proszę czekać");
				p.setSize(this.FormPanel.Size);
				p.setLocation(this.FormPanel.Location);
				base.Controls.Add(p.getPanel());
				base.Controls[p.getName()].BringToFront();
				p.setVisible(true);
				string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
				xml += "<save>";
				string hour = "";
				try
				{
					if (this.attendanceHour.SelectedItem != null)
					{
						hour = (this.attendanceHour.SelectedItem as AttendanceItem).getName();
					}
				}
				catch (System.Exception ex)
				{
				}
				string value = "";
				try
				{
					if (this.attendanceValue.Text != null)
					{
						value = this.attendanceValue.Text;
					}
				}
				catch (System.Exception ex)
				{
				}
				string text = xml;
				xml = string.Concat(new string[]
				{
					text,
					"<attendance><attendanceHour>",
					hour,
					"</attendanceHour><attendanceValue>",
					value,
					"</attendanceValue><currentLwyb>",
					this.currentLwyb.Text,
					"</currentLwyb></attendance>"
				});
				xml += "<header>";
				xml += this.getHeader(this.header);
				xml += "</header>";
				xml += "</save>";
				Commit c = new Commit(this.licensePath, this);
				c.ShowDialog();
				if (this.logged)
				{
					try
					{
						Certificate cer = new Certificate();
						cer.SignXmlText(xml, System.IO.Path.GetTempPath() + "KBW\\tmp\\attendance.xml", this.password, this.licensePath);
						System.IO.StreamReader sr = new System.IO.StreamReader(System.IO.Path.GetTempPath() + "KBW\\tmp\\attendance.xml");
						xml = sr.ReadToEnd();
						sr.Close();
						string uri = "attendances/readval/" + HttpUtility.UrlEncode((this.attendanceHour.SelectedItem as AttendanceItem).getName());
						string post = "xml=" + HttpUtility.UrlEncode(xml);
						Connection con = new Connection();
						Code res = con.postReq(uri, post, 0);
						MessageBox.Show(res.getText(), "Komunikat");
					}
					catch (System.Exception ex)
					{
						MessageBox.Show("Błąd utworzenia wiadomości: " + ex.Message, "Uwaga");
					}
				}
				p.setVisible(false);
			}
		}
		private string getHeader(XmlDocument header)
		{
			string xml = "";
			xml = xml + "<jns_kod>" + this.jns.Text + "</jns_kod>";
			if (this.role == "P")
			{
				xml = xml + "<nrObwodu>" + this.obwod.Text + "</nrObwodu>";
			}
			else
			{
				string obw = "";
				if (this.obwodList.SelectedItem != null)
				{
					obw = (this.obwodList.SelectedItem as AttendanceOBWItem).getName().ToString();
				}
				xml = xml + "<nrObwodu>" + obw + "</nrObwodu>";
			}
			XmlNode headerRoot = header.SelectSingleNode("/akcja_wyborcza/jns");
			XmlNode nameGmina = headerRoot.Attributes.GetNamedItem("nameGmina");
			if (nameGmina != null)
			{
				xml = xml + "<nameGmina>" + nameGmina.Value + "</nameGmina>";
			}
			else
			{
				xml += "<nameGmina></nameGmina>";
			}
			XmlNode namePowiat = headerRoot.Attributes.GetNamedItem("namePowiat");
			if (namePowiat != null)
			{
				xml = xml + "<namePowiat>" + namePowiat.Value + "</namePowiat>";
			}
			else
			{
				xml += "<namePowiat></namePowiat>";
			}
			XmlNode nameW = headerRoot.Attributes.GetNamedItem("nameWojewództwo");
			if (nameW != null)
			{
				xml = xml + "<nameWojewództwo>" + nameW.Value + "</nameWojewództwo>";
			}
			else
			{
				xml += "<nameWojewództwo></nameWojewództwo>";
			}
			xml = xml + "<KalVersion>" + Kalkulator1.instalClass.Version.getVersion().ToString() + "</KalVersion>";
			return xml + "<system>" + System.Environment.OSVersion.VersionString.ToString() + "</system>";
		}
		private void setComboBoxObwod(XmlDocument header)
		{
			XmlNode headerRoot = header.SelectSingleNode("/akcja_wyborcza/jns");
			try
			{
				System.Collections.ArrayList AttendanceOBW = new System.Collections.ArrayList();
				AttendanceOBW.Add(new AttendanceOBWItem(0, "", 0L));
				foreach (XmlNode obw in headerRoot)
				{
					XmlNode nr = obw.Attributes.GetNamedItem("nr");
					XmlNode typ_obwodu = obw.Attributes.GetNamedItem("typ_obwodu");
					if (typ_obwodu != null && typ_obwodu.Value == "P")
					{
						foreach (XmlNode inst in obw)
						{
							XmlNode instname = inst.Attributes.GetNamedItem("kod");
							if (instname != null && instname.Value == "WBP")
							{
								XmlNode lwyb = inst.FirstChild.Attributes.GetNamedItem("lwyb");
								int i = 1;
								if (this.role == "P")
								{
									if (nr != null && nr.Value == this.obwod.Text && lwyb != null)
									{
										AttendanceOBW.Add(new AttendanceOBWItem(i, nr.Value, System.Convert.ToInt64(lwyb.Value)));
										break;
									}
								}
								else
								{
									if (nr != null && lwyb != null)
									{
										AttendanceOBW.Add(new AttendanceOBWItem(i, nr.Value, System.Convert.ToInt64(lwyb.Value)));
										i++;
									}
								}
							}
						}
					}
				}
				this.obwodList.DataSource = AttendanceOBW;
				this.obwodList.DisplayMember = "LongName";
				this.obwodList.ValueMember = "ShortName";
				if (AttendanceOBW.Count == 2)
				{
					this.obwodList.SelectedIndex = 1;
					this.klkLwyb.Text = "(" + (this.obwodList.SelectedItem as AttendanceOBWItem).getLwyb().ToString() + ")";
				}
			}
			catch (System.Exception)
			{
				MessageBox.Show("Nieprawidłowy XML.", "Błąd");
			}
		}
		private void setComboBoxHour(string electoralEampaignSave)
		{
			if (!System.IO.Directory.Exists(this.path + "\\Attendance"))
			{
				try
				{
					System.IO.Directory.CreateDirectory(this.path + "\\Attendance");
				}
				catch (System.ArgumentNullException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Attendance\"", "Error");
				}
				catch (System.ArgumentException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Attendance\"", "Error");
				}
				catch (System.UnauthorizedAccessException)
				{
					MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
				}
				catch (System.IO.PathTooLongException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Attendance\"", "Error");
				}
				catch (System.IO.DirectoryNotFoundException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Attendance\"", "Error");
				}
				catch (System.NotSupportedException)
				{
					MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"Attendance\"", "Error");
				}
				catch (System.IO.IOException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Attendance\"", "Error");
				}
			}
			string uri = "KALK/freq/" + electoralEampaignSave.Replace('_', '/') + "-freq";
			Connection con = new Connection();
			KLKresponse res = con.getRequestKBWKlk(uri, this.path + "\\Attendance\\frekwencja.xml", 0);
			XmlDocument hour = new XmlDocument();
			hour.Load(this.path + "\\Attendance\\frekwencja.xml");
			XmlNode hourRoot = hour.SelectSingleNode("/frekwencja");
			System.Collections.ArrayList AttendanceTime = new System.Collections.ArrayList();
			AttendanceTime.Add(new AttendanceItem("0", ""));
			foreach (XmlNode item in hourRoot)
			{
				XmlNode id = item.Attributes.GetNamedItem("id");
				XmlNode value = item.Attributes.GetNamedItem("value");
				if (id != null && value != null)
				{
					AttendanceTime.Add(new AttendanceItem(id.Value, value.Value));
				}
			}
			this.attendanceHour.DataSource = AttendanceTime;
			this.attendanceHour.DisplayMember = "LongName";
			this.attendanceHour.ValueMember = "ShortName";
		}
		private void number_Validated(object sender, System.EventArgs e)
		{
			string pattern = "^[0-9]+$";
			if ((sender as TextBox).Text != "")
			{
				if (!System.Text.RegularExpressions.Regex.IsMatch((sender as TextBox).Text, pattern))
				{
					(sender as TextBox).ForeColor = System.Drawing.Color.Red;
					(sender as TextBox).BackColor = System.Drawing.SystemColors.Info;
					this.errorValue.Text = "Dozwolone liczby dodatnie";
					this.errorValue.Visible = true;
				}
				else
				{
					if ((this.currentLwyb.Text != "" && !this.errorCurrentLwyb.Visible) || (this.errorCurrentLwyb.Visible && this.errorCurrentLwyb.Text == "Liczba wyborców uprawnionych do głosowania jest mniejsza od 110% i większa od 90% szacowanej liczby wyborców (" + (this.obwodList.SelectedItem as AttendanceOBWItem).getLwyb().ToString() + ")."))
					{
						if (System.Convert.ToInt32((sender as TextBox).Text) <= System.Convert.ToInt32(this.currentLwyb.Text))
						{
							(sender as TextBox).ForeColor = System.Drawing.Color.Black;
							(sender as TextBox).BackColor = System.Drawing.SystemColors.Window;
							this.errorValue.Visible = false;
							(sender as TextBox).Text = System.Convert.ToInt32((sender as TextBox).Text).ToString();
						}
						else
						{
							(sender as TextBox).ForeColor = System.Drawing.Color.Red;
							(sender as TextBox).BackColor = System.Drawing.SystemColors.Info;
							this.errorValue.Text = "Liczba wydanych kart nie może być większa od liczby wyborców uprawnionych do głosowania";
							this.errorValue.Visible = true;
						}
					}
					else
					{
						this.currentLwyb.ForeColor = System.Drawing.Color.Red;
						this.currentLwyb.BackColor = System.Drawing.SystemColors.Info;
						if (this.currentLwyb.Text == "")
						{
							this.errorCurrentLwyb.Text = "Pole jest wymagane";
							this.errorCurrentLwyb.ForeColor = System.Drawing.Color.Red;
						}
						this.errorCurrentLwyb.Visible = true;
						this.currentLwyb.Text = "";
						(sender as TextBox).ForeColor = System.Drawing.Color.Red;
						(sender as TextBox).BackColor = System.Drawing.SystemColors.Info;
						this.errorValue.Text = "Do sprawdzenia tego pola wymagane jest poprawne wypełnienie pola liczby wyborców";
						this.errorValue.Visible = true;
					}
				}
			}
			else
			{
				(sender as TextBox).ForeColor = System.Drawing.Color.Red;
				(sender as TextBox).BackColor = System.Drawing.SystemColors.Info;
				this.errorValue.Text = "Pole jest wymagane";
				this.errorValue.Visible = true;
				(sender as TextBox).Text = "";
			}
		}
		private void attendanceHour_Validated(object sender, System.EventArgs e)
		{
			if ((sender as ComboBox).SelectedIndex == 0)
			{
				(sender as ComboBox).ForeColor = System.Drawing.Color.Red;
				(sender as ComboBox).BackColor = System.Drawing.SystemColors.Info;
				this.errorHour.Visible = true;
			}
			else
			{
				(sender as ComboBox).ForeColor = System.Drawing.Color.Black;
				(sender as ComboBox).BackColor = System.Drawing.SystemColors.Window;
				this.errorHour.Visible = false;
			}
		}
		private void currentLwyb_Validated(object sender, System.EventArgs e)
		{
			string pattern = "^[0-9]+$";
			if ((sender as TextBox).Text != "")
			{
				if (!System.Text.RegularExpressions.Regex.IsMatch((sender as TextBox).Text, pattern))
				{
					(sender as TextBox).ForeColor = System.Drawing.Color.Red;
					(sender as TextBox).BackColor = System.Drawing.SystemColors.Info;
					this.errorCurrentLwyb.ForeColor = System.Drawing.Color.Red;
					this.errorCurrentLwyb.Text = "Dozwolone liczby dodatnie";
					this.errorCurrentLwyb.Visible = true;
				}
				else
				{
					if (this.obwodList.SelectedIndex != 0)
					{
						this.errorOBW.Visible = false;
						object item = this.obwodList.SelectedItem;
						decimal a = System.Convert.ToDecimal((sender as TextBox).Text) / System.Convert.ToDecimal((item as AttendanceOBWItem).getLwyb()) * 100m;
						if (a >= 90m && a <= 110m)
						{
							(sender as TextBox).ForeColor = System.Drawing.Color.Black;
							this.errorCurrentLwyb.ForeColor = System.Drawing.Color.Black;
							(sender as TextBox).BackColor = System.Drawing.SystemColors.Window;
							this.errorCurrentLwyb.Visible = false;
							(sender as TextBox).Text = System.Convert.ToInt32((sender as TextBox).Text).ToString();
							if (this.errorValue.Visible && this.errorValue.Text == "Do sprawdzenia tego pola wymagane jest poprawne wypełnienie pola liczby wyborców")
							{
								this.number_Validated(this.attendanceValue, e);
							}
						}
						else
						{
							(sender as TextBox).ForeColor = System.Drawing.Color.DodgerBlue;
							this.errorCurrentLwyb.ForeColor = System.Drawing.Color.DodgerBlue;
							(sender as TextBox).BackColor = System.Drawing.SystemColors.Info;
							this.errorCurrentLwyb.Text = "Liczba wyborców uprawnionych do głosowania jest mniejsza od 110% i większa od 90% szacowanej liczby wyborców (" + (item as AttendanceOBWItem).getLwyb().ToString() + ").";
							this.errorCurrentLwyb.Visible = true;
						}
					}
					else
					{
						this.obwodList.ForeColor = System.Drawing.Color.Red;
						this.obwodList.BackColor = System.Drawing.SystemColors.Info;
						this.errorCurrentLwyb.ForeColor = System.Drawing.Color.Red;
						this.errorOBW.Visible = true;
						(sender as TextBox).ForeColor = System.Drawing.Color.Red;
						(sender as TextBox).BackColor = System.Drawing.SystemColors.Info;
						this.errorCurrentLwyb.ForeColor = System.Drawing.Color.Red;
						this.errorCurrentLwyb.Text = "Do sprawdzenia tego pola wymagane jest wybranie obwodu";
						this.errorCurrentLwyb.Visible = true;
					}
				}
			}
			else
			{
				(sender as TextBox).ForeColor = System.Drawing.Color.Red;
				this.errorCurrentLwyb.ForeColor = System.Drawing.Color.Red;
				(sender as TextBox).BackColor = System.Drawing.SystemColors.Info;
				this.errorCurrentLwyb.Text = "Pole jest wymagane";
				this.errorCurrentLwyb.Visible = true;
				(sender as TextBox).Text = "";
			}
		}
		private void obwodList_Validated(object sender, System.EventArgs e)
		{
			int a = (sender as ComboBox).SelectedIndex;
			if ((sender as ComboBox).SelectedIndex == 0)
			{
				(sender as ComboBox).ForeColor = System.Drawing.Color.Red;
				(sender as ComboBox).BackColor = System.Drawing.SystemColors.Info;
				this.errorOBW.Visible = true;
			}
			else
			{
				(sender as ComboBox).ForeColor = System.Drawing.Color.Black;
				(sender as ComboBox).BackColor = System.Drawing.SystemColors.Window;
				this.errorOBW.Visible = false;
				this.klkLwyb.Text = "(" + (this.obwodList.SelectedItem as AttendanceOBWItem).getLwyb().ToString() + ")";
				if (this.errorCurrentLwyb.Visible && this.errorCurrentLwyb.Text == "Do sprawdzenia tego pola wymagane jest wybranie obwodu")
				{
					this.currentLwyb_Validated(this.currentLwyb, e);
				}
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
			this.FormPanel = new Panel();
			this.klkLwyb = new Label();
			this.errorCurrentLwyb = new Label();
			this.errorOBW = new Label();
			this.currentLwyb = new TextBox();
			this.obwodList = new ComboBox();
			this.label7 = new Label();
			this.obwod = new Label();
			this.errorHour = new Label();
			this.errorValue = new Label();
			this.label6 = new Label();
			this.jns = new Label();
			this.label5 = new Label();
			this.okreg = new Label();
			this.label3 = new Label();
			this.send = new Button();
			this.attendanceHour = new ComboBox();
			this.attendanceValue = new TextBox();
			this.label2 = new Label();
			this.label1 = new Label();
			this.FormPanel.SuspendLayout();
			base.SuspendLayout();
			this.FormPanel.Controls.Add(this.klkLwyb);
			this.FormPanel.Controls.Add(this.errorCurrentLwyb);
			this.FormPanel.Controls.Add(this.errorOBW);
			this.FormPanel.Controls.Add(this.currentLwyb);
			this.FormPanel.Controls.Add(this.obwodList);
			this.FormPanel.Controls.Add(this.label7);
			this.FormPanel.Controls.Add(this.obwod);
			this.FormPanel.Controls.Add(this.errorHour);
			this.FormPanel.Controls.Add(this.errorValue);
			this.FormPanel.Controls.Add(this.label6);
			this.FormPanel.Controls.Add(this.jns);
			this.FormPanel.Controls.Add(this.label5);
			this.FormPanel.Controls.Add(this.okreg);
			this.FormPanel.Controls.Add(this.label3);
			this.FormPanel.Controls.Add(this.send);
			this.FormPanel.Controls.Add(this.attendanceHour);
			this.FormPanel.Controls.Add(this.attendanceValue);
			this.FormPanel.Controls.Add(this.label2);
			this.FormPanel.Controls.Add(this.label1);
			this.FormPanel.Location = new System.Drawing.Point(13, 13);
			this.FormPanel.Name = "FormPanel";
			this.FormPanel.Size = new System.Drawing.Size(515, 328);
			this.FormPanel.TabIndex = 0;
			this.klkLwyb.AutoSize = true;
			this.klkLwyb.Location = new System.Drawing.Point(22, 153);
			this.klkLwyb.MaximumSize = new System.Drawing.Size(495, 0);
			this.klkLwyb.Name = "klkLwyb";
			this.klkLwyb.Size = new System.Drawing.Size(0, 13);
			this.klkLwyb.TabIndex = 20;
			this.errorCurrentLwyb.AutoSize = true;
			this.errorCurrentLwyb.ForeColor = System.Drawing.Color.DodgerBlue;
			this.errorCurrentLwyb.Location = new System.Drawing.Point(146, 160);
			this.errorCurrentLwyb.MaximumSize = new System.Drawing.Size(350, 0);
			this.errorCurrentLwyb.Name = "errorCurrentLwyb";
			this.errorCurrentLwyb.Size = new System.Drawing.Size(136, 13);
			this.errorCurrentLwyb.TabIndex = 19;
			this.errorCurrentLwyb.Text = "Dozwolone liczby dodatnie.";
			this.errorOBW.AutoSize = true;
			this.errorOBW.ForeColor = System.Drawing.Color.Red;
			this.errorOBW.Location = new System.Drawing.Point(365, 41);
			this.errorOBW.Name = "errorOBW";
			this.errorOBW.Size = new System.Drawing.Size(80, 13);
			this.errorOBW.TabIndex = 18;
			this.errorOBW.Text = "Wybierz obwód";
			this.currentLwyb.Location = new System.Drawing.Point(148, 137);
			this.currentLwyb.Name = "currentLwyb";
			this.currentLwyb.Size = new System.Drawing.Size(341, 20);
			this.currentLwyb.TabIndex = 3;
			this.currentLwyb.TextChanged += new System.EventHandler(this.currentLwyb_Validated);
			this.currentLwyb.Validated += new System.EventHandler(this.currentLwyb_Validated);
			this.obwodList.FormattingEnabled = true;
			this.obwodList.Location = new System.Drawing.Point(368, 17);
			this.obwodList.Name = "obwodList";
			this.obwodList.Size = new System.Drawing.Size(121, 21);
			this.obwodList.TabIndex = 1;
			this.obwodList.SelectionChangeCommitted += new System.EventHandler(this.obwodList_Validated);
			this.obwodList.Validated += new System.EventHandler(this.obwodList_Validated);
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(22, 140);
			this.label7.MaximumSize = new System.Drawing.Size(495, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(110, 13);
			this.label7.TabIndex = 14;
			this.label7.Text = "Liczba uprawnionych:";
			this.obwod.AutoSize = true;
			this.obwod.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
			this.obwod.Location = new System.Drawing.Point(370, 20);
			this.obwod.Name = "obwod";
			this.obwod.Size = new System.Drawing.Size(23, 13);
			this.obwod.TabIndex = 13;
			this.obwod.Text = "XX";
			this.errorHour.AutoSize = true;
			this.errorHour.ForeColor = System.Drawing.Color.Red;
			this.errorHour.Location = new System.Drawing.Point(146, 97);
			this.errorHour.MaximumSize = new System.Drawing.Size(350, 0);
			this.errorHour.Name = "errorHour";
			this.errorHour.Size = new System.Drawing.Size(136, 13);
			this.errorHour.TabIndex = 12;
			this.errorHour.Text = "Wybierz godzinę frekwencji";
			this.errorValue.AutoSize = true;
			this.errorValue.ForeColor = System.Drawing.Color.Red;
			this.errorValue.Location = new System.Drawing.Point(145, 224);
			this.errorValue.MaximumSize = new System.Drawing.Size(341, 0);
			this.errorValue.Name = "errorValue";
			this.errorValue.Size = new System.Drawing.Size(136, 13);
			this.errorValue.TabIndex = 11;
			this.errorValue.Text = "Dozwolone liczby dodatnie.";
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(319, 20);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(44, 13);
			this.label6.TabIndex = 9;
			this.label6.Text = "Obwód:";
			this.jns.AutoSize = true;
			this.jns.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
			this.jns.Location = new System.Drawing.Point(190, 20);
			this.jns.Name = "jns";
			this.jns.Size = new System.Drawing.Size(55, 13);
			this.jns.TabIndex = 8;
			this.jns.Text = "XXXXXX";
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(145, 20);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(40, 13);
			this.label5.TabIndex = 7;
			this.label5.Text = "Gmina:";
			this.okreg.AutoSize = true;
			this.okreg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
			this.okreg.Location = new System.Drawing.Point(67, 20);
			this.okreg.Name = "okreg";
			this.okreg.Size = new System.Drawing.Size(23, 13);
			this.okreg.TabIndex = 6;
			this.okreg.Text = "XX";
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(22, 20);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(37, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Akcja:";
			this.send.Location = new System.Drawing.Point(317, 289);
			this.send.Name = "send";
			this.send.Size = new System.Drawing.Size(172, 23);
			this.send.TabIndex = 5;
			this.send.Text = "Wyślij";
			this.send.UseVisualStyleBackColor = true;
			this.send.Click += new System.EventHandler(this.send_Click);
			this.attendanceHour.FormattingEnabled = true;
			this.attendanceHour.Location = new System.Drawing.Point(148, 73);
			this.attendanceHour.Name = "attendanceHour";
			this.attendanceHour.Size = new System.Drawing.Size(341, 21);
			this.attendanceHour.TabIndex = 2;
			this.attendanceHour.SelectionChangeCommitted += new System.EventHandler(this.attendanceHour_Validated);
			this.attendanceHour.Validated += new System.EventHandler(this.attendanceHour_Validated);
			this.attendanceValue.Location = new System.Drawing.Point(148, 201);
			this.attendanceValue.Name = "attendanceValue";
			this.attendanceValue.Size = new System.Drawing.Size(341, 20);
			this.attendanceValue.TabIndex = 4;
			this.attendanceValue.TextChanged += new System.EventHandler(this.number_Validated);
			this.attendanceValue.Validated += new System.EventHandler(this.number_Validated);
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(22, 204);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(113, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Liczba wydanych kart:";
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(22, 76);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(49, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Godzina:";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(541, 356);
			base.Controls.Add(this.FormPanel);
			base.Name = "Attendance";
			this.Text = "Frekwencja";
			this.FormPanel.ResumeLayout(false);
			this.FormPanel.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
