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
		private string path;
		private System.ComponentModel.IContainer components = null;
		private WebBrowser webBrowser1;
		private Button button1;
		private PictureBox pictureBox1;
		private TextBox textBox1;
		private ToolTip toolTip1;
		public PrintTest()
		{
			this.InitializeComponent();
			this.path = System.IO.Path.GetTempPath() + "KBW\\";
			ToolTip a = new ToolTip();
			a.SetToolTip(this.textBox1, "aaaa " + '\n'.ToString() + "bb");
		}
		private void button1_Click(object sender, System.EventArgs e)
		{
			string keyName = "Software\\Microsoft\\Internet Explorer\\PageSetup";
			using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(keyName, true))
			{
				if (key != null)
				{
					object old_footer = key.GetValue("footer");
					object old_header = key.GetValue("header");
					key.SetValue("footer", "");
					key.SetValue("header", "");
					key.SetValue("margin_top", "0.75");
					key.SetValue("margin_right", "0.75");
					key.SetValue("margin_bottom", "0.75");
					key.SetValue("margin_left", "0.75");
					this.webBrowser1.ShowPrintDialog();
				}
			}
		}
		public static void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			WebBrowser wb = (WebBrowser)sender;
			if (wb.ReadyState.Equals(WebBrowserReadyState.Complete))
			{
				try
				{
					string keyName = "Software\\Microsoft\\Internet Explorer\\PageSetup";
					using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(keyName, true))
					{
						if (key != null)
						{
							object old_footer = key.GetValue("footer");
							object old_header = key.GetValue("header");
							key.SetValue("footer", "");
							key.SetValue("header", "");
							key.SetValue("margin_top", "0.75");
							key.SetValue("margin_right", "0.75");
							key.SetValue("margin_bottom", "0.75");
							key.SetValue("margin_left", "0.75");
						}
					}
				}
				catch (System.Exception ex)
				{
					MessageBox.Show("Usuniecie standarodwej stopki i nagłówka nie powiodło się. " + ex.Message, "Uwaga");
				}
				wb.ShowPrintDialog();
			}
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.webBrowser1 = new WebBrowser();
			this.button1 = new Button();
			this.pictureBox1 = new PictureBox();
			this.textBox1 = new TextBox();
			this.toolTip1 = new ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
			base.SuspendLayout();
			this.webBrowser1.Location = new System.Drawing.Point(12, 12);
			this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowser1.Name = "webBrowser1";
			this.webBrowser1.Size = new System.Drawing.Size(653, 444);
			this.webBrowser1.TabIndex = 0;
			this.button1.Location = new System.Drawing.Point(687, 99);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(196, 106);
			this.button1.TabIndex = 1;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			this.pictureBox1.BackColor = System.Drawing.Color.White;
			this.pictureBox1.Location = new System.Drawing.Point(12, 462);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(653, 114);
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			this.textBox1.Location = new System.Drawing.Point(737, 322);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(100, 75);
			this.textBox1.TabIndex = 3;
			this.textBox1.Text = "jrdyidsy";
			this.toolTip1.ToolTipTitle = "aaa /n bbb";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(929, 582);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.pictureBox1);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.webBrowser1);
			base.Name = "PrintTest";
			this.Text = "PrintTest";
			((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
