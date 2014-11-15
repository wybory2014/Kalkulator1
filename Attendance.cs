// Decompiled with JetBrains decompiler
// Type: Kalkulator1.Attendance
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

using Kalkulator1.AdditionalClass;
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
    private IContainer components = (IContainer) null;
    private string licensePath;
    public string password;
    public bool logged;
    private string role;
    private string path;
    private XmlDocument header;
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
      this.path = Path.GetTempPath() + "KBW";
    }

    public Attendance(string licensepath, XmlDocument header, string jns, string role, string circuit, string electoralEampaignSave)
    {
      this.licensePath = licensepath;
      string[] strArray = electoralEampaignSave.Split('_');
      this.password = "";
      this.logged = false;
      this.InitializeComponent();
      this.errorValue.Visible = false;
      this.errorHour.Visible = false;
      this.errorCurrentLwyb.Visible = false;
      this.errorOBW.Visible = false;
      this.okreg.Text = strArray[strArray.Length - 1];
      this.jns.Text = jns;
      this.role = role;
      if (this.role == "P" && circuit != null)
      {
        this.obwod.Text = circuit;
        this.obwodList.Visible = false;
      }
      else
        this.obwodList.Visible = true;
      this.path = Path.GetTempPath() + "KBW";
      this.header = header;
      this.setComboBoxObwod(header);
      this.setComboBoxHour(electoralEampaignSave);
      if ((this.obwodList.DataSource as ArrayList).Count > 1)
        return;
      int num = (int) MessageBox.Show("Nie ma obwodu, dla którego można by wysłać frekwencję.", "Komunikat");
      this.Close();
    }

    private void send_Click(object sender, EventArgs e)
    {
      this.obwodList_Validated((object) this.obwodList, e);
      this.currentLwyb_Validated((object) this.currentLwyb, e);
      this.attendanceHour_Validated((object) this.attendanceHour, e);
      this.number_Validated((object) this.attendanceValue, e);
      if ((this.errorValue.Visible || this.errorHour.Visible || (this.errorOBW.Visible || this.errorCurrentLwyb.Visible)) && (this.errorValue.Visible || this.errorHour.Visible || (this.errorOBW.Visible || !this.errorCurrentLwyb.Visible) || !(this.errorCurrentLwyb.Text == "Liczba wyborców uprawnionych do głosowania jest mniejsza od 110% i większa od 90% szacowanej liczby wyborców (" + (this.obwodList.SelectedItem as AttendanceOBWItem).getLwyb().ToString() + ").")))
        return;
      string name = "Wait_04";
      Size size = this.Size;
      int width = size.Width;
      size = this.Size;
      int height = size.Height;
      WaitPanel waitPanel = new WaitPanel(name, width, height);
      waitPanel.setWaitPanel("Trwa importowanie danych", "Proszę czekać");
      waitPanel.setSize(this.FormPanel.Size);
      waitPanel.setLocation(this.FormPanel.Location);
      this.Controls.Add((Control) waitPanel.getPanel());
      this.Controls[waitPanel.getName()].BringToFront();
      waitPanel.setVisible(true);
      string str1 = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" + "<save>";
      string str2 = "";
      Exception exception;
      try
      {
        if (this.attendanceHour.SelectedItem != null)
          str2 = (this.attendanceHour.SelectedItem as AttendanceItem).getName();
      }
      catch (Exception ex)
      {
        exception = ex;
      }
      string str3 = "";
      try
      {
        if (this.attendanceValue.Text != null)
          str3 = this.attendanceValue.Text;
      }
      catch (Exception ex)
      {
        exception = ex;
      }
      string XMLFile = str1 + "<attendance><attendanceHour>" + str2 + "</attendanceHour><attendanceValue>" + str3 + "</attendanceValue><currentLwyb>" + this.currentLwyb.Text + "</currentLwyb></attendance>" + "<header>" + this.getHeader(this.header) + "</header>" + "</save>";
      int num1 = (int) new Commit(this.licensePath, this).ShowDialog();
      if (this.logged)
      {
        try
        {
          new Certificate().SignXmlText(XMLFile, Path.GetTempPath() + "KBW\\tmp\\attendance.xml", this.password, this.licensePath);
          StreamReader streamReader = new StreamReader(Path.GetTempPath() + "KBW\\tmp\\attendance.xml");
          string str4 = streamReader.ReadToEnd();
          streamReader.Close();
          int num2 = (int) MessageBox.Show(new Connection().postReq("attendances/readval/" + HttpUtility.UrlEncode((this.attendanceHour.SelectedItem as AttendanceItem).getName()), "xml=" + HttpUtility.UrlEncode(str4), 0).getText(), "Komunikat");
        }
        catch (Exception ex)
        {
          int num2 = (int) MessageBox.Show("Błąd utworzenia wiadomości: " + ex.Message, "Uwaga");
        }
      }
      waitPanel.setVisible(false);
    }

    private string getHeader(XmlDocument header)
    {
      string str1 = "" + "<jns_kod>" + this.jns.Text + "</jns_kod>";
      string str2;
      if (this.role == "P")
      {
        str2 = str1 + "<nrObwodu>" + this.obwod.Text + "</nrObwodu>";
      }
      else
      {
        string str3 = "";
        if (this.obwodList.SelectedItem != null)
          str3 = ((object) (this.obwodList.SelectedItem as AttendanceOBWItem).getName()).ToString();
        str2 = str1 + "<nrObwodu>" + str3 + "</nrObwodu>";
      }
      XmlNode xmlNode = header.SelectSingleNode("/akcja_wyborcza/jns");
      XmlNode namedItem1 = xmlNode.Attributes.GetNamedItem("nameGmina");
      string str4 = namedItem1 == null ? str2 + "<nameGmina></nameGmina>" : str2 + "<nameGmina>" + namedItem1.Value + "</nameGmina>";
      XmlNode namedItem2 = xmlNode.Attributes.GetNamedItem("namePowiat");
      string str5 = namedItem2 == null ? str4 + "<namePowiat></namePowiat>" : str4 + "<namePowiat>" + namedItem2.Value + "</namePowiat>";
      XmlNode namedItem3 = xmlNode.Attributes.GetNamedItem("nameWojewództwo");
      return (namedItem3 == null ? str5 + "<nameWojewództwo></nameWojewództwo>" : str5 + "<nameWojewództwo>" + namedItem3.Value + "</nameWojewództwo>") + "<KalVersion>" + ((object) Kalkulator1.instalClass.Version.getVersion()).ToString() + "</KalVersion>" + "<system>" + ((object) Environment.OSVersion.VersionString).ToString() + "</system>";
    }

    private void setComboBoxObwod(XmlDocument header)
    {
      XmlNode xmlNode1 = header.SelectSingleNode("/akcja_wyborcza/jns");
      try
      {
        ArrayList arrayList = new ArrayList();
        arrayList.Add((object) new AttendanceOBWItem(0, "", 0L));
        foreach (XmlNode xmlNode2 in xmlNode1)
        {
          XmlNode namedItem1 = xmlNode2.Attributes.GetNamedItem("nr");
          XmlNode namedItem2 = xmlNode2.Attributes.GetNamedItem("typ_obwodu");
          if (namedItem2 != null && namedItem2.Value == "P")
          {
            foreach (XmlNode xmlNode3 in xmlNode2)
            {
              XmlNode namedItem3 = xmlNode3.Attributes.GetNamedItem("kod");
              if (namedItem3 != null && namedItem3.Value == "WBP")
              {
                XmlNode namedItem4 = xmlNode3.FirstChild.Attributes.GetNamedItem("lwyb");
                int id = 1;
                if (this.role == "P")
                {
                  if (namedItem1 != null && namedItem1.Value == this.obwod.Text && namedItem4 != null)
                  {
                    arrayList.Add((object) new AttendanceOBWItem(id, namedItem1.Value, Convert.ToInt64(namedItem4.Value)));
                    break;
                  }
                }
                else if (namedItem1 != null && namedItem4 != null)
                {
                  arrayList.Add((object) new AttendanceOBWItem(id, namedItem1.Value, Convert.ToInt64(namedItem4.Value)));
                  int num = id + 1;
                }
              }
            }
          }
        }
        this.obwodList.DataSource = (object) arrayList;
        this.obwodList.DisplayMember = "LongName";
        this.obwodList.ValueMember = "ShortName";
        if (arrayList.Count != 2)
          return;
        this.obwodList.SelectedIndex = 1;
        this.klkLwyb.Text = "(" + (this.obwodList.SelectedItem as AttendanceOBWItem).getLwyb().ToString() + ")";
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Nieprawidłowy XML.", "Błąd");
      }
    }

    private void setComboBoxHour(string electoralEampaignSave)
    {
      if (!Directory.Exists(this.path + "\\Attendance"))
      {
        try
        {
          Directory.CreateDirectory(this.path + "\\Attendance");
        }
        catch (ArgumentNullException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Attendance\"", "Error");
        }
        catch (ArgumentException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Attendance\"", "Error");
        }
        catch (UnauthorizedAccessException ex)
        {
          int num = (int) MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
        }
        catch (PathTooLongException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Attendance\"", "Error");
        }
        catch (DirectoryNotFoundException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Attendance\"", "Error");
        }
        catch (NotSupportedException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"Attendance\"", "Error");
        }
        catch (IOException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Attendance\"", "Error");
        }
      }
      new Connection().getRequestKBWKlk("KALK/freq/" + electoralEampaignSave.Replace('_', '/') + "-freq", this.path + "\\Attendance\\frekwencja.xml", 0);
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(this.path + "\\Attendance\\frekwencja.xml");
      XmlNode xmlNode1 = xmlDocument.SelectSingleNode("/frekwencja");
      ArrayList arrayList = new ArrayList();
      arrayList.Add((object) new AttendanceItem("0", ""));
      foreach (XmlNode xmlNode2 in xmlNode1)
      {
        XmlNode namedItem1 = xmlNode2.Attributes.GetNamedItem("id");
        XmlNode namedItem2 = xmlNode2.Attributes.GetNamedItem("value");
        if (namedItem1 != null && namedItem2 != null)
          arrayList.Add((object) new AttendanceItem(namedItem1.Value, namedItem2.Value));
      }
      this.attendanceHour.DataSource = (object) arrayList;
      this.attendanceHour.DisplayMember = "LongName";
      this.attendanceHour.ValueMember = "ShortName";
    }

    private void number_Validated(object sender, EventArgs e)
    {
      string pattern = "^[0-9]+$";
      if ((sender as TextBox).Text != "")
      {
        if (!Regex.IsMatch((sender as TextBox).Text, pattern))
        {
          (sender as TextBox).ForeColor = Color.Red;
          (sender as TextBox).BackColor = SystemColors.Info;
          this.errorValue.Text = "Dozwolone liczby dodatnie";
          this.errorValue.Visible = true;
        }
        else if (this.currentLwyb.Text != "" && !this.errorCurrentLwyb.Visible || this.errorCurrentLwyb.Visible && this.errorCurrentLwyb.Text == "Liczba wyborców uprawnionych do głosowania jest mniejsza od 110% i większa od 90% szacowanej liczby wyborców (" + (this.obwodList.SelectedItem as AttendanceOBWItem).getLwyb().ToString() + ").")
        {
          if (Convert.ToInt32((sender as TextBox).Text) <= Convert.ToInt32(this.currentLwyb.Text))
          {
            (sender as TextBox).ForeColor = Color.Black;
            (sender as TextBox).BackColor = SystemColors.Window;
            this.errorValue.Visible = false;
            (sender as TextBox).Text = Convert.ToInt32((sender as TextBox).Text).ToString();
          }
          else
          {
            (sender as TextBox).ForeColor = Color.Red;
            (sender as TextBox).BackColor = SystemColors.Info;
            this.errorValue.Text = "Liczba wydanych kart nie może być większa od liczby wyborców uprawnionych do głosowania";
            this.errorValue.Visible = true;
          }
        }
        else
        {
          this.currentLwyb.ForeColor = Color.Red;
          this.currentLwyb.BackColor = SystemColors.Info;
          if (this.currentLwyb.Text == "")
          {
            this.errorCurrentLwyb.Text = "Pole jest wymagane";
            this.errorCurrentLwyb.ForeColor = Color.Red;
          }
          this.errorCurrentLwyb.Visible = true;
          this.currentLwyb.Text = "";
          (sender as TextBox).ForeColor = Color.Red;
          (sender as TextBox).BackColor = SystemColors.Info;
          this.errorValue.Text = "Do sprawdzenia tego pola wymagane jest poprawne wypełnienie pola liczby wyborców";
          this.errorValue.Visible = true;
        }
      }
      else
      {
        (sender as TextBox).ForeColor = Color.Red;
        (sender as TextBox).BackColor = SystemColors.Info;
        this.errorValue.Text = "Pole jest wymagane";
        this.errorValue.Visible = true;
        (sender as TextBox).Text = "";
      }
    }

    private void attendanceHour_Validated(object sender, EventArgs e)
    {
      if ((sender as ComboBox).SelectedIndex == 0)
      {
        (sender as ComboBox).ForeColor = Color.Red;
        (sender as ComboBox).BackColor = SystemColors.Info;
        this.errorHour.Visible = true;
      }
      else
      {
        (sender as ComboBox).ForeColor = Color.Black;
        (sender as ComboBox).BackColor = SystemColors.Window;
        this.errorHour.Visible = false;
      }
    }

    private void currentLwyb_Validated(object sender, EventArgs e)
    {
      string pattern = "^[0-9]+$";
      if ((sender as TextBox).Text != "")
      {
        if (!Regex.IsMatch((sender as TextBox).Text, pattern))
        {
          (sender as TextBox).ForeColor = Color.Red;
          (sender as TextBox).BackColor = SystemColors.Info;
          this.errorCurrentLwyb.ForeColor = Color.Red;
          this.errorCurrentLwyb.Text = "Dozwolone liczby dodatnie";
          this.errorCurrentLwyb.Visible = true;
        }
        else if (this.obwodList.SelectedIndex != 0)
        {
          this.errorOBW.Visible = false;
          object selectedItem = this.obwodList.SelectedItem;
          Decimal num = Convert.ToDecimal((sender as TextBox).Text) / Convert.ToDecimal((selectedItem as AttendanceOBWItem).getLwyb()) * new Decimal(100);
          if (num >= new Decimal(90) && num <= new Decimal(110))
          {
            (sender as TextBox).ForeColor = Color.Black;
            this.errorCurrentLwyb.ForeColor = Color.Black;
            (sender as TextBox).BackColor = SystemColors.Window;
            this.errorCurrentLwyb.Visible = false;
            (sender as TextBox).Text = Convert.ToInt32((sender as TextBox).Text).ToString();
            if (this.errorValue.Visible && this.errorValue.Text == "Do sprawdzenia tego pola wymagane jest poprawne wypełnienie pola liczby wyborców")
              this.number_Validated((object) this.attendanceValue, e);
          }
          else
          {
            (sender as TextBox).ForeColor = Color.DodgerBlue;
            this.errorCurrentLwyb.ForeColor = Color.DodgerBlue;
            (sender as TextBox).BackColor = SystemColors.Info;
            this.errorCurrentLwyb.Text = "Liczba wyborców uprawnionych do głosowania jest mniejsza od 110% i większa od 90% szacowanej liczby wyborców (" + (selectedItem as AttendanceOBWItem).getLwyb().ToString() + ").";
            this.errorCurrentLwyb.Visible = true;
          }
        }
        else
        {
          this.obwodList.ForeColor = Color.Red;
          this.obwodList.BackColor = SystemColors.Info;
          this.errorCurrentLwyb.ForeColor = Color.Red;
          this.errorOBW.Visible = true;
          (sender as TextBox).ForeColor = Color.Red;
          (sender as TextBox).BackColor = SystemColors.Info;
          this.errorCurrentLwyb.ForeColor = Color.Red;
          this.errorCurrentLwyb.Text = "Do sprawdzenia tego pola wymagane jest wybranie obwodu";
          this.errorCurrentLwyb.Visible = true;
        }
      }
      else
      {
        (sender as TextBox).ForeColor = Color.Red;
        this.errorCurrentLwyb.ForeColor = Color.Red;
        (sender as TextBox).BackColor = SystemColors.Info;
        this.errorCurrentLwyb.Text = "Pole jest wymagane";
        this.errorCurrentLwyb.Visible = true;
        (sender as TextBox).Text = "";
      }
    }

    private void obwodList_Validated(object sender, EventArgs e)
    {
      int selectedIndex = (sender as ComboBox).SelectedIndex;
      if ((sender as ComboBox).SelectedIndex == 0)
      {
        (sender as ComboBox).ForeColor = Color.Red;
        (sender as ComboBox).BackColor = SystemColors.Info;
        this.errorOBW.Visible = true;
      }
      else
      {
        (sender as ComboBox).ForeColor = Color.Black;
        (sender as ComboBox).BackColor = SystemColors.Window;
        this.errorOBW.Visible = false;
        this.klkLwyb.Text = "(" + (this.obwodList.SelectedItem as AttendanceOBWItem).getLwyb().ToString() + ")";
        if (this.errorCurrentLwyb.Visible && this.errorCurrentLwyb.Text == "Do sprawdzenia tego pola wymagane jest wybranie obwodu")
          this.currentLwyb_Validated((object) this.currentLwyb, e);
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
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
      this.SuspendLayout();
      this.FormPanel.Controls.Add((Control) this.klkLwyb);
      this.FormPanel.Controls.Add((Control) this.errorCurrentLwyb);
      this.FormPanel.Controls.Add((Control) this.errorOBW);
      this.FormPanel.Controls.Add((Control) this.currentLwyb);
      this.FormPanel.Controls.Add((Control) this.obwodList);
      this.FormPanel.Controls.Add((Control) this.label7);
      this.FormPanel.Controls.Add((Control) this.obwod);
      this.FormPanel.Controls.Add((Control) this.errorHour);
      this.FormPanel.Controls.Add((Control) this.errorValue);
      this.FormPanel.Controls.Add((Control) this.label6);
      this.FormPanel.Controls.Add((Control) this.jns);
      this.FormPanel.Controls.Add((Control) this.label5);
      this.FormPanel.Controls.Add((Control) this.okreg);
      this.FormPanel.Controls.Add((Control) this.label3);
      this.FormPanel.Controls.Add((Control) this.send);
      this.FormPanel.Controls.Add((Control) this.attendanceHour);
      this.FormPanel.Controls.Add((Control) this.attendanceValue);
      this.FormPanel.Controls.Add((Control) this.label2);
      this.FormPanel.Controls.Add((Control) this.label1);
      this.FormPanel.Location = new Point(13, 13);
      this.FormPanel.Name = "FormPanel";
      this.FormPanel.Size = new Size(515, 328);
      this.FormPanel.TabIndex = 0;
      this.klkLwyb.AutoSize = true;
      this.klkLwyb.Location = new Point(22, 153);
      this.klkLwyb.MaximumSize = new Size(495, 0);
      this.klkLwyb.Name = "klkLwyb";
      this.klkLwyb.Size = new Size(0, 13);
      this.klkLwyb.TabIndex = 20;
      this.errorCurrentLwyb.AutoSize = true;
      this.errorCurrentLwyb.ForeColor = Color.DodgerBlue;
      this.errorCurrentLwyb.Location = new Point(146, 160);
      this.errorCurrentLwyb.MaximumSize = new Size(350, 0);
      this.errorCurrentLwyb.Name = "errorCurrentLwyb";
      this.errorCurrentLwyb.Size = new Size(136, 13);
      this.errorCurrentLwyb.TabIndex = 19;
      this.errorCurrentLwyb.Text = "Dozwolone liczby dodatnie.";
      this.errorOBW.AutoSize = true;
      this.errorOBW.ForeColor = Color.Red;
      this.errorOBW.Location = new Point(365, 41);
      this.errorOBW.Name = "errorOBW";
      this.errorOBW.Size = new Size(80, 13);
      this.errorOBW.TabIndex = 18;
      this.errorOBW.Text = "Wybierz obwód";
      this.currentLwyb.Location = new Point(148, 137);
      this.currentLwyb.Name = "currentLwyb";
      this.currentLwyb.Size = new Size(341, 20);
      this.currentLwyb.TabIndex = 3;
      this.currentLwyb.TextChanged += new EventHandler(this.currentLwyb_Validated);
      this.currentLwyb.Validated += new EventHandler(this.currentLwyb_Validated);
      this.obwodList.FormattingEnabled = true;
      this.obwodList.Location = new Point(368, 17);
      this.obwodList.Name = "obwodList";
      this.obwodList.Size = new Size(121, 21);
      this.obwodList.TabIndex = 1;
      this.obwodList.SelectionChangeCommitted += new EventHandler(this.obwodList_Validated);
      this.obwodList.Validated += new EventHandler(this.obwodList_Validated);
      this.label7.AutoSize = true;
      this.label7.Location = new Point(22, 140);
      this.label7.MaximumSize = new Size(495, 0);
      this.label7.Name = "label7";
      this.label7.Size = new Size(110, 13);
      this.label7.TabIndex = 14;
      this.label7.Text = "Liczba uprawnionych:";
      this.obwod.AutoSize = true;
      this.obwod.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.obwod.Location = new Point(370, 20);
      this.obwod.Name = "obwod";
      this.obwod.Size = new Size(23, 13);
      this.obwod.TabIndex = 13;
      this.obwod.Text = "XX";
      this.errorHour.AutoSize = true;
      this.errorHour.ForeColor = Color.Red;
      this.errorHour.Location = new Point(146, 97);
      this.errorHour.MaximumSize = new Size(350, 0);
      this.errorHour.Name = "errorHour";
      this.errorHour.Size = new Size(136, 13);
      this.errorHour.TabIndex = 12;
      this.errorHour.Text = "Wybierz godzinę frekwencji";
      this.errorValue.AutoSize = true;
      this.errorValue.ForeColor = Color.Red;
      this.errorValue.Location = new Point(145, 224);
      this.errorValue.MaximumSize = new Size(341, 0);
      this.errorValue.Name = "errorValue";
      this.errorValue.Size = new Size(136, 13);
      this.errorValue.TabIndex = 11;
      this.errorValue.Text = "Dozwolone liczby dodatnie.";
      this.label6.AutoSize = true;
      this.label6.Location = new Point(319, 20);
      this.label6.Name = "label6";
      this.label6.Size = new Size(44, 13);
      this.label6.TabIndex = 9;
      this.label6.Text = "Obwód:";
      this.jns.AutoSize = true;
      this.jns.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.jns.Location = new Point(190, 20);
      this.jns.Name = "jns";
      this.jns.Size = new Size(55, 13);
      this.jns.TabIndex = 8;
      this.jns.Text = "XXXXXX";
      this.label5.AutoSize = true;
      this.label5.Location = new Point(145, 20);
      this.label5.Name = "label5";
      this.label5.Size = new Size(40, 13);
      this.label5.TabIndex = 7;
      this.label5.Text = "Gmina:";
      this.okreg.AutoSize = true;
      this.okreg.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.okreg.Location = new Point(67, 20);
      this.okreg.Name = "okreg";
      this.okreg.Size = new Size(23, 13);
      this.okreg.TabIndex = 6;
      this.okreg.Text = "XX";
      this.label3.AutoSize = true;
      this.label3.Location = new Point(22, 20);
      this.label3.Name = "label3";
      this.label3.Size = new Size(37, 13);
      this.label3.TabIndex = 5;
      this.label3.Text = "Akcja:";
      this.send.Location = new Point(317, 289);
      this.send.Name = "send";
      this.send.Size = new Size(172, 23);
      this.send.TabIndex = 5;
      this.send.Text = "Wyślij";
      this.send.UseVisualStyleBackColor = true;
      this.send.Click += new EventHandler(this.send_Click);
      this.attendanceHour.FormattingEnabled = true;
      this.attendanceHour.Location = new Point(148, 73);
      this.attendanceHour.Name = "attendanceHour";
      this.attendanceHour.Size = new Size(341, 21);
      this.attendanceHour.TabIndex = 2;
      this.attendanceHour.SelectionChangeCommitted += new EventHandler(this.attendanceHour_Validated);
      this.attendanceHour.Validated += new EventHandler(this.attendanceHour_Validated);
      this.attendanceValue.Location = new Point(148, 201);
      this.attendanceValue.Name = "attendanceValue";
      this.attendanceValue.Size = new Size(341, 20);
      this.attendanceValue.TabIndex = 4;
      this.attendanceValue.TextChanged += new EventHandler(this.number_Validated);
      this.attendanceValue.Validated += new EventHandler(this.number_Validated);
      this.label2.AutoSize = true;
      this.label2.Location = new Point(22, 204);
      this.label2.Name = "label2";
      this.label2.Size = new Size(113, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "Liczba wydanych kart:";
      this.label1.AutoSize = true;
      this.label1.Location = new Point(22, 76);
      this.label1.Name = "label1";
      this.label1.Size = new Size(49, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Godzina:";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(541, 356);
      this.Controls.Add((Control) this.FormPanel);
      this.Name = "Attendance";
      this.Text = "Frekwencja";
      this.FormPanel.ResumeLayout(false);
      this.FormPanel.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
