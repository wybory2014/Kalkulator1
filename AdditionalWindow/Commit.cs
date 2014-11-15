// Decompiled with JetBrains decompiler
// Type: Kalkulator1.Commit
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace Kalkulator1
{
  public class Commit : Form
  {
    private IContainer components = (IContainer) null;
    private string filepath;
    private Start form;
    private ProtocolForm form2;
    private SendProtocol form3;
    private Attendance form4;
    private bool loggin;
    private Panel panelNet;
    private Button LoginNext;
    private TextBox password;
    private Label label3;
    private Label error;
    private Label label1;
    private Label label2;
    private Label code;
    private Label codeBarLabel;

    public Commit()
    {
      this.InitializeComponent();
      this.filepath = "";
      this.loggin = false;
      string[] strArray = this.filepath.Split('\\');
      this.label1.Text = "Licencja " + strArray[strArray.Length - 1].Replace(".pem", "");
      this.codeBarLabel.Visible = false;
    }

    public Commit(string filepath, Start start)
    {
      this.InitializeComponent();
      this.filepath = filepath;
      this.form = start;
      this.loggin = false;
      string[] strArray = filepath.Split('\\');
      this.label1.Text = strArray[strArray.Length - 1].Replace(".pem", "");
      this.form.logged = false;
      this.codeBarLabel.Visible = false;
    }

    public Commit(string filepath, ProtocolForm pf)
    {
      this.InitializeComponent();
      this.filepath = filepath;
      this.form2 = pf;
      this.LoginNext.Click -= new EventHandler(this.LoginNext_Click);
      this.LoginNext.Click += new EventHandler(this.LoginNext2_Click);
      this.loggin = false;
      string[] strArray = filepath.Split('\\');
      this.label1.Text = strArray[strArray.Length - 1].Replace(".pem", "");
      this.form2.goodcertificate = false;
      this.codeBarLabel.Visible = false;
    }

    public Commit(string filepath, ProtocolForm pf, string xml)
    {
      this.InitializeComponent();
      this.filepath = filepath;
      this.form2 = pf;
      this.LoginNext.Click -= new EventHandler(this.LoginNext_Click);
      this.LoginNext.Click += new EventHandler(this.LoginNext2_Click);
      this.loggin = false;
      string[] strArray = filepath.Split('\\');
      this.label1.Text = strArray[strArray.Length - 1].Replace(".pem", "");
      this.form2.goodcertificate = false;
      string input = "";
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.LoadXml(xml);
      XmlNode xmlNode1 = xmlDocument.SelectSingleNode("/save/header");
      if (xmlNode1 != null)
        input = input + xmlNode1.OuterXml;
      XmlNode xmlNode2 = xmlDocument.SelectSingleNode("/save/step");
      if (xmlNode2 != null)
        input = input + xmlNode2.OuterXml;
      XmlNode xmlNode3 = xmlDocument.SelectSingleNode("/save/form");
      if (xmlNode3 != null)
        input = input + xmlNode3.OuterXml;
      XmlNode xmlNode4 = xmlDocument.SelectSingleNode("/save/komisja_sklad");
      if (xmlNode4 != null)
        input = input + xmlNode4.OuterXml;
      XmlNode xmlNode5 = xmlDocument.SelectSingleNode("/save/hardWarningCode");
      if (xmlNode5 != null)
        input = input + xmlNode5.OuterXml;
      XmlNode xmlNode6 = xmlDocument.SelectSingleNode("/save/softError");
      if (xmlNode6 != null)
        input = input + xmlNode6.OuterXml;
      XmlNode xmlNode7 = xmlDocument.SelectSingleNode("/save/hardError");
      if (xmlNode7 != null)
        input = input + xmlNode7.OuterXml;
      XmlNode xmlNode8 = xmlDocument.SelectSingleNode("/save/hardWarning");
      if (xmlNode8 != null)
        input = input + xmlNode8.OuterXml;
      string md5Hash = new ClassMd5().CreateMD5Hash(input);
      codeBar codeBar = new codeBar();
      codeBar.generateCode(md5Hash);
      this.codeBarLabel.Text = "Podpisywanie protokołu o kodzie kreskowym: " + '\n'.ToString() + codeBar.getTextReadable();
      this.codeBarLabel.Visible = true;
    }

    public Commit(string filepath, SendProtocol pf)
    {
      this.InitializeComponent();
      this.filepath = filepath;
      this.form3 = pf;
      this.loggin = false;
      this.LoginNext.Click -= new EventHandler(this.LoginNext_Click);
      this.LoginNext.Click -= new EventHandler(this.LoginNext2_Click);
      this.LoginNext.Click += new EventHandler(this.LoginNext3_Click);
      string[] strArray = filepath.Split('\\');
      this.label1.Text = strArray[strArray.Length - 1].Replace(".pem", "");
      this.form3.goodcertificate = false;
      this.codeBarLabel.Visible = false;
    }

    public Commit(string filepath, Attendance pf)
    {
      this.InitializeComponent();
      this.filepath = filepath;
      this.form4 = pf;
      this.loggin = false;
      this.LoginNext.Click -= new EventHandler(this.LoginNext_Click);
      this.LoginNext.Click -= new EventHandler(this.LoginNext2_Click);
      this.LoginNext.Click -= new EventHandler(this.LoginNext3_Click);
      this.LoginNext.Click += new EventHandler(this.LoginNext4_Click);
      string[] strArray = filepath.Split('\\');
      this.label1.Text = strArray[strArray.Length - 1].Replace(".pem", "");
      this.form4.logged = false;
      this.codeBarLabel.Visible = false;
    }

    private void LoginNext_Click(object sender, EventArgs e)
    {
      this.error.Visible = false;
      this.form.logged = new Certificate().readKey(this.filepath, this.password.Text);
      if (this.form.logged)
      {
        this.loggin = true;
        this.error.Visible = false;
        this.Close();
      }
      else
        this.error.Visible = true;
    }

    private void LoginNext2_Click(object sender, EventArgs e)
    {
      this.error.Visible = false;
      this.form2.goodcertificate = new Certificate().readKey(this.filepath, this.password.Text);
      if (this.form2.goodcertificate)
      {
        this.error.Visible = false;
        this.loggin = true;
        this.form2.password = this.password.Text;
        this.Close();
      }
      else
      {
        this.error.Visible = true;
        this.loggin = false;
      }
    }

    private void LoginNext3_Click(object sender, EventArgs e)
    {
      this.error.Visible = false;
      this.form3.goodcertificate = new Certificate().readKey(this.filepath, this.password.Text);
      if (this.form3.goodcertificate)
      {
        this.loggin = true;
        this.form3.password = this.password.Text;
        this.error.Visible = false;
        this.Close();
      }
      else
        this.error.Visible = true;
    }

    private void LoginNext4_Click(object sender, EventArgs e)
    {
      this.error.Visible = false;
      this.form4.logged = new Certificate().readKey(this.filepath, this.password.Text);
      if (this.form4.logged)
      {
        this.loggin = true;
        this.form4.password = this.password.Text;
        this.error.Visible = false;
        this.Close();
      }
      else
        this.error.Visible = true;
    }

    private void Commit_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.loggin)
        return;
      if (this.form != null)
        this.form.logged = false;
      if (this.form2 != null)
        this.form2.goodcertificate = false;
      if (this.form3 != null)
        this.form3.goodcertificate = false;
      if (this.form4 != null)
        this.form4.logged = false;
    }

    private void Commit_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Return)
        return;
      if (this.form != null)
      {
        this.form.logged = false;
        this.LoginNext_Click(sender, (EventArgs) e);
      }
      if (this.form2 != null)
      {
        this.form2.goodcertificate = false;
        this.LoginNext2_Click(sender, (EventArgs) e);
      }
      if (this.form3 != null)
      {
        this.form3.goodcertificate = false;
        this.LoginNext3_Click(sender, (EventArgs) e);
      }
      if (this.form4 != null)
      {
        this.form4.logged = false;
        this.LoginNext4_Click(sender, (EventArgs) e);
      }
    }

    private void password_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Return)
        return;
      if (this.form != null)
      {
        this.form.logged = false;
        this.LoginNext_Click(sender, (EventArgs) e);
      }
      if (this.form2 != null)
      {
        this.form2.goodcertificate = false;
        this.LoginNext2_Click(sender, (EventArgs) e);
      }
      if (this.form3 != null)
      {
        this.form3.goodcertificate = false;
        this.LoginNext3_Click(sender, (EventArgs) e);
      }
      if (this.form4 != null)
      {
        this.form4.logged = false;
        this.LoginNext4_Click(sender, (EventArgs) e);
      }
    }

    private void error_Click(object sender, EventArgs e)
    {
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.panelNet = new Panel();
      this.code = new Label();
      this.label2 = new Label();
      this.label1 = new Label();
      this.error = new Label();
      this.LoginNext = new Button();
      this.password = new TextBox();
      this.label3 = new Label();
      this.codeBarLabel = new Label();
      this.panelNet.SuspendLayout();
      this.SuspendLayout();
      this.panelNet.Controls.Add((Control) this.codeBarLabel);
      this.panelNet.Controls.Add((Control) this.code);
      this.panelNet.Controls.Add((Control) this.label2);
      this.panelNet.Controls.Add((Control) this.label1);
      this.panelNet.Controls.Add((Control) this.error);
      this.panelNet.Controls.Add((Control) this.LoginNext);
      this.panelNet.Controls.Add((Control) this.password);
      this.panelNet.Controls.Add((Control) this.label3);
      this.panelNet.Location = new Point(3, 6);
      this.panelNet.Name = "panelNet";
      this.panelNet.Size = new Size(403, 197);
      this.panelNet.TabIndex = 3;
      this.code.AutoSize = true;
      this.code.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.code.Location = new Point(98, 98);
      this.code.MaximumSize = new Size(370, 0);
      this.code.Name = "code";
      this.code.Size = new Size(0, 15);
      this.code.TabIndex = 8;
      this.label2.AutoSize = true;
      this.label2.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.label2.Location = new Point(160, 3);
      this.label2.MaximumSize = new Size(370, 0);
      this.label2.Name = "label2";
      this.label2.Size = new Size(61, 15);
      this.label2.TabIndex = 7;
      this.label2.Text = "Licencja";
      this.label1.AutoSize = true;
      this.label1.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.label1.Location = new Point(16, 18);
      this.label1.MaximumSize = new Size(370, 0);
      this.label1.Name = "label1";
      this.label1.Size = new Size(44, 15);
      this.label1.TabIndex = 6;
      this.label1.Text = "Hasło";
      this.error.AutoSize = true;
      this.error.ForeColor = Color.Red;
      this.error.Location = new Point(16, 174);
      this.error.Name = "error";
      this.error.RightToLeft = RightToLeft.No;
      this.error.Size = new Size(108, 13);
      this.error.TabIndex = 5;
      this.error.Text = "Nieprawidłowe hasło";
      this.error.Visible = false;
      this.error.Click += new EventHandler(this.error_Click);
      this.LoginNext.Location = new Point(16, 138);
      this.LoginNext.Name = "LoginNext";
      this.LoginNext.Size = new Size(363, 33);
      this.LoginNext.TabIndex = 4;
      this.LoginNext.Text = "Zaloguj";
      this.LoginNext.UseVisualStyleBackColor = true;
      this.LoginNext.Click += new EventHandler(this.LoginNext_Click);
      this.password.Location = new Point(101, 71);
      this.password.Name = "password";
      this.password.PasswordChar = '*';
      this.password.Size = new Size(278, 20);
      this.password.TabIndex = 3;
      this.password.KeyDown += new KeyEventHandler(this.password_KeyDown);
      this.label3.AutoSize = true;
      this.label3.Location = new Point(16, 74);
      this.label3.Name = "label3";
      this.label3.Size = new Size(36, 13);
      this.label3.TabIndex = 1;
      this.label3.Text = "Hasło";
      this.codeBarLabel.AutoSize = true;
      this.codeBarLabel.Location = new Point(16, 100);
      this.codeBarLabel.Name = "codeBarLabel";
      this.codeBarLabel.Size = new Size(36, 13);
      this.codeBarLabel.TabIndex = 9;
      this.codeBarLabel.Text = "Hasło";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(411, 207);
      this.Controls.Add((Control) this.panelNet);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "Commit";
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Podaj hasło do licencji";
      this.FormClosing += new FormClosingEventHandler(this.Commit_FormClosing);
      this.KeyDown += new KeyEventHandler(this.Commit_KeyDown);
      this.panelNet.ResumeLayout(false);
      this.panelNet.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
