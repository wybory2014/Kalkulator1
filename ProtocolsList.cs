// Decompiled with JetBrains decompiler
// Type: Kalkulator1.ProtocolsList
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

using Ionic.Zip;
using Kalkulator1.AdditionalClass;
using Kalkulator1.AdditionalWindow;
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
    private IContainer components = (IContainer) null;
    private Connection con;
    private XmlDocument header;
    private string electoralEampaignSave;
    private string electoralEampaignURL;
    private string jns;
    private string role;
    private string path;
    private string circuit;
    private string licensepath;
    private DateTime electionData;
    public WaitPanel wait;
    private Start start;
    private WebBrowser web;
    private string version;
    public bool send;
    private bool powiat;
    private string test;
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
      this.path = Path.GetTempPath() + "KBW";
      this.con = new Connection();
      this.header = new XmlDocument();
      this.electoralEampaignSave = "";
      this.jns = "";
      this.role = "";
      this.circuit = "";
      this.wait = new WaitPanel("Wait2", this.Width, this.Height);
      this.Controls.Add((Control) this.wait.getPanel());
      this.Controls[this.wait.getName()].BringToFront();
      this.start = (Start) null;
      this.licensepath = "";
      this.electionData = new DateTime(2014, 8, 27, 7, 0, 0);
      this.send = false;
      this.powiat = false;
    }

    public ProtocolsList(string filepath, Start start, string licensepath, string version)
    {
      this.InitializeComponent();
      this.version = version;
      this.licensepath = licensepath;
      this.Text = this.Text + " (" + ((object) Kalkulator1.instalClass.Version.getVersion()).ToString() + ")";
      this.path = Path.GetTempPath() + "KBW";
      this.electionData = new DateTime(1, 1, 1, 0, 0, 0);
      DataTable dataTable = new DataTable();
      this.con = new Connection();
      this.header = new XmlDocument();
      string[] strArray1 = new X509Certificate(filepath).Subject.Split(new string[1]
      {
        ", "
      }, StringSplitOptions.None);
      for (int index = 0; index < strArray1.Length; ++index)
      {
        if (Regex.IsMatch(strArray1[index], "^OU="))
        {
          string[] strArray2 = strArray1[index].Replace("OU=", "").Split('-');
          this.electoralEampaignURL = strArray2[0];
          this.electoralEampaignSave = strArray2[0].Replace('/', '_');
          this.electoralEampaignSave = this.electoralEampaignSave.Replace(" ", "");
          this.jns = strArray2[1];
          this.role = strArray2[2];
          if (this.role == "P" || this.role == "Z")
            this.circuit = strArray2[3];
          if (this.role == "O" || this.role == "A")
          {
            this.panWyszukiwanie.Visible = true;
            this.txtWyszukaj.Focus();
            break;
          }
          else
            break;
        }
      }
      this.powiat = false;
      this.wait = new WaitPanel("Wait2", this.Width, this.Height);
      this.Controls.Add((Control) this.wait.getPanel());
      this.Controls[this.wait.getName()].BringToFront();
      this.protocolsTable.CellClick += new DataGridViewCellEventHandler(this.getProtocol_CellClick);
      this.start = start;
      if (!Directory.Exists(this.path + "\\electoralEampaign"))
      {
        try
        {
          Directory.CreateDirectory(this.path + "\\electoralEampaign");
        }
        catch (ArgumentNullException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
        }
        catch (ArgumentException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
        }
        catch (UnauthorizedAccessException ex)
        {
          int num = (int) MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
        }
        catch (PathTooLongException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
        }
        catch (DirectoryNotFoundException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
        }
        catch (NotSupportedException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
        }
        catch (IOException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
        }
      }
      if (!Directory.Exists(this.path + "\\ProtocolsDef"))
      {
        try
        {
          Directory.CreateDirectory(this.path + "\\ProtocolsDef");
        }
        catch (ArgumentNullException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
        }
        catch (ArgumentException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
        }
        catch (UnauthorizedAccessException ex)
        {
          int num = (int) MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
        }
        catch (PathTooLongException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
        }
        catch (DirectoryNotFoundException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
        }
        catch (NotSupportedException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
        }
        catch (IOException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
        }
      }
      if (!Directory.Exists(this.path + "\\saves"))
      {
        try
        {
          Directory.CreateDirectory(this.path + "\\saves");
        }
        catch (ArgumentNullException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"saves\"", "Error");
        }
        catch (ArgumentException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"saves\"", "Error");
        }
        catch (UnauthorizedAccessException ex)
        {
          int num = (int) MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako administrator.", "Uwaga");
        }
        catch (PathTooLongException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"saves\"", "Error");
        }
        catch (DirectoryNotFoundException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"saves\"", "Error");
        }
        catch (NotSupportedException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"saves\"", "Error");
        }
        catch (IOException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"saves\"", "Error");
        }
      }
      this.getKLK(true);
      this.getProtocols(true);
    }

    public ProtocolsList(string filepath, Start start, string licensepath, string version, string jnsNew, bool powiat)
    {
      this.InitializeComponent();
      this.version = version;
      this.licensepath = licensepath;
      this.powiat = powiat;
      this.Text = this.Text + " (" + ((object) Kalkulator1.instalClass.Version.getVersion()).ToString() + ")";
      this.path = Path.GetTempPath() + "KBW";
      this.electionData = new DateTime(1, 1, 1, 0, 0, 0);
      DataTable dataTable = new DataTable();
      this.con = new Connection();
      this.header = new XmlDocument();
      string[] strArray1 = new X509Certificate(filepath).Subject.Split(new string[1]
      {
        ", "
      }, StringSplitOptions.None);
      for (int index = 0; index < strArray1.Length; ++index)
      {
        if (Regex.IsMatch(strArray1[index], "^OU="))
        {
          string[] strArray2 = strArray1[index].Replace("OU=", "").Split('-');
          this.electoralEampaignURL = strArray2[0];
          this.electoralEampaignSave = strArray2[0].Replace('/', '_');
          this.electoralEampaignSave = this.electoralEampaignSave.Replace(" ", "");
          this.role = strArray2[2];
          if (this.role == "P" || this.role == "Z")
            this.circuit = strArray2[3];
          if (this.role == "O" || this.role == "A")
          {
            this.panWyszukiwanie.Visible = true;
            this.txtWyszukaj.Focus();
            break;
          }
          else
            break;
        }
      }
      this.jns = jnsNew;
      this.wait = new WaitPanel("Wait2", this.Width, this.Height);
      this.Controls.Add((Control) this.wait.getPanel());
      this.Controls[this.wait.getName()].BringToFront();
      this.protocolsTable.CellClick += new DataGridViewCellEventHandler(this.getProtocol_CellClick);
      this.start = start;
      this.web = new WebBrowser();
      this.web.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(ProtocolsList.webBrowser_DocumentCompleted);
      if (!Directory.Exists(this.path + "\\electoralEampaign"))
      {
        try
        {
          Directory.CreateDirectory(this.path + "\\electoralEampaign");
        }
        catch (ArgumentNullException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
        }
        catch (ArgumentException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
        }
        catch (UnauthorizedAccessException ex)
        {
          int num = (int) MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
        }
        catch (PathTooLongException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
        }
        catch (DirectoryNotFoundException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
        }
        catch (NotSupportedException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
        }
        catch (IOException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
        }
      }
      if (!Directory.Exists(this.path + "\\ProtocolsDef"))
      {
        try
        {
          Directory.CreateDirectory(this.path + "\\ProtocolsDef");
        }
        catch (ArgumentNullException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
        }
        catch (ArgumentException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
        }
        catch (UnauthorizedAccessException ex)
        {
          int num = (int) MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
        }
        catch (PathTooLongException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
        }
        catch (DirectoryNotFoundException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
        }
        catch (NotSupportedException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
        }
        catch (IOException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
        }
      }
      if (!Directory.Exists(this.path + "\\saves"))
      {
        try
        {
          Directory.CreateDirectory(this.path + "\\saves");
        }
        catch (ArgumentNullException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"saves\"", "Error");
        }
        catch (ArgumentException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"saves\"", "Error");
        }
        catch (UnauthorizedAccessException ex)
        {
          int num = (int) MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
        }
        catch (PathTooLongException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"saves\"", "Error");
        }
        catch (DirectoryNotFoundException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"saves\"", "Error");
        }
        catch (NotSupportedException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"saves\"", "Error");
        }
        catch (IOException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"saves\"", "Error");
        }
      }
      this.getKLK(true);
      this.getProtocols(true);
    }

    private void getProtocolsFromNet_Click(object sender, EventArgs e)
    {
      this.getKLK(false);
      this.refreshListOfProtocols();
    }

    public void refreshListOfProtocols()
    {
      this.wait.setWaitPanel("Trwa odświeżanie listy protokołów", "Proszę czekać");
      this.wait.setVisible(true);
      this.protocolsTable.DataSource = (object) new DataTable();
      this.protocolsTable.Refresh();
      this.getProtocols(false);
      this.wait.setWaitPanel("Trwa odświeżanie listy protokołów", "Proszę czekać");
      this.wait.setVisible(true);
      this.protocolsTable.Refresh();
      this.wait.setVisible(false);
    }

    private void getProtocol_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      Exception exception;
      try
      {
        string str1;
        string str2;
        string str3;
        string str4;
        string str5;
        if (e.ColumnIndex == this.protocolsTable.Columns["fill"].Index)
        {
          if (e.RowIndex >= 0)
          {
            try
            {
              DateTime.Compare(this.electionData, DateTime.Now);
              if (-1 <= 0)
              {
                try
                {
                  object obj1 = this.protocolsTable.Rows[e.RowIndex].Cells["Status"].Value;
                  if (obj1 != null && obj1.ToString().ToLower() != "podpisany" && obj1.ToString().ToLower() != "wysłany")
                  {
                    object obj2 = this.protocolsTable.Rows[e.RowIndex].Cells["Protokol"].Value;
                    object obj3 = this.protocolsTable.Rows[e.RowIndex].Cells["Gmina"].Value;
                    object obj4 = this.protocolsTable.Rows[e.RowIndex].Cells["Obwód"].Value;
                    object obj5 = this.protocolsTable.Rows[e.RowIndex].Cells["Instytucja"].Value;
                    object obj6 = this.protocolsTable.Rows[e.RowIndex].Cells["Okręg"].Value;
                    if (obj3 != null && obj4 != null && (obj5 != null && obj6 != null) && obj2 != null)
                    {
                      try
                      {
                        str1 = "";
                        str2 = "";
                        str3 = "";
                        str4 = "";
                        str5 = "";
                        string str6 = obj2.ToString().Split('-')[4];
                        string[] strArray = obj6.ToString().Split(' ');
                        string save = this.path + "\\saves\\" + obj2.ToString();
                        string candidates = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + obj5.ToString() + "-" + str6 + "-" + strArray[0] + ".xml";
                        string protocolDefinition = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + obj5.ToString() + ".xml";
                        string committee = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + this.jns + "-" + obj4.ToString() + ".xml";
                        string validateDefinition = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + obj5.ToString() + "_Walidacja.xml";
                        string OU = "";
                        if (this.role == "P" || this.role == "Z")
                          OU = this.electoralEampaignURL.Trim(' ') + (object) "-" + (string) obj3 + "-" + this.role + "-" + this.circuit;
                        if (this.role == "O" || this.role == "A")
                          OU = this.electoralEampaignURL.Trim(' ') + (object) "-" + (string) obj3 + "-" + this.role;
                        ProtocolForm protocolForm = new ProtocolForm(this, this.header, protocolDefinition, candidates, committee, validateDefinition, save, OU, this.licensepath, this.version);
                        try
                        {
                          int num = (int) protocolForm.ShowDialog();
                        }
                        catch (Exception ex)
                        {
                          exception = ex;
                          this.createProtocols();
                        }
                        this.refreshListOfProtocols();
                      }
                      catch (XmlException ex)
                      {
                        int num = (int) MessageBox.Show("Nieprawidłowy XML", "Error");
                      }
                    }
                  }
                }
                catch (Exception ex)
                {
                  int num = (int) MessageBox.Show(ex.Message, "Error");
                }
              }
              else
              {
                int num1 = (int) MessageBox.Show("Protokoły wypełniać można od " + this.electionData.ToShortDateString(), "Komunikat");
              }
            }
            catch (Exception ex)
            {
              int num = (int) MessageBox.Show("Fill: " + ex.Message, "Error");
            }
          }
        }
        if (e.ColumnIndex == this.protocolsTable.Columns["new"].Index)
        {
          try
          {
            if (e.RowIndex >= 0)
            {
              object obj1 = this.protocolsTable.Rows[e.RowIndex].Cells["Protokol"].Value;
              object obj2 = this.protocolsTable.Rows[e.RowIndex].Cells["Gmina"].Value;
              object obj3 = this.protocolsTable.Rows[e.RowIndex].Cells["Obwód"].Value;
              object obj4 = this.protocolsTable.Rows[e.RowIndex].Cells["Instytucja"].Value;
              object obj5 = this.protocolsTable.Rows[e.RowIndex].Cells["Okręg"].Value;
              object obj6 = this.protocolsTable.Rows[e.RowIndex].Cells["Status"].Value;
              if (obj2 != null && obj3 != null && obj4 != null && obj5 != null)
              {
                try
                {
                  if (obj1 != null && obj6 != null && (obj6.ToString().ToLower() == "podpisany" || obj6.ToString().ToLower() == "wysłany"))
                  {
                    if (MessageBox.Show("Utworzenie nowego protokołu spowoduje nadpisanie wcześniej zachowanych danych. Czy kontynuować?", "Nowy protokół", MessageBoxButtons.YesNo) == DialogResult.No)
                      return;
                    string str6 = obj1.ToString();
                    string[] strArray = obj1.ToString().Split(' ');
                    if (strArray.Length >= 2)
                      str6 = strArray[0];
                    int length = Directory.GetFiles(this.path + "\\saves", str6 + "*.xml").Length;
                    string str7 = str6.Replace(".xml", "") + " " + (length + 1).ToString();
                    File.Move(this.path + "\\saves\\" + obj1.ToString(), this.path + "\\saves\\" + str7 + ".xml");
                  }
                  StreamWriter streamWriter = new StreamWriter(this.path + "\\saves\\" + obj1.ToString(), false);
                  streamWriter.Write("<?xml version=\"1.0\"?><save><status>niewypełniony</status></save>");
                  streamWriter.Close();
                  this.getProtocols(false);
                  if (obj2 != null && obj3 != null && obj4 != null && obj5 != null)
                  {
                    try
                    {
                      str1 = "";
                      str2 = "";
                      str3 = "";
                      str4 = "";
                      str5 = "";
                      string str6 = obj1.ToString().Split('-')[4];
                      string[] strArray = obj5.ToString().Split(' ');
                      string save = this.path + "\\saves\\" + obj1.ToString();
                      string candidates = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + obj4.ToString() + "-" + str6 + "-" + strArray[0] + ".xml";
                      string protocolDefinition = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + obj4.ToString() + ".xml";
                      string committee = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + this.jns + "-" + obj3.ToString() + ".xml";
                      string validateDefinition = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + obj4.ToString() + "_Walidacja.xml";
                      string OU = "";
                      if (this.role == "P" || this.role == "Z")
                        OU = this.electoralEampaignURL.Trim(' ') + (object) "-" + (string) obj2 + "-" + this.role + "-" + this.circuit;
                      if (this.role == "O")
                        OU = this.electoralEampaignURL.Trim(' ') + (object) "-" + (string) obj2 + "-" + this.role;
                      ProtocolForm protocolForm = new ProtocolForm(this, this.header, protocolDefinition, candidates, committee, validateDefinition, save, OU, this.licensepath, this.version);
                      try
                      {
                        int num = (int) protocolForm.ShowDialog();
                      }
                      catch (Exception ex)
                      {
                        this.createProtocols();
                      }
                      this.refreshListOfProtocols();
                    }
                    catch (XmlException ex)
                    {
                      int num = (int) MessageBox.Show("Nieprawidłowy XML", "Error");
                    }
                  }
                }
                catch (XmlException ex)
                {
                  int num = (int) MessageBox.Show("Nieprawidłowy XML", "Error");
                }
              }
            }
          }
          catch (Exception ex)
          {
            int num = (int) MessageBox.Show(ex.Message, "Error");
          }
        }
        if (e.ColumnIndex == this.protocolsTable.Columns["print"].Index | e.ColumnIndex == this.protocolsTable.Columns["pdf"].Index)
        {
          try
          {
            if (e.RowIndex >= 0)
            {
              object obj1 = this.protocolsTable.Rows[e.RowIndex].Cells["Protokol"].Value;
              object obj2 = this.protocolsTable.Rows[e.RowIndex].Cells["Gmina"].Value;
              object obj3 = this.protocolsTable.Rows[e.RowIndex].Cells["Obwód"].Value;
              object obj4 = this.protocolsTable.Rows[e.RowIndex].Cells["Instytucja"].Value;
              object obj5 = this.protocolsTable.Rows[e.RowIndex].Cells["Okręg"].Value;
              if (obj2 != null && obj3 != null && (obj4 != null && obj5 != null) && obj1 != null)
              {
                this.wait.setWaitPanel("Trwa przygotowanie do druku", "Druk");
                if (e.ColumnIndex == this.protocolsTable.Columns["pdf"].Index)
                  this.wait.setWaitPanel("Trwa generowanie PDF", "PDF");
                this.wait.setVisible(true);
                try
                {
                  str1 = "";
                  str2 = "";
                  str5 = "";
                  string instJNS = obj1.ToString().Split('-')[4];
                  string[] strArray = obj5.ToString().Split(' ');
                  string filename1 = this.path + "\\saves\\" + obj1.ToString();
                  string str6 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + obj4.ToString() + "-" + instJNS + "-" + strArray[0] + ".xml";
                  string filename2 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + obj4.ToString() + ".xml";
                  string docDefinitionPath = filename2.Replace(".xml", ".docx");
                  XmlDocument xmlDocument1 = new XmlDocument();
                  XmlDocument xmlDocument2 = new XmlDocument();
                  XmlDocument candidates = new XmlDocument();
                  xmlDocument1.Load(filename2);
                  xmlDocument2.Load(filename1);
                  candidates.Load(str6);
                  string controlSum = "";
                  XmlNode xmlNode1 = xmlDocument2.SelectSingleNode("/save/step");
                  if (xmlNode1 != null && xmlNode1.InnerText == "0")
                  {
                    string input = "";
                    XmlNode xmlNode2 = xmlDocument2.SelectSingleNode("/save/header");
                    if (xmlNode2 != null)
                      input = input + xmlNode2.OuterXml;
                    XmlNode xmlNode3 = xmlDocument2.SelectSingleNode("/save/step");
                    if (xmlNode3 != null)
                      input = input + xmlNode3.OuterXml;
                    XmlNode xmlNode4 = xmlDocument2.SelectSingleNode("/save/form");
                    if (xmlNode4 != null)
                      input = input + xmlNode4.OuterXml;
                    XmlNode xmlNode5 = xmlDocument2.SelectSingleNode("/save/komisja_sklad");
                    if (xmlNode5 != null)
                      input = input + xmlNode5.OuterXml;
                    XmlNode xmlNode6 = xmlDocument2.SelectSingleNode("/save/hardWarningCode");
                    if (xmlNode6 != null)
                      input = input + xmlNode6.OuterXml;
                    XmlNode xmlNode7 = xmlDocument2.SelectSingleNode("/save/softError");
                    if (xmlNode7 != null)
                      input = input + xmlNode7.OuterXml;
                    XmlNode xmlNode8 = xmlDocument2.SelectSingleNode("/save/hardError");
                    if (xmlNode8 != null)
                      input = input + xmlNode8.OuterXml;
                    XmlNode xmlNode9 = xmlDocument2.SelectSingleNode("/save/hardWarning");
                    if (xmlNode9 != null)
                      input = input + xmlNode9.OuterXml;
                    controlSum = new ClassMd5().CreateMD5Hash(input);
                  }
                  printProtocolNew printProtocolNew = new printProtocolNew();
                  if (e.ColumnIndex == this.protocolsTable.Columns["print"].Index)
                    printProtocolNew.ProtocolPrint(this.header, xmlDocument2, candidates, docDefinitionPath, controlSum, false, obj3.ToString(), obj4.ToString(), obj5.ToString(), str6, instJNS);
                  else
                    printProtocolNew.ProtocolPrint(this.header, xmlDocument2, candidates, docDefinitionPath, controlSum, true, obj3.ToString(), obj4.ToString(), obj5.ToString(), str6, instJNS);
                }
                catch (XmlException ex)
                {
                  int num = (int) MessageBox.Show("Nieprawidłowy XML", "Error");
                }
              }
              this.wait.setVisible(false);
            }
          }
          catch (Exception ex)
          {
            int num = (int) MessageBox.Show("Print: " + ex.Message, "Error");
          }
        }
        if (e.ColumnIndex == this.protocolsTable.Columns["send"].Index)
        {
          try
          {
            if (e.RowIndex >= 0)
            {
              object obj1 = this.protocolsTable.Rows[e.RowIndex].Cells["Status"].Value;
              if (obj1 != null && (obj1.ToString().ToLower() == "podpisany" || obj1.ToString().ToLower() == "wysłany"))
              {
                object obj2 = this.protocolsTable.Rows[e.RowIndex].Cells["Protokol"].Value;
                object obj3 = this.protocolsTable.Rows[e.RowIndex].Cells["Gmina"].Value;
                object obj4 = this.protocolsTable.Rows[e.RowIndex].Cells["Obwód"].Value;
                object obj5 = this.protocolsTable.Rows[e.RowIndex].Cells["Instytucja"].Value;
                object obj6 = this.protocolsTable.Rows[e.RowIndex].Cells["Okręg"].Value;
                if (obj3 != null && obj4 != null && (obj5 != null && obj6 != null) && obj2 != null)
                {
                  try
                  {
                    str5 = "";
                    obj6.ToString().Split(' ');
                    string path = this.path + "\\saves\\" + obj2.ToString();
                    StreamReader streamReader = new StreamReader(path);
                    string str6 = streamReader.ReadToEnd();
                    streamReader.Close();
                    string xml = str6.Replace("<status>wysłany</status>", "<status>podpisany</status>");
                    if (this.con.IsAvailableNetworkActive())
                    {
                      Eksport eksport = new Eksport(xml, true, this);
                      try
                      {
                        int num = (int) eksport.ShowDialog();
                      }
                      catch (Exception ex)
                      {
                      }
                      if (this.send)
                      {
                        this.send = false;
                        string str7 = xml.Replace("<status>podpisany</status>", "<status>wysłany</status>");
                        StreamWriter streamWriter = new StreamWriter(path);
                        streamWriter.Write(str7);
                        streamWriter.Close();
                        this.refreshListOfProtocols();
                      }
                    }
                    else
                    {
                      int num1 = (int) MessageBox.Show("Protokół nie został wysłany z powodu braku Internetu.");
                    }
                  }
                  catch (XmlException ex)
                  {
                    int num = (int) MessageBox.Show("Nieprawidłowy XML", "Error");
                  }
                  catch (NullReferenceException ex)
                  {
                    int num = (int) MessageBox.Show("Podanno inny xml niz header", "Error");
                  }
                }
              }
              if (obj1 != null && (obj1.ToString().ToLower() == "roboczy" || obj1 != null && obj1.ToString().ToLower() == "niewypełniony"))
              {
                int num2 = (int) MessageBox.Show("Nie można wysłać protokołu o statusie: \"" + obj1.ToString().ToLower() + "\"", "Komunikat");
              }
            }
          }
          catch (Exception ex)
          {
            int num = (int) MessageBox.Show(ex.Message, "Error");
          }
        }
        if (e.ColumnIndex == this.protocolsTable.Columns["save1"].Index && e.RowIndex >= 0)
        {
          string path = this.path + "\\saves\\" + this.protocolsTable.Rows[e.RowIndex].Cells["Protokol"].Value.ToString();
          StreamReader streamReader = new StreamReader(path);
          string str6 = streamReader.ReadToEnd();
          streamReader.Close();
          string[] strArray = path.Split('\\');
          SaveFileDialog saveFileDialog = new SaveFileDialog();
          saveFileDialog.Filter = "(*.xml)|*.xml";
          saveFileDialog.FileName = strArray[strArray.Length - 1];
          if (saveFileDialog.ShowDialog() != DialogResult.Cancel)
          {
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
                  streamWriter.Write(str6);
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
        }
      }
      catch (ArgumentOutOfRangeException ex)
      {
      }
      catch (Exception ex)
      {
        exception = ex;
      }
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
        int num = (int) MessageBox.Show("Usuniecie standarodwej stopki i nagłówka nie powiodło się. " + ex.Message, "Uwaga");
      }
      webBrowser.ShowPrintDialog();
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
      if (Directory.Exists(this.path + "\\ProtocolsDef"))
      {
        DataTable dataTable = new DataTable();
        this.protocolsTable.DataSource = (object) dataTable;
        dataTable.Columns.Add(new DataColumn("Gmina", typeof (string)));
        dataTable.Columns.Add(new DataColumn("Obwód", typeof (int)));
        dataTable.Columns.Add(new DataColumn("Okręg", typeof (string)));
        dataTable.Columns.Add(new DataColumn("Instytucja", typeof (string)));
        dataTable.Columns.Add(new DataColumn("JNS instytucji", typeof (string)));
        dataTable.Columns.Add(new DataColumn("Adres Obwodu", typeof (string)));
        dataTable.Columns.Add(new DataColumn("Status", typeof (string)));
        dataTable.Columns.Add(new DataColumn("Ostatnia aktualizacja", typeof (string)));
        dataTable.Columns.Add(new DataColumn("Protokol", typeof (string)));
        string str1 = "";
        if (this.role == "P" || this.role == "Z")
          str1 = "-" + ((object) this.circuit).ToString();
        try
        {
          if (Directory.Exists(this.path + "\\saves"))
          {
            List<string> list = new List<string>();
            if (this.role == "A")
            {
              string str2 = this.jns;
              if (str2.Length < 6)
              {
                while (str2.Length < 6)
                  str2 = "0" + str2;
              }
              if (this.powiat)
              {
                if (str2.Substring(0, 4) == "1465")
                {
                  list.Add("RDP");
                }
                else
                {
                  list.Add("RDA");
                  list.Add("WBP");
                }
              }
              else if ((int) str2[2] == 55 || (int) str2[2] == 54)
              {
                if (str2.Substring(0, 4) == "1465")
                {
                  list.Add("RDP");
                  list.Add("RDW");
                  list.Add("WBP");
                }
                else
                  list.Add("RDP");
              }
              else
              {
                list.Add("RDP");
                list.Add("RDW");
              }
            }
            foreach (string str2 in Directory.EnumerateFiles(this.path + "\\saves", this.electoralEampaignSave + "-" + this.jns + str1 + "-*.xml"))
            {
              bool flag = true;
              string[] strArray1 = str2.Split('\\');
              DataRow row = dataTable.NewRow();
              string[] strArray2 = strArray1[strArray1.Length - 1].Replace(".xml", "").Split('-');
              row[0] = (object) strArray2[1];
              row[1] = (object) strArray2[2];
              row[2] = (object) strArray2[5];
              row[4] = (object) strArray2[4];
              if (this.role == "A")
              {
                for (int index = 0; index < list.Count; ++index)
                {
                  if (strArray2[3] == list[index])
                  {
                    flag = false;
                    break;
                  }
                }
              }
              if (flag)
              {
                string[] strArray3 = strArray2[5].Split(' ');
                if (strArray3.Length > 2)
                {
                  string str3 = strArray3[0] + " v." + strArray3[1];
                  for (int index = 2; index < strArray3.Length; ++index)
                    str3 = str3 + " " + strArray3[index];
                  row[2] = (object) str3;
                }
                if (strArray3.Length == 2)
                  row[2] = (object) (strArray3[0] + " v." + strArray3[1]);
                row[3] = (object) strArray2[3];
                try
                {
                  foreach (XmlNode xmlNode in this.header.SelectSingleNode("/akcja_wyborcza/jns"))
                  {
                    if (xmlNode.Name == "obw")
                    {
                      XmlNode namedItem1 = xmlNode.Attributes.GetNamedItem("nr");
                      if (namedItem1 != null && namedItem1.Value == strArray2[2])
                      {
                        XmlNode namedItem2 = xmlNode.Attributes.GetNamedItem("siedziba");
                        row[5] = namedItem2 == null ? (object) "" : (object) namedItem2.Value;
                      }
                    }
                  }
                  XmlDocument xmlDocument = new XmlDocument();
                  xmlDocument.Load(str2);
                  XmlNode xmlNode1 = xmlDocument.SelectSingleNode("/save/status");
                  row[6] = xmlNode1 == null ? (object) "" : (object) xmlNode1.InnerText;
                }
                catch (XmlException ex)
                {
                }
                catch (NullReferenceException ex)
                {
                }
                row[7] = (object) "";
                try
                {
                  row[7] = (object) File.GetLastWriteTime(str2);
                }
                catch (Exception ex)
                {
                }
                row[8] = (object) strArray1[strArray1.Length - 1];
                if (this.txtWyszukaj.Text != "" && this.panWyszukiwanie.Visible)
                {
                  if (row[1].ToString() == this.txtWyszukaj.Text)
                    dataTable.Rows.Add(row);
                }
                else
                  dataTable.Rows.Add(row);
              }
            }
          }
        }
        catch (ArgumentNullException ex)
        {
          int num = (int) MessageBox.Show("Błędna ścieżka", "Error");
        }
        catch (ArgumentOutOfRangeException ex)
        {
          int num = (int) MessageBox.Show("Błędna opcja szukania", "Error");
        }
        catch (ArgumentException ex)
        {
          int num = (int) MessageBox.Show("Błędna ścieżka", "Error");
        }
        catch (DirectoryNotFoundException ex)
        {
          int num = (int) MessageBox.Show("Nie znaleziono katalogu", "Error");
        }
        catch (PathTooLongException ex)
        {
          int num = (int) MessageBox.Show("Zbyt długa ścieżka do katalogu", "Error");
        }
        catch (IOException ex)
        {
          int num = (int) MessageBox.Show("Podano ścieżke do pliku zamiast katalogu", "Error");
        }
        catch (UnauthorizedAccessException ex)
        {
          int num = (int) MessageBox.Show("Program nie posiada uprawnień do usuwania plików", "Error");
        }
        if (dataTable.Rows.Count > 0)
        {
          this.protocolsTable.DataSource = (object) dataTable;
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
          DataGridViewButtonColumn viewButtonColumn1 = new DataGridViewButtonColumn();
          viewButtonColumn1.HeaderText = "";
          viewButtonColumn1.Text = "Drukuj";
          viewButtonColumn1.Name = "print";
          viewButtonColumn1.UseColumnTextForButtonValue = true;
          if (this.protocolsTable.Columns["print"] == null)
          {
            this.protocolsTable.Columns.Insert(8, (DataGridViewColumn) viewButtonColumn1);
            this.protocolsTable.Columns["print"].FillWeight = 8f;
          }
          else
          {
            this.protocolsTable.Columns["print"].DisplayIndex = 8;
            this.protocolsTable.Columns["print"].FillWeight = 8f;
          }
          this.protocolsPanel.Visible = true;
          DataGridViewButtonColumn viewButtonColumn2 = new DataGridViewButtonColumn();
          viewButtonColumn2.HeaderText = "";
          viewButtonColumn2.Text = "PDF";
          viewButtonColumn2.Name = "pdf";
          viewButtonColumn2.UseColumnTextForButtonValue = true;
          if (this.protocolsTable.Columns["pdf"] == null)
          {
            this.protocolsTable.Columns.Insert(9, (DataGridViewColumn) viewButtonColumn2);
            this.protocolsTable.Columns["pdf"].FillWeight = 7f;
          }
          else
          {
            this.protocolsTable.Columns["pdf"].DisplayIndex = 9;
            this.protocolsTable.Columns["pdf"].FillWeight = 7f;
          }
          DataGridViewButtonColumn viewButtonColumn3 = new DataGridViewButtonColumn();
          viewButtonColumn3.HeaderText = "";
          viewButtonColumn3.Text = "Edytuj";
          viewButtonColumn3.Name = "fill";
          viewButtonColumn3.UseColumnTextForButtonValue = true;
          if (this.protocolsTable.Columns["fill"] == null)
          {
            this.protocolsTable.Columns.Insert(10, (DataGridViewColumn) viewButtonColumn3);
            this.protocolsTable.Columns["fill"].FillWeight = 8f;
          }
          else
          {
            this.protocolsTable.Columns["fill"].DisplayIndex = 10;
            this.protocolsTable.Columns["fill"].FillWeight = 8f;
          }
          DataGridViewButtonColumn viewButtonColumn4 = new DataGridViewButtonColumn();
          viewButtonColumn4.HeaderText = "";
          viewButtonColumn4.Text = "Nowy";
          viewButtonColumn4.Name = "new";
          viewButtonColumn4.UseColumnTextForButtonValue = true;
          if (this.protocolsTable.Columns["new"] == null)
          {
            this.protocolsTable.Columns.Insert(11, (DataGridViewColumn) viewButtonColumn4);
            this.protocolsTable.Columns["new"].FillWeight = 8f;
          }
          else
          {
            this.protocolsTable.Columns["new"].DisplayIndex = 11;
            this.protocolsTable.Columns["new"].FillWeight = 8f;
          }
          DataGridViewButtonColumn viewButtonColumn5 = new DataGridViewButtonColumn();
          viewButtonColumn5.HeaderText = "";
          viewButtonColumn5.Text = "Wyślij";
          viewButtonColumn5.Name = "send";
          viewButtonColumn5.UseColumnTextForButtonValue = true;
          if (this.protocolsTable.Columns["send"] == null)
          {
            this.protocolsTable.Columns.Insert(12, (DataGridViewColumn) viewButtonColumn5);
            this.protocolsTable.Columns["send"].FillWeight = 8f;
          }
          else
          {
            this.protocolsTable.Columns["send"].DisplayIndex = 12;
            this.protocolsTable.Columns["send"].FillWeight = 8f;
          }
          DataGridViewButtonColumn viewButtonColumn6 = new DataGridViewButtonColumn();
          viewButtonColumn6.HeaderText = "";
          viewButtonColumn6.Text = "Zapisz";
          viewButtonColumn6.Name = "save1";
          viewButtonColumn6.UseColumnTextForButtonValue = true;
          if (this.protocolsTable.Columns["save1"] == null)
          {
            this.protocolsTable.Columns.Insert(13, (DataGridViewColumn) viewButtonColumn6);
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
        this.protocolsPanel.Visible = false;
      this.wait.setVisible(false);
      if (!old)
        return;
      this.start.wait.setVisible(false);
    }

    private void getKLK(bool old)
    {
      this.test = "";
      this.wait.setWaitPanel("Trwa przygotowanie listy protokołów", "Proszę czekać");
      this.wait.setVisible(true);
      if (old)
        this.start.wait.setWaitPanel("Trwa przygotowanie listy protokołów", "Proszę czekać");
      this.createProtocols();
      bool flag = false;
      for (int index = 0; index < 10; ++index)
      {
        flag = this.con.IsAvailableNetworkActive();
        if (flag)
          break;
      }
      if (flag)
      {
        this.wait.setWaitPanel("Trwa aktualizowanie danych klk", "Proszę czekać");
        if (old)
          this.start.wait.setWaitPanel("Trwa aktualizowanie danych klk", "Proszę czekać");
        string server2 = "KALK/";
        string savePath1 = this.path + "\\electoralEampaign\\" + this.electoralEampaignSave + "-" + this.jns + ".xml";
        KLKresponse requestKbwKlk = this.con.getRequestKBWKlk(server2 + "pollstations/" + this.electoralEampaignURL + "-" + this.jns, savePath1, 0);
        if (Directory.Exists(this.path + "\\electoralEampaign"))
        {
          if (File.Exists(this.path + "\\electoralEampaign\\" + this.electoralEampaignSave + "-" + this.jns + ".xml"))
          {
            try
            {
              this.header.Load(this.path + "\\electoralEampaign\\" + this.electoralEampaignSave + "-" + this.jns + ".xml");
              int num = 1;
              string str1 = "";
              string str2 = "";
              XmlNode xmlNode1 = this.header.SelectSingleNode("/akcja_wyborcza/jns");
              string str3 = this.jns;
              if (str3.Length < 6)
              {
                while (str3.Length < 6)
                  str3 = "0" + str3;
              }
              foreach (XmlNode xmlNode2 in xmlNode1)
              {
                if (xmlNode2.Name == "obw")
                {
                  XmlNode namedItem1 = xmlNode2.Attributes.GetNamedItem("nr");
                  if (namedItem1 != null)
                    str1 = namedItem1.Value;
                  KLKresponse klKresponse;
                  if (str1 != "" && (this.role == "O" || this.role == "A" || (this.role == "P" || this.role == "Z") && str1 == this.circuit))
                  {
                    string savePath2 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + this.jns + "-" + str1 + ".xml";
                    if (!Start.listaPlikow.Contains(savePath2))
                    {
                      klKresponse = this.con.getRequestKBWKlk(server2 + "peoplecommittes/" + this.electoralEampaignURL + "-" + this.jns + "-" + str1, savePath2, 0);
                      Start.listaPlikow.Add(savePath2);
                    }
                  }
                  char ch;
                  if ((this.role == "O" || this.role == "A") && num == 1)
                  {
                    this.getInstData("RDA", server2);
                    this.getInstData("RDW", server2);
                    this.getInstData("RDP", server2);
                    this.getInstData("WBP", server2);
                    for (int index = 0; index < 23; ++index)
                    {
                      string savePath2 = this.path + (object) "\\ProtocolsDef\\" + this.electoralEampaignSave + "-RDA-" + str3 + "-" + (string) (object) index + ".xml";
                      if (!Start.listaPlikow.Contains(savePath2))
                      {
                        string uri = server2 + (object) "candidates/" + this.electoralEampaignURL + "-RDA-" + str3 + "-" + (string) (object) index;
                        ProtocolsList protocolsList = this;
                        string str4 = protocolsList.test;
                        string str5 = uri;
                        string str6 = "; ";
                        ch = '\n';
                        string str7 = ch.ToString();
                        string str8 = str4 + str5 + str6 + str7;
                        protocolsList.test = str8;
                        klKresponse = this.con.getRequestKBWKlk(uri, savePath2, 0);
                        Start.listaPlikow.Add(savePath2);
                      }
                      string savePath3 = this.path + (object) "\\ProtocolsDef\\" + this.electoralEampaignSave + "-RDW-" + str3 + "-" + (string) (object) index + ".xml";
                      if (!Start.listaPlikow.Contains(savePath3))
                      {
                        string uri = server2 + (object) "candidates/" + this.electoralEampaignURL + "-RDW-" + str3 + "-" + (string) (object) index;
                        ProtocolsList protocolsList = this;
                        string str4 = protocolsList.test;
                        string str5 = uri;
                        string str6 = "; ";
                        ch = '\n';
                        string str7 = ch.ToString();
                        string str8 = str4 + str5 + str6 + str7;
                        protocolsList.test = str8;
                        klKresponse = this.con.getRequestKBWKlk(uri, savePath3, 0);
                        Start.listaPlikow.Add(savePath3);
                      }
                      string str9 = "";
                      if (this.jns.Length == 6)
                        str9 = this.jns.Substring(0, 2) + "0000";
                      if (this.jns.Length == 5)
                        str9 = this.jns.Substring(0, 1) + "0000";
                      string savePath4 = this.path + (object) "\\ProtocolsDef\\" + this.electoralEampaignSave + "-RDW-" + str9 + "-" + (string) (object) index + ".xml";
                      if (!Start.listaPlikow.Contains(savePath4))
                      {
                        string uri = server2 + (object) "candidates/" + this.electoralEampaignURL + "-RDW-" + str9 + "-" + (string) (object) index;
                        ProtocolsList protocolsList = this;
                        string str4 = protocolsList.test;
                        string str5 = uri;
                        string str6 = "; ";
                        ch = '\n';
                        string str7 = ch.ToString();
                        string str8 = str4 + str5 + str6 + str7;
                        protocolsList.test = str8;
                        klKresponse = this.con.getRequestKBWKlk(uri, savePath4, 0);
                        Start.listaPlikow.Add(savePath4);
                      }
                      string savePath5 = this.path + (object) "\\ProtocolsDef\\" + this.electoralEampaignSave + "-RDP-" + str3 + "-" + (string) (object) index + ".xml";
                      if (!Start.listaPlikow.Contains(savePath5))
                      {
                        string uri = server2 + (object) "candidates/" + this.electoralEampaignURL + "-RDP-" + str3 + "-" + (string) (object) index;
                        ProtocolsList protocolsList = this;
                        string str4 = protocolsList.test;
                        string str5 = uri;
                        string str6 = "; ";
                        ch = '\n';
                        string str7 = ch.ToString();
                        string str8 = str4 + str5 + str6 + str7;
                        protocolsList.test = str8;
                        klKresponse = this.con.getRequestKBWKlk(uri, savePath5, 0);
                        Start.listaPlikow.Add(savePath5);
                      }
                      string str10 = "";
                      if (this.jns.Length == 6)
                        str10 = this.jns.Substring(0, 4) + "0000";
                      if (this.jns.Length == 5)
                        str10 = this.jns.Substring(0, 3) + "0000";
                      string savePath6 = this.path + (object) "\\ProtocolsDef\\" + this.electoralEampaignSave + "-RDP-" + str10 + "-" + (string) (object) index + ".xml";
                      if (!Start.listaPlikow.Contains(savePath6))
                      {
                        string uri = server2 + (object) "candidates/" + this.electoralEampaignURL + "-RDP-" + str10 + "-" + (string) (object) index;
                        ProtocolsList protocolsList = this;
                        string str4 = protocolsList.test;
                        string str5 = uri;
                        string str6 = "; ";
                        ch = '\n';
                        string str7 = ch.ToString();
                        string str8 = str4 + str5 + str6 + str7;
                        protocolsList.test = str8;
                        klKresponse = this.con.getRequestKBWKlk(uri, savePath6, 0);
                        Start.listaPlikow.Add(savePath6);
                      }
                      string savePath7 = this.path + (object) "\\ProtocolsDef\\" + this.electoralEampaignSave + "-WBP-" + str3 + "-" + (string) (object) index + ".xml";
                      if (!Start.listaPlikow.Contains(savePath7))
                      {
                        string uri = server2 + (object) "candidates/" + this.electoralEampaignURL + "-WBP-" + str3 + "-" + (string) (object) index;
                        ProtocolsList protocolsList = this;
                        string str4 = protocolsList.test;
                        string str5 = uri;
                        string str6 = "; ";
                        ch = '\n';
                        string str7 = ch.ToString();
                        string str8 = str4 + str5 + str6 + str7;
                        protocolsList.test = str8;
                        klKresponse = this.con.getRequestKBWKlk(uri, savePath7, 0);
                        Start.listaPlikow.Add(savePath7);
                      }
                      if (this.jns.Substring(0, 4) == "1465")
                      {
                        string savePath8 = this.path + (object) "\\ProtocolsDef\\" + this.electoralEampaignSave + "-RDA-146501-" + (string) (object) index + ".xml";
                        if (!Start.listaPlikow.Contains(savePath8))
                        {
                          string uri = server2 + (object) "candidates/" + this.electoralEampaignURL + "-RDA-146501-" + (string) (object) index;
                          ProtocolsList protocolsList = this;
                          string str4 = protocolsList.test;
                          string str5 = uri;
                          string str6 = "; ";
                          ch = '\n';
                          string str7 = ch.ToString();
                          string str8 = str4 + str5 + str6 + str7;
                          protocolsList.test = str8;
                          klKresponse = this.con.getRequestKBWKlk(uri, savePath8, 0);
                          Start.listaPlikow.Add(savePath8);
                        }
                      }
                    }
                    if (this.jns.Substring(0, 4) == "1465")
                    {
                      string savePath2 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-WBP-146501-1.xml";
                      if (!Start.listaPlikow.Contains(savePath2))
                      {
                        string uri = server2 + "candidates/" + this.electoralEampaignURL + "-WBP-146501-1";
                        ProtocolsList protocolsList = this;
                        string str4 = protocolsList.test;
                        string str5 = uri;
                        string str6 = "; ";
                        ch = '\n';
                        string str7 = ch.ToString();
                        string str8 = str4 + str5 + str6 + str7;
                        protocolsList.test = str8;
                        klKresponse = this.con.getRequestKBWKlk(uri, savePath2, 0);
                        Start.listaPlikow.Add(savePath2);
                      }
                    }
                  }
                  if (this.role == "P" || this.role == "Z")
                  {
                    foreach (XmlNode xmlNode3 in xmlNode2)
                    {
                      if (xmlNode3.Name == "inst")
                      {
                        XmlNode namedItem2 = xmlNode3.Attributes.GetNamedItem("kod");
                        if (namedItem2 != null)
                          str2 = namedItem2.Value;
                        string str4 = "";
                        XmlNode namedItem3 = xmlNode3.Attributes.GetNamedItem("organNazwa");
                        if (namedItem3 != null)
                          str4 = namedItem3.Value;
                        string str5 = "";
                        XmlNode namedItem4 = xmlNode3.Attributes.GetNamedItem("inst_jns");
                        if (namedItem4 != null)
                          str5 = namedItem4.Value;
                        if (str2 == "RDA" && str4 == "m.st.")
                        {
                          string savePath2 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + str2 + "_M.xml";
                          if (!Start.listaPlikow.Contains(savePath2))
                          {
                            klKresponse = this.con.getRequestKBWKlk(server2 + "layout/" + this.electoralEampaignURL + "-" + str2 + "_M", savePath2, 0);
                            Start.listaPlikow.Add(savePath2);
                          }
                          string savePath3 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + str2 + "_M.docx";
                          if (!Start.listaPlikow.Contains(savePath3))
                          {
                            klKresponse = this.con.getRequestKBWKlkDocx(server2 + "layout/" + this.electoralEampaignURL + "-" + str2 + "_M.docx", savePath3, 0);
                            Start.listaPlikow.Add(savePath3);
                          }
                          string savePath4 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + str2 + "_M_EMPTY.docx";
                          if (!Start.listaPlikow.Contains(savePath4))
                          {
                            klKresponse = this.con.getRequestKBWKlkDocx(server2 + "layout/" + this.electoralEampaignURL + "-" + str2 + "_M_EMPTY.docx", savePath4, 0);
                            Start.listaPlikow.Add(savePath4);
                          }
                          string savePath5 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + str2 + "_D_EMPTY.docx";
                          if (!Start.listaPlikow.Contains(savePath5))
                          {
                            klKresponse = this.con.getRequestKBWKlkDocx(server2 + "layout/" + this.electoralEampaignURL + "-" + str2 + "_D_EMPTY.docx", savePath5, 0);
                            Start.listaPlikow.Add(savePath5);
                          }
                          string savePath6 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + str2 + "_M_ERR.docx";
                          if (!Start.listaPlikow.Contains(savePath6))
                          {
                            klKresponse = this.con.getRequestKBWKlkDocx(server2 + "layout/" + this.electoralEampaignURL + "-" + str2 + "_M_ERR.docx", savePath6, 0);
                            Start.listaPlikow.Add(savePath6);
                          }
                          string savePath7 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + str2 + "_M_Walidacja.xml";
                          if (!Start.listaPlikow.Contains(savePath7))
                          {
                            klKresponse = this.con.getRequestKBWKlk(server2 + "integrity/" + this.electoralEampaignURL + "-" + str2 + "_M", savePath7, 0);
                            Start.listaPlikow.Add(savePath7);
                          }
                        }
                        if (str2 == "RDA" && str4 == "Dzielnicy")
                        {
                          string savePath2 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + str2 + "_D.xml";
                          if (!Start.listaPlikow.Contains(savePath2))
                          {
                            klKresponse = this.con.getRequestKBWKlk(server2 + "layout/" + this.electoralEampaignURL + "-" + str2 + "_D", savePath2, 0);
                            Start.listaPlikow.Add(savePath2);
                          }
                          string savePath3 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + str2 + "_D.docx";
                          if (!Start.listaPlikow.Contains(savePath3))
                          {
                            klKresponse = this.con.getRequestKBWKlkDocx(server2 + "layout/" + this.electoralEampaignURL + "-" + str2 + "_D.docx", savePath3, 0);
                            Start.listaPlikow.Add(savePath3);
                          }
                          string savePath4 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + str2 + "_D_EMPTY.docx";
                          if (!Start.listaPlikow.Contains(savePath4))
                          {
                            klKresponse = this.con.getRequestKBWKlkDocx(server2 + "layout/" + this.electoralEampaignURL + "-" + str2 + "_D_EMPTY.docx", savePath4, 0);
                            Start.listaPlikow.Add(savePath4);
                          }
                          string savePath5 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + str2 + "_D_ERR.docx";
                          if (!Start.listaPlikow.Contains(savePath5))
                          {
                            klKresponse = this.con.getRequestKBWKlkDocx(server2 + "layout/" + this.electoralEampaignURL + "-" + str2 + "_D_ERR.docx", savePath5, 0);
                            Start.listaPlikow.Add(savePath5);
                          }
                          string savePath6 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + str2 + "_D_Walidacja.xml";
                          if (!Start.listaPlikow.Contains(savePath6))
                          {
                            klKresponse = this.con.getRequestKBWKlk(server2 + "integrity/" + this.electoralEampaignURL + "-" + str2 + "_D", savePath6, 0);
                            Start.listaPlikow.Add(savePath6);
                          }
                        }
                        if (str2 != "")
                        {
                          string savePath2 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + str2 + ".xml";
                          if (!Start.listaPlikow.Contains(savePath2))
                          {
                            klKresponse = this.con.getRequestKBWKlk(server2 + "layout/" + this.electoralEampaignURL + "-" + str2, savePath2, 0);
                            Start.listaPlikow.Add(savePath2);
                          }
                          string savePath3 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + str2 + ".docx";
                          if (!Start.listaPlikow.Contains(savePath3))
                          {
                            klKresponse = this.con.getRequestKBWKlkDocx(server2 + "layout/" + this.electoralEampaignURL + "-" + str2 + ".docx", savePath3, 0);
                            Start.listaPlikow.Add(savePath3);
                          }
                          string savePath4 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + str2 + "_EMPTY.docx";
                          if (!Start.listaPlikow.Contains(savePath4))
                          {
                            klKresponse = this.con.getRequestKBWKlkDocx(server2 + "layout/" + this.electoralEampaignURL + "-" + str2 + "_EMPTY.docx", savePath4, 0);
                            Start.listaPlikow.Add(savePath4);
                          }
                          string savePath5 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + str2 + "_ERR.docx";
                          if (!Start.listaPlikow.Contains(savePath5))
                          {
                            klKresponse = this.con.getRequestKBWKlkDocx(server2 + "layout/" + this.electoralEampaignURL + "-" + str2 + "_ERR.docx", savePath5, 0);
                            Start.listaPlikow.Add(savePath5);
                          }
                          string savePath6 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + str2 + "_Walidacja.xml";
                          if (!Start.listaPlikow.Contains(savePath6))
                          {
                            klKresponse = this.con.getRequestKBWKlk(server2 + "integrity/" + this.electoralEampaignURL + "-" + str2, savePath6, 0);
                            Start.listaPlikow.Add(savePath6);
                          }
                          if (str2 == "WBP")
                          {
                            string savePath7 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + str2 + "_1.xml";
                            if (!Start.listaPlikow.Contains(savePath7))
                            {
                              klKresponse = this.con.getRequestKBWKlk(server2 + "layout/" + this.electoralEampaignURL + "-" + str2 + "_1", savePath7, 0);
                              Start.listaPlikow.Add(savePath7);
                            }
                            string savePath8 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + str2 + "_1.docx";
                            if (!Start.listaPlikow.Contains(savePath8))
                            {
                              klKresponse = this.con.getRequestKBWKlkDocx(server2 + "layout/" + this.electoralEampaignURL + "-" + str2 + "_1.docx", savePath8, 0);
                              Start.listaPlikow.Add(savePath8);
                            }
                            string savePath9 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + str2 + "_1_EMPTY.docx";
                            if (!Start.listaPlikow.Contains(savePath9))
                            {
                              klKresponse = this.con.getRequestKBWKlkDocx(server2 + "layout/" + this.electoralEampaignURL + "-" + str2 + "_1_EMPTY.docx", savePath9, 0);
                              Start.listaPlikow.Add(savePath9);
                            }
                            string savePath10 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + str2 + "_1_ERR.docx";
                            if (!Start.listaPlikow.Contains(savePath10))
                            {
                              klKresponse = this.con.getRequestKBWKlkDocx(server2 + "layout/" + this.electoralEampaignURL + "-" + str2 + "_1_ERR.docx", savePath10, 0);
                              Start.listaPlikow.Add(savePath10);
                            }
                            string savePath11 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + str2 + "_1_Walidacja.xml";
                            if (!Start.listaPlikow.Contains(savePath11))
                            {
                              klKresponse = this.con.getRequestKBWKlk(server2 + "integrity/" + this.electoralEampaignURL + "-" + str2 + "_1", savePath11, 0);
                              Start.listaPlikow.Add(savePath11);
                            }
                          }
                        }
                        foreach (XmlNode xmlNode4 in xmlNode3)
                        {
                          if (xmlNode4.Name == "okr")
                          {
                            XmlNode namedItem5 = xmlNode4.Attributes.GetNamedItem("nr");
                            xmlNode4.Attributes.GetNamedItem("status");
                            if (namedItem5 != null && str2 != "" && namedItem5.Value != "")
                            {
                              string savePath2 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + str2 + "-" + str5 + "-" + namedItem5.Value + ".xml";
                              if (!Start.listaPlikow.Contains(savePath2))
                              {
                                string uri = server2 + "candidates/" + this.electoralEampaignURL + "-" + str2 + "-" + str5 + "-" + namedItem5.Value;
                                ProtocolsList protocolsList = this;
                                string str6 = protocolsList.test;
                                string str7 = uri;
                                string str8 = "; ";
                                ch = '\n';
                                string str9 = ch.ToString();
                                string str10 = str6 + str7 + str8 + str9;
                                protocolsList.test = str10;
                                klKresponse = this.con.getRequestKBWKlk(uri, savePath2, 0);
                                Start.listaPlikow.Add(savePath2);
                              }
                            }
                          }
                        }
                      }
                    }
                  }
                  ++num;
                }
              }
            }
            catch (XmlException ex)
            {
              int num = (int) MessageBox.Show("Nieprawidłowy XML", "Error");
            }
            catch (NullReferenceException ex)
            {
              int num = (int) MessageBox.Show("Podanno inny xml niz header", "Error");
            }
          }
        }
        this.wait.setWaitPanel("Trwa aktualizowanie listy protokołów", "Proszę czekać");
        if (old)
          this.start.wait.setWaitPanel("Trwa aktualizowanie listy protokołów", "Proszę czekać");
        if (requestKbwKlk.isSaved())
          this.createProtocols();
      }
      else
        this.getKLKWithoutNet(old, false);
      this.setElectionData();
      this.wait.setVisible(false);
      if (old)
        this.start.wait.setVisible(false);
      if (old)
        return;
      this.refreshListOfProtocols();
    }

    private void createProtocols()
    {
      if (!Directory.Exists(this.path + "\\electoralEampaign"))
        return;
      if (File.Exists(this.path + "\\electoralEampaign\\" + this.electoralEampaignSave + "-" + this.jns + ".xml"))
      {
        try
        {
          this.header.Load(this.path + "\\electoralEampaign\\" + this.electoralEampaignSave + "-" + this.jns + ".xml");
          if (!Directory.Exists(this.path + "\\saves"))
            Directory.CreateDirectory(this.path + "\\saves");
          try
          {
            string str1 = "";
            string str2 = "";
            string str3 = "";
            foreach (XmlNode xmlNode1 in this.header.SelectSingleNode("/akcja_wyborcza/jns"))
            {
              if (xmlNode1.Name == "obw")
              {
                XmlNode namedItem1 = xmlNode1.Attributes.GetNamedItem("nr");
                if (namedItem1 != null)
                  str1 = "-" + namedItem1.Value;
                foreach (XmlNode xmlNode2 in xmlNode1)
                {
                  if (xmlNode2.Name == "inst")
                  {
                    XmlNode namedItem2 = xmlNode2.Attributes.GetNamedItem("kod");
                    if (namedItem2 != null)
                      str2 = "-" + namedItem2.Value;
                    XmlNode namedItem3 = xmlNode2.Attributes.GetNamedItem("inst_jns");
                    if (namedItem3 != null)
                      str3 = "-" + namedItem3.Value;
                    foreach (XmlNode xmlNode3 in xmlNode2)
                    {
                      if (xmlNode3.Name == "okr")
                      {
                        XmlNode namedItem4 = xmlNode3.Attributes.GetNamedItem("nr");
                        XmlNode namedItem5 = xmlNode3.Attributes.GetNamedItem("status");
                        if (namedItem4 != null && namedItem5 != null && namedItem5.Value == "A")
                        {
                          if (!File.Exists(this.path + "\\saves\\" + this.electoralEampaignSave + "-" + this.jns + str1 + str2 + str3 + "-" + namedItem4.Value + ".xml"))
                          {
                            try
                            {
                              StreamWriter streamWriter = new StreamWriter(this.path + "\\saves\\" + this.electoralEampaignSave + "-" + this.jns + str1 + str2 + str3 + "-" + namedItem4.Value + ".xml", false);
                              streamWriter.Write("<?xml version=\"1.0\"?><save><status>niewypełniony</status></save>");
                              streamWriter.Close();
                            }
                            catch (ArgumentNullException ex)
                            {
                              int num = (int) MessageBox.Show("Błędna ścieżka", "Error");
                            }
                            catch (ArgumentException ex)
                            {
                              int num = (int) MessageBox.Show("Błędna ścieżka", "Error");
                            }
                            catch (DirectoryNotFoundException ex)
                            {
                              int num = (int) MessageBox.Show("Nie znaleziono katalogu", "Error");
                            }
                            catch (PathTooLongException ex)
                            {
                              int num = (int) MessageBox.Show("Zbyt długa ścieżka do katalogu", "Error");
                            }
                            catch (IOException ex)
                            {
                              int num = (int) MessageBox.Show("Podano ścieżke do pliku zamiast katalogu", "Error");
                            }
                            catch (UnauthorizedAccessException ex)
                            {
                              int num = (int) MessageBox.Show("Program nie posiada uprawnień do usuwania plików", "Error");
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
          catch (XmlException ex)
          {
            int num = (int) MessageBox.Show("Nieprawidłowy XML", "Error");
          }
          catch (ArgumentNullException ex)
          {
            int num = (int) MessageBox.Show("Błędna ścieżka", "Error");
          }
          catch (ArgumentOutOfRangeException ex)
          {
            int num = (int) MessageBox.Show("Błędna opcja szukania", "Error");
          }
          catch (ArgumentException ex)
          {
            int num = (int) MessageBox.Show("Błędna ścieżka", "Error");
          }
          catch (DirectoryNotFoundException ex)
          {
            int num = (int) MessageBox.Show("Nie znaleziono katalogu", "Error");
          }
          catch (FileNotFoundException ex)
          {
            int num = (int) MessageBox.Show("Nie znaleziono pliku", "Error");
          }
          catch (IOException ex)
          {
            int num = (int) MessageBox.Show("Podano ścieżke do pliku zamiast katalogu", "Error");
          }
          catch (UnauthorizedAccessException ex)
          {
            int num = (int) MessageBox.Show("Program nie posiada uprawnień do usuwania plików", "Error");
          }
          catch (NotSupportedException ex)
          {
            int num = (int) MessageBox.Show("Nieprawidłowa nazwa pliku", "Error");
          }
        }
        catch (XmlException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowy XML: " + ex.Message, "Błąd");
        }
        catch (NullReferenceException ex)
        {
          int num = (int) MessageBox.Show("Podanno inny xml niz header", "Error");
        }
        catch (ArgumentNullException ex)
        {
          int num = (int) MessageBox.Show("Błędna ścieżka", "Error");
        }
        catch (ArgumentOutOfRangeException ex)
        {
          int num = (int) MessageBox.Show("Błędna opcja szukania", "Error");
        }
        catch (ArgumentException ex)
        {
          int num = (int) MessageBox.Show("Błędna ścieżka", "Error");
        }
        catch (DirectoryNotFoundException ex)
        {
          int num = (int) MessageBox.Show("Nie znaleziono katalogu", "Error");
        }
        catch (FileNotFoundException ex)
        {
          int num = (int) MessageBox.Show("Nie znaleziono pliku", "Error");
        }
        catch (IOException ex)
        {
          int num = (int) MessageBox.Show("Podano ścieżke do pliku zamiast katalogu", "Error");
        }
        catch (UnauthorizedAccessException ex)
        {
          int num = (int) MessageBox.Show("Program nie posiada uprawnień do usuwania plików", "Error");
        }
        catch (NotSupportedException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa nazwa pliku", "Error");
        }
      }
    }

    private void getKLKWithoutNet(bool old, bool update)
    {
      this.wait.setWaitPanel("Trwa aktualizowanie danych klk", "Proszę czekać");
      if (old)
        this.start.wait.setWaitPanel("Trwa aktualizowanie danych klk", "Proszę czekać");
      if (!Directory.Exists(this.path + "\\electoralEampaign"))
      {
        try
        {
          Directory.CreateDirectory(this.path + "\\electoralEampaign");
        }
        catch (ArgumentNullException ex)
        {
          int num = (int) MessageBox.Show("Błędna ścieżka", "Error");
        }
        catch (ArgumentException ex)
        {
          int num = (int) MessageBox.Show("Błędna ścieżka", "Error");
        }
        catch (DirectoryNotFoundException ex)
        {
          int num = (int) MessageBox.Show("Nie znaleziono katalogu", "Error");
        }
        catch (PathTooLongException ex)
        {
          int num = (int) MessageBox.Show("Zbyt długa ścieżka do katalogu", "Error");
        }
        catch (IOException ex)
        {
          int num = (int) MessageBox.Show("Podano ścieżke do pliku zamiast katalogu", "Error");
        }
        catch (UnauthorizedAccessException ex)
        {
          int num = (int) MessageBox.Show("Program nie posiada uprawnień do usuwania plików", "Error");
        }
        catch (NotSupportedException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa nazwa pliku", "Error");
        }
      }
      if (!update)
      {
        this.getKlkFromDisc_Click(new object(), new EventArgs());
        this.createProtocols();
      }
      else if (Directory.Exists(this.path + "\\electoralEampaign"))
      {
        GetKlk getKlk = new GetKlk();
        int num = (int) (this.circuit == null || !(this.circuit != "") ? (Form) new GetKlk(this.path, this.electoralEampaignSave, this.jns, this.role) : (Form) new GetKlk(this.path, this.electoralEampaignSave, this.jns, this.role, this.circuit)).ShowDialog();
        this.wait.setWaitPanel("Trwa aktualizowanie listy protokołów", "Proszę czekać");
        if (old)
          this.start.wait.setWaitPanel("Trwa aktualizowanie listy protokołów", "Proszę czekać");
        this.createProtocols();
      }
    }

    private void getKlkFromDisc_Click(object sender, EventArgs e)
    {
      this.ExtractKlkFromDisc();
      this.setElectionData();
      this.createProtocols();
      this.refreshListOfProtocols();
    }

    private void ExtractKlkFromDisc(object sender, EventArgs ex)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Title = "Wskaż plik zip zawierający dokumenty...";
      openFileDialog.Filter = "(*.zip)|*.zip";
      string fileName = "";
      if (openFileDialog.ShowDialog() == DialogResult.OK)
        fileName = openFileDialog.FileName;
      if (!(fileName != ""))
        return;
      this.wait.setWaitPanel("Trwa importowanie danych", "Proszę czekać...");
      using (ZipFile zipFile = ZipFile.Read(fileName))
      {
        foreach (ZipEntry zipEntry in zipFile)
        {
          if (File.Exists(this.path + "\\" + zipEntry.FileName))
            File.Delete(this.path + "\\" + zipEntry.FileName);
          zipEntry.Extract(this.path);
        }
      }
      this.wait.setVisible(false);
    }

    private void protocolsTable_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Tab && e.KeyCode != Keys.Return)
        return;
      int index = (sender as DataGridView).CurrentRow.Index;
      int num1 = 1;
      int num2 = index + num1;
      int rowCount = (sender as DataGridView).RowCount;
      if (index < rowCount)
        SendKeys.Send("{down}");
      else
        SendKeys.Send("{home}");
    }

    private void wczytaj_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "(*.xml)|*.xml";
      string path = "";
      if (openFileDialog.ShowDialog() == DialogResult.OK)
        path = openFileDialog.FileName;
      if (!(path != ""))
        return;
      this.wait.setWaitPanel("Trwa importowanie protokołu", "Proszę czekać");
      this.wait.setVisible(true);
      StreamReader streamReader = new StreamReader(path);
      string xml = streamReader.ReadToEnd();
      streamReader.Close();
      try
      {
        if (xml != "")
        {
          bool flag = false;
          XmlDocument xmlDocument = new XmlDocument();
          xmlDocument.LoadXml(xml);
          string str1 = "";
          string str2 = "0";
          string str3 = "";
          string str4 = "0";
          XmlNode xmlNode1 = xmlDocument.SelectSingleNode("save/header/defklk");
          if (xmlNode1 != null && xmlNode1.FirstChild != null && xmlNode1.FirstChild.Value != "")
          {
            string[] strArray = xmlNode1.FirstChild.Attributes.GetNamedItem("name").Value.Split('-');
            str1 = strArray[0];
            str3 = strArray[1];
          }
          XmlNode xmlNode2 = xmlDocument.SelectSingleNode("/save/header/instJNS");
          if (xmlNode2 != null && xmlNode2.InnerText != "")
            str4 = xmlNode2.InnerText;
          XmlNode xmlNode3 = xmlDocument.SelectSingleNode("save/header/jns_kod");
          if (xmlNode3 != null && xmlNode3.InnerText != "")
            str2 = xmlNode3.InnerText;
          string str5 = "";
          string str6 = "";
          XmlNode xmlNode4 = xmlDocument.SelectSingleNode("save/header/nrObwodu");
          if (xmlNode4 != null && xmlNode4.InnerText != "")
            str6 = xmlNode4.InnerText;
          XmlNode xmlNode5 = xmlDocument.SelectSingleNode("save/header/nrOkregu");
          if (xmlNode5 != null && xmlNode5.InnerText != "")
            str5 = xmlNode5.InnerText;
          if ((this.role == "O" || this.role == "A") && (Convert.ToInt32(this.jns) == Convert.ToInt32(str2) && this.electoralEampaignURL == str1))
            flag = true;
          string str7 = "";
          if (this.role == "P" || this.role == "Z")
          {
            if (Convert.ToInt32(this.jns) == Convert.ToInt32(str2) && this.electoralEampaignURL == str1 && this.circuit == str6)
              flag = true;
            else
              str7 = " oraz obwód: " + str6;
          }
          if (flag)
          {
            int length = Directory.GetFiles(this.path + "\\saves", this.electoralEampaignSave + "-" + this.jns + "-" + str6 + "-" + str3 + "-" + str4 + "-" + str5 + "*.xml").Length;
            string str8 = this.electoralEampaignSave + "-" + this.jns + "-" + str6 + "-" + str3 + "-" + str4 + "-" + str5;
            if (length > 0)
              str8 = str8 + " " + (length + 1).ToString();
            StreamWriter streamWriter = new StreamWriter(this.path + "\\saves\\" + str8 + ".xml", false);
            streamWriter.Write(xml);
            streamWriter.Close();
            int num = (int) MessageBox.Show("Plik został zaimportowany jako " + str8, "Import protokołu");
            this.refreshListOfProtocols();
            this.wait.setVisible(false);
          }
          else if (str2 == "0" || str4 == "0")
          {
            int num1 = (int) MessageBox.Show("Nie można zaimportować niewypełnionego protokołu.", "Uwaga");
          }
          else
          {
            int num2 = (int) MessageBox.Show("Nie jesteś uprawniony do wczytania tego porotokołu. Zaloguj sie na licencje osoby przydzielonej do akcji wyborczej: " + str1 + " i jns: " + str2 + str7 + " aby zaimportowac protokół.", "Uwaga");
          }
          this.wait.setVisible(false);
        }
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Nie można wczytać tego porotokołu. Orginal exception: " + ex.Message, "Uwaga");
      }
      this.wait.setVisible(false);
    }

    private void setElectionData()
    {
      if (!Directory.Exists(this.path + "\\electoralEampaign"))
        return;
      if (File.Exists(this.path + "\\electoralEampaign\\" + this.electoralEampaignSave + "-" + this.jns + ".xml"))
      {
        try
        {
          this.header.Load(this.path + "\\electoralEampaign\\" + this.electoralEampaignSave + "-" + this.jns + ".xml");
          XmlNode namedItem = this.header.SelectSingleNode("/akcja_wyborcza").Attributes.GetNamedItem("data-ost-aktualizacji");
          if (namedItem != null && Regex.IsMatch(namedItem.Value, "^[0-9]{4}-[0-9]{2}-[0-9]{2}$"))
          {
            string[] strArray = namedItem.Value.Split('-');
            try
            {
              this.electionData = new DateTime(Convert.ToInt32(strArray[0]), Convert.ToInt32(strArray[1]), Convert.ToInt32(strArray[2]), 7, 0, 0);
            }
            catch (Exception ex)
            {
              this.electionData = new DateTime(1, 1, 1, 0, 0, 0);
            }
          }
          else
            this.electionData = new DateTime(1, 1, 1, 0, 0, 0);
        }
        catch (Exception ex)
        {
        }
      }
    }

    private void attendance_Click(object sender, EventArgs e)
    {
      try
      {
        int num = (int) new Attendance(this.licensepath, this.header, this.jns, this.role, this.circuit, this.electoralEampaignSave).ShowDialog();
      }
      catch (Exception ex)
      {
      }
    }

    private void getInstData(string instkod, string server2)
    {
      string savePath1 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + instkod + ".xml";
      KLKresponse klKresponse = this.con.getRequestKBWKlk(server2 + "layout/" + this.electoralEampaignURL + "-" + instkod, savePath1, 0);
      string savePath2 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + instkod + ".docx";
      klKresponse = this.con.getRequestKBWKlkDocx(server2 + "layout/" + this.electoralEampaignURL + "-" + instkod + ".docx", savePath2, 0);
      string savePath3 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + instkod + "_EMPTY.docx";
      klKresponse = this.con.getRequestKBWKlkDocx(server2 + "layout/" + this.electoralEampaignURL + "-" + instkod + "_EMPTY.docx", savePath3, 0);
      string savePath4 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + instkod + "_Walidacja.xml";
      klKresponse = this.con.getRequestKBWKlk(server2 + "integrity/" + this.electoralEampaignURL + "-" + instkod, savePath4, 0);
      if (instkod == "RDA")
      {
        string savePath5 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + instkod + "_M.xml";
        klKresponse = this.con.getRequestKBWKlk(server2 + "layout/" + this.electoralEampaignURL + "-" + instkod + "_M", savePath5, 0);
        string savePath6 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + instkod + "_D.xml";
        klKresponse = this.con.getRequestKBWKlk(server2 + "layout/" + this.electoralEampaignURL + "-" + instkod + "_D", savePath6, 0);
        string str1 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + instkod + "_M.docx";
        if (!File.Exists(str1))
          klKresponse = this.con.getRequestKBWKlkDocx(server2 + "layout/" + this.electoralEampaignURL + "-" + instkod + "_M.docx", str1, 0);
        string str2 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + instkod + "_D.docx";
        if (!File.Exists(str2))
          klKresponse = this.con.getRequestKBWKlkDocx(server2 + "layout/" + this.electoralEampaignURL + "-" + instkod + "_D.docx", str2, 0);
        string str3 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + instkod + "_M_EMPTY.docx";
        if (!File.Exists(str3))
          klKresponse = this.con.getRequestKBWKlkDocx(server2 + "layout/" + this.electoralEampaignURL + "-" + instkod + "_M_EMPTY.docx", str3, 0);
        string str4 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + instkod + "_D_EMPTY.docx";
        if (!File.Exists(str4))
          klKresponse = this.con.getRequestKBWKlkDocx(server2 + "layout/" + this.electoralEampaignURL + "-" + instkod + "_D_EMPTY.docx", str4, 0);
        string savePath7 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + instkod + "_M_Walidacja.xml";
        klKresponse = this.con.getRequestKBWKlk(server2 + "integrity/" + this.electoralEampaignURL + "-" + instkod + "_M", savePath7, 0);
        string savePath8 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + instkod + "_D_Walidacja.xml";
        klKresponse = this.con.getRequestKBWKlk(server2 + "integrity/" + this.electoralEampaignURL + "-" + instkod + "_D", savePath8, 0);
      }
      if (!(instkod == "WBP"))
        return;
      string savePath9 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + instkod + "_1.xml";
      klKresponse = this.con.getRequestKBWKlk(server2 + "layout/" + this.electoralEampaignURL + "-" + instkod + "_1", savePath9, 0);
      string str5 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + instkod + "_1.docx";
      if (!File.Exists(str5))
        klKresponse = this.con.getRequestKBWKlkDocx(server2 + "layout/" + this.electoralEampaignURL + "-" + instkod + "_1.docx", str5, 0);
      string str6 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + instkod + "_1_EMPTY.docx";
      if (!File.Exists(str6))
        klKresponse = this.con.getRequestKBWKlkDocx(server2 + "layout/" + this.electoralEampaignURL + "-" + instkod + "_1_EMPTY.docx", str6, 0);
      string savePath10 = this.path + "\\ProtocolsDef\\" + this.electoralEampaignSave + "-" + instkod + "_1_Walidacja.xml";
      klKresponse = this.con.getRequestKBWKlk(server2 + "integrity/" + this.electoralEampaignURL + "-" + instkod + "_1", savePath10, 0);
    }

    private void ExtractKlkFromDisc()
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Title = "Wskaż plik zip zawierający dokumenty...";
      openFileDialog.Filter = "(*.zip)|*.zip";
      string fileName = "";
      if (openFileDialog.ShowDialog() == DialogResult.OK)
        fileName = openFileDialog.FileName;
      if (!(fileName != ""))
        return;
      this.wait.setWaitPanel("Trwa importowanie danych", "Proszę czekać...");
      using (ZipFile zipFile = ZipFile.Read(fileName))
      {
        foreach (ZipEntry zipEntry in zipFile)
        {
          if (File.Exists(this.path + "\\" + zipEntry.FileName))
            File.Delete(this.path + "\\" + zipEntry.FileName);
          zipEntry.Extract(this.path);
        }
      }
      this.wait.setVisible(false);
    }

    private void txtWyszukaj_TextChanged(object sender, EventArgs e)
    {
      this.getProtocols(false);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
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
      ((ISupportInitialize) this.protocolsTable).BeginInit();
      this.SuspendLayout();
      this.attendance.Location = new Point(34, 23);
      this.attendance.Margin = new Padding(6);
      this.attendance.Name = "attendance";
      this.attendance.Size = new Size(320, 44);
      this.attendance.TabIndex = 4;
      this.attendance.Text = "Wprowadź frekwencję";
      this.attendance.UseVisualStyleBackColor = true;
      this.attendance.Click += new EventHandler(this.attendance_Click);
      this.protocolsPanel.Controls.Add((Control) this.panel1);
      this.protocolsPanel.Controls.Add((Control) this.protocolsTable);
      this.protocolsPanel.Dock = DockStyle.Fill;
      this.protocolsPanel.Location = new Point(0, 0);
      this.protocolsPanel.Margin = new Padding(6);
      this.protocolsPanel.Name = "protocolsPanel";
      this.protocolsPanel.Size = new Size(1752, 787);
      this.protocolsPanel.TabIndex = 4;
      this.panel1.Controls.Add((Control) this.panWyszukiwanie);
      this.panel1.Controls.Add((Control) this.attendance);
      this.panel1.Controls.Add((Control) this.button1);
      this.panel1.Controls.Add((Control) this.getKlkFromDisc);
      this.panel1.Controls.Add((Control) this.getProtocolsFromNet);
      this.panel1.Controls.Add((Control) this.label1);
      this.panel1.Dock = DockStyle.Top;
      this.panel1.Location = new Point(0, 0);
      this.panel1.Margin = new Padding(6);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(1752, 121);
      this.panel1.TabIndex = 4;
      this.panWyszukiwanie.Controls.Add((Control) this.lblWyszukaj);
      this.panWyszukiwanie.Controls.Add((Control) this.txtWyszukaj);
      this.panWyszukiwanie.Location = new Point(372, 23);
      this.panWyszukiwanie.Name = "panWyszukiwanie";
      this.panWyszukiwanie.Size = new Size(747, 89);
      this.panWyszukiwanie.TabIndex = 1;
      this.panWyszukiwanie.Visible = false;
      this.lblWyszukaj.AutoSize = true;
      this.lblWyszukaj.Location = new Point(39, 34);
      this.lblWyszukaj.Margin = new Padding(6, 0, 6, 0);
      this.lblWyszukaj.Name = "lblWyszukaj";
      this.lblWyszukaj.Size = new Size(157, 26);
      this.lblWyszukaj.TabIndex = 8;
      this.lblWyszukaj.Text = "Wyszukiwanie:";
      this.txtWyszukaj.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.txtWyszukaj.Location = new Point(205, 22);
      this.txtWyszukaj.Name = "txtWyszukaj";
      this.txtWyszukaj.Size = new Size(486, 44);
      this.txtWyszukaj.TabIndex = 1;
      this.txtWyszukaj.TextAlign = HorizontalAlignment.Center;
      this.txtWyszukaj.TextChanged += new EventHandler(this.txtWyszukaj_TextChanged);
      this.button1.Location = new Point(1128, 71);
      this.button1.Margin = new Padding(6);
      this.button1.Name = "button1";
      this.button1.Size = new Size(618, 44);
      this.button1.TabIndex = 3;
      this.button1.Text = "Wczytaj protokół z dysku";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.wczytaj_Click);
      this.getKlkFromDisc.Location = new Point(1408, 23);
      this.getKlkFromDisc.Margin = new Padding(6);
      this.getKlkFromDisc.Name = "getKlkFromDisc";
      this.getKlkFromDisc.Size = new Size(338, 44);
      this.getKlkFromDisc.TabIndex = 2;
      this.getKlkFromDisc.Text = "Wczytaj pliki definicyjne z dysku";
      this.getKlkFromDisc.UseVisualStyleBackColor = true;
      this.getKlkFromDisc.Click += new EventHandler(this.getKlkFromDisc_Click);
      this.getProtocolsFromNet.Location = new Point(1128, 23);
      this.getProtocolsFromNet.Margin = new Padding(6);
      this.getProtocolsFromNet.Name = "getProtocolsFromNet";
      this.getProtocolsFromNet.Size = new Size(268, 44);
      this.getProtocolsFromNet.TabIndex = 1;
      this.getProtocolsFromNet.Text = "Aktualizuj pliki definicyjne";
      this.getProtocolsFromNet.UseVisualStyleBackColor = true;
      this.getProtocolsFromNet.Click += new EventHandler(this.getProtocolsFromNet_Click);
      this.label1.AutoSize = true;
      this.label1.Location = new Point(28, 81);
      this.label1.Margin = new Padding(6, 0, 6, 0);
      this.label1.Name = "label1";
      this.label1.Size = new Size(324, 26);
      this.label1.TabIndex = 0;
      this.label1.Text = "Wybierz protokół do wypełnienia";
      this.protocolsTable.AllowUserToAddRows = false;
      this.protocolsTable.AllowUserToDeleteRows = false;
      this.protocolsTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.protocolsTable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
      this.protocolsTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.protocolsTable.Location = new Point(34, 144);
      this.protocolsTable.Margin = new Padding(6);
      this.protocolsTable.MultiSelect = false;
      this.protocolsTable.Name = "protocolsTable";
      this.protocolsTable.ReadOnly = true;
      this.protocolsTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.protocolsTable.Size = new Size(1694, 619);
      this.protocolsTable.TabIndex = 1;
      this.AutoScaleDimensions = new SizeF(12f, 25f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(1752, 787);
      this.Controls.Add((Control) this.protocolsPanel);
      this.Margin = new Padding(6);
      this.Name = "ProtocolsList";
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Lista Protokołów";
      this.protocolsPanel.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.panWyszukiwanie.ResumeLayout(false);
      this.panWyszukiwanie.PerformLayout();
      ((ISupportInitialize) this.protocolsTable).EndInit();
      this.ResumeLayout(false);
    }
  }
}
