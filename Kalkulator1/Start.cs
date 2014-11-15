using Kalkulator1.AdditionalClass;
using Kalkulator1.instalClass;
using Kalkulator1.ResponseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;
namespace Kalkulator1
{
	public class Start : Form
	{
		private string path;
		private Certificate certificate;
		public bool logged;
		private Connection con;
		public string jns;
		public static System.Collections.Generic.List<string> listaPlikow = new System.Collections.Generic.List<string>();
		public WaitPanel wait;
		private System.ComponentModel.IContainer components = null;
		private Label label1;
		private Label label2;
		private Panel licensesPanel;
		private Panel waitLicensesPanel;
		private Panel panel3;
		private Button getLicenseFromNet;
		private Label label4;
		private Button getLicenseFromDisk;
		private Label label3;
		private DataGridView LicencesTable;
		private Panel panel1;
		private DataGridView waitLicenses;
		private Button btnDeleteTemp;
		public Start()
		{
			try
			{
				this.InitializeComponent();
				this.Text = "Kalkulator (" + Kalkulator1.instalClass.Version.getVersion().ToString() + ")";
				Start.listaPlikow.Add("START");
				this.path = System.IO.Path.GetTempPath() + "KBW";
				if (!System.IO.Directory.Exists(this.path))
				{
					try
					{
						System.IO.Directory.CreateDirectory(this.path);
					}
					catch (System.ArgumentNullException)
					{
						MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"KBW\"", "Error");
					}
					catch (System.ArgumentException)
					{
						MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"KBW\"", "Error");
					}
					catch (System.UnauthorizedAccessException)
					{
						MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
					}
					catch (System.IO.PathTooLongException)
					{
						MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"KBW\"", "Error");
					}
					catch (System.IO.DirectoryNotFoundException)
					{
						MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"KBW\"", "Error");
					}
					catch (System.NotSupportedException)
					{
						MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"KBW\"", "Error");
					}
					catch (System.IO.IOException)
					{
						MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"KBW\"", "Error");
					}
				}
				if (!System.IO.Directory.Exists(this.path + "\\tmp"))
				{
					Instal inst = new Instal(this.path);
				}
				if (System.IO.Directory.Exists(System.IO.Path.GetTempPath() + "KBW\\tmp"))
				{
					if (!System.IO.File.Exists(System.IO.Path.GetTempPath() + "KBW\\tmp\\eksport.xml"))
					{
						System.IO.File.Create(System.IO.Path.GetTempPath() + "KBW\\tmp\\eksport.xml");
					}
					if (!System.IO.File.Exists(System.IO.Path.GetTempPath() + "KBW\\tmp\\import.xml"))
					{
						System.IO.File.Create(System.IO.Path.GetTempPath() + "KBW\\tmp\\import.xml");
					}
					if (!System.IO.File.Exists(System.IO.Path.GetTempPath() + "KBW\\tmp\\eksportCode.xml"))
					{
						System.IO.File.Create(System.IO.Path.GetTempPath() + "KBW\\tmp\\eksportCode.xml");
					}
				}
				this.logged = false;
				this.certificate = new Certificate();
				this.con = new Connection();
				this.LicencesTable.CellClick += new DataGridViewCellEventHandler(this.LicencesTable_CellClick);
				this.waitLicenses.CellClick += new DataGridViewCellEventHandler(this.waitLicenses_CellClick);
				this.wait = new WaitPanel("Wait1", base.Width, base.Height);
				base.Controls.Add(this.wait.getPanel());
				base.Controls[this.wait.getName()].BringToFront();
				this.getActiveLicense();
				this.getOtherLicense();
				if (!Kalkulator1.instalClass.Version.newApp())
				{
					MessageBox.Show("Twoja wersja kalkulatora wyborczego jest nieaktualna.", "Uwaga");
				}
			}
			catch (System.Exception)
			{
			}
		}
		private void LicencesTable_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.RowIndex >= 0)
				{
					if (e.ColumnIndex == this.LicencesTable.Columns["action"].Index)
					{
						try
						{
							if (!System.IO.Directory.Exists(this.path + "\\Licenses"))
							{
								try
								{
									System.IO.Directory.CreateDirectory(this.path + "\\Licenses");
								}
								catch (System.ArgumentNullException)
								{
									MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
								}
								catch (System.ArgumentException)
								{
									MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
								}
								catch (System.UnauthorizedAccessException)
								{
									MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
								}
								catch (System.IO.PathTooLongException)
								{
									MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
								}
								catch (System.IO.DirectoryNotFoundException)
								{
									MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
								}
								catch (System.NotSupportedException)
								{
									MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"Licenses\"", "Error");
								}
								catch (System.IO.IOException)
								{
									MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
								}
							}
							object name = this.LicencesTable.Rows[e.RowIndex].Cells["Licencja"].Value;
							string filepath = this.path + "\\Licenses\\" + name.ToString();
							Commit com = new Commit(filepath, this);
							com.ShowDialog();
							if (this.logged)
							{
								this.logged = false;
								string[] ou = this.readOU(filepath).Split(new char[]
								{
									'-'
								});
								if (ou[2] == "P" || ou[2] == "O" || ou[2] == "Z")
								{
									this.wait.setWaitPanel("Trwa przygotowanie listy protokołów", "Proszę czekać");
									this.wait.setVisible(true);
									ProtocolsList startprotocols = new ProtocolsList(filepath, this, filepath, Kalkulator1.instalClass.Version.getVersion());
									startprotocols.ShowDialog();
								}
								if (ou[2] == "A")
								{
									this.jns = "";
									bool powiat = false;
									string jnsTMP = ou[1];
									if (jnsTMP.Length < 6)
									{
										while (jnsTMP.Length < 6)
										{
											jnsTMP = "0" + jnsTMP;
										}
									}
									if (jnsTMP[4] == '0' && jnsTMP[5] == '0')
									{
										powiat = true;
										RegionList rl = new RegionList(this, filepath, ou[1]);
										try
										{
											rl.ShowDialog();
										}
										catch (System.Exception)
										{
										}
									}
									else
									{
										this.jns = ou[1];
									}
									if (this.jns != null && this.jns != "")
									{
										ProtocolsList startprotocols = new ProtocolsList(filepath, this, filepath, Kalkulator1.instalClass.Version.getVersion(), this.jns, powiat);
										startprotocols.ShowDialog();
									}
									this.jns = "";
								}
							}
						}
						catch (System.ArgumentOutOfRangeException)
						{
						}
						catch (System.Exception ex)
						{
							MessageBox.Show("Operacja nie powiodła się. Spróbuj jeszcze raz. Orginal error: " + ex.Message, "Uwaga");
						}
					}
					if (e.ColumnIndex == this.LicencesTable.Columns["remove"].Index)
					{
						try
						{
							DialogResult result = MessageBox.Show("Czy napewno usunąć licencje " + this.LicencesTable.Rows[e.RowIndex].Cells["Licencja"].Value.ToString() + "?", "Usuwanie", MessageBoxButtons.YesNo);
							if (result == DialogResult.Yes)
							{
								try
								{
									System.IO.File.Delete(this.path + "\\Licenses\\" + this.LicencesTable.Rows[e.RowIndex].Cells["Licencja"].Value.ToString());
									System.IO.File.Delete(this.path + "\\Licenses\\" + this.LicencesTable.Rows[e.RowIndex].Cells["Licencja"].Value.ToString().Replace(".crt", ".key"));
									this.LicencesTable.Rows.Remove(this.LicencesTable.Rows[e.RowIndex]);
								}
								catch (System.ArgumentNullException)
								{
									MessageBox.Show("Błędna scieżka do licencji", "Error");
								}
								catch (System.ArgumentException)
								{
									MessageBox.Show("Błędna scieżka do licencji", "Error");
								}
								catch (System.IO.DirectoryNotFoundException)
								{
									MessageBox.Show("Błąd znalezienia pliku licencji", "Error");
								}
								catch (System.IO.PathTooLongException)
								{
									MessageBox.Show("Zbyt długa scieżka do licencji", "Error");
								}
								catch (System.IO.IOException)
								{
									MessageBox.Show("Nie można usunąć licencji. Jeden z jej plików jest obecnie używany.", "Error");
								}
								catch (System.NotSupportedException)
								{
									MessageBox.Show("Błędna scieżka do licencji", "Error");
								}
								catch (System.UnauthorizedAccessException)
								{
									MessageBox.Show("Program nie posiada uprawnień do usuwania plików", "Error");
								}
							}
							this.getActiveLicense();
							this.getOtherLicense();
						}
						catch (System.ArgumentOutOfRangeException)
						{
						}
					}
					else
					{
						if (e.ColumnIndex == this.LicencesTable.Columns["save"].Index)
						{
							try
							{
								System.IO.StreamReader sr = new System.IO.StreamReader(this.path + "\\Licenses\\" + this.LicencesTable.Rows[e.RowIndex].Cells["Licencja"].Value.ToString());
								string file = sr.ReadToEnd();
								sr.Close();
								SaveFileDialog wnd = new SaveFileDialog();
								wnd.Title = "Zapisywanie licencji";
								wnd.Filter = "(*.pem)|*.pem";
								wnd.FileName = this.LicencesTable.Rows[e.RowIndex].Cells["Licencja"].Value.ToString();
								wnd.ShowDialog();
								this.wait.setWaitPanel("Trwa zapisywanie licencji", "Proszę czekać");
								this.wait.setVisible(true);
								string name2 = wnd.FileName;
								try
								{
									if (name2 != "")
									{
										System.IO.StreamWriter sw = new System.IO.StreamWriter(name2, false);
										sw.Write(file);
										sw.Close();
									}
									this.wait.setVisible(false);
								}
								catch (System.UnauthorizedAccessException)
								{
									MessageBox.Show("Nie można zapisać pliku. Otwórz aplikacje jako administracje", "Error");
								}
								catch (System.Exception ex)
								{
									MessageBox.Show("Nie można zapisać pliku. Orginal exception: " + ex.Message, "Error");
								}
							}
							catch (System.ArgumentOutOfRangeException)
							{
							}
						}
					}
				}
			}
			catch (System.Exception)
			{
			}
		}
		private void waitLicenses_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.RowIndex >= 0)
				{
					if (e.ColumnIndex == this.waitLicenses.Columns["action"].Index)
					{
						this.wait.setWaitPanel("Trwa sprawdzanie licencji", "Proszę czekać");
						this.wait.setVisible(true);
						try
						{
							string licenceName = this.waitLicenses.Rows[e.RowIndex].Cells["Licencja"].Value.ToString();
							try
							{
								Certificate c = new Certificate();
								if (System.IO.Directory.Exists(this.path + "\\Licenses"))
								{
									System.IO.StreamReader s = new System.IO.StreamReader(this.path + "\\Licenses\\" + licenceName);
									string csr = s.ReadToEnd();
									s.Close();
									string uri = "certificates/crtget";
									string post = "csr=" + HttpUtility.UrlEncode(csr);
									IsLicense res = this.con.postCheckLicense(uri, post, 0);
									string file = "";
									if (res.getCode().getcode() == 12)
									{
										if (!System.IO.Directory.Exists(this.path + "\\Licenses\\Anulowane"))
										{
											try
											{
												System.IO.Directory.CreateDirectory(this.path + "\\Licenses\\Anulowane");
											}
											catch (System.ArgumentNullException)
											{
												MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Anulowane\"", "Error");
											}
											catch (System.ArgumentException)
											{
												MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Anulowane\"", "Error");
											}
											catch (System.UnauthorizedAccessException)
											{
												MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
											}
											catch (System.IO.PathTooLongException)
											{
												MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Anulowane\"", "Error");
											}
											catch (System.IO.DirectoryNotFoundException)
											{
												MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Anulowane\"", "Error");
											}
											catch (System.NotSupportedException)
											{
												MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"Anulowane\"", "Error");
											}
											catch (System.IO.IOException)
											{
												MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Anulowane\"", "Error");
											}
										}
										if (System.IO.Directory.Exists(this.path + "\\Licenses\\Anulowane"))
										{
											try
											{
												System.IO.File.Move(this.path + "\\Licenses\\" + licenceName, this.path + "\\Licenses\\Anulowane\\" + licenceName);
												System.IO.File.Move(this.path + "\\Licenses\\" + licenceName.Replace(".csr", ".key"), this.path + "\\Licenses\\Anulowane\\" + licenceName.Replace(".csr", ".key"));
											}
											catch (System.ArgumentNullException)
											{
												MessageBox.Show("Sprawdzana licencja nie istnieje", "Uwaga");
											}
											catch (System.ArgumentException)
											{
												MessageBox.Show("Sprawdzana licencja nie istnieje", "Uwaga");
											}
											catch (System.UnauthorizedAccessException)
											{
												MessageBox.Show("Nie masz uprawnień do przeniesiena licenji do anulowanych. Otwórz aplikacje jako adnimistrator.", "Uwaga");
											}
											catch (System.IO.PathTooLongException)
											{
												MessageBox.Show("Nieprawidłowa ścieżka do licencji", "Uwaga");
											}
											catch (System.IO.DirectoryNotFoundException)
											{
												MessageBox.Show("Nieprawidłowa ścieżka do licencji", "Uwaga");
											}
											catch (System.NotSupportedException)
											{
												MessageBox.Show("Nieprawidłowy format ścieżki do licencji", "Uwaga");
											}
											catch (System.IO.IOException)
											{
												try
												{
													string[] nameFilePart = licenceName.Replace(".csr", "").Split(new char[]
													{
														' '
													});
													int num = System.IO.Directory.GetFiles(this.path + "\\Licenses\\Anulowane\\", nameFilePart[0] + "*.csr").Length + 1;
													string namefile;
													if (num > 1)
													{
														namefile = nameFilePart[0] + " " + num.ToString() + ".csr";
													}
													else
													{
														namefile = nameFilePart[0] + ".csr";
													}
													System.IO.File.Move(this.path + "\\Licenses\\" + licenceName, this.path + "\\Licenses\\Anulowane\\" + namefile);
													System.IO.File.Move(this.path + "\\Licenses\\" + licenceName.Replace(".csr", ".key"), this.path + "\\Licenses\\Anulowane\\" + namefile.Replace(".csr", ".key"));
												}
												catch (System.ArgumentNullException)
												{
													MessageBox.Show("Sprawdzana licencja nie istnieje", "Uwaga");
												}
												catch (System.ArgumentException)
												{
													MessageBox.Show("Sprawdzana licencja nie istnieje", "Uwaga");
												}
												catch (System.UnauthorizedAccessException)
												{
													MessageBox.Show("Nie masz uprawnień do przeniesiena licenji do anulowanych.", "Uwaga");
												}
												catch (System.IO.PathTooLongException)
												{
													MessageBox.Show("Nieprawidłowa ścieżka do licencji", "Uwaga");
												}
												catch (System.IO.DirectoryNotFoundException)
												{
													MessageBox.Show("Nieprawidłowa ścieżka do licencji", "Uwaga");
												}
												catch (System.NotSupportedException)
												{
													MessageBox.Show("Nieprawidłowy format ścieżki do licencji", "Uwaga");
												}
												catch (System.IO.IOException)
												{
													MessageBox.Show("Nie można odnaleźć licencji", "Uwaga");
												}
											}
											this.getOtherLicense();
										}
									}
									if (res.getCode().getcode() == 0 && res.getCrt() != "")
									{
										file = res.getCrt();
									}
									if (file != "")
									{
										if (!System.IO.Directory.Exists(this.path + "\\Licenses"))
										{
											try
											{
												System.IO.Directory.CreateDirectory(this.path + "\\Licenses");
											}
											catch (System.ArgumentNullException)
											{
												MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
											}
											catch (System.ArgumentException)
											{
												MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
											}
											catch (System.UnauthorizedAccessException)
											{
												MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
											}
											catch (System.IO.PathTooLongException)
											{
												MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
											}
											catch (System.IO.DirectoryNotFoundException)
											{
												MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
											}
											catch (System.NotSupportedException)
											{
												MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"Licenses\"", "Error");
											}
											catch (System.IO.IOException)
											{
												MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
											}
										}
										if (System.IO.Directory.Exists(this.path + "\\Licenses"))
										{
											try
											{
												System.IO.StreamReader swk = new System.IO.StreamReader(this.path + "\\Licenses\\" + licenceName.Replace(".csr", ".key"));
												string key = swk.ReadToEnd();
												swk.Close();
												string[] nameFilePart = licenceName.Replace(".csr", "").Split(new char[]
												{
													' '
												});
												int num = System.IO.Directory.GetFiles(this.path + "\\Licenses\\", nameFilePart[0] + "*.pem").Length + 1;
												string namefile;
												if (num > 1)
												{
													namefile = nameFilePart[0] + " " + num.ToString() + ".pem";
												}
												else
												{
													namefile = nameFilePart[0] + ".pem";
												}
												file = file + '\n'.ToString() + key;
												System.IO.StreamWriter sw = new System.IO.StreamWriter(this.path + "\\Licenses\\" + namefile, false);
												sw.Write(file);
												sw.Close();
												try
												{
													System.Security.Cryptography.X509Certificates.X509Certificate a = new System.Security.Cryptography.X509Certificates.X509Certificate(this.path + "\\Licenses\\" + namefile);
													System.IO.File.Delete(this.path + "\\Licenses\\" + licenceName);
													System.IO.File.Delete(this.path + "\\Licenses\\" + licenceName.Replace(".csr", ".key"));
												}
												catch (System.Security.Cryptography.CryptographicException)
												{
													MessageBox.Show("Przesłany certyfikat jest nie prawidłowy", "Error");
												}
												catch (System.ArgumentException)
												{
												}
											}
											catch (System.ArgumentNullException)
											{
												MessageBox.Show("Nie można zapisać licencji. Nieprawidłowa ścieżka ", "Error");
											}
											catch (System.ArgumentException)
											{
												MessageBox.Show("Nie można zapisać licencji. Nieprawidłowa ścieżka ", "Error");
											}
											catch (System.UnauthorizedAccessException)
											{
												MessageBox.Show("Nie masz uprawnień zapisania licencji. Otwórz aplikacje jako adnimistrator.", "Uwaga");
											}
											catch (System.IO.PathTooLongException)
											{
												MessageBox.Show("Nie można zapisać licencji. Nieprawidłowa ścieżka ", "Error");
											}
											catch (System.IO.DirectoryNotFoundException)
											{
												MessageBox.Show("Nie można zapisać licencji. Nieprawidłowa ścieżka ", "Error");
											}
											catch (System.NotSupportedException)
											{
												MessageBox.Show("Nie można zapisać licencji. Nieprawidłowa ścieżka ", "Error");
											}
											catch (System.IO.IOException)
											{
												MessageBox.Show("Nie można zapisać licencji. Nieprawidłowa ścieżka ", "Error");
											}
											catch (System.ObjectDisposedException)
											{
												MessageBox.Show("Nie można zapisać licencji (ObjectDisposedException)", "Error");
											}
											this.getActiveLicense();
											this.getOtherLicense();
										}
									}
									if (res.getCode().getcode() != 0 && res.getCode().getcode() != 11 && res.getCode().getcode() != 12)
									{
										MessageBox.Show(res.getCode().getText(), "Uwaga");
									}
									this.getOtherLicense();
									this.getActiveLicense();
								}
							}
							finally
							{
							}
						}
						catch (System.ArgumentOutOfRangeException)
						{
						}
						this.wait.setVisible(false);
					}
					if (e.ColumnIndex == this.waitLicenses.Columns["remove"].Index)
					{
						try
						{
							DialogResult result = MessageBox.Show("Czy napewno usunąć licencje " + this.waitLicenses.Rows[e.RowIndex].Cells["Licencja"].Value.ToString() + "?", "Usuwanie", MessageBoxButtons.YesNo);
							if (result == DialogResult.Yes)
							{
								try
								{
									if (!System.IO.Directory.Exists(this.path + "\\Licenses"))
									{
										System.IO.Directory.CreateDirectory(this.path + "\\Licenses");
									}
									if (this.waitLicenses.Rows[e.RowIndex].Cells["Status"].Value.ToString() == "Anulowana")
									{
										if (!System.IO.Directory.Exists(this.path + "\\Licenses\\Anulowane"))
										{
											System.IO.Directory.CreateDirectory(this.path + "\\Licenses\\Anulowane");
										}
										string a2 = this.path + "\\Licenses\\Anulowane\\" + this.waitLicenses.Rows[e.RowIndex].Cells["Licencja"].Value.ToString();
										System.IO.File.Delete(this.path + "\\Licenses\\Anulowane\\" + this.waitLicenses.Rows[e.RowIndex].Cells["Licencja"].Value.ToString());
										System.IO.File.Delete(this.path + "\\Licenses\\Anulowane\\" + this.waitLicenses.Rows[e.RowIndex].Cells["Licencja"].Value.ToString().Replace(".csr", ".key"));
									}
									if (this.waitLicenses.Rows[e.RowIndex].Cells["Status"].Value.ToString() == "Nie aktywna")
									{
										string a2 = this.path + "\\Licenses\\" + this.waitLicenses.Rows[e.RowIndex].Cells["Licencja"].Value.ToString();
										System.IO.File.Delete(this.path + "\\Licenses\\" + this.waitLicenses.Rows[e.RowIndex].Cells["Licencja"].Value.ToString());
										System.IO.File.Delete(this.path + "\\Licenses\\" + this.waitLicenses.Rows[e.RowIndex].Cells["Licencja"].Value.ToString().Replace(".crt", ".key"));
									}
									if (this.waitLicenses.Rows[e.RowIndex].Cells["Status"].Value.ToString() == "Oczekująca")
									{
										string a2 = this.path + "\\Licenses\\" + this.waitLicenses.Rows[e.RowIndex].Cells["Licencja"].Value.ToString();
										System.IO.File.Delete(this.path + "\\Licenses\\" + this.waitLicenses.Rows[e.RowIndex].Cells["Licencja"].Value.ToString());
										System.IO.File.Delete(this.path + "\\Licenses\\" + this.waitLicenses.Rows[e.RowIndex].Cells["Licencja"].Value.ToString().Replace(".csr", ".key"));
									}
									this.waitLicenses.Rows.Remove(this.waitLicenses.Rows[e.RowIndex]);
								}
								catch (System.ArgumentNullException)
								{
									MessageBox.Show("Błędna scieżka do licencji", "Error");
								}
								catch (System.ArgumentException)
								{
									MessageBox.Show("Błędna scieżka do licencji", "Error");
								}
								catch (System.IO.DirectoryNotFoundException)
								{
									MessageBox.Show("Błąd znalezienia pliku licencji", "Error");
								}
								catch (System.IO.PathTooLongException)
								{
									MessageBox.Show("Zbyt długa scieżka do licencji", "Error");
								}
								catch (System.IO.IOException)
								{
									MessageBox.Show("Nie można usunąć licencji. Jeden z jej plików jest obecnie używany.", "Error");
								}
								catch (System.NotSupportedException)
								{
									MessageBox.Show("Błędna scieżka do licencji", "Error");
								}
								catch (System.UnauthorizedAccessException)
								{
									MessageBox.Show("Program nie posiada uprawnień do usuwania plików", "Error");
								}
							}
							this.getActiveLicense();
							this.getOtherLicense();
						}
						catch (System.ArgumentOutOfRangeException)
						{
						}
					}
				}
			}
			catch (System.Exception)
			{
			}
		}
		private void getLicenseFromNet_Click(object sender, System.EventArgs e)
		{
			Login lic = new Login();
			lic.ShowDialog();
			this.getActiveLicense();
			this.getOtherLicense();
		}
		private void getLicenseFromDisk_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog wnd = new OpenFileDialog();
			string licenseFile = "";
			if (wnd.ShowDialog() == DialogResult.OK)
			{
				licenseFile = wnd.FileName;
			}
			this.wait.setWaitPanel("Trwa wczytywanie licencji", "Proszę czekać");
			this.wait.setVisible(true);
			if (licenseFile != "")
			{
				try
				{
					System.Security.Cryptography.X509Certificates.X509Certificate cert = new System.Security.Cryptography.X509Certificates.X509Certificate(licenseFile);
					try
					{
						System.DateTime fromDate = System.Convert.ToDateTime(cert.GetEffectiveDateString());
						System.DateTime toDate = System.Convert.ToDateTime(cert.GetExpirationDateString());
						int result = System.DateTime.Compare(fromDate, System.DateTime.Now);
						int result2 = System.DateTime.Compare(System.DateTime.Now, toDate);
						if (result <= 0 && result2 <= 0)
						{
							string role = "";
							string ou = "";
							string[] subjectPatrs = cert.Subject.Split(new string[]
							{
								", "
							}, System.StringSplitOptions.None);
							for (int i = 0; i < subjectPatrs.Length; i++)
							{
								if (System.Text.RegularExpressions.Regex.IsMatch(subjectPatrs[i], "^OU="))
								{
									string[] tmp = subjectPatrs[i].Replace("OU=", "").Split(new char[]
									{
										'-'
									});
									role = tmp[2];
									ou = subjectPatrs[i].Replace("OU=", "");
									break;
								}
							}
							if ((role == "P" && !this.isExsistOU(ou)) || (role == "Z" && !this.isExsistOU(ou)) || role == "O" || role == "A")
							{
								try
								{
									if (!System.IO.Directory.Exists(this.path + "\\Licenses"))
									{
										try
										{
											System.IO.Directory.CreateDirectory(this.path + "\\Licenses");
										}
										catch (System.ArgumentNullException)
										{
											MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
										}
										catch (System.ArgumentException)
										{
											MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
										}
										catch (System.UnauthorizedAccessException)
										{
											MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
										}
										catch (System.IO.PathTooLongException)
										{
											MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
										}
										catch (System.IO.DirectoryNotFoundException)
										{
											MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
										}
										catch (System.NotSupportedException)
										{
											MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"Licenses\"", "Error");
										}
										catch (System.IO.IOException)
										{
											MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
										}
									}
									System.IO.StreamReader sr = new System.IO.StreamReader(licenseFile);
									string licenseContent = sr.ReadToEnd();
									sr.Close();
									if (System.IO.Directory.Exists(this.path + "\\Licenses"))
									{
										string[] subcjecttab = cert.Subject.Split(new string[]
										{
											", "
										}, System.StringSplitOptions.RemoveEmptyEntries);
										string cn = "";
										string ou2 = "";
										int end = 0;
										for (int i = 0; i < subcjecttab.Length; i++)
										{
											if (System.Text.RegularExpressions.Regex.IsMatch(subcjecttab[i], "^CN="))
											{
												cn = subcjecttab[i].Replace("CN=", "");
												end++;
											}
											if (System.Text.RegularExpressions.Regex.IsMatch(subcjecttab[i], "^OU="))
											{
												ou2 = subcjecttab[i].Replace("OU=", "").Replace("/", "_");
												end++;
											}
											if (end >= 2)
											{
												break;
											}
										}
										string ouCn = ou2 + "_" + cn;
										int num = System.IO.Directory.GetFiles(this.path + "\\Licenses", ouCn + "*.pem").Length;
										string namefile = ouCn;
										if (num > 0)
										{
											namefile = namefile + " " + (num + 1).ToString();
										}
										System.IO.StreamWriter sw = new System.IO.StreamWriter(this.path + "\\Licenses\\" + namefile + ".pem", false);
										sw.Write(licenseContent);
										sw.Close();
									}
								}
								catch (System.ArgumentNullException)
								{
									MessageBox.Show("Nie wskazano gdzie leży licencja", "Error");
								}
								catch (System.ArgumentException)
								{
									MessageBox.Show("Nie wskazano gdzie leży licencja", "Error");
								}
								catch (System.IO.DirectoryNotFoundException)
								{
									MessageBox.Show("Nie znaleziono licencja", "Error");
								}
								catch (System.IO.IOException)
								{
									MessageBox.Show("Nieprawidłowa ścieżka do licencji", "Error");
								}
								catch (System.UnauthorizedAccessException)
								{
									MessageBox.Show("Nie masz uprawnień do zapisania licencji. Otwórz aplikacje jako adnimistrator.", "Uwaga");
								}
								catch (System.NotSupportedException)
								{
									MessageBox.Show("Nieprawidłowa ścieżka do licencji", "Error");
								}
								catch (System.ObjectDisposedException)
								{
									MessageBox.Show("Nie można zapisać licencji (ObjectDisposedException)", "Error");
								}
							}
							else
							{
								MessageBox.Show("Licencja nie została załadowana. Podana licencja przewodniczacego już istnieje.", "Komunikat");
							}
						}
						else
						{
							MessageBox.Show("Licencja nie została załadowana. Podana licencja jest nie ważna.", "Komunikat");
						}
					}
					catch (System.FormatException)
					{
						MessageBox.Show("Licencja nie została załadowana. Podana licencja jest nie ważna.", "Komunikat");
					}
				}
				catch (System.Security.Cryptography.CryptographicException)
				{
					MessageBox.Show("Licencja nie została załadowana. Licencja jest nieprawidłowa.", "Komunikat");
				}
			}
			this.wait.setVisible(false);
			this.getActiveLicense();
			this.getOtherLicense();
		}
		private void getActiveLicense()
		{
			this.wait.setWaitPanel("Trwa wczytywanie licencji", "Proszę czekać");
			this.wait.setVisible(true);
			if (System.IO.Directory.Exists(this.path + "\\Licenses"))
			{
				DataTable dt = new DataTable();
				this.LicencesTable.DataSource = dt;
				dt.Columns.Add(new DataColumn("lp", typeof(string)));
				dt.Columns.Add(new DataColumn("Licencja", typeof(string)));
				foreach (string file in System.IO.Directory.EnumerateFiles(this.path + "\\Licenses", "*.pem"))
				{
					Certificate c = new Certificate(file);
					if (c.isActiveLicense())
					{
						string[] filename = file.Split(new char[]
						{
							'\\'
						});
						DataRow dr = dt.NewRow();
						dr[0] = dt.Rows.Count + 1;
						dr[1] = filename[filename.Length - 1];
						dt.Rows.Add(dr);
					}
				}
				if (dt.Rows.Count > 0)
				{
					this.LicencesTable.DataSource = dt;
					this.LicencesTable.Columns["lp"].DisplayIndex = 0;
					this.LicencesTable.Columns["lp"].FillWeight = 15f;
					this.LicencesTable.Columns["Licencja"].DisplayIndex = 1;
					DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
					btn.HeaderText = "Akcje";
					btn.Text = "przejdź";
					btn.Name = "action";
					btn.UseColumnTextForButtonValue = true;
					if (this.LicencesTable.Columns["action"] == null)
					{
						this.LicencesTable.Columns.Insert(2, btn);
						this.LicencesTable.Columns["action"].FillWeight = 23f;
					}
					else
					{
						this.LicencesTable.Columns["action"].DisplayIndex = 2;
						this.LicencesTable.Columns["action"].FillWeight = 23f;
					}
					DataGridViewButtonColumn btn2 = new DataGridViewButtonColumn();
					btn2.HeaderText = "";
					btn2.Text = "Zapisz";
					btn2.Name = "save";
					btn2.UseColumnTextForButtonValue = true;
					if (this.LicencesTable.Columns["save"] == null)
					{
						this.LicencesTable.Columns.Insert(3, btn2);
						this.LicencesTable.Columns["save"].FillWeight = 23f;
					}
					else
					{
						this.LicencesTable.Columns["save"].DisplayIndex = 3;
						this.LicencesTable.Columns["save"].FillWeight = 23f;
					}
					DataGridViewButtonColumn btn3 = new DataGridViewButtonColumn();
					btn3.HeaderText = "";
					btn3.Text = "Usuń";
					btn3.Name = "remove";
					btn3.UseColumnTextForButtonValue = true;
					if (this.LicencesTable.Columns["remove"] == null)
					{
						this.LicencesTable.Columns.Insert(4, btn3);
						this.LicencesTable.Columns["remove"].FillWeight = 23f;
					}
					else
					{
						this.LicencesTable.Columns["remove"].DisplayIndex = 4;
						this.LicencesTable.Columns["remove"].FillWeight = 23f;
					}
					this.licensesPanel.Visible = true;
				}
			}
			else
			{
				this.licensesPanel.Visible = false;
			}
			this.wait.setVisible(false);
		}
		private void getOtherLicense()
		{
			this.wait.setWaitPanel("Trwa wczytywanie licencji oczekujących", "Proszę czekać");
			this.wait.setVisible(true);
			if (System.IO.Directory.Exists(this.path + "\\Licenses"))
			{
				DataTable dt = new DataTable();
				this.waitLicenses.DataSource = dt;
				dt.Columns.Add(new DataColumn("lp", typeof(string)));
				dt.Columns.Add(new DataColumn("Licencja", typeof(string)));
				dt.Columns.Add(new DataColumn("Status", typeof(string)));
				foreach (string file in System.IO.Directory.EnumerateFiles(this.path + "\\Licenses", "*.csr"))
				{
					string[] filename = file.Split(new char[]
					{
						'\\'
					});
					DataRow dr = dt.NewRow();
					dr[0] = dt.Rows.Count + 1;
					dr[1] = filename[filename.Length - 1];
					dr[2] = "Oczekująca";
					dt.Rows.Add(dr);
				}
				if (System.IO.Directory.Exists(this.path + "\\Licenses\\Anulowane"))
				{
					foreach (string file in System.IO.Directory.EnumerateFiles(this.path + "\\Licenses\\Anulowane\\", "*.csr"))
					{
						string[] filename = file.Split(new char[]
						{
							'\\'
						});
						DataRow dr = dt.NewRow();
						dr[0] = dt.Rows.Count + 1;
						dr[1] = filename[filename.Length - 1];
						dr[2] = "Anulowana";
						dt.Rows.Add(dr);
					}
				}
				foreach (string file in System.IO.Directory.EnumerateFiles(this.path + "\\Licenses", "*.pem"))
				{
					if (!this.certificate.isActiveLicense(file))
					{
						string[] filename = file.Split(new char[]
						{
							'\\'
						});
						DataRow dr = dt.NewRow();
						dr[0] = dt.Rows.Count + 1;
						dr[1] = filename[filename.Length - 1];
						dr[2] = "Nie aktywna";
						dt.Rows.Add(dr);
					}
				}
				if (dt.Rows.Count > 0)
				{
					this.waitLicenses.DataSource = dt;
					this.waitLicenses.Columns["lp"].DisplayIndex = 0;
					this.waitLicenses.Columns["lp"].FillWeight = 10f;
					this.waitLicenses.Columns["Licencja"].DisplayIndex = 1;
					this.waitLicenses.Columns["Status"].DisplayIndex = 2;
					this.waitLicenses.Columns["Status"].FillWeight = 20f;
					DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
					btn.HeaderText = "Akcje";
					btn.Text = "sprawdź status/pobierz";
					btn.Name = "action";
					btn.UseColumnTextForButtonValue = true;
					if (this.waitLicenses.Columns["action"] == null)
					{
						this.waitLicenses.Columns.Insert(3, btn);
						this.waitLicenses.Columns["action"].FillWeight = 38f;
					}
					else
					{
						this.waitLicenses.Columns["action"].DisplayIndex = 3;
						this.waitLicenses.Columns["action"].FillWeight = 38f;
					}
					DataGridViewButtonColumn btn2 = new DataGridViewButtonColumn();
					btn2.HeaderText = "";
					btn2.Text = "Usuń";
					btn2.Name = "remove";
					btn2.UseColumnTextForButtonValue = true;
					if (this.waitLicenses.Columns["remove"] == null)
					{
						this.waitLicenses.Columns.Insert(4, btn2);
						this.waitLicenses.Columns["remove"].FillWeight = 12f;
					}
					else
					{
						this.waitLicenses.Columns["remove"].DisplayIndex = 4;
						this.waitLicenses.Columns["remove"].FillWeight = 12f;
					}
					this.waitLicensesPanel.Visible = true;
					this.waitLicenses.Refresh();
				}
			}
			else
			{
				this.waitLicensesPanel.Visible = false;
			}
			this.wait.setVisible(false);
		}
		private bool isExsistOU(string OU)
		{
			bool result3;
			if (System.IO.Directory.Exists(this.path + "\\Licenses"))
			{
				foreach (string file in System.IO.Directory.EnumerateFiles(this.path + "\\Licenses", "*.pem"))
				{
					System.Security.Cryptography.X509Certificates.X509Certificate cert = new System.Security.Cryptography.X509Certificates.X509Certificate(file);
					System.DateTime fromDate = System.Convert.ToDateTime(cert.GetEffectiveDateString());
					System.DateTime toDate = System.Convert.ToDateTime(cert.GetExpirationDateString());
					int result = System.DateTime.Compare(fromDate, System.DateTime.Today);
					int result2 = System.DateTime.Compare(System.DateTime.Today, toDate);
					if (result <= 0 && result2 <= 0)
					{
						string[] subcjecttab = cert.Subject.Split(new string[]
						{
							", "
						}, System.StringSplitOptions.RemoveEmptyEntries);
						string ou = "";
						for (int i = 0; i < subcjecttab.Length; i++)
						{
							if (System.Text.RegularExpressions.Regex.IsMatch(subcjecttab[i], "^OU="))
							{
								ou = subcjecttab[i].Replace("OU=", "");
								break;
							}
						}
						if (ou == OU)
						{
							result3 = true;
							return result3;
						}
					}
				}
			}
			result3 = false;
			return result3;
		}
		private string readOU(string filePath)
		{
			System.Security.Cryptography.X509Certificates.X509Certificate cert = new System.Security.Cryptography.X509Certificates.X509Certificate(filePath);
			string[] subcjecttab = cert.Subject.Split(new string[]
			{
				", "
			}, System.StringSplitOptions.RemoveEmptyEntries);
			string ou = "";
			for (int i = 0; i < subcjecttab.Length; i++)
			{
				if (System.Text.RegularExpressions.Regex.IsMatch(subcjecttab[i], "^OU="))
				{
					ou = subcjecttab[i].Replace("OU=", "");
					break;
				}
			}
			return ou;
		}
		private void btnDeleteTemp_Click(object sender, System.EventArgs e)
		{
			DialogResult dr = MessageBox.Show("Wszystkie zapisane wcześniej dane zostaną usunięte. Czy kontynuować?", "Usuwanie danych tymczasowych", MessageBoxButtons.YesNo);
			DialogResult dialogResult = dr;
			if (dialogResult != DialogResult.No)
			{
				try
				{
					System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(this.path + "\\ProtocolsDef");
					if (dir.Exists)
					{
						dir.Delete(true);
					}
					dir = new System.IO.DirectoryInfo(this.path + "\\electoralEampaign");
					if (dir.Exists)
					{
						dir.Delete(true);
					}
					dir = new System.IO.DirectoryInfo(this.path + "\\saves");
					if (dir.Exists)
					{
						dir.Delete(true);
					}
					dir = new System.IO.DirectoryInfo(this.path + "\\tmp");
					if (dir.Exists)
					{
						dir.Delete(true);
					}
					Start.listaPlikow.Clear();
				}
				catch (System.Exception)
				{
					MessageBox.Show("Brak danych tymczasowych.", "Usuwanie danych tymczasowych", MessageBoxButtons.OK);
				}
				if (!System.IO.Directory.Exists(this.path + "\\tmp"))
				{
					Instal inst = new Instal(this.path);
				}
				if (System.IO.Directory.Exists(System.IO.Path.GetTempPath() + "KBW\\tmp"))
				{
					if (!System.IO.File.Exists(System.IO.Path.GetTempPath() + "KBW\\tmp\\eksport.xml"))
					{
						System.IO.File.Create(System.IO.Path.GetTempPath() + "KBW\\tmp\\eksport.xml");
					}
					if (!System.IO.File.Exists(System.IO.Path.GetTempPath() + "KBW\\tmp\\import.xml"))
					{
						System.IO.File.Create(System.IO.Path.GetTempPath() + "KBW\\tmp\\import.xml");
					}
					if (!System.IO.File.Exists(System.IO.Path.GetTempPath() + "KBW\\tmp\\eksportCode.xml"))
					{
						System.IO.File.Create(System.IO.Path.GetTempPath() + "KBW\\tmp\\eksportCode.xml");
					}
				}
				MessageBox.Show("Dane tymczasowe zostały pomyślnie usunięte.", "Usuwanie danych tymczasowych", MessageBoxButtons.OK);
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
			this.label1 = new Label();
			this.label2 = new Label();
			this.licensesPanel = new Panel();
			this.btnDeleteTemp = new Button();
			this.LicencesTable = new DataGridView();
			this.waitLicensesPanel = new Panel();
			this.waitLicenses = new DataGridView();
			this.panel3 = new Panel();
			this.getLicenseFromNet = new Button();
			this.label4 = new Label();
			this.getLicenseFromDisk = new Button();
			this.label3 = new Label();
			this.panel1 = new Panel();
			this.licensesPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.LicencesTable).BeginInit();
			this.waitLicensesPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.waitLicenses).BeginInit();
			this.panel3.SuspendLayout();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(289, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Wybierz licencję, zgodnie z którą chcesz wprowadzać dane";
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(14, 11);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(105, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Licencje oczekujące";
			this.licensesPanel.Controls.Add(this.btnDeleteTemp);
			this.licensesPanel.Controls.Add(this.LicencesTable);
			this.licensesPanel.Controls.Add(this.label1);
			this.licensesPanel.Location = new System.Drawing.Point(12, 12);
			this.licensesPanel.Name = "licensesPanel";
			this.licensesPanel.Size = new System.Drawing.Size(812, 168);
			this.licensesPanel.TabIndex = 2;
			this.btnDeleteTemp.Location = new System.Drawing.Point(564, 5);
			this.btnDeleteTemp.Margin = new Padding(2, 2, 2, 2);
			this.btnDeleteTemp.Name = "btnDeleteTemp";
			this.btnDeleteTemp.Size = new System.Drawing.Size(245, 25);
			this.btnDeleteTemp.TabIndex = 2;
			this.btnDeleteTemp.Text = "Wyczyść dane tymczasowe";
			this.btnDeleteTemp.UseVisualStyleBackColor = true;
			this.btnDeleteTemp.Click += new System.EventHandler(this.btnDeleteTemp_Click);
			this.LicencesTable.AllowUserToAddRows = false;
			this.LicencesTable.AllowUserToDeleteRows = false;
			this.LicencesTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			this.LicencesTable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
			this.LicencesTable.BackgroundColor = System.Drawing.SystemColors.Control;
			this.LicencesTable.BorderStyle = BorderStyle.None;
			this.LicencesTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.LicencesTable.GridColor = System.Drawing.SystemColors.Control;
			this.LicencesTable.Location = new System.Drawing.Point(17, 38);
			this.LicencesTable.Name = "LicencesTable";
			this.LicencesTable.ReadOnly = true;
			this.LicencesTable.Size = new System.Drawing.Size(792, 116);
			this.LicencesTable.TabIndex = 1;
			this.waitLicensesPanel.Controls.Add(this.waitLicenses);
			this.waitLicensesPanel.Controls.Add(this.label2);
			this.waitLicensesPanel.Location = new System.Drawing.Point(12, 172);
			this.waitLicensesPanel.Name = "waitLicensesPanel";
			this.waitLicensesPanel.Size = new System.Drawing.Size(812, 197);
			this.waitLicensesPanel.TabIndex = 3;
			this.waitLicenses.AllowUserToAddRows = false;
			this.waitLicenses.AllowUserToDeleteRows = false;
			this.waitLicenses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			this.waitLicenses.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
			this.waitLicenses.BackgroundColor = System.Drawing.SystemColors.Control;
			this.waitLicenses.BorderStyle = BorderStyle.None;
			this.waitLicenses.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.waitLicenses.Location = new System.Drawing.Point(17, 30);
			this.waitLicenses.Name = "waitLicenses";
			this.waitLicenses.ReadOnly = true;
			this.waitLicenses.Size = new System.Drawing.Size(792, 134);
			this.waitLicenses.TabIndex = 2;
			this.panel3.Controls.Add(this.getLicenseFromNet);
			this.panel3.Controls.Add(this.label4);
			this.panel3.Controls.Add(this.getLicenseFromDisk);
			this.panel3.Controls.Add(this.label3);
			this.panel3.Dock = DockStyle.Bottom;
			this.panel3.Location = new System.Drawing.Point(0, 301);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(831, 89);
			this.panel3.TabIndex = 4;
			this.getLicenseFromNet.Location = new System.Drawing.Point(576, 47);
			this.getLicenseFromNet.Name = "getLicenseFromNet";
			this.getLicenseFromNet.Size = new System.Drawing.Size(245, 23);
			this.getLicenseFromNet.TabIndex = 4;
			this.getLicenseFromNet.Text = "wnioskuj o licencję";
			this.getLicenseFromNet.UseVisualStyleBackColor = true;
			this.getLicenseFromNet.Click += new System.EventHandler(this.getLicenseFromNet_Click);
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(409, 47);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(21, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "lub";
			this.getLicenseFromDisk.Location = new System.Drawing.Point(29, 47);
			this.getLicenseFromDisk.Name = "getLicenseFromDisk";
			this.getLicenseFromDisk.Size = new System.Drawing.Size(245, 23);
			this.getLicenseFromDisk.TabIndex = 2;
			this.getLicenseFromDisk.Text = "znajdź licencję na komputerze";
			this.getLicenseFromDisk.UseVisualStyleBackColor = true;
			this.getLicenseFromDisk.Click += new System.EventHandler(this.getLicenseFromDisk_Click);
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(14, 14);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(175, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Nie ma licencji, której potrzebujesz?";
			this.panel1.BackColor = System.Drawing.Color.Transparent;
			this.panel1.Controls.Add(this.licensesPanel);
			this.panel1.Controls.Add(this.waitLicensesPanel);
			this.panel1.Dock = DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(831, 301);
			this.panel1.TabIndex = 5;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackgroundImageLayout = ImageLayout.None;
			base.ClientSize = new System.Drawing.Size(831, 390);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.panel3);
			base.Name = "Start";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Kalkulator";
			this.licensesPanel.ResumeLayout(false);
			this.licensesPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)this.LicencesTable).EndInit();
			this.waitLicensesPanel.ResumeLayout(false);
			this.waitLicensesPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)this.waitLicenses).EndInit();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.panel1.ResumeLayout(false);
			base.ResumeLayout(false);
		}
	}
}
