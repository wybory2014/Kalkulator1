// Decompiled with JetBrains decompiler
// Type: Kalkulator1.AdditionalWindow.Eksport
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

using Kalkulator1;
using Kalkulator1.ResponseClass;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Web;
using System.Windows.Forms;

namespace Kalkulator1.AdditionalWindow
{
  public class Eksport : Form
  {
    private IContainer components = (IContainer) null;
    private Connection con;
    private ProtocolForm p;
    private ProtocolsList p2;
    private SendProtocol p1;
    private Certificate c;
    private string pass;
    private string path;
    private string savePath;
    private string licensePath;
    private Panel passwordPanel;
    private Button log;
    private TextBox password;
    private Label label4;
    private Label label6;

    public Eksport()
    {
      this.InitializeComponent();
      this.con = new Connection();
      this.c = new Certificate();
      this.savePath = "";
      this.licensePath = "";
      this.pass = "";
    }

    public Eksport(string savePath, string licensePath)
    {
      this.InitializeComponent();
      this.con = new Connection();
      this.c = new Certificate();
      this.savePath = savePath;
      this.Text = "Eksport";
      this.licensePath = licensePath;
      this.pass = "";
      this.path = this.path = Path.GetTempPath() + "KBW";
    }

    public Eksport(string savePath, bool send, ProtocolForm p, string licensePath)
    {
      this.p = p;
      this.InitializeComponent();
      this.licensePath = licensePath;
      this.con = new Connection();
      this.c = new Certificate();
      this.savePath = savePath;
      if (send)
      {
        this.Text = "Wysyłanie protokołu na serwer";
        this.log.Click -= new EventHandler(this.log_Click);
        this.log.Click += new EventHandler(this.send_Click);
      }
      else
      {
        this.Text = "Eksport";
        this.log.Click += new EventHandler(this.log_Click);
        this.log.Click -= new EventHandler(this.send_Click);
      }
      this.pass = "";
      this.path = this.path = Path.GetTempPath() + "KBW";
    }

    public Eksport(string savePath, bool send, ProtocolForm p, string licensePath, string password)
    {
      this.p = p;
      this.InitializeComponent();
      this.licensePath = licensePath;
      this.con = new Connection();
      this.c = new Certificate();
      this.savePath = savePath;
      if (send)
      {
        this.Text = "Wysyłanie protokołu na serwer";
        this.log.Click -= new EventHandler(this.log_Click);
        this.log.Click += new EventHandler(this.send_Click);
      }
      else
      {
        this.Text = "Eksport";
        this.log.Click += new EventHandler(this.log_Click);
        this.log.Click -= new EventHandler(this.send_Click);
      }
      this.pass = password;
      this.path = this.path = Path.GetTempPath() + "KBW";
      this.send();
    }

    public Eksport(string xml, bool send, ProtocolsList p)
    {
      this.p2 = p;
      this.InitializeComponent();
      this.con = new Connection();
      this.c = new Certificate();
      this.savePath = xml;
      if (send)
        this.Text = "Wysyłanie protokołu na serwer";
      else
        this.Text = "Eksport";
      this.path = this.path = Path.GetTempPath() + "KBW";
      this.label4.Text = "Trwa wysyłanie protokołu";
      this.password.Visible = false;
      this.log.Visible = false;
      this.send2_Click();
      this.Close();
    }

    public Eksport(string savePath, bool send, SendProtocol p, string licensePath, string password)
    {
      this.p1 = p;
      this.InitializeComponent();
      this.licensePath = licensePath;
      this.con = new Connection();
      this.c = new Certificate();
      this.savePath = savePath;
      if (send)
      {
        this.Text = "Wysyłanie protokołu na serwer";
        this.log.Click -= new EventHandler(this.log_Click);
        this.log.Click += new EventHandler(this.send_Click);
      }
      else
      {
        this.Text = "Wysyłanie protokołu";
        this.log.Click += new EventHandler(this.log_Click);
        this.log.Click -= new EventHandler(this.send_Click);
      }
      this.pass = password;
      this.path = this.path = Path.GetTempPath() + "KBW";
      this.send();
    }

    private void log_Click(object sender, EventArgs e)
    {
      if (this.con.IsAvailableNetworkActive())
      {
        if (this.pass != "")
          this.password.Text = this.pass;
        if (this.c.readKey(this.licensePath, this.password.Text))
        {
          this.label6.Visible = false;
          try
          {
            this.c.SignXml(this.savePath, this.path + "\\tmp\\eksport.xml", this.password.Text, this.licensePath);
          }
          catch (Exception ex)
          {
            int num = (int) MessageBox.Show("Eksport: " + ex.Message, "Error");
          }
          string str1 = "flag=export";
          string[] strArray = this.savePath.Split('\\');
          strArray[strArray.Length - 1].Split('-');
          string str2 = "";
          try
          {
            try
            {
              StreamReader streamReader = new StreamReader(this.path + "\\tmp\\eksport.xml");
              str2 = streamReader.ReadToEnd();
              streamReader.Close();
            }
            catch (Exception ex)
            {
              int num = (int) MessageBox.Show("Eksport: " + ex.Message, "Error");
            }
            string post1 = str1 + "&xml=" + HttpUtility.UrlEncode(str2);
            string uri = "protocols/export";
            Code code = this.con.postReq(uri, post1, 0);
            if (code.getcode() == 1)
            {
              if (MessageBox.Show("Plik już istnieje na serwerze. Czy nadpisać zmiany?", "Eksport", MessageBoxButtons.YesNo) == DialogResult.Yes)
              {
                string post2 = post1 + "&save=overwrite";
                int num = (int) MessageBox.Show(this.con.postReq(uri, post2, 0).getText(), "Eksport");
              }
              this.Close();
            }
            else
            {
              int num1 = (int) MessageBox.Show(code.getText(), "Eksport");
            }
          }
          catch (ArgumentNullException ex)
          {
            int num = (int) MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
          }
          catch (ArgumentException ex)
          {
            int num = (int) MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
          }
          catch (FileNotFoundException ex)
          {
            int num = (int) MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
          }
          catch (DirectoryNotFoundException ex)
          {
            int num = (int) MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
          }
          catch (IOException ex)
          {
            int num = (int) MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
          }
        }
        else
          this.label6.Visible = true;
      }
      else
      {
        int num = (int) MessageBox.Show("Nie masz połaczenia z internetem! Podłącz internet i spróbuj jeszcze raz.", "Uwaga");
        this.Close();
      }
    }

    private void send_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.pass != "")
          this.password.Text = this.pass;
        if (this.c.readKey(this.licensePath, this.password.Text))
        {
          this.label6.Visible = false;
          string str1 = "flag=export";
          string[] strArray = this.savePath.Split('\\');
          strArray[strArray.Length - 1].Split('-');
          string str2 = "";
          try
          {
            try
            {
              StreamReader streamReader = new StreamReader(this.savePath);
              str2 = streamReader.ReadToEnd();
              streamReader.Close();
            }
            catch (Exception ex)
            {
              int num = (int) MessageBox.Show("Wysyłanie protokołu: " + ex.Message, "Error");
            }
            Code code = this.con.postReq("protocols/export", str1 + "&xml=" + HttpUtility.UrlEncode(str2), 0);
            if (code.getcode() == 0)
            {
              if (this.p != null)
              {
                this.p.komSend = "Protokół został wysłany na serwer";
                this.p.goodcertificate = true;
              }
              if (this.p1 != null)
              {
                this.p1.komSend = "Protokół został wysłany na serwer";
                this.p1.goodcertificate = true;
              }
              this.Close();
            }
            else
            {
              int num1 = (int) MessageBox.Show(code.getText(), "Wysyłanie protokołu na serwer");
            }
          }
          catch (ArgumentNullException ex)
          {
            int num = (int) MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
          }
          catch (ArgumentException ex)
          {
            int num = (int) MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
          }
          catch (FileNotFoundException ex)
          {
            int num = (int) MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
          }
          catch (DirectoryNotFoundException ex)
          {
            int num = (int) MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
          }
          catch (IOException ex)
          {
            int num = (int) MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
          }
        }
        else
          this.label6.Visible = true;
      }
      catch (Exception ex)
      {
      }
    }

    private void send()
    {
      try
      {
        if (this.pass != "")
          this.password.Text = this.pass;
        if (this.c.readKey(this.licensePath, this.password.Text))
        {
          this.label6.Visible = false;
          string str1 = "flag=export";
          string[] strArray = this.savePath.Split('\\');
          strArray[strArray.Length - 1].Split('-');
          string str2 = "";
          try
          {
            try
            {
              StreamReader streamReader = new StreamReader(this.savePath);
              str2 = streamReader.ReadToEnd();
              streamReader.Close();
            }
            catch (Exception ex)
            {
              int num = (int) MessageBox.Show("Wysyłanie protokołu: " + ex.Message, "Error");
            }
            Code code = this.con.postReq("protocols/export", str1 + "&xml=" + HttpUtility.UrlEncode(str2), 0);
            if (code.getcode() == 0)
            {
              if (this.p != null)
              {
                this.p.komSend = "Protokół został wysłany na serwer";
                this.p.goodcertificate = true;
              }
              if (this.p1 != null)
              {
                this.p1.komSend = "Protokół został wysłany na serwer";
                this.p1.goodcertificate = true;
              }
              this.Close();
            }
            if (code.getcode() == 15)
            {
              if (this.p != null)
              {
                this.p.komSend = "Protokół już jest na serwerze";
                this.p.goodcertificate = true;
              }
              if (this.p1 != null)
              {
                this.p1.komSend = "Protokół już jest na serwerze";
                this.p1.goodcertificate = true;
              }
              int num = (int) MessageBox.Show(code.getText(), "Wysyłanie protokołu na serwer");
              this.Close();
            }
            else
            {
              int num1 = (int) MessageBox.Show(code.getText(), "Wysyłanie protokołu na serwer");
            }
          }
          catch (ArgumentNullException ex)
          {
            int num = (int) MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
          }
          catch (ArgumentException ex)
          {
            int num = (int) MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
          }
          catch (FileNotFoundException ex)
          {
            int num = (int) MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
          }
          catch (DirectoryNotFoundException ex)
          {
            int num = (int) MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
          }
          catch (IOException ex)
          {
            int num = (int) MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
          }
        }
        else
          this.label6.Visible = true;
      }
      catch (Exception ex)
      {
      }
    }

    private void send2_Click()
    {
      try
      {
        this.label6.Visible = false;
        string str = "flag=export";
        string[] strArray = this.savePath.Split('\\');
        strArray[strArray.Length - 1].Split('-');
        try
        {
          Code code = this.con.postReq("protocols/export", str + "&xml=" + HttpUtility.UrlEncode(this.savePath), 0);
          if (code.getcode() == 0)
          {
            if (this.p != null)
            {
              this.p.komSend = "Protokół został wysłany na serwer";
              this.p.goodcertificate = true;
            }
            if (this.p1 != null)
            {
              this.p1.komSend = "Protokół został wysłany na serwer";
              this.p1.goodcertificate = true;
            }
            if (this.p2 != null)
              this.p2.send = true;
            this.Close();
          }
          else
          {
            int num = (int) MessageBox.Show(code.getText(), "Wysyłanie protokołu na serwer");
          }
        }
        catch (ArgumentNullException ex)
        {
          int num = (int) MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
        }
        catch (ArgumentException ex)
        {
          int num = (int) MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
        }
        catch (FileNotFoundException ex)
        {
          int num = (int) MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
        }
        catch (DirectoryNotFoundException ex)
        {
          int num = (int) MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
        }
        catch (IOException ex)
        {
          int num = (int) MessageBox.Show("Nie mozna znalesc pliku do eksportu", "Error");
        }
      }
      catch (Exception ex)
      {
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
      this.passwordPanel = new Panel();
      this.label6 = new Label();
      this.log = new Button();
      this.password = new TextBox();
      this.label4 = new Label();
      this.passwordPanel.SuspendLayout();
      this.SuspendLayout();
      this.passwordPanel.Controls.Add((Control) this.label6);
      this.passwordPanel.Controls.Add((Control) this.log);
      this.passwordPanel.Controls.Add((Control) this.password);
      this.passwordPanel.Controls.Add((Control) this.label4);
      this.passwordPanel.Location = new Point(6, 6);
      this.passwordPanel.Name = "passwordPanel";
      this.passwordPanel.Size = new Size(403, 185);
      this.passwordPanel.TabIndex = 7;
      this.label6.AutoSize = true;
      this.label6.ForeColor = Color.Red;
      this.label6.Location = new Point(24, 167);
      this.label6.Name = "label6";
      this.label6.Size = new Size(111, 13);
      this.label6.TabIndex = 6;
      this.label6.Text = "Nieprawidłowe hasło.";
      this.label6.Visible = false;
      this.log.Location = new Point(27, 124);
      this.log.Name = "log";
      this.log.Size = new Size(334, 23);
      this.log.TabIndex = 4;
      this.log.Text = "Zaloguj";
      this.log.UseVisualStyleBackColor = true;
      this.log.Click += new EventHandler(this.log_Click);
      this.password.Location = new Point(157, 57);
      this.password.Name = "password";
      this.password.PasswordChar = '*';
      this.password.Size = new Size(204, 20);
      this.password.TabIndex = 3;
      this.label4.AutoSize = true;
      this.label4.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 238);
      this.label4.Location = new Point(25, 58);
      this.label4.Name = "label4";
      this.label4.Size = new Size(133, 15);
      this.label4.TabIndex = 0;
      this.label4.Text = "Podaj hasło do licencji:";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(415, 195);
      this.Controls.Add((Control) this.passwordPanel);
      this.Name = "Eksport";
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Eksport do sieci";
      this.passwordPanel.ResumeLayout(false);
      this.passwordPanel.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
