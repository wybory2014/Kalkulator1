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
		private string pathRoot;
		private string electoralEampaign;
		private string jns;
		private string role;
		private string obw;
		private string path;
		private int countDefLoaded;
		private int countDef;
		private System.ComponentModel.IContainer components = null;
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
			this.path = System.IO.Path.GetTempPath() + "KBW";
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
			string akcja = electoralEampaign.Replace('_', '/') + "-" + jns;
			this.label1.Text = "Podaj definicje akcji wyborczej " + akcja;
			this.panel2.Visible = false;
			this.path = System.IO.Path.GetTempPath() + "KBW";
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
			string akcja = electoralEampaign.Replace('_', '/') + "-" + jns;
			this.label1.Text = "Podaj definicje akcji wyborczej " + akcja;
			this.panel2.Visible = false;
			this.path = System.IO.Path.GetTempPath() + "KBW";
		}
		private void openHeader_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog wnd = new OpenFileDialog();
			if (wnd.ShowDialog() == DialogResult.OK)
			{
				this.textBox1.Text = wnd.FileName;
			}
			try
			{
				string file = "";
				System.IO.StreamReader sr = new System.IO.StreamReader(this.textBox1.Text);
				file = sr.ReadToEnd();
				sr.Close();
				if (file != "")
				{
					if (!System.IO.Directory.Exists(this.pathRoot + "\\electoralEampaign"))
					{
						try
						{
							System.IO.Directory.CreateDirectory(this.pathRoot + "\\electoralEampaign");
						}
						catch (System.ArgumentNullException)
						{
							MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
						}
						catch (System.ArgumentException)
						{
							MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
						}
						catch (System.UnauthorizedAccessException)
						{
							MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
						}
						catch (System.IO.PathTooLongException)
						{
							MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
						}
						catch (System.IO.DirectoryNotFoundException)
						{
							MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
						}
						catch (System.NotSupportedException)
						{
							MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
						}
						catch (System.IO.IOException)
						{
							MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"electoralEampaign\"", "Error");
						}
					}
					try
					{
						System.IO.StreamWriter sw = new System.IO.StreamWriter(string.Concat(new string[]
						{
							this.pathRoot,
							"\\electoralEampaign\\",
							this.electoralEampaign,
							"-",
							this.jns,
							".xml"
						}), false);
						sw.Write(file);
						sw.Close();
						this.generateSecondWindow();
					}
					catch (System.UnauthorizedAccessException)
					{
						MessageBox.Show("Nie masz uprawnień do zapisania pliku definicyjnego. Otwórz aplikacje jako adnimistrator.", "Uwaga");
					}
					catch (System.Exception ex)
					{
						MessageBox.Show("Error: Nie można zapisać pliku definicyjnego. \n Original error: " + ex.Message);
					}
				}
				else
				{
					MessageBox.Show("Error: Podano pusty plik");
				}
			}
			catch (System.Exception ex)
			{
				MessageBox.Show("Error: Nie można odczytać pliku z dysku. \n Original error: " + ex.Message);
			}
		}
		private void generateSecondWindow()
		{
			this.panel1.Visible = false;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Visible = true;
			if (System.IO.Directory.Exists(this.path + "\\electoralEampaign"))
			{
				System.Collections.Generic.List<string> requestDesign = new System.Collections.Generic.List<string>();
				System.Collections.Generic.List<string> requestWalid = new System.Collections.Generic.List<string>();
				System.Collections.Generic.List<string> requestCandidate = new System.Collections.Generic.List<string>();
				System.Collections.Generic.List<string> requestComittee = new System.Collections.Generic.List<string>();
				if (System.IO.File.Exists(string.Concat(new string[]
				{
					this.path,
					"\\electoralEampaign\\",
					this.electoralEampaign,
					"-",
					this.jns,
					".xml"
				})))
				{
					XmlDocument header = new XmlDocument();
					header.Load(string.Concat(new string[]
					{
						this.pathRoot,
						"\\electoralEampaign\\",
						this.electoralEampaign,
						"-",
						this.jns,
						".xml"
					}));
					try
					{
						int y = 10;
						string obwRead = "";
						string instkod = "";
						XmlNode headerRoot = header.SelectSingleNode("/akcja_wyborcza/jns");
						foreach (XmlNode item in headerRoot)
						{
							if (item.Name == "obw")
							{
								XmlNode nr = item.Attributes.GetNamedItem("nr");
								if (nr != null)
								{
									obwRead = nr.Value;
								}
								if (this.role == "O" || (this.role == "P" && obwRead == this.obw))
								{
									if (obwRead != "")
									{
										string uri = string.Concat(new string[]
										{
											this.electoralEampaign.Replace('_', '/'),
											"-",
											this.jns,
											"-",
											obwRead
										});
										bool continueC = true;
										for (int i = 0; i < requestComittee.Count; i++)
										{
											if (requestComittee[i] == uri)
											{
												continueC = false;
												break;
											}
										}
										if (continueC)
										{
											requestComittee.Add(uri);
											Label lab = new Label();
											TextBox input = new TextBox();
											Button btn = new Button();
											lab.AutoSize = true;
											lab.MaximumSize = new System.Drawing.Size(this.panel2.Width, 0);
											input.Size = new System.Drawing.Size(482, 20);
											btn.Size = new System.Drawing.Size(75, 23);
											lab.Text = "Podaj definicje komisji " + uri;
											btn.Text = "Otwórz";
											input.Name = string.Concat(new string[]
											{
												"input",
												this.electoralEampaign,
												"-",
												this.jns,
												"-",
												obwRead
											});
											btn.Name = string.Concat(new string[]
											{
												this.electoralEampaign,
												"-",
												this.jns,
												"-",
												obwRead
											});
											btn.Click += new System.EventHandler(this.open);
											lab.Location = new System.Drawing.Point(14, y);
											if (lab.Height > 23)
											{
												y += lab.Height + 5;
											}
											else
											{
												y += 27;
											}
											input.Location = new System.Drawing.Point(17, y);
											btn.Location = new System.Drawing.Point(505, y);
											y += 33;
											this.panel2.Controls.Add(lab);
											this.panel2.Controls.Add(input);
											this.panel2.Controls.Add(btn);
											this.countDef++;
										}
									}
									foreach (XmlNode inst in item)
									{
										if (inst.Name == "inst")
										{
											XmlNode kod = inst.Attributes.GetNamedItem("kod");
											if (kod != null)
											{
												instkod = kod.Value;
											}
											if (instkod != "")
											{
												string uri = this.electoralEampaign.Replace('_', '/') + "-" + instkod + ".xml";
												bool continueDesign = true;
												for (int i = 0; i < requestDesign.Count; i++)
												{
													if (requestDesign[i] == uri)
													{
														continueDesign = false;
														break;
													}
												}
												if (continueDesign)
												{
													requestDesign.Add(uri);
													Label lab = new Label();
													TextBox input = new TextBox();
													Button btn = new Button();
													lab.AutoSize = true;
													lab.MaximumSize = new System.Drawing.Size(this.panel2.Width, 0);
													input.Size = new System.Drawing.Size(482, 20);
													btn.Size = new System.Drawing.Size(75, 23);
													lab.Text = "Podaj definicje wyglądu protokołu " + uri;
													btn.Text = "Otwórz";
													input.Name = "input" + this.electoralEampaign + "-" + instkod;
													btn.Name = this.electoralEampaign + "-" + instkod;
													btn.Click += new System.EventHandler(this.open);
													lab.Location = new System.Drawing.Point(14, y);
													if (lab.Height > 23)
													{
														y += lab.Height + 5;
													}
													else
													{
														y += 27;
													}
													input.Location = new System.Drawing.Point(17, y);
													btn.Location = new System.Drawing.Point(505, y);
													y += 33;
													this.panel2.Controls.Add(lab);
													this.panel2.Controls.Add(input);
													this.panel2.Controls.Add(btn);
													this.countDef++;
												}
												uri = this.electoralEampaign.Replace('_', '/') + "-" + instkod + "_Walidacja.xml";
												bool continueWalid = true;
												for (int i = 0; i < requestWalid.Count; i++)
												{
													if (requestWalid[i] == uri)
													{
														continueWalid = false;
														break;
													}
												}
												if (continueWalid)
												{
													requestWalid.Add(uri);
													Label lab2 = new Label();
													TextBox input2 = new TextBox();
													Button btn2 = new Button();
													lab2.AutoSize = true;
													lab2.MaximumSize = new System.Drawing.Size(this.panel2.Width, 0);
													input2.Size = new System.Drawing.Size(482, 20);
													btn2.Size = new System.Drawing.Size(75, 23);
													lab2.Text = "Podaj definicje walidacja " + uri;
													btn2.Text = "Otwórz";
													input2.Name = string.Concat(new string[]
													{
														"input",
														this.electoralEampaign,
														"-",
														instkod,
														"_Walidacja"
													});
													btn2.Name = this.electoralEampaign + "-" + instkod + "_Walidacja";
													btn2.Click += new System.EventHandler(this.open);
													lab2.Location = new System.Drawing.Point(14, y);
													if (lab2.Height > 23)
													{
														y += lab2.Height + 5;
													}
													else
													{
														y += 27;
													}
													input2.Location = new System.Drawing.Point(17, y);
													btn2.Location = new System.Drawing.Point(505, y);
													y += 33;
													this.panel2.Controls.Add(lab2);
													this.panel2.Controls.Add(input2);
													this.panel2.Controls.Add(btn2);
													this.countDef++;
												}
											}
											foreach (XmlNode okr in inst)
											{
												if (okr.Name == "okr")
												{
													XmlNode okrNr = okr.Attributes.GetNamedItem("nr");
													if (okrNr != null && instkod != "" && okrNr.Value != "")
													{
														string uri = string.Concat(new string[]
														{
															this.electoralEampaign.Replace('_', '/'),
															"-",
															instkod,
															"-",
															this.jns,
															"-",
															okrNr.Value,
															".xml"
														});
														bool continueC = true;
														for (int i = 0; i < requestComittee.Count; i++)
														{
															if (requestComittee[i] == uri)
															{
																continueC = false;
																break;
															}
														}
														if (continueC)
														{
															requestComittee.Add(uri);
															Label lab = new Label();
															TextBox input = new TextBox();
															Button btn = new Button();
															lab.AutoSize = true;
															lab.MaximumSize = new System.Drawing.Size(this.panel2.Width, 0);
															input.Size = new System.Drawing.Size(482, 20);
															btn.Size = new System.Drawing.Size(75, 23);
															lab.Text = "Podaj definicje kandydatow " + uri;
															btn.Text = "Otwórz";
															input.Name = string.Concat(new string[]
															{
																"input",
																this.electoralEampaign,
																"-",
																instkod,
																"-",
																this.jns,
																"-",
																okrNr.Value
															});
															btn.Name = string.Concat(new string[]
															{
																this.electoralEampaign,
																"-",
																instkod,
																"-",
																this.jns,
																"-",
																okrNr.Value
															});
															btn.Click += new System.EventHandler(this.open);
															lab.Location = new System.Drawing.Point(14, y);
															if (lab.Height > 23)
															{
																y += lab.Height + 5;
															}
															else
															{
																y += 27;
															}
															input.Location = new System.Drawing.Point(17, y);
															btn.Location = new System.Drawing.Point(505, y);
															y += 33;
															this.panel2.Controls.Add(lab);
															this.panel2.Controls.Add(input);
															this.panel2.Controls.Add(btn);
															this.countDef++;
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
					catch (XmlException)
					{
						MessageBox.Show("Nieprawidłowy XML", "Error");
					}
					catch (System.NullReferenceException)
					{
						MessageBox.Show("Podanno inny xml niz header", "Error");
					}
				}
			}
		}
		private void open(object sender, System.EventArgs e)
		{
			string a = (sender as Button).Name;
			OpenFileDialog wnd = new OpenFileDialog();
			if (wnd.ShowDialog() == DialogResult.OK)
			{
				this.panel2.Controls["input" + a].Text = wnd.FileName;
			}
			try
			{
				string file = "";
				System.IO.StreamReader sr = new System.IO.StreamReader(this.panel2.Controls["input" + a].Text);
				file = sr.ReadToEnd();
				sr.Close();
				if (file != "")
				{
					if (!System.IO.Directory.Exists(this.path + "\\ProtocolsDef"))
					{
						try
						{
							System.IO.Directory.CreateDirectory(this.path + "\\ProtocolsDef");
						}
						catch (System.ArgumentNullException)
						{
							MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
						}
						catch (System.ArgumentException)
						{
							MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
						}
						catch (System.UnauthorizedAccessException)
						{
							MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
						}
						catch (System.IO.PathTooLongException)
						{
							MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
						}
						catch (System.IO.DirectoryNotFoundException)
						{
							MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
						}
						catch (System.NotSupportedException)
						{
							MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"ProtocolsDef\"", "Error");
						}
						catch (System.IO.IOException)
						{
							MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
						}
					}
					try
					{
						System.IO.StreamWriter sw = new System.IO.StreamWriter(this.pathRoot + "\\ProtocolsDef\\" + a + ".xml", false);
						sw.Write(file);
						sw.Close();
						this.countDefLoaded++;
						if (this.countDef == this.countDefLoaded)
						{
							base.Close();
						}
					}
					catch (System.UnauthorizedAccessException)
					{
						MessageBox.Show("Nie masz uprawnień do zapisania pliku definicyjnego. Otwórz aplikacje jako adnimistrator.", "Uwaga");
					}
					catch (System.Exception ex)
					{
						MessageBox.Show("Error: Nie można zapisać pliku definicyjnego. \n Original error: " + ex.Message);
					}
				}
				else
				{
					MessageBox.Show("Error: Podano pusty plik");
				}
			}
			catch (System.Exception ex)
			{
				MessageBox.Show("Error: Nie można odczytać pliku z dysku. \n Original error: " + ex.Message);
			}
		}
		private void GetKlk_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.countDefLoaded < this.countDef)
			{
				if (MessageBox.Show("Nie wprowadzono wszystkich plikow definicyjnych. Czy napewno zamknąć okno?", "Uwaga", MessageBoxButtons.YesNo) == DialogResult.No)
				{
					e.Cancel = true;
				}
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
			this.panel1 = new Panel();
			this.openHeader = new Button();
			this.textBox1 = new TextBox();
			this.label1 = new Label();
			this.panel2 = new Panel();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.panel1.Controls.Add(this.openHeader);
			this.panel1.Controls.Add(this.textBox1);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Location = new System.Drawing.Point(13, 13);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(585, 73);
			this.panel1.TabIndex = 0;
			this.openHeader.Location = new System.Drawing.Point(505, 26);
			this.openHeader.Name = "openHeader";
			this.openHeader.Size = new System.Drawing.Size(75, 23);
			this.openHeader.TabIndex = 2;
			this.openHeader.Text = "Otwórz";
			this.openHeader.UseVisualStyleBackColor = true;
			this.openHeader.Click += new System.EventHandler(this.openHeader_Click);
			this.textBox1.Location = new System.Drawing.Point(17, 29);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(482, 20);
			this.textBox1.TabIndex = 1;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "label1";
			this.panel2.AutoScroll = true;
			this.panel2.AutoSize = true;
			this.panel2.Location = new System.Drawing.Point(13, 93);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(585, 239);
			this.panel2.TabIndex = 1;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.AutoScroll = true;
			base.ClientSize = new System.Drawing.Size(610, 344);
			base.Controls.Add(this.panel2);
			base.Controls.Add(this.panel1);
			base.Name = "GetKlk";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Podaj pliki klk";
			base.FormClosing += new FormClosingEventHandler(this.GetKlk_FormClosing);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
