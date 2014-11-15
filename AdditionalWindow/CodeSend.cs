// Decompiled with JetBrains decompiler
// Type: Kalkulator1.AdditionalWindow.CodeSend
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

using Kalkulator1;
using Kalkulator1.AdditionalClass;
using Kalkulator1.ResponseClass;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Web;
using System.Windows.Forms;

namespace Kalkulator1.AdditionalWindow
{
  public class CodeSend : Form
  {
    private IContainer components = (IContainer) null;
    private string postWarning;
    private string postWarning2;
    private string licensePath;
    private string xml;
    private EnteringCodes f;
    private Connection con;
    private Panel passwordPanel;
    private Label label6;
    private Button log;
    private TextBox password;
    private Label label4;

    public CodeSend(string post, string xml, string post2, string licensePath, EnteringCodes f)
    {
      this.InitializeComponent();
      this.postWarning = post;
      this.postWarning2 = post2;
      this.con = new Connection();
      this.licensePath = licensePath;
      this.f = f;
      this.xml = xml;
    }

    private void log_Click(object sender, EventArgs e)
    {
      if (this.con.IsAvailableNetworkActive())
      {
        Certificate certificate = new Certificate();
        if (certificate.readKey(this.licensePath, this.password.Text))
        {
          WaitPanel waitPanel1 = new WaitPanel("Wait_04", this.Size.Width, this.Size.Height);
          waitPanel1.setWaitPanel("Trwa oczekiwanie na przyznanie kodu odblokowującego przez pełnomocnika.", "Proszę czekać");
          waitPanel1.setSize(this.passwordPanel.Size);
          waitPanel1.setLocation(this.passwordPanel.Location);
          this.Controls.Add((Control) waitPanel1.getPanel());
          this.Controls[waitPanel1.getName()].BringToFront();
          waitPanel1.setVisible(true);
          this.label6.Visible = false;
          try
          {
            certificate.SignXmlText(this.xml, Path.GetTempPath() + "KBW\\tmp\\eksportCode.xml", this.password.Text, this.licensePath);
            StreamReader streamReader = new StreamReader(Path.GetTempPath() + "KBW\\tmp\\eksportCode.xml");
            this.xml = streamReader.ReadToEnd();
            streamReader.Close();
          }
          catch (Exception ex)
          {
            int num = (int) MessageBox.Show("Wprowadzanie kodu: " + ex.Message, "Error");
          }
          string post1 = this.postWarning + this.postWarning2 + "&flag=export&flag2=akcept&xml=" + HttpUtility.UrlEncode(this.xml);
          string uri = "protocols/export";
          ResponseData responseData1 = this.con.postSendCode(uri, post1, 0);
          if (responseData1.getCode().getcode() == 0)
          {
            WaitPanel waitPanel2 = new WaitPanel("Wait_04", this.Size.Width, this.Size.Height);
            waitPanel2.setWaitPanel("Trwa sprawdzanie czy ostrzeżenie/a twarde zostały zaakceptowane", "Proszę czekać");
            waitPanel2.setSize(this.passwordPanel.Size);
            waitPanel2.setLocation(this.passwordPanel.Location);
            this.Controls.Add((Control) waitPanel2.getPanel());
            this.Controls[waitPanel2.getName()].BringToFront();
            waitPanel2.setVisible(true);
            bool flag = false;
            int num = 0;
            while (!flag)
            {
              ++num;
              string post2 = "flag=export&flag2=check&" + this.postWarning2 + "&xml=" + HttpUtility.UrlEncode(this.xml);
              ResponseData responseData2 = this.con.postSendCode(uri, post2, 0);
              if (responseData2.getCode().getcode() == 10)
              {
                this.f.codeField.Text = responseData2.getXml();
                flag = true;
                this.Close();
              }
              else if (responseData2.getCode().getcode() == 9)
              {
                waitPanel2.setWaitPanel("Protokół z ostrzeżeniami twardymi został odrzucony", "Proszę czekać");
                flag = true;
              }
              else if (responseData2.getCode().getcode() != 11 && MessageBox.Show(responseData2.getCode().getText() + " Spróbować jeszcze raz?", "Oczekiwanie na odpowiedź", MessageBoxButtons.YesNo) == DialogResult.No)
              {
                flag = true;
                this.Close();
              }
              if (num % 11 == 0 && MessageBox.Show("Protokół ciągle oczekuje na akceptacje. Kontynuować sprawdzanie?", "Oczekiwanie na odpowiedź", MessageBoxButtons.YesNo) == DialogResult.No)
              {
                flag = true;
                this.Close();
              }
            }
            this.Close();
          }
          else
          {
            int num = (int) MessageBox.Show(responseData1.getCode().getText(), "Komuniakt");
            this.Close();
          }
          waitPanel1.setVisible(true);
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
      this.passwordPanel.Location = new Point(8, 8);
      this.passwordPanel.Margin = new Padding(6, 6, 6, 6);
      this.passwordPanel.Name = "passwordPanel";
      this.passwordPanel.Size = new Size(806, 356);
      this.passwordPanel.TabIndex = 8;
      this.label6.AutoSize = true;
      this.label6.ForeColor = Color.Red;
      this.label6.Location = new Point(48, 321);
      this.label6.Margin = new Padding(6, 0, 6, 0);
      this.label6.Name = "label6";
      this.label6.Size = new Size(258, 26);
      this.label6.TabIndex = 6;
      this.label6.Text = "Hasło jest nieprawidłowe.";
      this.label6.Visible = false;
      this.log.Location = new Point(54, 242);
      this.log.Margin = new Padding(6, 6, 6, 6);
      this.log.Name = "log";
      this.log.Size = new Size(668, 44);
      this.log.TabIndex = 4;
      this.log.Text = "Zaloguj";
      this.log.UseVisualStyleBackColor = true;
      this.log.Click += new EventHandler(this.log_Click);
      this.password.Location = new Point(146, 110);
      this.password.Margin = new Padding(6, 6, 6, 6);
      this.password.Name = "password";
      this.password.PasswordChar = '*';
      this.password.Size = new Size(572, 31);
      this.password.TabIndex = 3;
      this.label4.AutoSize = true;
      this.label4.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 238);
      this.label4.Location = new Point(50, 113);
      this.label4.Margin = new Padding(6, 0, 6, 0);
      this.label4.Name = "label4";
      this.label4.Size = new Size(84, 29);
      this.label4.TabIndex = 0;
      this.label4.Text = "Hasło:";
      this.AutoScaleDimensions = new SizeF(12f, 25f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(820, 371);
      this.Controls.Add((Control) this.passwordPanel);
      this.Margin = new Padding(6, 6, 6, 6);
      this.Name = "CodeSend";
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Logowanie";
      this.passwordPanel.ResumeLayout(false);
      this.passwordPanel.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
