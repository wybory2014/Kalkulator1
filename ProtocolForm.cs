// Decompiled with JetBrains decompiler
// Type: Kalkulator1.ProtocolForm
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

using Kalkulator1.AdditionalClass;
using Kalkulator1.AdditionalWindow;
using Kalkulator1.validation;
using Microsoft.Win32;
using System;
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
using System.Windows.Forms.Layout;
using System.Xml;

namespace Kalkulator1
{
  public class ProtocolForm : Form
  {
    private string jns = "";
    private string obwod = "";
    private string inst = "";
    private string okreg = "";
    private string instJNS = "";
    private string lwyb = "";
    private string naklad = "";
    private string plusminus = "";
    private string plus = "";
    private string minus = "";
    private string lwybA = "";
    private string lwybB = "";
    private FontFamily myfont = new FontFamily("Arial");
    public string komSend = "";
    private string zORp = "";
    public string password = "";
    private string path = "";
    private string instId = "";
    private string okregId = "";
    private IContainer components = (IContainer) null;
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
    private string typeObw;
    private bool correspondence;
    private XmlDocument save;
    private string savePath;
    private int deletedCandidates;
    private List<string> controlsCanBeNull;
    private List<string> headerField;
    private string headerField2;
    private List<int[]> countcandidatesoflist;
    private Dictionary<string, ValidationRange> range;
    private List<ValidationPatern> lastValidators;
    private Dictionary<string, string> candidatesRule;
    private Dictionary<string, string> typeValidation;
    public KBWErrorProvider errorProvider1;
    private ToolTip tooltipErrors;
    private bool error;
    private int currentStep;
    private string version;
    private Control lastControl;
    public bool goodcertificate;
    public bool imported;
    public string codeWarning;
    private bool currentCommittee;
    private bool clicNext;
    public WaitPanel wait;
    private ProtocolsList form;
    private WebBrowser web;
    private string OU;
    private string codeBarText;
    private string codeBarCode;
    private string licensePath;
    private bool isKLKPro;
    private bool isKLKWali;
    private bool isKLKCan;
    private bool isKLK;
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
    private BackgroundWorker backgroundWorker1;
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
      this.path = Path.GetTempPath() + "KBW";
      this.licensePath = "";
      this.lastControl = (Control) null;
      this.controlsCanBeNull = new List<string>();
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
      this.lastValidators = new List<ValidationPatern>();
      this.countcandidatesoflist = new List<int[]>();
      this.candidatesRule = new Dictionary<string, string>();
      this.errorProvider1 = new KBWErrorProvider();
      this.error = false;
      this.range = new Dictionary<string, ValidationRange>();
      this.personList.Visible = false;
      this.committeePanel.Visible = false;
      this.currentStep = 1;
      this.goodcertificate = false;
      this.personList.CellClick += new DataGridViewCellEventHandler(this.committee_CellClick);
      this.currentCommittee = false;
      this.clicNext = false;
      this.imported = false;
      this.wait = new WaitPanel("Wait3", this.Width, this.Height);
      this.form = (ProtocolsList) null;
      this.typeValidation = new Dictionary<string, string>();
      this.codeBarText = "";
      this.codeBarCode = "";
    }

    public ProtocolForm(ProtocolsList form, XmlDocument header, string protocolDefinition, string candidates, string committee, string validateDefinition, string save, string OU, string licensePath, string version)
    {
      this.InitializeComponent();
      this.tooltipErrors = new ToolTip();
      this.isKLKCan = true;
      this.isKLK = true;
      this.isKLKPro = true;
      this.isKLKWali = true;
      this.version = version;
      this.licensePath = licensePath;
      this.path = Path.GetTempPath() + "KBW";
      this.controlsCanBeNull = new List<string>();
      this.codeWarning = "";
      this.wait = new WaitPanel("Wait3", this.Width, this.Height);
      this.Controls.Add((Control) this.wait.getPanel());
      this.Controls[this.wait.getName()].BringToFront();
      this.form = form;
      this.OU = OU;
      form.wait.setWaitPanel("Trwa otwieranie formularza protokołu", "Proszę czekać");
      form.wait.setVisible(true);
      this.error = false;
      this.range = new Dictionary<string, ValidationRange>();
      this.lastControl = (Control) null;
      this.typeObw = "";
      this.correspondence = false;
      this.errorProvider1 = new KBWErrorProvider();
      this.errorProvider1.clearErrors();
      string[] strArray1 = protocolDefinition.Split('\\');
      this.protocolDefinitionName = strArray1[strArray1.Length - 1].Replace('_', '/').Replace(".xml", "");
      string[] strArray2 = candidates.Split('\\');
      this.candidatesName = strArray2[strArray2.Length - 1].Replace('_', '/').Replace(".xml", "");
      string[] strArray3 = committee.Split('\\');
      this.committeeName = strArray3[strArray3.Length - 1].Replace('_', '/').Replace(".xml", "");
      string[] strArray4 = validateDefinition.Split('\\');
      this.validateDefinitionName = strArray4[strArray4.Length - 1].Replace('_', '/').Replace(".xml", "");
      string[] strArray5 = this.committeeName.Split('-');
      if (strArray5.Length > 2)
        this.headerName = strArray5[0] + "-" + strArray5[1];
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
          if (File.Exists(protocolDefinition))
            this.protocolDefinition.Load(protocolDefinition);
          else
            this.isKLKPro = false;
        }
        if (candidates != "")
        {
          this.candidates = new XmlDocument();
          if (File.Exists(candidates))
            this.candidates.Load(candidates);
          else
            this.isKLKCan = false;
        }
        if (committee != "")
        {
          this.committee = new XmlDocument();
          if (File.Exists(committee))
            this.committee.Load(committee);
          else
            this.isKLK = false;
        }
        if (validateDefinition != "")
        {
          this.validateDefinition = new XmlDocument();
          if (File.Exists(validateDefinition))
            this.validateDefinition.Load(validateDefinition);
          else
            this.isKLKWali = false;
        }
        if (save != "")
        {
          this.save = new XmlDocument();
          if (File.Exists(save))
            this.save.Load(save);
        }
        string[] strArray6 = this.savePath.Split('\\');
        string[] strArray7 = strArray6[strArray6.Length - 1].Split('-');
        string str1 = strArray7[1].Replace("Jns", "");
        string str2 = strArray7[3].Replace("Inst", "");
        string str3 = strArray7[2].Replace("Obw", "");
        this.instJNS = strArray7[4];
        string str4 = strArray7[5].Replace("Okr", "").Replace(".xml", "");
        string str5 = "";
        foreach (XmlNode xmlNode1 in header.SelectSingleNode("/akcja_wyborcza/jns"))
        {
          if (xmlNode1.Attributes["nr"].InnerText == str3)
          {
            foreach (XmlNode xmlNode2 in xmlNode1)
            {
              if (xmlNode2.Attributes["kod"].InnerText == str2)
              {
                foreach (XmlNode xmlNode3 in xmlNode2)
                {
                  if (xmlNode3.Attributes["nr"].InnerText == str4 && Convert.ToInt32(xmlNode2.Attributes["inst_jns"].InnerText) == Convert.ToInt32(this.instJNS))
                  {
                    str5 = xmlNode2.Attributes["organNazwa"].InnerText;
                    break;
                  }
                }
              }
            }
          }
        }
        if (str2 == "WBP" && (str1.Substring(0, 4) == "1465" && str1.Length == 6 && candidates != ""))
        {
          string str6 = candidates.Replace(str1 + "-" + str4 + ".xml", "146501-1.xml");
          this.candidates = new XmlDocument();
          if (File.Exists(str6))
          {
            this.candidates.Load(str6);
            this.isKLKCan = true;
          }
          else
            this.isKLKCan = false;
        }
        if (str2 == "RDA")
        {
          if (str1.Length < 6)
          {
            while (str1.Length < 6)
              str1 = "0" + str1;
          }
          if ((int) str1[2] == 55 || (int) str1[2] == 54)
          {
            if (str1.Substring(0, 4) == "1465" && str5 == "m.st.")
            {
              this.protocolDefinition = new XmlDocument();
              string str6 = protocolDefinition.Replace(".xml", "_M.xml");
              if (File.Exists(str6))
              {
                this.protocolDefinition.Load(str6);
                this.isKLKPro = true;
              }
              else
                this.isKLKPro = false;
              if (validateDefinition != "")
              {
                this.validateDefinition = new XmlDocument();
                string str7 = validateDefinition.Replace("_Walidacja.xml", "_M_Walidacja.xml");
                if (File.Exists(str7))
                {
                  this.validateDefinition.Load(str7);
                  this.isKLKWali = true;
                }
                else
                  this.isKLKWali = false;
              }
              if (candidates != "")
              {
                string str7 = candidates.Replace(str1 + "-" + str4 + ".xml", "146501-" + str4 + ".xml");
                this.candidates = new XmlDocument();
                if (File.Exists(str7))
                {
                  this.candidates.Load(str7);
                  this.isKLKCan = true;
                }
                else
                {
                  this.isKLK = false;
                  this.isKLKCan = false;
                }
              }
            }
            if (str1.Substring(0, 4) != "1465")
            {
              if (protocolDefinition != "")
              {
                this.protocolDefinition = new XmlDocument();
                string str6 = protocolDefinition.Replace(".xml", "_M.xml");
                if (File.Exists(str6))
                {
                  this.protocolDefinition.Load(str6);
                  this.isKLKPro = true;
                }
                else
                  this.isKLKPro = false;
              }
              if (validateDefinition != "")
              {
                this.validateDefinition = new XmlDocument();
                string str6 = validateDefinition.Replace("_Walidacja.xml", "_M_Walidacja.xml");
                if (File.Exists(str6))
                {
                  this.validateDefinition.Load(str6);
                  this.isKLKWali = true;
                }
                else
                  this.isKLKWali = false;
              }
            }
          }
          if (str1.Substring(0, 4) == "1465" && str5 == "Dzielnicy")
          {
            if (protocolDefinition != "")
            {
              this.protocolDefinition = new XmlDocument();
              string str6 = protocolDefinition.Replace(".xml", "_D.xml");
              if (File.Exists(str6))
              {
                this.protocolDefinition.Load(str6);
                this.isKLKPro = true;
              }
              else
                this.isKLKPro = false;
            }
            if (validateDefinition != "")
            {
              this.validateDefinition = new XmlDocument();
              string str6 = validateDefinition.Replace("_Walidacja.xml", "_D_Walidacja.xml");
              if (File.Exists(str6))
              {
                this.validateDefinition.Load(str6);
                this.isKLKWali = true;
              }
              else
                this.isKLKWali = false;
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
              string str6 = protocolDefinition.Replace(".xml", "_1.xml");
              if (File.Exists(str6))
              {
                this.protocolDefinition.Load(str6);
                this.isKLKPro = true;
              }
              else
                this.isKLKPro = false;
            }
            if (validateDefinition != "")
            {
              this.validateDefinition = new XmlDocument();
              string str6 = validateDefinition.Replace("_Walidacja.xml", "_1_Walidacja.xml");
              if (File.Exists(str6))
              {
                this.validateDefinition.Load(str6);
                this.isKLKWali = true;
              }
              else
                this.isKLKWali = false;
            }
          }
        }
      }
      catch (XmlException ex)
      {
        int num = (int) MessageBox.Show("Nieprawidłowy XML: " + ex.Message, "Błąd");
      }
      bool flag = false;
      if (this.isKLK && this.isKLKCan && this.isKLKPro && this.isKLKWali)
        flag = this.checkProtocol(this.save, this.savePath);
      if (this.isKLK && this.isKLKCan && this.isKLKPro && this.isKLKWali)
      {
        if (flag)
        {
          this.committeePanel.Visible = false;
          this.goodcertificate = false;
          this.countcandidatesoflist = new List<int[]>();
          this.lastValidators = new List<ValidationPatern>();
          this.candidatesRule = new Dictionary<string, string>();
          this.typeValidation = new Dictionary<string, string>();
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
          int num = (int) MessageBox.Show("Nie jesteś uprowniony do wypełniana tego protokołu.", "Uwaga");
          this.Close();
        }
      }
      else
      {
        int num = (int) MessageBox.Show("Nie pobrano wszystkich plików klk.", "Uwaga");
        this.Close();
      }
    }

    public bool canBeNull(string controlName)
    {
      for (int index = 0; index < this.controlsCanBeNull.Count; ++index)
      {
        if (this.controlsCanBeNull[index] == controlName)
          return true;
      }
      return false;
    }

    private bool saves(int step)
    {
      this.wait.setWaitPanel("Trwa zapisywanie protokołu", "Proszę czekać");
      this.wait.setVisible(true);
      try
      {
        string str1 = "<?xml version=\"1.0\"?>" + "<save>" + "<step>" + step.ToString() + "</step>" + "<header>" + "<id_intytucji>" + this.instId + "</id_intytucji>" + "<id_okregu>" + this.okregId + "</id_okregu>" + "<instJNS>" + this.instJNS + "</instJNS>";
        foreach (Control control in (ArrangedElementCollection) this.protocolHeader.Controls)
        {
          if (control is TextBox || control is MaskedTextBox)
            str1 = str1 + "<" + control.Name + ">" + control.Text + "</" + control.Name + ">";
        }
        string[] strArray = this.savePath.Split('-');
        string str2;
        string str3;
        string str4;
        if (strArray.Length == 6)
        {
          str2 = strArray[2].Replace("Obw", "");
          str3 = strArray[3].Replace("Inst", "");
          str4 = strArray[5].Replace("Okr", "").Replace(".xml", "");
        }
        else
        {
          str2 = strArray[strArray.Length - 4].Replace("Obw", "");
          str3 = strArray[strArray.Length - 3].Replace("Inst", "");
          str4 = strArray[strArray.Length - 1].Replace("Okr", "").Replace(".xml", "");
        }
        if (str3 == "WBP" || str3 == "WBP_1")
          str1 = str1 + "<nrOkregu>" + str4 + "</nrOkregu>";
        string str5 = str1 + "<KalVersion>" + ((object) this.version).ToString() + "</KalVersion>" + "<system>" + ((object) Environment.OSVersion.VersionString).ToString() + "</system>" + "<defklk>";
        XmlNode namedItem1 = this.protocolDefinition.SelectSingleNode("/protokol_info").Attributes.GetNamedItem("data_wersji");
        string str6 = "";
        if (namedItem1 != null)
          str6 = namedItem1.Value;
        string str7 = str5 + "<klk name=\"" + this.protocolDefinitionName + "\" data_wersji=\"" + str6 + "\"/>";
        XmlNode namedItem2 = this.committee.SelectSingleNode("/komisja_sklad").Attributes.GetNamedItem("data_wersji");
        string str8 = "";
        if (namedItem2 != null)
          str8 = namedItem2.Value;
        string str9 = str7 + "<klk name=\"" + this.committeeName + "\" data_wersji=\"" + str8 + "\"/>";
        XmlNode namedItem3 = this.validateDefinition.SelectSingleNode("/validate_info").Attributes.GetNamedItem("data_wersji");
        string str10 = "";
        if (namedItem3 != null)
          str10 = namedItem3.Value;
        string str11 = str9 + "<klk name=\"" + this.validateDefinitionName + "\" data_wersji=\"" + str10 + "\"/>";
        XmlNode namedItem4 = this.candidates.SelectSingleNode("/listy").Attributes.GetNamedItem("data_aktualizacji");
        string str12 = "";
        if (namedItem4 != null)
          str12 = namedItem4.Value;
        string str13 = str11 + "<klk name=\"" + this.candidatesName + "\" data_wersji=\"" + str12 + "\"/>";
        XmlNode namedItem5 = this.header.SelectSingleNode("/akcja_wyborcza").Attributes.GetNamedItem("data_aktualizacji");
        string str14 = "";
        if (namedItem5 != null)
          str14 = namedItem5.Value;
        string str15 = str13 + "<klk name=\"" + this.headerName + "\" data_wersji=\"" + str14 + "\"/>" + "</defklk>" + "</header>";
        if (step == 0 || step == 4)
        {
          string str16 = str15 + "<form>";
          foreach (Control control in (ArrangedElementCollection) this.SummationPanel.Controls)
          {
            if (control is TextBox || control is MaskedTextBox)
            {
              if (Regex.IsMatch(control.Name, "^Kandydtat"))
                str16 = str16 + "<" + control.Name + " id_kand=\"" + this.candidatesRule[control.Name] + "\">" + control.Text + "</" + control.Name + ">";
              else
                str16 = str16 + "<" + control.Name + ">" + control.Text + "</" + control.Name + ">";
            }
          }
          string str17 = str16 + "</form>" + "<komisja_sklad";
          foreach (XmlAttribute xmlAttribute in (XmlNamedNodeMap) this.committee.SelectSingleNode("/komisja_sklad").Attributes)
            str17 = str17 + " " + xmlAttribute.Name + "=\"" + xmlAttribute.Value + "\"";
          string str18 = str17 + ">";
          for (int index = 0; index < this.personList.Rows.Count - 1; ++index)
          {
            string str19 = str18 + "<osoba";
            string str20 = this.personList.Rows[index].Cells["Imię"] == null || this.personList.Rows[index].Cells["Imię"].Value == null ? str19 + " imie=\"\"" : str19 + " imie=\"" + HttpUtility.UrlEncode(this.personList.Rows[index].Cells["Imię"].Value.ToString()) + "\"";
            string str21 = this.personList.Rows[index].Cells["Drugie imię"] == null || this.personList.Rows[index].Cells["Drugie imię"].Value == null ? str20 + " imie2=\"\"" : str20 + " imie2=\"" + HttpUtility.UrlEncode(this.personList.Rows[index].Cells["Drugie imię"].Value.ToString()) + "\"";
            string str22 = this.personList.Rows[index].Cells["Nazwisko"] == null || this.personList.Rows[index].Cells["Nazwisko"].Value == null ? str21 + " nazwisko=\"\"" : str21 + " nazwisko=\"" + HttpUtility.UrlEncode(this.personList.Rows[index].Cells["Nazwisko"].Value.ToString()) + "\"";
            string str23 = this.personList.Rows[index].Cells["Funkcja"] == null || this.personList.Rows[index].Cells["Funkcja"].Value == null ? str22 + " funkcja=\"\"" : str22 + " funkcja=\"" + this.personList.Rows[index].Cells["Funkcja"].Value.ToString() + "\"";
            str18 = (this.personList.Rows[index].Cells["action3"].Value == null ? str23 + " obecny=\"False\"" : str23 + " obecny=\"" + this.personList.Rows[index].Cells["action3"].Value.ToString() + "\"") + "/>";
          }
          str15 = str18 + "</komisja_sklad>";
          if (step == 0)
            str15 = str15 + this.validateExportedXmlS(3) + "<status>podpisany</status>" + "<codeBar><code>" + this.codeBarCode + "</code><id>" + this.codeBarText + "</id></codeBar>";
          if (step == 4)
            str15 = str15 + this.validateExportedXmlS(3) + "<status>roboczy</status>";
          if (this.codeWarning != "")
            str15 = str15 + "<hardWarningCode>" + this.codeWarning + "</hardWarningCode>";
        }
        if (step == 1)
        {
          string str16 = str15 + "<step1>";
          foreach (Control control in (ArrangedElementCollection) this.Form1panel.Controls)
          {
            if (control is TextBox || control is MaskedTextBox)
            {
              if (Regex.IsMatch(control.Name, "^Kandydtat"))
                str16 = str16 + "<" + control.Name + " id_kand=\"" + this.candidatesRule[control.Name] + "\">" + control.Text + "</" + control.Name + ">";
              else
                str16 = str16 + "<" + control.Name + ">" + control.Text + "</" + control.Name + ">";
            }
          }
          str15 = str16 + "</step1>" + " <status>roboczy</status>";
        }
        if (step == 2)
        {
          string str16 = str15 + "<step1>";
          foreach (Control control in (ArrangedElementCollection) this.Form1panel.Controls)
          {
            if (control is TextBox || control is MaskedTextBox)
            {
              if (Regex.IsMatch(control.Name, "^Kandydtat"))
                str16 = str16 + "<" + control.Name + " id_kand=\"" + this.candidatesRule[control.Name] + "\">" + control.Text + "</" + control.Name + ">";
              else
                str16 = str16 + "<" + control.Name + ">" + control.Text + "</" + control.Name + ">";
            }
          }
          string str17 = str16 + "</step1>" + "<step2>";
          foreach (Control control in (ArrangedElementCollection) this.Form2panel.Controls)
          {
            if (control is TextBox || control is MaskedTextBox)
            {
              if (Regex.IsMatch(control.Name, "^Kandydtat"))
                str17 = str17 + "<" + control.Name + " id_kand=\"" + this.candidatesRule[control.Name] + "\" >" + control.Text + "</" + control.Name + ">";
              else
                str17 = str17 + "<" + control.Name + ">" + control.Text + "</" + control.Name + ">";
            }
          }
          str15 = str17 + "</step2>" + "<status>roboczy</status>";
        }
        if (step == 3)
        {
          string str16 = str15 + "<step1>";
          foreach (Control control in (ArrangedElementCollection) this.Form1panel.Controls)
          {
            if (control is TextBox || control is MaskedTextBox)
            {
              if (Regex.IsMatch(control.Name, "^Kandydtat"))
                str16 = str16 + "<" + control.Name + " id_kand=\"" + this.candidatesRule[control.Name] + "\">" + control.Text + "</" + control.Name + ">";
              else
                str16 = str16 + "<" + control.Name + ">" + control.Text + "</" + control.Name + ">";
            }
          }
          string str17 = str16 + "</step1>" + "<step2>";
          foreach (Control control in (ArrangedElementCollection) this.Form2panel.Controls)
          {
            if (control is TextBox || control is MaskedTextBox)
            {
              if (Regex.IsMatch(control.Name, "^Kandydtat"))
                str17 = str17 + "<" + control.Name + " id_kand=\"" + this.candidatesRule[control.Name] + "\" >" + control.Text + "</" + control.Name + ">";
              else
                str17 = str17 + "<" + control.Name + ">" + control.Text + "</" + control.Name + ">";
            }
          }
          string str18 = str17 + "</step2>" + "<step3>";
          foreach (Control control in (ArrangedElementCollection) this.SummationPanel.Controls)
          {
            if (control is TextBox || control is MaskedTextBox)
            {
              if (Regex.IsMatch(control.Name, "^Kandydtat"))
                str18 = str18 + "<" + control.Name + " id_kand=\"" + this.candidatesRule[control.Name] + "\" >" + control.Text + "</" + control.Name + ">";
              else
                str18 = str18 + "<" + control.Name + ">" + control.Text + "</" + control.Name + ">";
            }
          }
          str15 = str18 + "</step3>" + this.validateExportedXmlS(3) + "<status>roboczy</status>";
          if (this.codeWarning != "")
            str15 = str15 + "<hardWarningCode>" + this.codeWarning + "</hardWarningCode>";
        }
        if (step == 0 || step == 4 || step == 3)
        {
          string str16 = str15 + "<report>";
          foreach (Control control in (ArrangedElementCollection) this.raportPanel.Controls)
          {
            if (control is TextBox || control is MaskedTextBox)
              str16 = str16 + "<" + control.Name + ">" + control.Text + "</" + control.Name + ">";
          }
          str15 = str16 + "</report>";
        }
        string str24 = str15 + "</save>";
        StreamWriter streamWriter = new StreamWriter(this.savePath, false);
        streamWriter.Write(str24);
        streamWriter.Close();
        this.wait.setVisible(false);
        return true;
      }
      catch (IOException ex)
      {
        this.wait.setVisible(false);
        int num = (int) MessageBox.Show("Nie mozna zapisac pliku. Orginal Error: " + ex.Message, "Error");
        return false;
      }
      catch (Exception ex)
      {
        this.wait.setVisible(false);
        int num = (int) MessageBox.Show("Nie mozna zapisac pliku. Orginal Error: " + ex.Message, "Error");
        return false;
      }
    }

    private string generateSaves(int step)
    {
      this.wait.setWaitPanel("Trwa zapisywanie protokołu", "Proszę czekać");
      this.wait.setVisible(true);
      string str1 = "";
      try
      {
        str1 = "<?xml version=\"1.0\"?>";
        str1 = str1 + "<save>";
        str1 = str1 + "<step>" + step.ToString() + "</step>";
        str1 = str1 + "<header>";
        str1 = str1 + "<id_intytucji>" + this.instId + "</id_intytucji>";
        str1 = str1 + "<id_okregu>" + this.okregId + "</id_okregu>";
        str1 = str1 + "<instJNS>" + this.instJNS + "</instJNS>";
        foreach (Control control in (ArrangedElementCollection) this.protocolHeader.Controls)
        {
          if (control is TextBox || control is MaskedTextBox)
            str1 = str1 + "<" + control.Name + ">" + control.Text + "</" + control.Name + ">";
        }
        string[] strArray = this.savePath.Split('-');
        string str2;
        string str3;
        string str4;
        if (strArray.Length == 6)
        {
          str2 = strArray[2].Replace("Obw", "");
          str3 = strArray[3].Replace("Inst", "");
          str4 = strArray[5].Replace("Okr", "").Replace(".xml", "");
        }
        else
        {
          str2 = strArray[strArray.Length - 4].Replace("Obw", "");
          str3 = strArray[strArray.Length - 3].Replace("Inst", "");
          str4 = strArray[strArray.Length - 1].Replace("Okr", "").Replace(".xml", "");
        }
        if (str3 == "WBP" || str3 == "WBP_1")
          str1 = str1 + "<nrOkregu>" + str4 + "</nrOkregu>";
        str1 = str1 + "<KalVersion>" + ((object) this.version).ToString() + "</KalVersion>";
        str1 = str1 + "<system>" + ((object) Environment.OSVersion.VersionString).ToString() + "</system>";
        str1 = str1 + "<defklk>";
        XmlNode namedItem1 = this.protocolDefinition.SelectSingleNode("/protokol_info").Attributes.GetNamedItem("data_wersji");
        string str5 = "";
        if (namedItem1 != null)
          str5 = namedItem1.Value;
        str1 = str1 + "<klk name=\"" + this.protocolDefinitionName + "\" data_wersji=\"" + str5 + "\"/>";
        XmlNode namedItem2 = this.committee.SelectSingleNode("/komisja_sklad").Attributes.GetNamedItem("data_wersji");
        string str6 = "";
        if (namedItem2 != null)
          str6 = namedItem2.Value;
        str1 = str1 + "<klk name=\"" + this.committeeName + "\" data_wersji=\"" + str6 + "\"/>";
        XmlNode namedItem3 = this.validateDefinition.SelectSingleNode("/validate_info").Attributes.GetNamedItem("data_wersji");
        string str7 = "";
        if (namedItem3 != null)
          str7 = namedItem3.Value;
        str1 = str1 + "<klk name=\"" + this.validateDefinitionName + "\" data_wersji=\"" + str7 + "\"/>";
        XmlNode namedItem4 = this.candidates.SelectSingleNode("/listy").Attributes.GetNamedItem("data_aktualizacji");
        string str8 = "";
        if (namedItem4 != null)
          str8 = namedItem4.Value;
        str1 = str1 + "<klk name=\"" + this.candidatesName + "\" data_wersji=\"" + str8 + "\"/>";
        XmlNode namedItem5 = this.header.SelectSingleNode("/akcja_wyborcza").Attributes.GetNamedItem("data_aktualizacji");
        string str9 = "";
        if (namedItem5 != null)
          str9 = namedItem5.Value;
        str1 = str1 + "<klk name=\"" + this.headerName + "\" data_wersji=\"" + str9 + "\"/>";
        str1 = str1 + "</defklk>";
        str1 = str1 + "</header>";
        if (step == 0 || step == 4)
        {
          str1 = str1 + "<form>";
          foreach (Control control in (ArrangedElementCollection) this.SummationPanel.Controls)
          {
            if (control is TextBox || control is MaskedTextBox)
            {
              if (Regex.IsMatch(control.Name, "^Kandydtat"))
                str1 = str1 + "<" + control.Name + " id_kand=\"" + this.candidatesRule[control.Name] + "\">" + control.Text + "</" + control.Name + ">";
              else
                str1 = str1 + "<" + control.Name + ">" + control.Text + "</" + control.Name + ">";
            }
          }
          str1 = str1 + "</form>";
          str1 = str1 + "<komisja_sklad";
          foreach (XmlAttribute xmlAttribute in (XmlNamedNodeMap) this.committee.SelectSingleNode("/komisja_sklad").Attributes)
            str1 = str1 + " " + xmlAttribute.Name + "=\"" + xmlAttribute.Value + "\"";
          str1 = str1 + ">";
          for (int index = 0; index < this.personList.Rows.Count - 1; ++index)
          {
            str1 = str1 + "<osoba";
            str1 = this.personList.Rows[index].Cells["Imię"] == null || this.personList.Rows[index].Cells["Imię"].Value == null ? str1 + " imie=\"\"" : str1 + " imie=\"" + HttpUtility.UrlEncode(this.personList.Rows[index].Cells["Imię"].Value.ToString()) + "\"";
            str1 = this.personList.Rows[index].Cells["Drugie imię"] == null || this.personList.Rows[index].Cells["Drugie imię"].Value == null ? str1 + " imie2=\"\"" : str1 + " imie2=\"" + HttpUtility.UrlEncode(this.personList.Rows[index].Cells["Drugie imię"].Value.ToString()) + "\"";
            str1 = this.personList.Rows[index].Cells["Nazwisko"] == null || this.personList.Rows[index].Cells["Nazwisko"].Value == null ? str1 + " nazwisko=\"\"" : str1 + " nazwisko=\"" + HttpUtility.UrlEncode(this.personList.Rows[index].Cells["Nazwisko"].Value.ToString()) + "\"";
            str1 = this.personList.Rows[index].Cells["Funkcja"] == null || this.personList.Rows[index].Cells["Funkcja"].Value == null ? str1 + " funkcja=\"\"" : str1 + " funkcja=\"" + this.personList.Rows[index].Cells["Funkcja"].Value.ToString() + "\"";
            str1 = this.personList.Rows[index].Cells["action3"].Value == null ? str1 + " obecny=\"False\"" : str1 + " obecny=\"" + this.personList.Rows[index].Cells["action3"].Value.ToString() + "\"";
            str1 = str1 + "/>";
          }
          str1 = str1 + "</komisja_sklad>";
          if (step == 0)
          {
            str1 = str1 + this.validateExportedXmlS(3);
            str1 = str1 + "<status>podpisany</status>";
            str1 = str1 + "<codeBar><code>" + this.codeBarCode + "</code><id>" + this.codeBarText + "</id></codeBar>";
          }
          if (step == 4)
          {
            str1 = str1 + this.validateExportedXmlS(3);
            str1 = str1 + "<status>roboczy</status>";
          }
          if (this.codeWarning != "")
            str1 = str1 + "<hardWarningCode>" + this.codeWarning + "</hardWarningCode>";
        }
        if (step == 1)
        {
          str1 = str1 + "<step1>";
          foreach (Control control in (ArrangedElementCollection) this.Form1panel.Controls)
          {
            if (control is TextBox || control is MaskedTextBox)
            {
              if (Regex.IsMatch(control.Name, "^Kandydtat"))
                str1 = str1 + "<" + control.Name + " id_kand=\"" + this.candidatesRule[control.Name] + "\">" + control.Text + "</" + control.Name + ">";
              else
                str1 = str1 + "<" + control.Name + ">" + control.Text + "</" + control.Name + ">";
            }
          }
          str1 = str1 + "</step1>";
          str1 = str1 + " <status>roboczy</status>";
        }
        if (step == 2)
        {
          str1 = str1 + "<step1>";
          foreach (Control control in (ArrangedElementCollection) this.Form1panel.Controls)
          {
            if (control is TextBox || control is MaskedTextBox)
            {
              if (Regex.IsMatch(control.Name, "^Kandydtat"))
                str1 = str1 + "<" + control.Name + " id_kand=\"" + this.candidatesRule[control.Name] + "\">" + control.Text + "</" + control.Name + ">";
              else
                str1 = str1 + "<" + control.Name + ">" + control.Text + "</" + control.Name + ">";
            }
          }
          str1 = str1 + "</step1>";
          str1 = str1 + "<step2>";
          foreach (Control control in (ArrangedElementCollection) this.Form2panel.Controls)
          {
            if (control is TextBox || control is MaskedTextBox)
            {
              if (Regex.IsMatch(control.Name, "^Kandydtat"))
                str1 = str1 + "<" + control.Name + " id_kand=\"" + this.candidatesRule[control.Name] + "\" >" + control.Text + "</" + control.Name + ">";
              else
                str1 = str1 + "<" + control.Name + ">" + control.Text + "</" + control.Name + ">";
            }
          }
          str1 = str1 + "</step2>";
          str1 = str1 + "<status>roboczy</status>";
        }
        if (step == 3)
        {
          str1 = str1 + "<step1>";
          foreach (Control control in (ArrangedElementCollection) this.Form1panel.Controls)
          {
            if (control is TextBox || control is MaskedTextBox)
            {
              if (Regex.IsMatch(control.Name, "^Kandydtat"))
                str1 = str1 + "<" + control.Name + " id_kand=\"" + this.candidatesRule[control.Name] + "\">" + control.Text + "</" + control.Name + ">";
              else
                str1 = str1 + "<" + control.Name + ">" + control.Text + "</" + control.Name + ">";
            }
          }
          str1 = str1 + "</step1>";
          str1 = str1 + "<step2>";
          foreach (Control control in (ArrangedElementCollection) this.Form2panel.Controls)
          {
            if (control is TextBox || control is MaskedTextBox)
            {
              if (Regex.IsMatch(control.Name, "^Kandydtat"))
                str1 = str1 + "<" + control.Name + " id_kand=\"" + this.candidatesRule[control.Name] + "\" >" + control.Text + "</" + control.Name + ">";
              else
                str1 = str1 + "<" + control.Name + ">" + control.Text + "</" + control.Name + ">";
            }
          }
          str1 = str1 + "</step2>";
          str1 = str1 + "<step3>";
          foreach (Control control in (ArrangedElementCollection) this.SummationPanel.Controls)
          {
            if (control is TextBox || control is MaskedTextBox)
            {
              if (Regex.IsMatch(control.Name, "^Kandydtat"))
                str1 = str1 + "<" + control.Name + " id_kand=\"" + this.candidatesRule[control.Name] + "\" >" + control.Text + "</" + control.Name + ">";
              else
                str1 = str1 + "<" + control.Name + ">" + control.Text + "</" + control.Name + ">";
            }
          }
          str1 = str1 + "</step3>";
          str1 = str1 + this.validateExportedXmlS(3);
          str1 = str1 + "<status>roboczy</status>";
          if (this.codeWarning != "")
            str1 = str1 + "<hardWarningCode>" + this.codeWarning + "</hardWarningCode>";
        }
        if (step == 0 || step == 4 || step == 3)
        {
          str1 = str1 + "<report>";
          foreach (Control control in (ArrangedElementCollection) this.raportPanel.Controls)
          {
            if (control is TextBox || control is MaskedTextBox)
              str1 = str1 + "<" + control.Name + ">" + control.Text + "</" + control.Name + ">";
          }
          str1 = str1 + "</report>";
        }
        str1 = str1 + "</save>";
      }
      catch (IOException ex)
      {
        this.wait.setVisible(false);
        int num = (int) MessageBox.Show("Nie mozna zapisac pliku. Orginal Error: " + ex.Message, "Error");
      }
      catch (Exception ex)
      {
        this.wait.setVisible(false);
        int num = (int) MessageBox.Show("Nie mozna zapisac pliku. Orginal Error: " + ex.Message, "Error");
      }
      this.wait.setVisible(false);
      return str1;
    }

    private void saves(int step, string errors)
    {
      this.wait.setWaitPanel("Trwa zapisywanie protokołu", "Proszę czekać");
      this.wait.setVisible(true);
      try
      {
        string str1 = "<?xml version=\"1.0\"?>" + "<save>" + "<step>" + step.ToString() + "</step>" + "<header>" + "<id_intytucji>" + this.instId + "</id_intytucji>" + "<id_okregu>" + this.okregId + "</id_okregu>" + "<instJNS>" + this.instJNS + "</instJNS>";
        foreach (Control control in (ArrangedElementCollection) this.protocolHeader.Controls)
        {
          if (control is TextBox || control is MaskedTextBox)
            str1 = str1 + "<" + control.Name + ">" + control.Text + "</" + control.Name + ">";
        }
        string[] strArray = this.savePath.Split('-');
        string str2;
        string str3;
        string str4;
        if (strArray.Length == 6)
        {
          str2 = strArray[2].Replace("Obw", "");
          str3 = strArray[3].Replace("Inst", "");
          str4 = strArray[5].Replace("Okr", "").Replace(".xml", "");
        }
        else
        {
          str2 = strArray[strArray.Length - 4].Replace("Obw", "");
          str3 = strArray[strArray.Length - 3].Replace("Inst", "");
          str4 = strArray[strArray.Length - 1].Replace("Okr", "").Replace(".xml", "");
        }
        if (str3 == "WBP" || str3 == "WBP_1")
          str1 = str1 + "<nrOkregu>" + str4 + "</nrOkregu>";
        string str5 = str1 + "<KalVersion>" + ((object) this.version).ToString() + "</KalVersion>" + "<system>" + ((object) Environment.OSVersion.VersionString).ToString() + "</system>" + "<defklk>";
        XmlNode namedItem1 = this.protocolDefinition.SelectSingleNode("/protokol_info").Attributes.GetNamedItem("data_wersji");
        string str6 = "";
        if (namedItem1 != null)
          str6 = namedItem1.Value;
        string str7 = str5 + "<klk name=\"" + this.protocolDefinitionName + "\" data_wersji=\"" + str6 + "\"/>";
        XmlNode namedItem2 = this.committee.SelectSingleNode("/komisja_sklad").Attributes.GetNamedItem("data_wersji");
        string str8 = "";
        if (namedItem2 != null)
          str8 = namedItem2.Value;
        string str9 = str7 + "<klk name=\"" + this.committeeName + "\" data_wersji=\"" + str8 + "\"/>";
        XmlNode namedItem3 = this.validateDefinition.SelectSingleNode("/validate_info").Attributes.GetNamedItem("data_wersji");
        string str10 = "";
        if (namedItem3 != null)
          str10 = namedItem3.Value;
        string str11 = str9 + "<klk name=\"" + this.validateDefinitionName + "\" data_wersji=\"" + str10 + "\"/>";
        XmlNode namedItem4 = this.candidates.SelectSingleNode("/listy").Attributes.GetNamedItem("data_aktualizacji");
        string str12 = "";
        if (namedItem4 != null)
          str12 = namedItem4.Value;
        string str13 = str11 + "<klk name=\"" + this.candidatesName + "\" data_wersji=\"" + str12 + "\"/>";
        XmlNode namedItem5 = this.header.SelectSingleNode("/akcja_wyborcza").Attributes.GetNamedItem("data-ost-aktualizacji");
        string str14 = "";
        if (namedItem5 != null)
          str14 = namedItem5.Value;
        string str15 = str13 + "<klk name=\"" + this.headerName + "\" data_wersji=\"" + str14 + "\"/>" + "</defklk>" + "</header>";
        if (step == 0 || step == 4)
        {
          string str16 = str15 + "<form>";
          foreach (Control control in (ArrangedElementCollection) this.SummationPanel.Controls)
          {
            if (control is TextBox || control is MaskedTextBox)
            {
              if (Regex.IsMatch(control.Name, "^Kandydtat"))
                str16 = str16 + "<" + control.Name + " id_kand=\"" + this.candidatesRule[control.Name] + "\">" + control.Text + "</" + control.Name + ">";
              else
                str16 = str16 + "<" + control.Name + ">" + control.Text + "</" + control.Name + ">";
            }
          }
          string str17 = str16 + "</form>" + "<komisja_sklad";
          foreach (XmlAttribute xmlAttribute in (XmlNamedNodeMap) this.committee.SelectSingleNode("/komisja_sklad").Attributes)
            str17 = str17 + " " + xmlAttribute.Name + "=\"" + xmlAttribute.Value + "\"";
          string str18 = str17 + ">";
          for (int index = 0; index < this.personList.Rows.Count - 1; ++index)
          {
            string str19 = str18 + "<osoba";
            string str20 = this.personList.Rows[index].Cells["Imię"] == null || this.personList.Rows[index].Cells["Imię"].Value == null ? str19 + " imie=\"\"" : str19 + " imie=\"" + HttpUtility.UrlEncode(this.personList.Rows[index].Cells["Imię"].Value.ToString()) + "\"";
            string str21 = this.personList.Rows[index].Cells["Drugie imię"] == null || this.personList.Rows[index].Cells["Drugie imię"].Value == null ? str20 + " imie2=\"\"" : str20 + " imie2=\"" + HttpUtility.UrlEncode(this.personList.Rows[index].Cells["Drugie imię"].Value.ToString()) + "\"";
            string str22 = this.personList.Rows[index].Cells["Nazwisko"] == null || this.personList.Rows[index].Cells["Nazwisko"].Value == null ? str21 + " nazwisko=\"\"" : str21 + " nazwisko=\"" + HttpUtility.UrlEncode(this.personList.Rows[index].Cells["Nazwisko"].Value.ToString()) + "\"";
            string str23 = this.personList.Rows[index].Cells["Funkcja"] == null || this.personList.Rows[index].Cells["Funkcja"].Value == null ? str22 + " funkcja=\"\"" : str22 + " funkcja=\"" + this.personList.Rows[index].Cells["Funkcja"].Value.ToString() + "\"";
            str18 = (this.personList.Rows[index].Cells["action3"].Value == null ? str23 + " obecny=\"False\"" : str23 + " obecny=\"" + this.personList.Rows[index].Cells["action3"].Value.ToString() + "\"") + "/>";
          }
          str15 = str18 + "</komisja_sklad>";
          if (step == 0)
            str15 = str15 + "<status>podpisany</status>" + "<codeBar><code>" + this.codeBarCode + "</code><id>" + this.codeBarText + "</id></codeBar>";
          if (step == 4)
            str15 = str15 + "<status>roboczy</status>";
          if (this.codeWarning != "")
            str15 = str15 + "<hardWarningCode>" + this.codeWarning + "</hardWarningCode>";
        }
        if (step == 1)
        {
          string str16 = str15 + "<step1>";
          foreach (Control control in (ArrangedElementCollection) this.Form1panel.Controls)
          {
            if (control is TextBox || control is MaskedTextBox)
            {
              if (Regex.IsMatch(control.Name, "^Kandydtat"))
                str16 = str16 + "<" + control.Name + " id_kand=\"" + this.candidatesRule[control.Name] + "\">" + control.Text + "</" + control.Name + ">";
              else
                str16 = str16 + "<" + control.Name + ">" + control.Text + "</" + control.Name + ">";
            }
          }
          str15 = str16 + "</step1>" + " <status>roboczy</status>";
        }
        if (step == 2)
        {
          string str16 = str15 + "<step1>";
          foreach (Control control in (ArrangedElementCollection) this.Form1panel.Controls)
          {
            if (control is TextBox || control is MaskedTextBox)
            {
              if (Regex.IsMatch(control.Name, "^Kandydtat"))
                str16 = str16 + "<" + control.Name + " id_kand=\"" + this.candidatesRule[control.Name] + "\">" + control.Text + "</" + control.Name + ">";
              else
                str16 = str16 + "<" + control.Name + ">" + control.Text + "</" + control.Name + ">";
            }
          }
          string str17 = str16 + "</step1>" + "<step2>";
          foreach (Control control in (ArrangedElementCollection) this.Form2panel.Controls)
          {
            if (control is TextBox || control is MaskedTextBox)
            {
              if (Regex.IsMatch(control.Name, "^Kandydtat"))
                str17 = str17 + "<" + control.Name + " id_kand=\"" + this.candidatesRule[control.Name] + "\" >" + control.Text + "</" + control.Name + ">";
              else
                str17 = str17 + "<" + control.Name + ">" + control.Text + "</" + control.Name + ">";
            }
          }
          str15 = str17 + "</step2>" + "<status>roboczy</status>";
        }
        if (step == 3)
        {
          string str16 = str15 + "<step1>";
          foreach (Control control in (ArrangedElementCollection) this.Form1panel.Controls)
          {
            if (control is TextBox || control is MaskedTextBox)
            {
              if (Regex.IsMatch(control.Name, "^Kandydtat"))
                str16 = str16 + "<" + control.Name + " id_kand=\"" + this.candidatesRule[control.Name] + "\">" + control.Text + "</" + control.Name + ">";
              else
                str16 = str16 + "<" + control.Name + ">" + control.Text + "</" + control.Name + ">";
            }
          }
          string str17 = str16 + "</step1>" + "<step2>";
          foreach (Control control in (ArrangedElementCollection) this.Form2panel.Controls)
          {
            if (control is TextBox || control is MaskedTextBox)
            {
              if (Regex.IsMatch(control.Name, "^Kandydtat"))
                str17 = str17 + "<" + control.Name + " id_kand=\"" + this.candidatesRule[control.Name] + "\" >" + control.Text + "</" + control.Name + ">";
              else
                str17 = str17 + "<" + control.Name + ">" + control.Text + "</" + control.Name + ">";
            }
          }
          string str18 = str17 + "</step2>" + "<step3>";
          foreach (Control control in (ArrangedElementCollection) this.SummationPanel.Controls)
          {
            if (control is TextBox || control is MaskedTextBox)
            {
              if (Regex.IsMatch(control.Name, "^Kandydtat"))
                str18 = str18 + "<" + control.Name + " id_kand=\"" + this.candidatesRule[control.Name] + "\" >" + control.Text + "</" + control.Name + ">";
              else
                str18 = str18 + "<" + control.Name + ">" + control.Text + "</" + control.Name + ">";
            }
          }
          str15 = str18 + "</step3>" + "<status>roboczy</status>";
          if (this.codeWarning != "")
            str15 = str15 + "<hardWarningCode>" + this.codeWarning + "</hardWarningCode>";
        }
        if (step == 0 || step == 4 || step == 3)
        {
          string str16 = str15 + "<report>";
          foreach (Control control in (ArrangedElementCollection) this.raportPanel.Controls)
          {
            if (control is TextBox || control is MaskedTextBox)
              str16 = str16 + "<" + control.Name + ">" + control.Text + "</" + control.Name + ">";
          }
          str15 = str16 + "</report>";
        }
        string str24 = str15 + errors + "</save>";
        StreamWriter streamWriter = new StreamWriter(this.savePath, false);
        streamWriter.Write(str24);
        streamWriter.Close();
      }
      catch (IOException ex)
      {
        this.wait.setVisible(false);
        int num = (int) MessageBox.Show("Nie mozna zapisac pliku. Orginal Error: " + ex.Message, "Error");
      }
      catch (Exception ex)
      {
        this.wait.setVisible(false);
        int num = (int) MessageBox.Show("Nie mozna zapisac pliku. Orginal Error: " + ex.Message, "Error");
      }
      this.wait.setVisible(false);
    }

    private int isSave()
    {
      XmlNode xmlNode = this.save.SelectSingleNode("/save/step");
      if (xmlNode != null)
        return Convert.ToInt32(xmlNode.InnerText);
      else
        return -1;
    }

    private void ProtocolForm_KeyDown(object sender, KeyEventArgs e)
    {
      Control control = (Control) sender;
      if (!(this.protocolForm2.BackColor == SystemColors.GradientInactiveCaption))
      {
        if (!(this.ActiveControl.Text != ""))
          return;
        if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Return)
          control.SelectNextControl(this.ActiveControl, true, true, true, true);
        else if (e.KeyCode == Keys.Up)
          control.SelectNextControl(this.ActiveControl, false, true, true, true);
      }
      else if (this.ActiveControl.Text != "")
      {
        if (e.KeyCode == Keys.Down)
          control.SelectNextControl(this.ActiveControl, false, true, true, true);
        else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Return)
          control.SelectNextControl(this.ActiveControl, true, true, true, true);
      }
    }

    private void LostFocus(object sender, EventArgs e)
    {
      this.lastControl = sender as Control;
      if (!((sender as Control).Text == ""))
        return;
      this.ActiveControl = this.lastControl;
    }

    private void Control_SHead_MouseClick(object sender, MouseEventArgs e)
    {
      if (this.currentStep == 1 && this.protocolForm1.BackColor == SystemColors.GradientInactiveCaption && !this.clicNext)
        this.lastControl.Select();
      if (this.currentStep == 2 && this.protocolForm2.BackColor == SystemColors.GradientInactiveCaption && !this.clicNext)
        this.lastControl.Select();
      if (this.currentStep != 3 || !(this.protocolSummation.BackColor == SystemColors.GradientInactiveCaption) || this.clicNext)
        return;
      this.lastControl.Select();
    }

    private void Control_S1_MouseClick(object sender, MouseEventArgs e)
    {
      if (this.currentStep != 1 || this.clicNext)
        return;
      this.lastControl.Select();
    }

    private void Control_S2_MouseClick(object sender, MouseEventArgs e)
    {
      if (this.currentStep != 2 || this.clicNext)
        return;
      this.lastControl.Select();
    }

    private void Control_S3_MouseClick(object sender, MouseEventArgs e)
    {
      if (this.currentStep != 3 || this.clicNext)
        return;
      this.lastControl.Select();
    }

    private void getHeader()
    {
      this.wait.setWaitPanel("Trwa ładowanie nagłówka protokołu", "Proszę czekać");
      this.wait.setVisible(true);
      this.headerField = new List<string>();
      string str = "Protokół dla ";
      try
      {
        XmlNode xmlNode1 = this.protocolDefinition.SelectSingleNode("/protokol_info");
        XmlNode xmlNode2 = this.header.SelectSingleNode("/akcja_wyborcza/jns");
        int y = 0;
        int width1 = this.protocolHeader.Size.Width - 20;
        XmlNode namedItem1 = this.header.SelectSingleNode("/akcja_wyborcza").Attributes.GetNamedItem("data-ost-aktualizacji");
        string[] strArray1 = this.savePath.Split('\\');
        string[] strArray2 = strArray1[strArray1.Length - 1].Split('-');
        this.jns = strArray2[1].Replace("Jns", "");
        this.obwod = strArray2[2].Replace("Obw", "");
        this.inst = strArray2[3].Replace("Inst", "");
        this.okreg = strArray2[5].Replace("Okr", "");
        this.okreg = this.okreg.Split(' ')[0].Replace(".xml", "");
        this.Text = str + this.jns + " " + this.inst + ": obwod-" + this.obwod + ", okręg-" + this.okreg + " (" + ((object) Kalkulator1.instalClass.Version.getVersion()).ToString() + ")";
        foreach (XmlNode xmlNode3 in xmlNode2)
        {
          if (xmlNode3.Attributes["nr"].InnerText == this.obwod)
          {
            foreach (XmlNode xmlNode4 in xmlNode3)
            {
              if (xmlNode4.Attributes["kod"].InnerText == this.inst)
              {
                this.instId = xmlNode4.Attributes["id"].InnerText;
                foreach (XmlNode xmlNode5 in xmlNode4)
                {
                  if (xmlNode5.Attributes["nr"].InnerText == this.okreg)
                    this.okregId = xmlNode5.Attributes["id"].InnerText;
                }
              }
            }
          }
        }
        foreach (XmlNode xmlNode3 in xmlNode1)
        {
          if (xmlNode3.Attributes.GetNamedItem("type").Value == "header")
          {
            foreach (XmlNode xmlNode4 in xmlNode3)
            {
              foreach (XmlNode xmlNode5 in xmlNode4)
              {
                if (xmlNode5.Name == "title")
                {
                  int x = 0;
                  XmlNode namedItem2 = xmlNode5.Attributes.GetNamedItem("bold");
                  Label label = new Label();
                  label.Text = xmlNode5.InnerText;
                  label.AutoSize = true;
                  label.MaximumSize = new Size(width1, 0);
                  label.Font = new Font(this.myfont, 10f);
                  label.Padding = new Padding(10, 0, 10, 0);
                  if (namedItem2.Value == "true")
                    label.Font = new Font(this.myfont, 10f, FontStyle.Bold);
                  label.Location = new Point(x, y);
                  this.protocolHeader.Controls.Add((Control) label);
                  y += label.Height + 30;
                }
                if (xmlNode5.Name == "row")
                {
                  int x1 = 0;
                  XmlNode namedItem2 = xmlNode5.Attributes.GetNamedItem("scale_count");
                  int num1 = xmlNode5.ChildNodes.Count;
                  if (namedItem2 != null)
                  {
                    try
                    {
                      int num2 = Convert.ToInt32(namedItem2.Value);
                      if (num2 > num1)
                        num1 = num2;
                    }
                    catch (Exception ex)
                    {
                    }
                  }
                  int num3 = Convert.ToInt32(width1 / num1);
                  int num4 = 85;
                  int num5 = 20;
                  for (int index = 0; index < xmlNode5.ChildNodes.Count; ++index)
                  {
                    XmlNode namedItem3 = xmlNode5.ChildNodes[index].Attributes.GetNamedItem("name");
                    XmlNode namedItem4 = xmlNode5.ChildNodes[index].Attributes.GetNamedItem("valueName");
                    XmlNode namedItem5 = xmlNode5.ChildNodes[index].Attributes.GetNamedItem("save_as");
                    XmlNode namedItem6 = xmlNode5.ChildNodes[index].Attributes.GetNamedItem("valueType");
                    XmlNode namedItem7 = xmlNode5.ChildNodes[index].Attributes.GetNamedItem("valueDefault");
                    XmlNode namedItem8 = xmlNode5.ChildNodes[index].Attributes.GetNamedItem("enable");
                    XmlNode namedItem9 = xmlNode5.ChildNodes[index].Attributes.GetNamedItem("scale");
                    int width2 = num3;
                    int width3 = num4;
                    int num2 = 1;
                    if (namedItem9 != null)
                    {
                      try
                      {
                        num2 = Convert.ToInt32(namedItem9.Value);
                      }
                      catch (Exception ex)
                      {
                      }
                    }
                    if (num2 > 1)
                    {
                      width2 = num3 * num2;
                      width3 = num4 * num2;
                    }
                    Label label = new Label();
                    label.Name = "Lab_" + namedItem4.Value;
                    label.Text = namedItem3.Value;
                    label.AutoSize = true;
                    label.MaximumSize = new Size(width2 - width3, 0);
                    label.Font = new Font(this.myfont, 9f);
                    label.Location = new Point(x1, y);
                    label.Padding = new Padding(10, 0, 10, 0);
                    this.protocolHeader.Controls.Add((Control) label);
                    if (namedItem5 == null)
                      label.MaximumSize = new Size(width2, 0);
                    Size size;
                    if (width2 - label.Size.Width > width3)
                    {
                      int num6 = width2;
                      size = label.Size;
                      int width4 = size.Width;
                      width3 = num6 - width4;
                    }
                    int x2 = x1 + (width2 - width3);
                    if (label.Height > num5)
                      num5 = label.Height;
                    if (namedItem6 != null && namedItem5 != null)
                    {
                      if (namedItem6.Value == "date" || namedItem6.Value == "time" || namedItem6.Value == "dateTime")
                      {
                        MaskedTextBox maskedTextBox = new MaskedTextBox();
                        int num6 = width2;
                        size = label.Size;
                        int width4 = size.Width;
                        if (num6 - width4 > 85)
                        {
                          int num7 = width2;
                          size = label.Size;
                          int width5 = size.Width;
                          width3 = num7 - width5;
                        }
                        maskedTextBox.Size = new Size(width3, 20);
                        maskedTextBox.Name = namedItem5.Value;
                        maskedTextBox.MouseClick += new MouseEventHandler(this.Control_SHead_MouseClick);
                        maskedTextBox.LostFocus += new EventHandler(this.LostFocus);
                        maskedTextBox.Location = new Point(x2, y);
                        XmlNode xmlNode6 = this.save.SelectSingleNode("/save/header/" + maskedTextBox.Name);
                        if (xmlNode6 != null)
                          maskedTextBox.Text = xmlNode6.InnerText;
                        if (namedItem6.Value == "date")
                        {
                          maskedTextBox.Mask = "00-00-0000";
                          maskedTextBox.Validated += new EventHandler(this.date_Validated);
                          if (namedItem7 != null && namedItem7.Value != "" && Regex.IsMatch(namedItem7.Value, "^[0-9]{2}-[0-9]{2}-[0-9]{4}$"))
                            maskedTextBox.Text = namedItem7.Value;
                          if (namedItem4.Value == "data-ost-aktualizacji" && namedItem1 != null)
                          {
                            string[] strArray3 = namedItem1.Value.Split(new char[2]
                            {
                              ' ',
                              '-'
                            });
                            maskedTextBox.Text = strArray3[2] + "-" + strArray3[1] + "-" + strArray3[0];
                          }
                          try
                          {
                            this.typeValidation.Add(maskedTextBox.Name, "date");
                          }
                          catch (ArgumentException ex)
                          {
                          }
                        }
                        if (namedItem6.Value == "time")
                        {
                          maskedTextBox.Mask = "00:00";
                          maskedTextBox.Validated += new EventHandler(this.time_Validated);
                          if (namedItem7 != null && namedItem7.Value != "" && Regex.IsMatch(namedItem7.Value, "^[0-9]{2}:[0-9]{2}$"))
                            maskedTextBox.Text = namedItem7.Value;
                          try
                          {
                            this.typeValidation.Add(maskedTextBox.Name, "time");
                          }
                          catch (ArgumentException ex)
                          {
                          }
                        }
                        if (namedItem6.Value == "dateTime")
                        {
                          maskedTextBox.Mask = "00-00-0000 00:00";
                          maskedTextBox.Validated += new EventHandler(this.dateTime_Validated);
                          if (namedItem7 != null && namedItem7.Value != "" && Regex.IsMatch(namedItem7.Value, "^[0-9]{2}-[0-9]{2}-[0-9]{4} [0-9]{2}:[0-9]{2}$"))
                            maskedTextBox.Text = namedItem7.Value;
                          try
                          {
                            this.typeValidation.Add(maskedTextBox.Name, "dateTime");
                          }
                          catch (ArgumentException ex)
                          {
                          }
                        }
                        if (namedItem7 != null && namedItem7.Value != "")
                          maskedTextBox.Text = namedItem7.Value;
                        this.headerField.Add(maskedTextBox.Name);
                        this.protocolHeader.Controls.Add((Control) maskedTextBox);
                      }
                      else
                      {
                        TextBox textBox = new TextBox();
                        textBox.Size = new Size(width3, 20);
                        textBox.Name = namedItem5.Value;
                        textBox.Location = new Point(x2, y);
                        textBox.MouseClick += new MouseEventHandler(this.Control_SHead_MouseClick);
                        textBox.LostFocus += new EventHandler(this.LostFocus);
                        if (namedItem6.Value == "number")
                        {
                          textBox.Validated += new EventHandler(this.number_Validated);
                          this.typeValidation.Add(textBox.Name, "number");
                        }
                        if (namedItem6.Value == "text" || namedItem6.Value == "char")
                        {
                          textBox.Validating += new CancelEventHandler(this.text_Validated);
                          try
                          {
                            this.typeValidation.Add(textBox.Name, "text");
                          }
                          catch (ArgumentException ex)
                          {
                          }
                        }
                        this.protocolHeader.Controls.Add((Control) textBox);
                        if (namedItem7 != null && namedItem7.Value != "")
                          textBox.Text = namedItem7.Value;
                        XmlNode xmlNode6 = this.save.SelectSingleNode("/save/header/" + textBox.Name);
                        if (xmlNode6 != null)
                        {
                          textBox.Text = xmlNode6.InnerText;
                        }
                        else
                        {
                          if (namedItem4.Value == "jns_kod" || namedItem4.Value == "nameGmina" || namedItem4.Value == "namePowiat" || namedItem4.Value == "nameWojewodztwo")
                            textBox.Text = xmlNode2.Attributes.GetNamedItem(namedItem4.Value).Value;
                          if (namedItem4.Value == "nr")
                            textBox.Text = this.obwod;
                          if (namedItem4.Value == "algorytmOkreg")
                            textBox.Text = this.okreg;
                          if (namedItem4.Value == "algorytmOKW")
                          {
                            foreach (XmlNode xmlNode7 in xmlNode2)
                            {
                              if (xmlNode7.Name == "obw" && xmlNode7.Attributes.GetNamedItem("nr") != null && xmlNode7.Attributes.GetNamedItem("nr").Value == this.obwod)
                              {
                                foreach (XmlNode xmlNode8 in xmlNode7)
                                {
                                  if (xmlNode8.Name == "inst" && xmlNode8.Attributes.GetNamedItem("kod") != null && (xmlNode8.Attributes.GetNamedItem("kod").Value == this.inst && xmlNode8.Attributes.GetNamedItem("inst_jns") != null) && xmlNode8.Attributes.GetNamedItem("inst_jns").Value == this.instJNS)
                                  {
                                    foreach (XmlNode xmlNode9 in xmlNode8)
                                    {
                                      if (xmlNode9.Name == "okr" && xmlNode9.Attributes.GetNamedItem("nr") != null && xmlNode9.Attributes.GetNamedItem("nr").Value == this.okreg && xmlNode9.Attributes.GetNamedItem("siedziba") != null)
                                        textBox.Text = xmlNode9.Attributes.GetNamedItem("siedziba").Value;
                                    }
                                  }
                                }
                              }
                            }
                          }
                          if (namedItem4.Value == "algorytmOKW_L")
                          {
                            foreach (XmlNode xmlNode7 in xmlNode2)
                            {
                              if (xmlNode7.Name == "obw" && xmlNode7.Attributes.GetNamedItem("nr") != null && xmlNode7.Attributes.GetNamedItem("nr").Value == this.obwod)
                              {
                                foreach (XmlNode xmlNode8 in xmlNode7)
                                {
                                  if (xmlNode8.Name == "inst" && xmlNode8.Attributes.GetNamedItem("kod") != null && (xmlNode8.Attributes.GetNamedItem("kod").Value == this.inst && xmlNode8.Attributes.GetNamedItem("inst_jns") != null) && xmlNode8.Attributes.GetNamedItem("inst_jns").Value == this.instJNS)
                                  {
                                    foreach (XmlNode xmlNode9 in xmlNode8)
                                    {
                                      if (xmlNode9.Name == "okr" && xmlNode9.Attributes.GetNamedItem("nr") != null && xmlNode9.Attributes.GetNamedItem("nr").Value == this.okreg && xmlNode9.Attributes.GetNamedItem("siedzibaL") != null)
                                        textBox.Text = xmlNode9.Attributes.GetNamedItem("siedzibaL").Value;
                                    }
                                  }
                                }
                              }
                            }
                          }
                          if (namedItem4.Value == "algorytmOKW_R")
                          {
                            foreach (XmlNode xmlNode7 in xmlNode2)
                            {
                              if (xmlNode7.Name == "obw" && xmlNode7.Attributes.GetNamedItem("nr") != null && xmlNode7.Attributes.GetNamedItem("nr").Value == this.obwod)
                              {
                                foreach (XmlNode xmlNode8 in xmlNode7)
                                {
                                  if (xmlNode8.Name == "inst" && xmlNode8.Attributes.GetNamedItem("kod") != null && (xmlNode8.Attributes.GetNamedItem("kod").Value == this.inst && xmlNode8.Attributes.GetNamedItem("inst_jns") != null) && xmlNode8.Attributes.GetNamedItem("inst_jns").Value == this.instJNS)
                                  {
                                    foreach (XmlNode xmlNode9 in xmlNode8)
                                    {
                                      if (xmlNode9.Name == "okr" && xmlNode9.Attributes.GetNamedItem("nr") != null && xmlNode9.Attributes.GetNamedItem("nr").Value == this.okreg && xmlNode9.Attributes.GetNamedItem("siedzibaR") != null)
                                        textBox.Text = xmlNode9.Attributes.GetNamedItem("siedzibaR").Value;
                                    }
                                  }
                                }
                              }
                            }
                          }
                          if (namedItem4.Value == "siedzibaObwod")
                          {
                            foreach (XmlNode xmlNode7 in xmlNode2)
                            {
                              if (xmlNode7.Attributes.GetNamedItem("nr") != null && xmlNode7.Attributes.GetNamedItem("nr").Value == this.obwod && xmlNode7.Attributes.GetNamedItem("siedziba") != null)
                                textBox.Text = xmlNode7.Attributes.GetNamedItem("siedziba").Value;
                            }
                          }
                          if (namedItem4.Value == "liczbamandatow")
                          {
                            foreach (XmlNode xmlNode7 in xmlNode2)
                            {
                              if (xmlNode7.Name == "obw" && xmlNode7.Attributes.GetNamedItem("nr") != null && xmlNode7.Attributes.GetNamedItem("nr").Value == this.obwod)
                              {
                                foreach (XmlNode xmlNode8 in xmlNode7)
                                {
                                  if (xmlNode8.Name == "inst" && xmlNode8.Attributes.GetNamedItem("kod") != null && (xmlNode8.Attributes.GetNamedItem("kod").Value == this.inst && xmlNode8.Attributes.GetNamedItem("inst_jns") != null) && xmlNode8.Attributes.GetNamedItem("inst_jns").Value == this.instJNS)
                                  {
                                    foreach (XmlNode xmlNode9 in xmlNode8)
                                    {
                                      if (xmlNode9.Name == "okr" && xmlNode9.Attributes.GetNamedItem("nr") != null && xmlNode9.Attributes.GetNamedItem("nr").Value == this.okreg && xmlNode9.Attributes.GetNamedItem("lmandatow") != null)
                                        textBox.Text = xmlNode9.Attributes.GetNamedItem("lmandatow").Value;
                                    }
                                  }
                                }
                              }
                            }
                          }
                          if (namedItem4.Value == "wyborRady")
                          {
                            foreach (XmlNode xmlNode7 in xmlNode2)
                            {
                              if (xmlNode7.Name == "obw" && xmlNode7.Attributes.GetNamedItem("nr") != null && xmlNode7.Attributes.GetNamedItem("nr").Value == this.obwod)
                              {
                                foreach (XmlNode xmlNode8 in xmlNode7)
                                {
                                  if (xmlNode8.Name == "inst" && xmlNode8.Attributes.GetNamedItem("kod") != null && (xmlNode8.Attributes.GetNamedItem("kod").Value == this.inst && xmlNode8.Attributes.GetNamedItem("inst_jns") != null) && xmlNode8.Attributes.GetNamedItem("inst_jns").Value == this.instJNS && xmlNode8.Attributes.GetNamedItem("nazwaRadyMian") != null)
                                    textBox.Text = xmlNode8.Attributes.GetNamedItem("nazwaRadyMian").Value;
                                }
                              }
                            }
                          }
                          if (namedItem4.Value == "organNazwa")
                          {
                            foreach (XmlNode xmlNode7 in xmlNode2)
                            {
                              if (xmlNode7.Name == "obw" && xmlNode7.Attributes.GetNamedItem("nr") != null && xmlNode7.Attributes.GetNamedItem("nr").Value == this.obwod)
                              {
                                foreach (XmlNode xmlNode8 in xmlNode7)
                                {
                                  if (xmlNode8.Name == "inst" && xmlNode8.Attributes.GetNamedItem("kod") != null && (xmlNode8.Attributes.GetNamedItem("kod").Value == this.inst && xmlNode8.Attributes.GetNamedItem("inst_jns") != null) && xmlNode8.Attributes.GetNamedItem("inst_jns").Value == this.instJNS && xmlNode8.Attributes.GetNamedItem("organNazwa") != null)
                                    textBox.Text = xmlNode8.Attributes.GetNamedItem("organNazwa").Value.ToUpper();
                                }
                              }
                            }
                          }
                          if (namedItem4.Value == "nazwaRadyDopel")
                          {
                            foreach (XmlNode xmlNode7 in xmlNode2)
                            {
                              if (xmlNode7.Name == "obw" && xmlNode7.Attributes.GetNamedItem("nr") != null && xmlNode7.Attributes.GetNamedItem("nr").Value == this.obwod)
                              {
                                foreach (XmlNode xmlNode8 in xmlNode7)
                                {
                                  if (xmlNode8.Name == "inst" && xmlNode8.Attributes.GetNamedItem("kod") != null && (xmlNode8.Attributes.GetNamedItem("kod").Value == this.inst && xmlNode8.Attributes.GetNamedItem("inst_jns") != null) && xmlNode8.Attributes.GetNamedItem("inst_jns").Value == this.instJNS && xmlNode8.Attributes.GetNamedItem("nazwaRadyDopel") != null)
                                    textBox.Text = xmlNode8.Attributes.GetNamedItem("nazwaRadyDopel").Value.ToUpper();
                                }
                              }
                            }
                          }
                        }
                        this.headerField.Add(textBox.Name);
                      }
                    }
                    if (namedItem6 == null && namedItem5 != null && namedItem4 != null)
                    {
                      TextBox textBox = new TextBox();
                      textBox.Text = "";
                      int num6 = width2;
                      size = label.Size;
                      int width4 = size.Width;
                      if (num6 - width4 > 85)
                      {
                        int num7 = width2;
                        size = label.Size;
                        int width5 = size.Width;
                        width3 = num7 - width5;
                      }
                      else
                        width3 = 85;
                      textBox.Size = new Size(width3, 20);
                      textBox.Name = namedItem5.Value;
                      textBox.Location = new Point(x2, y);
                      XmlNode xmlNode6 = this.save.SelectSingleNode("/save/header/" + textBox.Name);
                      if (xmlNode6 != null)
                      {
                        textBox.Text = xmlNode6.InnerText;
                      }
                      else
                      {
                        if (namedItem4.Value == "jns_kod" || namedItem4.Value == "nameGmina" || namedItem4.Value == "namePowiat" || namedItem4.Value == "nameWojewodztwo")
                          textBox.Text = xmlNode2.Attributes.GetNamedItem(namedItem4.Value).Value;
                        if (namedItem4.Value == "nr")
                          textBox.Text = this.obwod;
                        if (namedItem4.Value == "algorytmOkreg")
                          textBox.Text = this.okreg;
                        if (namedItem4.Value == "algorytmOKW")
                        {
                          foreach (XmlNode xmlNode7 in xmlNode2)
                          {
                            if (xmlNode7.Name == "obw" && xmlNode7.Attributes.GetNamedItem("nr") != null && xmlNode7.Attributes.GetNamedItem("nr").Value == this.obwod)
                            {
                              foreach (XmlNode xmlNode8 in xmlNode7)
                              {
                                if (xmlNode8.Name == "inst" && xmlNode8.Attributes.GetNamedItem("kod") != null && (xmlNode8.Attributes.GetNamedItem("kod").Value == this.inst && xmlNode8.Attributes.GetNamedItem("inst_jns") != null) && xmlNode8.Attributes.GetNamedItem("inst_jns").Value == this.instJNS)
                                {
                                  foreach (XmlNode xmlNode9 in xmlNode8)
                                  {
                                    if (xmlNode9.Name == "okr" && xmlNode9.Attributes.GetNamedItem("nr") != null && xmlNode9.Attributes.GetNamedItem("nr").Value == this.okreg && xmlNode9.Attributes.GetNamedItem("siedziba") != null)
                                      textBox.Text = xmlNode9.Attributes.GetNamedItem("siedziba").Value;
                                  }
                                }
                              }
                            }
                          }
                        }
                        if (namedItem4.Value == "algorytmOKW_L")
                        {
                          foreach (XmlNode xmlNode7 in xmlNode2)
                          {
                            if (xmlNode7.Name == "obw" && xmlNode7.Attributes.GetNamedItem("nr") != null && xmlNode7.Attributes.GetNamedItem("nr").Value == this.obwod)
                            {
                              foreach (XmlNode xmlNode8 in xmlNode7)
                              {
                                if (xmlNode8.Name == "inst" && xmlNode8.Attributes.GetNamedItem("kod") != null && (xmlNode8.Attributes.GetNamedItem("kod").Value == this.inst && xmlNode8.Attributes.GetNamedItem("inst_jns") != null) && xmlNode8.Attributes.GetNamedItem("inst_jns").Value == this.instJNS)
                                {
                                  foreach (XmlNode xmlNode9 in xmlNode8)
                                  {
                                    if (xmlNode9.Name == "okr" && xmlNode9.Attributes.GetNamedItem("nr") != null && xmlNode9.Attributes.GetNamedItem("nr").Value == this.okreg && xmlNode9.Attributes.GetNamedItem("siedzibaL") != null)
                                      textBox.Text = xmlNode9.Attributes.GetNamedItem("siedzibaL").Value;
                                  }
                                }
                              }
                            }
                          }
                        }
                        if (namedItem4.Value == "algorytmOKW_R")
                        {
                          foreach (XmlNode xmlNode7 in xmlNode2)
                          {
                            if (xmlNode7.Name == "obw" && xmlNode7.Attributes.GetNamedItem("nr") != null && xmlNode7.Attributes.GetNamedItem("nr").Value == this.obwod)
                            {
                              foreach (XmlNode xmlNode8 in xmlNode7)
                              {
                                if (xmlNode8.Name == "inst" && xmlNode8.Attributes.GetNamedItem("kod") != null && (xmlNode8.Attributes.GetNamedItem("kod").Value == this.inst && xmlNode8.Attributes.GetNamedItem("inst_jns") != null) && xmlNode8.Attributes.GetNamedItem("inst_jns").Value == this.instJNS)
                                {
                                  foreach (XmlNode xmlNode9 in xmlNode8)
                                  {
                                    if (xmlNode9.Name == "okr" && xmlNode9.Attributes.GetNamedItem("nr") != null && xmlNode9.Attributes.GetNamedItem("nr").Value == this.okreg && xmlNode9.Attributes.GetNamedItem("siedzibaR") != null)
                                      textBox.Text = xmlNode9.Attributes.GetNamedItem("siedzibaR").Value;
                                  }
                                }
                              }
                            }
                          }
                        }
                        if (namedItem4.Value == "siedzibaObwod")
                        {
                          foreach (XmlNode xmlNode7 in xmlNode2)
                          {
                            if (xmlNode7.Attributes.GetNamedItem("nr") != null && xmlNode7.Attributes.GetNamedItem("nr").Value == this.obwod && xmlNode7.Attributes.GetNamedItem("siedziba") != null)
                              textBox.Text = xmlNode7.Attributes.GetNamedItem("siedziba").Value;
                          }
                        }
                        if (namedItem4.Value == "liczbamandatow")
                        {
                          foreach (XmlNode xmlNode7 in xmlNode2)
                          {
                            if (xmlNode7.Name == "obw" && xmlNode7.Attributes.GetNamedItem("nr") != null && xmlNode7.Attributes.GetNamedItem("nr").Value == this.obwod)
                            {
                              foreach (XmlNode xmlNode8 in xmlNode7)
                              {
                                if (xmlNode8.Name == "inst" && xmlNode8.Attributes.GetNamedItem("kod") != null && (xmlNode8.Attributes.GetNamedItem("kod").Value == this.inst && xmlNode8.Attributes.GetNamedItem("inst_jns") != null) && xmlNode8.Attributes.GetNamedItem("inst_jns").Value == this.instJNS)
                                {
                                  foreach (XmlNode xmlNode9 in xmlNode8)
                                  {
                                    if (xmlNode9.Name == "okr" && xmlNode9.Attributes.GetNamedItem("nr") != null && xmlNode9.Attributes.GetNamedItem("nr").Value == this.okreg && xmlNode9.Attributes.GetNamedItem("lmandatow") != null)
                                      textBox.Text = xmlNode9.Attributes.GetNamedItem("lmandatow").Value;
                                  }
                                }
                              }
                            }
                          }
                        }
                        if (namedItem4.Value == "wyborRady")
                        {
                          foreach (XmlNode xmlNode7 in xmlNode2)
                          {
                            if (xmlNode7.Name == "obw" && xmlNode7.Attributes.GetNamedItem("nr") != null && xmlNode7.Attributes.GetNamedItem("nr").Value == this.obwod)
                            {
                              foreach (XmlNode xmlNode8 in xmlNode7)
                              {
                                if (xmlNode8.Name == "inst" && xmlNode8.Attributes.GetNamedItem("kod") != null && (xmlNode8.Attributes.GetNamedItem("kod").Value == this.inst && xmlNode8.Attributes.GetNamedItem("inst_jns") != null) && xmlNode8.Attributes.GetNamedItem("inst_jns").Value == this.instJNS && xmlNode8.Attributes.GetNamedItem("nazwaRadyMian") != null)
                                  textBox.Text = xmlNode8.Attributes.GetNamedItem("nazwaRadyMian").Value;
                              }
                            }
                          }
                        }
                        if (namedItem4.Value == "organNazwa")
                        {
                          foreach (XmlNode xmlNode7 in xmlNode2)
                          {
                            if (xmlNode7.Name == "obw" && xmlNode7.Attributes.GetNamedItem("nr") != null && xmlNode7.Attributes.GetNamedItem("nr").Value == this.obwod)
                            {
                              foreach (XmlNode xmlNode8 in xmlNode7)
                              {
                                if (xmlNode8.Name == "inst" && xmlNode8.Attributes.GetNamedItem("kod") != null && (xmlNode8.Attributes.GetNamedItem("kod").Value == this.inst && xmlNode8.Attributes.GetNamedItem("inst_jns") != null) && xmlNode8.Attributes.GetNamedItem("inst_jns").Value == this.instJNS && xmlNode8.Attributes.GetNamedItem("organNazwa") != null)
                                  textBox.Text = xmlNode8.Attributes.GetNamedItem("organNazwa").Value.ToUpper();
                              }
                            }
                          }
                        }
                        if (namedItem4.Value == "nazwaRadyDopel")
                        {
                          foreach (XmlNode xmlNode7 in xmlNode2)
                          {
                            if (xmlNode7.Name == "obw" && xmlNode7.Attributes.GetNamedItem("nr") != null && xmlNode7.Attributes.GetNamedItem("nr").Value == this.obwod)
                            {
                              foreach (XmlNode xmlNode8 in xmlNode7)
                              {
                                if (xmlNode8.Name == "inst" && xmlNode8.Attributes.GetNamedItem("kod") != null && (xmlNode8.Attributes.GetNamedItem("kod").Value == this.inst && xmlNode8.Attributes.GetNamedItem("inst_jns") != null) && xmlNode8.Attributes.GetNamedItem("inst_jns").Value == this.instJNS && xmlNode8.Attributes.GetNamedItem("nazwaRadyDopel") != null)
                                  textBox.Text = xmlNode8.Attributes.GetNamedItem("nazwaRadyDopel").Value.ToUpper();
                              }
                            }
                          }
                        }
                      }
                      textBox.Enabled = false;
                      if (namedItem8 != null && namedItem8.Value == "hand")
                      {
                        int num7 = width2;
                        size = label.Size;
                        int width5 = size.Width;
                        if (num7 - width5 - 95 > 85)
                        {
                          int num8 = width2;
                          size = label.Size;
                          int width6 = size.Width;
                          width3 = num8 - width6 - 95;
                        }
                        else
                          width3 = 85;
                        textBox.Size = new Size(width3, 20);
                        Button button1 = new Button();
                        button1.Name = "btn_" + textBox.Name;
                        button1.Text = "Edytuj";
                        button1.Size = new Size(85, 20);
                        button1.Click += new EventHandler(this.EditSystem);
                        button1.Visible = true;
                        Button button2 = button1;
                        int num9 = x2;
                        int num10 = width2;
                        size = label.Size;
                        int width7 = size.Width;
                        int num11 = num10 - width7 - 95;
                        Point point = new Point(num9 + num11, y);
                        button2.Location = point;
                        this.protocolHeader.Controls.Add((Control) textBox);
                        this.protocolHeader.Controls.Add((Control) button1);
                        this.headerField.Add(button1.Name);
                        this.headerField2 = textBox.Name;
                      }
                      else
                        this.protocolHeader.Controls.Add((Control) textBox);
                    }
                    x1 = x2 + width3;
                  }
                  y += num5 + 5;
                }
              }
              y += 30;
            }
          }
          else
            break;
        }
        foreach (XmlNode xmlNode3 in xmlNode2)
        {
          if (xmlNode3.Attributes.GetNamedItem("nr") != null && xmlNode3.Attributes.GetNamedItem("nr").Value == this.obwod)
          {
            XmlNode namedItem2 = xmlNode3.Attributes.GetNamedItem("typ_obwodu");
            if (namedItem2 != null)
              this.typeObw = namedItem2.Value;
            foreach (XmlNode xmlNode4 in xmlNode3)
            {
              XmlNode namedItem3 = xmlNode4.Attributes.GetNamedItem("kod");
              if (namedItem3 != null && namedItem3.Value == this.inst)
              {
                XmlNode namedItem4 = xmlNode4.Attributes.GetNamedItem("naklad");
                if (namedItem4 != null && Regex.IsMatch(namedItem4.Value, "^[0-9]{1,3}$"))
                {
                  int num = Convert.ToInt32(namedItem4.Value);
                  this.naklad = num < 0 || num > 100 ? "100" : namedItem4.Value;
                }
                XmlNode namedItem5 = xmlNode4.Attributes.GetNamedItem("plusminus");
                this.plusminus = namedItem5 == null || !Regex.IsMatch(namedItem5.Value, "^[0-9]{0,2}$") ? "0" : namedItem5.Value;
                XmlNode namedItem6 = xmlNode4.Attributes.GetNamedItem("plus");
                this.plus = namedItem6 == null || !Regex.IsMatch(namedItem6.Value, "^[0-9]+$") ? "0" : namedItem6.Value;
                XmlNode namedItem7 = xmlNode4.Attributes.GetNamedItem("minus");
                this.minus = namedItem7 == null || !Regex.IsMatch(namedItem7.Value, "^[0-9]+$") ? "0" : namedItem7.Value;
                foreach (XmlNode xmlNode5 in xmlNode4)
                {
                  if (xmlNode5.Attributes.GetNamedItem("nr") != null && xmlNode5.Attributes.GetNamedItem("nr").Value == this.okreg)
                  {
                    XmlNode namedItem8 = xmlNode5.Attributes.GetNamedItem("koresp");
                    if (namedItem8 != null)
                      this.correspondence = !(namedItem8.Value == "0");
                    XmlNode namedItem9 = xmlNode5.Attributes.GetNamedItem("lwyb");
                    if (namedItem9 != null && Regex.IsMatch(namedItem9.Value, "^[0-9]+$"))
                      this.lwyb = namedItem9.Value;
                    XmlNode namedItem10 = xmlNode5.Attributes.GetNamedItem("lwybA");
                    if (namedItem10 != null && Regex.IsMatch(namedItem10.Value, "^[0-9]+$"))
                      this.lwybA = namedItem10.Value;
                    XmlNode namedItem11 = xmlNode5.Attributes.GetNamedItem("lwybB");
                    if (namedItem11 != null && Regex.IsMatch(namedItem11.Value, "^[0-9]+$"))
                      this.lwybB = namedItem11.Value;
                  }
                }
              }
            }
          }
        }
      }
      catch (XmlException ex)
      {
        this.wait.setVisible(false);
        int num = (int) MessageBox.Show("Nieprawidłowy XML: " + ex.Message, "Błąd");
      }
      catch (NullReferenceException ex)
      {
        this.wait.setVisible(false);
        int num = (int) MessageBox.Show("Podanno inny xml niz header. " + ex.Message, "Bład");
      }
      this.setFirstFocus();
      this.wait.setVisible(false);
    }

    private void EditSystem(object sender, EventArgs e)
    {
      if (!(this.protocolForm1.BackColor == SystemColors.GradientInactiveCaption) && !(this.protocolForm2.BackColor == SystemColors.GradientInactiveCaption))
        return;
      string index = (sender as Button).Name.Replace("btn_", "");
      if (!this.protocolHeader.Controls[index].Enabled)
        this.protocolHeader.Controls[index].Enabled = true;
      else
        this.protocolHeader.Controls[index].Enabled = false;
    }

    private void lockHeader()
    {
      if (this.headerField != null)
      {
        for (int index = 0; index < this.headerField.Count; ++index)
          this.protocolHeader.Controls[this.headerField[index]].Enabled = false;
      }
      if (this.headerField2 == null)
        return;
      this.protocolHeader.Controls[this.headerField2].Enabled = false;
    }

    private void unlockHeader()
    {
      if (this.headerField == null)
        return;
      for (int index = 0; index < this.headerField.Count; ++index)
        this.protocolHeader.Controls[this.headerField[index]].Enabled = true;
    }

    private void getCalculator()
    {
      this.wait.setWaitPanel("Trwa ładowanie pierwszego kroku protokołu", "Proszę czekać");
      this.wait.setVisible(true);
      this.Form1panel.TabIndex = 3;
      this.protocolHeader.TabIndex = 2;
      this.buttonNext.Enabled = false;
      int num1 = 760;
      int num2 = 105;
      try
      {
        string str1 = "";
        if (this.isSave() == 0 || this.isSave() == 4)
          str1 = "/save/form";
        if (this.isSave() == 1 || this.isSave() == 2 || this.isSave() == 3)
          str1 = "/save/step1";
        string[] strArray1 = this.savePath.Split('-');
        string str2;
        string str3;
        string str4;
        if (strArray1.Length == 6)
        {
          str2 = strArray1[2].Replace("Obw", "");
          str3 = strArray1[3].Replace("Inst", "");
          str4 = strArray1[5].Replace("Okr", "").Replace(".xml", "");
        }
        else
        {
          str2 = strArray1[strArray1.Length - 4].Replace("Obw", "");
          str3 = strArray1[strArray1.Length - 3].Replace("Inst", "");
          str4 = strArray1[strArray1.Length - 1].Replace("Okr", "").Replace(".xml", "");
        }
        XmlNode xmlNode1 = this.protocolDefinition.SelectSingleNode("/protokol_info");
        XmlNode xmlNode2 = this.candidates.SelectSingleNode("/listy");
        int x = 0;
        int y1 = 0;
        foreach (XmlNode xmlNode3 in xmlNode1)
        {
          XmlNode namedItem1 = xmlNode3.Attributes.GetNamedItem("type");
          if (namedItem1 != null)
          {
            this.wait.setWaitPanel("Trwa ładowanie pierwszego kroku protokołu - ładowanie pol kalkulatora", "Proszę czekać");
            if (namedItem1.Value == "calculator")
            {
              x = 10;
              if (xmlNode3.Name == "fields")
              {
                foreach (XmlNode xmlNode4 in xmlNode3)
                {
                  if (xmlNode4.Name == "title")
                  {
                    Label label = new Label();
                    label.Text = xmlNode4.InnerText;
                    label.AutoSize = true;
                    label.MaximumSize = new Size(this.Form1panel.Size.Width - 20, 0);
                    label.Font = new Font(this.myfont, 10f, FontStyle.Bold);
                    label.Location = new Point(x, y1);
                    this.Form1panel.Controls.Add((Control) label);
                    y1 += label.Height + 30;
                  }
                  if (xmlNode4.Name == "description")
                  {
                    x = 10;
                    Label label = new Label();
                    label.Text = xmlNode4.InnerText;
                    label.AutoSize = true;
                    label.MaximumSize = new Size(this.Form1panel.Size.Width - 20, 0);
                    label.Font = new Font(this.myfont, 9f, FontStyle.Bold);
                    label.Location = new Point(x, y1 - 15);
                    this.Form1panel.Controls.Add((Control) label);
                    y1 += label.Height + 25;
                  }
                  if (xmlNode4.Name == "note")
                  {
                    x = 10;
                    Label label1 = new Label();
                    label1.Text = "Uwaga";
                    label1.AutoSize = true;
                    label1.MaximumSize = new Size(100, 0);
                    label1.Font = new Font(this.myfont, 9f, FontStyle.Bold);
                    label1.Location = new Point(x, y1);
                    this.Form1panel.Controls.Add((Control) label1);
                    x += label1.Width + 5;
                    this.Form1panel.Controls.Add((Control) label1);
                    Label label2 = new Label();
                    label2.Text = xmlNode4.InnerText;
                    label2.AutoSize = true;
                    label2.MaximumSize = new Size(this.Form1panel.Size.Width - 120, 0);
                    label2.Font = new Font(this.myfont, 9f);
                    label2.Location = new Point(x, y1);
                    this.Form1panel.Controls.Add((Control) label2);
                    y1 += label2.Height + 25;
                  }
                  if (xmlNode4.Name == "field")
                  {
                    string str5 = "";
                    bool flag = true;
                    XmlNode namedItem2 = xmlNode4.Attributes.GetNamedItem("correspondence");
                    XmlNode namedItem3 = xmlNode4.Attributes.GetNamedItem("elseCorrespondenceDefault");
                    if (namedItem2 != null && (namedItem2.Value.ToLower() == "false" && this.correspondence || namedItem2.Value.ToLower() == "true" && !this.correspondence))
                    {
                      flag = false;
                      if (namedItem3 != null)
                        str5 = namedItem3.Value;
                    }
                    XmlNode namedItem4 = xmlNode4.Attributes.GetNamedItem("district");
                    XmlNode namedItem5 = xmlNode4.Attributes.GetNamedItem("districtDefault");
                    if (namedItem4 != null && namedItem4.Value.ToUpper() != "ALL")
                    {
                      if (Regex.IsMatch(namedItem4.Value.ToUpper(), "ONLY:") && namedItem4.Value.ToUpper().Replace("ONLY:", "") == this.typeObw.ToUpper())
                      {
                        flag = false;
                        if (namedItem5 != null)
                          str5 = namedItem5.Value;
                      }
                      if (Regex.IsMatch(namedItem4.Value.ToUpper(), "EXCLUDE:") && namedItem4.Value.ToUpper().Replace("EXCLUDE:", "") != this.typeObw.ToUpper())
                      {
                        flag = false;
                        if (namedItem5 != null)
                          str5 = namedItem5.Value;
                      }
                    }
                    x = 10;
                    Label label = new Label();
                    TextBox textBox = new TextBox();
                    foreach (XmlNode xmlNode5 in xmlNode4)
                    {
                      if (xmlNode5.Name == "name")
                      {
                        label.Text = xmlNode5.ParentNode.Attributes.GetNamedItem("lp").Value + " " + xmlNode5.InnerText;
                        label.AutoSize = true;
                        label.MaximumSize = new Size(num1 - num2, 0);
                        label.Font = new Font(this.myfont, 9f);
                        label.Location = new Point(x, y1);
                        x = num1 - 95;
                      }
                      if (xmlNode5.Name == "save_as")
                      {
                        textBox.Size = new Size(85, 20);
                        textBox.Name = xmlNode5.InnerText;
                        textBox.Enabled = flag;
                        textBox.Text = str5;
                        XmlNode xmlNode6 = this.save.SelectSingleNode(str1 + "/" + xmlNode5.InnerText);
                        if (xmlNode6 != null && flag)
                          textBox.Text = xmlNode6.InnerText;
                        textBox.MouseClick += new MouseEventHandler(this.Control_S1_MouseClick);
                        textBox.LostFocus += new EventHandler(this.LostFocus);
                        textBox.Location = new Point(x, y1);
                        textBox.CausesValidation = true;
                        XmlNode namedItem6 = xmlNode5.ParentNode.Attributes.GetNamedItem("data");
                        if (namedItem6 != null && namedItem6.Value == "number")
                        {
                          textBox.Validated += new EventHandler(this.number_Validated);
                          try
                          {
                            this.typeValidation.Add(textBox.Name, "number");
                          }
                          catch (ArgumentException ex)
                          {
                          }
                        }
                        if (namedItem6 != null && namedItem6.Value == "text")
                        {
                          textBox.Validated += new EventHandler(this.number_Validated);
                          try
                          {
                            this.typeValidation.Add(textBox.Name, "text");
                          }
                          catch (ArgumentException ex)
                          {
                          }
                        }
                        label.Name = "Lab_" + xmlNode5.InnerText;
                        XmlNode namedItem7 = xmlNode5.ParentNode.Attributes.GetNamedItem("min");
                        XmlNode namedItem8 = xmlNode5.ParentNode.Attributes.GetNamedItem("max");
                        if (namedItem7 != null && namedItem8 != null)
                          this.range.Add(textBox.Name, new ValidationRange(textBox.Name, Convert.ToInt32(namedItem7.Value), Convert.ToInt32(namedItem8.Value)));
                        if (namedItem7 != null && namedItem8 == null)
                          this.range.Add(textBox.Name, new ValidationRange(textBox.Name, Convert.ToInt32(namedItem7.Value)));
                      }
                    }
                    this.Form1panel.Controls.Add((Control) label);
                    this.Form1panel.Controls.Add((Control) textBox);
                    if (label.Height > 20)
                      y1 += label.Height + 5;
                    else
                      y1 += 25;
                  }
                }
              }
              y1 += 50;
            }
            this.wait.setWaitPanel("Trwa ładowanie pierwszego kroku protokołu - ładowanie kandydatów", "Proszę czekać");
            if (namedItem1 != null && namedItem1.Value == "additional-calculator")
            {
              foreach (XmlNode xmlNode4 in xmlNode3)
              {
                if (xmlNode4.Name == "field")
                {
                  XmlNode namedItem2 = xmlNode4.Attributes.GetNamedItem("data");
                  if (namedItem2 != null && namedItem2.Value == "kandydaci")
                  {
                    foreach (XmlNode itemChild1 in xmlNode4)
                    {
                      if (itemChild1.Name == "name")
                      {
                        x = 10;
                        Label label = new Label();
                        label.Text = itemChild1.ParentNode.Attributes.GetNamedItem("lp").Value + " " + itemChild1.InnerText;
                        label.AutoSize = true;
                        label.MaximumSize = new Size(this.Form2panel.Width, 0);
                        label.MinimumSize = new Size(this.Form2panel.Width, 0);
                        label.Font = new Font(this.myfont, 9f);
                        label.Location = new Point(x, y1);
                        this.Form1panel.Controls.Add((Control) label);
                        y1 += label.Size.Height + 30;
                      }
                      if (xmlNode4.Name == "note")
                      {
                        x = 10;
                        Label label1 = new Label();
                        label1.Text = "Uwaga";
                        label1.AutoSize = true;
                        label1.MaximumSize = new Size(100, 0);
                        label1.Font = new Font(this.myfont, 9f, FontStyle.Bold);
                        label1.Location = new Point(x, y1);
                        this.Form2panel.Controls.Add((Control) label1);
                        x += label1.Width + 5;
                        this.Form1panel.Controls.Add((Control) label1);
                        Label label2 = new Label();
                        label2.Text = xmlNode4.InnerText;
                        label2.AutoSize = true;
                        label2.MaximumSize = new Size(700, 0);
                        label2.Font = new Font(this.myfont, 9f);
                        label2.Location = new Point(x, y1);
                        this.Form1panel.Controls.Add((Control) label2);
                        y1 += label2.Height + 25;
                      }
                      string str5;
                      if (itemChild1.Name == "patternrows")
                      {
                        List<Field> patternField = new List<Field>();
                        List<Field> list = this.readPatternCandidate(itemChild1, patternField);
                        int num3 = 1;
                        string str6 = "";
                        foreach (XmlNode xmlNode5 in xmlNode2)
                        {
                          Label label = new Label();
                          TextBox textBox = new TextBox();
                          string str7 = "A";
                          for (int index = 0; index < list.Count; ++index)
                          {
                            string str8 = "A";
                            if (list[index].getStatus() == list[index].getStatus().Replace("parent:", ""))
                            {
                              if (xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getStatus()) != null)
                                str8 = xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getStatus()).Value;
                            }
                            else
                            {
                              string name = list[index].getStatus().Replace("parent:", "");
                              if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                str8 = xmlNode5.Attributes.GetNamedItem(name).Value;
                            }
                            str7 = str8;
                            if (str8 == "A")
                            {
                              string str9 = "";
                              string str10 = "";
                              string str11 = "";
                              string str12 = "";
                              string str13 = "";
                              string str14 = "";
                              str5 = "";
                              if (list[index].getDisplay() == list[index].getDisplay().Replace("parent:", ""))
                              {
                                if (xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getDisplay()) != null)
                                  str5 = xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getDisplay()).Value;
                              }
                              else
                              {
                                string name = list[index].getDisplay().Replace("parent:", "");
                                if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                  str5 = xmlNode5.Attributes.GetNamedItem(name).Value;
                              }
                              if (list[index].getIdCandidate() == list[index].getIdCandidate().Replace("parent:", ""))
                              {
                                if (xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getIdCandidate()) != null)
                                  str6 = xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getIdCandidate()).Value;
                              }
                              else
                              {
                                string name = list[index].getIdCandidate().Replace("parent:", "");
                                if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                  str6 = xmlNode5.Attributes.GetNamedItem(name).Value;
                              }
                              if (list[index].getImie2() == list[index].getImie2().Replace("parent:", ""))
                              {
                                if (xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getImie2()) != null)
                                  str10 = xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getImie2()).Value;
                              }
                              else
                              {
                                string name = list[index].getImie2().Replace("parent:", "");
                                if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                  str10 = xmlNode5.Attributes.GetNamedItem(name).Value;
                              }
                              if (list[index].getImie1() == list[index].getImie1().Replace("parent:", ""))
                              {
                                if (xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getImie1()) != null)
                                  str9 = xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getImie1()).Value;
                              }
                              else
                              {
                                string name = list[index].getImie1().Replace("parent:", "");
                                if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                  str9 = xmlNode5.Attributes.GetNamedItem(name).Value;
                              }
                              if (list[index].getNazwisko() == list[index].getNazwisko().Replace("parent:", ""))
                              {
                                if (xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getNazwisko()) != null)
                                  str11 = xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getNazwisko()).Value;
                              }
                              else
                              {
                                string name = list[index].getNazwisko().Replace("parent:", "");
                                if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                  str11 = xmlNode5.Attributes.GetNamedItem(name).Value;
                              }
                              if (list[index].getKomitet() == list[index].getKomitet().Replace("parent:", ""))
                              {
                                if (xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getKomitet()) != null)
                                  str13 = xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getKomitet()).Value;
                              }
                              else
                              {
                                string name = list[index].getKomitet().Replace("parent:", "");
                                if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                  str13 = xmlNode5.Attributes.GetNamedItem(name).Value;
                              }
                              string str15 = "";
                              if (list[index].getLista() == list[index].getLista().Replace("parent:", ""))
                              {
                                if (xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getLista()) != null)
                                  str15 = xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getLista()).Value;
                              }
                              else
                              {
                                string name = list[index].getLista().Replace("parent:", "");
                                if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                  str15 = xmlNode5.Attributes.GetNamedItem(name).Value;
                              }
                              if (str15 != "")
                                str14 = list[index].getName3() + str15;
                              if (list[index].getPlec() == list[index].getPlec().Replace("parent:", ""))
                              {
                                if (xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getPlec()) != null)
                                  str12 = !(xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getPlec()).Value.ToUpper() == "M") ? list[index].getName2v2() : list[index].getName2();
                              }
                              else
                              {
                                string name = list[index].getPlec().Replace("parent:", "");
                                if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                  str12 = !(xmlNode5.Attributes.GetNamedItem(name).Value.ToUpper() == "M") ? list[index].getName2v2() : list[index].getName2();
                              }
                              string str16 = list[index].getName1() + " " + str11 + " " + str9 + " " + str10 + " " + str12 + " " + str13 + str14;
                              if (list[index].getDataType() == "text" && list[index].getSave() == "")
                              {
                                x = 10;
                                label.Text = str16;
                                label.AutoSize = true;
                                label.MaximumSize = new Size(num1 - 105, 0);
                                label.Font = new Font(this.myfont, 9f);
                                label.Location = new Point(x, y1);
                                x = num1 - 95;
                                this.Form1panel.Controls.Add((Control) label);
                              }
                              if (list[index].getDataType() == "number" && list[index].getSave() != "")
                              {
                                textBox.Size = new Size(85, 20);
                                textBox.Name = list[index].getSave().Replace("X", num3.ToString());
                                textBox.Location = new Point(x, y1);
                                textBox.Validated += new EventHandler(this.number_Validated);
                                textBox.MouseClick += new MouseEventHandler(this.Control_S1_MouseClick);
                                textBox.LostFocus += new EventHandler(this.LostFocus);
                                try
                                {
                                  this.typeValidation.Add(textBox.Name, "number");
                                }
                                catch (ArgumentException ex)
                                {
                                }
                                XmlNode xmlNode6 = this.save.SelectSingleNode(str1 + "/" + textBox.Name);
                                if (xmlNode6 != null)
                                  textBox.Text = xmlNode6.InnerText;
                                if (!(list[index].getDisplay().ToLower() == "false"))
                                  this.Form1panel.Controls.Add((Control) textBox);
                                if (label.Height > 20)
                                  y1 += label.Height + 5;
                                else
                                  y1 += 25;
                                try
                                {
                                  this.range.Add(textBox.Name, list[index].getRange(textBox.Name));
                                }
                                catch (Exception ex)
                                {
                                }
                                this.candidatesRule[textBox.Name] = str6;
                                str6 = "";
                              }
                            }
                          }
                          if (str7 == "A")
                            ++num3;
                        }
                        this.countcandidatesoflist.Add(new int[2]
                        {
                          0,
                          num3 - 1
                        });
                      }
                      if (itemChild1.Name == "patternrow")
                      {
                        List<Field> patternField = new List<Field>();
                        List<Field> list = this.readPatternCandidate(itemChild1, patternField);
                        for (int index = 0; index < list.Count; ++index)
                        {
                          if (!list[index].needImportData())
                          {
                            x = 10;
                            Label label = new Label();
                            label.Text = list[index].getName1();
                            label.AutoSize = true;
                            label.MaximumSize = new Size(num1 - 105, 0);
                            label.Font = new Font(this.myfont, 9f);
                            label.Location = new Point(x, y1);
                            x = num1 - 95;
                            this.Form1panel.Controls.Add((Control) label);
                            TextBox textBox = new TextBox();
                            textBox.Size = new Size(85, 20);
                            textBox.Name = list[index].getSave();
                            textBox.Location = new Point(x, y1);
                            textBox.MouseClick += new MouseEventHandler(this.Control_S1_MouseClick);
                            textBox.LostFocus += new EventHandler(this.LostFocus);
                            textBox.Validated += new EventHandler(this.number_Validated);
                            try
                            {
                              this.typeValidation.Add(textBox.Name, "number");
                            }
                            catch (ArgumentException ex)
                            {
                            }
                            XmlNode xmlNode5 = this.save.SelectSingleNode(str1 + "/" + textBox.Name);
                            if (xmlNode5 != null)
                              textBox.Text = xmlNode5.InnerText;
                            this.Form1panel.Controls.Add((Control) textBox);
                            if (label.Height > 20)
                              y1 += label.Height + 5;
                            else
                              y1 += 25;
                            this.range.Add(textBox.Name, list[index].getRange(textBox.Name));
                          }
                        }
                      }
                      if (itemChild1.Name == "patternlist")
                      {
                        int num3 = 1;
                        foreach (XmlNode xmlNode5 in xmlNode2)
                        {
                          foreach (XmlNode itemChild2 in itemChild1)
                          {
                            if (itemChild2.Name == "title")
                            {
                              x = 0;
                              XmlNode namedItem3 = itemChild2.Attributes.GetNamedItem("bold");
                              XmlNode namedItem4 = itemChild2.Attributes.GetNamedItem("nr");
                              XmlNode namedItem5 = itemChild2.Attributes.GetNamedItem("komitet");
                              XmlNode namedItem6 = itemChild2.Attributes.GetNamedItem("lista");
                              string str6 = "";
                              if (namedItem5 != null)
                              {
                                XmlNode namedItem7 = xmlNode5.Attributes.GetNamedItem(namedItem5.Value);
                                if (namedItem7 != null && namedItem7.Value != "")
                                  str6 = namedItem7.Value;
                              }
                              string str7 = "";
                              if (namedItem6 != null)
                              {
                                XmlNode namedItem7 = xmlNode5.Attributes.GetNamedItem(namedItem6.Value);
                                if (namedItem7 != null && namedItem7.Value != "")
                                  str7 = namedItem7.Value;
                              }
                              string str8;
                              if (namedItem4 != null)
                                str8 = namedItem4.Value + " " + str7 + " " + itemChild2.InnerText + str6;
                              else
                                str8 = itemChild2.InnerText;
                              Label label1 = new Label();
                              label1.Text = str8;
                              label1.AutoSize = true;
                              Label label2 = label1;
                              Size size1 = this.Form1panel.Size;
                              Size size2 = new Size(size1.Width, 0);
                              label2.MaximumSize = size2;
                              label1.Font = new Font(this.myfont, 10f);
                              label1.Padding = new Padding(10, 0, 10, 0);
                              if (namedItem3 != null && namedItem3.Value == "true")
                                label1.Font = new Font(this.myfont, 10f, FontStyle.Bold);
                              label1.Location = new Point(x, y1);
                              this.Form1panel.Controls.Add((Control) label1);
                              int num4 = y1;
                              size1 = label1.Size;
                              int num5 = size1.Height + 30;
                              y1 = num4 + num5;
                            }
                            if (itemChild2.Name == "patternrow")
                            {
                              List<Field> patternField = new List<Field>();
                              List<Field> list = this.readPatternCandidate(itemChild2, patternField);
                              for (int index = 0; index < list.Count; ++index)
                              {
                                if (!list[index].needImportData())
                                {
                                  x = 10;
                                  Label label = new Label();
                                  label.Text = list[index].getName1();
                                  label.AutoSize = true;
                                  label.MaximumSize = new Size(this.Form2panel.Width - 105, 0);
                                  label.Font = new Font(this.myfont, 9f);
                                  label.Location = new Point(x, y1);
                                  x = this.Form2panel.Width - 95;
                                  this.Form1panel.Controls.Add((Control) label);
                                  TextBox textBox = new TextBox();
                                  textBox.Size = new Size(85, 20);
                                  textBox.Name = list[index].getSave().Replace("Y", num3.ToString());
                                  textBox.MouseClick += new MouseEventHandler(this.Control_S1_MouseClick);
                                  textBox.LostFocus += new EventHandler(this.LostFocus);
                                  XmlNode xmlNode6 = this.save.SelectSingleNode(str1 + "/" + textBox.Name);
                                  if (xmlNode6 != null)
                                    textBox.Text = xmlNode6.InnerText;
                                  textBox.Location = new Point(x, y1);
                                  textBox.Validated += new EventHandler(this.number_Validated);
                                  Exception exception;
                                  try
                                  {
                                    this.typeValidation.Add(textBox.Name, "number");
                                  }
                                  catch (Exception ex)
                                  {
                                    exception = ex;
                                  }
                                  try
                                  {
                                    this.Form1panel.Controls.Add((Control) textBox);
                                  }
                                  catch (Exception ex)
                                  {
                                    exception = ex;
                                  }
                                  if (label.Height > 20)
                                    y1 += label.Height + 5;
                                  else
                                    y1 += 25;
                                  try
                                  {
                                    this.range.Add(textBox.Name, list[index].getRange(textBox.Name));
                                  }
                                  catch (Exception ex)
                                  {
                                  }
                                }
                              }
                            }
                            if (itemChild2.Name == "patternrows")
                            {
                              List<Field> patternField = new List<Field>();
                              List<Field> list = this.readPatternCandidate(itemChild2, patternField);
                              int num4 = 1;
                              for (int index1 = 0; index1 < xmlNode5.ChildNodes.Count; ++index1)
                              {
                                string str6 = "";
                                Label label = new Label();
                                TextBox textBox = new TextBox();
                                string str7 = "A";
                                for (int index2 = 0; index2 < list.Count; ++index2)
                                {
                                  string str8 = "A";
                                  if (list[index2].getStatus() == list[index2].getStatus().Replace("parent:", ""))
                                  {
                                    if (xmlNode5.ChildNodes[index1].Attributes.GetNamedItem(list[index2].getStatus()) != null)
                                      str8 = xmlNode5.ChildNodes[index1].Attributes.GetNamedItem(list[index2].getStatus()).Value;
                                  }
                                  else
                                  {
                                    string name = list[index2].getStatus().Replace("parent:", "");
                                    if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                      str8 = xmlNode5.Attributes.GetNamedItem(name).Value;
                                  }
                                  str7 = str8;
                                  if (str8 == "A")
                                  {
                                    string str9 = "";
                                    string str10 = "";
                                    string str11 = "";
                                    string str12 = "";
                                    string str13 = "";
                                    str5 = "";
                                    if (list[index2].getDisplay() == list[index2].getDisplay().Replace("parent:", ""))
                                    {
                                      if (xmlNode5.ChildNodes[index1].Attributes.GetNamedItem(list[index2].getDisplay()) != null)
                                        str5 = xmlNode5.ChildNodes[index1].Attributes.GetNamedItem(list[index2].getDisplay()).Value;
                                    }
                                    else
                                    {
                                      string name = list[index2].getDisplay().Replace("parent:", "");
                                      if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                        str5 = xmlNode5.Attributes.GetNamedItem(name).Value;
                                    }
                                    if (list[index2].getIdCandidate() == list[index2].getIdCandidate().Replace("parent:", ""))
                                    {
                                      if (xmlNode5.ChildNodes[index1].Attributes.GetNamedItem(list[index2].getIdCandidate()) != null)
                                        str6 = xmlNode5.ChildNodes[index1].Attributes.GetNamedItem(list[index2].getIdCandidate()).Value;
                                    }
                                    else
                                    {
                                      string name = list[index2].getIdCandidate().Replace("parent:", "");
                                      if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                        str6 = xmlNode5.Attributes.GetNamedItem(name).Value;
                                    }
                                    if (list[index2].getImie2() == list[index2].getImie2().Replace("parent:", ""))
                                    {
                                      if (xmlNode5.ChildNodes[index1].Attributes.GetNamedItem(list[index2].getImie2()) != null)
                                        str10 = xmlNode5.ChildNodes[index1].Attributes.GetNamedItem(list[index2].getImie2()).Value;
                                    }
                                    else
                                    {
                                      string name = list[index2].getImie2().Replace("parent:", "");
                                      if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                        str10 = xmlNode5.Attributes.GetNamedItem(name).Value;
                                    }
                                    if (list[index2].getImie1() == list[index2].getImie1().Replace("parent:", ""))
                                    {
                                      if (xmlNode5.ChildNodes[index1].Attributes.GetNamedItem(list[index2].getImie1()) != null)
                                        str9 = xmlNode5.ChildNodes[index1].Attributes.GetNamedItem(list[index2].getImie1()).Value;
                                    }
                                    else
                                    {
                                      string name = list[index2].getImie1().Replace("parent:", "");
                                      if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                        str9 = xmlNode5.Attributes.GetNamedItem(name).Value;
                                    }
                                    if (list[index2].getNazwisko() == list[index2].getNazwisko().Replace("parent:", ""))
                                    {
                                      if (xmlNode5.ChildNodes[index1].Attributes.GetNamedItem(list[index2].getNazwisko()) != null)
                                        str11 = xmlNode5.ChildNodes[index1].Attributes.GetNamedItem(list[index2].getNazwisko()).Value;
                                    }
                                    else
                                    {
                                      string name = list[index2].getNazwisko().Replace("parent:", "");
                                      if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                        str11 = xmlNode5.Attributes.GetNamedItem(name).Value;
                                    }
                                    if (list[index2].getKomitet() == list[index2].getKomitet().Replace("parent:", ""))
                                    {
                                      if (xmlNode5.ChildNodes[index1].Attributes.GetNamedItem(list[index2].getKomitet()) != null)
                                        str12 = xmlNode5.ChildNodes[index1].Attributes.GetNamedItem(list[index2].getKomitet()).Value;
                                    }
                                    else
                                    {
                                      string name = list[index2].getKomitet().Replace("parent:", "");
                                      if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                        str12 = xmlNode5.Attributes.GetNamedItem(name).Value;
                                    }
                                    if (list[index2].getPlec() == list[index2].getPlec().Replace("parent:", ""))
                                    {
                                      if (xmlNode5.ChildNodes[index1].Attributes.GetNamedItem(list[index2].getPlec()) != null)
                                        str13 = !(xmlNode5.ChildNodes[index1].Attributes.GetNamedItem(list[index2].getPlec()).Value.ToUpper() == "M") ? list[index2].getName2v2() : list[index2].getName2();
                                    }
                                    else
                                    {
                                      string name = list[index2].getPlec().Replace("parent:", "");
                                      if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                        str13 = !(xmlNode5.Attributes.GetNamedItem(name).Value.ToUpper() == "M") ? list[index2].getName2v2() : list[index2].getName2();
                                    }
                                    string str14 = list[index2].getName1() + " " + str11 + " " + str9 + " " + str10 + " " + str13 + " " + str12;
                                    if (list[index2].getDataType() == "text" && list[index2].getSave() == "")
                                    {
                                      x = 10;
                                      label.Text = str14;
                                      label.AutoSize = true;
                                      label.MaximumSize = new Size(this.Form2panel.Width - 105, 0);
                                      label.Font = new Font(this.myfont, 9f);
                                      label.Location = new Point(x, y1);
                                      x = this.Form2panel.Width - 95;
                                      this.Form1panel.Controls.Add((Control) label);
                                    }
                                    if (list[index2].getDataType() == "number" && list[index2].getSave() != "")
                                    {
                                      textBox.Size = new Size(85, 20);
                                      string str15 = list[index2].getSave().Replace("X", num4.ToString()).Replace("Y", num3.ToString());
                                      textBox.Name = str15;
                                      XmlNode xmlNode6 = this.save.SelectSingleNode(str1 + "/" + textBox.Name);
                                      if (xmlNode6 != null)
                                        textBox.Text = xmlNode6.InnerText;
                                      textBox.Location = new Point(x, y1);
                                      textBox.MouseClick += new MouseEventHandler(this.Control_S1_MouseClick);
                                      textBox.LostFocus += new EventHandler(this.LostFocus);
                                      textBox.Validated += new EventHandler(this.number_Validated);
                                      try
                                      {
                                        this.typeValidation.Add(textBox.Name, "number");
                                      }
                                      catch (Exception ex)
                                      {
                                      }
                                      if (!(list[index2].getDisplay().ToLower() == "false"))
                                      {
                                        try
                                        {
                                          this.Form1panel.Controls.Add((Control) textBox);
                                        }
                                        catch (Exception ex)
                                        {
                                        }
                                      }
                                      if (label.Height > 20)
                                        y1 += label.Height + 5;
                                      else
                                        y1 += 25;
                                      try
                                      {
                                        this.range.Add(textBox.Name, list[index2].getRange(textBox.Name));
                                      }
                                      catch (Exception ex)
                                      {
                                      }
                                      this.candidatesRule[textBox.Name] = str6;
                                      str6 = "";
                                    }
                                  }
                                }
                                if (str7 == "A")
                                  ++num4;
                              }
                              this.countcandidatesoflist.Add(new int[2]
                              {
                                num3,
                                num4 - 1
                              });
                            }
                          }
                          ++num3;
                          y1 += 30;
                        }
                      }
                    }
                  }
                }
              }
            }
            if (namedItem1.Value == "additional-table")
            {
              x = 10;
              int y2 = y1 + 30;
              if (xmlNode3.Name == "fields")
              {
                foreach (XmlNode xmlNode4 in xmlNode3)
                {
                  string str5 = "";
                  bool flag1 = true;
                  bool flag2 = false;
                  XmlNode namedItem2 = xmlNode4.Attributes.GetNamedItem("enabled");
                  if (namedItem2 != null && namedItem2.Value == "false")
                  {
                    flag1 = false;
                    flag2 = true;
                  }
                  XmlNode namedItem3 = xmlNode4.Attributes.GetNamedItem("correspondence");
                  XmlNode namedItem4 = xmlNode4.Attributes.GetNamedItem("elseCorrespondenceDefault");
                  if (namedItem3 != null && (namedItem3.Value.ToLower() == "false" && this.correspondence || namedItem3.Value.ToLower() == "true" && !this.correspondence))
                  {
                    flag1 = false;
                    if (namedItem4 != null)
                      str5 = namedItem4.Value;
                  }
                  XmlNode namedItem5 = xmlNode4.Attributes.GetNamedItem("district");
                  XmlNode namedItem6 = xmlNode4.Attributes.GetNamedItem("districtDefault");
                  if (namedItem5 != null && namedItem5.Value.ToUpper() != "ALL")
                  {
                    if (Regex.IsMatch(namedItem5.Value.ToUpper(), "ONLY:") && namedItem5.Value.ToUpper().Replace("ONLY:", "") == this.typeObw.ToUpper())
                    {
                      flag1 = false;
                      if (namedItem6 != null)
                        str5 = namedItem6.Value;
                    }
                    if (Regex.IsMatch(namedItem5.Value.ToUpper(), "EXCLUDE:") && namedItem5.Value.ToUpper().Replace("EXCLUDE:", "") != this.typeObw.ToUpper())
                    {
                      flag1 = false;
                      if (namedItem6 != null)
                        str5 = namedItem6.Value;
                    }
                  }
                  XmlNode xmlNode5 = this.header.SelectSingleNode("/akcja_wyborcza/jns");
                  XmlNode namedItem7 = xmlNode4.Attributes.GetNamedItem("valueName");
                  if (namedItem7 != null)
                  {
                    if (namedItem7.Value == "algorytmOKW")
                    {
                      foreach (XmlNode xmlNode6 in xmlNode5)
                      {
                        if (xmlNode6.Name == "obw" && xmlNode6.Attributes.GetNamedItem("nr") != null && xmlNode6.Attributes.GetNamedItem("nr").Value == str2)
                        {
                          foreach (XmlNode xmlNode7 in xmlNode6)
                          {
                            if (xmlNode7.Name == "inst" && xmlNode7.Attributes.GetNamedItem("kod") != null && xmlNode7.Attributes.GetNamedItem("kod").Value == str3)
                            {
                              foreach (XmlNode xmlNode8 in xmlNode7)
                              {
                                if (xmlNode8.Name == "okr" && xmlNode8.Attributes.GetNamedItem("nr") != null && xmlNode8.Attributes.GetNamedItem("nr").Value == str4 && xmlNode8.Attributes.GetNamedItem("siedziba") != null)
                                  str5 = xmlNode8.Attributes.GetNamedItem("siedziba").Value;
                              }
                            }
                          }
                        }
                      }
                    }
                    if (namedItem7.Value == "siedzibaObwod")
                    {
                      foreach (XmlNode xmlNode6 in xmlNode5)
                      {
                        if (xmlNode6.Attributes.GetNamedItem("nr") != null && xmlNode6.Attributes.GetNamedItem("nr").Value == str2 && xmlNode6.Attributes.GetNamedItem("siedziba") != null)
                          str5 = xmlNode6.Attributes.GetNamedItem("siedziba").Value;
                      }
                    }
                    if (namedItem7.Value == "nr")
                      str5 = str2;
                  }
                  if (namedItem7 != null && Regex.IsMatch(namedItem7.Value, "\\+"))
                  {
                    string str6 = "";
                    string str7 = namedItem7.Value;
                    if (Regex.IsMatch(namedItem7.Value, "header:"))
                    {
                      str7 = namedItem7.Value.Replace("header:", "");
                      str6 = "header";
                    }
                    if (Regex.IsMatch(namedItem7.Value, "calculator:"))
                    {
                      str7 = namedItem7.Value.Replace("calculator:", "");
                      str6 = "calculator";
                    }
                    if (Regex.IsMatch(namedItem7.Value, "additional-calculator:"))
                    {
                      str7 = namedItem7.Value.Replace("additional-calculator:", "");
                      str6 = "additional-calculator";
                    }
                    if (Regex.IsMatch(namedItem7.Value, "additional-table:"))
                    {
                      str7 = namedItem7.Value.Replace("additional-table:", "");
                      str6 = "additional-table";
                    }
                    string[] strArray2 = str7.Split('+');
                    str5 = "";
                    for (int index = 0; index < strArray2.Length; ++index)
                    {
                      if (index != 0)
                        str5 = str5 + ", ";
                      if (str6 == "header")
                        str5 = str5 + this.protocolHeader.Controls[strArray2[index]].Text;
                      if (str6 == "calculator" || str6 == "additional-calculator" || str6 == "additional-table")
                        str5 = str5 + this.Form1panel.Controls[strArray2[index]].Text;
                    }
                  }
                  if (xmlNode4.Name == "title")
                  {
                    Label label = new Label();
                    label.Text = xmlNode4.InnerText;
                    label.AutoSize = true;
                    label.MaximumSize = new Size(this.Form1panel.Size.Width - 20, 0);
                    label.Font = new Font(this.myfont, 10f, FontStyle.Bold);
                    label.Location = new Point(x, y2);
                    this.Form1panel.Controls.Add((Control) label);
                    y2 += label.Height + 30;
                  }
                  if (xmlNode4.Name == "description")
                  {
                    x = 10;
                    Label label = new Label();
                    label.Text = xmlNode4.InnerText;
                    label.AutoSize = true;
                    label.MaximumSize = new Size(this.Form1panel.Size.Width - 20, 0);
                    label.Font = new Font(this.myfont, 9f, FontStyle.Bold);
                    label.Location = new Point(x, y2 - 15);
                    this.Form1panel.Controls.Add((Control) label);
                    y2 += label.Height + 25;
                  }
                  if (xmlNode4.Name == "note" && xmlNode4.InnerText != "")
                  {
                    x = 10;
                    Label label1 = new Label();
                    label1.Text = "Uwaga";
                    label1.AutoSize = true;
                    label1.MaximumSize = new Size(100, 0);
                    label1.Font = new Font(this.myfont, 9f, FontStyle.Bold);
                    label1.Location = new Point(x, y2);
                    this.Form1panel.Controls.Add((Control) label1);
                    x += label1.Width + 5;
                    this.Form1panel.Controls.Add((Control) label1);
                    Label label2 = new Label();
                    label2.Text = xmlNode4.InnerText;
                    label2.AutoSize = true;
                    label2.MaximumSize = new Size(this.Form1panel.Size.Width - 120, 0);
                    label2.Font = new Font(this.myfont, 9f);
                    label2.Location = new Point(x, y2);
                    this.Form1panel.Controls.Add((Control) label2);
                    y2 += label2.Height + 25;
                  }
                  if (xmlNode4.Name == "field")
                  {
                    x = 10;
                    Label label1 = new Label();
                    TextBox textBox = new TextBox();
                    foreach (XmlNode xmlNode6 in xmlNode4)
                    {
                      if (xmlNode6.Name == "name")
                      {
                        if (xmlNode6.ParentNode.Attributes.GetNamedItem("lp") != null)
                          label1.Text = xmlNode6.ParentNode.Attributes.GetNamedItem("lp").Value + " ";
                        Label label2 = label1;
                        string str6 = label2.Text + xmlNode6.InnerText;
                        label2.Text = str6;
                        label1.AutoSize = true;
                        label1.MaximumSize = new Size(num1 - num2, 0);
                        label1.Font = new Font(this.myfont, 9f);
                        label1.Location = new Point(x, y2);
                        x = num1 - 95;
                      }
                      if (xmlNode6.Name == "save_as")
                      {
                        int width = 85;
                        XmlNode namedItem8 = xmlNode6.ParentNode.Attributes.GetNamedItem("data");
                        textBox.Name = xmlNode6.InnerText;
                        if (namedItem8 != null && namedItem8.Value == "number")
                        {
                          textBox.Validated += new EventHandler(this.number_Validated);
                          try
                          {
                            this.typeValidation.Add(textBox.Name, "number");
                          }
                          catch (ArgumentException ex)
                          {
                          }
                        }
                        if (namedItem8 != null && namedItem8.Value == "text")
                        {
                          textBox.Validated += new EventHandler(this.text_Validated);
                          try
                          {
                            this.typeValidation.Add(textBox.Name, "text");
                          }
                          catch (ArgumentException ex)
                          {
                          }
                          label1.MaximumSize = new Size(num1 - num2 * 2, 0);
                          width = 170;
                          label1.AutoSize = true;
                          x = num1 - num2 * 2 + 20;
                        }
                        textBox.Size = new Size(width, 20);
                        if (str5 != "")
                          textBox.Text = str5;
                        XmlNode xmlNode7 = this.save.SelectSingleNode(str1 + "/" + xmlNode6.InnerText);
                        if (xmlNode7 != null)
                          textBox.Text = xmlNode7.InnerText;
                        textBox.MouseClick += new MouseEventHandler(this.Control_S1_MouseClick);
                        textBox.LostFocus += new EventHandler(this.LostFocus);
                        textBox.Enabled = flag1;
                        textBox.Location = new Point(x, y2);
                        textBox.CausesValidation = true;
                        label1.Name = "Lab_" + xmlNode6.InnerText;
                        XmlNode namedItem9 = xmlNode6.ParentNode.Attributes.GetNamedItem("min");
                        XmlNode namedItem10 = xmlNode6.ParentNode.Attributes.GetNamedItem("max");
                        if (namedItem9 != null && namedItem10 != null)
                          this.range.Add(textBox.Name, new ValidationRange(textBox.Name, Convert.ToInt32(namedItem9.Value), Convert.ToInt32(namedItem10.Value)));
                        if (flag2)
                          this.controlsCanBeNull.Add(textBox.Name);
                      }
                    }
                    this.Form1panel.Controls.Add((Control) label1);
                    this.Form1panel.Controls.Add((Control) textBox);
                    if (label1.Height > 20)
                      y2 += label1.Height + 5;
                    else
                      y2 += 25;
                  }
                }
              }
              y1 = y2 + 50;
            }
          }
          else
            break;
        }
        this.buttonNext.Text = "Dalej";
        this.Click += new EventHandler(this.protocolForm2_Click);
      }
      catch (XmlException ex)
      {
        int num3 = (int) MessageBox.Show("Nieprawidłowy XML - " + ex.Message, "Error");
      }
      catch (NullReferenceException ex)
      {
        int num3 = (int) MessageBox.Show("Podanno inny xml definicje wygladu - " + ex.Message, "Błąd");
      }
      this.Form1panel.Location = new Point(this.protocolHeader.Location.X, this.protocolHeader.Location.Y + this.protocolHeader.Size.Height + 50);
      this.buttonNext.Text = "Dalej";
      this.buttonNext.Click -= new EventHandler(this.protocolForm2_Click);
      this.buttonNext.Click -= new EventHandler(this.protocolSummation_Click);
      this.buttonNext.Click -= new EventHandler(this.committee_Click);
      this.buttonNext.Click -= new EventHandler(this.committeeValid_Click);
      this.buttonNext.Click -= new EventHandler(this.signProtocol_Click);
      this.buttonNext.Click += new EventHandler(this.protocolForm2_Click);
      Panel panel = this.bottomPanel;
      Point location = this.Form1panel.Location;
      int x1 = location.X;
      location = this.Form1panel.Location;
      int y = location.Y + this.Form1panel.Size.Height;
      Point point = new Point(x1, y);
      panel.Location = point;
      this.bottomPanel.Visible = true;
      if (this.lastValidators.Count == 0)
        this.getDefinitionLastValidation();
      this.buttonNext.Enabled = true;
      this.wait.setVisible(false);
    }

    private void getCalculator2()
    {
      this.wait.setWaitPanel("Trwa ładowanie drugiego kroku protokołu", "Proszę czekać");
      this.wait.setVisible(true);
      this.Form2panel.Controls.Clear();
      string str1 = "";
      if (this.isSave() == 0 || this.isSave() == 4)
        str1 = "/save/form";
      if (this.isSave() == 2 || this.isSave() == 3)
        str1 = "/save/step2";
      int num1 = this.Form1panel.Controls.Count * 2;
      string[] strArray1 = this.savePath.Split('-');
      string str2;
      string str3;
      string str4;
      if (strArray1.Length == 6)
      {
        str2 = strArray1[2].Replace("Obw", "");
        str3 = strArray1[3].Replace("Inst", "");
        str4 = strArray1[5].Replace("Okr", "").Replace(".xml", "");
      }
      else
      {
        str2 = strArray1[strArray1.Length - 4].Replace("Obw", "");
        str3 = strArray1[strArray1.Length - 3].Replace("Inst", "");
        str4 = strArray1[strArray1.Length - 1].Replace("Okr", "").Replace(".xml", "");
      }
      foreach (Control control in (ArrangedElementCollection) this.protocolHeader.Controls)
      {
        if (control is TextBox)
        {
          (control as TextBox).TabIndex = num1;
          --num1;
        }
        if (control is MaskedTextBox)
        {
          (control as MaskedTextBox).TabIndex = num1;
          --num1;
        }
      }
      this.Form2panel.TabIndex = 2;
      this.protocolHeader.TabIndex = 3;
      int num2 = 760;
      int num3 = 105;
      try
      {
        XmlNode xmlNode1 = this.protocolDefinition.SelectSingleNode("/protokol_info");
        XmlNode xmlNode2 = this.candidates.SelectSingleNode("/listy");
        int x = 0;
        int y1 = 0;
        foreach (XmlNode xmlNode3 in xmlNode1)
        {
          XmlNode namedItem1 = xmlNode3.Attributes.GetNamedItem("type");
          if (namedItem1 != null)
          {
            this.wait.setWaitPanel("Trwa ładowanie drugiego kroku protokołu - ładowanie pol kalkulatora", "Proszę czekać");
            if (namedItem1.Value == "calculator")
            {
              x = 10;
              if (xmlNode3.Name == "fields")
              {
                foreach (XmlNode xmlNode4 in xmlNode3)
                {
                  if (xmlNode4.Name == "title")
                  {
                    Label label = new Label();
                    label.Text = xmlNode4.InnerText;
                    label.AutoSize = true;
                    label.MaximumSize = new Size(780, 0);
                    label.Font = new Font(this.myfont, 10f, FontStyle.Bold);
                    label.Location = new Point(x, y1);
                    this.Form2panel.Controls.Add((Control) label);
                    y1 += label.Height + 30;
                  }
                  if (xmlNode4.Name == "description")
                  {
                    x = 10;
                    Label label = new Label();
                    label.Text = xmlNode4.InnerText;
                    label.AutoSize = true;
                    label.MaximumSize = new Size(700, 0);
                    label.Font = new Font(this.myfont, 9f, FontStyle.Bold);
                    label.Location = new Point(x, y1);
                    this.Form2panel.Controls.Add((Control) label);
                    y1 += label.Height + 25;
                  }
                  if (xmlNode4.Name == "note")
                  {
                    x = 10;
                    Label label1 = new Label();
                    label1.Text = "Uwaga";
                    label1.AutoSize = true;
                    label1.MaximumSize = new Size(100, 0);
                    label1.Font = new Font(this.myfont, 9f, FontStyle.Bold);
                    label1.Location = new Point(x, y1);
                    this.Form2panel.Controls.Add((Control) label1);
                    x += label1.Width + 5;
                    this.Form2panel.Controls.Add((Control) label1);
                    Label label2 = new Label();
                    label2.Text = xmlNode4.InnerText;
                    label2.AutoSize = true;
                    label2.MaximumSize = new Size(700, 0);
                    label2.Font = new Font(this.myfont, 9f);
                    label2.Location = new Point(x, y1);
                    this.Form2panel.Controls.Add((Control) label2);
                    y1 += label2.Height + 25;
                  }
                  if (xmlNode4.Name == "field")
                  {
                    string str5 = "";
                    bool flag = true;
                    XmlNode namedItem2 = xmlNode4.Attributes.GetNamedItem("correspondence");
                    XmlNode namedItem3 = xmlNode4.Attributes.GetNamedItem("elseCorrespondenceDefault");
                    if (namedItem2 != null && (namedItem2.Value.ToLower() == "false" && this.correspondence || namedItem2.Value.ToLower() == "true" && !this.correspondence))
                    {
                      flag = false;
                      if (namedItem3 != null)
                        str5 = namedItem3.Value;
                    }
                    XmlNode namedItem4 = xmlNode4.Attributes.GetNamedItem("district");
                    XmlNode namedItem5 = xmlNode4.Attributes.GetNamedItem("districtDefault");
                    if (namedItem4 != null && namedItem4.Value.ToUpper() != "ALL")
                    {
                      if (Regex.IsMatch(namedItem4.Value.ToUpper(), "ONLY:") && namedItem4.Value.ToUpper().Replace("ONLY:", "") == this.typeObw.ToUpper())
                      {
                        flag = false;
                        if (namedItem5 != null)
                          str5 = namedItem5.Value;
                      }
                      if (Regex.IsMatch(namedItem4.Value.ToUpper(), "EXCLUDE:") && namedItem4.Value.ToUpper().Replace("EXCLUDE:", "") != this.typeObw.ToUpper())
                      {
                        flag = false;
                        if (namedItem5 != null)
                          str5 = namedItem5.Value;
                      }
                    }
                    x = 10;
                    Label label = new Label();
                    TextBox textBox = new TextBox();
                    foreach (XmlNode xmlNode5 in xmlNode4)
                    {
                      if (xmlNode5.Name == "name")
                      {
                        label.Text = xmlNode5.ParentNode.Attributes.GetNamedItem("lp").Value + " " + xmlNode5.InnerText;
                        label.AutoSize = true;
                        label.MaximumSize = new Size(num2 - num3, 0);
                        label.Font = new Font(this.myfont, 9f);
                        label.Location = new Point(x, y1);
                        x = num2 - 95;
                      }
                      if (xmlNode5.Name == "save_as")
                      {
                        textBox.Size = new Size(85, 20);
                        textBox.Name = xmlNode5.InnerText;
                        textBox.Location = new Point(x, y1);
                        textBox.MouseClick += new MouseEventHandler(this.Control_S2_MouseClick);
                        textBox.LostFocus += new EventHandler(this.LostFocus);
                        textBox.CausesValidation = true;
                        textBox.TabIndex = num1;
                        --num1;
                        textBox.Enabled = flag;
                        textBox.Text = str5;
                        XmlNode xmlNode6 = this.save.SelectSingleNode(str1 + "/" + textBox.Name);
                        if (xmlNode6 != null && flag)
                          textBox.Text = xmlNode6.InnerText;
                        XmlNode namedItem6 = xmlNode5.ParentNode.Attributes.GetNamedItem("data");
                        if (namedItem6 != null && namedItem6.Value == "number")
                          textBox.Validated += new EventHandler(this.number_Validated);
                        label.Name = "Lab_" + xmlNode5.InnerText;
                      }
                    }
                    this.Form2panel.Controls.Add((Control) label);
                    this.Form2panel.Controls.Add((Control) textBox);
                    if (label.Height > 20)
                      y1 += label.Height + 5;
                    else
                      y1 += 25;
                  }
                }
              }
              y1 += 50;
            }
            this.wait.setWaitPanel("Trwa ładowanie drugiego kroku protokołu - ładowanie kandydatów", "Proszę czekać");
            if (namedItem1 != null && namedItem1.Value == "additional-calculator")
            {
              foreach (XmlNode xmlNode4 in xmlNode3)
              {
                if (xmlNode4.Name == "field")
                {
                  XmlNode namedItem2 = xmlNode4.Attributes.GetNamedItem("data");
                  if (namedItem2 != null && namedItem2.Value == "kandydaci")
                  {
                    foreach (XmlNode itemChild1 in xmlNode4)
                    {
                      if (itemChild1.Name == "name")
                      {
                        x = 10;
                        Label label = new Label();
                        label.Text = itemChild1.ParentNode.Attributes.GetNamedItem("lp").Value + " " + itemChild1.InnerText;
                        label.AutoSize = true;
                        label.MaximumSize = new Size(num2 - 5, 0);
                        label.MinimumSize = new Size(num2 - 5, 0);
                        label.Font = new Font(this.myfont, 9f);
                        label.Location = new Point(x, y1);
                        this.Form2panel.Controls.Add((Control) label);
                        y1 += label.Size.Height + 30;
                      }
                      if (xmlNode4.Name == "note")
                      {
                        x = 10;
                        Label label1 = new Label();
                        label1.Text = "Uwaga";
                        label1.AutoSize = true;
                        label1.MaximumSize = new Size(100, 0);
                        label1.Font = new Font(this.myfont, 9f, FontStyle.Bold);
                        label1.Location = new Point(x, y1);
                        this.Form2panel.Controls.Add((Control) label1);
                        x += label1.Width + 5;
                        this.Form2panel.Controls.Add((Control) label1);
                        Label label2 = new Label();
                        label2.Text = xmlNode4.InnerText;
                        label2.AutoSize = true;
                        label2.MaximumSize = new Size(num2 - 100, 0);
                        label2.Font = new Font(this.myfont, 9f);
                        label2.Location = new Point(x, y1);
                        this.Form2panel.Controls.Add((Control) label2);
                        y1 += label2.Height + 25;
                      }
                      string str5;
                      if (itemChild1.Name == "patternrows")
                      {
                        List<Field> patternField = new List<Field>();
                        List<Field> list = this.readPatternCandidate(itemChild1, patternField);
                        int num4 = 1;
                        string str6 = "";
                        foreach (XmlNode xmlNode5 in xmlNode2)
                        {
                          Label label = new Label();
                          TextBox textBox = new TextBox();
                          string str7 = "A";
                          for (int index = 0; index < list.Count; ++index)
                          {
                            string str8 = "A";
                            if (list[index].getStatus() == list[index].getStatus().Replace("parent:", ""))
                            {
                              if (xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getStatus()) != null)
                                str8 = xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getStatus()).Value;
                            }
                            else
                            {
                              string name = list[index].getStatus().Replace("parent:", "");
                              if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                str8 = xmlNode5.Attributes.GetNamedItem(name).Value;
                            }
                            str7 = str8;
                            if (str8 == "A")
                            {
                              string str9 = "";
                              string str10 = "";
                              string str11 = "";
                              string str12 = "";
                              string str13 = "";
                              string str14 = "";
                              str5 = "";
                              if (list[index].getDisplay() == list[index].getDisplay().Replace("parent:", ""))
                              {
                                if (xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getDisplay()) != null)
                                  str5 = xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getDisplay()).Value;
                              }
                              else
                              {
                                string name = list[index].getDisplay().Replace("parent:", "");
                                if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                  str5 = xmlNode5.Attributes.GetNamedItem(name).Value;
                              }
                              if (list[index].getIdCandidate() == list[index].getIdCandidate().Replace("parent:", ""))
                              {
                                if (xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getIdCandidate()) != null)
                                  str6 = xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getIdCandidate()).Value;
                              }
                              else
                              {
                                string name = list[index].getIdCandidate().Replace("parent:", "");
                                if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                  str6 = xmlNode5.Attributes.GetNamedItem(name).Value;
                              }
                              if (list[index].getImie2() == list[index].getImie2().Replace("parent:", ""))
                              {
                                if (xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getImie2()) != null)
                                  str10 = xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getImie2()).Value;
                              }
                              else
                              {
                                string name = list[index].getImie2().Replace("parent:", "");
                                if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                  str10 = xmlNode5.Attributes.GetNamedItem(name).Value;
                              }
                              if (list[index].getImie1() == list[index].getImie1().Replace("parent:", ""))
                              {
                                if (xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getImie1()) != null)
                                  str9 = xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getImie1()).Value;
                              }
                              else
                              {
                                string name = list[index].getImie1().Replace("parent:", "");
                                if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                  str9 = xmlNode5.Attributes.GetNamedItem(name).Value;
                              }
                              if (list[index].getNazwisko() == list[index].getNazwisko().Replace("parent:", ""))
                              {
                                if (xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getNazwisko()) != null)
                                  str11 = xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getNazwisko()).Value;
                              }
                              else
                              {
                                string name = list[index].getNazwisko().Replace("parent:", "");
                                if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                  str11 = xmlNode5.Attributes.GetNamedItem(name).Value;
                              }
                              if (list[index].getKomitet() == list[index].getKomitet().Replace("parent:", ""))
                              {
                                if (xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getKomitet()) != null)
                                  str12 = xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getKomitet()).Value;
                              }
                              else
                              {
                                string name = list[index].getKomitet().Replace("parent:", "");
                                if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                  str12 = xmlNode5.Attributes.GetNamedItem(name).Value;
                              }
                              string str15 = "";
                              if (list[index].getLista() == list[index].getLista().Replace("parent:", ""))
                              {
                                if (xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getLista()) != null)
                                  str15 = xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getLista()).Value;
                              }
                              else
                              {
                                string name = list[index].getLista().Replace("parent:", "");
                                if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                  str15 = xmlNode5.Attributes.GetNamedItem(name).Value;
                              }
                              if (str15 != "")
                                str14 = list[index].getName3() + str15;
                              if (list[index].getPlec() == list[index].getPlec().Replace("parent:", ""))
                              {
                                if (xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getPlec()) != null)
                                  str13 = !(xmlNode5.FirstChild.Attributes.GetNamedItem(list[index].getPlec()).Value.ToUpper() == "M") ? list[index].getName2v2() : list[index].getName2();
                              }
                              else
                              {
                                string name = list[index].getPlec().Replace("parent:", "");
                                if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                  str13 = !(xmlNode5.Attributes.GetNamedItem(name).Value.ToUpper() == "M") ? list[index].getName2v2() : list[index].getName2();
                              }
                              string str16 = list[index].getName1() + " " + str11 + " " + str9 + " " + str10 + " " + str13 + " " + str12 + str14;
                              if (list[index].getDataType() == "text" && list[index].getSave() == "")
                              {
                                x = 10;
                                label.Text = str16;
                                label.AutoSize = true;
                                label.MaximumSize = new Size(num2 - 105, 0);
                                label.Font = new Font(this.myfont, 9f);
                                label.Location = new Point(x, y1);
                                x = num2 - 95;
                                this.Form2panel.Controls.Add((Control) label);
                              }
                              if (list[index].getDataType() == "number" && list[index].getSave() != "")
                              {
                                textBox.Size = new Size(85, 20);
                                textBox.Name = list[index].getSave().Replace("X", num4.ToString());
                                textBox.Location = new Point(x, y1);
                                textBox.MouseClick += new MouseEventHandler(this.Control_S1_MouseClick);
                                textBox.LostFocus += new EventHandler(this.LostFocus);
                                textBox.Validated += new EventHandler(this.number_Validated);
                                textBox.TabIndex = num1;
                                --num1;
                                XmlNode xmlNode6 = this.save.SelectSingleNode(str1 + "/" + textBox.Name);
                                if (xmlNode6 != null)
                                  textBox.Text = xmlNode6.InnerText;
                                if (!(list[index].getDisplay().ToLower() == "false"))
                                  this.Form2panel.Controls.Add((Control) textBox);
                                if (label.Height > 20)
                                  y1 += label.Height + 5;
                                else
                                  y1 += 25;
                                this.candidatesRule[textBox.Name] = str6;
                                str6 = "";
                              }
                            }
                          }
                          if (str7 == "A")
                            ++num4;
                        }
                        this.countcandidatesoflist.Add(new int[2]
                        {
                          0,
                          num4 - 1
                        });
                      }
                      if (itemChild1.Name == "patternrow")
                      {
                        List<Field> patternField = new List<Field>();
                        List<Field> list = this.readPatternCandidate(itemChild1, patternField);
                        for (int index = 0; index < list.Count; ++index)
                        {
                          if (!list[index].needImportData())
                          {
                            x = 10;
                            Label label = new Label();
                            label.Text = list[index].getName1();
                            label.AutoSize = true;
                            label.MaximumSize = new Size(num2 - 105, 0);
                            label.Font = new Font(this.myfont, 9f);
                            label.Location = new Point(x, y1);
                            x = num2 - 95;
                            this.Form2panel.Controls.Add((Control) label);
                            TextBox textBox = new TextBox();
                            textBox.Size = new Size(85, 20);
                            textBox.Name = list[index].getSave();
                            textBox.Location = new Point(x, y1);
                            textBox.MouseClick += new MouseEventHandler(this.Control_S1_MouseClick);
                            textBox.LostFocus += new EventHandler(this.LostFocus);
                            textBox.Validated += new EventHandler(this.number_Validated);
                            textBox.TabIndex = num1;
                            --num1;
                            XmlNode xmlNode5 = this.save.SelectSingleNode(str1 + "/" + textBox.Name);
                            if (xmlNode5 != null)
                              textBox.Text = xmlNode5.InnerText;
                            this.Form2panel.Controls.Add((Control) textBox);
                            if (label.Height > 20)
                              y1 += label.Height + 5;
                            else
                              y1 += 25;
                          }
                        }
                      }
                      if (itemChild1.Name == "patternlist")
                      {
                        int num4 = 1;
                        foreach (XmlNode xmlNode5 in xmlNode2)
                        {
                          foreach (XmlNode itemChild2 in itemChild1)
                          {
                            if (itemChild2.Name == "title")
                            {
                              x = 0;
                              XmlNode namedItem3 = itemChild2.Attributes.GetNamedItem("bold");
                              XmlNode namedItem4 = itemChild2.Attributes.GetNamedItem("nr");
                              XmlNode namedItem5 = itemChild2.Attributes.GetNamedItem("komitet");
                              XmlNode namedItem6 = itemChild2.Attributes.GetNamedItem("lista");
                              string str6 = "";
                              if (namedItem5 != null)
                              {
                                XmlNode namedItem7 = xmlNode5.Attributes.GetNamedItem(namedItem5.Value);
                                if (namedItem7 != null && namedItem7.Value != "")
                                  str6 = namedItem7.Value;
                              }
                              string str7 = "";
                              if (namedItem6 != null)
                              {
                                XmlNode namedItem7 = xmlNode5.Attributes.GetNamedItem(namedItem6.Value);
                                if (namedItem7 != null && namedItem7.Value != "")
                                  str7 = namedItem7.Value;
                              }
                              if (namedItem4 != null)
                                str6 = namedItem4.Value + " " + str7 + " " + itemChild2.InnerText + str6;
                              Label label1 = new Label();
                              label1.Text = str6;
                              label1.AutoSize = true;
                              Label label2 = label1;
                              Size size1 = this.Form2panel.Size;
                              Size size2 = new Size(size1.Width, 0);
                              label2.MaximumSize = size2;
                              label1.Font = new Font(this.myfont, 10f);
                              label1.Padding = new Padding(10, 0, 10, 0);
                              if (namedItem3 != null && namedItem3.Value == "true")
                                label1.Font = new Font(this.myfont, 10f, FontStyle.Bold);
                              label1.Location = new Point(x, y1);
                              this.Form2panel.Controls.Add((Control) label1);
                              int num5 = y1;
                              size1 = label1.Size;
                              int num6 = size1.Height + 30;
                              y1 = num5 + num6;
                            }
                            if (itemChild2.Name == "patternrow")
                            {
                              List<Field> patternField = new List<Field>();
                              List<Field> list = this.readPatternCandidate(itemChild2, patternField);
                              for (int index = 0; index < list.Count; ++index)
                              {
                                if (!list[index].needImportData())
                                {
                                  x = 10;
                                  Label label = new Label();
                                  label.Text = list[index].getName1();
                                  label.AutoSize = true;
                                  label.MaximumSize = new Size(this.Form2panel.Width - 105, 0);
                                  label.Font = new Font(this.myfont, 9f);
                                  label.Location = new Point(x, y1);
                                  x = this.Form2panel.Width - 95;
                                  this.Form2panel.Controls.Add((Control) label);
                                  TextBox textBox = new TextBox();
                                  textBox.Size = new Size(85, 20);
                                  textBox.TabIndex = num1;
                                  --num1;
                                  textBox.Name = list[index].getSave().Replace("Y", num4.ToString());
                                  XmlNode xmlNode6 = this.save.SelectSingleNode(str1 + "/" + textBox.Name);
                                  if (xmlNode6 != null)
                                    textBox.Text = xmlNode6.InnerText;
                                  textBox.Location = new Point(x, y1);
                                  textBox.MouseClick += new MouseEventHandler(this.Control_S1_MouseClick);
                                  textBox.LostFocus += new EventHandler(this.LostFocus);
                                  textBox.Validated += new EventHandler(this.number_Validated);
                                  this.Form2panel.Controls.Add((Control) textBox);
                                  if (label.Height > 20)
                                    y1 += label.Height + 5;
                                  else
                                    y1 += 25;
                                }
                              }
                            }
                            if (itemChild2.Name == "patternrows")
                            {
                              List<Field> patternField = new List<Field>();
                              List<Field> list = this.readPatternCandidate(itemChild2, patternField);
                              int num5 = 1;
                              for (int index1 = 0; index1 < xmlNode5.ChildNodes.Count; ++index1)
                              {
                                string str6 = "";
                                Label label = new Label();
                                TextBox textBox = new TextBox();
                                string str7 = "A";
                                for (int index2 = 0; index2 < list.Count; ++index2)
                                {
                                  string str8 = "A";
                                  if (list[index2].getStatus() == list[index2].getStatus().Replace("parent:", ""))
                                  {
                                    if (xmlNode5.ChildNodes[index1].Attributes.GetNamedItem(list[index2].getStatus()) != null)
                                      str8 = xmlNode5.ChildNodes[index1].Attributes.GetNamedItem(list[index2].getStatus()).Value;
                                  }
                                  else
                                  {
                                    string name = list[index2].getStatus().Replace("parent:", "");
                                    if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                      str8 = xmlNode5.Attributes.GetNamedItem(name).Value;
                                  }
                                  str7 = str8;
                                  if (str8 == "A")
                                  {
                                    string str9 = "";
                                    string str10 = "";
                                    string str11 = "";
                                    string str12 = "";
                                    string str13 = "";
                                    str5 = "";
                                    if (list[index2].getDisplay() == list[index2].getDisplay().Replace("parent:", ""))
                                    {
                                      if (xmlNode5.ChildNodes[index1].Attributes.GetNamedItem(list[index2].getDisplay()) != null)
                                        str5 = xmlNode5.ChildNodes[index1].Attributes.GetNamedItem(list[index2].getDisplay()).Value;
                                    }
                                    else
                                    {
                                      string name = list[index2].getDisplay().Replace("parent:", "");
                                      if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                        str5 = xmlNode5.Attributes.GetNamedItem(name).Value;
                                    }
                                    if (list[index2].getIdCandidate() == list[index2].getIdCandidate().Replace("parent:", ""))
                                    {
                                      if (xmlNode5.ChildNodes[index1].Attributes.GetNamedItem(list[index2].getIdCandidate()) != null)
                                        str6 = xmlNode5.ChildNodes[index1].Attributes.GetNamedItem(list[index2].getIdCandidate()).Value;
                                    }
                                    else
                                    {
                                      string name = list[index2].getIdCandidate().Replace("parent:", "");
                                      if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                        str6 = xmlNode5.Attributes.GetNamedItem(name).Value;
                                    }
                                    if (list[index2].getImie2() == list[index2].getImie2().Replace("parent:", ""))
                                    {
                                      if (xmlNode5.ChildNodes[index1].Attributes.GetNamedItem(list[index2].getImie2()) != null)
                                        str10 = xmlNode5.ChildNodes[index1].Attributes.GetNamedItem(list[index2].getImie2()).Value;
                                    }
                                    else
                                    {
                                      string name = list[index2].getImie2().Replace("parent:", "");
                                      if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                        str10 = xmlNode5.Attributes.GetNamedItem(name).Value;
                                    }
                                    if (list[index2].getImie1() == list[index2].getImie1().Replace("parent:", ""))
                                    {
                                      if (xmlNode5.ChildNodes[index1].Attributes.GetNamedItem(list[index2].getImie1()) != null)
                                        str9 = xmlNode5.ChildNodes[index1].Attributes.GetNamedItem(list[index2].getImie1()).Value;
                                    }
                                    else
                                    {
                                      string name = list[index2].getImie1().Replace("parent:", "");
                                      if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                        str9 = xmlNode5.Attributes.GetNamedItem(name).Value;
                                    }
                                    if (list[index2].getNazwisko() == list[index2].getNazwisko().Replace("parent:", ""))
                                    {
                                      if (xmlNode5.ChildNodes[index1].Attributes.GetNamedItem(list[index2].getNazwisko()) != null)
                                        str11 = xmlNode5.ChildNodes[index1].Attributes.GetNamedItem(list[index2].getNazwisko()).Value;
                                    }
                                    else
                                    {
                                      string name = list[index2].getNazwisko().Replace("parent:", "");
                                      if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                        str11 = xmlNode5.Attributes.GetNamedItem(name).Value;
                                    }
                                    if (list[index2].getKomitet() == list[index2].getKomitet().Replace("parent:", ""))
                                    {
                                      if (xmlNode5.ChildNodes[index1].Attributes.GetNamedItem(list[index2].getKomitet()) != null)
                                        str13 = xmlNode5.ChildNodes[index1].Attributes.GetNamedItem(list[index2].getKomitet()).Value;
                                    }
                                    else
                                    {
                                      string name = list[index2].getKomitet().Replace("parent:", "");
                                      if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                        str13 = xmlNode5.Attributes.GetNamedItem(name).Value;
                                    }
                                    if (list[index2].getPlec() == list[index2].getPlec().Replace("parent:", ""))
                                    {
                                      if (xmlNode5.ChildNodes[index1].Attributes.GetNamedItem(list[index2].getPlec()) != null)
                                        str12 = !(xmlNode5.ChildNodes[index1].Attributes.GetNamedItem(list[index2].getPlec()).Value.ToUpper() == "M") ? list[index2].getName2v2() : list[index2].getName2();
                                    }
                                    else
                                    {
                                      string name = list[index2].getPlec().Replace("parent:", "");
                                      if (xmlNode5.Attributes.GetNamedItem(name) != null)
                                        str12 = !(xmlNode5.Attributes.GetNamedItem(name).Value.ToUpper() == "M") ? list[index2].getName2v2() : list[index2].getName2();
                                    }
                                    string str14 = list[index2].getName1() + " " + str11 + " " + str9 + " " + str10 + " " + str12 + " " + str13;
                                    if (list[index2].getDataType() == "text" && list[index2].getSave() == "")
                                    {
                                      x = 10;
                                      label.Text = str14;
                                      label.AutoSize = true;
                                      label.MaximumSize = new Size(this.Form2panel.Width - 105, 0);
                                      label.Font = new Font(this.myfont, 9f);
                                      label.Location = new Point(x, y1);
                                      x = this.Form2panel.Width - 95;
                                      this.Form2panel.Controls.Add((Control) label);
                                    }
                                    if (list[index2].getDataType() == "number" && list[index2].getSave() != "")
                                    {
                                      textBox.Size = new Size(85, 20);
                                      textBox.Name = list[index2].getSave().Replace("X", num5.ToString()).Replace("Y", num4.ToString());
                                      XmlNode xmlNode6 = this.save.SelectSingleNode(str1 + "/" + textBox.Name);
                                      if (xmlNode6 != null)
                                        textBox.Text = xmlNode6.InnerText;
                                      textBox.Location = new Point(x, y1);
                                      textBox.TabIndex = num1;
                                      --num1;
                                      textBox.MouseClick += new MouseEventHandler(this.Control_S1_MouseClick);
                                      textBox.LostFocus += new EventHandler(this.LostFocus);
                                      textBox.Validated += new EventHandler(this.number_Validated);
                                      if (!(list[index2].getDisplay().ToLower() == "false"))
                                        this.Form2panel.Controls.Add((Control) textBox);
                                      if (label.Height > 20)
                                        y1 += label.Height + 5;
                                      else
                                        y1 += 25;
                                      this.candidatesRule[textBox.Name] = str6;
                                      str6 = "";
                                    }
                                  }
                                }
                                if (str7 == "A")
                                  ++num5;
                              }
                              this.countcandidatesoflist.Add(new int[2]
                              {
                                num4,
                                num5 - 1
                              });
                            }
                          }
                          ++num4;
                          y1 += 30;
                        }
                      }
                    }
                  }
                }
              }
            }
            if (namedItem1.Value == "additional-table")
            {
              x = 10;
              int y2 = y1 + 30;
              if (xmlNode3.Name == "fields")
              {
                foreach (XmlNode xmlNode4 in xmlNode3)
                {
                  bool flag1 = true;
                  bool flag2 = false;
                  XmlNode namedItem2 = xmlNode4.Attributes.GetNamedItem("enabled");
                  if (namedItem2 != null && namedItem2.Value == "false")
                  {
                    flag1 = false;
                    flag2 = true;
                  }
                  string str5 = "";
                  XmlNode namedItem3 = xmlNode4.Attributes.GetNamedItem("correspondence");
                  XmlNode namedItem4 = xmlNode4.Attributes.GetNamedItem("elseCorrespondenceDefault");
                  if (namedItem3 != null && (namedItem3.Value.ToLower() == "false" && this.correspondence || namedItem3.Value.ToLower() == "true" && !this.correspondence))
                  {
                    flag1 = false;
                    if (namedItem4 != null)
                      str5 = namedItem4.Value;
                  }
                  XmlNode namedItem5 = xmlNode4.Attributes.GetNamedItem("district");
                  XmlNode namedItem6 = xmlNode4.Attributes.GetNamedItem("districtDefault");
                  if (namedItem5 != null && namedItem5.Value.ToUpper() != "ALL")
                  {
                    if (Regex.IsMatch(namedItem5.Value.ToUpper(), "ONLY:") && namedItem5.Value.ToUpper().Replace("ONLY:", "") == this.typeObw.ToUpper())
                    {
                      flag1 = false;
                      if (namedItem6 != null)
                        str5 = namedItem6.Value;
                    }
                    if (Regex.IsMatch(namedItem5.Value.ToUpper(), "EXCLUDE:") && namedItem5.Value.ToUpper().Replace("EXCLUDE:", "") != this.typeObw.ToUpper())
                    {
                      flag1 = false;
                      if (namedItem6 != null)
                        str5 = namedItem6.Value;
                    }
                  }
                  XmlNode xmlNode5 = this.header.SelectSingleNode("/akcja_wyborcza/jns");
                  XmlNode namedItem7 = xmlNode4.Attributes.GetNamedItem("valueName");
                  if (namedItem7 != null)
                  {
                    if (namedItem7.Value == "algorytmOKW")
                    {
                      foreach (XmlNode xmlNode6 in xmlNode5)
                      {
                        if (xmlNode6.Name == "obw" && xmlNode6.Attributes.GetNamedItem("nr") != null && xmlNode6.Attributes.GetNamedItem("nr").Value == str2)
                        {
                          foreach (XmlNode xmlNode7 in xmlNode6)
                          {
                            if (xmlNode7.Name == "inst" && xmlNode7.Attributes.GetNamedItem("kod") != null && xmlNode7.Attributes.GetNamedItem("kod").Value == str3)
                            {
                              foreach (XmlNode xmlNode8 in xmlNode7)
                              {
                                if (xmlNode8.Name == "okr" && xmlNode8.Attributes.GetNamedItem("nr") != null && xmlNode8.Attributes.GetNamedItem("nr").Value == str4 && xmlNode8.Attributes.GetNamedItem("siedziba") != null)
                                  str5 = xmlNode8.Attributes.GetNamedItem("siedziba").Value;
                              }
                            }
                          }
                        }
                      }
                    }
                    if (namedItem7.Value == "siedzibaObwod")
                    {
                      foreach (XmlNode xmlNode6 in xmlNode5)
                      {
                        if (xmlNode6.Attributes.GetNamedItem("nr") != null && xmlNode6.Attributes.GetNamedItem("nr").Value == str2 && xmlNode6.Attributes.GetNamedItem("siedziba") != null)
                          str5 = xmlNode6.Attributes.GetNamedItem("siedziba").Value;
                      }
                    }
                    if (namedItem7.Value == "nr")
                      str5 = str2;
                    if (namedItem7 != null && Regex.IsMatch(namedItem7.Value, "\\+"))
                    {
                      string str6 = "";
                      string str7 = namedItem7.Value;
                      if (Regex.IsMatch(namedItem7.Value, "header:"))
                      {
                        str7 = namedItem7.Value.Replace("header:", "");
                        str6 = "header";
                      }
                      if (Regex.IsMatch(namedItem7.Value, "calculator:"))
                      {
                        str7 = namedItem7.Value.Replace("calculator:", "");
                        str6 = "calculator";
                      }
                      if (Regex.IsMatch(namedItem7.Value, "additional-calculator:"))
                      {
                        str7 = namedItem7.Value.Replace("additional-calculator:", "");
                        str6 = "additional-calculator";
                      }
                      if (Regex.IsMatch(namedItem7.Value, "additional-table:"))
                      {
                        str7 = namedItem7.Value.Replace("additional-table:", "");
                        str6 = "additional-table";
                      }
                      string[] strArray2 = str7.Split('+');
                      str5 = "";
                      for (int index = 0; index < strArray2.Length; ++index)
                      {
                        if (index != 0)
                          str5 = str5 + ", ";
                        if (str6 == "header")
                          str5 = str5 + this.protocolHeader.Controls[strArray2[index]].Text;
                        if (str6 == "calculator" || str6 == "additional-calculator" || str6 == "additional-table")
                          str5 = str5 + this.Form1panel.Controls[strArray2[index]].Text;
                      }
                    }
                  }
                  if (xmlNode4.Name == "title")
                  {
                    Label label = new Label();
                    label.Text = xmlNode4.InnerText;
                    label.AutoSize = true;
                    label.MaximumSize = new Size(this.Form2panel.Size.Width - 20, 0);
                    label.Font = new Font(this.myfont, 10f, FontStyle.Bold);
                    label.Location = new Point(x, y2);
                    this.Form2panel.Controls.Add((Control) label);
                    y2 += label.Height + 30;
                  }
                  if (xmlNode4.Name == "description")
                  {
                    x = 10;
                    Label label = new Label();
                    label.Text = xmlNode4.InnerText;
                    label.AutoSize = true;
                    label.MaximumSize = new Size(this.Form2panel.Size.Width - 20, 0);
                    label.Font = new Font(this.myfont, 9f, FontStyle.Bold);
                    label.Location = new Point(x, y2 - 15);
                    this.Form2panel.Controls.Add((Control) label);
                    y2 += label.Height + 25;
                  }
                  if (xmlNode4.Name == "note" && xmlNode4.InnerText != "")
                  {
                    x = 10;
                    Label label1 = new Label();
                    label1.Text = "Uwaga";
                    label1.AutoSize = true;
                    label1.MaximumSize = new Size(100, 0);
                    label1.Font = new Font(this.myfont, 9f, FontStyle.Bold);
                    label1.Location = new Point(x, y2);
                    this.Form2panel.Controls.Add((Control) label1);
                    x += label1.Width + 5;
                    this.Form1panel.Controls.Add((Control) label1);
                    Label label2 = new Label();
                    label2.Text = xmlNode4.InnerText;
                    label2.AutoSize = true;
                    label2.MaximumSize = new Size(this.Form2panel.Size.Width - 120, 0);
                    label2.Font = new Font(this.myfont, 9f);
                    label2.Location = new Point(x, y2);
                    this.Form2panel.Controls.Add((Control) label2);
                    y2 += label2.Height + 25;
                  }
                  if (xmlNode4.Name == "field")
                  {
                    x = 10;
                    Label label1 = new Label();
                    TextBox textBox = new TextBox();
                    foreach (XmlNode xmlNode6 in xmlNode4)
                    {
                      if (xmlNode6.Name == "name")
                      {
                        if (xmlNode6.ParentNode.Attributes.GetNamedItem("lp") != null)
                          label1.Text = xmlNode6.ParentNode.Attributes.GetNamedItem("lp").Value + " ";
                        Label label2 = label1;
                        string str6 = label2.Text + xmlNode6.InnerText;
                        label2.Text = str6;
                        label1.AutoSize = true;
                        label1.MaximumSize = new Size(num2 - num3, 0);
                        label1.Font = new Font(this.myfont, 9f);
                        label1.Location = new Point(x, y2);
                        x = num2 - 95;
                      }
                      if (xmlNode6.Name == "save_as")
                      {
                        int width = 85;
                        XmlNode namedItem8 = xmlNode6.ParentNode.Attributes.GetNamedItem("data");
                        if (namedItem8 != null && namedItem8.Value == "number")
                        {
                          textBox.Validated += new EventHandler(this.number_Validated);
                          try
                          {
                            this.typeValidation.Add(textBox.Name, "number");
                          }
                          catch (ArgumentException ex)
                          {
                          }
                        }
                        if (namedItem8 != null && namedItem8.Value == "text")
                        {
                          textBox.Validated += new EventHandler(this.text_Validated);
                          try
                          {
                            this.typeValidation.Add(textBox.Name, "text");
                          }
                          catch (ArgumentException ex)
                          {
                          }
                          label1.MaximumSize = new Size(num2 - num3 * 2, 0);
                          width = 170;
                          label1.AutoSize = true;
                          x = num2 - num3 * 2 + 20;
                        }
                        textBox.Size = new Size(width, 20);
                        textBox.Name = xmlNode6.InnerText;
                        if (str5 != "")
                          textBox.Text = str5;
                        textBox.Enabled = flag1;
                        XmlNode xmlNode7 = this.save.SelectSingleNode(str1 + "/" + xmlNode6.InnerText);
                        if (xmlNode7 != null)
                          textBox.Text = xmlNode7.InnerText;
                        textBox.MouseClick += new MouseEventHandler(this.Control_S1_MouseClick);
                        textBox.LostFocus += new EventHandler(this.LostFocus);
                        textBox.TabIndex = num1;
                        --num1;
                        textBox.Location = new Point(x, y2);
                        textBox.CausesValidation = true;
                        label1.Name = "Lab_" + xmlNode6.InnerText;
                        if (flag2)
                          this.controlsCanBeNull.Add(textBox.Name);
                      }
                    }
                    this.Form2panel.Controls.Add((Control) label1);
                    this.Form2panel.Controls.Add((Control) textBox);
                    if (label1.Height > 20)
                      y2 += label1.Height + 5;
                    else
                      y2 += 25;
                  }
                }
              }
              y1 = y2 + 50;
            }
          }
          else
            break;
        }
      }
      catch (XmlException ex)
      {
        int num4 = (int) MessageBox.Show("Nieprawidłowy XML - " + ex.Message, "Błąd");
      }
      catch (NullReferenceException ex)
      {
        int num4 = (int) MessageBox.Show("Podanno inny xml definicje wygladu - " + ex.Message, "Błąd");
      }
      this.Form2panel.Location = new Point(this.protocolHeader.Location.X, this.protocolHeader.Location.Y + this.protocolHeader.Size.Height + 50);
      this.buttonNext.Text = "Dalej";
      this.buttonNext.Click -= new EventHandler(this.protocolForm2_Click);
      this.buttonNext.Click -= new EventHandler(this.protocolSummation_Click);
      this.buttonNext.Click -= new EventHandler(this.committee_Click);
      this.buttonNext.Click -= new EventHandler(this.committeeValid_Click);
      this.buttonNext.Click -= new EventHandler(this.signProtocol_Click);
      this.buttonNext.Click += new EventHandler(this.protocolSummation_Click);
      this.bottomPanel.Location = new Point(this.Form2panel.Location.X, this.Form2panel.Location.Y + this.Form2panel.Size.Height);
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
          this.currentCommittee = false;
        XmlNode xmlNode1 = this.committee.SelectSingleNode("/komisja_sklad");
        if (this.isSave() == 0 || this.isSave() == 4)
        {
          XmlNode xmlNode2 = this.save.SelectSingleNode("/save/komisja_sklad");
          if (xmlNode2 != null)
            xmlNode1 = xmlNode2;
        }
        DataTable dataTable = new DataTable();
        int x = 0;
        int y = 30;
        if (dataTable.Columns["Lp"] == null)
          dataTable.Columns.Add(new DataColumn("lp", typeof (string)));
        if (dataTable.Columns["Imię"] == null)
          dataTable.Columns.Add(new DataColumn("Imię", typeof (string)));
        if (dataTable.Columns["Drugie imię"] == null)
          dataTable.Columns.Add(new DataColumn("Drugie imię", typeof (string)));
        if (dataTable.Columns["Nazwisko"] == null)
          dataTable.Columns.Add(new DataColumn("Nazwisko", typeof (string)));
        foreach (XmlNode xmlNode2 in xmlNode1)
        {
          DataRow row = dataTable.NewRow();
          string str1 = "";
          row[0] = (object) (dataTable.Rows.Count + 1);
          XmlNode namedItem1 = xmlNode2.Attributes.GetNamedItem("imie");
          if (namedItem1 != null && namedItem1.Value != "")
            str1 = HttpUtility.UrlDecode(namedItem1.Value);
          row[1] = (object) str1;
          string str2 = "";
          XmlNode namedItem2 = xmlNode2.Attributes.GetNamedItem("imie2");
          if (namedItem2 != null && namedItem2.Value != "")
            str2 = HttpUtility.UrlDecode(namedItem2.Value);
          row[2] = (object) str2;
          string str3 = "";
          XmlNode namedItem3 = xmlNode2.Attributes.GetNamedItem("nazwisko");
          if (namedItem3 != null && namedItem3.Value != "")
            str3 = HttpUtility.UrlDecode(namedItem3.Value);
          row[3] = (object) str3;
          dataTable.Rows.Add(row);
        }
        this.personList.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
        this.personList.ReadOnly = false;
        DataGridView dataGridView1 = this.personList;
        Size size1 = this.committeePanel.Size;
        Size size2 = new Size(size1.Width, 0);
        dataGridView1.MaximumSize = size2;
        DataGridView dataGridView2 = this.personList;
        size1 = this.committeePanel.Size;
        Size size3 = new Size(size1.Width, 100);
        dataGridView2.Size = size3;
        XmlNode namedItem4 = xmlNode1.Attributes.GetNamedItem("data_wersji");
        if (namedItem4 != null)
        {
          Label label = new Label();
          label.Text = "Definicja komisji z pliku klk z dnia " + namedItem4.Value;
          label.AutoSize = true;
          label.MaximumSize = new Size(this.committeePanel.Width - 300, 0);
          label.Font = new Font(this.myfont, 9f);
          label.Location = new Point(x, y);
          this.committeePanel.Controls.Add((Control) label);
          Button button = new Button();
          button.Size = new Size(300, 20);
          button.Text = "Pobierz aktualny skład Komisji";
          button.Location = new Point(this.committeePanel.Width - 300, y);
          button.Click += new EventHandler(this.getCurrentCommitteeKlk_Click);
          this.committeePanel.Controls.Add((Control) button);
          if (label.Height > 20)
            y += label.Height + 20;
          else
            y += 40;
        }
        this.personList.Location = new Point(x, y);
        this.personList.AllowUserToAddRows = true;
        this.personList.AllowUserToDeleteRows = true;
        this.personList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        this.personList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        this.personList.AutoSize = true;
        this.personList.DataSource = (object) new DataTable();
        this.personList.DataSource = (object) dataTable;
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
        DataGridViewComboBoxColumn viewComboBoxColumn = new DataGridViewComboBoxColumn();
        viewComboBoxColumn.HeaderText = "Funkcja";
        viewComboBoxColumn.Name = "Funkcja";
        viewComboBoxColumn.Items.AddRange((object) "CZŁONEK", (object) "PRZEWODNICZĄCY", (object) "ZASTĘPCA");
        DataGridViewCheckBoxColumn viewCheckBoxColumn = new DataGridViewCheckBoxColumn();
        viewCheckBoxColumn.HeaderText = "Obecny";
        viewCheckBoxColumn.Name = "action3";
        DataGridViewButtonColumn viewButtonColumn = new DataGridViewButtonColumn();
        viewButtonColumn.HeaderText = "";
        viewButtonColumn.Text = "Usuń";
        viewButtonColumn.Name = "remove";
        viewButtonColumn.UseColumnTextForButtonValue = true;
        if (this.personList.Columns["Funkcja"] == null)
        {
          this.personList.Columns.Insert(4, (DataGridViewColumn) viewComboBoxColumn);
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
          this.personList.Columns.Insert(5, (DataGridViewColumn) viewCheckBoxColumn);
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
          this.personList.Columns.Insert(6, (DataGridViewColumn) viewButtonColumn);
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
          for (int index = 0; index < this.personList.Rows.Count - 1; ++index)
          {
            XmlNode namedItem1 = xmlNode1.ChildNodes[index].Attributes.GetNamedItem("obecny");
            XmlNode namedItem2 = xmlNode1.ChildNodes[index].Attributes.GetNamedItem("funkcja");
            if (namedItem1 != null)
              this.personList.Rows[index].Cells["action3"].Value = (object) (bool) (Convert.ToBoolean(namedItem1.Value) ? 1 : 0);
            if (namedItem2 != null)
            {
              if (namedItem2.Value.ToUpper() == "CZLONEK" || namedItem2.Value.ToUpper() == "CZŁONEK")
                this.personList.Rows[index].Cells["Funkcja"].Value = (object) "CZŁONEK";
              if (namedItem2.Value.ToUpper() == "ZASTEPCA" || namedItem2.Value.ToUpper() == "ZASTĘPCA")
                this.personList.Rows[index].Cells["Funkcja"].Value = (object) "ZASTĘPCA";
              if (namedItem2.Value.ToUpper() == "PRZEWODNICZACY" || namedItem2.Value.ToUpper() == "PRZEWODNICZĄCY")
                this.personList.Rows[index].Cells["Funkcja"].Value = (object) "PRZEWODNICZĄCY";
            }
            this.personList.Rows[index].Cells["lp"].ToolTipText = "Kliknij dwukrotnie na komórkę, aby edytować";
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
          for (int index = 0; index < this.personList.Rows.Count - 1; ++index)
          {
            this.personList.Rows[index].Cells["lp"].ToolTipText = "Kliknij dwukrotnie na komórkę, aby edytować";
            this.personList.Columns["Imię"].ToolTipText = "Kliknij dwukrotnie na komórkę, aby edytować";
            this.personList.Columns["Drugie imię"].ToolTipText = "Kliknij dwukrotnie na komórkę, aby edytować";
            this.personList.Columns["Nazwisko"].ToolTipText = "Kliknij dwukrotnie na komórkę, aby edytować";
            XmlNode namedItem1 = xmlNode1.ChildNodes[index].Attributes.GetNamedItem("funkcja");
            if (namedItem1 != null)
            {
              if (namedItem1.Value.ToUpper() == "CZLONEK" || namedItem1.Value.ToUpper() == "CZŁONEK")
                this.personList.Rows[index].Cells["Funkcja"].Value = (object) "CZŁONEK";
              if (namedItem1.Value.ToUpper() == "ZASTEPCA" || namedItem1.Value.ToUpper() == "ZASTĘPCA")
                this.personList.Rows[index].Cells["Funkcja"].Value = (object) "ZASTĘPCA";
              if (namedItem1.Value.ToUpper() == "PRZEWODNICZACY" || namedItem1.Value.ToUpper() == "PRZEWODNICZĄCY")
                this.personList.Rows[index].Cells["Funkcja"].Value = (object) "PRZEWODNICZĄCY";
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
        this.buttonNext.Click -= new EventHandler(this.protocolForm2_Click);
        this.buttonNext.Click -= new EventHandler(this.protocolSummation_Click);
        this.buttonNext.Click -= new EventHandler(this.committee_Click);
        this.buttonNext.Click -= new EventHandler(this.committeeValid_Click);
        this.buttonNext.Click -= new EventHandler(this.signProtocol_Click);
        this.buttonNext.Click += new EventHandler(this.committeeValid_Click);
      }
      catch (XmlException ex)
      {
        int num = (int) MessageBox.Show("Nieprawidłowy XML - " + ex.Message, "Błąd");
      }
      catch (NullReferenceException ex)
      {
        int num = (int) MessageBox.Show("Podanno inny xml niz header - " + ex.Message, "Błąd");
      }
      this.bottomPanel.Location = new Point(this.committeePanel.Location.X, this.committeePanel.Location.Y + this.committeePanel.Size.Height);
      this.wait.setVisible(false);
    }

    private void Summation()
    {
      int num1 = 0;
      foreach (Control control in (ArrangedElementCollection) this.protocolHeader.Controls)
      {
        if (control is TextBox)
        {
          (control as TextBox).TabIndex = num1;
          ++num1;
        }
        if (control is MaskedTextBox)
        {
          (control as MaskedTextBox).TabIndex = num1;
          ++num1;
        }
      }
      this.SummationPanel.Controls.Clear();
      this.wait.setWaitPanel("Trwa ładowanie podsumowania", "Proszę czekać");
      this.wait.setVisible(true);
      string str1 = "";
      if (this.isSave() == 0 || this.isSave() == 4)
        str1 = "/save/form";
      if (this.isSave() == 3)
        str1 = "/save/step3";
      int num2 = 0;
      this.protocolHeader.TabIndex = 3;
      this.SummationPanel.TabIndex = 2;
      foreach (Control control in (ArrangedElementCollection) this.Form2panel.Controls)
      {
        int count = this.Form2panel.Controls.Count;
        Size size;
        if (control is TextBox)
        {
          TextBox textBox = new TextBox();
          textBox.Size = control.Size;
          textBox.Location = control.Location;
          textBox.Text = control.Text;
          textBox.Name = control.Name;
          textBox.Enabled = false;
          this.SummationPanel.Controls.Add((Control) textBox);
          int num3 = num2;
          int y1 = textBox.Location.Y;
          size = textBox.Size;
          int height1 = size.Height;
          int num4 = y1 + height1;
          if (num3 <= num4)
          {
            int y2 = textBox.Location.Y;
            size = textBox.Size;
            int height2 = size.Height;
            num2 = y2 + height2;
          }
        }
        if (control is Label)
        {
          Label label = new Label();
          label.Size = control.Size;
          label.Location = control.Location;
          label.Text = control.Text;
          label.Name = control.Name;
          label.AutoSize = control.AutoSize;
          label.MaximumSize = control.MaximumSize;
          label.Font = control.Font;
          int num3 = num2;
          int y1 = label.Location.Y;
          size = label.Size;
          int height1 = size.Height;
          int num4 = y1 + height1;
          if (num3 <= num4)
          {
            int y2 = label.Location.Y;
            size = label.Size;
            int height2 = size.Height;
            num2 = y2 + height2;
          }
          this.SummationPanel.Controls.Add((Control) label);
        }
      }
      int y3 = num2 + 40;
      try
      {
        XmlNode xmlNode1 = this.protocolDefinition.SelectSingleNode("/protokol_info");
        this.candidates.SelectSingleNode("/listy");
        foreach (XmlNode xmlNode2 in xmlNode1)
        {
          XmlNode namedItem1 = xmlNode2.Attributes.GetNamedItem("type");
          if (namedItem1 != null && namedItem1.Value == "additional-calculator")
          {
            this.wait.setWaitPanel("Trwa ładowanie podsumowania - ładowanie dodatkowych pól (adnotacji)", "Proszę czekać");
            foreach (XmlNode xmlNode3 in xmlNode2)
            {
              if (xmlNode3.Name == "title")
              {
                int x = 0;
                XmlNode namedItem2 = xmlNode3.Attributes.GetNamedItem("bold");
                Label label1 = new Label();
                label1.Text = xmlNode3.InnerText;
                label1.AutoSize = true;
                Label label2 = label1;
                Size size1 = this.SummationPanel.Size;
                Size size2 = new Size(size1.Width, 0);
                label2.MaximumSize = size2;
                label1.Font = new Font(this.myfont, 10f);
                label1.Padding = new Padding(10, 0, 10, 0);
                if (namedItem2.Value == "true")
                  label1.Font = new Font(this.myfont, 10f, FontStyle.Bold);
                label1.Location = new Point(x, y3);
                this.SummationPanel.Controls.Add((Control) label1);
                int num3 = y3;
                size1 = label1.Size;
                int num4 = size1.Height + 30;
                y3 = num3 + num4;
              }
              if (xmlNode3.Name == "field")
              {
                XmlNode namedItem2 = xmlNode3.Attributes.GetNamedItem("data");
                if (namedItem2 != null && namedItem2.Value != "kandydaci")
                {
                  int x = 10;
                  Label label = new Label();
                  TextBox textBox = new TextBox();
                  foreach (XmlNode xmlNode4 in xmlNode3)
                  {
                    if (xmlNode4.Name == "name")
                    {
                      label.Text = xmlNode4.ParentNode.Attributes.GetNamedItem("lp").Value + " " + xmlNode4.InnerText;
                      label.AutoSize = true;
                      label.MaximumSize = new Size(this.Form2panel.Width - 20, 0);
                      label.MinimumSize = new Size(this.Form2panel.Width - 20, 0);
                      label.Font = new Font(this.myfont, 9f);
                      label.Location = new Point(x, y3);
                      y3 += label.Size.Height + 45;
                    }
                    if (xmlNode4.Name == "save_as")
                    {
                      textBox.Size = new Size(this.Form2panel.Width - 20, 100);
                      textBox.Name = xmlNode4.InnerText;
                      textBox.MouseClick += new MouseEventHandler(this.Control_S3_MouseClick);
                      textBox.LostFocus += new EventHandler(this.LostFocus);
                      XmlNode namedItem3 = xmlNode4.Attributes.GetNamedItem("valueDefault");
                      if (namedItem3 != null)
                        textBox.Text = namedItem3.InnerText;
                      XmlNode xmlNode5 = this.save.SelectSingleNode(str1 + "/" + textBox.Name);
                      if (xmlNode5 != null)
                        textBox.Text = xmlNode5.InnerText;
                      textBox.Location = new Point(x, y3);
                      textBox.CausesValidation = true;
                      textBox.Multiline = true;
                      textBox.ScrollBars = ScrollBars.Vertical;
                      XmlNode namedItem4 = xmlNode4.ParentNode.Attributes.GetNamedItem("data");
                      if (namedItem4 != null && namedItem4.Value == "text")
                      {
                        textBox.Validated += new EventHandler(this.normalText_Validated);
                        try
                        {
                          string str2 = this.typeValidation[textBox.Name];
                        }
                        catch (KeyNotFoundException ex)
                        {
                          this.typeValidation.Add(textBox.Name, "normalText");
                        }
                      }
                      label.Name = "Lab_" + xmlNode4.InnerText;
                    }
                  }
                  this.SummationPanel.Controls.Add((Control) label);
                  this.SummationPanel.Controls.Add((Control) textBox);
                  if (label.Height > 100)
                    y3 += label.Height + 5;
                  else
                    y3 += 105;
                }
              }
            }
          }
        }
      }
      catch (XmlException ex)
      {
        int num3 = (int) MessageBox.Show("Nieprawidłowy XML - " + ex.Message, "Błąd");
      }
      catch (NullReferenceException ex)
      {
        int num3 = (int) MessageBox.Show("Podanno inny xml definicje wygladu - " + ex.Message, "Błąd");
      }
      Panel panel1 = this.SummationPanel;
      int x1 = this.protocolHeader.Location.X;
      Point location = this.protocolHeader.Location;
      int y4 = location.Y + this.protocolHeader.Size.Height + 50;
      Point point1 = new Point(x1, y4);
      panel1.Location = point1;
      this.buttonNext.Text = "Dalej";
      this.buttonNext.Click -= new EventHandler(this.protocolForm2_Click);
      this.buttonNext.Click -= new EventHandler(this.protocolSummation_Click);
      this.buttonNext.Click -= new EventHandler(this.committee_Click);
      this.buttonNext.Click -= new EventHandler(this.committeeValid_Click);
      this.buttonNext.Click -= new EventHandler(this.signProtocol_Click);
      this.buttonNext.Click += new EventHandler(this.committee_Click);
      Panel panel2 = this.bottomPanel;
      location = this.SummationPanel.Location;
      int x2 = location.X;
      location = this.SummationPanel.Location;
      int y5 = location.Y + this.SummationPanel.Size.Height;
      Point point2 = new Point(x2, y5);
      panel2.Location = point2;
      this.report();
      this.wait.setVisible(false);
    }

    private void report()
    {
      this.raportPanel.Controls.Clear();
      this.raportPanel.MaximumSize = new Size(776, 0);
      if (this.errorProvider1.hasHardWarning() || this.errorProvider1.hasWarning())
      {
        this.raportPanel.Visible = true;
        this.raportPanel.AutoSize = true;
        this.wait.setWaitPanel("Trwa ładowanie formularza wyjaśnień dla raportu ostrzeżeń", "Proszę czekać");
        this.wait.setVisible(true);
        int x1 = 0;
        int y1 = 0;
        this.raportPanel.TabIndex = 3;
        this.protocolHeader.TabIndex = 4;
        Dictionary<string, Dictionary<string, KBWValue>> warnings = this.errorProvider1.getWarnings();
        Dictionary<string, Dictionary<string, KBWValue>> hardWarnings = this.errorProvider1.getHardWarnings();
        Label label1 = new Label();
        label1.Text = "Wyjaśnienia do raportu ostrzeżeń";
        label1.AutoSize = true;
        label1.MaximumSize = new Size(this.raportPanel.Size.Width, 0);
        label1.Font = new Font(this.myfont, 10f);
        label1.Padding = new Padding(10, 0, 10, 0);
        label1.Font = new Font(this.myfont, 10f, FontStyle.Bold);
        label1.Location = new Point(x1, y1);
        this.raportPanel.Controls.Add((Control) label1);
        int y2 = y1 + (label1.Size.Height + 30);
        if (this.errorProvider1.hasHardWarning())
        {
          List<string> list = new List<string>();
          foreach (KeyValuePair<string, Dictionary<string, KBWValue>> keyValuePair1 in hardWarnings)
          {
            foreach (KeyValuePair<string, KBWValue> keyValuePair2 in keyValuePair1.Value)
            {
              int x2 = 0;
              bool flag = true;
              for (int index = 0; index < list.Count; ++index)
              {
                if (list[index] == keyValuePair2.Key)
                {
                  flag = false;
                  break;
                }
              }
              if (flag)
              {
                list.Add(keyValuePair2.Key);
                Label label2 = new Label();
                string message = hardWarnings[keyValuePair1.Key][keyValuePair2.Key].getMessage();
                label2.Text = message;
                label2.AutoSize = true;
                label2.MaximumSize = new Size(this.errorPanel.Size.Width - 20, 0);
                label2.Font = new Font(this.myfont, 9f);
                label2.Padding = new Padding(10, 0, 10, 0);
                label2.Location = new Point(x2, y2);
                this.raportPanel.Controls.Add((Control) label2);
                y2 += label2.Height + 20;
                int x3 = 10;
                TextBox textBox = new TextBox();
                textBox.Size = new Size(this.raportPanel.Width - 20, 100);
                textBox.Name = keyValuePair2.Key + "_explain";
                textBox.MouseClick += new MouseEventHandler(this.Control_S3_MouseClick);
                textBox.LostFocus += new EventHandler(this.LostFocus);
                textBox.Location = new Point(x3, y2);
                textBox.CausesValidation = true;
                textBox.Multiline = true;
                textBox.ScrollBars = ScrollBars.Vertical;
                textBox.Validated += new EventHandler(this.normalText_Validated);
                XmlNode xmlNode = this.save.SelectSingleNode("/save/report/" + textBox.Name);
                if (xmlNode != null)
                  textBox.Text = xmlNode.InnerText;
                this.raportPanel.Controls.Add((Control) textBox);
                y2 += textBox.Height + 40;
              }
            }
          }
        }
        if (this.errorProvider1.hasWarning())
        {
          List<string> list = new List<string>();
          foreach (KeyValuePair<string, Dictionary<string, KBWValue>> keyValuePair1 in warnings)
          {
            foreach (KeyValuePair<string, KBWValue> keyValuePair2 in keyValuePair1.Value)
            {
              int x2 = 0;
              bool flag = true;
              for (int index = 0; index < list.Count; ++index)
              {
                if (list[index] == keyValuePair2.Key)
                {
                  flag = false;
                  break;
                }
              }
              if (flag)
              {
                list.Add(keyValuePair2.Key);
                Label label2 = new Label();
                string message = warnings[keyValuePair1.Key][keyValuePair2.Key].getMessage();
                label2.Text = message;
                label2.AutoSize = true;
                label2.MaximumSize = new Size(this.errorPanel.Size.Width - 20, 0);
                label2.Font = new Font(this.myfont, 8f);
                label2.Padding = new Padding(10, 0, 10, 0);
                label2.Location = new Point(x2, y2);
                this.raportPanel.Controls.Add((Control) label2);
                y2 += label2.Height + 20;
                int x3 = 10;
                TextBox textBox = new TextBox();
                textBox.Size = new Size(this.raportPanel.Width - 20, 100);
                textBox.Name = keyValuePair2.Key + "_explain";
                textBox.MouseClick += new MouseEventHandler(this.Control_S3_MouseClick);
                textBox.LostFocus += new EventHandler(this.LostFocus);
                textBox.Location = new Point(x3, y2);
                textBox.CausesValidation = true;
                textBox.Multiline = true;
                textBox.ScrollBars = ScrollBars.Vertical;
                textBox.Validated += new EventHandler(this.normalText_Validated);
                XmlNode xmlNode = this.save.SelectSingleNode("/save/report/" + textBox.Name);
                if (xmlNode != null)
                  textBox.Text = xmlNode.InnerText;
                this.raportPanel.Controls.Add((Control) textBox);
                y2 += textBox.Height + 40;
              }
            }
          }
        }
        Panel panel1 = this.raportPanel;
        Point location = this.SummationPanel.Location;
        int x4 = location.X;
        location = this.SummationPanel.Location;
        int y3 = location.Y;
        Size size = this.SummationPanel.Size;
        int height1 = size.Height;
        int y4 = y3 + height1 + 50;
        Point point1 = new Point(x4, y4);
        panel1.Location = point1;
        Panel panel2 = this.bottomPanel;
        location = this.raportPanel.Location;
        int x5 = location.X;
        location = this.raportPanel.Location;
        int y5 = location.Y;
        size = this.raportPanel.Size;
        int height2 = size.Height;
        int y6 = y5 + height2;
        Point point2 = new Point(x5, y6);
        panel2.Location = point2;
      }
      else
        this.raportPanel.Visible = false;
      this.wait.setVisible(false);
    }

    private void getDefinitionLastValidation()
    {
      this.wait.setWaitPanel("Trwa ładowanie definicji walidacji", "Proszę czekać");
      try
      {
        foreach (XmlNode xmlNode1 in this.validateDefinition.SelectSingleNode("/validate_info"))
        {
          string paternLeft = "";
          string paternRight = "";
          string note = "";
          string validationFor = "";
          string id = "";
          Operation operation = Operation.Null;
          ErrorType type = ErrorType.Null;
          XmlNode namedItem1 = xmlNode1.Attributes.GetNamedItem("correspondence");
          if (namedItem1 == null || !(namedItem1.Value.ToUpper() != "BOTH") || (!(namedItem1.Value.ToUpper() == "TRUE") || this.correspondence) && (!(namedItem1.Value.ToUpper() == "FALSE") || !this.correspondence))
          {
            XmlNode namedItem2 = xmlNode1.Attributes.GetNamedItem("district");
            if (namedItem2 == null || !(namedItem2.Value.ToUpper() != "ALL") || (!Regex.IsMatch(namedItem2.Value.ToUpper(), "ONLY:") || !(namedItem2.Value.ToUpper().Replace("ONLY:", "") != this.typeObw.ToUpper())) && (!Regex.IsMatch(namedItem2.Value.ToUpper(), "EXCLUDE:") || !(namedItem2.Value.ToUpper().Replace("EXCLUDE:", "") == this.typeObw.ToUpper())))
            {
              XmlNode namedItem3 = xmlNode1.Attributes.GetNamedItem("deletedCandidate");
              if ((namedItem3 == null || !(namedItem3.Value.ToUpper() == "0") || this.deletedCandidates == 0) && (namedItem3 == null || !(namedItem3.Value.ToUpper() == "1") || this.deletedCandidates != 0))
              {
                XmlNode namedItem4 = xmlNode1.Attributes.GetNamedItem("type");
                XmlNode namedItem5 = xmlNode1.Attributes.GetNamedItem("for");
                XmlNode namedItem6 = xmlNode1.Attributes.GetNamedItem("id");
                if (namedItem4 != null)
                {
                  if (namedItem4.Value == "twardy")
                    type = ErrorType.HardError;
                  if (namedItem4.Value == "miekki")
                    type = ErrorType.Soft;
                  if (namedItem4.Value == "twarde_ostrzezenie")
                    type = ErrorType.HardWarning;
                }
                if (namedItem5 != null)
                  validationFor = namedItem5.Value;
                if (namedItem6 != null)
                  id = namedItem6.Value;
                ValidationPatern validationPatern = new ValidationPatern();
                foreach (XmlNode xmlNode2 in xmlNode1)
                {
                  if (xmlNode2.Name == "left")
                    paternLeft = xmlNode2.InnerText;
                  if (xmlNode2.Name == "right")
                    paternRight = xmlNode2.InnerText;
                  if (xmlNode2.Name == "note")
                    note = xmlNode2.InnerText;
                  if (xmlNode2.Name == "operation")
                  {
                    if (xmlNode2.InnerText == "==")
                      operation = Operation.Equal;
                    if (xmlNode2.InnerText == "!=")
                      operation = Operation.Different;
                    if (xmlNode2.InnerText == "less")
                      operation = Operation.Less;
                    if (xmlNode2.InnerText == "less=")
                      operation = Operation.LessOrEqual;
                    if (xmlNode2.InnerText == "more")
                      operation = Operation.More;
                    if (xmlNode2.InnerText == "more=")
                      operation = Operation.MoreOrEqual;
                  }
                  if (xmlNode2.Name == "fields")
                  {
                    foreach (XmlNode xmlNode3 in xmlNode2)
                    {
                      XmlNode namedItem7 = xmlNode3.Attributes.GetNamedItem("name");
                      XmlNode namedItem8 = xmlNode3.Attributes.GetNamedItem("from");
                      XmlNode namedItem9 = xmlNode3.Attributes.GetNamedItem("variable1");
                      XmlNode namedItem10 = xmlNode3.Attributes.GetNamedItem("variable1Mean");
                      XmlNode namedItem11 = xmlNode3.Attributes.GetNamedItem("variable2");
                      XmlNode namedItem12 = xmlNode3.Attributes.GetNamedItem("variable2Mean");
                      if (namedItem7 != null && namedItem8 != null && (namedItem9 != null && namedItem10 != null) && (namedItem11 != null && namedItem12 != null && (namedItem9.Value != "" && namedItem10.Value != "")) && namedItem11.Value != "" && namedItem12.Value != "")
                      {
                        if (namedItem10.Value == "kandydat" && namedItem12.Value == "lista")
                        {
                          validationPatern.addVariable(namedItem7.Value);
                          for (int index1 = 0; index1 < this.countcandidatesoflist.Count; ++index1)
                          {
                            for (int index2 = 0; index2 < this.countcandidatesoflist[index1][1]; ++index2)
                            {
                              string fieldName = namedItem7.Value.Replace(namedItem9.Value, (index2 + 1).ToString()).Replace(namedItem11.Value, (index1 + 1).ToString());
                              validationPatern.addField(fieldName, namedItem8.Value, true);
                            }
                          }
                        }
                        if (namedItem10.Value == "lista" && namedItem12.Value == "kandydat")
                        {
                          validationPatern.addVariable(namedItem7.Value);
                          for (int index1 = 1; index1 <= this.countcandidatesoflist.Count; ++index1)
                          {
                            for (int index2 = 1; index2 <= this.countcandidatesoflist[index1][1]; ++index2)
                            {
                              namedItem7.Value.Replace(namedItem9.Value, index1.ToString());
                              string fieldName = namedItem7.Value.Replace(namedItem11.Value, index2.ToString());
                              validationPatern.addField(fieldName, namedItem8.Value, true);
                            }
                          }
                        }
                      }
                      else if (namedItem7 != null && namedItem8 != null && (namedItem9 != null && namedItem10 != null) && (namedItem11 == null && namedItem12 == null) || namedItem7 != null && namedItem8 != null && (namedItem9 != null && namedItem10 != null) && (namedItem11 != null && namedItem12 != null && (namedItem9.Value != "" && namedItem10.Value != "")) && namedItem11.Value == "" && namedItem12.Value == "")
                      {
                        validationPatern.addVariable(namedItem7.Value);
                        if (namedItem10.Value == "kandydat" && this.countcandidatesoflist.Count == 1)
                        {
                          for (int index = 1; index <= this.countcandidatesoflist[0][1]; ++index)
                          {
                            string fieldName = namedItem7.Value.Replace(namedItem9.Value, index.ToString());
                            validationPatern.addField(fieldName, namedItem8.Value, true);
                          }
                        }
                        if (namedItem10.Value == "lista" && this.countcandidatesoflist.Count != 0)
                        {
                          for (int index = 1; index <= this.countcandidatesoflist.Count; ++index)
                          {
                            string fieldName = namedItem7.Value.Replace(namedItem9.Value, index.ToString());
                            validationPatern.addField(fieldName, namedItem8.Value, true);
                          }
                        }
                        if (namedItem10.Value == "kandydatZa" && this.countcandidatesoflist.Count == 3)
                        {
                          for (int index = 1; index <= this.countcandidatesoflist[1][1]; ++index)
                          {
                            string fieldName = namedItem7.Value.Replace(namedItem9.Value, index.ToString());
                            validationPatern.addField(fieldName, namedItem8.Value, true);
                          }
                        }
                        if (namedItem10.Value == "kandydatPrzeciw" && this.countcandidatesoflist.Count == 3)
                        {
                          for (int index = 1; index <= this.countcandidatesoflist[2][1]; ++index)
                          {
                            string fieldName = namedItem7.Value.Replace(namedItem9.Value, index.ToString());
                            validationPatern.addField(fieldName, namedItem8.Value, true);
                          }
                        }
                      }
                      else if (namedItem7 != null && namedItem8 != null)
                        validationPatern.addField(namedItem7.Value, namedItem8.Value);
                    }
                  }
                }
                validationPatern.SetValidationPatern(validationFor, paternLeft, paternRight, note, id, operation, type);
                this.lastValidators.Add(validationPatern);
              }
            }
          }
        }
      }
      catch (XmlException ex)
      {
        int num = (int) MessageBox.Show("Nieprawidłowy XML", "Błąd");
      }
      catch (NullReferenceException ex)
      {
        int num = (int) MessageBox.Show("Podanno inny xml niz definicja walidacji", "Błąd");
      }
    }

    private void getSignPage()
    {
      this.wait.setWaitPanel("Trwa ładowanie sekcji podpisu protokołu", "Proszę czekać");
      this.wait.setVisible(true);
      int x = 0;
      int y1 = 0;
      this.errorPanel.Visible = false;
      Label label = new Label();
      label.Text = "Wybierz licencję, którą chcesz podpisać protokół";
      label.AutoSize = true;
      label.MaximumSize = new Size(this.signPanel.Size.Width - 20, 0);
      label.Font = new Font(this.myfont, 9f, FontStyle.Bold);
      label.ForeColor = System.Drawing.Color.Black;
      label.Padding = new Padding(10, 0, 10, 0);
      label.Location = new Point(x, y1);
      int y2 = y1 + (label.Height + 20);
      this.signPanel.Controls.Add((Control) label);
      DataTable dataTable = new DataTable();
      if (Directory.Exists(this.path + "\\Licenses"))
      {
        dataTable.Columns.Add(new DataColumn("lp", typeof (string)));
        dataTable.Columns.Add(new DataColumn("Użytkownik", typeof (string)));
        dataTable.Columns.Add(new DataColumn("Funkcja", typeof (string)));
        dataTable.Columns.Add(new DataColumn("Licencja", typeof (string)));
        char[] chArray = this.zORp.ToCharArray();
        try
        {
          this.save.Load(this.savePath);
          string str1 = "";
          string str2 = "";
          string str3 = "";
          XmlNode xmlNode1 = this.save.SelectSingleNode("/save/header/jns_kod");
          if (xmlNode1 != null && xmlNode1.InnerText != "")
            str1 = xmlNode1.InnerText;
          XmlNode xmlNode2 = this.save.SelectSingleNode("/save/header/defklk");
          if (xmlNode2 != null && xmlNode2.FirstChild != null)
          {
            XmlNode namedItem = xmlNode2.FirstChild.Attributes.GetNamedItem("name");
            if (namedItem != null && namedItem.Value != "")
              str2 = namedItem.Value.Split('-')[0].Replace("/", "_");
          }
          XmlNode xmlNode3 = this.save.SelectSingleNode("/save/header/nrObwodu");
          if (xmlNode3 != null && xmlNode3.InnerText != "")
            str3 = xmlNode3.InnerText;
          string pattern1 = "";
          string pattern2 = "";
          string str4 = str1;
          string str5 = Convert.ToInt32(str1).ToString();
          if (str4.Length < 6)
          {
            while (str4.Length < 6)
              str4 = "0" + str4;
          }
          if (str4.Substring(0, 4) == "1465")
          {
            if (this.inst == "RDP" || this.inst == "RDW")
              pattern2 = "^" + str2 + "-146500-A_";
          }
          else if (this.inst == "RDP" || this.inst == "RDW" || this.inst == "WBP")
            pattern2 = "^" + str2 + "-" + Convert.ToInt32(str4.Substring(0, 4)).ToString() + "00-A_";
          if (str4.Substring(0, 4) == "1465")
          {
            if (((int) str4[4] != 0 || (int) str4[5] != 0) && this.inst == "RDA")
              pattern1 = "^" + str2 + "-" + str5 + "-A_";
          }
          else if ((int) str4[2] == 55 || (int) str4[2] == 54)
          {
            if (this.inst == "RDA" || this.inst == "RDW" || this.inst == "WBP")
              pattern1 = "^" + str2 + "-" + str5 + "-A_";
          }
          else if (this.inst == "RDA" || this.inst == "WBP")
            pattern1 = "^" + str2 + "-" + str5 + "-A_";
          Certificate certificate1 = new Certificate();
          foreach (string filename in Directory.EnumerateFiles(this.path + "\\Licenses", "*.pem"))
          {
            Certificate certificate2 = new Certificate(filename);
            if (certificate2.isActiveLicense())
            {
              string[] strArray1 = filename.Split('\\');
              bool flag = false;
              for (int index = 0; index < chArray.Length; ++index)
              {
                string str6 = "^" + str2 + "-" + str5 + "-" + chArray[index].ToString();
                if (chArray[index].ToString() == "P" || chArray[index].ToString() == "Z")
                  str6 = str6 + "-" + str3;
                string pattern3 = str6 + "_";
                string input = strArray1[strArray1.Length - 1];
                if (Regex.IsMatch(input, pattern3))
                  flag = true;
                if (pattern1 != "" && Regex.IsMatch(input, pattern1))
                  flag = true;
                if (pattern2 != "" && Regex.IsMatch(input, pattern2))
                  flag = true;
              }
              if (flag)
              {
                DataRow row = dataTable.NewRow();
                row[0] = (object) (dataTable.Rows.Count + 1);
                try
                {
                  string[] strArray2 = certificate2.getSubject().Split(new string[1]
                  {
                    ", "
                  }, StringSplitOptions.None);
                  int num = 0;
                  for (int index = 0; index < strArray2.Length; ++index)
                  {
                    if (Regex.IsMatch(strArray2[index], "^CN="))
                    {
                      row[1] = (object) strArray2[index].Replace("CN=", "");
                      ++num;
                    }
                    if (Regex.IsMatch(strArray2[index], "^OU="))
                    {
                      string[] strArray3 = strArray2[index].Replace("OU=", "").Split('-');
                      row[2] = !(strArray3[2] == "O") ? (!(strArray3[2] == "P") ? (!(strArray3[2] == "Z") ? (object) strArray3[2] : (object) "Zastępca") : (object) "Przewodniczący") : (object) "Operator";
                      ++num;
                    }
                    if (num >= 2)
                      break;
                  }
                }
                catch (Exception ex)
                {
                  row[1] = (object) "";
                  row[2] = (object) "";
                }
                row[3] = (object) strArray1[strArray1.Length - 1];
                dataTable.Rows.Add(row);
              }
            }
          }
        }
        catch (Exception ex)
        {
          int num = (int) MessageBox.Show("Nie można wyswietlic licencji do podpisu dla tego protokolu. " + ex.Message, "Error");
        }
        if (dataTable.Rows.Count > 0)
        {
          this.LicencesTable.DataSource = (object) dataTable;
          this.LicencesTable.Columns["lp"].DisplayIndex = 0;
          this.LicencesTable.Columns["Użytkownik"].DisplayIndex = 1;
          this.LicencesTable.Columns["Funkcja"].DisplayIndex = 2;
          DataGridViewButtonColumn viewButtonColumn = new DataGridViewButtonColumn();
          viewButtonColumn.HeaderText = "Akcje";
          viewButtonColumn.Text = "Podpisz";
          viewButtonColumn.Name = "action";
          viewButtonColumn.UseColumnTextForButtonValue = true;
          if (this.LicencesTable.Columns["action"] == null)
            this.LicencesTable.Columns.Insert(3, (DataGridViewColumn) viewButtonColumn);
          else
            this.LicencesTable.Columns["action"].DisplayIndex = 3;
          this.LicencesTable.Columns["Licencja"].DisplayIndex = 4;
          this.LicencesTable.Columns["Licencja"].Visible = false;
        }
      }
      this.LicencesTable.Visible = true;
      this.LicencesTable.Location = new Point(x, y2);
      this.LicencesTable.MaximumSize = new Size(this.signPanel.Size.Width - 20, 0);
      this.LicencesTable.Size = new Size(0, this.LicencesTable.Rows.Count * 20);
      this.LicencesTable.AutoSize = true;
      this.wait.setVisible(false);
    }

    private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
    {
      if (e.RowCount != 1)
        return;
      try
      {
        (sender as DataGridView).Rows[e.RowIndex - 1].Cells["Lp"].Value = (object) e.RowIndex.ToString();
        this.currentCommittee = false;
      }
      catch (ArgumentException ex)
      {
      }
    }

    private void DataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
    {
      Panel panel = this.bottomPanel;
      Point location = this.committeePanel.Location;
      int x = location.X;
      location = this.committeePanel.Location;
      int y = location.Y + this.committeePanel.Size.Height;
      Point point = new Point(x, y);
      panel.Location = point;
    }

    private void protocolForm1_Click(object sender, EventArgs e)
    {
      if (this.currentStep >= 3 || this.currentStep == 0)
      {
        this.edit();
        this.currentStep = 1;
        this.buttonNext.Text = "Dalej";
        this.buttonNext.Click -= new EventHandler(this.protocolForm2_Click);
        this.buttonNext.Click -= new EventHandler(this.protocolSummation_Click);
        this.buttonNext.Click -= new EventHandler(this.committee_Click);
        this.buttonNext.Click -= new EventHandler(this.committeeValid_Click);
        this.buttonNext.Click -= new EventHandler(this.signProtocol_Click);
        this.buttonNext.Click += new EventHandler(this.protocolForm2_Click);
        this.buttonNext.Enabled = true;
      }
      else
      {
        this.buttonNext.Visible = true;
        this.unlockHeader();
        int num = 1;
        foreach (Control control in (ArrangedElementCollection) this.protocolHeader.Controls)
        {
          if (control is TextBox)
          {
            (control as TextBox).TabIndex = num;
            ++num;
          }
          if (control is MaskedTextBox)
          {
            (control as MaskedTextBox).TabIndex = num;
            ++num;
          }
        }
        this.protocolHeader.TabIndex = 2;
        this.Form1panel.TabIndex = 3;
        this.Form1panel.Visible = true;
        this.Form2panel.Visible = false;
        this.committeePanel.Visible = false;
        this.SummationPanel.Visible = false;
        this.signPanel.Visible = false;
        this.protocolForm2.BackColor = SystemColors.Control;
        this.protocolForm1.BackColor = SystemColors.GradientInactiveCaption;
        this.protocolSummation.BackColor = SystemColors.Control;
        this.protocolCommittee.BackColor = SystemColors.Control;
        this.signProtocol.BackColor = SystemColors.Control;
        this.raportPanel.Visible = false;
        if (this.currentStep != 1)
        {
          this.buttonNext.Text = "Dalej";
          this.buttonNext.Enabled = false;
        }
        else
        {
          this.buttonNext.Text = "Dalej";
          this.buttonNext.Click -= new EventHandler(this.protocolForm2_Click);
          this.buttonNext.Click -= new EventHandler(this.protocolSummation_Click);
          this.buttonNext.Click -= new EventHandler(this.committee_Click);
          this.buttonNext.Click -= new EventHandler(this.committeeValid_Click);
          this.buttonNext.Click -= new EventHandler(this.signProtocol_Click);
          this.buttonNext.Click += new EventHandler(this.protocolForm2_Click);
          this.buttonNext.Enabled = true;
        }
        this.setFirstFocus();
        this.bottomPanel.Location = new Point(this.Form1panel.Location.X, this.Form1panel.Location.Y + this.Form1panel.Size.Height);
        this.saves(this.currentStep);
      }
    }

    private void protocolForm2_Click(object sender, EventArgs e)
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
        catch (Exception ex)
        {
        }
        this.wait.setVisible(false);
        this.error = !this.IsValid();
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
          this.protocolForm1.BackColor = SystemColors.Control;
          this.protocolForm2.BackColor = SystemColors.GradientInactiveCaption;
          this.protocolSummation.BackColor = SystemColors.Control;
          this.protocolCommittee.BackColor = SystemColors.Control;
          this.signProtocol.BackColor = SystemColors.Control;
        }
        else
        {
          this.setFirstFocus();
          this.printErrors(this.currentStep);
        }
        this.error = false;
      }
      else if (this.currentStep >= 3 || this.currentStep == 0)
      {
        this.edit();
        this.currentStep = 1;
        this.buttonNext.Text = "Dalej";
        this.buttonNext.Click -= new EventHandler(this.protocolForm2_Click);
        this.buttonNext.Click -= new EventHandler(this.protocolSummation_Click);
        this.buttonNext.Click -= new EventHandler(this.committee_Click);
        this.buttonNext.Click -= new EventHandler(this.committeeValid_Click);
        this.buttonNext.Click -= new EventHandler(this.signProtocol_Click);
        this.buttonNext.Click += new EventHandler(this.protocolForm2_Click);
        this.buttonNext.Enabled = true;
      }
      else
      {
        this.raportPanel.Visible = false;
        this.unlockHeader();
        int num = this.Form1panel.Controls.Count * 2;
        foreach (Control control in (ArrangedElementCollection) this.protocolHeader.Controls)
        {
          if (control is TextBox)
          {
            (control as TextBox).TabIndex = num;
            --num;
          }
          if (control is MaskedTextBox)
          {
            (control as MaskedTextBox).TabIndex = num;
            --num;
          }
        }
        this.Form2panel.TabIndex = 2;
        this.protocolHeader.TabIndex = 3;
        this.Form1panel.Visible = false;
        this.Form2panel.Visible = true;
        this.committeePanel.Visible = false;
        this.SummationPanel.Visible = false;
        this.protocolForm1.BackColor = SystemColors.Control;
        this.protocolForm2.BackColor = SystemColors.GradientInactiveCaption;
        this.protocolSummation.BackColor = SystemColors.Control;
        this.protocolCommittee.BackColor = SystemColors.Control;
        this.signProtocol.BackColor = SystemColors.Control;
        if (this.currentStep != 2)
        {
          this.buttonNext.Text = "Dalej";
          this.buttonNext.Enabled = false;
        }
        else
        {
          this.buttonNext.Text = "Dalej";
          this.buttonNext.Click -= new EventHandler(this.protocolForm2_Click);
          this.buttonNext.Click -= new EventHandler(this.protocolSummation_Click);
          this.buttonNext.Click -= new EventHandler(this.committee_Click);
          this.buttonNext.Click -= new EventHandler(this.committeeValid_Click);
          this.buttonNext.Click -= new EventHandler(this.signProtocol_Click);
          this.buttonNext.Click += new EventHandler(this.protocolSummation_Click);
          this.buttonNext.Enabled = true;
        }
        Panel panel = this.bottomPanel;
        Point location = this.Form2panel.Location;
        int x = location.X;
        location = this.Form2panel.Location;
        int y = location.Y + this.Form2panel.Size.Height;
        Point point = new Point(x, y);
        panel.Location = point;
        this.setLastFocus();
        this.saves(this.currentStep);
      }
    }

    private void protocolSummation_Click(object sender, EventArgs e)
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
        catch (Exception ex)
        {
        }
        this.wait.setVisible(false);
        this.error = !this.IsValid();
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
          this.protocolForm2.BackColor = SystemColors.Control;
          this.protocolForm1.BackColor = SystemColors.Control;
          this.protocolSummation.BackColor = SystemColors.GradientInactiveCaption;
          this.protocolCommittee.BackColor = SystemColors.Control;
          this.signProtocol.BackColor = SystemColors.Control;
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
        this.protocolForm2.BackColor = SystemColors.Control;
        this.protocolForm1.BackColor = SystemColors.Control;
        this.protocolSummation.BackColor = SystemColors.GradientInactiveCaption;
        this.protocolCommittee.BackColor = SystemColors.Control;
        this.signProtocol.BackColor = SystemColors.Control;
        if (this.currentStep != 3)
        {
          this.buttonNext.Text = "Dalej";
          this.buttonNext.Enabled = false;
        }
        else
        {
          this.buttonNext.Text = "Dalej";
          this.buttonNext.Click -= new EventHandler(this.protocolForm2_Click);
          this.buttonNext.Click -= new EventHandler(this.protocolSummation_Click);
          this.buttonNext.Click -= new EventHandler(this.committee_Click);
          this.buttonNext.Click -= new EventHandler(this.committeeValid_Click);
          this.buttonNext.Click -= new EventHandler(this.signProtocol_Click);
          this.buttonNext.Click += new EventHandler(this.committee_Click);
          this.buttonNext.Enabled = true;
        }
        this.setFirstFocus(this.SummationPanel);
        this.bottomPanel.Location = new Point(this.SummationPanel.Location.X, this.SummationPanel.Location.Y + this.SummationPanel.Size.Height);
      }
      this.saves(this.currentStep);
    }

    private void committee_Click(object sender, EventArgs e)
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
        this.error = !this.IsValid();
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
            string[] strArray = this.savePath.Split('\\');
            int num = (int) new EnteringCodes(strArray[strArray.Length - 1].Replace(".xml", ""), this.savePath, this.OU, this.licensePath, this).ShowDialog();
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
              this.committeePanel.MaximumSize = new Size(this.committeePanel.Size.Width, 0);
              this.committeePanel.Location = new Point(0, this.protocolHeader.Size.Height + this.protocolHeader.Location.Y);
              this.committeePanel.AutoSize = true;
              this.protocolForm2.BackColor = SystemColors.Control;
              this.protocolForm1.BackColor = SystemColors.Control;
              this.protocolSummation.BackColor = SystemColors.Control;
              this.protocolCommittee.BackColor = SystemColors.GradientInactiveCaption;
              this.signProtocol.BackColor = SystemColors.Control;
              this.getCommitee();
              this.buttonNext.Click -= new EventHandler(this.protocolForm2_Click);
              this.buttonNext.Click -= new EventHandler(this.protocolSummation_Click);
              this.buttonNext.Click -= new EventHandler(this.committee_Click);
              this.buttonNext.Click -= new EventHandler(this.committeeValid_Click);
              this.buttonNext.Click -= new EventHandler(this.signProtocol_Click);
              this.buttonNext.Click += new EventHandler(this.committeeValid_Click);
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
            this.committeePanel.MaximumSize = new Size(this.committeePanel.Size.Width, 0);
            this.committeePanel.Location = new Point(0, this.protocolHeader.Size.Height + this.protocolHeader.Location.Y);
            this.committeePanel.AutoSize = true;
            this.protocolForm2.BackColor = SystemColors.Control;
            this.protocolForm1.BackColor = SystemColors.Control;
            this.protocolSummation.BackColor = SystemColors.Control;
            this.protocolCommittee.BackColor = SystemColors.GradientInactiveCaption;
            this.signProtocol.BackColor = SystemColors.Control;
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
        this.committeePanel.MaximumSize = new Size(this.committeePanel.Size.Width, 0);
        Panel panel1 = this.committeePanel;
        int x1 = 0;
        int height = this.protocolHeader.Size.Height;
        Point location = this.protocolHeader.Location;
        int y1 = location.Y;
        int y2 = height + y1;
        Point point1 = new Point(x1, y2);
        panel1.Location = point1;
        this.committeePanel.AutoSize = true;
        this.protocolForm2.BackColor = SystemColors.Control;
        this.protocolForm1.BackColor = SystemColors.Control;
        this.protocolSummation.BackColor = SystemColors.Control;
        this.protocolCommittee.BackColor = SystemColors.GradientInactiveCaption;
        this.signProtocol.BackColor = SystemColors.Control;
        if (this.currentStep != 4)
        {
          this.buttonNext.Text = "Dalej";
          this.buttonNext.Enabled = false;
        }
        else
        {
          this.buttonNext.Text = "Dalej";
          this.buttonNext.Click -= new EventHandler(this.protocolForm2_Click);
          this.buttonNext.Click -= new EventHandler(this.protocolSummation_Click);
          this.buttonNext.Click -= new EventHandler(this.committee_Click);
          this.buttonNext.Click -= new EventHandler(this.committeeValid_Click);
          this.buttonNext.Click -= new EventHandler(this.signProtocol_Click);
          this.buttonNext.Click += new EventHandler(this.committeeValid_Click);
          this.buttonNext.Enabled = true;
        }
        this.setFirstFocus();
        Panel panel2 = this.bottomPanel;
        location = this.committeePanel.Location;
        int x2 = location.X;
        location = this.committeePanel.Location;
        int y3 = location.Y + this.committeePanel.Size.Height;
        Point point2 = new Point(x2, y3);
        panel2.Location = point2;
        this.saves(this.currentStep);
      }
      this.buttonNext.Enabled = true;
    }

    private void signProtocol_Click(object sender, EventArgs e)
    {
      this.protocolForm1.Enabled = false;
      this.protocolForm2.Enabled = false;
      this.protocolSummation.Enabled = false;
      this.protocolCommittee.Enabled = false;
      this.signProtocol.Enabled = true;
      this.protocolForm2.BackColor = SystemColors.Control;
      this.protocolForm1.BackColor = SystemColors.Control;
      this.protocolSummation.BackColor = SystemColors.Control;
      this.protocolCommittee.BackColor = SystemColors.Control;
      this.signProtocol.BackColor = SystemColors.GradientInactiveCaption;
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

    private void committeeValid_Click(object sender, EventArgs e)
    {
      try
      {
        XmlNode xmlNode = this.committee.SelectSingleNode("/komisja_sklad");
        int num1 = Convert.ToInt32(xmlNode.Attributes.GetNamedItem("l_wymaganych").Value);
        int num2 = Convert.ToInt32(xmlNode.Attributes.GetNamedItem("min_l_wymaganych").Value);
        int num3 = 0;
        int num4 = 0;
        int num5 = 0;
        int num6 = 0;
        string pattern = "^[^0-9<>&;:" + '"'.ToString() + "]+$";
        for (int index = 0; index < this.personList.Rows.Count - 1; ++index)
        {
          object obj1 = this.personList.Rows[index].Cells["action3"].Value;
          if (obj1 != null && obj1.ToString().ToLower() == "true")
            ++num5;
          object obj2 = this.personList.Rows[index].Cells["Funkcja"].Value;
          if (obj2 != null && obj2.ToString().ToUpper() == "PRZEWODNICZĄCY" && obj1 != null && obj1.ToString().ToLower() == "true")
            ++num3;
          if (obj2 != null && obj2.ToString().ToUpper() == "ZASTĘPCA" && obj1 != null && obj1.ToString().ToLower() == "true")
            ++num4;
          if (obj2 == null || obj2 != null && obj2.ToString() == "")
          {
            this.errorProvider1.SetErrorWithCount((Control) this.komisjaL1, "Nie wypełnione pole/a \"Funkcja\"", "FNW" + index.ToString(), "4");
            ++num6;
          }
          else
            this.errorProvider1.SetErrorWithCount((Control) this.komisjaL1, "", "FNW" + index.ToString(), "4");
          object obj3 = this.personList.Rows[index].Cells["Imię"].Value;
          if (obj3 == null || obj3 != null && obj3.ToString() == "")
          {
            this.errorProvider1.SetErrorWithCount((Control) this.komisjaL1, "Nie wypełnione pole/a \"Imię\"", "INW" + index.ToString(), "4");
            ++num6;
          }
          else
          {
            this.errorProvider1.SetErrorWithCount((Control) this.komisjaL1, "", "INW" + index.ToString(), "4");
            if (!Regex.IsMatch(obj3.ToString(), pattern))
            {
              this.errorProvider1.SetErrorWithCount((Control) this.komisjaL1, "Nieprawidłowo wypełnione pole/a \"Imię\"", "F-INW" + index.ToString(), "4");
              ++num6;
            }
            else
              this.errorProvider1.SetErrorWithCount((Control) this.komisjaL1, "", "F-INW" + index.ToString(), "4");
          }
          object obj4 = this.personList.Rows[index].Cells["Drugie imię"].Value;
          if (obj4 != null && obj4.ToString() != "")
          {
            if (!Regex.IsMatch(obj4.ToString(), pattern))
            {
              this.errorProvider1.SetErrorWithCount((Control) this.komisjaL1, "Nieprawidłowo wypełnione pole/a \"Drugie imię\"", "F-I2NW" + index.ToString(), "4");
              ++num6;
            }
            else
              this.errorProvider1.SetErrorWithCount((Control) this.komisjaL1, "", "F-I2NW" + index.ToString(), "4");
          }
          else
            this.errorProvider1.SetErrorWithCount((Control) this.komisjaL1, "", "F-I2NW" + index.ToString(), "4");
          object obj5 = this.personList.Rows[index].Cells["Nazwisko"].Value;
          if (obj5 == null || obj5 != null && obj5.ToString() == "")
          {
            this.errorProvider1.SetErrorWithCount((Control) this.komisjaL1, "Nie wypełnione pole/a \"Nazwisko\"", "NNW" + index.ToString(), "4");
            ++num6;
          }
          else
          {
            this.errorProvider1.SetErrorWithCount((Control) this.komisjaL1, "", "NNW" + index.ToString(), "4");
            if (!Regex.IsMatch(obj5.ToString(), pattern))
            {
              this.errorProvider1.SetErrorWithCount((Control) this.komisjaL1, "Nieprawidłowo wypełnione pole/a \"Nazwisko\"", "F-NNW" + index.ToString(), "4");
              ++num6;
            }
            else
              this.errorProvider1.SetErrorWithCount((Control) this.komisjaL1, "", "F-NNW" + index.ToString(), "4");
          }
        }
        int num7 = this.personList.Rows.Count - 1;
        int num8 = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(num7) / 2.0));
        int num9;
        if (num3 > 0)
        {
          this.errorProvider1.SetErrorWithCount((Control) this.komisjaL3, "", "SNM11", "4");
          this.errorProvider1.SetErrorWithCount((Control) this.komisjaL3, "", "SNM12", "4");
          if (num3 == 1)
          {
            this.errorProvider1.SetErrorWithCount((Control) this.komisjaL3, "", "SNM11a", "4");
            if (num7 < num2 || num7 > num1)
            {
              if (num2 == num1)
                this.errorProvider1.SetErrorWithCount((Control) this.komisjaL2, "Liczba osób w komisji powinna mieścić się w przedziale <" + (object) num2.ToString() + ", " + (string) (object) num1 + ">", "SNM10", "4");
              else
                this.errorProvider1.SetErrorWithCount((Control) this.komisjaL2, "Liczba osób w komisji powinna mieścić się w przedziale <" + (object) num2.ToString() + ", " + (string) (object) num1 + ">", "SNM10", "4");
              num9 = num6 + 1;
            }
            else if (num5 < num8)
            {
              this.errorProvider1.SetErrorWithCount((Control) this.komisjaL2, "Nie zebrało się kworum.", "SNM10a", "4");
            }
            else
            {
              this.errorProvider1.SetErrorWithCount((Control) this.komisjaL2, "", "SNM10a", "4");
              this.errorProvider1.SetErrorWithCount((Control) this.komisjaL2, "", "SNM10", "4");
              if (num6 == 0)
              {
                this.zORp = "P";
                if (num4 > 0)
                  this.zORp = "ZP";
                if (num4 == 1 || num4 == 0)
                {
                  this.errorProvider1.SetErrorWithCount((Control) this.komisjaL3, "", "SNM12a", "4");
                  this.errorProvider1.SetErrorWithCount((Control) this.komisjaL2, "", "SNM10", "4");
                  this.errorPanel.Visible = false;
                  this.signProtocol_Click(sender, e);
                  return;
                }
                else
                {
                  this.errorProvider1.SetErrorWithCount((Control) this.komisjaL3, "Jest więcej niż jeden zastępca", "SNM12a", "4");
                  num9 = num6 + 1;
                }
              }
            }
          }
          else
          {
            this.errorProvider1.SetErrorWithCount((Control) this.komisjaL3, "Jest więcej niż jeden przewodniczący", "SNM11a", "4");
            num9 = num6 + 1;
          }
        }
        else if (num4 > 0)
        {
          this.errorProvider1.SetErrorWithCount((Control) this.komisjaL3, "", "SNM11", "4");
          this.errorProvider1.SetErrorWithCount((Control) this.komisjaL3, "", "SNM12", "4");
          if (num4 == 1)
          {
            this.errorProvider1.SetErrorWithCount((Control) this.komisjaL3, "", "SNM12a", "4");
            if (num7 < num2 || num7 > num1)
            {
              if (num2 == num1)
                this.errorProvider1.SetErrorWithCount((Control) this.komisjaL2, "Liczba osób w komisji powinna mieścić się w przedziale <" + (object) num2.ToString() + ", " + (string) (object) num1 + ">", "SNM10", "4");
              else
                this.errorProvider1.SetErrorWithCount((Control) this.komisjaL2, "Liczba osób w komisji powinna mieścić się w przedziale <" + (object) num2.ToString() + ", " + (string) (object) num1 + ">", "SNM10", "4");
              num9 = num6 + 1;
            }
            else if (num5 < num8)
            {
              this.errorProvider1.SetErrorWithCount((Control) this.komisjaL2, "Nie zebrało się kworum.", "SNM10a", "4");
            }
            else
            {
              this.errorProvider1.SetErrorWithCount((Control) this.komisjaL2, "", "SNM10a", "4");
              this.errorProvider1.SetErrorWithCount((Control) this.komisjaL2, "", "SNM10", "4");
              if (num6 == 0)
              {
                this.zORp = "Z";
                this.errorProvider1.SetErrorWithCount((Control) this.komisjaL2, num1.ToString() ?? "", "SNM10", "4");
                this.errorPanel.Visible = false;
                this.signProtocol_Click(sender, e);
                return;
              }
            }
          }
          else
            this.errorProvider1.SetErrorWithCount((Control) this.komisjaL3, "Jest więcej niż jeden zastępca", "SNM12a", "4");
        }
        else
        {
          this.errorProvider1.SetErrorWithCount((Control) this.komisjaL3, "Brakuje osoby o funkcji \"PRZEWODNICZĄCY\"", "SNM11", "4");
          this.errorProvider1.SetErrorWithCount((Control) this.komisjaL3, "Brakuje osoby o funkcji \"ZASTĘPCA\"", "SNM12", "4");
          int num10 = num6 + 1;
          if (num7 < num2 || num7 > num1)
          {
            if (num2 == num1)
              this.errorProvider1.SetErrorWithCount((Control) this.komisjaL2, "Liczba osób w komisji powinna mieścić się w przedziale <" + (object) num2.ToString() + ", " + (string) (object) num1 + ">", "SNM10", "4");
            else
              this.errorProvider1.SetErrorWithCount((Control) this.komisjaL2, "Liczba osób w komisji powinna mieścić się w przedziale <" + (object) num2.ToString() + ", " + (string) (object) num1 + ">", "SNM10", "4");
            num9 = num10 + 1;
          }
          else if (num5 < num8)
          {
            this.errorProvider1.SetErrorWithCount((Control) this.komisjaL2, "Nie zebrało się kworum.", "SNM10a", "4");
          }
          else
          {
            this.errorProvider1.SetErrorWithCount((Control) this.komisjaL2, "", "SNM10a", "4");
            this.errorProvider1.SetErrorWithCount((Control) this.komisjaL2, "", "SNM10", "4");
          }
        }
      }
      catch (XmlException ex)
      {
        int num = (int) MessageBox.Show("Nieprawidłowy XML", "Błąd");
      }
      catch (NullReferenceException ex)
      {
        int num = (int) MessageBox.Show("Podanno inny xml niz header", "Błąd");
      }
      this.printErrors(this.currentStep);
    }

    private Dictionary<string, Errors> committeeValid(Dictionary<string, Errors> errorsD)
    {
      try
      {
        XmlNode xmlNode = this.committee.SelectSingleNode("/komisja_sklad");
        int num1 = Convert.ToInt32(xmlNode.Attributes.GetNamedItem("l_wymaganych").Value);
        int num2 = Convert.ToInt32(xmlNode.Attributes.GetNamedItem("min_l_wymaganych").Value);
        int num3 = 0;
        int num4 = 0;
        int num5 = 0;
        string pattern = "^[^0-9<>&;:" + '"'.ToString() + "]+$";
        for (int index = 0; index < this.personList.Rows.Count - 1; ++index)
        {
          object obj1 = this.personList.Rows[index].Cells["action3"].Value;
          if (obj1 != null && obj1.ToString().ToLower() == "true")
            ++num5;
          object obj2 = this.personList.Rows[index].Cells["Funkcja"].Value;
          if (obj2 != null && obj2.ToString().ToUpper() == "PRZEWODNICZĄCY" && obj1 != null && obj1.ToString().ToLower() == "true")
            ++num3;
          object obj3 = this.personList.Rows[index].Cells["Funkcja"].Value;
          if (obj3 != null && obj3.ToString().ToUpper() == "ZASTĘPCA" && obj1 != null && obj1.ToString().ToLower() == "true")
            ++num4;
          object obj4 = this.personList.Rows[index].Cells["Funkcja"].Value;
          if (obj4 == null || obj4 != null && obj4.ToString() == "")
          {
            try
            {
              errorsD["gridKomisja"].addHardError("FNW" + index.ToString());
            }
            catch (KeyNotFoundException ex)
            {
              errorsD.Add("gridKomisja", new Errors());
              errorsD["gridKomisja"].addHardError("FNW" + index.ToString());
            }
          }
          object obj5 = this.personList.Rows[index].Cells["Imię"].Value;
          if (obj5 == null || obj5 != null && obj5.ToString() == "")
          {
            try
            {
              errorsD["gridKomisja"].addHardError("INW" + index.ToString());
            }
            catch (KeyNotFoundException ex)
            {
              errorsD.Add("gridKomisja", new Errors());
              errorsD["gridKomisja"].addHardError("INW" + index.ToString());
            }
          }
          else if (!Regex.IsMatch(obj5.ToString(), pattern))
          {
            try
            {
              errorsD["gridKomisja"].addHardError("F-INW" + index.ToString());
            }
            catch (KeyNotFoundException ex)
            {
              errorsD.Add("gridKomisja", new Errors());
              errorsD["gridKomisja"].addHardError("F-INW" + index.ToString());
            }
          }
          object obj6 = this.personList.Rows[index].Cells["Drugie imię"].Value;
          if (obj6 != null && obj6.ToString() != "")
          {
            if (!Regex.IsMatch(obj6.ToString(), pattern))
            {
              try
              {
                errorsD["gridKomisja"].addHardError("F-I2NW" + index.ToString());
              }
              catch (KeyNotFoundException ex)
              {
                errorsD.Add("gridKomisja", new Errors());
                errorsD["gridKomisja"].addHardError("F-I2NW" + index.ToString());
              }
            }
          }
          object obj7 = this.personList.Rows[index].Cells["Nazwisko"].Value;
          if (obj7 == null || obj7 != null && obj7.ToString() == "")
          {
            try
            {
              errorsD["gridKomisja"].addHardError("NNW" + index.ToString());
            }
            catch (KeyNotFoundException ex)
            {
              errorsD.Add("gridKomisja", new Errors());
              errorsD["gridKomisja"].addHardError("NNW" + index.ToString());
            }
          }
          else if (!Regex.IsMatch(obj7.ToString(), pattern))
          {
            try
            {
              errorsD["gridKomisja"].addHardError("F-NNW" + index.ToString());
            }
            catch (KeyNotFoundException ex)
            {
              errorsD.Add("gridKomisja", new Errors());
              errorsD["gridKomisja"].addHardError("F-NNW" + index.ToString());
            }
          }
        }
        int num6 = this.personList.Rows.Count - 1;
        int num7 = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(num6) / 2.0));
        if (num3 > 0)
        {
          if (num6 < num2 || num6 > num1)
          {
            try
            {
              errorsD["gridKomisja"].addHardError("SNM10");
            }
            catch (KeyNotFoundException ex)
            {
              errorsD.Add("gridKomisja", new Errors());
              errorsD["gridKomisja"].addHardError("SNM10");
            }
          }
          if (num5 < num7)
          {
            try
            {
              errorsD["gridKomisja"].addHardError("SNM10a");
            }
            catch (KeyNotFoundException ex)
            {
              errorsD.Add("gridKomisja", new Errors());
              errorsD["gridKomisja"].addHardError("SNM10a");
            }
          }
          if (num4 > 0 && num4 != 1)
          {
            try
            {
              errorsD["gridKomisja"].addHardError("SNM12a");
            }
            catch (KeyNotFoundException ex)
            {
              errorsD.Add("gridKomisja", new Errors());
              errorsD["gridKomisja"].addHardError("SNM12a");
            }
          }
          if (num3 != 1)
          {
            try
            {
              errorsD["gridKomisja"].addHardError("SNM11a");
            }
            catch (KeyNotFoundException ex)
            {
              errorsD.Add("gridKomisja", new Errors());
              errorsD["gridKomisja"].addHardError("SNM11a");
            }
          }
        }
        else if (num4 > 0)
        {
          if (num4 != 1)
          {
            try
            {
              errorsD["gridKomisja"].addHardError("SNM12a");
            }
            catch (KeyNotFoundException ex)
            {
              errorsD.Add("gridKomisja", new Errors());
              errorsD["gridKomisja"].addHardError("SNM12a");
            }
          }
          if (num6 < num2 || num6 > num1)
          {
            try
            {
              errorsD["gridKomisja"].addHardError("SNM10");
            }
            catch (KeyNotFoundException ex)
            {
              errorsD.Add("gridKomisja", new Errors());
              errorsD["gridKomisja"].addHardError("SNM10");
            }
          }
          if (num5 < num7)
          {
            try
            {
              errorsD["gridKomisja"].addHardError("SNM10a");
            }
            catch (KeyNotFoundException ex)
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
          catch (KeyNotFoundException ex)
          {
            errorsD.Add("gridKomisja", new Errors());
            errorsD["gridKomisja"].addHardError("SNM11");
          }
          try
          {
            errorsD["gridKomisja"].addHardError("SNM12");
          }
          catch (KeyNotFoundException ex)
          {
            errorsD.Add("gridKomisja", new Errors());
            errorsD["gridKomisja"].addHardError("SNM12");
          }
          if (num6 < num2 || num6 > num1)
          {
            try
            {
              errorsD["gridKomisja"].addHardError("SNM10");
            }
            catch (KeyNotFoundException ex)
            {
              errorsD.Add("gridKomisja", new Errors());
              errorsD["gridKomisja"].addHardError("SNM10");
            }
          }
          if (num5 < num7)
          {
            try
            {
              errorsD["gridKomisja"].addHardError("SNM10a");
            }
            catch (KeyNotFoundException ex)
            {
              errorsD.Add("gridKomisja", new Errors());
              errorsD["gridKomisja"].addHardError("SNM10a");
            }
          }
        }
      }
      catch (XmlException ex)
      {
        int num = (int) MessageBox.Show("Nieprawidłowy XML", "Błąd");
      }
      catch (NullReferenceException ex)
      {
        int num = (int) MessageBox.Show("Podanno inny xml niz header", "Błąd");
      }
      return errorsD;
    }

    private void text_Validated(object sender, EventArgs e)
    {
      string pattern = "^[0-9a-ząćęłńóśźżA-ZĄĆĘŁŃÓŚŹŻÄÖÜËäöüë() ,\\.:\\-–_]+$";
      if (!((sender as TextBox).Text != ""))
        return;
      if (!Regex.IsMatch((sender as TextBox).Text, pattern))
      {
        this.errorProvider1.SetErrorWithCount((Control) (sender as TextBox), "Dozwolone tylko litery, cyfry, spacja, przecinek, kropka, dwukropek, myslnik, podłoga i nawiasy okrągłe", "ErrorType", this.stepControl(sender));
        (sender as TextBox).ForeColor = System.Drawing.Color.Red;
        (sender as TextBox).BackColor = SystemColors.Info;
        this.error = true;
      }
      else
      {
        this.errorProvider1.SetErrorWithCount((Control) (sender as TextBox), "", "ErrorType", this.stepControl(sender));
        if (!this.errorProvider1.hasError((Control) (sender as TextBox)))
        {
          (sender as TextBox).ForeColor = System.Drawing.Color.Black;
          (sender as TextBox).BackColor = SystemColors.Window;
        }
      }
    }

    private bool text_isValid(string value)
    {
      string pattern = "^[0-9a-ząćęłńóśźżA-ZĄĆĘŁŃÓŚŹŻÄÖÜËäöüë() ,\\.:\\-–_]+$";
      return value != "" && Regex.IsMatch(value, pattern);
    }

    private void normalText_Validated(object sender, EventArgs e)
    {
      string pattern = "^[0-9a-ząćęłńóśźżA-ZĄĆĘŁŃÓŚŹŻÄÖÜËäöüë() ,\\.:\\-–_\r\n]+$";
      if ((sender as TextBox).Text != "")
      {
        if (!Regex.IsMatch((sender as TextBox).Text, pattern))
        {
          this.errorProvider1.SetErrorWithCount((Control) (sender as TextBox), "Dozwolone tylko litery, cyfry, spacja, przecinek, kropka, dwukropek, myslnik, podłoga i nawiasy okrągłe", "ErrorType", this.stepControl(sender));
          (sender as TextBox).ForeColor = System.Drawing.Color.Red;
          (sender as TextBox).BackColor = SystemColors.Info;
          this.error = true;
        }
        else
        {
          char[] chArray = new char[8]
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
          if ((sender as TextBox).Text.Trim(chArray) == "")
          {
            this.errorProvider1.SetErrorWithCount((Control) (sender as TextBox), "Pole nie moze byc puste", "ErrorType", this.stepControl(sender));
            (sender as TextBox).ForeColor = System.Drawing.Color.Red;
            (sender as TextBox).BackColor = SystemColors.Info;
          }
          else
          {
            this.errorProvider1.SetErrorWithCount((Control) (sender as TextBox), "", "ErrorType", this.stepControl(sender));
            if (!this.errorProvider1.hasError((Control) (sender as TextBox)))
            {
              (sender as TextBox).ForeColor = System.Drawing.Color.Black;
              (sender as TextBox).BackColor = SystemColors.Window;
            }
          }
        }
      }
      else
      {
        char[] chArray = new char[8]
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
        if ((sender as TextBox).Text.Trim(chArray) == "")
        {
          this.errorProvider1.SetErrorWithCount((Control) (sender as TextBox), "Pole nie moze byc puste", "ErrorType", this.stepControl(sender));
          (sender as TextBox).ForeColor = System.Drawing.Color.Red;
          (sender as TextBox).BackColor = SystemColors.Info;
        }
      }
    }

    private bool normalText_isValid(string value)
    {
      string pattern = "^[0-9a-ząćęłńóśźżA-ZĄĆĘŁŃÓŚŹŻÄÖÜËäöüë() ,\\.:\\-–_\r\n]+$";
      if (!(value != "") || !Regex.IsMatch(value, pattern))
        return false;
      char[] chArray = new char[8]
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
      return !(value.Trim(chArray) == "");
    }

    private void number_Validated(object sender, EventArgs e)
    {
      string pattern = "^[0-9]+$";
      if ((sender as TextBox).Text != "")
      {
        if (!Regex.IsMatch((sender as TextBox).Text, pattern))
        {
          this.errorProvider1.SetErrorWithCount((Control) (sender as TextBox), "Dozwolone tylko cyfry (liczby nie ujemne)", "ErrorType", this.stepControl(sender));
          (sender as TextBox).ForeColor = System.Drawing.Color.Red;
          (sender as TextBox).BackColor = SystemColors.Info;
        }
        else
        {
          this.errorProvider1.SetErrorWithCount((Control) (sender as TextBox), "", "ErrorType", this.stepControl(sender));
          if (!this.errorProvider1.hasError((Control) (sender as TextBox)))
          {
            (sender as TextBox).ForeColor = System.Drawing.Color.Black;
            (sender as TextBox).BackColor = SystemColors.Window;
          }
          (sender as TextBox).Text = Convert.ToInt32((sender as TextBox).Text).ToString();
        }
      }
      else
      {
        this.errorProvider1.SetErrorWithCount((Control) (sender as TextBox), "", "ErrorType", this.stepControl(sender));
        if (!this.errorProvider1.hasError((Control) (sender as TextBox)))
        {
          (sender as TextBox).ForeColor = System.Drawing.Color.Black;
          (sender as TextBox).BackColor = SystemColors.Window;
        }
        (sender as TextBox).Text = "";
      }
    }

    private void date_Validated(object sender, EventArgs e)
    {
      string pattern = "^[0-9]{2}-[0-9]{2}-[0-9]{4}$";
      if ((sender as MaskedTextBox).Text != "")
      {
        if (!Regex.IsMatch((sender as MaskedTextBox).Text, pattern))
        {
          this.errorProvider1.SetErrorWithCount((Control) (sender as MaskedTextBox), "Zły format daty", "ErrorType", this.stepControl(sender));
          (sender as MaskedTextBox).ForeColor = System.Drawing.Color.Red;
          (sender as MaskedTextBox).BackColor = SystemColors.Info;
          this.error = true;
        }
        else
        {
          try
          {
            string[] strArray = (sender as MaskedTextBox).Text.Split('-');
            DateTime dateTime = new DateTime(Convert.ToInt32(strArray[2]), Convert.ToInt32(strArray[1]), Convert.ToInt32(strArray[0]));
            this.errorProvider1.SetErrorWithCount((Control) (sender as MaskedTextBox), "", "ErrorType", this.stepControl(sender));
            if (!this.errorProvider1.hasError((Control) (sender as MaskedTextBox)))
            {
              (sender as MaskedTextBox).ForeColor = System.Drawing.Color.Black;
              (sender as MaskedTextBox).BackColor = SystemColors.Window;
            }
          }
          catch (ArgumentOutOfRangeException ex)
          {
            this.errorProvider1.SetErrorWithCount((Control) (sender as MaskedTextBox), "Nieprawidłowa data", "ErrorType", this.stepControl(sender));
            (sender as MaskedTextBox).ForeColor = System.Drawing.Color.Red;
            (sender as MaskedTextBox).BackColor = SystemColors.Info;
            this.error = true;
          }
        }
      }
      else
      {
        this.errorProvider1.SetErrorWithCount((Control) (sender as MaskedTextBox), "Pole wymagane", "ErrorNull", this.stepControl(sender));
        (sender as MaskedTextBox).ForeColor = System.Drawing.Color.Red;
        (sender as MaskedTextBox).BackColor = SystemColors.Info;
        this.error = true;
      }
    }

    private void time_Validated(object sender, EventArgs e)
    {
      string pattern = "^[0-9]{2}:[0-9]{2}$";
      if ((sender as MaskedTextBox).Text != "")
      {
        if (!Regex.IsMatch((sender as MaskedTextBox).Text, pattern))
        {
          this.errorProvider1.SetErrorWithCount((Control) (sender as MaskedTextBox), "Zły format czasu", "ErrorType", this.stepControl(sender));
          (sender as MaskedTextBox).ForeColor = System.Drawing.Color.Red;
          (sender as MaskedTextBox).BackColor = SystemColors.Info;
          this.error = true;
        }
        else
        {
          try
          {
            string[] strArray = (sender as MaskedTextBox).Text.Split(':');
            DateTime dateTime = new DateTime(2014, 1, 1, Convert.ToInt32(strArray[0]), Convert.ToInt32(strArray[1]), 0);
            this.errorProvider1.SetErrorWithCount((Control) (sender as MaskedTextBox), "", "ErrorType", this.stepControl(sender));
            if (!this.errorProvider1.hasError((Control) (sender as MaskedTextBox)))
            {
              (sender as MaskedTextBox).ForeColor = System.Drawing.Color.Black;
              (sender as MaskedTextBox).BackColor = SystemColors.Window;
            }
          }
          catch (ArgumentOutOfRangeException ex)
          {
            this.errorProvider1.SetErrorWithCount((Control) (sender as MaskedTextBox), "Nieprawidłowy czas", "ErrorType", this.stepControl(sender));
            (sender as MaskedTextBox).ForeColor = System.Drawing.Color.Red;
            (sender as MaskedTextBox).BackColor = SystemColors.Info;
            this.error = true;
          }
        }
      }
      else
      {
        this.errorProvider1.SetErrorWithCount((Control) (sender as MaskedTextBox), "Pole wymagane", "ErrorNull", this.stepControl(sender));
        (sender as MaskedTextBox).ForeColor = System.Drawing.Color.Red;
        (sender as MaskedTextBox).BackColor = SystemColors.Info;
        this.error = true;
      }
    }

    private void dateTime_Validated(object sender, EventArgs e)
    {
      string pattern = "^[0-9]{2}-[0-9]{2}-[0-9]{4} [0-9]{2}:[0-9]{2}$";
      if ((sender as MaskedTextBox).Text != "")
      {
        if (!Regex.IsMatch((sender as MaskedTextBox).Text, pattern))
        {
          this.errorProvider1.SetErrorWithCount((Control) (sender as MaskedTextBox), "Zły format daty i czasu", "ErrorType", this.stepControl(sender));
          (sender as MaskedTextBox).ForeColor = System.Drawing.Color.Red;
          (sender as MaskedTextBox).BackColor = SystemColors.Info;
          this.error = true;
        }
        else
        {
          try
          {
            string[] strArray = (sender as MaskedTextBox).Text.Split(new char[3]
            {
              '-',
              ' ',
              ':'
            });
            DateTime dateTime = new DateTime(Convert.ToInt32(strArray[2]), Convert.ToInt32(strArray[1]), Convert.ToInt32(strArray[0]), Convert.ToInt32(strArray[3]), Convert.ToInt32(strArray[4]), 0);
            this.errorProvider1.SetErrorWithCount((Control) (sender as MaskedTextBox), "", "ErrorType", this.stepControl(sender));
            if (!this.errorProvider1.hasError((Control) (sender as MaskedTextBox)))
            {
              (sender as MaskedTextBox).ForeColor = System.Drawing.Color.Black;
              (sender as MaskedTextBox).BackColor = SystemColors.Window;
            }
          }
          catch (ArgumentOutOfRangeException ex)
          {
            this.errorProvider1.SetErrorWithCount((Control) (sender as MaskedTextBox), "Nieprawidłowa data lub czas", "ErrorType", this.stepControl(sender));
            (sender as MaskedTextBox).ForeColor = System.Drawing.Color.Red;
            (sender as MaskedTextBox).BackColor = SystemColors.Info;
            this.error = true;
          }
        }
      }
      else
      {
        this.errorProvider1.SetErrorWithCount((Control) (sender as MaskedTextBox), "Pole wymagane", "ErrorNull", this.stepControl(sender));
        (sender as MaskedTextBox).ForeColor = System.Drawing.Color.Red;
        (sender as MaskedTextBox).BackColor = SystemColors.Info;
        this.error = true;
      }
    }

    private bool date_isValid(string value)
    {
      string pattern = "^[0-9]{2}-[0-9]{2}-[0-9]{4}$";
      if (!(value != ""))
        return false;
      if (!Regex.IsMatch(value, pattern))
        return false;
      try
      {
        string[] strArray = value.Split('-');
        DateTime dateTime = new DateTime(Convert.ToInt32(strArray[2]), Convert.ToInt32(strArray[1]), Convert.ToInt32(strArray[0]));
        return true;
      }
      catch (ArgumentOutOfRangeException ex)
      {
        return false;
      }
    }

    private bool time_isValid(string value)
    {
      string pattern = "^[0-9]{2}:[0-9]{2}$";
      if (!(value != ""))
        return false;
      if (!Regex.IsMatch(value, pattern))
        return false;
      try
      {
        string[] strArray = value.Split(':');
        DateTime dateTime = new DateTime(2014, 1, 1, Convert.ToInt32(strArray[0]), Convert.ToInt32(strArray[1]), 0);
        return true;
      }
      catch (ArgumentOutOfRangeException ex)
      {
        return false;
      }
    }

    private bool dateTime_isValid(string value)
    {
      string pattern = "^[0-9]{2}-[0-9]{2}-[0-9]{4} [0-9]{2}:[0-9]{2}$";
      if (!(value != ""))
        return false;
      if (!Regex.IsMatch(value, pattern))
        return false;
      try
      {
        string[] strArray = value.Split(new char[3]
        {
          '-',
          ' ',
          ':'
        });
        DateTime dateTime = new DateTime(Convert.ToInt32(strArray[2]), Convert.ToInt32(strArray[1]), Convert.ToInt32(strArray[0]), Convert.ToInt32(strArray[3]), Convert.ToInt32(strArray[4]), 0);
        return true;
      }
      catch (ArgumentOutOfRangeException ex)
      {
        return false;
      }
    }

    private bool number_isValid(string value)
    {
      string pattern = "^[0-9]+$";
      return value != "" && Regex.IsMatch(value, pattern);
    }

    private void lastValidation(Panel p)
    {
      try
      {
        for (int index1 = 0; index1 < this.lastValidators.Count; ++index1)
        {
          this.lastValidators[index1].getId();
          int fieldsCount = this.lastValidators[index1].getFieldsCount();
          List<string[]> fieldsValues = new List<string[]>();
          for (int index2 = 0; index2 < fieldsCount; ++index2)
          {
            string nameField = this.lastValidators[index1].getNameField(index2);
            if (p.Controls[nameField] != null)
              fieldsValues.Add(new string[2]
              {
                nameField,
                p.Controls[nameField].Text
              });
            int count = fieldsValues.Count;
            if (nameField == "lwyb")
              fieldsValues.Add(new string[2]
              {
                nameField,
                this.lwyb
              });
            if (nameField == "lwybA")
              fieldsValues.Add(new string[2]
              {
                nameField,
                this.lwybA
              });
            if (nameField == "lwybB")
              fieldsValues.Add(new string[2]
              {
                nameField,
                this.lwybB
              });
            if (nameField == "plusminus")
              fieldsValues.Add(new string[2]
              {
                nameField,
                this.plusminus
              });
            if (nameField == "plus")
              fieldsValues.Add(new string[2]
              {
                nameField,
                this.plus
              });
            if (nameField == "minus")
              fieldsValues.Add(new string[2]
              {
                nameField,
                this.minus
              });
            if (nameField == "naklad")
            {
              string str = this.naklad.Replace("%", "");
              if (str.Length == 2)
              {
                try
                {
                  double num = Convert.ToDouble(this.lwyb) * Convert.ToDouble((double) (100 / Convert.ToInt32(str)));
                  fieldsValues.Add(new string[2]
                  {
                    nameField,
                    num.ToString()
                  });
                }
                catch (Exception ex)
                {
                  int num = (int) MessageBox.Show("Komunikat błedu: ProtocolForm.lastValidation: " + ex.Message, "Uwaga");
                }
              }
              if (str.Length == 1)
              {
                double num = Convert.ToDouble(this.lwyb) * Convert.ToDouble((double) (100 / Convert.ToInt32(str)));
                fieldsValues.Add(new string[2]
                {
                  nameField,
                  num.ToString()
                });
              }
            }
            if (nameField == "naklad%")
            {
              string str = this.naklad.Replace("%", "");
              if (str.Length > 0 && str.Length <= 3)
              {
                try
                {
                  fieldsValues.Add(new string[2]
                  {
                    nameField,
                    ((object) str).ToString()
                  });
                }
                catch (Exception ex)
                {
                  int num = (int) MessageBox.Show("Komunikat błedu: ProtocolForm.lastValidation: " + ex.Message, "Uwaga");
                }
              }
            }
          }
          bool flag = this.lastValidators[index1].valid(fieldsValues);
          for (int index2 = 0; index2 < fieldsCount; ++index2)
          {
            int num;
            if (!this.lastValidators[index1].containVariables(index2))
            {
              string nameField = this.lastValidators[index1].getNameField(index2);
              if (p.Controls[nameField] != null)
              {
                num = 1;
                if (p.Name == "Form2panel")
                {
                  if (flag)
                  {
                    if (this.lastValidators[index1].getErrorType() == ErrorType.HardError || this.lastValidators[index1].getErrorType() == ErrorType.Null)
                      this.errorProvider1.SetErrorWithCount(this.Form1panel.Controls[nameField], "", this.lastValidators[index1].getId(), num.ToString());
                    if (this.lastValidators[index1].getErrorType() == ErrorType.HardWarning)
                      this.errorProvider1.SetErrorWithCount(this.Form1panel.Controls[nameField], "", 2, this.lastValidators[index1].getId(), num.ToString());
                    if (this.lastValidators[index1].getErrorType() == ErrorType.Soft)
                      this.errorProvider1.SetErrorWithCount(this.Form1panel.Controls[nameField], "", 3, this.lastValidators[index1].getId(), num.ToString());
                    if (this.Form1panel.Controls[nameField].Enabled && !this.errorProvider1.hasError(this.Form1panel.Controls[nameField]))
                    {
                      this.Form1panel.Controls[nameField].ForeColor = System.Drawing.Color.Black;
                      this.Form1panel.Controls[nameField].BackColor = SystemColors.Window;
                    }
                  }
                  else
                  {
                    if (this.lastValidators[index1].getErrorType() == ErrorType.HardError || this.lastValidators[index1].getErrorType() == ErrorType.Null)
                      this.errorProvider1.SetErrorWithCount(this.Form1panel.Controls[nameField], this.lastValidators[index1].getNote(), this.lastValidators[index1].getId(), num.ToString());
                    if (this.lastValidators[index1].getErrorType() == ErrorType.HardWarning)
                      this.errorProvider1.SetErrorWithCount(this.Form1panel.Controls[nameField], this.lastValidators[index1].getNote(), 2, this.lastValidators[index1].getId(), num.ToString());
                    if (this.lastValidators[index1].getErrorType() == ErrorType.Soft)
                      this.errorProvider1.SetErrorWithCount(this.Form1panel.Controls[nameField], this.lastValidators[index1].getNote(), 3, this.lastValidators[index1].getId(), num.ToString());
                    if (this.Form1panel.Controls[nameField].Enabled)
                    {
                      this.Form1panel.Controls[nameField].ForeColor = System.Drawing.Color.Red;
                      this.Form1panel.Controls[nameField].BackColor = SystemColors.Info;
                    }
                  }
                  num = 2;
                }
                if (flag)
                {
                  if (this.lastValidators[index1].getErrorType() == ErrorType.HardError || this.lastValidators[index1].getErrorType() == ErrorType.Null)
                    this.errorProvider1.SetErrorWithCount(p.Controls[nameField], "", this.lastValidators[index1].getId(), num.ToString());
                  if (this.lastValidators[index1].getErrorType() == ErrorType.HardWarning)
                    this.errorProvider1.SetErrorWithCount(p.Controls[nameField], "", 2, this.lastValidators[index1].getId(), num.ToString());
                  if (this.lastValidators[index1].getErrorType() == ErrorType.Soft)
                    this.errorProvider1.SetErrorWithCount(p.Controls[nameField], "", 3, this.lastValidators[index1].getId(), num.ToString());
                  if (p.Controls[nameField].Enabled && !this.errorProvider1.hasError(p.Controls[nameField]))
                  {
                    p.Controls[nameField].ForeColor = System.Drawing.Color.Black;
                    p.Controls[nameField].BackColor = SystemColors.Window;
                  }
                }
                else
                {
                  if (this.lastValidators[index1].getErrorType() == ErrorType.HardError || this.lastValidators[index1].getErrorType() == ErrorType.Null)
                    this.errorProvider1.SetErrorWithCount(p.Controls[nameField], this.lastValidators[index1].getNote(), this.lastValidators[index1].getId(), num.ToString());
                  if (this.lastValidators[index1].getErrorType() == ErrorType.HardWarning)
                    this.errorProvider1.SetErrorWithCount(p.Controls[nameField], this.lastValidators[index1].getNote(), 2, this.lastValidators[index1].getId(), num.ToString());
                  if (this.lastValidators[index1].getErrorType() == ErrorType.Soft)
                    this.errorProvider1.SetErrorWithCount(p.Controls[nameField], this.lastValidators[index1].getNote(), 3, this.lastValidators[index1].getId(), num.ToString());
                  if (p.Controls[nameField].Enabled)
                  {
                    p.Controls[nameField].ForeColor = System.Drawing.Color.Red;
                    p.Controls[nameField].BackColor = SystemColors.Info;
                  }
                }
              }
            }
            else
            {
              for (int index3 = 0; index3 < fieldsValues.Count; ++index3)
              {
                string index4 = fieldsValues[index3][0];
                if (p.Controls[index4] != null)
                {
                  num = 1;
                  if (p.Name == "Form2panel")
                  {
                    if (flag)
                    {
                      if (this.lastValidators[index1].getErrorType() == ErrorType.HardError || this.lastValidators[index1].getErrorType() == ErrorType.Null)
                        this.errorProvider1.SetErrorWithCount(this.Form1panel.Controls[index4], "", this.lastValidators[index1].getId(), num.ToString());
                      if (this.lastValidators[index1].getErrorType() == ErrorType.HardWarning)
                        this.errorProvider1.SetErrorWithCount(this.Form1panel.Controls[index4], "", 2, this.lastValidators[index1].getId(), num.ToString());
                      if (this.lastValidators[index1].getErrorType() == ErrorType.Soft)
                        this.errorProvider1.SetErrorWithCount(this.Form1panel.Controls[index4], "", 3, this.lastValidators[index1].getId(), num.ToString());
                      if (this.Form1panel.Controls[index4].Enabled && !this.errorProvider1.hasError(this.Form1panel.Controls[index4]))
                      {
                        this.Form1panel.Controls[index4].ForeColor = System.Drawing.Color.Black;
                        this.Form1panel.Controls[index4].BackColor = SystemColors.Window;
                      }
                    }
                    else
                    {
                      if (this.lastValidators[index1].getErrorType() == ErrorType.HardError || this.lastValidators[index1].getErrorType() == ErrorType.Null)
                        this.errorProvider1.SetErrorWithCount(this.Form1panel.Controls[index4], this.lastValidators[index1].getNote(), this.lastValidators[index1].getId(), num.ToString());
                      if (this.lastValidators[index1].getErrorType() == ErrorType.HardWarning)
                        this.errorProvider1.SetErrorWithCount(this.Form1panel.Controls[index4], this.lastValidators[index1].getNote(), 2, this.lastValidators[index1].getId(), num.ToString());
                      if (this.lastValidators[index1].getErrorType() == ErrorType.Soft)
                        this.errorProvider1.SetErrorWithCount(this.Form1panel.Controls[index4], this.lastValidators[index1].getNote(), 3, this.lastValidators[index1].getId(), num.ToString());
                      if (this.Form1panel.Controls[index4].Enabled)
                      {
                        this.Form1panel.Controls[index4].ForeColor = System.Drawing.Color.Red;
                        this.Form1panel.Controls[index4].BackColor = SystemColors.Info;
                      }
                    }
                    num = 2;
                  }
                  if (flag)
                  {
                    if (this.lastValidators[index1].getErrorType() == ErrorType.HardError || this.lastValidators[index1].getErrorType() == ErrorType.Null)
                      this.errorProvider1.SetErrorWithCount(p.Controls[index4], "", this.lastValidators[index1].getId(), num.ToString());
                    if (this.lastValidators[index1].getErrorType() == ErrorType.HardWarning)
                      this.errorProvider1.SetErrorWithCount(p.Controls[index4], "", 2, this.lastValidators[index1].getId(), num.ToString());
                    if (this.lastValidators[index1].getErrorType() == ErrorType.Soft)
                      this.errorProvider1.SetErrorWithCount(p.Controls[index4], "", 3, this.lastValidators[index1].getId(), num.ToString());
                    if (p.Controls[index4].Enabled && !this.errorProvider1.hasError(p.Controls[index4]))
                    {
                      p.Controls[index4].ForeColor = System.Drawing.Color.Black;
                      p.Controls[index4].BackColor = SystemColors.Window;
                    }
                  }
                  else
                  {
                    if (this.lastValidators[index1].getErrorType() == ErrorType.HardError || this.lastValidators[index1].getErrorType() == ErrorType.Null)
                      this.errorProvider1.SetErrorWithCount(p.Controls[index4], this.lastValidators[index1].getNote(), this.lastValidators[index1].getId(), num.ToString());
                    if (this.lastValidators[index1].getErrorType() == ErrorType.HardWarning)
                      this.errorProvider1.SetErrorWithCount(p.Controls[index4], this.lastValidators[index1].getNote(), 2, this.lastValidators[index1].getId(), num.ToString());
                    if (this.lastValidators[index1].getErrorType() == ErrorType.Soft)
                      this.errorProvider1.SetErrorWithCount(p.Controls[index4], this.lastValidators[index1].getNote(), 3, this.lastValidators[index1].getId(), num.ToString());
                    if (p.Controls[index4].Enabled)
                    {
                      p.Controls[index4].ForeColor = System.Drawing.Color.Red;
                      p.Controls[index4].BackColor = SystemColors.Info;
                    }
                  }
                }
              }
            }
          }
        }
      }
      catch (XmlException ex)
      {
        int num = (int) MessageBox.Show("Nieprawidłowy XML: " + ex.Message, "Error");
      }
      catch (NullReferenceException ex)
      {
        int num = (int) MessageBox.Show("Podanno inny xml definicje wygladu", "Error");
      }
    }

    private Dictionary<string, Errors> lastValidation(Panel p, Dictionary<string, Errors> errors, bool writeStep)
    {
      string str1 = "step1_";
      if (p.Name == "Form2panel")
        str1 = "step2_";
      try
      {
        for (int index1 = 0; index1 < this.lastValidators.Count; ++index1)
        {
          int fieldsCount = this.lastValidators[index1].getFieldsCount();
          List<string[]> fieldsValues = new List<string[]>();
          for (int index2 = 0; index2 < fieldsCount; ++index2)
          {
            string nameField = this.lastValidators[index1].getNameField(index2);
            if (p.Controls[nameField] != null)
              fieldsValues.Add(new string[2]
              {
                nameField,
                p.Controls[nameField].Text
              });
            if (nameField == "lwyb")
              fieldsValues.Add(new string[2]
              {
                nameField,
                this.lwyb
              });
            if (nameField == "lwybA")
              fieldsValues.Add(new string[2]
              {
                nameField,
                this.lwybA
              });
            if (nameField == "lwybB")
              fieldsValues.Add(new string[2]
              {
                nameField,
                this.lwybB
              });
            if (nameField == "plusminus")
              fieldsValues.Add(new string[2]
              {
                nameField,
                this.plusminus
              });
            if (nameField == "plus")
              fieldsValues.Add(new string[2]
              {
                nameField,
                this.plus
              });
            if (nameField == "minus")
              fieldsValues.Add(new string[2]
              {
                nameField,
                this.minus
              });
            if (nameField == "naklad")
            {
              string str2 = this.naklad.Replace("%", "");
              if (str2.Length == 2)
              {
                try
                {
                  double num = Convert.ToDouble(this.lwyb) * Convert.ToDouble((double) (100 / Convert.ToInt32(str2)));
                  fieldsValues.Add(new string[2]
                  {
                    nameField,
                    num.ToString()
                  });
                }
                catch (Exception ex)
                {
                  int num = (int) MessageBox.Show("Komunikat błedu: ProtocolForm.lastValidation: " + ex.Message, "Uwaga");
                }
              }
              if (str2.Length == 1)
              {
                double num = Convert.ToDouble(this.lwyb) * Convert.ToDouble((double) (100 / Convert.ToInt32(str2)));
                fieldsValues.Add(new string[2]
                {
                  nameField,
                  num.ToString()
                });
              }
            }
            if (nameField == "naklad%")
            {
              string str2 = this.naklad.Replace("%", "");
              if (str2.Length > 0 && str2.Length <= 3)
              {
                try
                {
                  fieldsValues.Add(new string[2]
                  {
                    nameField,
                    ((object) str2).ToString()
                  });
                }
                catch (Exception ex)
                {
                  int num = (int) MessageBox.Show("Komunikat błedu: ProtocolForm.lastValidation: " + ex.Message, "Uwaga");
                }
              }
            }
          }
          if (!this.lastValidators[index1].valid(fieldsValues))
          {
            for (int index2 = 0; index2 < fieldsCount; ++index2)
            {
              if (!this.lastValidators[index1].containVariables(index2))
              {
                string nameField = this.lastValidators[index1].getNameField(index2);
                if (p.Controls[nameField] != null)
                {
                  if (this.lastValidators[index1].getErrorType() == ErrorType.HardError || this.lastValidators[index1].getErrorType() == ErrorType.Null)
                  {
                    if (writeStep)
                    {
                      try
                      {
                        errors[str1 + nameField].addHardError(this.lastValidators[index1].getId());
                      }
                      catch (KeyNotFoundException ex)
                      {
                        errors.Add(str1 + nameField, new Errors());
                        errors[str1 + nameField].addHardError(this.lastValidators[index1].getId());
                      }
                    }
                    else
                    {
                      try
                      {
                        errors[nameField].addValueInHardError(this.lastValidators[index1].getId());
                      }
                      catch (KeyNotFoundException ex)
                      {
                        errors.Add(nameField, new Errors());
                        errors[nameField].addHardError(this.lastValidators[index1].getId());
                      }
                    }
                  }
                  if (this.lastValidators[index1].getErrorType() == ErrorType.HardWarning)
                  {
                    if (writeStep)
                    {
                      try
                      {
                        errors[str1 + nameField].addHardWarning(this.lastValidators[index1].getId());
                      }
                      catch (KeyNotFoundException ex)
                      {
                        errors.Add(str1 + nameField, new Errors());
                        errors[str1 + nameField].addHardWarning(this.lastValidators[index1].getId());
                      }
                    }
                    else
                    {
                      try
                      {
                        errors[nameField].addValueInHardWarning(this.lastValidators[index1].getId());
                      }
                      catch (KeyNotFoundException ex)
                      {
                        errors.Add(nameField, new Errors());
                        errors[nameField].addHardWarning(this.lastValidators[index1].getId());
                      }
                    }
                  }
                  if (this.lastValidators[index1].getErrorType() == ErrorType.Soft)
                  {
                    if (writeStep)
                    {
                      try
                      {
                        errors[str1 + nameField].addSoftError(this.lastValidators[index1].getId());
                      }
                      catch (KeyNotFoundException ex)
                      {
                        errors.Add(str1 + nameField, new Errors());
                        errors[str1 + nameField].addSoftError(this.lastValidators[index1].getId());
                      }
                    }
                    else
                    {
                      try
                      {
                        errors[nameField].addValueInSoftError(this.lastValidators[index1].getId());
                      }
                      catch (KeyNotFoundException ex)
                      {
                        errors.Add(nameField, new Errors());
                        errors[nameField].addSoftError(this.lastValidators[index1].getId());
                      }
                    }
                  }
                }
              }
              else
              {
                for (int index3 = index2; index3 < fieldsValues.Count; ++index3)
                {
                  string key = fieldsValues[index3][0];
                  if (p.Controls[key] != null)
                  {
                    if (this.lastValidators[index1].getErrorType() == ErrorType.HardError || this.lastValidators[index1].getErrorType() == ErrorType.Null)
                    {
                      if (writeStep)
                      {
                        try
                        {
                          errors[str1 + key].addHardError(this.lastValidators[index1].getId());
                        }
                        catch (KeyNotFoundException ex)
                        {
                          errors.Add(str1 + key, new Errors());
                          errors[str1 + key].addHardError(this.lastValidators[index1].getId());
                        }
                      }
                      else
                      {
                        try
                        {
                          errors[key].addValueInHardError(this.lastValidators[index1].getId());
                        }
                        catch (KeyNotFoundException ex)
                        {
                          errors.Add(key, new Errors());
                          errors[key].addHardError(this.lastValidators[index1].getId());
                        }
                      }
                    }
                    if (this.lastValidators[index1].getErrorType() == ErrorType.HardWarning)
                    {
                      if (writeStep)
                      {
                        try
                        {
                          errors[str1 + key].addHardWarning(this.lastValidators[index1].getId());
                        }
                        catch (KeyNotFoundException ex)
                        {
                          errors.Add(str1 + key, new Errors());
                          errors[str1 + key].addHardWarning(this.lastValidators[index1].getId());
                        }
                      }
                      else
                      {
                        try
                        {
                          errors[key].addValueInHardWarning(this.lastValidators[index1].getId());
                        }
                        catch (KeyNotFoundException ex)
                        {
                          errors.Add(key, new Errors());
                          errors[key].addHardWarning(this.lastValidators[index1].getId());
                        }
                      }
                    }
                    if (this.lastValidators[index1].getErrorType() == ErrorType.Soft)
                    {
                      if (writeStep)
                      {
                        try
                        {
                          errors[str1 + key].addSoftError(this.lastValidators[index1].getId());
                        }
                        catch (KeyNotFoundException ex)
                        {
                          errors.Add(str1 + key, new Errors());
                          errors[str1 + key].addSoftError(this.lastValidators[index1].getId());
                        }
                      }
                      else
                      {
                        try
                        {
                          errors[key].addValueInSoftError(this.lastValidators[index1].getId());
                        }
                        catch (KeyNotFoundException ex)
                        {
                          errors.Add(key, new Errors());
                          errors[key].addSoftError(this.lastValidators[index1].getId());
                        }
                      }
                    }
                  }
                }
                break;
              }
            }
          }
        }
      }
      catch (XmlException ex)
      {
        int num = (int) MessageBox.Show("Nieprawidłowy XML: " + ex.Message, "Błąd");
      }
      catch (NullReferenceException ex)
      {
        int num = (int) MessageBox.Show("Podanno inny xml definicje wygladu", "Błąd");
      }
      return errors;
    }

    private void rangeValidation(Panel p)
    {
      string step = this.stepControl(p);
      foreach (Control c in (ArrangedElementCollection) p.Controls)
      {
        if (c is TextBox)
        {
          string pattern = "^[0-9]+$";
          try
          {
            if (this.range[c.Name] != null)
            {
              if (Regex.IsMatch(c.Text, pattern))
              {
                ValidationRange validationRange = this.range[c.Name];
                try
                {
                  int num = Convert.ToInt32(c.Text);
                  validationRange.getMin();
                  validationRange.getMax();
                  if (num < validationRange.getMin() || num > validationRange.getMax())
                  {
                    if (validationRange.getMin() != 0 || validationRange.getMax() != 0)
                    {
                      this.errorProvider1.SetErrorWithCount((Control) (c as TextBox), "Cyfra nie miesci sie w przedziale (" + (object) validationRange.getMin() + ", " + (string) (object) validationRange.getMax() + ")", "SNT01", step);
                      if (c.Enabled)
                      {
                        (c as TextBox).ForeColor = System.Drawing.Color.Red;
                        (c as TextBox).BackColor = SystemColors.Info;
                      }
                      this.error = true;
                    }
                    else
                    {
                      this.errorProvider1.SetErrorWithCount((Control) (c as TextBox), "", "SNT01", step);
                      if (c.Enabled && !this.errorProvider1.hasError(c))
                      {
                        (c as TextBox).ForeColor = System.Drawing.Color.Black;
                        (c as TextBox).BackColor = SystemColors.Window;
                      }
                    }
                  }
                  else
                  {
                    this.errorProvider1.SetErrorWithCount((Control) (c as TextBox), "", "SNT01", step);
                    if (c.Enabled && !this.errorProvider1.hasError(c))
                    {
                      (c as TextBox).ForeColor = System.Drawing.Color.Black;
                      (c as TextBox).BackColor = SystemColors.Window;
                    }
                  }
                }
                catch (OverflowException ex)
                {
                  this.errorProvider1.SetErrorWithCount((Control) (c as TextBox), "Cyfra nie miesci sie w przedziale (" + (object) validationRange.getMin() + ", " + (string) (object) validationRange.getMax() + ") lub przekroczyła zakres Int32", "SNT01", step);
                  if (c.Enabled)
                  {
                    (c as TextBox).ForeColor = System.Drawing.Color.Red;
                    (c as TextBox).BackColor = SystemColors.Info;
                  }
                  this.error = true;
                }
              }
            }
          }
          catch (KeyNotFoundException ex)
          {
          }
        }
      }
    }

    private void checkFieldValidation()
    {
      foreach (Control c in (ArrangedElementCollection) this.Form1panel.Controls)
      {
        if (c is TextBox && this.Form2panel.Controls[c.Name] != null)
        {
          if (this.Form2panel.Controls[c.Name].Text != c.Text)
          {
            this.errorProvider1.SetErrorWithCount((Control) (c as TextBox), "Pole musi być równe odpowiedniemu polu z kroku \"Wypełnij protokół dół-góra\"", "ErrorEqual", "2");
            this.errorProvider1.SetErrorWithCount((Control) (this.Form2panel.Controls[c.Name] as TextBox), "Pole musi być równe odpowiedniemu polu z kroku \"Wypełnij protokół góra-dół\"", "ErrorEqual", "2");
            if (c.Enabled)
            {
              c.ForeColor = System.Drawing.Color.Red;
              c.BackColor = SystemColors.Info;
              this.Form2panel.Controls[c.Name].ForeColor = System.Drawing.Color.Red;
              this.Form2panel.Controls[c.Name].BackColor = SystemColors.Info;
            }
          }
          else
          {
            this.errorProvider1.SetErrorWithCount((Control) (c as TextBox), "", 1, "ErrorEqual", "2");
            this.errorProvider1.SetErrorWithCount((Control) (this.Form2panel.Controls[c.Name] as TextBox), "", 1, "ErrorEqual", "2");
            if (c.Enabled && !this.errorProvider1.hasError(c))
            {
              c.ForeColor = System.Drawing.Color.Black;
              c.BackColor = SystemColors.Window;
              this.Form2panel.Controls[c.Name].ForeColor = System.Drawing.Color.Black;
              this.Form2panel.Controls[c.Name].BackColor = SystemColors.Window;
            }
          }
        }
      }
    }

    private void isNotEmpty(Panel p)
    {
      string step = this.stepControl(p);
      foreach (Control c in (ArrangedElementCollection) p.Controls)
      {
        if (c is TextBox && c.Enabled && !this.canBeNull(c.Name))
        {
          char[] chArray = new char[7]
          {
            ' ',
            '.',
            '-',
            ':',
            ',',
            '_',
            '\n'
          };
          if (c.Text.Trim(chArray) == "")
          {
            c.ForeColor = System.Drawing.Color.Red;
            c.BackColor = SystemColors.Info;
            this.errorProvider1.SetErrorWithCount((Control) (c as TextBox), "Pole nie może być puste", "ErrorNull", step);
          }
          else
          {
            this.errorProvider1.SetErrorWithCount((Control) (c as TextBox), "", "ErrorNull", step);
            if (c.Enabled && !this.errorProvider1.hasError(c))
            {
              c.ForeColor = System.Drawing.Color.Black;
              c.BackColor = SystemColors.Window;
            }
          }
        }
        else if (c is MaskedTextBox && c.Enabled && !this.canBeNull(c.Name))
        {
          char[] chArray = new char[7]
          {
            ' ',
            '.',
            '-',
            ':',
            ',',
            '_',
            '\n'
          };
          if (c.Text.Trim(chArray) == "")
          {
            if (c.Enabled)
            {
              c.ForeColor = System.Drawing.Color.Red;
              c.BackColor = SystemColors.Info;
            }
            this.errorProvider1.SetErrorWithCount((Control) (c as MaskedTextBox), "Pole nie może być puste", "ErrorNull", step);
          }
          else
          {
            this.errorProvider1.SetErrorWithCount((Control) (c as MaskedTextBox), "", "ErrorNull", step);
            if (c.Enabled && !this.errorProvider1.hasError(c))
            {
              c.ForeColor = System.Drawing.Color.Black;
              c.BackColor = SystemColors.Window;
            }
          }
        }
      }
    }

    private void getLicense_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      try
      {
        if (e.RowIndex < 0)
          return;
        if (e.ColumnIndex == this.LicencesTable.Columns["action"].Index)
        {
          try
          {
            string str1 = this.path + "\\Licenses\\" + this.LicencesTable.Rows[e.RowIndex].Cells["Licencja"].Value.ToString();
            this.wait.setWaitPanel("Trwa autozapis protokołu", "Prosimy czekać");
            this.wait.setVisible(true);
            this.saves(4);
            this.wait.setWaitPanel("Trwa przygotowanie do podpisania protokołu", "Prosimy czekać");
            this.wait.setVisible(true);
            string xml = this.generateSaves(0);
            int num1 = (int) new Commit(str1, this, xml).ShowDialog();
            if (this.goodcertificate)
            {
              this.wait.setWaitPanel("Trwa generowanie kodu kreskowego dla protokołu", "Prosimy czekać");
              this.wait.setVisible(true);
              string input = "";
              this.currentStep = 0;
              this.save.LoadXml(xml);
              XmlNode xmlNode1 = this.save.SelectSingleNode("/save/header");
              if (xmlNode1 != null)
                input = input + xmlNode1.OuterXml;
              XmlNode xmlNode2 = this.save.SelectSingleNode("/save/step");
              if (xmlNode2 != null)
                input = input + xmlNode2.OuterXml;
              XmlNode xmlNode3 = this.save.SelectSingleNode("/save/form");
              if (xmlNode3 != null)
                input = input + xmlNode3.OuterXml;
              XmlNode xmlNode4 = this.save.SelectSingleNode("/save/komisja_sklad");
              if (xmlNode4 != null)
                input = input + xmlNode4.OuterXml;
              XmlNode xmlNode5 = this.save.SelectSingleNode("/save/hardWarningCode");
              if (xmlNode5 != null)
                input = input + xmlNode5.OuterXml;
              XmlNode xmlNode6 = this.save.SelectSingleNode("/save/softError");
              if (xmlNode6 != null)
                input = input + xmlNode6.OuterXml;
              XmlNode xmlNode7 = this.save.SelectSingleNode("/save/hardError");
              if (xmlNode7 != null)
                input = input + xmlNode7.OuterXml;
              XmlNode xmlNode8 = this.save.SelectSingleNode("/save/hardWarning");
              if (xmlNode8 != null)
                input = input + xmlNode8.OuterXml;
              string md5Hash = new ClassMd5().CreateMD5Hash(input);
              codeBar codeBar = new codeBar();
              codeBar.generateCode(md5Hash);
              this.codeBarCode = codeBar.getCode();
              this.codeBarText = codeBar.getTextReadable();
              this.wait.setWaitPanel("Trwa podpisywanie protokołu", "Prosimy czekać");
              this.wait.setVisible(true);
              string XMLFile = this.generateSaves(this.currentStep);
              try
              {
                new Certificate().SignXmlText(XMLFile, this.savePath, this.password, str1);
              }
              catch (Exception ex)
              {
                int num2 = (int) MessageBox.Show("Sign: " + ex.Message, "Uwaga");
              }
              try
              {
                this.komSend = "";
                if (new Connection().IsAvailableNetworkActive())
                {
                  this.wait.setWaitPanel("Trwa wysyłanie protokołu", "Prosimy czekać");
                  this.wait.setVisible(true);
                  this.goodcertificate = false;
                  Eksport eksport = new Eksport(this.savePath, true, this, str1, this.password);
                  try
                  {
                    int num2 = (int) eksport.ShowDialog();
                  }
                  catch (Exception ex)
                  {
                  }
                  if (this.goodcertificate)
                  {
                    try
                    {
                      StreamReader streamReader = new StreamReader(this.savePath);
                      string str2 = streamReader.ReadToEnd();
                      streamReader.Close();
                      string str3 = str2.Replace("<status>podpisany</status>", "<status>wysłany</status>");
                      StreamWriter streamWriter = new StreamWriter(this.savePath, false);
                      streamWriter.Write(str3);
                      streamWriter.Close();
                    }
                    catch (Exception ex)
                    {
                      int num2 = (int) MessageBox.Show("Protokół został wysłany, ale nie można zmienić jego statusu. " + ex.Message, "Uwaga");
                    }
                  }
                }
                else
                  this.komSend = "Protokół nie został wysłany na serwer z powodu braku internetu. Z poziomu listy protokołów bedzie można ponowić operacje.";
                this.password = "";
              }
              catch (Exception ex)
              {
                int num2 = (int) MessageBox.Show("Send: " + ex.Message, "Uwaga");
              }
              try
              {
                this.wait.setWaitPanel("Trwa zapisywanie protokołu na dysk", "Prosimy czekać");
                this.wait.setVisible(true);
                int num2 = (int) new SaveProtocol(this.komSend, this.savePath).ShowDialog();
              }
              catch (Exception ex)
              {
                int num2 = (int) MessageBox.Show("Save: " + ex.Message, "Uwaga");
              }
              try
              {
                this.wait.setWaitPanel("Trwa przygotowanie protokołu do druku", "Prosimy czekać");
                this.wait.setVisible(true);
                this.drukToolStripMenuItem_Click(sender, (EventArgs) e);
                this.wait.setVisible(false);
              }
              catch (Exception ex)
              {
                int num2 = (int) MessageBox.Show("Print: " + ex.Message, "Uwaga");
              }
            }
          }
          catch (ArgumentOutOfRangeException ex)
          {
          }
        }
      }
      catch (Exception ex)
      {
      }
    }

    private void getCurrentCommitteeKlk_Click(object sender, EventArgs e)
    {
      Connection connection = new Connection();
      if (connection.IsAvailableNetworkActive())
      {
        this.wait.setWaitPanel("Trwa pobieranie aktualnej listy członków komisji", "Proszę czekać");
        this.wait.setVisible(true);
        string[] strArray1 = this.savePath.Split('\\');
        string[] strArray2 = strArray1[strArray1.Length - 1].Split('-');
        string str1 = strArray2[0].Replace('_', '/');
        string str2 = strArray2[1].Replace("Jns", "");
        string str3 = strArray2[2].Replace("Obw", "");
        string str4 = "klk/";
        string str5 = "KALK/";
        str4 + str1 + "-" + str2 + "-" + str3 + ".xml";
        string filename = this.path + "\\ProtocolsDef\\" + strArray2[0] + "-" + str2 + "-" + str3 + ".xml";
        string uri = str5 + "integrity/" + str1 + "-" + str2 + "-" + str3;
        connection.getRequestKBWKlk(uri, this.savePath, 0);
        if (!this.currentCommittee)
        {
          if (MessageBox.Show("Czy nadpisać zmodyfikowaną listę członków komisji?", "Aktualizacja członków komisji", MessageBoxButtons.YesNo) == DialogResult.Yes)
          {
            this.personList.DataSource = (object) new DataTable();
            this.personList.Columns.Clear();
            if (this.personList.Columns["remove"] != null)
              this.personList.Columns.Remove("remove");
            if (this.personList.Columns["action3"] != null)
              this.personList.Columns.Remove("action3");
            this.personList.Refresh();
            this.saves(this.currentStep - 1);
            this.save.Load(this.savePath);
            this.committee.Load(filename);
            this.getCommitee();
            this.personList.Refresh();
            this.currentCommittee = true;
          }
        }
        else
        {
          int num = (int) MessageBox.Show("Lista członków komisji jest aktualna", "Aktualizacja członków komisji");
        }
        this.wait.setVisible(false);
      }
      else
      {
        int num1 = (int) MessageBox.Show("Nie masz połaczenia z internetem! Jeśli masz możliwość włącz Internet i ponownie kliknij przycisk \"Pobierz aktualną definicje Komisji\"", "Uwaga");
      }
    }

    private void committee_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      try
      {
        if (e.ColumnIndex == this.personList.Columns["remove"].Index)
        {
          if (e.RowIndex == (sender as DataGridView).Rows.Count - 1 || MessageBox.Show("Czy napewno usunąć członka komisji?", "Usuwanie", MessageBoxButtons.YesNo) != DialogResult.Yes)
            return;
          this.personList.Rows.Remove(this.personList.Rows[e.RowIndex]);
          this.currentCommittee = false;
        }
        else
          this.currentCommittee = false;
      }
      catch (Exception ex)
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
        this.errorPanel.Controls.RemoveAt(0);
      while (this.warningPanel.Controls.Count > 0)
        this.warningPanel.Controls.RemoveAt(0);
      while (this.errorWarningPanel.Controls.Count > 0)
        this.errorWarningPanel.Controls.RemoveAt(0);
    }

    private void printErrors(int step)
    {
      this.tooltipErrors.RemoveAll();
      string str = "Krok 1 i 2";
      if (step == 1)
        str = "Krok 1";
      this.wait.setWaitPanel("Trwa wypisywanie błędów", "Proszę czekać");
      this.wait.setVisible(true);
      this.clearErrors();
      Dictionary<string, Dictionary<string, KBWValue>> errors = this.errorProvider1.getErrors();
      Dictionary<string, Dictionary<string, KBWValue>> warnings = this.errorProvider1.getWarnings();
      Dictionary<string, Dictionary<string, KBWValue>> hardWarnings = this.errorProvider1.getHardWarnings();
      int x1 = 0;
      int y1 = 20;
      Label label1 = new Label();
      label1.Text = "Błędy (Nagłówek)";
      label1.AutoSize = true;
      label1.MaximumSize = new Size(this.errorPanel.Size.Width - 20, 0);
      label1.Font = new Font(this.myfont, 9f, FontStyle.Bold);
      label1.ForeColor = System.Drawing.Color.Red;
      label1.Padding = new Padding(10, 0, 10, 0);
      List<Label> list1 = new List<Label>();
      list1.Add(label1);
      Label label2 = new Label();
      label2.Text = "Błędy (" + str + ")";
      label2.AutoSize = true;
      Label label3 = label2;
      Size size1 = this.errorPanel.Size;
      Size size2 = new Size(size1.Width - 20, 0);
      label3.MaximumSize = size2;
      label2.Font = new Font(this.myfont, 9f, FontStyle.Bold);
      label2.ForeColor = System.Drawing.Color.Red;
      label2.Padding = new Padding(10, 0, 10, 0);
      List<Label> list2 = new List<Label>();
      list2.Add(label2);
      Label label4 = new Label();
      label4.Text = "Błędy (Krok 3)";
      label4.AutoSize = true;
      Label label5 = label4;
      size1 = this.errorPanel.Size;
      Size size3 = new Size(size1.Width - 20, 0);
      label5.MaximumSize = size3;
      label4.Font = new Font(this.myfont, 9f, FontStyle.Bold);
      label4.ForeColor = System.Drawing.Color.Red;
      label4.Padding = new Padding(10, 0, 10, 0);
      List<Label> list3 = new List<Label>();
      list3.Add(label4);
      Label label6 = new Label();
      label6.Text = "Błędy (Komisja)";
      label6.AutoSize = true;
      Label label7 = label6;
      size1 = this.errorPanel.Size;
      Size size4 = new Size(size1.Width - 20, 0);
      label7.MaximumSize = size4;
      label6.Font = new Font(this.myfont, 9f, FontStyle.Bold);
      label6.ForeColor = System.Drawing.Color.Red;
      label6.Padding = new Padding(10, 0, 10, 0);
      List<Label> list4 = new List<Label>();
      list4.Add(label6);
      foreach (KeyValuePair<string, Dictionary<string, KBWValue>> keyValuePair1 in errors)
      {
        string caption1 = "";
        string caption2 = "";
        string caption3 = "";
        foreach (KeyValuePair<string, KBWValue> keyValuePair2 in keyValuePair1.Value)
        {
          string message = errors[keyValuePair1.Key][keyValuePair2.Key].getMessage();
          Label label8 = new Label();
          label8.Text = message;
          label8.AutoSize = true;
          Label label9 = label8;
          size1 = this.errorPanel.Size;
          Size size5 = new Size(size1.Width - 20, 0);
          label9.MaximumSize = size5;
          label8.Font = new Font(this.myfont, 8f);
          label8.ForeColor = System.Drawing.Color.Red;
          label8.Padding = new Padding(10, 0, 10, 0);
          if (keyValuePair2.Value.getStep() == "-1")
          {
            if (!this.isDoubled(list1, message))
              list1.Add(label8);
            caption1 = !(caption1 != "") ? caption1 + message : caption1 + '\n'.ToString() + message;
          }
          if (keyValuePair2.Value.getStep() == "1" || keyValuePair2.Value.getStep() == "2")
          {
            if (!this.isDoubled(list2, message))
              list2.Add(label8);
            caption2 = !(caption2 != "") ? caption2 + message : caption2 + '\n'.ToString() + message;
          }
          if (keyValuePair2.Value.getStep() == "3")
          {
            if (!this.isDoubled(list3, message))
              list3.Add(label8);
            caption3 = !(caption3 != "") ? caption3 + message : caption3 + '\n'.ToString() + message;
          }
          if (keyValuePair2.Value.getStep() == "4" && !this.isDoubled(list4, message))
            list4.Add(label8);
        }
        try
        {
          if (caption1 != "")
          {
            if (this.protocolHeader.Controls[keyValuePair1.Key].Enabled)
              this.tooltipErrors.SetToolTip(this.protocolHeader.Controls[keyValuePair1.Key], caption1);
          }
        }
        catch (Exception ex)
        {
        }
        try
        {
          if (caption2 != "")
          {
            if (this.Form1panel.Controls[keyValuePair1.Key].Enabled)
              this.tooltipErrors.SetToolTip(this.Form1panel.Controls[keyValuePair1.Key], caption2);
            if (step == 2 && this.Form2panel.Controls[keyValuePair1.Key].Enabled)
              this.tooltipErrors.SetToolTip(this.Form2panel.Controls[keyValuePair1.Key], caption2);
          }
        }
        catch (Exception ex)
        {
        }
        try
        {
          if (caption3 != "")
          {
            try
            {
              if (this.SummationPanel.Controls[keyValuePair1.Key].Enabled)
                this.tooltipErrors.SetToolTip(this.SummationPanel.Controls[keyValuePair1.Key], caption3);
            }
            catch (Exception ex1)
            {
              try
              {
                if (this.raportPanel.Controls[keyValuePair1.Key].Enabled)
                  this.tooltipErrors.SetToolTip(this.raportPanel.Controls[keyValuePair1.Key], caption3);
              }
              catch (Exception ex2)
              {
              }
            }
          }
        }
        catch (Exception ex)
        {
        }
      }
      if (list1.Count > 1)
      {
        for (int index = 0; index < Enumerable.Count<Label>((IEnumerable<Label>) list1); ++index)
        {
          if (index == 0)
          {
            list1[index].Location = new Point(x1, y1);
            this.errorPanel.Controls.Add((Control) list1[index]);
            y1 += list1[index].Height + 20;
          }
          else
          {
            list1[index].Location = new Point(x1, y1);
            this.errorPanel.Controls.Add((Control) list1[index]);
            y1 += list1[index].Height + 5;
          }
        }
        y1 += 20;
      }
      if (list2.Count > 1)
      {
        for (int index = 0; index < Enumerable.Count<Label>((IEnumerable<Label>) list2); ++index)
        {
          if (index == 0)
          {
            list2[index].Location = new Point(x1, y1);
            this.errorPanel.Controls.Add((Control) list2[index]);
            y1 += list2[index].Height + 20;
          }
          else
          {
            list2[index].Location = new Point(x1, y1);
            this.errorPanel.Controls.Add((Control) list2[index]);
            y1 += list2[index].Height + 5;
          }
        }
        y1 += 20;
      }
      if (list3.Count > 1)
      {
        for (int index = 0; index < Enumerable.Count<Label>((IEnumerable<Label>) list3); ++index)
        {
          if (index == 0)
          {
            list3[index].Location = new Point(x1, y1);
            this.errorPanel.Controls.Add((Control) list3[index]);
            y1 += list3[index].Height + 20;
          }
          else
          {
            list3[index].Location = new Point(x1, y1);
            this.errorPanel.Controls.Add((Control) list3[index]);
            y1 += list3[index].Height + 5;
          }
        }
        y1 += 20;
      }
      int num;
      if (list4.Count > 1)
      {
        for (int index = 0; index < Enumerable.Count<Label>((IEnumerable<Label>) list4); ++index)
        {
          if (index == 0)
          {
            list4[index].Location = new Point(x1, y1);
            this.errorPanel.Controls.Add((Control) list4[index]);
            y1 += list4[index].Height + 20;
          }
          else
          {
            list4[index].Location = new Point(x1, y1);
            this.errorPanel.Controls.Add((Control) list4[index]);
            y1 += list4[index].Height + 5;
          }
        }
        num = y1 + 20;
      }
      this.errorPanel.Visible = true;
      if (this.errorPanel.Controls.Count == 0)
        this.errorPanel.Visible = false;
      int x2 = 0;
      int y2 = 0;
      Label label10 = new Label();
      label10.Text = "Ostrzeżenia (Nagłówek)";
      label10.AutoSize = true;
      label10.MaximumSize = new Size(this.errorPanel.Size.Width - 20, 0);
      label10.Font = new Font(this.myfont, 9f, FontStyle.Bold);
      label10.ForeColor = System.Drawing.Color.DodgerBlue;
      label10.Padding = new Padding(10, 0, 10, 0);
      label10.Location = new Point(x2, y2);
      int y3 = y2 + (label10.Height + 20);
      List<Label> list5 = new List<Label>();
      list5.Add(label10);
      Label label11 = new Label();
      label11.Text = "Ostrzeżenia (" + str + ")";
      label11.AutoSize = true;
      label11.MaximumSize = new Size(this.errorPanel.Size.Width - 20, 0);
      label11.Font = new Font(this.myfont, 9f, FontStyle.Bold);
      label11.ForeColor = System.Drawing.Color.DodgerBlue;
      label11.Padding = new Padding(10, 0, 10, 0);
      List<Label> list6 = new List<Label>();
      list6.Add(label11);
      Label label12 = new Label();
      label12.Text = "Ostrzeżenia (Krok 3)";
      label12.AutoSize = true;
      label12.MaximumSize = new Size(this.errorPanel.Size.Width - 20, 0);
      label12.Font = new Font(this.myfont, 9f, FontStyle.Bold);
      label12.ForeColor = System.Drawing.Color.DodgerBlue;
      label12.Padding = new Padding(10, 0, 10, 0);
      List<Label> list7 = new List<Label>();
      list7.Add(label12);
      Label label13 = new Label();
      label13.Text = "Ostrzeżenia (Komisja)";
      label13.AutoSize = true;
      label13.MaximumSize = new Size(this.errorPanel.Size.Width - 20, 0);
      label13.Font = new Font(this.myfont, 9f, FontStyle.Bold);
      label13.ForeColor = System.Drawing.Color.DodgerBlue;
      label13.Padding = new Padding(10, 0, 10, 0);
      List<Label> list8 = new List<Label>();
      list8.Add(label13);
      foreach (KeyValuePair<string, Dictionary<string, KBWValue>> keyValuePair1 in warnings)
      {
        string caption1 = "";
        string caption2 = "";
        string caption3 = "";
        foreach (KeyValuePair<string, KBWValue> keyValuePair2 in keyValuePair1.Value)
        {
          Label label8 = new Label();
          string message = warnings[keyValuePair1.Key][keyValuePair2.Key].getMessage();
          label8.Text = message;
          label8.AutoSize = true;
          label8.MaximumSize = new Size(this.errorPanel.Size.Width - 20, 0);
          label8.Font = new Font(this.myfont, 8f);
          label8.ForeColor = System.Drawing.Color.DodgerBlue;
          label8.Padding = new Padding(10, 0, 10, 0);
          if (keyValuePair2.Value.getStep() == "-1")
          {
            if (!this.isDoubled(list5, message))
              list5.Add(label8);
            caption1 = !(caption1 != "") ? caption1 + message : caption1 + '\n'.ToString() + message;
          }
          if (keyValuePair2.Value.getStep() == "1" || keyValuePair2.Value.getStep() == "2")
          {
            if (!this.isDoubled(list6, message))
              list6.Add(label8);
            caption2 = !(caption2 != "") ? caption2 + message : caption2 + '\n'.ToString() + message;
          }
          if (keyValuePair2.Value.getStep() == "3")
          {
            if (!this.isDoubled(list7, message))
              list7.Add(label8);
            caption3 = !(caption3 != "") ? caption3 + message : caption3 + '\n'.ToString() + message;
          }
          if (keyValuePair2.Value.getStep() == "4" && !this.isDoubled(list8, message))
            list8.Add(label8);
        }
        if (caption1 != "" && this.protocolHeader.Controls[keyValuePair1.Key].Enabled)
          this.tooltipErrors.SetToolTip(this.protocolHeader.Controls[keyValuePair1.Key], caption1);
        if (caption2 != "")
        {
          if (this.Form1panel.Controls[keyValuePair1.Key].Enabled)
            this.tooltipErrors.SetToolTip(this.Form1panel.Controls[keyValuePair1.Key], caption2);
          if (step == 2 && this.Form2panel.Controls[keyValuePair1.Key].Enabled)
            this.tooltipErrors.SetToolTip(this.Form2panel.Controls[keyValuePair1.Key], caption2);
        }
        try
        {
          if (caption3 != "")
          {
            try
            {
              if (this.SummationPanel.Controls[keyValuePair1.Key].Enabled)
                this.tooltipErrors.SetToolTip(this.SummationPanel.Controls[keyValuePair1.Key], caption3);
            }
            catch (Exception ex1)
            {
              try
              {
                if (this.raportPanel.Controls[keyValuePair1.Key].Enabled)
                  this.tooltipErrors.SetToolTip(this.raportPanel.Controls[keyValuePair1.Key], caption3);
              }
              catch (Exception ex2)
              {
              }
            }
          }
        }
        catch (Exception ex)
        {
        }
      }
      if (list5.Count > 1)
      {
        for (int index = 0; index < Enumerable.Count<Label>((IEnumerable<Label>) list5); ++index)
        {
          if (index == 0)
          {
            list5[index].Location = new Point(x2, y3);
            this.warningPanel.Controls.Add((Control) list5[index]);
            y3 += list5[index].Height + 20;
          }
          else
          {
            list5[index].Location = new Point(x2, y3);
            this.warningPanel.Controls.Add((Control) list5[index]);
            y3 += list5[index].Height + 5;
          }
        }
        y3 += 20;
      }
      if (list6.Count > 1)
      {
        for (int index = 0; index < Enumerable.Count<Label>((IEnumerable<Label>) list6); ++index)
        {
          if (index == 0)
          {
            list6[index].Location = new Point(x2, y3);
            this.warningPanel.Controls.Add((Control) list6[index]);
            y3 += list6[index].Height + 20;
          }
          else
          {
            list6[index].Location = new Point(x2, y3);
            this.warningPanel.Controls.Add((Control) list6[index]);
            y3 += list6[index].Height + 5;
          }
        }
        y3 += 20;
      }
      if (list7.Count > 1)
      {
        for (int index = 0; index < Enumerable.Count<Label>((IEnumerable<Label>) list7); ++index)
        {
          if (index == 0)
          {
            list7[index].Location = new Point(x2, y3);
            this.warningPanel.Controls.Add((Control) list7[index]);
            y3 += list7[index].Height + 20;
          }
          else
          {
            list7[index].Location = new Point(x2, y3);
            this.warningPanel.Controls.Add((Control) list7[index]);
            y3 += list7[index].Height + 5;
          }
        }
        y3 += 20;
      }
      if (list8.Count > 1)
      {
        for (int index = 0; index < Enumerable.Count<Label>((IEnumerable<Label>) list8); ++index)
        {
          if (index == 0)
          {
            list8[index].Location = new Point(x2, y3);
            this.warningPanel.Controls.Add((Control) list8[index]);
            y3 += list8[index].Height + 20;
          }
          else
          {
            list8[index].Location = new Point(x2, y3);
            this.warningPanel.Controls.Add((Control) list8[index]);
            y3 += list8[index].Height + 5;
          }
        }
        num = y3 + 20;
      }
      this.warningPanel.Visible = true;
      if (this.warningPanel.Controls.Count == 0)
        this.warningPanel.Visible = false;
      if (this.errorPanel.Visible)
      {
        Panel panel = this.warningPanel;
        Point location = this.errorPanel.Location;
        int x3 = location.X;
        location = this.errorPanel.Location;
        int y4 = location.Y + this.errorPanel.Size.Height + 10;
        Point point = new Point(x3, y4);
        panel.Location = point;
      }
      else
        this.warningPanel.Location = new Point(4, 3);
      int x4 = 0;
      int y5 = 0;
      Label label14 = new Label();
      label14.Text = "Ostrzeżenia blokujące wydruk (Nagłówek)";
      label14.AutoSize = true;
      label14.MaximumSize = new Size(this.errorWarningPanel.Size.Width - 20, 0);
      label14.Font = new Font(this.myfont, 9f, FontStyle.Bold);
      label14.ForeColor = System.Drawing.Color.DodgerBlue;
      label14.Padding = new Padding(10, 0, 10, 0);
      List<Label> list9 = new List<Label>();
      list9.Add(label14);
      Label label15 = new Label();
      label15.Text = "Ostrzeżenia blokujące wydruk (" + str + ")";
      label15.AutoSize = true;
      Label label16 = label15;
      Size size6 = this.errorWarningPanel.Size;
      Size size7 = new Size(size6.Width - 20, 0);
      label16.MaximumSize = size7;
      label15.Font = new Font(this.myfont, 9f, FontStyle.Bold);
      label15.ForeColor = System.Drawing.Color.DodgerBlue;
      label15.Padding = new Padding(10, 0, 10, 0);
      List<Label> list10 = new List<Label>();
      list10.Add(label15);
      Label label17 = new Label();
      label17.Text = "Ostrzeżenia blokujące wydruk ( (Krok 3)";
      label17.AutoSize = true;
      Label label18 = label17;
      size6 = this.errorWarningPanel.Size;
      Size size8 = new Size(size6.Width - 20, 0);
      label18.MaximumSize = size8;
      label17.Font = new Font(this.myfont, 9f, FontStyle.Bold);
      label17.ForeColor = System.Drawing.Color.DodgerBlue;
      label17.Padding = new Padding(10, 0, 10, 0);
      List<Label> list11 = new List<Label>();
      list11.Add(label17);
      Label label19 = new Label();
      label19.Text = "Ostrzeżenia blokujące wydruk (Komisja)";
      label19.AutoSize = true;
      Label label20 = label19;
      size6 = this.errorWarningPanel.Size;
      Size size9 = new Size(size6.Width - 20, 0);
      label20.MaximumSize = size9;
      label19.Font = new Font(this.myfont, 9f, FontStyle.Bold);
      label19.ForeColor = System.Drawing.Color.DodgerBlue;
      label19.Padding = new Padding(10, 0, 10, 0);
      List<Label> list12 = new List<Label>();
      list12.Add(label19);
      foreach (KeyValuePair<string, Dictionary<string, KBWValue>> keyValuePair1 in hardWarnings)
      {
        string caption1 = "";
        string caption2 = "";
        string caption3 = "";
        foreach (KeyValuePair<string, KBWValue> keyValuePair2 in keyValuePair1.Value)
        {
          Label label8 = new Label();
          string message = hardWarnings[keyValuePair1.Key][keyValuePair2.Key].getMessage();
          label8.Text = message;
          label8.AutoSize = true;
          Label label9 = label8;
          size6 = this.errorPanel.Size;
          Size size5 = new Size(size6.Width - 20, 0);
          label9.MaximumSize = size5;
          label8.Font = new Font(this.myfont, 8f);
          label8.ForeColor = System.Drawing.Color.DodgerBlue;
          label8.Padding = new Padding(10, 0, 10, 0);
          if (keyValuePair2.Value.getStep() == "-1")
          {
            if (!this.isDoubled(list9, message))
              list9.Add(label8);
            caption1 = !(caption1 != "") ? caption1 + message : caption1 + '\n'.ToString() + message;
          }
          if (keyValuePair2.Value.getStep() == "1" || keyValuePair2.Value.getStep() == "2")
          {
            if (!this.isDoubled(list10, message))
              list10.Add(label8);
            caption2 = !(caption2 != "") ? caption2 + message : caption2 + '\n'.ToString() + message;
          }
          if (keyValuePair2.Value.getStep() == "3")
          {
            if (!this.isDoubled(list11, message))
              list11.Add(label8);
            caption3 = !(caption3 != "") ? caption3 + message : caption3 + '\n'.ToString() + message;
          }
          if (keyValuePair2.Value.getStep() == "4" && !this.isDoubled(list12, message))
            list12.Add(label8);
        }
        if (caption1 != "" && this.protocolHeader.Controls[keyValuePair1.Key].Enabled)
          this.tooltipErrors.SetToolTip(this.protocolHeader.Controls[keyValuePair1.Key], caption1);
        if (caption2 != "")
        {
          if (this.Form1panel.Controls[keyValuePair1.Key].Enabled)
            this.tooltipErrors.SetToolTip(this.Form1panel.Controls[keyValuePair1.Key], caption2);
          if (step == 2 && this.Form2panel.Controls[keyValuePair1.Key].Enabled)
            this.tooltipErrors.SetToolTip(this.Form2panel.Controls[keyValuePair1.Key], caption2);
        }
        if (caption3 != "" && this.SummationPanel.Controls[keyValuePair1.Key].Enabled)
          this.tooltipErrors.SetToolTip(this.SummationPanel.Controls[keyValuePair1.Key], caption3);
      }
      if (list9.Count > 1)
      {
        for (int index = 0; index < Enumerable.Count<Label>((IEnumerable<Label>) list9); ++index)
        {
          if (index == 0)
          {
            list9[index].Location = new Point(x4, y5);
            this.errorWarningPanel.Controls.Add((Control) list9[index]);
            y5 += list9[index].Height + 20;
          }
          else
          {
            list9[index].Location = new Point(x4, y5);
            this.errorWarningPanel.Controls.Add((Control) list9[index]);
            y5 += list9[index].Height + 5;
          }
        }
        y5 += 20;
      }
      if (list10.Count > 1)
      {
        for (int index = 0; index < Enumerable.Count<Label>((IEnumerable<Label>) list10); ++index)
        {
          if (index == 0)
          {
            list10[index].Location = new Point(x4, y5);
            this.errorWarningPanel.Controls.Add((Control) list10[index]);
            y5 += list10[index].Height + 20;
          }
          else
          {
            list10[index].Location = new Point(x4, y5);
            this.errorWarningPanel.Controls.Add((Control) list10[index]);
            y5 += list10[index].Height + 5;
          }
        }
        y5 += 20;
      }
      if (list11.Count > 1)
      {
        for (int index = 0; index < Enumerable.Count<Label>((IEnumerable<Label>) list11); ++index)
        {
          if (index == 0)
          {
            list11[index].Location = new Point(x4, y5);
            this.errorWarningPanel.Controls.Add((Control) list11[index]);
            y5 += list11[index].Height + 20;
          }
          else
          {
            list11[index].Location = new Point(x4, y5);
            this.errorWarningPanel.Controls.Add((Control) list11[index]);
            y5 += list11[index].Height + 5;
          }
        }
        y5 += 20;
      }
      if (list12.Count > 1)
      {
        for (int index = 0; index < Enumerable.Count<Label>((IEnumerable<Label>) list12); ++index)
        {
          if (index == 0)
          {
            list12[index].Location = new Point(x4, y5);
            this.errorWarningPanel.Controls.Add((Control) list12[index]);
            y5 += list12[index].Height + 20;
          }
          else
          {
            list12[index].Location = new Point(x4, y5);
            this.errorWarningPanel.Controls.Add((Control) list12[index]);
            y5 += list12[index].Height + 5;
          }
        }
        num = y5 + 20;
      }
      this.errorWarningPanel.Visible = true;
      if (this.errorWarningPanel.Controls.Count == 0)
        this.errorWarningPanel.Visible = false;
      if (this.warningPanel.Visible)
      {
        Panel panel = this.errorWarningPanel;
        Point location = this.warningPanel.Location;
        int x3 = location.X;
        location = this.warningPanel.Location;
        int y4 = location.Y + this.warningPanel.Size.Height + 10;
        Point point = new Point(x3, y4);
        panel.Location = point;
      }
      else if (this.errorPanel.Visible)
      {
        Panel panel = this.errorWarningPanel;
        Point location = this.errorPanel.Location;
        int x3 = location.X;
        location = this.errorPanel.Location;
        int y4 = location.Y + this.errorPanel.Size.Height + 10;
        Point point = new Point(x3, y4);
        panel.Location = point;
      }
      else
        this.errorWarningPanel.Location = new Point(4, 3);
      this.wait.setVisible(false);
    }

    private void plikiToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.currentStep != 0)
      {
        XmlNode xmlNode = this.save.SelectSingleNode("/save/form/status");
        if (xmlNode != null && (!(xmlNode.InnerText == "podpisany") && !(xmlNode.InnerText == "wysłany")))
        {
          if (!this.saves(this.currentStep))
            return;
          int num = (int) MessageBox.Show("Protokół został zapisany", "Autozapis");
        }
        else if (this.saves(this.currentStep))
        {
          int num1 = (int) MessageBox.Show("Protokół został zapisany", "Autozapis");
        }
      }
      else
      {
        int num2 = (int) MessageBox.Show("Protokół nie można nadpisać, ponieważ został już podpisany", "Autozapis");
      }
    }

    private void importujToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Import protokołu spowoduje nadpisanie obecnego protokołu. Czy kontynuować?", "Import", MessageBoxButtons.YesNo) != DialogResult.Yes)
        return;
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "(*.xml)|*.xml";
      string str1 = "";
      if (openFileDialog.ShowDialog() == DialogResult.OK)
        str1 = openFileDialog.FileName;
      if (str1 != "")
      {
        if (this.savePath != str1)
        {
          this.wait.setWaitPanel("Trwa importowanie protokołu", "Proszę czekać");
          this.wait.setVisible(true);
          StreamReader streamReader = new StreamReader(str1);
          string xml = streamReader.ReadToEnd();
          streamReader.Close();
          try
          {
            string str2 = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            string str6 = "";
            string str7 = "";
            XmlDocument xmlDocument1 = new XmlDocument();
            xmlDocument1.LoadXml(xml);
            XmlNode xmlNode1 = xmlDocument1.SelectSingleNode("/save/header/instJNS");
            if (xmlNode1 != null && xmlNode1.InnerText != "")
              str7 = xmlNode1.InnerText;
            XmlNode xmlNode2 = xmlDocument1.SelectSingleNode("/save/header/jns_kod");
            if (xmlNode2 != null && xmlNode2.InnerText != "")
              str2 = xmlNode2.InnerText;
            XmlNode xmlNode3 = xmlDocument1.SelectSingleNode("/save/header/defklk");
            if (xmlNode3 != null && xmlNode3.FirstChild != null)
            {
              XmlNode namedItem = xmlNode3.FirstChild.Attributes.GetNamedItem("name");
              if (namedItem != null && namedItem.Value != "")
              {
                string[] strArray = namedItem.Value.Split('-');
                str3 = strArray[0].Replace("/", "_");
                str6 = strArray[1].Split(' ')[0];
              }
            }
            XmlNode xmlNode4 = xmlDocument1.SelectSingleNode("/save/header/nrObwodu");
            if (xmlNode4 != null && xmlNode4.InnerText != "")
              str4 = xmlNode4.InnerText;
            XmlNode xmlNode5 = xmlDocument1.SelectSingleNode("/save/header/nrOkregu");
            if (xmlNode5 != null && xmlNode5.InnerText != "")
              str5 = xmlNode5.InnerText;
            bool flag = false;
            string[] strArray1 = this.OU.Split('-');
            if (strArray1[0].Replace("/", "_") == str3 && Convert.ToInt32(strArray1[1]) == Convert.ToInt32(str2))
            {
              if (strArray1[2] == "O" || strArray1[2] == "A")
                flag = true;
              else if (strArray1[2] == "P" || strArray1[2] == "Z")
                flag = strArray1[3] == str4;
            }
            if (flag)
            {
              XmlDocument xmlDocument2 = new XmlDocument();
              xmlDocument2.Load(str1);
              string str8 = "";
              XmlNode xmlNode6 = xmlDocument2.SelectSingleNode("/save/header/nrObwodu");
              if (xmlNode6 != null && xmlNode6.InnerText != "")
                str8 = xmlNode6.InnerText;
              string str9 = "";
              XmlNode xmlNode7 = xmlDocument2.SelectSingleNode("/save/header/defklk");
              if (xmlNode7 != null && xmlNode7.FirstChild != null)
              {
                XmlNode namedItem = xmlNode7.FirstChild.Attributes.GetNamedItem("name");
                if (namedItem != null && namedItem.Value != "")
                  str9 = namedItem.Value.Split('-')[1];
              }
              if (str4 != str8 || str6 != str9 || Convert.ToInt32(str7) != Convert.ToInt32(this.instJNS))
              {
                string path = this.path + (object) "\\saves\\" + str3 + "-" + str2 + "-" + str4 + "-" + str6 + "-" + str7 + (string) (object) '-' + str5;
                int length = Directory.GetFiles(this.path + "\\saves", str3 + (object) "-" + str2 + "-" + str4 + "-" + str6 + "-" + str7 + (string) (object) '-' + str5 + "*.crt").Length;
                if (length > 0)
                  path = path + " " + (length + 1).ToString();
                try
                {
                  StreamWriter streamWriter = new StreamWriter(path, false);
                  streamWriter.Write(xml);
                  streamWriter.Close();
                  if (MessageBox.Show("Zaimportowany plik dotyczy innego protokołu (" + (object) str3 + "-" + str2 + "-" + str4 + "-" + str6 + "-" + str7 + (string) (object) '-' + str5 + "). Chcesz przejsc do listy protokołów, aby otworzyć zaimportowany plik?", "Uwaga", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    this.Close();
                }
                catch (Exception ex)
                {
                  int num = (int) MessageBox.Show("Nie można zaimportować protokołu. " + ex.Message, "Uwaga");
                }
              }
              else
              {
                this.path + (object) "\\saves\\" + str3 + "-" + str2 + "-" + str4 + "-" + str6 + "-" + str7 + (string) (object) '-' + str5;
                string path = this.savePath;
                StreamWriter streamWriter = new StreamWriter(path, false);
                streamWriter.Write(xml);
                streamWriter.Close();
                this.savePath = path;
                try
                {
                  this.save.Load(this.savePath);
                }
                catch (XmlException ex)
                {
                  int num = (int) MessageBox.Show("Nieprawidłowy XML", "Error");
                }
                int num1 = this.isSave();
                if (num1 != -1)
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
                  this.protocolForm1.BackColor = SystemColors.GradientInactiveCaption;
                  this.protocolForm2.BackColor = SystemColors.Control;
                  this.protocolSummation.BackColor = SystemColors.Control;
                  this.protocolCommittee.BackColor = SystemColors.Control;
                  this.signProtocol.BackColor = SystemColors.Control;
                  this.buttonNext.Visible = false;
                  if (num1 != 1)
                  {
                    this.Form2panel.Controls.Clear();
                    if (num1 != 2)
                    {
                      this.SummationPanel.Controls.Clear();
                      if (num1 != 3)
                      {
                        this.personList.DataSource = (object) new DataTable();
                        this.personList.Columns.Clear();
                        if (this.personList.Columns["remove"] != null)
                          this.personList.Columns.Remove("remove");
                        if (this.personList.Columns["action3"] != null)
                          this.personList.Columns.Remove("action3");
                        this.personList.Refresh();
                      }
                    }
                  }
                  this.errorProvider1.clearErrors();
                  this.printErrors(1);
                }
                this.buttonNext.Text = "Dalej";
                this.buttonNext.Click -= new EventHandler(this.protocolForm2_Click);
                this.buttonNext.Click -= new EventHandler(this.protocolSummation_Click);
                this.buttonNext.Click -= new EventHandler(this.committee_Click);
                this.buttonNext.Click -= new EventHandler(this.committeeValid_Click);
                this.buttonNext.Click -= new EventHandler(this.signProtocol_Click);
                this.buttonNext.Click += new EventHandler(this.protocolForm2_Click);
                Panel panel = this.bottomPanel;
                Point location = this.Form1panel.Location;
                int x = location.X;
                location = this.Form1panel.Location;
                int y = location.Y + this.Form1panel.Size.Height;
                Point point = new Point(x, y);
                panel.Location = point;
                this.bottomPanel.Visible = true;
                this.buttonNext.Visible = true;
              }
            }
            else
            {
              int num2 = (int) MessageBox.Show("Nie można zaimportować protokołu, ponieważ licencja nie ma do niego uprawnień");
            }
          }
          catch (Exception ex)
          {
            int num = (int) MessageBox.Show("Importowany plik jest nieprawidłowy.");
          }
        }
        else
        {
          int num3 = (int) MessageBox.Show("Protokól nie został zaimportowany. Probowano zaimportowac ten sam protokół");
        }
      }
      else
      {
        int num4 = (int) MessageBox.Show("Importowany plik jest pusty. Protokól nie został zaimportowany");
      }
      this.wait.setVisible(false);
    }

    private void eksportToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      this.saves(this.currentStep);
      StreamReader streamReader = new StreamReader(this.savePath);
      string str = streamReader.ReadToEnd();
      streamReader.Close();
      string[] strArray = this.savePath.Split('\\');
      SaveFileDialog saveFileDialog = new SaveFileDialog();
      saveFileDialog.Filter = "(*.xml)|*.xml";
      saveFileDialog.FileName = strArray[strArray.Length - 1];
      if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
        return;
      this.wait.setWaitPanel("Trwa eksportowanie protokołu", "Proszę czekać");
      this.wait.setVisible(true);
      if (saveFileDialog.CheckPathExists)
      {
        string fileName = saveFileDialog.FileName;
        try
        {
          if (fileName != null && fileName != "" && fileName != strArray[strArray.Length - 1])
          {
            StreamWriter streamWriter = new StreamWriter(fileName, false);
            streamWriter.Write(str);
            streamWriter.Close();
          }
          this.wait.setVisible(false);
        }
        catch (UnauthorizedAccessException ex)
        {
          int num = (int) MessageBox.Show("Nie jestes uprawniony do zapisania pliku we wskazanym miejscu", "Uwaga");
        }
      }
    }

    private void drukToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.wait.setWaitPanel("Trwa przygotowanie do druku protokołu", "Proszę czekać");
      this.wait.setVisible(true);
      if (this.currentStep != 0)
      {
        XmlNode xmlNode = this.save.SelectSingleNode("/save/form/status");
        if (xmlNode != null && (!(xmlNode.InnerText == "podpisany") && !(xmlNode.InnerText == "wysłany")))
          this.saves(this.currentStep);
        else
          Thread.Sleep(2000);
      }
      else
        Thread.Sleep(2000);
      this.save.Load(this.savePath);
      string controlSum = "";
      if (this.currentStep == 0)
      {
        string input = "";
        XmlNode xmlNode1 = this.save.SelectSingleNode("/save/header");
        if (xmlNode1 != null)
          input = input + xmlNode1.OuterXml;
        XmlNode xmlNode2 = this.save.SelectSingleNode("/save/step");
        if (xmlNode2 != null)
          input = input + xmlNode2.OuterXml;
        XmlNode xmlNode3 = this.save.SelectSingleNode("/save/form");
        if (xmlNode3 != null)
          input = input + xmlNode3.OuterXml;
        XmlNode xmlNode4 = this.save.SelectSingleNode("/save/komisja_sklad");
        if (xmlNode4 != null)
          input = input + xmlNode4.OuterXml;
        XmlNode xmlNode5 = this.save.SelectSingleNode("/save/hardWarningCode");
        if (xmlNode5 != null)
          input = input + xmlNode5.OuterXml;
        XmlNode xmlNode6 = this.save.SelectSingleNode("/save/softError");
        if (xmlNode6 != null)
          input = input + xmlNode6.OuterXml;
        XmlNode xmlNode7 = this.save.SelectSingleNode("/save/hardError");
        if (xmlNode7 != null)
          input = input + xmlNode7.OuterXml;
        XmlNode xmlNode8 = this.save.SelectSingleNode("/save/hardWarning");
        if (xmlNode8 != null)
          input = input + xmlNode8.OuterXml;
        controlSum = new ClassMd5().CreateMD5Hash(input);
      }
      try
      {
        printProtocolNew printProtocolNew = new printProtocolNew();
        string[] strArray1 = this.savePath.Split('\\');
        string[] strArray2 = strArray1[strArray1.Length - 1].Split('-');
        strArray2[1].Replace("Jns", "");
        string inst = strArray2[3].Replace("Inst", "");
        string instJNS = strArray2[4];
        string obw = strArray2[2].Replace("Obw", "");
        string okr = strArray2[5].Replace("Okr", "").Replace(".xml", "");
        printProtocolNew.ProtocolPrint(this.header, this.save, this.candidates, this.docxDefinition, controlSum, false, obw, inst, okr, this.candDefinition, instJNS);
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Komunikat błedu: funkcja drukToolStripMenuItem_Click (wywołanie p.ProtocolPrint(...)): " + ex.Message, "Uwaga");
      }
      this.wait.setVisible(false);
    }

    public static void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
    {
      WebBrowser webBrowser = (WebBrowser) sender;
      if (!webBrowser.ReadyState.Equals((object) WebBrowserReadyState.Complete))
        return;
      try
      {
        string name = "Software\\Microsoft\\Internet Explorer\\PageSetup";
        using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(name, true))
        {
          if (registryKey != null)
          {
            registryKey.GetValue("footer");
            registryKey.GetValue("header");
            registryKey.SetValue("footer", (object) "");
            registryKey.SetValue("header", (object) "");
            registryKey.SetValue("margin_top", (object) "0.75");
            registryKey.SetValue("margin_right", (object) "0.75");
            registryKey.SetValue("margin_bottom", (object) "0.75");
            registryKey.SetValue("margin_left", (object) "0.75");
          }
        }
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Usuniecie standarodowej stopki i nagłówka nie powiodło się. " + ex.Message, "Uwaga");
      }
      webBrowser.ShowPrintDialog();
    }

    private void importujZSieciToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Import protokołu spowoduje nadpisanie obecnego protokołu. Czy kontynuować?", "Import", MessageBoxButtons.YesNo) != DialogResult.Yes)
        return;
      int num1 = (int) new Import(this.savePath, this.licensePath, this, this.header).ShowDialog();
      this.wait.setWaitPanel("Trwa wczytywanie protokołu", "Proszę czekać");
      this.wait.setVisible(true);
      if (this.imported)
      {
        int num2 = this.isSave();
        if (num2 != -1)
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
          this.protocolForm1.BackColor = SystemColors.GradientInactiveCaption;
          this.protocolForm2.BackColor = SystemColors.Control;
          this.protocolSummation.BackColor = SystemColors.Control;
          this.protocolCommittee.BackColor = SystemColors.Control;
          this.signProtocol.BackColor = SystemColors.Control;
          this.buttonNext.Visible = false;
          if (num2 != 1)
          {
            this.Form2panel.Controls.Clear();
            if (num2 != 2)
            {
              this.SummationPanel.Controls.Clear();
              this.raportPanel.Controls.Clear();
              this.raportPanel.Visible = false;
              if (num2 != 3)
              {
                this.personList.DataSource = (object) new DataTable();
                this.personList.Columns.Clear();
                if (this.personList.Columns["remove"] != null)
                  this.personList.Columns.Remove("remove");
                if (this.personList.Columns["action3"] != null)
                  this.personList.Columns.Remove("action3");
                this.personList.Refresh();
              }
            }
          }
        }
        this.buttonNext.Text = "Dalej";
        this.buttonNext.Click -= new EventHandler(this.protocolForm2_Click);
        this.buttonNext.Click -= new EventHandler(this.protocolSummation_Click);
        this.buttonNext.Click -= new EventHandler(this.committee_Click);
        this.buttonNext.Click -= new EventHandler(this.committeeValid_Click);
        this.buttonNext.Click -= new EventHandler(this.signProtocol_Click);
        this.buttonNext.Click += new EventHandler(this.protocolForm2_Click);
        this.bottomPanel.Location = new Point(this.Form1panel.Location.X, this.Form1panel.Location.Y + this.Form1panel.Size.Height);
        this.bottomPanel.Visible = true;
        this.buttonNext.Visible = true;
        this.errorProvider1.clearErrors();
      }
      this.imported = false;
      this.wait.setVisible(false);
    }

    private void eksportujZSieciToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.currentStep != 0)
      {
        this.validateExportedXml(this.currentStep);
        int num = (int) new Eksport(this.savePath, this.licensePath).ShowDialog();
      }
      else
      {
        int num1 = (int) MessageBox.Show("Nie można wyeksportować podpisanego protokołu. Aby wysłać podpisany protokół proszę wybrać odpowiednią licencje.");
      }
    }

    private List<Field> readPatternCandidate(XmlNode itemChild, List<Field> patternField)
    {
      foreach (XmlNode xmlNode in itemChild)
      {
        if (xmlNode.Name == "fildpatern")
        {
          XmlNode namedItem1 = xmlNode.Attributes.GetNamedItem("name");
          XmlNode namedItem2 = xmlNode.Attributes.GetNamedItem("name2");
          XmlNode namedItem3 = xmlNode.Attributes.GetNamedItem("name2v2");
          XmlNode namedItem4 = xmlNode.Attributes.GetNamedItem("name3");
          XmlNode namedItem5 = xmlNode.Attributes.GetNamedItem("lista");
          XmlNode namedItem6 = xmlNode.Attributes.GetNamedItem("plec");
          XmlNode namedItem7 = xmlNode.Attributes.GetNamedItem("status");
          XmlNode namedItem8 = xmlNode.Attributes.GetNamedItem("imie");
          XmlNode namedItem9 = xmlNode.Attributes.GetNamedItem("imie2");
          XmlNode namedItem10 = xmlNode.Attributes.GetNamedItem("idCandidate");
          XmlNode namedItem11 = xmlNode.Attributes.GetNamedItem("nazwisko");
          XmlNode namedItem12 = xmlNode.Attributes.GetNamedItem("data");
          XmlNode namedItem13 = xmlNode.Attributes.GetNamedItem("komitet-wyborczy");
          XmlNode namedItem14 = xmlNode.Attributes.GetNamedItem("save_as");
          XmlNode namedItem15 = xmlNode.Attributes.GetNamedItem("char");
          XmlNode namedItem16 = xmlNode.Attributes.GetNamedItem("fill");
          XmlNode namedItem17 = xmlNode.Attributes.GetNamedItem("display");
          string name1 = "";
          if (namedItem1 != null)
            name1 = namedItem1.Value;
          string name2 = "";
          if (namedItem2 != null)
            name2 = namedItem2.Value;
          string name2v2 = "";
          if (namedItem3 != null)
            name2v2 = namedItem3.Value;
          string name3 = "";
          if (namedItem4 != null)
            name3 = namedItem4.Value;
          string lista = "";
          if (namedItem5 != null)
            lista = namedItem5.Value;
          string status = "";
          if (namedItem7 != null)
            status = namedItem7.Value;
          string plec = "";
          if (namedItem6 != null)
            plec = namedItem6.Value;
          string imie1 = "";
          if (namedItem8 != null)
            imie1 = namedItem8.Value;
          string imie2 = "";
          if (namedItem9 != null)
            imie2 = namedItem9.Value;
          string nazwisko = "";
          if (namedItem11 != null)
            nazwisko = namedItem11.Value;
          string dataType = "";
          if (namedItem12 != null)
            dataType = namedItem12.Value;
          string save_as = "";
          if (namedItem14 != null)
            save_as = namedItem14.Value;
          string komitet = "";
          if (namedItem13 != null)
            komitet = namedItem13.Value;
          string idCandidate = "";
          if (namedItem10 != null)
            idCandidate = namedItem10.Value;
          string display = "";
          if (namedItem17 != null)
            display = namedItem17.Value;
          string isChar = "";
          if (namedItem15 != null)
            isChar = namedItem15.Value;
          string isFill = "";
          if (namedItem16 != null)
            isFill = namedItem16.Value;
          patternField.Add(new Field(name1, name2, name2v2, plec, status, imie1, imie2, nazwisko, dataType, save_as, komitet, idCandidate, isChar, isFill, name3, lista, display));
        }
      }
      return patternField;
    }

    private void setFirstFocus()
    {
      foreach (Control control in (ArrangedElementCollection) this.protocolHeader.Controls)
      {
        if ((control is TextBox || control is MaskedTextBox) && control.Enabled)
        {
          this.lastControl = control;
          control.Select();
          control.Focus();
          this.ActiveControl = control;
          break;
        }
      }
    }

    private void setFirstFocus(Panel p)
    {
      foreach (Control control in (ArrangedElementCollection) p.Controls)
      {
        if ((control is TextBox || control is MaskedTextBox) && control.Enabled)
        {
          this.lastControl = control;
          this.ActiveControl = control;
          control.Select();
          control.Focus();
          break;
        }
      }
    }

    private void setLastFocus()
    {
      int num1 = this.Form2panel.Controls.Count - 1;
      try
      {
        for (int index = num1; index >= 0; --index)
        {
          if (this.Form2panel.Controls[index] is TextBox && this.Form2panel.Controls[index].Enabled)
          {
            this.lastControl = this.Form2panel.Controls[index];
            this.ActiveControl = this.Form2panel.Controls[index];
            this.Form2panel.Controls[index].Focus();
            this.Form2panel.Controls[index].Select();
            return;
          }
        }
        this.ActiveControl = (Control) this.buttonNext;
        this.lastControl = (Control) this.buttonNext;
      }
      catch (ArgumentOutOfRangeException ex)
      {
        int num2 = (int) MessageBox.Show("Komunikat błedu: funkcja setLastFocus: " + ex.Message, "Uwaga");
      }
    }

    private void validateExportedXml(int step)
    {
      this.wait.setWaitPanel("Trwa walidacja protokołu", "Proszę czekać");
      this.wait.setVisible(true);
      Dictionary<string, Errors> dictionary = new Dictionary<string, Errors>();
      char[] chArray = new char[7]
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
        foreach (Control control in (ArrangedElementCollection) this.Form1panel.Controls)
        {
          if (control is TextBox && control.Enabled)
          {
            if (control.Text.Trim(chArray) == "")
            {
              try
              {
                dictionary["step1_" + control.Name].addHardError("ErrorNull");
              }
              catch (KeyNotFoundException ex)
              {
                dictionary.Add("step1_" + control.Name, new Errors());
                dictionary["step1_" + control.Name].addHardError("ErrorNull");
              }
            }
            else
            {
              try
              {
                if (this.typeValidation[control.Name] == "text")
                {
                  if (!this.text_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step1_" + control.Name, new Errors());
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "normalText")
                {
                  if (!this.normalText_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step1_" + control.Name, new Errors());
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "dateTime")
                {
                  if (!this.dateTime_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step1_" + control.Name, new Errors());
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "time")
                {
                  if (!this.time_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step1_" + control.Name, new Errors());
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "date")
                {
                  if (!this.date_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step1_" + control.Name, new Errors());
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "number")
                {
                  string pattern = "^[0-9]+$";
                  try
                  {
                    ValidationRange validationRange = this.range[control.Name];
                    if (Regex.IsMatch(control.Text, pattern))
                    {
                      try
                      {
                        int num = Convert.ToInt32(control.Text);
                        if (num < validationRange.getMin() || num > validationRange.getMax())
                        {
                          if (validationRange.getMin() != 0 || validationRange.getMax() != 0)
                          {
                            try
                            {
                              dictionary["step1_" + control.Name].addHardError("SNT01");
                            }
                            catch (KeyNotFoundException ex)
                            {
                              dictionary.Add("step1_" + control.Name, new Errors());
                              dictionary["step1_" + control.Name].addHardError("SNT01");
                            }
                          }
                        }
                      }
                      catch (OverflowException ex1)
                      {
                        try
                        {
                          dictionary["step1_" + control.Name].addHardError("SNT01");
                        }
                        catch (KeyNotFoundException ex2)
                        {
                          dictionary.Add("step1_" + control.Name, new Errors());
                          dictionary["step1_" + control.Name].addHardError("SNT01");
                        }
                      }
                    }
                    else
                    {
                      try
                      {
                        dictionary["step1_" + control.Name].addHardError("ErrorType");
                      }
                      catch (KeyNotFoundException ex)
                      {
                        dictionary.Add("step1_" + control.Name, new Errors());
                        dictionary["step1_" + control.Name].addHardError("ErrorType");
                      }
                    }
                  }
                  catch (KeyNotFoundException ex)
                  {
                  }
                }
              }
              catch (Exception ex)
              {
              }
            }
          }
        }
        try
        {
          dictionary = this.lastValidation(this.Form1panel, dictionary, true);
        }
        catch (Exception ex)
        {
        }
      }
      if (step == 2)
      {
        foreach (Control control in (ArrangedElementCollection) this.Form1panel.Controls)
        {
          if (control is TextBox && control.Enabled)
          {
            if (control.Text.Trim(chArray) == "")
            {
              try
              {
                dictionary["step1_" + control.Name].addHardError("ErrorNull");
              }
              catch (KeyNotFoundException ex)
              {
                dictionary.Add("step1_" + control.Name, new Errors());
                dictionary["step1_" + control.Name].addHardError("ErrorNull");
              }
            }
            else
            {
              try
              {
                if (this.typeValidation[control.Name] == "text")
                {
                  if (!this.text_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step1_" + control.Name, new Errors());
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "normalText")
                {
                  if (!this.normalText_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step1_" + control.Name, new Errors());
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "dateTime")
                {
                  if (!this.dateTime_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step1_" + control.Name, new Errors());
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "time")
                {
                  if (!this.time_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step1_" + control.Name, new Errors());
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "date")
                {
                  if (!this.date_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step1_" + control.Name, new Errors());
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "number")
                {
                  string pattern = "^[0-9]+$";
                  try
                  {
                    ValidationRange validationRange = this.range[control.Name];
                    if (Regex.IsMatch(control.Text, pattern))
                    {
                      try
                      {
                        int num = Convert.ToInt32(control.Text);
                        if (num < validationRange.getMin() || num > validationRange.getMax())
                        {
                          if (validationRange.getMin() != 0 || validationRange.getMax() != 0)
                          {
                            try
                            {
                              dictionary["step1_" + control.Name].addHardError("SNT01");
                            }
                            catch (KeyNotFoundException ex)
                            {
                              dictionary.Add("step1_" + control.Name, new Errors());
                              dictionary["step1_" + control.Name].addHardError("SNT01");
                            }
                          }
                        }
                      }
                      catch (OverflowException ex1)
                      {
                        try
                        {
                          dictionary["step1_" + control.Name].addHardError("SNT01");
                        }
                        catch (KeyNotFoundException ex2)
                        {
                          dictionary.Add("step1_" + control.Name, new Errors());
                          dictionary["step1_" + control.Name].addHardError("SNT01");
                        }
                      }
                    }
                    else
                    {
                      try
                      {
                        dictionary["step1_" + control.Name].addHardError("ErrorType");
                      }
                      catch (KeyNotFoundException ex)
                      {
                        dictionary.Add("step1_" + control.Name, new Errors());
                        dictionary["step1_" + control.Name].addHardError("ErrorType");
                      }
                    }
                  }
                  catch (KeyNotFoundException ex)
                  {
                  }
                }
              }
              catch (Exception ex)
              {
              }
            }
            if (this.Form2panel.Controls[control.Name].Text.Trim(chArray) == "")
            {
              try
              {
                dictionary["step2_" + control.Name].addHardError("ErrorNull");
              }
              catch (KeyNotFoundException ex)
              {
                dictionary.Add("step2_" + control.Name, new Errors());
                dictionary["step2_" + control.Name].addHardError("ErrorNull");
              }
            }
            else
            {
              try
              {
                if (this.typeValidation[control.Name] == "text")
                {
                  if (!this.text_isValid(this.Form2panel.Controls[control.Name].Text))
                  {
                    try
                    {
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step2_" + control.Name, new Errors());
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "normalText")
                {
                  if (!this.normalText_isValid(this.Form2panel.Controls[control.Name].Text))
                  {
                    try
                    {
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step2_" + control.Name, new Errors());
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "dateTime")
                {
                  if (!this.dateTime_isValid(this.Form2panel.Controls[control.Name].Text))
                  {
                    try
                    {
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step2_" + control.Name, new Errors());
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "time")
                {
                  if (!this.time_isValid(this.Form2panel.Controls[control.Name].Text))
                  {
                    try
                    {
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step2_" + control.Name, new Errors());
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "date")
                {
                  if (!this.date_isValid(this.Form2panel.Controls[control.Name].Text))
                  {
                    try
                    {
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step2_" + control.Name, new Errors());
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "number")
                {
                  try
                  {
                    ValidationRange validationRange = this.range["step2_" + control.Name];
                    string pattern = "^[0-9]+$";
                    if (Regex.IsMatch(this.Form2panel.Controls["step2_" + control.Name].Text, pattern))
                    {
                      try
                      {
                        int num = Convert.ToInt32(this.Form2panel.Controls["step2_" + control.Name].Text);
                        if (num < validationRange.getMin() || num > validationRange.getMax())
                        {
                          if (validationRange.getMin() != 0 || validationRange.getMax() != 0)
                          {
                            try
                            {
                              dictionary["step2_" + control.Name].addHardError("SNT01");
                            }
                            catch (KeyNotFoundException ex)
                            {
                              dictionary.Add("step2_" + control.Name, new Errors());
                              dictionary["step2_" + control.Name].addHardError("SNT01");
                            }
                          }
                        }
                      }
                      catch (OverflowException ex1)
                      {
                        try
                        {
                          dictionary["step2_" + control.Name].addHardError("SNT01");
                        }
                        catch (KeyNotFoundException ex2)
                        {
                          dictionary.Add("step2_" + control.Name, new Errors());
                          dictionary["step2_" + control.Name].addHardError("SNT01");
                        }
                      }
                    }
                    else
                    {
                      try
                      {
                        dictionary["step2_" + control.Name].addHardError("ErrorType");
                      }
                      catch (KeyNotFoundException ex)
                      {
                        dictionary.Add("step2_" + control.Name, new Errors());
                        dictionary["step2_" + control.Name].addHardError("ErrorType");
                      }
                    }
                  }
                  catch (KeyNotFoundException ex)
                  {
                  }
                }
              }
              catch (Exception ex)
              {
              }
            }
            if (this.Form2panel.Controls[control.Name].Text != control.Text)
            {
              try
              {
                dictionary["step2_" + control.Name].addHardError("ErrorEqual");
              }
              catch (KeyNotFoundException ex)
              {
                dictionary.Add("step2_" + control.Name, new Errors());
                dictionary["step2_" + control.Name].addHardError("ErrorEqual");
              }
            }
          }
        }
        try
        {
          dictionary = this.lastValidation(this.Form1panel, dictionary, true);
        }
        catch (Exception ex)
        {
        }
        try
        {
          dictionary = this.lastValidation(this.Form2panel, dictionary, true);
        }
        catch (Exception ex)
        {
        }
      }
      if (step == 3)
      {
        foreach (Control control in (ArrangedElementCollection) this.Form1panel.Controls)
        {
          if (control is TextBox && control.Enabled)
          {
            if (control.Text.Trim(chArray) == "")
            {
              try
              {
                dictionary["step1_" + control.Name].addHardError("ErrorNull");
              }
              catch (KeyNotFoundException ex)
              {
                dictionary.Add("step1_" + control.Name, new Errors());
                dictionary["step1_" + control.Name].addHardError("ErrorNull");
              }
            }
            else
            {
              try
              {
                if (this.typeValidation[control.Name] == "text")
                {
                  if (!this.text_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step1_" + control.Name, new Errors());
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "normalText")
                {
                  if (!this.normalText_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step1_" + control.Name, new Errors());
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "dateTime")
                {
                  if (!this.dateTime_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step1_" + control.Name, new Errors());
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "time")
                {
                  if (!this.time_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step1_" + control.Name, new Errors());
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "date")
                {
                  if (!this.date_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step1_" + control.Name, new Errors());
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "number")
                {
                  string pattern = "^[0-9]+$";
                  try
                  {
                    ValidationRange validationRange = this.range[control.Name];
                    if (Regex.IsMatch(control.Text, pattern))
                    {
                      try
                      {
                        int num = Convert.ToInt32(control.Text);
                        if (num < validationRange.getMin() || num > validationRange.getMax())
                        {
                          if (validationRange.getMin() != 0 || validationRange.getMax() != 0)
                          {
                            try
                            {
                              dictionary["step1_" + control.Name].addHardError("SNT01");
                            }
                            catch (KeyNotFoundException ex)
                            {
                              dictionary.Add("step1_" + control.Name, new Errors());
                              dictionary["step1_" + control.Name].addHardError("SNT01");
                            }
                          }
                        }
                      }
                      catch (OverflowException ex1)
                      {
                        try
                        {
                          dictionary["step1_" + control.Name].addHardError("SNT01");
                        }
                        catch (KeyNotFoundException ex2)
                        {
                          dictionary.Add("step1_" + control.Name, new Errors());
                          dictionary["step1_" + control.Name].addHardError("SNT01");
                        }
                      }
                    }
                    else
                    {
                      try
                      {
                        dictionary["step1_" + control.Name].addHardError("ErrorType");
                      }
                      catch (KeyNotFoundException ex)
                      {
                        dictionary.Add("step1_" + control.Name, new Errors());
                        dictionary["step1_" + control.Name].addHardError("ErrorType");
                      }
                    }
                  }
                  catch (KeyNotFoundException ex)
                  {
                  }
                }
              }
              catch (Exception ex)
              {
              }
            }
            if (this.Form2panel.Controls[control.Name].Text.Trim(chArray) == "")
            {
              try
              {
                dictionary["step2_" + control.Name].addHardError("ErrorNull");
              }
              catch (KeyNotFoundException ex)
              {
                dictionary.Add("step2_" + control.Name, new Errors());
                dictionary["step2_" + control.Name].addHardError("ErrorNull");
              }
            }
            else
            {
              try
              {
                if (this.typeValidation[control.Name] == "text")
                {
                  if (!this.text_isValid(this.Form2panel.Controls[control.Name].Text))
                  {
                    try
                    {
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step2_" + control.Name, new Errors());
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "normalText")
                {
                  if (!this.normalText_isValid(this.Form2panel.Controls[control.Name].Text))
                  {
                    try
                    {
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step2_" + control.Name, new Errors());
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "dateTime")
                {
                  if (!this.dateTime_isValid(this.Form2panel.Controls[control.Name].Text))
                  {
                    try
                    {
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step2_" + control.Name, new Errors());
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "time")
                {
                  if (!this.time_isValid(this.Form2panel.Controls[control.Name].Text))
                  {
                    try
                    {
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step2_" + control.Name, new Errors());
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "date")
                {
                  if (!this.date_isValid(this.Form2panel.Controls[control.Name].Text))
                  {
                    try
                    {
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step2_" + control.Name, new Errors());
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "number")
                {
                  try
                  {
                    ValidationRange validationRange = this.range["step2_" + control.Name];
                    string pattern = "^[0-9]+$";
                    if (Regex.IsMatch(this.Form2panel.Controls["step2_" + control.Name].Text, pattern))
                    {
                      try
                      {
                        int num = Convert.ToInt32(this.Form2panel.Controls["step2_" + control.Name].Text);
                        if (num < validationRange.getMin() || num > validationRange.getMax())
                        {
                          if (validationRange.getMin() != 0 || validationRange.getMax() != 0)
                          {
                            try
                            {
                              dictionary["step2_" + control.Name].addHardError("SNT01");
                            }
                            catch (KeyNotFoundException ex)
                            {
                              dictionary.Add("step2_" + control.Name, new Errors());
                              dictionary["step2_" + control.Name].addHardError("SNT01");
                            }
                          }
                        }
                      }
                      catch (OverflowException ex1)
                      {
                        try
                        {
                          dictionary["step2_" + control.Name].addHardError("SNT01");
                        }
                        catch (KeyNotFoundException ex2)
                        {
                          dictionary.Add("step2_" + control.Name, new Errors());
                          dictionary["step2_" + control.Name].addHardError("SNT01");
                        }
                      }
                    }
                    else
                    {
                      try
                      {
                        dictionary["step2_" + control.Name].addHardError("ErrorType");
                      }
                      catch (KeyNotFoundException ex)
                      {
                        dictionary.Add("step2_" + control.Name, new Errors());
                        dictionary["step2_" + control.Name].addHardError("ErrorType");
                      }
                    }
                  }
                  catch (KeyNotFoundException ex)
                  {
                  }
                }
              }
              catch (Exception ex)
              {
              }
            }
            if (this.Form2panel.Controls[control.Name].Text != control.Text)
            {
              try
              {
                dictionary["step2_" + control.Name].addHardError("ErrorEqual");
              }
              catch (KeyNotFoundException ex)
              {
                dictionary.Add("step2_" + control.Name, new Errors());
                dictionary["step2_" + control.Name].addHardError("ErrorEqual");
              }
            }
          }
        }
        try
        {
          dictionary = this.lastValidation(this.Form1panel, dictionary, true);
        }
        catch (Exception ex)
        {
        }
        try
        {
          dictionary = this.lastValidation(this.Form2panel, dictionary, true);
        }
        catch (Exception ex)
        {
        }
        foreach (Control control in (ArrangedElementCollection) this.SummationPanel.Controls)
        {
          if (control is TextBox && control.Enabled)
          {
            if (control.Text.Trim(chArray) == "")
            {
              try
              {
                dictionary["step3_" + control.Name].addHardError("ErrorNull");
              }
              catch (KeyNotFoundException ex)
              {
                dictionary.Add("step3_" + control.Name, new Errors());
                dictionary["step3_" + control.Name].addHardError("ErrorNull");
              }
            }
            else
            {
              try
              {
                if (this.typeValidation[control.Name] == "text")
                {
                  if (!this.text_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step3_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step3_" + control.Name, new Errors());
                      dictionary["step3_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "normalText")
                {
                  if (!this.normalText_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step3_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step3_" + control.Name, new Errors());
                      dictionary["step3_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "dateTime")
                {
                  if (!this.dateTime_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step3_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step3_" + control.Name, new Errors());
                      dictionary["step3_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "time")
                {
                  if (!this.time_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step3_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step3_" + control.Name, new Errors());
                      dictionary["step3_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "date")
                {
                  if (!this.date_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step3_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step3_" + control.Name, new Errors());
                      dictionary["step3_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (!(this.typeValidation[control.Name] == "number"))
                  ;
              }
              catch (Exception ex)
              {
              }
            }
          }
        }
      }
      if (step == 4 || step == 0)
        dictionary = this.committeeValid(dictionary);
      string str1 = "";
      string str2 = "";
      string str3 = "";
      foreach (KeyValuePair<string, Errors> keyValuePair in dictionary)
      {
        string hardErrorXml = keyValuePair.Value.getHardErrorXml();
        if (hardErrorXml != "")
          str1 = str1 + "<" + keyValuePair.Key + ">" + hardErrorXml + "</" + keyValuePair.Key + ">";
        string hardWarningXml = keyValuePair.Value.getHardWarningXml();
        if (hardWarningXml != "")
          str2 = str2 + "<" + keyValuePair.Key + ">" + hardWarningXml + "</" + keyValuePair.Key + ">";
        string softErrorXml = keyValuePair.Value.getSoftErrorXml();
        if (softErrorXml != "")
          str3 = str3 + "<" + keyValuePair.Key + ">" + softErrorXml + "</" + keyValuePair.Key + ">";
      }
      string errors = "";
      if (str1 != "")
        errors = errors + "<hardError>" + str1 + "</hardError>";
      if (str2 != "")
        errors = errors + "<hardWarning>" + str2 + "</hardWarning>";
      if (str3 != "")
        errors = errors + "<softError>" + str3 + "</softError>";
      this.saves(step, errors);
      this.wait.setVisible(false);
    }

    private string validateExportedXmlS(int step)
    {
      this.wait.setWaitPanel("Trwa walidacja protokołu", "Proszę czekać");
      this.wait.setVisible(true);
      Dictionary<string, Errors> dictionary = new Dictionary<string, Errors>();
      char[] chArray = new char[7]
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
        foreach (Control control in (ArrangedElementCollection) this.Form1panel.Controls)
        {
          if (control is TextBox && control.Enabled)
          {
            if (control.Text.Trim(chArray) == "")
            {
              try
              {
                dictionary["step1_" + control.Name].addHardError("ErrorNull");
              }
              catch (KeyNotFoundException ex)
              {
                dictionary.Add("step1_" + control.Name, new Errors());
                dictionary["step1_" + control.Name].addHardError("ErrorNull");
              }
            }
            else
            {
              try
              {
                if (this.typeValidation[control.Name] == "text")
                {
                  if (!this.text_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step1_" + control.Name, new Errors());
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "normalText")
                {
                  if (!this.normalText_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step1_" + control.Name, new Errors());
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "dateTime")
                {
                  if (!this.dateTime_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step1_" + control.Name, new Errors());
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "time")
                {
                  if (!this.time_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step1_" + control.Name, new Errors());
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "date")
                {
                  if (!this.date_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step1_" + control.Name, new Errors());
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "number")
                {
                  string pattern = "^[0-9]+$";
                  try
                  {
                    ValidationRange validationRange = this.range[control.Name];
                    if (Regex.IsMatch(control.Text, pattern))
                    {
                      try
                      {
                        int num = Convert.ToInt32(control.Text);
                        if (num < validationRange.getMin() || num > validationRange.getMax())
                        {
                          if (validationRange.getMin() != 0 || validationRange.getMax() != 0)
                          {
                            try
                            {
                              dictionary["step1_" + control.Name].addHardError("SNT01");
                            }
                            catch (KeyNotFoundException ex)
                            {
                              dictionary.Add("step1_" + control.Name, new Errors());
                              dictionary["step1_" + control.Name].addHardError("SNT01");
                            }
                          }
                        }
                      }
                      catch (OverflowException ex1)
                      {
                        try
                        {
                          dictionary["step1_" + control.Name].addHardError("SNT01");
                        }
                        catch (KeyNotFoundException ex2)
                        {
                          dictionary.Add("step1_" + control.Name, new Errors());
                          dictionary["step1_" + control.Name].addHardError("SNT01");
                        }
                      }
                    }
                    else
                    {
                      try
                      {
                        dictionary["step1_" + control.Name].addHardError("ErrorType");
                      }
                      catch (KeyNotFoundException ex)
                      {
                        dictionary.Add("step1_" + control.Name, new Errors());
                        dictionary["step1_" + control.Name].addHardError("ErrorType");
                      }
                    }
                  }
                  catch (KeyNotFoundException ex)
                  {
                  }
                }
              }
              catch (Exception ex)
              {
              }
            }
          }
        }
        dictionary = this.lastValidation(this.Form1panel, dictionary, true);
      }
      if (step == 2)
      {
        foreach (Control control in (ArrangedElementCollection) this.Form1panel.Controls)
        {
          if (control is TextBox && control.Enabled)
          {
            if (control.Text.Trim(chArray) == "")
            {
              try
              {
                dictionary["step1_" + control.Name].addHardError("ErrorNull");
              }
              catch (KeyNotFoundException ex)
              {
                dictionary.Add("step1_" + control.Name, new Errors());
                dictionary["step1_" + control.Name].addHardError("ErrorNull");
              }
            }
            else
            {
              try
              {
                if (this.typeValidation[control.Name] == "text")
                {
                  if (!this.text_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step1_" + control.Name, new Errors());
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "normalText")
                {
                  if (!this.normalText_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step1_" + control.Name, new Errors());
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "dateTime")
                {
                  if (!this.dateTime_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step1_" + control.Name, new Errors());
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "time")
                {
                  if (!this.time_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step1_" + control.Name, new Errors());
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "date")
                {
                  if (!this.date_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step1_" + control.Name, new Errors());
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "number")
                {
                  string pattern = "^[0-9]+$";
                  try
                  {
                    ValidationRange validationRange = this.range[control.Name];
                    if (Regex.IsMatch(control.Text, pattern))
                    {
                      try
                      {
                        int num = Convert.ToInt32(control.Text);
                        if (num < validationRange.getMin() || num > validationRange.getMax())
                        {
                          if (validationRange.getMin() != 0 || validationRange.getMax() != 0)
                          {
                            try
                            {
                              dictionary["step1_" + control.Name].addHardError("SNT01");
                            }
                            catch (KeyNotFoundException ex)
                            {
                              dictionary.Add("step1_" + control.Name, new Errors());
                              dictionary["step1_" + control.Name].addHardError("SNT01");
                            }
                          }
                        }
                      }
                      catch (OverflowException ex1)
                      {
                        try
                        {
                          dictionary["step1_" + control.Name].addHardError("SNT01");
                        }
                        catch (KeyNotFoundException ex2)
                        {
                          dictionary.Add("step1_" + control.Name, new Errors());
                          dictionary["step1_" + control.Name].addHardError("SNT01");
                        }
                      }
                    }
                    else
                    {
                      try
                      {
                        dictionary["step1_" + control.Name].addHardError("ErrorType");
                      }
                      catch (KeyNotFoundException ex)
                      {
                        dictionary.Add("step1_" + control.Name, new Errors());
                        dictionary["step1_" + control.Name].addHardError("ErrorType");
                      }
                    }
                  }
                  catch (KeyNotFoundException ex)
                  {
                  }
                }
              }
              catch (Exception ex)
              {
              }
            }
            if (this.Form2panel.Controls[control.Name].Text.Trim(chArray) == "")
            {
              try
              {
                dictionary["step2_" + control.Name].addHardError("ErrorNull");
              }
              catch (KeyNotFoundException ex)
              {
                dictionary.Add("step2_" + control.Name, new Errors());
                dictionary["step2_" + control.Name].addHardError("ErrorNull");
              }
            }
            else
            {
              try
              {
                if (this.typeValidation[control.Name] == "text")
                {
                  if (!this.text_isValid(this.Form2panel.Controls[control.Name].Text))
                  {
                    try
                    {
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step2_" + control.Name, new Errors());
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "normalText")
                {
                  if (!this.normalText_isValid(this.Form2panel.Controls[control.Name].Text))
                  {
                    try
                    {
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step2_" + control.Name, new Errors());
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "dateTime")
                {
                  if (!this.dateTime_isValid(this.Form2panel.Controls[control.Name].Text))
                  {
                    try
                    {
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step2_" + control.Name, new Errors());
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "time")
                {
                  if (!this.time_isValid(this.Form2panel.Controls[control.Name].Text))
                  {
                    try
                    {
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step2_" + control.Name, new Errors());
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "date")
                {
                  if (!this.date_isValid(this.Form2panel.Controls[control.Name].Text))
                  {
                    try
                    {
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step2_" + control.Name, new Errors());
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "number")
                {
                  try
                  {
                    ValidationRange validationRange = this.range["step2_" + control.Name];
                    string pattern = "^[0-9]+$";
                    if (Regex.IsMatch(this.Form2panel.Controls["step2_" + control.Name].Text, pattern))
                    {
                      try
                      {
                        int num = Convert.ToInt32(this.Form2panel.Controls["step2_" + control.Name].Text);
                        if (num < validationRange.getMin() || num > validationRange.getMax())
                        {
                          if (validationRange.getMin() != 0 || validationRange.getMax() != 0)
                          {
                            try
                            {
                              dictionary["step2_" + control.Name].addHardError("SNT01");
                            }
                            catch (KeyNotFoundException ex)
                            {
                              dictionary.Add("step2_" + control.Name, new Errors());
                              dictionary["step2_" + control.Name].addHardError("SNT01");
                            }
                          }
                        }
                      }
                      catch (OverflowException ex1)
                      {
                        try
                        {
                          dictionary["step2_" + control.Name].addHardError("SNT01");
                        }
                        catch (KeyNotFoundException ex2)
                        {
                          dictionary.Add("step2_" + control.Name, new Errors());
                          dictionary["step2_" + control.Name].addHardError("SNT01");
                        }
                      }
                    }
                    else
                    {
                      try
                      {
                        dictionary["step2_" + control.Name].addHardError("ErrorType");
                      }
                      catch (KeyNotFoundException ex)
                      {
                        dictionary.Add("step2_" + control.Name, new Errors());
                        dictionary["step2_" + control.Name].addHardError("ErrorType");
                      }
                    }
                  }
                  catch (KeyNotFoundException ex)
                  {
                  }
                }
              }
              catch (Exception ex)
              {
              }
            }
            if (this.Form2panel.Controls[control.Name].Text != control.Text)
            {
              try
              {
                dictionary["step2_" + control.Name].addHardError("ErrorEqual");
              }
              catch (KeyNotFoundException ex)
              {
                dictionary.Add("step2_" + control.Name, new Errors());
                dictionary["step2_" + control.Name].addHardError("ErrorEqual");
              }
            }
          }
        }
        dictionary = this.lastValidation(this.Form2panel, this.lastValidation(this.Form1panel, dictionary, true), true);
      }
      if (step == 3)
      {
        foreach (Control control in (ArrangedElementCollection) this.Form1panel.Controls)
        {
          if (control is TextBox && control.Enabled)
          {
            if (control.Text.Trim(chArray) == "")
            {
              try
              {
                dictionary["step1_" + control.Name].addHardError("ErrorNull");
              }
              catch (KeyNotFoundException ex)
              {
                dictionary.Add("step1_" + control.Name, new Errors());
                dictionary["step1_" + control.Name].addHardError("ErrorNull");
              }
            }
            else
            {
              try
              {
                if (this.typeValidation[control.Name] == "text")
                {
                  if (!this.text_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step1_" + control.Name, new Errors());
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "normalText")
                {
                  if (!this.normalText_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step1_" + control.Name, new Errors());
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "dateTime")
                {
                  if (!this.dateTime_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step1_" + control.Name, new Errors());
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "time")
                {
                  if (!this.time_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step1_" + control.Name, new Errors());
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "date")
                {
                  if (!this.date_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step1_" + control.Name, new Errors());
                      dictionary["step1_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "number")
                {
                  string pattern = "^[0-9]+$";
                  try
                  {
                    ValidationRange validationRange = this.range[control.Name];
                    if (Regex.IsMatch(control.Text, pattern))
                    {
                      try
                      {
                        int num = Convert.ToInt32(control.Text);
                        if (num < validationRange.getMin() || num > validationRange.getMax())
                        {
                          if (validationRange.getMin() != 0 || validationRange.getMax() != 0)
                          {
                            try
                            {
                              dictionary["step1_" + control.Name].addHardError("SNT01");
                            }
                            catch (KeyNotFoundException ex)
                            {
                              dictionary.Add("step1_" + control.Name, new Errors());
                              dictionary["step1_" + control.Name].addHardError("SNT01");
                            }
                          }
                        }
                      }
                      catch (OverflowException ex1)
                      {
                        try
                        {
                          dictionary["step1_" + control.Name].addHardError("SNT01");
                        }
                        catch (KeyNotFoundException ex2)
                        {
                          dictionary.Add("step1_" + control.Name, new Errors());
                          dictionary["step1_" + control.Name].addHardError("SNT01");
                        }
                      }
                    }
                    else
                    {
                      try
                      {
                        dictionary["step1_" + control.Name].addHardError("ErrorType");
                      }
                      catch (KeyNotFoundException ex)
                      {
                        dictionary.Add("step1_" + control.Name, new Errors());
                        dictionary["step1_" + control.Name].addHardError("ErrorType");
                      }
                    }
                  }
                  catch (KeyNotFoundException ex)
                  {
                  }
                }
              }
              catch (Exception ex)
              {
              }
            }
            if (this.Form2panel.Controls[control.Name].Text.Trim(chArray) == "")
            {
              try
              {
                dictionary["step2_" + control.Name].addHardError("ErrorNull");
              }
              catch (KeyNotFoundException ex)
              {
                dictionary.Add("step2_" + control.Name, new Errors());
                dictionary["step2_" + control.Name].addHardError("ErrorNull");
              }
            }
            else
            {
              try
              {
                if (this.typeValidation[control.Name] == "text")
                {
                  if (!this.text_isValid(this.Form2panel.Controls[control.Name].Text))
                  {
                    try
                    {
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step2_" + control.Name, new Errors());
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "normalText")
                {
                  if (!this.normalText_isValid(this.Form2panel.Controls[control.Name].Text))
                  {
                    try
                    {
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step2_" + control.Name, new Errors());
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "dateTime")
                {
                  if (!this.dateTime_isValid(this.Form2panel.Controls[control.Name].Text))
                  {
                    try
                    {
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step2_" + control.Name, new Errors());
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "time")
                {
                  if (!this.time_isValid(this.Form2panel.Controls[control.Name].Text))
                  {
                    try
                    {
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step2_" + control.Name, new Errors());
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "date")
                {
                  if (!this.date_isValid(this.Form2panel.Controls[control.Name].Text))
                  {
                    try
                    {
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step2_" + control.Name, new Errors());
                      dictionary["step2_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "number")
                {
                  try
                  {
                    ValidationRange validationRange = this.range["step2_" + control.Name];
                    string pattern = "^[0-9]+$";
                    if (Regex.IsMatch(this.Form2panel.Controls["step2_" + control.Name].Text, pattern))
                    {
                      try
                      {
                        int num = Convert.ToInt32(this.Form2panel.Controls["step2_" + control.Name].Text);
                        if (num < validationRange.getMin() || num > validationRange.getMax())
                        {
                          if (validationRange.getMin() != 0 || validationRange.getMax() != 0)
                          {
                            try
                            {
                              dictionary["step2_" + control.Name].addHardError("SNT01");
                            }
                            catch (KeyNotFoundException ex)
                            {
                              dictionary.Add("step2_" + control.Name, new Errors());
                              dictionary["step2_" + control.Name].addHardError("SNT01");
                            }
                          }
                        }
                      }
                      catch (OverflowException ex1)
                      {
                        try
                        {
                          dictionary["step2_" + control.Name].addHardError("SNT01");
                        }
                        catch (KeyNotFoundException ex2)
                        {
                          dictionary.Add("step2_" + control.Name, new Errors());
                          dictionary["step2_" + control.Name].addHardError("SNT01");
                        }
                      }
                    }
                    else
                    {
                      try
                      {
                        dictionary["step2_" + control.Name].addHardError("ErrorType");
                      }
                      catch (KeyNotFoundException ex)
                      {
                        dictionary.Add("step2_" + control.Name, new Errors());
                        dictionary["step2_" + control.Name].addHardError("ErrorType");
                      }
                    }
                  }
                  catch (KeyNotFoundException ex)
                  {
                  }
                }
              }
              catch (Exception ex)
              {
              }
            }
            if (this.Form2panel.Controls[control.Name].Text != control.Text)
            {
              try
              {
                dictionary["step2_" + control.Name].addHardError("ErrorEqual");
              }
              catch (KeyNotFoundException ex)
              {
                dictionary.Add("step2_" + control.Name, new Errors());
                dictionary["step2_" + control.Name].addHardError("ErrorEqual");
              }
            }
          }
        }
        dictionary = this.lastValidation(this.Form2panel, this.lastValidation(this.Form1panel, dictionary, true), true);
        foreach (Control control in (ArrangedElementCollection) this.SummationPanel.Controls)
        {
          if (control is TextBox && control.Enabled)
          {
            if (control.Text.Trim(chArray) == "")
            {
              try
              {
                dictionary["step3_" + control.Name].addHardError("ErrorNull");
              }
              catch (KeyNotFoundException ex)
              {
                dictionary.Add("step3_" + control.Name, new Errors());
                dictionary["step3_" + control.Name].addHardError("ErrorNull");
              }
            }
            else
            {
              try
              {
                if (this.typeValidation[control.Name] == "text")
                {
                  if (!this.text_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step3_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step3_" + control.Name, new Errors());
                      dictionary["step3_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "normalText")
                {
                  if (!this.normalText_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step3_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step3_" + control.Name, new Errors());
                      dictionary["step3_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "dateTime")
                {
                  if (!this.dateTime_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step3_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step3_" + control.Name, new Errors());
                      dictionary["step3_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "time")
                {
                  if (!this.time_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step3_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step3_" + control.Name, new Errors());
                      dictionary["step3_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (this.typeValidation[control.Name] == "date")
                {
                  if (!this.date_isValid(control.Text))
                  {
                    try
                    {
                      dictionary["step3_" + control.Name].addHardError("ErrorType");
                    }
                    catch (KeyNotFoundException ex)
                    {
                      dictionary.Add("step3_" + control.Name, new Errors());
                      dictionary["step3_" + control.Name].addHardError("ErrorType");
                    }
                  }
                }
                if (!(this.typeValidation[control.Name] == "number"))
                  ;
              }
              catch (Exception ex)
              {
              }
            }
          }
        }
      }
      if (step == 4 || step == 0)
        dictionary = this.committeeValid(dictionary);
      string str1 = "";
      string str2 = "";
      string str3 = "";
      foreach (KeyValuePair<string, Errors> keyValuePair in dictionary)
      {
        string hardErrorXml = keyValuePair.Value.getHardErrorXml();
        if (hardErrorXml != "")
          str1 = str1 + "<" + keyValuePair.Key + ">" + hardErrorXml + "</" + keyValuePair.Key + ">";
        string hardWarningXml = keyValuePair.Value.getHardWarningXml();
        if (hardWarningXml != "")
          str2 = str2 + "<" + keyValuePair.Key + ">" + hardWarningXml + "</" + keyValuePair.Key + ">";
        string softErrorXml = keyValuePair.Value.getSoftErrorXml();
        if (softErrorXml != "")
          str3 = str3 + "<" + keyValuePair.Key + ">" + softErrorXml + "</" + keyValuePair.Key + ">";
      }
      string str4 = "";
      if (str1 != "")
        str4 = str4 + "<hardError>" + str1 + "</hardError>";
      if (str2 != "")
        str4 = str4 + "<hardWarning>" + str2 + "</hardWarning>";
      if (str3 != "")
        str4 = str4 + "<softError>" + str3 + "</softError>";
      this.wait.setVisible(false);
      return str4;
    }

    private bool checkProtocol(XmlDocument save, string spath)
    {
      string str1 = "";
      string str2 = "";
      string str3 = "";
      string str4 = "";
      string str5 = "";
      string str6 = "";
      if (save.SelectSingleNode("/save/header") == null)
        return true;
      XmlNode xmlNode1 = save.SelectSingleNode("/save/header/jns_kod");
      if (xmlNode1 != null && xmlNode1.InnerText != "")
        str1 = Convert.ToInt32(xmlNode1.InnerText).ToString();
      XmlNode xmlNode2 = save.SelectSingleNode("/save/header/instJNS");
      if (xmlNode2 != null && xmlNode2.InnerText != "")
        str6 = xmlNode2.InnerText;
      XmlNode xmlNode3 = save.SelectSingleNode("/save/header/defklk");
      if (xmlNode3 != null && xmlNode3.FirstChild != null)
      {
        XmlNode namedItem = xmlNode3.FirstChild.Attributes.GetNamedItem("name");
        if (namedItem != null && namedItem.Value != "")
        {
          string[] strArray = namedItem.Value.Split('-');
          str2 = strArray[0].Replace("/", "_");
          str5 = strArray[1].Split(' ')[0].Replace(".xml", "");
        }
      }
      XmlNode xmlNode4 = save.SelectSingleNode("/save/header/nrObwodu");
      if (xmlNode4 != null && xmlNode4.InnerText != "")
        str3 = xmlNode4.InnerText;
      XmlNode xmlNode5 = save.SelectSingleNode("/save/header/nrOkregu");
      if (xmlNode5 != null && xmlNode5.InnerText != "")
        str4 = xmlNode5.InnerText.Split(' ')[0];
      bool flag = false;
      string[] strArray1 = this.OU.Split('-');
      if (strArray1[0].Replace("/", "_") == str2 && Convert.ToInt32(strArray1[1]) == Convert.ToInt32(str1))
      {
        if (strArray1[2] == "O" || strArray1[2] == "A")
          flag = true;
        else if (strArray1[2] == "P" || strArray1[2] == "Z")
          flag = strArray1[3] == str3;
      }
      string[] strArray2 = spath.Split('\\');
      if (strArray2[strArray2.Length - 1].Split(' ')[0].Replace(".xml", "") != str2 + "-" + str1 + "-" + str3 + "-" + str5 + "-" + str6 + "-" + str4)
      {
        int length = Directory.GetFiles(this.path + "\\saves", str2 + "-" + str1 + "-" + str3 + "-" + str5 + "-" + str6 + "-" + str4 + "*.xml").Length;
        string str7 = this.path + "\\saves\\" + str2 + "-" + str1 + "-" + str3 + "-" + str5 + "-" + str6 + "-" + str4;
        if (length > 0)
          str7 = str7 + " " + (length + 1).ToString();
        try
        {
          File.Move(spath, str7 + ".xml");
          this.savePath = str7 + ".xml";
          this.protocolDefinitionName = str2 + "-" + str5 + ".xml";
          this.candidatesName = str2 + "-" + str5 + "-" + str1 + "-" + str4 + ".xml";
          this.committeeName = str2 + "-" + str1 + "-" + str3 + ".xml";
          this.validateDefinitionName = str2 + "-" + str5 + "_Walidacja.xml";
          this.headerName = str2 + "-" + str1 + ".xml";
          this.form.wait.setWaitPanel("Trwa otwieranie formularza protokołu - wczytywanie danych", "Proszę czekać");
          try
          {
            if (this.headerName != "")
            {
              this.header = new XmlDocument();
              if (File.Exists(this.path + "\\electoralEampaign\\" + this.headerName))
                this.header.Load(this.path + "\\electoralEampaign\\" + this.headerName);
            }
            try
            {
              if (this.headerName != "")
              {
                this.header = new XmlDocument();
                if (File.Exists(this.path + "\\electoralEampaign\\" + this.headerName))
                  this.header.Load(this.path + "\\electoralEampaign\\" + this.headerName);
              }
              if (this.protocolDefinitionName != "")
              {
                this.protocolDefinition = new XmlDocument();
                if (File.Exists(this.protocolDefinitionName))
                  this.protocolDefinition.Load(this.protocolDefinitionName);
                else
                  this.isKLKPro = false;
              }
              if (this.candidatesName != "")
              {
                this.candidates = new XmlDocument();
                if (File.Exists(this.candidatesName))
                  this.candidates.Load(this.candidatesName);
                else
                  this.isKLKCan = false;
              }
              if (this.committeeName != "")
              {
                this.committee = new XmlDocument();
                if (File.Exists(this.committeeName))
                  this.committee.Load(this.committeeName);
                else
                  this.isKLK = false;
              }
              if (this.validateDefinitionName != "")
              {
                this.validateDefinition = new XmlDocument();
                if (File.Exists(this.validateDefinitionName))
                  this.validateDefinition.Load(this.validateDefinitionName);
                else
                  this.isKLK = false;
              }
              if (this.savePath != "")
              {
                this.save = new XmlDocument();
                if (File.Exists(this.savePath))
                  this.save.Load(this.savePath);
              }
              string str8 = "";
              foreach (XmlNode xmlNode6 in this.header.SelectSingleNode("/akcja_wyborcza/jns"))
              {
                if (xmlNode6.Attributes["nr"].InnerText == str3)
                {
                  foreach (XmlNode xmlNode7 in xmlNode6)
                  {
                    if (xmlNode7.Attributes["kod"].InnerText == str5)
                    {
                      foreach (XmlNode xmlNode8 in xmlNode7)
                      {
                        if (xmlNode8.Attributes["nr"].InnerText == str3 && Convert.ToInt32(xmlNode7.Attributes["inst_jns"].InnerText) == Convert.ToInt32(str6))
                        {
                          str8 = xmlNode7.Attributes["organNazwa"].InnerText;
                          break;
                        }
                      }
                    }
                  }
                }
              }
              string str9 = str1;
              if (str9.Length < 6)
              {
                while (str9.Length < 6)
                  str9 = "0" + str9;
              }
              if (str5 == "WBP" && (str9.Substring(0, 4) == "1465" && str9.Length == 6 && this.candidatesName != ""))
              {
                string str10 = this.candidatesName.Replace(str9 + "-" + str4 + ".xml", "146501-1.xml");
                this.candidates = new XmlDocument();
                if (File.Exists(str10))
                {
                  this.candidates.Load(str10);
                  this.isKLKCan = true;
                }
                else
                {
                  this.isKLK = false;
                  this.isKLKCan = false;
                }
              }
              if (str5 == "RDA")
              {
                if (str9.Length < 6)
                {
                  while (str9.Length < 6)
                    str9 = "0" + str9;
                }
                if ((int) str9[2] == 55 || (int) str9[2] == 54)
                {
                  if (str9.Substring(0, 4) == "1465" && str8 == "m.st.")
                  {
                    this.protocolDefinition = new XmlDocument();
                    string str10 = this.protocolDefinitionName.Replace(".xml", "_M.xml");
                    if (File.Exists(str10))
                    {
                      this.protocolDefinition.Load(str10);
                      this.isKLKPro = true;
                    }
                    else
                      this.isKLKPro = false;
                    if (this.validateDefinitionName != "")
                    {
                      this.validateDefinition = new XmlDocument();
                      string str11 = this.validateDefinitionName.Replace("_Walidacja.xml", "_M_Walidacja.xml");
                      if (File.Exists(str11))
                      {
                        this.validateDefinition.Load(str11);
                        this.isKLKWali = true;
                      }
                      else
                        this.isKLKWali = false;
                    }
                    if (this.candidatesName != "")
                    {
                      string str11 = this.candidatesName.Replace(str9 + "-" + str4 + ".xml", "146501-" + str4 + ".xml");
                      this.candidates = new XmlDocument();
                      if (File.Exists(str11))
                      {
                        this.candidates.Load(str11);
                        this.isKLKCan = true;
                      }
                      else
                      {
                        this.isKLK = false;
                        this.isKLKCan = false;
                      }
                    }
                  }
                  if (str9.Substring(0, 4) != "1465")
                  {
                    if (this.protocolDefinitionName != "")
                    {
                      this.protocolDefinition = new XmlDocument();
                      string str10 = this.protocolDefinitionName.Replace(".xml", "_M.xml");
                      if (File.Exists(str10))
                      {
                        this.protocolDefinition.Load(str10);
                        this.isKLKPro = true;
                      }
                      else
                        this.isKLKPro = false;
                    }
                    if (this.validateDefinitionName != "")
                    {
                      this.validateDefinition = new XmlDocument();
                      string str10 = this.validateDefinitionName.Replace("_Walidacja.xml", "_M_Walidacja.xml");
                      if (File.Exists(str10))
                      {
                        this.validateDefinition.Load(str10);
                        this.isKLKWali = true;
                      }
                      else
                        this.isKLKWali = false;
                    }
                  }
                }
                if (str9.Substring(0, 4) == "1465" && str8 == "Dzielnicy")
                {
                  if (this.protocolDefinitionName != "")
                  {
                    this.protocolDefinition = new XmlDocument();
                    string str10 = this.protocolDefinitionName.Replace(".xml", "_D.xml");
                    if (File.Exists(str10))
                    {
                      this.protocolDefinition.Load(str10);
                      this.isKLKPro = true;
                    }
                    else
                      this.isKLKPro = false;
                  }
                  if (this.validateDefinitionName != "")
                  {
                    this.validateDefinition = new XmlDocument();
                    string str10 = this.validateDefinitionName.Replace("_Walidacja.xml", "_D_Walidacja.xml");
                    if (File.Exists(str10))
                    {
                      this.validateDefinition.Load(str10);
                      this.isKLKWali = true;
                    }
                    else
                      this.isKLKWali = false;
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
                    string str10 = this.protocolDefinitionName.Replace(".xml", "_1.xml");
                    if (File.Exists(str10))
                    {
                      this.protocolDefinition.Load(str10);
                      this.isKLKPro = true;
                    }
                    else
                      this.isKLKPro = false;
                  }
                  if (this.validateDefinitionName != "")
                  {
                    this.validateDefinition = new XmlDocument();
                    string str10 = this.validateDefinitionName.Replace("_Walidacja.xml", "_1_Walidacja.xml");
                    if (File.Exists(str10))
                    {
                      this.validateDefinition.Load(str10);
                      this.isKLKWali = true;
                    }
                    else
                      this.isKLKWali = false;
                  }
                }
              }
            }
            catch (XmlException ex)
            {
              int num = (int) MessageBox.Show("Nieprawidłowy XML: " + ex.Message, "Błąd");
            }
          }
          catch (XmlException ex)
          {
            int num = (int) MessageBox.Show("Nieprawidłowy XML: " + ex.Message, "Błąd");
          }
          int num1 = (int) MessageBox.Show("Protokół został przeniesiony do pliku: " + str2 + "-" + str1 + "-" + str3 + "-" + str5 + "-" + str6 + "-" + str4 + ".xml", "Uwaga");
        }
        catch (ArgumentNullException ex)
        {
          int num = (int) MessageBox.Show("Nie można przeniesć protokołu w odpowiednie miejsce", "Uwaga");
        }
        catch (ArgumentException ex)
        {
          int num = (int) MessageBox.Show("Nie można przeniesć protokołu w odpowiednie miejsce", "Uwaga");
        }
        catch (UnauthorizedAccessException ex)
        {
          int num = (int) MessageBox.Show("Nie masz uprawnień do przeniesiena protokołu w odpowiednie miejsce", "Uwaga");
        }
        catch (PathTooLongException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka do protokołu", "Uwaga");
        }
        catch (DirectoryNotFoundException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka do protokołu", "Uwaga");
        }
        catch (NotSupportedException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowy format ścieżki do protokołu", "Uwaga");
        }
        catch (IOException ex)
        {
          int num = (int) MessageBox.Show("Nie można odnaleźć protokołu", "Uwaga");
        }
      }
      return flag;
    }

    private string stepControl(object sender)
    {
      int num = -1;
      Control control = sender as Control;
      if (control.Parent.Name == "Form1panel")
        num = 1;
      if (control.Parent.Name == "Form2panel")
        num = 2;
      if (control.Name == "raportPanel")
        num = 3;
      if (control.Parent.Name == "SummationPanel")
        num = 3;
      if (control.Parent.Name == "committeePanel")
        num = 4;
      return num.ToString();
    }

    private string stepControl(Panel p)
    {
      int num = -1;
      if (p.Name == "Form1panel")
        num = 1;
      if (p.Name == "Form2panel")
        num = 2;
      if (p.Name == "raportPanel")
        num = 3;
      if (p.Name == "SummationPanel")
        num = 3;
      if (p.Name == "committeePanel")
        num = 4;
      return num.ToString();
    }

    private bool isDoubled(List<Label> list, string text)
    {
      for (int index = 0; index < list.Count; ++index)
      {
        if (list[index].Text == text)
          return true;
      }
      return false;
    }

    private bool isOneCandidate()
    {
      XmlNode xmlNode = this.candidates.SelectSingleNode("/listy");
      return xmlNode.ChildNodes.Count <= 1 && xmlNode.ChildNodes.Count != 0 && xmlNode.FirstChild.ChildNodes.Count <= 1;
    }

    private int countOfDeletedCandidate()
    {
      XmlNode xmlNode1 = this.candidates.SelectSingleNode("/listy");
      int num = 0;
      foreach (XmlNode xmlNode2 in xmlNode1)
      {
        foreach (XmlNode xmlNode3 in xmlNode2)
        {
          XmlNode namedItem = xmlNode3.Attributes.GetNamedItem("status");
          if (namedItem != null && namedItem.Value != "A")
            ++num;
        }
      }
      return num;
    }

    private void checkadnotation()
    {
      if (this.inst == "RDA" || this.inst == "WBP")
      {
        bool flag1 = false;
        bool flag2 = false;
        try
        {
          if (Convert.ToInt32(this.SummationPanel.Controls["field_1_4"].Text) == Convert.ToInt32(this.SummationPanel.Controls["field_1_4a"].Text) + Convert.ToInt32(this.SummationPanel.Controls["field_1_4b"].Text))
            flag1 = true;
        }
        catch (Exception ex)
        {
        }
        try
        {
          if (Convert.ToInt32(this.SummationPanel.Controls["field_1_2"].Text) == Convert.ToInt32(this.SummationPanel.Controls["field_1_3"].Text) + Convert.ToInt32(this.SummationPanel.Controls["field_1_4"].Text))
            flag2 = true;
        }
        catch (Exception ex)
        {
        }
        if (!flag1 || !flag2)
        {
          if (this.SummationPanel.Controls["field_3_14"].Text == "brak uwag")
          {
            this.errorProvider1.SetErrorWithCount(this.SummationPanel.Controls["field_3_14"], "W polu 14 musi zostać podane wyjaśnienie.", "AD14", this.stepControl(this.SummationPanel));
            this.SummationPanel.Controls["field_3_14"].ForeColor = System.Drawing.Color.Red;
            this.SummationPanel.Controls["field_3_14"].BackColor = SystemColors.Info;
          }
          else
          {
            this.errorProvider1.SetErrorWithCount(this.SummationPanel.Controls["field_3_14"], "", "AD14", this.stepControl(this.SummationPanel));
            if (!this.errorProvider1.hasError(this.SummationPanel.Controls["field_3_14"]))
            {
              this.SummationPanel.Controls["field_3_14"].ForeColor = System.Drawing.Color.Black;
              this.SummationPanel.Controls["field_3_14"].BackColor = SystemColors.Window;
            }
          }
        }
        bool flag3 = false;
        bool flag4 = false;
        try
        {
          if (Convert.ToInt32(this.SummationPanel.Controls["field_1_4"].Text) == Convert.ToInt32(this.SummationPanel.Controls["field_1_8"].Text) - Convert.ToInt32(this.SummationPanel.Controls["field_1_8a"].Text))
            flag3 = true;
        }
        catch (Exception ex)
        {
        }
        try
        {
          if (Convert.ToInt32(this.SummationPanel.Controls["field_1_8a"].Text) <= Convert.ToInt32(this.SummationPanel.Controls["field_1_7e"].Text))
            flag4 = true;
        }
        catch (Exception ex)
        {
        }
        if (!flag3 || !flag4)
        {
          if (this.SummationPanel.Controls["field_3_15"].Text == "brak uwag")
          {
            this.errorProvider1.SetErrorWithCount(this.SummationPanel.Controls["field_3_15"], "W polu 15 musi zostać podane wyjaśnienie.", "AD15", this.stepControl(this.SummationPanel));
            this.SummationPanel.Controls["field_3_15"].ForeColor = System.Drawing.Color.Red;
            this.SummationPanel.Controls["field_3_15"].BackColor = SystemColors.Info;
          }
          else
          {
            this.errorProvider1.SetErrorWithCount(this.SummationPanel.Controls["field_3_15"], "", "AD15", this.stepControl(this.SummationPanel));
            if (!this.errorProvider1.hasError(this.SummationPanel.Controls["field_3_15"]))
            {
              this.SummationPanel.Controls["field_3_15"].ForeColor = System.Drawing.Color.Black;
              this.SummationPanel.Controls["field_3_15"].BackColor = SystemColors.Window;
            }
          }
        }
        bool flag5 = false;
        try
        {
          if (Convert.ToInt32(this.SummationPanel.Controls["field_1_9"].Text) == 0)
            flag5 = true;
        }
        catch (Exception ex)
        {
        }
        if (!flag5)
        {
          if (this.SummationPanel.Controls["field_3_16"].Text == "brak uwag")
          {
            this.errorProvider1.SetErrorWithCount(this.SummationPanel.Controls["field_3_16"], "W polu 16 musi zostać podane wyjaśnienie.", "AD16", this.stepControl(this.SummationPanel));
            this.SummationPanel.Controls["field_3_16"].ForeColor = System.Drawing.Color.Red;
            this.SummationPanel.Controls["field_3_16"].BackColor = SystemColors.Info;
          }
          else
          {
            this.errorProvider1.SetErrorWithCount(this.SummationPanel.Controls["field_3_16"], "", "AD16", this.stepControl(this.SummationPanel));
            if (!this.errorProvider1.hasError(this.SummationPanel.Controls["field_3_16"]))
            {
              this.SummationPanel.Controls["field_3_16"].ForeColor = System.Drawing.Color.Black;
              this.SummationPanel.Controls["field_3_16"].BackColor = SystemColors.Window;
            }
          }
        }
      }
      if (!(this.inst == "RDP") && !(this.inst == "RDW"))
        return;
      bool flag6 = false;
      try
      {
        if (Convert.ToInt32(this.SummationPanel.Controls["field_1_2"].Text) == Convert.ToInt32(this.SummationPanel.Controls["field_1_3"].Text) + Convert.ToInt32(this.SummationPanel.Controls["field_1_4"].Text))
          flag6 = true;
      }
      catch (Exception ex)
      {
      }
      if (!flag6)
      {
        if (this.SummationPanel.Controls["field_3_14"].Text == "brak uwag")
        {
          this.errorProvider1.SetErrorWithCount(this.SummationPanel.Controls["field_3_14"], "W polu 14 musi zostać podane wyjaśnienie.", "AD14", this.stepControl(this.SummationPanel));
          this.SummationPanel.Controls["field_3_14"].ForeColor = System.Drawing.Color.Red;
          this.SummationPanel.Controls["field_3_14"].BackColor = SystemColors.Info;
        }
        else
        {
          this.errorProvider1.SetErrorWithCount(this.SummationPanel.Controls["field_3_14"], "", "AD14", this.stepControl(this.SummationPanel));
          if (!this.errorProvider1.hasError(this.SummationPanel.Controls["field_3_14"]))
          {
            this.SummationPanel.Controls["field_3_14"].ForeColor = System.Drawing.Color.Black;
            this.SummationPanel.Controls["field_3_14"].BackColor = SystemColors.Window;
          }
        }
      }
      bool flag7 = false;
      bool flag8 = false;
      try
      {
        if (Convert.ToInt32(this.SummationPanel.Controls["field_1_4"].Text) == Convert.ToInt32(this.SummationPanel.Controls["field_1_8"].Text) - Convert.ToInt32(this.SummationPanel.Controls["field_1_8a"].Text))
          flag7 = true;
      }
      catch (Exception ex)
      {
      }
      try
      {
        if (Convert.ToInt32(this.SummationPanel.Controls["field_1_8a"].Text) <= Convert.ToInt32(this.SummationPanel.Controls["field_1_7e"].Text))
          flag8 = true;
      }
      catch (Exception ex)
      {
      }
      if (!flag7 || !flag8)
      {
        if (this.SummationPanel.Controls["field_3_15"].Text == "brak uwag")
        {
          this.errorProvider1.SetErrorWithCount(this.SummationPanel.Controls["field_3_15"], "W polu 15 musi zostać podane wyjaśnienie.", "AD15", this.stepControl(this.SummationPanel));
          this.SummationPanel.Controls["field_3_15"].ForeColor = System.Drawing.Color.Red;
          this.SummationPanel.Controls["field_3_15"].BackColor = SystemColors.Info;
        }
        else
        {
          this.errorProvider1.SetErrorWithCount(this.SummationPanel.Controls["field_3_15"], "", "AD15", this.stepControl(this.SummationPanel));
          if (!this.errorProvider1.hasError(this.SummationPanel.Controls["field_3_15"]))
          {
            this.SummationPanel.Controls["field_3_15"].ForeColor = System.Drawing.Color.Black;
            this.SummationPanel.Controls["field_3_15"].BackColor = SystemColors.Window;
          }
        }
      }
      bool flag9 = false;
      try
      {
        if (Convert.ToInt32(this.SummationPanel.Controls["field_1_9"].Text) == 0)
          flag9 = true;
      }
      catch (Exception ex)
      {
      }
      if (!flag9)
      {
        if (this.SummationPanel.Controls["field_3_16"].Text == "brak uwag")
        {
          this.errorProvider1.SetErrorWithCount(this.SummationPanel.Controls["field_3_16"], "W polu 16 musi zostać podane wyjaśnienie.", "AD16", this.stepControl(this.SummationPanel));
          this.SummationPanel.Controls["field_3_16"].ForeColor = System.Drawing.Color.Red;
          this.SummationPanel.Controls["field_3_16"].BackColor = SystemColors.Info;
        }
        else
        {
          this.errorProvider1.SetErrorWithCount(this.SummationPanel.Controls["field_3_16"], "", "AD16", this.stepControl(this.SummationPanel));
          if (!this.errorProvider1.hasError(this.SummationPanel.Controls["field_3_16"]))
          {
            this.SummationPanel.Controls["field_3_16"].ForeColor = System.Drawing.Color.Black;
            this.SummationPanel.Controls["field_3_16"].BackColor = SystemColors.Window;
          }
        }
      }
    }

    private void edit()
    {
      if (MessageBox.Show("Powrót do edycji tego kroku spowoduje utratę danych z kroku 3 i 4. Czy kontynuować?", "Edycja", MessageBoxButtons.YesNo) == DialogResult.Yes)
      {
        this.saves(2);
        string str1 = this.savePath;
        this.wait.setWaitPanel("Trwa przygotowanie protokołu do edycji", "Proszę czekać");
        this.wait.setVisible(true);
        StreamReader streamReader = new StreamReader(str1);
        string xml = streamReader.ReadToEnd();
        streamReader.Close();
        try
        {
          string str2 = "";
          string str3 = "";
          string str4 = "";
          string str5 = "";
          string str6 = "";
          string str7 = "";
          XmlDocument xmlDocument1 = new XmlDocument();
          xmlDocument1.LoadXml(xml);
          XmlNode xmlNode1 = xmlDocument1.SelectSingleNode("/save/header/instJNS");
          if (xmlNode1 != null && xmlNode1.InnerText != "")
            str7 = xmlNode1.InnerText;
          XmlNode xmlNode2 = xmlDocument1.SelectSingleNode("/save/header/jns_kod");
          if (xmlNode2 != null && xmlNode2.InnerText != "")
            str2 = xmlNode2.InnerText;
          XmlNode xmlNode3 = xmlDocument1.SelectSingleNode("/save/header/defklk");
          if (xmlNode3 != null && xmlNode3.FirstChild != null)
          {
            XmlNode namedItem = xmlNode3.FirstChild.Attributes.GetNamedItem("name");
            if (namedItem != null && namedItem.Value != "")
            {
              string[] strArray = namedItem.Value.Split('-');
              str3 = strArray[0].Replace("/", "_");
              str6 = strArray[1].Split(' ')[0];
            }
          }
          XmlNode xmlNode4 = xmlDocument1.SelectSingleNode("/save/header/nrObwodu");
          if (xmlNode4 != null && xmlNode4.InnerText != "")
            str4 = xmlNode4.InnerText;
          XmlNode xmlNode5 = xmlDocument1.SelectSingleNode("/save/header/nrOkregu");
          if (xmlNode5 != null && xmlNode5.InnerText != "")
            str5 = xmlNode5.InnerText;
          bool flag = false;
          string[] strArray1 = this.OU.Split('-');
          if (strArray1[0].Replace("/", "_") == str3 && Convert.ToInt32(strArray1[1]) == Convert.ToInt32(str2))
          {
            if (strArray1[2] == "O" || strArray1[2] == "A")
              flag = true;
            else if (strArray1[2] == "P" || strArray1[2] == "Z")
              flag = strArray1[3] == str4;
          }
          if (flag)
          {
            XmlDocument xmlDocument2 = new XmlDocument();
            xmlDocument2.Load(str1);
            string str8 = "";
            XmlNode xmlNode6 = xmlDocument2.SelectSingleNode("/save/header/nrObwodu");
            if (xmlNode6 != null && xmlNode6.InnerText != "")
              str8 = xmlNode6.InnerText;
            string str9 = "";
            XmlNode xmlNode7 = xmlDocument2.SelectSingleNode("/save/header/defklk");
            if (xmlNode7 != null && xmlNode7.FirstChild != null)
            {
              XmlNode namedItem = xmlNode7.FirstChild.Attributes.GetNamedItem("name");
              if (namedItem != null && namedItem.Value != "")
                str9 = namedItem.Value.Split('-')[1];
            }
            if (Convert.ToInt32(str4) == Convert.ToInt32(str8) || Convert.ToInt32(str6) == Convert.ToInt32(str9))
            {
              this.path + "\\saves\\" + str3 + "-" + str2 + "-" + str4 + "-" + str6 + "-" + str7 + "-" + str5;
              string path = this.savePath;
              StreamWriter streamWriter = new StreamWriter(path, false);
              streamWriter.Write(xml);
              streamWriter.Close();
              this.savePath = path;
              try
              {
                this.save.Load(this.savePath);
              }
              catch (XmlException ex)
              {
                int num = (int) MessageBox.Show("Nieprawidłowy XML", "Error");
              }
              int num1 = this.isSave();
              if (num1 != -1)
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
                this.protocolForm1.BackColor = SystemColors.GradientInactiveCaption;
                this.protocolForm2.BackColor = SystemColors.Control;
                this.protocolSummation.BackColor = SystemColors.Control;
                this.protocolCommittee.BackColor = SystemColors.Control;
                this.signProtocol.BackColor = SystemColors.Control;
                this.buttonNext.Visible = false;
                if (num1 != 1)
                {
                  this.Form2panel.Controls.Clear();
                  if (num1 != 2)
                  {
                    this.SummationPanel.Controls.Clear();
                    if (num1 != 3)
                    {
                      this.personList.DataSource = (object) new DataTable();
                      this.personList.Columns.Clear();
                      if (this.personList.Columns["remove"] != null)
                        this.personList.Columns.Remove("remove");
                      if (this.personList.Columns["action3"] != null)
                        this.personList.Columns.Remove("action3");
                      this.personList.Refresh();
                    }
                  }
                }
                this.printErrors(1);
              }
              this.buttonNext.Text = "Dalej";
              this.buttonNext.Click -= new EventHandler(this.protocolForm2_Click);
              this.buttonNext.Click -= new EventHandler(this.protocolSummation_Click);
              this.buttonNext.Click -= new EventHandler(this.committee_Click);
              this.buttonNext.Click -= new EventHandler(this.committeeValid_Click);
              this.buttonNext.Click -= new EventHandler(this.signProtocol_Click);
              this.buttonNext.Click += new EventHandler(this.protocolForm2_Click);
              this.bottomPanel.Location = new Point(this.Form1panel.Location.X, this.Form1panel.Location.Y + this.Form1panel.Size.Height);
              this.bottomPanel.Visible = true;
              this.buttonNext.Visible = true;
            }
          }
          else
          {
            int num2 = (int) MessageBox.Show("Nie można edytować protokołu, ponieważ licencja nie ma do niego uprawnień");
          }
        }
        catch (Exception ex)
        {
          int num = (int) MessageBox.Show("Edytowany plik jest nieprawidłowy.");
        }
      }
      this.wait.setVisible(false);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
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
      this.backgroundWorker1 = new BackgroundWorker();
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
      ((ISupportInitialize) this.LicencesTable).BeginInit();
      this.bottomPanel.SuspendLayout();
      this.buttonPanel.SuspendLayout();
      this.committeePanel.SuspendLayout();
      ((ISupportInitialize) this.personList).BeginInit();
      this.menuStrip1.SuspendLayout();
      this.SuspendLayout();
      this.protocolHeader.AutoSize = true;
      this.protocolHeader.Dock = DockStyle.Top;
      this.protocolHeader.Location = new Point(0, 0);
      this.protocolHeader.MinimumSize = new Size(0, 100);
      this.protocolHeader.Name = "protocolHeader";
      this.protocolHeader.Size = new Size(784, 100);
      this.protocolHeader.TabIndex = 0;
      this.steps.Controls.Add((Control) this.signProtocol);
      this.steps.Controls.Add((Control) this.protocolCommittee);
      this.steps.Controls.Add((Control) this.protocolSummation);
      this.steps.Controls.Add((Control) this.protocolForm2);
      this.steps.Controls.Add((Control) this.protocolForm1);
      this.steps.Dock = DockStyle.Top;
      this.steps.Location = new Point(0, 24);
      this.steps.Name = "steps";
      this.steps.Size = new Size(784, 54);
      this.steps.TabIndex = 1;
      this.steps.TabStop = true;
      this.signProtocol.Dock = DockStyle.Left;
      this.signProtocol.Location = new Point(600, 0);
      this.signProtocol.Name = "signProtocol";
      this.signProtocol.Size = new Size(164, 54);
      this.signProtocol.TabIndex = 4;
      this.signProtocol.TabStop = false;
      this.signProtocol.Text = "Podpisz \r\nprotokół";
      this.signProtocol.UseVisualStyleBackColor = true;
      this.signProtocol.Click += new EventHandler(this.signProtocol_Click);
      this.protocolCommittee.Dock = DockStyle.Left;
      this.protocolCommittee.Location = new Point(450, 0);
      this.protocolCommittee.Name = "protocolCommittee";
      this.protocolCommittee.Size = new Size(150, 54);
      this.protocolCommittee.TabIndex = 3;
      this.protocolCommittee.TabStop = false;
      this.protocolCommittee.Text = "Członkowie \r\nkomisji";
      this.protocolCommittee.UseVisualStyleBackColor = true;
      this.protocolCommittee.Click += new EventHandler(this.committee_Click);
      this.protocolSummation.Dock = DockStyle.Left;
      this.protocolSummation.Location = new Point(300, 0);
      this.protocolSummation.Name = "protocolSummation";
      this.protocolSummation.Size = new Size(150, 54);
      this.protocolSummation.TabIndex = 2;
      this.protocolSummation.TabStop = false;
      this.protocolSummation.Text = "Podsumowanie\r\nprotokołu";
      this.protocolSummation.UseVisualStyleBackColor = true;
      this.protocolSummation.Click += new EventHandler(this.protocolSummation_Click);
      this.protocolForm2.Dock = DockStyle.Left;
      this.protocolForm2.Location = new Point(150, 0);
      this.protocolForm2.Name = "protocolForm2";
      this.protocolForm2.Size = new Size(150, 54);
      this.protocolForm2.TabIndex = 1;
      this.protocolForm2.TabStop = false;
      this.protocolForm2.Text = "Wypełnij protokół\r\ndół-góra";
      this.protocolForm2.UseVisualStyleBackColor = true;
      this.protocolForm2.Click += new EventHandler(this.protocolForm2_Click);
      this.protocolForm1.BackColor = SystemColors.GradientInactiveCaption;
      this.protocolForm1.Dock = DockStyle.Left;
      this.protocolForm1.Location = new Point(0, 0);
      this.protocolForm1.Name = "protocolForm1";
      this.protocolForm1.Size = new Size(150, 54);
      this.protocolForm1.TabIndex = 0;
      this.protocolForm1.TabStop = false;
      this.protocolForm1.Text = "Wypełnij protokół\r\ngóra-dół";
      this.protocolForm1.UseVisualStyleBackColor = false;
      this.protocolForm1.Click += new EventHandler(this.protocolForm1_Click);
      this.panel3.Controls.Add((Control) this.protocolContent);
      this.panel3.Dock = DockStyle.Fill;
      this.panel3.Location = new Point(0, 78);
      this.panel3.MinimumSize = new Size(0, 200);
      this.panel3.Name = "panel3";
      this.panel3.Size = new Size(784, 572);
      this.panel3.TabIndex = 2;
      this.protocolContent.AutoScroll = true;
      this.protocolContent.AutoSize = true;
      this.protocolContent.Controls.Add((Control) this.raportPanel);
      this.protocolContent.Controls.Add((Control) this.signPanel);
      this.protocolContent.Controls.Add((Control) this.SummationPanel);
      this.protocolContent.Controls.Add((Control) this.bottomPanel);
      this.protocolContent.Controls.Add((Control) this.Form2panel);
      this.protocolContent.Controls.Add((Control) this.Form1panel);
      this.protocolContent.Controls.Add((Control) this.committeePanel);
      this.protocolContent.Controls.Add((Control) this.protocolHeader);
      this.protocolContent.Dock = DockStyle.Fill;
      this.protocolContent.Location = new Point(0, 0);
      this.protocolContent.MinimumSize = new Size(0, 50);
      this.protocolContent.Name = "protocolContent";
      this.protocolContent.Size = new Size(784, 572);
      this.protocolContent.TabIndex = 1;
      this.raportPanel.AutoScroll = true;
      this.raportPanel.AutoSize = true;
      this.raportPanel.Location = new Point(6, 197);
      this.raportPanel.Name = "raportPanel";
      this.raportPanel.Size = new Size(755, 20);
      this.raportPanel.TabIndex = 8;
      this.signPanel.AutoScroll = true;
      this.signPanel.AutoSize = true;
      this.signPanel.Controls.Add((Control) this.LicencesTable);
      this.signPanel.Location = new Point(5, 338);
      this.signPanel.Name = "signPanel";
      this.signPanel.Size = new Size(757, 108);
      this.signPanel.TabIndex = 7;
      this.LicencesTable.AllowUserToAddRows = false;
      this.LicencesTable.AllowUserToDeleteRows = false;
      this.LicencesTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.LicencesTable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
      this.LicencesTable.BackgroundColor = SystemColors.Control;
      this.LicencesTable.BorderStyle = BorderStyle.None;
      this.LicencesTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.LicencesTable.GridColor = SystemColors.Control;
      this.LicencesTable.Location = new Point(14, 31);
      this.LicencesTable.MinimumSize = new Size(727, 0);
      this.LicencesTable.Name = "LicencesTable";
      this.LicencesTable.ReadOnly = true;
      this.LicencesTable.Size = new Size(727, 63);
      this.LicencesTable.TabIndex = 2;
      this.SummationPanel.AutoScroll = true;
      this.SummationPanel.AutoSize = true;
      this.SummationPanel.Location = new Point(6, 161);
      this.SummationPanel.Name = "SummationPanel";
      this.SummationPanel.Size = new Size(757, 20);
      this.SummationPanel.TabIndex = 6;
      this.bottomPanel.AutoScroll = true;
      this.bottomPanel.AutoSize = true;
      this.bottomPanel.BackColor = SystemColors.Control;
      this.bottomPanel.Controls.Add((Control) this.errorWarningPanel);
      this.bottomPanel.Controls.Add((Control) this.warningPanel);
      this.bottomPanel.Controls.Add((Control) this.errorPanel);
      this.bottomPanel.Controls.Add((Control) this.buttonPanel);
      this.bottomPanel.Location = new Point(5, 452);
      this.bottomPanel.MinimumSize = new Size(0, 100);
      this.bottomPanel.Name = "bottomPanel";
      this.bottomPanel.Size = new Size(756, 108);
      this.bottomPanel.TabIndex = 6;
      this.errorWarningPanel.AutoScroll = true;
      this.errorWarningPanel.AutoSize = true;
      this.errorWarningPanel.Location = new Point(4, 75);
      this.errorWarningPanel.MinimumSize = new Size(0, 30);
      this.errorWarningPanel.Name = "errorWarningPanel";
      this.errorWarningPanel.Size = new Size(635, 30);
      this.errorWarningPanel.TabIndex = 6;
      this.warningPanel.AutoScroll = true;
      this.warningPanel.AutoSize = true;
      this.warningPanel.Location = new Point(4, 39);
      this.warningPanel.MinimumSize = new Size(0, 30);
      this.warningPanel.Name = "warningPanel";
      this.warningPanel.Size = new Size(635, 30);
      this.warningPanel.TabIndex = 5;
      this.errorPanel.AutoScroll = true;
      this.errorPanel.AutoSize = true;
      this.errorPanel.Location = new Point(4, 3);
      this.errorPanel.MinimumSize = new Size(0, 30);
      this.errorPanel.Name = "errorPanel";
      this.errorPanel.Size = new Size(635, 30);
      this.errorPanel.TabIndex = 4;
      this.buttonPanel.AutoSize = true;
      this.buttonPanel.Controls.Add((Control) this.buttonNext);
      this.buttonPanel.Location = new Point(665, 12);
      this.buttonPanel.Name = "buttonPanel";
      this.buttonPanel.Size = new Size(88, 82);
      this.buttonPanel.TabIndex = 3;
      this.buttonNext.Location = new Point(7, 23);
      this.buttonNext.Name = "buttonNext";
      this.buttonNext.Size = new Size(75, 23);
      this.buttonNext.TabIndex = 2;
      this.buttonNext.Text = "button1";
      this.buttonNext.UseVisualStyleBackColor = true;
      this.Form2panel.AutoScroll = true;
      this.Form2panel.AutoSize = true;
      this.Form2panel.Location = new Point(5, 134);
      this.Form2panel.Name = "Form2panel";
      this.Form2panel.Size = new Size(757, 21);
      this.Form2panel.TabIndex = 5;
      this.Form1panel.AutoScroll = true;
      this.Form1panel.AutoSize = true;
      this.Form1panel.Location = new Point(4, 106);
      this.Form1panel.Name = "Form1panel";
      this.Form1panel.Size = new Size(757, 22);
      this.Form1panel.TabIndex = 4;
      this.committeePanel.AutoSize = true;
      this.committeePanel.Controls.Add((Control) this.komisjaL3);
      this.committeePanel.Controls.Add((Control) this.komisja1);
      this.committeePanel.Controls.Add((Control) this.komisjaL2);
      this.committeePanel.Controls.Add((Control) this.komisjaL1);
      this.committeePanel.Controls.Add((Control) this.personList);
      this.committeePanel.Location = new Point(5, 234);
      this.committeePanel.Name = "committeePanel";
      this.committeePanel.Size = new Size(757, 88);
      this.committeePanel.TabIndex = 3;
      this.komisjaL3.AutoSize = true;
      this.komisjaL3.Location = new Point(89, 3);
      this.komisjaL3.Name = "komisjaL3";
      this.komisjaL3.Size = new Size(10, 13);
      this.komisjaL3.TabIndex = 5;
      this.komisjaL3.Text = " ";
      this.komisja1.AutoSize = true;
      this.komisja1.Location = new Point(7, 3);
      this.komisja1.Name = "komisja1";
      this.komisja1.Size = new Size(0, 13);
      this.komisja1.TabIndex = 4;
      this.komisjaL2.AutoSize = true;
      this.komisjaL2.Location = new Point(48, 3);
      this.komisjaL2.Name = "komisjaL2";
      this.komisjaL2.Size = new Size(10, 13);
      this.komisjaL2.TabIndex = 3;
      this.komisjaL2.Text = " ";
      this.komisjaL1.AutoSize = true;
      this.komisjaL1.Location = new Point(7, 3);
      this.komisjaL1.Name = "komisjaL1";
      this.komisjaL1.Size = new Size(10, 13);
      this.komisjaL1.TabIndex = 2;
      this.komisjaL1.Text = " ";
      this.personList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
      this.personList.BorderStyle = BorderStyle.None;
      this.personList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.personList.Location = new Point(7, 21);
      this.personList.Name = "personList";
      this.personList.Size = new Size(240, 64);
      this.personList.TabIndex = 1;
      this.menuStrip1.Items.AddRange(new ToolStripItem[6]
      {
        (ToolStripItem) this.plikiToolStripMenuItem,
        (ToolStripItem) this.eksportToolStripMenuItem1,
        (ToolStripItem) this.eksportujZSieciToolStripMenuItem,
        (ToolStripItem) this.importujToolStripMenuItem,
        (ToolStripItem) this.importujZSieciToolStripMenuItem,
        (ToolStripItem) this.drukToolStripMenuItem
      });
      this.menuStrip1.Location = new Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new Size(784, 24);
      this.menuStrip1.TabIndex = 3;
      this.menuStrip1.Text = "menuStrip1";
      this.plikiToolStripMenuItem.Name = "plikiToolStripMenuItem";
      this.plikiToolStripMenuItem.Size = new Size(52, 20);
      this.plikiToolStripMenuItem.Text = "Zapisz";
      this.plikiToolStripMenuItem.Click += new EventHandler(this.plikiToolStripMenuItem_Click);
      this.eksportToolStripMenuItem1.Name = "eksportToolStripMenuItem1";
      this.eksportToolStripMenuItem1.Size = new Size(102, 20);
      this.eksportToolStripMenuItem1.Text = "Zapisz na dysku";
      this.eksportToolStripMenuItem1.Click += new EventHandler(this.eksportToolStripMenuItem1_Click);
      this.eksportujZSieciToolStripMenuItem.Name = "eksportujZSieciToolStripMenuItem";
      this.eksportujZSieciToolStripMenuItem.Size = new Size(103, 20);
      this.eksportujZSieciToolStripMenuItem.Text = "Wyślij na serwer";
      this.eksportujZSieciToolStripMenuItem.Click += new EventHandler(this.eksportujZSieciToolStripMenuItem_Click);
      this.importujToolStripMenuItem.Name = "importujToolStripMenuItem";
      this.importujToolStripMenuItem.Size = new Size(102, 20);
      this.importujToolStripMenuItem.Text = "Wczytaj z dysku";
      this.importujToolStripMenuItem.Click += new EventHandler(this.importujToolStripMenuItem_Click);
      this.importujZSieciToolStripMenuItem.Name = "importujZSieciToolStripMenuItem";
      this.importujZSieciToolStripMenuItem.Size = new Size(109, 20);
      this.importujZSieciToolStripMenuItem.Text = "Pobierz z serwera";
      this.importujZSieciToolStripMenuItem.Click += new EventHandler(this.importujZSieciToolStripMenuItem_Click);
      this.drukToolStripMenuItem.Name = "drukToolStripMenuItem";
      this.drukToolStripMenuItem.Size = new Size(54, 20);
      this.drukToolStripMenuItem.Text = "Drukuj";
      this.drukToolStripMenuItem.Click += new EventHandler(this.drukToolStripMenuItem_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.AutoScroll = true;
      this.ClientSize = new Size(784, 650);
      this.Controls.Add((Control) this.panel3);
      this.Controls.Add((Control) this.steps);
      this.Controls.Add((Control) this.menuStrip1);
      this.KeyPreview = true;
      this.MainMenuStrip = this.menuStrip1;
      this.MinimumSize = new Size(690, 385);
      this.Name = "ProtocolForm";
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Formularz protokołu";
      this.KeyDown += new KeyEventHandler(this.ProtocolForm_KeyDown);
      this.steps.ResumeLayout(false);
      this.panel3.ResumeLayout(false);
      this.panel3.PerformLayout();
      this.protocolContent.ResumeLayout(false);
      this.protocolContent.PerformLayout();
      this.signPanel.ResumeLayout(false);
      ((ISupportInitialize) this.LicencesTable).EndInit();
      this.bottomPanel.ResumeLayout(false);
      this.bottomPanel.PerformLayout();
      this.buttonPanel.ResumeLayout(false);
      this.committeePanel.ResumeLayout(false);
      this.committeePanel.PerformLayout();
      ((ISupportInitialize) this.personList).EndInit();
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
