// Decompiled with JetBrains decompiler
// Type: Kalkulator1.AdditionalWindow.Import
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
using System.Xml;

namespace Kalkulator1.AdditionalWindow
{
  public class Import : Form
  {
    private IContainer components = (IContainer) null;
    private Connection con;
    private Certificate c;
    private string name;
    private ProtocolForm form2;
    private string licensePath;
    private string okregId;
    private string instId;
    private string obwod;
    private string inst;
    private string okreg;
    private Panel passwordPanel;
    private Label label6;
    private Button log;
    private TextBox password;
    private Label label4;

    public Import()
    {
      this.InitializeComponent();
      this.con = new Connection();
      this.name = "";
      this.form2 = new ProtocolForm();
      this.licensePath = "";
      this.c = new Certificate();
    }

    public Import(string fileName, string licensePath, ProtocolForm pf, XmlDocument header)
    {
      this.InitializeComponent();
      this.con = new Connection();
      this.name = fileName;
      this.form2 = pf;
      this.licensePath = licensePath;
      this.c = new Certificate();
      string[] strArray1 = this.name.Split('\\');
      string str1 = strArray1[strArray1.Length - 1];
      string str2 = "flag=import&filename=" + HttpUtility.UrlEncode(str1);
      string[] strArray2 = str1.Split('-');
      string str3 = str2 + "&akcja=" + HttpUtility.UrlEncode(strArray2[0].Replace('_', '/')) + "&jsn=" + strArray2[1];
      this.obwod = strArray2[2];
      this.inst = strArray2[3];
      this.okreg = strArray2[4].Replace(".xml", "").Split(' ')[0];
      foreach (XmlNode xmlNode1 in header.SelectSingleNode("/akcja_wyborcza/jns"))
      {
        if (xmlNode1.Attributes["nr"].InnerText == this.obwod)
        {
          foreach (XmlNode xmlNode2 in xmlNode1)
          {
            if (xmlNode2.Attributes["kod"].InnerText == this.inst)
            {
              this.instId = xmlNode2.Attributes["id"].InnerText;
              foreach (XmlNode xmlNode3 in xmlNode2)
              {
                if (xmlNode3.Attributes["nr"].InnerText == this.okreg)
                  this.okregId = xmlNode3.Attributes["id"].InnerText;
              }
            }
          }
        }
      }
    }

    private void log_Click(object sender, EventArgs e)
    {
      if (this.con.IsAvailableNetworkActive())
      {
        if (this.c.readKey(this.licensePath, this.password.Text))
        {
          WaitPanel waitPanel = new WaitPanel("Wait_04", this.Size.Width, this.Size.Height);
          waitPanel.setWaitPanel("Trwa importowanie danych", "Proszę czekać");
          waitPanel.setSize(this.passwordPanel.Size);
          waitPanel.setLocation(this.passwordPanel.Location);
          this.Controls.Add((Control) waitPanel.getPanel());
          this.Controls[waitPanel.getName()].BringToFront();
          waitPanel.setVisible(true);
          string[] strArray1 = this.name.Split('\\');
          string str1 = strArray1[strArray1.Length - 1];
          string uri = "protocols/import";
          string str2 = "flag=import&filename=" + HttpUtility.UrlEncode(str1);
          string[] strArray2 = str1.Split('-');
          string str3 = str2 + "&akcja=" + HttpUtility.UrlEncode(strArray2[0].Replace('_', '/')) + "&jsn=" + strArray2[1] + "&obw=" + strArray2[2] + "&inst=" + strArray2[3] + "&okr=" + strArray2[4].Replace(".xml", "").Split(' ')[0];
          string str4 = "<import><header><jns_kod>" + strArray2[1] + "</jns_kod><nrObwodu>" + strArray2[2] + "</nrObwodu>" + "<id_intytucji>" + this.instId + "</id_intytucji>" + "<id_okregu>" + this.okregId + "</id_okregu></header></import>";
          try
          {
            new Certificate().SignXmlText(str4, Path.GetTempPath() + "KBW\\tmp\\import.xml", this.password.Text, this.licensePath);
            StreamReader streamReader = new StreamReader(Path.GetTempPath() + "KBW\\tmp\\import.xml");
            str4 = streamReader.ReadToEnd();
            streamReader.Close();
          }
          catch (Exception ex)
          {
            int num = (int) MessageBox.Show("Import protokołu: " + ex.Message, "Error");
          }
          string post = str3 + "&xmlImport=" + HttpUtility.UrlEncode(str4);
          ResponseData responseData = this.con.postImport(uri, post, 0);
          if (responseData.getCode().getcode() == 0)
          {
            StreamWriter streamWriter = new StreamWriter(this.name, false);
            streamWriter.Write(responseData.getXml());
            streamWriter.Close();
            this.form2.imported = true;
            this.Close();
          }
          else
          {
            int num = (int) MessageBox.Show(responseData.getCode().getText(), "Import");
            this.form2.imported = false;
          }
          waitPanel.setVisible(true);
        }
        else
        {
          this.label6.Visible = true;
          this.form2.imported = false;
        }
      }
      else
      {
        int num = (int) MessageBox.Show("Nie masz połaczenia z internetem!", "Uwaga");
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
      this.passwordPanel.Location = new Point(4, 4);
      this.passwordPanel.Name = "passwordPanel";
      this.passwordPanel.Size = new Size(403, 185);
      this.passwordPanel.TabIndex = 8;
      this.label6.AutoSize = true;
      this.label6.ForeColor = Color.Red;
      this.label6.Location = new Point(24, 167);
      this.label6.Name = "label6";
      this.label6.Size = new Size(111, 13);
      this.label6.TabIndex = 6;
      this.label6.Text = "Nieprawidłowe hasło.";
      this.label6.Visible = false;
      this.log.Location = new Point(27, 126);
      this.log.Name = "log";
      this.log.Size = new Size(334, 23);
      this.log.TabIndex = 4;
      this.log.Text = "Zaloguj";
      this.log.UseVisualStyleBackColor = true;
      this.log.Click += new EventHandler(this.log_Click);
      this.password.Location = new Point(164, 59);
      this.password.Name = "password";
      this.password.PasswordChar = '*';
      this.password.Size = new Size(197, 20);
      this.password.TabIndex = 3;
      this.label4.AutoSize = true;
      this.label4.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 238);
      this.label4.Location = new Point(25, 60);
      this.label4.Name = "label4";
      this.label4.Size = new Size(133, 15);
      this.label4.TabIndex = 0;
      this.label4.Text = "Podaj hasło do licencji:";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(409, 192);
      this.Controls.Add((Control) this.passwordPanel);
      this.Name = "Import";
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Import z sieci";
      this.passwordPanel.ResumeLayout(false);
      this.passwordPanel.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
