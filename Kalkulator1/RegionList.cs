using Kalkulator1.AdditionalClass;
using Kalkulator1.instalClass;
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
		private Connection con;
		private string jns;
		private bool exit;
		private string path;
		private string licensepath;
		public WaitPanel wait;
		private Start start;
		private System.ComponentModel.IContainer components = null;
		private DataGridView dataGridView1;
		public RegionList()
		{
			this.InitializeComponent();
			this.path = System.IO.Path.GetTempPath() + "KBW";
			this.con = new Connection();
			this.jns = "";
			this.wait = new WaitPanel("Wait2", base.Width, base.Height);
			base.Controls.Add(this.wait.getPanel());
			base.Controls[this.wait.getName()].BringToFront();
			this.start = null;
			this.licensepath = "";
		}
		public RegionList(Start start, string licensepath, string jns)
		{
			this.con = new Connection();
			this.path = System.IO.Path.GetTempPath() + "KBW";
			this.InitializeComponent();
			this.licensepath = licensepath;
			this.jns = jns;
			this.start = start;
			this.Text = this.Text + " (" + Kalkulator1.instalClass.Version.getVersion().ToString() + ")";
			this.wait = new WaitPanel("Wait99", base.Width, base.Height);
			base.Controls.Add(this.wait.getPanel());
			base.Controls[this.wait.getName()].BringToFront();
			this.exit = false;
			this.getRegions(true);
			if (this.exit)
			{
				base.Close();
			}
		}
		private void getRegions(bool old)
		{
			this.wait.setWaitPanel("Trwa pobieranie listy gmin", "Proszę czekać");
			if (old)
			{
				this.start.wait.setWaitPanel("Trwa pobieranie listy gmin", "Proszę czekać");
			}
			string server2 = "KALK/";
			string jnsTemp = this.jns;
			while (jnsTemp.Length < 6)
			{
				jnsTemp = "0" + jnsTemp;
			}
			string uri = server2 + "geopowgm-" + jnsTemp;
			ResponseData res = this.con.getRequestRegions(uri, 0);
			try
			{
				XmlDocument xml = new XmlDocument();
				xml.LoadXml(res.getXml());
				XmlNode root = xml.SelectSingleNode("/teryty");
				if (root.ChildNodes.Count > 1)
				{
					DataTable dt = new DataTable();
					this.dataGridView1.DataSource = dt;
					dt.Columns.Add(new DataColumn("Gmina", typeof(string)));
					dt.Columns.Add(new DataColumn("Kod", typeof(string)));
					foreach (XmlNode gmina in root)
					{
						DataRow dr = dt.NewRow();
						dr[0] = "";
						dr[1] = "";
						for (int i = 0; i < gmina.ChildNodes.Count; i++)
						{
							if (gmina.ChildNodes[i].Name == "jns_kod")
							{
								dr[1] = gmina.ChildNodes[i].InnerText;
							}
							if (gmina.ChildNodes[i].Name == "jns_nazwa")
							{
								dr[0] = gmina.ChildNodes[i].InnerText;
							}
						}
						dt.Rows.Add(dr);
					}
					this.dataGridView1.DataSource = dt;
					DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
					btn.HeaderText = "";
					btn.Text = "Przejdź";
					btn.Name = "enter";
					btn.UseColumnTextForButtonValue = true;
					if (this.dataGridView1.Columns["enter"] == null)
					{
						this.dataGridView1.Columns.Insert(2, btn);
					}
					else
					{
						this.dataGridView1.Columns["enter"].DisplayIndex = 2;
					}
					this.dataGridView1.Visible = true;
				}
				else
				{
					for (int i = 0; i < root.FirstChild.ChildNodes.Count; i++)
					{
						if (root.FirstChild.ChildNodes[i].Name == "jns_kod")
						{
							this.start.jns = root.FirstChild.ChildNodes[i].InnerText;
							this.exit = true;
							return;
						}
					}
				}
			}
			catch (System.Exception)
			{
				MessageBox.Show("Błąd pobrania listy gmin", "Bład");
			}
			this.wait.setVisible(false);
			if (old)
			{
				this.start.wait.setVisible(false);
			}
		}
		private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.ColumnIndex == this.dataGridView1.Columns["enter"].Index)
				{
					object jns = this.dataGridView1.Rows[e.RowIndex].Cells["Kod"].Value;
					if (jns != null)
					{
						this.start.jns = jns.ToString();
						base.Close();
					}
				}
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message, "Error");
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
			this.dataGridView1 = new DataGridView();
			((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
			base.SuspendLayout();
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
			this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(13, 12);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.Size = new System.Drawing.Size(470, 234);
			this.dataGridView1.TabIndex = 0;
			this.dataGridView1.CellClick += new DataGridViewCellEventHandler(this.dataGridView1_CellClick);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(495, 258);
			base.Controls.Add(this.dataGridView1);
			base.Name = "RegionList";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Lista gmin";
			((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
			base.ResumeLayout(false);
		}
	}
}
