using Kalkulator1.AdditionalClass;
using Kalkulator1.AdditionalWindow;
using Kalkulator1.instalClass;
using Kalkulator1.ResponseClass;
using Kalkulator1.validation;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using System.Xml;
namespace Kalkulator1
{
	public class ProtocolForm : Form
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
		private string docxDefinition;
		private string candDefinition;
		private string jns = "";
		private string obwod = "";
		private string inst = "";
		private string okreg = "";
		private string instJNS = "";
		private string xmlKandydaci;
		private string typeObw;
		private bool correspondence;
		private string lwyb = "";
		private string naklad = "";
		private string plusminus = "";
		private string plus = "";
		private string minus = "";
		private string lwybA = "";
		private string lwybB = "";
		private XmlDocument save;
		private string savePath;
		private int deletedCandidates;
		private System.Collections.Generic.List<string> controlsCanBeNull;
		private System.Collections.Generic.List<string> headerField;
		private string headerField2;
		private System.Collections.Generic.List<int[]> countcandidatesoflist;
		private System.Collections.Generic.Dictionary<string, ValidationRange> range;
		private System.Collections.Generic.List<ValidationPatern> lastValidators;
		private System.Collections.Generic.Dictionary<string, string> candidatesRule;
		private System.Collections.Generic.Dictionary<string, string> typeValidation;
		public KBWErrorProvider errorProvider1;
		private ToolTip tooltipErrors;
		public bool error;
		private int currentStep;
		private string version;
		private Control lastControl;
		private System.Drawing.FontFamily myfont = new System.Drawing.FontFamily("Arial");
		public bool goodcertificate;
		public bool imported;
		public string codeWarning;
		public string komSend = "";
		private bool currentCommittee;
		private bool clicNext;
		public WaitPanel wait;
		private ProtocolsList form;
		private WebBrowser web;
		private string OU;
		private string zORp = "";
		public string password = "";
		private string codeBarText;
		private string codeBarCode;
		private string path = "";
		private string licensePath;
		private string instId = "";
		private string okregId = "";
		private bool isKLKPro;
		private bool isKLKWali;
		private bool isKLKCan;
		private bool isKLK;
		private System.ComponentModel.IContainer components = null;
		private Panel protocolHeader;
		private Panel steps;
		private Panel panel3;
		private Button signProtocol;
		private Button protocolCommittee;
		private Button protocolSummation;
		private Button protocolForm2;
		private Button protocolForm1;
		private Panel protocolContent;
		private Button buttonNext;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private DataGridView personList;
		private Panel committeePanel;
		private Panel Form1panel;
		private Panel Form2panel;
		private Panel bottomPanel;
		private Panel buttonPanel;
		private Panel SummationPanel;
		private Panel errorPanel;
		private Label komisja1;
		private Label komisjaL2;
		private Label komisjaL1;
		private Label komisjaL3;
		private Panel signPanel;
		private DataGridView LicencesTable;
		private MenuStrip menuStrip1;
		private ToolStripMenuItem plikiToolStripMenuItem;
		private ToolStripMenuItem importujToolStripMenuItem;
		private ToolStripMenuItem eksportToolStripMenuItem1;
		private ToolStripMenuItem drukToolStripMenuItem;
		private Panel warningPanel;
		private Panel errorWarningPanel;
		private ToolStripMenuItem importujZSieciToolStripMenuItem;
		private ToolStripMenuItem eksportujZSieciToolStripMenuItem;
		private Panel raportPanel;
		public ProtocolForm()
		{
			this.path = System.IO.Path.GetTempPath() + "KBW";
			this.licensePath = "";
			this.lastControl = null;
			this.controlsCanBeNull = new System.Collections.Generic.List<string>();
			this.InitializeComponent();
			this.codeWarning = "";
			this.typeObw = "";
			this.headerName = "";
			this.committeeName = "";
			this.candidatesName = "";
			this.protocolDefinitionName = "";
			this.validateDefinitionName = "";
			this.docxDefinition = "";
			this.protocolDefinition = new XmlDocument();
			this.candidates = new XmlDocument();
			this.committee = new XmlDocument();
			this.validateDefinition = new XmlDocument();
			this.header = new XmlDocument();
			this.lastValidators = new System.Collections.Generic.List<ValidationPatern>();
			this.countcandidatesoflist = new System.Collections.Generic.List<int[]>();
			this.candidatesRule = new System.Collections.Generic.Dictionary<string, string>();
			this.errorProvider1 = new KBWErrorProvider();
			this.error = false;
			this.range = new System.Collections.Generic.Dictionary<string, ValidationRange>();
			this.personList.Visible = false;
			this.committeePanel.Visible = false;
			this.currentStep = 1;
			this.goodcertificate = false;
			this.personList.CellClick += new DataGridViewCellEventHandler(this.committee_CellClick);
			this.currentCommittee = false;
			this.clicNext = false;
			this.imported = false;
			this.wait = new WaitPanel("Wait3", base.Width, base.Height);
			this.form = null;
			this.typeValidation = new System.Collections.Generic.Dictionary<string, string>();
			this.codeBarText = "";
			this.codeBarCode = "";
			this.xmlKandydaci = "";
		}
		public ProtocolForm(ProtocolsList form, XmlDocument header, string protocolDefinition, string candidates, string committee, string validateDefinition, string save, string OU, string licensePath, string version)
		{
			this.InitializeComponent();
			this.tooltipErrors = new ToolTip();
			this.isKLKCan = true;
			this.isKLK = true;
			this.isKLKPro = true;
			this.isKLKWali = true;
			this.xmlKandydaci = "";
			this.version = version;
			this.licensePath = licensePath;
			this.path = System.IO.Path.GetTempPath() + "KBW";
			this.controlsCanBeNull = new System.Collections.Generic.List<string>();
			this.codeWarning = "";
			this.wait = new WaitPanel("Wait3", base.Width, base.Height);
			base.Controls.Add(this.wait.getPanel());
			base.Controls[this.wait.getName()].BringToFront();
			this.form = form;
			this.OU = OU;
			form.wait.setWaitPanel("Trwa otwieranie formularza protokołu", "Proszę czekać");
			form.wait.setVisible(true);
			this.error = false;
			this.range = new System.Collections.Generic.Dictionary<string, ValidationRange>();
			this.lastControl = null;
			this.typeObw = "";
			this.correspondence = false;
			this.errorProvider1 = new KBWErrorProvider();
			this.errorProvider1.clearErrors();
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
				this.docxDefinition = protocolDefinition.Replace(".xml", ".docx");
				this.candDefinition = candidates;
				if (protocolDefinition != "")
				{
					this.protocolDefinition = new XmlDocument();
					if (System.IO.File.Exists(protocolDefinition))
					{
						this.protocolDefinition.Load(protocolDefinition);
					}
					else
					{
						this.isKLKPro = false;
					}
				}
				if (candidates != "")
				{
					this.candidates = new XmlDocument();
					if (System.IO.File.Exists(candidates))
					{
						this.candidates.Load(candidates);
					}
					else
					{
						this.isKLKCan = false;
					}
				}
				if (committee != "")
				{
					this.committee = new XmlDocument();
					if (System.IO.File.Exists(committee))
					{
						this.committee.Load(committee);
					}
					else
					{
						this.isKLK = false;
					}
				}
				if (validateDefinition != "")
				{
					this.validateDefinition = new XmlDocument();
					if (System.IO.File.Exists(validateDefinition))
					{
						this.validateDefinition.Load(validateDefinition);
					}
					else
					{
						this.isKLKWali = false;
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
				this.instJNS = dataPath[4];
				string okreg = dataPath[5].Replace("Okr", "");
				okreg = okreg.Replace(".xml", "");
				string organNazwa = "";
				XmlNode headerRoot = header.SelectSingleNode("/akcja_wyborcza/jns");
				foreach (XmlNode xObwod in headerRoot)
				{
					if (xObwod.Attributes["nr"].InnerText == obwod)
					{
						foreach (XmlNode xInst in xObwod)
						{
							if (xInst.Attributes["kod"].InnerText == inst)
							{
								foreach (XmlNode xobw in xInst)
								{
									if (xobw.Attributes["nr"].InnerText == okreg && System.Convert.ToInt32(xInst.Attributes["inst_jns"].InnerText) == System.Convert.ToInt32(this.instJNS))
									{
										organNazwa = xInst.Attributes["organNazwa"].InnerText;
										break;
									}
								}
							}
						}
					}
				}
				if (inst == "WBP")
				{
					if (jns.Substring(0, 4) == "1465" && jns.Length == 6)
					{
						if (candidates != "")
						{
							string candidates2 = candidates.Replace(jns + "-" + okreg + ".xml", "146501-1.xml");
							this.candidates = new XmlDocument();
							if (System.IO.File.Exists(candidates2))
							{
								this.candidates.Load(candidates2);
								this.isKLKCan = true;
							}
							else
							{
								this.isKLKCan = false;
							}
						}
					}
				}
				if (inst == "RDA")
				{
					if (jns.Length < 6)
					{
						while (jns.Length < 6)
						{
							jns = "0" + jns;
						}
					}
					if (jns[2] == '7' || jns[2] == '6')
					{
						if (jns.Substring(0, 4) == "1465" && organNazwa == "m.st.")
						{
							this.protocolDefinition = new XmlDocument();
							string protocolDefinition2 = protocolDefinition.Replace(".xml", "_M.xml");
							if (System.IO.File.Exists(protocolDefinition2))
							{
								this.protocolDefinition.Load(protocolDefinition2);
								this.isKLKPro = true;
							}
							else
							{
								this.isKLKPro = false;
							}
							if (validateDefinition != "")
							{
								this.validateDefinition = new XmlDocument();
								string validateDefinition2 = validateDefinition.Replace("_Walidacja.xml", "_M_Walidacja.xml");
								if (System.IO.File.Exists(validateDefinition2))
								{
									this.validateDefinition.Load(validateDefinition2);
									this.isKLKWali = true;
								}
								else
								{
									this.isKLKWali = false;
								}
							}
							if (candidates != "")
							{
								string candidates2 = candidates.Replace(jns + "-" + okreg + ".xml", "146501-" + okreg + ".xml");
								this.candidates = new XmlDocument();
								if (System.IO.File.Exists(candidates2))
								{
									this.candidates.Load(candidates2);
									this.isKLKCan = true;
								}
								else
								{
									this.isKLK = false;
									this.isKLKCan = false;
								}
							}
						}
						if (jns.Substring(0, 4) != "1465")
						{
							if (protocolDefinition != "")
							{
								this.protocolDefinition = new XmlDocument();
								string protocolDefinition2 = protocolDefinition.Replace(".xml", "_M.xml");
								if (System.IO.File.Exists(protocolDefinition2))
								{
									this.protocolDefinition.Load(protocolDefinition2);
									this.isKLKPro = true;
								}
								else
								{
									this.isKLKPro = false;
								}
							}
							if (validateDefinition != "")
							{
								this.validateDefinition = new XmlDocument();
								string validateDefinition2 = validateDefinition.Replace("_Walidacja.xml", "_M_Walidacja.xml");
								if (System.IO.File.Exists(validateDefinition2))
								{
									this.validateDefinition.Load(validateDefinition2);
									this.isKLKWali = true;
								}
								else
								{
									this.isKLKWali = false;
								}
							}
						}
					}
					if (jns.Substring(0, 4) == "1465" && organNazwa == "Dzielnicy")
					{
						if (protocolDefinition != "")
						{
							this.protocolDefinition = new XmlDocument();
							string protocolDefinition2 = protocolDefinition.Replace(".xml", "_D.xml");
							if (System.IO.File.Exists(protocolDefinition2))
							{
								this.protocolDefinition.Load(protocolDefinition2);
								this.isKLKPro = true;
							}
							else
							{
								this.isKLKPro = false;
							}
						}
						if (validateDefinition != "")
						{
							this.validateDefinition = new XmlDocument();
							string validateDefinition2 = validateDefinition.Replace("_Walidacja.xml", "_D_Walidacja.xml");
							if (System.IO.File.Exists(validateDefinition2))
							{
								this.validateDefinition.Load(validateDefinition2);
								this.isKLKWali = true;
							}
							else
							{
								this.isKLKWali = false;
							}
						}
					}
				}
				if (this.isKLKCan)
				{
					this.deletedCandidates = this.countOfDeletedCandidate();
					if (this.isOneCandidate())
					{
						this.deletedCandidates = 1;
						if (protocolDefinition != "")
						{
							this.protocolDefinition = new XmlDocument();
							string protocolDefinition2 = protocolDefinition.Replace(".xml", "_1.xml");
							if (System.IO.File.Exists(protocolDefinition2))
							{
								this.protocolDefinition.Load(protocolDefinition2);
								this.isKLKPro = true;
							}
							else
							{
								this.isKLKPro = false;
							}
						}
						if (validateDefinition != "")
						{
							this.validateDefinition = new XmlDocument();
							string validateDefinition2 = validateDefinition.Replace("_Walidacja.xml", "_1_Walidacja.xml");
							if (System.IO.File.Exists(validateDefinition2))
							{
								this.validateDefinition.Load(validateDefinition2);
								this.isKLKWali = true;
							}
							else
							{
								this.isKLKWali = false;
							}
						}
					}
				}
			}
			catch (XmlException e)
			{
				MessageBox.Show("Nieprawidłowy XML: " + e.Message, "Błąd");
			}
			bool go = false;
			if (this.isKLK && this.isKLKCan && this.isKLKPro && this.isKLKWali)
			{
				go = this.checkProtocol(this.save, this.savePath);
			}
			if (this.isKLK && this.isKLKCan && this.isKLKPro && this.isKLKWali)
			{
				if (go)
				{
					this.committeePanel.Visible = false;
					this.goodcertificate = false;
					this.countcandidatesoflist = new System.Collections.Generic.List<int[]>();
					this.lastValidators = new System.Collections.Generic.List<ValidationPatern>();
					this.candidatesRule = new System.Collections.Generic.Dictionary<string, string>();
					this.typeValidation = new System.Collections.Generic.Dictionary<string, string>();
					form.wait.setWaitPanel("Trwa otwieranie formularza protokołu - ładowanie nagłówka", "Proszę czekać");
					this.getHeader();
					form.wait.setWaitPanel("Trwa otwieranie formularza protokołu - ładowanie pierwszego kroku", "Proszę czekać");
					this.getCalculator();
					this.Form1panel.Visible = true;
					this.Form1panel.Width = 776;
					this.Form2panel.Visible = false;
					this.Form2panel.Width = 776;
					this.committeePanel.Visible = false;
					this.committeePanel.Width = 776;
					this.SummationPanel.Visible = false;
					this.SummationPanel.Width = 776;
					this.signPanel.Visible = false;
					this.signPanel.Width = 776;
					this.raportPanel.Visible = false;
					this.raportPanel.Width = 776;
					this.protocolHeader.Width = 776;
					this.protocolForm1.Enabled = true;
					this.protocolForm2.Enabled = false;
					this.protocolSummation.Enabled = false;
					this.protocolCommittee.Enabled = false;
					this.signProtocol.Enabled = false;
					this.currentStep = 1;
					this.personList.CellClick += new DataGridViewCellEventHandler(this.committee_CellClick);
					this.currentCommittee = false;
					this.clicNext = false;
					this.imported = false;
					this.web = new WebBrowser();
					this.web.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(ProtocolForm.webBrowser_DocumentCompleted);
					form.wait.setVisible(false);
					this.LicencesTable.CellClick += new DataGridViewCellEventHandler(this.getLicense_CellClick);
					this.codeBarText = "";
					this.codeBarCode = "";
				}
				else
				{
					MessageBox.Show("Nie jesteś uprowniony do wypełniana tego protokołu.", "Uwaga");
					base.Close();
				}
			}
			else
			{
				MessageBox.Show("Nie pobrano wszystkich plików klk.", "Uwaga");
				base.Close();
			}
		}
		public bool canBeNull(string controlName)
		{
			bool result;
			for (int i = 0; i < this.controlsCanBeNull.Count; i++)
			{
				if (this.controlsCanBeNull[i] == controlName)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}
		private bool saves(int step)
		{
			this.wait.setWaitPanel("Trwa zapisywanie protokołu", "Proszę czekać");
			this.wait.setVisible(true);
			bool result;
			try
			{
				string xml = "<?xml version=\"1.0\"?>";
				xml += "<save>";
				xml = xml + "<step>" + step.ToString() + "</step>";
				xml += "<header>";
				xml = xml + "<id_intytucji>" + this.instId + "</id_intytucji>";
				xml = xml + "<id_okregu>" + this.okregId + "</id_okregu>";
				xml = xml + "<instJNS>" + this.instJNS + "</instJNS>";
				string text;
				foreach (Control c in this.protocolHeader.Controls)
				{
					if (c is TextBox || c is MaskedTextBox)
					{
						text = xml;
						xml = string.Concat(new string[]
						{
							text,
							"<",
							c.Name,
							">",
							c.Text,
							"</",
							c.Name,
							">"
						});
					}
				}
				string[] dataPath = this.savePath.Split(new char[]
				{
					'-'
				});
				string inst;
				string okreg;
				if (dataPath.Length == 6)
				{
					string obwod = dataPath[2].Replace("Obw", "");
					inst = dataPath[3].Replace("Inst", "");
					okreg = dataPath[5].Replace("Okr", "");
					okreg = okreg.Replace(".xml", "");
				}
				else
				{
					string obwod = dataPath[dataPath.Length - 4].Replace("Obw", "");
					inst = dataPath[dataPath.Length - 3].Replace("Inst", "");
					okreg = dataPath[dataPath.Length - 1].Replace("Okr", "");
					okreg = okreg.Replace(".xml", "");
				}
				if (inst == "WBP" || inst == "WBP_1")
				{
					xml = xml + "<nrOkregu>" + okreg + "</nrOkregu>";
				}
				xml = xml + "<KalVersion>" + this.version.ToString() + "</KalVersion>";
				xml = xml + "<system>" + System.Environment.OSVersion.VersionString.ToString() + "</system>";
				xml += "<defklk>";
				XmlNode def = this.protocolDefinition.SelectSingleNode("/protokol_info");
				XmlNode Datedef = def.Attributes.GetNamedItem("data_wersji");
				string date = "";
				if (Datedef != null)
				{
					date = Datedef.Value;
				}
				text = xml;
				xml = string.Concat(new string[]
				{
					text,
					"<klk name=\"",
					this.protocolDefinitionName,
					"\" data_wersji=\"",
					date,
					"\"/>"
				});
				XmlNode com = this.committee.SelectSingleNode("/komisja_sklad");
				XmlNode Datecom = com.Attributes.GetNamedItem("data_wersji");
				date = "";
				if (Datecom != null)
				{
					date = Datecom.Value;
				}
				text = xml;
				xml = string.Concat(new string[]
				{
					text,
					"<klk name=\"",
					this.committeeName,
					"\" data_wersji=\"",
					date,
					"\"/>"
				});
				XmlNode val = this.validateDefinition.SelectSingleNode("/validate_info");
				XmlNode Dateval = val.Attributes.GetNamedItem("data_wersji");
				date = "";
				if (Dateval != null)
				{
					date = Dateval.Value;
				}
				text = xml;
				xml = string.Concat(new string[]
				{
					text,
					"<klk name=\"",
					this.validateDefinitionName,
					"\" data_wersji=\"",
					date,
					"\"/>"
				});
				XmlNode list = this.candidates.SelectSingleNode("/listy");
				XmlNode Datelist = list.Attributes.GetNamedItem("data_aktualizacji");
				date = "";
				if (Datelist != null)
				{
					date = Datelist.Value;
				}
				text = xml;
				xml = string.Concat(new string[]
				{
					text,
					"<klk name=\"",
					this.candidatesName,
					"\" data_wersji=\"",
					date,
					"\"/>"
				});
				XmlNode h = this.header.SelectSingleNode("/akcja_wyborcza");
				XmlNode Dateh = h.Attributes.GetNamedItem("data_aktualizacji");
				date = "";
				if (Dateh != null)
				{
					date = Dateh.Value;
				}
				text = xml;
				xml = string.Concat(new string[]
				{
					text,
					"<klk name=\"",
					this.headerName,
					"\" data_wersji=\"",
					date,
					"\"/>"
				});
				xml += "</defklk>";
				xml += "</header>";
				if (step == 0 || step == 4)
				{
					xml += "<form>";
					foreach (Control c in this.SummationPanel.Controls)
					{
						if (c is TextBox || c is MaskedTextBox)
						{
							if (System.Text.RegularExpressions.Regex.IsMatch(c.Name, "^Kandydtat"))
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									" id_kand=\"",
									this.candidatesRule[c.Name],
									"\">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
							else
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
						}
					}
					xml = xml + "<skresleni>" + this.xmlKandydaci + "</skresleni>";
					xml += "</form>";
					xml += "<komisja_sklad";
					XmlNode nodesList = this.committee.SelectSingleNode("/komisja_sklad");
					foreach (XmlAttribute a in nodesList.Attributes)
					{
						text = xml;
						xml = string.Concat(new string[]
						{
							text,
							" ",
							a.Name,
							"=\"",
							a.Value,
							"\""
						});
					}
					xml += ">";
					for (int i = 0; i < this.personList.Rows.Count - 1; i++)
					{
						xml += "<osoba";
						if (this.personList.Rows[i].Cells["Imię"] != null && this.personList.Rows[i].Cells["Imię"].Value != null)
						{
							xml = xml + " imie=\"" + HttpUtility.UrlEncode(this.personList.Rows[i].Cells["Imię"].Value.ToString()) + "\"";
						}
						else
						{
							xml += " imie=\"\"";
						}
						if (this.personList.Rows[i].Cells["Drugie imię"] != null && this.personList.Rows[i].Cells["Drugie imię"].Value != null)
						{
							xml = xml + " imie2=\"" + HttpUtility.UrlEncode(this.personList.Rows[i].Cells["Drugie imię"].Value.ToString()) + "\"";
						}
						else
						{
							xml += " imie2=\"\"";
						}
						if (this.personList.Rows[i].Cells["Nazwisko"] != null && this.personList.Rows[i].Cells["Nazwisko"].Value != null)
						{
							xml = xml + " nazwisko=\"" + HttpUtility.UrlEncode(this.personList.Rows[i].Cells["Nazwisko"].Value.ToString()) + "\"";
						}
						else
						{
							xml += " nazwisko=\"\"";
						}
						if (this.personList.Rows[i].Cells["Funkcja"] != null && this.personList.Rows[i].Cells["Funkcja"].Value != null)
						{
							xml = xml + " funkcja=\"" + this.personList.Rows[i].Cells["Funkcja"].Value.ToString() + "\"";
						}
						else
						{
							xml += " funkcja=\"\"";
						}
						if (this.personList.Rows[i].Cells["action3"].Value != null)
						{
							xml = xml + " obecny=\"" + this.personList.Rows[i].Cells["action3"].Value.ToString() + "\"";
						}
						else
						{
							xml += " obecny=\"False\"";
						}
						xml += "/>";
					}
					xml += "</komisja_sklad>";
					if (step == 0)
					{
						xml += this.validateExportedXmlS(3);
						xml += "<status>podpisany</status>";
						text = xml;
						xml = string.Concat(new string[]
						{
							text,
							"<codeBar><code>",
							this.codeBarCode,
							"</code><id>",
							this.codeBarText,
							"</id></codeBar>"
						});
					}
					if (step == 4)
					{
						xml += this.validateExportedXmlS(3);
						xml += "<status>roboczy</status>";
					}
					if (this.codeWarning != "")
					{
						xml = xml + "<hardWarningCode>" + this.codeWarning + "</hardWarningCode>";
					}
				}
				if (step == 1)
				{
					xml += "<step1>";
					foreach (Control c in this.Form1panel.Controls)
					{
						if (c is TextBox || c is MaskedTextBox)
						{
							if (System.Text.RegularExpressions.Regex.IsMatch(c.Name, "^Kandydtat"))
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									" id_kand=\"",
									this.candidatesRule[c.Name],
									"\">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
							else
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
						}
					}
					xml = xml + "<skresleni>" + this.xmlKandydaci + "</skresleni>";
					xml += "</step1>";
					xml += " <status>roboczy</status>";
				}
				if (step == 2)
				{
					xml += "<step1>";
					foreach (Control c in this.Form1panel.Controls)
					{
						if (c is TextBox || c is MaskedTextBox)
						{
							if (System.Text.RegularExpressions.Regex.IsMatch(c.Name, "^Kandydtat"))
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									" id_kand=\"",
									this.candidatesRule[c.Name],
									"\">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
							else
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
						}
					}
					xml = xml + "<skresleni>" + this.xmlKandydaci + "</skresleni>";
					xml += "</step1>";
					xml += "<step2>";
					foreach (Control c in this.Form2panel.Controls)
					{
						if (c is TextBox || c is MaskedTextBox)
						{
							if (System.Text.RegularExpressions.Regex.IsMatch(c.Name, "^Kandydtat"))
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									" id_kand=\"",
									this.candidatesRule[c.Name],
									"\" >",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
							else
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
						}
					}
					xml = xml + "<skresleni>" + this.xmlKandydaci + "</skresleni>";
					xml += "</step2>";
					xml += "<status>roboczy</status>";
				}
				if (step == 3)
				{
					xml += "<step1>";
					foreach (Control c in this.Form1panel.Controls)
					{
						if (c is TextBox || c is MaskedTextBox)
						{
							if (System.Text.RegularExpressions.Regex.IsMatch(c.Name, "^Kandydtat"))
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									" id_kand=\"",
									this.candidatesRule[c.Name],
									"\">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
							else
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
						}
					}
					xml = xml + "<skresleni>" + this.xmlKandydaci + "</skresleni>";
					xml += "</step1>";
					xml += "<step2>";
					foreach (Control c in this.Form2panel.Controls)
					{
						if (c is TextBox || c is MaskedTextBox)
						{
							if (System.Text.RegularExpressions.Regex.IsMatch(c.Name, "^Kandydtat"))
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									" id_kand=\"",
									this.candidatesRule[c.Name],
									"\" >",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
							else
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
						}
					}
					xml = xml + "<skresleni>" + this.xmlKandydaci + "</skresleni>";
					xml += "</step2>";
					xml += "<step3>";
					foreach (Control c in this.SummationPanel.Controls)
					{
						if (c is TextBox || c is MaskedTextBox)
						{
							if (System.Text.RegularExpressions.Regex.IsMatch(c.Name, "^Kandydtat"))
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									" id_kand=\"",
									this.candidatesRule[c.Name],
									"\" >",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
							else
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
						}
					}
					xml = xml + "<skresleni>" + this.xmlKandydaci + "</skresleni>";
					xml += "</step3>";
					xml += this.validateExportedXmlS(3);
					xml += "<status>roboczy</status>";
					if (this.codeWarning != "")
					{
						xml = xml + "<hardWarningCode>" + this.codeWarning + "</hardWarningCode>";
					}
				}
				if (step == 0 || step == 4 || step == 3)
				{
					xml += "<report>";
					foreach (Control c in this.raportPanel.Controls)
					{
						if (c is TextBox || c is MaskedTextBox)
						{
							text = xml;
							xml = string.Concat(new string[]
							{
								text,
								"<",
								c.Name,
								">",
								c.Text,
								"</",
								c.Name,
								">"
							});
						}
					}
					xml += "</report>";
				}
				xml += "</save>";
				System.IO.StreamWriter sw = new System.IO.StreamWriter(this.savePath, false);
				sw.Write(xml);
				sw.Close();
				this.wait.setVisible(false);
				result = true;
			}
			catch (System.IO.IOException e)
			{
				this.wait.setVisible(false);
				MessageBox.Show("Nie mozna zapisac pliku. Orginal Error: " + e.Message, "Error");
				result = false;
			}
			catch (System.Exception e2)
			{
				this.wait.setVisible(false);
				MessageBox.Show("Nie mozna zapisac pliku. Orginal Error: " + e2.Message, "Error");
				result = false;
			}
			return result;
		}
		private string generateSaves(int step)
		{
			this.wait.setWaitPanel("Trwa zapisywanie protokołu", "Proszę czekać");
			this.wait.setVisible(true);
			string xml = "";
			try
			{
				xml = "<?xml version=\"1.0\"?>";
				xml += "<save>";
				xml = xml + "<step>" + step.ToString() + "</step>";
				xml += "<header>";
				xml = xml + "<id_intytucji>" + this.instId + "</id_intytucji>";
				xml = xml + "<id_okregu>" + this.okregId + "</id_okregu>";
				xml = xml + "<instJNS>" + this.instJNS + "</instJNS>";
				string text;
				foreach (Control c in this.protocolHeader.Controls)
				{
					if (c is TextBox || c is MaskedTextBox)
					{
						text = xml;
						xml = string.Concat(new string[]
						{
							text,
							"<",
							c.Name,
							">",
							c.Text,
							"</",
							c.Name,
							">"
						});
					}
				}
				string[] dataPath = this.savePath.Split(new char[]
				{
					'-'
				});
				string inst;
				string okreg;
				if (dataPath.Length == 6)
				{
					string obwod = dataPath[2].Replace("Obw", "");
					inst = dataPath[3].Replace("Inst", "");
					okreg = dataPath[5].Replace("Okr", "");
					okreg = okreg.Replace(".xml", "");
				}
				else
				{
					string obwod = dataPath[dataPath.Length - 4].Replace("Obw", "");
					inst = dataPath[dataPath.Length - 3].Replace("Inst", "");
					okreg = dataPath[dataPath.Length - 1].Replace("Okr", "");
					okreg = okreg.Replace(".xml", "");
				}
				if (inst == "WBP" || inst == "WBP_1")
				{
					xml = xml + "<nrOkregu>" + okreg + "</nrOkregu>";
				}
				xml = xml + "<KalVersion>" + this.version.ToString() + "</KalVersion>";
				xml = xml + "<system>" + System.Environment.OSVersion.VersionString.ToString() + "</system>";
				xml += "<defklk>";
				XmlNode def = this.protocolDefinition.SelectSingleNode("/protokol_info");
				XmlNode Datedef = def.Attributes.GetNamedItem("data_wersji");
				string date = "";
				if (Datedef != null)
				{
					date = Datedef.Value;
				}
				text = xml;
				xml = string.Concat(new string[]
				{
					text,
					"<klk name=\"",
					this.protocolDefinitionName,
					"\" data_wersji=\"",
					date,
					"\"/>"
				});
				XmlNode com = this.committee.SelectSingleNode("/komisja_sklad");
				XmlNode Datecom = com.Attributes.GetNamedItem("data_wersji");
				date = "";
				if (Datecom != null)
				{
					date = Datecom.Value;
				}
				text = xml;
				xml = string.Concat(new string[]
				{
					text,
					"<klk name=\"",
					this.committeeName,
					"\" data_wersji=\"",
					date,
					"\"/>"
				});
				XmlNode val = this.validateDefinition.SelectSingleNode("/validate_info");
				XmlNode Dateval = val.Attributes.GetNamedItem("data_wersji");
				date = "";
				if (Dateval != null)
				{
					date = Dateval.Value;
				}
				text = xml;
				xml = string.Concat(new string[]
				{
					text,
					"<klk name=\"",
					this.validateDefinitionName,
					"\" data_wersji=\"",
					date,
					"\"/>"
				});
				XmlNode list = this.candidates.SelectSingleNode("/listy");
				XmlNode Datelist = list.Attributes.GetNamedItem("data_aktualizacji");
				date = "";
				if (Datelist != null)
				{
					date = Datelist.Value;
				}
				text = xml;
				xml = string.Concat(new string[]
				{
					text,
					"<klk name=\"",
					this.candidatesName,
					"\" data_wersji=\"",
					date,
					"\"/>"
				});
				XmlNode h = this.header.SelectSingleNode("/akcja_wyborcza");
				XmlNode Dateh = h.Attributes.GetNamedItem("data_aktualizacji");
				date = "";
				if (Dateh != null)
				{
					date = Dateh.Value;
				}
				text = xml;
				xml = string.Concat(new string[]
				{
					text,
					"<klk name=\"",
					this.headerName,
					"\" data_wersji=\"",
					date,
					"\"/>"
				});
				xml += "</defklk>";
				xml += "</header>";
				if (step == 0 || step == 4)
				{
					xml += "<form>";
					foreach (Control c in this.SummationPanel.Controls)
					{
						if (c is TextBox || c is MaskedTextBox)
						{
							if (System.Text.RegularExpressions.Regex.IsMatch(c.Name, "^Kandydtat"))
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									" id_kand=\"",
									this.candidatesRule[c.Name],
									"\">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
							else
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
						}
					}
					xml = xml + "<skresleni>" + this.xmlKandydaci + "</skresleni>";
					xml += "</form>";
					xml += "<komisja_sklad";
					XmlNode nodesList = this.committee.SelectSingleNode("/komisja_sklad");
					foreach (XmlAttribute a in nodesList.Attributes)
					{
						text = xml;
						xml = string.Concat(new string[]
						{
							text,
							" ",
							a.Name,
							"=\"",
							a.Value,
							"\""
						});
					}
					xml += ">";
					for (int i = 0; i < this.personList.Rows.Count - 1; i++)
					{
						xml += "<osoba";
						if (this.personList.Rows[i].Cells["Imię"] != null && this.personList.Rows[i].Cells["Imię"].Value != null)
						{
							xml = xml + " imie=\"" + HttpUtility.UrlEncode(this.personList.Rows[i].Cells["Imię"].Value.ToString()) + "\"";
						}
						else
						{
							xml += " imie=\"\"";
						}
						if (this.personList.Rows[i].Cells["Drugie imię"] != null && this.personList.Rows[i].Cells["Drugie imię"].Value != null)
						{
							xml = xml + " imie2=\"" + HttpUtility.UrlEncode(this.personList.Rows[i].Cells["Drugie imię"].Value.ToString()) + "\"";
						}
						else
						{
							xml += " imie2=\"\"";
						}
						if (this.personList.Rows[i].Cells["Nazwisko"] != null && this.personList.Rows[i].Cells["Nazwisko"].Value != null)
						{
							xml = xml + " nazwisko=\"" + HttpUtility.UrlEncode(this.personList.Rows[i].Cells["Nazwisko"].Value.ToString()) + "\"";
						}
						else
						{
							xml += " nazwisko=\"\"";
						}
						if (this.personList.Rows[i].Cells["Funkcja"] != null && this.personList.Rows[i].Cells["Funkcja"].Value != null)
						{
							xml = xml + " funkcja=\"" + this.personList.Rows[i].Cells["Funkcja"].Value.ToString() + "\"";
						}
						else
						{
							xml += " funkcja=\"\"";
						}
						if (this.personList.Rows[i].Cells["action3"].Value != null)
						{
							xml = xml + " obecny=\"" + this.personList.Rows[i].Cells["action3"].Value.ToString() + "\"";
						}
						else
						{
							xml += " obecny=\"False\"";
						}
						xml += "/>";
					}
					xml += "</komisja_sklad>";
					if (step == 0)
					{
						xml += this.validateExportedXmlS(3);
						xml += "<status>podpisany</status>";
						text = xml;
						xml = string.Concat(new string[]
						{
							text,
							"<codeBar><code>",
							this.codeBarCode,
							"</code><id>",
							this.codeBarText,
							"</id></codeBar>"
						});
					}
					if (step == 4)
					{
						xml += this.validateExportedXmlS(3);
						xml += "<status>roboczy</status>";
					}
					if (this.codeWarning != "")
					{
						xml = xml + "<hardWarningCode>" + this.codeWarning + "</hardWarningCode>";
					}
				}
				if (step == 1)
				{
					xml += "<step1>";
					foreach (Control c in this.Form1panel.Controls)
					{
						if (c is TextBox || c is MaskedTextBox)
						{
							if (System.Text.RegularExpressions.Regex.IsMatch(c.Name, "^Kandydtat"))
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									" id_kand=\"",
									this.candidatesRule[c.Name],
									"\">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
							else
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
						}
					}
					xml = xml + "<skresleni>" + this.xmlKandydaci + "</skresleni>";
					xml += "</step1>";
					xml += " <status>roboczy</status>";
				}
				if (step == 2)
				{
					xml += "<step1>";
					foreach (Control c in this.Form1panel.Controls)
					{
						if (c is TextBox || c is MaskedTextBox)
						{
							if (System.Text.RegularExpressions.Regex.IsMatch(c.Name, "^Kandydtat"))
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									" id_kand=\"",
									this.candidatesRule[c.Name],
									"\">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
							else
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
						}
					}
					xml = xml + "<skresleni>" + this.xmlKandydaci + "</skresleni>";
					xml += "</step1>";
					xml += "<step2>";
					foreach (Control c in this.Form2panel.Controls)
					{
						if (c is TextBox || c is MaskedTextBox)
						{
							if (System.Text.RegularExpressions.Regex.IsMatch(c.Name, "^Kandydtat"))
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									" id_kand=\"",
									this.candidatesRule[c.Name],
									"\" >",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
							else
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
						}
					}
					xml = xml + "<skresleni>" + this.xmlKandydaci + "</skresleni>";
					xml += "</step2>";
					xml += "<status>roboczy</status>";
				}
				if (step == 3)
				{
					xml += "<step1>";
					foreach (Control c in this.Form1panel.Controls)
					{
						if (c is TextBox || c is MaskedTextBox)
						{
							if (System.Text.RegularExpressions.Regex.IsMatch(c.Name, "^Kandydtat"))
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									" id_kand=\"",
									this.candidatesRule[c.Name],
									"\">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
							else
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
						}
					}
					xml += "</step1>";
					xml += "<step2>";
					foreach (Control c in this.Form2panel.Controls)
					{
						if (c is TextBox || c is MaskedTextBox)
						{
							if (System.Text.RegularExpressions.Regex.IsMatch(c.Name, "^Kandydtat"))
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									" id_kand=\"",
									this.candidatesRule[c.Name],
									"\" >",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
							else
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
						}
					}
					xml += "</step2>";
					xml += "<step3>";
					foreach (Control c in this.SummationPanel.Controls)
					{
						if (c is TextBox || c is MaskedTextBox)
						{
							if (System.Text.RegularExpressions.Regex.IsMatch(c.Name, "^Kandydtat"))
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									" id_kand=\"",
									this.candidatesRule[c.Name],
									"\" >",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
							else
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
						}
					}
					xml += "</step3>";
					xml += this.validateExportedXmlS(3);
					xml += "<status>roboczy</status>";
					if (this.codeWarning != "")
					{
						xml = xml + "<hardWarningCode>" + this.codeWarning + "</hardWarningCode>";
					}
				}
				if (step == 0 || step == 4 || step == 3)
				{
					xml += "<report>";
					foreach (Control c in this.raportPanel.Controls)
					{
						if (c is TextBox || c is MaskedTextBox)
						{
							text = xml;
							xml = string.Concat(new string[]
							{
								text,
								"<",
								c.Name,
								">",
								c.Text,
								"</",
								c.Name,
								">"
							});
						}
					}
					xml += "</report>";
				}
				xml += "</save>";
			}
			catch (System.IO.IOException e)
			{
				this.wait.setVisible(false);
				MessageBox.Show("Nie mozna zapisac pliku. Orginal Error: " + e.Message, "Error");
			}
			catch (System.Exception e2)
			{
				this.wait.setVisible(false);
				MessageBox.Show("Nie mozna zapisac pliku. Orginal Error: " + e2.Message, "Error");
			}
			this.wait.setVisible(false);
			return xml;
		}
		private void saves(int step, string errors)
		{
			this.wait.setWaitPanel("Trwa zapisywanie protokołu", "Proszę czekać");
			this.wait.setVisible(true);
			try
			{
				string xml = "<?xml version=\"1.0\"?>";
				xml += "<save>";
				xml = xml + "<step>" + step.ToString() + "</step>";
				xml += "<header>";
				xml = xml + "<id_intytucji>" + this.instId + "</id_intytucji>";
				xml = xml + "<id_okregu>" + this.okregId + "</id_okregu>";
				xml = xml + "<instJNS>" + this.instJNS + "</instJNS>";
				string text;
				foreach (Control c in this.protocolHeader.Controls)
				{
					if (c is TextBox || c is MaskedTextBox)
					{
						text = xml;
						xml = string.Concat(new string[]
						{
							text,
							"<",
							c.Name,
							">",
							c.Text,
							"</",
							c.Name,
							">"
						});
					}
				}
				string[] dataPath = this.savePath.Split(new char[]
				{
					'-'
				});
				string inst;
				string okreg;
				if (dataPath.Length == 6)
				{
					string obwod = dataPath[2].Replace("Obw", "");
					inst = dataPath[3].Replace("Inst", "");
					okreg = dataPath[5].Replace("Okr", "");
					okreg = okreg.Replace(".xml", "");
				}
				else
				{
					string obwod = dataPath[dataPath.Length - 4].Replace("Obw", "");
					inst = dataPath[dataPath.Length - 3].Replace("Inst", "");
					okreg = dataPath[dataPath.Length - 1].Replace("Okr", "");
					okreg = okreg.Replace(".xml", "");
				}
				if (inst == "WBP" || inst == "WBP_1")
				{
					xml = xml + "<nrOkregu>" + okreg + "</nrOkregu>";
				}
				xml = xml + "<KalVersion>" + this.version.ToString() + "</KalVersion>";
				xml = xml + "<system>" + System.Environment.OSVersion.VersionString.ToString() + "</system>";
				xml += "<defklk>";
				XmlNode def = this.protocolDefinition.SelectSingleNode("/protokol_info");
				XmlNode Datedef = def.Attributes.GetNamedItem("data_wersji");
				string date = "";
				if (Datedef != null)
				{
					date = Datedef.Value;
				}
				text = xml;
				xml = string.Concat(new string[]
				{
					text,
					"<klk name=\"",
					this.protocolDefinitionName,
					"\" data_wersji=\"",
					date,
					"\"/>"
				});
				XmlNode com = this.committee.SelectSingleNode("/komisja_sklad");
				XmlNode Datecom = com.Attributes.GetNamedItem("data_wersji");
				date = "";
				if (Datecom != null)
				{
					date = Datecom.Value;
				}
				text = xml;
				xml = string.Concat(new string[]
				{
					text,
					"<klk name=\"",
					this.committeeName,
					"\" data_wersji=\"",
					date,
					"\"/>"
				});
				XmlNode val = this.validateDefinition.SelectSingleNode("/validate_info");
				XmlNode Dateval = val.Attributes.GetNamedItem("data_wersji");
				date = "";
				if (Dateval != null)
				{
					date = Dateval.Value;
				}
				text = xml;
				xml = string.Concat(new string[]
				{
					text,
					"<klk name=\"",
					this.validateDefinitionName,
					"\" data_wersji=\"",
					date,
					"\"/>"
				});
				XmlNode list = this.candidates.SelectSingleNode("/listy");
				XmlNode Datelist = list.Attributes.GetNamedItem("data_aktualizacji");
				date = "";
				if (Datelist != null)
				{
					date = Datelist.Value;
				}
				text = xml;
				xml = string.Concat(new string[]
				{
					text,
					"<klk name=\"",
					this.candidatesName,
					"\" data_wersji=\"",
					date,
					"\"/>"
				});
				XmlNode h = this.header.SelectSingleNode("/akcja_wyborcza");
				XmlNode Dateh = h.Attributes.GetNamedItem("data-ost-aktualizacji");
				date = "";
				if (Dateh != null)
				{
					date = Dateh.Value;
				}
				text = xml;
				xml = string.Concat(new string[]
				{
					text,
					"<klk name=\"",
					this.headerName,
					"\" data_wersji=\"",
					date,
					"\"/>"
				});
				xml += "</defklk>";
				xml += "</header>";
				if (step == 0 || step == 4)
				{
					xml += "<form>";
					foreach (Control c in this.SummationPanel.Controls)
					{
						if (c is TextBox || c is MaskedTextBox)
						{
							if (System.Text.RegularExpressions.Regex.IsMatch(c.Name, "^Kandydtat"))
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									" id_kand=\"",
									this.candidatesRule[c.Name],
									"\">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
							else
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
						}
					}
					xml = xml + "<skresleni>" + this.xmlKandydaci + "</skresleni>";
					xml += "</form>";
					xml += "<komisja_sklad";
					XmlNode nodesList = this.committee.SelectSingleNode("/komisja_sklad");
					foreach (XmlAttribute a in nodesList.Attributes)
					{
						text = xml;
						xml = string.Concat(new string[]
						{
							text,
							" ",
							a.Name,
							"=\"",
							a.Value,
							"\""
						});
					}
					xml += ">";
					for (int i = 0; i < this.personList.Rows.Count - 1; i++)
					{
						xml += "<osoba";
						if (this.personList.Rows[i].Cells["Imię"] != null && this.personList.Rows[i].Cells["Imię"].Value != null)
						{
							xml = xml + " imie=\"" + HttpUtility.UrlEncode(this.personList.Rows[i].Cells["Imię"].Value.ToString()) + "\"";
						}
						else
						{
							xml += " imie=\"\"";
						}
						if (this.personList.Rows[i].Cells["Drugie imię"] != null && this.personList.Rows[i].Cells["Drugie imię"].Value != null)
						{
							xml = xml + " imie2=\"" + HttpUtility.UrlEncode(this.personList.Rows[i].Cells["Drugie imię"].Value.ToString()) + "\"";
						}
						else
						{
							xml += " imie2=\"\"";
						}
						if (this.personList.Rows[i].Cells["Nazwisko"] != null && this.personList.Rows[i].Cells["Nazwisko"].Value != null)
						{
							xml = xml + " nazwisko=\"" + HttpUtility.UrlEncode(this.personList.Rows[i].Cells["Nazwisko"].Value.ToString()) + "\"";
						}
						else
						{
							xml += " nazwisko=\"\"";
						}
						if (this.personList.Rows[i].Cells["Funkcja"] != null && this.personList.Rows[i].Cells["Funkcja"].Value != null)
						{
							xml = xml + " funkcja=\"" + this.personList.Rows[i].Cells["Funkcja"].Value.ToString() + "\"";
						}
						else
						{
							xml += " funkcja=\"\"";
						}
						if (this.personList.Rows[i].Cells["action3"].Value != null)
						{
							xml = xml + " obecny=\"" + this.personList.Rows[i].Cells["action3"].Value.ToString() + "\"";
						}
						else
						{
							xml += " obecny=\"False\"";
						}
						xml += "/>";
					}
					xml += "</komisja_sklad>";
					if (step == 0)
					{
						xml += "<status>podpisany</status>";
						text = xml;
						xml = string.Concat(new string[]
						{
							text,
							"<codeBar><code>",
							this.codeBarCode,
							"</code><id>",
							this.codeBarText,
							"</id></codeBar>"
						});
					}
					if (step == 4)
					{
						xml += "<status>roboczy</status>";
					}
					if (this.codeWarning != "")
					{
						xml = xml + "<hardWarningCode>" + this.codeWarning + "</hardWarningCode>";
					}
				}
				if (step == 1)
				{
					xml += "<step1>";
					foreach (Control c in this.Form1panel.Controls)
					{
						if (c is TextBox || c is MaskedTextBox)
						{
							if (System.Text.RegularExpressions.Regex.IsMatch(c.Name, "^Kandydtat"))
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									" id_kand=\"",
									this.candidatesRule[c.Name],
									"\">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
							else
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
						}
					}
					xml = xml + "<skresleni>" + this.xmlKandydaci + "</skresleni>";
					xml += "</step1>";
					xml += " <status>roboczy</status>";
				}
				if (step == 2)
				{
					xml += "<step1>";
					foreach (Control c in this.Form1panel.Controls)
					{
						if (c is TextBox || c is MaskedTextBox)
						{
							if (System.Text.RegularExpressions.Regex.IsMatch(c.Name, "^Kandydtat"))
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									" id_kand=\"",
									this.candidatesRule[c.Name],
									"\">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
							else
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
						}
					}
					xml = xml + "<skresleni>" + this.xmlKandydaci + "</skresleni>";
					xml += "</step1>";
					xml += "<step2>";
					foreach (Control c in this.Form2panel.Controls)
					{
						if (c is TextBox || c is MaskedTextBox)
						{
							if (System.Text.RegularExpressions.Regex.IsMatch(c.Name, "^Kandydtat"))
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									" id_kand=\"",
									this.candidatesRule[c.Name],
									"\" >",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
							else
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
						}
					}
					xml = xml + "<skresleni>" + this.xmlKandydaci + "</skresleni>";
					xml += "</step2>";
					xml += "<status>roboczy</status>";
				}
				if (step == 3)
				{
					xml += "<step1>";
					foreach (Control c in this.Form1panel.Controls)
					{
						if (c is TextBox || c is MaskedTextBox)
						{
							if (System.Text.RegularExpressions.Regex.IsMatch(c.Name, "^Kandydtat"))
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									" id_kand=\"",
									this.candidatesRule[c.Name],
									"\">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
							else
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
						}
					}
					xml = xml + "<skresleni>" + this.xmlKandydaci + "</skresleni>";
					xml += "</step1>";
					xml += "<step2>";
					foreach (Control c in this.Form2panel.Controls)
					{
						if (c is TextBox || c is MaskedTextBox)
						{
							if (System.Text.RegularExpressions.Regex.IsMatch(c.Name, "^Kandydtat"))
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									" id_kand=\"",
									this.candidatesRule[c.Name],
									"\" >",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
							else
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
						}
					}
					xml = xml + "<skresleni>" + this.xmlKandydaci + "</skresleni>";
					xml += "</step2>";
					xml += "<step3>";
					foreach (Control c in this.SummationPanel.Controls)
					{
						if (c is TextBox || c is MaskedTextBox)
						{
							if (System.Text.RegularExpressions.Regex.IsMatch(c.Name, "^Kandydtat"))
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									" id_kand=\"",
									this.candidatesRule[c.Name],
									"\" >",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
							else
							{
								text = xml;
								xml = string.Concat(new string[]
								{
									text,
									"<",
									c.Name,
									">",
									c.Text,
									"</",
									c.Name,
									">"
								});
							}
						}
					}
					xml = xml + "<skresleni>" + this.xmlKandydaci + "</skresleni>";
					xml += "</step3>";
					xml += "<status>roboczy</status>";
					if (this.codeWarning != "")
					{
						xml = xml + "<hardWarningCode>" + this.codeWarning + "</hardWarningCode>";
					}
				}
				if (step == 0 || step == 4 || step == 3)
				{
					xml += "<report>";
					foreach (Control c in this.raportPanel.Controls)
					{
						if (c is TextBox || c is MaskedTextBox)
						{
							text = xml;
							xml = string.Concat(new string[]
							{
								text,
								"<",
								c.Name,
								">",
								c.Text,
								"</",
								c.Name,
								">"
							});
						}
					}
					xml += "</report>";
				}
				xml += errors;
				xml += "</save>";
				System.IO.StreamWriter sw = new System.IO.StreamWriter(this.savePath, false);
				sw.Write(xml);
				sw.Close();
			}
			catch (System.IO.IOException e)
			{
				this.wait.setVisible(false);
				MessageBox.Show("Nie mozna zapisac pliku. Orginal Error: " + e.Message, "Error");
			}
			catch (System.Exception e2)
			{
				this.wait.setVisible(false);
				MessageBox.Show("Nie mozna zapisac pliku. Orginal Error: " + e2.Message, "Error");
			}
			this.wait.setVisible(false);
		}
		private int isSave()
		{
			XmlNode nodesList = this.save.SelectSingleNode("/save/step");
			int result;
			if (nodesList != null)
			{
				result = System.Convert.ToInt32(nodesList.InnerText);
			}
			else
			{
				result = -1;
			}
			return result;
		}
		private void ProtocolForm_KeyDown(object sender, KeyEventArgs e)
		{
			Control ctl = (Control)sender;
			if (!(this.protocolForm2.BackColor == System.Drawing.SystemColors.GradientInactiveCaption))
			{
				if (base.ActiveControl.Text != "")
				{
					if (e.KeyCode == Keys.Down)
					{
						ctl.SelectNextControl(base.ActiveControl, true, true, true, true);
					}
					if (e.KeyCode == Keys.Return && this.protocolSummation.BackColor != System.Drawing.SystemColors.GradientInactiveCaption)
					{
						ctl.SelectNextControl(base.ActiveControl, true, true, true, true);
					}
					else
					{
						if (e.KeyCode == Keys.Up)
						{
							ctl.SelectNextControl(base.ActiveControl, false, true, true, true);
						}
					}
				}
			}
			else
			{
				if (base.ActiveControl.Text != "")
				{
					if (e.KeyCode == Keys.Down)
					{
						ctl.SelectNextControl(base.ActiveControl, false, true, true, true);
					}
					else
					{
						if (e.KeyCode == Keys.Up)
						{
							ctl.SelectNextControl(base.ActiveControl, true, true, true, true);
						}
					}
					if (e.KeyCode == Keys.Return && this.protocolSummation.BackColor != System.Drawing.SystemColors.GradientInactiveCaption)
					{
						ctl.SelectNextControl(base.ActiveControl, true, true, true, true);
					}
				}
			}
		}
		private new void LostFocus(object sender, System.EventArgs e)
		{
			this.lastControl = (sender as Control);
			if ((sender as Control).Text == "")
			{
				base.ActiveControl = this.lastControl;
			}
		}
		private void Control_SHead_MouseClick(object sender, MouseEventArgs e)
		{
			if (this.currentStep == 1 && this.protocolForm1.BackColor == System.Drawing.SystemColors.GradientInactiveCaption && !this.clicNext)
			{
				this.lastControl.Select();
			}
			if (this.currentStep == 2 && this.protocolForm2.BackColor == System.Drawing.SystemColors.GradientInactiveCaption && !this.clicNext)
			{
				this.lastControl.Select();
			}
			if (this.currentStep == 3 && this.protocolSummation.BackColor == System.Drawing.SystemColors.GradientInactiveCaption && !this.clicNext)
			{
				this.lastControl.Select();
			}
		}
		private void Control_S1_MouseClick(object sender, MouseEventArgs e)
		{
			if (this.currentStep == 1 && !this.clicNext)
			{
				this.lastControl.Select();
			}
		}
		private void Control_S2_MouseClick(object sender, MouseEventArgs e)
		{
			if (this.currentStep == 2 && !this.clicNext)
			{
				this.lastControl.Select();
			}
		}
		private void Control_S3_MouseClick(object sender, MouseEventArgs e)
		{
			if (this.currentStep == 3 && !this.clicNext)
			{
				this.lastControl.Select();
			}
		}
		private void getHeader()
		{
			this.wait.setWaitPanel("Trwa ładowanie nagłówka protokołu", "Proszę czekać");
			this.wait.setVisible(true);
			this.headerField = new System.Collections.Generic.List<string>();
			string title = "Protokół dla ";
			try
			{
				XmlNode nodesList = this.protocolDefinition.SelectSingleNode("/protokol_info");
				XmlNode headerRoot = this.header.SelectSingleNode("/akcja_wyborcza/jns");
				int x = 0;
				int y = 0;
				int fullWidth = this.protocolHeader.Size.Width - 20;
				XmlNode updateData = this.header.SelectSingleNode("/akcja_wyborcza").Attributes.GetNamedItem("data-ost-aktualizacji");
				string[] partfilepath = this.savePath.Split(new char[]
				{
					'\\'
				});
				string[] dataPath = partfilepath[partfilepath.Length - 1].Split(new char[]
				{
					'-'
				});
				this.jns = dataPath[1].Replace("Jns", "");
				this.obwod = dataPath[2].Replace("Obw", "");
				this.inst = dataPath[3].Replace("Inst", "");
				this.okreg = dataPath[5].Replace("Okr", "");
				string[] okreg = this.okreg.Split(new char[]
				{
					' '
				});
				this.okreg = okreg[0].Replace(".xml", "");
				string text = title;
				title = string.Concat(new string[]
				{
					text,
					this.jns,
					" ",
					this.inst,
					": obwod-",
					this.obwod,
					", okręg-",
					this.okreg
				});
				this.Text = title + " (" + Kalkulator1.instalClass.Version.getVersion().ToString() + ")";
				foreach (XmlNode xObwod in headerRoot)
				{
					if (xObwod.Attributes["nr"].InnerText == this.obwod)
					{
						foreach (XmlNode xInst in xObwod)
						{
							if (xInst.Attributes["kod"].InnerText == this.inst && xInst.Attributes["inst_jns"].InnerText == this.instJNS)
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
				foreach (XmlNode node in nodesList)
				{
					XmlNode type = node.Attributes.GetNamedItem("type");
					if (!(type.Value == "header"))
					{
						break;
					}
					foreach (XmlNode box in node)
					{
						foreach (XmlNode item in box)
						{
							if (item.Name == "title")
							{
								x = 0;
								XmlNode bold = item.Attributes.GetNamedItem("bold");
								Label lab = new Label();
								lab.Text = item.InnerText;
								lab.AutoSize = true;
								lab.MaximumSize = new System.Drawing.Size(fullWidth, 0);
								lab.Font = new System.Drawing.Font(this.myfont, 10f);
								lab.Padding = new Padding(10, 0, 10, 0);
								if (bold.Value == "true")
								{
									lab.Font = new System.Drawing.Font(this.myfont, 10f, System.Drawing.FontStyle.Bold);
								}
								lab.Location = new System.Drawing.Point(x, y);
								this.protocolHeader.Controls.Add(lab);
								y += lab.Height + 30;
							}
							if (item.Name == "row")
							{
								x = 0;
								XmlNode scaleCount = item.Attributes.GetNamedItem("scale_count");
								int count = item.ChildNodes.Count;
								if (scaleCount != null)
								{
									try
									{
										int s = System.Convert.ToInt32(scaleCount.Value);
										if (s > count)
										{
											count = s;
										}
									}
									catch (System.Exception)
									{
									}
								}
								int fieldSizeMaster = System.Convert.ToInt32(fullWidth / count);
								int inputSizeMaster = 85;
								int maxHeight = 20;
								for (int i = 0; i < item.ChildNodes.Count; i++)
								{
									XmlNode name = item.ChildNodes[i].Attributes.GetNamedItem("name");
									XmlNode valueName = item.ChildNodes[i].Attributes.GetNamedItem("valueName");
									XmlNode saveAs = item.ChildNodes[i].Attributes.GetNamedItem("save_as");
									XmlNode valueType = item.ChildNodes[i].Attributes.GetNamedItem("valueType");
									XmlNode valueDefault = item.ChildNodes[i].Attributes.GetNamedItem("valueDefault");
									XmlNode enableSystem = item.ChildNodes[i].Attributes.GetNamedItem("enable");
									XmlNode scale = item.ChildNodes[i].Attributes.GetNamedItem("scale");
									int fieldSize = fieldSizeMaster;
									int inputSize = inputSizeMaster;
									int scaleNum = 1;
									if (scale != null)
									{
										try
										{
											scaleNum = System.Convert.ToInt32(scale.Value);
										}
										catch (System.Exception)
										{
										}
									}
									if (scaleNum > 1)
									{
										fieldSize = fieldSizeMaster * scaleNum;
										inputSize = inputSizeMaster * scaleNum;
									}
									Label lab = new Label();
									lab.Name = "Lab_" + valueName.Value;
									lab.Text = name.Value;
									lab.AutoSize = true;
									lab.MaximumSize = new System.Drawing.Size(fieldSize - inputSize, 0);
									lab.Font = new System.Drawing.Font(this.myfont, 9f);
									lab.Location = new System.Drawing.Point(x, y);
									lab.Padding = new Padding(10, 0, 10, 0);
									this.protocolHeader.Controls.Add(lab);
									if (saveAs == null)
									{
										lab.MaximumSize = new System.Drawing.Size(fieldSize, 0);
									}
									if (fieldSize - lab.Size.Width > inputSize)
									{
										inputSize = fieldSize - lab.Size.Width;
									}
									x += fieldSize - inputSize;
									if (lab.Height > maxHeight)
									{
										maxHeight = lab.Height;
									}
									if (valueType != null && saveAs != null)
									{
										if (valueType.Value == "date" || valueType.Value == "time" || valueType.Value == "dateTime")
										{
											MaskedTextBox maskInput = new MaskedTextBox();
											if (fieldSize - lab.Size.Width > 85)
											{
												inputSize = fieldSize - lab.Size.Width;
											}
											maskInput.Size = new System.Drawing.Size(inputSize, 20);
											maskInput.Name = saveAs.Value;
											maskInput.MouseClick += new MouseEventHandler(this.Control_SHead_MouseClick);
											maskInput.LostFocus += new System.EventHandler(this.LostFocus);
											maskInput.Location = new System.Drawing.Point(x, y);
											XmlNode value = this.save.SelectSingleNode("/save/header/" + maskInput.Name);
											if (value != null)
											{
												maskInput.Text = value.InnerText;
											}
											if (valueType.Value == "date")
											{
												maskInput.Mask = "00-00-0000";
												maskInput.Validated += new System.EventHandler(this.date_Validated);
												if (valueDefault != null && valueDefault.Value != "" && System.Text.RegularExpressions.Regex.IsMatch(valueDefault.Value, "^[0-9]{2}-[0-9]{2}-[0-9]{4}$"))
												{
													maskInput.Text = valueDefault.Value;
												}
												if (valueName.Value == "data-ost-aktualizacji" && updateData != null)
												{
													string[] dateData = updateData.Value.Split(new char[]
													{
														' ',
														'-'
													});
													maskInput.Text = string.Concat(new string[]
													{
														dateData[2],
														"-",
														dateData[1],
														"-",
														dateData[0]
													});
												}
												try
												{
													this.typeValidation.Add(maskInput.Name, "date");
												}
												catch (System.ArgumentException)
												{
												}
											}
											if (valueType.Value == "time")
											{
												maskInput.Mask = "00:00";
												maskInput.Validated += new System.EventHandler(this.time_Validated);
												if (valueDefault != null && valueDefault.Value != "" && System.Text.RegularExpressions.Regex.IsMatch(valueDefault.Value, "^[0-9]{2}:[0-9]{2}$"))
												{
													maskInput.Text = valueDefault.Value;
												}
												try
												{
													this.typeValidation.Add(maskInput.Name, "time");
												}
												catch (System.ArgumentException)
												{
												}
											}
											if (valueType.Value == "dateTime")
											{
												maskInput.Mask = "00-00-0000 00:00";
												maskInput.Validated += new System.EventHandler(this.dateTime_Validated);
												if (valueDefault != null && valueDefault.Value != "" && System.Text.RegularExpressions.Regex.IsMatch(valueDefault.Value, "^[0-9]{2}-[0-9]{2}-[0-9]{4} [0-9]{2}:[0-9]{2}$"))
												{
													maskInput.Text = valueDefault.Value;
												}
												try
												{
													this.typeValidation.Add(maskInput.Name, "dateTime");
												}
												catch (System.ArgumentException)
												{
												}
											}
											if (valueDefault != null && valueDefault.Value != "")
											{
												maskInput.Text = valueDefault.Value;
											}
											this.headerField.Add(maskInput.Name);
											this.protocolHeader.Controls.Add(maskInput);
										}
										else
										{
											TextBox input = new TextBox();
											input.Size = new System.Drawing.Size(inputSize, 20);
											input.Name = saveAs.Value;
											input.Location = new System.Drawing.Point(x, y);
											input.MouseClick += new MouseEventHandler(this.Control_SHead_MouseClick);
											input.LostFocus += new System.EventHandler(this.LostFocus);
											if (valueType.Value == "number")
											{
												input.Validated += new System.EventHandler(this.number_Validated);
												this.typeValidation.Add(input.Name, "number");
											}
											if (valueType.Value == "text" || valueType.Value == "char")
											{
												input.Validating += new System.ComponentModel.CancelEventHandler(this.text_Validated);
												try
												{
													this.typeValidation.Add(input.Name, "text");
												}
												catch (System.ArgumentException)
												{
												}
											}
											this.protocolHeader.Controls.Add(input);
											if (valueDefault != null && valueDefault.Value != "")
											{
												input.Text = valueDefault.Value;
											}
											XmlNode value = this.save.SelectSingleNode("/save/header/" + input.Name);
											if (value != null)
											{
												input.Text = value.InnerText;
											}
											else
											{
												if (valueName.Value == "jns_kod" || valueName.Value == "nameGmina" || valueName.Value == "namePowiat" || valueName.Value == "nameWojewodztwo")
												{
													input.Text = headerRoot.Attributes.GetNamedItem(valueName.Value).Value;
												}
												if (valueName.Value == "nr")
												{
													input.Text = this.obwod;
												}
												if (valueName.Value == "algorytmOkreg")
												{
													input.Text = this.okreg;
												}
												if (valueName.Value == "algorytmOKW")
												{
													foreach (XmlNode obw in headerRoot)
													{
														if (obw.Name == "obw" && obw.Attributes.GetNamedItem("nr") != null && obw.Attributes.GetNamedItem("nr").Value == this.obwod)
														{
															foreach (XmlNode institutions in obw)
															{
																if (institutions.Name == "inst" && institutions.Attributes.GetNamedItem("kod") != null && institutions.Attributes.GetNamedItem("kod").Value == this.inst && institutions.Attributes.GetNamedItem("inst_jns") != null && institutions.Attributes.GetNamedItem("inst_jns").Value == this.instJNS)
																{
																	foreach (XmlNode okr in institutions)
																	{
																		if (okr.Name == "okr" && okr.Attributes.GetNamedItem("nr") != null && okr.Attributes.GetNamedItem("nr").Value == this.okreg)
																		{
																			if (okr.Attributes.GetNamedItem("siedziba") != null)
																			{
																				input.Text = okr.Attributes.GetNamedItem("siedziba").Value;
																			}
																		}
																	}
																}
															}
														}
													}
												}
												if (valueName.Value == "algorytmOKW_L")
												{
													foreach (XmlNode obw in headerRoot)
													{
														if (obw.Name == "obw" && obw.Attributes.GetNamedItem("nr") != null && obw.Attributes.GetNamedItem("nr").Value == this.obwod)
														{
															foreach (XmlNode institutions in obw)
															{
																if (institutions.Name == "inst" && institutions.Attributes.GetNamedItem("kod") != null && institutions.Attributes.GetNamedItem("kod").Value == this.inst && institutions.Attributes.GetNamedItem("inst_jns") != null && institutions.Attributes.GetNamedItem("inst_jns").Value == this.instJNS)
																{
																	foreach (XmlNode okr in institutions)
																	{
																		if (okr.Name == "okr" && okr.Attributes.GetNamedItem("nr") != null && okr.Attributes.GetNamedItem("nr").Value == this.okreg)
																		{
																			if (okr.Attributes.GetNamedItem("siedzibaL") != null)
																			{
																				input.Text = okr.Attributes.GetNamedItem("siedzibaL").Value;
																			}
																		}
																	}
																}
															}
														}
													}
												}
												if (valueName.Value == "algorytmOKW_R")
												{
													foreach (XmlNode obw in headerRoot)
													{
														if (obw.Name == "obw" && obw.Attributes.GetNamedItem("nr") != null && obw.Attributes.GetNamedItem("nr").Value == this.obwod)
														{
															foreach (XmlNode institutions in obw)
															{
																if (institutions.Name == "inst" && institutions.Attributes.GetNamedItem("kod") != null && institutions.Attributes.GetNamedItem("kod").Value == this.inst && institutions.Attributes.GetNamedItem("inst_jns") != null && institutions.Attributes.GetNamedItem("inst_jns").Value == this.instJNS)
																{
																	foreach (XmlNode okr in institutions)
																	{
																		if (okr.Name == "okr" && okr.Attributes.GetNamedItem("nr") != null && okr.Attributes.GetNamedItem("nr").Value == this.okreg)
																		{
																			if (okr.Attributes.GetNamedItem("siedzibaR") != null)
																			{
																				input.Text = okr.Attributes.GetNamedItem("siedzibaR").Value;
																			}
																		}
																	}
																}
															}
														}
													}
												}
												if (valueName.Value == "siedzibaObwod")
												{
													foreach (XmlNode s2 in headerRoot)
													{
														if (s2.Attributes.GetNamedItem("nr") != null && s2.Attributes.GetNamedItem("nr").Value == this.obwod)
														{
															if (s2.Attributes.GetNamedItem("siedziba") != null)
															{
																input.Text = s2.Attributes.GetNamedItem("siedziba").Value;
															}
														}
													}
												}
												if (valueName.Value == "liczbamandatow")
												{
													foreach (XmlNode obw in headerRoot)
													{
														if (obw.Name == "obw" && obw.Attributes.GetNamedItem("nr") != null && obw.Attributes.GetNamedItem("nr").Value == this.obwod)
														{
															foreach (XmlNode institutions in obw)
															{
																if (institutions.Name == "inst" && institutions.Attributes.GetNamedItem("kod") != null && institutions.Attributes.GetNamedItem("kod").Value == this.inst && institutions.Attributes.GetNamedItem("inst_jns") != null && institutions.Attributes.GetNamedItem("inst_jns").Value == this.instJNS)
																{
																	foreach (XmlNode okr in institutions)
																	{
																		if (okr.Name == "okr" && okr.Attributes.GetNamedItem("nr") != null && okr.Attributes.GetNamedItem("nr").Value == this.okreg)
																		{
																			if (okr.Attributes.GetNamedItem("lmandatow") != null)
																			{
																				input.Text = okr.Attributes.GetNamedItem("lmandatow").Value;
																			}
																		}
																	}
																}
															}
														}
													}
												}
												if (valueName.Value == "wyborRady")
												{
													foreach (XmlNode obw in headerRoot)
													{
														if (obw.Name == "obw" && obw.Attributes.GetNamedItem("nr") != null && obw.Attributes.GetNamedItem("nr").Value == this.obwod)
														{
															foreach (XmlNode institutions in obw)
															{
																if (institutions.Name == "inst" && institutions.Attributes.GetNamedItem("kod") != null && institutions.Attributes.GetNamedItem("kod").Value == this.inst && institutions.Attributes.GetNamedItem("inst_jns") != null && institutions.Attributes.GetNamedItem("inst_jns").Value == this.instJNS)
																{
																	if (institutions.Attributes.GetNamedItem("nazwaRadyMian") != null)
																	{
																		input.Text = institutions.Attributes.GetNamedItem("nazwaRadyMian").Value;
																	}
																}
															}
														}
													}
												}
												if (valueName.Value == "organNazwa")
												{
													foreach (XmlNode obw in headerRoot)
													{
														if (obw.Name == "obw" && obw.Attributes.GetNamedItem("nr") != null && obw.Attributes.GetNamedItem("nr").Value == this.obwod)
														{
															foreach (XmlNode institutions in obw)
															{
																if (institutions.Name == "inst" && institutions.Attributes.GetNamedItem("kod") != null && institutions.Attributes.GetNamedItem("kod").Value == this.inst && institutions.Attributes.GetNamedItem("inst_jns") != null && institutions.Attributes.GetNamedItem("inst_jns").Value == this.instJNS)
																{
																	if (institutions.Attributes.GetNamedItem("organNazwa") != null)
																	{
																		input.Text = institutions.Attributes.GetNamedItem("organNazwa").Value.ToUpper();
																	}
																}
															}
														}
													}
												}
												if (valueName.Value == "nazwaRadyDopel")
												{
													foreach (XmlNode obw in headerRoot)
													{
														if (obw.Name == "obw" && obw.Attributes.GetNamedItem("nr") != null && obw.Attributes.GetNamedItem("nr").Value == this.obwod)
														{
															foreach (XmlNode institutions in obw)
															{
																if (institutions.Name == "inst" && institutions.Attributes.GetNamedItem("kod") != null && institutions.Attributes.GetNamedItem("kod").Value == this.inst && institutions.Attributes.GetNamedItem("inst_jns") != null && institutions.Attributes.GetNamedItem("inst_jns").Value == this.instJNS)
																{
																	if (institutions.Attributes.GetNamedItem("nazwaRadyDopel") != null)
																	{
																		input.Text = institutions.Attributes.GetNamedItem("nazwaRadyDopel").Value.ToUpper();
																	}
																}
															}
														}
													}
												}
											}
											this.headerField.Add(input.Name);
										}
									}
									if (valueType == null && saveAs != null)
									{
										if (valueName != null)
										{
											TextBox Input = new TextBox();
											Input.Text = "";
											if (fieldSize - lab.Size.Width > 85)
											{
												inputSize = fieldSize - lab.Size.Width;
											}
											else
											{
												inputSize = 85;
											}
											Input.Size = new System.Drawing.Size(inputSize, 20);
											Input.Name = saveAs.Value;
											Input.Location = new System.Drawing.Point(x, y);
											XmlNode value = this.save.SelectSingleNode("/save/header/" + Input.Name);
											if (value != null)
											{
												Input.Text = value.InnerText;
											}
											else
											{
												if (valueName.Value == "jns_kod" || valueName.Value == "nameGmina" || valueName.Value == "namePowiat" || valueName.Value == "nameWojewodztwo")
												{
													Input.Text = headerRoot.Attributes.GetNamedItem(valueName.Value).Value;
												}
												if (valueName.Value == "nr")
												{
													Input.Text = this.obwod;
												}
												if (valueName.Value == "algorytmOkreg")
												{
													Input.Text = this.okreg;
												}
												if (valueName.Value == "algorytmOKW")
												{
													foreach (XmlNode obw in headerRoot)
													{
														if (obw.Name == "obw" && obw.Attributes.GetNamedItem("nr") != null && obw.Attributes.GetNamedItem("nr").Value == this.obwod)
														{
															foreach (XmlNode institutions in obw)
															{
																if (institutions.Name == "inst" && institutions.Attributes.GetNamedItem("kod") != null && institutions.Attributes.GetNamedItem("kod").Value == this.inst && institutions.Attributes.GetNamedItem("inst_jns") != null && institutions.Attributes.GetNamedItem("inst_jns").Value == this.instJNS)
																{
																	foreach (XmlNode okr in institutions)
																	{
																		if (okr.Name == "okr" && okr.Attributes.GetNamedItem("nr") != null && okr.Attributes.GetNamedItem("nr").Value == this.okreg)
																		{
																			if (okr.Attributes.GetNamedItem("siedziba") != null)
																			{
																				Input.Text = okr.Attributes.GetNamedItem("siedziba").Value;
																			}
																		}
																	}
																}
															}
														}
													}
												}
												if (valueName.Value == "algorytmOKW_L")
												{
													foreach (XmlNode obw in headerRoot)
													{
														if (obw.Name == "obw" && obw.Attributes.GetNamedItem("nr") != null && obw.Attributes.GetNamedItem("nr").Value == this.obwod)
														{
															foreach (XmlNode institutions in obw)
															{
																if (institutions.Name == "inst" && institutions.Attributes.GetNamedItem("kod") != null && institutions.Attributes.GetNamedItem("kod").Value == this.inst && institutions.Attributes.GetNamedItem("inst_jns") != null && institutions.Attributes.GetNamedItem("inst_jns").Value == this.instJNS)
																{
																	foreach (XmlNode okr in institutions)
																	{
																		if (okr.Name == "okr" && okr.Attributes.GetNamedItem("nr") != null && okr.Attributes.GetNamedItem("nr").Value == this.okreg)
																		{
																			if (okr.Attributes.GetNamedItem("siedzibaL") != null)
																			{
																				Input.Text = okr.Attributes.GetNamedItem("siedzibaL").Value;
																			}
																		}
																	}
																}
															}
														}
													}
												}
												if (valueName.Value == "algorytmOKW_R")
												{
													foreach (XmlNode obw in headerRoot)
													{
														if (obw.Name == "obw" && obw.Attributes.GetNamedItem("nr") != null && obw.Attributes.GetNamedItem("nr").Value == this.obwod)
														{
															foreach (XmlNode institutions in obw)
															{
																if (institutions.Name == "inst" && institutions.Attributes.GetNamedItem("kod") != null && institutions.Attributes.GetNamedItem("kod").Value == this.inst && institutions.Attributes.GetNamedItem("inst_jns") != null && institutions.Attributes.GetNamedItem("inst_jns").Value == this.instJNS)
																{
																	foreach (XmlNode okr in institutions)
																	{
																		if (okr.Name == "okr" && okr.Attributes.GetNamedItem("nr") != null && okr.Attributes.GetNamedItem("nr").Value == this.okreg)
																		{
																			if (okr.Attributes.GetNamedItem("siedzibaR") != null)
																			{
																				Input.Text = okr.Attributes.GetNamedItem("siedzibaR").Value;
																			}
																		}
																	}
																}
															}
														}
													}
												}
												if (valueName.Value == "siedzibaObwod")
												{
													foreach (XmlNode s2 in headerRoot)
													{
														if (s2.Attributes.GetNamedItem("nr") != null && s2.Attributes.GetNamedItem("nr").Value == this.obwod)
														{
															if (s2.Attributes.GetNamedItem("siedziba") != null)
															{
																Input.Text = s2.Attributes.GetNamedItem("siedziba").Value;
															}
														}
													}
												}
												if (valueName.Value == "liczbamandatow")
												{
													foreach (XmlNode obw in headerRoot)
													{
														if (obw.Name == "obw" && obw.Attributes.GetNamedItem("nr") != null && obw.Attributes.GetNamedItem("nr").Value == this.obwod)
														{
															foreach (XmlNode institutions in obw)
															{
																if (institutions.Name == "inst" && institutions.Attributes.GetNamedItem("kod") != null && institutions.Attributes.GetNamedItem("kod").Value == this.inst && institutions.Attributes.GetNamedItem("inst_jns") != null && institutions.Attributes.GetNamedItem("inst_jns").Value == this.instJNS)
																{
																	foreach (XmlNode okr in institutions)
																	{
																		if (okr.Name == "okr" && okr.Attributes.GetNamedItem("nr") != null && okr.Attributes.GetNamedItem("nr").Value == this.okreg)
																		{
																			if (okr.Attributes.GetNamedItem("lmandatow") != null)
																			{
																				Input.Text = okr.Attributes.GetNamedItem("lmandatow").Value;
																			}
																		}
																	}
																}
															}
														}
													}
												}
												if (valueName.Value == "wyborRady")
												{
													foreach (XmlNode obw in headerRoot)
													{
														if (obw.Name == "obw" && obw.Attributes.GetNamedItem("nr") != null && obw.Attributes.GetNamedItem("nr").Value == this.obwod)
														{
															foreach (XmlNode institutions in obw)
															{
																if (institutions.Name == "inst" && institutions.Attributes.GetNamedItem("kod") != null && institutions.Attributes.GetNamedItem("kod").Value == this.inst && institutions.Attributes.GetNamedItem("inst_jns") != null && institutions.Attributes.GetNamedItem("inst_jns").Value == this.instJNS)
																{
																	if (institutions.Attributes.GetNamedItem("nazwaRadyMian") != null)
																	{
																		Input.Text = institutions.Attributes.GetNamedItem("nazwaRadyMian").Value;
																	}
																}
															}
														}
													}
												}
												if (valueName.Value == "organNazwa")
												{
													foreach (XmlNode obw in headerRoot)
													{
														if (obw.Name == "obw" && obw.Attributes.GetNamedItem("nr") != null && obw.Attributes.GetNamedItem("nr").Value == this.obwod)
														{
															foreach (XmlNode institutions in obw)
															{
																if (institutions.Name == "inst" && institutions.Attributes.GetNamedItem("kod") != null && institutions.Attributes.GetNamedItem("kod").Value == this.inst && institutions.Attributes.GetNamedItem("inst_jns") != null && institutions.Attributes.GetNamedItem("inst_jns").Value == this.instJNS)
																{
																	if (institutions.Attributes.GetNamedItem("organNazwa") != null)
																	{
																		Input.Text = institutions.Attributes.GetNamedItem("organNazwa").Value.ToUpper();
																	}
																}
															}
														}
													}
												}
												if (valueName.Value == "nazwaRadyDopel")
												{
													foreach (XmlNode obw in headerRoot)
													{
														if (obw.Name == "obw" && obw.Attributes.GetNamedItem("nr") != null && obw.Attributes.GetNamedItem("nr").Value == this.obwod)
														{
															foreach (XmlNode institutions in obw)
															{
																if (institutions.Name == "inst" && institutions.Attributes.GetNamedItem("kod") != null && institutions.Attributes.GetNamedItem("kod").Value == this.inst && institutions.Attributes.GetNamedItem("inst_jns") != null && institutions.Attributes.GetNamedItem("inst_jns").Value == this.instJNS)
																{
																	if (institutions.Attributes.GetNamedItem("nazwaRadyDopel") != null)
																	{
																		Input.Text = institutions.Attributes.GetNamedItem("nazwaRadyDopel").Value.ToUpper();
																	}
																}
															}
														}
													}
												}
											}
											Input.Enabled = false;
											if (enableSystem != null && enableSystem.Value == "hand")
											{
												if (fieldSize - lab.Size.Width - 95 > 85)
												{
													inputSize = fieldSize - lab.Size.Width - 95;
												}
												else
												{
													inputSize = 85;
												}
												Input.Size = new System.Drawing.Size(inputSize, 20);
												Button b = new Button();
												b.Name = "btn_" + Input.Name;
												b.Text = "Edytuj";
												b.Size = new System.Drawing.Size(85, 20);
												b.Click += new System.EventHandler(this.EditSystem);
												b.Visible = true;
												b.Location = new System.Drawing.Point(x + (fieldSize - lab.Size.Width - 95), y);
												this.protocolHeader.Controls.Add(Input);
												this.protocolHeader.Controls.Add(b);
												this.headerField.Add(b.Name);
												this.headerField2 = Input.Name;
											}
											else
											{
												this.protocolHeader.Controls.Add(Input);
											}
										}
									}
									x += inputSize;
								}
								y += maxHeight + 5;
							}
						}
						y += 30;
					}
				}
				foreach (XmlNode s2 in headerRoot)
				{
					if (s2.Attributes.GetNamedItem("nr") != null && s2.Attributes.GetNamedItem("nr").Value == this.obwod)
					{
						XmlNode type = s2.Attributes.GetNamedItem("typ_obwodu");
						if (type != null)
						{
							this.typeObw = type.Value;
						}
						foreach (XmlNode ins in s2)
						{
							XmlNode kod = ins.Attributes.GetNamedItem("kod");
							if (kod != null && kod.Value == this.inst && ins.Attributes["inst_jns"].Value == this.instJNS)
							{
								XmlNode nakladXML = ins.Attributes.GetNamedItem("naklad");
								if (nakladXML != null && System.Text.RegularExpressions.Regex.IsMatch(nakladXML.Value, "^[0-9]{1,3}$"))
								{
									int a = System.Convert.ToInt32(nakladXML.Value);
									if (a >= 0 && a <= 100)
									{
										this.naklad = nakladXML.Value;
									}
									else
									{
										this.naklad = "100";
									}
								}
								XmlNode plusminusXML = ins.Attributes.GetNamedItem("plusminus");
								if (plusminusXML != null && System.Text.RegularExpressions.Regex.IsMatch(plusminusXML.Value, "^[0-9]{0,2}$"))
								{
									this.plusminus = plusminusXML.Value;
								}
								else
								{
									this.plusminus = "0";
								}
								XmlNode plusXML = ins.Attributes.GetNamedItem("plus");
								if (plusXML != null && System.Text.RegularExpressions.Regex.IsMatch(plusXML.Value, "^[0-9]+$"))
								{
									this.plus = plusXML.Value;
								}
								else
								{
									this.plus = "0";
								}
								XmlNode minusXML = ins.Attributes.GetNamedItem("minus");
								if (minusXML != null && System.Text.RegularExpressions.Regex.IsMatch(minusXML.Value, "^[0-9]+$"))
								{
									this.minus = minusXML.Value;
								}
								else
								{
									this.minus = "0";
								}
								foreach (XmlNode okr in ins)
								{
									if (okr.Attributes.GetNamedItem("nr") != null && okr.Attributes.GetNamedItem("nr").Value == this.okreg)
									{
										XmlNode typeCorrespondence = okr.Attributes.GetNamedItem("koresp");
										if (typeCorrespondence != null)
										{
											if (typeCorrespondence.Value == "0")
											{
												this.correspondence = false;
											}
											else
											{
												this.correspondence = true;
											}
										}
										XmlNode lwybXML = okr.Attributes.GetNamedItem("lwyb");
										if (lwybXML != null && System.Text.RegularExpressions.Regex.IsMatch(lwybXML.Value, "^[0-9]+$"))
										{
											this.lwyb = lwybXML.Value;
										}
										XmlNode lwybAXML = okr.Attributes.GetNamedItem("lwybA");
										if (lwybAXML != null && System.Text.RegularExpressions.Regex.IsMatch(lwybAXML.Value, "^[0-9]+$"))
										{
											this.lwybA = lwybAXML.Value;
										}
										XmlNode lwybBXML = okr.Attributes.GetNamedItem("lwybB");
										if (lwybBXML != null && System.Text.RegularExpressions.Regex.IsMatch(lwybBXML.Value, "^[0-9]+$"))
										{
											this.lwybB = lwybBXML.Value;
										}
									}
								}
							}
						}
					}
				}
			}
			catch (XmlException e)
			{
				this.wait.setVisible(false);
				MessageBox.Show("Nieprawidłowy XML: " + e.Message, "Błąd");
			}
			catch (System.NullReferenceException e2)
			{
				this.wait.setVisible(false);
				MessageBox.Show("Podanno inny xml niz header. " + e2.Message, "Bład");
			}
			this.setFirstFocus();
			this.wait.setVisible(false);
		}
		private void EditSystem(object sender, System.EventArgs e)
		{
			if (this.protocolForm1.BackColor == System.Drawing.SystemColors.GradientInactiveCaption || this.protocolForm2.BackColor == System.Drawing.SystemColors.GradientInactiveCaption)
			{
				string name = (sender as Button).Name.Replace("btn_", "");
				if (!this.protocolHeader.Controls[name].Enabled)
				{
					this.protocolHeader.Controls[name].Enabled = true;
				}
				else
				{
					this.protocolHeader.Controls[name].Enabled = false;
				}
			}
		}
		private void lockHeader()
		{
			if (this.headerField != null)
			{
				for (int i = 0; i < this.headerField.Count; i++)
				{
					this.protocolHeader.Controls[this.headerField[i]].Enabled = false;
				}
			}
			if (this.headerField2 != null)
			{
				this.protocolHeader.Controls[this.headerField2].Enabled = false;
			}
		}
		private void unlockHeader()
		{
			if (this.headerField != null)
			{
				for (int i = 0; i < this.headerField.Count; i++)
				{
					this.protocolHeader.Controls[this.headerField[i]].Enabled = true;
				}
			}
		}
		private void getCalculator()
		{
			this.wait.setWaitPanel("Trwa ładowanie pierwszego kroku protokołu", "Proszę czekać");
			this.wait.setVisible(true);
			this.Form1panel.TabIndex = 3;
			this.protocolHeader.TabIndex = 2;
			this.buttonNext.Enabled = false;
			int widthLine = 760;
			int placeForButton = 105;
			try
			{
				string pathXML = "";
				if (this.isSave() == 0 || this.isSave() == 4)
				{
					pathXML = "/save/form";
				}
				if (this.isSave() == 1 || this.isSave() == 2 || this.isSave() == 3)
				{
					pathXML = "/save/step1";
				}
				string obwod = "";
				string inst = "";
				string okreg = "";
				string[] dataPath = this.savePath.Split(new char[]
				{
					'-'
				});
				if (dataPath.Length == 6)
				{
					obwod = dataPath[2].Replace("Obw", "");
					inst = dataPath[3].Replace("Inst", "");
					okreg = dataPath[5].Replace("Okr", "");
					okreg = okreg.Replace(".xml", "");
				}
				else
				{
					obwod = dataPath[dataPath.Length - 4].Replace("Obw", "");
					inst = dataPath[dataPath.Length - 3].Replace("Inst", "");
					okreg = dataPath[dataPath.Length - 1].Replace("Okr", "");
					okreg = okreg.Replace(".xml", "");
				}
				XmlNode nodesList = this.protocolDefinition.SelectSingleNode("/protokol_info");
				XmlNode candidatesRoot = this.candidates.SelectSingleNode("/listy");
				int x = 0;
				int y = 0;
				foreach (XmlNode fields in nodesList)
				{
					XmlNode type = fields.Attributes.GetNamedItem("type");
					if (type == null)
					{
						break;
					}
					this.wait.setWaitPanel("Trwa ładowanie pierwszego kroku protokołu - ładowanie pol kalkulatora", "Proszę czekać");
					if (type.Value == "calculator")
					{
						x = 10;
						if (fields.Name == "fields")
						{
							foreach (XmlNode field in fields)
							{
								if (field.Name == "title")
								{
									Label lab = new Label();
									lab.Text = field.InnerText;
									lab.AutoSize = true;
									lab.MaximumSize = new System.Drawing.Size(this.Form1panel.Size.Width - 20, 0);
									lab.Font = new System.Drawing.Font(this.myfont, 10f, System.Drawing.FontStyle.Bold);
									lab.Location = new System.Drawing.Point(x, y);
									this.Form1panel.Controls.Add(lab);
									y += lab.Height + 30;
								}
								if (field.Name == "description")
								{
									x = 10;
									Label lab2 = new Label();
									lab2.Text = field.InnerText;
									lab2.AutoSize = true;
									lab2.MaximumSize = new System.Drawing.Size(this.Form1panel.Size.Width - 20, 0);
									lab2.Font = new System.Drawing.Font(this.myfont, 9f, System.Drawing.FontStyle.Bold);
									lab2.Location = new System.Drawing.Point(x, y - 15);
									this.Form1panel.Controls.Add(lab2);
									y += lab2.Height + 25;
								}
								if (field.Name == "note")
								{
									x = 10;
									Label lab2 = new Label();
									lab2.Text = "Uwaga";
									lab2.AutoSize = true;
									lab2.MaximumSize = new System.Drawing.Size(100, 0);
									lab2.Font = new System.Drawing.Font(this.myfont, 9f, System.Drawing.FontStyle.Bold);
									lab2.Location = new System.Drawing.Point(x, y);
									this.Form1panel.Controls.Add(lab2);
									x += lab2.Width + 5;
									this.Form1panel.Controls.Add(lab2);
									Label lab = new Label();
									lab.Text = field.InnerText;
									lab.AutoSize = true;
									lab.MaximumSize = new System.Drawing.Size(this.Form1panel.Size.Width - 120, 0);
									lab.Font = new System.Drawing.Font(this.myfont, 9f);
									lab.Location = new System.Drawing.Point(x, y);
									this.Form1panel.Controls.Add(lab);
									y += lab.Height + 25;
								}
								if (field.Name == "field")
								{
									string valueText = "";
									bool enab = true;
									XmlNode cor = field.Attributes.GetNamedItem("correspondence");
									XmlNode corValue = field.Attributes.GetNamedItem("elseCorrespondenceDefault");
									System.Collections.IEnumerator enumerator3;
									if (cor != null)
									{
										if ((cor.Value.ToLower() == "false" && this.correspondence) || (cor.Value.ToLower() == "true" && !this.correspondence))
										{
											enab = false;
											if (corValue != null)
											{
												string value = corValue.Value;
												if (value == "pakiet")
												{
													XmlNode headerRoot = this.header.SelectSingleNode("/akcja_wyborcza/jns");
													enumerator3 = headerRoot.GetEnumerator();
													try
													{
														while (enumerator3.MoveNext())
														{
															XmlNode obw = (XmlNode)enumerator3.Current;
															if (obw.Name == "obw" && obw.Attributes.GetNamedItem("nr") != null && obw.Attributes.GetNamedItem("nr").Value == obwod)
															{
																foreach (XmlNode institutions in obw)
																{
																	if (institutions.Name == "inst" && institutions.Attributes.GetNamedItem("kod") != null && institutions.Attributes.GetNamedItem("kod").Value == inst && institutions.Attributes.GetNamedItem("inst_jns") != null && institutions.Attributes.GetNamedItem("inst_jns").Value == this.instJNS)
																	{
																		foreach (XmlNode okr in institutions)
																		{
																			if (okr.Name == "okr" && okr.Attributes.GetNamedItem("nr") != null && okr.Attributes.GetNamedItem("nr").Value == okreg)
																			{
																				if (okr.Attributes.GetNamedItem("koresp") != null)
																				{
																					valueText = okr.Attributes.GetNamedItem("koresp").Value;
																				}
																			}
																		}
																	}
																}
															}
														}
													}
													finally
													{
														System.IDisposable disposable = enumerator3 as System.IDisposable;
														if (disposable != null)
														{
															disposable.Dispose();
														}
													}
												}
												else
												{
													valueText = corValue.Value;
												}
											}
										}
									}
									XmlNode dis = field.Attributes.GetNamedItem("district");
									XmlNode disValue = field.Attributes.GetNamedItem("districtDefault");
									if (dis != null && dis.Value.ToUpper() != "ALL")
									{
										if (System.Text.RegularExpressions.Regex.IsMatch(dis.Value.ToUpper(), "ONLY:"))
										{
											string allow = dis.Value.ToUpper().Replace("ONLY:", "");
											if (allow == this.typeObw.ToUpper())
											{
												enab = false;
												if (disValue != null)
												{
													valueText = disValue.Value;
												}
											}
										}
										if (System.Text.RegularExpressions.Regex.IsMatch(dis.Value.ToUpper(), "EXCLUDE:"))
										{
											string allow = dis.Value.ToUpper().Replace("EXCLUDE:", "");
											if (allow != this.typeObw.ToUpper())
											{
												enab = false;
												if (disValue != null)
												{
													valueText = disValue.Value;
												}
											}
										}
									}
									x = 10;
									Label lab = new Label();
									TextBox inputNum = new TextBox();
									enumerator3 = field.GetEnumerator();
									try
									{
										while (enumerator3.MoveNext())
										{
											XmlNode node = (XmlNode)enumerator3.Current;
											if (node.Name == "name")
											{
												lab.Text = node.ParentNode.Attributes.GetNamedItem("lp").Value + " " + node.InnerText;
												lab.AutoSize = true;
												lab.MaximumSize = new System.Drawing.Size(widthLine - placeForButton, 0);
												lab.Font = new System.Drawing.Font(this.myfont, 9f);
												lab.Location = new System.Drawing.Point(x, y);
												x = widthLine - 95;
											}
											if (node.Name == "save_as")
											{
												inputNum.Size = new System.Drawing.Size(85, 20);
												inputNum.Name = node.InnerText;
												inputNum.Enabled = enab;
												inputNum.Text = valueText;
												XmlNode value2 = this.save.SelectSingleNode(pathXML + "/" + node.InnerText);
												if (value2 != null && enab)
												{
													inputNum.Text = value2.InnerText;
												}
												inputNum.MouseClick += new MouseEventHandler(this.Control_S1_MouseClick);
												inputNum.LostFocus += new System.EventHandler(this.LostFocus);
												inputNum.Location = new System.Drawing.Point(x, y);
												inputNum.CausesValidation = true;
												XmlNode valid = node.ParentNode.Attributes.GetNamedItem("data");
												if (valid != null && valid.Value == "number")
												{
													inputNum.Validated += new System.EventHandler(this.number_Validated);
													try
													{
														this.typeValidation.Add(inputNum.Name, "number");
													}
													catch (System.ArgumentException)
													{
													}
												}
												if (valid != null && valid.Value == "text")
												{
													inputNum.Validated += new System.EventHandler(this.number_Validated);
													try
													{
														this.typeValidation.Add(inputNum.Name, "text");
													}
													catch (System.ArgumentException)
													{
													}
												}
												lab.Name = "Lab_" + node.InnerText;
												XmlNode min = node.ParentNode.Attributes.GetNamedItem("min");
												XmlNode max = node.ParentNode.Attributes.GetNamedItem("max");
												if (min != null && max != null)
												{
													this.range.Add(inputNum.Name, new ValidationRange(inputNum.Name, System.Convert.ToInt32(min.Value), System.Convert.ToInt32(max.Value)));
												}
												if (min != null && max == null)
												{
													this.range.Add(inputNum.Name, new ValidationRange(inputNum.Name, System.Convert.ToInt32(min.Value)));
												}
											}
										}
									}
									finally
									{
										System.IDisposable disposable = enumerator3 as System.IDisposable;
										if (disposable != null)
										{
											disposable.Dispose();
										}
									}
									this.Form1panel.Controls.Add(lab);
									this.Form1panel.Controls.Add(inputNum);
									if (lab.Height > 20)
									{
										y += lab.Height + 5;
									}
									else
									{
										y += 25;
									}
								}
							}
						}
						y += 50;
					}
					this.wait.setWaitPanel("Trwa ładowanie pierwszego kroku protokołu - ładowanie kandydatów", "Proszę czekać");
					if (type != null && type.Value == "additional-calculator")
					{
						foreach (XmlNode field in fields)
						{
							if (field.Name == "field")
							{
								XmlNode data = field.Attributes.GetNamedItem("data");
								if (data != null && data.Value == "kandydaci")
								{
									foreach (XmlNode node in field)
									{
										if (node.Name == "name")
										{
											x = 10;
											Label lab2 = new Label();
											lab2.Text = node.ParentNode.Attributes.GetNamedItem("lp").Value + " " + node.InnerText;
											lab2.AutoSize = true;
											lab2.MaximumSize = new System.Drawing.Size(this.Form2panel.Width, 0);
											lab2.MinimumSize = new System.Drawing.Size(this.Form2panel.Width, 0);
											lab2.Font = new System.Drawing.Font(this.myfont, 9f);
											lab2.Location = new System.Drawing.Point(x, y);
											this.Form1panel.Controls.Add(lab2);
											y += lab2.Size.Height + 30;
										}
										if (field.Name == "note")
										{
											x = 10;
											Label lab2 = new Label();
											lab2.Text = "Uwaga";
											lab2.AutoSize = true;
											lab2.MaximumSize = new System.Drawing.Size(100, 0);
											lab2.Font = new System.Drawing.Font(this.myfont, 9f, System.Drawing.FontStyle.Bold);
											lab2.Location = new System.Drawing.Point(x, y);
											this.Form2panel.Controls.Add(lab2);
											x += lab2.Width + 5;
											this.Form1panel.Controls.Add(lab2);
											Label lab = new Label();
											lab.Text = field.InnerText;
											lab.AutoSize = true;
											lab.MaximumSize = new System.Drawing.Size(700, 0);
											lab.Font = new System.Drawing.Font(this.myfont, 9f);
											lab.Location = new System.Drawing.Point(x, y);
											this.Form1panel.Controls.Add(lab);
											y += lab.Height + 25;
										}
										if (node.Name == "patternrows")
										{
											System.Collections.Generic.List<Field> patternField = new System.Collections.Generic.List<Field>();
											patternField = this.readPatternCandidate(node, patternField);
											int save_as_candidate = 1;
											string idcandidate = "";
											int countListaItem = 0;
											foreach (XmlNode lista in candidatesRoot)
											{
												countListaItem++;
												Label lab = new Label();
												TextBox inputNum = new TextBox();
												string statustTMP = "A";
												for (int i = 0; i < patternField.Count; i++)
												{
													string status = "A";
													if (patternField[i].getStatus() == patternField[i].getStatus().Replace("parent:", ""))
													{
														if (lista.FirstChild.Attributes.GetNamedItem(patternField[i].getStatus()) != null)
														{
															status = lista.FirstChild.Attributes.GetNamedItem(patternField[i].getStatus()).Value;
														}
													}
													else
													{
														string label = patternField[i].getStatus().Replace("parent:", "");
														if (lista.Attributes.GetNamedItem(label) != null)
														{
															status = lista.Attributes.GetNamedItem(label).Value;
														}
													}
													statustTMP = status;
													if (status == "A")
													{
														string imie = "";
														string imie2 = "";
														string nazwisko = "";
														string name2 = "";
														string komitet = "";
														string listaText = "";
														if (patternField[i].getDisplay() == patternField[i].getDisplay().Replace("parent:", ""))
														{
															if (lista.FirstChild.Attributes.GetNamedItem(patternField[i].getDisplay()) != null)
															{
																string isDisplay = lista.FirstChild.Attributes.GetNamedItem(patternField[i].getDisplay()).Value;
															}
														}
														else
														{
															string label = patternField[i].getDisplay().Replace("parent:", "");
															if (lista.Attributes.GetNamedItem(label) != null)
															{
																string isDisplay = lista.Attributes.GetNamedItem(label).Value;
															}
														}
														if (patternField[i].getIdCandidate() == patternField[i].getIdCandidate().Replace("parent:", ""))
														{
															if (lista.FirstChild.Attributes.GetNamedItem(patternField[i].getIdCandidate()) != null)
															{
																idcandidate = lista.FirstChild.Attributes.GetNamedItem(patternField[i].getIdCandidate()).Value;
															}
														}
														else
														{
															string label = patternField[i].getIdCandidate().Replace("parent:", "");
															if (lista.Attributes.GetNamedItem(label) != null)
															{
																idcandidate = lista.Attributes.GetNamedItem(label).Value;
															}
														}
														if (patternField[i].getImie2() == patternField[i].getImie2().Replace("parent:", ""))
														{
															if (lista.FirstChild.Attributes.GetNamedItem(patternField[i].getImie2()) != null)
															{
																imie2 = lista.FirstChild.Attributes.GetNamedItem(patternField[i].getImie2()).Value;
															}
														}
														else
														{
															string label = patternField[i].getImie2().Replace("parent:", "");
															if (lista.Attributes.GetNamedItem(label) != null)
															{
																imie2 = lista.Attributes.GetNamedItem(label).Value;
															}
														}
														if (patternField[i].getImie1() == patternField[i].getImie1().Replace("parent:", ""))
														{
															if (lista.FirstChild.Attributes.GetNamedItem(patternField[i].getImie1()) != null)
															{
																imie = lista.FirstChild.Attributes.GetNamedItem(patternField[i].getImie1()).Value;
															}
														}
														else
														{
															string label = patternField[i].getImie1().Replace("parent:", "");
															if (lista.Attributes.GetNamedItem(label) != null)
															{
																imie = lista.Attributes.GetNamedItem(label).Value;
															}
														}
														if (patternField[i].getNazwisko() == patternField[i].getNazwisko().Replace("parent:", ""))
														{
															if (lista.FirstChild.Attributes.GetNamedItem(patternField[i].getNazwisko()) != null)
															{
																nazwisko = lista.FirstChild.Attributes.GetNamedItem(patternField[i].getNazwisko()).Value;
															}
														}
														else
														{
															string label = patternField[i].getNazwisko().Replace("parent:", "");
															if (lista.Attributes.GetNamedItem(label) != null)
															{
																nazwisko = lista.Attributes.GetNamedItem(label).Value;
															}
														}
														if (patternField[i].getKomitet() == patternField[i].getKomitet().Replace("parent:", ""))
														{
															if (lista.FirstChild.Attributes.GetNamedItem(patternField[i].getKomitet()) != null)
															{
																komitet = lista.FirstChild.Attributes.GetNamedItem(patternField[i].getKomitet()).Value;
															}
														}
														else
														{
															string label = patternField[i].getKomitet().Replace("parent:", "");
															if (lista.Attributes.GetNamedItem(label) != null)
															{
																komitet = lista.Attributes.GetNamedItem(label).Value;
															}
														}
														string nrLista = "";
														if (patternField[i].getLista() == patternField[i].getLista().Replace("parent:", ""))
														{
															if (lista.FirstChild.Attributes.GetNamedItem(patternField[i].getLista()) != null)
															{
																nrLista = lista.FirstChild.Attributes.GetNamedItem(patternField[i].getLista()).Value;
															}
														}
														else
														{
															string label = patternField[i].getLista().Replace("parent:", "");
															if (lista.Attributes.GetNamedItem(label) != null)
															{
																nrLista = lista.Attributes.GetNamedItem(label).Value;
															}
														}
														if (nrLista != "")
														{
															listaText = patternField[i].getName3() + nrLista;
														}
														if (patternField[i].getPlec() == patternField[i].getPlec().Replace("parent:", ""))
														{
															if (lista.FirstChild.Attributes.GetNamedItem(patternField[i].getPlec()) != null)
															{
																if (lista.FirstChild.Attributes.GetNamedItem(patternField[i].getPlec()).Value.ToUpper() == "M")
																{
																	name2 = patternField[i].getName2();
																}
																else
																{
																	name2 = patternField[i].getName2v2();
																}
															}
														}
														else
														{
															string label = patternField[i].getPlec().Replace("parent:", "");
															if (lista.Attributes.GetNamedItem(label) != null)
															{
																if (lista.Attributes.GetNamedItem(label).Value.ToUpper() == "M")
																{
																	name2 = patternField[i].getName2();
																}
																else
																{
																	name2 = patternField[i].getName2v2();
																}
															}
														}
														string text = string.Concat(new string[]
														{
															patternField[i].getName1(),
															" ",
															nazwisko,
															" ",
															imie,
															" ",
															imie2,
															" ",
															name2,
															" ",
															komitet,
															listaText
														});
														if (patternField[i].getDataType() == "text" && patternField[i].getSave() == "")
														{
															x = 10;
															lab.Text = text;
															lab.AutoSize = true;
															lab.MaximumSize = new System.Drawing.Size(widthLine - 105, 0);
															lab.Font = new System.Drawing.Font(this.myfont, 9f);
															lab.Location = new System.Drawing.Point(x, y);
															x = widthLine - 95;
															this.Form1panel.Controls.Add(lab);
														}
														if (patternField[i].getDataType() == "number" && patternField[i].getSave() != "")
														{
															inputNum.Size = new System.Drawing.Size(85, 20);
															inputNum.Name = patternField[i].getSave().Replace("X", save_as_candidate.ToString());
															inputNum.Location = new System.Drawing.Point(x, y);
															inputNum.Validated += new System.EventHandler(this.number_Validated);
															inputNum.MouseClick += new MouseEventHandler(this.Control_S1_MouseClick);
															inputNum.LostFocus += new System.EventHandler(this.LostFocus);
															try
															{
																this.typeValidation.Add(inputNum.Name, "number");
															}
															catch (System.ArgumentException)
															{
															}
															XmlNode value2 = this.save.SelectSingleNode(pathXML + "/" + inputNum.Name);
															if (value2 != null)
															{
																inputNum.Text = value2.InnerText;
															}
															if (!(patternField[i].getDisplay().ToLower() == "false"))
															{
																this.Form1panel.Controls.Add(inputNum);
															}
															if (lab.Height > 20)
															{
																y += lab.Height + 5;
															}
															else
															{
																y += 25;
															}
															try
															{
																this.range.Add(inputNum.Name, patternField[i].getRange(inputNum.Name));
															}
															catch (System.Exception)
															{
															}
															this.candidatesRule[inputNum.Name] = idcandidate;
															idcandidate = "";
														}
													}
													if (status == "S")
													{
														if (patternField[i].getIdCandidate() == patternField[i].getIdCandidate().Replace("parent:", ""))
														{
															if (lista.FirstChild.Attributes.GetNamedItem(patternField[i].getIdCandidate()) != null)
															{
																idcandidate = lista.FirstChild.Attributes.GetNamedItem(patternField[i].getIdCandidate()).Value;
															}
														}
														else
														{
															string label = patternField[i].getIdCandidate().Replace("parent:", "");
															if (lista.Attributes.GetNamedItem(label) != null)
															{
																idcandidate = lista.Attributes.GetNamedItem(label).Value;
															}
														}
														if (patternField[i].getDataType() == "number" && patternField[i].getSave() != "")
														{
															string name3 = patternField[i].getSave().Replace("X", "S" + countListaItem.ToString());
															string text2 = this.xmlKandydaci;
															this.xmlKandydaci = string.Concat(new string[]
															{
																text2,
																"<",
																name3,
																" id_kand=\"",
																idcandidate,
																"\">X</",
																name3,
																">"
															});
															idcandidate = "";
														}
													}
												}
												if (statustTMP == "A")
												{
													save_as_candidate++;
												}
											}
											this.countcandidatesoflist.Add(new int[]
											{
												0,
												save_as_candidate - 1
											});
										}
										if (node.Name == "patternrow")
										{
											System.Collections.Generic.List<Field> patternField = new System.Collections.Generic.List<Field>();
											patternField = this.readPatternCandidate(node, patternField);
											for (int i = 0; i < patternField.Count; i++)
											{
												if (!patternField[i].needImportData())
												{
													x = 10;
													Label lab = new Label();
													lab.Text = patternField[i].getName1();
													lab.AutoSize = true;
													lab.MaximumSize = new System.Drawing.Size(widthLine - 105, 0);
													lab.Font = new System.Drawing.Font(this.myfont, 9f);
													lab.Location = new System.Drawing.Point(x, y);
													x = widthLine - 95;
													this.Form1panel.Controls.Add(lab);
													TextBox inputNum = new TextBox();
													inputNum.Size = new System.Drawing.Size(85, 20);
													inputNum.Name = patternField[i].getSave();
													inputNum.Location = new System.Drawing.Point(x, y);
													inputNum.MouseClick += new MouseEventHandler(this.Control_S1_MouseClick);
													inputNum.LostFocus += new System.EventHandler(this.LostFocus);
													inputNum.Validated += new System.EventHandler(this.number_Validated);
													try
													{
														this.typeValidation.Add(inputNum.Name, "number");
													}
													catch (System.ArgumentException)
													{
													}
													XmlNode value2 = this.save.SelectSingleNode(pathXML + "/" + inputNum.Name);
													if (value2 != null)
													{
														inputNum.Text = value2.InnerText;
													}
													this.Form1panel.Controls.Add(inputNum);
													if (lab.Height > 20)
													{
														y += lab.Height + 5;
													}
													else
													{
														y += 25;
													}
													this.range.Add(inputNum.Name, patternField[i].getRange(inputNum.Name));
												}
											}
										}
										if (node.Name == "patternlist")
										{
											int nrListy = 1;
											int save_as_candidate = 1;
											foreach (XmlNode lista in candidatesRoot)
											{
												XmlNode Lstatu = lista.Attributes.GetNamedItem("lista_status");
												if (Lstatu != null && Lstatu.Value == "R")
												{
													foreach (XmlNode paternNode in node)
													{
														if (paternNode.Name == "title")
														{
															x = 0;
															XmlNode bold = paternNode.Attributes.GetNamedItem("bold");
															XmlNode nr = paternNode.Attributes.GetNamedItem("nr");
															XmlNode komitet2 = paternNode.Attributes.GetNamedItem("komitet");
															XmlNode nrListyVal = paternNode.Attributes.GetNamedItem("lista");
															string text = "";
															if (komitet2 != null)
															{
																XmlNode komitetText = lista.Attributes.GetNamedItem(komitet2.Value);
																if (komitetText != null && komitetText.Value != "")
																{
																	text = komitetText.Value;
																}
															}
															string valL = "";
															if (nrListyVal != null)
															{
																XmlNode nrListyValText = lista.Attributes.GetNamedItem(nrListyVal.Value);
																if (nrListyValText != null && nrListyValText.Value != "")
																{
																	valL = nrListyValText.Value;
																}
															}
															if (nr != null)
															{
																text = string.Concat(new string[]
																{
																	nr.Value,
																	" ",
																	valL,
																	" ",
																	paternNode.InnerText,
																	text
																});
															}
															else
															{
																text = paternNode.InnerText;
															}
															Label lab = new Label();
															lab.Text = text;
															lab.AutoSize = true;
															lab.MaximumSize = new System.Drawing.Size(this.Form1panel.Size.Width, 0);
															lab.Font = new System.Drawing.Font(this.myfont, 10f);
															lab.Padding = new Padding(10, 0, 10, 0);
															if (bold != null && bold.Value == "true")
															{
																lab.Font = new System.Drawing.Font(this.myfont, 10f, System.Drawing.FontStyle.Bold);
															}
															lab.Location = new System.Drawing.Point(x, y);
															this.Form1panel.Controls.Add(lab);
															y += lab.Size.Height + 30;
														}
														if (paternNode.Name == "patternrow")
														{
															System.Collections.Generic.List<Field> patternField = new System.Collections.Generic.List<Field>();
															patternField = this.readPatternCandidate(paternNode, patternField);
															for (int i = 0; i < patternField.Count; i++)
															{
																if (!patternField[i].needImportData())
																{
																	x = 10;
																	Label lab = new Label();
																	lab.Text = patternField[i].getName1();
																	lab.AutoSize = true;
																	lab.MaximumSize = new System.Drawing.Size(this.Form2panel.Width - 105, 0);
																	lab.Font = new System.Drawing.Font(this.myfont, 9f);
																	lab.Location = new System.Drawing.Point(x, y);
																	x = this.Form2panel.Width - 95;
																	this.Form1panel.Controls.Add(lab);
																	TextBox inputNum = new TextBox();
																	inputNum.Size = new System.Drawing.Size(85, 20);
																	inputNum.Name = patternField[i].getSave().Replace("Y", nrListy.ToString());
																	inputNum.MouseClick += new MouseEventHandler(this.Control_S1_MouseClick);
																	inputNum.LostFocus += new System.EventHandler(this.LostFocus);
																	XmlNode value2 = this.save.SelectSingleNode(pathXML + "/" + inputNum.Name);
																	if (value2 != null)
																	{
																		inputNum.Text = value2.InnerText;
																	}
																	inputNum.Location = new System.Drawing.Point(x, y);
																	inputNum.Validated += new System.EventHandler(this.number_Validated);
																	try
																	{
																		this.typeValidation.Add(inputNum.Name, "number");
																	}
																	catch (System.Exception ex_261F)
																	{
																	}
																	try
																	{
																		this.Form1panel.Controls.Add(inputNum);
																	}
																	catch (System.Exception ex_261F)
																	{
																	}
																	if (lab.Height > 20)
																	{
																		y += lab.Height + 5;
																	}
																	else
																	{
																		y += 25;
																	}
																	try
																	{
																		this.range.Add(inputNum.Name, patternField[i].getRange(inputNum.Name));
																	}
																	catch (System.Exception)
																	{
																	}
																}
															}
														}
														if (paternNode.Name == "patternrows")
														{
															System.Collections.Generic.List<Field> patternField = new System.Collections.Generic.List<Field>();
															patternField = this.readPatternCandidate(paternNode, patternField);
															save_as_candidate = 1;
															for (int j = 0; j < lista.ChildNodes.Count; j++)
															{
																string idcandidate = "";
																Label lab = new Label();
																TextBox inputNum = new TextBox();
																string statustTMP = "A";
																for (int i = 0; i < patternField.Count; i++)
																{
																	string status = "A";
																	if (patternField[i].getStatus() == patternField[i].getStatus().Replace("parent:", ""))
																	{
																		if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getStatus()) != null)
																		{
																			status = lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getStatus()).Value;
																		}
																	}
																	else
																	{
																		string label = patternField[i].getStatus().Replace("parent:", "");
																		if (lista.Attributes.GetNamedItem(label) != null)
																		{
																			status = lista.Attributes.GetNamedItem(label).Value;
																		}
																	}
																	statustTMP = status;
																	if (status == "A")
																	{
																		string imie = "";
																		string imie2 = "";
																		string nazwisko = "";
																		string komitet = "";
																		string name2 = "";
																		if (patternField[i].getDisplay() == patternField[i].getDisplay().Replace("parent:", ""))
																		{
																			if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getDisplay()) != null)
																			{
																				string isDisplay = lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getDisplay()).Value;
																			}
																		}
																		else
																		{
																			string label = patternField[i].getDisplay().Replace("parent:", "");
																			if (lista.Attributes.GetNamedItem(label) != null)
																			{
																				string isDisplay = lista.Attributes.GetNamedItem(label).Value;
																			}
																		}
																		if (patternField[i].getIdCandidate() == patternField[i].getIdCandidate().Replace("parent:", ""))
																		{
																			if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getIdCandidate()) != null)
																			{
																				idcandidate = lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getIdCandidate()).Value;
																			}
																		}
																		else
																		{
																			string label = patternField[i].getIdCandidate().Replace("parent:", "");
																			if (lista.Attributes.GetNamedItem(label) != null)
																			{
																				idcandidate = lista.Attributes.GetNamedItem(label).Value;
																			}
																		}
																		if (patternField[i].getImie2() == patternField[i].getImie2().Replace("parent:", ""))
																		{
																			if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getImie2()) != null)
																			{
																				imie2 = lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getImie2()).Value;
																			}
																		}
																		else
																		{
																			string label = patternField[i].getImie2().Replace("parent:", "");
																			if (lista.Attributes.GetNamedItem(label) != null)
																			{
																				imie2 = lista.Attributes.GetNamedItem(label).Value;
																			}
																		}
																		if (patternField[i].getImie1() == patternField[i].getImie1().Replace("parent:", ""))
																		{
																			if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getImie1()) != null)
																			{
																				imie = lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getImie1()).Value;
																			}
																		}
																		else
																		{
																			string label = patternField[i].getImie1().Replace("parent:", "");
																			if (lista.Attributes.GetNamedItem(label) != null)
																			{
																				imie = lista.Attributes.GetNamedItem(label).Value;
																			}
																		}
																		if (patternField[i].getNazwisko() == patternField[i].getNazwisko().Replace("parent:", ""))
																		{
																			if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getNazwisko()) != null)
																			{
																				nazwisko = lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getNazwisko()).Value;
																			}
																		}
																		else
																		{
																			string label = patternField[i].getNazwisko().Replace("parent:", "");
																			if (lista.Attributes.GetNamedItem(label) != null)
																			{
																				nazwisko = lista.Attributes.GetNamedItem(label).Value;
																			}
																		}
																		if (patternField[i].getKomitet() == patternField[i].getKomitet().Replace("parent:", ""))
																		{
																			if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getKomitet()) != null)
																			{
																				komitet = lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getKomitet()).Value;
																			}
																		}
																		else
																		{
																			string label = patternField[i].getKomitet().Replace("parent:", "");
																			if (lista.Attributes.GetNamedItem(label) != null)
																			{
																				komitet = lista.Attributes.GetNamedItem(label).Value;
																			}
																		}
																		if (patternField[i].getPlec() == patternField[i].getPlec().Replace("parent:", ""))
																		{
																			if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getPlec()) != null)
																			{
																				if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getPlec()).Value.ToUpper() == "M")
																				{
																					name2 = patternField[i].getName2();
																				}
																				else
																				{
																					name2 = patternField[i].getName2v2();
																				}
																			}
																		}
																		else
																		{
																			string label = patternField[i].getPlec().Replace("parent:", "");
																			if (lista.Attributes.GetNamedItem(label) != null)
																			{
																				if (lista.Attributes.GetNamedItem(label).Value.ToUpper() == "M")
																				{
																					name2 = patternField[i].getName2();
																				}
																				else
																				{
																					name2 = patternField[i].getName2v2();
																				}
																			}
																		}
																		string text = string.Concat(new string[]
																		{
																			patternField[i].getName1(),
																			" ",
																			nazwisko,
																			" ",
																			imie,
																			" ",
																			imie2,
																			" ",
																			name2,
																			" ",
																			komitet
																		});
																		if (patternField[i].getDataType() == "text" && patternField[i].getSave() == "")
																		{
																			x = 10;
																			lab.Text = text;
																			lab.AutoSize = true;
																			lab.MaximumSize = new System.Drawing.Size(this.Form2panel.Width - 105, 0);
																			lab.Font = new System.Drawing.Font(this.myfont, 9f);
																			lab.Location = new System.Drawing.Point(x, y);
																			x = this.Form2panel.Width - 95;
																			this.Form1panel.Controls.Add(lab);
																		}
																		if (patternField[i].getDataType() == "number" && patternField[i].getSave() != "")
																		{
																			inputNum.Size = new System.Drawing.Size(85, 20);
																			string nameFI = patternField[i].getSave().Replace("X", save_as_candidate.ToString()).Replace("Y", nrListy.ToString());
																			inputNum.Name = nameFI;
																			XmlNode value2 = this.save.SelectSingleNode(pathXML + "/" + inputNum.Name);
																			if (value2 != null)
																			{
																				inputNum.Text = value2.InnerText;
																			}
																			inputNum.Location = new System.Drawing.Point(x, y);
																			inputNum.MouseClick += new MouseEventHandler(this.Control_S1_MouseClick);
																			inputNum.LostFocus += new System.EventHandler(this.LostFocus);
																			inputNum.Validated += new System.EventHandler(this.number_Validated);
																			try
																			{
																				this.typeValidation.Add(inputNum.Name, "number");
																			}
																			catch (System.Exception)
																			{
																			}
																			if (!(patternField[i].getDisplay().ToLower() == "false"))
																			{
																				try
																				{
																					this.Form1panel.Controls.Add(inputNum);
																				}
																				catch (System.Exception)
																				{
																				}
																			}
																			if (lab.Height > 20)
																			{
																				y += lab.Height + 5;
																			}
																			else
																			{
																				y += 25;
																			}
																			try
																			{
																				this.range.Add(inputNum.Name, patternField[i].getRange(inputNum.Name));
																			}
																			catch (System.Exception)
																			{
																			}
																			this.candidatesRule[inputNum.Name] = idcandidate;
																			idcandidate = "";
																		}
																	}
																	if (status == "S")
																	{
																		if (patternField[i].getIdCandidate() == patternField[i].getIdCandidate().Replace("parent:", ""))
																		{
																			if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getIdCandidate()) != null)
																			{
																				idcandidate = lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getIdCandidate()).Value;
																			}
																		}
																		else
																		{
																			string label = patternField[i].getIdCandidate().Replace("parent:", "");
																			if (lista.Attributes.GetNamedItem(label) != null)
																			{
																				idcandidate = lista.Attributes.GetNamedItem(label).Value;
																			}
																		}
																		if (patternField[i].getDataType() == "number" && patternField[i].getSave() != "")
																		{
																			string name3 = patternField[i].getSave().Replace("X", "S" + j.ToString()).Replace("Y", nrListy.ToString());
																			string text2 = this.xmlKandydaci;
																			this.xmlKandydaci = string.Concat(new string[]
																			{
																				text2,
																				"<",
																				name3,
																				" id_kand=\"",
																				idcandidate,
																				"\">X</",
																				name3,
																				">"
																			});
																			idcandidate = "";
																		}
																	}
																}
																if (statustTMP == "A")
																{
																	save_as_candidate++;
																}
															}
															this.countcandidatesoflist.Add(new int[]
															{
																nrListy,
																save_as_candidate - 1
															});
														}
													}
													nrListy++;
													y += 30;
												}
												if (Lstatu != null && Lstatu.Value == "U")
												{
													foreach (XmlNode paternNode in node)
													{
														if (paternNode.Name == "patternrow")
														{
															System.Collections.Generic.List<Field> patternField = new System.Collections.Generic.List<Field>();
															patternField = this.readPatternCandidate(paternNode, patternField);
															for (int i = 0; i < patternField.Count; i++)
															{
																if (!patternField[i].needImportData())
																{
																	string name3 = patternField[i].getSave().Replace("Y", nrListy.ToString());
																	string text2 = this.xmlKandydaci;
																	this.xmlKandydaci = string.Concat(new string[]
																	{
																		text2,
																		"<",
																		name3,
																		">X</",
																		name3,
																		">"
																	});
																}
															}
														}
														if (paternNode.Name == "patternrows")
														{
															System.Collections.Generic.List<Field> patternField = new System.Collections.Generic.List<Field>();
															patternField = this.readPatternCandidate(paternNode, patternField);
															save_as_candidate = 1;
															for (int j = 0; j < lista.ChildNodes.Count; j++)
															{
																string idcandidate = "";
																Label lab = new Label();
																TextBox inputNum = new TextBox();
																string statustTMP = "A";
																for (int i = 0; i < patternField.Count; i++)
																{
																	string status = "A";
																	if (patternField[i].getStatus() == patternField[i].getStatus().Replace("parent:", ""))
																	{
																		if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getStatus()) != null)
																		{
																			status = lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getStatus()).Value;
																		}
																	}
																	else
																	{
																		string label = patternField[i].getStatus().Replace("parent:", "");
																		if (lista.Attributes.GetNamedItem(label) != null)
																		{
																			status = lista.Attributes.GetNamedItem(label).Value;
																		}
																	}
																	statustTMP = status;
																	if (status == "A")
																	{
																		string imie = "";
																		string imie2 = "";
																		string nazwisko = "";
																		string komitet = "";
																		string name2 = "";
																		if (patternField[i].getDisplay() == patternField[i].getDisplay().Replace("parent:", ""))
																		{
																			if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getDisplay()) != null)
																			{
																				string isDisplay = lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getDisplay()).Value;
																			}
																		}
																		else
																		{
																			string label = patternField[i].getDisplay().Replace("parent:", "");
																			if (lista.Attributes.GetNamedItem(label) != null)
																			{
																				string isDisplay = lista.Attributes.GetNamedItem(label).Value;
																			}
																		}
																		if (patternField[i].getIdCandidate() == patternField[i].getIdCandidate().Replace("parent:", ""))
																		{
																			if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getIdCandidate()) != null)
																			{
																				idcandidate = lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getIdCandidate()).Value;
																			}
																		}
																		else
																		{
																			string label = patternField[i].getIdCandidate().Replace("parent:", "");
																			if (lista.Attributes.GetNamedItem(label) != null)
																			{
																				idcandidate = lista.Attributes.GetNamedItem(label).Value;
																			}
																		}
																		if (patternField[i].getImie2() == patternField[i].getImie2().Replace("parent:", ""))
																		{
																			if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getImie2()) != null)
																			{
																				imie2 = lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getImie2()).Value;
																			}
																		}
																		else
																		{
																			string label = patternField[i].getImie2().Replace("parent:", "");
																			if (lista.Attributes.GetNamedItem(label) != null)
																			{
																				imie2 = lista.Attributes.GetNamedItem(label).Value;
																			}
																		}
																		if (patternField[i].getImie1() == patternField[i].getImie1().Replace("parent:", ""))
																		{
																			if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getImie1()) != null)
																			{
																				imie = lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getImie1()).Value;
																			}
																		}
																		else
																		{
																			string label = patternField[i].getImie1().Replace("parent:", "");
																			if (lista.Attributes.GetNamedItem(label) != null)
																			{
																				imie = lista.Attributes.GetNamedItem(label).Value;
																			}
																		}
																		if (patternField[i].getNazwisko() == patternField[i].getNazwisko().Replace("parent:", ""))
																		{
																			if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getNazwisko()) != null)
																			{
																				nazwisko = lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getNazwisko()).Value;
																			}
																		}
																		else
																		{
																			string label = patternField[i].getNazwisko().Replace("parent:", "");
																			if (lista.Attributes.GetNamedItem(label) != null)
																			{
																				nazwisko = lista.Attributes.GetNamedItem(label).Value;
																			}
																		}
																		if (patternField[i].getKomitet() == patternField[i].getKomitet().Replace("parent:", ""))
																		{
																			if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getKomitet()) != null)
																			{
																				komitet = lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getKomitet()).Value;
																			}
																		}
																		else
																		{
																			string label = patternField[i].getKomitet().Replace("parent:", "");
																			if (lista.Attributes.GetNamedItem(label) != null)
																			{
																				komitet = lista.Attributes.GetNamedItem(label).Value;
																			}
																		}
																		if (patternField[i].getPlec() == patternField[i].getPlec().Replace("parent:", ""))
																		{
																			if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getPlec()) != null)
																			{
																				if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getPlec()).Value.ToUpper() == "M")
																				{
																					name2 = patternField[i].getName2();
																				}
																				else
																				{
																					name2 = patternField[i].getName2v2();
																				}
																			}
																		}
																		else
																		{
																			string label = patternField[i].getPlec().Replace("parent:", "");
																			if (lista.Attributes.GetNamedItem(label) != null)
																			{
																				if (lista.Attributes.GetNamedItem(label).Value.ToUpper() == "M")
																				{
																					name2 = patternField[i].getName2();
																				}
																				else
																				{
																					name2 = patternField[i].getName2v2();
																				}
																			}
																		}
																		string text = string.Concat(new string[]
																		{
																			patternField[i].getName1(),
																			" ",
																			nazwisko,
																			" ",
																			imie,
																			" ",
																			imie2,
																			" ",
																			name2,
																			" ",
																			komitet
																		});
																	}
																	if (status == "S")
																	{
																		if (patternField[i].getIdCandidate() == patternField[i].getIdCandidate().Replace("parent:", ""))
																		{
																			if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getIdCandidate()) != null)
																			{
																				idcandidate = lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getIdCandidate()).Value;
																			}
																		}
																		else
																		{
																			string label = patternField[i].getIdCandidate().Replace("parent:", "");
																			if (lista.Attributes.GetNamedItem(label) != null)
																			{
																				idcandidate = lista.Attributes.GetNamedItem(label).Value;
																			}
																		}
																		if (patternField[i].getDataType() == "number" && patternField[i].getSave() != "")
																		{
																			string name3 = patternField[i].getSave().Replace("X", "S" + j.ToString()).Replace("Y", nrListy.ToString());
																			string text2 = this.xmlKandydaci;
																			this.xmlKandydaci = string.Concat(new string[]
																			{
																				text2,
																				"<",
																				name3,
																				" id_kand=\"",
																				idcandidate,
																				"\">X</",
																				name3,
																				">"
																			});
																			idcandidate = "";
																		}
																	}
																}
																if (statustTMP == "A")
																{
																	save_as_candidate++;
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
						}
					}
					if (type.Value == "additional-table")
					{
						x = 10;
						y += 30;
						if (fields.Name == "fields")
						{
							foreach (XmlNode field in fields)
							{
								string valueText = "";
								bool enab = true;
								bool isEnable = false;
								XmlNode enableValue = field.Attributes.GetNamedItem("enabled");
								if (enableValue != null)
								{
									if (enableValue.Value == "false")
									{
										enab = false;
										isEnable = true;
									}
								}
								XmlNode cor = field.Attributes.GetNamedItem("correspondence");
								XmlNode corValue = field.Attributes.GetNamedItem("elseCorrespondenceDefault");
								if (cor != null)
								{
									if ((cor.Value.ToLower() == "false" && this.correspondence) || (cor.Value.ToLower() == "true" && !this.correspondence))
									{
										enab = false;
										if (corValue != null)
										{
											valueText = corValue.Value;
										}
									}
								}
								XmlNode dis = field.Attributes.GetNamedItem("district");
								XmlNode disValue = field.Attributes.GetNamedItem("districtDefault");
								if (dis != null && dis.Value.ToUpper() != "ALL")
								{
									if (System.Text.RegularExpressions.Regex.IsMatch(dis.Value.ToUpper(), "ONLY:"))
									{
										string allow = dis.Value.ToUpper().Replace("ONLY:", "");
										if (allow == this.typeObw.ToUpper())
										{
											enab = false;
											if (disValue != null)
											{
												valueText = disValue.Value;
											}
										}
									}
									if (System.Text.RegularExpressions.Regex.IsMatch(dis.Value.ToUpper(), "EXCLUDE:"))
									{
										string allow = dis.Value.ToUpper().Replace("EXCLUDE:", "");
										if (allow != this.typeObw.ToUpper())
										{
											enab = false;
											if (disValue != null)
											{
												valueText = disValue.Value;
											}
										}
									}
								}
								XmlNode headerRoot = this.header.SelectSingleNode("/akcja_wyborcza/jns");
								XmlNode valueName = field.Attributes.GetNamedItem("valueName");
								if (valueName != null)
								{
									if (valueName.Value == "algorytmOKW")
									{
										foreach (XmlNode obw in headerRoot)
										{
											if (obw.Name == "obw" && obw.Attributes.GetNamedItem("nr") != null && obw.Attributes.GetNamedItem("nr").Value == obwod)
											{
												foreach (XmlNode institutions in obw)
												{
													if (institutions.Name == "inst" && institutions.Attributes.GetNamedItem("kod") != null && institutions.Attributes.GetNamedItem("kod").Value == inst)
													{
														foreach (XmlNode okr in institutions)
														{
															if (okr.Name == "okr" && okr.Attributes.GetNamedItem("nr") != null && okr.Attributes.GetNamedItem("nr").Value == okreg)
															{
																if (okr.Attributes.GetNamedItem("siedziba") != null)
																{
																	valueText = okr.Attributes.GetNamedItem("siedziba").Value;
																}
															}
														}
													}
												}
											}
										}
									}
									if (valueName.Value == "siedzibaObwod")
									{
										foreach (XmlNode s in headerRoot)
										{
											if (s.Attributes.GetNamedItem("nr") != null && s.Attributes.GetNamedItem("nr").Value == obwod)
											{
												if (s.Attributes.GetNamedItem("siedziba") != null)
												{
													valueText = s.Attributes.GetNamedItem("siedziba").Value;
												}
											}
										}
									}
									if (valueName.Value == "nr")
									{
										valueText = obwod;
									}
								}
								if (valueName != null && System.Text.RegularExpressions.Regex.IsMatch(valueName.Value, "\\+"))
								{
									string where = "";
									string text = valueName.Value;
									if (System.Text.RegularExpressions.Regex.IsMatch(valueName.Value, "header:"))
									{
										text = valueName.Value.Replace("header:", "");
										where = "header";
									}
									if (System.Text.RegularExpressions.Regex.IsMatch(valueName.Value, "calculator:"))
									{
										text = valueName.Value.Replace("calculator:", "");
										where = "calculator";
									}
									if (System.Text.RegularExpressions.Regex.IsMatch(valueName.Value, "additional-calculator:"))
									{
										text = valueName.Value.Replace("additional-calculator:", "");
										where = "additional-calculator";
									}
									if (System.Text.RegularExpressions.Regex.IsMatch(valueName.Value, "additional-table:"))
									{
										text = valueName.Value.Replace("additional-table:", "");
										where = "additional-table";
									}
									string[] fieldOfName = text.Split(new char[]
									{
										'+'
									});
									valueText = "";
									for (int i = 0; i < fieldOfName.Length; i++)
									{
										if (i != 0)
										{
											valueText += ", ";
										}
										if (where == "header")
										{
											valueText += this.protocolHeader.Controls[fieldOfName[i]].Text;
										}
										if (where == "calculator" || where == "additional-calculator" || where == "additional-table")
										{
											valueText += this.Form1panel.Controls[fieldOfName[i]].Text;
										}
									}
								}
								if (field.Name == "title")
								{
									Label lab = new Label();
									lab.Text = field.InnerText;
									lab.AutoSize = true;
									lab.MaximumSize = new System.Drawing.Size(this.Form1panel.Size.Width - 20, 0);
									lab.Font = new System.Drawing.Font(this.myfont, 10f, System.Drawing.FontStyle.Bold);
									lab.Location = new System.Drawing.Point(x, y);
									this.Form1panel.Controls.Add(lab);
									y += lab.Height + 30;
								}
								if (field.Name == "description")
								{
									x = 10;
									Label lab2 = new Label();
									lab2.Text = field.InnerText;
									lab2.AutoSize = true;
									lab2.MaximumSize = new System.Drawing.Size(this.Form1panel.Size.Width - 20, 0);
									lab2.Font = new System.Drawing.Font(this.myfont, 9f, System.Drawing.FontStyle.Bold);
									lab2.Location = new System.Drawing.Point(x, y - 15);
									this.Form1panel.Controls.Add(lab2);
									y += lab2.Height + 25;
								}
								if (field.Name == "note")
								{
									if (field.InnerText != "")
									{
										x = 10;
										Label lab2 = new Label();
										lab2.Text = "Uwaga";
										lab2.AutoSize = true;
										lab2.MaximumSize = new System.Drawing.Size(100, 0);
										lab2.Font = new System.Drawing.Font(this.myfont, 9f, System.Drawing.FontStyle.Bold);
										lab2.Location = new System.Drawing.Point(x, y);
										this.Form1panel.Controls.Add(lab2);
										x += lab2.Width + 5;
										this.Form1panel.Controls.Add(lab2);
										Label lab = new Label();
										lab.Text = field.InnerText;
										lab.AutoSize = true;
										lab.MaximumSize = new System.Drawing.Size(this.Form1panel.Size.Width - 120, 0);
										lab.Font = new System.Drawing.Font(this.myfont, 9f);
										lab.Location = new System.Drawing.Point(x, y);
										this.Form1panel.Controls.Add(lab);
										y += lab.Height + 25;
									}
								}
								if (field.Name == "field")
								{
									x = 10;
									Label lab = new Label();
									TextBox inputNum = new TextBox();
									foreach (XmlNode node in field)
									{
										if (node.Name == "name")
										{
											if (node.ParentNode.Attributes.GetNamedItem("lp") != null)
											{
												lab.Text = node.ParentNode.Attributes.GetNamedItem("lp").Value + " ";
											}
											Label expr_4DA7 = lab;
											expr_4DA7.Text += node.InnerText;
											lab.AutoSize = true;
											lab.MaximumSize = new System.Drawing.Size(widthLine - placeForButton, 0);
											lab.Font = new System.Drawing.Font(this.myfont, 9f);
											lab.Location = new System.Drawing.Point(x, y);
											x = widthLine - 95;
										}
										if (node.Name == "save_as")
										{
											int widthIn = 85;
											XmlNode valid = node.ParentNode.Attributes.GetNamedItem("data");
											inputNum.Name = node.InnerText;
											if (valid != null && valid.Value == "number")
											{
												inputNum.Validated += new System.EventHandler(this.number_Validated);
												try
												{
													this.typeValidation.Add(inputNum.Name, "number");
												}
												catch (System.ArgumentException)
												{
												}
											}
											if (valid != null && valid.Value == "text")
											{
												inputNum.Validated += new System.EventHandler(this.text_Validated);
												try
												{
													this.typeValidation.Add(inputNum.Name, "text");
												}
												catch (System.ArgumentException)
												{
												}
												lab.MaximumSize = new System.Drawing.Size(widthLine - placeForButton * 2, 0);
												widthIn = 170;
												lab.AutoSize = true;
												x = widthLine - placeForButton * 2 + 20;
											}
											inputNum.Size = new System.Drawing.Size(widthIn, 20);
											if (valueText != "")
											{
												inputNum.Text = valueText;
											}
											XmlNode value2 = this.save.SelectSingleNode(pathXML + "/" + node.InnerText);
											if (value2 != null)
											{
												inputNum.Text = value2.InnerText;
											}
											inputNum.MouseClick += new MouseEventHandler(this.Control_S1_MouseClick);
											inputNum.LostFocus += new System.EventHandler(this.LostFocus);
											inputNum.Enabled = enab;
											inputNum.Location = new System.Drawing.Point(x, y);
											inputNum.CausesValidation = true;
											lab.Name = "Lab_" + node.InnerText;
											XmlNode min = node.ParentNode.Attributes.GetNamedItem("min");
											XmlNode max = node.ParentNode.Attributes.GetNamedItem("max");
											if (min != null && max != null)
											{
												this.range.Add(inputNum.Name, new ValidationRange(inputNum.Name, System.Convert.ToInt32(min.Value), System.Convert.ToInt32(max.Value)));
											}
											if (isEnable)
											{
												this.controlsCanBeNull.Add(inputNum.Name);
											}
										}
									}
									this.Form1panel.Controls.Add(lab);
									this.Form1panel.Controls.Add(inputNum);
									if (lab.Height > 20)
									{
										y += lab.Height + 5;
									}
									else
									{
										y += 25;
									}
								}
							}
						}
						y += 50;
					}
				}
				this.buttonNext.Text = "Dalej";
				base.Click += new System.EventHandler(this.protocolForm2_Click);
			}
			catch (XmlException ex)
			{
				MessageBox.Show("Nieprawidłowy XML - " + ex.Message, "Error");
			}
			catch (System.NullReferenceException ex2)
			{
				MessageBox.Show("Podanno inny xml definicje wygladu - " + ex2.Message, "Błąd");
			}
			this.Form1panel.Location = new System.Drawing.Point(this.protocolHeader.Location.X, this.protocolHeader.Location.Y + this.protocolHeader.Size.Height + 50);
			this.buttonNext.Text = "Dalej";
			this.buttonNext.Click -= new System.EventHandler(this.protocolForm2_Click);
			this.buttonNext.Click -= new System.EventHandler(this.protocolSummation_Click);
			this.buttonNext.Click -= new System.EventHandler(this.committee_Click);
			this.buttonNext.Click -= new System.EventHandler(this.committeeValid_Click);
			this.buttonNext.Click -= new System.EventHandler(this.signProtocol_Click);
			this.buttonNext.Click += new System.EventHandler(this.protocolForm2_Click);
			this.bottomPanel.Location = new System.Drawing.Point(this.Form1panel.Location.X, this.Form1panel.Location.Y + this.Form1panel.Size.Height);
			this.bottomPanel.Visible = true;
			if (this.lastValidators.Count == 0)
			{
				this.getDefinitionLastValidation();
			}
			this.buttonNext.Enabled = true;
			this.wait.setVisible(false);
		}
		private void getCalculator2()
		{
			this.wait.setWaitPanel("Trwa ładowanie drugiego kroku protokołu", "Proszę czekać");
			this.wait.setVisible(true);
			this.Form2panel.Controls.Clear();
			string pathXML = "";
			if (this.isSave() == 0 || this.isSave() == 4)
			{
				pathXML = "/save/form";
			}
			if (this.isSave() == 2 || this.isSave() == 3)
			{
				pathXML = "/save/step2";
			}
			int count = this.Form1panel.Controls.Count * 2;
			string obwod = "";
			string inst = "";
			string okreg = "";
			string[] dataPath = this.savePath.Split(new char[]
			{
				'-'
			});
			if (dataPath.Length == 6)
			{
				obwod = dataPath[2].Replace("Obw", "");
				inst = dataPath[3].Replace("Inst", "");
				okreg = dataPath[5].Replace("Okr", "");
				okreg = okreg.Replace(".xml", "");
			}
			else
			{
				obwod = dataPath[dataPath.Length - 4].Replace("Obw", "");
				inst = dataPath[dataPath.Length - 3].Replace("Inst", "");
				okreg = dataPath[dataPath.Length - 1].Replace("Okr", "");
				okreg = okreg.Replace(".xml", "");
			}
			foreach (Control c in this.protocolHeader.Controls)
			{
				if (c is TextBox)
				{
					(c as TextBox).TabIndex = count;
					count--;
				}
				if (c is MaskedTextBox)
				{
					(c as MaskedTextBox).TabIndex = count;
					count--;
				}
			}
			this.Form2panel.TabIndex = 2;
			this.protocolHeader.TabIndex = 3;
			int widthLine = 760;
			int placeForButton = 105;
			try
			{
				XmlNode nodesList = this.protocolDefinition.SelectSingleNode("/protokol_info");
				XmlNode candidatesRoot = this.candidates.SelectSingleNode("/listy");
				int x = 0;
				int y = 0;
				foreach (XmlNode fields in nodesList)
				{
					XmlNode type = fields.Attributes.GetNamedItem("type");
					if (type == null)
					{
						break;
					}
					this.wait.setWaitPanel("Trwa ładowanie drugiego kroku protokołu - ładowanie pol kalkulatora", "Proszę czekać");
					if (type.Value == "calculator")
					{
						x = 10;
						if (fields.Name == "fields")
						{
							foreach (XmlNode field in fields)
							{
								if (field.Name == "title")
								{
									Label lab = new Label();
									lab.Text = field.InnerText;
									lab.AutoSize = true;
									lab.MaximumSize = new System.Drawing.Size(780, 0);
									lab.Font = new System.Drawing.Font(this.myfont, 10f, System.Drawing.FontStyle.Bold);
									lab.Location = new System.Drawing.Point(x, y);
									this.Form2panel.Controls.Add(lab);
									y += lab.Height + 30;
								}
								if (field.Name == "description")
								{
									x = 10;
									Label lab2 = new Label();
									lab2.Text = field.InnerText;
									lab2.AutoSize = true;
									lab2.MaximumSize = new System.Drawing.Size(700, 0);
									lab2.Font = new System.Drawing.Font(this.myfont, 9f, System.Drawing.FontStyle.Bold);
									lab2.Location = new System.Drawing.Point(x, y);
									this.Form2panel.Controls.Add(lab2);
									y += lab2.Height + 25;
								}
								if (field.Name == "note")
								{
									x = 10;
									Label lab2 = new Label();
									lab2.Text = "Uwaga";
									lab2.AutoSize = true;
									lab2.MaximumSize = new System.Drawing.Size(100, 0);
									lab2.Font = new System.Drawing.Font(this.myfont, 9f, System.Drawing.FontStyle.Bold);
									lab2.Location = new System.Drawing.Point(x, y);
									this.Form2panel.Controls.Add(lab2);
									x += lab2.Width + 5;
									this.Form2panel.Controls.Add(lab2);
									Label lab = new Label();
									lab.Text = field.InnerText;
									lab.AutoSize = true;
									lab.MaximumSize = new System.Drawing.Size(700, 0);
									lab.Font = new System.Drawing.Font(this.myfont, 9f);
									lab.Location = new System.Drawing.Point(x, y);
									this.Form2panel.Controls.Add(lab);
									y += lab.Height + 25;
								}
								if (field.Name == "field")
								{
									string valueText = "";
									bool enab = true;
									XmlNode cor = field.Attributes.GetNamedItem("correspondence");
									XmlNode corValue = field.Attributes.GetNamedItem("elseCorrespondenceDefault");
									System.Collections.IEnumerator enumerator3;
									if (cor != null)
									{
										if ((cor.Value.ToLower() == "false" && this.correspondence) || (cor.Value.ToLower() == "true" && !this.correspondence))
										{
											enab = false;
											if (corValue != null)
											{
												string value = corValue.Value;
												if (value == "pakiet")
												{
													XmlNode headerRoot = this.header.SelectSingleNode("/akcja_wyborcza/jns");
													enumerator3 = headerRoot.GetEnumerator();
													try
													{
														while (enumerator3.MoveNext())
														{
															XmlNode obw = (XmlNode)enumerator3.Current;
															if (obw.Name == "obw" && obw.Attributes.GetNamedItem("nr") != null && obw.Attributes.GetNamedItem("nr").Value == obwod)
															{
																foreach (XmlNode institutions in obw)
																{
																	if (institutions.Name == "inst" && institutions.Attributes.GetNamedItem("kod") != null && institutions.Attributes.GetNamedItem("kod").Value == inst && institutions.Attributes.GetNamedItem("inst_jns") != null && institutions.Attributes.GetNamedItem("inst_jns").Value == this.instJNS)
																	{
																		foreach (XmlNode okr in institutions)
																		{
																			if (okr.Name == "okr" && okr.Attributes.GetNamedItem("nr") != null && okr.Attributes.GetNamedItem("nr").Value == okreg)
																			{
																				if (okr.Attributes.GetNamedItem("koresp") != null)
																				{
																					valueText = okr.Attributes.GetNamedItem("koresp").Value;
																				}
																			}
																		}
																	}
																}
															}
														}
													}
													finally
													{
														System.IDisposable disposable = enumerator3 as System.IDisposable;
														if (disposable != null)
														{
															disposable.Dispose();
														}
													}
												}
												else
												{
													valueText = corValue.Value;
												}
											}
										}
										if (valueText == "pakiet")
										{
											XmlNode headerRoot = this.header.SelectSingleNode("/akcja_wyborcza/jns");
											enumerator3 = headerRoot.GetEnumerator();
											try
											{
												while (enumerator3.MoveNext())
												{
													XmlNode obw = (XmlNode)enumerator3.Current;
													if (obw.Name == "obw" && obw.Attributes.GetNamedItem("nr") != null && obw.Attributes.GetNamedItem("nr").Value == obwod)
													{
														foreach (XmlNode institutions in obw)
														{
															if (institutions.Name == "inst" && institutions.Attributes.GetNamedItem("kod") != null && institutions.Attributes.GetNamedItem("kod").Value == inst && institutions.Attributes.GetNamedItem("inst_jns") != null && institutions.Attributes.GetNamedItem("inst_jns").Value == this.instJNS)
															{
																foreach (XmlNode okr in institutions)
																{
																	if (okr.Name == "okr" && okr.Attributes.GetNamedItem("nr") != null && okr.Attributes.GetNamedItem("nr").Value == okreg)
																	{
																		if (okr.Attributes.GetNamedItem("koresp") != null)
																		{
																			valueText = okr.Attributes.GetNamedItem("koresp").Value;
																		}
																	}
																}
															}
														}
													}
												}
											}
											finally
											{
												System.IDisposable disposable = enumerator3 as System.IDisposable;
												if (disposable != null)
												{
													disposable.Dispose();
												}
											}
										}
									}
									XmlNode dis = field.Attributes.GetNamedItem("district");
									XmlNode disValue = field.Attributes.GetNamedItem("districtDefault");
									if (dis != null && dis.Value.ToUpper() != "ALL")
									{
										if (System.Text.RegularExpressions.Regex.IsMatch(dis.Value.ToUpper(), "ONLY:"))
										{
											string allow = dis.Value.ToUpper().Replace("ONLY:", "");
											if (allow == this.typeObw.ToUpper())
											{
												enab = false;
												if (disValue != null)
												{
													valueText = disValue.Value;
												}
											}
										}
										if (System.Text.RegularExpressions.Regex.IsMatch(dis.Value.ToUpper(), "EXCLUDE:"))
										{
											string allow = dis.Value.ToUpper().Replace("EXCLUDE:", "");
											if (allow != this.typeObw.ToUpper())
											{
												enab = false;
												if (disValue != null)
												{
													valueText = disValue.Value;
												}
											}
										}
									}
									x = 10;
									Label lab = new Label();
									TextBox inputNum = new TextBox();
									enumerator3 = field.GetEnumerator();
									try
									{
										while (enumerator3.MoveNext())
										{
											XmlNode node = (XmlNode)enumerator3.Current;
											if (node.Name == "name")
											{
												lab.Text = node.ParentNode.Attributes.GetNamedItem("lp").Value + " " + node.InnerText;
												lab.AutoSize = true;
												lab.MaximumSize = new System.Drawing.Size(widthLine - placeForButton, 0);
												lab.Font = new System.Drawing.Font(this.myfont, 9f);
												lab.Location = new System.Drawing.Point(x, y);
												x = widthLine - 95;
											}
											if (node.Name == "save_as")
											{
												inputNum.Size = new System.Drawing.Size(85, 20);
												inputNum.Name = node.InnerText;
												inputNum.Location = new System.Drawing.Point(x, y);
												inputNum.MouseClick += new MouseEventHandler(this.Control_S2_MouseClick);
												inputNum.LostFocus += new System.EventHandler(this.LostFocus);
												inputNum.CausesValidation = true;
												inputNum.TabIndex = count;
												count--;
												inputNum.Enabled = enab;
												inputNum.Text = valueText;
												XmlNode value2 = this.save.SelectSingleNode(pathXML + "/" + inputNum.Name);
												if (value2 != null && enab)
												{
													inputNum.Text = value2.InnerText;
												}
												XmlNode valid = node.ParentNode.Attributes.GetNamedItem("data");
												if (valid != null && valid.Value == "number")
												{
													inputNum.Validated += new System.EventHandler(this.number_Validated);
												}
												lab.Name = "Lab_" + node.InnerText;
											}
										}
									}
									finally
									{
										System.IDisposable disposable = enumerator3 as System.IDisposable;
										if (disposable != null)
										{
											disposable.Dispose();
										}
									}
									this.Form2panel.Controls.Add(lab);
									this.Form2panel.Controls.Add(inputNum);
									if (lab.Height > 20)
									{
										y += lab.Height + 5;
									}
									else
									{
										y += 25;
									}
								}
							}
						}
						y += 50;
					}
					this.wait.setWaitPanel("Trwa ładowanie drugiego kroku protokołu - ładowanie kandydatów", "Proszę czekać");
					if (type != null && type.Value == "additional-calculator")
					{
						foreach (XmlNode field in fields)
						{
							if (field.Name == "field")
							{
								XmlNode data = field.Attributes.GetNamedItem("data");
								if (data != null && data.Value == "kandydaci")
								{
									foreach (XmlNode node in field)
									{
										if (node.Name == "name")
										{
											x = 10;
											Label lab2 = new Label();
											lab2.Text = node.ParentNode.Attributes.GetNamedItem("lp").Value + " " + node.InnerText;
											lab2.AutoSize = true;
											lab2.MaximumSize = new System.Drawing.Size(widthLine - 5, 0);
											lab2.MinimumSize = new System.Drawing.Size(widthLine - 5, 0);
											lab2.Font = new System.Drawing.Font(this.myfont, 9f);
											lab2.Location = new System.Drawing.Point(x, y);
											this.Form2panel.Controls.Add(lab2);
											y += lab2.Size.Height + 30;
										}
										if (field.Name == "note")
										{
											x = 10;
											Label lab2 = new Label();
											lab2.Text = "Uwaga";
											lab2.AutoSize = true;
											lab2.MaximumSize = new System.Drawing.Size(100, 0);
											lab2.Font = new System.Drawing.Font(this.myfont, 9f, System.Drawing.FontStyle.Bold);
											lab2.Location = new System.Drawing.Point(x, y);
											this.Form2panel.Controls.Add(lab2);
											x += lab2.Width + 5;
											this.Form2panel.Controls.Add(lab2);
											Label lab = new Label();
											lab.Text = field.InnerText;
											lab.AutoSize = true;
											lab.MaximumSize = new System.Drawing.Size(widthLine - 100, 0);
											lab.Font = new System.Drawing.Font(this.myfont, 9f);
											lab.Location = new System.Drawing.Point(x, y);
											this.Form2panel.Controls.Add(lab);
											y += lab.Height + 25;
										}
										if (node.Name == "patternrows")
										{
											System.Collections.Generic.List<Field> patternField = new System.Collections.Generic.List<Field>();
											patternField = this.readPatternCandidate(node, patternField);
											int save_as_candidate = 1;
											string idcandidate = "";
											foreach (XmlNode lista in candidatesRoot)
											{
												Label lab = new Label();
												TextBox inputNum = new TextBox();
												string statustTMP = "A";
												for (int i = 0; i < patternField.Count; i++)
												{
													string status = "A";
													if (patternField[i].getStatus() == patternField[i].getStatus().Replace("parent:", ""))
													{
														if (lista.FirstChild.Attributes.GetNamedItem(patternField[i].getStatus()) != null)
														{
															status = lista.FirstChild.Attributes.GetNamedItem(patternField[i].getStatus()).Value;
														}
													}
													else
													{
														string label = patternField[i].getStatus().Replace("parent:", "");
														if (lista.Attributes.GetNamedItem(label) != null)
														{
															status = lista.Attributes.GetNamedItem(label).Value;
														}
													}
													statustTMP = status;
													if (status == "A")
													{
														string imie = "";
														string imie2 = "";
														string nazwisko = "";
														string komitet = "";
														string name2 = "";
														string listaText = "";
														if (patternField[i].getDisplay() == patternField[i].getDisplay().Replace("parent:", ""))
														{
															if (lista.FirstChild.Attributes.GetNamedItem(patternField[i].getDisplay()) != null)
															{
																string isDisplay = lista.FirstChild.Attributes.GetNamedItem(patternField[i].getDisplay()).Value;
															}
														}
														else
														{
															string label = patternField[i].getDisplay().Replace("parent:", "");
															if (lista.Attributes.GetNamedItem(label) != null)
															{
																string isDisplay = lista.Attributes.GetNamedItem(label).Value;
															}
														}
														if (patternField[i].getIdCandidate() == patternField[i].getIdCandidate().Replace("parent:", ""))
														{
															if (lista.FirstChild.Attributes.GetNamedItem(patternField[i].getIdCandidate()) != null)
															{
																idcandidate = lista.FirstChild.Attributes.GetNamedItem(patternField[i].getIdCandidate()).Value;
															}
														}
														else
														{
															string label = patternField[i].getIdCandidate().Replace("parent:", "");
															if (lista.Attributes.GetNamedItem(label) != null)
															{
																idcandidate = lista.Attributes.GetNamedItem(label).Value;
															}
														}
														if (patternField[i].getImie2() == patternField[i].getImie2().Replace("parent:", ""))
														{
															if (lista.FirstChild.Attributes.GetNamedItem(patternField[i].getImie2()) != null)
															{
																imie2 = lista.FirstChild.Attributes.GetNamedItem(patternField[i].getImie2()).Value;
															}
														}
														else
														{
															string label = patternField[i].getImie2().Replace("parent:", "");
															if (lista.Attributes.GetNamedItem(label) != null)
															{
																imie2 = lista.Attributes.GetNamedItem(label).Value;
															}
														}
														if (patternField[i].getImie1() == patternField[i].getImie1().Replace("parent:", ""))
														{
															if (lista.FirstChild.Attributes.GetNamedItem(patternField[i].getImie1()) != null)
															{
																imie = lista.FirstChild.Attributes.GetNamedItem(patternField[i].getImie1()).Value;
															}
														}
														else
														{
															string label = patternField[i].getImie1().Replace("parent:", "");
															if (lista.Attributes.GetNamedItem(label) != null)
															{
																imie = lista.Attributes.GetNamedItem(label).Value;
															}
														}
														if (patternField[i].getNazwisko() == patternField[i].getNazwisko().Replace("parent:", ""))
														{
															if (lista.FirstChild.Attributes.GetNamedItem(patternField[i].getNazwisko()) != null)
															{
																nazwisko = lista.FirstChild.Attributes.GetNamedItem(patternField[i].getNazwisko()).Value;
															}
														}
														else
														{
															string label = patternField[i].getNazwisko().Replace("parent:", "");
															if (lista.Attributes.GetNamedItem(label) != null)
															{
																nazwisko = lista.Attributes.GetNamedItem(label).Value;
															}
														}
														if (patternField[i].getKomitet() == patternField[i].getKomitet().Replace("parent:", ""))
														{
															if (lista.FirstChild.Attributes.GetNamedItem(patternField[i].getKomitet()) != null)
															{
																komitet = lista.FirstChild.Attributes.GetNamedItem(patternField[i].getKomitet()).Value;
															}
														}
														else
														{
															string label = patternField[i].getKomitet().Replace("parent:", "");
															if (lista.Attributes.GetNamedItem(label) != null)
															{
																komitet = lista.Attributes.GetNamedItem(label).Value;
															}
														}
														string nrLista = "";
														if (patternField[i].getLista() == patternField[i].getLista().Replace("parent:", ""))
														{
															if (lista.FirstChild.Attributes.GetNamedItem(patternField[i].getLista()) != null)
															{
																nrLista = lista.FirstChild.Attributes.GetNamedItem(patternField[i].getLista()).Value;
															}
														}
														else
														{
															string label = patternField[i].getLista().Replace("parent:", "");
															if (lista.Attributes.GetNamedItem(label) != null)
															{
																nrLista = lista.Attributes.GetNamedItem(label).Value;
															}
														}
														if (nrLista != "")
														{
															listaText = patternField[i].getName3() + nrLista;
														}
														if (patternField[i].getPlec() == patternField[i].getPlec().Replace("parent:", ""))
														{
															if (lista.FirstChild.Attributes.GetNamedItem(patternField[i].getPlec()) != null)
															{
																if (lista.FirstChild.Attributes.GetNamedItem(patternField[i].getPlec()).Value.ToUpper() == "M")
																{
																	name2 = patternField[i].getName2();
																}
																else
																{
																	name2 = patternField[i].getName2v2();
																}
															}
														}
														else
														{
															string label = patternField[i].getPlec().Replace("parent:", "");
															if (lista.Attributes.GetNamedItem(label) != null)
															{
																if (lista.Attributes.GetNamedItem(label).Value.ToUpper() == "M")
																{
																	name2 = patternField[i].getName2();
																}
																else
																{
																	name2 = patternField[i].getName2v2();
																}
															}
														}
														string text = string.Concat(new string[]
														{
															patternField[i].getName1(),
															" ",
															nazwisko,
															" ",
															imie,
															" ",
															imie2,
															" ",
															name2,
															" ",
															komitet,
															listaText
														});
														if (patternField[i].getDataType() == "text" && patternField[i].getSave() == "")
														{
															x = 10;
															lab.Text = text;
															lab.AutoSize = true;
															lab.MaximumSize = new System.Drawing.Size(widthLine - 105, 0);
															lab.Font = new System.Drawing.Font(this.myfont, 9f);
															lab.Location = new System.Drawing.Point(x, y);
															x = widthLine - 95;
															this.Form2panel.Controls.Add(lab);
														}
														if (patternField[i].getDataType() == "number" && patternField[i].getSave() != "")
														{
															inputNum.Size = new System.Drawing.Size(85, 20);
															inputNum.Name = patternField[i].getSave().Replace("X", save_as_candidate.ToString());
															inputNum.Location = new System.Drawing.Point(x, y);
															inputNum.MouseClick += new MouseEventHandler(this.Control_S1_MouseClick);
															inputNum.LostFocus += new System.EventHandler(this.LostFocus);
															inputNum.Validated += new System.EventHandler(this.number_Validated);
															inputNum.TabIndex = count;
															count--;
															XmlNode value2 = this.save.SelectSingleNode(pathXML + "/" + inputNum.Name);
															if (value2 != null)
															{
																inputNum.Text = value2.InnerText;
															}
															if (!(patternField[i].getDisplay().ToLower() == "false"))
															{
																this.Form2panel.Controls.Add(inputNum);
															}
															if (lab.Height > 20)
															{
																y += lab.Height + 5;
															}
															else
															{
																y += 25;
															}
															this.candidatesRule[inputNum.Name] = idcandidate;
															idcandidate = "";
														}
													}
												}
												if (statustTMP == "A")
												{
													save_as_candidate++;
												}
											}
											this.countcandidatesoflist.Add(new int[]
											{
												0,
												save_as_candidate - 1
											});
										}
										if (node.Name == "patternrow")
										{
											System.Collections.Generic.List<Field> patternField = new System.Collections.Generic.List<Field>();
											patternField = this.readPatternCandidate(node, patternField);
											for (int i = 0; i < patternField.Count; i++)
											{
												if (!patternField[i].needImportData())
												{
													x = 10;
													Label lab = new Label();
													lab.Text = patternField[i].getName1();
													lab.AutoSize = true;
													lab.MaximumSize = new System.Drawing.Size(widthLine - 105, 0);
													lab.Font = new System.Drawing.Font(this.myfont, 9f);
													lab.Location = new System.Drawing.Point(x, y);
													x = widthLine - 95;
													this.Form2panel.Controls.Add(lab);
													TextBox inputNum = new TextBox();
													inputNum.Size = new System.Drawing.Size(85, 20);
													inputNum.Name = patternField[i].getSave();
													inputNum.Location = new System.Drawing.Point(x, y);
													inputNum.MouseClick += new MouseEventHandler(this.Control_S1_MouseClick);
													inputNum.LostFocus += new System.EventHandler(this.LostFocus);
													inputNum.Validated += new System.EventHandler(this.number_Validated);
													inputNum.TabIndex = count;
													count--;
													XmlNode value2 = this.save.SelectSingleNode(pathXML + "/" + inputNum.Name);
													if (value2 != null)
													{
														inputNum.Text = value2.InnerText;
													}
													this.Form2panel.Controls.Add(inputNum);
													if (lab.Height > 20)
													{
														y += lab.Height + 5;
													}
													else
													{
														y += 25;
													}
												}
											}
										}
										if (node.Name == "patternlist")
										{
											int nrListy = 1;
											int save_as_candidate = 1;
											foreach (XmlNode lista in candidatesRoot)
											{
												XmlNode Lstatu = lista.Attributes.GetNamedItem("lista_status");
												if (Lstatu != null && Lstatu.Value == "R")
												{
													foreach (XmlNode paternNode in node)
													{
														if (paternNode.Name == "title")
														{
															x = 0;
															XmlNode bold = paternNode.Attributes.GetNamedItem("bold");
															XmlNode nr = paternNode.Attributes.GetNamedItem("nr");
															XmlNode komitet2 = paternNode.Attributes.GetNamedItem("komitet");
															XmlNode nrListyVal = paternNode.Attributes.GetNamedItem("lista");
															string text = "";
															if (komitet2 != null)
															{
																XmlNode komitetText = lista.Attributes.GetNamedItem(komitet2.Value);
																if (komitetText != null && komitetText.Value != "")
																{
																	text = komitetText.Value;
																}
															}
															string valL = "";
															if (nrListyVal != null)
															{
																XmlNode nrListyValText = lista.Attributes.GetNamedItem(nrListyVal.Value);
																if (nrListyValText != null && nrListyValText.Value != "")
																{
																	valL = nrListyValText.Value;
																}
															}
															if (nr != null)
															{
																text = string.Concat(new string[]
																{
																	nr.Value,
																	" ",
																	valL,
																	" ",
																	paternNode.InnerText,
																	text
																});
															}
															Label lab = new Label();
															lab.Text = text;
															lab.AutoSize = true;
															lab.MaximumSize = new System.Drawing.Size(this.Form2panel.Size.Width, 0);
															lab.Font = new System.Drawing.Font(this.myfont, 10f);
															lab.Padding = new Padding(10, 0, 10, 0);
															if (bold != null && bold.Value == "true")
															{
																lab.Font = new System.Drawing.Font(this.myfont, 10f, System.Drawing.FontStyle.Bold);
															}
															lab.Location = new System.Drawing.Point(x, y);
															this.Form2panel.Controls.Add(lab);
															y += lab.Size.Height + 30;
														}
														if (paternNode.Name == "patternrow")
														{
															System.Collections.Generic.List<Field> patternField = new System.Collections.Generic.List<Field>();
															patternField = this.readPatternCandidate(paternNode, patternField);
															for (int i = 0; i < patternField.Count; i++)
															{
																if (!patternField[i].needImportData())
																{
																	x = 10;
																	Label lab = new Label();
																	lab.Text = patternField[i].getName1();
																	lab.AutoSize = true;
																	lab.MaximumSize = new System.Drawing.Size(this.Form2panel.Width - 105, 0);
																	lab.Font = new System.Drawing.Font(this.myfont, 9f);
																	lab.Location = new System.Drawing.Point(x, y);
																	x = this.Form2panel.Width - 95;
																	this.Form2panel.Controls.Add(lab);
																	TextBox inputNum = new TextBox();
																	inputNum.Size = new System.Drawing.Size(85, 20);
																	inputNum.TabIndex = count;
																	count--;
																	inputNum.Name = patternField[i].getSave().Replace("Y", nrListy.ToString());
																	XmlNode value2 = this.save.SelectSingleNode(pathXML + "/" + inputNum.Name);
																	if (value2 != null)
																	{
																		inputNum.Text = value2.InnerText;
																	}
																	inputNum.Location = new System.Drawing.Point(x, y);
																	inputNum.MouseClick += new MouseEventHandler(this.Control_S1_MouseClick);
																	inputNum.LostFocus += new System.EventHandler(this.LostFocus);
																	inputNum.Validated += new System.EventHandler(this.number_Validated);
																	this.Form2panel.Controls.Add(inputNum);
																	if (lab.Height > 20)
																	{
																		y += lab.Height + 5;
																	}
																	else
																	{
																		y += 25;
																	}
																}
															}
														}
														if (paternNode.Name == "patternrows")
														{
															System.Collections.Generic.List<Field> patternField = new System.Collections.Generic.List<Field>();
															patternField = this.readPatternCandidate(paternNode, patternField);
															save_as_candidate = 1;
															for (int j = 0; j < lista.ChildNodes.Count; j++)
															{
																string idcandidate = "";
																Label lab = new Label();
																TextBox inputNum = new TextBox();
																string statustTMP = "A";
																for (int i = 0; i < patternField.Count; i++)
																{
																	string status = "A";
																	if (patternField[i].getStatus() == patternField[i].getStatus().Replace("parent:", ""))
																	{
																		if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getStatus()) != null)
																		{
																			status = lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getStatus()).Value;
																		}
																	}
																	else
																	{
																		string label = patternField[i].getStatus().Replace("parent:", "");
																		if (lista.Attributes.GetNamedItem(label) != null)
																		{
																			status = lista.Attributes.GetNamedItem(label).Value;
																		}
																	}
																	statustTMP = status;
																	if (status == "A")
																	{
																		string imie = "";
																		string imie2 = "";
																		string nazwisko = "";
																		string name2 = "";
																		string komitet = "";
																		if (patternField[i].getDisplay() == patternField[i].getDisplay().Replace("parent:", ""))
																		{
																			if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getDisplay()) != null)
																			{
																				string isDisplay = lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getDisplay()).Value;
																			}
																		}
																		else
																		{
																			string label = patternField[i].getDisplay().Replace("parent:", "");
																			if (lista.Attributes.GetNamedItem(label) != null)
																			{
																				string isDisplay = lista.Attributes.GetNamedItem(label).Value;
																			}
																		}
																		if (patternField[i].getIdCandidate() == patternField[i].getIdCandidate().Replace("parent:", ""))
																		{
																			if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getIdCandidate()) != null)
																			{
																				idcandidate = lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getIdCandidate()).Value;
																			}
																		}
																		else
																		{
																			string label = patternField[i].getIdCandidate().Replace("parent:", "");
																			if (lista.Attributes.GetNamedItem(label) != null)
																			{
																				idcandidate = lista.Attributes.GetNamedItem(label).Value;
																			}
																		}
																		if (patternField[i].getImie2() == patternField[i].getImie2().Replace("parent:", ""))
																		{
																			if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getImie2()) != null)
																			{
																				imie2 = lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getImie2()).Value;
																			}
																		}
																		else
																		{
																			string label = patternField[i].getImie2().Replace("parent:", "");
																			if (lista.Attributes.GetNamedItem(label) != null)
																			{
																				imie2 = lista.Attributes.GetNamedItem(label).Value;
																			}
																		}
																		if (patternField[i].getImie1() == patternField[i].getImie1().Replace("parent:", ""))
																		{
																			if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getImie1()) != null)
																			{
																				imie = lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getImie1()).Value;
																			}
																		}
																		else
																		{
																			string label = patternField[i].getImie1().Replace("parent:", "");
																			if (lista.Attributes.GetNamedItem(label) != null)
																			{
																				imie = lista.Attributes.GetNamedItem(label).Value;
																			}
																		}
																		if (patternField[i].getNazwisko() == patternField[i].getNazwisko().Replace("parent:", ""))
																		{
																			if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getNazwisko()) != null)
																			{
																				nazwisko = lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getNazwisko()).Value;
																			}
																		}
																		else
																		{
																			string label = patternField[i].getNazwisko().Replace("parent:", "");
																			if (lista.Attributes.GetNamedItem(label) != null)
																			{
																				nazwisko = lista.Attributes.GetNamedItem(label).Value;
																			}
																		}
																		if (patternField[i].getKomitet() == patternField[i].getKomitet().Replace("parent:", ""))
																		{
																			if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getKomitet()) != null)
																			{
																				komitet = lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getKomitet()).Value;
																			}
																		}
																		else
																		{
																			string label = patternField[i].getKomitet().Replace("parent:", "");
																			if (lista.Attributes.GetNamedItem(label) != null)
																			{
																				komitet = lista.Attributes.GetNamedItem(label).Value;
																			}
																		}
																		if (patternField[i].getPlec() == patternField[i].getPlec().Replace("parent:", ""))
																		{
																			if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getPlec()) != null)
																			{
																				if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getPlec()).Value.ToUpper() == "M")
																				{
																					name2 = patternField[i].getName2();
																				}
																				else
																				{
																					name2 = patternField[i].getName2v2();
																				}
																			}
																		}
																		else
																		{
																			string label = patternField[i].getPlec().Replace("parent:", "");
																			if (lista.Attributes.GetNamedItem(label) != null)
																			{
																				if (lista.Attributes.GetNamedItem(label).Value.ToUpper() == "M")
																				{
																					name2 = patternField[i].getName2();
																				}
																				else
																				{
																					name2 = patternField[i].getName2v2();
																				}
																			}
																		}
																		string text = string.Concat(new string[]
																		{
																			patternField[i].getName1(),
																			" ",
																			nazwisko,
																			" ",
																			imie,
																			" ",
																			imie2,
																			" ",
																			name2,
																			" ",
																			komitet
																		});
																		if (patternField[i].getDataType() == "text" && patternField[i].getSave() == "")
																		{
																			x = 10;
																			lab.Text = text;
																			lab.AutoSize = true;
																			lab.MaximumSize = new System.Drawing.Size(this.Form2panel.Width - 105, 0);
																			lab.Font = new System.Drawing.Font(this.myfont, 9f);
																			lab.Location = new System.Drawing.Point(x, y);
																			x = this.Form2panel.Width - 95;
																			this.Form2panel.Controls.Add(lab);
																		}
																		if (patternField[i].getDataType() == "number" && patternField[i].getSave() != "")
																		{
																			inputNum.Size = new System.Drawing.Size(85, 20);
																			inputNum.Name = patternField[i].getSave().Replace("X", save_as_candidate.ToString()).Replace("Y", nrListy.ToString());
																			XmlNode value2 = this.save.SelectSingleNode(pathXML + "/" + inputNum.Name);
																			if (value2 != null)
																			{
																				inputNum.Text = value2.InnerText;
																			}
																			inputNum.Location = new System.Drawing.Point(x, y);
																			inputNum.TabIndex = count;
																			count--;
																			inputNum.MouseClick += new MouseEventHandler(this.Control_S1_MouseClick);
																			inputNum.LostFocus += new System.EventHandler(this.LostFocus);
																			inputNum.Validated += new System.EventHandler(this.number_Validated);
																			if (!(patternField[i].getDisplay().ToLower() == "false"))
																			{
																				this.Form2panel.Controls.Add(inputNum);
																			}
																			if (lab.Height > 20)
																			{
																				y += lab.Height + 5;
																			}
																			else
																			{
																				y += 25;
																			}
																			this.candidatesRule[inputNum.Name] = idcandidate;
																			idcandidate = "";
																		}
																	}
																}
																if (statustTMP == "A")
																{
																	save_as_candidate++;
																}
															}
															this.countcandidatesoflist.Add(new int[]
															{
																nrListy,
																save_as_candidate - 1
															});
														}
													}
													nrListy++;
													y += 30;
												}
												if (Lstatu != null && Lstatu.Value == "U")
												{
													foreach (XmlNode paternNode in node)
													{
														if (paternNode.Name == "patternrow")
														{
															System.Collections.Generic.List<Field> patternField = new System.Collections.Generic.List<Field>();
															patternField = this.readPatternCandidate(paternNode, patternField);
															for (int i = 0; i < patternField.Count; i++)
															{
																if (!patternField[i].needImportData())
																{
																	string name3 = patternField[i].getSave().Replace("Y", nrListy.ToString());
																	string text2 = this.xmlKandydaci;
																	this.xmlKandydaci = string.Concat(new string[]
																	{
																		text2,
																		"<",
																		name3,
																		">X</",
																		name3,
																		">"
																	});
																}
															}
														}
														if (paternNode.Name == "patternrows")
														{
															System.Collections.Generic.List<Field> patternField = new System.Collections.Generic.List<Field>();
															patternField = this.readPatternCandidate(paternNode, patternField);
															save_as_candidate = 1;
															for (int j = 0; j < lista.ChildNodes.Count; j++)
															{
																string idcandidate = "";
																Label lab = new Label();
																TextBox inputNum = new TextBox();
																string statustTMP = "A";
																for (int i = 0; i < patternField.Count; i++)
																{
																	string status = "A";
																	if (patternField[i].getStatus() == patternField[i].getStatus().Replace("parent:", ""))
																	{
																		if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getStatus()) != null)
																		{
																			status = lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getStatus()).Value;
																		}
																	}
																	else
																	{
																		string label = patternField[i].getStatus().Replace("parent:", "");
																		if (lista.Attributes.GetNamedItem(label) != null)
																		{
																			status = lista.Attributes.GetNamedItem(label).Value;
																		}
																	}
																	statustTMP = status;
																	if (status == "A")
																	{
																		string imie = "";
																		string imie2 = "";
																		string nazwisko = "";
																		string komitet = "";
																		string name2 = "";
																		if (patternField[i].getDisplay() == patternField[i].getDisplay().Replace("parent:", ""))
																		{
																			if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getDisplay()) != null)
																			{
																				string isDisplay = lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getDisplay()).Value;
																			}
																		}
																		else
																		{
																			string label = patternField[i].getDisplay().Replace("parent:", "");
																			if (lista.Attributes.GetNamedItem(label) != null)
																			{
																				string isDisplay = lista.Attributes.GetNamedItem(label).Value;
																			}
																		}
																		if (patternField[i].getIdCandidate() == patternField[i].getIdCandidate().Replace("parent:", ""))
																		{
																			if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getIdCandidate()) != null)
																			{
																				idcandidate = lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getIdCandidate()).Value;
																			}
																		}
																		else
																		{
																			string label = patternField[i].getIdCandidate().Replace("parent:", "");
																			if (lista.Attributes.GetNamedItem(label) != null)
																			{
																				idcandidate = lista.Attributes.GetNamedItem(label).Value;
																			}
																		}
																		if (patternField[i].getImie2() == patternField[i].getImie2().Replace("parent:", ""))
																		{
																			if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getImie2()) != null)
																			{
																				imie2 = lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getImie2()).Value;
																			}
																		}
																		else
																		{
																			string label = patternField[i].getImie2().Replace("parent:", "");
																			if (lista.Attributes.GetNamedItem(label) != null)
																			{
																				imie2 = lista.Attributes.GetNamedItem(label).Value;
																			}
																		}
																		if (patternField[i].getImie1() == patternField[i].getImie1().Replace("parent:", ""))
																		{
																			if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getImie1()) != null)
																			{
																				imie = lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getImie1()).Value;
																			}
																		}
																		else
																		{
																			string label = patternField[i].getImie1().Replace("parent:", "");
																			if (lista.Attributes.GetNamedItem(label) != null)
																			{
																				imie = lista.Attributes.GetNamedItem(label).Value;
																			}
																		}
																		if (patternField[i].getNazwisko() == patternField[i].getNazwisko().Replace("parent:", ""))
																		{
																			if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getNazwisko()) != null)
																			{
																				nazwisko = lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getNazwisko()).Value;
																			}
																		}
																		else
																		{
																			string label = patternField[i].getNazwisko().Replace("parent:", "");
																			if (lista.Attributes.GetNamedItem(label) != null)
																			{
																				nazwisko = lista.Attributes.GetNamedItem(label).Value;
																			}
																		}
																		if (patternField[i].getKomitet() == patternField[i].getKomitet().Replace("parent:", ""))
																		{
																			if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getKomitet()) != null)
																			{
																				komitet = lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getKomitet()).Value;
																			}
																		}
																		else
																		{
																			string label = patternField[i].getKomitet().Replace("parent:", "");
																			if (lista.Attributes.GetNamedItem(label) != null)
																			{
																				komitet = lista.Attributes.GetNamedItem(label).Value;
																			}
																		}
																		if (patternField[i].getPlec() == patternField[i].getPlec().Replace("parent:", ""))
																		{
																			if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getPlec()) != null)
																			{
																				if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getPlec()).Value.ToUpper() == "M")
																				{
																					name2 = patternField[i].getName2();
																				}
																				else
																				{
																					name2 = patternField[i].getName2v2();
																				}
																			}
																		}
																		else
																		{
																			string label = patternField[i].getPlec().Replace("parent:", "");
																			if (lista.Attributes.GetNamedItem(label) != null)
																			{
																				if (lista.Attributes.GetNamedItem(label).Value.ToUpper() == "M")
																				{
																					name2 = patternField[i].getName2();
																				}
																				else
																				{
																					name2 = patternField[i].getName2v2();
																				}
																			}
																		}
																		string text = string.Concat(new string[]
																		{
																			patternField[i].getName1(),
																			" ",
																			nazwisko,
																			" ",
																			imie,
																			" ",
																			imie2,
																			" ",
																			name2,
																			" ",
																			komitet
																		});
																	}
																	if (status == "S")
																	{
																		if (patternField[i].getIdCandidate() == patternField[i].getIdCandidate().Replace("parent:", ""))
																		{
																			if (lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getIdCandidate()) != null)
																			{
																				idcandidate = lista.ChildNodes[j].Attributes.GetNamedItem(patternField[i].getIdCandidate()).Value;
																			}
																		}
																		else
																		{
																			string label = patternField[i].getIdCandidate().Replace("parent:", "");
																			if (lista.Attributes.GetNamedItem(label) != null)
																			{
																				idcandidate = lista.Attributes.GetNamedItem(label).Value;
																			}
																		}
																		if (patternField[i].getDataType() == "number" && patternField[i].getSave() != "")
																		{
																			string name3 = patternField[i].getSave().Replace("X", "S" + j.ToString()).Replace("Y", nrListy.ToString());
																			string text2 = this.xmlKandydaci;
																			this.xmlKandydaci = string.Concat(new string[]
																			{
																				text2,
																				"<",
																				name3,
																				" id_kand=\"",
																				idcandidate,
																				"\">X</",
																				name3,
																				">"
																			});
																			idcandidate = "";
																		}
																	}
																}
																if (statustTMP == "A")
																{
																	save_as_candidate++;
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
						}
					}
					if (type.Value == "additional-table")
					{
						x = 10;
						y += 30;
						if (fields.Name == "fields")
						{
							foreach (XmlNode field in fields)
							{
								bool enab = true;
								bool isEnable = false;
								XmlNode enableValue = field.Attributes.GetNamedItem("enabled");
								if (enableValue != null)
								{
									if (enableValue.Value == "false")
									{
										enab = false;
										isEnable = true;
									}
								}
								string valueText = "";
								XmlNode cor = field.Attributes.GetNamedItem("correspondence");
								XmlNode corValue = field.Attributes.GetNamedItem("elseCorrespondenceDefault");
								if (cor != null)
								{
									if ((cor.Value.ToLower() == "false" && this.correspondence) || (cor.Value.ToLower() == "true" && !this.correspondence))
									{
										enab = false;
										if (corValue != null)
										{
											valueText = corValue.Value;
										}
									}
								}
								XmlNode dis = field.Attributes.GetNamedItem("district");
								XmlNode disValue = field.Attributes.GetNamedItem("districtDefault");
								if (dis != null && dis.Value.ToUpper() != "ALL")
								{
									if (System.Text.RegularExpressions.Regex.IsMatch(dis.Value.ToUpper(), "ONLY:"))
									{
										string allow = dis.Value.ToUpper().Replace("ONLY:", "");
										if (allow == this.typeObw.ToUpper())
										{
											enab = false;
											if (disValue != null)
											{
												valueText = disValue.Value;
											}
										}
									}
									if (System.Text.RegularExpressions.Regex.IsMatch(dis.Value.ToUpper(), "EXCLUDE:"))
									{
										string allow = dis.Value.ToUpper().Replace("EXCLUDE:", "");
										if (allow != this.typeObw.ToUpper())
										{
											enab = false;
											if (disValue != null)
											{
												valueText = disValue.Value;
											}
										}
									}
								}
								XmlNode headerRoot = this.header.SelectSingleNode("/akcja_wyborcza/jns");
								XmlNode valueName = field.Attributes.GetNamedItem("valueName");
								if (valueName != null)
								{
									if (valueName.Value == "algorytmOKW")
									{
										foreach (XmlNode obw in headerRoot)
										{
											if (obw.Name == "obw" && obw.Attributes.GetNamedItem("nr") != null && obw.Attributes.GetNamedItem("nr").Value == obwod)
											{
												foreach (XmlNode institutions in obw)
												{
													if (institutions.Name == "inst" && institutions.Attributes.GetNamedItem("kod") != null && institutions.Attributes.GetNamedItem("kod").Value == inst)
													{
														foreach (XmlNode okr in institutions)
														{
															if (okr.Name == "okr" && okr.Attributes.GetNamedItem("nr") != null && okr.Attributes.GetNamedItem("nr").Value == okreg)
															{
																if (okr.Attributes.GetNamedItem("siedziba") != null)
																{
																	valueText = okr.Attributes.GetNamedItem("siedziba").Value;
																}
															}
														}
													}
												}
											}
										}
									}
									if (valueName.Value == "siedzibaObwod")
									{
										foreach (XmlNode s in headerRoot)
										{
											if (s.Attributes.GetNamedItem("nr") != null && s.Attributes.GetNamedItem("nr").Value == obwod)
											{
												if (s.Attributes.GetNamedItem("siedziba") != null)
												{
													valueText = s.Attributes.GetNamedItem("siedziba").Value;
												}
											}
										}
									}
									if (valueName.Value == "nr")
									{
										valueText = obwod;
									}
									if (valueName != null && System.Text.RegularExpressions.Regex.IsMatch(valueName.Value, "\\+"))
									{
										string where = "";
										string text = valueName.Value;
										if (System.Text.RegularExpressions.Regex.IsMatch(valueName.Value, "header:"))
										{
											text = valueName.Value.Replace("header:", "");
											where = "header";
										}
										if (System.Text.RegularExpressions.Regex.IsMatch(valueName.Value, "calculator:"))
										{
											text = valueName.Value.Replace("calculator:", "");
											where = "calculator";
										}
										if (System.Text.RegularExpressions.Regex.IsMatch(valueName.Value, "additional-calculator:"))
										{
											text = valueName.Value.Replace("additional-calculator:", "");
											where = "additional-calculator";
										}
										if (System.Text.RegularExpressions.Regex.IsMatch(valueName.Value, "additional-table:"))
										{
											text = valueName.Value.Replace("additional-table:", "");
											where = "additional-table";
										}
										string[] fieldOfName = text.Split(new char[]
										{
											'+'
										});
										valueText = "";
										for (int i = 0; i < fieldOfName.Length; i++)
										{
											if (i != 0)
											{
												valueText += ", ";
											}
											if (where == "header")
											{
												valueText += this.protocolHeader.Controls[fieldOfName[i]].Text;
											}
											if (where == "calculator" || where == "additional-calculator" || where == "additional-table")
											{
												valueText += this.Form1panel.Controls[fieldOfName[i]].Text;
											}
										}
									}
								}
								if (field.Name == "title")
								{
									Label lab = new Label();
									lab.Text = field.InnerText;
									lab.AutoSize = true;
									lab.MaximumSize = new System.Drawing.Size(this.Form2panel.Size.Width - 20, 0);
									lab.Font = new System.Drawing.Font(this.myfont, 10f, System.Drawing.FontStyle.Bold);
									lab.Location = new System.Drawing.Point(x, y);
									this.Form2panel.Controls.Add(lab);
									y += lab.Height + 30;
								}
								if (field.Name == "description")
								{
									x = 10;
									Label lab2 = new Label();
									lab2.Text = field.InnerText;
									lab2.AutoSize = true;
									lab2.MaximumSize = new System.Drawing.Size(this.Form2panel.Size.Width - 20, 0);
									lab2.Font = new System.Drawing.Font(this.myfont, 9f, System.Drawing.FontStyle.Bold);
									lab2.Location = new System.Drawing.Point(x, y - 15);
									this.Form2panel.Controls.Add(lab2);
									y += lab2.Height + 25;
								}
								if (field.Name == "note")
								{
									if (field.InnerText != "")
									{
										x = 10;
										Label lab2 = new Label();
										lab2.Text = "Uwaga";
										lab2.AutoSize = true;
										lab2.MaximumSize = new System.Drawing.Size(100, 0);
										lab2.Font = new System.Drawing.Font(this.myfont, 9f, System.Drawing.FontStyle.Bold);
										lab2.Location = new System.Drawing.Point(x, y);
										this.Form2panel.Controls.Add(lab2);
										x += lab2.Width + 5;
										this.Form1panel.Controls.Add(lab2);
										Label lab = new Label();
										lab.Text = field.InnerText;
										lab.AutoSize = true;
										lab.MaximumSize = new System.Drawing.Size(this.Form2panel.Size.Width - 120, 0);
										lab.Font = new System.Drawing.Font(this.myfont, 9f);
										lab.Location = new System.Drawing.Point(x, y);
										this.Form2panel.Controls.Add(lab);
										y += lab.Height + 25;
									}
								}
								if (field.Name == "field")
								{
									x = 10;
									Label lab = new Label();
									TextBox inputNum = new TextBox();
									foreach (XmlNode node in field)
									{
										if (node.Name == "name")
										{
											if (node.ParentNode.Attributes.GetNamedItem("lp") != null)
											{
												lab.Text = node.ParentNode.Attributes.GetNamedItem("lp").Value + " ";
											}
											Label expr_4A79 = lab;
											expr_4A79.Text += node.InnerText;
											lab.AutoSize = true;
											lab.MaximumSize = new System.Drawing.Size(widthLine - placeForButton, 0);
											lab.Font = new System.Drawing.Font(this.myfont, 9f);
											lab.Location = new System.Drawing.Point(x, y);
											x = widthLine - 95;
										}
										if (node.Name == "save_as")
										{
											int widthIn = 85;
											XmlNode valid = node.ParentNode.Attributes.GetNamedItem("data");
											if (valid != null && valid.Value == "number")
											{
												inputNum.Validated += new System.EventHandler(this.number_Validated);
												try
												{
													this.typeValidation.Add(inputNum.Name, "number");
												}
												catch (System.ArgumentException)
												{
												}
											}
											if (valid != null && valid.Value == "text")
											{
												inputNum.Validated += new System.EventHandler(this.text_Validated);
												try
												{
													this.typeValidation.Add(inputNum.Name, "text");
												}
												catch (System.ArgumentException)
												{
												}
												lab.MaximumSize = new System.Drawing.Size(widthLine - placeForButton * 2, 0);
												widthIn = 170;
												lab.AutoSize = true;
												x = widthLine - placeForButton * 2 + 20;
											}
											inputNum.Size = new System.Drawing.Size(widthIn, 20);
											inputNum.Name = node.InnerText;
											if (valueText != "")
											{
												inputNum.Text = valueText;
											}
											inputNum.Enabled = enab;
											XmlNode value2 = this.save.SelectSingleNode(pathXML + "/" + node.InnerText);
											if (value2 != null)
											{
												inputNum.Text = value2.InnerText;
											}
											inputNum.MouseClick += new MouseEventHandler(this.Control_S1_MouseClick);
											inputNum.LostFocus += new System.EventHandler(this.LostFocus);
											inputNum.TabIndex = count;
											count--;
											inputNum.Location = new System.Drawing.Point(x, y);
											inputNum.CausesValidation = true;
											lab.Name = "Lab_" + node.InnerText;
											if (isEnable)
											{
												this.controlsCanBeNull.Add(inputNum.Name);
											}
										}
									}
									this.Form2panel.Controls.Add(lab);
									this.Form2panel.Controls.Add(inputNum);
									if (lab.Height > 20)
									{
										y += lab.Height + 5;
									}
									else
									{
										y += 25;
									}
								}
							}
						}
						y += 50;
					}
				}
			}
			catch (XmlException ex)
			{
				MessageBox.Show("Nieprawidłowy XML - " + ex.Message, "Błąd");
			}
			catch (System.NullReferenceException ex2)
			{
				MessageBox.Show("Podanno inny xml definicje wygladu - " + ex2.Message, "Błąd");
			}
			this.Form2panel.Location = new System.Drawing.Point(this.protocolHeader.Location.X, this.protocolHeader.Location.Y + this.protocolHeader.Size.Height + 50);
			this.buttonNext.Text = "Dalej";
			this.buttonNext.Click -= new System.EventHandler(this.protocolForm2_Click);
			this.buttonNext.Click -= new System.EventHandler(this.protocolSummation_Click);
			this.buttonNext.Click -= new System.EventHandler(this.committee_Click);
			this.buttonNext.Click -= new System.EventHandler(this.committeeValid_Click);
			this.buttonNext.Click -= new System.EventHandler(this.signProtocol_Click);
			this.buttonNext.Click += new System.EventHandler(this.protocolSummation_Click);
			this.bottomPanel.Location = new System.Drawing.Point(this.Form2panel.Location.X, this.Form2panel.Location.Y + this.Form2panel.Size.Height);
			this.wait.setVisible(false);
		}
		private void getCommitee()
		{
			this.raportPanel.Visible = false;
			this.wait.setWaitPanel("Trwa ładowanie komisji", "Proszę czekać");
			this.wait.setVisible(true);
			try
			{
				if (this.isSave() != -1)
				{
					this.currentCommittee = false;
				}
				XmlNode nodesList = this.committee.SelectSingleNode("/komisja_sklad");
				if (this.isSave() == 0 || this.isSave() == 4)
				{
					XmlNode value = this.save.SelectSingleNode("/save/komisja_sklad");
					if (value != null)
					{
						nodesList = value;
					}
				}
				DataTable dtCommittee = new DataTable();
				int x = 0;
				int y = 30;
				if (dtCommittee.Columns["Lp"] == null)
				{
					dtCommittee.Columns.Add(new DataColumn("lp", typeof(string)));
				}
				if (dtCommittee.Columns["Imię"] == null)
				{
					dtCommittee.Columns.Add(new DataColumn("Imię", typeof(string)));
				}
				if (dtCommittee.Columns["Drugie imię"] == null)
				{
					dtCommittee.Columns.Add(new DataColumn("Drugie imię", typeof(string)));
				}
				if (dtCommittee.Columns["Nazwisko"] == null)
				{
					dtCommittee.Columns.Add(new DataColumn("Nazwisko", typeof(string)));
				}
				foreach (XmlNode person in nodesList)
				{
					DataRow dr = dtCommittee.NewRow();
					string name = "";
					dr[0] = dtCommittee.Rows.Count + 1;
					XmlNode firstname = person.Attributes.GetNamedItem("imie");
					if (firstname != null && firstname.Value != "")
					{
						name = HttpUtility.UrlDecode(firstname.Value);
					}
					dr[1] = name;
					name = "";
					XmlNode secondName = person.Attributes.GetNamedItem("imie2");
					if (secondName != null && secondName.Value != "")
					{
						name = HttpUtility.UrlDecode(secondName.Value);
					}
					dr[2] = name;
					name = "";
					XmlNode lastName = person.Attributes.GetNamedItem("nazwisko");
					if (lastName != null && lastName.Value != "")
					{
						name = HttpUtility.UrlDecode(lastName.Value);
					}
					dr[3] = name;
					dtCommittee.Rows.Add(dr);
				}
				this.personList.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
				this.personList.ReadOnly = false;
				this.personList.MaximumSize = new System.Drawing.Size(this.committeePanel.Size.Width, 0);
				this.personList.Size = new System.Drawing.Size(this.committeePanel.Size.Width, 100);
				XmlNode data = nodesList.Attributes.GetNamedItem("data_wersji");
				if (data != null)
				{
					Label lab = new Label();
					lab.Text = "Definicja komisji z pliku klk z dnia " + data.Value;
					lab.AutoSize = true;
					lab.MaximumSize = new System.Drawing.Size(this.committeePanel.Width - 300, 0);
					lab.Font = new System.Drawing.Font(this.myfont, 9f);
					lab.Location = new System.Drawing.Point(x, y);
					this.committeePanel.Controls.Add(lab);
					Button b = new Button();
					b.Size = new System.Drawing.Size(300, 20);
					b.Text = "Pobierz aktualny skład Komisji";
					b.Location = new System.Drawing.Point(this.committeePanel.Width - 300, y);
					b.Click += new System.EventHandler(this.getCurrentCommitteeKlk_Click);
					this.committeePanel.Controls.Add(b);
					if (lab.Height > 20)
					{
						y += lab.Height + 20;
					}
					else
					{
						y += 40;
					}
				}
				this.personList.Location = new System.Drawing.Point(x, y);
				this.personList.AllowUserToAddRows = true;
				this.personList.AllowUserToDeleteRows = true;
				this.personList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
				this.personList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
				this.personList.AutoSize = true;
				DataTable dt = new DataTable();
				this.personList.DataSource = dt;
				this.personList.DataSource = dtCommittee;
				this.personList.Refresh();
				this.personList.Columns["lp"].DisplayIndex = 0;
				this.personList.Columns["lp"].FillWeight = 30f;
				this.personList.Columns["lp"].SortMode = DataGridViewColumnSortMode.NotSortable;
				this.personList.Columns["Imię"].DisplayIndex = 1;
				this.personList.Columns["Imię"].SortMode = DataGridViewColumnSortMode.NotSortable;
				this.personList.Columns["Drugie imię"].DisplayIndex = 2;
				this.personList.Columns["Drugie imię"].SortMode = DataGridViewColumnSortMode.NotSortable;
				this.personList.Columns["Nazwisko"].DisplayIndex = 3;
				this.personList.Columns["Nazwisko"].SortMode = DataGridViewColumnSortMode.NotSortable;
				DataGridViewComboBoxColumn sel = new DataGridViewComboBoxColumn();
				sel.HeaderText = "Funkcja";
				sel.Name = "Funkcja";
				sel.Items.AddRange(new object[]
				{
					"CZŁONEK",
					"PRZEWODNICZĄCY",
					"ZASTĘPCA"
				});
				DataGridViewCheckBoxColumn btn2 = new DataGridViewCheckBoxColumn();
				btn2.HeaderText = "Obecny";
				btn2.Name = "action3";
				DataGridViewButtonColumn btn3 = new DataGridViewButtonColumn();
				btn3.HeaderText = "";
				btn3.Text = "Usuń";
				btn3.Name = "remove";
				btn3.UseColumnTextForButtonValue = true;
				if (this.personList.Columns["Funkcja"] == null)
				{
					this.personList.Columns.Insert(4, sel);
					this.personList.Columns["Funkcja"].FillWeight = 110f;
					this.personList.Columns["Funkcja"].SortMode = DataGridViewColumnSortMode.NotSortable;
				}
				else
				{
					this.personList.Columns["Funkcja"].DisplayIndex = 4;
					this.personList.Columns["Funkcja"].FillWeight = 110f;
					this.personList.Columns["Funkcja"].SortMode = DataGridViewColumnSortMode.NotSortable;
				}
				if (this.personList.Columns["action3"] == null)
				{
					this.personList.Columns.Insert(5, btn2);
					this.personList.Columns["action3"].FillWeight = 40f;
					this.personList.Columns["action3"].SortMode = DataGridViewColumnSortMode.NotSortable;
				}
				else
				{
					this.personList.Columns["action3"].DisplayIndex = 5;
					this.personList.Columns["action3"].FillWeight = 40f;
					this.personList.Columns["action3"].SortMode = DataGridViewColumnSortMode.NotSortable;
				}
				if (this.personList.Columns["remove"] == null)
				{
					this.personList.Columns.Insert(6, btn3);
					this.personList.Columns["remove"].FillWeight = 50f;
					this.personList.Columns["remove"].SortMode = DataGridViewColumnSortMode.NotSortable;
				}
				else
				{
					this.personList.Columns["remove"].DisplayIndex = 6;
					this.personList.Columns["remove"].FillWeight = 50f;
					this.personList.Columns["remove"].SortMode = DataGridViewColumnSortMode.NotSortable;
				}
				if (this.isSave() == 0 || this.isSave() == 4)
				{
					for (int i = 0; i < this.personList.Rows.Count - 1; i++)
					{
						XmlNode present = nodesList.ChildNodes[i].Attributes.GetNamedItem("obecny");
						XmlNode function = nodesList.ChildNodes[i].Attributes.GetNamedItem("funkcja");
						if (present != null)
						{
							this.personList.Rows[i].Cells["action3"].Value = System.Convert.ToBoolean(present.Value);
						}
						if (function != null)
						{
							if (function.Value.ToUpper() == "CZLONEK" || function.Value.ToUpper() == "CZŁONEK")
							{
								this.personList.Rows[i].Cells["Funkcja"].Value = "CZŁONEK";
							}
							if (function.Value.ToUpper() == "ZASTEPCA" || function.Value.ToUpper() == "ZASTĘPCA")
							{
								this.personList.Rows[i].Cells["Funkcja"].Value = "ZASTĘPCA";
							}
							if (function.Value.ToUpper() == "PRZEWODNICZACY" || function.Value.ToUpper() == "PRZEWODNICZĄCY")
							{
								this.personList.Rows[i].Cells["Funkcja"].Value = "PRZEWODNICZĄCY";
							}
						}
						this.personList.Rows[i].Cells["lp"].ToolTipText = "Kliknij dwukrotnie na komórkę, aby edytować";
						this.personList.Columns["Imię"].ToolTipText = "Kliknij dwukrotnie na komórkę, aby edytować";
						this.personList.Columns["Drugie imię"].ToolTipText = "Kliknij dwukrotnie na komórkę, aby edytować";
						this.personList.Columns["Nazwisko"].ToolTipText = "Kliknij dwukrotnie na komórkę, aby edytować";
					}
					this.personList.Rows[this.personList.Rows.Count - 1].Cells["lp"].ToolTipText = "Kliknij dwukrotnie na komórkę i uzupełnij dane, aby dodać członka komisji";
					this.personList.Columns["Imię"].ToolTipText = "Kliknij dwukrotnie na komórkę i uzupełnij dane, aby dodać członka komisji";
					this.personList.Columns["Drugie imię"].ToolTipText = "Kliknij dwukrotnie na komórkę i uzupełnij dane, aby dodać członka komisji";
					this.personList.Columns["Nazwisko"].ToolTipText = "Kliknij dwukrotnie na komórkę i uzupełnij dane, aby dodać członka komisji";
				}
				else
				{
					for (int i = 0; i < this.personList.Rows.Count - 1; i++)
					{
						this.personList.Rows[i].Cells["lp"].ToolTipText = "Kliknij dwukrotnie na komórkę, aby edytować";
						this.personList.Columns["Imię"].ToolTipText = "Kliknij dwukrotnie na komórkę, aby edytować";
						this.personList.Columns["Drugie imię"].ToolTipText = "Kliknij dwukrotnie na komórkę, aby edytować";
						this.personList.Columns["Nazwisko"].ToolTipText = "Kliknij dwukrotnie na komórkę, aby edytować";
						XmlNode function = nodesList.ChildNodes[i].Attributes.GetNamedItem("funkcja");
						if (function != null)
						{
							if (function.Value.ToUpper() == "CZLONEK" || function.Value.ToUpper() == "CZŁONEK")
							{
								this.personList.Rows[i].Cells["Funkcja"].Value = "CZŁONEK";
							}
							if (function.Value.ToUpper() == "ZASTEPCA" || function.Value.ToUpper() == "ZASTĘPCA")
							{
								this.personList.Rows[i].Cells["Funkcja"].Value = "ZASTĘPCA";
							}
							if (function.Value.ToUpper() == "PRZEWODNICZACY" || function.Value.ToUpper() == "PRZEWODNICZĄCY")
							{
								this.personList.Rows[i].Cells["Funkcja"].Value = "PRZEWODNICZĄCY";
							}
						}
					}
					if (this.personList.Rows.Count > 0)
					{
						this.personList.Rows[this.personList.Rows.Count - 1].Cells["lp"].ToolTipText = "Kliknij dwukrotnie na komórkę i uzupełnij dane, aby dodać członka komisji";
						this.personList.Columns["Imię"].ToolTipText = "Kliknij dwukrotnie na komórkę i uzupełnij dane, aby dodać członka komisji";
						this.personList.Columns["Drugie imię"].ToolTipText = "Kliknij dwukrotnie na komórkę i uzupełnij dane, aby dodać członka komisji";
						this.personList.Columns["Nazwisko"].ToolTipText = "Kliknij dwukrotnie na komórkę i uzupełnij dane, aby dodać członka komisji";
					}
				}
				this.personList.Visible = true;
				this.personList.RowsAdded += new DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
				this.personList.UserAddedRow += new DataGridViewRowEventHandler(this.DataGridView1_UserAddedRow);
				this.personList.Refresh();
				this.buttonNext.Text = "Dalej";
				this.buttonNext.Click -= new System.EventHandler(this.protocolForm2_Click);
				this.buttonNext.Click -= new System.EventHandler(this.protocolSummation_Click);
				this.buttonNext.Click -= new System.EventHandler(this.committee_Click);
				this.buttonNext.Click -= new System.EventHandler(this.committeeValid_Click);
				this.buttonNext.Click -= new System.EventHandler(this.signProtocol_Click);
				this.buttonNext.Click += new System.EventHandler(this.committeeValid_Click);
			}
			catch (XmlException ex)
			{
				MessageBox.Show("Nieprawidłowy XML - " + ex.Message, "Błąd");
			}
			catch (System.NullReferenceException ex2)
			{
				MessageBox.Show("Podanno inny xml niz header - " + ex2.Message, "Błąd");
			}
			this.bottomPanel.Location = new System.Drawing.Point(this.committeePanel.Location.X, this.committeePanel.Location.Y + this.committeePanel.Size.Height);
			this.wait.setVisible(false);
		}
		private void Summation()
		{
			int count = 0;
			foreach (Control c in this.protocolHeader.Controls)
			{
				if (c is TextBox)
				{
					(c as TextBox).TabIndex = count;
					count++;
				}
				if (c is MaskedTextBox)
				{
					(c as MaskedTextBox).TabIndex = count;
					count++;
				}
			}
			this.SummationPanel.Controls.Clear();
			this.wait.setWaitPanel("Trwa ładowanie podsumowania", "Proszę czekać");
			this.wait.setVisible(true);
			string pathXML = "";
			if (this.isSave() == 0 || this.isSave() == 4)
			{
				pathXML = "/save/form";
			}
			if (this.isSave() == 3)
			{
				pathXML = "/save/step3";
			}
			int y = 0;
			this.protocolHeader.TabIndex = 3;
			this.SummationPanel.TabIndex = 2;
			foreach (Control c in this.Form2panel.Controls)
			{
				int a = this.Form2panel.Controls.Count;
				if (c is TextBox)
				{
					TextBox lab = new TextBox();
					lab.Size = c.Size;
					lab.Location = c.Location;
					lab.Text = c.Text;
					lab.Name = c.Name;
					lab.Enabled = false;
					this.SummationPanel.Controls.Add(lab);
					if (y <= lab.Location.Y + lab.Size.Height)
					{
						y = lab.Location.Y + lab.Size.Height;
					}
				}
				if (c is Label)
				{
					Label lab2 = new Label();
					lab2.Size = c.Size;
					lab2.Location = c.Location;
					lab2.Text = c.Text;
					lab2.Name = c.Name;
					lab2.AutoSize = c.AutoSize;
					lab2.MaximumSize = c.MaximumSize;
					lab2.Font = c.Font;
					if (y <= lab2.Location.Y + lab2.Size.Height)
					{
						y = lab2.Location.Y + lab2.Size.Height;
					}
					this.SummationPanel.Controls.Add(lab2);
				}
			}
			y += 40;
			try
			{
				XmlNode nodesList = this.protocolDefinition.SelectSingleNode("/protokol_info");
				XmlNode candidatesRoot = this.candidates.SelectSingleNode("/listy");
				int x = 0;
				foreach (XmlNode fields in nodesList)
				{
					XmlNode type = fields.Attributes.GetNamedItem("type");
					if (type != null && type.Value == "additional-calculator")
					{
						this.wait.setWaitPanel("Trwa ładowanie podsumowania - ładowanie dodatkowych pól (adnotacji)", "Proszę czekać");
						foreach (XmlNode field in fields)
						{
							if (field.Name == "title")
							{
								x = 0;
								XmlNode bold = field.Attributes.GetNamedItem("bold");
								Label lab2 = new Label();
								lab2.Text = field.InnerText;
								lab2.AutoSize = true;
								lab2.MaximumSize = new System.Drawing.Size(this.SummationPanel.Size.Width, 0);
								lab2.Font = new System.Drawing.Font(this.myfont, 10f);
								lab2.Padding = new Padding(10, 0, 10, 0);
								if (bold.Value == "true")
								{
									lab2.Font = new System.Drawing.Font(this.myfont, 10f, System.Drawing.FontStyle.Bold);
								}
								lab2.Location = new System.Drawing.Point(x, y);
								this.SummationPanel.Controls.Add(lab2);
								y += lab2.Size.Height + 30;
							}
							if (field.Name == "field")
							{
								XmlNode data = field.Attributes.GetNamedItem("data");
								if (data != null && data.Value != "kandydaci")
								{
									x = 10;
									Label lab3 = new Label();
									TextBox input = new TextBox();
									foreach (XmlNode node in field)
									{
										if (node.Name == "name")
										{
											lab3.Text = node.ParentNode.Attributes.GetNamedItem("lp").Value + " " + node.InnerText;
											lab3.AutoSize = true;
											lab3.MaximumSize = new System.Drawing.Size(this.Form2panel.Width - 20, 0);
											lab3.MinimumSize = new System.Drawing.Size(this.Form2panel.Width - 20, 0);
											lab3.Font = new System.Drawing.Font(this.myfont, 9f);
											lab3.Location = new System.Drawing.Point(x, y);
											y += lab3.Size.Height + 45;
										}
										if (node.Name == "save_as")
										{
											input.Size = new System.Drawing.Size(this.Form2panel.Width - 20, 100);
											input.Name = node.InnerText;
											input.MouseClick += new MouseEventHandler(this.Control_S3_MouseClick);
											input.LostFocus += new System.EventHandler(this.LostFocus);
											XmlNode valueDefault = node.Attributes.GetNamedItem("valueDefault");
											if (valueDefault != null)
											{
												input.Text = valueDefault.InnerText;
											}
											XmlNode value = this.save.SelectSingleNode(pathXML + "/" + input.Name);
											if (value != null)
											{
												input.Text = value.InnerText;
											}
											input.Location = new System.Drawing.Point(x, y);
											input.CausesValidation = true;
											input.Multiline = true;
											input.ScrollBars = ScrollBars.Vertical;
											XmlNode valid = node.ParentNode.Attributes.GetNamedItem("data");
											if (valid != null && valid.Value == "text")
											{
												input.Validated += new System.EventHandler(this.normalText_Validated);
												try
												{
													string existvalue = this.typeValidation[input.Name];
												}
												catch (System.Collections.Generic.KeyNotFoundException)
												{
													this.typeValidation.Add(input.Name, "normalText");
												}
											}
											lab3.Name = "Lab_" + node.InnerText;
										}
									}
									this.SummationPanel.Controls.Add(lab3);
									this.SummationPanel.Controls.Add(input);
									if (lab3.Height > 100)
									{
										y += lab3.Height + 5;
									}
									else
									{
										y += 105;
									}
								}
							}
						}
					}
				}
			}
			catch (XmlException ex)
			{
				MessageBox.Show("Nieprawidłowy XML - " + ex.Message, "Błąd");
			}
			catch (System.NullReferenceException ex2)
			{
				MessageBox.Show("Podanno inny xml definicje wygladu - " + ex2.Message, "Błąd");
			}
			this.SummationPanel.Location = new System.Drawing.Point(this.protocolHeader.Location.X, this.protocolHeader.Location.Y + this.protocolHeader.Size.Height + 50);
			this.buttonNext.Text = "Dalej";
			this.buttonNext.Click -= new System.EventHandler(this.protocolForm2_Click);
			this.buttonNext.Click -= new System.EventHandler(this.protocolSummation_Click);
			this.buttonNext.Click -= new System.EventHandler(this.committee_Click);
			this.buttonNext.Click -= new System.EventHandler(this.committeeValid_Click);
			this.buttonNext.Click -= new System.EventHandler(this.signProtocol_Click);
			this.buttonNext.Click += new System.EventHandler(this.committee_Click);
			this.bottomPanel.Location = new System.Drawing.Point(this.SummationPanel.Location.X, this.SummationPanel.Location.Y + this.SummationPanel.Size.Height);
			this.report();
			this.wait.setVisible(false);
		}
		private void report()
		{
			this.raportPanel.Controls.Clear();
			this.raportPanel.MaximumSize = new System.Drawing.Size(776, 0);
			if (this.errorProvider1.hasHardWarning() || this.errorProvider1.hasWarning())
			{
				this.raportPanel.Visible = true;
				this.raportPanel.AutoSize = true;
				this.wait.setWaitPanel("Trwa ładowanie formularza wyjaśnień dla raportu ostrzeżeń", "Proszę czekać");
				this.wait.setVisible(true);
				int x = 0;
				int y = 0;
				this.raportPanel.TabIndex = 3;
				this.protocolHeader.TabIndex = 4;
				System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, KBWValue>> er2 = this.errorProvider1.getWarnings();
				System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, KBWValue>> er3 = this.errorProvider1.getHardWarnings();
				Label lab = new Label();
				lab.Text = "Wyjaśnienia do raportu ostrzeżeń";
				lab.AutoSize = true;
				lab.MaximumSize = new System.Drawing.Size(this.raportPanel.Size.Width, 0);
				lab.Font = new System.Drawing.Font(this.myfont, 10f);
				lab.Padding = new Padding(10, 0, 10, 0);
				lab.Font = new System.Drawing.Font(this.myfont, 10f, System.Drawing.FontStyle.Bold);
				lab.Location = new System.Drawing.Point(x, y);
				this.raportPanel.Controls.Add(lab);
				y += lab.Size.Height + 30;
				if (this.errorProvider1.hasHardWarning())
				{
					System.Collections.Generic.List<string> id = new System.Collections.Generic.List<string>();
					foreach (System.Collections.Generic.KeyValuePair<string, System.Collections.Generic.Dictionary<string, KBWValue>> contolErrors in er3)
					{
						foreach (System.Collections.Generic.KeyValuePair<string, KBWValue> item in contolErrors.Value)
						{
							x = 0;
							bool go = true;
							for (int i = 0; i < id.Count; i++)
							{
								if (id[i] == item.Key)
								{
									go = false;
									break;
								}
							}
							if (go)
							{
								id.Add(item.Key);
								Label j = new Label();
								string text = er3[contolErrors.Key][item.Key].getMessage();
								j.Text = text;
								j.AutoSize = true;
								j.MaximumSize = new System.Drawing.Size(this.errorPanel.Size.Width - 20, 0);
								j.Font = new System.Drawing.Font(this.myfont, 9f);
								j.Padding = new Padding(10, 0, 10, 0);
								j.Location = new System.Drawing.Point(x, y);
								this.raportPanel.Controls.Add(j);
								y += j.Height + 20;
								x = 10;
								TextBox input = new TextBox();
								input.Size = new System.Drawing.Size(this.raportPanel.Width - 20, 100);
								input.Name = item.Key + "_explain";
								input.MouseClick += new MouseEventHandler(this.Control_S3_MouseClick);
								input.LostFocus += new System.EventHandler(this.LostFocus);
								input.Location = new System.Drawing.Point(x, y);
								input.CausesValidation = true;
								input.Multiline = true;
								input.ScrollBars = ScrollBars.Vertical;
								input.Validated += new System.EventHandler(this.normalText_Validated);
								XmlNode saveValue = this.save.SelectSingleNode("/save/report/" + input.Name);
								if (saveValue != null)
								{
									input.Text = saveValue.InnerText;
								}
								this.raportPanel.Controls.Add(input);
								y += input.Height + 40;
							}
						}
					}
				}
				if (this.errorProvider1.hasWarning())
				{
					System.Collections.Generic.List<string> id = new System.Collections.Generic.List<string>();
					foreach (System.Collections.Generic.KeyValuePair<string, System.Collections.Generic.Dictionary<string, KBWValue>> contolErrors in er2)
					{
						foreach (System.Collections.Generic.KeyValuePair<string, KBWValue> item in contolErrors.Value)
						{
							x = 0;
							bool go = true;
							for (int i = 0; i < id.Count; i++)
							{
								if (id[i] == item.Key)
								{
									go = false;
									break;
								}
							}
							if (go)
							{
								id.Add(item.Key);
								Label j = new Label();
								string text = er2[contolErrors.Key][item.Key].getMessage();
								j.Text = text;
								j.AutoSize = true;
								j.MaximumSize = new System.Drawing.Size(this.errorPanel.Size.Width - 20, 0);
								j.Font = new System.Drawing.Font(this.myfont, 8f);
								j.Padding = new Padding(10, 0, 10, 0);
								j.Location = new System.Drawing.Point(x, y);
								this.raportPanel.Controls.Add(j);
								y += j.Height + 20;
								x = 10;
								TextBox input = new TextBox();
								input.Size = new System.Drawing.Size(this.raportPanel.Width - 20, 100);
								input.Name = item.Key + "_explain";
								input.MouseClick += new MouseEventHandler(this.Control_S3_MouseClick);
								input.LostFocus += new System.EventHandler(this.LostFocus);
								input.Location = new System.Drawing.Point(x, y);
								input.CausesValidation = true;
								input.Multiline = true;
								input.ScrollBars = ScrollBars.Vertical;
								input.Validated += new System.EventHandler(this.normalText_Validated);
								XmlNode saveValue = this.save.SelectSingleNode("/save/report/" + input.Name);
								if (saveValue != null)
								{
									input.Text = saveValue.InnerText;
								}
								this.raportPanel.Controls.Add(input);
								y += input.Height + 40;
							}
						}
					}
				}
				this.raportPanel.Location = new System.Drawing.Point(this.SummationPanel.Location.X, this.SummationPanel.Location.Y + this.SummationPanel.Size.Height + 50);
				this.bottomPanel.Location = new System.Drawing.Point(this.raportPanel.Location.X, this.raportPanel.Location.Y + this.raportPanel.Size.Height);
			}
			else
			{
				this.raportPanel.Visible = false;
			}
			this.wait.setVisible(false);
		}
		private void getDefinitionLastValidation()
		{
			this.wait.setWaitPanel("Trwa ładowanie definicji walidacji", "Proszę czekać");
			try
			{
				XmlNode nodesList = this.validateDefinition.SelectSingleNode("/validate_info");
				foreach (XmlNode rule in nodesList)
				{
					string left = "";
					string right = "";
					string note = "";
					string valFor = "";
					string id = "";
					Operation op = Operation.Null;
					ErrorType type = ErrorType.Null;
					XmlNode cor = rule.Attributes.GetNamedItem("correspondence");
					if (cor != null && cor.Value.ToUpper() != "BOTH")
					{
						if (cor.Value.ToUpper() == "TRUE" && !this.correspondence)
						{
							continue;
						}
						if (cor.Value.ToUpper() == "FALSE" && this.correspondence)
						{
							continue;
						}
					}
					XmlNode dis = rule.Attributes.GetNamedItem("district");
					if (dis != null && dis.Value.ToUpper() != "ALL")
					{
						if (System.Text.RegularExpressions.Regex.IsMatch(dis.Value.ToUpper(), "ONLY:"))
						{
							string allow = dis.Value.ToUpper().Replace("ONLY:", "");
							if (allow != this.typeObw.ToUpper())
							{
								continue;
							}
						}
						if (System.Text.RegularExpressions.Regex.IsMatch(dis.Value.ToUpper(), "EXCLUDE:"))
						{
							string allow = dis.Value.ToUpper().Replace("EXCLUDE:", "");
							if (allow == this.typeObw.ToUpper())
							{
								continue;
							}
						}
					}
					XmlNode delCandidates = rule.Attributes.GetNamedItem("deletedCandidate");
					if (delCandidates != null && delCandidates.Value.ToUpper() == "0")
					{
						if (this.deletedCandidates != 0)
						{
							continue;
						}
					}
					if (delCandidates != null && delCandidates.Value.ToUpper() == "1")
					{
						if (this.deletedCandidates == 0)
						{
							continue;
						}
					}
					XmlNode erType = rule.Attributes.GetNamedItem("type");
					XmlNode vFor = rule.Attributes.GetNamedItem("for");
					XmlNode erId = rule.Attributes.GetNamedItem("id");
					if (erType != null)
					{
						if (erType.Value == "twardy")
						{
							type = ErrorType.HardError;
						}
						if (erType.Value == "miekki")
						{
							type = ErrorType.Soft;
						}
						if (erType.Value == "twarde_ostrzezenie")
						{
							type = ErrorType.HardWarning;
						}
					}
					if (vFor != null)
					{
						valFor = vFor.Value;
					}
					if (erId != null)
					{
						id = erId.Value;
					}
					ValidationPatern vp = new ValidationPatern();
					foreach (XmlNode item in rule)
					{
						if (item.Name == "left")
						{
							left = item.InnerText;
						}
						if (item.Name == "right")
						{
							right = item.InnerText;
						}
						if (item.Name == "note")
						{
							note = item.InnerText;
						}
						if (item.Name == "operation")
						{
							if (item.InnerText == "==")
							{
								op = Operation.Equal;
							}
							if (item.InnerText == "!=")
							{
								op = Operation.Different;
							}
							if (item.InnerText == "less")
							{
								op = Operation.Less;
							}
							if (item.InnerText == "less=")
							{
								op = Operation.LessOrEqual;
							}
							if (item.InnerText == "more")
							{
								op = Operation.More;
							}
							if (item.InnerText == "more=")
							{
								op = Operation.MoreOrEqual;
							}
						}
						if (item.Name == "fields")
						{
							foreach (XmlNode field in item)
							{
								XmlNode name = field.Attributes.GetNamedItem("name");
								XmlNode from = field.Attributes.GetNamedItem("from");
								XmlNode v = field.Attributes.GetNamedItem("variable1");
								XmlNode v1m = field.Attributes.GetNamedItem("variable1Mean");
								XmlNode v2 = field.Attributes.GetNamedItem("variable2");
								XmlNode v2m = field.Attributes.GetNamedItem("variable2Mean");
								if (name != null && from != null && v != null && v1m != null && v2 != null && v2m != null && v.Value != "" && v1m.Value != "" && v2.Value != "" && v2m.Value != "")
								{
									if (v1m.Value == "kandydat" && v2m.Value == "lista")
									{
										vp.addVariable(name.Value);
										for (int i = 0; i < this.countcandidatesoflist.Count; i++)
										{
											for (int j = 0; j < this.countcandidatesoflist[i][1]; j++)
											{
												string fieldname = name.Value.Replace(v.Value, (j + 1).ToString());
												fieldname = fieldname.Replace(v2.Value, (i + 1).ToString());
												vp.addField(fieldname, from.Value, true);
											}
										}
									}
									if (v1m.Value == "lista" && v2m.Value == "kandydat")
									{
										vp.addVariable(name.Value);
										for (int i = 1; i <= this.countcandidatesoflist.Count; i++)
										{
											for (int j = 1; j <= this.countcandidatesoflist[i][1]; j++)
											{
												string fieldname = name.Value.Replace(v.Value, i.ToString());
												fieldname = name.Value.Replace(v2.Value, j.ToString());
												vp.addField(fieldname, from.Value, true);
											}
										}
									}
								}
								else
								{
									if ((name != null && from != null && v != null && v1m != null && v2 == null && v2m == null) || (name != null && from != null && v != null && v1m != null && v2 != null && v2m != null && v.Value != "" && v1m.Value != "" && v2.Value == "" && v2m.Value == ""))
									{
										vp.addVariable(name.Value);
										if (v1m.Value == "kandydat" && this.countcandidatesoflist.Count == 1)
										{
											for (int j = 1; j <= this.countcandidatesoflist[0][1]; j++)
											{
												string fieldname = name.Value.Replace(v.Value, j.ToString());
												vp.addField(fieldname, from.Value, true);
											}
										}
										if (v1m.Value == "lista" && this.countcandidatesoflist.Count != 0)
										{
											for (int j = 1; j <= this.countcandidatesoflist.Count; j++)
											{
												string fieldname = name.Value.Replace(v.Value, j.ToString());
												vp.addField(fieldname, from.Value, true);
											}
										}
										if (v1m.Value == "kandydatZa" && this.countcandidatesoflist.Count == 3)
										{
											for (int j = 1; j <= this.countcandidatesoflist[1][1]; j++)
											{
												string fieldname = name.Value.Replace(v.Value, j.ToString());
												vp.addField(fieldname, from.Value, true);
											}
										}
										if (v1m.Value == "kandydatPrzeciw" && this.countcandidatesoflist.Count == 3)
										{
											for (int j = 1; j <= this.countcandidatesoflist[2][1]; j++)
											{
												string fieldname = name.Value.Replace(v.Value, j.ToString());
												vp.addField(fieldname, from.Value, true);
											}
										}
									}
									else
									{
										if (name != null && from != null)
										{
											vp.addField(name.Value, from.Value);
										}
									}
								}
							}
						}
					}
					vp.SetValidationPatern(valFor, left, right, note, id, op, type);
					this.lastValidators.Add(vp);
				}
			}
			catch (XmlException)
			{
				MessageBox.Show("Nieprawidłowy XML", "Błąd");
			}
			catch (System.NullReferenceException)
			{
				MessageBox.Show("Podanno inny xml niz definicja walidacji", "Błąd");
			}
		}
		private void getSignPage()
		{
			this.wait.setWaitPanel("Trwa ładowanie sekcji podpisu protokołu", "Proszę czekać");
			this.wait.setVisible(true);
			int x = 0;
			int y = 0;
			this.errorPanel.Visible = false;
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
					this.save.Load(this.savePath);
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
					string paternA = "";
					string paternApowiat = "";
					string jnsTMP = saveJns;
					saveJns = System.Convert.ToInt32(saveJns).ToString();
					if (jnsTMP.Length < 6)
					{
						while (jnsTMP.Length < 6)
						{
							jnsTMP = "0" + jnsTMP;
						}
					}
					if (jnsTMP.Substring(0, 4) == "1465")
					{
						if (this.inst == "RDP" || this.inst == "RDW")
						{
							paternApowiat = "^" + saveElection + "-146500-A_";
						}
					}
					else
					{
						if (this.inst == "RDP" || this.inst == "RDW" || this.inst == "WBP")
						{
							paternApowiat = string.Concat(new string[]
							{
								"^",
								saveElection,
								"-",
								System.Convert.ToInt32(jnsTMP.Substring(0, 4)).ToString(),
								"00-A_"
							});
						}
					}
					if (jnsTMP.Substring(0, 4) == "1465")
					{
						if (jnsTMP[4] != '\0' || jnsTMP[5] != '\0')
						{
							if (this.inst == "RDA")
							{
								paternA = string.Concat(new string[]
								{
									"^",
									saveElection,
									"-",
									saveJns,
									"-A_"
								});
							}
						}
					}
					else
					{
						if (jnsTMP[2] == '7' || jnsTMP[2] == '6')
						{
							if (this.inst == "RDA" || this.inst == "RDW" || this.inst == "WBP")
							{
								paternA = string.Concat(new string[]
								{
									"^",
									saveElection,
									"-",
									saveJns,
									"-A_"
								});
							}
						}
						else
						{
							if (this.inst == "RDA" || this.inst == "WBP")
							{
								paternA = string.Concat(new string[]
								{
									"^",
									saveElection,
									"-",
									saveJns,
									"-A_"
								});
							}
						}
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
								patern += "_";
								string a = filename[filename.Length - 1];
								if (System.Text.RegularExpressions.Regex.IsMatch(a, patern))
								{
									go = true;
								}
								if (paternA != "" && System.Text.RegularExpressions.Regex.IsMatch(a, paternA))
								{
									go = true;
								}
								if (paternApowiat != "" && System.Text.RegularExpressions.Regex.IsMatch(a, paternApowiat))
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
					btn.Text = "Podpisz";
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
			this.wait.setVisible(false);
		}
		private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			if (e.RowCount == 1)
			{
				try
				{
					(sender as DataGridView).Rows[e.RowIndex - 1].Cells["Lp"].Value = e.RowIndex.ToString();
					this.currentCommittee = false;
				}
				catch (System.ArgumentException)
				{
				}
			}
		}
		private void DataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
		{
			this.bottomPanel.Location = new System.Drawing.Point(this.committeePanel.Location.X, this.committeePanel.Location.Y + this.committeePanel.Size.Height);
		}
		private void protocolForm1_Click(object sender, System.EventArgs e)
		{
			if (this.currentStep >= 3 || this.currentStep == 0)
			{
				bool go = this.edit();
				if (go)
				{
					this.currentStep = 1;
					this.buttonNext.Text = "Dalej";
					this.buttonNext.Click -= new System.EventHandler(this.protocolForm2_Click);
					this.buttonNext.Click -= new System.EventHandler(this.protocolSummation_Click);
					this.buttonNext.Click -= new System.EventHandler(this.committee_Click);
					this.buttonNext.Click -= new System.EventHandler(this.committeeValid_Click);
					this.buttonNext.Click -= new System.EventHandler(this.signProtocol_Click);
					this.buttonNext.Click += new System.EventHandler(this.protocolForm2_Click);
					this.buttonNext.Enabled = true;
					this.bottomPanel.Location = new System.Drawing.Point(this.Form1panel.Location.X, this.Form1panel.Location.Y + this.Form1panel.Size.Height);
				}
			}
			else
			{
				this.buttonNext.Visible = true;
				this.unlockHeader();
				int count = 1;
				foreach (Control c in this.protocolHeader.Controls)
				{
					if (c is TextBox)
					{
						(c as TextBox).TabIndex = count;
						count++;
					}
					if (c is MaskedTextBox)
					{
						(c as MaskedTextBox).TabIndex = count;
						count++;
					}
				}
				this.protocolHeader.TabIndex = 2;
				this.Form1panel.TabIndex = 3;
				this.Form1panel.Visible = true;
				this.Form2panel.Visible = false;
				this.committeePanel.Visible = false;
				this.SummationPanel.Visible = false;
				this.signPanel.Visible = false;
				this.protocolForm2.BackColor = System.Drawing.SystemColors.Control;
				this.protocolForm1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
				this.protocolSummation.BackColor = System.Drawing.SystemColors.Control;
				this.protocolCommittee.BackColor = System.Drawing.SystemColors.Control;
				this.signProtocol.BackColor = System.Drawing.SystemColors.Control;
				this.raportPanel.Visible = false;
				if (this.currentStep != 1)
				{
					this.buttonNext.Text = "Dalej";
					this.buttonNext.Enabled = false;
				}
				else
				{
					this.buttonNext.Text = "Dalej";
					this.buttonNext.Click -= new System.EventHandler(this.protocolForm2_Click);
					this.buttonNext.Click -= new System.EventHandler(this.protocolSummation_Click);
					this.buttonNext.Click -= new System.EventHandler(this.committee_Click);
					this.buttonNext.Click -= new System.EventHandler(this.committeeValid_Click);
					this.buttonNext.Click -= new System.EventHandler(this.signProtocol_Click);
					this.buttonNext.Click += new System.EventHandler(this.protocolForm2_Click);
					this.buttonNext.Enabled = true;
				}
				this.setFirstFocus();
				this.bottomPanel.Location = new System.Drawing.Point(this.Form1panel.Location.X, this.Form1panel.Location.Y + this.Form1panel.Size.Height);
				this.saves(this.currentStep);
			}
		}
		private void protocolForm2_Click(object sender, System.EventArgs e)
		{
			this.buttonNext.Visible = true;
			if ((sender as Button).Name == "buttonNext")
			{
				this.clicNext = true;
				this.protocolForm1.Enabled = true;
				this.protocolForm2.Enabled = false;
				this.protocolSummation.Enabled = false;
				this.protocolCommittee.Enabled = false;
				this.signProtocol.Enabled = false;
				this.wait.setVisible(true);
				this.rangeValidation(this.Form1panel);
				this.isNotEmpty(this.Form1panel);
				try
				{
					this.lastValidation(this.Form1panel);
				}
				catch (System.Exception ex_B0)
				{
				}
				this.wait.setVisible(false);
				if (this.IsValid())
				{
					this.error = false;
				}
				else
				{
					this.error = true;
				}
				if (!this.error)
				{
					this.clicNext = false;
					if (this.errorProvider1.hasHardWarning() || this.errorProvider1.hasWarning())
					{
						this.printErrors(this.currentStep);
					}
					else
					{
						this.errorPanel.Visible = false;
						this.warningPanel.Visible = false;
						this.errorWarningPanel.Visible = false;
					}
					this.protocolForm1.Enabled = true;
					this.protocolForm2.Enabled = true;
					this.protocolSummation.Enabled = false;
					this.protocolCommittee.Enabled = false;
					this.signProtocol.Enabled = false;
					this.currentStep = 2;
					this.getCalculator2();
					this.errorPanel.Visible = false;
					this.Form1panel.Visible = false;
					this.Form2panel.Visible = true;
					this.committeePanel.Visible = false;
					this.SummationPanel.Visible = false;
					this.signPanel.Visible = false;
					this.setLastFocus();
					this.protocolForm1.BackColor = System.Drawing.SystemColors.Control;
					this.protocolForm2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
					this.protocolSummation.BackColor = System.Drawing.SystemColors.Control;
					this.protocolCommittee.BackColor = System.Drawing.SystemColors.Control;
					this.signProtocol.BackColor = System.Drawing.SystemColors.Control;
				}
				else
				{
					this.setFirstFocus();
					this.printErrors(this.currentStep);
				}
				this.error = false;
			}
			else
			{
				if (this.currentStep >= 3 || this.currentStep == 0)
				{
					bool go = this.edit();
					if (go)
					{
						this.currentStep = 1;
						this.buttonNext.Text = "Dalej";
						this.buttonNext.Click -= new System.EventHandler(this.protocolForm2_Click);
						this.buttonNext.Click -= new System.EventHandler(this.protocolSummation_Click);
						this.buttonNext.Click -= new System.EventHandler(this.committee_Click);
						this.buttonNext.Click -= new System.EventHandler(this.committeeValid_Click);
						this.buttonNext.Click -= new System.EventHandler(this.signProtocol_Click);
						this.buttonNext.Click += new System.EventHandler(this.protocolForm2_Click);
						this.buttonNext.Enabled = true;
						this.bottomPanel.Location = new System.Drawing.Point(this.Form2panel.Location.X, this.Form2panel.Location.Y + this.Form2panel.Size.Height);
					}
				}
				else
				{
					this.raportPanel.Visible = false;
					this.unlockHeader();
					int count = this.Form1panel.Controls.Count * 2;
					foreach (Control c in this.protocolHeader.Controls)
					{
						if (c is TextBox)
						{
							(c as TextBox).TabIndex = count;
							count--;
						}
						if (c is MaskedTextBox)
						{
							(c as MaskedTextBox).TabIndex = count;
							count--;
						}
					}
					this.Form2panel.TabIndex = 2;
					this.protocolHeader.TabIndex = 3;
					this.Form1panel.Visible = false;
					this.Form2panel.Visible = true;
					this.committeePanel.Visible = false;
					this.SummationPanel.Visible = false;
					this.protocolForm1.BackColor = System.Drawing.SystemColors.Control;
					this.protocolForm2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
					this.protocolSummation.BackColor = System.Drawing.SystemColors.Control;
					this.protocolCommittee.BackColor = System.Drawing.SystemColors.Control;
					this.signProtocol.BackColor = System.Drawing.SystemColors.Control;
					if (this.currentStep != 2)
					{
						this.buttonNext.Text = "Dalej";
						this.buttonNext.Enabled = false;
					}
					else
					{
						this.buttonNext.Text = "Dalej";
						this.buttonNext.Click -= new System.EventHandler(this.protocolForm2_Click);
						this.buttonNext.Click -= new System.EventHandler(this.protocolSummation_Click);
						this.buttonNext.Click -= new System.EventHandler(this.committee_Click);
						this.buttonNext.Click -= new System.EventHandler(this.committeeValid_Click);
						this.buttonNext.Click -= new System.EventHandler(this.signProtocol_Click);
						this.buttonNext.Click += new System.EventHandler(this.protocolSummation_Click);
						this.buttonNext.Enabled = true;
					}
					this.bottomPanel.Location = new System.Drawing.Point(this.Form2panel.Location.X, this.Form2panel.Location.Y + this.Form2panel.Size.Height);
					this.setLastFocus();
					this.saves(this.currentStep);
				}
			}
		}
		private void protocolSummation_Click(object sender, System.EventArgs e)
		{
			this.buttonNext.Visible = true;
			if ((sender as Button).Name == "buttonNext")
			{
				this.clicNext = true;
				this.protocolForm1.Enabled = true;
				this.protocolForm2.Enabled = true;
				this.protocolSummation.Enabled = false;
				this.protocolCommittee.Enabled = false;
				this.signProtocol.Enabled = false;
				this.wait.setWaitPanel("Trwa walidacja protokołu", "Proszę czekać");
				this.wait.setVisible(true);
				this.rangeValidation(this.Form2panel);
				this.isNotEmpty(this.Form1panel);
				this.isNotEmpty(this.Form2panel);
				this.checkFieldValidation();
				try
				{
					this.lastValidation(this.Form2panel);
				}
				catch (System.Exception ex_D8)
				{
				}
				this.wait.setVisible(false);
				if (this.IsValid())
				{
					this.error = false;
				}
				else
				{
					this.error = true;
				}
				if (!this.error)
				{
					this.clicNext = false;
					if (this.errorProvider1.hasHardWarning() || this.errorProvider1.hasWarning())
					{
						this.printErrors(this.currentStep);
					}
					else
					{
						this.errorPanel.Visible = false;
						this.warningPanel.Visible = false;
						this.errorWarningPanel.Visible = false;
					}
					this.protocolForm1.Enabled = true;
					this.protocolForm2.Enabled = true;
					this.protocolSummation.Enabled = true;
					this.protocolCommittee.Enabled = false;
					this.signProtocol.Enabled = false;
					this.Summation();
					this.lockHeader();
					this.currentStep = 3;
					this.errorPanel.Visible = false;
					this.Form1panel.Visible = false;
					this.Form2panel.Visible = false;
					this.committeePanel.Visible = false;
					this.SummationPanel.Visible = true;
					this.signPanel.Visible = false;
					this.setFirstFocus(this.SummationPanel);
					this.protocolForm2.BackColor = System.Drawing.SystemColors.Control;
					this.protocolForm1.BackColor = System.Drawing.SystemColors.Control;
					this.protocolSummation.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
					this.protocolCommittee.BackColor = System.Drawing.SystemColors.Control;
					this.signProtocol.BackColor = System.Drawing.SystemColors.Control;
					if (this.errorProvider1.hasHardWarning() || this.errorProvider1.hasWarning())
					{
						this.raportPanel.Location = new System.Drawing.Point(this.SummationPanel.Location.X, this.SummationPanel.Location.Y + this.SummationPanel.Size.Height + 50);
						this.bottomPanel.Location = new System.Drawing.Point(this.raportPanel.Location.X, this.raportPanel.Location.Y + this.raportPanel.Size.Height);
					}
					else
					{
						this.bottomPanel.Location = new System.Drawing.Point(this.SummationPanel.Location.X, this.SummationPanel.Location.Y + this.SummationPanel.Size.Height + 50);
					}
				}
				else
				{
					this.setFirstFocus();
					this.printErrors(this.currentStep);
				}
				this.error = false;
			}
			else
			{
				this.raportPanel.Visible = true;
				this.lockHeader();
				this.setFirstFocus(this.SummationPanel);
				this.Form1panel.Visible = false;
				this.Form2panel.Visible = false;
				this.committeePanel.Visible = false;
				this.SummationPanel.Visible = true;
				this.protocolForm2.BackColor = System.Drawing.SystemColors.Control;
				this.protocolForm1.BackColor = System.Drawing.SystemColors.Control;
				this.protocolSummation.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
				this.protocolCommittee.BackColor = System.Drawing.SystemColors.Control;
				this.signProtocol.BackColor = System.Drawing.SystemColors.Control;
				if (this.currentStep != 3)
				{
					this.buttonNext.Text = "Dalej";
					this.buttonNext.Enabled = false;
				}
				else
				{
					this.buttonNext.Text = "Dalej";
					this.buttonNext.Click -= new System.EventHandler(this.protocolForm2_Click);
					this.buttonNext.Click -= new System.EventHandler(this.protocolSummation_Click);
					this.buttonNext.Click -= new System.EventHandler(this.committee_Click);
					this.buttonNext.Click -= new System.EventHandler(this.committeeValid_Click);
					this.buttonNext.Click -= new System.EventHandler(this.signProtocol_Click);
					this.buttonNext.Click += new System.EventHandler(this.committee_Click);
					this.buttonNext.Enabled = true;
				}
				this.setFirstFocus(this.SummationPanel);
				if (this.errorProvider1.hasHardWarning() || this.errorProvider1.hasWarning())
				{
					this.raportPanel.Location = new System.Drawing.Point(this.SummationPanel.Location.X, this.SummationPanel.Location.Y + this.SummationPanel.Size.Height + 50);
					this.bottomPanel.Location = new System.Drawing.Point(this.raportPanel.Location.X, this.raportPanel.Location.Y + this.raportPanel.Size.Height);
				}
				else
				{
					this.bottomPanel.Location = new System.Drawing.Point(this.SummationPanel.Location.X, this.SummationPanel.Location.Y + this.SummationPanel.Size.Height + 50);
				}
			}
			this.saves(this.currentStep);
		}
		private void committee_Click(object sender, System.EventArgs e)
		{
			this.buttonNext.Visible = true;
			if ((sender as Button).Name == "buttonNext")
			{
				this.buttonNext.Enabled = false;
				this.clicNext = true;
				this.protocolForm1.Enabled = true;
				this.protocolForm2.Enabled = true;
				this.protocolSummation.Enabled = true;
				this.protocolCommittee.Enabled = false;
				this.signProtocol.Enabled = false;
				this.wait.setWaitPanel("Trwa walidacja protokołu", "Proszę czekać");
				this.wait.setVisible(true);
				this.rangeValidation(this.Form2panel);
				this.checkFieldValidation();
				this.isNotEmpty(this.Form2panel);
				this.isNotEmpty(this.raportPanel);
				this.lastValidation(this.Form2panel);
				this.isNotEmpty(this.SummationPanel);
				this.checkadnotation();
				this.wait.setVisible(false);
				if (this.IsValid())
				{
					this.error = false;
				}
				else
				{
					this.error = true;
				}
				this.saves(this.currentStep);
				if (!this.error)
				{
					this.clicNext = false;
					if (this.errorProvider1.hasHardWarning() || this.errorProvider1.hasWarning())
					{
						this.printErrors(this.currentStep);
					}
					else
					{
						this.errorPanel.Visible = false;
						this.warningPanel.Visible = false;
						this.errorWarningPanel.Visible = false;
					}
					if (this.errorProvider1.hasHardWarning())
					{
						this.validateExportedXml(this.currentStep);
						string[] pathItem = this.savePath.Split(new char[]
						{
							'\\'
						});
						EnteringCodes entry = new EnteringCodes(pathItem[pathItem.Length - 1].Replace(".xml", ""), this.savePath, this.OU, this.licensePath, this);
						entry.ShowDialog();
						if (this.goodcertificate)
						{
							this.raportPanel.Visible = false;
							this.currentStep = 4;
							this.protocolForm1.Enabled = true;
							this.protocolForm2.Enabled = true;
							this.protocolSummation.Enabled = true;
							this.protocolCommittee.Enabled = true;
							this.signProtocol.Enabled = false;
							this.committeePanel.Visible = true;
							this.errorPanel.Visible = false;
							this.Form1panel.Visible = false;
							this.Form2panel.Visible = false;
							this.personList.Visible = true;
							this.signPanel.Visible = false;
							this.SummationPanel.Visible = false;
							this.committeePanel.MaximumSize = new System.Drawing.Size(this.committeePanel.Size.Width, 0);
							this.committeePanel.Location = new System.Drawing.Point(0, this.protocolHeader.Size.Height + this.protocolHeader.Location.Y);
							this.committeePanel.AutoSize = true;
							this.protocolForm2.BackColor = System.Drawing.SystemColors.Control;
							this.protocolForm1.BackColor = System.Drawing.SystemColors.Control;
							this.protocolSummation.BackColor = System.Drawing.SystemColors.Control;
							this.protocolCommittee.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
							this.signProtocol.BackColor = System.Drawing.SystemColors.Control;
							this.getCommitee();
							this.buttonNext.Click -= new System.EventHandler(this.protocolForm2_Click);
							this.buttonNext.Click -= new System.EventHandler(this.protocolSummation_Click);
							this.buttonNext.Click -= new System.EventHandler(this.committee_Click);
							this.buttonNext.Click -= new System.EventHandler(this.committeeValid_Click);
							this.buttonNext.Click -= new System.EventHandler(this.signProtocol_Click);
							this.buttonNext.Click += new System.EventHandler(this.committeeValid_Click);
							this.bottomPanel.Location = new System.Drawing.Point(this.committeePanel.Location.X, this.committeePanel.Location.Y + this.committeePanel.Size.Height);
							this.setFirstFocus(this.committeePanel);
							this.saves(this.currentStep);
						}
						this.goodcertificate = false;
					}
					else
					{
						this.currentStep = 4;
						this.protocolForm1.Enabled = true;
						this.protocolForm2.Enabled = true;
						this.protocolSummation.Enabled = true;
						this.protocolCommittee.Enabled = true;
						this.signProtocol.Enabled = false;
						this.committeePanel.Visible = true;
						this.errorPanel.Visible = false;
						this.Form1panel.Visible = false;
						this.Form2panel.Visible = false;
						this.personList.Visible = true;
						this.signPanel.Visible = false;
						this.SummationPanel.Visible = false;
						this.committeePanel.MaximumSize = new System.Drawing.Size(this.committeePanel.Size.Width, 0);
						this.committeePanel.Location = new System.Drawing.Point(0, this.protocolHeader.Size.Height + this.protocolHeader.Location.Y);
						this.committeePanel.AutoSize = true;
						this.protocolForm2.BackColor = System.Drawing.SystemColors.Control;
						this.protocolForm1.BackColor = System.Drawing.SystemColors.Control;
						this.protocolSummation.BackColor = System.Drawing.SystemColors.Control;
						this.protocolCommittee.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
						this.signProtocol.BackColor = System.Drawing.SystemColors.Control;
						this.getCommitee();
						this.setFirstFocus(this.committeePanel);
						this.saves(this.currentStep);
					}
				}
				else
				{
					this.setFirstFocus();
					this.printErrors(this.currentStep);
				}
				this.error = false;
			}
			else
			{
				this.raportPanel.Visible = false;
				this.lockHeader();
				this.lockHeader();
				this.setFirstFocus(this.committeePanel);
				this.committeePanel.Visible = true;
				this.errorPanel.Visible = false;
				this.Form1panel.Visible = false;
				this.Form2panel.Visible = false;
				this.personList.Visible = true;
				this.signPanel.Visible = false;
				this.SummationPanel.Visible = false;
				this.committeePanel.MaximumSize = new System.Drawing.Size(this.committeePanel.Size.Width, 0);
				this.committeePanel.Location = new System.Drawing.Point(0, this.protocolHeader.Size.Height + this.protocolHeader.Location.Y);
				this.committeePanel.AutoSize = true;
				this.protocolForm2.BackColor = System.Drawing.SystemColors.Control;
				this.protocolForm1.BackColor = System.Drawing.SystemColors.Control;
				this.protocolSummation.BackColor = System.Drawing.SystemColors.Control;
				this.protocolCommittee.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
				this.signProtocol.BackColor = System.Drawing.SystemColors.Control;
				if (this.currentStep != 4)
				{
					this.buttonNext.Text = "Dalej";
					this.buttonNext.Enabled = false;
				}
				else
				{
					this.buttonNext.Text = "Dalej";
					this.buttonNext.Click -= new System.EventHandler(this.protocolForm2_Click);
					this.buttonNext.Click -= new System.EventHandler(this.protocolSummation_Click);
					this.buttonNext.Click -= new System.EventHandler(this.committee_Click);
					this.buttonNext.Click -= new System.EventHandler(this.committeeValid_Click);
					this.buttonNext.Click -= new System.EventHandler(this.signProtocol_Click);
					this.buttonNext.Click += new System.EventHandler(this.committeeValid_Click);
					this.buttonNext.Enabled = true;
				}
				this.setFirstFocus();
				this.bottomPanel.Location = new System.Drawing.Point(this.committeePanel.Location.X, this.committeePanel.Location.Y + this.committeePanel.Size.Height);
				this.saves(this.currentStep);
			}
			this.buttonNext.Enabled = true;
		}
		private void signProtocol_Click(object sender, System.EventArgs e)
		{
			this.protocolForm1.Enabled = false;
			this.protocolForm2.Enabled = false;
			this.protocolSummation.Enabled = false;
			this.protocolCommittee.Enabled = false;
			this.signProtocol.Enabled = true;
			this.protocolForm2.BackColor = System.Drawing.SystemColors.Control;
			this.protocolForm1.BackColor = System.Drawing.SystemColors.Control;
			this.protocolSummation.BackColor = System.Drawing.SystemColors.Control;
			this.protocolCommittee.BackColor = System.Drawing.SystemColors.Control;
			this.signProtocol.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			this.errorPanel.Visible = false;
			this.Form1panel.Visible = false;
			this.Form2panel.Visible = false;
			this.committeePanel.Visible = false;
			this.SummationPanel.Visible = false;
			this.signPanel.Visible = true;
			this.setFirstFocus();
			this.buttonNext.Visible = false;
			this.saves(4);
			this.getSignPage();
		}
		private void committeeValid_Click(object sender, System.EventArgs e)
		{
			try
			{
				XmlNode nodesList = this.committee.SelectSingleNode("/komisja_sklad");
				int countPerson = System.Convert.ToInt32(nodesList.Attributes.GetNamedItem("l_wymaganych").Value);
				int minCountPerson = System.Convert.ToInt32(nodesList.Attributes.GetNamedItem("min_l_wymaganych").Value);
				int isChairman = 0;
				int isSecondChairman = 0;
				int presentPerson = 0;
				int errors = 0;
				string pattern = "^[^0-9<>&;:" + '"'.ToString() + "]+$";
				for (int i = 0; i < this.personList.Rows.Count - 1; i++)
				{
					object b = this.personList.Rows[i].Cells["action3"].Value;
					if (b != null && b.ToString().ToLower() == "true")
					{
						presentPerson++;
					}
					object a = this.personList.Rows[i].Cells["Funkcja"].Value;
					if (a != null && a.ToString().ToUpper() == "PRZEWODNICZĄCY" && b != null && b.ToString().ToLower() == "true")
					{
						isChairman++;
					}
					if (a != null && a.ToString().ToUpper() == "ZASTĘPCA" && b != null && b.ToString().ToLower() == "true")
					{
						isSecondChairman++;
					}
					if (a == null || (a != null && a.ToString() == ""))
					{
						this.errorProvider1.SetErrorWithCount(this.komisjaL1, "Nie wypełnione pole/a \"Funkcja\"", "FNW" + i.ToString(), "4");
						errors++;
					}
					else
					{
						this.errorProvider1.SetErrorWithCount(this.komisjaL1, "", "FNW" + i.ToString(), "4");
					}
					object c = this.personList.Rows[i].Cells["Imię"].Value;
					if (c == null || (c != null && c.ToString() == ""))
					{
						this.errorProvider1.SetErrorWithCount(this.komisjaL1, "Nie wypełnione pole/a \"Imię\"", "INW" + i.ToString(), "4");
						errors++;
					}
					else
					{
						this.errorProvider1.SetErrorWithCount(this.komisjaL1, "", "INW" + i.ToString(), "4");
						if (!System.Text.RegularExpressions.Regex.IsMatch(c.ToString(), pattern))
						{
							this.errorProvider1.SetErrorWithCount(this.komisjaL1, "Nieprawidłowo wypełnione pole/a \"Imię\"", "F-INW" + i.ToString(), "4");
							errors++;
						}
						else
						{
							this.errorProvider1.SetErrorWithCount(this.komisjaL1, "", "F-INW" + i.ToString(), "4");
						}
					}
					object c2 = this.personList.Rows[i].Cells["Drugie imię"].Value;
					if (c2 != null && c2.ToString() != "")
					{
						if (!System.Text.RegularExpressions.Regex.IsMatch(c2.ToString(), pattern))
						{
							this.errorProvider1.SetErrorWithCount(this.komisjaL1, "Nieprawidłowo wypełnione pole/a \"Drugie imię\"", "F-I2NW" + i.ToString(), "4");
							errors++;
						}
						else
						{
							this.errorProvider1.SetErrorWithCount(this.komisjaL1, "", "F-I2NW" + i.ToString(), "4");
						}
					}
					else
					{
						this.errorProvider1.SetErrorWithCount(this.komisjaL1, "", "F-I2NW" + i.ToString(), "4");
					}
					object d = this.personList.Rows[i].Cells["Nazwisko"].Value;
					if (d == null || (d != null && d.ToString() == ""))
					{
						this.errorProvider1.SetErrorWithCount(this.komisjaL1, "Nie wypełnione pole/a \"Nazwisko\"", "NNW" + i.ToString(), "4");
						errors++;
					}
					else
					{
						this.errorProvider1.SetErrorWithCount(this.komisjaL1, "", "NNW" + i.ToString(), "4");
						if (!System.Text.RegularExpressions.Regex.IsMatch(d.ToString(), pattern))
						{
							this.errorProvider1.SetErrorWithCount(this.komisjaL1, "Nieprawidłowo wypełnione pole/a \"Nazwisko\"", "F-NNW" + i.ToString(), "4");
							errors++;
						}
						else
						{
							this.errorProvider1.SetErrorWithCount(this.komisjaL1, "", "F-NNW" + i.ToString(), "4");
						}
					}
				}
				int allPerson = this.personList.Rows.Count - 1;
				int kworum = System.Convert.ToInt32(System.Math.Ceiling(System.Convert.ToDouble(allPerson) / 2.0));
				if (isChairman > 0)
				{
					this.errorProvider1.SetErrorWithCount(this.komisjaL3, "", "SNM11", "4");
					this.errorProvider1.SetErrorWithCount(this.komisjaL3, "", "SNM12", "4");
					if (isChairman == 1)
					{
						this.errorProvider1.SetErrorWithCount(this.komisjaL3, "", "SNM11a", "4");
						if (allPerson < minCountPerson || allPerson > countPerson)
						{
							if (minCountPerson == countPerson)
							{
								this.errorProvider1.SetErrorWithCount(this.komisjaL2, string.Concat(new object[]
								{
									"Liczba osób w komisji powinna mieścić się w przedziale <",
									minCountPerson.ToString(),
									", ",
									countPerson,
									">"
								}), "SNM10", "4");
							}
							else
							{
								this.errorProvider1.SetErrorWithCount(this.komisjaL2, string.Concat(new object[]
								{
									"Liczba osób w komisji powinna mieścić się w przedziale <",
									minCountPerson.ToString(),
									", ",
									countPerson,
									">"
								}), "SNM10", "4");
							}
							errors++;
						}
						else
						{
							if (presentPerson < kworum)
							{
								this.errorProvider1.SetErrorWithCount(this.komisjaL2, "Nie zebrało się kworum.", "SNM10a", "4");
							}
							else
							{
								this.errorProvider1.SetErrorWithCount(this.komisjaL2, "", "SNM10a", "4");
								this.errorProvider1.SetErrorWithCount(this.komisjaL2, "", "SNM10", "4");
								if (errors == 0)
								{
									this.zORp = "P";
									if (isSecondChairman > 0)
									{
										this.zORp = "ZP";
									}
									if (isSecondChairman == 1 || isSecondChairman == 0)
									{
										this.errorProvider1.SetErrorWithCount(this.komisjaL3, "", "SNM12a", "4");
										this.errorProvider1.SetErrorWithCount(this.komisjaL2, "", "SNM10", "4");
										this.errorPanel.Visible = false;
										this.signProtocol_Click(sender, e);
										return;
									}
									this.errorProvider1.SetErrorWithCount(this.komisjaL3, "Jest więcej niż jeden zastępca", "SNM12a", "4");
									errors++;
								}
							}
						}
					}
					else
					{
						this.errorProvider1.SetErrorWithCount(this.komisjaL3, "Jest więcej niż jeden przewodniczący", "SNM11a", "4");
						errors++;
					}
				}
				else
				{
					if (isSecondChairman > 0)
					{
						this.errorProvider1.SetErrorWithCount(this.komisjaL3, "", "SNM11", "4");
						this.errorProvider1.SetErrorWithCount(this.komisjaL3, "", "SNM12", "4");
						if (isSecondChairman == 1)
						{
							this.errorProvider1.SetErrorWithCount(this.komisjaL3, "", "SNM12a", "4");
							if (allPerson < minCountPerson || allPerson > countPerson)
							{
								if (minCountPerson == countPerson)
								{
									this.errorProvider1.SetErrorWithCount(this.komisjaL2, string.Concat(new object[]
									{
										"Liczba osób w komisji powinna mieścić się w przedziale <",
										minCountPerson.ToString(),
										", ",
										countPerson,
										">"
									}), "SNM10", "4");
								}
								else
								{
									this.errorProvider1.SetErrorWithCount(this.komisjaL2, string.Concat(new object[]
									{
										"Liczba osób w komisji powinna mieścić się w przedziale <",
										minCountPerson.ToString(),
										", ",
										countPerson,
										">"
									}), "SNM10", "4");
								}
								errors++;
							}
							else
							{
								if (presentPerson < kworum)
								{
									this.errorProvider1.SetErrorWithCount(this.komisjaL2, "Nie zebrało się kworum.", "SNM10a", "4");
								}
								else
								{
									this.errorProvider1.SetErrorWithCount(this.komisjaL2, "", "SNM10a", "4");
									this.errorProvider1.SetErrorWithCount(this.komisjaL2, "", "SNM10", "4");
									if (errors == 0)
									{
										this.zORp = "Z";
										this.errorProvider1.SetErrorWithCount(this.komisjaL2, countPerson.ToString() ?? "", "SNM10", "4");
										this.errorPanel.Visible = false;
										this.signProtocol_Click(sender, e);
										return;
									}
								}
							}
						}
						else
						{
							this.errorProvider1.SetErrorWithCount(this.komisjaL3, "Jest więcej niż jeden zastępca", "SNM12a", "4");
						}
					}
					else
					{
						this.errorProvider1.SetErrorWithCount(this.komisjaL3, "Brakuje osoby o funkcji \"PRZEWODNICZĄCY\"", "SNM11", "4");
						this.errorProvider1.SetErrorWithCount(this.komisjaL3, "Brakuje osoby o funkcji \"ZASTĘPCA\"", "SNM12", "4");
						errors++;
						if (allPerson < minCountPerson || allPerson > countPerson)
						{
							if (minCountPerson == countPerson)
							{
								this.errorProvider1.SetErrorWithCount(this.komisjaL2, string.Concat(new object[]
								{
									"Liczba osób w komisji powinna mieścić się w przedziale <",
									minCountPerson.ToString(),
									", ",
									countPerson,
									">"
								}), "SNM10", "4");
							}
							else
							{
								this.errorProvider1.SetErrorWithCount(this.komisjaL2, string.Concat(new object[]
								{
									"Liczba osób w komisji powinna mieścić się w przedziale <",
									minCountPerson.ToString(),
									", ",
									countPerson,
									">"
								}), "SNM10", "4");
							}
							errors++;
						}
						else
						{
							if (presentPerson < kworum)
							{
								this.errorProvider1.SetErrorWithCount(this.komisjaL2, "Nie zebrało się kworum.", "SNM10a", "4");
							}
							else
							{
								this.errorProvider1.SetErrorWithCount(this.komisjaL2, "", "SNM10a", "4");
								this.errorProvider1.SetErrorWithCount(this.komisjaL2, "", "SNM10", "4");
							}
						}
					}
				}
			}
			catch (XmlException)
			{
				MessageBox.Show("Nieprawidłowy XML", "Błąd");
			}
			catch (System.NullReferenceException)
			{
				MessageBox.Show("Podanno inny xml niz header", "Błąd");
			}
			this.printErrors(this.currentStep);
		}
		private System.Collections.Generic.Dictionary<string, Errors> committeeValid(System.Collections.Generic.Dictionary<string, Errors> errorsD)
		{
			try
			{
				XmlNode nodesList = this.committee.SelectSingleNode("/komisja_sklad");
				int countPerson = System.Convert.ToInt32(nodesList.Attributes.GetNamedItem("l_wymaganych").Value);
				int minCountPerson = System.Convert.ToInt32(nodesList.Attributes.GetNamedItem("min_l_wymaganych").Value);
				int isChairman = 0;
				int isSecondChairman = 0;
				int presentPerson = 0;
				string pattern = "^[^0-9<>&;:" + '"'.ToString() + "]+$";
				for (int i = 0; i < this.personList.Rows.Count - 1; i++)
				{
					object b = this.personList.Rows[i].Cells["action3"].Value;
					if (b != null && b.ToString().ToLower() == "true")
					{
						presentPerson++;
					}
					object a = this.personList.Rows[i].Cells["Funkcja"].Value;
					if (a != null && a.ToString().ToUpper() == "PRZEWODNICZĄCY" && b != null && b.ToString().ToLower() == "true")
					{
						isChairman++;
					}
					object a2 = this.personList.Rows[i].Cells["Funkcja"].Value;
					if (a2 != null && a2.ToString().ToUpper() == "ZASTĘPCA" && b != null && b.ToString().ToLower() == "true")
					{
						isSecondChairman++;
					}
					object c = this.personList.Rows[i].Cells["Funkcja"].Value;
					if (c == null || (c != null && c.ToString() == ""))
					{
						try
						{
							errorsD["gridKomisja"].addHardError("FNW" + i.ToString());
						}
						catch (System.Collections.Generic.KeyNotFoundException)
						{
							errorsD.Add("gridKomisja", new Errors());
							errorsD["gridKomisja"].addHardError("FNW" + i.ToString());
						}
					}
					object c2 = this.personList.Rows[i].Cells["Imię"].Value;
					if (c2 == null || (c2 != null && c2.ToString() == ""))
					{
						try
						{
							errorsD["gridKomisja"].addHardError("INW" + i.ToString());
						}
						catch (System.Collections.Generic.KeyNotFoundException)
						{
							errorsD.Add("gridKomisja", new Errors());
							errorsD["gridKomisja"].addHardError("INW" + i.ToString());
						}
					}
					else
					{
						if (!System.Text.RegularExpressions.Regex.IsMatch(c2.ToString(), pattern))
						{
							try
							{
								errorsD["gridKomisja"].addHardError("F-INW" + i.ToString());
							}
							catch (System.Collections.Generic.KeyNotFoundException)
							{
								errorsD.Add("gridKomisja", new Errors());
								errorsD["gridKomisja"].addHardError("F-INW" + i.ToString());
							}
						}
					}
					object c3 = this.personList.Rows[i].Cells["Drugie imię"].Value;
					if (c3 != null && c3.ToString() != "")
					{
						if (!System.Text.RegularExpressions.Regex.IsMatch(c3.ToString(), pattern))
						{
							try
							{
								errorsD["gridKomisja"].addHardError("F-I2NW" + i.ToString());
							}
							catch (System.Collections.Generic.KeyNotFoundException)
							{
								errorsD.Add("gridKomisja", new Errors());
								errorsD["gridKomisja"].addHardError("F-I2NW" + i.ToString());
							}
						}
					}
					object d = this.personList.Rows[i].Cells["Nazwisko"].Value;
					if (d == null || (d != null && d.ToString() == ""))
					{
						try
						{
							errorsD["gridKomisja"].addHardError("NNW" + i.ToString());
						}
						catch (System.Collections.Generic.KeyNotFoundException)
						{
							errorsD.Add("gridKomisja", new Errors());
							errorsD["gridKomisja"].addHardError("NNW" + i.ToString());
						}
					}
					else
					{
						if (!System.Text.RegularExpressions.Regex.IsMatch(d.ToString(), pattern))
						{
							try
							{
								errorsD["gridKomisja"].addHardError("F-NNW" + i.ToString());
							}
							catch (System.Collections.Generic.KeyNotFoundException)
							{
								errorsD.Add("gridKomisja", new Errors());
								errorsD["gridKomisja"].addHardError("F-NNW" + i.ToString());
							}
						}
					}
				}
				int allPerson = this.personList.Rows.Count - 1;
				int kworum = System.Convert.ToInt32(System.Math.Ceiling(System.Convert.ToDouble(allPerson) / 2.0));
				if (isChairman > 0)
				{
					if (allPerson < minCountPerson || allPerson > countPerson)
					{
						try
						{
							errorsD["gridKomisja"].addHardError("SNM10");
						}
						catch (System.Collections.Generic.KeyNotFoundException)
						{
							errorsD.Add("gridKomisja", new Errors());
							errorsD["gridKomisja"].addHardError("SNM10");
						}
					}
					if (presentPerson < kworum)
					{
						try
						{
							errorsD["gridKomisja"].addHardError("SNM10a");
						}
						catch (System.Collections.Generic.KeyNotFoundException)
						{
							errorsD.Add("gridKomisja", new Errors());
							errorsD["gridKomisja"].addHardError("SNM10a");
						}
					}
					if (isSecondChairman > 0 && isSecondChairman != 1)
					{
						try
						{
							errorsD["gridKomisja"].addHardError("SNM12a");
						}
						catch (System.Collections.Generic.KeyNotFoundException)
						{
							errorsD.Add("gridKomisja", new Errors());
							errorsD["gridKomisja"].addHardError("SNM12a");
						}
					}
					if (isChairman != 1)
					{
						try
						{
							errorsD["gridKomisja"].addHardError("SNM11a");
						}
						catch (System.Collections.Generic.KeyNotFoundException)
						{
							errorsD.Add("gridKomisja", new Errors());
							errorsD["gridKomisja"].addHardError("SNM11a");
						}
					}
				}
				else
				{
					if (isSecondChairman > 0)
					{
						if (isSecondChairman != 1)
						{
							try
							{
								errorsD["gridKomisja"].addHardError("SNM12a");
							}
							catch (System.Collections.Generic.KeyNotFoundException)
							{
								errorsD.Add("gridKomisja", new Errors());
								errorsD["gridKomisja"].addHardError("SNM12a");
							}
						}
						if (allPerson < minCountPerson || allPerson > countPerson)
						{
							try
							{
								errorsD["gridKomisja"].addHardError("SNM10");
							}
							catch (System.Collections.Generic.KeyNotFoundException)
							{
								errorsD.Add("gridKomisja", new Errors());
								errorsD["gridKomisja"].addHardError("SNM10");
							}
						}
						if (presentPerson < kworum)
						{
							try
							{
								errorsD["gridKomisja"].addHardError("SNM10a");
							}
							catch (System.Collections.Generic.KeyNotFoundException)
							{
								errorsD.Add("gridKomisja", new Errors());
								errorsD["gridKomisja"].addHardError("SNM10a");
							}
						}
					}
					else
					{
						try
						{
							errorsD["gridKomisja"].addHardError("SNM11");
						}
						catch (System.Collections.Generic.KeyNotFoundException)
						{
							errorsD.Add("gridKomisja", new Errors());
							errorsD["gridKomisja"].addHardError("SNM11");
						}
						try
						{
							errorsD["gridKomisja"].addHardError("SNM12");
						}
						catch (System.Collections.Generic.KeyNotFoundException)
						{
							errorsD.Add("gridKomisja", new Errors());
							errorsD["gridKomisja"].addHardError("SNM12");
						}
						if (allPerson < minCountPerson || allPerson > countPerson)
						{
							try
							{
								errorsD["gridKomisja"].addHardError("SNM10");
							}
							catch (System.Collections.Generic.KeyNotFoundException)
							{
								errorsD.Add("gridKomisja", new Errors());
								errorsD["gridKomisja"].addHardError("SNM10");
							}
						}
						if (presentPerson < kworum)
						{
							try
							{
								errorsD["gridKomisja"].addHardError("SNM10a");
							}
							catch (System.Collections.Generic.KeyNotFoundException)
							{
								errorsD.Add("gridKomisja", new Errors());
								errorsD["gridKomisja"].addHardError("SNM10a");
							}
						}
					}
				}
			}
			catch (XmlException)
			{
				MessageBox.Show("Nieprawidłowy XML", "Błąd");
			}
			catch (System.NullReferenceException)
			{
				MessageBox.Show("Podanno inny xml niz header", "Błąd");
			}
			return errorsD;
		}
		private void text_Validated(object sender, System.EventArgs e)
		{
			string pattern = "^[0-9a-ząćęłńóśźżA-ZĄĆĘŁŃÓŚŹŻÄÖÜËäöüë() ,\\.:\\-–_]+$";
			if ((sender as TextBox).Text != "")
			{
				if (!System.Text.RegularExpressions.Regex.IsMatch((sender as TextBox).Text, pattern))
				{
					this.errorProvider1.SetErrorWithCount(sender as TextBox, "Dozwolone tylko litery, cyfry, spacja, przecinek, kropka, dwukropek, myslnik, podłoga i nawiasy okrągłe", "ErrorType", this.stepControl(sender));
					(sender as TextBox).ForeColor = System.Drawing.Color.Red;
					(sender as TextBox).BackColor = System.Drawing.SystemColors.Info;
					this.error = true;
				}
				else
				{
					this.errorProvider1.SetErrorWithCount(sender as TextBox, "", "ErrorType", this.stepControl(sender));
					if (!this.errorProvider1.hasError(sender as TextBox))
					{
						(sender as TextBox).ForeColor = System.Drawing.Color.Black;
						(sender as TextBox).BackColor = System.Drawing.SystemColors.Window;
					}
				}
			}
		}
		private bool text_isValid(string value)
		{
			string pattern = "^[0-9a-ząćęłńóśźżA-ZĄĆĘŁŃÓŚŹŻÄÖÜËäöüë() ,\\.:\\-–_]+$";
			return value != "" && System.Text.RegularExpressions.Regex.IsMatch(value, pattern);
		}
		private void normalText_Validated(object sender, System.EventArgs e)
		{
			string pattern = "^[0-9a-ząćęłńóśźżA-ZĄĆĘŁŃÓŚŹŻÄÖÜËäöüë() ,\\.:\\-–_\r\n]+$";
			if ((sender as TextBox).Text != "")
			{
				if (!System.Text.RegularExpressions.Regex.IsMatch((sender as TextBox).Text, pattern))
				{
					this.errorProvider1.SetErrorWithCount(sender as TextBox, "Dozwolone tylko litery, cyfry, spacja, przecinek, kropka, dwukropek, myslnik, podłoga i nawiasy okrągłe", "ErrorType", this.stepControl(sender));
					(sender as TextBox).ForeColor = System.Drawing.Color.Red;
					(sender as TextBox).BackColor = System.Drawing.SystemColors.Info;
					this.error = true;
				}
				else
				{
					char[] charsToTrim = new char[]
					{
						' ',
						'.',
						'-',
						'–',
						':',
						',',
						'_',
						'\n'
					};
					string text = (sender as TextBox).Text.Trim(charsToTrim);
					if (text == "")
					{
						this.errorProvider1.SetErrorWithCount(sender as TextBox, "Pole nie moze byc puste", "ErrorType", this.stepControl(sender));
						(sender as TextBox).ForeColor = System.Drawing.Color.Red;
						(sender as TextBox).BackColor = System.Drawing.SystemColors.Info;
					}
					else
					{
						this.errorProvider1.SetErrorWithCount(sender as TextBox, "", "ErrorType", this.stepControl(sender));
						if (!this.errorProvider1.hasError(sender as TextBox))
						{
							(sender as TextBox).ForeColor = System.Drawing.Color.Black;
							(sender as TextBox).BackColor = System.Drawing.SystemColors.Window;
						}
					}
				}
			}
			else
			{
				char[] charsToTrim = new char[]
				{
					' ',
					'.',
					'-',
					'–',
					':',
					',',
					'_',
					'\n'
				};
				string text = (sender as TextBox).Text.Trim(charsToTrim);
				if (text == "")
				{
					this.errorProvider1.SetErrorWithCount(sender as TextBox, "Pole nie moze byc puste", "ErrorType", this.stepControl(sender));
					(sender as TextBox).ForeColor = System.Drawing.Color.Red;
					(sender as TextBox).BackColor = System.Drawing.SystemColors.Info;
				}
			}
		}
		private bool normalText_isValid(string value)
		{
			string pattern = "^[0-9a-ząćęłńóśźżA-ZĄĆĘŁŃÓŚŹŻÄÖÜËäöüë() ,\\.:\\-–_\r\n]+$";
			bool result;
			if (value != "")
			{
				if (!System.Text.RegularExpressions.Regex.IsMatch(value, pattern))
				{
					result = false;
				}
				else
				{
					char[] charsToTrim = new char[]
					{
						' ',
						'.',
						'-',
						'–',
						':',
						',',
						'_',
						'\n'
					};
					string text = value.Trim(charsToTrim);
					result = !(text == "");
				}
			}
			else
			{
				result = false;
			}
			return result;
		}
		private void number_Validated(object sender, System.EventArgs e)
		{
			string pattern = "^[0-9]+$";
			if ((sender as TextBox).Text != "")
			{
				if (!System.Text.RegularExpressions.Regex.IsMatch((sender as TextBox).Text, pattern))
				{
					this.errorProvider1.SetErrorWithCount(sender as TextBox, "Dozwolone tylko cyfry (liczby nie ujemne)", "ErrorType", this.stepControl(sender));
					(sender as TextBox).ForeColor = System.Drawing.Color.Red;
					(sender as TextBox).BackColor = System.Drawing.SystemColors.Info;
				}
				else
				{
					this.errorProvider1.SetErrorWithCount(sender as TextBox, "", "ErrorType", this.stepControl(sender));
					if (!this.errorProvider1.hasError(sender as TextBox))
					{
						(sender as TextBox).ForeColor = System.Drawing.Color.Black;
						(sender as TextBox).BackColor = System.Drawing.SystemColors.Window;
					}
					(sender as TextBox).Text = System.Convert.ToInt32((sender as TextBox).Text).ToString();
				}
			}
			else
			{
				this.errorProvider1.SetErrorWithCount(sender as TextBox, "", "ErrorType", this.stepControl(sender));
				if (!this.errorProvider1.hasError(sender as TextBox))
				{
					(sender as TextBox).ForeColor = System.Drawing.Color.Black;
					(sender as TextBox).BackColor = System.Drawing.SystemColors.Window;
				}
				(sender as TextBox).Text = "";
			}
		}
		private void date_Validated(object sender, System.EventArgs e)
		{
			string pattern = "^[0-9]{2}-[0-9]{2}-[0-9]{4}$";
			if ((sender as MaskedTextBox).Text != "")
			{
				if (!System.Text.RegularExpressions.Regex.IsMatch((sender as MaskedTextBox).Text, pattern))
				{
					this.errorProvider1.SetErrorWithCount(sender as MaskedTextBox, "Zły format daty", "ErrorType", this.stepControl(sender));
					(sender as MaskedTextBox).ForeColor = System.Drawing.Color.Red;
					(sender as MaskedTextBox).BackColor = System.Drawing.SystemColors.Info;
					this.error = true;
				}
				else
				{
					try
					{
						string[] date = (sender as MaskedTextBox).Text.Split(new char[]
						{
							'-'
						});
						System.DateTime dt = new System.DateTime(System.Convert.ToInt32(date[2]), System.Convert.ToInt32(date[1]), System.Convert.ToInt32(date[0]));
						this.errorProvider1.SetErrorWithCount(sender as MaskedTextBox, "", "ErrorType", this.stepControl(sender));
						if (!this.errorProvider1.hasError(sender as MaskedTextBox))
						{
							(sender as MaskedTextBox).ForeColor = System.Drawing.Color.Black;
							(sender as MaskedTextBox).BackColor = System.Drawing.SystemColors.Window;
						}
					}
					catch (System.ArgumentOutOfRangeException)
					{
						this.errorProvider1.SetErrorWithCount(sender as MaskedTextBox, "Nieprawidłowa data", "ErrorType", this.stepControl(sender));
						(sender as MaskedTextBox).ForeColor = System.Drawing.Color.Red;
						(sender as MaskedTextBox).BackColor = System.Drawing.SystemColors.Info;
						this.error = true;
					}
				}
			}
			else
			{
				this.errorProvider1.SetErrorWithCount(sender as MaskedTextBox, "Pole wymagane", "ErrorNull", this.stepControl(sender));
				(sender as MaskedTextBox).ForeColor = System.Drawing.Color.Red;
				(sender as MaskedTextBox).BackColor = System.Drawing.SystemColors.Info;
				this.error = true;
			}
		}
		private void time_Validated(object sender, System.EventArgs e)
		{
			string pattern = "^[0-9]{2}:[0-9]{2}$";
			if ((sender as MaskedTextBox).Text != "")
			{
				if (!System.Text.RegularExpressions.Regex.IsMatch((sender as MaskedTextBox).Text, pattern))
				{
					this.errorProvider1.SetErrorWithCount(sender as MaskedTextBox, "Zły format czasu", "ErrorType", this.stepControl(sender));
					(sender as MaskedTextBox).ForeColor = System.Drawing.Color.Red;
					(sender as MaskedTextBox).BackColor = System.Drawing.SystemColors.Info;
					this.error = true;
				}
				else
				{
					try
					{
						string[] time = (sender as MaskedTextBox).Text.Split(new char[]
						{
							':'
						});
						System.DateTime dt = new System.DateTime(2014, 1, 1, System.Convert.ToInt32(time[0]), System.Convert.ToInt32(time[1]), 0);
						this.errorProvider1.SetErrorWithCount(sender as MaskedTextBox, "", "ErrorType", this.stepControl(sender));
						if (!this.errorProvider1.hasError(sender as MaskedTextBox))
						{
							(sender as MaskedTextBox).ForeColor = System.Drawing.Color.Black;
							(sender as MaskedTextBox).BackColor = System.Drawing.SystemColors.Window;
						}
					}
					catch (System.ArgumentOutOfRangeException)
					{
						this.errorProvider1.SetErrorWithCount(sender as MaskedTextBox, "Nieprawidłowy czas", "ErrorType", this.stepControl(sender));
						(sender as MaskedTextBox).ForeColor = System.Drawing.Color.Red;
						(sender as MaskedTextBox).BackColor = System.Drawing.SystemColors.Info;
						this.error = true;
					}
				}
			}
			else
			{
				this.errorProvider1.SetErrorWithCount(sender as MaskedTextBox, "Pole wymagane", "ErrorNull", this.stepControl(sender));
				(sender as MaskedTextBox).ForeColor = System.Drawing.Color.Red;
				(sender as MaskedTextBox).BackColor = System.Drawing.SystemColors.Info;
				this.error = true;
			}
		}
		private void dateTime_Validated(object sender, System.EventArgs e)
		{
			string pattern = "^[0-9]{2}-[0-9]{2}-[0-9]{4} [0-9]{2}:[0-9]{2}$";
			if ((sender as MaskedTextBox).Text != "")
			{
				if (!System.Text.RegularExpressions.Regex.IsMatch((sender as MaskedTextBox).Text, pattern))
				{
					this.errorProvider1.SetErrorWithCount(sender as MaskedTextBox, "Zły format daty i czasu", "ErrorType", this.stepControl(sender));
					(sender as MaskedTextBox).ForeColor = System.Drawing.Color.Red;
					(sender as MaskedTextBox).BackColor = System.Drawing.SystemColors.Info;
					this.error = true;
				}
				else
				{
					try
					{
						string[] date = (sender as MaskedTextBox).Text.Split(new char[]
						{
							'-',
							' ',
							':'
						});
						System.DateTime dt = new System.DateTime(System.Convert.ToInt32(date[2]), System.Convert.ToInt32(date[1]), System.Convert.ToInt32(date[0]), System.Convert.ToInt32(date[3]), System.Convert.ToInt32(date[4]), 0);
						this.errorProvider1.SetErrorWithCount(sender as MaskedTextBox, "", "ErrorType", this.stepControl(sender));
						if (!this.errorProvider1.hasError(sender as MaskedTextBox))
						{
							(sender as MaskedTextBox).ForeColor = System.Drawing.Color.Black;
							(sender as MaskedTextBox).BackColor = System.Drawing.SystemColors.Window;
						}
					}
					catch (System.ArgumentOutOfRangeException)
					{
						this.errorProvider1.SetErrorWithCount(sender as MaskedTextBox, "Nieprawidłowa data lub czas", "ErrorType", this.stepControl(sender));
						(sender as MaskedTextBox).ForeColor = System.Drawing.Color.Red;
						(sender as MaskedTextBox).BackColor = System.Drawing.SystemColors.Info;
						this.error = true;
					}
				}
			}
			else
			{
				this.errorProvider1.SetErrorWithCount(sender as MaskedTextBox, "Pole wymagane", "ErrorNull", this.stepControl(sender));
				(sender as MaskedTextBox).ForeColor = System.Drawing.Color.Red;
				(sender as MaskedTextBox).BackColor = System.Drawing.SystemColors.Info;
				this.error = true;
			}
		}
		private bool date_isValid(string value)
		{
			string pattern = "^[0-9]{2}-[0-9]{2}-[0-9]{4}$";
			bool result;
			if (value != "")
			{
				if (!System.Text.RegularExpressions.Regex.IsMatch(value, pattern))
				{
					result = false;
					return result;
				}
				try
				{
					string[] date = value.Split(new char[]
					{
						'-'
					});
					System.DateTime dt = new System.DateTime(System.Convert.ToInt32(date[2]), System.Convert.ToInt32(date[1]), System.Convert.ToInt32(date[0]));
					result = true;
					return result;
				}
				catch (System.ArgumentOutOfRangeException)
				{
					result = false;
					return result;
				}
			}
			result = false;
			return result;
		}
		private bool time_isValid(string value)
		{
			string pattern = "^[0-9]{2}:[0-9]{2}$";
			bool result;
			if (value != "")
			{
				if (!System.Text.RegularExpressions.Regex.IsMatch(value, pattern))
				{
					result = false;
					return result;
				}
				try
				{
					string[] time = value.Split(new char[]
					{
						':'
					});
					System.DateTime dt = new System.DateTime(2014, 1, 1, System.Convert.ToInt32(time[0]), System.Convert.ToInt32(time[1]), 0);
					result = true;
					return result;
				}
				catch (System.ArgumentOutOfRangeException)
				{
					result = false;
					return result;
				}
			}
			result = false;
			return result;
		}
		private bool dateTime_isValid(string value)
		{
			string pattern = "^[0-9]{2}-[0-9]{2}-[0-9]{4} [0-9]{2}:[0-9]{2}$";
			bool result;
			if (value != "")
			{
				if (!System.Text.RegularExpressions.Regex.IsMatch(value, pattern))
				{
					result = false;
					return result;
				}
				try
				{
					string[] date = value.Split(new char[]
					{
						'-',
						' ',
						':'
					});
					System.DateTime dt = new System.DateTime(System.Convert.ToInt32(date[2]), System.Convert.ToInt32(date[1]), System.Convert.ToInt32(date[0]), System.Convert.ToInt32(date[3]), System.Convert.ToInt32(date[4]), 0);
					result = true;
					return result;
				}
				catch (System.ArgumentOutOfRangeException)
				{
					result = false;
					return result;
				}
			}
			result = false;
			return result;
		}
		private bool number_isValid(string value)
		{
			string pattern = "^[0-9]+$";
			return value != "" && System.Text.RegularExpressions.Regex.IsMatch(value, pattern);
		}
		private void lastValidation(Panel p)
		{
			try
			{
				for (int i = 0; i < this.lastValidators.Count; i++)
				{
					string idName = this.lastValidators[i].getId();
					int count = this.lastValidators[i].getFieldsCount();
					System.Collections.Generic.List<string[]> replace = new System.Collections.Generic.List<string[]>();
					for (int j = 0; j < count; j++)
					{
						string tmp = this.lastValidators[i].getNameField(j);
						if (p.Controls[tmp] != null)
						{
							replace.Add(new string[]
							{
								tmp,
								p.Controls[tmp].Text
							});
						}
						int c = replace.Count;
						if (tmp == "lwyb")
						{
							replace.Add(new string[]
							{
								tmp,
								this.lwyb
							});
						}
						if (tmp == "lwybA")
						{
							replace.Add(new string[]
							{
								tmp,
								this.lwybA
							});
						}
						if (tmp == "lwybB")
						{
							replace.Add(new string[]
							{
								tmp,
								this.lwybB
							});
						}
						if (tmp == "plusminus")
						{
							replace.Add(new string[]
							{
								tmp,
								this.plusminus
							});
						}
						if (tmp == "plus")
						{
							replace.Add(new string[]
							{
								tmp,
								this.plus
							});
						}
						if (tmp == "minus")
						{
							replace.Add(new string[]
							{
								tmp,
								this.minus
							});
						}
						if (tmp == "naklad")
						{
							string val = this.naklad.Replace("%", "");
							if (val.Length == 2)
							{
								try
								{
									double k = (double)(100 / System.Convert.ToInt32(val));
									replace.Add(new string[]
									{
										tmp,
										(System.Convert.ToDouble(this.lwyb) * System.Convert.ToDouble(k)).ToString()
									});
								}
								catch (System.Exception ex)
								{
									MessageBox.Show("Komunikat błedu: ProtocolForm.lastValidation: " + ex.Message, "Uwaga");
								}
							}
							if (val.Length == 1)
							{
								double k = (double)(100 / System.Convert.ToInt32(val));
								replace.Add(new string[]
								{
									tmp,
									(System.Convert.ToDouble(this.lwyb) * System.Convert.ToDouble(k)).ToString()
								});
							}
						}
						if (tmp == "naklad%")
						{
							string val = this.naklad.Replace("%", "");
							if (val.Length > 0 && val.Length <= 3)
							{
								try
								{
									replace.Add(new string[]
									{
										tmp,
										val.ToString()
									});
								}
								catch (System.Exception ex)
								{
									MessageBox.Show("Komunikat błedu: ProtocolForm.lastValidation: " + ex.Message, "Uwaga");
								}
							}
						}
					}
					bool valid = this.lastValidators[i].valid(replace);
					for (int j = 0; j < count; j++)
					{
						if (!this.lastValidators[i].containVariables(j))
						{
							string tmp = this.lastValidators[i].getNameField(j);
							if (p.Controls[tmp] != null)
							{
								int step = 1;
								if (p.Name == "Form2panel")
								{
									if (valid)
									{
										if (this.lastValidators[i].getErrorType() == ErrorType.HardError || this.lastValidators[i].getErrorType() == ErrorType.Null)
										{
											this.errorProvider1.SetErrorWithCount(this.Form1panel.Controls[tmp], "", this.lastValidators[i].getId(), step.ToString());
										}
										if (this.lastValidators[i].getErrorType() == ErrorType.HardWarning)
										{
											this.errorProvider1.SetErrorWithCount(this.Form1panel.Controls[tmp], "", 2, this.lastValidators[i].getId(), step.ToString());
										}
										if (this.lastValidators[i].getErrorType() == ErrorType.Soft)
										{
											this.errorProvider1.SetErrorWithCount(this.Form1panel.Controls[tmp], "", 3, this.lastValidators[i].getId(), step.ToString());
										}
										if (this.Form1panel.Controls[tmp].Enabled)
										{
											if (!this.errorProvider1.hasError(this.Form1panel.Controls[tmp]))
											{
												this.Form1panel.Controls[tmp].ForeColor = System.Drawing.Color.Black;
												this.Form1panel.Controls[tmp].BackColor = System.Drawing.SystemColors.Window;
											}
										}
									}
									else
									{
										if (this.lastValidators[i].getErrorType() == ErrorType.HardError || this.lastValidators[i].getErrorType() == ErrorType.Null)
										{
											this.errorProvider1.SetErrorWithCount(this.Form1panel.Controls[tmp], this.lastValidators[i].getNote(), this.lastValidators[i].getId(), step.ToString());
										}
										if (this.lastValidators[i].getErrorType() == ErrorType.HardWarning)
										{
											this.errorProvider1.SetErrorWithCount(this.Form1panel.Controls[tmp], this.lastValidators[i].getNote(), 2, this.lastValidators[i].getId(), step.ToString());
										}
										if (this.lastValidators[i].getErrorType() == ErrorType.Soft)
										{
											this.errorProvider1.SetErrorWithCount(this.Form1panel.Controls[tmp], this.lastValidators[i].getNote(), 3, this.lastValidators[i].getId(), step.ToString());
										}
										if (this.Form1panel.Controls[tmp].Enabled)
										{
											this.Form1panel.Controls[tmp].ForeColor = System.Drawing.Color.Red;
											this.Form1panel.Controls[tmp].BackColor = System.Drawing.SystemColors.Info;
										}
									}
									step = 2;
								}
								if (valid)
								{
									if (this.lastValidators[i].getErrorType() == ErrorType.HardError || this.lastValidators[i].getErrorType() == ErrorType.Null)
									{
										this.errorProvider1.SetErrorWithCount(p.Controls[tmp], "", this.lastValidators[i].getId(), step.ToString());
									}
									if (this.lastValidators[i].getErrorType() == ErrorType.HardWarning)
									{
										this.errorProvider1.SetErrorWithCount(p.Controls[tmp], "", 2, this.lastValidators[i].getId(), step.ToString());
									}
									if (this.lastValidators[i].getErrorType() == ErrorType.Soft)
									{
										this.errorProvider1.SetErrorWithCount(p.Controls[tmp], "", 3, this.lastValidators[i].getId(), step.ToString());
									}
									if (p.Controls[tmp].Enabled)
									{
										if (!this.errorProvider1.hasError(p.Controls[tmp]))
										{
											p.Controls[tmp].ForeColor = System.Drawing.Color.Black;
											p.Controls[tmp].BackColor = System.Drawing.SystemColors.Window;
										}
									}
								}
								else
								{
									if (this.lastValidators[i].getErrorType() == ErrorType.HardError || this.lastValidators[i].getErrorType() == ErrorType.Null)
									{
										this.errorProvider1.SetErrorWithCount(p.Controls[tmp], this.lastValidators[i].getNote(), this.lastValidators[i].getId(), step.ToString());
									}
									if (this.lastValidators[i].getErrorType() == ErrorType.HardWarning)
									{
										this.errorProvider1.SetErrorWithCount(p.Controls[tmp], this.lastValidators[i].getNote(), 2, this.lastValidators[i].getId(), step.ToString());
									}
									if (this.lastValidators[i].getErrorType() == ErrorType.Soft)
									{
										this.errorProvider1.SetErrorWithCount(p.Controls[tmp], this.lastValidators[i].getNote(), 3, this.lastValidators[i].getId(), step.ToString());
									}
									if (p.Controls[tmp].Enabled)
									{
										p.Controls[tmp].ForeColor = System.Drawing.Color.Red;
										p.Controls[tmp].BackColor = System.Drawing.SystemColors.Info;
									}
								}
							}
						}
						else
						{
							for (int l = 0; l < replace.Count; l++)
							{
								string tmp = replace[l][0];
								if (p.Controls[tmp] != null)
								{
									int step = 1;
									if (p.Name == "Form2panel")
									{
										if (valid)
										{
											if (this.lastValidators[i].getErrorType() == ErrorType.HardError || this.lastValidators[i].getErrorType() == ErrorType.Null)
											{
												this.errorProvider1.SetErrorWithCount(this.Form1panel.Controls[tmp], "", this.lastValidators[i].getId(), step.ToString());
											}
											if (this.lastValidators[i].getErrorType() == ErrorType.HardWarning)
											{
												this.errorProvider1.SetErrorWithCount(this.Form1panel.Controls[tmp], "", 2, this.lastValidators[i].getId(), step.ToString());
											}
											if (this.lastValidators[i].getErrorType() == ErrorType.Soft)
											{
												this.errorProvider1.SetErrorWithCount(this.Form1panel.Controls[tmp], "", 3, this.lastValidators[i].getId(), step.ToString());
											}
											if (this.Form1panel.Controls[tmp].Enabled)
											{
												if (!this.errorProvider1.hasError(this.Form1panel.Controls[tmp]))
												{
													this.Form1panel.Controls[tmp].ForeColor = System.Drawing.Color.Black;
													this.Form1panel.Controls[tmp].BackColor = System.Drawing.SystemColors.Window;
												}
											}
										}
										else
										{
											if (this.lastValidators[i].getErrorType() == ErrorType.HardError || this.lastValidators[i].getErrorType() == ErrorType.Null)
											{
												this.errorProvider1.SetErrorWithCount(this.Form1panel.Controls[tmp], this.lastValidators[i].getNote(), this.lastValidators[i].getId(), step.ToString());
											}
											if (this.lastValidators[i].getErrorType() == ErrorType.HardWarning)
											{
												this.errorProvider1.SetErrorWithCount(this.Form1panel.Controls[tmp], this.lastValidators[i].getNote(), 2, this.lastValidators[i].getId(), step.ToString());
											}
											if (this.lastValidators[i].getErrorType() == ErrorType.Soft)
											{
												this.errorProvider1.SetErrorWithCount(this.Form1panel.Controls[tmp], this.lastValidators[i].getNote(), 3, this.lastValidators[i].getId(), step.ToString());
											}
											if (this.Form1panel.Controls[tmp].Enabled)
											{
												this.Form1panel.Controls[tmp].ForeColor = System.Drawing.Color.Red;
												this.Form1panel.Controls[tmp].BackColor = System.Drawing.SystemColors.Info;
											}
										}
										step = 2;
									}
									if (valid)
									{
										if (this.lastValidators[i].getErrorType() == ErrorType.HardError || this.lastValidators[i].getErrorType() == ErrorType.Null)
										{
											this.errorProvider1.SetErrorWithCount(p.Controls[tmp], "", this.lastValidators[i].getId(), step.ToString());
										}
										if (this.lastValidators[i].getErrorType() == ErrorType.HardWarning)
										{
											this.errorProvider1.SetErrorWithCount(p.Controls[tmp], "", 2, this.lastValidators[i].getId(), step.ToString());
										}
										if (this.lastValidators[i].getErrorType() == ErrorType.Soft)
										{
											this.errorProvider1.SetErrorWithCount(p.Controls[tmp], "", 3, this.lastValidators[i].getId(), step.ToString());
										}
										if (p.Controls[tmp].Enabled)
										{
											if (!this.errorProvider1.hasError(p.Controls[tmp]))
											{
												p.Controls[tmp].ForeColor = System.Drawing.Color.Black;
												p.Controls[tmp].BackColor = System.Drawing.SystemColors.Window;
											}
										}
									}
									else
									{
										if (this.lastValidators[i].getErrorType() == ErrorType.HardError || this.lastValidators[i].getErrorType() == ErrorType.Null)
										{
											this.errorProvider1.SetErrorWithCount(p.Controls[tmp], this.lastValidators[i].getNote(), this.lastValidators[i].getId(), step.ToString());
										}
										if (this.lastValidators[i].getErrorType() == ErrorType.HardWarning)
										{
											this.errorProvider1.SetErrorWithCount(p.Controls[tmp], this.lastValidators[i].getNote(), 2, this.lastValidators[i].getId(), step.ToString());
										}
										if (this.lastValidators[i].getErrorType() == ErrorType.Soft)
										{
											this.errorProvider1.SetErrorWithCount(p.Controls[tmp], this.lastValidators[i].getNote(), 3, this.lastValidators[i].getId(), step.ToString());
										}
										if (p.Controls[tmp].Enabled)
										{
											p.Controls[tmp].ForeColor = System.Drawing.Color.Red;
											p.Controls[tmp].BackColor = System.Drawing.SystemColors.Info;
										}
									}
								}
							}
						}
					}
				}
			}
			catch (XmlException e)
			{
				MessageBox.Show("Nieprawidłowy XML: " + e.Message, "Error");
			}
			catch (System.NullReferenceException)
			{
				MessageBox.Show("Podanno inny xml definicje wygladu", "Error");
			}
		}
		private System.Collections.Generic.Dictionary<string, Errors> lastValidation(Panel p, System.Collections.Generic.Dictionary<string, Errors> errors, bool writeStep)
		{
			string step = "step1_";
			if (p.Name == "Form2panel")
			{
				step = "step2_";
			}
			try
			{
				for (int i = 0; i < this.lastValidators.Count; i++)
				{
					int count = this.lastValidators[i].getFieldsCount();
					System.Collections.Generic.List<string[]> replace = new System.Collections.Generic.List<string[]>();
					for (int j = 0; j < count; j++)
					{
						string tmp = this.lastValidators[i].getNameField(j);
						if (p.Controls[tmp] != null)
						{
							replace.Add(new string[]
							{
								tmp,
								p.Controls[tmp].Text
							});
						}
						if (tmp == "lwyb")
						{
							replace.Add(new string[]
							{
								tmp,
								this.lwyb
							});
						}
						if (tmp == "lwybA")
						{
							replace.Add(new string[]
							{
								tmp,
								this.lwybA
							});
						}
						if (tmp == "lwybB")
						{
							replace.Add(new string[]
							{
								tmp,
								this.lwybB
							});
						}
						if (tmp == "plusminus")
						{
							replace.Add(new string[]
							{
								tmp,
								this.plusminus
							});
						}
						if (tmp == "plus")
						{
							replace.Add(new string[]
							{
								tmp,
								this.plus
							});
						}
						if (tmp == "minus")
						{
							replace.Add(new string[]
							{
								tmp,
								this.minus
							});
						}
						if (tmp == "naklad")
						{
							string val = this.naklad.Replace("%", "");
							if (val.Length == 2)
							{
								try
								{
									double k = (double)(100 / System.Convert.ToInt32(val));
									replace.Add(new string[]
									{
										tmp,
										(System.Convert.ToDouble(this.lwyb) * System.Convert.ToDouble(k)).ToString()
									});
								}
								catch (System.Exception ex)
								{
									MessageBox.Show("Komunikat błedu: ProtocolForm.lastValidation: " + ex.Message, "Uwaga");
								}
							}
							if (val.Length == 1)
							{
								double k = (double)(100 / System.Convert.ToInt32(val));
								replace.Add(new string[]
								{
									tmp,
									(System.Convert.ToDouble(this.lwyb) * System.Convert.ToDouble(k)).ToString()
								});
							}
						}
						if (tmp == "naklad%")
						{
							string val = this.naklad.Replace("%", "");
							if (val.Length > 0 && val.Length <= 3)
							{
								try
								{
									replace.Add(new string[]
									{
										tmp,
										val.ToString()
									});
								}
								catch (System.Exception ex)
								{
									MessageBox.Show("Komunikat błedu: ProtocolForm.lastValidation: " + ex.Message, "Uwaga");
								}
							}
						}
					}
					if (!this.lastValidators[i].valid(replace))
					{
						for (int j = 0; j < count; j++)
						{
							string tmp;
							if (this.lastValidators[i].containVariables(j))
							{
								for (int l = j; l < replace.Count; l++)
								{
									tmp = replace[l][0];
									if (p.Controls[tmp] != null)
									{
										if (this.lastValidators[i].getErrorType() == ErrorType.HardError || this.lastValidators[i].getErrorType() == ErrorType.Null)
										{
											if (writeStep)
											{
												try
												{
													errors[step + tmp].addHardError(this.lastValidators[i].getId());
												}
												catch (System.Collections.Generic.KeyNotFoundException)
												{
													errors.Add(step + tmp, new Errors());
													errors[step + tmp].addHardError(this.lastValidators[i].getId());
												}
											}
											else
											{
												try
												{
													errors[tmp].addValueInHardError(this.lastValidators[i].getId());
												}
												catch (System.Collections.Generic.KeyNotFoundException)
												{
													errors.Add(tmp, new Errors());
													errors[tmp].addHardError(this.lastValidators[i].getId());
												}
											}
										}
										if (this.lastValidators[i].getErrorType() == ErrorType.HardWarning)
										{
											if (writeStep)
											{
												try
												{
													errors[step + tmp].addHardWarning(this.lastValidators[i].getId());
												}
												catch (System.Collections.Generic.KeyNotFoundException)
												{
													errors.Add(step + tmp, new Errors());
													errors[step + tmp].addHardWarning(this.lastValidators[i].getId());
												}
											}
											else
											{
												try
												{
													errors[tmp].addValueInHardWarning(this.lastValidators[i].getId());
												}
												catch (System.Collections.Generic.KeyNotFoundException)
												{
													errors.Add(tmp, new Errors());
													errors[tmp].addHardWarning(this.lastValidators[i].getId());
												}
											}
										}
										if (this.lastValidators[i].getErrorType() == ErrorType.Soft)
										{
											if (writeStep)
											{
												try
												{
													errors[step + tmp].addSoftError(this.lastValidators[i].getId());
												}
												catch (System.Collections.Generic.KeyNotFoundException)
												{
													errors.Add(step + tmp, new Errors());
													errors[step + tmp].addSoftError(this.lastValidators[i].getId());
												}
											}
											else
											{
												try
												{
													errors[tmp].addValueInSoftError(this.lastValidators[i].getId());
												}
												catch (System.Collections.Generic.KeyNotFoundException)
												{
													errors.Add(tmp, new Errors());
													errors[tmp].addSoftError(this.lastValidators[i].getId());
												}
											}
										}
									}
								}
								break;
							}
							tmp = this.lastValidators[i].getNameField(j);
							if (p.Controls[tmp] != null)
							{
								if (this.lastValidators[i].getErrorType() == ErrorType.HardError || this.lastValidators[i].getErrorType() == ErrorType.Null)
								{
									if (writeStep)
									{
										try
										{
											errors[step + tmp].addHardError(this.lastValidators[i].getId());
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add(step + tmp, new Errors());
											errors[step + tmp].addHardError(this.lastValidators[i].getId());
										}
									}
									else
									{
										try
										{
											errors[tmp].addValueInHardError(this.lastValidators[i].getId());
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add(tmp, new Errors());
											errors[tmp].addHardError(this.lastValidators[i].getId());
										}
									}
								}
								if (this.lastValidators[i].getErrorType() == ErrorType.HardWarning)
								{
									if (writeStep)
									{
										try
										{
											errors[step + tmp].addHardWarning(this.lastValidators[i].getId());
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add(step + tmp, new Errors());
											errors[step + tmp].addHardWarning(this.lastValidators[i].getId());
										}
									}
									else
									{
										try
										{
											errors[tmp].addValueInHardWarning(this.lastValidators[i].getId());
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add(tmp, new Errors());
											errors[tmp].addHardWarning(this.lastValidators[i].getId());
										}
									}
								}
								if (this.lastValidators[i].getErrorType() == ErrorType.Soft)
								{
									if (writeStep)
									{
										try
										{
											errors[step + tmp].addSoftError(this.lastValidators[i].getId());
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add(step + tmp, new Errors());
											errors[step + tmp].addSoftError(this.lastValidators[i].getId());
										}
									}
									else
									{
										try
										{
											errors[tmp].addValueInSoftError(this.lastValidators[i].getId());
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add(tmp, new Errors());
											errors[tmp].addSoftError(this.lastValidators[i].getId());
										}
									}
								}
							}
						}
					}
				}
			}
			catch (XmlException e)
			{
				MessageBox.Show("Nieprawidłowy XML: " + e.Message, "Błąd");
			}
			catch (System.NullReferenceException)
			{
				MessageBox.Show("Podanno inny xml definicje wygladu", "Błąd");
			}
			return errors;
		}
		private void rangeValidation(Panel p)
		{
			string step = this.stepControl(p);
			foreach (Control c in p.Controls)
			{
				if (c is TextBox)
				{
					string pattern = "^[0-9]+$";
					try
					{
						if (this.range[c.Name] != null)
						{
							bool r = System.Text.RegularExpressions.Regex.IsMatch(c.Text, pattern);
							if (r)
							{
								ValidationRange value = this.range[c.Name];
								try
								{
									int num = System.Convert.ToInt32(c.Text);
									int a = value.getMin();
									int b = value.getMax();
									if (num < value.getMin() || num > value.getMax())
									{
										if (value.getMin() != 0 || value.getMax() != 0)
										{
											this.errorProvider1.SetErrorWithCount(c as TextBox, string.Concat(new object[]
											{
												"Cyfra nie miesci sie w przedziale (",
												value.getMin(),
												", ",
												value.getMax(),
												")"
											}), "SNT01", step);
											if (c.Enabled)
											{
												(c as TextBox).ForeColor = System.Drawing.Color.Red;
												(c as TextBox).BackColor = System.Drawing.SystemColors.Info;
											}
											this.error = true;
										}
										else
										{
											this.errorProvider1.SetErrorWithCount(c as TextBox, "", "SNT01", step);
											if (c.Enabled)
											{
												if (!this.errorProvider1.hasError(c))
												{
													(c as TextBox).ForeColor = System.Drawing.Color.Black;
													(c as TextBox).BackColor = System.Drawing.SystemColors.Window;
												}
											}
										}
									}
									else
									{
										this.errorProvider1.SetErrorWithCount(c as TextBox, "", "SNT01", step);
										if (c.Enabled)
										{
											if (!this.errorProvider1.hasError(c))
											{
												(c as TextBox).ForeColor = System.Drawing.Color.Black;
												(c as TextBox).BackColor = System.Drawing.SystemColors.Window;
											}
										}
									}
								}
								catch (System.OverflowException)
								{
									this.errorProvider1.SetErrorWithCount(c as TextBox, string.Concat(new object[]
									{
										"Cyfra nie miesci sie w przedziale (",
										value.getMin(),
										", ",
										value.getMax(),
										") lub przekroczyła zakres Int32"
									}), "SNT01", step);
									if (c.Enabled)
									{
										(c as TextBox).ForeColor = System.Drawing.Color.Red;
										(c as TextBox).BackColor = System.Drawing.SystemColors.Info;
									}
									this.error = true;
								}
							}
						}
					}
					catch (System.Collections.Generic.KeyNotFoundException)
					{
					}
				}
			}
		}
		private void checkFieldValidation()
		{
			foreach (Control c in this.Form1panel.Controls)
			{
				if (c is TextBox)
				{
					if (this.Form2panel.Controls[c.Name] != null)
					{
						if (this.Form2panel.Controls[c.Name].Text != c.Text)
						{
							this.errorProvider1.SetErrorWithCount(c as TextBox, "Pole musi być równe odpowiedniemu polu z kroku \"Wypełnij protokół dół-góra\"", "ErrorEqual", "2");
							this.errorProvider1.SetErrorWithCount(this.Form2panel.Controls[c.Name] as TextBox, "Pole musi być równe odpowiedniemu polu z kroku \"Wypełnij protokół góra-dół\"", "ErrorEqual", "2");
							if (c.Enabled)
							{
								c.ForeColor = System.Drawing.Color.Red;
								c.BackColor = System.Drawing.SystemColors.Info;
								this.Form2panel.Controls[c.Name].ForeColor = System.Drawing.Color.Red;
								this.Form2panel.Controls[c.Name].BackColor = System.Drawing.SystemColors.Info;
							}
						}
						else
						{
							this.errorProvider1.SetErrorWithCount(c as TextBox, "", 1, "ErrorEqual", "2");
							this.errorProvider1.SetErrorWithCount(this.Form2panel.Controls[c.Name] as TextBox, "", 1, "ErrorEqual", "2");
							if (c.Enabled)
							{
								if (!this.errorProvider1.hasError(c))
								{
									c.ForeColor = System.Drawing.Color.Black;
									c.BackColor = System.Drawing.SystemColors.Window;
									this.Form2panel.Controls[c.Name].ForeColor = System.Drawing.Color.Black;
									this.Form2panel.Controls[c.Name].BackColor = System.Drawing.SystemColors.Window;
								}
							}
						}
					}
				}
			}
		}
		private void isNotEmpty(Panel p)
		{
			string step = this.stepControl(p);
			foreach (Control c in p.Controls)
			{
				if (c is TextBox && c.Enabled && !this.canBeNull(c.Name))
				{
					char[] charsToTrim = new char[]
					{
						' ',
						'.',
						'-',
						':',
						',',
						'_',
						'\n'
					};
					string text = c.Text.Trim(charsToTrim);
					if (text == "")
					{
						c.ForeColor = System.Drawing.Color.Red;
						c.BackColor = System.Drawing.SystemColors.Info;
						this.errorProvider1.SetErrorWithCount(c as TextBox, "Pole nie może być puste", "ErrorNull", step);
					}
					else
					{
						this.errorProvider1.SetErrorWithCount(c as TextBox, "", "ErrorNull", step);
						if (c.Enabled)
						{
							if (!this.errorProvider1.hasError(c))
							{
								c.ForeColor = System.Drawing.Color.Black;
								c.BackColor = System.Drawing.SystemColors.Window;
							}
						}
					}
				}
				else
				{
					if (c is MaskedTextBox && c.Enabled && !this.canBeNull(c.Name))
					{
						char[] charsToTrim = new char[]
						{
							' ',
							'.',
							'-',
							':',
							',',
							'_',
							'\n'
						};
						string text = c.Text.Trim(charsToTrim);
						if (text == "")
						{
							if (c.Enabled)
							{
								c.ForeColor = System.Drawing.Color.Red;
								c.BackColor = System.Drawing.SystemColors.Info;
							}
							this.errorProvider1.SetErrorWithCount(c as MaskedTextBox, "Pole nie może być puste", "ErrorNull", step);
						}
						else
						{
							this.errorProvider1.SetErrorWithCount(c as MaskedTextBox, "", "ErrorNull", step);
							if (c.Enabled)
							{
								if (!this.errorProvider1.hasError(c))
								{
									c.ForeColor = System.Drawing.Color.Black;
									c.BackColor = System.Drawing.SystemColors.Window;
								}
							}
						}
					}
				}
			}
		}
		private void getLicense_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.RowIndex >= 0)
				{
					if (e.ColumnIndex == this.LicencesTable.Columns["action"].Index)
					{
						try
						{
							object name = this.LicencesTable.Rows[e.RowIndex].Cells["Licencja"].Value;
							string filepath = this.path + "\\Licenses\\" + name.ToString();
							this.wait.setWaitPanel("Trwa autozapis protokołu", "Prosimy czekać");
							this.wait.setVisible(true);
							this.saves(4);
							this.wait.setWaitPanel("Trwa przygotowanie do podpisania protokołu", "Prosimy czekać");
							this.wait.setVisible(true);
							string xmlTMP = this.generateSaves(0);
							Commit com = new Commit(filepath, this, xmlTMP);
							com.ShowDialog();
							if (this.goodcertificate)
							{
								this.wait.setWaitPanel("Trwa generowanie kodu kreskowego dla protokołu", "Prosimy czekać");
								this.wait.setVisible(true);
								string docXml = "";
								this.currentStep = 0;
								string xml = xmlTMP;
								this.save.LoadXml(xml);
								XmlNode header = this.save.SelectSingleNode("/save/header");
								if (header != null)
								{
									docXml += header.OuterXml;
								}
								XmlNode step = this.save.SelectSingleNode("/save/step");
								if (step != null)
								{
									docXml += step.OuterXml;
								}
								XmlNode form = this.save.SelectSingleNode("/save/form");
								if (form != null)
								{
									docXml += form.OuterXml;
								}
								XmlNode komisja_sklad = this.save.SelectSingleNode("/save/komisja_sklad");
								if (komisja_sklad != null)
								{
									docXml += komisja_sklad.OuterXml;
								}
								XmlNode hardWarningCode = this.save.SelectSingleNode("/save/hardWarningCode");
								if (hardWarningCode != null)
								{
									docXml += hardWarningCode.OuterXml;
								}
								XmlNode softError = this.save.SelectSingleNode("/save/softError");
								if (softError != null)
								{
									docXml += softError.OuterXml;
								}
								XmlNode hardError = this.save.SelectSingleNode("/save/hardError");
								if (hardError != null)
								{
									docXml += hardError.OuterXml;
								}
								XmlNode hardWarning = this.save.SelectSingleNode("/save/hardWarning");
								if (hardWarning != null)
								{
									docXml += hardWarning.OuterXml;
								}
								ClassMd5 i = new ClassMd5();
								string controlSum = i.CreateMD5Hash(docXml);
								codeBar code = new codeBar();
								code.generateCode(controlSum);
								this.codeBarCode = code.getCode();
								this.codeBarText = code.getTextReadable();
								this.wait.setWaitPanel("Trwa podpisywanie protokołu", "Prosimy czekać");
								this.wait.setVisible(true);
								xml = this.generateSaves(this.currentStep);
								try
								{
									Certificate cer = new Certificate();
									cer.SignXmlText(xml, this.savePath, this.password, filepath);
								}
								catch (System.Exception ex)
								{
									MessageBox.Show("Sign: " + ex.Message, "Uwaga");
								}
								try
								{
									this.komSend = "";
									Connection con = new Connection();
									if (con.IsAvailableNetworkActive())
									{
										this.wait.setWaitPanel("Trwa wysyłanie protokołu", "Prosimy czekać");
										this.wait.setVisible(true);
										this.goodcertificate = false;
										this.error = false;
										Eksport ex2 = new Eksport(this.savePath, true, this, filepath, this.password);
										try
										{
											ex2.ShowDialog();
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
											catch (System.Exception ex3)
											{
												MessageBox.Show("Protokół został wysłany, ale nie można zmienić jego statusu. " + ex3.Message, "Uwaga");
											}
										}
										if (!this.goodcertificate && this.error)
										{
											this.codeBarCode = "";
											this.codeBarText = "";
											this.saves(4);
										}
									}
									else
									{
										this.komSend = "Protokół nie został wysłany na serwer z powodu braku internetu. Z poziomu listy protokołów bedzie można ponowić operacje.";
									}
									this.password = "";
								}
								catch (System.Exception ex)
								{
									MessageBox.Show("Send: " + ex.Message, "Uwaga");
								}
								try
								{
									this.wait.setWaitPanel("Trwa zapisywanie protokołu na dysk", "Prosimy czekać");
									this.wait.setVisible(true);
									SaveProtocol saveP = new SaveProtocol(this.komSend, this.savePath);
									saveP.ShowDialog();
								}
								catch (System.Exception ex)
								{
									MessageBox.Show("Save: " + ex.Message, "Uwaga");
								}
								try
								{
									this.wait.setWaitPanel("Trwa przygotowanie protokołu do druku", "Prosimy czekać");
									this.wait.setVisible(true);
									this.drukToolStripMenuItem_Click(sender, e);
									this.wait.setVisible(false);
								}
								catch (System.Exception ex)
								{
									MessageBox.Show("Print: " + ex.Message, "Uwaga");
								}
							}
						}
						catch (System.ArgumentOutOfRangeException ex_5C5)
						{
						}
					}
				}
			}
			catch (System.Exception ex)
			{
			}
		}
		private void getCurrentCommitteeKlk_Click(object sender, System.EventArgs e)
		{
			Connection con = new Connection();
			if (con.IsAvailableNetworkActive())
			{
				this.wait.setWaitPanel("Trwa pobieranie aktualnej listy członków komisji", "Proszę czekać");
				this.wait.setVisible(true);
				string[] s = this.savePath.Split(new char[]
				{
					'\\'
				});
				string[] s2 = s[s.Length - 1].Split(new char[]
				{
					'-'
				});
				string akcja = s2[0].Replace('_', '/');
				string jns = s2[1].Replace("Jns", "");
				string obw = s2[2].Replace("Obw", "");
				string server = "klk/";
				string server2 = "KALK/";
				string uri = string.Concat(new string[]
				{
					server,
					akcja,
					"-",
					jns,
					"-",
					obw,
					".xml"
				});
				string path = string.Concat(new string[]
				{
					this.path,
					"\\ProtocolsDef\\",
					s2[0],
					"-",
					jns,
					"-",
					obw,
					".xml"
				});
				uri = string.Concat(new string[]
				{
					server2,
					"integrity/",
					akcja,
					"-",
					jns,
					"-",
					obw
				});
				KLKresponse resHeader = con.getRequestKBWKlk(uri, this.savePath, 0);
				if (!this.currentCommittee)
				{
					if (MessageBox.Show("Czy nadpisać zmodyfikowaną listę członków komisji?", "Aktualizacja członków komisji", MessageBoxButtons.YesNo) == DialogResult.Yes)
					{
						DataTable dtCommittee = new DataTable();
						this.personList.DataSource = dtCommittee;
						this.personList.Columns.Clear();
						if (this.personList.Columns["remove"] != null)
						{
							this.personList.Columns.Remove("remove");
						}
						if (this.personList.Columns["action3"] != null)
						{
							this.personList.Columns.Remove("action3");
						}
						this.personList.Refresh();
						int predStep = this.currentStep - 1;
						this.saves(predStep);
						this.save.Load(this.savePath);
						this.committee.Load(path);
						this.getCommitee();
						this.personList.Refresh();
						this.currentCommittee = true;
					}
				}
				else
				{
					MessageBox.Show("Lista członków komisji jest aktualna", "Aktualizacja członków komisji");
				}
				this.wait.setVisible(false);
			}
			else
			{
				MessageBox.Show("Nie masz połaczenia z internetem! Jeśli masz możliwość włącz Internet i ponownie kliknij przycisk \"Pobierz aktualną definicje Komisji\"", "Uwaga");
			}
		}
		private void committee_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.ColumnIndex == this.personList.Columns["remove"].Index)
				{
					if (e.RowIndex != (sender as DataGridView).Rows.Count - 1)
					{
						DialogResult result = MessageBox.Show("Czy napewno usunąć członka komisji?", "Usuwanie", MessageBoxButtons.YesNo);
						if (result == DialogResult.Yes)
						{
							this.personList.Rows.Remove(this.personList.Rows[e.RowIndex]);
							this.currentCommittee = false;
						}
					}
				}
				else
				{
					this.currentCommittee = false;
				}
			}
			catch (System.Exception)
			{
			}
		}
		private bool IsValid()
		{
			return !this.errorProvider1.hasErrors();
		}
		private void clearErrors()
		{
			this.errorPanel.Visible = false;
			this.warningPanel.Visible = false;
			this.errorWarningPanel.Visible = false;
			while (this.errorPanel.Controls.Count > 0)
			{
				this.errorPanel.Controls.RemoveAt(0);
			}
			while (this.warningPanel.Controls.Count > 0)
			{
				this.warningPanel.Controls.RemoveAt(0);
			}
			while (this.errorWarningPanel.Controls.Count > 0)
			{
				this.errorWarningPanel.Controls.RemoveAt(0);
			}
		}
		private void printErrors(int step)
		{
			this.tooltipErrors.RemoveAll();
			string stepName = "Krok 1 i 2";
			if (step == 1)
			{
				stepName = "Krok 1";
			}
			this.wait.setWaitPanel("Trwa wypisywanie błędów", "Proszę czekać");
			this.wait.setVisible(true);
			this.clearErrors();
			System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, KBWValue>> er = this.errorProvider1.getErrors();
			System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, KBWValue>> er2 = this.errorProvider1.getWarnings();
			System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, KBWValue>> er3 = this.errorProvider1.getHardWarnings();
			int x = 0;
			int y = 20;
			Label lab = new Label();
			lab.Text = "Błędy (Nagłówek)";
			lab.AutoSize = true;
			lab.MaximumSize = new System.Drawing.Size(this.errorPanel.Size.Width - 20, 0);
			lab.Font = new System.Drawing.Font(this.myfont, 9f, System.Drawing.FontStyle.Bold);
			lab.ForeColor = System.Drawing.Color.Red;
			lab.Padding = new Padding(10, 0, 10, 0);
			System.Collections.Generic.List<Label> headerErrors = new System.Collections.Generic.List<Label>();
			headerErrors.Add(lab);
			Label lab2 = new Label();
			lab2.Text = "Błędy (" + stepName + ")";
			lab2.AutoSize = true;
			lab2.MaximumSize = new System.Drawing.Size(this.errorPanel.Size.Width - 20, 0);
			lab2.Font = new System.Drawing.Font(this.myfont, 9f, System.Drawing.FontStyle.Bold);
			lab2.ForeColor = System.Drawing.Color.Red;
			lab2.Padding = new Padding(10, 0, 10, 0);
			System.Collections.Generic.List<Label> step1and2Errors = new System.Collections.Generic.List<Label>();
			step1and2Errors.Add(lab2);
			Label lab3 = new Label();
			lab3.Text = "Błędy (Krok 3)";
			lab3.AutoSize = true;
			lab3.MaximumSize = new System.Drawing.Size(this.errorPanel.Size.Width - 20, 0);
			lab3.Font = new System.Drawing.Font(this.myfont, 9f, System.Drawing.FontStyle.Bold);
			lab3.ForeColor = System.Drawing.Color.Red;
			lab3.Padding = new Padding(10, 0, 10, 0);
			System.Collections.Generic.List<Label> step3Errors = new System.Collections.Generic.List<Label>();
			step3Errors.Add(lab3);
			Label lab4 = new Label();
			lab4.Text = "Błędy (Komisja)";
			lab4.AutoSize = true;
			lab4.MaximumSize = new System.Drawing.Size(this.errorPanel.Size.Width - 20, 0);
			lab4.Font = new System.Drawing.Font(this.myfont, 9f, System.Drawing.FontStyle.Bold);
			lab4.ForeColor = System.Drawing.Color.Red;
			lab4.Padding = new Padding(10, 0, 10, 0);
			System.Collections.Generic.List<Label> step4Errors = new System.Collections.Generic.List<Label>();
			step4Errors.Add(lab4);
			foreach (System.Collections.Generic.KeyValuePair<string, System.Collections.Generic.Dictionary<string, KBWValue>> contolErrors in er)
			{
				string tooltipText = "";
				string tooltipText2 = "";
				string tooltipText3 = "";
				foreach (System.Collections.Generic.KeyValuePair<string, KBWValue> item in contolErrors.Value)
				{
					string text = er[contolErrors.Key][item.Key].getMessage();
					Label i = new Label();
					i.Text = text;
					i.AutoSize = true;
					i.MaximumSize = new System.Drawing.Size(this.errorPanel.Size.Width - 20, 0);
					i.Font = new System.Drawing.Font(this.myfont, 8f);
					i.ForeColor = System.Drawing.Color.Red;
					i.Padding = new Padding(10, 0, 10, 0);
					if (item.Value.getStep() == "-1")
					{
						if (!this.isDoubled(headerErrors, text))
						{
							headerErrors.Add(i);
						}
						if (tooltipText != "")
						{
							tooltipText = tooltipText + '\n'.ToString() + text;
						}
						else
						{
							tooltipText += text;
						}
					}
					if (item.Value.getStep() == "1" || item.Value.getStep() == "2")
					{
						if (!this.isDoubled(step1and2Errors, text))
						{
							step1and2Errors.Add(i);
						}
						if (tooltipText2 != "")
						{
							tooltipText2 = tooltipText2 + '\n'.ToString() + text;
						}
						else
						{
							tooltipText2 += text;
						}
					}
					if (item.Value.getStep() == "3")
					{
						if (!this.isDoubled(step3Errors, text))
						{
							step3Errors.Add(i);
						}
						if (tooltipText3 != "")
						{
							tooltipText3 = tooltipText3 + '\n'.ToString() + text;
						}
						else
						{
							tooltipText3 += text;
						}
					}
					if (item.Value.getStep() == "4")
					{
						if (!this.isDoubled(step4Errors, text))
						{
							step4Errors.Add(i);
						}
					}
				}
				try
				{
					if (tooltipText != "")
					{
						if (this.protocolHeader.Controls[contolErrors.Key].Enabled)
						{
							this.tooltipErrors.SetToolTip(this.protocolHeader.Controls[contolErrors.Key], tooltipText);
						}
					}
				}
				catch (System.Exception)
				{
				}
				try
				{
					if (tooltipText2 != "")
					{
						if (this.Form1panel.Controls[contolErrors.Key].Enabled)
						{
							this.tooltipErrors.SetToolTip(this.Form1panel.Controls[contolErrors.Key], tooltipText2);
						}
						if (step == 2)
						{
							if (this.Form2panel.Controls[contolErrors.Key].Enabled)
							{
								this.tooltipErrors.SetToolTip(this.Form2panel.Controls[contolErrors.Key], tooltipText2);
							}
						}
					}
				}
				catch (System.Exception)
				{
				}
				try
				{
					if (tooltipText3 != "")
					{
						try
						{
							if (this.SummationPanel.Controls[contolErrors.Key].Enabled)
							{
								this.tooltipErrors.SetToolTip(this.SummationPanel.Controls[contolErrors.Key], tooltipText3);
							}
						}
						catch (System.Exception)
						{
							try
							{
								if (this.raportPanel.Controls[contolErrors.Key].Enabled)
								{
									this.tooltipErrors.SetToolTip(this.raportPanel.Controls[contolErrors.Key], tooltipText3);
								}
							}
							catch (System.Exception)
							{
							}
						}
					}
				}
				catch (System.Exception)
				{
				}
			}
			if (headerErrors.Count > 1)
			{
				for (int j = 0; j < headerErrors.Count<Label>(); j++)
				{
					if (j == 0)
					{
						headerErrors[j].Location = new System.Drawing.Point(x, y);
						this.errorPanel.Controls.Add(headerErrors[j]);
						y += headerErrors[j].Height + 20;
					}
					else
					{
						headerErrors[j].Location = new System.Drawing.Point(x, y);
						this.errorPanel.Controls.Add(headerErrors[j]);
						y += headerErrors[j].Height + 5;
					}
				}
				y += 20;
			}
			if (step1and2Errors.Count > 1)
			{
				for (int j = 0; j < step1and2Errors.Count<Label>(); j++)
				{
					if (j == 0)
					{
						step1and2Errors[j].Location = new System.Drawing.Point(x, y);
						this.errorPanel.Controls.Add(step1and2Errors[j]);
						y += step1and2Errors[j].Height + 20;
					}
					else
					{
						step1and2Errors[j].Location = new System.Drawing.Point(x, y);
						this.errorPanel.Controls.Add(step1and2Errors[j]);
						y += step1and2Errors[j].Height + 5;
					}
				}
				y += 20;
			}
			if (step3Errors.Count > 1)
			{
				for (int j = 0; j < step3Errors.Count<Label>(); j++)
				{
					if (j == 0)
					{
						step3Errors[j].Location = new System.Drawing.Point(x, y);
						this.errorPanel.Controls.Add(step3Errors[j]);
						y += step3Errors[j].Height + 20;
					}
					else
					{
						step3Errors[j].Location = new System.Drawing.Point(x, y);
						this.errorPanel.Controls.Add(step3Errors[j]);
						y += step3Errors[j].Height + 5;
					}
				}
				y += 20;
			}
			if (step4Errors.Count > 1)
			{
				for (int j = 0; j < step4Errors.Count<Label>(); j++)
				{
					if (j == 0)
					{
						step4Errors[j].Location = new System.Drawing.Point(x, y);
						this.errorPanel.Controls.Add(step4Errors[j]);
						y += step4Errors[j].Height + 20;
					}
					else
					{
						step4Errors[j].Location = new System.Drawing.Point(x, y);
						this.errorPanel.Controls.Add(step4Errors[j]);
						y += step4Errors[j].Height + 5;
					}
				}
				y += 20;
			}
			this.errorPanel.Visible = true;
			if (this.errorPanel.Controls.Count == 0)
			{
				this.errorPanel.Visible = false;
			}
			x = 0;
			y = 0;
			Label Wlab = new Label();
			Wlab.Text = "Ostrzeżenia (Nagłówek)";
			Wlab.AutoSize = true;
			Wlab.MaximumSize = new System.Drawing.Size(this.errorPanel.Size.Width - 20, 0);
			Wlab.Font = new System.Drawing.Font(this.myfont, 9f, System.Drawing.FontStyle.Bold);
			Wlab.ForeColor = System.Drawing.Color.DodgerBlue;
			Wlab.Padding = new Padding(10, 0, 10, 0);
			Wlab.Location = new System.Drawing.Point(x, y);
			y += Wlab.Height + 20;
			headerErrors = new System.Collections.Generic.List<Label>();
			headerErrors.Add(Wlab);
			Label Wlab2 = new Label();
			Wlab2.Text = "Ostrzeżenia (" + stepName + ")";
			Wlab2.AutoSize = true;
			Wlab2.MaximumSize = new System.Drawing.Size(this.errorPanel.Size.Width - 20, 0);
			Wlab2.Font = new System.Drawing.Font(this.myfont, 9f, System.Drawing.FontStyle.Bold);
			Wlab2.ForeColor = System.Drawing.Color.DodgerBlue;
			Wlab2.Padding = new Padding(10, 0, 10, 0);
			step1and2Errors = new System.Collections.Generic.List<Label>();
			step1and2Errors.Add(Wlab2);
			Label Wlab3 = new Label();
			Wlab3.Text = "Ostrzeżenia (Krok 3)";
			Wlab3.AutoSize = true;
			Wlab3.MaximumSize = new System.Drawing.Size(this.errorPanel.Size.Width - 20, 0);
			Wlab3.Font = new System.Drawing.Font(this.myfont, 9f, System.Drawing.FontStyle.Bold);
			Wlab3.ForeColor = System.Drawing.Color.DodgerBlue;
			Wlab3.Padding = new Padding(10, 0, 10, 0);
			step3Errors = new System.Collections.Generic.List<Label>();
			step3Errors.Add(Wlab3);
			Label Wlab4 = new Label();
			Wlab4.Text = "Ostrzeżenia (Komisja)";
			Wlab4.AutoSize = true;
			Wlab4.MaximumSize = new System.Drawing.Size(this.errorPanel.Size.Width - 20, 0);
			Wlab4.Font = new System.Drawing.Font(this.myfont, 9f, System.Drawing.FontStyle.Bold);
			Wlab4.ForeColor = System.Drawing.Color.DodgerBlue;
			Wlab4.Padding = new Padding(10, 0, 10, 0);
			step4Errors = new System.Collections.Generic.List<Label>();
			step4Errors.Add(Wlab4);
			foreach (System.Collections.Generic.KeyValuePair<string, System.Collections.Generic.Dictionary<string, KBWValue>> contolErrors in er2)
			{
				string tooltipText = "";
				string tooltipText2 = "";
				string tooltipText3 = "";
				foreach (System.Collections.Generic.KeyValuePair<string, KBWValue> item in contolErrors.Value)
				{
					Label i = new Label();
					string text = er2[contolErrors.Key][item.Key].getMessage();
					i.Text = text;
					i.AutoSize = true;
					i.MaximumSize = new System.Drawing.Size(this.errorPanel.Size.Width - 20, 0);
					i.Font = new System.Drawing.Font(this.myfont, 8f);
					i.ForeColor = System.Drawing.Color.DodgerBlue;
					i.Padding = new Padding(10, 0, 10, 0);
					if (item.Value.getStep() == "-1")
					{
						if (!this.isDoubled(headerErrors, text))
						{
							headerErrors.Add(i);
						}
						if (tooltipText != "")
						{
							tooltipText = tooltipText + '\n'.ToString() + text;
						}
						else
						{
							tooltipText += text;
						}
					}
					if (item.Value.getStep() == "1" || item.Value.getStep() == "2")
					{
						if (!this.isDoubled(step1and2Errors, text))
						{
							step1and2Errors.Add(i);
						}
						if (tooltipText2 != "")
						{
							tooltipText2 = tooltipText2 + '\n'.ToString() + text;
						}
						else
						{
							tooltipText2 += text;
						}
					}
					if (item.Value.getStep() == "3")
					{
						if (!this.isDoubled(step3Errors, text))
						{
							step3Errors.Add(i);
						}
						if (tooltipText3 != "")
						{
							tooltipText3 = tooltipText3 + '\n'.ToString() + text;
						}
						else
						{
							tooltipText3 += text;
						}
					}
					if (item.Value.getStep() == "4")
					{
						if (!this.isDoubled(step4Errors, text))
						{
							step4Errors.Add(i);
						}
					}
				}
				if (tooltipText != "")
				{
					if (this.protocolHeader.Controls[contolErrors.Key].Enabled)
					{
						this.tooltipErrors.SetToolTip(this.protocolHeader.Controls[contolErrors.Key], tooltipText);
					}
				}
				if (tooltipText2 != "")
				{
					if (this.Form1panel.Controls[contolErrors.Key].Enabled)
					{
						this.tooltipErrors.SetToolTip(this.Form1panel.Controls[contolErrors.Key], tooltipText2);
					}
					if (step == 2)
					{
						if (this.Form2panel.Controls[contolErrors.Key].Enabled)
						{
							this.tooltipErrors.SetToolTip(this.Form2panel.Controls[contolErrors.Key], tooltipText2);
						}
					}
				}
				try
				{
					if (tooltipText3 != "")
					{
						try
						{
							if (this.SummationPanel.Controls[contolErrors.Key].Enabled)
							{
								this.tooltipErrors.SetToolTip(this.SummationPanel.Controls[contolErrors.Key], tooltipText3);
							}
						}
						catch (System.Exception)
						{
							try
							{
								if (this.raportPanel.Controls[contolErrors.Key].Enabled)
								{
									this.tooltipErrors.SetToolTip(this.raportPanel.Controls[contolErrors.Key], tooltipText3);
								}
							}
							catch (System.Exception)
							{
							}
						}
					}
				}
				catch (System.Exception)
				{
				}
			}
			if (headerErrors.Count > 1)
			{
				for (int j = 0; j < headerErrors.Count<Label>(); j++)
				{
					if (j == 0)
					{
						headerErrors[j].Location = new System.Drawing.Point(x, y);
						this.warningPanel.Controls.Add(headerErrors[j]);
						y += headerErrors[j].Height + 20;
					}
					else
					{
						headerErrors[j].Location = new System.Drawing.Point(x, y);
						this.warningPanel.Controls.Add(headerErrors[j]);
						y += headerErrors[j].Height + 5;
					}
				}
				y += 20;
			}
			if (step1and2Errors.Count > 1)
			{
				for (int j = 0; j < step1and2Errors.Count<Label>(); j++)
				{
					if (j == 0)
					{
						step1and2Errors[j].Location = new System.Drawing.Point(x, y);
						this.warningPanel.Controls.Add(step1and2Errors[j]);
						y += step1and2Errors[j].Height + 20;
					}
					else
					{
						step1and2Errors[j].Location = new System.Drawing.Point(x, y);
						this.warningPanel.Controls.Add(step1and2Errors[j]);
						y += step1and2Errors[j].Height + 5;
					}
				}
				y += 20;
			}
			if (step3Errors.Count > 1)
			{
				for (int j = 0; j < step3Errors.Count<Label>(); j++)
				{
					if (j == 0)
					{
						step3Errors[j].Location = new System.Drawing.Point(x, y);
						this.warningPanel.Controls.Add(step3Errors[j]);
						y += step3Errors[j].Height + 20;
					}
					else
					{
						step3Errors[j].Location = new System.Drawing.Point(x, y);
						this.warningPanel.Controls.Add(step3Errors[j]);
						y += step3Errors[j].Height + 5;
					}
				}
				y += 20;
			}
			if (step4Errors.Count > 1)
			{
				for (int j = 0; j < step4Errors.Count<Label>(); j++)
				{
					if (j == 0)
					{
						step4Errors[j].Location = new System.Drawing.Point(x, y);
						this.warningPanel.Controls.Add(step4Errors[j]);
						y += step4Errors[j].Height + 20;
					}
					else
					{
						step4Errors[j].Location = new System.Drawing.Point(x, y);
						this.warningPanel.Controls.Add(step4Errors[j]);
						y += step4Errors[j].Height + 5;
					}
				}
				y += 20;
			}
			this.warningPanel.Visible = true;
			if (this.warningPanel.Controls.Count == 0)
			{
				this.warningPanel.Visible = false;
			}
			if (this.errorPanel.Visible)
			{
				this.warningPanel.Location = new System.Drawing.Point(this.errorPanel.Location.X, this.errorPanel.Location.Y + this.errorPanel.Size.Height + 10);
			}
			else
			{
				this.warningPanel.Location = new System.Drawing.Point(4, 3);
			}
			x = 0;
			y = 0;
			Label WHlab = new Label();
			WHlab.Text = "Ostrzeżenia blokujące wydruk (Nagłówek)";
			WHlab.AutoSize = true;
			WHlab.MaximumSize = new System.Drawing.Size(this.errorWarningPanel.Size.Width - 20, 0);
			WHlab.Font = new System.Drawing.Font(this.myfont, 9f, System.Drawing.FontStyle.Bold);
			WHlab.ForeColor = System.Drawing.Color.DodgerBlue;
			WHlab.Padding = new Padding(10, 0, 10, 0);
			headerErrors = new System.Collections.Generic.List<Label>();
			headerErrors.Add(WHlab);
			Label WHlab2 = new Label();
			WHlab2.Text = "Ostrzeżenia blokujące wydruk (" + stepName + ")";
			WHlab2.AutoSize = true;
			WHlab2.MaximumSize = new System.Drawing.Size(this.errorWarningPanel.Size.Width - 20, 0);
			WHlab2.Font = new System.Drawing.Font(this.myfont, 9f, System.Drawing.FontStyle.Bold);
			WHlab2.ForeColor = System.Drawing.Color.DodgerBlue;
			WHlab2.Padding = new Padding(10, 0, 10, 0);
			step1and2Errors = new System.Collections.Generic.List<Label>();
			step1and2Errors.Add(WHlab2);
			Label WHlab3 = new Label();
			WHlab3.Text = "Ostrzeżenia blokujące wydruk ( (Krok 3)";
			WHlab3.AutoSize = true;
			WHlab3.MaximumSize = new System.Drawing.Size(this.errorWarningPanel.Size.Width - 20, 0);
			WHlab3.Font = new System.Drawing.Font(this.myfont, 9f, System.Drawing.FontStyle.Bold);
			WHlab3.ForeColor = System.Drawing.Color.DodgerBlue;
			WHlab3.Padding = new Padding(10, 0, 10, 0);
			step3Errors = new System.Collections.Generic.List<Label>();
			step3Errors.Add(WHlab3);
			Label WHlab4 = new Label();
			WHlab4.Text = "Ostrzeżenia blokujące wydruk (Komisja)";
			WHlab4.AutoSize = true;
			WHlab4.MaximumSize = new System.Drawing.Size(this.errorWarningPanel.Size.Width - 20, 0);
			WHlab4.Font = new System.Drawing.Font(this.myfont, 9f, System.Drawing.FontStyle.Bold);
			WHlab4.ForeColor = System.Drawing.Color.DodgerBlue;
			WHlab4.Padding = new Padding(10, 0, 10, 0);
			step4Errors = new System.Collections.Generic.List<Label>();
			step4Errors.Add(WHlab4);
			foreach (System.Collections.Generic.KeyValuePair<string, System.Collections.Generic.Dictionary<string, KBWValue>> contolErrors in er3)
			{
				string tooltipText = "";
				string tooltipText2 = "";
				string tooltipText3 = "";
				foreach (System.Collections.Generic.KeyValuePair<string, KBWValue> item in contolErrors.Value)
				{
					Label i = new Label();
					string text = er3[contolErrors.Key][item.Key].getMessage();
					i.Text = text;
					i.AutoSize = true;
					i.MaximumSize = new System.Drawing.Size(this.errorPanel.Size.Width - 20, 0);
					i.Font = new System.Drawing.Font(this.myfont, 8f);
					i.ForeColor = System.Drawing.Color.DodgerBlue;
					i.Padding = new Padding(10, 0, 10, 0);
					if (item.Value.getStep() == "-1")
					{
						if (!this.isDoubled(headerErrors, text))
						{
							headerErrors.Add(i);
						}
						if (tooltipText != "")
						{
							tooltipText = tooltipText + '\n'.ToString() + text;
						}
						else
						{
							tooltipText += text;
						}
					}
					if (item.Value.getStep() == "1" || item.Value.getStep() == "2")
					{
						if (!this.isDoubled(step1and2Errors, text))
						{
							step1and2Errors.Add(i);
						}
						if (tooltipText2 != "")
						{
							tooltipText2 = tooltipText2 + '\n'.ToString() + text;
						}
						else
						{
							tooltipText2 += text;
						}
					}
					if (item.Value.getStep() == "3")
					{
						if (!this.isDoubled(step3Errors, text))
						{
							step3Errors.Add(i);
						}
						if (tooltipText3 != "")
						{
							tooltipText3 = tooltipText3 + '\n'.ToString() + text;
						}
						else
						{
							tooltipText3 += text;
						}
					}
					if (item.Value.getStep() == "4")
					{
						if (!this.isDoubled(step4Errors, text))
						{
							step4Errors.Add(i);
						}
					}
				}
				if (tooltipText != "")
				{
					if (this.protocolHeader.Controls[contolErrors.Key].Enabled)
					{
						this.tooltipErrors.SetToolTip(this.protocolHeader.Controls[contolErrors.Key], tooltipText);
					}
				}
				if (tooltipText2 != "")
				{
					if (this.Form1panel.Controls[contolErrors.Key].Enabled)
					{
						this.tooltipErrors.SetToolTip(this.Form1panel.Controls[contolErrors.Key], tooltipText2);
					}
					if (step == 2)
					{
						if (this.Form2panel.Controls[contolErrors.Key].Enabled)
						{
							this.tooltipErrors.SetToolTip(this.Form2panel.Controls[contolErrors.Key], tooltipText2);
						}
					}
				}
				if (tooltipText3 != "")
				{
					if (this.SummationPanel.Controls[contolErrors.Key].Enabled)
					{
						this.tooltipErrors.SetToolTip(this.SummationPanel.Controls[contolErrors.Key], tooltipText3);
					}
				}
			}
			if (headerErrors.Count > 1)
			{
				for (int j = 0; j < headerErrors.Count<Label>(); j++)
				{
					if (j == 0)
					{
						headerErrors[j].Location = new System.Drawing.Point(x, y);
						this.errorWarningPanel.Controls.Add(headerErrors[j]);
						y += headerErrors[j].Height + 20;
					}
					else
					{
						headerErrors[j].Location = new System.Drawing.Point(x, y);
						this.errorWarningPanel.Controls.Add(headerErrors[j]);
						y += headerErrors[j].Height + 5;
					}
				}
				y += 20;
			}
			if (step1and2Errors.Count > 1)
			{
				for (int j = 0; j < step1and2Errors.Count<Label>(); j++)
				{
					if (j == 0)
					{
						step1and2Errors[j].Location = new System.Drawing.Point(x, y);
						this.errorWarningPanel.Controls.Add(step1and2Errors[j]);
						y += step1and2Errors[j].Height + 20;
					}
					else
					{
						step1and2Errors[j].Location = new System.Drawing.Point(x, y);
						this.errorWarningPanel.Controls.Add(step1and2Errors[j]);
						y += step1and2Errors[j].Height + 5;
					}
				}
				y += 20;
			}
			if (step3Errors.Count > 1)
			{
				for (int j = 0; j < step3Errors.Count<Label>(); j++)
				{
					if (j == 0)
					{
						step3Errors[j].Location = new System.Drawing.Point(x, y);
						this.errorWarningPanel.Controls.Add(step3Errors[j]);
						y += step3Errors[j].Height + 20;
					}
					else
					{
						step3Errors[j].Location = new System.Drawing.Point(x, y);
						this.errorWarningPanel.Controls.Add(step3Errors[j]);
						y += step3Errors[j].Height + 5;
					}
				}
				y += 20;
			}
			if (step4Errors.Count > 1)
			{
				for (int j = 0; j < step4Errors.Count<Label>(); j++)
				{
					if (j == 0)
					{
						step4Errors[j].Location = new System.Drawing.Point(x, y);
						this.errorWarningPanel.Controls.Add(step4Errors[j]);
						y += step4Errors[j].Height + 20;
					}
					else
					{
						step4Errors[j].Location = new System.Drawing.Point(x, y);
						this.errorWarningPanel.Controls.Add(step4Errors[j]);
						y += step4Errors[j].Height + 5;
					}
				}
				y += 20;
			}
			this.errorWarningPanel.Visible = true;
			if (this.errorWarningPanel.Controls.Count == 0)
			{
				this.errorWarningPanel.Visible = false;
			}
			if (this.warningPanel.Visible)
			{
				this.errorWarningPanel.Location = new System.Drawing.Point(this.warningPanel.Location.X, this.warningPanel.Location.Y + this.warningPanel.Size.Height + 10);
			}
			else
			{
				if (this.errorPanel.Visible)
				{
					this.errorWarningPanel.Location = new System.Drawing.Point(this.errorPanel.Location.X, this.errorPanel.Location.Y + this.errorPanel.Size.Height + 10);
				}
				else
				{
					this.errorWarningPanel.Location = new System.Drawing.Point(4, 3);
				}
			}
			this.wait.setVisible(false);
		}
		private void plikiToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			if (this.currentStep != 0)
			{
				XmlNode status = this.save.SelectSingleNode("/save/form/status");
				if (status != null && !(status.InnerText == "podpisany") && !(status.InnerText == "wysłany"))
				{
					if (this.saves(this.currentStep))
					{
						MessageBox.Show("Protokół został zapisany", "Autozapis");
					}
				}
				else
				{
					if (this.saves(this.currentStep))
					{
						MessageBox.Show("Protokół został zapisany", "Autozapis");
					}
				}
			}
			else
			{
				MessageBox.Show("Protokół nie można nadpisać, ponieważ został już podpisany", "Autozapis");
			}
		}
		private void importujToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			if (MessageBox.Show("Import protokołu spowoduje nadpisanie obecnego protokołu. Czy kontynuować?", "Import", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
					if (this.savePath != filePath)
					{
						this.wait.setWaitPanel("Trwa importowanie protokołu", "Proszę czekać");
						this.wait.setVisible(true);
						System.IO.StreamReader sr = new System.IO.StreamReader(filePath);
						string file = sr.ReadToEnd();
						sr.Close();
						try
						{
							string importedJns = "";
							string importedElection = "";
							string importedCircuit = "";
							string importedOkr = "";
							string importedInst = "";
							string importedInstJNS = "";
							XmlDocument importedProtocol = new XmlDocument();
							importedProtocol.LoadXml(file);
							XmlNode xmlImportedInst = importedProtocol.SelectSingleNode("/save/header/instJNS");
							if (xmlImportedInst != null && xmlImportedInst.InnerText != "")
							{
								importedInstJNS = xmlImportedInst.InnerText;
							}
							XmlNode xmlJns = importedProtocol.SelectSingleNode("/save/header/jns_kod");
							if (xmlJns != null && xmlJns.InnerText != "")
							{
								importedJns = xmlJns.InnerText;
							}
							XmlNode xmlElection = importedProtocol.SelectSingleNode("/save/header/defklk");
							if (xmlElection != null && xmlElection.FirstChild != null)
							{
								XmlNode xmlElection2 = xmlElection.FirstChild.Attributes.GetNamedItem("name");
								if (xmlElection2 != null && xmlElection2.Value != "")
								{
									string[] saveElectionPart = xmlElection2.Value.Split(new char[]
									{
										'-'
									});
									importedElection = saveElectionPart[0].Replace("/", "_");
									string[] okrPart = saveElectionPart[1].Split(new char[]
									{
										' '
									});
									importedInst = okrPart[0];
								}
							}
							XmlNode xmlCircuit = importedProtocol.SelectSingleNode("/save/header/nrObwodu");
							if (xmlCircuit != null && xmlCircuit.InnerText != "")
							{
								importedCircuit = xmlCircuit.InnerText;
							}
							XmlNode xmlOkr = importedProtocol.SelectSingleNode("/save/header/nrOkregu");
							if (xmlOkr != null && xmlOkr.InnerText != "")
							{
								importedOkr = xmlOkr.InnerText;
							}
							bool go = false;
							string[] ouPart = this.OU.Split(new char[]
							{
								'-'
							});
							if (ouPart[0].Replace("/", "_") == importedElection && System.Convert.ToInt32(ouPart[1]) == System.Convert.ToInt32(importedJns))
							{
								if (ouPart[2] == "O" || ouPart[2] == "A")
								{
									go = true;
								}
								else
								{
									if (ouPart[2] == "P" || ouPart[2] == "Z")
									{
										go = (ouPart[3] == importedCircuit);
									}
								}
							}
							if (go)
							{
								XmlDocument s = new XmlDocument();
								s.Load(filePath);
								string saveCircuit = "";
								xmlCircuit = s.SelectSingleNode("/save/header/nrObwodu");
								if (xmlCircuit != null && xmlCircuit.InnerText != "")
								{
									saveCircuit = xmlCircuit.InnerText;
								}
								string saveInst = "";
								xmlElection = s.SelectSingleNode("/save/header/defklk");
								if (xmlElection != null && xmlElection.FirstChild != null)
								{
									XmlNode xmlElection2 = xmlElection.FirstChild.Attributes.GetNamedItem("name");
									if (xmlElection2 != null && xmlElection2.Value != "")
									{
										string[] okrPart = xmlElection2.Value.Split(new char[]
										{
											'-'
										});
										saveInst = okrPart[1];
									}
								}
								if (importedCircuit != saveCircuit || importedInst != saveInst || System.Convert.ToInt32(importedInstJNS) != System.Convert.ToInt32(this.instJNS))
								{
									string x = string.Concat(new object[]
									{
										this.path,
										"\\saves\\",
										importedElection,
										"-",
										importedJns,
										"-",
										importedCircuit,
										"-",
										importedInst,
										"-",
										importedInstJNS,
										'-',
										importedOkr
									});
									int num = System.IO.Directory.GetFiles(this.path + "\\saves", string.Concat(new object[]
									{
										importedElection,
										"-",
										importedJns,
										"-",
										importedCircuit,
										"-",
										importedInst,
										"-",
										importedInstJNS,
										'-',
										importedOkr,
										"*.crt"
									})).Length;
									if (num > 0)
									{
										x = x + " " + (num + 1).ToString();
									}
									try
									{
										System.IO.StreamWriter sw = new System.IO.StreamWriter(x, false);
										sw.Write(file);
										sw.Close();
										if (MessageBox.Show(string.Concat(new object[]
										{
											"Zaimportowany plik dotyczy innego protokołu (",
											importedElection,
											"-",
											importedJns,
											"-",
											importedCircuit,
											"-",
											importedInst,
											"-",
											importedInstJNS,
											'-',
											importedOkr,
											"). Chcesz przejsc do listy protokołów, aby otworzyć zaimportowany plik?"
										}), "Uwaga", MessageBoxButtons.YesNo) == DialogResult.Yes)
										{
											base.Close();
										}
									}
									catch (System.Exception ex)
									{
										MessageBox.Show("Nie można zaimportować protokołu. " + ex.Message, "Uwaga");
									}
								}
								else
								{
									string x = string.Concat(new object[]
									{
										this.path,
										"\\saves\\",
										importedElection,
										"-",
										importedJns,
										"-",
										importedCircuit,
										"-",
										importedInst,
										"-",
										importedInstJNS,
										'-',
										importedOkr
									});
									string x2 = this.savePath;
									System.IO.StreamWriter sw = new System.IO.StreamWriter(x2, false);
									sw.Write(file);
									sw.Close();
									this.savePath = x2;
									try
									{
										this.save.Load(this.savePath);
									}
									catch (XmlException)
									{
										MessageBox.Show("Nieprawidłowy XML", "Error");
									}
									int stepSave = this.isSave();
									if (stepSave != -1)
									{
										this.errorProvider1.clearErrors();
										this.protocolHeader.Controls.Clear();
										this.raportPanel.Controls.Clear();
										this.raportPanel.Visible = false;
										this.getHeader();
										this.Form1panel.Controls.Clear();
										this.range.Clear();
										this.getCalculator();
										this.wait.setWaitPanel("Trwa zerowanie nastepnych krokow protokołu", "Proszę czekać");
										this.wait.setVisible(true);
										this.protocolForm1.Enabled = true;
										this.protocolForm2.Enabled = false;
										this.protocolSummation.Enabled = false;
										this.protocolCommittee.Enabled = false;
										this.signProtocol.Enabled = false;
										this.Form1panel.Visible = true;
										this.Form2panel.Visible = false;
										this.committeePanel.Visible = false;
										this.SummationPanel.Visible = false;
										this.signPanel.Visible = false;
										this.protocolForm1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
										this.protocolForm2.BackColor = System.Drawing.SystemColors.Control;
										this.protocolSummation.BackColor = System.Drawing.SystemColors.Control;
										this.protocolCommittee.BackColor = System.Drawing.SystemColors.Control;
										this.signProtocol.BackColor = System.Drawing.SystemColors.Control;
										this.buttonNext.Visible = false;
										if (stepSave != 1)
										{
											this.Form2panel.Controls.Clear();
											if (stepSave != 2)
											{
												this.SummationPanel.Controls.Clear();
												if (stepSave != 3)
												{
													DataTable dtCommittee = new DataTable();
													this.personList.DataSource = dtCommittee;
													this.personList.Columns.Clear();
													if (this.personList.Columns["remove"] != null)
													{
														this.personList.Columns.Remove("remove");
													}
													if (this.personList.Columns["action3"] != null)
													{
														this.personList.Columns.Remove("action3");
													}
													this.personList.Refresh();
												}
											}
										}
										this.errorProvider1.clearErrors();
										this.printErrors(1);
									}
									this.buttonNext.Text = "Dalej";
									this.buttonNext.Click -= new System.EventHandler(this.protocolForm2_Click);
									this.buttonNext.Click -= new System.EventHandler(this.protocolSummation_Click);
									this.buttonNext.Click -= new System.EventHandler(this.committee_Click);
									this.buttonNext.Click -= new System.EventHandler(this.committeeValid_Click);
									this.buttonNext.Click -= new System.EventHandler(this.signProtocol_Click);
									this.buttonNext.Click += new System.EventHandler(this.protocolForm2_Click);
									this.bottomPanel.Location = new System.Drawing.Point(this.Form1panel.Location.X, this.Form1panel.Location.Y + this.Form1panel.Size.Height);
									this.bottomPanel.Visible = true;
									this.buttonNext.Visible = true;
								}
							}
							else
							{
								MessageBox.Show("Nie można zaimportować protokołu, ponieważ licencja nie ma do niego uprawnień");
							}
						}
						catch (System.Exception)
						{
							MessageBox.Show("Importowany plik jest nieprawidłowy.");
						}
					}
					else
					{
						MessageBox.Show("Protokól nie został zaimportowany. Probowano zaimportowac ten sam protokół");
					}
				}
				else
				{
					MessageBox.Show("Importowany plik jest pusty. Protokól nie został zaimportowany");
				}
				this.wait.setVisible(false);
			}
		}
		private void eksportToolStripMenuItem1_Click(object sender, System.EventArgs e)
		{
			this.saves(this.currentStep);
			System.IO.StreamReader sr = new System.IO.StreamReader(this.savePath);
			string file = sr.ReadToEnd();
			sr.Close();
			string[] nameFile = this.savePath.Split(new char[]
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
		private void drukToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			this.wait.setWaitPanel("Trwa przygotowanie do druku protokołu", "Proszę czekać");
			this.wait.setVisible(true);
			if (this.currentStep != 0)
			{
				XmlNode status = this.save.SelectSingleNode("/save/status");
				if (status != null && !(status.InnerText == "podpisany") && !(status.InnerText == "wysłany"))
				{
					this.saves(this.currentStep);
				}
				else
				{
					System.Threading.Thread.Sleep(2000);
				}
			}
			else
			{
				System.Threading.Thread.Sleep(2000);
			}
			this.save.Load(this.savePath);
			string controlSum = "";
			if (this.currentStep == 0)
			{
				string docXml = "";
				XmlNode header = this.save.SelectSingleNode("/save/header");
				if (header != null)
				{
					docXml += header.OuterXml;
				}
				XmlNode step = this.save.SelectSingleNode("/save/step");
				if (step != null)
				{
					docXml += step.OuterXml;
				}
				XmlNode form = this.save.SelectSingleNode("/save/form");
				if (form != null)
				{
					docXml += form.OuterXml;
				}
				XmlNode komisja_sklad = this.save.SelectSingleNode("/save/komisja_sklad");
				if (komisja_sklad != null)
				{
					docXml += komisja_sklad.OuterXml;
				}
				XmlNode hardWarningCode = this.save.SelectSingleNode("/save/hardWarningCode");
				if (hardWarningCode != null)
				{
					docXml += hardWarningCode.OuterXml;
				}
				XmlNode softError = this.save.SelectSingleNode("/save/softError");
				if (softError != null)
				{
					docXml += softError.OuterXml;
				}
				XmlNode hardError = this.save.SelectSingleNode("/save/hardError");
				if (hardError != null)
				{
					docXml += hardError.OuterXml;
				}
				XmlNode hardWarning = this.save.SelectSingleNode("/save/hardWarning");
				if (hardWarning != null)
				{
					docXml += hardWarning.OuterXml;
				}
				ClassMd5 i = new ClassMd5();
				controlSum = i.CreateMD5Hash(docXml);
			}
			try
			{
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
				string instJNS = dataPath[4];
				string obwod = dataPath[2].Replace("Obw", "");
				string okreg = dataPath[5].Replace("Okr", "");
				okreg = okreg.Replace(".xml", "");
				protocol.ProtocolPrint(this.header, this.save, this.candidates, this.docxDefinition, controlSum, false, obwod, inst, okreg, this.candDefinition, instJNS);
			}
			catch (System.Exception ex)
			{
				MessageBox.Show("Komunikat błedu: funkcja drukToolStripMenuItem_Click (wywołanie p.ProtocolPrint(...)): " + ex.Message, "Uwaga");
			}
			this.wait.setVisible(false);
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
					MessageBox.Show("Usuniecie standarodowej stopki i nagłówka nie powiodło się. " + ex.Message, "Uwaga");
				}
				wb.ShowPrintDialog();
			}
		}
		private void importujZSieciToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			if (MessageBox.Show("Import protokołu spowoduje nadpisanie obecnego protokołu. Czy kontynuować?", "Import", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				Import imp = new Import(this.savePath, this.licensePath, this, this.header);
				imp.ShowDialog();
				this.wait.setWaitPanel("Trwa wczytywanie protokołu", "Proszę czekać");
				this.wait.setVisible(true);
				if (this.imported)
				{
					int stepSave = this.isSave();
					if (stepSave != -1)
					{
						this.save.Load(this.savePath);
						this.protocolHeader.Controls.Clear();
						this.getHeader();
						this.Form1panel.Controls.Clear();
						this.range.Clear();
						this.getCalculator();
						this.protocolForm1.Enabled = true;
						this.protocolForm2.Enabled = false;
						this.protocolSummation.Enabled = false;
						this.protocolCommittee.Enabled = false;
						this.signProtocol.Enabled = false;
						this.Form1panel.Visible = true;
						this.Form2panel.Visible = false;
						this.committeePanel.Visible = false;
						this.SummationPanel.Visible = false;
						this.signPanel.Visible = false;
						this.protocolForm1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
						this.protocolForm2.BackColor = System.Drawing.SystemColors.Control;
						this.protocolSummation.BackColor = System.Drawing.SystemColors.Control;
						this.protocolCommittee.BackColor = System.Drawing.SystemColors.Control;
						this.signProtocol.BackColor = System.Drawing.SystemColors.Control;
						this.buttonNext.Visible = false;
						if (stepSave != 1)
						{
							this.Form2panel.Controls.Clear();
							if (stepSave != 2)
							{
								this.SummationPanel.Controls.Clear();
								this.raportPanel.Controls.Clear();
								this.raportPanel.Visible = false;
								if (stepSave != 3)
								{
									DataTable dtCommittee = new DataTable();
									this.personList.DataSource = dtCommittee;
									this.personList.Columns.Clear();
									if (this.personList.Columns["remove"] != null)
									{
										this.personList.Columns.Remove("remove");
									}
									if (this.personList.Columns["action3"] != null)
									{
										this.personList.Columns.Remove("action3");
									}
									this.personList.Refresh();
								}
							}
						}
					}
					this.buttonNext.Text = "Dalej";
					this.buttonNext.Click -= new System.EventHandler(this.protocolForm2_Click);
					this.buttonNext.Click -= new System.EventHandler(this.protocolSummation_Click);
					this.buttonNext.Click -= new System.EventHandler(this.committee_Click);
					this.buttonNext.Click -= new System.EventHandler(this.committeeValid_Click);
					this.buttonNext.Click -= new System.EventHandler(this.signProtocol_Click);
					this.buttonNext.Click += new System.EventHandler(this.protocolForm2_Click);
					this.bottomPanel.Location = new System.Drawing.Point(this.Form1panel.Location.X, this.Form1panel.Location.Y + this.Form1panel.Size.Height);
					this.bottomPanel.Visible = true;
					this.buttonNext.Visible = true;
					this.errorProvider1.clearErrors();
				}
				this.imported = false;
				this.wait.setVisible(false);
			}
		}
		private void eksportujZSieciToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			if (this.currentStep != 0)
			{
				this.validateExportedXml(this.currentStep);
				Eksport imp = new Eksport(this.savePath, this.licensePath);
				imp.ShowDialog();
			}
			else
			{
				MessageBox.Show("Nie można wyeksportować podpisanego protokołu. Aby wysłać podpisany protokół proszę wybrać odpowiednią licencje.");
			}
		}
		private System.Collections.Generic.List<Field> readPatternCandidate(XmlNode itemChild, System.Collections.Generic.List<Field> patternField)
		{
			foreach (XmlNode nodeItem in itemChild)
			{
				if (nodeItem.Name == "fildpatern")
				{
					XmlNode name = nodeItem.Attributes.GetNamedItem("name");
					XmlNode name2 = nodeItem.Attributes.GetNamedItem("name2");
					XmlNode name3 = nodeItem.Attributes.GetNamedItem("name2v2");
					XmlNode name4 = nodeItem.Attributes.GetNamedItem("name3");
					XmlNode lista = nodeItem.Attributes.GetNamedItem("lista");
					XmlNode p = nodeItem.Attributes.GetNamedItem("plec");
					XmlNode state = nodeItem.Attributes.GetNamedItem("status");
					XmlNode imie = nodeItem.Attributes.GetNamedItem("imie");
					XmlNode imie2 = nodeItem.Attributes.GetNamedItem("imie2");
					XmlNode idCandidate = nodeItem.Attributes.GetNamedItem("idCandidate");
					XmlNode nazwisko = nodeItem.Attributes.GetNamedItem("nazwisko");
					XmlNode dataType = nodeItem.Attributes.GetNamedItem("data");
					XmlNode komitet = nodeItem.Attributes.GetNamedItem("komitet-wyborczy");
					XmlNode save_as = nodeItem.Attributes.GetNamedItem("save_as");
					XmlNode ischar = nodeItem.Attributes.GetNamedItem("char");
					XmlNode fill = nodeItem.Attributes.GetNamedItem("fill");
					XmlNode display = nodeItem.Attributes.GetNamedItem("display");
					string n = "";
					if (name != null)
					{
						n = name.Value;
					}
					string n2 = "";
					if (name2 != null)
					{
						n2 = name2.Value;
					}
					string n3 = "";
					if (name3 != null)
					{
						n3 = name3.Value;
					}
					string n4 = "";
					if (name4 != null)
					{
						n4 = name4.Value;
					}
					string i = "";
					if (lista != null)
					{
						i = lista.Value;
					}
					string s = "";
					if (state != null)
					{
						s = state.Value;
					}
					string p2 = "";
					if (p != null)
					{
						p2 = p.Value;
					}
					string i2 = "";
					if (imie != null)
					{
						i2 = imie.Value;
					}
					string i3 = "";
					if (imie2 != null)
					{
						i3 = imie2.Value;
					}
					string naz = "";
					if (nazwisko != null)
					{
						naz = nazwisko.Value;
					}
					string dt = "";
					if (dataType != null)
					{
						dt = dataType.Value;
					}
					string sa = "";
					if (save_as != null)
					{
						sa = save_as.Value;
					}
					string j = "";
					if (komitet != null)
					{
						j = komitet.Value;
					}
					string id = "";
					if (idCandidate != null)
					{
						id = idCandidate.Value;
					}
					string dis = "";
					if (display != null)
					{
						dis = display.Value;
					}
					string @char = "";
					if (ischar != null)
					{
						@char = ischar.Value;
					}
					string fill2 = "";
					if (fill != null)
					{
						fill2 = fill.Value;
					}
					patternField.Add(new Field(n, n2, n3, p2, s, i2, i3, naz, dt, sa, j, id, @char, fill2, n4, i, dis));
				}
			}
			return patternField;
		}
		private void setFirstFocus()
		{
			foreach (Control c in this.protocolHeader.Controls)
			{
				if ((c is TextBox || c is MaskedTextBox) && c.Enabled)
				{
					this.lastControl = c;
					c.Select();
					c.Focus();
					base.ActiveControl = c;
					break;
				}
			}
		}
		private void setFirstFocus(Panel p)
		{
			foreach (Control c in p.Controls)
			{
				if ((c is TextBox || c is MaskedTextBox) && c.Enabled)
				{
					this.lastControl = c;
					base.ActiveControl = c;
					c.Select();
					c.Focus();
					break;
				}
			}
		}
		private void setLastFocus()
		{
			int count = this.Form2panel.Controls.Count - 1;
			try
			{
				for (int i = count; i >= 0; i--)
				{
					if (this.Form2panel.Controls[i] is TextBox && this.Form2panel.Controls[i].Enabled)
					{
						this.lastControl = this.Form2panel.Controls[i];
						base.ActiveControl = this.Form2panel.Controls[i];
						this.Form2panel.Controls[i].Focus();
						this.Form2panel.Controls[i].Select();
						return;
					}
				}
				base.ActiveControl = this.buttonNext;
				this.lastControl = this.buttonNext;
			}
			catch (System.ArgumentOutOfRangeException ex)
			{
				MessageBox.Show("Komunikat błedu: funkcja setLastFocus: " + ex.Message, "Uwaga");
			}
		}
		private void validateExportedXml(int step)
		{
			this.wait.setWaitPanel("Trwa walidacja protokołu", "Proszę czekać");
			this.wait.setVisible(true);
			System.Collections.Generic.Dictionary<string, Errors> errors = new System.Collections.Generic.Dictionary<string, Errors>();
			char[] charsToTrim = new char[]
			{
				' ',
				'.',
				'-',
				':',
				',',
				'_',
				'\n'
			};
			if (step == 1)
			{
				foreach (Control c in this.Form1panel.Controls)
				{
					if (c is TextBox && c.Enabled)
					{
						string text = c.Text.Trim(charsToTrim);
						if (text == "")
						{
							try
							{
								errors["step1_" + c.Name].addHardError("ErrorNull");
							}
							catch (System.Collections.Generic.KeyNotFoundException)
							{
								errors.Add("step1_" + c.Name, new Errors());
								errors["step1_" + c.Name].addHardError("ErrorNull");
							}
						}
						else
						{
							try
							{
								if (this.typeValidation[c.Name] == "text")
								{
									if (!this.text_isValid(c.Text))
									{
										try
										{
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step1_" + c.Name, new Errors());
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "normalText")
								{
									if (!this.normalText_isValid(c.Text))
									{
										try
										{
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step1_" + c.Name, new Errors());
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "dateTime")
								{
									if (!this.dateTime_isValid(c.Text))
									{
										try
										{
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step1_" + c.Name, new Errors());
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "time")
								{
									if (!this.time_isValid(c.Text))
									{
										try
										{
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step1_" + c.Name, new Errors());
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "date")
								{
									if (!this.date_isValid(c.Text))
									{
										try
										{
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step1_" + c.Name, new Errors());
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "number")
								{
									string pattern = "^[0-9]+$";
									try
									{
										ValidationRange value = this.range[c.Name];
										bool r = System.Text.RegularExpressions.Regex.IsMatch(c.Text, pattern);
										if (r)
										{
											try
											{
												int num = System.Convert.ToInt32(c.Text);
												if (num < value.getMin() || num > value.getMax())
												{
													if (value.getMin() != 0 || value.getMax() != 0)
													{
														try
														{
															errors["step1_" + c.Name].addHardError("SNT01");
														}
														catch (System.Collections.Generic.KeyNotFoundException)
														{
															errors.Add("step1_" + c.Name, new Errors());
															errors["step1_" + c.Name].addHardError("SNT01");
														}
													}
												}
											}
											catch (System.OverflowException)
											{
												try
												{
													errors["step1_" + c.Name].addHardError("SNT01");
												}
												catch (System.Collections.Generic.KeyNotFoundException)
												{
													errors.Add("step1_" + c.Name, new Errors());
													errors["step1_" + c.Name].addHardError("SNT01");
												}
											}
										}
										else
										{
											try
											{
												errors["step1_" + c.Name].addHardError("ErrorType");
											}
											catch (System.Collections.Generic.KeyNotFoundException)
											{
												errors.Add("step1_" + c.Name, new Errors());
												errors["step1_" + c.Name].addHardError("ErrorType");
											}
										}
									}
									catch (System.Collections.Generic.KeyNotFoundException)
									{
									}
								}
							}
							catch (System.Exception)
							{
							}
						}
					}
				}
				try
				{
					errors = this.lastValidation(this.Form1panel, errors, true);
				}
				catch (System.Exception)
				{
				}
			}
			if (step == 2)
			{
				foreach (Control c in this.Form1panel.Controls)
				{
					if (c is TextBox && c.Enabled)
					{
						string text = c.Text.Trim(charsToTrim);
						if (text == "")
						{
							try
							{
								errors["step1_" + c.Name].addHardError("ErrorNull");
							}
							catch (System.Collections.Generic.KeyNotFoundException)
							{
								errors.Add("step1_" + c.Name, new Errors());
								errors["step1_" + c.Name].addHardError("ErrorNull");
							}
						}
						else
						{
							try
							{
								if (this.typeValidation[c.Name] == "text")
								{
									if (!this.text_isValid(c.Text))
									{
										try
										{
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step1_" + c.Name, new Errors());
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "normalText")
								{
									if (!this.normalText_isValid(c.Text))
									{
										try
										{
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step1_" + c.Name, new Errors());
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "dateTime")
								{
									if (!this.dateTime_isValid(c.Text))
									{
										try
										{
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step1_" + c.Name, new Errors());
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "time")
								{
									if (!this.time_isValid(c.Text))
									{
										try
										{
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step1_" + c.Name, new Errors());
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "date")
								{
									if (!this.date_isValid(c.Text))
									{
										try
										{
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step1_" + c.Name, new Errors());
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "number")
								{
									string pattern = "^[0-9]+$";
									try
									{
										ValidationRange value = this.range[c.Name];
										bool r = System.Text.RegularExpressions.Regex.IsMatch(c.Text, pattern);
										if (r)
										{
											try
											{
												int num = System.Convert.ToInt32(c.Text);
												if (num < value.getMin() || num > value.getMax())
												{
													if (value.getMin() != 0 || value.getMax() != 0)
													{
														try
														{
															errors["step1_" + c.Name].addHardError("SNT01");
														}
														catch (System.Collections.Generic.KeyNotFoundException)
														{
															errors.Add("step1_" + c.Name, new Errors());
															errors["step1_" + c.Name].addHardError("SNT01");
														}
													}
												}
											}
											catch (System.OverflowException)
											{
												try
												{
													errors["step1_" + c.Name].addHardError("SNT01");
												}
												catch (System.Collections.Generic.KeyNotFoundException)
												{
													errors.Add("step1_" + c.Name, new Errors());
													errors["step1_" + c.Name].addHardError("SNT01");
												}
											}
										}
										else
										{
											try
											{
												errors["step1_" + c.Name].addHardError("ErrorType");
											}
											catch (System.Collections.Generic.KeyNotFoundException)
											{
												errors.Add("step1_" + c.Name, new Errors());
												errors["step1_" + c.Name].addHardError("ErrorType");
											}
										}
									}
									catch (System.Collections.Generic.KeyNotFoundException)
									{
									}
								}
							}
							catch (System.Exception)
							{
							}
						}
						text = this.Form2panel.Controls[c.Name].Text.Trim(charsToTrim);
						if (text == "")
						{
							try
							{
								errors["step2_" + c.Name].addHardError("ErrorNull");
							}
							catch (System.Collections.Generic.KeyNotFoundException)
							{
								errors.Add("step2_" + c.Name, new Errors());
								errors["step2_" + c.Name].addHardError("ErrorNull");
							}
						}
						else
						{
							try
							{
								if (this.typeValidation[c.Name] == "text")
								{
									if (!this.text_isValid(this.Form2panel.Controls[c.Name].Text))
									{
										try
										{
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step2_" + c.Name, new Errors());
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "normalText")
								{
									if (!this.normalText_isValid(this.Form2panel.Controls[c.Name].Text))
									{
										try
										{
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step2_" + c.Name, new Errors());
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "dateTime")
								{
									if (!this.dateTime_isValid(this.Form2panel.Controls[c.Name].Text))
									{
										try
										{
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step2_" + c.Name, new Errors());
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "time")
								{
									if (!this.time_isValid(this.Form2panel.Controls[c.Name].Text))
									{
										try
										{
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step2_" + c.Name, new Errors());
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "date")
								{
									if (!this.date_isValid(this.Form2panel.Controls[c.Name].Text))
									{
										try
										{
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step2_" + c.Name, new Errors());
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "number")
								{
									try
									{
										ValidationRange value = this.range["step2_" + c.Name];
										string pattern = "^[0-9]+$";
										bool r = System.Text.RegularExpressions.Regex.IsMatch(this.Form2panel.Controls["step2_" + c.Name].Text, pattern);
										if (r)
										{
											try
											{
												int num = System.Convert.ToInt32(this.Form2panel.Controls["step2_" + c.Name].Text);
												if (num < value.getMin() || num > value.getMax())
												{
													if (value.getMin() != 0 || value.getMax() != 0)
													{
														try
														{
															errors["step2_" + c.Name].addHardError("SNT01");
														}
														catch (System.Collections.Generic.KeyNotFoundException)
														{
															errors.Add("step2_" + c.Name, new Errors());
															errors["step2_" + c.Name].addHardError("SNT01");
														}
													}
												}
											}
											catch (System.OverflowException)
											{
												try
												{
													errors["step2_" + c.Name].addHardError("SNT01");
												}
												catch (System.Collections.Generic.KeyNotFoundException)
												{
													errors.Add("step2_" + c.Name, new Errors());
													errors["step2_" + c.Name].addHardError("SNT01");
												}
											}
										}
										else
										{
											try
											{
												errors["step2_" + c.Name].addHardError("ErrorType");
											}
											catch (System.Collections.Generic.KeyNotFoundException)
											{
												errors.Add("step2_" + c.Name, new Errors());
												errors["step2_" + c.Name].addHardError("ErrorType");
											}
										}
									}
									catch (System.Collections.Generic.KeyNotFoundException)
									{
									}
								}
							}
							catch (System.Exception)
							{
							}
						}
						if (this.Form2panel.Controls[c.Name].Text != c.Text)
						{
							try
							{
								errors["step2_" + c.Name].addHardError("ErrorEqual");
							}
							catch (System.Collections.Generic.KeyNotFoundException)
							{
								errors.Add("step2_" + c.Name, new Errors());
								errors["step2_" + c.Name].addHardError("ErrorEqual");
							}
						}
					}
				}
				try
				{
					errors = this.lastValidation(this.Form1panel, errors, true);
				}
				catch (System.Exception)
				{
				}
				try
				{
					errors = this.lastValidation(this.Form2panel, errors, true);
				}
				catch (System.Exception)
				{
				}
			}
			if (step == 3)
			{
				foreach (Control c in this.Form1panel.Controls)
				{
					if (c is TextBox && c.Enabled)
					{
						string text = c.Text.Trim(charsToTrim);
						if (text == "")
						{
							try
							{
								errors["step1_" + c.Name].addHardError("ErrorNull");
							}
							catch (System.Collections.Generic.KeyNotFoundException)
							{
								errors.Add("step1_" + c.Name, new Errors());
								errors["step1_" + c.Name].addHardError("ErrorNull");
							}
						}
						else
						{
							try
							{
								if (this.typeValidation[c.Name] == "text")
								{
									if (!this.text_isValid(c.Text))
									{
										try
										{
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step1_" + c.Name, new Errors());
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "normalText")
								{
									if (!this.normalText_isValid(c.Text))
									{
										try
										{
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step1_" + c.Name, new Errors());
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "dateTime")
								{
									if (!this.dateTime_isValid(c.Text))
									{
										try
										{
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step1_" + c.Name, new Errors());
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "time")
								{
									if (!this.time_isValid(c.Text))
									{
										try
										{
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step1_" + c.Name, new Errors());
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "date")
								{
									if (!this.date_isValid(c.Text))
									{
										try
										{
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step1_" + c.Name, new Errors());
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "number")
								{
									string pattern = "^[0-9]+$";
									try
									{
										ValidationRange value = this.range[c.Name];
										bool r = System.Text.RegularExpressions.Regex.IsMatch(c.Text, pattern);
										if (r)
										{
											try
											{
												int num = System.Convert.ToInt32(c.Text);
												if (num < value.getMin() || num > value.getMax())
												{
													if (value.getMin() != 0 || value.getMax() != 0)
													{
														try
														{
															errors["step1_" + c.Name].addHardError("SNT01");
														}
														catch (System.Collections.Generic.KeyNotFoundException)
														{
															errors.Add("step1_" + c.Name, new Errors());
															errors["step1_" + c.Name].addHardError("SNT01");
														}
													}
												}
											}
											catch (System.OverflowException)
											{
												try
												{
													errors["step1_" + c.Name].addHardError("SNT01");
												}
												catch (System.Collections.Generic.KeyNotFoundException)
												{
													errors.Add("step1_" + c.Name, new Errors());
													errors["step1_" + c.Name].addHardError("SNT01");
												}
											}
										}
										else
										{
											try
											{
												errors["step1_" + c.Name].addHardError("ErrorType");
											}
											catch (System.Collections.Generic.KeyNotFoundException)
											{
												errors.Add("step1_" + c.Name, new Errors());
												errors["step1_" + c.Name].addHardError("ErrorType");
											}
										}
									}
									catch (System.Collections.Generic.KeyNotFoundException)
									{
									}
								}
							}
							catch (System.Exception)
							{
							}
						}
						text = this.Form2panel.Controls[c.Name].Text.Trim(charsToTrim);
						if (text == "")
						{
							try
							{
								errors["step2_" + c.Name].addHardError("ErrorNull");
							}
							catch (System.Collections.Generic.KeyNotFoundException)
							{
								errors.Add("step2_" + c.Name, new Errors());
								errors["step2_" + c.Name].addHardError("ErrorNull");
							}
						}
						else
						{
							try
							{
								if (this.typeValidation[c.Name] == "text")
								{
									if (!this.text_isValid(this.Form2panel.Controls[c.Name].Text))
									{
										try
										{
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step2_" + c.Name, new Errors());
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "normalText")
								{
									if (!this.normalText_isValid(this.Form2panel.Controls[c.Name].Text))
									{
										try
										{
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step2_" + c.Name, new Errors());
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "dateTime")
								{
									if (!this.dateTime_isValid(this.Form2panel.Controls[c.Name].Text))
									{
										try
										{
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step2_" + c.Name, new Errors());
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "time")
								{
									if (!this.time_isValid(this.Form2panel.Controls[c.Name].Text))
									{
										try
										{
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step2_" + c.Name, new Errors());
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "date")
								{
									if (!this.date_isValid(this.Form2panel.Controls[c.Name].Text))
									{
										try
										{
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step2_" + c.Name, new Errors());
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "number")
								{
									try
									{
										ValidationRange value = this.range["step2_" + c.Name];
										string pattern = "^[0-9]+$";
										bool r = System.Text.RegularExpressions.Regex.IsMatch(this.Form2panel.Controls["step2_" + c.Name].Text, pattern);
										if (r)
										{
											try
											{
												int num = System.Convert.ToInt32(this.Form2panel.Controls["step2_" + c.Name].Text);
												if (num < value.getMin() || num > value.getMax())
												{
													if (value.getMin() != 0 || value.getMax() != 0)
													{
														try
														{
															errors["step2_" + c.Name].addHardError("SNT01");
														}
														catch (System.Collections.Generic.KeyNotFoundException)
														{
															errors.Add("step2_" + c.Name, new Errors());
															errors["step2_" + c.Name].addHardError("SNT01");
														}
													}
												}
											}
											catch (System.OverflowException)
											{
												try
												{
													errors["step2_" + c.Name].addHardError("SNT01");
												}
												catch (System.Collections.Generic.KeyNotFoundException)
												{
													errors.Add("step2_" + c.Name, new Errors());
													errors["step2_" + c.Name].addHardError("SNT01");
												}
											}
										}
										else
										{
											try
											{
												errors["step2_" + c.Name].addHardError("ErrorType");
											}
											catch (System.Collections.Generic.KeyNotFoundException)
											{
												errors.Add("step2_" + c.Name, new Errors());
												errors["step2_" + c.Name].addHardError("ErrorType");
											}
										}
									}
									catch (System.Collections.Generic.KeyNotFoundException)
									{
									}
								}
							}
							catch (System.Exception)
							{
							}
						}
						if (this.Form2panel.Controls[c.Name].Text != c.Text)
						{
							try
							{
								errors["step2_" + c.Name].addHardError("ErrorEqual");
							}
							catch (System.Collections.Generic.KeyNotFoundException)
							{
								errors.Add("step2_" + c.Name, new Errors());
								errors["step2_" + c.Name].addHardError("ErrorEqual");
							}
						}
					}
				}
				try
				{
					errors = this.lastValidation(this.Form1panel, errors, true);
				}
				catch (System.Exception)
				{
				}
				try
				{
					errors = this.lastValidation(this.Form2panel, errors, true);
				}
				catch (System.Exception)
				{
				}
				foreach (Control c in this.SummationPanel.Controls)
				{
					if (c is TextBox && c.Enabled)
					{
						string text = c.Text.Trim(charsToTrim);
						if (text == "")
						{
							try
							{
								errors["step3_" + c.Name].addHardError("ErrorNull");
							}
							catch (System.Collections.Generic.KeyNotFoundException)
							{
								errors.Add("step3_" + c.Name, new Errors());
								errors["step3_" + c.Name].addHardError("ErrorNull");
							}
						}
						else
						{
							try
							{
								if (this.typeValidation[c.Name] == "text")
								{
									if (!this.text_isValid(c.Text))
									{
										try
										{
											errors["step3_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step3_" + c.Name, new Errors());
											errors["step3_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "normalText")
								{
									if (!this.normalText_isValid(c.Text))
									{
										try
										{
											errors["step3_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step3_" + c.Name, new Errors());
											errors["step3_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "dateTime")
								{
									if (!this.dateTime_isValid(c.Text))
									{
										try
										{
											errors["step3_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step3_" + c.Name, new Errors());
											errors["step3_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "time")
								{
									if (!this.time_isValid(c.Text))
									{
										try
										{
											errors["step3_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step3_" + c.Name, new Errors());
											errors["step3_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "date")
								{
									if (!this.date_isValid(c.Text))
									{
										try
										{
											errors["step3_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step3_" + c.Name, new Errors());
											errors["step3_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "number")
								{
								}
							}
							catch (System.Exception)
							{
							}
						}
					}
				}
			}
			if (step == 4 || step == 0)
			{
				errors = this.committeeValid(errors);
			}
			string hardErrorsXml = "";
			string hardWarringXml = "";
			string softErrorsXml = "";
			foreach (System.Collections.Generic.KeyValuePair<string, Errors> entry in errors)
			{
				string s = entry.Value.getHardErrorXml();
				if (s != "")
				{
					string text2 = hardErrorsXml;
					hardErrorsXml = string.Concat(new string[]
					{
						text2,
						"<",
						entry.Key,
						">",
						s,
						"</",
						entry.Key,
						">"
					});
				}
				s = entry.Value.getHardWarningXml();
				if (s != "")
				{
					string text2 = hardWarringXml;
					hardWarringXml = string.Concat(new string[]
					{
						text2,
						"<",
						entry.Key,
						">",
						s,
						"</",
						entry.Key,
						">"
					});
				}
				s = entry.Value.getSoftErrorXml();
				if (s != "")
				{
					string text2 = softErrorsXml;
					softErrorsXml = string.Concat(new string[]
					{
						text2,
						"<",
						entry.Key,
						">",
						s,
						"</",
						entry.Key,
						">"
					});
				}
			}
			string xml = "";
			if (hardErrorsXml != "")
			{
				xml = xml + "<hardError>" + hardErrorsXml + "</hardError>";
			}
			if (hardWarringXml != "")
			{
				xml = xml + "<hardWarning>" + hardWarringXml + "</hardWarning>";
			}
			if (softErrorsXml != "")
			{
				xml = xml + "<softError>" + softErrorsXml + "</softError>";
			}
			this.saves(step, xml);
			this.wait.setVisible(false);
		}
		private string validateExportedXmlS(int step)
		{
			this.wait.setWaitPanel("Trwa walidacja protokołu", "Proszę czekać");
			this.wait.setVisible(true);
			System.Collections.Generic.Dictionary<string, Errors> errors = new System.Collections.Generic.Dictionary<string, Errors>();
			char[] charsToTrim = new char[]
			{
				' ',
				'.',
				'-',
				':',
				',',
				'_',
				'\n'
			};
			if (step == 1)
			{
				foreach (Control c in this.Form1panel.Controls)
				{
					if (c is TextBox && c.Enabled)
					{
						string text = c.Text.Trim(charsToTrim);
						if (text == "")
						{
							try
							{
								errors["step1_" + c.Name].addHardError("ErrorNull");
							}
							catch (System.Collections.Generic.KeyNotFoundException)
							{
								errors.Add("step1_" + c.Name, new Errors());
								errors["step1_" + c.Name].addHardError("ErrorNull");
							}
						}
						else
						{
							try
							{
								if (this.typeValidation[c.Name] == "text")
								{
									if (!this.text_isValid(c.Text))
									{
										try
										{
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step1_" + c.Name, new Errors());
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "normalText")
								{
									if (!this.normalText_isValid(c.Text))
									{
										try
										{
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step1_" + c.Name, new Errors());
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "dateTime")
								{
									if (!this.dateTime_isValid(c.Text))
									{
										try
										{
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step1_" + c.Name, new Errors());
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "time")
								{
									if (!this.time_isValid(c.Text))
									{
										try
										{
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step1_" + c.Name, new Errors());
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "date")
								{
									if (!this.date_isValid(c.Text))
									{
										try
										{
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step1_" + c.Name, new Errors());
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "number")
								{
									string pattern = "^[0-9]+$";
									try
									{
										ValidationRange value = this.range[c.Name];
										bool r = System.Text.RegularExpressions.Regex.IsMatch(c.Text, pattern);
										if (r)
										{
											try
											{
												int num = System.Convert.ToInt32(c.Text);
												if (num < value.getMin() || num > value.getMax())
												{
													if (value.getMin() != 0 || value.getMax() != 0)
													{
														try
														{
															errors["step1_" + c.Name].addHardError("SNT01");
														}
														catch (System.Collections.Generic.KeyNotFoundException)
														{
															errors.Add("step1_" + c.Name, new Errors());
															errors["step1_" + c.Name].addHardError("SNT01");
														}
													}
												}
											}
											catch (System.OverflowException)
											{
												try
												{
													errors["step1_" + c.Name].addHardError("SNT01");
												}
												catch (System.Collections.Generic.KeyNotFoundException)
												{
													errors.Add("step1_" + c.Name, new Errors());
													errors["step1_" + c.Name].addHardError("SNT01");
												}
											}
										}
										else
										{
											try
											{
												errors["step1_" + c.Name].addHardError("ErrorType");
											}
											catch (System.Collections.Generic.KeyNotFoundException)
											{
												errors.Add("step1_" + c.Name, new Errors());
												errors["step1_" + c.Name].addHardError("ErrorType");
											}
										}
									}
									catch (System.Collections.Generic.KeyNotFoundException)
									{
									}
								}
							}
							catch (System.Exception)
							{
							}
						}
					}
				}
				errors = this.lastValidation(this.Form1panel, errors, true);
			}
			if (step == 2)
			{
				foreach (Control c in this.Form1panel.Controls)
				{
					if (c is TextBox && c.Enabled)
					{
						string text = c.Text.Trim(charsToTrim);
						if (text == "")
						{
							try
							{
								errors["step1_" + c.Name].addHardError("ErrorNull");
							}
							catch (System.Collections.Generic.KeyNotFoundException)
							{
								errors.Add("step1_" + c.Name, new Errors());
								errors["step1_" + c.Name].addHardError("ErrorNull");
							}
						}
						else
						{
							try
							{
								if (this.typeValidation[c.Name] == "text")
								{
									if (!this.text_isValid(c.Text))
									{
										try
										{
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step1_" + c.Name, new Errors());
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "normalText")
								{
									if (!this.normalText_isValid(c.Text))
									{
										try
										{
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step1_" + c.Name, new Errors());
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "dateTime")
								{
									if (!this.dateTime_isValid(c.Text))
									{
										try
										{
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step1_" + c.Name, new Errors());
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "time")
								{
									if (!this.time_isValid(c.Text))
									{
										try
										{
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step1_" + c.Name, new Errors());
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "date")
								{
									if (!this.date_isValid(c.Text))
									{
										try
										{
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step1_" + c.Name, new Errors());
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "number")
								{
									string pattern = "^[0-9]+$";
									try
									{
										ValidationRange value = this.range[c.Name];
										bool r = System.Text.RegularExpressions.Regex.IsMatch(c.Text, pattern);
										if (r)
										{
											try
											{
												int num = System.Convert.ToInt32(c.Text);
												if (num < value.getMin() || num > value.getMax())
												{
													if (value.getMin() != 0 || value.getMax() != 0)
													{
														try
														{
															errors["step1_" + c.Name].addHardError("SNT01");
														}
														catch (System.Collections.Generic.KeyNotFoundException)
														{
															errors.Add("step1_" + c.Name, new Errors());
															errors["step1_" + c.Name].addHardError("SNT01");
														}
													}
												}
											}
											catch (System.OverflowException)
											{
												try
												{
													errors["step1_" + c.Name].addHardError("SNT01");
												}
												catch (System.Collections.Generic.KeyNotFoundException)
												{
													errors.Add("step1_" + c.Name, new Errors());
													errors["step1_" + c.Name].addHardError("SNT01");
												}
											}
										}
										else
										{
											try
											{
												errors["step1_" + c.Name].addHardError("ErrorType");
											}
											catch (System.Collections.Generic.KeyNotFoundException)
											{
												errors.Add("step1_" + c.Name, new Errors());
												errors["step1_" + c.Name].addHardError("ErrorType");
											}
										}
									}
									catch (System.Collections.Generic.KeyNotFoundException)
									{
									}
								}
							}
							catch (System.Exception)
							{
							}
						}
						text = this.Form2panel.Controls[c.Name].Text.Trim(charsToTrim);
						if (text == "")
						{
							try
							{
								errors["step2_" + c.Name].addHardError("ErrorNull");
							}
							catch (System.Collections.Generic.KeyNotFoundException)
							{
								errors.Add("step2_" + c.Name, new Errors());
								errors["step2_" + c.Name].addHardError("ErrorNull");
							}
						}
						else
						{
							try
							{
								if (this.typeValidation[c.Name] == "text")
								{
									if (!this.text_isValid(this.Form2panel.Controls[c.Name].Text))
									{
										try
										{
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step2_" + c.Name, new Errors());
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "normalText")
								{
									if (!this.normalText_isValid(this.Form2panel.Controls[c.Name].Text))
									{
										try
										{
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step2_" + c.Name, new Errors());
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "dateTime")
								{
									if (!this.dateTime_isValid(this.Form2panel.Controls[c.Name].Text))
									{
										try
										{
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step2_" + c.Name, new Errors());
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "time")
								{
									if (!this.time_isValid(this.Form2panel.Controls[c.Name].Text))
									{
										try
										{
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step2_" + c.Name, new Errors());
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "date")
								{
									if (!this.date_isValid(this.Form2panel.Controls[c.Name].Text))
									{
										try
										{
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step2_" + c.Name, new Errors());
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "number")
								{
									try
									{
										ValidationRange value = this.range["step2_" + c.Name];
										string pattern = "^[0-9]+$";
										bool r = System.Text.RegularExpressions.Regex.IsMatch(this.Form2panel.Controls["step2_" + c.Name].Text, pattern);
										if (r)
										{
											try
											{
												int num = System.Convert.ToInt32(this.Form2panel.Controls["step2_" + c.Name].Text);
												if (num < value.getMin() || num > value.getMax())
												{
													if (value.getMin() != 0 || value.getMax() != 0)
													{
														try
														{
															errors["step2_" + c.Name].addHardError("SNT01");
														}
														catch (System.Collections.Generic.KeyNotFoundException)
														{
															errors.Add("step2_" + c.Name, new Errors());
															errors["step2_" + c.Name].addHardError("SNT01");
														}
													}
												}
											}
											catch (System.OverflowException)
											{
												try
												{
													errors["step2_" + c.Name].addHardError("SNT01");
												}
												catch (System.Collections.Generic.KeyNotFoundException)
												{
													errors.Add("step2_" + c.Name, new Errors());
													errors["step2_" + c.Name].addHardError("SNT01");
												}
											}
										}
										else
										{
											try
											{
												errors["step2_" + c.Name].addHardError("ErrorType");
											}
											catch (System.Collections.Generic.KeyNotFoundException)
											{
												errors.Add("step2_" + c.Name, new Errors());
												errors["step2_" + c.Name].addHardError("ErrorType");
											}
										}
									}
									catch (System.Collections.Generic.KeyNotFoundException)
									{
									}
								}
							}
							catch (System.Exception)
							{
							}
						}
						if (this.Form2panel.Controls[c.Name].Text != c.Text)
						{
							try
							{
								errors["step2_" + c.Name].addHardError("ErrorEqual");
							}
							catch (System.Collections.Generic.KeyNotFoundException)
							{
								errors.Add("step2_" + c.Name, new Errors());
								errors["step2_" + c.Name].addHardError("ErrorEqual");
							}
						}
					}
				}
				errors = this.lastValidation(this.Form1panel, errors, true);
				errors = this.lastValidation(this.Form2panel, errors, true);
			}
			if (step == 3)
			{
				foreach (Control c in this.Form1panel.Controls)
				{
					if (c is TextBox && c.Enabled)
					{
						string text = c.Text.Trim(charsToTrim);
						if (text == "")
						{
							try
							{
								errors["step1_" + c.Name].addHardError("ErrorNull");
							}
							catch (System.Collections.Generic.KeyNotFoundException)
							{
								errors.Add("step1_" + c.Name, new Errors());
								errors["step1_" + c.Name].addHardError("ErrorNull");
							}
						}
						else
						{
							try
							{
								if (this.typeValidation[c.Name] == "text")
								{
									if (!this.text_isValid(c.Text))
									{
										try
										{
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step1_" + c.Name, new Errors());
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "normalText")
								{
									if (!this.normalText_isValid(c.Text))
									{
										try
										{
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step1_" + c.Name, new Errors());
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "dateTime")
								{
									if (!this.dateTime_isValid(c.Text))
									{
										try
										{
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step1_" + c.Name, new Errors());
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "time")
								{
									if (!this.time_isValid(c.Text))
									{
										try
										{
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step1_" + c.Name, new Errors());
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "date")
								{
									if (!this.date_isValid(c.Text))
									{
										try
										{
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step1_" + c.Name, new Errors());
											errors["step1_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "number")
								{
									string pattern = "^[0-9]+$";
									try
									{
										ValidationRange value = this.range[c.Name];
										bool r = System.Text.RegularExpressions.Regex.IsMatch(c.Text, pattern);
										if (r)
										{
											try
											{
												int num = System.Convert.ToInt32(c.Text);
												if (num < value.getMin() || num > value.getMax())
												{
													if (value.getMin() != 0 || value.getMax() != 0)
													{
														try
														{
															errors["step1_" + c.Name].addHardError("SNT01");
														}
														catch (System.Collections.Generic.KeyNotFoundException)
														{
															errors.Add("step1_" + c.Name, new Errors());
															errors["step1_" + c.Name].addHardError("SNT01");
														}
													}
												}
											}
											catch (System.OverflowException)
											{
												try
												{
													errors["step1_" + c.Name].addHardError("SNT01");
												}
												catch (System.Collections.Generic.KeyNotFoundException)
												{
													errors.Add("step1_" + c.Name, new Errors());
													errors["step1_" + c.Name].addHardError("SNT01");
												}
											}
										}
										else
										{
											try
											{
												errors["step1_" + c.Name].addHardError("ErrorType");
											}
											catch (System.Collections.Generic.KeyNotFoundException)
											{
												errors.Add("step1_" + c.Name, new Errors());
												errors["step1_" + c.Name].addHardError("ErrorType");
											}
										}
									}
									catch (System.Collections.Generic.KeyNotFoundException)
									{
									}
								}
							}
							catch (System.Exception)
							{
							}
						}
						text = this.Form2panel.Controls[c.Name].Text.Trim(charsToTrim);
						if (text == "")
						{
							try
							{
								errors["step2_" + c.Name].addHardError("ErrorNull");
							}
							catch (System.Collections.Generic.KeyNotFoundException)
							{
								errors.Add("step2_" + c.Name, new Errors());
								errors["step2_" + c.Name].addHardError("ErrorNull");
							}
						}
						else
						{
							try
							{
								if (this.typeValidation[c.Name] == "text")
								{
									if (!this.text_isValid(this.Form2panel.Controls[c.Name].Text))
									{
										try
										{
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step2_" + c.Name, new Errors());
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "normalText")
								{
									if (!this.normalText_isValid(this.Form2panel.Controls[c.Name].Text))
									{
										try
										{
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step2_" + c.Name, new Errors());
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "dateTime")
								{
									if (!this.dateTime_isValid(this.Form2panel.Controls[c.Name].Text))
									{
										try
										{
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step2_" + c.Name, new Errors());
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "time")
								{
									if (!this.time_isValid(this.Form2panel.Controls[c.Name].Text))
									{
										try
										{
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step2_" + c.Name, new Errors());
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "date")
								{
									if (!this.date_isValid(this.Form2panel.Controls[c.Name].Text))
									{
										try
										{
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step2_" + c.Name, new Errors());
											errors["step2_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "number")
								{
									try
									{
										ValidationRange value = this.range["step2_" + c.Name];
										string pattern = "^[0-9]+$";
										bool r = System.Text.RegularExpressions.Regex.IsMatch(this.Form2panel.Controls["step2_" + c.Name].Text, pattern);
										if (r)
										{
											try
											{
												int num = System.Convert.ToInt32(this.Form2panel.Controls["step2_" + c.Name].Text);
												if (num < value.getMin() || num > value.getMax())
												{
													if (value.getMin() != 0 || value.getMax() != 0)
													{
														try
														{
															errors["step2_" + c.Name].addHardError("SNT01");
														}
														catch (System.Collections.Generic.KeyNotFoundException)
														{
															errors.Add("step2_" + c.Name, new Errors());
															errors["step2_" + c.Name].addHardError("SNT01");
														}
													}
												}
											}
											catch (System.OverflowException)
											{
												try
												{
													errors["step2_" + c.Name].addHardError("SNT01");
												}
												catch (System.Collections.Generic.KeyNotFoundException)
												{
													errors.Add("step2_" + c.Name, new Errors());
													errors["step2_" + c.Name].addHardError("SNT01");
												}
											}
										}
										else
										{
											try
											{
												errors["step2_" + c.Name].addHardError("ErrorType");
											}
											catch (System.Collections.Generic.KeyNotFoundException)
											{
												errors.Add("step2_" + c.Name, new Errors());
												errors["step2_" + c.Name].addHardError("ErrorType");
											}
										}
									}
									catch (System.Collections.Generic.KeyNotFoundException)
									{
									}
								}
							}
							catch (System.Exception)
							{
							}
						}
						if (this.Form2panel.Controls[c.Name].Text != c.Text)
						{
							try
							{
								errors["step2_" + c.Name].addHardError("ErrorEqual");
							}
							catch (System.Collections.Generic.KeyNotFoundException)
							{
								errors.Add("step2_" + c.Name, new Errors());
								errors["step2_" + c.Name].addHardError("ErrorEqual");
							}
						}
					}
				}
				errors = this.lastValidation(this.Form1panel, errors, true);
				errors = this.lastValidation(this.Form2panel, errors, true);
				foreach (Control c in this.SummationPanel.Controls)
				{
					if (c is TextBox && c.Enabled)
					{
						string text = c.Text.Trim(charsToTrim);
						if (text == "")
						{
							try
							{
								errors["step3_" + c.Name].addHardError("ErrorNull");
							}
							catch (System.Collections.Generic.KeyNotFoundException)
							{
								errors.Add("step3_" + c.Name, new Errors());
								errors["step3_" + c.Name].addHardError("ErrorNull");
							}
						}
						else
						{
							try
							{
								if (this.typeValidation[c.Name] == "text")
								{
									if (!this.text_isValid(c.Text))
									{
										try
										{
											errors["step3_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step3_" + c.Name, new Errors());
											errors["step3_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "normalText")
								{
									if (!this.normalText_isValid(c.Text))
									{
										try
										{
											errors["step3_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step3_" + c.Name, new Errors());
											errors["step3_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "dateTime")
								{
									if (!this.dateTime_isValid(c.Text))
									{
										try
										{
											errors["step3_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step3_" + c.Name, new Errors());
											errors["step3_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "time")
								{
									if (!this.time_isValid(c.Text))
									{
										try
										{
											errors["step3_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step3_" + c.Name, new Errors());
											errors["step3_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "date")
								{
									if (!this.date_isValid(c.Text))
									{
										try
										{
											errors["step3_" + c.Name].addHardError("ErrorType");
										}
										catch (System.Collections.Generic.KeyNotFoundException)
										{
											errors.Add("step3_" + c.Name, new Errors());
											errors["step3_" + c.Name].addHardError("ErrorType");
										}
									}
								}
								if (this.typeValidation[c.Name] == "number")
								{
								}
							}
							catch (System.Exception)
							{
							}
						}
					}
				}
			}
			if (step == 4 || step == 0)
			{
				errors = this.committeeValid(errors);
			}
			string hardErrorsXml = "";
			string hardWarringXml = "";
			string softErrorsXml = "";
			foreach (System.Collections.Generic.KeyValuePair<string, Errors> entry in errors)
			{
				string s = entry.Value.getHardErrorXml();
				if (s != "")
				{
					string text2 = hardErrorsXml;
					hardErrorsXml = string.Concat(new string[]
					{
						text2,
						"<",
						entry.Key,
						">",
						s,
						"</",
						entry.Key,
						">"
					});
				}
				s = entry.Value.getHardWarningXml();
				if (s != "")
				{
					string text2 = hardWarringXml;
					hardWarringXml = string.Concat(new string[]
					{
						text2,
						"<",
						entry.Key,
						">",
						s,
						"</",
						entry.Key,
						">"
					});
				}
				s = entry.Value.getSoftErrorXml();
				if (s != "")
				{
					string text2 = softErrorsXml;
					softErrorsXml = string.Concat(new string[]
					{
						text2,
						"<",
						entry.Key,
						">",
						s,
						"</",
						entry.Key,
						">"
					});
				}
			}
			string xml = "";
			if (hardErrorsXml != "")
			{
				xml = xml + "<hardError>" + hardErrorsXml + "</hardError>";
			}
			if (hardWarringXml != "")
			{
				xml = xml + "<hardWarning>" + hardWarringXml + "</hardWarning>";
			}
			if (softErrorsXml != "")
			{
				xml = xml + "<softError>" + softErrorsXml + "</softError>";
			}
			this.wait.setVisible(false);
			return xml;
		}
		private bool checkProtocol(XmlDocument save, string spath)
		{
			string importedJns = "";
			string importedElection = "";
			string importedCircuit = "";
			string importedOkr = "";
			string importedInst = "";
			string importedInstJNS = "";
			bool result;
			if (save.SelectSingleNode("/save/header") != null)
			{
				XmlNode xmlJns = save.SelectSingleNode("/save/header/jns_kod");
				if (xmlJns != null && xmlJns.InnerText != "")
				{
					importedJns = System.Convert.ToInt32(xmlJns.InnerText).ToString();
				}
				XmlNode xmlImportedInst = save.SelectSingleNode("/save/header/instJNS");
				if (xmlImportedInst != null && xmlImportedInst.InnerText != "")
				{
					importedInstJNS = xmlImportedInst.InnerText;
				}
				XmlNode xmlElection = save.SelectSingleNode("/save/header/defklk");
				if (xmlElection != null && xmlElection.FirstChild != null)
				{
					XmlNode xmlElection2 = xmlElection.FirstChild.Attributes.GetNamedItem("name");
					if (xmlElection2 != null && xmlElection2.Value != "")
					{
						string[] saveElectionPart = xmlElection2.Value.Split(new char[]
						{
							'-'
						});
						importedElection = saveElectionPart[0].Replace("/", "_");
						string[] okrPart = saveElectionPart[1].Split(new char[]
						{
							' '
						});
						importedInst = okrPart[0].Replace(".xml", "");
					}
				}
				XmlNode xmlCircuit = save.SelectSingleNode("/save/header/nrObwodu");
				if (xmlCircuit != null && xmlCircuit.InnerText != "")
				{
					importedCircuit = xmlCircuit.InnerText;
				}
				XmlNode xmlOkr = save.SelectSingleNode("/save/header/nrOkregu");
				if (xmlOkr != null && xmlOkr.InnerText != "")
				{
					string[] importedOkrPart = xmlOkr.InnerText.Split(new char[]
					{
						' '
					});
					importedOkr = importedOkrPart[0];
				}
				bool go = false;
				string[] ouPart = this.OU.Split(new char[]
				{
					'-'
				});
				if (ouPart[0].Replace("/", "_") == importedElection && System.Convert.ToInt32(ouPart[1]) == System.Convert.ToInt32(importedJns))
				{
					if (ouPart[2] == "O" || ouPart[2] == "A")
					{
						go = true;
					}
					else
					{
						if (ouPart[2] == "P" || ouPart[2] == "Z")
						{
							go = (ouPart[3] == importedCircuit);
						}
					}
				}
				string[] s = spath.Split(new char[]
				{
					'\\'
				});
				string[] name = s[s.Length - 1].Split(new char[]
				{
					' '
				});
				if (name[0].Replace(".xml", "") != string.Concat(new string[]
				{
					importedElection,
					"-",
					importedJns,
					"-",
					importedCircuit,
					"-",
					importedInst,
					"-",
					importedInstJNS,
					"-",
					importedOkr
				}))
				{
					int num = System.IO.Directory.GetFiles(this.path + "\\saves", string.Concat(new string[]
					{
						importedElection,
						"-",
						importedJns,
						"-",
						importedCircuit,
						"-",
						importedInst,
						"-",
						importedInstJNS,
						"-",
						importedOkr,
						"*.xml"
					})).Length;
					string namefile = string.Concat(new string[]
					{
						this.path,
						"\\saves\\",
						importedElection,
						"-",
						importedJns,
						"-",
						importedCircuit,
						"-",
						importedInst,
						"-",
						importedInstJNS,
						"-",
						importedOkr
					});
					if (num > 0)
					{
						namefile = namefile + " " + (num + 1).ToString();
					}
					try
					{
						System.IO.File.Move(spath, namefile + ".xml");
						this.savePath = namefile + ".xml";
						this.protocolDefinitionName = importedElection + "-" + importedInst + ".xml";
						this.candidatesName = string.Concat(new string[]
						{
							importedElection,
							"-",
							importedInst,
							"-",
							importedJns,
							"-",
							importedOkr,
							".xml"
						});
						this.committeeName = string.Concat(new string[]
						{
							importedElection,
							"-",
							importedJns,
							"-",
							importedCircuit,
							".xml"
						});
						this.validateDefinitionName = importedElection + "-" + importedInst + "_Walidacja.xml";
						this.headerName = importedElection + "-" + importedJns + ".xml";
						this.form.wait.setWaitPanel("Trwa otwieranie formularza protokołu - wczytywanie danych", "Proszę czekać");
						try
						{
							if (this.headerName != "")
							{
								this.header = new XmlDocument();
								if (System.IO.File.Exists(this.path + "\\electoralEampaign\\" + this.headerName))
								{
									this.header.Load(this.path + "\\electoralEampaign\\" + this.headerName);
								}
							}
							try
							{
								if (this.headerName != "")
								{
									this.header = new XmlDocument();
									if (System.IO.File.Exists(this.path + "\\electoralEampaign\\" + this.headerName))
									{
										this.header.Load(this.path + "\\electoralEampaign\\" + this.headerName);
									}
								}
								if (this.protocolDefinitionName != "")
								{
									this.protocolDefinition = new XmlDocument();
									if (System.IO.File.Exists(this.protocolDefinitionName))
									{
										this.protocolDefinition.Load(this.protocolDefinitionName);
									}
									else
									{
										this.isKLKPro = false;
									}
								}
								if (this.candidatesName != "")
								{
									this.candidates = new XmlDocument();
									if (System.IO.File.Exists(this.candidatesName))
									{
										this.candidates.Load(this.candidatesName);
									}
									else
									{
										this.isKLKCan = false;
									}
								}
								if (this.committeeName != "")
								{
									this.committee = new XmlDocument();
									if (System.IO.File.Exists(this.committeeName))
									{
										this.committee.Load(this.committeeName);
									}
									else
									{
										this.isKLK = false;
									}
								}
								if (this.validateDefinitionName != "")
								{
									this.validateDefinition = new XmlDocument();
									if (System.IO.File.Exists(this.validateDefinitionName))
									{
										this.validateDefinition.Load(this.validateDefinitionName);
									}
									else
									{
										this.isKLK = false;
									}
								}
								if (this.savePath != "")
								{
									this.save = new XmlDocument();
									if (System.IO.File.Exists(this.savePath))
									{
										this.save.Load(this.savePath);
									}
								}
								string organNazwa = "";
								XmlNode headerRoot = this.header.SelectSingleNode("/akcja_wyborcza/jns");
								foreach (XmlNode xObwod in headerRoot)
								{
									if (xObwod.Attributes["nr"].InnerText == importedCircuit)
									{
										foreach (XmlNode xInst in xObwod)
										{
											if (xInst.Attributes["kod"].InnerText == importedInst)
											{
												foreach (XmlNode xobw in xInst)
												{
													if (xobw.Attributes["nr"].InnerText == importedCircuit && System.Convert.ToInt32(xInst.Attributes["inst_jns"].InnerText) == System.Convert.ToInt32(importedInstJNS))
													{
														organNazwa = xInst.Attributes["organNazwa"].InnerText;
														break;
													}
												}
											}
										}
									}
								}
								string jns = importedJns;
								if (jns.Length < 6)
								{
									while (jns.Length < 6)
									{
										jns = "0" + jns;
									}
								}
								if (importedInst == "WBP")
								{
									if (jns.Substring(0, 4) == "1465" && jns.Length == 6)
									{
										if (this.candidatesName != "")
										{
											string candidates = this.candidatesName.Replace(jns + "-" + importedOkr + ".xml", "146501-1.xml");
											this.candidates = new XmlDocument();
											if (System.IO.File.Exists(candidates))
											{
												this.candidates.Load(candidates);
												this.isKLKCan = true;
											}
											else
											{
												this.isKLK = false;
												this.isKLKCan = false;
											}
										}
									}
								}
								if (importedInst == "RDA")
								{
									if (jns.Length < 6)
									{
										while (jns.Length < 6)
										{
											jns = "0" + jns;
										}
									}
									if (jns[2] == '7' || jns[2] == '6')
									{
										if (jns.Substring(0, 4) == "1465" && organNazwa == "m.st.")
										{
											this.protocolDefinition = new XmlDocument();
											string protocolDefinition = this.protocolDefinitionName.Replace(".xml", "_M.xml");
											if (System.IO.File.Exists(protocolDefinition))
											{
												this.protocolDefinition.Load(protocolDefinition);
												this.isKLKPro = true;
											}
											else
											{
												this.isKLKPro = false;
											}
											if (this.validateDefinitionName != "")
											{
												this.validateDefinition = new XmlDocument();
												string validateDefinition = this.validateDefinitionName.Replace("_Walidacja.xml", "_M_Walidacja.xml");
												if (System.IO.File.Exists(validateDefinition))
												{
													this.validateDefinition.Load(validateDefinition);
													this.isKLKWali = true;
												}
												else
												{
													this.isKLKWali = false;
												}
											}
											if (this.candidatesName != "")
											{
												string candidates = this.candidatesName.Replace(jns + "-" + importedOkr + ".xml", "146501-" + importedOkr + ".xml");
												this.candidates = new XmlDocument();
												if (System.IO.File.Exists(candidates))
												{
													this.candidates.Load(candidates);
													this.isKLKCan = true;
												}
												else
												{
													this.isKLK = false;
													this.isKLKCan = false;
												}
											}
										}
										if (jns.Substring(0, 4) != "1465")
										{
											if (this.protocolDefinitionName != "")
											{
												this.protocolDefinition = new XmlDocument();
												string protocolDefinition = this.protocolDefinitionName.Replace(".xml", "_M.xml");
												if (System.IO.File.Exists(protocolDefinition))
												{
													this.protocolDefinition.Load(protocolDefinition);
													this.isKLKPro = true;
												}
												else
												{
													this.isKLKPro = false;
												}
											}
											if (this.validateDefinitionName != "")
											{
												this.validateDefinition = new XmlDocument();
												string validateDefinition = this.validateDefinitionName.Replace("_Walidacja.xml", "_M_Walidacja.xml");
												if (System.IO.File.Exists(validateDefinition))
												{
													this.validateDefinition.Load(validateDefinition);
													this.isKLKWali = true;
												}
												else
												{
													this.isKLKWali = false;
												}
											}
										}
									}
									if (jns.Substring(0, 4) == "1465" && organNazwa == "Dzielnicy")
									{
										if (this.protocolDefinitionName != "")
										{
											this.protocolDefinition = new XmlDocument();
											string protocolDefinition = this.protocolDefinitionName.Replace(".xml", "_D.xml");
											if (System.IO.File.Exists(protocolDefinition))
											{
												this.protocolDefinition.Load(protocolDefinition);
												this.isKLKPro = true;
											}
											else
											{
												this.isKLKPro = false;
											}
										}
										if (this.validateDefinitionName != "")
										{
											this.validateDefinition = new XmlDocument();
											string validateDefinition = this.validateDefinitionName.Replace("_Walidacja.xml", "_D_Walidacja.xml");
											if (System.IO.File.Exists(validateDefinition))
											{
												this.validateDefinition.Load(validateDefinition);
												this.isKLKWali = true;
											}
											else
											{
												this.isKLKWali = false;
											}
										}
									}
								}
								if (this.isKLK && this.isKLKCan)
								{
									this.deletedCandidates = this.countOfDeletedCandidate();
									if (this.isOneCandidate())
									{
										this.deletedCandidates = 1;
										if (this.protocolDefinitionName != "")
										{
											this.protocolDefinition = new XmlDocument();
											string protocolDefinition = this.protocolDefinitionName.Replace(".xml", "_1.xml");
											if (System.IO.File.Exists(protocolDefinition))
											{
												this.protocolDefinition.Load(protocolDefinition);
												this.isKLKPro = true;
											}
											else
											{
												this.isKLKPro = false;
											}
										}
										if (this.validateDefinitionName != "")
										{
											this.validateDefinition = new XmlDocument();
											string validateDefinition = this.validateDefinitionName.Replace("_Walidacja.xml", "_1_Walidacja.xml");
											if (System.IO.File.Exists(validateDefinition))
											{
												this.validateDefinition.Load(validateDefinition);
												this.isKLKWali = true;
											}
											else
											{
												this.isKLKWali = false;
											}
										}
									}
								}
							}
							catch (XmlException e)
							{
								MessageBox.Show("Nieprawidłowy XML: " + e.Message, "Błąd");
							}
						}
						catch (XmlException e)
						{
							MessageBox.Show("Nieprawidłowy XML: " + e.Message, "Błąd");
						}
						MessageBox.Show(string.Concat(new string[]
						{
							"Protokół został przeniesiony do pliku: ",
							importedElection,
							"-",
							importedJns,
							"-",
							importedCircuit,
							"-",
							importedInst,
							"-",
							importedInstJNS,
							"-",
							importedOkr,
							".xml"
						}), "Uwaga");
					}
					catch (System.ArgumentNullException)
					{
						MessageBox.Show("Nie można przeniesć protokołu w odpowiednie miejsce", "Uwaga");
					}
					catch (System.ArgumentException)
					{
						MessageBox.Show("Nie można przeniesć protokołu w odpowiednie miejsce", "Uwaga");
					}
					catch (System.UnauthorizedAccessException)
					{
						MessageBox.Show("Nie masz uprawnień do przeniesiena protokołu w odpowiednie miejsce", "Uwaga");
					}
					catch (System.IO.PathTooLongException)
					{
						MessageBox.Show("Nieprawidłowa ścieżka do protokołu", "Uwaga");
					}
					catch (System.IO.DirectoryNotFoundException)
					{
						MessageBox.Show("Nieprawidłowa ścieżka do protokołu", "Uwaga");
					}
					catch (System.NotSupportedException)
					{
						MessageBox.Show("Nieprawidłowy format ścieżki do protokołu", "Uwaga");
					}
					catch (System.IO.IOException)
					{
						MessageBox.Show("Nie można odnaleźć protokołu", "Uwaga");
					}
				}
				result = go;
			}
			else
			{
				result = true;
			}
			return result;
		}
		private string stepControl(object sender)
		{
			int step = -1;
			Control a = sender as Control;
			if (a.Parent.Name == "Form1panel")
			{
				step = 1;
			}
			if (a.Parent.Name == "Form2panel")
			{
				step = 2;
			}
			if (a.Name == "raportPanel")
			{
				step = 3;
			}
			if (a.Parent.Name == "SummationPanel")
			{
				step = 3;
			}
			if (a.Parent.Name == "committeePanel")
			{
				step = 4;
			}
			return step.ToString();
		}
		private string stepControl(Panel p)
		{
			int step = -1;
			if (p.Name == "Form1panel")
			{
				step = 1;
			}
			if (p.Name == "Form2panel")
			{
				step = 2;
			}
			if (p.Name == "raportPanel")
			{
				step = 3;
			}
			if (p.Name == "SummationPanel")
			{
				step = 3;
			}
			if (p.Name == "committeePanel")
			{
				step = 4;
			}
			return step.ToString();
		}
		private bool isDoubled(System.Collections.Generic.List<Label> list, string text)
		{
			bool result;
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].Text == text)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}
		private bool isOneCandidate()
		{
			XmlNode c = this.candidates.SelectSingleNode("/listy");
			return c.ChildNodes.Count <= 1 && c.ChildNodes.Count != 0 && c.FirstChild.ChildNodes.Count <= 1;
		}
		private int countOfDeletedCandidate()
		{
			XmlNode c = this.candidates.SelectSingleNode("/listy");
			int sum = 0;
			foreach (XmlNode lista in c)
			{
				foreach (XmlNode candidate in lista)
				{
					XmlNode status = candidate.Attributes.GetNamedItem("status");
					if (status != null && status.Value != "A")
					{
						sum++;
					}
				}
			}
			return sum;
		}
		private void checkadnotation()
		{
			if (this.inst == "RDA" || this.inst == "WBP")
			{
				bool v = false;
				bool v2 = false;
				try
				{
					if (System.Convert.ToInt32(this.SummationPanel.Controls["field_1_4"].Text) == System.Convert.ToInt32(this.SummationPanel.Controls["field_1_4a"].Text) + System.Convert.ToInt32(this.SummationPanel.Controls["field_1_4b"].Text))
					{
						v = true;
					}
				}
				catch (System.Exception)
				{
				}
				try
				{
					if (System.Convert.ToInt32(this.SummationPanel.Controls["field_1_2"].Text) == System.Convert.ToInt32(this.SummationPanel.Controls["field_1_3"].Text) + System.Convert.ToInt32(this.SummationPanel.Controls["field_1_4"].Text))
					{
						v2 = true;
					}
				}
				catch (System.Exception)
				{
				}
				if (!v || !v2)
				{
					if (this.SummationPanel.Controls["field_3_14"].Text == "brak uwag")
					{
						this.errorProvider1.SetErrorWithCount(this.SummationPanel.Controls["field_3_14"], "W polu 14 musi zostać podane wyjaśnienie.", "AD14", this.stepControl(this.SummationPanel));
						this.SummationPanel.Controls["field_3_14"].ForeColor = System.Drawing.Color.Red;
						this.SummationPanel.Controls["field_3_14"].BackColor = System.Drawing.SystemColors.Info;
					}
					else
					{
						this.errorProvider1.SetErrorWithCount(this.SummationPanel.Controls["field_3_14"], "", "AD14", this.stepControl(this.SummationPanel));
						if (!this.errorProvider1.hasError(this.SummationPanel.Controls["field_3_14"]))
						{
							this.SummationPanel.Controls["field_3_14"].ForeColor = System.Drawing.Color.Black;
							this.SummationPanel.Controls["field_3_14"].BackColor = System.Drawing.SystemColors.Window;
						}
					}
				}
				v = false;
				v2 = false;
				try
				{
					if (System.Convert.ToInt32(this.SummationPanel.Controls["field_1_4"].Text) == System.Convert.ToInt32(this.SummationPanel.Controls["field_1_8"].Text) - System.Convert.ToInt32(this.SummationPanel.Controls["field_1_8a"].Text))
					{
						v = true;
					}
				}
				catch (System.Exception)
				{
				}
				try
				{
					if (System.Convert.ToInt32(this.SummationPanel.Controls["field_1_8a"].Text) <= System.Convert.ToInt32(this.SummationPanel.Controls["field_1_7e"].Text))
					{
						v2 = true;
					}
				}
				catch (System.Exception)
				{
				}
				if (!v || !v2)
				{
					if (this.SummationPanel.Controls["field_3_15"].Text == "brak uwag")
					{
						this.errorProvider1.SetErrorWithCount(this.SummationPanel.Controls["field_3_15"], "W polu 15 musi zostać podane wyjaśnienie.", "AD15", this.stepControl(this.SummationPanel));
						this.SummationPanel.Controls["field_3_15"].ForeColor = System.Drawing.Color.Red;
						this.SummationPanel.Controls["field_3_15"].BackColor = System.Drawing.SystemColors.Info;
					}
					else
					{
						this.errorProvider1.SetErrorWithCount(this.SummationPanel.Controls["field_3_15"], "", "AD15", this.stepControl(this.SummationPanel));
						if (!this.errorProvider1.hasError(this.SummationPanel.Controls["field_3_15"]))
						{
							this.SummationPanel.Controls["field_3_15"].ForeColor = System.Drawing.Color.Black;
							this.SummationPanel.Controls["field_3_15"].BackColor = System.Drawing.SystemColors.Window;
						}
					}
				}
				v = false;
				try
				{
					if (System.Convert.ToInt32(this.SummationPanel.Controls["field_1_9"].Text) == 0)
					{
						v = true;
					}
				}
				catch (System.Exception)
				{
				}
				if (!v)
				{
					if (this.SummationPanel.Controls["field_3_16"].Text == "brak uwag")
					{
						this.errorProvider1.SetErrorWithCount(this.SummationPanel.Controls["field_3_16"], "W polu 16 musi zostać podane wyjaśnienie.", "AD16", this.stepControl(this.SummationPanel));
						this.SummationPanel.Controls["field_3_16"].ForeColor = System.Drawing.Color.Red;
						this.SummationPanel.Controls["field_3_16"].BackColor = System.Drawing.SystemColors.Info;
					}
					else
					{
						this.errorProvider1.SetErrorWithCount(this.SummationPanel.Controls["field_3_16"], "", "AD16", this.stepControl(this.SummationPanel));
						if (!this.errorProvider1.hasError(this.SummationPanel.Controls["field_3_16"]))
						{
							this.SummationPanel.Controls["field_3_16"].ForeColor = System.Drawing.Color.Black;
							this.SummationPanel.Controls["field_3_16"].BackColor = System.Drawing.SystemColors.Window;
						}
					}
				}
			}
			if (this.inst == "RDP" || this.inst == "RDW")
			{
				bool v = false;
				bool v2 = false;
				try
				{
					if (System.Convert.ToInt32(this.SummationPanel.Controls["field_1_2"].Text) == System.Convert.ToInt32(this.SummationPanel.Controls["field_1_3"].Text) + System.Convert.ToInt32(this.SummationPanel.Controls["field_1_4"].Text))
					{
						v2 = true;
					}
				}
				catch (System.Exception)
				{
				}
				if (!v2)
				{
					if (this.SummationPanel.Controls["field_3_14"].Text == "brak uwag")
					{
						this.errorProvider1.SetErrorWithCount(this.SummationPanel.Controls["field_3_14"], "W polu 14 musi zostać podane wyjaśnienie.", "AD14", this.stepControl(this.SummationPanel));
						this.SummationPanel.Controls["field_3_14"].ForeColor = System.Drawing.Color.Red;
						this.SummationPanel.Controls["field_3_14"].BackColor = System.Drawing.SystemColors.Info;
					}
					else
					{
						this.errorProvider1.SetErrorWithCount(this.SummationPanel.Controls["field_3_14"], "", "AD14", this.stepControl(this.SummationPanel));
						if (!this.errorProvider1.hasError(this.SummationPanel.Controls["field_3_14"]))
						{
							this.SummationPanel.Controls["field_3_14"].ForeColor = System.Drawing.Color.Black;
							this.SummationPanel.Controls["field_3_14"].BackColor = System.Drawing.SystemColors.Window;
						}
					}
				}
				v = false;
				v2 = false;
				try
				{
					if (System.Convert.ToInt32(this.SummationPanel.Controls["field_1_4"].Text) == System.Convert.ToInt32(this.SummationPanel.Controls["field_1_8"].Text) - System.Convert.ToInt32(this.SummationPanel.Controls["field_1_8a"].Text))
					{
						v = true;
					}
				}
				catch (System.Exception)
				{
				}
				try
				{
					if (System.Convert.ToInt32(this.SummationPanel.Controls["field_1_8a"].Text) <= System.Convert.ToInt32(this.SummationPanel.Controls["field_1_7e"].Text))
					{
						v2 = true;
					}
				}
				catch (System.Exception)
				{
				}
				if (!v || !v2)
				{
					if (this.SummationPanel.Controls["field_3_15"].Text == "brak uwag")
					{
						this.errorProvider1.SetErrorWithCount(this.SummationPanel.Controls["field_3_15"], "W polu 15 musi zostać podane wyjaśnienie.", "AD15", this.stepControl(this.SummationPanel));
						this.SummationPanel.Controls["field_3_15"].ForeColor = System.Drawing.Color.Red;
						this.SummationPanel.Controls["field_3_15"].BackColor = System.Drawing.SystemColors.Info;
					}
					else
					{
						this.errorProvider1.SetErrorWithCount(this.SummationPanel.Controls["field_3_15"], "", "AD15", this.stepControl(this.SummationPanel));
						if (!this.errorProvider1.hasError(this.SummationPanel.Controls["field_3_15"]))
						{
							this.SummationPanel.Controls["field_3_15"].ForeColor = System.Drawing.Color.Black;
							this.SummationPanel.Controls["field_3_15"].BackColor = System.Drawing.SystemColors.Window;
						}
					}
				}
				v = false;
				try
				{
					if (System.Convert.ToInt32(this.SummationPanel.Controls["field_1_9"].Text) == 0)
					{
						v = true;
					}
				}
				catch (System.Exception)
				{
				}
				if (!v)
				{
					if (this.SummationPanel.Controls["field_3_16"].Text == "brak uwag")
					{
						this.errorProvider1.SetErrorWithCount(this.SummationPanel.Controls["field_3_16"], "W polu 16 musi zostać podane wyjaśnienie.", "AD16", this.stepControl(this.SummationPanel));
						this.SummationPanel.Controls["field_3_16"].ForeColor = System.Drawing.Color.Red;
						this.SummationPanel.Controls["field_3_16"].BackColor = System.Drawing.SystemColors.Info;
					}
					else
					{
						this.errorProvider1.SetErrorWithCount(this.SummationPanel.Controls["field_3_16"], "", "AD16", this.stepControl(this.SummationPanel));
						if (!this.errorProvider1.hasError(this.SummationPanel.Controls["field_3_16"]))
						{
							this.SummationPanel.Controls["field_3_16"].ForeColor = System.Drawing.Color.Black;
							this.SummationPanel.Controls["field_3_16"].BackColor = System.Drawing.SystemColors.Window;
						}
					}
				}
			}
		}
		private bool edit()
		{
			bool result;
			if (MessageBox.Show("Powrót do edycji tego kroku spowoduje utratę danych z kroku 3 i 4. Czy kontynuować?", "Edycja", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				this.saves(2);
				string filePath = this.savePath;
				this.wait.setWaitPanel("Trwa przygotowanie protokołu do edycji", "Proszę czekać");
				this.wait.setVisible(true);
				System.IO.StreamReader sr = new System.IO.StreamReader(filePath);
				string file = sr.ReadToEnd();
				sr.Close();
				try
				{
					string importedJns = "";
					string importedElection = "";
					string importedCircuit = "";
					string importedOkr = "";
					string importedInst = "";
					string importedInstJNS = "";
					XmlDocument importedProtocol = new XmlDocument();
					importedProtocol.LoadXml(file);
					XmlNode xmlImportedInst = importedProtocol.SelectSingleNode("/save/header/instJNS");
					if (xmlImportedInst != null && xmlImportedInst.InnerText != "")
					{
						importedInstJNS = xmlImportedInst.InnerText;
					}
					XmlNode xmlJns = importedProtocol.SelectSingleNode("/save/header/jns_kod");
					if (xmlJns != null && xmlJns.InnerText != "")
					{
						importedJns = xmlJns.InnerText;
					}
					XmlNode xmlElection = importedProtocol.SelectSingleNode("/save/header/defklk");
					if (xmlElection != null && xmlElection.FirstChild != null)
					{
						XmlNode xmlElection2 = xmlElection.FirstChild.Attributes.GetNamedItem("name");
						if (xmlElection2 != null && xmlElection2.Value != "")
						{
							string[] saveElectionPart = xmlElection2.Value.Split(new char[]
							{
								'-'
							});
							importedElection = saveElectionPart[0].Replace("/", "_");
							string[] okrPart = saveElectionPart[1].Split(new char[]
							{
								' '
							});
							importedInst = okrPart[0];
						}
					}
					XmlNode xmlCircuit = importedProtocol.SelectSingleNode("/save/header/nrObwodu");
					if (xmlCircuit != null && xmlCircuit.InnerText != "")
					{
						importedCircuit = xmlCircuit.InnerText;
					}
					XmlNode xmlOkr = importedProtocol.SelectSingleNode("/save/header/nrOkregu");
					if (xmlOkr != null && xmlOkr.InnerText != "")
					{
						importedOkr = xmlOkr.InnerText;
					}
					bool go = false;
					string[] ouPart = this.OU.Split(new char[]
					{
						'-'
					});
					if (ouPart[0].Replace("/", "_") == importedElection && System.Convert.ToInt32(ouPart[1]) == System.Convert.ToInt32(importedJns))
					{
						if (ouPart[2] == "O" || ouPart[2] == "A")
						{
							go = true;
						}
						else
						{
							if (ouPart[2] == "P" || ouPart[2] == "Z")
							{
								go = (ouPart[3] == importedCircuit);
							}
						}
					}
					if (go)
					{
						XmlDocument s = new XmlDocument();
						s.Load(filePath);
						string saveCircuit = "";
						xmlCircuit = s.SelectSingleNode("/save/header/nrObwodu");
						if (xmlCircuit != null && xmlCircuit.InnerText != "")
						{
							saveCircuit = xmlCircuit.InnerText;
						}
						string saveInst = "";
						xmlElection = s.SelectSingleNode("/save/header/defklk");
						if (xmlElection != null && xmlElection.FirstChild != null)
						{
							XmlNode xmlElection2 = xmlElection.FirstChild.Attributes.GetNamedItem("name");
							if (xmlElection2 != null && xmlElection2.Value != "")
							{
								string[] okrPart = xmlElection2.Value.Split(new char[]
								{
									'-'
								});
								saveInst = okrPart[1];
							}
						}
						if (System.Convert.ToInt32(importedCircuit) == System.Convert.ToInt32(saveCircuit) || System.Convert.ToInt32(importedInst) == System.Convert.ToInt32(saveInst))
						{
							string x = string.Concat(new string[]
							{
								this.path,
								"\\saves\\",
								importedElection,
								"-",
								importedJns,
								"-",
								importedCircuit,
								"-",
								importedInst,
								"-",
								importedInstJNS,
								"-",
								importedOkr
							});
							string x2 = this.savePath;
							System.IO.StreamWriter sw = new System.IO.StreamWriter(x2, false);
							sw.Write(file);
							sw.Close();
							this.savePath = x2;
							try
							{
								this.save.Load(this.savePath);
							}
							catch (XmlException)
							{
								MessageBox.Show("Nieprawidłowy XML", "Error");
							}
							int stepSave = this.isSave();
							if (stepSave != -1)
							{
								this.errorProvider1.clearErrors();
								this.protocolHeader.Controls.Clear();
								this.raportPanel.Controls.Clear();
								this.raportPanel.Visible = false;
								this.getHeader();
								this.Form1panel.Controls.Clear();
								this.range.Clear();
								this.getCalculator();
								this.wait.setWaitPanel("Trwa zerowanie nastepnych krokow protokołu", "Proszę czekać");
								this.wait.setVisible(true);
								this.protocolForm1.Enabled = true;
								this.protocolForm2.Enabled = false;
								this.protocolSummation.Enabled = false;
								this.protocolCommittee.Enabled = false;
								this.signProtocol.Enabled = false;
								this.Form1panel.Visible = true;
								this.Form2panel.Visible = false;
								this.committeePanel.Visible = false;
								this.SummationPanel.Visible = false;
								this.signPanel.Visible = false;
								this.protocolForm1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
								this.protocolForm2.BackColor = System.Drawing.SystemColors.Control;
								this.protocolSummation.BackColor = System.Drawing.SystemColors.Control;
								this.protocolCommittee.BackColor = System.Drawing.SystemColors.Control;
								this.signProtocol.BackColor = System.Drawing.SystemColors.Control;
								this.buttonNext.Visible = false;
								if (stepSave != 1)
								{
									this.Form2panel.Controls.Clear();
									if (stepSave != 2)
									{
										this.SummationPanel.Controls.Clear();
										if (stepSave != 3)
										{
											DataTable dtCommittee = new DataTable();
											this.personList.DataSource = dtCommittee;
											this.personList.Columns.Clear();
											if (this.personList.Columns["remove"] != null)
											{
												this.personList.Columns.Remove("remove");
											}
											if (this.personList.Columns["action3"] != null)
											{
												this.personList.Columns.Remove("action3");
											}
											this.personList.Refresh();
										}
									}
								}
								this.printErrors(1);
							}
							this.buttonNext.Text = "Dalej";
							this.buttonNext.Click -= new System.EventHandler(this.protocolForm2_Click);
							this.buttonNext.Click -= new System.EventHandler(this.protocolSummation_Click);
							this.buttonNext.Click -= new System.EventHandler(this.committee_Click);
							this.buttonNext.Click -= new System.EventHandler(this.committeeValid_Click);
							this.buttonNext.Click -= new System.EventHandler(this.signProtocol_Click);
							this.buttonNext.Click += new System.EventHandler(this.protocolForm2_Click);
							this.bottomPanel.Location = new System.Drawing.Point(this.Form1panel.Location.X, this.Form1panel.Location.Y + this.Form1panel.Size.Height);
							this.bottomPanel.Visible = true;
							this.buttonNext.Visible = true;
						}
					}
					else
					{
						MessageBox.Show("Nie można edytować protokołu, ponieważ licencja nie ma do niego uprawnień");
					}
				}
				catch (System.Exception)
				{
					MessageBox.Show("Edytowany plik jest nieprawidłowy.");
				}
				this.wait.setVisible(false);
				result = true;
			}
			else
			{
				if (this.currentStep == 3)
				{
					this.protocolSummation.Focus();
					this.protocolSummation.Select();
				}
				if (this.currentStep == 4)
				{
					this.protocolCommittee.Focus();
					this.protocolCommittee.Select();
				}
				if (this.currentStep == 5)
				{
					this.signProtocol.Focus();
					this.signProtocol.Select();
				}
				this.wait.setVisible(false);
				result = false;
			}
			return result;
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
			this.protocolHeader = new Panel();
			this.steps = new Panel();
			this.signProtocol = new Button();
			this.protocolCommittee = new Button();
			this.protocolSummation = new Button();
			this.protocolForm2 = new Button();
			this.protocolForm1 = new Button();
			this.panel3 = new Panel();
			this.protocolContent = new Panel();
			this.raportPanel = new Panel();
			this.signPanel = new Panel();
			this.LicencesTable = new DataGridView();
			this.SummationPanel = new Panel();
			this.bottomPanel = new Panel();
			this.errorWarningPanel = new Panel();
			this.warningPanel = new Panel();
			this.errorPanel = new Panel();
			this.buttonPanel = new Panel();
			this.buttonNext = new Button();
			this.Form2panel = new Panel();
			this.Form1panel = new Panel();
			this.committeePanel = new Panel();
			this.komisjaL3 = new Label();
			this.komisja1 = new Label();
			this.komisjaL2 = new Label();
			this.komisjaL1 = new Label();
			this.personList = new DataGridView();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.menuStrip1 = new MenuStrip();
			this.plikiToolStripMenuItem = new ToolStripMenuItem();
			this.eksportToolStripMenuItem1 = new ToolStripMenuItem();
			this.eksportujZSieciToolStripMenuItem = new ToolStripMenuItem();
			this.importujToolStripMenuItem = new ToolStripMenuItem();
			this.importujZSieciToolStripMenuItem = new ToolStripMenuItem();
			this.drukToolStripMenuItem = new ToolStripMenuItem();
			this.steps.SuspendLayout();
			this.panel3.SuspendLayout();
			this.protocolContent.SuspendLayout();
			this.signPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.LicencesTable).BeginInit();
			this.bottomPanel.SuspendLayout();
			this.buttonPanel.SuspendLayout();
			this.committeePanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.personList).BeginInit();
			this.menuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.protocolHeader.AutoSize = true;
			this.protocolHeader.Dock = DockStyle.Top;
			this.protocolHeader.Location = new System.Drawing.Point(0, 0);
			this.protocolHeader.MinimumSize = new System.Drawing.Size(0, 100);
			this.protocolHeader.Name = "protocolHeader";
			this.protocolHeader.Size = new System.Drawing.Size(784, 100);
			this.protocolHeader.TabIndex = 0;
			this.steps.Controls.Add(this.signProtocol);
			this.steps.Controls.Add(this.protocolCommittee);
			this.steps.Controls.Add(this.protocolSummation);
			this.steps.Controls.Add(this.protocolForm2);
			this.steps.Controls.Add(this.protocolForm1);
			this.steps.Dock = DockStyle.Top;
			this.steps.Location = new System.Drawing.Point(0, 24);
			this.steps.Name = "steps";
			this.steps.Size = new System.Drawing.Size(784, 54);
			this.steps.TabIndex = 1;
			this.steps.TabStop = true;
			this.signProtocol.Dock = DockStyle.Left;
			this.signProtocol.Location = new System.Drawing.Point(600, 0);
			this.signProtocol.Name = "signProtocol";
			this.signProtocol.Size = new System.Drawing.Size(164, 54);
			this.signProtocol.TabIndex = 4;
			this.signProtocol.TabStop = false;
			this.signProtocol.Text = "Podpisz \r\nprotokół";
			this.signProtocol.UseVisualStyleBackColor = true;
			this.signProtocol.Click += new System.EventHandler(this.signProtocol_Click);
			this.protocolCommittee.Dock = DockStyle.Left;
			this.protocolCommittee.Location = new System.Drawing.Point(450, 0);
			this.protocolCommittee.Name = "protocolCommittee";
			this.protocolCommittee.Size = new System.Drawing.Size(150, 54);
			this.protocolCommittee.TabIndex = 3;
			this.protocolCommittee.TabStop = false;
			this.protocolCommittee.Text = "Członkowie \r\nkomisji";
			this.protocolCommittee.UseVisualStyleBackColor = true;
			this.protocolCommittee.Click += new System.EventHandler(this.committee_Click);
			this.protocolSummation.Dock = DockStyle.Left;
			this.protocolSummation.Location = new System.Drawing.Point(300, 0);
			this.protocolSummation.Name = "protocolSummation";
			this.protocolSummation.Size = new System.Drawing.Size(150, 54);
			this.protocolSummation.TabIndex = 2;
			this.protocolSummation.TabStop = false;
			this.protocolSummation.Text = "Podsumowanie\r\nprotokołu";
			this.protocolSummation.UseVisualStyleBackColor = true;
			this.protocolSummation.Click += new System.EventHandler(this.protocolSummation_Click);
			this.protocolForm2.Dock = DockStyle.Left;
			this.protocolForm2.Location = new System.Drawing.Point(150, 0);
			this.protocolForm2.Name = "protocolForm2";
			this.protocolForm2.Size = new System.Drawing.Size(150, 54);
			this.protocolForm2.TabIndex = 1;
			this.protocolForm2.TabStop = false;
			this.protocolForm2.Text = "Wypełnij protokół\r\ndół-góra";
			this.protocolForm2.UseVisualStyleBackColor = true;
			this.protocolForm2.Click += new System.EventHandler(this.protocolForm2_Click);
			this.protocolForm1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			this.protocolForm1.Dock = DockStyle.Left;
			this.protocolForm1.Location = new System.Drawing.Point(0, 0);
			this.protocolForm1.Name = "protocolForm1";
			this.protocolForm1.Size = new System.Drawing.Size(150, 54);
			this.protocolForm1.TabIndex = 0;
			this.protocolForm1.TabStop = false;
			this.protocolForm1.Text = "Wypełnij protokół\r\ngóra-dół";
			this.protocolForm1.UseVisualStyleBackColor = false;
			this.protocolForm1.Click += new System.EventHandler(this.protocolForm1_Click);
			this.panel3.Controls.Add(this.protocolContent);
			this.panel3.Dock = DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(0, 78);
			this.panel3.MinimumSize = new System.Drawing.Size(0, 200);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(784, 572);
			this.panel3.TabIndex = 2;
			this.protocolContent.AutoScroll = true;
			this.protocolContent.AutoSize = true;
			this.protocolContent.Controls.Add(this.raportPanel);
			this.protocolContent.Controls.Add(this.signPanel);
			this.protocolContent.Controls.Add(this.SummationPanel);
			this.protocolContent.Controls.Add(this.bottomPanel);
			this.protocolContent.Controls.Add(this.Form2panel);
			this.protocolContent.Controls.Add(this.Form1panel);
			this.protocolContent.Controls.Add(this.committeePanel);
			this.protocolContent.Controls.Add(this.protocolHeader);
			this.protocolContent.Dock = DockStyle.Fill;
			this.protocolContent.Location = new System.Drawing.Point(0, 0);
			this.protocolContent.MinimumSize = new System.Drawing.Size(0, 50);
			this.protocolContent.Name = "protocolContent";
			this.protocolContent.Size = new System.Drawing.Size(784, 572);
			this.protocolContent.TabIndex = 1;
			this.raportPanel.AutoScroll = true;
			this.raportPanel.AutoSize = true;
			this.raportPanel.Location = new System.Drawing.Point(6, 197);
			this.raportPanel.Name = "raportPanel";
			this.raportPanel.Size = new System.Drawing.Size(755, 20);
			this.raportPanel.TabIndex = 8;
			this.signPanel.AutoScroll = true;
			this.signPanel.AutoSize = true;
			this.signPanel.Controls.Add(this.LicencesTable);
			this.signPanel.Location = new System.Drawing.Point(5, 338);
			this.signPanel.Name = "signPanel";
			this.signPanel.Size = new System.Drawing.Size(757, 108);
			this.signPanel.TabIndex = 7;
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
			this.SummationPanel.AutoScroll = true;
			this.SummationPanel.AutoSize = true;
			this.SummationPanel.Location = new System.Drawing.Point(6, 161);
			this.SummationPanel.Name = "SummationPanel";
			this.SummationPanel.Size = new System.Drawing.Size(757, 20);
			this.SummationPanel.TabIndex = 6;
			this.bottomPanel.AutoScroll = true;
			this.bottomPanel.AutoSize = true;
			this.bottomPanel.BackColor = System.Drawing.SystemColors.Control;
			this.bottomPanel.Controls.Add(this.errorWarningPanel);
			this.bottomPanel.Controls.Add(this.warningPanel);
			this.bottomPanel.Controls.Add(this.errorPanel);
			this.bottomPanel.Controls.Add(this.buttonPanel);
			this.bottomPanel.Location = new System.Drawing.Point(5, 452);
			this.bottomPanel.MinimumSize = new System.Drawing.Size(0, 100);
			this.bottomPanel.Name = "bottomPanel";
			this.bottomPanel.Size = new System.Drawing.Size(756, 108);
			this.bottomPanel.TabIndex = 6;
			this.errorWarningPanel.AutoScroll = true;
			this.errorWarningPanel.AutoSize = true;
			this.errorWarningPanel.Location = new System.Drawing.Point(4, 75);
			this.errorWarningPanel.MinimumSize = new System.Drawing.Size(0, 30);
			this.errorWarningPanel.Name = "errorWarningPanel";
			this.errorWarningPanel.Size = new System.Drawing.Size(635, 30);
			this.errorWarningPanel.TabIndex = 6;
			this.warningPanel.AutoScroll = true;
			this.warningPanel.AutoSize = true;
			this.warningPanel.Location = new System.Drawing.Point(4, 39);
			this.warningPanel.MinimumSize = new System.Drawing.Size(0, 30);
			this.warningPanel.Name = "warningPanel";
			this.warningPanel.Size = new System.Drawing.Size(635, 30);
			this.warningPanel.TabIndex = 5;
			this.errorPanel.AutoScroll = true;
			this.errorPanel.AutoSize = true;
			this.errorPanel.Location = new System.Drawing.Point(4, 3);
			this.errorPanel.MinimumSize = new System.Drawing.Size(0, 30);
			this.errorPanel.Name = "errorPanel";
			this.errorPanel.Size = new System.Drawing.Size(635, 30);
			this.errorPanel.TabIndex = 4;
			this.buttonPanel.AutoSize = true;
			this.buttonPanel.Controls.Add(this.buttonNext);
			this.buttonPanel.Location = new System.Drawing.Point(665, 12);
			this.buttonPanel.Name = "buttonPanel";
			this.buttonPanel.Size = new System.Drawing.Size(88, 82);
			this.buttonPanel.TabIndex = 3;
			this.buttonNext.Location = new System.Drawing.Point(7, 23);
			this.buttonNext.Name = "buttonNext";
			this.buttonNext.Size = new System.Drawing.Size(75, 23);
			this.buttonNext.TabIndex = 2;
			this.buttonNext.Text = "button1";
			this.buttonNext.UseVisualStyleBackColor = true;
			this.Form2panel.AutoScroll = true;
			this.Form2panel.AutoSize = true;
			this.Form2panel.Location = new System.Drawing.Point(5, 134);
			this.Form2panel.Name = "Form2panel";
			this.Form2panel.Size = new System.Drawing.Size(757, 21);
			this.Form2panel.TabIndex = 5;
			this.Form1panel.AutoScroll = true;
			this.Form1panel.AutoSize = true;
			this.Form1panel.Location = new System.Drawing.Point(4, 106);
			this.Form1panel.Name = "Form1panel";
			this.Form1panel.Size = new System.Drawing.Size(757, 22);
			this.Form1panel.TabIndex = 4;
			this.committeePanel.AutoSize = true;
			this.committeePanel.Controls.Add(this.komisjaL3);
			this.committeePanel.Controls.Add(this.komisja1);
			this.committeePanel.Controls.Add(this.komisjaL2);
			this.committeePanel.Controls.Add(this.komisjaL1);
			this.committeePanel.Controls.Add(this.personList);
			this.committeePanel.Location = new System.Drawing.Point(5, 234);
			this.committeePanel.Name = "committeePanel";
			this.committeePanel.Size = new System.Drawing.Size(757, 88);
			this.committeePanel.TabIndex = 3;
			this.komisjaL3.AutoSize = true;
			this.komisjaL3.Location = new System.Drawing.Point(89, 3);
			this.komisjaL3.Name = "komisjaL3";
			this.komisjaL3.Size = new System.Drawing.Size(10, 13);
			this.komisjaL3.TabIndex = 5;
			this.komisjaL3.Text = " ";
			this.komisja1.AutoSize = true;
			this.komisja1.Location = new System.Drawing.Point(7, 3);
			this.komisja1.Name = "komisja1";
			this.komisja1.Size = new System.Drawing.Size(0, 13);
			this.komisja1.TabIndex = 4;
			this.komisjaL2.AutoSize = true;
			this.komisjaL2.Location = new System.Drawing.Point(48, 3);
			this.komisjaL2.Name = "komisjaL2";
			this.komisjaL2.Size = new System.Drawing.Size(10, 13);
			this.komisjaL2.TabIndex = 3;
			this.komisjaL2.Text = " ";
			this.komisjaL1.AutoSize = true;
			this.komisjaL1.Location = new System.Drawing.Point(7, 3);
			this.komisjaL1.Name = "komisjaL1";
			this.komisjaL1.Size = new System.Drawing.Size(10, 13);
			this.komisjaL1.TabIndex = 2;
			this.komisjaL1.Text = " ";
			this.personList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
			this.personList.BorderStyle = BorderStyle.None;
			this.personList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.personList.Location = new System.Drawing.Point(7, 21);
			this.personList.Name = "personList";
			this.personList.Size = new System.Drawing.Size(240, 64);
			this.personList.TabIndex = 1;
			this.menuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.plikiToolStripMenuItem,
				this.eksportToolStripMenuItem1,
				this.eksportujZSieciToolStripMenuItem,
				this.importujToolStripMenuItem,
				this.importujZSieciToolStripMenuItem,
				this.drukToolStripMenuItem
			});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(784, 24);
			this.menuStrip1.TabIndex = 3;
			this.menuStrip1.Text = "menuStrip1";
			this.plikiToolStripMenuItem.Name = "plikiToolStripMenuItem";
			this.plikiToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
			this.plikiToolStripMenuItem.Text = "Zapisz";
			this.plikiToolStripMenuItem.Click += new System.EventHandler(this.plikiToolStripMenuItem_Click);
			this.eksportToolStripMenuItem1.Name = "eksportToolStripMenuItem1";
			this.eksportToolStripMenuItem1.Size = new System.Drawing.Size(102, 20);
			this.eksportToolStripMenuItem1.Text = "Zapisz na dysku";
			this.eksportToolStripMenuItem1.Click += new System.EventHandler(this.eksportToolStripMenuItem1_Click);
			this.eksportujZSieciToolStripMenuItem.Name = "eksportujZSieciToolStripMenuItem";
			this.eksportujZSieciToolStripMenuItem.Size = new System.Drawing.Size(103, 20);
			this.eksportujZSieciToolStripMenuItem.Text = "Wyślij na serwer";
			this.eksportujZSieciToolStripMenuItem.Click += new System.EventHandler(this.eksportujZSieciToolStripMenuItem_Click);
			this.importujToolStripMenuItem.Name = "importujToolStripMenuItem";
			this.importujToolStripMenuItem.Size = new System.Drawing.Size(102, 20);
			this.importujToolStripMenuItem.Text = "Wczytaj z dysku";
			this.importujToolStripMenuItem.Click += new System.EventHandler(this.importujToolStripMenuItem_Click);
			this.importujZSieciToolStripMenuItem.Name = "importujZSieciToolStripMenuItem";
			this.importujZSieciToolStripMenuItem.Size = new System.Drawing.Size(109, 20);
			this.importujZSieciToolStripMenuItem.Text = "Pobierz z serwera";
			this.importujZSieciToolStripMenuItem.Click += new System.EventHandler(this.importujZSieciToolStripMenuItem_Click);
			this.drukToolStripMenuItem.Name = "drukToolStripMenuItem";
			this.drukToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
			this.drukToolStripMenuItem.Text = "Drukuj";
			this.drukToolStripMenuItem.Click += new System.EventHandler(this.drukToolStripMenuItem_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.AutoScroll = true;
			base.ClientSize = new System.Drawing.Size(784, 650);
			base.Controls.Add(this.panel3);
			base.Controls.Add(this.steps);
			base.Controls.Add(this.menuStrip1);
			base.KeyPreview = true;
			base.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(690, 385);
			base.Name = "ProtocolForm";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Formularz protokołu";
			base.KeyDown += new KeyEventHandler(this.ProtocolForm_KeyDown);
			this.steps.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.protocolContent.ResumeLayout(false);
			this.protocolContent.PerformLayout();
			this.signPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.LicencesTable).EndInit();
			this.bottomPanel.ResumeLayout(false);
			this.bottomPanel.PerformLayout();
			this.buttonPanel.ResumeLayout(false);
			this.committeePanel.ResumeLayout(false);
			this.committeePanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)this.personList).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
