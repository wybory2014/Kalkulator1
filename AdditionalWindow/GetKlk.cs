// Decompiled with JetBrains decompiler
// Type: Kalkulator1.GetKlk
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace Kalkulator1
{
  public class GetKlk : Form
  {
    private IContainer components = (IContainer) null;
    private string pathRoot;
    private string electoralEampaign;
    private string jns;
    private string role;
    private string obw;
    private string path;
    private int countDefLoaded;
    private int countDef;
    private Panel panel1;
    private Button openHeader;
    private TextBox textBox1;
    private Label label1;
    private Panel panel2;

    public GetKlk()
    {
      this.InitializeComponent();
      this.pathRoot = "";
      this.electoralEampaign = "";
      this.jns = "";
      this.role = "";
      this.obw = "";
      this.countDefLoaded = 0;
      this.countDef = 0;
      this.path = Path.GetTempPath() + "KBW";
    }

    public GetKlk(string path, string electoralEampaign, string jns, string role)
    {
      this.InitializeComponent();
      this.pathRoot = path;
      this.electoralEampaign = electoralEampaign;
      this.jns = jns;
      this.role = role;
      this.obw = "";
      this.countDefLoaded = 0;
      this.countDef = 0;
      this.label1.Text = "Podaj definicje akcji wyborczej " + electoralEampaign.Replace('_', '/') + "-" + jns;
      this.panel2.Visible = false;
      this.path = Path.GetTempPath() + "KBW";
    }

    public GetKlk(string path, string electoralEampaign, string jns, string role, string obw)
    {
      this.InitializeComponent();
      this.pathRoot = path;
      this.electoralEampaign = electoralEampaign;
      this.jns = jns;
      this.role = role;
      this.obw = obw;
      this.countDefLoaded = 0;
      this.countDef = 0;
      this.label1.Text = "Podaj definicje akcji wyborczej " + electoralEampaign.Replace('_', '/') + "-" + jns;
      this.panel2.Visible = false;
      this.path = Path.GetTempPath() + "KBW";
    }

    private void openHeader_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      if (openFileDialog.ShowDialog() == DialogResult.OK)
        this.textBox1.Text = openFileDialog.FileName;
      try
      {
        StreamReader streamReader = new StreamReader(this.textBox1.Text);
        string str = streamReader.ReadToEnd();
        streamReader.Close();
        if (str != "")
        {
          if (!Directory.Exists(this.pathRoot + "\\electoralEampaign"))
          {
            try
            {
              Directory.CreateDirectory(this.pathRoot + "\\electoralEampaign");
            }
            catch (ArgumentNullException ex)
            {
              int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
            }
            catch (ArgumentException ex)
            {
              int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
            }
            catch (UnauthorizedAccessException ex)
            {
              int num = (int) MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
            }
            catch (PathTooLongException ex)
            {
              int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
            }
            catch (DirectoryNotFoundException ex)
            {
              int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
            }
            catch (NotSupportedException ex)
            {
              int num = (int) MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
            }
            catch (IOException ex)
            {
              int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
            }
          }
          try
          {
            StreamWriter streamWriter = new StreamWriter(this.pathRoot + "\\electoralEampaign\\" + this.electoralEampaign + "-" + this.jns + ".xml", false);
            streamWriter.Write(str);
            streamWriter.Close();
            this.generateSecondWindow();
          }
          catch (UnauthorizedAccessException ex)
          {
            int num = (int) MessageBox.Show("Nie masz uprawnień do zapisania pliku definicyjnego. Otwórz aplikacje jako adnimistrator.", "Uwaga");
          }
          catch (Exception ex)
          {
            int num = (int) MessageBox.Show("Error: Nie można zapisać pliku definicyjnego. \n Original error: " + ex.Message);
          }
        }
        else
        {
          int num1 = (int) MessageBox.Show("Error: Podano pusty plik");
        }
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Error: Nie można odczytać pliku z dysku. \n Original error: " + ex.Message);
      }
    }

    private void generateSecondWindow()
    {
      this.panel1.Visible = false;
      this.panel2.Location = new Point(0, 0);
      this.panel2.Visible = true;
      if (!Directory.Exists(this.path + "\\electoralEampaign"))
        return;
      List<string> list1 = new List<string>();
      List<string> list2 = new List<string>();
      List<string> list3 = new List<string>();
      List<string> list4 = new List<string>();
      if (File.Exists(this.path + "\\electoralEampaign\\" + this.electoralEampaign + "-" + this.jns + ".xml"))
      {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(this.pathRoot + "\\electoralEampaign\\" + this.electoralEampaign + "-" + this.jns + ".xml");
        try
        {
          int y1 = 10;
          string str1 = "";
          string str2 = "";
          foreach (XmlNode xmlNode1 in xmlDocument.SelectSingleNode("/akcja_wyborcza/jns"))
          {
            if (xmlNode1.Name == "obw")
            {
              XmlNode namedItem1 = xmlNode1.Attributes.GetNamedItem("nr");
              if (namedItem1 != null)
                str1 = namedItem1.Value;
              if (this.role == "O" || this.role == "P" && str1 == this.obw)
              {
                if (str1 != "")
                {
                  string str3 = this.electoralEampaign.Replace('_', '/') + "-" + this.jns + "-" + str1;
                  bool flag = true;
                  for (int index = 0; index < list4.Count; ++index)
                  {
                    if (list4[index] == str3)
                    {
                      flag = false;
                      break;
                    }
                  }
                  if (flag)
                  {
                    list4.Add(str3);
                    Label label = new Label();
                    TextBox textBox = new TextBox();
                    Button button = new Button();
                    label.AutoSize = true;
                    label.MaximumSize = new Size(this.panel2.Width, 0);
                    textBox.Size = new Size(482, 20);
                    button.Size = new Size(75, 23);
                    label.Text = "Podaj definicje komisji " + str3;
                    button.Text = "Otwórz";
                    textBox.Name = "input" + this.electoralEampaign + "-" + this.jns + "-" + str1;
                    button.Name = this.electoralEampaign + "-" + this.jns + "-" + str1;
                    button.Click += new EventHandler(this.open);
                    label.Location = new Point(14, y1);
                    int y2 = label.Height <= 23 ? y1 + 27 : y1 + (label.Height + 5);
                    textBox.Location = new Point(17, y2);
                    button.Location = new Point(505, y2);
                    y1 = y2 + 33;
                    this.panel2.Controls.Add((Control) label);
                    this.panel2.Controls.Add((Control) textBox);
                    this.panel2.Controls.Add((Control) button);
                    ++this.countDef;
                  }
                }
                foreach (XmlNode xmlNode2 in xmlNode1)
                {
                  if (xmlNode2.Name == "inst")
                  {
                    XmlNode namedItem2 = xmlNode2.Attributes.GetNamedItem("kod");
                    if (namedItem2 != null)
                      str2 = namedItem2.Value;
                    if (str2 != "")
                    {
                      string str3 = this.electoralEampaign.Replace('_', '/') + "-" + str2 + ".xml";
                      bool flag1 = true;
                      for (int index = 0; index < list1.Count; ++index)
                      {
                        if (list1[index] == str3)
                        {
                          flag1 = false;
                          break;
                        }
                      }
                      if (flag1)
                      {
                        list1.Add(str3);
                        Label label = new Label();
                        TextBox textBox = new TextBox();
                        Button button = new Button();
                        label.AutoSize = true;
                        label.MaximumSize = new Size(this.panel2.Width, 0);
                        textBox.Size = new Size(482, 20);
                        button.Size = new Size(75, 23);
                        label.Text = "Podaj definicje wyglądu protokołu " + str3;
                        button.Text = "Otwórz";
                        textBox.Name = "input" + this.electoralEampaign + "-" + str2;
                        button.Name = this.electoralEampaign + "-" + str2;
                        button.Click += new EventHandler(this.open);
                        label.Location = new Point(14, y1);
                        if (label.Height > 23)
                          y1 += label.Height + 5;
                        else
                          y1 += 27;
                        textBox.Location = new Point(17, y1);
                        button.Location = new Point(505, y1);
                        y1 += 33;
                        this.panel2.Controls.Add((Control) label);
                        this.panel2.Controls.Add((Control) textBox);
                        this.panel2.Controls.Add((Control) button);
                        ++this.countDef;
                      }
                      string str4 = this.electoralEampaign.Replace('_', '/') + "-" + str2 + "_Walidacja.xml";
                      bool flag2 = true;
                      for (int index = 0; index < list2.Count; ++index)
                      {
                        if (list2[index] == str4)
                        {
                          flag2 = false;
                          break;
                        }
                      }
                      if (flag2)
                      {
                        list2.Add(str4);
                        Label label = new Label();
                        TextBox textBox = new TextBox();
                        Button button = new Button();
                        label.AutoSize = true;
                        label.MaximumSize = new Size(this.panel2.Width, 0);
                        textBox.Size = new Size(482, 20);
                        button.Size = new Size(75, 23);
                        label.Text = "Podaj definicje walidacja " + str4;
                        button.Text = "Otwórz";
                        textBox.Name = "input" + this.electoralEampaign + "-" + str2 + "_Walidacja";
                        button.Name = this.electoralEampaign + "-" + str2 + "_Walidacja";
                        button.Click += new EventHandler(this.open);
                        label.Location = new Point(14, y1);
                        if (label.Height > 23)
                          y1 += label.Height + 5;
                        else
                          y1 += 27;
                        textBox.Location = new Point(17, y1);
                        button.Location = new Point(505, y1);
                        y1 += 33;
                        this.panel2.Controls.Add((Control) label);
                        this.panel2.Controls.Add((Control) textBox);
                        this.panel2.Controls.Add((Control) button);
                        ++this.countDef;
                      }
                    }
                    foreach (XmlNode xmlNode3 in xmlNode2)
                    {
                      if (xmlNode3.Name == "okr")
                      {
                        XmlNode namedItem3 = xmlNode3.Attributes.GetNamedItem("nr");
                        if (namedItem3 != null && str2 != "" && namedItem3.Value != "")
                        {
                          string str3 = this.electoralEampaign.Replace('_', '/') + "-" + str2 + "-" + this.jns + "-" + namedItem3.Value + ".xml";
                          bool flag = true;
                          for (int index = 0; index < list4.Count; ++index)
                          {
                            if (list4[index] == str3)
                            {
                              flag = false;
                              break;
                            }
                          }
                          if (flag)
                          {
                            list4.Add(str3);
                            Label label = new Label();
                            TextBox textBox = new TextBox();
                            Button button = new Button();
                            label.AutoSize = true;
                            label.MaximumSize = new Size(this.panel2.Width, 0);
                            textBox.Size = new Size(482, 20);
                            button.Size = new Size(75, 23);
                            label.Text = "Podaj definicje kandydatow " + str3;
                            button.Text = "Otwórz";
                            textBox.Name = "input" + this.electoralEampaign + "-" + str2 + "-" + this.jns + "-" + namedItem3.Value;
                            button.Name = this.electoralEampaign + "-" + str2 + "-" + this.jns + "-" + namedItem3.Value;
                            button.Click += new EventHandler(this.open);
                            label.Location = new Point(14, y1);
                            if (label.Height > 23)
                              y1 += label.Height + 5;
                            else
                              y1 += 27;
                            textBox.Location = new Point(17, y1);
                            button.Location = new Point(505, y1);
                            y1 += 33;
                            this.panel2.Controls.Add((Control) label);
                            this.panel2.Controls.Add((Control) textBox);
                            this.panel2.Controls.Add((Control) button);
                            ++this.countDef;
                          }
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }
        catch (XmlException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowy XML", "Error");
        }
        catch (NullReferenceException ex)
        {
          int num = (int) MessageBox.Show("Podanno inny xml niz header", "Error");
        }
      }
    }

    private void open(object sender, EventArgs e)
    {
      string name = (sender as Button).Name;
      OpenFileDialog openFileDialog = new OpenFileDialog();
      if (openFileDialog.ShowDialog() == DialogResult.OK)
        this.panel2.Controls["input" + name].Text = openFileDialog.FileName;
      try
      {
        StreamReader streamReader = new StreamReader(this.panel2.Controls["input" + name].Text);
        string str = streamReader.ReadToEnd();
        streamReader.Close();
        if (str != "")
        {
          if (!Directory.Exists(this.path + "\\ProtocolsDef"))
          {
            try
            {
              Directory.CreateDirectory(this.path + "\\ProtocolsDef");
            }
            catch (ArgumentNullException ex)
            {
              int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
            }
            catch (ArgumentException ex)
            {
              int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
            }
            catch (UnauthorizedAccessException ex)
            {
              int num = (int) MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
            }
            catch (PathTooLongException ex)
            {
              int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
            }
            catch (DirectoryNotFoundException ex)
            {
              int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
            }
            catch (NotSupportedException ex)
            {
              int num = (int) MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
            }
            catch (IOException ex)
            {
              int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
            }
          }
          try
          {
            StreamWriter streamWriter = new StreamWriter(this.pathRoot + "\\ProtocolsDef\\" + name + ".xml", false);
            streamWriter.Write(str);
            streamWriter.Close();
            ++this.countDefLoaded;
            if (this.countDef != this.countDefLoaded)
              return;
            this.Close();
          }
          catch (UnauthorizedAccessException ex)
          {
            int num = (int) MessageBox.Show("Nie masz uprawnień do zapisania pliku definicyjnego. Otwórz aplikacje jako adnimistrator.", "Uwaga");
          }
          catch (Exception ex)
          {
            int num = (int) MessageBox.Show("Error: Nie można zapisać pliku definicyjnego. \n Original error: " + ex.Message);
          }
        }
        else
        {
          int num1 = (int) MessageBox.Show("Error: Podano pusty plik");
        }
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Error: Nie można odczytać pliku z dysku. \n Original error: " + ex.Message);
      }
    }

    private void GetKlk_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.countDefLoaded >= this.countDef || MessageBox.Show("Nie wprowadzono wszystkich plikow definicyjnych. Czy napewno zamknąć okno?", "Uwaga", MessageBoxButtons.YesNo) != DialogResult.No)
        return;
      e.Cancel = true;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.panel1 = new Panel();
      this.openHeader = new Button();
      this.textBox1 = new TextBox();
      this.label1 = new Label();
      this.panel2 = new Panel();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      this.panel1.Controls.Add((Control) this.openHeader);
      this.panel1.Controls.Add((Control) this.textBox1);
      this.panel1.Controls.Add((Control) this.label1);
      this.panel1.Location = new Point(13, 13);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(585, 73);
      this.panel1.TabIndex = 0;
      this.openHeader.Location = new Point(505, 26);
      this.openHeader.Name = "openHeader";
      this.openHeader.Size = new Size(75, 23);
      this.openHeader.TabIndex = 2;
      this.openHeader.Text = "Otwórz";
      this.openHeader.UseVisualStyleBackColor = true;
      this.openHeader.Click += new EventHandler(this.openHeader_Click);
      this.textBox1.Location = new Point(17, 29);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new Size(482, 20);
      this.textBox1.TabIndex = 1;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(14, 5);
      this.label1.Name = "label1";
      this.label1.Size = new Size(35, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "label1";
      this.panel2.AutoScroll = true;
      this.panel2.AutoSize = true;
      this.panel2.Location = new Point(13, 93);
      this.panel2.Name = "panel2";
      this.panel2.Size = new Size(585, 239);
      this.panel2.TabIndex = 1;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.AutoScroll = true;
      this.ClientSize = new Size(610, 344);
      this.Controls.Add((Control) this.panel2);
      this.Controls.Add((Control) this.panel1);
      this.Name = "GetKlk";
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Podaj pliki klk";
      this.FormClosing += new FormClosingEventHandler(this.GetKlk_FormClosing);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
