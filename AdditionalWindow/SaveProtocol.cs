// Decompiled with JetBrains decompiler
// Type: Kalkulator1.SaveProtocol
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Kalkulator1
{
  public class SaveProtocol : Form
  {
    private IContainer components = (IContainer) null;
    private string savePath;
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

    private void save_Click(object sender, EventArgs e)
    {
      try
      {
        StreamReader streamReader = new StreamReader(this.savePath);
        string str = streamReader.ReadToEnd();
        streamReader.Close();
        string[] strArray = this.savePath.Split('\\');
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.FileName = strArray[strArray.Length - 1];
        if (saveFileDialog.ShowDialog() == DialogResult.Cancel || !saveFileDialog.CheckPathExists)
          return;
        string fileName = saveFileDialog.FileName;
        if (fileName != null && fileName != "" && fileName != strArray[strArray.Length - 1])
        {
          StreamWriter streamWriter = new StreamWriter(fileName, false);
          streamWriter.Write(str);
          streamWriter.Close();
          if (MessageBox.Show("Plik został poprawnie zapisany", "Komunikat") == DialogResult.OK)
            this.Close();
        }
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.Message, "Uwaga");
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
      this.isSend = new Label();
      this.label1 = new Label();
      this.filepath = new TextBox();
      this.save = new Button();
      this.SuspendLayout();
      this.isSend.AutoSize = true;
      this.isSend.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte) 238);
      this.isSend.Location = new Point(15, 19);
      this.isSend.MaximumSize = new Size(400, 0);
      this.isSend.Name = "isSend";
      this.isSend.Size = new Size(329, 15);
      this.isSend.TabIndex = 0;
      this.isSend.Text = "Szablon: Nie udało się wysłac protokołu na serwer";
      this.label1.AutoSize = true;
      this.label1.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 238);
      this.label1.Location = new Point(12, 53);
      this.label1.Name = "label1";
      this.label1.Size = new Size(224, 15);
      this.label1.TabIndex = 1;
      this.label1.Text = "Wybierz miejsce gdzie zapisać protokół:";
      this.filepath.Location = new Point(15, 83);
      this.filepath.Name = "filepath";
      this.filepath.Size = new Size(362, 20);
      this.filepath.TabIndex = 2;
      this.save.Location = new Point(383, 81);
      this.save.Name = "save";
      this.save.Size = new Size(75, 23);
      this.save.TabIndex = 3;
      this.save.Text = "Przeglądaj";
      this.save.UseVisualStyleBackColor = true;
      this.save.Click += new EventHandler(this.save_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(477, 124);
      this.Controls.Add((Control) this.save);
      this.Controls.Add((Control) this.filepath);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.isSend);
      this.Name = "SaveProtocol";
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Zapisywanie protokołu";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
