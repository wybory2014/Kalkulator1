using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace Kalkulator1
{
	public class SaveProtocol : Form
	{
		private string savePath;
		private System.ComponentModel.IContainer components = null;
		private Label isSend;
		private Label label1;
		private TextBox filepath;
		private Button save;
		public SaveProtocol()
		{
			this.InitializeComponent();
			this.savePath = "";
		}
		public SaveProtocol(string statement, string savePath)
		{
			this.InitializeComponent();
			this.savePath = savePath;
			this.isSend.Text = statement;
		}
		private void save_Click(object sender, System.EventArgs e)
		{
			try
			{
				System.IO.StreamReader sr = new System.IO.StreamReader(this.savePath);
				string file = sr.ReadToEnd();
				sr.Close();
				string[] nameFile = this.savePath.Split(new char[]
				{
					'\\'
				});
				SaveFileDialog wnd = new SaveFileDialog();
				wnd.FileName = nameFile[nameFile.Length - 1];
				if (wnd.ShowDialog() != DialogResult.Cancel)
				{
					if (wnd.CheckPathExists)
					{
						string name = wnd.FileName;
						if (name != null && name != "" && name != nameFile[nameFile.Length - 1])
						{
							System.IO.StreamWriter sw = new System.IO.StreamWriter(name, false);
							sw.Write(file);
							sw.Close();
							if (MessageBox.Show("Plik został poprawnie zapisany", "Komunikat") == DialogResult.OK)
							{
								base.Close();
							}
						}
					}
				}
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message, "Uwaga");
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
			this.isSend = new Label();
			this.label1 = new Label();
			this.filepath = new TextBox();
			this.save = new Button();
			base.SuspendLayout();
			this.isSend.AutoSize = true;
			this.isSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
			this.isSend.Location = new System.Drawing.Point(15, 19);
			this.isSend.MaximumSize = new System.Drawing.Size(400, 0);
			this.isSend.Name = "isSend";
			this.isSend.Size = new System.Drawing.Size(329, 15);
			this.isSend.TabIndex = 0;
			this.isSend.Text = "Szablon: Nie udało się wysłac protokołu na serwer";
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
			this.label1.Location = new System.Drawing.Point(12, 53);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(224, 15);
			this.label1.TabIndex = 1;
			this.label1.Text = "Wybierz miejsce gdzie zapisać protokół:";
			this.filepath.Location = new System.Drawing.Point(15, 83);
			this.filepath.Name = "filepath";
			this.filepath.Size = new System.Drawing.Size(362, 20);
			this.filepath.TabIndex = 2;
			this.save.Location = new System.Drawing.Point(383, 81);
			this.save.Name = "save";
			this.save.Size = new System.Drawing.Size(75, 23);
			this.save.TabIndex = 3;
			this.save.Text = "Przeglądaj";
			this.save.UseVisualStyleBackColor = true;
			this.save.Click += new System.EventHandler(this.save_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(477, 124);
			base.Controls.Add(this.save);
			base.Controls.Add(this.filepath);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.isSend);
			base.Name = "SaveProtocol";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Zapisywanie protokołu";
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
