// Decompiled with JetBrains decompiler
// Type: Kalkulator1.PrintTest
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Kalkulator1
{
  public class PrintTest : Form
  {
    private IContainer components = (IContainer) null;
    private string path;
    private WebBrowser webBrowser1;
    private Button button1;
    private PictureBox pictureBox1;
    private TextBox textBox1;
    private ToolTip toolTip1;

    public PrintTest()
    {
      this.InitializeComponent();
      this.path = Path.GetTempPath() + "KBW\\";
      new ToolTip().SetToolTip((Control) this.textBox1, "aaaa " + '\n'.ToString() + "bb");
    }

    private void button1_Click(object sender, EventArgs e)
    {
      string name = "Software\\Microsoft\\Internet Explorer\\PageSetup";
      using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(name, true))
      {
        if (registryKey == null)
          return;
        registryKey.GetValue("footer");
        registryKey.GetValue("header");
        registryKey.SetValue("footer", (object) "");
        registryKey.SetValue("header", (object) "");
        registryKey.SetValue("margin_top", (object) "0.75");
        registryKey.SetValue("margin_right", (object) "0.75");
        registryKey.SetValue("margin_bottom", (object) "0.75");
        registryKey.SetValue("margin_left", (object) "0.75");
        this.webBrowser1.ShowPrintDialog();
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

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.webBrowser1 = new WebBrowser();
      this.button1 = new Button();
      this.pictureBox1 = new PictureBox();
      this.textBox1 = new TextBox();
      this.toolTip1 = new ToolTip(this.components);
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.SuspendLayout();
      this.webBrowser1.Location = new Point(12, 12);
      this.webBrowser1.MinimumSize = new Size(20, 20);
      this.webBrowser1.Name = "webBrowser1";
      this.webBrowser1.Size = new Size(653, 444);
      this.webBrowser1.TabIndex = 0;
      this.button1.Location = new Point(687, 99);
      this.button1.Name = "button1";
      this.button1.Size = new Size(196, 106);
      this.button1.TabIndex = 1;
      this.button1.Text = "button1";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.pictureBox1.BackColor = System.Drawing.Color.White;
      this.pictureBox1.Location = new Point(12, 462);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(653, 114);
      this.pictureBox1.TabIndex = 2;
      this.pictureBox1.TabStop = false;
      this.textBox1.Location = new Point(737, 322);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new Size(100, 75);
      this.textBox1.TabIndex = 3;
      this.textBox1.Text = "jrdyidsy";
      this.toolTip1.ToolTipTitle = "aaa /n bbb";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(929, 582);
      this.Controls.Add((Control) this.textBox1);
      this.Controls.Add((Control) this.pictureBox1);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.webBrowser1);
      this.Name = "PrintTest";
      this.Text = "PrintTest";
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
