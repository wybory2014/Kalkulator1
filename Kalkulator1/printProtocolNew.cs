using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Gnostice.Documents;
using Gnostice.Documents.Controls.WinForms;
using Novacode;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using Zen.Barcode;
namespace Kalkulator1
{
	internal class printProtocolNew
	{
		private string newFile = "";
		private string newFileErr = "";
		private PrintDialog printDialog1 = new PrintDialog();
		private string imageBarCode;
		private System.IO.Stream GetBinaryDataStream(string base64String)
		{
			return new System.IO.MemoryStream(System.Convert.FromBase64String(base64String));
		}
		public void ProtocolPrint(System.Xml.XmlDocument headerDefinition, System.Xml.XmlDocument value, System.Xml.XmlDocument candidates, string docDefinitionPath, string controlSum, bool printToPDF, string obw, string inst, string okr, string candidatesPath, string instJNS)
		{
			Framework.ActivateLicense("4E5A-14CC-D4D2-14C2-F558-B99F-C5F5-5E4B");
			bool isWielopak = false;
			string docErrPath = "";
			int idxCand2 = 0;
			XmlNode jns = headerDefinition.SelectSingleNode("/akcja_wyborcza/jns");
			string jnskod = jns.Attributes.GetNamedItem("jns_kod").Value;
			string organNazwa = "";
			foreach (XmlNode xObwod in jns)
			{
				XmlNode xObwod;
				if (xObwod.Attributes["nr"].InnerText == obw)
				{
					foreach (XmlNode xInst in xObwod)
					{
						if (xInst.Attributes["kod"].InnerText == inst)
						{
							foreach (XmlNode xobw in xInst)
							{
								if (xobw.Attributes["nr"].InnerText == okr && System.Convert.ToInt32(xInst.Attributes["inst_jns"].InnerText) == System.Convert.ToInt32(instJNS))
								{
									organNazwa = xInst.Attributes["organNazwa"].InnerText;
									break;
								}
							}
						}
					}
				}
			}
			if (inst == "WBP")
			{
				if (jnskod.Substring(0, 4) == "1465" && jnskod.Length == 6)
				{
					string candidates2 = candidatesPath.Replace(jnskod + "-" + okr + ".xml", "146501-1.xml");
					if (System.IO.File.Exists(candidates2))
					{
						candidates.Load(candidates2);
					}
				}
				if (this.isOneCandidate(candidates))
				{
					docDefinitionPath = docDefinitionPath.Replace("WBP", "WBP_1");
				}
			}
			if (inst == "RDA")
			{
				if (jnskod.Length < 6)
				{
					while (jnskod.Length < 6)
					{
						jnskod = "0" + jnskod;
					}
				}
				if (jnskod[2] == '7' || jnskod[2] == '6')
				{
					if (jnskod.Substring(0, 4) == "1465" && organNazwa == "m.st.")
					{
						docDefinitionPath = docDefinitionPath.Replace("RDA", "RDA_M");
						string candidates2 = candidatesPath.Replace(jnskod + "-" + okr + ".xml", "146501-" + okr + ".xml");
						if (System.IO.File.Exists(candidates2))
						{
							candidates.Load(candidates2);
						}
					}
					if (jnskod.Substring(0, 4) != "1465")
					{
						docDefinitionPath = docDefinitionPath.Replace("RDA", "RDA_M");
					}
				}
				if (jnskod.Substring(0, 4) == "1465" && organNazwa == "Dzielnicy")
				{
					docDefinitionPath = docDefinitionPath.Replace("RDA", "RDA_D");
				}
			}
			XmlNode xmlValues = new System.Xml.XmlDocument();
			XmlNode xmlErr = value.SelectSingleNode("/save/hardError");
			if (xmlErr == null)
			{
				xmlErr = value.SelectSingleNode("/save/hardWarning");
			}
			if (xmlErr == null)
			{
				xmlErr = value.SelectSingleNode("/save/softError");
			}
			if (xmlErr != null)
			{
				docErrPath = docDefinitionPath.Replace(".docx", "_ERR.docx");
				this.newFileErr = docErrPath.Replace(".docx", "TMP.docx");
				System.Xml.XmlDocument xmlWalidacja = new System.Xml.XmlDocument();
				xmlWalidacja.Load(docDefinitionPath.Replace(".docx", "_Walidacja.xml"));
				using (DocX docTemplate = DocX.Load(docErrPath))
				{
					xmlErr = value.SelectSingleNode("/save/report");
					if (xmlErr != null)
					{
						if (xmlErr != null)
						{
							foreach (XmlNode xmlField in xmlErr)
							{
								foreach (Novacode.Table table in docTemplate.Tables)
								{
									int idxCommMember = 0;
									foreach (Row row in table.Rows)
									{
										idxCommMember++;
										if (row.Cells.Count == 1)
										{
											if (row.FindAll("<ERROR>").Count > 0)
											{
												string strErr = "[" + xmlField.Name.Substring(0, xmlField.Name.IndexOf("_")) + "]";
												string strErrDesc = "";
												Row rowNew = row;
												table.InsertRow(rowNew, idxCommMember);
												XmlNode xmlErrDesc = xmlWalidacja.SelectSingleNode("/validate_info");
												foreach (XmlNode xmlRule in xmlErrDesc)
												{
													foreach (XmlNode xmlErrField in xmlRule)
													{
														if (xmlErrField.Name == "note")
														{
															if (xmlErrField.InnerText.Length >= strErr.Length)
															{
																if (xmlErrField.InnerText.Substring(0, strErr.Length) == strErr)
																{
																	strErrDesc = xmlErrField.InnerText;
																}
															}
														}
													}
												}
												if (strErrDesc != "")
												{
													row.ReplaceText("<ERROR>", strErrDesc + System.Environment.NewLine + "Stanowisko komisji: " + xmlField.InnerText, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
												}
												else
												{
													row.ReplaceText("<ERROR>", strErr + System.Environment.NewLine + "Stanowisko komisji: " + xmlField.InnerText, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
												}
											}
										}
									}
								}
							}
						}
					}
					xmlValues = value.SelectSingleNode("/save/header");
					if (xmlValues != null)
					{
						foreach (XmlNode xmlField in xmlValues)
						{
							System.Collections.Generic.List<int> i = docTemplate.FindAll("<" + xmlField.Name + "*");
							int iCount = i.Count;
							int fCount = xmlField.InnerText.Length;
							int x = 2;
							if (iCount >= 1)
							{
								iCount++;
								if (fCount > 0)
								{
									docTemplate.ReplaceText("<" + xmlField.Name + ">", xmlField.InnerText.Substring(fCount - 1, 1), false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
								}
								else
								{
									docTemplate.ReplaceText("<" + xmlField.Name + ">", "", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
								}
								for (int j = fCount - 2; j >= 0; j--)
								{
									if (fCount > 0)
									{
										docTemplate.ReplaceText(string.Concat(new object[]
										{
											"<",
											xmlField.Name,
											"*",
											x,
											">"
										}), xmlField.InnerText.Substring(j, 1), false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
									}
									else
									{
										docTemplate.ReplaceText(string.Concat(new object[]
										{
											"<",
											xmlField.Name,
											"*",
											x,
											">"
										}), "", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
									}
									x++;
								}
								if (fCount < iCount)
								{
									for (int k = fCount; k <= iCount; k++)
									{
										if (fCount > 0)
										{
											docTemplate.ReplaceText(string.Concat(new object[]
											{
												"<",
												xmlField.Name,
												"*",
												k,
												">"
											}), "*", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
										}
										else
										{
											docTemplate.ReplaceText(string.Concat(new object[]
											{
												"<",
												xmlField.Name,
												"*",
												k,
												">"
											}), "", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
										}
									}
								}
							}
							if (iCount == 0)
							{
								if (fCount > 0)
								{
									docTemplate.ReplaceText("<" + xmlField.Name + ">", xmlField.InnerText, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
								}
								else
								{
									docTemplate.ReplaceText("<" + xmlField.Name + ">", "", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
								}
							}
						}
					}
					xmlValues = value.SelectSingleNode("/save/komisja_sklad");
					if (xmlValues != null)
					{
						foreach (XmlNode xmlField in xmlValues)
						{
							foreach (Novacode.Table table in docTemplate.Tables)
							{
								int idxCommMember = 0;
								foreach (Row row in table.Rows)
								{
									idxCommMember++;
									if (row.Cells.Count > 1)
									{
										if (row.FindAll("<osoba_lp>").Count > 0 && xmlField.Attributes["obecny"].InnerText == "True")
										{
											Row rowNew = row;
											table.InsertRow(rowNew, idxCommMember);
											row.ReplaceText("<osoba_lp>", idxCommMember + ".", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
											row.ReplaceText("<osoba_ImieNazwisko>", string.Concat(new string[]
											{
												HttpUtility.UrlDecode(xmlField.Attributes["nazwisko"].InnerText),
												" ",
												HttpUtility.UrlDecode(xmlField.Attributes["imie"].InnerText),
												" ",
												HttpUtility.UrlDecode(xmlField.Attributes["imie2"].InnerText),
												" - ",
												xmlField.Attributes["funkcja"].InnerText
											}), false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
										}
									}
								}
							}
						}
					}
					if (controlSum != "")
					{
						this.GenerateBarcode(controlSum);
						int iLength = controlSum.Length;
						int iCount = iLength / 4;
						for (int iSign = iCount - 1; iSign > 0; iSign--)
						{
							controlSum = controlSum.Insert(iSign * 4, "-");
						}
						docTemplate.ReplaceText("<control_sum>", controlSum, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
					}
					else
					{
						docTemplate.ReplaceText("<control_sum>", "", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
					}
					foreach (Novacode.Table table in docTemplate.Tables)
					{
						int idxCand3 = 0;
						foreach (Row row in table.Rows)
						{
							if (row.FindAll("<osoba_lp>").Count > 0 || row.FindAll("<ERROR>").Count > 0)
							{
								row.Remove();
							}
							idxCand3++;
						}
					}
					docTemplate.ReplaceText("<field_3_14>", "", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
					docTemplate.ReplaceText("<field_3_15>", "", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
					docTemplate.ReplaceText("<field_3_16>", "", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
					docTemplate.ReplaceText("<field_3_17>", "", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
					docTemplate.ReplaceText("<field_3_18>", "", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
					docTemplate.ReplaceText("<field_3_19>", "", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
					docTemplate.ReplaceText("<field_3_20>", "", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
					docTemplate.SaveAs(this.newFileErr);
				}
			}
			xmlValues = value.SelectSingleNode("/save/form");
			if (xmlValues == null)
			{
				xmlValues = value.SelectSingleNode("/save/step3");
			}
			if (xmlValues == null)
			{
				xmlValues = value.SelectSingleNode("/save/step2");
			}
			if (xmlValues == null)
			{
				xmlValues = value.SelectSingleNode("/save/step1");
			}
			if (xmlValues == null)
			{
				this.newFile = docDefinitionPath.Replace(".docx", "TMP.docx");
				using (DocX docTemplate = DocX.Load(docDefinitionPath.Replace(".docx", "_EMPTY.docx")))
				{
					xmlValues = headerDefinition.SelectSingleNode("/akcja_wyborcza/jns");
					for (int iStale = 1; iStale < 3; iStale++)
					{
						string stalaName = "";
						string stalaValue = "";
						if (iStale == 1)
						{
							stalaName = "nrObwodu";
							stalaValue = obw;
						}
						if (iStale == 2)
						{
							stalaName = "nrOkregu";
							stalaValue = okr;
						}
						System.Collections.Generic.List<int> i = docTemplate.FindAll("<" + stalaName);
						int iCount = i.Count;
						int fCount = stalaValue.Length;
						int x = 2;
						if (iCount > 1)
						{
							docTemplate.ReplaceText("<" + stalaName + ">", stalaValue.Substring(fCount - 1, 1), false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
							for (int j = fCount - 2; j >= 0; j--)
							{
								docTemplate.ReplaceText(string.Concat(new object[]
								{
									"<",
									stalaName,
									"*",
									x,
									">"
								}), stalaValue.Substring(j, 1), false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
								x++;
							}
							if (fCount < iCount)
							{
								for (int k = fCount; k <= iCount; k++)
								{
									docTemplate.ReplaceText(string.Concat(new object[]
									{
										"<",
										stalaName,
										"*",
										k,
										">"
									}), "*", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
								}
							}
						}
						if (iCount == 1)
						{
							docTemplate.ReplaceText("<" + stalaName + ">", stalaValue, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
						}
					}
					System.Collections.IEnumerator enumerator;
					if (xmlValues.Attributes.Count > 0)
					{
						enumerator = xmlValues.Attributes.GetEnumerator();
						try
						{
							while (enumerator.MoveNext())
							{
								XmlAttribute xAttr = (XmlAttribute)enumerator.Current;
								System.Collections.Generic.List<int> i = docTemplate.FindAll("<" + xAttr.Name);
								int iCount = i.Count;
								int fCount = xAttr.InnerText.Length;
								int x = 2;
								if (iCount > 1)
								{
									docTemplate.ReplaceText("<" + xAttr.Name + ">", xAttr.InnerText.Substring(fCount - 1, 1), false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
									for (int j = fCount - 2; j >= 0; j--)
									{
										docTemplate.ReplaceText(string.Concat(new object[]
										{
											"<",
											xAttr.Name,
											"*",
											x,
											">"
										}), xAttr.InnerText.Substring(j, 1), false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
										x++;
									}
									if (fCount < iCount)
									{
										for (int k = fCount; k <= iCount; k++)
										{
											docTemplate.ReplaceText(string.Concat(new object[]
											{
												"<",
												xAttr.Name,
												"*",
												k,
												">"
											}), "*", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
										}
									}
								}
								if (iCount == 1)
								{
									docTemplate.ReplaceText("<" + xAttr.Name + ">", xAttr.Value, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
								}
							}
						}
						finally
						{
							System.IDisposable disposable = enumerator as System.IDisposable;
							if (disposable != null)
							{
								disposable.Dispose();
							}
						}
					}
					enumerator = xmlValues.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							XmlNode xObwod = (XmlNode)enumerator.Current;
							if (xObwod.Name == "obw" && xObwod.Attributes.GetNamedItem("nr") != null && xObwod.Attributes.GetNamedItem("nr").Value == obw)
							{
								docTemplate.ReplaceText("<siedziba>", xObwod.Attributes["siedziba"].InnerText, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
								foreach (XmlNode xInst in xObwod)
								{
									if (xInst.Name == "inst" && xInst.Attributes.GetNamedItem("kod") != null && xInst.Attributes.GetNamedItem("kod").Value == inst && xInst.Attributes.GetNamedItem("inst_jns") != null && xInst.Attributes.GetNamedItem("inst_jns").Value == instJNS)
									{
										foreach (XmlAttribute xAttr in xInst.Attributes)
										{
											if (xAttr.Name.ToUpper() == "ORGANNAZWA" || xAttr.Name.ToUpper() == "NAZWARADYDOPEL")
											{
												docTemplate.ReplaceText("<" + xAttr.Name + ">", xAttr.InnerText.ToUpper(), false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
												docTemplate.ReplaceText("<" + xAttr.Name.ToUpper() + ">", xAttr.InnerText.ToUpper(), false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
											}
											docTemplate.ReplaceText("<" + xAttr.Name + ">", xAttr.InnerText, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
											docTemplate.ReplaceText("<" + xAttr.Name.ToUpper() + ">", xAttr.InnerText, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
										}
										foreach (XmlNode xOkreg in xInst)
										{
											if (xOkreg.Name == "okr" && xOkreg.Attributes.GetNamedItem("nr") != null && xOkreg.Attributes.GetNamedItem("nr").Value == okr)
											{
												docTemplate.ReplaceText("<siedzibaR>", xOkreg.Attributes["siedzibaR"].InnerText, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
												docTemplate.ReplaceText("<siedzibaL>", xOkreg.Attributes["siedzibaL"].InnerText, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
												docTemplate.ReplaceText("<lmandatow>", xOkreg.Attributes["lmandatow"].InnerText, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
											}
										}
									}
								}
							}
						}
					}
					finally
					{
						System.IDisposable disposable = enumerator as System.IDisposable;
						if (disposable != null)
						{
							disposable.Dispose();
						}
					}
					XmlNode xmlListy = candidates.SelectSingleNode("/listy");
					int listaId = 0;
					bool listaSkreslona = false;
					enumerator = xmlListy.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							XmlNode xmlLista = (XmlNode)enumerator.Current;
							listaSkreslona = (xmlLista.Attributes["lista_status"].InnerText == "U");
							listaId++;
							if (isWielopak)
							{
								idxCand2 = 0;
							}
							foreach (Novacode.Table table in docTemplate.Tables)
							{
								int idxCand3 = 0;
								foreach (Row row in table.Rows)
								{
									if (row.FindAll("<Lista_L" + listaId + ">").Count > 0)
									{
										isWielopak = true;
										row.ReplaceText("<Lista_L" + listaId + ">", xmlLista.Attributes["nrlisty"].InnerText, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
										row.ReplaceText("<Lista_L" + listaId + "_skrot>", xmlLista.Attributes["oznaczenie_listy"].InnerText, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
									}
									if (row.FindAll("<Kandydtat_L" + listaId + "_liczba>").Count > 0 || row.FindAll("<Kandydtat_L" + listaId + "_razem>").Count > 0)
									{
										if (listaSkreslona && (this.newFile.Contains("RDA") || this.newFile.Contains("RDP") || this.newFile.Contains("RDW")))
										{
											row.Cells[1].InsertParagraph("X");
											row.Cells[2].InsertParagraph("X");
											row.Cells[3].InsertParagraph("X");
											row.Cells[4].InsertParagraph("X");
											row.Cells[5].InsertParagraph("X");
										}
										if (listaSkreslona && this.newFile.Contains("WBPTMP.docx"))
										{
											row.Cells[2].InsertParagraph("X");
											row.Cells[3].InsertParagraph("X");
											row.Cells[4].InsertParagraph("X");
											row.Cells[5].InsertParagraph("X");
											row.Cells[6].InsertParagraph("X");
										}
										row.ReplaceText("<Kandydtat_L" + listaId + "_liczba>", "Liczba głosów ważnych oddanych na listę:", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
										row.ReplaceText("<Kandydtat_L" + listaId + "_razem>", "Razem", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
									}
									if (row.FindAll("<kandydat_").Count > 0 || row.FindAll("<Kandydtat_L" + listaId + "_nazwisko_imie>").Count > 0)
									{
										idxCand3++;
										foreach (XmlNode xmlPerson in xmlLista)
										{
											if (xmlPerson.Attributes["status"].InnerText == "A" || xmlPerson.Attributes["status"].InnerText == "S")
											{
												idxCand2++;
												int iLP;
												if (this.newFile.Contains("RDATMP.docx") || this.newFile.Contains("WBPTMP.docx") || this.newFile.Contains("WBP_1TMP.docx"))
												{
													iLP = idxCand2;
												}
												else
												{
													iLP = idxCand2 + 2;
												}
												Row rowNew2 = table.InsertRow(row, iLP);
												rowNew2.ReplaceText("<kandydat_lp>", idxCand2 + ".", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
												rowNew2.ReplaceText("<Kandydtat_L" + listaId + "_lp>", idxCand2 + ".", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
												rowNew2.ReplaceText("<Kandydtat_L" + listaId + "_nazwisko_imie>", string.Concat(new string[]
												{
													xmlPerson.Attributes["nazwisko"].InnerText.ToUpper(),
													" ",
													xmlPerson.Attributes["imie1"].InnerText,
													" ",
													xmlPerson.Attributes["imie2"].InnerText
												}), false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
												rowNew2.ReplaceText("<kandydat_nazwisko_imie>", string.Concat(new string[]
												{
													xmlPerson.Attributes["nazwisko"].InnerText.ToUpper(),
													" ",
													xmlPerson.Attributes["imie1"].InnerText,
													" ",
													xmlPerson.Attributes["imie2"].InnerText
												}), false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
												if (xmlPerson.Attributes["kdt_plec"].InnerText == "K")
												{
													if (this.newFile.Contains("WBP"))
													{
														if (this.newFile.Contains("WBP_1"))
														{
															rowNew2.ReplaceText("<kandydat_zgloszony_przez>", xmlLista.Attributes["komitet_skrot"].InnerText, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
														}
														else
														{
															rowNew2.ReplaceText("<kandydat_zgloszony_przez>", "zgłoszona przez " + xmlLista.Attributes["komitet_skrot"].InnerText, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
														}
													}
													else
													{
														rowNew2.ReplaceText("<kandydat_zgloszony_przez>", "zgłoszona przez " + xmlLista.Attributes["komitet_skrot"].InnerText + ", Lista nr " + xmlLista.Attributes["nrlisty"].InnerText, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
													}
													rowNew2.ReplaceText("<Kandydtat_L" + listaId + "_zgloszony_przez>", "zgłoszona przez " + xmlLista.Attributes["komitet_nazwa"].InnerText, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
												}
												else
												{
													if (this.newFile.Contains("WBP"))
													{
														if (this.newFile.Contains("WBP_1"))
														{
															rowNew2.ReplaceText("<kandydat_zgloszony_przez>", xmlLista.Attributes["komitet_skrot"].InnerText, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
														}
														else
														{
															rowNew2.ReplaceText("<kandydat_zgloszony_przez>", "zgłoszony przez " + xmlLista.Attributes["komitet_skrot"].InnerText, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
														}
													}
													else
													{
														rowNew2.ReplaceText("<kandydat_zgloszony_przez>", "zgłoszony przez " + xmlLista.Attributes["komitet_skrot"].InnerText + ", Lista nr " + xmlLista.Attributes["nrlisty"].InnerText, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
													}
													rowNew2.ReplaceText("<Kandydtat_L" + listaId + "_zgloszony_przez>", "zgłoszony przez " + xmlLista.Attributes["komitet_nazwa"].InnerText, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
												}
												if ((xmlPerson.Attributes["status"].InnerText == "S" || listaSkreslona) && (this.newFile.Contains("RDA") || this.newFile.Contains("RDP") || this.newFile.Contains("RDW")) && rowNew2.Cells.Count == 7)
												{
													rowNew2.Cells[2].InsertParagraph("X");
													rowNew2.Cells[3].InsertParagraph("X");
													rowNew2.Cells[4].InsertParagraph("X");
													rowNew2.Cells[5].InsertParagraph("X");
													rowNew2.Cells[6].InsertParagraph("X");
												}
												if ((xmlPerson.Attributes["status"].InnerText == "S" || listaSkreslona) && this.newFile.Contains("WBPTMP.docx") && rowNew2.Cells.Count == 8)
												{
													rowNew2.Cells[3].InsertParagraph("X");
													rowNew2.Cells[4].InsertParagraph("X");
													rowNew2.Cells[5].InsertParagraph("X");
													rowNew2.Cells[6].InsertParagraph("X");
													rowNew2.Cells[7].InsertParagraph("X");
												}
											}
										}
									}
								}
							}
						}
					}
					finally
					{
						System.IDisposable disposable = enumerator as System.IDisposable;
						if (disposable != null)
						{
							disposable.Dispose();
						}
					}
					docTemplate.SaveAs(this.newFile);
				}
			}
			else
			{
				this.newFile = docDefinitionPath;
				using (DocX docTemplate = DocX.Load(this.newFile))
				{
					this.newFile = docDefinitionPath.Replace(".docx", "TMP.docx");
					XmlNode xmlListy = candidates.SelectSingleNode("/listy");
					int listaId = 0;
					bool listaSkreslona = false;
					foreach (XmlNode xmlLista in xmlListy)
					{
						listaSkreslona = (xmlLista.Attributes["lista_status"].InnerText == "U");
						listaId++;
						if (isWielopak)
						{
							idxCand2 = 0;
						}
						foreach (Novacode.Table table in docTemplate.Tables)
						{
							int idxCand3 = 0;
							foreach (Row row in table.Rows)
							{
								if (row.FindAll("<Lista_L" + listaId + ">").Count > 0)
								{
									isWielopak = true;
									row.ReplaceText("<Lista_L" + listaId + ">", xmlLista.Attributes["nrlisty"].InnerText, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
									row.ReplaceText("<Lista_L" + listaId + "_skrot>", xmlLista.Attributes["oznaczenie_listy"].InnerText, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
								}
								if (row.FindAll("<Lista_" + listaId + ">").Count > 0 && listaSkreslona)
								{
									string strNazwaListy = "Lista_" + listaId;
									System.Collections.Generic.List<int> iii = row.FindAll("<" + strNazwaListy);
									int iiiCount = iii.Count;
									row.ReplaceText("<" + strNazwaListy + ">", "X", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
									for (int kk = 0; kk <= iiiCount; kk++)
									{
										row.ReplaceText(string.Concat(new object[]
										{
											"<",
											strNazwaListy,
											"*",
											kk,
											">"
										}), "X", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
									}
								}
								if (row.FindAll("<razem_L" + listaId + ">").Count > 0 && listaSkreslona)
								{
									string strNazwaListy = "razem_L" + listaId;
									System.Collections.Generic.List<int> iii = row.FindAll("<" + strNazwaListy);
									int iiiCount = iii.Count;
									row.ReplaceText("<" + strNazwaListy + ">", "X", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
									for (int kk = 0; kk <= iiiCount; kk++)
									{
										row.ReplaceText(string.Concat(new object[]
										{
											"<",
											strNazwaListy,
											"*",
											kk,
											">"
										}), "X", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
									}
								}
								if (row.FindAll("<kandydat_").Count > 0 || row.FindAll("<Kandydtat_L" + listaId + "_nazwisko_imie>").Count > 0)
								{
									idxCand3++;
									foreach (XmlNode xmlPerson in xmlLista)
									{
										if (xmlPerson.Attributes["status"].InnerText == "A" || xmlPerson.Attributes["status"].InnerText == "S")
										{
											idxCand2++;
											int iLP;
											if (this.newFile.Contains("RDATMP.docx") || this.newFile.Contains("WBPTMP.docx") || this.newFile.Contains("WBP_1TMP.docx"))
											{
												iLP = idxCand2;
											}
											else
											{
												iLP = idxCand2 + 2;
											}
											Row rowNew2 = table.InsertRow(row, iLP);
											rowNew2.ReplaceText("<kandydat_lp>", idxCand2 + ".", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
											rowNew2.ReplaceText("<Kandydtat_L" + listaId + "_lp>", idxCand2 + ".", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
											rowNew2.ReplaceText("<Kandydtat_L" + listaId + "_nazwisko_imie>", string.Concat(new string[]
											{
												xmlPerson.Attributes["nazwisko"].InnerText.ToUpper(),
												" ",
												xmlPerson.Attributes["imie1"].InnerText,
												" ",
												xmlPerson.Attributes["imie2"].InnerText
											}), false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
											rowNew2.ReplaceText("<kandydat_nazwisko_imie>", string.Concat(new string[]
											{
												xmlPerson.Attributes["nazwisko"].InnerText.ToUpper(),
												" ",
												xmlPerson.Attributes["imie1"].InnerText,
												" ",
												xmlPerson.Attributes["imie2"].InnerText
											}), false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
											if (xmlPerson.Attributes["kdt_plec"].InnerText == "K")
											{
												if (this.newFile.Contains("WBP"))
												{
													if (this.newFile.Contains("WBP_1"))
													{
														rowNew2.ReplaceText("<kandydat_zgloszony_przez>", xmlLista.Attributes["komitet_skrot"].InnerText, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
													}
													else
													{
														rowNew2.ReplaceText("<kandydat_zgloszony_przez>", "zgłoszona przez " + xmlLista.Attributes["komitet_skrot"].InnerText, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
													}
												}
												else
												{
													rowNew2.ReplaceText("<kandydat_zgloszony_przez>", "zgłoszona przez " + xmlLista.Attributes["komitet_skrot"].InnerText + ", Lista nr " + xmlLista.Attributes["nrlisty"].InnerText, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
												}
												rowNew2.ReplaceText("<Kandydtat_L" + listaId + "_zgloszony_przez>", "zgłoszona przez " + xmlLista.Attributes["komitet_nazwa"].InnerText, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
											}
											else
											{
												if (this.newFile.Contains("WBP"))
												{
													if (this.newFile.Contains("WBP_1"))
													{
														rowNew2.ReplaceText("<kandydat_zgloszony_przez>", xmlLista.Attributes["komitet_skrot"].InnerText, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
													}
													else
													{
														rowNew2.ReplaceText("<kandydat_zgloszony_przez>", "zgłoszony przez " + xmlLista.Attributes["komitet_skrot"].InnerText, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
													}
												}
												else
												{
													rowNew2.ReplaceText("<kandydat_zgloszony_przez>", "zgłoszony przez " + xmlLista.Attributes["komitet_skrot"].InnerText + ", Lista nr " + xmlLista.Attributes["nrlisty"].InnerText, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
												}
												rowNew2.ReplaceText("<Kandydtat_L" + listaId + "_zgloszony_przez>", "zgłoszony przez " + xmlLista.Attributes["komitet_nazwa"].InnerText, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
											}
											if (xmlPerson.Attributes["status"].InnerText == "S" || listaSkreslona)
											{
												string strKandS;
												if (isWielopak)
												{
													strKandS = "Kandydtat_L" + listaId;
												}
												else
												{
													strKandS = "kandydat";
												}
												System.Collections.Generic.List<int> iii = rowNew2.FindAll("<" + strKandS + "_g");
												int iiiCount = iii.Count;
												rowNew2.ReplaceText("<" + strKandS + "_g>", "X", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
												for (int kk = 0; kk <= iiiCount; kk++)
												{
													rowNew2.ReplaceText(string.Concat(new object[]
													{
														"<",
														strKandS,
														"_g*",
														kk,
														">"
													}), "X", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
												}
											}
											else
											{
												XmlNode xmlSaveValues = value.SelectSingleNode("/save/form");
												if (xmlSaveValues == null)
												{
													xmlSaveValues = value.SelectSingleNode("/save/step3");
												}
												if (xmlSaveValues == null)
												{
													xmlSaveValues = value.SelectSingleNode("/save/step2");
												}
												if (xmlSaveValues == null)
												{
													xmlSaveValues = value.SelectSingleNode("/save/step1");
												}
												foreach (XmlNode xmlSaveField in xmlSaveValues)
												{
													if (xmlSaveField.Attributes.Count > 0)
													{
														if (xmlSaveField.Attributes["id_kand"].InnerText == xmlPerson.Attributes["id_kand"].InnerText && xmlPerson.Attributes["status"].InnerText == "A")
														{
															string strKand;
															if (xmlSaveField.Name.Substring(0, 11) == "Kandydtat_L")
															{
																strKand = xmlSaveField.Name.Substring(0, 13);
																if (strKand.Substring(12, 1) == "_")
																{
																	strKand = xmlSaveField.Name.Substring(0, 12);
																}
															}
															else
															{
																strKand = "kandydat";
															}
															System.Collections.Generic.List<int> ii = rowNew2.FindAll("<" + strKand + "_g");
															int iiCount = ii.Count;
															int ffCount = xmlSaveField.InnerText.Length;
															int xx = 2;
															if (iiCount > 1)
															{
																if (ffCount > 0)
																{
																	rowNew2.ReplaceText("<" + strKand + "_g>", xmlSaveField.InnerText.Substring(ffCount - 1, 1), false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
																}
																else
																{
																	rowNew2.ReplaceText("<" + strKand + "_g>", "", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
																}
																for (int jj = ffCount - 2; jj >= 0; jj--)
																{
																	if (ffCount > 0)
																	{
																		rowNew2.ReplaceText(string.Concat(new object[]
																		{
																			"<",
																			strKand,
																			"_g*",
																			xx,
																			">"
																		}), xmlSaveField.InnerText.Substring(jj, 1), false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
																	}
																	else
																	{
																		rowNew2.ReplaceText(string.Concat(new object[]
																		{
																			"<",
																			strKand,
																			"_g*",
																			xx,
																			">"
																		}), "", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
																	}
																	xx++;
																}
																if (ffCount < iiCount)
																{
																	for (int kk = ffCount; kk <= iiCount; kk++)
																	{
																		if (ffCount > 0)
																		{
																			rowNew2.ReplaceText(string.Concat(new object[]
																			{
																				"<",
																				strKand,
																				"_g*",
																				kk,
																				">"
																			}), "*", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
																		}
																		else
																		{
																			rowNew2.ReplaceText(string.Concat(new object[]
																			{
																				"<",
																				strKand,
																				"_g*",
																				kk,
																				">"
																			}), "", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
																		}
																	}
																}
															}
															if (iiCount == 1)
															{
																rowNew2.ReplaceText("<" + strKand + "_g>", xmlSaveField.InnerText, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
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
					}
					for (int a = 1; a <= 3; a++)
					{
						xmlValues = value.SelectSingleNode("/save/header");
						if (a == 2)
						{
							xmlValues = value.SelectSingleNode("/save/form");
							if (xmlValues == null)
							{
								xmlValues = value.SelectSingleNode("/save/step4");
							}
							if (xmlValues == null)
							{
								xmlValues = value.SelectSingleNode("/save/step3");
							}
							if (xmlValues == null)
							{
								xmlValues = value.SelectSingleNode("/save/step2");
							}
							if (xmlValues == null)
							{
								xmlValues = value.SelectSingleNode("/save/step1");
							}
						}
						if (a == 3)
						{
							xmlValues = value.SelectSingleNode("/save/komisja_sklad");
						}
						if (xmlValues != null)
						{
							foreach (XmlNode xmlField in xmlValues)
							{
								if (a == 3)
								{
									foreach (Novacode.Table table in docTemplate.Tables)
									{
										int idxCommMember = 0;
										foreach (Row row in table.Rows)
										{
											idxCommMember++;
											if (row.Cells.Count > 1)
											{
												if (row.FindAll("<osoba_lp>").Count > 0 && xmlField.Attributes["obecny"].InnerText == "True")
												{
													Row rowNew = row;
													table.InsertRow(rowNew, idxCommMember);
													row.ReplaceText("<osoba_lp>", idxCommMember + ".", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
													row.ReplaceText("<osoba_ImieNazwisko>", string.Concat(new string[]
													{
														HttpUtility.UrlDecode(xmlField.Attributes["nazwisko"].InnerText),
														" ",
														HttpUtility.UrlDecode(xmlField.Attributes["imie"].InnerText),
														" ",
														HttpUtility.UrlDecode(xmlField.Attributes["imie2"].InnerText),
														" - ",
														xmlField.Attributes["funkcja"].InnerText
													}), false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
												}
											}
										}
									}
								}
								else
								{
									System.Collections.Generic.List<int> i = docTemplate.FindAll("<" + xmlField.Name + "*");
									int iCount = i.Count;
									int fCount = xmlField.InnerText.Length;
									int x = 2;
									if (iCount >= 1)
									{
										iCount++;
										if (fCount > 0)
										{
											docTemplate.ReplaceText("<" + xmlField.Name + ">", xmlField.InnerText.Substring(fCount - 1, 1), false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
										}
										else
										{
											docTemplate.ReplaceText("<" + xmlField.Name + ">", "", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
										}
										for (int j = fCount - 2; j >= 0; j--)
										{
											if (fCount > 0)
											{
												docTemplate.ReplaceText(string.Concat(new object[]
												{
													"<",
													xmlField.Name,
													"*",
													x,
													">"
												}), xmlField.InnerText.Substring(j, 1), false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
											}
											else
											{
												docTemplate.ReplaceText(string.Concat(new object[]
												{
													"<",
													xmlField.Name,
													"*",
													x,
													">"
												}), "", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
											}
											x++;
										}
										if (fCount < iCount)
										{
											for (int k = fCount; k <= iCount; k++)
											{
												if (fCount > 0)
												{
													docTemplate.ReplaceText(string.Concat(new object[]
													{
														"<",
														xmlField.Name,
														"*",
														k,
														">"
													}), "*", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
												}
												else
												{
													docTemplate.ReplaceText(string.Concat(new object[]
													{
														"<",
														xmlField.Name,
														"*",
														k,
														">"
													}), "", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
												}
											}
										}
									}
									if (iCount == 0)
									{
										if (fCount > 0)
										{
											docTemplate.ReplaceText("<" + xmlField.Name + ">", xmlField.InnerText, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
										}
										else
										{
											docTemplate.ReplaceText("<" + xmlField.Name + ">", "", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
										}
									}
									if (xmlField.Attributes.Count > 0 && xmlField.Name.Substring(0, 11) == "Kandydtat_L")
									{
										isWielopak = true;
									}
								}
							}
						}
					}
					docTemplate.SaveAs(this.newFile);
				}
			}
			using (DocX docTemplate = DocX.Load(this.newFile))
			{
				foreach (Novacode.Table table in docTemplate.Tables)
				{
					foreach (Row row in table.Rows)
					{
						if (row.Cells.Count > 1)
						{
							if (row.FindAll("<osoba_lp>").Count > 0 || row.FindAll("<Kandydtat_L").Count > 0 || row.FindAll("<kandydat_").Count > 0)
							{
								row.Remove();
							}
						}
					}
				}
				docTemplate.ReplaceText("<field_3_14>", "", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
				docTemplate.ReplaceText("<field_3_15>", "", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
				docTemplate.ReplaceText("<field_3_16>", "", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
				docTemplate.ReplaceText("<field_3_17>", "", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
				docTemplate.ReplaceText("<field_3_18>", "", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
				docTemplate.ReplaceText("<field_3_19>", "", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
				docTemplate.ReplaceText("<field_3_20>", "", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
				if (controlSum != "")
				{
					this.GenerateBarcode(controlSum);
					int iLength = controlSum.Length;
					int iCount = iLength / 4;
					for (int iSign = iCount - 1; iSign > 0; iSign--)
					{
						controlSum = controlSum.Insert(iSign * 4, "-");
					}
					docTemplate.ReplaceText("<control_sum>", controlSum, false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
				}
				else
				{
					docTemplate.ReplaceText("<control_sum>", "", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.SubsetMatch);
				}
				docTemplate.Save();
			}
			if (isWielopak)
			{
				for (int iTab = 1; iTab < 20; iTab++)
				{
					this.DocxDeleteTables(this.newFile);
				}
			}
			if (this.imageBarCode != null && this.newFile != "")
			{
				this.DocxInsertBarcode(this.newFile);
			}
			if (this.imageBarCode != null && this.newFileErr != "")
			{
				this.DocxInsertBarcode(this.newFileErr);
			}
			if (!printToPDF)
			{
				if (this.printDialog1.ShowDialog() == DialogResult.OK)
				{
					if (this.newFileErr != "")
					{
						System.Threading.Thread printThread = new System.Threading.Thread(new System.Threading.ThreadStart(this.PrintWarnings));
						printThread.Start();
					}
					System.Threading.Thread printThread2 = new System.Threading.Thread(new System.Threading.ThreadStart(this.Print));
					printThread2.Start();
				}
			}
			else
			{
				if (docErrPath != "" && this.newFileErr != "")
				{
					SaveFileDialog sd = new SaveFileDialog();
					sd.Title = "Zapisz raport ostrzeżeń jako PDF...";
					sd.Filter = "Pliki PDF | *.pdf";
					sd.DefaultExt = "pdf";
					sd.AddExtension = true;
					sd.FileName = System.IO.Path.GetFileName(docErrPath).Replace("TMP.docx", ".pdf");
					if (sd.ShowDialog() == DialogResult.OK)
					{
						PDFEncoderParams pem = new PDFEncoderParams();
						DocumentManager dm = new DocumentManager();
						dm.LoadDocument(this.newFileErr, "");
						if (sd.FileName.Contains("."))
						{
							sd.FileName.Remove(sd.FileName.IndexOf("."), sd.FileName.Length - 1 - sd.FileName.IndexOf("."));
							sd.FileName += ".pdf";
						}
						else
						{
							sd.FileName += ".pdf";
						}
						dm.ConvertDocument(dm.Documents[0], sd.FileName, "pdf", null, pem, "");
						dm.CloseAllDocuments();
					}
				}
				SaveFileDialog sd2 = new SaveFileDialog();
				sd2.Title = "Zapisz protokół jako PDF...";
				sd2.Filter = "Pliki PDF | *.pdf";
				sd2.DefaultExt = "pdf";
				sd2.AddExtension = true;
				sd2.FileName = System.IO.Path.GetFileName(this.newFile).Replace("TMP.docx", ".pdf");
				if (sd2.ShowDialog() == DialogResult.OK)
				{
					PDFEncoderParams pem2 = new PDFEncoderParams();
					DocumentManager dm2 = new DocumentManager();
					dm2.LoadDocument(this.newFile, "");
					if (sd2.FileName.Contains("."))
					{
						sd2.FileName.Remove(sd2.FileName.IndexOf("."), sd2.FileName.Length - 1 - sd2.FileName.IndexOf("."));
						sd2.FileName += ".pdf";
					}
					else
					{
						sd2.FileName += ".pdf";
					}
					dm2.ConvertDocument(dm2.Documents[0], sd2.FileName, "pdf", null, pem2, "");
					dm2.CloseAllDocuments();
				}
			}
		}
		private void Print()
		{
			DocumentPrinter documentPrinter = new DocumentPrinter();
			documentPrinter.PrintDocument.PrinterSettings = this.printDialog1.PrinterSettings;
			documentPrinter.PrintDocument.DefaultPageSettings.Margins.Top = 0;
			documentPrinter.PrintDocument.DefaultPageSettings.Margins.Right = 0;
			documentPrinter.PrintDocument.DefaultPageSettings.Margins.Bottom = 0;
			documentPrinter.PrintDocument.DefaultPageSettings.Margins.Left = 0;
			documentPrinter.PagePositioning.Horizontal = HPagePosition.Left;
			documentPrinter.PagePositioning.Vertical = VPagePosition.Top;
			documentPrinter.PageScaling = PageScalingOptions.Fit;
			documentPrinter.PrintDocument.DocumentName = this.newFile;
			System.IO.Stream file = new System.IO.FileStream(this.newFile, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
			documentPrinter.Print(file, "");
			Application.ExitThread();
		}
		private void PrintWarnings()
		{
			DocumentPrinter documentPrinter = new DocumentPrinter();
			documentPrinter.PrintDocument.PrinterSettings = this.printDialog1.PrinterSettings;
			documentPrinter.PrintDocument.DefaultPageSettings.Margins.Top = 0;
			documentPrinter.PrintDocument.DefaultPageSettings.Margins.Right = 0;
			documentPrinter.PrintDocument.DefaultPageSettings.Margins.Bottom = 0;
			documentPrinter.PrintDocument.DefaultPageSettings.Margins.Left = 0;
			documentPrinter.PagePositioning.Horizontal = HPagePosition.Left;
			documentPrinter.PagePositioning.Vertical = VPagePosition.Top;
			documentPrinter.PageScaling = PageScalingOptions.Fit;
			documentPrinter.PrintDocument.DocumentName = this.newFileErr;
			System.IO.Stream file = new System.IO.FileStream(this.newFileErr, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
			documentPrinter.Print(file, "");
			Application.ExitThread();
		}
		private void GenerateBarcode(string controlSum)
		{
			Code128BarcodeDraw bdf = BarcodeDrawFactory.Code128WithChecksum;
			PictureBox picCodeBar = new PictureBox();
			picCodeBar.Image = bdf.Draw(controlSum, 40);
			System.IO.MemoryStream ms = new System.IO.MemoryStream();
			picCodeBar.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
			byte[] imageBytes = ms.ToArray();
			this.imageBarCode = System.Convert.ToBase64String(imageBytes);
		}
		private void DocxInsertBarcode(string docxPath)
		{
			using (WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(docxPath, true))
			{
				foreach (FooterPart mainPart in wordprocessingDocument.MainDocumentPart.FooterParts)
				{
					if (mainPart.ImageParts.Count<ImagePart>() > 0)
					{
						ImagePart imagePart = mainPart.ImageParts.FirstOrDefault<ImagePart>();
						System.IO.Stream data = this.GetBinaryDataStream(this.imageBarCode);
						imagePart.FeedData(data);
						data.Close();
						wordprocessingDocument.MainDocumentPart.Document.Save();
					}
				}
			}
		}
		public static string EncodeTo64(string toEncode)
		{
			byte[] toEncodeAsBytes = System.Text.Encoding.ASCII.GetBytes(toEncode);
			return System.Convert.ToBase64String(toEncodeAsBytes);
		}
		private void DocxDeleteTables(string docxPath)
		{
			using (WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(docxPath, true))
			{
				System.Collections.Generic.IEnumerable<DocumentFormat.OpenXml.Wordprocessing.Table> table = wordprocessingDocument.MainDocumentPart.Document.Body.Elements<DocumentFormat.OpenXml.Wordprocessing.Table>();
				foreach (DocumentFormat.OpenXml.Wordprocessing.Table i in table)
				{
					if (i.InnerText.Contains("<Lista_"))
					{
						i.Remove();
					}
				}
				wordprocessingDocument.MainDocumentPart.Document.Save();
			}
		}
		private bool isOneCandidate(XmlNode candidates)
		{
			XmlNode c = candidates.SelectSingleNode("/listy");
			return c.ChildNodes.Count <= 1 && c.ChildNodes.Count != 0 && c.FirstChild.ChildNodes.Count <= 1;
		}
	}
}
