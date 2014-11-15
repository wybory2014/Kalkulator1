using Kalkulator1.AdditionalClass;
using Kalkulator1.AdditionalWindow;
using Kalkulator1.instalClass;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
namespace Kalkulator1
{
	public class SendProtocol : Form
	{
		private XmlDocument protocolDefinition;
		private XmlDocument candidates;
		private XmlDocument committee;
		private XmlDocument header;
		private XmlDocument validateDefinition;
		private string protocolDefinitionName;
		private string candidatesName;
		private string committeeName;
		private string headerName;
		private string validateDefinitionName;
		private XmlDocument save;
		private string savePath;
		private string path = "";
		private System.Drawing.FontFamily myfont = new System.Drawing.FontFamily("Arial");
		public ErrorProvider errorProvider1;
		public bool goodcertificate;
		public string codeWarning;
		public string komSend = "";
		public string password = "";
		public WaitPanel wait;
		private ProtocolsList form;
		private string OU;
		private string zORp = "";
		private string licensePath = "";
		private System.ComponentModel.IContainer components = null;
		private Panel signPanel;
		private DataGridView LicencesTable;
		private Panel preview;
		private WebBrowser webPreview;
		public SendProtocol()
		{
			this.InitializeComponent();
			this.codeWarning = "";
			this.headerName = "";
			this.committeeName = "";
			this.candidatesName = "";
			this.protocolDefinitionName = "";
			this.validateDefinitionName = "";
			this.protocolDefinition = new XmlDocument();
			this.candidates = new XmlDocument();
			this.committee = new XmlDocument();
			this.validateDefinition = new XmlDocument();
			this.header = new XmlDocument();
			this.path = System.IO.Path.GetTempPath() + "KBW";
			this.goodcertificate = false;
			this.wait = new WaitPanel("Wait3", base.Width, base.Height);
			this.form = null;
		}
		public SendProtocol(ProtocolsList form, XmlDocument header, string protocolDefinition, string candidates, string committee, string validateDefinition, string save, string OU, string licensePath)
		{
			this.InitializeComponent();
			this.Text = this.Text + " (" + Kalkulator1.instalClass.Version.getVersion().ToString() + ")";
			this.licensePath = licensePath;
			this.path = System.IO.Path.GetTempPath() + "KBW";
			this.preview.Width = 750;
			this.codeWarning = "";
			this.wait = new WaitPanel("Wait3", base.Width, base.Height);
			base.Controls.Add(this.wait.getPanel());
			base.Controls[this.wait.getName()].BringToFront();
			this.form = form;
			this.OU = OU;
			form.wait.setWaitPanel("Trwa otwieranie formularza protokołu", "Proszę czekać");
			form.wait.setVisible(true);
			this.errorProvider1 = new ErrorProvider();
			string[] p = protocolDefinition.Split(new char[]
			{
				'\\'
			});
			this.protocolDefinitionName = p[p.Length - 1].Replace('_', '/').Replace(".xml", "");
			string[] p2 = candidates.Split(new char[]
			{
				'\\'
			});
			this.candidatesName = p2[p2.Length - 1].Replace('_', '/').Replace(".xml", "");
			string[] p3 = committee.Split(new char[]
			{
				'\\'
			});
			this.committeeName = p3[p3.Length - 1].Replace('_', '/').Replace(".xml", "");
			string[] p4 = validateDefinition.Split(new char[]
			{
				'\\'
			});
			this.validateDefinitionName = p4[p4.Length - 1].Replace('_', '/').Replace(".xml", "");
			string[] p5 = this.committeeName.Split(new char[]
			{
				'-'
			});
			if (p5.Length > 2)
			{
				this.headerName = p5[0] + "-" + p5[1];
			}
			form.wait.setWaitPanel("Trwa otwieranie formularza protokołu - wczytywanie danych", "Proszę czekać");
			try
			{
				this.header = new XmlDocument();
				this.header = header;
				this.savePath = save;
				if (protocolDefinition != "")
				{
					this.protocolDefinition = new XmlDocument();
					if (System.IO.File.Exists(protocolDefinition))
					{
						this.protocolDefinition.Load(protocolDefinition);
					}
				}
				if (candidates != "")
				{
					this.candidates = new XmlDocument();
					if (System.IO.File.Exists(candidates))
					{
						this.candidates.Load(candidates);
					}
				}
				if (committee != "")
				{
					this.committee = new XmlDocument();
					if (System.IO.File.Exists(committee))
					{
						this.committee.Load(committee);
					}
				}
				if (validateDefinition != "")
				{
					this.validateDefinition = new XmlDocument();
					if (System.IO.File.Exists(validateDefinition))
					{
						this.validateDefinition.Load(validateDefinition);
					}
				}
				if (save != "")
				{
					this.save = new XmlDocument();
					if (System.IO.File.Exists(save))
					{
						this.save.Load(save);
					}
				}
			}
			catch (XmlException e)
			{
				MessageBox.Show("Nieprawidłowy XML: " + e.Message, "Error");
			}
			this.goodcertificate = false;
			form.wait.setWaitPanel("Trwa otwieranie formularza protokołu - sprawdzanie plikow definicyjnych formularza protokołu", "Proszę czekać");
			try
			{
				bool isKlk = true;
				bool isupdatedKlk = true;
				string kom = "Nieaktualnie pliki definicyjne: ";
				int i = 0;
				XmlNode klk = this.save.SelectSingleNode("/save/header/defklk");
				foreach (XmlNode j in klk)
				{
					XmlNode name = j.Attributes.GetNamedItem("name");
					XmlNode data_wersji = j.Attributes.GetNamedItem("data_wersji");
					if (name != null && data_wersji != null)
					{
						if (this.protocolDefinitionName == name.Value)
						{
							XmlNode wersja = this.protocolDefinition.SelectSingleNode("/protokol_info").Attributes.GetNamedItem("data_wersji");
							if (wersja != null && data_wersji.Value != wersja.Value)
							{
								if (i == 0)
								{
									string text = kom;
									kom = string.Concat(new string[]
									{
										text,
										this.protocolDefinitionName,
										" (wymagany z dnia ",
										data_wersji.Value,
										")"
									});
								}
								else
								{
									string text = kom;
									kom = string.Concat(new string[]
									{
										text,
										", ",
										this.protocolDefinitionName,
										" (wymagany z dnia ",
										data_wersji.Value,
										")"
									});
								}
								i++;
								isupdatedKlk = false;
							}
						}
						else
						{
							if (this.candidatesName == name.Value)
							{
								XmlNode wersja = this.candidates.SelectSingleNode("/listy").Attributes.GetNamedItem("data_aktualizacji");
								if (wersja != null && data_wersji.Value != wersja.Value)
								{
									if (i == 0)
									{
										string text = kom;
										kom = string.Concat(new string[]
										{
											text,
											this.candidatesName,
											" (wymagany z dnia ",
											data_wersji.Value,
											")"
										});
									}
									else
									{
										string text = kom;
										kom = string.Concat(new string[]
										{
											text,
											", ",
											this.candidatesName,
											" (wymagany z dnia ",
											data_wersji.Value,
											")"
										});
									}
									i++;
									isupdatedKlk = false;
								}
							}
							else
							{
								if (this.committeeName == name.Value)
								{
									XmlNode wersja = this.committee.SelectSingleNode("/komisja_sklad").Attributes.GetNamedItem("data_wersji");
									if (wersja != null && data_wersji.Value != wersja.Value)
									{
										if (i == 0)
										{
											string text = kom;
											kom = string.Concat(new string[]
											{
												text,
												this.committeeName,
												" (wymagany z dnia ",
												data_wersji.Value,
												")"
											});
										}
										else
										{
											string text = kom;
											kom = string.Concat(new string[]
											{
												text,
												", ",
												this.committeeName,
												" (wymagany z dnia ",
												data_wersji.Value,
												")"
											});
										}
										i++;
										isupdatedKlk = false;
									}
								}
								else
								{
									if (this.validateDefinitionName == name.Value)
									{
										XmlNode wersja = this.validateDefinition.SelectSingleNode("/validate_info").Attributes.GetNamedItem("data_wersji");
										if (wersja != null && data_wersji.Value != wersja.Value)
										{
											if (i == 0)
											{
												string text = kom;
												kom = string.Concat(new string[]
												{
													text,
													this.validateDefinitionName,
													" (wymagany z dnia ",
													data_wersji.Value,
													")"
												});
											}
											else
											{
												string text = kom;
												kom = string.Concat(new string[]
												{
													text,
													", ",
													this.validateDefinitionName,
													" (wymagany z dnia ",
													data_wersji.Value,
													")"
												});
											}
											i++;
											isupdatedKlk = false;
										}
									}
									else
									{
										if (!(this.headerName == name.Value))
										{
											isKlk = false;
											break;
										}
										XmlNode wersja = this.header.SelectSingleNode("/akcja_wyborcza").Attributes.GetNamedItem("data_aktualizacji");
										if (wersja != null && data_wersji.Value != wersja.Value)
										{
											if (i == 0)
											{
												string text = kom;
												kom = string.Concat(new string[]
												{
													text,
													this.headerName,
													" (wymagany z dnia ",
													data_wersji.Value,
													")"
												});
											}
											else
											{
												string text = kom;
												kom = string.Concat(new string[]
												{
													text,
													", ",
													this.headerName,
													" (wymagany z dnia ",
													data_wersji.Value,
													")"
												});
											}
											i++;
											isupdatedKlk = false;
										}
									}
								}
							}
						}
					}
				}
				if (!isKlk)
				{
					MessageBox.Show("Nie posiadasz wszystkich potrzebnych plików klk", "Uwaga");
					base.Close();
				}
				else
				{
					if (!isupdatedKlk)
					{
						MessageBox.Show("Nie posiadasz odpowiednich plików klk. " + kom, "Uwaga");
						base.Close();
					}
					else
					{
						form.wait.setWaitPanel("Trwa otwieranie formularza protokołu - ładowanie podglądu", "Proszę czekać");
						string controlSum = "";
						XmlNode saveStep = this.save.SelectSingleNode("/save/step");
						if (saveStep != null && saveStep.InnerText == "0")
						{
							string docXml = "";
							XmlNode saveheader = this.save.SelectSingleNode("/save/header");
							if (header != null)
							{
								docXml += header.OuterXml;
							}
							XmlNode savestep = this.save.SelectSingleNode("/save/step");
							if (savestep != null)
							{
								docXml += savestep.OuterXml;
							}
							XmlNode saveform = this.save.SelectSingleNode("/save/form");
							if (form != null)
							{
								docXml += saveform.OuterXml;
							}
							XmlNode savekomisja_sklad = this.save.SelectSingleNode("/save/komisja_sklad");
							if (savekomisja_sklad != null)
							{
								docXml += savekomisja_sklad.OuterXml;
							}
							XmlNode savehardWarningCode = this.save.SelectSingleNode("/save/hardWarningCode");
							if (savehardWarningCode != null)
							{
								docXml += savehardWarningCode.OuterXml;
							}
							XmlNode savesoftError = this.save.SelectSingleNode("/save/softError");
							if (savesoftError != null)
							{
								docXml += savesoftError.OuterXml;
							}
							XmlNode savehardError = this.save.SelectSingleNode("/save/hardError");
							if (savehardError != null)
							{
								docXml += savehardError.OuterXml;
							}
							XmlNode savehardWarning = this.save.SelectSingleNode("/save/hardWarning");
							if (savehardWarning != null)
							{
								docXml += savehardWarning.OuterXml;
							}
							ClassMd5 k = new ClassMd5();
							controlSum = k.CreateMD5Hash(docXml);
						}
						string docDefinition = protocolDefinition.Replace(".xml", ".docx");
						printProtocolNew protocol = new printProtocolNew();
						string[] partfilepath = this.savePath.Split(new char[]
						{
							'\\'
						});
						string[] dataPath = partfilepath[partfilepath.Length - 1].Split(new char[]
						{
							'-'
						});
						string jns = dataPath[1].Replace("Jns", "");
						string inst = dataPath[3].Replace("Inst", "");
						string obwod = dataPath[2].Replace("Obw", "");
						string instJNS = dataPath[4].Replace("Obw", "");
						string okreg = dataPath[5].Replace("Okr", "");
						okreg = okreg.Replace(".xml", "");
						protocol.ProtocolPrint(this.header, this.save, this.candidates, docDefinition, controlSum, false, obwod, inst, okreg, candidates, instJNS);
						form.wait.setWaitPanel("Trwa wczytywanie licencji", "Proszę czekać");
						bool isP = false;
						bool isZ = false;
						XmlNode com = this.save.SelectSingleNode("/save/komisja_sklad");
						foreach (XmlNode j in com)
						{
							XmlNode funkcja = j.Attributes.GetNamedItem("funkcja");
							if (funkcja != null)
							{
								if (funkcja.Value == "ZASTĘPCA")
								{
									isZ = true;
								}
								if (funkcja.Value == "PRZEWODNICZĄCY")
								{
									isP = true;
								}
							}
							if (isZ && isP)
							{
								break;
							}
						}
						if (isP)
						{
							this.zORp += "P";
						}
						if (isZ)
						{
							this.zORp += "Z";
						}
						this.getSignPage();
						this.LicencesTable.CellClick += new DataGridViewCellEventHandler(this.getLicense_CellClick);
					}
				}
			}
			catch (System.Exception e2)
			{
				MessageBox.Show("Nie mozna wczytać danych. " + e2.Message, "Uwaga");
				base.Close();
			}
			form.wait.setVisible(false);
		}
		private void getLicense_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.ColumnIndex == this.LicencesTable.Columns["action"].Index)
				{
					try
					{
						object name = this.LicencesTable.Rows[e.RowIndex].Cells["Licencja"].Value;
						string filepath = this.path + "\\Licenses\\" + name.ToString();
						Commit com = new Commit(filepath, this);
						com.ShowDialog();
						if (this.goodcertificate)
						{
							this.komSend = "";
							Connection con = new Connection();
							if (con.IsAvailableNetworkActive())
							{
								Eksport ex = new Eksport(this.savePath, true, this, this.licensePath, this.password);
								try
								{
									ex.ShowDialog();
								}
								catch (System.Exception)
								{
								}
								if (this.goodcertificate)
								{
									try
									{
										System.IO.StreamReader sr = new System.IO.StreamReader(this.savePath);
										string file = sr.ReadToEnd();
										sr.Close();
										file = file.Replace("<status>podpisany</status>", "<status>wysłany</status>");
										System.IO.StreamWriter sw = new System.IO.StreamWriter(this.savePath, false);
										sw.Write(file);
										sw.Close();
									}
									catch (System.Exception ex2)
									{
										MessageBox.Show("Protokół został wysłany, ale nie można zmienić jego statusu. " + ex2.Message, "Uwaga");
										base.Close();
									}
								}
							}
							else
							{
								this.komSend = "Protokół nie został wysłany na serwer z powodu braku internetu";
							}
							MessageBox.Show(this.komSend, "Uwaga");
							base.Close();
						}
					}
					catch (System.ArgumentOutOfRangeException)
					{
					}
				}
			}
			catch (System.Exception)
			{
			}
		}
		private void getSignPage()
		{
			int x = 0;
			int y = 0;
			Label lab = new Label();
			lab.Text = "Wybierz licencję, którą chcesz podpisać protokół";
			lab.AutoSize = true;
			lab.MaximumSize = new System.Drawing.Size(this.signPanel.Size.Width - 20, 0);
			lab.Font = new System.Drawing.Font(this.myfont, 9f, System.Drawing.FontStyle.Bold);
			lab.ForeColor = System.Drawing.Color.Black;
			lab.Padding = new Padding(10, 0, 10, 0);
			lab.Location = new System.Drawing.Point(x, y);
			y += lab.Height + 20;
			this.signPanel.Controls.Add(lab);
			DataTable dt = new DataTable();
			if (System.IO.Directory.Exists(this.path + "\\Licenses"))
			{
				dt.Columns.Add(new DataColumn("lp", typeof(string)));
				dt.Columns.Add(new DataColumn("Użytkownik", typeof(string)));
				dt.Columns.Add(new DataColumn("Funkcja", typeof(string)));
				dt.Columns.Add(new DataColumn("Licencja", typeof(string)));
				char[] zORpChar = this.zORp.ToCharArray();
				try
				{
					string saveJns = "";
					string saveElection = "";
					string saveCircuit = "";
					XmlNode xmlJns = this.save.SelectSingleNode("/save/header/jns_kod");
					if (xmlJns != null && xmlJns.InnerText != "")
					{
						saveJns = xmlJns.InnerText;
					}
					XmlNode xmlElection = this.save.SelectSingleNode("/save/header/defklk");
					if (xmlElection != null && xmlElection.FirstChild != null)
					{
						XmlNode xmlElection2 = xmlElection.FirstChild.Attributes.GetNamedItem("name");
						if (xmlElection2 != null && xmlElection2.Value != "")
						{
							string[] saveElectionPart = xmlElection2.Value.Split(new char[]
							{
								'-'
							});
							saveElection = saveElectionPart[0].Replace("/", "_");
						}
					}
					XmlNode xmlCircuit = this.save.SelectSingleNode("/save/header/nrObwodu");
					if (xmlCircuit != null && xmlCircuit.InnerText != "")
					{
						saveCircuit = xmlCircuit.InnerText;
					}
					Certificate certificate = new Certificate();
					foreach (string file in System.IO.Directory.EnumerateFiles(this.path + "\\Licenses", "*.pem"))
					{
						Certificate c = new Certificate(file);
						if (c.isActiveLicense())
						{
							string[] filename = file.Split(new char[]
							{
								'\\'
							});
							bool go = false;
							string paternA = string.Concat(new string[]
							{
								"^",
								saveElection,
								"-",
								saveJns,
								"-A"
							});
							for (int i = 0; i < zORpChar.Length; i++)
							{
								string patern = string.Concat(new string[]
								{
									"^",
									saveElection,
									"-",
									saveJns,
									"-",
									zORpChar[i].ToString()
								});
								if (zORpChar[i].ToString() == "P" || zORpChar[i].ToString() == "Z")
								{
									patern = patern + "-" + saveCircuit;
								}
								if (System.Text.RegularExpressions.Regex.IsMatch(filename[filename.Length - 1], patern) || System.Text.RegularExpressions.Regex.IsMatch(filename[filename.Length - 1], paternA))
								{
									go = true;
								}
							}
							if (go)
							{
								DataRow dr = dt.NewRow();
								dr[0] = dt.Rows.Count + 1;
								try
								{
									string[] subjectPatrs = c.getSubject().Split(new string[]
									{
										", "
									}, System.StringSplitOptions.None);
									int end = 0;
									for (int i = 0; i < subjectPatrs.Length; i++)
									{
										if (System.Text.RegularExpressions.Regex.IsMatch(subjectPatrs[i], "^CN="))
										{
											dr[1] = subjectPatrs[i].Replace("CN=", "");
											end++;
										}
										if (System.Text.RegularExpressions.Regex.IsMatch(subjectPatrs[i], "^OU="))
										{
											string[] tmp = subjectPatrs[i].Replace("OU=", "").Split(new char[]
											{
												'-'
											});
											if (tmp[2] == "O")
											{
												dr[2] = "Operator";
											}
											else
											{
												if (tmp[2] == "P")
												{
													dr[2] = "Przewodniczący";
												}
												else
												{
													if (tmp[2] == "Z")
													{
														dr[2] = "Zastępca";
													}
													else
													{
														dr[2] = tmp[2];
													}
												}
											}
											end++;
										}
										if (end >= 2)
										{
											break;
										}
									}
								}
								catch (System.Exception)
								{
									dr[1] = "";
									dr[2] = "";
								}
								dr[3] = filename[filename.Length - 1];
								dt.Rows.Add(dr);
							}
						}
					}
				}
				catch (System.Exception ex)
				{
					MessageBox.Show("Nie można wyswietlic licencji do podpisu dla tego protokolu. " + ex.Message, "Error");
				}
				if (dt.Rows.Count > 0)
				{
					this.LicencesTable.DataSource = dt;
					this.LicencesTable.Columns["lp"].DisplayIndex = 0;
					this.LicencesTable.Columns["Użytkownik"].DisplayIndex = 1;
					this.LicencesTable.Columns["Funkcja"].DisplayIndex = 2;
					DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
					btn.HeaderText = "Akcje";
					btn.Text = "przejdź";
					btn.Name = "action";
					btn.UseColumnTextForButtonValue = true;
					if (this.LicencesTable.Columns["action"] == null)
					{
						this.LicencesTable.Columns.Insert(3, btn);
					}
					else
					{
						this.LicencesTable.Columns["action"].DisplayIndex = 3;
					}
					this.LicencesTable.Columns["Licencja"].DisplayIndex = 4;
					this.LicencesTable.Columns["Licencja"].Visible = false;
				}
			}
			this.LicencesTable.Visible = true;
			this.LicencesTable.Location = new System.Drawing.Point(x, y);
			this.LicencesTable.MaximumSize = new System.Drawing.Size(this.signPanel.Size.Width - 20, 0);
			this.LicencesTable.Size = new System.Drawing.Size(0, this.LicencesTable.Rows.Count * 20);
			this.LicencesTable.AutoSize = true;
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
			this.signPanel = new Panel();
			this.LicencesTable = new DataGridView();
			this.preview = new Panel();
			this.webPreview = new WebBrowser();
			this.signPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.LicencesTable).BeginInit();
			this.preview.SuspendLayout();
			base.SuspendLayout();
			this.signPanel.AutoScroll = true;
			this.signPanel.AutoSize = true;
			this.signPanel.Controls.Add(this.LicencesTable);
			this.signPanel.Location = new System.Drawing.Point(9, 369);
			this.signPanel.Name = "signPanel";
			this.signPanel.Size = new System.Drawing.Size(757, 108);
			this.signPanel.TabIndex = 9;
			this.LicencesTable.AllowUserToAddRows = false;
			this.LicencesTable.AllowUserToDeleteRows = false;
			this.LicencesTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			this.LicencesTable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
			this.LicencesTable.BackgroundColor = System.Drawing.SystemColors.Control;
			this.LicencesTable.BorderStyle = BorderStyle.None;
			this.LicencesTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.LicencesTable.GridColor = System.Drawing.SystemColors.Control;
			this.LicencesTable.Location = new System.Drawing.Point(14, 31);
			this.LicencesTable.MinimumSize = new System.Drawing.Size(727, 0);
			this.LicencesTable.Name = "LicencesTable";
			this.LicencesTable.ReadOnly = true;
			this.LicencesTable.Size = new System.Drawing.Size(727, 63);
			this.LicencesTable.TabIndex = 2;
			this.preview.AutoSize = true;
			this.preview.Controls.Add(this.webPreview);
			this.preview.Location = new System.Drawing.Point(10, 2);
			this.preview.MinimumSize = new System.Drawing.Size(0, 100);
			this.preview.Name = "preview";
			this.preview.Size = new System.Drawing.Size(757, 361);
			this.preview.TabIndex = 10;
			this.webPreview.Dock = DockStyle.Fill;
			this.webPreview.Location = new System.Drawing.Point(0, 0);
			this.webPreview.MinimumSize = new System.Drawing.Size(20, 20);
			this.webPreview.Name = "webPreview";
			this.webPreview.Size = new System.Drawing.Size(757, 361);
			this.webPreview.TabIndex = 0;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(776, 489);
			base.Controls.Add(this.preview);
			base.Controls.Add(this.signPanel);
			base.Name = "SendProtocol";
			this.Text = "SendProtocol";
			this.signPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.LicencesTable).EndInit();
			this.preview.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
