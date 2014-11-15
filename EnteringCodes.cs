// Decompiled with JetBrains decompiler
// Type: Kalkulator1.EnteringCodes
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

using Kalkulator1.AdditionalWindow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Web;
using System.Windows.Forms;
using System.Xml;

namespace Kalkulator1
{
  public class EnteringCodes : Form
  {
    private IContainer components = (IContainer) null;
    private List<string> posts;
    private string protocol;
    private string savePath;
    private string OU;
    private string licensePath;
    private ProtocolForm f;
    private Label label1;
    private Label label2;
    private Panel codesPanel;
    private Panel webPanel;
    private Label label4;
    private Label label3;
    private Button send;
    private ToolTip toolTip1;
    private Button akcept;
    private Label label5;
    public TextBox codeField;
    private Button show;
    private Label errorsList;

    public EnteringCodes()
    {
      this.InitializeComponent();
      this.posts = new List<string>();
      this.protocol = "";
      this.OU = "";
      this.show.Visible = false;
    }

    public EnteringCodes(string protocolName, string savePath, string OU, string licensePath, ProtocolForm form)
    {
      this.InitializeComponent();
      this.posts = new List<string>();
      this.savePath = savePath;
      this.protocol = protocolName;
      this.OU = OU;
      this.f = form;
      this.licensePath = licensePath;
      this.errorsList.Visible = false;
    }

    private string generetedCode(string errors, string controlSum)
    {
      return new ClassMd5().CreateMD5Hash2(errors + this.OU + controlSum);
    }

    private bool check(string code, string errors, string controlSum)
    {
      return this.generetedCode(errors, controlSum) == code;
    }

    private void send_Click(object sender, EventArgs e)
    {
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(this.savePath);
      try
      {
        XmlNode xmlNode1 = xmlDocument.SelectSingleNode("/save/header");
        XmlNode xmlNode2 = xmlDocument.SelectSingleNode("/save/step1");
        XmlNode xmlNode3 = xmlDocument.SelectSingleNode("/save/step2");
        XmlNode xmlNode4 = xmlDocument.SelectSingleNode("/save/step3");
        XmlNode xmlNode5 = xmlDocument.SelectSingleNode("/save/hardWarning");
        string innerXml1 = xmlDocument.InnerXml;
        string input = "";
        if (xmlNode1 != null)
          input = input + xmlNode1.OuterXml;
        if (xmlNode2 != null)
          input = input + xmlNode2.OuterXml;
        if (xmlNode3 != null)
          input = input + xmlNode3.OuterXml;
        if (xmlNode4 != null)
          input = input + xmlNode4.OuterXml;
        string md5Hash2 = new ClassMd5().CreateMD5Hash2(input);
        string str1 = "";
        if (xmlNode5 != null)
          str1 = xmlNode5.OuterXml;
        string post = "controlSum=" + HttpUtility.UrlEncode(md5Hash2) + "&errors=" + HttpUtility.UrlEncode(str1);
        string[] strArray1 = this.savePath.Split('\\');
        string[] strArray2 = strArray1[strArray1.Length - 1].Split('-');
        string post2 = "&fileName=" + HttpUtility.UrlEncode(strArray1[strArray1.Length - 1]) + "&jns=" + HttpUtility.UrlEncode(strArray2[1]);
        int num1 = (int) new CodeSend(post, innerXml1, post2, this.licensePath, this).ShowDialog();
        if (this.check(this.codeField.Text, str1, md5Hash2))
        {
          string innerXml2 = xmlDocument.InnerXml;
          string str2 = "<hardWarningCode>" + this.codeField.Text + "</hardWarningCode>";
          innerXml2.Replace("</save>", str2 + "</save>");
          StreamWriter streamWriter = new StreamWriter(this.savePath, false);
          streamWriter.Write(innerXml2);
          streamWriter.Close();
          this.f.goodcertificate = true;
          this.Close();
        }
        else
        {
          int num2 = (int) MessageBox.Show("Wpisany kod jest nie poprawny, spróbuj jeszcze raz", "Kounikat");
        }
      }
      catch (XmlException ex)
      {
      }
      catch (NullReferenceException ex)
      {
      }
    }

    private void akcept_Click(object sender, EventArgs e)
    {
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(this.savePath);
      if (this.checkPhone() == this.codeField.Text)
      {
        string innerXml = xmlDocument.InnerXml;
        string str = "<hardWarningCode>" + this.codeField.Text + "</hardWarningCode>";
        innerXml.Replace("</save>", str + "</save>");
        StreamWriter streamWriter = new StreamWriter(this.savePath, false);
        streamWriter.Write(innerXml);
        streamWriter.Close();
        this.f.goodcertificate = true;
        this.Close();
      }
      else
      {
        int num = (int) MessageBox.Show("Wpisany kod jest nie poprawny, spróbuj jeszcze raz", "Kounikat");
      }
    }

    private string checkPhone()
    {
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(this.savePath);
      try
      {
        int day = DateTime.Now.Day;
        DateTime now = DateTime.Now;
        int month = now.Month;
        now = DateTime.Now;
        int year = now.Year;
        XmlNode xmlNode1 = xmlDocument.SelectSingleNode("/save/header/nrObwodu");
        List<string> list = new List<string>();
        XmlNode xmlNode2 = xmlDocument.SelectSingleNode("/save/hardWarning");
        if (xmlNode2 != null)
        {
          foreach (XmlNode xmlNode3 in xmlNode2)
          {
            for (int index1 = 0; index1 < xmlNode3.ChildNodes.Count; ++index1)
            {
              bool flag = true;
              for (int index2 = 0; index2 < list.Count; ++index2)
              {
                if (xmlNode3.ChildNodes[index1].InnerText == list[index2])
                {
                  flag = false;
                  break;
                }
              }
              if (flag)
                list.Add(xmlNode3.ChildNodes[index1].InnerText);
            }
          }
        }
        list.Sort();
        string str = "";
        for (int index = 0; index < list.Count; ++index)
          str = str + list[index];
        if (xmlNode1 != null)
          str = str + xmlNode1.InnerText;
        return new ClassMd5().CreateMD5Hash2(str + day.ToString() + month.ToString() + year.ToString()).Substring(0, 10);
      }
      catch (XmlException ex)
      {
      }
      catch (NullReferenceException ex)
      {
      }
      return "";
    }

    private void show_Click(object sender, EventArgs e)
    {
      this.codesPanel.AutoSize = true;
      this.codesPanel.MaximumSize = new Size(640, 0);
      this.codesPanel.Visible = true;
      this.show.Text = "Showaj błędy";
      this.show.Click -= new EventHandler(this.show_Click);
      this.show.Click += new EventHandler(this.hide_Click);
      this.errorsList.Visible = true;
      string str = "";
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(this.savePath);
      try
      {
        xmlDocument.SelectSingleNode("/save/hardWarning");
        List<string> list = new List<string>();
        XmlNode xmlNode1 = xmlDocument.SelectSingleNode("/save/hardWarning");
        if (xmlNode1 != null)
        {
          foreach (XmlNode xmlNode2 in xmlNode1)
          {
            for (int index1 = 0; index1 < xmlNode2.ChildNodes.Count; ++index1)
            {
              bool flag = true;
              for (int index2 = 0; index2 < list.Count; ++index2)
              {
                if (xmlNode2.ChildNodes[index1].InnerText == list[index2])
                {
                  flag = false;
                  break;
                }
              }
              if (flag)
                list.Add(xmlNode2.ChildNodes[index1].InnerText);
            }
          }
        }
        list.Sort();
        str = "";
        for (int index = 0; index < list.Count; ++index)
          str = str + list[index] + " ";
      }
      catch (XmlException ex)
      {
      }
      catch (NullReferenceException ex)
      {
      }
      this.errorsList.Text = str;
      this.webPanel.Location = new Point(10, this.codesPanel.Location.Y + this.codesPanel.Height);
    }

    private void hide_Click(object sender, EventArgs e)
    {
      this.codesPanel.AutoSize = false;
      this.codesPanel.MaximumSize = new Size(640, 80);
      this.codesPanel.Size = new Size(640, 80);
      this.codesPanel.Visible = true;
      this.show.Text = "Pokaż błędy";
      this.show.Click += new EventHandler(this.show_Click);
      this.show.Click -= new EventHandler(this.hide_Click);
      this.errorsList.Visible = false;
      this.errorsList.Text = "bledy";
      this.webPanel.Location = new Point(10, 174);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.label1 = new Label();
      this.label2 = new Label();
      this.codesPanel = new Panel();
      this.akcept = new Button();
      this.codeField = new TextBox();
      this.label5 = new Label();
      this.webPanel = new Panel();
      this.label4 = new Label();
      this.label3 = new Label();
      this.send = new Button();
      this.toolTip1 = new ToolTip(this.components);
      this.show = new Button();
      this.errorsList = new Label();
      this.codesPanel.SuspendLayout();
      this.webPanel.SuspendLayout();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.label1.Location = new Point(10, 13);
      this.label1.Name = "label1";
      this.label1.Size = new Size(60, 16);
      this.label1.TabIndex = 0;
      this.label1.Text = "Uwaga!";
      this.label2.AutoSize = true;
      this.label2.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 238);
      this.label2.Location = new Point(10, 42);
      this.label2.MaximumSize = new Size(640, 0);
      this.label2.Name = "label2";
      this.label2.Size = new Size(613, 30);
      this.label2.TabIndex = 1;
      this.label2.Text = "Protokół zawiera ostrzeżenia twarde. Aby przejść dalej musisz wpisać kod, bądź wysłać prośbę o potwierdzenie możliwości zatwierdzenia protokołu z ostrzeżeniami twardymi.";
      this.codesPanel.AutoSize = true;
      this.codesPanel.Controls.Add((Control) this.errorsList);
      this.codesPanel.Controls.Add((Control) this.show);
      this.codesPanel.Controls.Add((Control) this.akcept);
      this.codesPanel.Controls.Add((Control) this.codeField);
      this.codesPanel.Controls.Add((Control) this.label5);
      this.codesPanel.Location = new Point(10, 88);
      this.codesPanel.Name = "codesPanel";
      this.codesPanel.Size = new Size(640, 80);
      this.codesPanel.TabIndex = 2;
      this.akcept.Location = new Point(535, 14);
      this.akcept.Name = "akcept";
      this.akcept.Size = new Size(75, 23);
      this.akcept.TabIndex = 2;
      this.akcept.Text = "zatwierdź";
      this.akcept.UseVisualStyleBackColor = true;
      this.akcept.Click += new EventHandler(this.akcept_Click);
      this.codeField.Location = new Point(78, 19);
      this.codeField.Name = "codeField";
      this.codeField.Size = new Size(347, 20);
      this.codeField.TabIndex = 1;
      this.label5.AutoSize = true;
      this.label5.Location = new Point(3, 19);
      this.label5.Name = "label5";
      this.label5.Size = new Size(57, 13);
      this.label5.TabIndex = 0;
      this.label5.Text = "Wpisz kod";
      this.webPanel.AutoSize = true;
      this.webPanel.Controls.Add((Control) this.label4);
      this.webPanel.Controls.Add((Control) this.label3);
      this.webPanel.Controls.Add((Control) this.send);
      this.webPanel.Location = new Point(10, 174);
      this.webPanel.Name = "webPanel";
      this.webPanel.Size = new Size(640, 68);
      this.webPanel.TabIndex = 3;
      this.label4.AutoSize = true;
      this.label4.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 238);
      this.label4.Location = new Point(3, 7);
      this.label4.MaximumSize = new Size(640, 0);
      this.label4.Name = "label4";
      this.label4.Size = new Size(24, 15);
      this.label4.TabIndex = 5;
      this.label4.Text = "lub";
      this.label3.AutoSize = true;
      this.label3.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 238);
      this.label3.Location = new Point(3, 35);
      this.label3.MaximumSize = new Size(640, 0);
      this.label3.Name = "label3";
      this.label3.Size = new Size(422, 15);
      this.label3.TabIndex = 4;
      this.label3.Text = "wyślij prośbę o możliwość zatwierdzenia protokołu z ostrzeżeniami twardymi";
      this.send.Location = new Point(535, 27);
      this.send.Name = "send";
      this.send.Size = new Size(80, 23);
      this.send.TabIndex = 0;
      this.send.Text = "wyślij";
      this.send.UseVisualStyleBackColor = true;
      this.send.Click += new EventHandler(this.send_Click);
      this.show.Location = new Point(535, 54);
      this.show.Name = "show";
      this.show.Size = new Size(75, 23);
      this.show.TabIndex = 3;
      this.show.Text = "Pokaż błędy";
      this.show.UseVisualStyleBackColor = true;
      this.show.Click += new EventHandler(this.show_Click);
      this.errorsList.AutoSize = true;
      this.errorsList.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.errorsList.ForeColor = Color.DodgerBlue;
      this.errorsList.Location = new Point(9, 59);
      this.errorsList.MaximumSize = new Size(500, 0);
      this.errorsList.Name = "errorsList";
      this.errorsList.Size = new Size(37, 13);
      this.errorsList.TabIndex = 4;
      this.errorsList.Text = "bledy";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.AutoScroll = true;
      this.AutoSize = true;
      this.ClientSize = new Size(664, 252);
      this.Controls.Add((Control) this.webPanel);
      this.Controls.Add((Control) this.codesPanel);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.Name = "EnteringCodes";
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Wprowadzanie kodu";
      this.codesPanel.ResumeLayout(false);
      this.codesPanel.PerformLayout();
      this.webPanel.ResumeLayout(false);
      this.webPanel.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
