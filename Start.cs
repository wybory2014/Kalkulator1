// Decompiled with JetBrains decompiler
// Type: Kalkulator1.Start
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

using Kalkulator1.AdditionalClass;
using Kalkulator1.instalClass;
using Kalkulator1.ResponseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;

namespace Kalkulator1
{
  public class Start : Form
  {
    public static List<string> listaPlikow = new List<string>();
    private IContainer components = (IContainer) null;
    private string path;
    private Certificate certificate;
    public bool logged;
    private Connection con;
    public string jns;
    public WaitPanel wait;
    private Label label1;
    private Label label2;
    private Panel licensesPanel;
    private Panel waitLicensesPanel;
    private Panel panel3;
    private Button getLicenseFromNet;
    private Label label4;
    private Button getLicenseFromDisk;
    private Label label3;
    private DataGridView LicencesTable;
    private Panel panel1;
    private DataGridView waitLicenses;
    private Button btnDeleteTemp;

    public Start()
    {
      try
      {
        this.InitializeComponent();
        this.Text = "Kalkulator (" + ((object) Kalkulator1.instalClass.Version.getVersion()).ToString() + ")";
        Start.listaPlikow.Add("START");
        this.path = Path.GetTempPath() + "KBW";
        if (!Directory.Exists(this.path))
        {
          try
          {
            Directory.CreateDirectory(this.path);
          }
          catch (ArgumentNullException ex)
          {
            int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"KBW\"", "Error");
          }
          catch (ArgumentException ex)
          {
            int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"KBW\"", "Error");
          }
          catch (UnauthorizedAccessException ex)
          {
            int num = (int) MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
          }
          catch (PathTooLongException ex)
          {
            int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"KBW\"", "Error");
          }
          catch (DirectoryNotFoundException ex)
          {
            int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"KBW\"", "Error");
          }
          catch (NotSupportedException ex)
          {
            int num = (int) MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"KBW\"", "Error");
          }
          catch (IOException ex)
          {
            int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"KBW\"", "Error");
          }
        }
        if (!Directory.Exists(this.path + "\\tmp"))
        {
          Instal instal = new Instal(this.path);
        }
        if (Directory.Exists(Path.GetTempPath() + "KBW\\tmp"))
        {
          if (!File.Exists(Path.GetTempPath() + "KBW\\tmp\\eksport.xml"))
            File.Create(Path.GetTempPath() + "KBW\\tmp\\eksport.xml");
          if (!File.Exists(Path.GetTempPath() + "KBW\\tmp\\import.xml"))
            File.Create(Path.GetTempPath() + "KBW\\tmp\\import.xml");
          if (!File.Exists(Path.GetTempPath() + "KBW\\tmp\\eksportCode.xml"))
            File.Create(Path.GetTempPath() + "KBW\\tmp\\eksportCode.xml");
        }
        this.logged = false;
        this.certificate = new Certificate();
        this.con = new Connection();
        this.LicencesTable.CellClick += new DataGridViewCellEventHandler(this.LicencesTable_CellClick);
        this.waitLicenses.CellClick += new DataGridViewCellEventHandler(this.waitLicenses_CellClick);
        this.wait = new WaitPanel("Wait1", this.Width, this.Height);
        this.Controls.Add((Control) this.wait.getPanel());
        this.Controls[this.wait.getName()].BringToFront();
        this.getActiveLicense();
        this.getOtherLicense();
        if (Kalkulator1.instalClass.Version.newApp())
          return;
        int num1 = (int) MessageBox.Show("Twoja wersja kalkulatora wyborczego jest nieaktualna.", "Uwaga");
      }
      catch (Exception ex)
      {
      }
    }

    private void LicencesTable_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      try
      {
        if (e.RowIndex >= 0)
        {
          if (e.ColumnIndex == this.LicencesTable.Columns["action"].Index)
          {
            try
            {
              if (!Directory.Exists(this.path + "\\Licenses"))
              {
                try
                {
                  Directory.CreateDirectory(this.path + "\\Licenses");
                }
                catch (ArgumentNullException ex)
                {
                  int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
                }
                catch (ArgumentException ex)
                {
                  int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
                }
                catch (UnauthorizedAccessException ex)
                {
                  int num = (int) MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
                }
                catch (PathTooLongException ex)
                {
                  int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
                }
                catch (DirectoryNotFoundException ex)
                {
                  int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
                }
                catch (NotSupportedException ex)
                {
                  int num = (int) MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"Licenses\"", "Error");
                }
                catch (IOException ex)
                {
                  int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
                }
              }
              string str1 = this.path + "\\Licenses\\" + this.LicencesTable.Rows[e.RowIndex].Cells["Licencja"].Value.ToString();
              int num1 = (int) new Commit(str1, this).ShowDialog();
              if (this.logged)
              {
                this.logged = false;
                string[] strArray = this.readOU(str1).Split('-');
                if (strArray[2] == "P" || strArray[2] == "O" || strArray[2] == "Z")
                {
                  this.wait.setWaitPanel("Trwa przygotowanie listy protokołów", "Proszę czekać");
                  this.wait.setVisible(true);
                  int num2 = (int) new ProtocolsList(str1, this, str1, Kalkulator1.instalClass.Version.getVersion()).ShowDialog();
                }
                if (strArray[2] == "A")
                {
                  this.jns = "";
                  bool powiat = false;
                  string str2 = strArray[1];
                  if (str2.Length < 6)
                  {
                    while (str2.Length < 6)
                      str2 = "0" + str2;
                  }
                  if ((int) str2[4] == 48 && (int) str2[5] == 48)
                  {
                    powiat = true;
                    RegionList regionList = new RegionList(this, str1, strArray[1]);
                    try
                    {
                      int num2 = (int) regionList.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                    }
                  }
                  else
                    this.jns = strArray[1];
                  if (this.jns != null && this.jns != "")
                  {
                    int num3 = (int) new ProtocolsList(str1, this, str1, Kalkulator1.instalClass.Version.getVersion(), this.jns, powiat).ShowDialog();
                  }
                  this.jns = "";
                }
              }
            }
            catch (ArgumentOutOfRangeException ex)
            {
            }
            catch (Exception ex)
            {
              int num = (int) MessageBox.Show("Operacja nie powiodła się. Spróbuj jeszcze raz. Orginal error: " + ex.Message, "Uwaga");
            }
          }
          if (e.ColumnIndex == this.LicencesTable.Columns["remove"].Index)
          {
            try
            {
              if (MessageBox.Show("Czy napewno usunąć licencje " + this.LicencesTable.Rows[e.RowIndex].Cells["Licencja"].Value.ToString() + "?", "Usuwanie", MessageBoxButtons.YesNo) == DialogResult.Yes)
              {
                try
                {
                  File.Delete(this.path + "\\Licenses\\" + this.LicencesTable.Rows[e.RowIndex].Cells["Licencja"].Value.ToString());
                  File.Delete(this.path + "\\Licenses\\" + this.LicencesTable.Rows[e.RowIndex].Cells["Licencja"].Value.ToString().Replace(".crt", ".key"));
                  this.LicencesTable.Rows.Remove(this.LicencesTable.Rows[e.RowIndex]);
                }
                catch (ArgumentNullException ex)
                {
                  int num = (int) MessageBox.Show("Błędna scieżka do licencji", "Error");
                }
                catch (ArgumentException ex)
                {
                  int num = (int) MessageBox.Show("Błędna scieżka do licencji", "Error");
                }
                catch (DirectoryNotFoundException ex)
                {
                  int num = (int) MessageBox.Show("Błąd znalezienia pliku licencji", "Error");
                }
                catch (PathTooLongException ex)
                {
                  int num = (int) MessageBox.Show("Zbyt długa scieżka do licencji", "Error");
                }
                catch (IOException ex)
                {
                  int num = (int) MessageBox.Show("Nie można usunąć licencji. Jeden z jej plików jest obecnie używany.", "Error");
                }
                catch (NotSupportedException ex)
                {
                  int num = (int) MessageBox.Show("Błędna scieżka do licencji", "Error");
                }
                catch (UnauthorizedAccessException ex)
                {
                  int num = (int) MessageBox.Show("Program nie posiada uprawnień do usuwania plików", "Error");
                }
              }
              this.getActiveLicense();
              this.getOtherLicense();
            }
            catch (ArgumentOutOfRangeException ex)
            {
            }
          }
          else if (e.ColumnIndex == this.LicencesTable.Columns["save"].Index)
          {
            try
            {
              StreamReader streamReader = new StreamReader(this.path + "\\Licenses\\" + this.LicencesTable.Rows[e.RowIndex].Cells["Licencja"].Value.ToString());
              string str = streamReader.ReadToEnd();
              streamReader.Close();
              SaveFileDialog saveFileDialog = new SaveFileDialog();
              saveFileDialog.Title = "Zapisywanie licencji";
              saveFileDialog.Filter = "(*.pem)|*.pem";
              saveFileDialog.FileName = this.LicencesTable.Rows[e.RowIndex].Cells["Licencja"].Value.ToString();
              int num1 = (int) saveFileDialog.ShowDialog();
              this.wait.setWaitPanel("Trwa zapisywanie licencji", "Proszę czekać");
              this.wait.setVisible(true);
              string fileName = saveFileDialog.FileName;
              try
              {
                if (fileName != "")
                {
                  StreamWriter streamWriter = new StreamWriter(fileName, false);
                  streamWriter.Write(str);
                  streamWriter.Close();
                }
                this.wait.setVisible(false);
              }
              catch (UnauthorizedAccessException ex)
              {
                int num2 = (int) MessageBox.Show("Nie można zapisać pliku. Otwórz aplikacje jako administracje", "Error");
              }
              catch (Exception ex)
              {
                int num2 = (int) MessageBox.Show("Nie można zapisać pliku. Orginal exception: " + ex.Message, "Error");
              }
            }
            catch (ArgumentOutOfRangeException ex)
            {
            }
          }
        }
      }
      catch (Exception ex)
      {
      }
    }

    private void waitLicenses_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      try
      {
        if (e.RowIndex >= 0)
        {
          if (e.ColumnIndex == this.waitLicenses.Columns["action"].Index)
          {
            this.wait.setWaitPanel("Trwa sprawdzanie licencji", "Proszę czekać");
            this.wait.setVisible(true);
            try
            {
              string str1 = this.waitLicenses.Rows[e.RowIndex].Cells["Licencja"].Value.ToString();
              try
              {
                Certificate certificate = new Certificate();
                if (Directory.Exists(this.path + "\\Licenses"))
                {
                  StreamReader streamReader1 = new StreamReader(this.path + "\\Licenses\\" + str1);
                  string str2 = streamReader1.ReadToEnd();
                  streamReader1.Close();
                  IsLicense isLicense = this.con.postCheckLicense("certificates/crtget", "csr=" + HttpUtility.UrlEncode(str2), 0);
                  string str3 = "";
                  string str4;
                  if (isLicense.getCode().getcode() == 12)
                  {
                    if (!Directory.Exists(this.path + "\\Licenses\\Anulowane"))
                    {
                      try
                      {
                        Directory.CreateDirectory(this.path + "\\Licenses\\Anulowane");
                      }
                      catch (ArgumentNullException ex)
                      {
                        int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Anulowane\"", "Error");
                      }
                      catch (ArgumentException ex)
                      {
                        int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Anulowane\"", "Error");
                      }
                      catch (UnauthorizedAccessException ex)
                      {
                        int num = (int) MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
                      }
                      catch (PathTooLongException ex)
                      {
                        int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Anulowane\"", "Error");
                      }
                      catch (DirectoryNotFoundException ex)
                      {
                        int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Anulowane\"", "Error");
                      }
                      catch (NotSupportedException ex)
                      {
                        int num = (int) MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"Anulowane\"", "Error");
                      }
                      catch (IOException ex)
                      {
                        int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Anulowane\"", "Error");
                      }
                    }
                    if (Directory.Exists(this.path + "\\Licenses\\Anulowane"))
                    {
                      try
                      {
                        File.Move(this.path + "\\Licenses\\" + str1, this.path + "\\Licenses\\Anulowane\\" + str1);
                        File.Move(this.path + "\\Licenses\\" + str1.Replace(".csr", ".key"), this.path + "\\Licenses\\Anulowane\\" + str1.Replace(".csr", ".key"));
                      }
                      catch (ArgumentNullException ex)
                      {
                        int num = (int) MessageBox.Show("Sprawdzana licencja nie istnieje", "Uwaga");
                      }
                      catch (ArgumentException ex)
                      {
                        int num = (int) MessageBox.Show("Sprawdzana licencja nie istnieje", "Uwaga");
                      }
                      catch (UnauthorizedAccessException ex)
                      {
                        int num = (int) MessageBox.Show("Nie masz uprawnień do przeniesiena licenji do anulowanych. Otwórz aplikacje jako adnimistrator.", "Uwaga");
                      }
                      catch (PathTooLongException ex)
                      {
                        int num = (int) MessageBox.Show("Nieprawidłowa ścieżka do licencji", "Uwaga");
                      }
                      catch (DirectoryNotFoundException ex)
                      {
                        int num = (int) MessageBox.Show("Nieprawidłowa ścieżka do licencji", "Uwaga");
                      }
                      catch (NotSupportedException ex)
                      {
                        int num = (int) MessageBox.Show("Nieprawidłowy format ścieżki do licencji", "Uwaga");
                      }
                      catch (IOException ex1)
                      {
                        try
                        {
                          str4 = "";
                          string[] strArray = str1.Replace(".csr", "").Split(' ');
                          int num = Directory.GetFiles(this.path + "\\Licenses\\Anulowane\\", strArray[0] + "*.csr").Length + 1;
                          string str5 = num <= 1 ? strArray[0] + ".csr" : strArray[0] + " " + num.ToString() + ".csr";
                          File.Move(this.path + "\\Licenses\\" + str1, this.path + "\\Licenses\\Anulowane\\" + str5);
                          File.Move(this.path + "\\Licenses\\" + str1.Replace(".csr", ".key"), this.path + "\\Licenses\\Anulowane\\" + str5.Replace(".csr", ".key"));
                        }
                        catch (ArgumentNullException ex2)
                        {
                          int num = (int) MessageBox.Show("Sprawdzana licencja nie istnieje", "Uwaga");
                        }
                        catch (ArgumentException ex2)
                        {
                          int num = (int) MessageBox.Show("Sprawdzana licencja nie istnieje", "Uwaga");
                        }
                        catch (UnauthorizedAccessException ex2)
                        {
                          int num = (int) MessageBox.Show("Nie masz uprawnień do przeniesiena licenji do anulowanych.", "Uwaga");
                        }
                        catch (PathTooLongException ex2)
                        {
                          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka do licencji", "Uwaga");
                        }
                        catch (DirectoryNotFoundException ex2)
                        {
                          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka do licencji", "Uwaga");
                        }
                        catch (NotSupportedException ex2)
                        {
                          int num = (int) MessageBox.Show("Nieprawidłowy format ścieżki do licencji", "Uwaga");
                        }
                        catch (IOException ex2)
                        {
                          int num = (int) MessageBox.Show("Nie można odnaleźć licencji", "Uwaga");
                        }
                      }
                      this.getOtherLicense();
                    }
                  }
                  if (isLicense.getCode().getcode() == 0 && isLicense.getCrt() != "")
                    str3 = isLicense.getCrt();
                  if (str3 != "")
                  {
                    if (!Directory.Exists(this.path + "\\Licenses"))
                    {
                      try
                      {
                        Directory.CreateDirectory(this.path + "\\Licenses");
                      }
                      catch (ArgumentNullException ex)
                      {
                        int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
                      }
                      catch (ArgumentException ex)
                      {
                        int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
                      }
                      catch (UnauthorizedAccessException ex)
                      {
                        int num = (int) MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
                      }
                      catch (PathTooLongException ex)
                      {
                        int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
                      }
                      catch (DirectoryNotFoundException ex)
                      {
                        int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
                      }
                      catch (NotSupportedException ex)
                      {
                        int num = (int) MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"Licenses\"", "Error");
                      }
                      catch (IOException ex)
                      {
                        int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
                      }
                    }
                    if (Directory.Exists(this.path + "\\Licenses"))
                    {
                      try
                      {
                        StreamReader streamReader2 = new StreamReader(this.path + "\\Licenses\\" + str1.Replace(".csr", ".key"));
                        string str5 = streamReader2.ReadToEnd();
                        streamReader2.Close();
                        str4 = "";
                        string[] strArray = str1.Replace(".csr", "").Split(' ');
                        int num1 = Directory.GetFiles(this.path + "\\Licenses\\", strArray[0] + "*.pem").Length + 1;
                        string str6 = num1 <= 1 ? strArray[0] + ".pem" : strArray[0] + " " + num1.ToString() + ".pem";
                        string str7 = str3 + '\n'.ToString() + str5;
                        StreamWriter streamWriter = new StreamWriter(this.path + "\\Licenses\\" + str6, false);
                        streamWriter.Write(str7);
                        streamWriter.Close();
                        try
                        {
                          X509Certificate x509Certificate = new X509Certificate(this.path + "\\Licenses\\" + str6);
                          File.Delete(this.path + "\\Licenses\\" + str1);
                          File.Delete(this.path + "\\Licenses\\" + str1.Replace(".csr", ".key"));
                        }
                        catch (CryptographicException ex)
                        {
                          int num2 = (int) MessageBox.Show("Przesłany certyfikat jest nie prawidłowy", "Error");
                        }
                        catch (ArgumentException ex)
                        {
                        }
                      }
                      catch (ArgumentNullException ex)
                      {
                        int num = (int) MessageBox.Show("Nie można zapisać licencji. Nieprawidłowa ścieżka ", "Error");
                      }
                      catch (ArgumentException ex)
                      {
                        int num = (int) MessageBox.Show("Nie można zapisać licencji. Nieprawidłowa ścieżka ", "Error");
                      }
                      catch (UnauthorizedAccessException ex)
                      {
                        int num = (int) MessageBox.Show("Nie masz uprawnień zapisania licencji. Otwórz aplikacje jako adnimistrator.", "Uwaga");
                      }
                      catch (PathTooLongException ex)
                      {
                        int num = (int) MessageBox.Show("Nie można zapisać licencji. Nieprawidłowa ścieżka ", "Error");
                      }
                      catch (DirectoryNotFoundException ex)
                      {
                        int num = (int) MessageBox.Show("Nie można zapisać licencji. Nieprawidłowa ścieżka ", "Error");
                      }
                      catch (NotSupportedException ex)
                      {
                        int num = (int) MessageBox.Show("Nie można zapisać licencji. Nieprawidłowa ścieżka ", "Error");
                      }
                      catch (IOException ex)
                      {
                        int num = (int) MessageBox.Show("Nie można zapisać licencji. Nieprawidłowa ścieżka ", "Error");
                      }
                      catch (ObjectDisposedException ex)
                      {
                        int num = (int) MessageBox.Show("Nie można zapisać licencji (ObjectDisposedException)", "Error");
                      }
                      this.getActiveLicense();
                      this.getOtherLicense();
                    }
                  }
                  if (isLicense.getCode().getcode() != 0 && isLicense.getCode().getcode() != 11 && isLicense.getCode().getcode() != 12)
                  {
                    int num3 = (int) MessageBox.Show(isLicense.getCode().getText(), "Uwaga");
                  }
                  this.getOtherLicense();
                  this.getActiveLicense();
                }
              }
              finally
              {
              }
            }
            catch (ArgumentOutOfRangeException ex)
            {
            }
            this.wait.setVisible(false);
          }
          if (e.ColumnIndex == this.waitLicenses.Columns["remove"].Index)
          {
            try
            {
              if (MessageBox.Show("Czy napewno usunąć licencje " + this.waitLicenses.Rows[e.RowIndex].Cells["Licencja"].Value.ToString() + "?", "Usuwanie", MessageBoxButtons.YesNo) == DialogResult.Yes)
              {
                try
                {
                  if (!Directory.Exists(this.path + "\\Licenses"))
                    Directory.CreateDirectory(this.path + "\\Licenses");
                  string str;
                  if (this.waitLicenses.Rows[e.RowIndex].Cells["Status"].Value.ToString() == "Anulowana")
                  {
                    if (!Directory.Exists(this.path + "\\Licenses\\Anulowane"))
                      Directory.CreateDirectory(this.path + "\\Licenses\\Anulowane");
                    str = this.path + "\\Licenses\\Anulowane\\" + this.waitLicenses.Rows[e.RowIndex].Cells["Licencja"].Value.ToString();
                    File.Delete(this.path + "\\Licenses\\Anulowane\\" + this.waitLicenses.Rows[e.RowIndex].Cells["Licencja"].Value.ToString());
                    File.Delete(this.path + "\\Licenses\\Anulowane\\" + this.waitLicenses.Rows[e.RowIndex].Cells["Licencja"].Value.ToString().Replace(".csr", ".key"));
                  }
                  if (this.waitLicenses.Rows[e.RowIndex].Cells["Status"].Value.ToString() == "Nie aktywna")
                  {
                    str = this.path + "\\Licenses\\" + this.waitLicenses.Rows[e.RowIndex].Cells["Licencja"].Value.ToString();
                    File.Delete(this.path + "\\Licenses\\" + this.waitLicenses.Rows[e.RowIndex].Cells["Licencja"].Value.ToString());
                    File.Delete(this.path + "\\Licenses\\" + this.waitLicenses.Rows[e.RowIndex].Cells["Licencja"].Value.ToString().Replace(".crt", ".key"));
                  }
                  if (this.waitLicenses.Rows[e.RowIndex].Cells["Status"].Value.ToString() == "Oczekująca")
                  {
                    str = this.path + "\\Licenses\\" + this.waitLicenses.Rows[e.RowIndex].Cells["Licencja"].Value.ToString();
                    File.Delete(this.path + "\\Licenses\\" + this.waitLicenses.Rows[e.RowIndex].Cells["Licencja"].Value.ToString());
                    File.Delete(this.path + "\\Licenses\\" + this.waitLicenses.Rows[e.RowIndex].Cells["Licencja"].Value.ToString().Replace(".csr", ".key"));
                  }
                  this.waitLicenses.Rows.Remove(this.waitLicenses.Rows[e.RowIndex]);
                }
                catch (ArgumentNullException ex)
                {
                  int num = (int) MessageBox.Show("Błędna scieżka do licencji", "Error");
                }
                catch (ArgumentException ex)
                {
                  int num = (int) MessageBox.Show("Błędna scieżka do licencji", "Error");
                }
                catch (DirectoryNotFoundException ex)
                {
                  int num = (int) MessageBox.Show("Błąd znalezienia pliku licencji", "Error");
                }
                catch (PathTooLongException ex)
                {
                  int num = (int) MessageBox.Show("Zbyt długa scieżka do licencji", "Error");
                }
                catch (IOException ex)
                {
                  int num = (int) MessageBox.Show("Nie można usunąć licencji. Jeden z jej plików jest obecnie używany.", "Error");
                }
                catch (NotSupportedException ex)
                {
                  int num = (int) MessageBox.Show("Błędna scieżka do licencji", "Error");
                }
                catch (UnauthorizedAccessException ex)
                {
                  int num = (int) MessageBox.Show("Program nie posiada uprawnień do usuwania plików", "Error");
                }
              }
              this.getActiveLicense();
              this.getOtherLicense();
            }
            catch (ArgumentOutOfRangeException ex)
            {
            }
          }
        }
      }
      catch (Exception ex)
      {
      }
    }

    private void getLicenseFromNet_Click(object sender, EventArgs e)
    {
      int num = (int) new Login().ShowDialog();
      this.getActiveLicense();
      this.getOtherLicense();
    }

    private void getLicenseFromDisk_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      string str1 = "";
      if (openFileDialog.ShowDialog() == DialogResult.OK)
        str1 = openFileDialog.FileName;
      this.wait.setWaitPanel("Trwa wczytywanie licencji", "Proszę czekać");
      this.wait.setVisible(true);
      if (str1 != "")
      {
        try
        {
          X509Certificate x509Certificate = new X509Certificate(str1);
          try
          {
            if (DateTime.Compare(Convert.ToDateTime(x509Certificate.GetEffectiveDateString()), DateTime.Now) <= 0 && DateTime.Compare(DateTime.Now, Convert.ToDateTime(x509Certificate.GetExpirationDateString())) <= 0)
            {
              string str2 = "";
              string OU = "";
              string[] strArray1 = x509Certificate.Subject.Split(new string[1]
              {
                ", "
              }, StringSplitOptions.None);
              for (int index = 0; index < strArray1.Length; ++index)
              {
                if (Regex.IsMatch(strArray1[index], "^OU="))
                {
                  str2 = strArray1[index].Replace("OU=", "").Split('-')[2];
                  OU = strArray1[index].Replace("OU=", "");
                  break;
                }
              }
              if (str2 == "P" && !this.isExsistOU(OU) || str2 == "Z" && !this.isExsistOU(OU) || str2 == "O" || str2 == "A")
              {
                try
                {
                  if (!Directory.Exists(this.path + "\\Licenses"))
                  {
                    try
                    {
                      Directory.CreateDirectory(this.path + "\\Licenses");
                    }
                    catch (ArgumentNullException ex)
                    {
                      int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
                    }
                    catch (ArgumentException ex)
                    {
                      int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                      int num = (int) MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
                    }
                    catch (PathTooLongException ex)
                    {
                      int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
                    }
                    catch (DirectoryNotFoundException ex)
                    {
                      int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
                    }
                    catch (NotSupportedException ex)
                    {
                      int num = (int) MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"Licenses\"", "Error");
                    }
                    catch (IOException ex)
                    {
                      int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
                    }
                  }
                  StreamReader streamReader = new StreamReader(str1);
                  string str3 = streamReader.ReadToEnd();
                  streamReader.Close();
                  if (Directory.Exists(this.path + "\\Licenses"))
                  {
                    string[] strArray2 = x509Certificate.Subject.Split(new string[1]
                    {
                      ", "
                    }, StringSplitOptions.RemoveEmptyEntries);
                    string str4 = "";
                    string str5 = "";
                    int num = 0;
                    for (int index = 0; index < strArray2.Length; ++index)
                    {
                      if (Regex.IsMatch(strArray2[index], "^CN="))
                      {
                        str4 = strArray2[index].Replace("CN=", "");
                        ++num;
                      }
                      if (Regex.IsMatch(strArray2[index], "^OU="))
                      {
                        str5 = strArray2[index].Replace("OU=", "").Replace("/", "_");
                        ++num;
                      }
                      if (num >= 2)
                        break;
                    }
                    string str6 = str5 + "_" + str4;
                    int length = Directory.GetFiles(this.path + "\\Licenses", str6 + "*.pem").Length;
                    string str7 = str6;
                    if (length > 0)
                      str7 = str7 + " " + (length + 1).ToString();
                    StreamWriter streamWriter = new StreamWriter(this.path + "\\Licenses\\" + str7 + ".pem", false);
                    streamWriter.Write(str3);
                    streamWriter.Close();
                  }
                }
                catch (ArgumentNullException ex)
                {
                  int num = (int) MessageBox.Show("Nie wskazano gdzie leży licencja", "Error");
                }
                catch (ArgumentException ex)
                {
                  int num = (int) MessageBox.Show("Nie wskazano gdzie leży licencja", "Error");
                }
                catch (DirectoryNotFoundException ex)
                {
                  int num = (int) MessageBox.Show("Nie znaleziono licencja", "Error");
                }
                catch (IOException ex)
                {
                  int num = (int) MessageBox.Show("Nieprawidłowa ścieżka do licencji", "Error");
                }
                catch (UnauthorizedAccessException ex)
                {
                  int num = (int) MessageBox.Show("Nie masz uprawnień do zapisania licencji. Otwórz aplikacje jako adnimistrator.", "Uwaga");
                }
                catch (NotSupportedException ex)
                {
                  int num = (int) MessageBox.Show("Nieprawidłowa ścieżka do licencji", "Error");
                }
                catch (ObjectDisposedException ex)
                {
                  int num = (int) MessageBox.Show("Nie można zapisać licencji (ObjectDisposedException)", "Error");
                }
              }
              else
              {
                int num1 = (int) MessageBox.Show("Licencja nie została załadowana. Podana licencja przewodniczacego już istnieje.", "Komunikat");
              }
            }
            else
            {
              int num2 = (int) MessageBox.Show("Licencja nie została załadowana. Podana licencja jest nie ważna.", "Komunikat");
            }
          }
          catch (FormatException ex)
          {
            int num = (int) MessageBox.Show("Licencja nie została załadowana. Podana licencja jest nie ważna.", "Komunikat");
          }
        }
        catch (CryptographicException ex)
        {
          int num = (int) MessageBox.Show("Licencja nie została załadowana. Licencja jest nieprawidłowa.", "Komunikat");
        }
      }
      this.wait.setVisible(false);
      this.getActiveLicense();
      this.getOtherLicense();
    }

    private void getActiveLicense()
    {
      this.wait.setWaitPanel("Trwa wczytywanie licencji", "Proszę czekać");
      this.wait.setVisible(true);
      if (Directory.Exists(this.path + "\\Licenses"))
      {
        DataTable dataTable = new DataTable();
        this.LicencesTable.DataSource = (object) dataTable;
        dataTable.Columns.Add(new DataColumn("lp", typeof (string)));
        dataTable.Columns.Add(new DataColumn("Licencja", typeof (string)));
        foreach (string filename in Directory.EnumerateFiles(this.path + "\\Licenses", "*.pem"))
        {
          if (new Certificate(filename).isActiveLicense())
          {
            string[] strArray = filename.Split('\\');
            DataRow row = dataTable.NewRow();
            row[0] = (object) (dataTable.Rows.Count + 1);
            row[1] = (object) strArray[strArray.Length - 1];
            dataTable.Rows.Add(row);
          }
        }
        if (dataTable.Rows.Count > 0)
        {
          this.LicencesTable.DataSource = (object) dataTable;
          this.LicencesTable.Columns["lp"].DisplayIndex = 0;
          this.LicencesTable.Columns["lp"].FillWeight = 15f;
          this.LicencesTable.Columns["Licencja"].DisplayIndex = 1;
          DataGridViewButtonColumn viewButtonColumn1 = new DataGridViewButtonColumn();
          viewButtonColumn1.HeaderText = "Akcje";
          viewButtonColumn1.Text = "przejdź";
          viewButtonColumn1.Name = "action";
          viewButtonColumn1.UseColumnTextForButtonValue = true;
          if (this.LicencesTable.Columns["action"] == null)
          {
            this.LicencesTable.Columns.Insert(2, (DataGridViewColumn) viewButtonColumn1);
            this.LicencesTable.Columns["action"].FillWeight = 23f;
          }
          else
          {
            this.LicencesTable.Columns["action"].DisplayIndex = 2;
            this.LicencesTable.Columns["action"].FillWeight = 23f;
          }
          DataGridViewButtonColumn viewButtonColumn2 = new DataGridViewButtonColumn();
          viewButtonColumn2.HeaderText = "";
          viewButtonColumn2.Text = "Zapisz";
          viewButtonColumn2.Name = "save";
          viewButtonColumn2.UseColumnTextForButtonValue = true;
          if (this.LicencesTable.Columns["save"] == null)
          {
            this.LicencesTable.Columns.Insert(3, (DataGridViewColumn) viewButtonColumn2);
            this.LicencesTable.Columns["save"].FillWeight = 23f;
          }
          else
          {
            this.LicencesTable.Columns["save"].DisplayIndex = 3;
            this.LicencesTable.Columns["save"].FillWeight = 23f;
          }
          DataGridViewButtonColumn viewButtonColumn3 = new DataGridViewButtonColumn();
          viewButtonColumn3.HeaderText = "";
          viewButtonColumn3.Text = "Usuń";
          viewButtonColumn3.Name = "remove";
          viewButtonColumn3.UseColumnTextForButtonValue = true;
          if (this.LicencesTable.Columns["remove"] == null)
          {
            this.LicencesTable.Columns.Insert(4, (DataGridViewColumn) viewButtonColumn3);
            this.LicencesTable.Columns["remove"].FillWeight = 23f;
          }
          else
          {
            this.LicencesTable.Columns["remove"].DisplayIndex = 4;
            this.LicencesTable.Columns["remove"].FillWeight = 23f;
          }
          this.licensesPanel.Visible = true;
        }
      }
      else
        this.licensesPanel.Visible = false;
      this.wait.setVisible(false);
    }

    private void getOtherLicense()
    {
      this.wait.setWaitPanel("Trwa wczytywanie licencji oczekujących", "Proszę czekać");
      this.wait.setVisible(true);
      if (Directory.Exists(this.path + "\\Licenses"))
      {
        DataTable dataTable = new DataTable();
        this.waitLicenses.DataSource = (object) dataTable;
        dataTable.Columns.Add(new DataColumn("lp", typeof (string)));
        dataTable.Columns.Add(new DataColumn("Licencja", typeof (string)));
        dataTable.Columns.Add(new DataColumn("Status", typeof (string)));
        foreach (string str in Directory.EnumerateFiles(this.path + "\\Licenses", "*.csr"))
        {
          string[] strArray = str.Split('\\');
          DataRow row = dataTable.NewRow();
          row[0] = (object) (dataTable.Rows.Count + 1);
          row[1] = (object) strArray[strArray.Length - 1];
          row[2] = (object) "Oczekująca";
          dataTable.Rows.Add(row);
        }
        if (Directory.Exists(this.path + "\\Licenses\\Anulowane"))
        {
          foreach (string str in Directory.EnumerateFiles(this.path + "\\Licenses\\Anulowane\\", "*.csr"))
          {
            string[] strArray = str.Split('\\');
            DataRow row = dataTable.NewRow();
            row[0] = (object) (dataTable.Rows.Count + 1);
            row[1] = (object) strArray[strArray.Length - 1];
            row[2] = (object) "Anulowana";
            dataTable.Rows.Add(row);
          }
        }
        foreach (string license in Directory.EnumerateFiles(this.path + "\\Licenses", "*.pem"))
        {
          if (!this.certificate.isActiveLicense(license))
          {
            string[] strArray = license.Split('\\');
            DataRow row = dataTable.NewRow();
            row[0] = (object) (dataTable.Rows.Count + 1);
            row[1] = (object) strArray[strArray.Length - 1];
            row[2] = (object) "Nie aktywna";
            dataTable.Rows.Add(row);
          }
        }
        if (dataTable.Rows.Count > 0)
        {
          this.waitLicenses.DataSource = (object) dataTable;
          this.waitLicenses.Columns["lp"].DisplayIndex = 0;
          this.waitLicenses.Columns["lp"].FillWeight = 10f;
          this.waitLicenses.Columns["Licencja"].DisplayIndex = 1;
          this.waitLicenses.Columns["Status"].DisplayIndex = 2;
          this.waitLicenses.Columns["Status"].FillWeight = 20f;
          DataGridViewButtonColumn viewButtonColumn1 = new DataGridViewButtonColumn();
          viewButtonColumn1.HeaderText = "Akcje";
          viewButtonColumn1.Text = "sprawdź status/pobierz";
          viewButtonColumn1.Name = "action";
          viewButtonColumn1.UseColumnTextForButtonValue = true;
          if (this.waitLicenses.Columns["action"] == null)
          {
            this.waitLicenses.Columns.Insert(3, (DataGridViewColumn) viewButtonColumn1);
            this.waitLicenses.Columns["action"].FillWeight = 38f;
          }
          else
          {
            this.waitLicenses.Columns["action"].DisplayIndex = 3;
            this.waitLicenses.Columns["action"].FillWeight = 38f;
          }
          DataGridViewButtonColumn viewButtonColumn2 = new DataGridViewButtonColumn();
          viewButtonColumn2.HeaderText = "";
          viewButtonColumn2.Text = "Usuń";
          viewButtonColumn2.Name = "remove";
          viewButtonColumn2.UseColumnTextForButtonValue = true;
          if (this.waitLicenses.Columns["remove"] == null)
          {
            this.waitLicenses.Columns.Insert(4, (DataGridViewColumn) viewButtonColumn2);
            this.waitLicenses.Columns["remove"].FillWeight = 12f;
          }
          else
          {
            this.waitLicenses.Columns["remove"].DisplayIndex = 4;
            this.waitLicenses.Columns["remove"].FillWeight = 12f;
          }
          this.waitLicensesPanel.Visible = true;
          this.waitLicenses.Refresh();
        }
      }
      else
        this.waitLicensesPanel.Visible = false;
      this.wait.setVisible(false);
    }

    private bool isExsistOU(string OU)
    {
      if (Directory.Exists(this.path + "\\Licenses"))
      {
        foreach (string fileName in Directory.EnumerateFiles(this.path + "\\Licenses", "*.pem"))
        {
          X509Certificate x509Certificate = new X509Certificate(fileName);
          if (DateTime.Compare(Convert.ToDateTime(x509Certificate.GetEffectiveDateString()), DateTime.Today) <= 0 && DateTime.Compare(DateTime.Today, Convert.ToDateTime(x509Certificate.GetExpirationDateString())) <= 0)
          {
            string[] strArray = x509Certificate.Subject.Split(new string[1]
            {
              ", "
            }, StringSplitOptions.RemoveEmptyEntries);
            string str = "";
            for (int index = 0; index < strArray.Length; ++index)
            {
              if (Regex.IsMatch(strArray[index], "^OU="))
              {
                str = strArray[index].Replace("OU=", "");
                break;
              }
            }
            if (str == OU)
              return true;
          }
        }
      }
      return false;
    }

    private string readOU(string filePath)
    {
      string[] strArray = new X509Certificate(filePath).Subject.Split(new string[1]
      {
        ", "
      }, StringSplitOptions.RemoveEmptyEntries);
      string str = "";
      for (int index = 0; index < strArray.Length; ++index)
      {
        if (Regex.IsMatch(strArray[index], "^OU="))
        {
          str = strArray[index].Replace("OU=", "");
          break;
        }
      }
      return str;
    }

    private void btnDeleteTemp_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Wszystkie zapisane wcześniej dane zostaną usunięte. Czy kontynuować?", "Usuwanie danych tymczasowych", MessageBoxButtons.YesNo) == DialogResult.No)
        return;
      try
      {
        DirectoryInfo directoryInfo1 = new DirectoryInfo(this.path + "\\ProtocolsDef");
        if (directoryInfo1.Exists)
          directoryInfo1.Delete(true);
        DirectoryInfo directoryInfo2 = new DirectoryInfo(this.path + "\\electoralEampaign");
        if (directoryInfo2.Exists)
          directoryInfo2.Delete(true);
        DirectoryInfo directoryInfo3 = new DirectoryInfo(this.path + "\\saves");
        if (directoryInfo3.Exists)
          directoryInfo3.Delete(true);
        DirectoryInfo directoryInfo4 = new DirectoryInfo(this.path + "\\tmp");
        if (directoryInfo4.Exists)
          directoryInfo4.Delete(true);
        Start.listaPlikow.Clear();
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Brak danych tymczasowych.", "Usuwanie danych tymczasowych", MessageBoxButtons.OK);
      }
      if (!Directory.Exists(this.path + "\\tmp"))
      {
        Instal instal = new Instal(this.path);
      }
      if (Directory.Exists(Path.GetTempPath() + "KBW\\tmp"))
      {
        if (!File.Exists(Path.GetTempPath() + "KBW\\tmp\\eksport.xml"))
          File.Create(Path.GetTempPath() + "KBW\\tmp\\eksport.xml");
        if (!File.Exists(Path.GetTempPath() + "KBW\\tmp\\import.xml"))
          File.Create(Path.GetTempPath() + "KBW\\tmp\\import.xml");
        if (!File.Exists(Path.GetTempPath() + "KBW\\tmp\\eksportCode.xml"))
          File.Create(Path.GetTempPath() + "KBW\\tmp\\eksportCode.xml");
      }
      int num1 = (int) MessageBox.Show("Dane tymczasowe zostały pomyślnie usunięte.", "Usuwanie danych tymczasowych", MessageBoxButtons.OK);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.label1 = new Label();
      this.label2 = new Label();
      this.licensesPanel = new Panel();
      this.btnDeleteTemp = new Button();
      this.LicencesTable = new DataGridView();
      this.waitLicensesPanel = new Panel();
      this.waitLicenses = new DataGridView();
      this.panel3 = new Panel();
      this.getLicenseFromNet = new Button();
      this.label4 = new Label();
      this.getLicenseFromDisk = new Button();
      this.label3 = new Label();
      this.panel1 = new Panel();
      this.licensesPanel.SuspendLayout();
      ((ISupportInitialize) this.LicencesTable).BeginInit();
      this.waitLicensesPanel.SuspendLayout();
      ((ISupportInitialize) this.waitLicenses).BeginInit();
      this.panel3.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.Location = new Point(14, 11);
      this.label1.Name = "label1";
      this.label1.Size = new Size(289, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Wybierz licencję, zgodnie z którą chcesz wprowadzać dane";
      this.label2.AutoSize = true;
      this.label2.Location = new Point(14, 11);
      this.label2.Name = "label2";
      this.label2.Size = new Size(105, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "Licencje oczekujące";
      this.licensesPanel.Controls.Add((Control) this.btnDeleteTemp);
      this.licensesPanel.Controls.Add((Control) this.LicencesTable);
      this.licensesPanel.Controls.Add((Control) this.label1);
      this.licensesPanel.Location = new Point(12, 12);
      this.licensesPanel.Name = "licensesPanel";
      this.licensesPanel.Size = new Size(812, 168);
      this.licensesPanel.TabIndex = 2;
      this.btnDeleteTemp.Location = new Point(564, 5);
      this.btnDeleteTemp.Margin = new Padding(2, 2, 2, 2);
      this.btnDeleteTemp.Name = "btnDeleteTemp";
      this.btnDeleteTemp.Size = new Size(245, 25);
      this.btnDeleteTemp.TabIndex = 2;
      this.btnDeleteTemp.Text = "Wyczyść dane tymczasowe";
      this.btnDeleteTemp.UseVisualStyleBackColor = true;
      this.btnDeleteTemp.Click += new EventHandler(this.btnDeleteTemp_Click);
      this.LicencesTable.AllowUserToAddRows = false;
      this.LicencesTable.AllowUserToDeleteRows = false;
      this.LicencesTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.LicencesTable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
      this.LicencesTable.BackgroundColor = SystemColors.Control;
      this.LicencesTable.BorderStyle = BorderStyle.None;
      this.LicencesTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.LicencesTable.GridColor = SystemColors.Control;
      this.LicencesTable.Location = new Point(17, 38);
      this.LicencesTable.Name = "LicencesTable";
      this.LicencesTable.ReadOnly = true;
      this.LicencesTable.Size = new Size(792, 116);
      this.LicencesTable.TabIndex = 1;
      this.waitLicensesPanel.Controls.Add((Control) this.waitLicenses);
      this.waitLicensesPanel.Controls.Add((Control) this.label2);
      this.waitLicensesPanel.Location = new Point(12, 172);
      this.waitLicensesPanel.Name = "waitLicensesPanel";
      this.waitLicensesPanel.Size = new Size(812, 197);
      this.waitLicensesPanel.TabIndex = 3;
      this.waitLicenses.AllowUserToAddRows = false;
      this.waitLicenses.AllowUserToDeleteRows = false;
      this.waitLicenses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.waitLicenses.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
      this.waitLicenses.BackgroundColor = SystemColors.Control;
      this.waitLicenses.BorderStyle = BorderStyle.None;
      this.waitLicenses.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.waitLicenses.Location = new Point(17, 30);
      this.waitLicenses.Name = "waitLicenses";
      this.waitLicenses.ReadOnly = true;
      this.waitLicenses.Size = new Size(792, 134);
      this.waitLicenses.TabIndex = 2;
      this.panel3.Controls.Add((Control) this.getLicenseFromNet);
      this.panel3.Controls.Add((Control) this.label4);
      this.panel3.Controls.Add((Control) this.getLicenseFromDisk);
      this.panel3.Controls.Add((Control) this.label3);
      this.panel3.Dock = DockStyle.Bottom;
      this.panel3.Location = new Point(0, 301);
      this.panel3.Name = "panel3";
      this.panel3.Size = new Size(831, 89);
      this.panel3.TabIndex = 4;
      this.getLicenseFromNet.Location = new Point(576, 47);
      this.getLicenseFromNet.Name = "getLicenseFromNet";
      this.getLicenseFromNet.Size = new Size(245, 23);
      this.getLicenseFromNet.TabIndex = 4;
      this.getLicenseFromNet.Text = "wnioskuj o licencję";
      this.getLicenseFromNet.UseVisualStyleBackColor = true;
      this.getLicenseFromNet.Click += new EventHandler(this.getLicenseFromNet_Click);
      this.label4.AutoSize = true;
      this.label4.Location = new Point(409, 47);
      this.label4.Name = "label4";
      this.label4.Size = new Size(21, 13);
      this.label4.TabIndex = 3;
      this.label4.Text = "lub";
      this.getLicenseFromDisk.Location = new Point(29, 47);
      this.getLicenseFromDisk.Name = "getLicenseFromDisk";
      this.getLicenseFromDisk.Size = new Size(245, 23);
      this.getLicenseFromDisk.TabIndex = 2;
      this.getLicenseFromDisk.Text = "znajdź licencję na komputerze";
      this.getLicenseFromDisk.UseVisualStyleBackColor = true;
      this.getLicenseFromDisk.Click += new EventHandler(this.getLicenseFromDisk_Click);
      this.label3.AutoSize = true;
      this.label3.Location = new Point(14, 14);
      this.label3.Name = "label3";
      this.label3.Size = new Size(175, 13);
      this.label3.TabIndex = 0;
      this.label3.Text = "Nie ma licencji, której potrzebujesz?";
      this.panel1.BackColor = Color.Transparent;
      this.panel1.Controls.Add((Control) this.licensesPanel);
      this.panel1.Controls.Add((Control) this.waitLicensesPanel);
      this.panel1.Dock = DockStyle.Fill;
      this.panel1.Location = new Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(831, 301);
      this.panel1.TabIndex = 5;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackgroundImageLayout = ImageLayout.None;
      this.ClientSize = new Size(831, 390);
      this.Controls.Add((Control) this.panel1);
      this.Controls.Add((Control) this.panel3);
      this.Name = "Start";
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Kalkulator";
      this.licensesPanel.ResumeLayout(false);
      this.licensesPanel.PerformLayout();
      ((ISupportInitialize) this.LicencesTable).EndInit();
      this.waitLicensesPanel.ResumeLayout(false);
      this.waitLicensesPanel.PerformLayout();
      ((ISupportInitialize) this.waitLicenses).EndInit();
      this.panel3.ResumeLayout(false);
      this.panel3.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
