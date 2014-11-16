using Ionic.Zip;
using Kalkulator1.AdditionalClass;
using Kalkulator1.AdditionalWindow;
using Kalkulator1.instalClass;
using Kalkulator1.ResponseClass;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
namespace Kalkulator1
{
	public class ProtocolsList : Form
	{
		private Connection con;
		private XmlDocument header;
		private string electoralEampaignSave;
		private string electoralEampaignURL;
		private string jns;
		private string role;
		private string path;
		private string circuit;
		private string licensepath;
		private System.DateTime electionData;
		public WaitPanel wait;
		private Start start;
		private WebBrowser web;
		private string version;
		public bool send;
		private bool powiat;
		private string test;
		private System.ComponentModel.IContainer components = null;
		private Button attendance;
		private Panel protocolsPanel;
		private Panel panel1;
		private Button button1;
		private Button getKlkFromDisc;
		private Button getProtocolsFromNet;
		private Label label1;
		private DataGridView protocolsTable;
		private Panel panWyszukiwanie;
		private Label lblWyszukaj;
		private TextBox txtWyszukaj;
		public ProtocolsList()
		{
			this.InitializeComponent();
			this.path = System.IO.Path.GetTempPath() + "KBW";
			this.con = new Connection();
			this.header = new XmlDocument();
			this.electoralEampaignSave = "";
			this.jns = "";
			this.role = "";
			this.circuit = "";
			this.wait = new WaitPanel("Wait2", base.Width, base.Height);
			base.Controls.Add(this.wait.getPanel());
			base.Controls[this.wait.getName()].BringToFront();
			this.start = null;
			this.licensepath = "";
			this.electionData = new System.DateTime(2014, 8, 27, 7, 0, 0);
			this.send = false;
			this.powiat = false;
		}
		public ProtocolsList(string filepath, Start start, string licensepath, string version)
		{
			this.InitializeComponent();
			this.start = start;
			start.wait.setWaitPanel("Trwa wczytywanie protokołów", "Proszę czekać");
			start.wait.setVisible(true);
			this.version = version;
			this.licensepath = licensepath;
			this.Text = this.Text + " (" + Kalkulator1.instalClass.Version.getVersion().ToString() + ")";
			this.path = System.IO.Path.GetTempPath() + "KBW";
			this.electionData = new System.DateTime(1, 1, 1, 0, 0, 0);
			DataTable dt = new DataTable();
			this.con = new Connection();
			this.header = new XmlDocument();
			System.Security.Cryptography.X509Certificates.X509Certificate cert = new System.Security.Cryptography.X509Certificates.X509Certificate(filepath);
			string subjectCert = cert.Subject;
			string[] tab = subjectCert.Split(new string[]
			{
				", "
			}, System.StringSplitOptions.None);
			for (int i = 0; i < tab.Length; i++)
			{
				if (System.Text.RegularExpressions.Regex.IsMatch(tab[i], "^OU="))
				{
					string[] tmp = tab[i].Replace("OU=", "").Split(new char[]
					{
						'-'
					});
					this.electoralEampaignURL = tmp[0];
					this.electoralEampaignSave = tmp[0].Replace('/', '_');
					this.electoralEampaignSave = this.electoralEampaignSave.Replace(" ", "");
					this.jns = tmp[1];
					this.role = tmp[2];
					if (this.role == "P" || this.role == "Z")
					{
						this.circuit = tmp[3];
					}
					if (this.role == "O" || this.role == "A")
					{
						this.panWyszukiwanie.Visible = true;
						this.txtWyszukaj.Focus();
					}
					break;
				}
			}
			this.powiat = false;
			this.wait = new WaitPanel("Wait2", base.Width, base.Height);
			base.Controls.Add(this.wait.getPanel());
			base.Controls[this.wait.getName()].BringToFront();
			this.protocolsTable.CellClick += new DataGridViewCellEventHandler(this.getProtocol_CellClick);
			if (!System.IO.Directory.Exists(this.path + "\\electoralEampaign"))
			{
				try
				{
					System.IO.Directory.CreateDirectory(this.path + "\\electoralEampaign");
				}
				catch (System.ArgumentNullException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
				}
				catch (System.ArgumentException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
				}
				catch (System.UnauthorizedAccessException)
				{
					MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
				}
				catch (System.IO.PathTooLongException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
				}
				catch (System.IO.DirectoryNotFoundException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
				}
				catch (System.NotSupportedException)
				{
					MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
				}
				catch (System.IO.IOException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
				}
			}
			if (!System.IO.Directory.Exists(this.path + "\\ProtocolsDef"))
			{
				try
				{
					System.IO.Directory.CreateDirectory(this.path + "\\ProtocolsDef");
				}
				catch (System.ArgumentNullException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
				}
				catch (System.ArgumentException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
				}
				catch (System.UnauthorizedAccessException)
				{
					MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
				}
				catch (System.IO.PathTooLongException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
				}
				catch (System.IO.DirectoryNotFoundException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
				}
				catch (System.NotSupportedException)
				{
					MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
				}
				catch (System.IO.IOException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
				}
			}
			if (!System.IO.Directory.Exists(this.path + "\\saves"))
			{
				try
				{
					System.IO.Directory.CreateDirectory(this.path + "\\saves");
				}
				catch (System.ArgumentNullException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"saves\"", "Error");
				}
				catch (System.ArgumentException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"saves\"", "Error");
				}
				catch (System.UnauthorizedAccessException)
				{
					MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako administrator.", "Uwaga");
				}
				catch (System.IO.PathTooLongException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"saves\"", "Error");
				}
				catch (System.IO.DirectoryNotFoundException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"saves\"", "Error");
				}
				catch (System.NotSupportedException)
				{
					MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"saves\"", "Error");
				}
				catch (System.IO.IOException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"saves\"", "Error");
				}
			}
			this.getKLK(true);
			this.getProtocols(true);
			start.wait.setVisible(false);
		}
		public ProtocolsList(string filepath, Start start, string licensepath, string version, string jnsNew, bool powiat)
		{
			this.InitializeComponent();
			this.start = start;
			start.wait.setWaitPanel("Trwa wczytywanie protokołów", "Proszę czekać");
			start.wait.setVisible(true);
			this.version = version;
			this.licensepath = licensepath;
			this.powiat = powiat;
			this.Text = this.Text + " (" + Kalkulator1.instalClass.Version.getVersion().ToString() + ")";
			this.path = System.IO.Path.GetTempPath() + "KBW";
			this.electionData = new System.DateTime(1, 1, 1, 0, 0, 0);
			DataTable dt = new DataTable();
			this.con = new Connection();
			this.header = new XmlDocument();
			System.Security.Cryptography.X509Certificates.X509Certificate cert = new System.Security.Cryptography.X509Certificates.X509Certificate(filepath);
			string subjectCert = cert.Subject;
			string[] tab = subjectCert.Split(new string[]
			{
				", "
			}, System.StringSplitOptions.None);
			for (int i = 0; i < tab.Length; i++)
			{
				if (System.Text.RegularExpressions.Regex.IsMatch(tab[i], "^OU="))
				{
					string[] tmp = tab[i].Replace("OU=", "").Split(new char[]
					{
						'-'
					});
					this.electoralEampaignURL = tmp[0];
					this.electoralEampaignSave = tmp[0].Replace('/', '_');
					this.electoralEampaignSave = this.electoralEampaignSave.Replace(" ", "");
					this.role = tmp[2];
					if (this.role == "P" || this.role == "Z")
					{
						this.circuit = tmp[3];
					}
					if (this.role == "O" || this.role == "A")
					{
						this.panWyszukiwanie.Visible = true;
						this.txtWyszukaj.Focus();
					}
					break;
				}
			}
			this.jns = jnsNew;
			this.wait = new WaitPanel("Wait2", base.Width, base.Height);
			base.Controls.Add(this.wait.getPanel());
			base.Controls[this.wait.getName()].BringToFront();
			this.protocolsTable.CellClick += new DataGridViewCellEventHandler(this.getProtocol_CellClick);
			this.start = start;
			this.web = new WebBrowser();
			this.web.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(ProtocolsList.webBrowser_DocumentCompleted);
			if (!System.IO.Directory.Exists(this.path + "\\electoralEampaign"))
			{
				try
				{
					System.IO.Directory.CreateDirectory(this.path + "\\electoralEampaign");
				}
				catch (System.ArgumentNullException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
				}
				catch (System.ArgumentException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
				}
				catch (System.UnauthorizedAccessException)
				{
					MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
				}
				catch (System.IO.PathTooLongException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
				}
				catch (System.IO.DirectoryNotFoundException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
				}
				catch (System.NotSupportedException)
				{
					MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
				}
				catch (System.IO.IOException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
				}
			}
			if (!System.IO.Directory.Exists(this.path + "\\ProtocolsDef"))
			{
				try
				{
					System.IO.Directory.CreateDirectory(this.path + "\\ProtocolsDef");
				}
				catch (System.ArgumentNullException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
				}
				catch (System.ArgumentException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
				}
				catch (System.UnauthorizedAccessException)
				{
					MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
				}
				catch (System.IO.PathTooLongException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
				}
				catch (System.IO.DirectoryNotFoundException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
				}
				catch (System.NotSupportedException)
				{
					MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
				}
				catch (System.IO.IOException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
				}
			}
			if (!System.IO.Directory.Exists(this.path + "\\saves"))
			{
				try
				{
					System.IO.Directory.CreateDirectory(this.path + "\\saves");
				}
				catch (System.ArgumentNullException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"saves\"", "Error");
				}
				catch (System.ArgumentException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"saves\"", "Error");
				}
				catch (System.UnauthorizedAccessException)
				{
					MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
				}
				catch (System.IO.PathTooLongException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"saves\"", "Error");
				}
				catch (System.IO.DirectoryNotFoundException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"saves\"", "Error");
				}
				catch (System.NotSupportedException)
				{
					MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"saves\"", "Error");
				}
				catch (System.IO.IOException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"saves\"", "Error");
				}
			}
			this.getKLK(true);
			this.getProtocols(true);
			start.wait.setVisible(false);
		}
		private void getProtocolsFromNet_Click(object sender, System.EventArgs e)
		{
			this.getKLK(false);
			this.refreshListOfProtocols();
		}
		public void refreshListOfProtocols()
		{
			this.wait.setWaitPanel("Trwa odświeżanie listy protokołów", "Proszę czekać");
			this.wait.setVisible(true);
			DataTable dt = new DataTable();
			this.protocolsTable.DataSource = dt;
			this.protocolsTable.Refresh();
			this.getProtocols(false);
			this.wait.setWaitPanel("Trwa odświeżanie listy protokołów", "Proszę czekać");
			this.wait.setVisible(true);
			this.protocolsTable.Refresh();
			this.wait.setVisible(false);
		}
		private void getProtocol_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.ColumnIndex == this.protocolsTable.Columns["fill"].Index)
				{
					if (e.RowIndex >= 0)
					{
						try
						{
							int result = System.DateTime.Compare(this.electionData, System.DateTime.Now);
							result = -1;
							if (result <= 0)
							{
								try
								{
									object st = this.protocolsTable.Rows[e.RowIndex].Cells["Status"].Value;
									if (st != null && st.ToString().ToLower() != "podpisany" && st.ToString().ToLower() != "wysłany")
									{
										object protokol = this.protocolsTable.Rows[e.RowIndex].Cells["Protokol"].Value;
										object jns = this.protocolsTable.Rows[e.RowIndex].Cells["Gmina"].Value;
										object obw = this.protocolsTable.Rows[e.RowIndex].Cells["Obwód"].Value;
										object inst = this.protocolsTable.Rows[e.RowIndex].Cells["Instytucja"].Value;
										object okr = this.protocolsTable.Rows[e.RowIndex].Cells["Okręg"].Value;
										if (jns != null && obw != null && inst != null && okr != null && protokol != null)
										{
											try
											{
												string save = "";
												string[] protocolpart = protokol.ToString().Split(new char[]
												{
													'-'
												});
												string instJNS = protocolpart[4];
												string[] okr2 = okr.ToString().Split(new char[]
												{
													' '
												});
												save = this.path + "\\saves\\" + protokol.ToString();
												string candidates = string.Concat(new string[]
												{
													this.path,
													"\\ProtocolsDef\\",
													this.electoralEampaignSave,
													"-",
													inst.ToString(),
													"-",
													instJNS,
													"-",
													okr2[0],
													".xml"
												});
												string protocolDefinition = string.Concat(new string[]
												{
													this.path,
													"\\ProtocolsDef\\",
													this.electoralEampaignSave,
													"-",
													inst.ToString(),
													".xml"
												});
												string committee = string.Concat(new string[]
												{
													this.path,
													"\\ProtocolsDef\\",
													this.electoralEampaignSave,
													"-",
													this.jns,
													"-",
													obw.ToString(),
													".xml"
												});
												string validateDefinition = string.Concat(new string[]
												{
													this.path,
													"\\ProtocolsDef\\",
													this.electoralEampaignSave,
													"-",
													inst.ToString(),
													"_Walidacja.xml"
												});
												string OU = "";
												if (this.role == "P" || this.role == "Z")
												{
													OU = string.Concat(new object[]
													{
														this.electoralEampaignURL.Trim(new char[]
														{
															' '
														}),
														"-",
														jns,
														"-",
														this.role,
														"-",
														this.circuit
													});
												}
												if (this.role == "O" || this.role == "A")
												{
													OU = string.Concat(new object[]
													{
														this.electoralEampaignURL.Trim(new char[]
														{
															' '
														}),
														"-",
														jns,
														"-",
														this.role
													});
												}
												ProtocolForm pf = new ProtocolForm(this, this.header, protocolDefinition, candidates, committee, validateDefinition, save, OU, this.licensepath, this.version);
												try
												{
													pf.ShowDialog();
												}
												catch (System.Exception ex)
												{
													this.createProtocols();
												}
												this.refreshListOfProtocols();
											}
											catch (XmlException)
											{
												MessageBox.Show("Nieprawidłowy XML", "Error");
											}
										}
									}
								}
								catch (System.Exception ex)
								{
									MessageBox.Show(ex.Message, "Error");
								}
							}
							else
							{
								MessageBox.Show("Protokoły wypełniać można od " + this.electionData.ToShortDateString(), "Komunikat");
							}
						}
						catch (System.Exception ex)
						{
							MessageBox.Show("Fill: " + ex.Message, "Error");
						}
					}
				}
				if (e.ColumnIndex == this.protocolsTable.Columns["new"].Index)
				{
					try
					{
						if (e.RowIndex >= 0)
						{
							object protokol = this.protocolsTable.Rows[e.RowIndex].Cells["Protokol"].Value;
							object jns = this.protocolsTable.Rows[e.RowIndex].Cells["Gmina"].Value;
							object obw = this.protocolsTable.Rows[e.RowIndex].Cells["Obwód"].Value;
							object inst = this.protocolsTable.Rows[e.RowIndex].Cells["Instytucja"].Value;
							object okr = this.protocolsTable.Rows[e.RowIndex].Cells["Okręg"].Value;
							object status = this.protocolsTable.Rows[e.RowIndex].Cells["Status"].Value;
							if (jns != null && obw != null && inst != null && okr != null)
							{
								try
								{
									if (protokol != null && status != null && (status.ToString().ToLower() == "podpisany" || status.ToString().ToLower() == "wysłany"))
									{
										DialogResult dr = MessageBox.Show("Utworzenie nowego protokołu spowoduje nadpisanie wcześniej zachowanych danych. Czy kontynuować?", "Nowy protokół", MessageBoxButtons.YesNo);
										DialogResult dialogResult = dr;
										if (dialogResult == DialogResult.No)
										{
											return;
										}
										string protokolname = protokol.ToString();
										string[] partname = protokol.ToString().Split(new char[]
										{
											' '
										});
										if (partname.Length >= 2)
										{
											protokolname = partname[0];
										}
										int num = System.IO.Directory.GetFiles(this.path + "\\saves", protokolname + "*.xml").Length;
										string namefile = protokolname.Replace(".xml", "");
										namefile = namefile + " " + (num + 1).ToString();
										System.IO.File.Move(this.path + "\\saves\\" + protokol.ToString(), this.path + "\\saves\\" + namefile + ".xml");
									}
									System.IO.StreamWriter sw = new System.IO.StreamWriter(this.path + "\\saves\\" + protokol.ToString(), false);
									sw.Write("<?xml version=\"1.0\"?><save><status>niewypełniony</status></save>");
									sw.Close();
									this.getProtocols(false);
									if (jns != null && obw != null && inst != null && okr != null)
									{
										try
										{
											string save = "";
											string[] protocolpart = protokol.ToString().Split(new char[]
											{
												'-'
											});
											string instJNS = protocolpart[4];
											string[] okr2 = okr.ToString().Split(new char[]
											{
												' '
											});
											save = this.path + "\\saves\\" + protokol.ToString();
											string candidates = string.Concat(new string[]
											{
												this.path,
												"\\ProtocolsDef\\",
												this.electoralEampaignSave,
												"-",
												inst.ToString(),
												"-",
												instJNS,
												"-",
												okr2[0],
												".xml"
											});
											string protocolDefinition = string.Concat(new string[]
											{
												this.path,
												"\\ProtocolsDef\\",
												this.electoralEampaignSave,
												"-",
												inst.ToString(),
												".xml"
											});
											string committee = string.Concat(new string[]
											{
												this.path,
												"\\ProtocolsDef\\",
												this.electoralEampaignSave,
												"-",
												this.jns,
												"-",
												obw.ToString(),
												".xml"
											});
											string validateDefinition = string.Concat(new string[]
											{
												this.path,
												"\\ProtocolsDef\\",
												this.electoralEampaignSave,
												"-",
												inst.ToString(),
												"_Walidacja.xml"
											});
											string OU = "";
											if (this.role == "P" || this.role == "Z")
											{
												OU = string.Concat(new object[]
												{
													this.electoralEampaignURL.Trim(new char[]
													{
														' '
													}),
													"-",
													jns,
													"-",
													this.role,
													"-",
													this.circuit
												});
											}
											if (this.role == "O")
											{
												OU = string.Concat(new object[]
												{
													this.electoralEampaignURL.Trim(new char[]
													{
														' '
													}),
													"-",
													jns,
													"-",
													this.role
												});
											}
											ProtocolForm pf = new ProtocolForm(this, this.header, protocolDefinition, candidates, committee, validateDefinition, save, OU, this.licensepath, this.version);
											try
											{
												pf.ShowDialog();
											}
											catch (System.Exception)
											{
												this.createProtocols();
											}
											this.refreshListOfProtocols();
										}
										catch (XmlException)
										{
											MessageBox.Show("Nieprawidłowy XML", "Error");
										}
									}
								}
								catch (XmlException)
								{
									MessageBox.Show("Nieprawidłowy XML", "Error");
								}
							}
						}
					}
					catch (System.Exception ex)
					{
						MessageBox.Show(ex.Message, "Error");
					}
				}
				if (e.ColumnIndex == this.protocolsTable.Columns["print"].Index | e.ColumnIndex == this.protocolsTable.Columns["pdf"].Index)
				{
					try
					{
						if (e.RowIndex >= 0)
						{
							object protokol = this.protocolsTable.Rows[e.RowIndex].Cells["Protokol"].Value;
							object jns = this.protocolsTable.Rows[e.RowIndex].Cells["Gmina"].Value;
							object obw = this.protocolsTable.Rows[e.RowIndex].Cells["Obwód"].Value;
							object inst = this.protocolsTable.Rows[e.RowIndex].Cells["Instytucja"].Value;
							object okr = this.protocolsTable.Rows[e.RowIndex].Cells["Okręg"].Value;
							if (jns != null && obw != null && inst != null && okr != null && protokol != null)
							{
								this.wait.setWaitPanel("Trwa przygotowanie do druku", "Druk");
								if (e.ColumnIndex == this.protocolsTable.Columns["pdf"].Index)
								{
									this.wait.setWaitPanel("Trwa generowanie PDF", "PDF");
								}
								this.wait.setVisible(true);
								try
								{
									string save = "";
									string[] protocolpart = protokol.ToString().Split(new char[]
									{
										'-'
									});
									string instJNS = protocolpart[4];
									string[] okr2 = okr.ToString().Split(new char[]
									{
										' '
									});
									save = this.path + "\\saves\\" + protokol.ToString();
									string candidates = string.Concat(new string[]
									{
										this.path,
										"\\ProtocolsDef\\",
										this.electoralEampaignSave,
										"-",
										inst.ToString(),
										"-",
										instJNS,
										"-",
										okr2[0],
										".xml"
									});
									string protocolDefinition = string.Concat(new string[]
									{
										this.path,
										"\\ProtocolsDef\\",
										this.electoralEampaignSave,
										"-",
										inst.ToString(),
										".xml"
									});
									string docDefinition = protocolDefinition.Replace(".xml", ".docx");
									XmlDocument protocolDefinition2 = new XmlDocument();
									XmlDocument save2 = new XmlDocument();
									XmlDocument candidates2 = new XmlDocument();
									protocolDefinition2.Load(protocolDefinition);
									save2.Load(save);
									candidates2.Load(candidates);
									string controlSum = "";
									XmlNode saveStep = save2.SelectSingleNode("/save/step");
									if (saveStep != null && saveStep.InnerText == "0")
									{
										string docXml = "";
										XmlNode header = save2.SelectSingleNode("/save/header");
										if (header != null)
										{
											docXml += header.OuterXml;
										}
										XmlNode step = save2.SelectSingleNode("/save/step");
										if (step != null)
										{
											docXml += step.OuterXml;
										}
										XmlNode form = save2.SelectSingleNode("/save/form");
										if (form != null)
										{
											docXml += form.OuterXml;
										}
										XmlNode komisja_sklad = save2.SelectSingleNode("/save/komisja_sklad");
										if (komisja_sklad != null)
										{
											docXml += komisja_sklad.OuterXml;
										}
										XmlNode hardWarningCode = save2.SelectSingleNode("/save/hardWarningCode");
										if (hardWarningCode != null)
										{
											docXml += hardWarningCode.OuterXml;
										}
										XmlNode softError = save2.SelectSingleNode("/save/softError");
										if (softError != null)
										{
											docXml += softError.OuterXml;
										}
										XmlNode hardError = save2.SelectSingleNode("/save/hardError");
										if (hardError != null)
										{
											docXml += hardError.OuterXml;
										}
										XmlNode hardWarning = save2.SelectSingleNode("/save/hardWarning");
										if (hardWarning != null)
										{
											docXml += hardWarning.OuterXml;
										}
										ClassMd5 i = new ClassMd5();
										controlSum = i.CreateMD5Hash(docXml);
									}
									printProtocolNew p = new printProtocolNew();
									if (e.ColumnIndex == this.protocolsTable.Columns["print"].Index)
									{
										p.ProtocolPrint(this.header, save2, candidates2, docDefinition, controlSum, false, obw.ToString(), inst.ToString(), okr.ToString(), candidates, instJNS);
									}
									else
									{
										p.ProtocolPrint(this.header, save2, candidates2, docDefinition, controlSum, true, obw.ToString(), inst.ToString(), okr.ToString(), candidates, instJNS);
									}
								}
								catch (XmlException)
								{
									MessageBox.Show("Nieprawidłowy XML", "Error");
								}
							}
							this.wait.setVisible(false);
						}
					}
					catch (System.Exception ex)
					{
						MessageBox.Show("Print: " + ex.Message, "Error");
					}
				}
				if (e.ColumnIndex == this.protocolsTable.Columns["send"].Index)
				{
					try
					{
						if (e.RowIndex >= 0)
						{
							object st = this.protocolsTable.Rows[e.RowIndex].Cells["Status"].Value;
							if (st != null && (st.ToString().ToLower() == "podpisany" || st.ToString().ToLower() == "wysłany"))
							{
								object protokol = this.protocolsTable.Rows[e.RowIndex].Cells["Protokol"].Value;
								object jns = this.protocolsTable.Rows[e.RowIndex].Cells["Gmina"].Value;
								object obw = this.protocolsTable.Rows[e.RowIndex].Cells["Obwód"].Value;
								object inst = this.protocolsTable.Rows[e.RowIndex].Cells["Instytucja"].Value;
								object okr = this.protocolsTable.Rows[e.RowIndex].Cells["Okręg"].Value;
								if (jns != null && obw != null && inst != null && okr != null && protokol != null)
								{
									try
									{
										string save = "";
										string[] okr2 = okr.ToString().Split(new char[]
										{
											' '
										});
										save = this.path + "\\saves\\" + protokol.ToString();
										System.IO.StreamReader sr = new System.IO.StreamReader(save);
										string fileXML = sr.ReadToEnd();
										sr.Close();
										fileXML = fileXML.Replace("<status>wysłany</status>", "<status>podpisany</status>");
										if (this.con.IsAvailableNetworkActive())
										{
											Eksport ex2 = new Eksport(fileXML, true, this);
											try
											{
												ex2.ShowDialog();
											}
											catch (System.Exception)
											{
											}
											if (this.send)
											{
												this.send = false;
												fileXML = fileXML.Replace("<status>podpisany</status>", "<status>wysłany</status>");
												System.IO.StreamWriter sw = new System.IO.StreamWriter(save);
												sw.Write(fileXML);
												sw.Close();
												this.refreshListOfProtocols();
											}
										}
										else
										{
											MessageBox.Show("Protokół nie został wysłany z powodu braku Internetu.");
										}
									}
									catch (XmlException)
									{
										MessageBox.Show("Nieprawidłowy XML", "Error");
									}
									catch (System.NullReferenceException)
									{
										MessageBox.Show("Podanno inny xml niz header", "Error");
									}
								}
							}
							if (st != null && (st.ToString().ToLower() == "roboczy" || (st != null && st.ToString().ToLower() == "niewypełniony")))
							{
								MessageBox.Show("Nie można wysłać protokołu o statusie: \"" + st.ToString().ToLower() + "\"", "Komunikat");
							}
						}
					}
					catch (System.Exception ex)
					{
						MessageBox.Show(ex.Message, "Error");
					}
				}
				if (e.ColumnIndex == this.protocolsTable.Columns["save1"].Index)
				{
					if (e.RowIndex >= 0)
					{
						object protokol = this.protocolsTable.Rows[e.RowIndex].Cells["Protokol"].Value;
						string savePath = this.path + "\\saves\\" + protokol.ToString();
						System.IO.StreamReader sr = new System.IO.StreamReader(savePath);
						string file = sr.ReadToEnd();
						sr.Close();
						string[] nameFile = savePath.Split(new char[]
						{
							'\\'
						});
						SaveFileDialog wnd = new SaveFileDialog();
						wnd.Filter = "(*.xml)|*.xml";
						wnd.FileName = nameFile[nameFile.Length - 1];
						if (wnd.ShowDialog() != DialogResult.Cancel)
						{
							this.wait.setWaitPanel("Trwa eksportowanie protokołu", "Proszę czekać");
							this.wait.setVisible(true);
							if (wnd.CheckPathExists)
							{
								string name = wnd.FileName;
								try
								{
									if (name != null && name != "" && name != nameFile[nameFile.Length - 1])
									{
										System.IO.StreamWriter sw = new System.IO.StreamWriter(name, false);
										sw.Write(file);
										sw.Close();
									}
									this.wait.setVisible(false);
								}
								catch (System.UnauthorizedAccessException)
								{
									MessageBox.Show("Nie jestes uprawniony do zapisania pliku we wskazanym miejscu", "Uwaga");
								}
							}
						}
					}
				}
			}
			catch (System.ArgumentOutOfRangeException ex_16E9)
			{
			}
			catch (System.Exception ex)
			{
			}
		}
		public static void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			WebBrowser wb = (WebBrowser)sender;
			if (wb.ReadyState.Equals(WebBrowserReadyState.Complete))
			{
				try
				{
					string keyName = "Software\\Microsoft\\Internet Explorer\\PageSetup";
					using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(keyName, true))
					{
						if (key != null)
						{
							object old_footer = key.GetValue("footer");
							object old_header = key.GetValue("header");
							key.SetValue("footer", "");
							key.SetValue("header", "");
							key.SetValue("margin_top", "0.75");
							key.SetValue("margin_right", "0.75");
							key.SetValue("margin_bottom", "0.75");
							key.SetValue("margin_left", "0.75");
						}
					}
				}
				catch (System.Exception ex)
				{
					MessageBox.Show("Usuniecie standarodwej stopki i nagłówka nie powiodło się. " + ex.Message, "Uwaga");
				}
				wb.ShowPrintDialog();
			}
		}
		private void getProtocols(bool old)
		{
			this.wait.setWaitPanel("Trwa wczytywanie protokołów", "Proszę czekać");
			this.wait.setVisible(true);
			if (old)
			{
				this.start.wait.setWaitPanel("Trwa wczytywanie protokołów", "Proszę czekać");
				this.start.wait.setVisible(true);
			}
			if (System.IO.Directory.Exists(this.path + "\\ProtocolsDef"))
			{
				DataTable dt = new DataTable();
				this.protocolsTable.DataSource = dt;
				dt.Columns.Add(new DataColumn("Gmina", typeof(string)));
				dt.Columns.Add(new DataColumn("Obwód", typeof(int)));
				dt.Columns.Add(new DataColumn("Okręg", typeof(string)));
				dt.Columns.Add(new DataColumn("Instytucja", typeof(string)));
				dt.Columns.Add(new DataColumn("JNS instytucji", typeof(string)));
				dt.Columns.Add(new DataColumn("Adres Obwodu", typeof(string)));
				dt.Columns.Add(new DataColumn("Status", typeof(string)));
				dt.Columns.Add(new DataColumn("Ostatnia aktualizacja", typeof(string)));
				dt.Columns.Add(new DataColumn("Protokol", typeof(string)));
				string obwTmp = "";
				if (this.role == "P" || this.role == "Z")
				{
					obwTmp = "-" + this.circuit.ToString();
				}
				try
				{
					if (System.IO.Directory.Exists(this.path + "\\saves"))
					{
						System.Collections.Generic.List<string> unauthorizedInstitutions = new System.Collections.Generic.List<string>();
						if (this.role == "A")
						{
							string jnsTMP = this.jns;
							if (jnsTMP.Length < 6)
							{
								while (jnsTMP.Length < 6)
								{
									jnsTMP = "0" + jnsTMP;
								}
							}
							if (this.powiat)
							{
								if (jnsTMP.Substring(0, 4) == "1465")
								{
									unauthorizedInstitutions.Add("RDP");
								}
								else
								{
									unauthorizedInstitutions.Add("RDA");
									unauthorizedInstitutions.Add("WBP");
								}
							}
							else
							{
								if (jnsTMP[2] == '7' || jnsTMP[2] == '6')
								{
									if (jnsTMP.Substring(0, 4) == "1465")
									{
										unauthorizedInstitutions.Add("RDP");
										unauthorizedInstitutions.Add("RDW");
										unauthorizedInstitutions.Add("WBP");
									}
									else
									{
										unauthorizedInstitutions.Add("RDP");
									}
								}
								else
								{
									unauthorizedInstitutions.Add("RDP");
									unauthorizedInstitutions.Add("RDW");
								}
							}
						}
						foreach (string file in System.IO.Directory.EnumerateFiles(this.path + "\\saves", string.Concat(new string[]
						{
							this.electoralEampaignSave,
							"-",
							this.jns,
							obwTmp,
							"-*.xml"
						})))
						{
							bool add = true;
							string[] filename = file.Split(new char[]
							{
								'\\'
							});
							DataRow dr = dt.NewRow();
							string[] filenamePart = filename[filename.Length - 1].Replace(".xml", "").Split(new char[]
							{
								'-'
							});
							dr[0] = filenamePart[1];
							dr[1] = filenamePart[2];
							dr[2] = filenamePart[5];
							dr[4] = filenamePart[4];
							if (this.role == "A")
							{
								for (int i = 0; i < unauthorizedInstitutions.Count; i++)
								{
									if (filenamePart[3] == unauthorizedInstitutions[i])
									{
										add = false;
										break;
									}
								}
							}
							if (add)
							{
								string[] okr = filenamePart[5].Split(new char[]
								{
									' '
								});
								if (okr.Length > 2)
								{
									string okrName = okr[0] + " v." + okr[1];
									for (int i = 2; i < okr.Length; i++)
									{
										okrName = okrName + " " + okr[i];
									}
									dr[2] = okrName;
								}
								if (okr.Length == 2)
								{
									dr[2] = okr[0] + " v." + okr[1];
								}
								dr[3] = filenamePart[3];
								try
								{
									XmlNode jnsNode = this.header.SelectSingleNode("/akcja_wyborcza/jns");
									foreach (XmlNode obwNode in jnsNode)
									{
										if (obwNode.Name == "obw")
										{
											XmlNode nr = obwNode.Attributes.GetNamedItem("nr");
											if (nr != null && nr.Value == filenamePart[2])
											{
												XmlNode siedziba = obwNode.Attributes.GetNamedItem("siedziba");
												if (siedziba != null)
												{
													dr[5] = siedziba.Value;
												}
												else
												{
													dr[5] = "";
												}
											}
										}
									}
									XmlDocument protocolXml = new XmlDocument();
									protocolXml.Load(file);
									XmlNode nodesList = protocolXml.SelectSingleNode("/save/status");
									if (nodesList != null)
									{
										dr[6] = nodesList.InnerText;
									}
									else
									{
										dr[6] = "";
									}
								}
								catch (XmlException)
								{
								}
								catch (System.NullReferenceException)
								{
								}
								dr[7] = "";
								try
								{
									dr[7] = System.IO.File.GetLastWriteTime(file);
								}
								catch (System.Exception)
								{
								}
								dr[8] = filename[filename.Length - 1];
								if (this.txtWyszukaj.Text != "" && this.panWyszukiwanie.Visible)
								{
									if (dr[1].ToString() == this.txtWyszukaj.Text)
									{
										dt.Rows.Add(dr);
									}
								}
								else
								{
									dt.Rows.Add(dr);
								}
							}
						}
					}
				}
				catch (System.ArgumentNullException)
				{
					MessageBox.Show("Błędna ścieżka", "Error");
				}
				catch (System.ArgumentOutOfRangeException)
				{
					MessageBox.Show("Błędna opcja szukania", "Error");
				}
				catch (System.ArgumentException e_7DB)
				{
					MessageBox.Show("Błędna ścieżka", "Error");
				}
				catch (System.IO.DirectoryNotFoundException)
				{
					MessageBox.Show("Nie znaleziono katalogu", "Error");
				}
				catch (System.IO.PathTooLongException)
				{
					MessageBox.Show("Zbyt długa ścieżka do katalogu", "Error");
				}
				catch (System.IO.IOException)
				{
					MessageBox.Show("Podano ścieżke do pliku zamiast katalogu", "Error");
				}
				catch (System.UnauthorizedAccessException)
				{
					MessageBox.Show("Program nie posiada uprawnień do usuwania plików", "Error");
				}
				if (dt.Rows.Count > 0)
				{
					this.protocolsTable.DataSource = dt;
					this.protocolsTable.Columns["Gmina"].DisplayIndex = 0;
					this.protocolsTable.Columns["Gmina"].FillWeight = 9f;
					this.protocolsTable.Columns["Obwód"].DisplayIndex = 1;
					this.protocolsTable.Columns["Obwód"].FillWeight = 7f;
					this.protocolsTable.Columns["Okręg"].DisplayIndex = 2;
					this.protocolsTable.Columns["Okręg"].FillWeight = 7f;
					this.protocolsTable.Columns["Instytucja"].DisplayIndex = 3;
					this.protocolsTable.Columns["Instytucja"].FillWeight = 10f;
					this.protocolsTable.Columns["JNS instytucji"].DisplayIndex = 4;
					this.protocolsTable.Columns["JNS instytucji"].FillWeight = 10f;
					this.protocolsTable.Columns["Adres Obwodu"].DisplayIndex = 5;
					this.protocolsTable.Columns["Adres Obwodu"].FillWeight = 40f;
					this.protocolsTable.Columns["Status"].DisplayIndex = 6;
					this.protocolsTable.Columns["Status"].FillWeight = 14f;
					this.protocolsTable.Columns["Ostatnia aktualizacja"].DisplayIndex = 7;
					this.protocolsTable.Columns["Ostatnia aktualizacja"].FillWeight = 14f;
					DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
					btn.HeaderText = "";
					btn.Text = "Drukuj";
					btn.Name = "print";
					btn.UseColumnTextForButtonValue = true;
					if (this.protocolsTable.Columns["print"] == null)
					{
						this.protocolsTable.Columns.Insert(8, btn);
						this.protocolsTable.Columns["print"].FillWeight = 8f;
					}
					else
					{
						this.protocolsTable.Columns["print"].DisplayIndex = 8;
						this.protocolsTable.Columns["print"].FillWeight = 8f;
					}
					this.protocolsPanel.Visible = true;
					DataGridViewButtonColumn btn2 = new DataGridViewButtonColumn();
					btn2.HeaderText = "";
					btn2.Text = "PDF";
					btn2.Name = "pdf";
					btn2.UseColumnTextForButtonValue = true;
					if (this.protocolsTable.Columns["pdf"] == null)
					{
						this.protocolsTable.Columns.Insert(9, btn2);
						this.protocolsTable.Columns["pdf"].FillWeight = 7f;
					}
					else
					{
						this.protocolsTable.Columns["pdf"].DisplayIndex = 9;
						this.protocolsTable.Columns["pdf"].FillWeight = 7f;
					}
					DataGridViewButtonColumn btn3 = new DataGridViewButtonColumn();
					btn3.HeaderText = "";
					btn3.Text = "Edytuj";
					btn3.Name = "fill";
					btn3.UseColumnTextForButtonValue = true;
					if (this.protocolsTable.Columns["fill"] == null)
					{
						this.protocolsTable.Columns.Insert(10, btn3);
						this.protocolsTable.Columns["fill"].FillWeight = 8f;
					}
					else
					{
						this.protocolsTable.Columns["fill"].DisplayIndex = 10;
						this.protocolsTable.Columns["fill"].FillWeight = 8f;
					}
					DataGridViewButtonColumn btn4 = new DataGridViewButtonColumn();
					btn4.HeaderText = "";
					btn4.Text = "Nowy";
					btn4.Name = "new";
					btn4.UseColumnTextForButtonValue = true;
					if (this.protocolsTable.Columns["new"] == null)
					{
						this.protocolsTable.Columns.Insert(11, btn4);
						this.protocolsTable.Columns["new"].FillWeight = 8f;
					}
					else
					{
						this.protocolsTable.Columns["new"].DisplayIndex = 11;
						this.protocolsTable.Columns["new"].FillWeight = 8f;
					}
					DataGridViewButtonColumn btn5 = new DataGridViewButtonColumn();
					btn5.HeaderText = "";
					btn5.Text = "Wyślij";
					btn5.Name = "send";
					btn5.UseColumnTextForButtonValue = true;
					if (this.protocolsTable.Columns["send"] == null)
					{
						this.protocolsTable.Columns.Insert(12, btn5);
						this.protocolsTable.Columns["send"].FillWeight = 8f;
					}
					else
					{
						this.protocolsTable.Columns["send"].DisplayIndex = 12;
						this.protocolsTable.Columns["send"].FillWeight = 8f;
					}
					DataGridViewButtonColumn btn6 = new DataGridViewButtonColumn();
					btn6.HeaderText = "";
					btn6.Text = "Zapisz";
					btn6.Name = "save1";
					btn6.UseColumnTextForButtonValue = true;
					if (this.protocolsTable.Columns["save1"] == null)
					{
						this.protocolsTable.Columns.Insert(13, btn6);
						this.protocolsTable.Columns["save1"].FillWeight = 8f;
					}
					else
					{
						this.protocolsTable.Columns["save1"].DisplayIndex = 13;
						this.protocolsTable.Columns["save1"].FillWeight = 8f;
					}
					this.protocolsTable.Columns["Protokol"].DisplayIndex = 14;
					this.protocolsTable.Columns["Protokol"].Visible = false;
				}
			}
			else
			{
				this.protocolsPanel.Visible = false;
			}
			this.wait.setVisible(false);
			if (old)
			{
				this.start.wait.setVisible(false);
			}
		}
		private void getKLK(bool old)
		{
			this.test = "";
			this.wait.setWaitPanel("Trwa przygotowanie listy protokołów", "Proszę czekać");
			this.wait.setVisible(true);
			if (old)
			{
				this.start.wait.setWaitPanel("Trwa przygotowanie listy protokołów", "Proszę czekać");
			}
			this.createProtocols();
			bool bAvailableNetworkActive = false;
			for (int i = 0; i < 10; i++)
			{
				bAvailableNetworkActive = this.con.IsAvailableNetworkActive();
				if (bAvailableNetworkActive)
				{
					break;
				}
			}
			if (bAvailableNetworkActive)
			{
				this.wait.setWaitPanel("Trwa aktualizowanie danych klk", "Proszę czekać");
				if (old)
				{
					this.start.wait.setWaitPanel("Trwa aktualizowanie danych klk", "Proszę czekać");
				}
				string server2 = "KALK/";
				string savePath = string.Concat(new string[]
				{
					this.path,
					"\\electoralEampaign\\",
					this.electoralEampaignSave,
					"-",
					this.jns,
					".xml"
				});
				string uri = string.Concat(new string[]
				{
					server2,
					"pollstations/",
					this.electoralEampaignURL,
					"-",
					this.jns
				});
				KLKresponse resHeader = this.con.getRequestKBWKlk(uri, savePath, 0);
				if (System.IO.Directory.Exists(this.path + "\\electoralEampaign"))
				{
					if (System.IO.File.Exists(string.Concat(new string[]
					{
						this.path,
						"\\electoralEampaign\\",
						this.electoralEampaignSave,
						"-",
						this.jns,
						".xml"
					})))
					{
						try
						{
							this.header.Load(string.Concat(new string[]
							{
								this.path,
								"\\electoralEampaign\\",
								this.electoralEampaignSave,
								"-",
								this.jns,
								".xml"
							}));
							int loop = 1;
							string obw = "";
							string instkod = "";
							XmlNode headerRoot = this.header.SelectSingleNode("/akcja_wyborcza/jns");
							string tmpJns = this.jns;
							if (tmpJns.Length < 6)
							{
								while (tmpJns.Length < 6)
								{
									tmpJns = "0" + tmpJns;
								}
							}
							foreach (XmlNode item in headerRoot)
							{
								if (item.Name == "obw")
								{
									XmlNode nr = item.Attributes.GetNamedItem("nr");
									if (nr != null)
									{
										obw = nr.Value;
									}
									if (obw != "" && (this.role == "O" || this.role == "A" || ((this.role == "P" || this.role == "Z") && obw == this.circuit)))
									{
										savePath = string.Concat(new string[]
										{
											this.path,
											"\\ProtocolsDef\\",
											this.electoralEampaignSave,
											"-",
											this.jns,
											"-",
											obw,
											".xml"
										});
										if (!Start.listaPlikow.Contains(savePath))
										{
											uri = string.Concat(new string[]
											{
												server2,
												"peoplecommittes/",
												this.electoralEampaignURL,
												"-",
												this.jns,
												"-",
												obw
											});
											KLKresponse res = this.con.getRequestKBWKlk(uri, savePath, 0);
											Start.listaPlikow.Add(savePath);
										}
									}
									if ((this.role == "O" || this.role == "A") && loop == 1)
									{
										this.getInstData("RDA", server2);
										this.getInstData("RDW", server2);
										this.getInstData("RDP", server2);
										this.getInstData("WBP", server2);
										for (int i = 1; i < 24; i++)
										{
											string savePathRDA = string.Concat(new object[]
											{
												this.path,
												"\\ProtocolsDef\\",
												this.electoralEampaignSave,
												"-RDA-",
												tmpJns,
												"-",
												i,
												".xml"
											});
											if (!Start.listaPlikow.Contains(savePathRDA))
											{
												uri = string.Concat(new object[]
												{
													server2,
													"candidates/",
													this.electoralEampaignURL,
													"-RDA-",
													tmpJns,
													"-",
													i
												});
												this.test = this.test + uri + "; " + '\n'.ToString();
												KLKresponse res = this.con.getRequestKBWKlk(uri, savePathRDA, 0);
												Start.listaPlikow.Add(savePathRDA);
											}
											string savePathRDW = string.Concat(new object[]
											{
												this.path,
												"\\ProtocolsDef\\",
												this.electoralEampaignSave,
												"-RDW-",
												tmpJns,
												"-",
												i,
												".xml"
											});
											if (!Start.listaPlikow.Contains(savePathRDW))
											{
												uri = string.Concat(new object[]
												{
													server2,
													"candidates/",
													this.electoralEampaignURL,
													"-RDW-",
													tmpJns,
													"-",
													i
												});
												this.test = this.test + uri + "; " + '\n'.ToString();
												KLKresponse res = this.con.getRequestKBWKlk(uri, savePathRDW, 0);
												Start.listaPlikow.Add(savePathRDW);
											}
											string jnsTMP = "";
											if (this.jns.Length == 6)
											{
												jnsTMP = this.jns.Substring(0, 2) + "0000";
											}
											if (this.jns.Length == 5)
											{
												jnsTMP = "0" + this.jns.Substring(0, 1) + "0000";
											}
											savePathRDW = string.Concat(new object[]
											{
												this.path,
												"\\ProtocolsDef\\",
												this.electoralEampaignSave,
												"-RDW-",
												jnsTMP,
												"-",
												i,
												".xml"
											});
											if (!Start.listaPlikow.Contains(savePathRDW))
											{
												uri = string.Concat(new object[]
												{
													server2,
													"candidates/",
													this.electoralEampaignURL,
													"-RDW-",
													jnsTMP,
													"-",
													i
												});
												this.test = this.test + uri + "; " + '\n'.ToString();
												KLKresponse res = this.con.getRequestKBWKlk(uri, savePathRDW, 0);
												Start.listaPlikow.Add(savePathRDW);
											}
											string savePathRDP = string.Concat(new object[]
											{
												this.path,
												"\\ProtocolsDef\\",
												this.electoralEampaignSave,
												"-RDP-",
												tmpJns,
												"-",
												i,
												".xml"
											});
											if (!Start.listaPlikow.Contains(savePathRDP))
											{
												uri = string.Concat(new object[]
												{
													server2,
													"candidates/",
													this.electoralEampaignURL,
													"-RDP-",
													tmpJns,
													"-",
													i
												});
												this.test = this.test + uri + "; " + '\n'.ToString();
												KLKresponse res = this.con.getRequestKBWKlk(uri, savePathRDP, 0);
												Start.listaPlikow.Add(savePathRDP);
											}
											jnsTMP = "";
											if (this.jns.Length == 6)
											{
												jnsTMP = this.jns.Substring(0, 4) + "00";
											}
											if (this.jns.Length == 5)
											{
												jnsTMP = "0" + this.jns.Substring(0, 3) + "00";
											}
											savePathRDP = string.Concat(new object[]
											{
												this.path,
												"\\ProtocolsDef\\",
												this.electoralEampaignSave,
												"-RDP-",
												jnsTMP,
												"-",
												i,
												".xml"
											});
											if (!Start.listaPlikow.Contains(savePathRDP))
											{
												uri = string.Concat(new object[]
												{
													server2,
													"candidates/",
													this.electoralEampaignURL,
													"-RDP-",
													jnsTMP,
													"-",
													i
												});
												this.test = this.test + uri + "; " + '\n'.ToString();
												KLKresponse res = this.con.getRequestKBWKlk(uri, savePathRDP, 0);
												Start.listaPlikow.Add(savePathRDP);
											}
											string savePathWBP = string.Concat(new object[]
											{
												this.path,
												"\\ProtocolsDef\\",
												this.electoralEampaignSave,
												"-WBP-",
												tmpJns,
												"-",
												i,
												".xml"
											});
											if (!Start.listaPlikow.Contains(savePathWBP))
											{
												uri = string.Concat(new object[]
												{
													server2,
													"candidates/",
													this.electoralEampaignURL,
													"-WBP-",
													tmpJns,
													"-",
													i
												});
												this.test = this.test + uri + "; " + '\n'.ToString();
												KLKresponse res = this.con.getRequestKBWKlk(uri, savePathWBP, 0);
												Start.listaPlikow.Add(savePathWBP);
											}
											if (this.jns.Substring(0, 4) == "1465")
											{
												savePathRDA = string.Concat(new object[]
												{
													this.path,
													"\\ProtocolsDef\\",
													this.electoralEampaignSave,
													"-RDA-146501-",
													i,
													".xml"
												});
												if (!Start.listaPlikow.Contains(savePathRDA))
												{
													uri = string.Concat(new object[]
													{
														server2,
														"candidates/",
														this.electoralEampaignURL,
														"-RDA-146501-",
														i
													});
													this.test = this.test + uri + "; " + '\n'.ToString();
													KLKresponse res = this.con.getRequestKBWKlk(uri, savePathRDA, 0);
													Start.listaPlikow.Add(savePathRDA);
												}
											}
										}
										if (this.jns.Substring(0, 4) == "1465")
										{
											string savePathWBP = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-WBP-146501-1.xml";
											if (!Start.listaPlikow.Contains(savePathWBP))
											{
												uri = server2 + "candidates/" + this.electoralEampaignURL + "-WBP-146501-1";
												this.test = this.test + uri + "; " + '\n'.ToString();
												KLKresponse res = this.con.getRequestKBWKlk(uri, savePathWBP, 0);
												Start.listaPlikow.Add(savePathWBP);
											}
										}
									}
									if (this.role == "P" || this.role == "Z")
									{
										foreach (XmlNode inst in item)
										{
											if (inst.Name == "inst")
											{
												XmlNode kod = inst.Attributes.GetNamedItem("kod");
												if (kod != null)
												{
													instkod = kod.Value;
												}
												string organNazwa = "";
												XmlNode instOrgan = inst.Attributes.GetNamedItem("organNazwa");
												if (instOrgan != null)
												{
													organNazwa = instOrgan.Value;
												}
												string inst_jns = "";
												XmlNode inst_jnsXML = inst.Attributes.GetNamedItem("inst_jns");
												if (inst_jnsXML != null)
												{
													inst_jns = inst_jnsXML.Value;
												}
												if (instkod == "RDA" && organNazwa == "m.st.")
												{
													savePath = string.Concat(new string[]
													{
														this.path,
														"\\ProtocolsDef\\",
														this.electoralEampaignSave,
														"-",
														instkod,
														"_M.xml"
													});
													if (!Start.listaPlikow.Contains(savePath))
													{
														uri = string.Concat(new string[]
														{
															server2,
															"layout/",
															this.electoralEampaignURL,
															"-",
															instkod,
															"_M"
														});
														KLKresponse res = this.con.getRequestKBWKlk(uri, savePath, 0);
														Start.listaPlikow.Add(savePath);
													}
													savePath = string.Concat(new string[]
													{
														this.path,
														"\\ProtocolsDef\\",
														this.electoralEampaignSave,
														"-",
														instkod,
														"_M.docx"
													});
													if (!Start.listaPlikow.Contains(savePath))
													{
														uri = string.Concat(new string[]
														{
															server2,
															"layout/",
															this.electoralEampaignURL,
															"-",
															instkod,
															"_M.docx"
														});
														KLKresponse res = this.con.getRequestKBWKlkDocx(uri, savePath, 0);
														Start.listaPlikow.Add(savePath);
													}
													savePath = string.Concat(new string[]
													{
														this.path,
														"\\ProtocolsDef\\",
														this.electoralEampaignSave,
														"-",
														instkod,
														"_M_EMPTY.docx"
													});
													if (!Start.listaPlikow.Contains(savePath))
													{
														uri = string.Concat(new string[]
														{
															server2,
															"layout/",
															this.electoralEampaignURL,
															"-",
															instkod,
															"_M_EMPTY.docx"
														});
														KLKresponse res = this.con.getRequestKBWKlkDocx(uri, savePath, 0);
														Start.listaPlikow.Add(savePath);
													}
													savePath = string.Concat(new string[]
													{
														this.path,
														"\\ProtocolsDef\\",
														this.electoralEampaignSave,
														"-",
														instkod,
														"_D_EMPTY.docx"
													});
													if (!Start.listaPlikow.Contains(savePath))
													{
														uri = string.Concat(new string[]
														{
															server2,
															"layout/",
															this.electoralEampaignURL,
															"-",
															instkod,
															"_D_EMPTY.docx"
														});
														KLKresponse res = this.con.getRequestKBWKlkDocx(uri, savePath, 0);
														Start.listaPlikow.Add(savePath);
													}
													savePath = string.Concat(new string[]
													{
														this.path,
														"\\ProtocolsDef\\",
														this.electoralEampaignSave,
														"-",
														instkod,
														"_M_ERR.docx"
													});
													if (!Start.listaPlikow.Contains(savePath))
													{
														uri = string.Concat(new string[]
														{
															server2,
															"layout/",
															this.electoralEampaignURL,
															"-",
															instkod,
															"_M_ERR.docx"
														});
														KLKresponse res = this.con.getRequestKBWKlkDocx(uri, savePath, 0);
														Start.listaPlikow.Add(savePath);
													}
													savePath = string.Concat(new string[]
													{
														this.path,
														"\\ProtocolsDef\\",
														this.electoralEampaignSave,
														"-",
														instkod,
														"_M_Walidacja.xml"
													});
													if (!Start.listaPlikow.Contains(savePath))
													{
														uri = string.Concat(new string[]
														{
															server2,
															"integrity/",
															this.electoralEampaignURL,
															"-",
															instkod,
															"_M"
														});
														KLKresponse res = this.con.getRequestKBWKlk(uri, savePath, 0);
														Start.listaPlikow.Add(savePath);
													}
												}
												if (instkod == "RDA" && organNazwa == "Dzielnicy")
												{
													savePath = string.Concat(new string[]
													{
														this.path,
														"\\ProtocolsDef\\",
														this.electoralEampaignSave,
														"-",
														instkod,
														"_D.xml"
													});
													if (!Start.listaPlikow.Contains(savePath))
													{
														uri = string.Concat(new string[]
														{
															server2,
															"layout/",
															this.electoralEampaignURL,
															"-",
															instkod,
															"_D"
														});
														KLKresponse res = this.con.getRequestKBWKlk(uri, savePath, 0);
														Start.listaPlikow.Add(savePath);
													}
													savePath = string.Concat(new string[]
													{
														this.path,
														"\\ProtocolsDef\\",
														this.electoralEampaignSave,
														"-",
														instkod,
														"_D.docx"
													});
													if (!Start.listaPlikow.Contains(savePath))
													{
														uri = string.Concat(new string[]
														{
															server2,
															"layout/",
															this.electoralEampaignURL,
															"-",
															instkod,
															"_D.docx"
														});
														KLKresponse res = this.con.getRequestKBWKlkDocx(uri, savePath, 0);
														Start.listaPlikow.Add(savePath);
													}
													savePath = string.Concat(new string[]
													{
														this.path,
														"\\ProtocolsDef\\",
														this.electoralEampaignSave,
														"-",
														instkod,
														"_D_EMPTY.docx"
													});
													if (!Start.listaPlikow.Contains(savePath))
													{
														uri = string.Concat(new string[]
														{
															server2,
															"layout/",
															this.electoralEampaignURL,
															"-",
															instkod,
															"_D_EMPTY.docx"
														});
														KLKresponse res = this.con.getRequestKBWKlkDocx(uri, savePath, 0);
														Start.listaPlikow.Add(savePath);
													}
													savePath = string.Concat(new string[]
													{
														this.path,
														"\\ProtocolsDef\\",
														this.electoralEampaignSave,
														"-",
														instkod,
														"_D_ERR.docx"
													});
													if (!Start.listaPlikow.Contains(savePath))
													{
														uri = string.Concat(new string[]
														{
															server2,
															"layout/",
															this.electoralEampaignURL,
															"-",
															instkod,
															"_D_ERR.docx"
														});
														KLKresponse res = this.con.getRequestKBWKlkDocx(uri, savePath, 0);
														Start.listaPlikow.Add(savePath);
													}
													savePath = string.Concat(new string[]
													{
														this.path,
														"\\ProtocolsDef\\",
														this.electoralEampaignSave,
														"-",
														instkod,
														"_D_Walidacja.xml"
													});
													if (!Start.listaPlikow.Contains(savePath))
													{
														uri = string.Concat(new string[]
														{
															server2,
															"integrity/",
															this.electoralEampaignURL,
															"-",
															instkod,
															"_D"
														});
														KLKresponse res = this.con.getRequestKBWKlk(uri, savePath, 0);
														Start.listaPlikow.Add(savePath);
													}
												}
												if (instkod != "")
												{
													savePath = string.Concat(new string[]
													{
														this.path,
														"\\ProtocolsDef\\",
														this.electoralEampaignSave,
														"-",
														instkod,
														".xml"
													});
													if (!Start.listaPlikow.Contains(savePath))
													{
														uri = string.Concat(new string[]
														{
															server2,
															"layout/",
															this.electoralEampaignURL,
															"-",
															instkod
														});
														KLKresponse res = this.con.getRequestKBWKlk(uri, savePath, 0);
														Start.listaPlikow.Add(savePath);
													}
													savePath = string.Concat(new string[]
													{
														this.path,
														"\\ProtocolsDef\\",
														this.electoralEampaignSave,
														"-",
														instkod,
														".docx"
													});
													if (!Start.listaPlikow.Contains(savePath))
													{
														uri = string.Concat(new string[]
														{
															server2,
															"layout/",
															this.electoralEampaignURL,
															"-",
															instkod,
															".docx"
														});
														KLKresponse res = this.con.getRequestKBWKlkDocx(uri, savePath, 0);
														Start.listaPlikow.Add(savePath);
													}
													savePath = string.Concat(new string[]
													{
														this.path,
														"\\ProtocolsDef\\",
														this.electoralEampaignSave,
														"-",
														instkod,
														"_EMPTY.docx"
													});
													if (!Start.listaPlikow.Contains(savePath))
													{
														uri = string.Concat(new string[]
														{
															server2,
															"layout/",
															this.electoralEampaignURL,
															"-",
															instkod,
															"_EMPTY.docx"
														});
														KLKresponse res = this.con.getRequestKBWKlkDocx(uri, savePath, 0);
														Start.listaPlikow.Add(savePath);
													}
													savePath = string.Concat(new string[]
													{
														this.path,
														"\\ProtocolsDef\\",
														this.electoralEampaignSave,
														"-",
														instkod,
														"_ERR.docx"
													});
													if (!Start.listaPlikow.Contains(savePath))
													{
														uri = string.Concat(new string[]
														{
															server2,
															"layout/",
															this.electoralEampaignURL,
															"-",
															instkod,
															"_ERR.docx"
														});
														KLKresponse res = this.con.getRequestKBWKlkDocx(uri, savePath, 0);
														Start.listaPlikow.Add(savePath);
													}
													savePath = string.Concat(new string[]
													{
														this.path,
														"\\ProtocolsDef\\",
														this.electoralEampaignSave,
														"-",
														instkod,
														"_Walidacja.xml"
													});
													if (!Start.listaPlikow.Contains(savePath))
													{
														uri = string.Concat(new string[]
														{
															server2,
															"integrity/",
															this.electoralEampaignURL,
															"-",
															instkod
														});
														KLKresponse res = this.con.getRequestKBWKlk(uri, savePath, 0);
														Start.listaPlikow.Add(savePath);
													}
													if (instkod == "WBP")
													{
														savePath = string.Concat(new string[]
														{
															this.path,
															"\\ProtocolsDef\\",
															this.electoralEampaignSave,
															"-",
															instkod,
															"_1.xml"
														});
														if (!Start.listaPlikow.Contains(savePath))
														{
															uri = string.Concat(new string[]
															{
																server2,
																"layout/",
																this.electoralEampaignURL,
																"-",
																instkod,
																"_1"
															});
															KLKresponse res = this.con.getRequestKBWKlk(uri, savePath, 0);
															Start.listaPlikow.Add(savePath);
														}
														savePath = string.Concat(new string[]
														{
															this.path,
															"\\ProtocolsDef\\",
															this.electoralEampaignSave,
															"-",
															instkod,
															"_1.docx"
														});
														if (!Start.listaPlikow.Contains(savePath))
														{
															uri = string.Concat(new string[]
															{
																server2,
																"layout/",
																this.electoralEampaignURL,
																"-",
																instkod,
																"_1.docx"
															});
															KLKresponse res = this.con.getRequestKBWKlkDocx(uri, savePath, 0);
															Start.listaPlikow.Add(savePath);
														}
														savePath = string.Concat(new string[]
														{
															this.path,
															"\\ProtocolsDef\\",
															this.electoralEampaignSave,
															"-",
															instkod,
															"_1_EMPTY.docx"
														});
														if (!Start.listaPlikow.Contains(savePath))
														{
															uri = string.Concat(new string[]
															{
																server2,
																"layout/",
																this.electoralEampaignURL,
																"-",
																instkod,
																"_1_EMPTY.docx"
															});
															KLKresponse res = this.con.getRequestKBWKlkDocx(uri, savePath, 0);
															Start.listaPlikow.Add(savePath);
														}
														savePath = string.Concat(new string[]
														{
															this.path,
															"\\ProtocolsDef\\",
															this.electoralEampaignSave,
															"-",
															instkod,
															"_1_ERR.docx"
														});
														if (!Start.listaPlikow.Contains(savePath))
														{
															uri = string.Concat(new string[]
															{
																server2,
																"layout/",
																this.electoralEampaignURL,
																"-",
																instkod,
																"_1_ERR.docx"
															});
															KLKresponse res = this.con.getRequestKBWKlkDocx(uri, savePath, 0);
															Start.listaPlikow.Add(savePath);
														}
														savePath = string.Concat(new string[]
														{
															this.path,
															"\\ProtocolsDef\\",
															this.electoralEampaignSave,
															"-",
															instkod,
															"_1_Walidacja.xml"
														});
														if (!Start.listaPlikow.Contains(savePath))
														{
															uri = string.Concat(new string[]
															{
																server2,
																"integrity/",
																this.electoralEampaignURL,
																"-",
																instkod,
																"_1"
															});
															KLKresponse res = this.con.getRequestKBWKlk(uri, savePath, 0);
															Start.listaPlikow.Add(savePath);
														}
													}
												}
												foreach (XmlNode okr in inst)
												{
													if (okr.Name == "okr")
													{
														XmlNode okrNr = okr.Attributes.GetNamedItem("nr");
														XmlNode status = okr.Attributes.GetNamedItem("status");
														if (okrNr != null && instkod != "" && okrNr.Value != "")
														{
															savePath = string.Concat(new string[]
															{
																this.path,
																"\\ProtocolsDef\\",
																this.electoralEampaignSave,
																"-",
																instkod,
																"-",
																inst_jns,
																"-",
																okrNr.Value,
																".xml"
															});
															if (!Start.listaPlikow.Contains(savePath))
															{
																uri = string.Concat(new string[]
																{
																	server2,
																	"candidates/",
																	this.electoralEampaignURL,
																	"-",
																	instkod,
																	"-",
																	inst_jns,
																	"-",
																	okrNr.Value
																});
																this.test = this.test + uri + "; " + '\n'.ToString();
																KLKresponse res = this.con.getRequestKBWKlk(uri, savePath, 0);
																Start.listaPlikow.Add(savePath);
															}
														}
													}
												}
											}
										}
									}
									loop++;
								}
							}
						}
						catch (XmlException)
						{
							MessageBox.Show("Nieprawidłowy XML", "Error");
						}
						catch (System.NullReferenceException)
						{
							MessageBox.Show("Podanno inny xml niz header", "Error");
						}
					}
				}
				this.wait.setWaitPanel("Trwa aktualizowanie listy protokołów", "Proszę czekać");
				if (old)
				{
					this.start.wait.setWaitPanel("Trwa aktualizowanie listy protokołów", "Proszę czekać");
				}
				if (resHeader.isSaved())
				{
					this.createProtocols();
				}
			}
			else
			{
				this.getKLKWithoutNet(old, false);
			}
			this.setElectionData();
			this.wait.setVisible(false);
			if (old)
			{
				this.start.wait.setVisible(false);
			}
			if (!old)
			{
				this.refreshListOfProtocols();
			}
		}
		private void createProtocols()
		{
			if (System.IO.Directory.Exists(this.path + "\\electoralEampaign"))
			{
				string a = string.Concat(new string[]
				{
					this.path,
					"\\electoralEampaign\\",
					this.electoralEampaignSave,
					"-",
					this.jns,
					".xml"
				});
				if (System.IO.File.Exists(a))
				{
					try
					{
						this.header.Load(string.Concat(new string[]
						{
							this.path,
							"\\electoralEampaign\\",
							this.electoralEampaignSave,
							"-",
							this.jns,
							".xml"
						}));
						if (!System.IO.Directory.Exists(this.path + "\\saves"))
						{
							System.IO.Directory.CreateDirectory(this.path + "\\saves");
						}
						try
						{
							string obw = "";
							string instkod = "";
							string instJNS = "";
							XmlNode headerRoot = this.header.SelectSingleNode("/akcja_wyborcza/jns");
							foreach (XmlNode item in headerRoot)
							{
								if (item.Name == "obw")
								{
									XmlNode nr = item.Attributes.GetNamedItem("nr");
									if (nr != null)
									{
										obw = "-" + nr.Value;
									}
									foreach (XmlNode inst in item)
									{
										if (inst.Name == "inst")
										{
											XmlNode kod = inst.Attributes.GetNamedItem("kod");
											if (kod != null)
											{
												instkod = "-" + kod.Value;
											}
											XmlNode Ijns = inst.Attributes.GetNamedItem("inst_jns");
											if (Ijns != null)
											{
												instJNS = "-" + Ijns.Value;
											}
											foreach (XmlNode okr in inst)
											{
												if (okr.Name == "okr")
												{
													XmlNode okrNr = okr.Attributes.GetNamedItem("nr");
													XmlNode status = okr.Attributes.GetNamedItem("status");
													if (okrNr != null && status != null && status.Value == "A")
													{
														if (!System.IO.File.Exists(string.Concat(new string[]
														{
															this.path,
															"\\saves\\",
															this.electoralEampaignSave,
															"-",
															this.jns,
															obw,
															instkod,
															instJNS,
															"-",
															okrNr.Value,
															".xml"
														})))
														{
															try
															{
																System.IO.StreamWriter sw = new System.IO.StreamWriter(string.Concat(new string[]
																{
																	this.path,
																	"\\saves\\",
																	this.electoralEampaignSave,
																	"-",
																	this.jns,
																	obw,
																	instkod,
																	instJNS,
																	"-",
																	okrNr.Value,
																	".xml"
																}), false);
																sw.Write("<?xml version=\"1.0\"?><save><status>niewypełniony</status></save>");
																sw.Close();
															}
															catch (System.ArgumentNullException)
															{
																MessageBox.Show("Błędna ścieżka", "Error");
															}
															catch (System.ArgumentException)
															{
																MessageBox.Show("Błędna ścieżka", "Error");
															}
															catch (System.IO.DirectoryNotFoundException)
															{
																MessageBox.Show("Nie znaleziono katalogu", "Error");
															}
															catch (System.IO.PathTooLongException)
															{
																MessageBox.Show("Zbyt długa ścieżka do katalogu", "Error");
															}
															catch (System.IO.IOException)
															{
																MessageBox.Show("Podano ścieżke do pliku zamiast katalogu", "Error");
															}
															catch (System.UnauthorizedAccessException)
															{
																MessageBox.Show("Program nie posiada uprawnień do usuwania plików", "Error");
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
						catch (XmlException)
						{
							MessageBox.Show("Nieprawidłowy XML", "Error");
						}
						catch (System.ArgumentNullException)
						{
							MessageBox.Show("Błędna ścieżka", "Error");
						}
						catch (System.ArgumentOutOfRangeException)
						{
							MessageBox.Show("Błędna opcja szukania", "Error");
						}
						catch (System.ArgumentException)
						{
							MessageBox.Show("Błędna ścieżka", "Error");
						}
						catch (System.IO.DirectoryNotFoundException)
						{
							MessageBox.Show("Nie znaleziono katalogu", "Error");
						}
						catch (System.IO.FileNotFoundException)
						{
							MessageBox.Show("Nie znaleziono pliku", "Error");
						}
						catch (System.IO.IOException)
						{
							MessageBox.Show("Podano ścieżke do pliku zamiast katalogu", "Error");
						}
						catch (System.UnauthorizedAccessException)
						{
							MessageBox.Show("Program nie posiada uprawnień do usuwania plików", "Error");
						}
						catch (System.NotSupportedException)
						{
							MessageBox.Show("Nieprawidłowa nazwa pliku", "Error");
						}
					}
					catch (XmlException e)
					{
						MessageBox.Show("Nieprawidłowy XML: " + e.Message, "Błąd");
					}
					catch (System.NullReferenceException)
					{
						MessageBox.Show("Podanno inny xml niz header", "Error");
					}
					catch (System.ArgumentNullException)
					{
						MessageBox.Show("Błędna ścieżka", "Error");
					}
					catch (System.ArgumentOutOfRangeException)
					{
						MessageBox.Show("Błędna opcja szukania", "Error");
					}
					catch (System.ArgumentException)
					{
						MessageBox.Show("Błędna ścieżka", "Error");
					}
					catch (System.IO.DirectoryNotFoundException)
					{
						MessageBox.Show("Nie znaleziono katalogu", "Error");
					}
					catch (System.IO.FileNotFoundException)
					{
						MessageBox.Show("Nie znaleziono pliku", "Error");
					}
					catch (System.IO.IOException)
					{
						MessageBox.Show("Podano ścieżke do pliku zamiast katalogu", "Error");
					}
					catch (System.UnauthorizedAccessException)
					{
						MessageBox.Show("Program nie posiada uprawnień do usuwania plików", "Error");
					}
					catch (System.NotSupportedException)
					{
						MessageBox.Show("Nieprawidłowa nazwa pliku", "Error");
					}
				}
			}
		}
		private void getKLKWithoutNet(bool old, bool update)
		{
			this.wait.setWaitPanel("Trwa aktualizowanie danych klk", "Proszę czekać");
			if (old)
			{
				this.start.wait.setWaitPanel("Trwa aktualizowanie danych klk", "Proszę czekać");
			}
			if (!System.IO.Directory.Exists(this.path + "\\electoralEampaign"))
			{
				try
				{
					System.IO.Directory.CreateDirectory(this.path + "\\electoralEampaign");
				}
				catch (System.ArgumentNullException)
				{
					MessageBox.Show("Błędna ścieżka", "Error");
				}
				catch (System.ArgumentException)
				{
					MessageBox.Show("Błędna ścieżka", "Error");
				}
				catch (System.IO.DirectoryNotFoundException)
				{
					MessageBox.Show("Nie znaleziono katalogu", "Error");
				}
				catch (System.IO.PathTooLongException)
				{
					MessageBox.Show("Zbyt długa ścieżka do katalogu", "Error");
				}
				catch (System.IO.IOException)
				{
					MessageBox.Show("Podano ścieżke do pliku zamiast katalogu", "Error");
				}
				catch (System.UnauthorizedAccessException)
				{
					MessageBox.Show("Program nie posiada uprawnień do usuwania plików", "Error");
				}
				catch (System.NotSupportedException)
				{
					MessageBox.Show("Nieprawidłowa nazwa pliku", "Error");
				}
			}
			if (!update)
			{
				object sender = new object();
				System.EventArgs e = new System.EventArgs();
				this.getKlkFromDisc_Click(sender, e);
				this.createProtocols();
			}
			else
			{
				if (System.IO.Directory.Exists(this.path + "\\electoralEampaign"))
				{
					GetKlk klk = new GetKlk();
					if (this.circuit != null && this.circuit != "")
					{
						klk = new GetKlk(this.path, this.electoralEampaignSave, this.jns, this.role, this.circuit);
					}
					else
					{
						klk = new GetKlk(this.path, this.electoralEampaignSave, this.jns, this.role);
					}
					klk.ShowDialog();
					this.wait.setWaitPanel("Trwa aktualizowanie listy protokołów", "Proszę czekać");
					if (old)
					{
						this.start.wait.setWaitPanel("Trwa aktualizowanie listy protokołów", "Proszę czekać");
					}
					this.createProtocols();
				}
			}
		}
		private void getKlkFromDisc_Click(object sender, System.EventArgs e)
		{
			this.ExtractKlkFromDisc();
			this.setElectionData();
			this.createProtocols();
			this.refreshListOfProtocols();
		}
		private void ExtractKlkFromDisc(object sender, System.EventArgs ex)
		{
			OpenFileDialog wnd = new OpenFileDialog();
			wnd.Title = "Wskaż plik zip zawierający dokumenty...";
			wnd.Filter = "(*.zip)|*.zip";
			string filePath = "";
			if (wnd.ShowDialog() == DialogResult.OK)
			{
				filePath = wnd.FileName;
			}
			if (filePath != "")
			{
				this.wait.setWaitPanel("Trwa importowanie danych", "Proszę czekać...");
				using (ZipFile zip = ZipFile.Read(filePath))
				{
					foreach (ZipEntry e in zip)
					{
						if (System.IO.File.Exists(this.path + "\\" + e.FileName))
						{
							System.IO.File.Delete(this.path + "\\" + e.FileName);
						}
						e.Extract(this.path);
					}
				}
				this.wait.setVisible(false);
			}
		}
		private void protocolsTable_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Return)
			{
				int i = (sender as DataGridView).CurrentRow.Index;
				if (i++ < (sender as DataGridView).RowCount)
				{
					SendKeys.Send("{down}");
				}
				else
				{
					SendKeys.Send("{home}");
				}
			}
		}
		private void wczytaj_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog wnd = new OpenFileDialog();
			wnd.Filter = "(*.xml)|*.xml";
			string filePath = "";
			if (wnd.ShowDialog() == DialogResult.OK)
			{
				filePath = wnd.FileName;
			}
			if (filePath != "")
			{
				this.wait.setWaitPanel("Trwa importowanie protokołu", "Proszę czekać");
				this.wait.setVisible(true);
				System.IO.StreamReader sr = new System.IO.StreamReader(filePath);
				string file = sr.ReadToEnd();
				sr.Close();
				try
				{
					if (file != "")
					{
						bool canSave = false;
						XmlDocument save = new XmlDocument();
						save.LoadXml(file);
						string saveAction = "";
						string saveJns = "0";
						string instkod = "";
						string importedInstJNS = "0";
						XmlNode actionXML = save.SelectSingleNode("save/header/defklk");
						if (actionXML != null && actionXML.FirstChild != null && actionXML.FirstChild.Value != "")
						{
							string[] dataklk = actionXML.FirstChild.Attributes.GetNamedItem("name").Value.Split(new char[]
							{
								'-'
							});
							saveAction = dataklk[0];
							instkod = dataklk[1];
						}
						XmlNode xmlImportedInst = save.SelectSingleNode("/save/header/instJNS");
						if (xmlImportedInst != null && xmlImportedInst.InnerText != "")
						{
							importedInstJNS = xmlImportedInst.InnerText;
						}
						XmlNode jnsXML = save.SelectSingleNode("save/header/jns_kod");
						if (jnsXML != null && jnsXML.InnerText != "")
						{
							saveJns = jnsXML.InnerText;
						}
						string saveOkr = "";
						string saveObw = "";
						XmlNode obwXML = save.SelectSingleNode("save/header/nrObwodu");
						if (obwXML != null && obwXML.InnerText != "")
						{
							saveObw = obwXML.InnerText;
						}
						XmlNode okrXML = save.SelectSingleNode("save/header/nrOkregu");
						if (okrXML != null && okrXML.InnerText != "")
						{
							saveOkr = okrXML.InnerText;
						}
						if (this.role == "O" || this.role == "A")
						{
							if (System.Convert.ToInt32(this.jns) == System.Convert.ToInt32(saveJns) && this.electoralEampaignURL == saveAction)
							{
								canSave = true;
							}
						}
						string obwod = "";
						if (this.role == "P" || this.role == "Z")
						{
							if (System.Convert.ToInt32(this.jns) == System.Convert.ToInt32(saveJns) && this.electoralEampaignURL == saveAction && this.circuit == saveObw)
							{
								canSave = true;
							}
							else
							{
								obwod = " oraz obwód: " + saveObw;
							}
						}
						if (canSave)
						{
							int num = System.IO.Directory.GetFiles(this.path + "\\saves", string.Concat(new string[]
							{
								this.electoralEampaignSave,
								"-",
								this.jns,
								"-",
								saveObw,
								"-",
								instkod,
								"-",
								importedInstJNS,
								"-",
								saveOkr,
								"*.xml"
							})).Length;
							string namefile = string.Concat(new string[]
							{
								this.electoralEampaignSave,
								"-",
								this.jns,
								"-",
								saveObw,
								"-",
								instkod,
								"-",
								importedInstJNS,
								"-",
								saveOkr
							});
							if (num > 0)
							{
								namefile = namefile + " " + (num + 1).ToString();
							}
							System.IO.StreamWriter sw = new System.IO.StreamWriter(this.path + "\\saves\\" + namefile + ".xml", false);
							sw.Write(file);
							sw.Close();
							MessageBox.Show("Plik został zaimportowany jako " + namefile, "Import protokołu");
							this.refreshListOfProtocols();
							this.wait.setVisible(false);
						}
						else
						{
							if (saveJns == "0" || importedInstJNS == "0")
							{
								MessageBox.Show("Nie można zaimportować niewypełnionego protokołu.", "Uwaga");
							}
							else
							{
								MessageBox.Show(string.Concat(new string[]
								{
									"Nie jesteś uprawniony do wczytania tego porotokołu. Zaloguj sie na licencje osoby przydzielonej do akcji wyborczej: ",
									saveAction,
									" i jns: ",
									saveJns,
									obwod,
									" aby zaimportowac protokół."
								}), "Uwaga");
							}
						}
						this.wait.setVisible(false);
					}
				}
				catch (System.Exception ex)
				{
					MessageBox.Show("Nie można wczytać tego porotokołu. Orginal exception: " + ex.Message, "Uwaga");
				}
				this.wait.setVisible(false);
			}
		}
		private void setElectionData()
		{
			if (System.IO.Directory.Exists(this.path + "\\electoralEampaign"))
			{
				if (System.IO.File.Exists(string.Concat(new string[]
				{
					this.path,
					"\\electoralEampaign\\",
					this.electoralEampaignSave,
					"-",
					this.jns,
					".xml"
				})))
				{
					try
					{
						this.header.Load(string.Concat(new string[]
						{
							this.path,
							"\\electoralEampaign\\",
							this.electoralEampaignSave,
							"-",
							this.jns,
							".xml"
						}));
						XmlNode date = this.header.SelectSingleNode("/akcja_wyborcza").Attributes.GetNamedItem("data-ost-aktualizacji");
						if (date != null && System.Text.RegularExpressions.Regex.IsMatch(date.Value, "^[0-9]{4}-[0-9]{2}-[0-9]{2}$"))
						{
							string[] a = date.Value.Split(new char[]
							{
								'-'
							});
							try
							{
								this.electionData = new System.DateTime(System.Convert.ToInt32(a[0]), System.Convert.ToInt32(a[1]), System.Convert.ToInt32(a[2]), 7, 0, 0);
							}
							catch (System.Exception)
							{
								this.electionData = new System.DateTime(1, 1, 1, 0, 0, 0);
							}
						}
						else
						{
							this.electionData = new System.DateTime(1, 1, 1, 0, 0, 0);
						}
					}
					catch (System.Exception)
					{
					}
				}
			}
		}
		private void attendance_Click(object sender, System.EventArgs e)
		{
			try
			{
				Attendance formAtt = new Attendance(this.licensepath, this.header, this.jns, this.role, this.circuit, this.electoralEampaignSave);
				formAtt.ShowDialog();
			}
			catch (System.Exception)
			{
			}
		}
		private void getInstData(string instkod, string server2)
		{
			string savePath = string.Concat(new string[]
			{
				this.path,
				"\\ProtocolsDef\\",
				this.electoralEampaignSave,
				"-",
				instkod,
				".xml"
			});
			string uri = string.Concat(new string[]
			{
				server2,
				"layout/",
				this.electoralEampaignURL,
				"-",
				instkod
			});
			KLKresponse res = this.con.getRequestKBWKlk(uri, savePath, 0);
			savePath = string.Concat(new string[]
			{
				this.path,
				"\\ProtocolsDef\\",
				this.electoralEampaignSave,
				"-",
				instkod,
				".docx"
			});
			uri = string.Concat(new string[]
			{
				server2,
				"layout/",
				this.electoralEampaignURL,
				"-",
				instkod,
				".docx"
			});
			res = this.con.getRequestKBWKlkDocx(uri, savePath, 0);
			savePath = string.Concat(new string[]
			{
				this.path,
				"\\ProtocolsDef\\",
				this.electoralEampaignSave,
				"-",
				instkod,
				"_EMPTY.docx"
			});
			uri = string.Concat(new string[]
			{
				server2,
				"layout/",
				this.electoralEampaignURL,
				"-",
				instkod,
				"_EMPTY.docx"
			});
			res = this.con.getRequestKBWKlkDocx(uri, savePath, 0);
			savePath = string.Concat(new string[]
			{
				this.path,
				"\\ProtocolsDef\\",
				this.electoralEampaignSave,
				"-",
				instkod,
				"_Walidacja.xml"
			});
			uri = string.Concat(new string[]
			{
				server2,
				"integrity/",
				this.electoralEampaignURL,
				"-",
				instkod
			});
			res = this.con.getRequestKBWKlk(uri, savePath, 0);
			if (instkod == "RDA")
			{
				savePath = string.Concat(new string[]
				{
					this.path,
					"\\ProtocolsDef\\",
					this.electoralEampaignSave,
					"-",
					instkod,
					"_M.xml"
				});
				uri = string.Concat(new string[]
				{
					server2,
					"layout/",
					this.electoralEampaignURL,
					"-",
					instkod,
					"_M"
				});
				res = this.con.getRequestKBWKlk(uri, savePath, 0);
				savePath = string.Concat(new string[]
				{
					this.path,
					"\\ProtocolsDef\\",
					this.electoralEampaignSave,
					"-",
					instkod,
					"_D.xml"
				});
				uri = string.Concat(new string[]
				{
					server2,
					"layout/",
					this.electoralEampaignURL,
					"-",
					instkod,
					"_D"
				});
				res = this.con.getRequestKBWKlk(uri, savePath, 0);
				savePath = string.Concat(new string[]
				{
					this.path,
					"\\ProtocolsDef\\",
					this.electoralEampaignSave,
					"-",
					instkod,
					"_M.docx"
				});
				if (!System.IO.File.Exists(savePath))
				{
					uri = string.Concat(new string[]
					{
						server2,
						"layout/",
						this.electoralEampaignURL,
						"-",
						instkod,
						"_M.docx"
					});
					res = this.con.getRequestKBWKlkDocx(uri, savePath, 0);
				}
				savePath = string.Concat(new string[]
				{
					this.path,
					"\\ProtocolsDef\\",
					this.electoralEampaignSave,
					"-",
					instkod,
					"_D.docx"
				});
				if (!System.IO.File.Exists(savePath))
				{
					uri = string.Concat(new string[]
					{
						server2,
						"layout/",
						this.electoralEampaignURL,
						"-",
						instkod,
						"_D.docx"
					});
					res = this.con.getRequestKBWKlkDocx(uri, savePath, 0);
				}
				savePath = string.Concat(new string[]
				{
					this.path,
					"\\ProtocolsDef\\",
					this.electoralEampaignSave,
					"-",
					instkod,
					"_M_EMPTY.docx"
				});
				if (!System.IO.File.Exists(savePath))
				{
					uri = string.Concat(new string[]
					{
						server2,
						"layout/",
						this.electoralEampaignURL,
						"-",
						instkod,
						"_M_EMPTY.docx"
					});
					res = this.con.getRequestKBWKlkDocx(uri, savePath, 0);
				}
				savePath = string.Concat(new string[]
				{
					this.path,
					"\\ProtocolsDef\\",
					this.electoralEampaignSave,
					"-",
					instkod,
					"_D_EMPTY.docx"
				});
				if (!System.IO.File.Exists(savePath))
				{
					uri = string.Concat(new string[]
					{
						server2,
						"layout/",
						this.electoralEampaignURL,
						"-",
						instkod,
						"_D_EMPTY.docx"
					});
					res = this.con.getRequestKBWKlkDocx(uri, savePath, 0);
				}
				savePath = string.Concat(new string[]
				{
					this.path,
					"\\ProtocolsDef\\",
					this.electoralEampaignSave,
					"-",
					instkod,
					"_M_Walidacja.xml"
				});
				uri = string.Concat(new string[]
				{
					server2,
					"integrity/",
					this.electoralEampaignURL,
					"-",
					instkod,
					"_M"
				});
				res = this.con.getRequestKBWKlk(uri, savePath, 0);
				savePath = string.Concat(new string[]
				{
					this.path,
					"\\ProtocolsDef\\",
					this.electoralEampaignSave,
					"-",
					instkod,
					"_D_Walidacja.xml"
				});
				uri = string.Concat(new string[]
				{
					server2,
					"integrity/",
					this.electoralEampaignURL,
					"-",
					instkod,
					"_D"
				});
				res = this.con.getRequestKBWKlk(uri, savePath, 0);
			}
			if (instkod == "WBP")
			{
				savePath = string.Concat(new string[]
				{
					this.path,
					"\\ProtocolsDef\\",
					this.electoralEampaignSave,
					"-",
					instkod,
					"_1.xml"
				});
				uri = string.Concat(new string[]
				{
					server2,
					"layout/",
					this.electoralEampaignURL,
					"-",
					instkod,
					"_1"
				});
				res = this.con.getRequestKBWKlk(uri, savePath, 0);
				savePath = string.Concat(new string[]
				{
					this.path,
					"\\ProtocolsDef\\",
					this.electoralEampaignSave,
					"-",
					instkod,
					"_1.docx"
				});
				if (!System.IO.File.Exists(savePath))
				{
					uri = string.Concat(new string[]
					{
						server2,
						"layout/",
						this.electoralEampaignURL,
						"-",
						instkod,
						"_1.docx"
					});
					res = this.con.getRequestKBWKlkDocx(uri, savePath, 0);
				}
				savePath = string.Concat(new string[]
				{
					this.path,
					"\\ProtocolsDef\\",
					this.electoralEampaignSave,
					"-",
					instkod,
					"_1_EMPTY.docx"
				});
				if (!System.IO.File.Exists(savePath))
				{
					uri = string.Concat(new string[]
					{
						server2,
						"layout/",
						this.electoralEampaignURL,
						"-",
						instkod,
						"_1_EMPTY.docx"
					});
					res = this.con.getRequestKBWKlkDocx(uri, savePath, 0);
				}
				savePath = string.Concat(new string[]
				{
					this.path,
					"\\ProtocolsDef\\",
					this.electoralEampaignSave,
					"-",
					instkod,
					"_1_Walidacja.xml"
				});
				uri = string.Concat(new string[]
				{
					server2,
					"integrity/",
					this.electoralEampaignURL,
					"-",
					instkod,
					"_1"
				});
				res = this.con.getRequestKBWKlk(uri, savePath, 0);
			}
		}
		private void ExtractKlkFromDisc()
		{
			OpenFileDialog wnd = new OpenFileDialog();
			wnd.Title = "Wskaż plik zip zawierający dokumenty...";
			wnd.Filter = "(*.zip)|*.zip";
			string filePath = "";
			if (wnd.ShowDialog() == DialogResult.OK)
			{
				filePath = wnd.FileName;
			}
			if (filePath != "")
			{
				this.wait.setWaitPanel("Trwa importowanie danych", "Proszę czekać...");
				using (ZipFile zip = ZipFile.Read(filePath))
				{
					foreach (ZipEntry e in zip)
					{
						if (System.IO.File.Exists(this.path + "\\" + e.FileName))
						{
							System.IO.File.Delete(this.path + "\\" + e.FileName);
						}
						e.Extract(this.path);
					}
				}
				this.wait.setVisible(false);
			}
		}
		private void txtWyszukaj_TextChanged(object sender, System.EventArgs e)
		{
			this.getProtocols(false);
		}
		private void lblWyszukaj_Click(object sender, System.EventArgs e)
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
			this.attendance = new Button();
			this.protocolsPanel = new Panel();
			this.panel1 = new Panel();
			this.panWyszukiwanie = new Panel();
			this.lblWyszukaj = new Label();
			this.txtWyszukaj = new TextBox();
			this.button1 = new Button();
			this.getKlkFromDisc = new Button();
			this.getProtocolsFromNet = new Button();
			this.label1 = new Label();
			this.protocolsTable = new DataGridView();
			this.protocolsPanel.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panWyszukiwanie.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.protocolsTable).BeginInit();
			base.SuspendLayout();
			this.attendance.Location = new System.Drawing.Point(17, 12);
			this.attendance.Name = "attendance";
			this.attendance.Size = new System.Drawing.Size(160, 23);
			this.attendance.TabIndex = 4;
			this.attendance.Text = "Wprowadź frekwencję";
			this.attendance.UseVisualStyleBackColor = true;
			this.attendance.Click += new System.EventHandler(this.attendance_Click);
			this.protocolsPanel.Controls.Add(this.panel1);
			this.protocolsPanel.Controls.Add(this.protocolsTable);
			this.protocolsPanel.Dock = DockStyle.Fill;
			this.protocolsPanel.Location = new System.Drawing.Point(0, 0);
			this.protocolsPanel.Name = "protocolsPanel";
			this.protocolsPanel.Size = new System.Drawing.Size(881, 409);
			this.protocolsPanel.TabIndex = 4;
			this.panel1.Controls.Add(this.panWyszukiwanie);
			this.panel1.Controls.Add(this.attendance);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.getKlkFromDisc);
			this.panel1.Controls.Add(this.getProtocolsFromNet);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(881, 63);
			this.panel1.TabIndex = 4;
			this.panWyszukiwanie.Controls.Add(this.lblWyszukaj);
			this.panWyszukiwanie.Controls.Add(this.txtWyszukaj);
			this.panWyszukiwanie.Location = new System.Drawing.Point(186, 12);
			this.panWyszukiwanie.Margin = new Padding(2);
			this.panWyszukiwanie.Name = "panWyszukiwanie";
			this.panWyszukiwanie.Size = new System.Drawing.Size(374, 46);
			this.panWyszukiwanie.TabIndex = 1;
			this.panWyszukiwanie.Visible = false;
			this.lblWyszukaj.AutoSize = true;
			this.lblWyszukaj.Location = new System.Drawing.Point(3, 25);
			this.lblWyszukaj.Name = "lblWyszukaj";
			this.lblWyszukaj.Size = new System.Drawing.Size(141, 13);
			this.lblWyszukaj.TabIndex = 8;
			this.lblWyszukaj.Text = "Wyszukiwanie po obwodzie:";
			this.lblWyszukaj.Click += new System.EventHandler(this.lblWyszukaj_Click);
			this.txtWyszukaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
			this.txtWyszukaj.Location = new System.Drawing.Point(149, 18);
			this.txtWyszukaj.Margin = new Padding(2);
			this.txtWyszukaj.Name = "txtWyszukaj";
			this.txtWyszukaj.Size = new System.Drawing.Size(215, 26);
			this.txtWyszukaj.TabIndex = 1;
			this.txtWyszukaj.TextAlign = HorizontalAlignment.Center;
			this.txtWyszukaj.TextChanged += new System.EventHandler(this.txtWyszukaj_TextChanged);
			this.button1.Location = new System.Drawing.Point(564, 37);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(309, 23);
			this.button1.TabIndex = 3;
			this.button1.Text = "Wczytaj protokół z dysku";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.wczytaj_Click);
			this.getKlkFromDisc.Location = new System.Drawing.Point(704, 12);
			this.getKlkFromDisc.Name = "getKlkFromDisc";
			this.getKlkFromDisc.Size = new System.Drawing.Size(169, 23);
			this.getKlkFromDisc.TabIndex = 2;
			this.getKlkFromDisc.Text = "Wczytaj pliki definicyjne z dysku";
			this.getKlkFromDisc.UseVisualStyleBackColor = true;
			this.getKlkFromDisc.Click += new System.EventHandler(this.getKlkFromDisc_Click);
			this.getProtocolsFromNet.Location = new System.Drawing.Point(564, 12);
			this.getProtocolsFromNet.Name = "getProtocolsFromNet";
			this.getProtocolsFromNet.Size = new System.Drawing.Size(134, 23);
			this.getProtocolsFromNet.TabIndex = 1;
			this.getProtocolsFromNet.Text = "Aktualizuj pliki definicyjne";
			this.getProtocolsFromNet.UseVisualStyleBackColor = true;
			this.getProtocolsFromNet.Click += new System.EventHandler(this.getProtocolsFromNet_Click);
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 42);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(163, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Wybierz protokół do wypełnienia";
			this.protocolsTable.AllowUserToAddRows = false;
			this.protocolsTable.AllowUserToDeleteRows = false;
			this.protocolsTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			this.protocolsTable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
			this.protocolsTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.protocolsTable.Location = new System.Drawing.Point(17, 75);
			this.protocolsTable.MultiSelect = false;
			this.protocolsTable.Name = "protocolsTable";
			this.protocolsTable.ReadOnly = true;
			this.protocolsTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.protocolsTable.Size = new System.Drawing.Size(847, 322);
			this.protocolsTable.TabIndex = 1;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(881, 409);
			base.Controls.Add(this.protocolsPanel);
			base.Name = "ProtocolsList";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Lista Protokołów";
			this.protocolsPanel.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panWyszukiwanie.ResumeLayout(false);
			this.panWyszukiwanie.PerformLayout();
			((System.ComponentModel.ISupportInitialize)this.protocolsTable).EndInit();
			base.ResumeLayout(false);
		}
	}
}
