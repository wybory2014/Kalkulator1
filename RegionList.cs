// Decompiled with JetBrains decompiler
// Type: Kalkulator1.RegionList
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

using Kalkulator1.AdditionalClass;
using Kalkulator1.ResponseClass;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace Kalkulator1
{
  public class RegionList : Form
  {
    private IContainer components = (IContainer) null;
    private Connection con;
    private string jns;
    private bool exit;
    private string path;
    private string licensepath;
    public WaitPanel wait;
    private Start start;
    private DataGridView dataGridView1;

    public RegionList()
    {
      this.InitializeComponent();
      this.path = Path.GetTempPath() + "KBW";
      this.con = new Connection();
      this.jns = "";
      this.wait = new WaitPanel("Wait2", this.Width, this.Height);
      this.Controls.Add((Control) this.wait.getPanel());
      this.Controls[this.wait.getName()].BringToFront();
      this.start = (Start) null;
      this.licensepath = "";
    }

    public RegionList(Start start, string licensepath, string jns)
    {
      this.con = new Connection();
      this.path = Path.GetTempPath() + "KBW";
      this.InitializeComponent();
      this.licensepath = licensepath;
      this.jns = jns;
      this.start = start;
      this.Text = this.Text + " (" + ((object) Kalkulator1.instalClass.Version.getVersion()).ToString() + ")";
      this.wait = new WaitPanel("Wait99", this.Width, this.Height);
      this.Controls.Add((Control) this.wait.getPanel());
      this.Controls[this.wait.getName()].BringToFront();
      this.exit = false;
      this.getRegions(true);
      if (!this.exit)
        return;
      this.Close();
    }

    private void getRegions(bool old)
    {
      this.wait.setWaitPanel("Trwa pobieranie listy gmin", "Proszę czekać");
      if (old)
        this.start.wait.setWaitPanel("Trwa pobieranie listy gmin", "Proszę czekać");
      string str;
      ResponseData requestRegions = this.con.getRequestRegions(str = "KALK/" + "geopowgm-" + this.jns, 0);
      try
      {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(requestRegions.getXml());
        XmlNode xmlNode1 = xmlDocument.SelectSingleNode("/teryty");
        if (xmlNode1.ChildNodes.Count > 1)
        {
          DataTable dataTable = new DataTable();
          this.dataGridView1.DataSource = (object) dataTable;
          dataTable.Columns.Add(new DataColumn("Gmina", typeof (string)));
          dataTable.Columns.Add(new DataColumn("Kod", typeof (string)));
          foreach (XmlNode xmlNode2 in xmlNode1)
          {
            DataRow row = dataTable.NewRow();
            row[0] = (object) "";
            row[1] = (object) "";
            for (int index = 0; index < xmlNode2.ChildNodes.Count; ++index)
            {
              if (xmlNode2.ChildNodes[index].Name == "jns_kod")
                row[1] = (object) xmlNode2.ChildNodes[index].InnerText;
              if (xmlNode2.ChildNodes[index].Name == "jns_nazwa")
                row[0] = (object) xmlNode2.ChildNodes[index].InnerText;
            }
            dataTable.Rows.Add(row);
          }
          this.dataGridView1.DataSource = (object) dataTable;
          DataGridViewButtonColumn viewButtonColumn = new DataGridViewButtonColumn();
          viewButtonColumn.HeaderText = "";
          viewButtonColumn.Text = "Przejdź";
          viewButtonColumn.Name = "enter";
          viewButtonColumn.UseColumnTextForButtonValue = true;
          if (this.dataGridView1.Columns["enter"] == null)
            this.dataGridView1.Columns.Insert(2, (DataGridViewColumn) viewButtonColumn);
          else
            this.dataGridView1.Columns["enter"].DisplayIndex = 2;
          this.dataGridView1.Visible = true;
        }
        else
        {
          for (int index = 0; index < xmlNode1.FirstChild.ChildNodes.Count; ++index)
          {
            if (xmlNode1.FirstChild.ChildNodes[index].Name == "jns_kod")
            {
              this.start.jns = xmlNode1.FirstChild.ChildNodes[index].InnerText;
              this.exit = true;
              return;
            }
          }
        }
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Błąd pobrania listy gmin", "Bład");
      }
      this.wait.setVisible(false);
      if (!old)
        return;
      this.start.wait.setVisible(false);
    }

    private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      try
      {
        if (e.ColumnIndex != this.dataGridView1.Columns["enter"].Index)
          return;
        object obj = this.dataGridView1.Rows[e.RowIndex].Cells["Kod"].Value;
        if (obj != null)
        {
          this.start.jns = obj.ToString();
          this.Close();
        }
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.Message, "Error");
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
      this.dataGridView1 = new DataGridView();
      ((ISupportInitialize) this.dataGridView1).BeginInit();
      this.SuspendLayout();
      this.dataGridView1.AllowUserToAddRows = false;
      this.dataGridView1.AllowUserToDeleteRows = false;
      this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
      this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Location = new Point(13, 12);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.ReadOnly = true;
      this.dataGridView1.Size = new Size(470, 234);
      this.dataGridView1.TabIndex = 0;
      this.dataGridView1.CellClick += new DataGridViewCellEventHandler(this.dataGridView1_CellClick);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(495, 258);
      this.Controls.Add((Control) this.dataGridView1);
      this.Name = "RegionList";
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Lista gmin";
      ((ISupportInitialize) this.dataGridView1).EndInit();
      this.ResumeLayout(false);
    }
  }
}
