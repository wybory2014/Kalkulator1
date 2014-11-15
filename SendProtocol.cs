// Decompiled with JetBrains decompiler
// Type: Kalkulator1.SendProtocol
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

using Kalkulator1.AdditionalClass;
using Kalkulator1.AdditionalWindow;
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
    private string path = "";
    private FontFamily myfont = new FontFamily("Arial");
    public string komSend = "";
    public string password = "";
    private string zORp = "";
    private string licensePath = "";
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
    private XmlDocument save;
    private string savePath;
    public ErrorProvider errorProvider1;
    public bool goodcertificate;
    public string codeWarning;
    public WaitPanel wait;
    private ProtocolsList form;
    private string OU;
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
      this.path = Path.GetTempPath() + "KBW";
      this.goodcertificate = false;
      this.wait = new WaitPanel("Wait3", this.Width, this.Height);
      this.form = (ProtocolsList) null;
    }

    public SendProtocol(ProtocolsList form, XmlDocument header, string protocolDefinition, string candidates, string committee, string validateDefinition, string save, string OU, string licensePath)
    {
      this.InitializeComponent();
      this.Text = this.Text + " (" + ((object) Kalkulator1.instalClass.Version.getVersion()).ToString() + ")";
      this.licensePath = licensePath;
      this.path = Path.GetTempPath() + "KBW";
      this.preview.Width = 750;
      this.codeWarning = "";
      this.wait = new WaitPanel("Wait3", this.Width, this.Height);
      this.Controls.Add((Control) this.wait.getPanel());
      this.Controls[this.wait.getName()].BringToFront();
      this.form = form;
      this.OU = OU;
      form.wait.setWaitPanel("Trwa otwieranie formularza protokołu", "Proszę czekać");
      form.wait.setVisible(true);
      this.errorProvider1 = new ErrorProvider();
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
        if (protocolDefinition != "")
        {
          this.protocolDefinition = new XmlDocument();
          if (File.Exists(protocolDefinition))
            this.protocolDefinition.Load(protocolDefinition);
        }
        if (candidates != "")
        {
          this.candidates = new XmlDocument();
          if (File.Exists(candidates))
            this.candidates.Load(candidates);
        }
        if (committee != "")
        {
          this.committee = new XmlDocument();
          if (File.Exists(committee))
            this.committee.Load(committee);
        }
        if (validateDefinition != "")
        {
          this.validateDefinition = new XmlDocument();
          if (File.Exists(validateDefinition))
            this.validateDefinition.Load(validateDefinition);
        }
        if (save != "")
        {
          this.save = new XmlDocument();
          if (File.Exists(save))
            this.save.Load(save);
        }
      }
      catch (XmlException ex)
      {
        int num = (int) MessageBox.Show("Nieprawidłowy XML: " + ex.Message, "Error");
      }
      this.goodcertificate = false;
      form.wait.setWaitPanel("Trwa otwieranie formularza protokołu - sprawdzanie plikow definicyjnych formularza protokołu", "Proszę czekać");
      try
      {
        bool flag1 = true;
        bool flag2 = true;
        string str1 = "Nieaktualnie pliki definicyjne: ";
        int num1 = 0;
        foreach (XmlNode xmlNode in this.save.SelectSingleNode("/save/header/defklk"))
        {
          XmlNode namedItem1 = xmlNode.Attributes.GetNamedItem("name");
          XmlNode namedItem2 = xmlNode.Attributes.GetNamedItem("data_wersji");
          if (namedItem1 != null && namedItem2 != null)
          {
            if (this.protocolDefinitionName == namedItem1.Value)
            {
              XmlNode namedItem3 = this.protocolDefinition.SelectSingleNode("/protokol_info").Attributes.GetNamedItem("data_wersji");
              if (namedItem3 != null && namedItem2.Value != namedItem3.Value)
              {
                if (num1 == 0)
                  str1 = str1 + this.protocolDefinitionName + " (wymagany z dnia " + namedItem2.Value + ")";
                else
                  str1 = str1 + ", " + this.protocolDefinitionName + " (wymagany z dnia " + namedItem2.Value + ")";
                ++num1;
                flag2 = false;
              }
            }
            else if (this.candidatesName == namedItem1.Value)
            {
              XmlNode namedItem3 = this.candidates.SelectSingleNode("/listy").Attributes.GetNamedItem("data_aktualizacji");
              if (namedItem3 != null && namedItem2.Value != namedItem3.Value)
              {
                if (num1 == 0)
                  str1 = str1 + this.candidatesName + " (wymagany z dnia " + namedItem2.Value + ")";
                else
                  str1 = str1 + ", " + this.candidatesName + " (wymagany z dnia " + namedItem2.Value + ")";
                ++num1;
                flag2 = false;
              }
            }
            else if (this.committeeName == namedItem1.Value)
            {
              XmlNode namedItem3 = this.committee.SelectSingleNode("/komisja_sklad").Attributes.GetNamedItem("data_wersji");
              if (namedItem3 != null && namedItem2.Value != namedItem3.Value)
              {
                if (num1 == 0)
                  str1 = str1 + this.committeeName + " (wymagany z dnia " + namedItem2.Value + ")";
                else
                  str1 = str1 + ", " + this.committeeName + " (wymagany z dnia " + namedItem2.Value + ")";
                ++num1;
                flag2 = false;
              }
            }
            else if (this.validateDefinitionName == namedItem1.Value)
            {
              XmlNode namedItem3 = this.validateDefinition.SelectSingleNode("/validate_info").Attributes.GetNamedItem("data_wersji");
              if (namedItem3 != null && namedItem2.Value != namedItem3.Value)
              {
                if (num1 == 0)
                  str1 = str1 + this.validateDefinitionName + " (wymagany z dnia " + namedItem2.Value + ")";
                else
                  str1 = str1 + ", " + this.validateDefinitionName + " (wymagany z dnia " + namedItem2.Value + ")";
                ++num1;
                flag2 = false;
              }
            }
            else if (this.headerName == namedItem1.Value)
            {
              XmlNode namedItem3 = this.header.SelectSingleNode("/akcja_wyborcza").Attributes.GetNamedItem("data_aktualizacji");
              if (namedItem3 != null && namedItem2.Value != namedItem3.Value)
              {
                if (num1 == 0)
                  str1 = str1 + this.headerName + " (wymagany z dnia " + namedItem2.Value + ")";
                else
                  str1 = str1 + ", " + this.headerName + " (wymagany z dnia " + namedItem2.Value + ")";
                ++num1;
                flag2 = false;
              }
            }
            else
            {
              flag1 = false;
              break;
            }
          }
        }
        if (!flag1)
        {
          int num2 = (int) MessageBox.Show("Nie posiadasz wszystkich potrzebnych plików klk", "Uwaga");
          this.Close();
        }
        else if (!flag2)
        {
          int num2 = (int) MessageBox.Show("Nie posiadasz odpowiednich plików klk. " + str1, "Uwaga");
          this.Close();
        }
        else
        {
          form.wait.setWaitPanel("Trwa otwieranie formularza protokołu - ładowanie podglądu", "Proszę czekać");
          string controlSum = "";
          XmlNode xmlNode1 = this.save.SelectSingleNode("/save/step");
          if (xmlNode1 != null && xmlNode1.InnerText == "0")
          {
            string input = "";
            this.save.SelectSingleNode("/save/header");
            if (header != null)
              input = input + header.OuterXml;
            XmlNode xmlNode2 = this.save.SelectSingleNode("/save/step");
            if (xmlNode2 != null)
              input = input + xmlNode2.OuterXml;
            XmlNode xmlNode3 = this.save.SelectSingleNode("/save/form");
            if (form != null)
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
          string docDefinitionPath = protocolDefinition.Replace(".xml", ".docx");
          printProtocolNew printProtocolNew = new printProtocolNew();
          string[] strArray6 = this.savePath.Split('\\');
          string[] strArray7 = strArray6[strArray6.Length - 1].Split('-');
          strArray7[1].Replace("Jns", "");
          string inst = strArray7[3].Replace("Inst", "");
          string obw = strArray7[2].Replace("Obw", "");
          string instJNS = strArray7[4].Replace("Obw", "");
          string okr = strArray7[5].Replace("Okr", "").Replace(".xml", "");
          printProtocolNew.ProtocolPrint(this.header, this.save, this.candidates, docDefinitionPath, controlSum, false, obw, inst, okr, candidates, instJNS);
          form.wait.setWaitPanel("Trwa wczytywanie licencji", "Proszę czekać");
          bool flag3 = false;
          bool flag4 = false;
          foreach (XmlNode xmlNode2 in this.save.SelectSingleNode("/save/komisja_sklad"))
          {
            XmlNode namedItem = xmlNode2.Attributes.GetNamedItem("funkcja");
            if (namedItem != null)
            {
              if (namedItem.Value == "ZASTĘPCA")
                flag4 = true;
              if (namedItem.Value == "PRZEWODNICZĄCY")
                flag3 = true;
            }
            if (flag4 && flag3)
              break;
          }
          if (flag3)
          {
            SendProtocol sendProtocol = this;
            string str2 = sendProtocol.zORp + "P";
            sendProtocol.zORp = str2;
          }
          if (flag4)
          {
            SendProtocol sendProtocol = this;
            string str2 = sendProtocol.zORp + "Z";
            sendProtocol.zORp = str2;
          }
          this.getSignPage();
          this.LicencesTable.CellClick += new DataGridViewCellEventHandler(this.getLicense_CellClick);
        }
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Nie mozna wczytać danych. " + ex.Message, "Uwaga");
        this.Close();
      }
      form.wait.setVisible(false);
    }

    private void getLicense_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      try
      {
        if (e.ColumnIndex != this.LicencesTable.Columns["action"].Index)
          return;
        try
        {
          int num1 = (int) new Commit(this.path + "\\Licenses\\" + this.LicencesTable.Rows[e.RowIndex].Cells["Licencja"].Value.ToString(), this).ShowDialog();
          if (this.goodcertificate)
          {
            this.komSend = "";
            if (new Connection().IsAvailableNetworkActive())
            {
              Eksport eksport = new Eksport(this.savePath, true, this, this.licensePath, this.password);
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
                  string str1 = streamReader.ReadToEnd();
                  streamReader.Close();
                  string str2 = str1.Replace("<status>podpisany</status>", "<status>wysłany</status>");
                  StreamWriter streamWriter = new StreamWriter(this.savePath, false);
                  streamWriter.Write(str2);
                  streamWriter.Close();
                }
                catch (Exception ex)
                {
                  int num2 = (int) MessageBox.Show("Protokół został wysłany, ale nie można zmienić jego statusu. " + ex.Message, "Uwaga");
                  this.Close();
                }
              }
            }
            else
              this.komSend = "Protokół nie został wysłany na serwer z powodu braku internetu";
            int num3 = (int) MessageBox.Show(this.komSend, "Uwaga");
            this.Close();
          }
        }
        catch (ArgumentOutOfRangeException ex)
        {
        }
      }
      catch (Exception ex)
      {
      }
    }

    private void getSignPage()
    {
      int x = 0;
      int y1 = 0;
      Label label = new Label();
      label.Text = "Wybierz licencję, którą chcesz podpisać protokół";
      label.AutoSize = true;
      label.MaximumSize = new Size(this.signPanel.Size.Width - 20, 0);
      label.Font = new Font(this.myfont, 9f, FontStyle.Bold);
      label.ForeColor = Color.Black;
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
          Certificate certificate1 = new Certificate();
          foreach (string filename in Directory.EnumerateFiles(this.path + "\\Licenses", "*.pem"))
          {
            Certificate certificate2 = new Certificate(filename);
            if (certificate2.isActiveLicense())
            {
              string[] strArray1 = filename.Split('\\');
              bool flag = false;
              string pattern1 = "^" + str2 + "-" + str1 + "-A";
              for (int index = 0; index < chArray.Length; ++index)
              {
                string pattern2 = "^" + str2 + "-" + str1 + "-" + chArray[index].ToString();
                if (chArray[index].ToString() == "P" || chArray[index].ToString() == "Z")
                  pattern2 = pattern2 + "-" + str3;
                if (Regex.IsMatch(strArray1[strArray1.Length - 1], pattern2) || Regex.IsMatch(strArray1[strArray1.Length - 1], pattern1))
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
          viewButtonColumn.Text = "przejdź";
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
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.signPanel = new Panel();
      this.LicencesTable = new DataGridView();
      this.preview = new Panel();
      this.webPreview = new WebBrowser();
      this.signPanel.SuspendLayout();
      ((ISupportInitialize) this.LicencesTable).BeginInit();
      this.preview.SuspendLayout();
      this.SuspendLayout();
      this.signPanel.AutoScroll = true;
      this.signPanel.AutoSize = true;
      this.signPanel.Controls.Add((Control) this.LicencesTable);
      this.signPanel.Location = new Point(9, 369);
      this.signPanel.Name = "signPanel";
      this.signPanel.Size = new Size(757, 108);
      this.signPanel.TabIndex = 9;
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
      this.preview.AutoSize = true;
      this.preview.Controls.Add((Control) this.webPreview);
      this.preview.Location = new Point(10, 2);
      this.preview.MinimumSize = new Size(0, 100);
      this.preview.Name = "preview";
      this.preview.Size = new Size(757, 361);
      this.preview.TabIndex = 10;
      this.webPreview.Dock = DockStyle.Fill;
      this.webPreview.Location = new Point(0, 0);
      this.webPreview.MinimumSize = new Size(20, 20);
      this.webPreview.Name = "webPreview";
      this.webPreview.Size = new Size(757, 361);
      this.webPreview.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(776, 489);
      this.Controls.Add((Control) this.preview);
      this.Controls.Add((Control) this.signPanel);
      this.Name = "SendProtocol";
      this.Text = "SendProtocol";
      this.signPanel.ResumeLayout(false);
      ((ISupportInitialize) this.LicencesTable).EndInit();
      this.preview.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
