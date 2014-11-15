// Decompiled with JetBrains decompiler
// Type: Kalkulator1.printProtocolNew
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using Gnostice.Documents;
using Gnostice.Documents.Controls.WinForms;
using Novacode;
using System;
using System.Collections.Generic;
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

    private Stream GetBinaryDataStream(string base64String)
    {
      return (Stream) new MemoryStream(Convert.FromBase64String(base64String));
    }

    public void ProtocolPrint(System.Xml.XmlDocument headerDefinition, System.Xml.XmlDocument value, System.Xml.XmlDocument candidates, string docDefinitionPath, string controlSum, bool printToPDF, string obw, string inst, string okr, string candidatesPath, string instJNS)
    {
      Framework.ActivateLicense("4E5A-14CC-D4D2-14C2-F558-B99F-C5F5-5E4B");
      bool flag1 = false;
      bool flag2 = false;
      string str1 = "";
      int num1 = 0;
      XmlNode xmlNode1 = headerDefinition.SelectSingleNode("/akcja_wyborcza/jns");
      string str2 = xmlNode1.Attributes.GetNamedItem("jns_kod").Value;
      string str3 = "";
      foreach (XmlNode xmlNode2 in xmlNode1)
      {
        if (xmlNode2.Attributes["nr"].InnerText == obw)
        {
          foreach (XmlNode xmlNode3 in xmlNode2)
          {
            if (xmlNode3.Attributes["kod"].InnerText == inst)
            {
              foreach (XmlNode xmlNode4 in xmlNode3)
              {
                if (xmlNode4.Attributes["nr"].InnerText == okr && Convert.ToInt32(xmlNode3.Attributes["inst_jns"].InnerText) == Convert.ToInt32(instJNS))
                {
                  str3 = xmlNode3.Attributes["organNazwa"].InnerText;
                  break;
                }
              }
            }
          }
        }
      }
      if (inst == "WBP")
      {
        if (str2.Substring(0, 4) == "1465" && str2.Length == 6)
        {
          string str4 = candidatesPath.Replace(str2 + "-" + okr + ".xml", "146501-1.xml");
          if (File.Exists(str4))
            candidates.Load(str4);
        }
        if (this.isOneCandidate((XmlNode) candidates))
        {
          docDefinitionPath = docDefinitionPath.Replace("WBP", "WBP_1");
          flag2 = true;
        }
      }
      if (inst == "RDA")
      {
        if (str2.Length < 6)
        {
          while (str2.Length < 6)
            str2 = "0" + str2;
        }
        if ((int) str2[2] == 55 || (int) str2[2] == 54)
        {
          if (str2.Substring(0, 4) == "1465" && str3 == "m.st.")
          {
            docDefinitionPath = docDefinitionPath.Replace("RDA", "RDA_M");
            string str4 = candidatesPath.Replace(str2 + "-" + okr + ".xml", "146501-" + okr + ".xml");
            if (File.Exists(str4))
              candidates.Load(str4);
          }
          if (str2.Substring(0, 4) != "1465")
            docDefinitionPath = docDefinitionPath.Replace("RDA", "RDA_M");
        }
        if (str2.Substring(0, 4) == "1465" && str3 == "Dzielnicy")
          docDefinitionPath = docDefinitionPath.Replace("RDA", "RDA_D");
      }
      XmlNode xmlNode5 = (XmlNode) new System.Xml.XmlDocument();
      if (((value.SelectSingleNode("/save/hardError") ?? value.SelectSingleNode("/save/hardWarning")) ?? value.SelectSingleNode("/save/softError")) != null)
      {
        str1 = docDefinitionPath.Replace(".docx", "_ERR.docx");
        this.newFileErr = str1.Replace(".docx", "TMP.docx");
        using (DocX docX = DocX.Load(str1))
        {
          XmlNode xmlNode2 = value.SelectSingleNode("/save/report");
          if (xmlNode2 != null && xmlNode2 != null)
          {
            foreach (XmlNode xmlNode3 in xmlNode2)
            {
              foreach (Novacode.Table table in docX.Tables)
              {
                int index = 0;
                foreach (Row row1 in table.Rows)
                {
                  ++index;
                  if (row1.Cells.Count == 1 && row1.FindAll("<ERROR>").Count > 0)
                  {
                    Row row2 = row1;
                    table.InsertRow(row2, index);
                    row1.ReplaceText("<ERROR>", "[" + xmlNode3.Name.Substring(0, xmlNode3.Name.IndexOf("_")) + "]" + Environment.NewLine + "Stanowisko komisji: " + xmlNode3.InnerText, 0 != 0, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                  }
                }
              }
            }
          }
          XmlNode xmlNode4 = value.SelectSingleNode("/save/header");
          if (xmlNode4 != null)
          {
            foreach (XmlNode xmlNode3 in xmlNode4)
            {
              int count = docX.FindAll("<" + xmlNode3.Name + "*").Count;
              int length = xmlNode3.InnerText.Length;
              int num2 = 2;
              if (count >= 1)
              {
                ++count;
                if (length > 0)
                  docX.ReplaceText("<" + xmlNode3.Name + ">", xmlNode3.InnerText.Substring(length - 1, 1), false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                else
                  docX.ReplaceText("<" + xmlNode3.Name + ">", "", false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                for (int startIndex = length - 2; startIndex >= 0; --startIndex)
                {
                  if (length > 0)
                    docX.ReplaceText("<" + (object) xmlNode3.Name + "*" + (string) (object) num2 + ">", xmlNode3.InnerText.Substring(startIndex, 1), 0 != 0, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                  else
                    docX.ReplaceText("<" + (object) xmlNode3.Name + "*" + (string) (object) num2 + ">", "", 0 != 0, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                  ++num2;
                }
                if (length < count)
                {
                  for (int index = length; index <= count; ++index)
                  {
                    if (length > 0)
                      docX.ReplaceText("<" + (object) xmlNode3.Name + "*" + (string) (object) index + ">", "*", 0 != 0, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                    else
                      docX.ReplaceText("<" + (object) xmlNode3.Name + "*" + (string) (object) index + ">", "", 0 != 0, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                  }
                }
              }
              if (count == 0)
              {
                if (length > 0)
                  docX.ReplaceText("<" + xmlNode3.Name + ">", xmlNode3.InnerText, false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                else
                  docX.ReplaceText("<" + xmlNode3.Name + ">", "", false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
              }
            }
          }
          XmlNode xmlNode6 = value.SelectSingleNode("/save/komisja_sklad");
          if (xmlNode6 != null)
          {
            foreach (XmlNode xmlNode3 in xmlNode6)
            {
              foreach (Novacode.Table table in docX.Tables)
              {
                int index = 0;
                foreach (Row row1 in table.Rows)
                {
                  ++index;
                  if (row1.Cells.Count > 1 && (row1.FindAll("<osoba_lp>").Count > 0 && xmlNode3.Attributes["obecny"].InnerText == "True"))
                  {
                    Row row2 = row1;
                    table.InsertRow(row2, index);
                    row1.ReplaceText("<osoba_lp>", (string) (object) index + (object) ".", false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                    row1.ReplaceText("<osoba_ImieNazwisko>", HttpUtility.UrlDecode(xmlNode3.Attributes["nazwisko"].InnerText) + " " + HttpUtility.UrlDecode(xmlNode3.Attributes["imie"].InnerText) + " " + HttpUtility.UrlDecode(xmlNode3.Attributes["imie2"].InnerText) + " - " + xmlNode3.Attributes["funkcja"].InnerText, 0 != 0, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                  }
                }
              }
            }
          }
          if (controlSum != "")
          {
            this.GenerateBarcode(controlSum);
            for (int index = controlSum.Length / 4 - 1; index > 0; --index)
              controlSum = controlSum.Insert(index * 4, "-");
            docX.ReplaceText("<control_sum>", controlSum, false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
          }
          else
            docX.ReplaceText("<control_sum>", "", false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
          foreach (Novacode.Table table in docX.Tables)
          {
            int num2 = 0;
            foreach (Row row in table.Rows)
            {
              if (row.FindAll("<osoba_lp>").Count > 0 || row.FindAll("<ERROR>").Count > 0)
                row.Remove();
              ++num2;
            }
          }
          docX.ReplaceText("<field_3_14>", "", false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
          docX.ReplaceText("<field_3_15>", "", false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
          docX.ReplaceText("<field_3_16>", "", false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
          docX.ReplaceText("<field_3_17>", "", false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
          docX.ReplaceText("<field_3_18>", "", false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
          docX.ReplaceText("<field_3_19>", "", false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
          docX.ReplaceText("<field_3_20>", "", false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
          docX.SaveAs(this.newFileErr);
        }
      }
      if ((((value.SelectSingleNode("/save/form") ?? value.SelectSingleNode("/save/step3")) ?? value.SelectSingleNode("/save/step2")) ?? value.SelectSingleNode("/save/step1")) == null)
      {
        this.newFile = docDefinitionPath.Replace(".docx", "TMP.docx");
        using (DocX docX = DocX.Load(docDefinitionPath.Replace(".docx", "_EMPTY.docx")))
        {
          XmlNode xmlNode2 = headerDefinition.SelectSingleNode("/akcja_wyborcza/jns");
          for (int index1 = 1; index1 < 3; ++index1)
          {
            string str4 = "";
            string newValue = "";
            if (index1 == 1)
            {
              str4 = "nrObwodu";
              newValue = obw;
            }
            if (index1 == 2)
            {
              str4 = "nrOkregu";
              newValue = okr;
            }
            int count = docX.FindAll("<" + str4).Count;
            int length = newValue.Length;
            int num2 = 2;
            if (count > 1)
            {
              docX.ReplaceText("<" + str4 + ">", newValue.Substring(length - 1, 1), false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
              for (int startIndex = length - 2; startIndex >= 0; --startIndex)
              {
                docX.ReplaceText("<" + (object) str4 + "*" + (string) (object) num2 + ">", newValue.Substring(startIndex, 1), 0 != 0, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                ++num2;
              }
              if (length < count)
              {
                for (int index2 = length; index2 <= count; ++index2)
                  docX.ReplaceText("<" + (object) str4 + "*" + (string) (object) index2 + ">", "*", 0 != 0, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
              }
            }
            if (count == 1)
              docX.ReplaceText("<" + str4 + ">", newValue, false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
          }
          if (xmlNode2.Attributes.Count > 0)
          {
            foreach (XmlAttribute xmlAttribute in (XmlNamedNodeMap) xmlNode2.Attributes)
            {
              int count = docX.FindAll("<" + xmlAttribute.Name).Count;
              int length = xmlAttribute.InnerText.Length;
              int num2 = 2;
              if (count > 1)
              {
                docX.ReplaceText("<" + xmlAttribute.Name + ">", xmlAttribute.InnerText.Substring(length - 1, 1), false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                for (int startIndex = length - 2; startIndex >= 0; --startIndex)
                {
                  docX.ReplaceText("<" + (object) xmlAttribute.Name + "*" + (string) (object) num2 + ">", xmlAttribute.InnerText.Substring(startIndex, 1), 0 != 0, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                  ++num2;
                }
                if (length < count)
                {
                  for (int index = length; index <= count; ++index)
                    docX.ReplaceText("<" + (object) xmlAttribute.Name + "*" + (string) (object) index + ">", "*", 0 != 0, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                }
              }
              if (count == 1)
                docX.ReplaceText("<" + xmlAttribute.Name + ">", xmlAttribute.Value, false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
            }
          }
          foreach (XmlNode xmlNode3 in xmlNode2)
          {
            if (xmlNode3.Name == "obw" && xmlNode3.Attributes.GetNamedItem("nr") != null && xmlNode3.Attributes.GetNamedItem("nr").Value == obw)
            {
              docX.ReplaceText("<siedziba>", xmlNode3.Attributes["siedziba"].InnerText, false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
              foreach (XmlNode xmlNode4 in xmlNode3)
              {
                if (xmlNode4.Name == "inst" && xmlNode4.Attributes.GetNamedItem("kod") != null && (xmlNode4.Attributes.GetNamedItem("kod").Value == inst && xmlNode4.Attributes.GetNamedItem("inst_jns") != null) && xmlNode4.Attributes.GetNamedItem("inst_jns").Value == instJNS)
                {
                  foreach (XmlAttribute xmlAttribute in (XmlNamedNodeMap) xmlNode4.Attributes)
                  {
                    if (xmlAttribute.Name.ToUpper() == "ORGANNAZWA" || xmlAttribute.Name.ToUpper() == "NAZWARADYDOPEL")
                    {
                      docX.ReplaceText("<" + xmlAttribute.Name + ">", xmlAttribute.InnerText.ToUpper(), false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                      docX.ReplaceText("<" + xmlAttribute.Name.ToUpper() + ">", xmlAttribute.InnerText.ToUpper(), false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                    }
                    docX.ReplaceText("<" + xmlAttribute.Name + ">", xmlAttribute.InnerText, false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                    docX.ReplaceText("<" + xmlAttribute.Name.ToUpper() + ">", xmlAttribute.InnerText, false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                  }
                  foreach (XmlNode xmlNode6 in xmlNode4)
                  {
                    if (xmlNode6.Name == "okr" && xmlNode6.Attributes.GetNamedItem("nr") != null && xmlNode6.Attributes.GetNamedItem("nr").Value == okr)
                    {
                      docX.ReplaceText("<siedzibaR>", xmlNode6.Attributes["siedzibaR"].InnerText, false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                      docX.ReplaceText("<siedzibaL>", xmlNode6.Attributes["siedzibaL"].InnerText, false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                      docX.ReplaceText("<lmandatow>", xmlNode6.Attributes["lmandatow"].InnerText, false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                    }
                  }
                }
              }
            }
          }
          XmlNode xmlNode7 = candidates.SelectSingleNode("/listy");
          int num3 = 0;
          foreach (XmlNode xmlNode3 in xmlNode7)
          {
            ++num3;
            if (flag1)
              num1 = 0;
            foreach (Novacode.Table table in docX.Tables)
            {
              int num2 = 0;
              foreach (Row row1 in table.Rows)
              {
                if (row1.FindAll("<Lista_L" + (object) num3 + ">").Count > 0)
                {
                  flag1 = true;
                  row1.ReplaceText("<Lista_L" + (object) num3 + ">", xmlNode3.Attributes["nrlisty"].InnerText, false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                  row1.ReplaceText("<Lista_L" + (object) num3 + "_skrot>", xmlNode3.Attributes["oznaczenie_listy"].InnerText, false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                }
                if (row1.FindAll("<kandydat_nazwisko_imie>").Count > 0 || row1.FindAll("<Kandydtat_L" + (object) num3 + "_nazwisko_imie>").Count > 0)
                {
                  ++num2;
                  foreach (XmlNode xmlNode4 in xmlNode3)
                  {
                    if (xmlNode4.Attributes["status"].InnerText == "A")
                    {
                      ++num1;
                      int index = !this.newFile.Contains("RDATMP.docx") && !this.newFile.Contains("WBPTMP.docx") && !this.newFile.Contains("WBP_1TMP.docx") ? num1 + 2 : num1;
                      Row row2 = table.InsertRow(row1, index);
                      row2.ReplaceText("<kandydat_lp>", (string) (object) num1 + (object) ".", false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                      row2.ReplaceText("<Kandydtat_L" + (object) num3 + "_lp>", (string) (object) num1 + (object) ".", false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                      row2.ReplaceText("<Kandydtat_L" + (object) num3 + "_nazwisko_imie>", xmlNode4.Attributes["nazwisko"].InnerText.ToUpper() + " " + xmlNode4.Attributes["imie1"].InnerText + " " + xmlNode4.Attributes["imie2"].InnerText, 0 != 0, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                      row2.ReplaceText("<kandydat_nazwisko_imie>", xmlNode4.Attributes["nazwisko"].InnerText.ToUpper() + " " + xmlNode4.Attributes["imie1"].InnerText + " " + xmlNode4.Attributes["imie2"].InnerText, 0 != 0, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                      if (xmlNode4.Attributes["kdt_plec"].InnerText == "K")
                      {
                        row2.ReplaceText("<kandydat_zgloszony_przez>", "zgłoszona przez " + xmlNode3.Attributes["komitet_skrot"].InnerText + ", Lista nr. " + xmlNode3.Attributes["nrlisty"].InnerText, false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                        row2.ReplaceText("<Kandydtat_L" + (object) num3 + "_zgloszony_przez>", "zgłoszona przez " + xmlNode3.Attributes["komitet_nazwa"].InnerText, false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                      }
                      else
                      {
                        row2.ReplaceText("<kandydat_zgloszony_przez>", "zgłoszony przez " + xmlNode3.Attributes["komitet_skrot"].InnerText + ", Lista nr. " + xmlNode3.Attributes["nrlisty"].InnerText, false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                        row2.ReplaceText("<Kandydtat_L" + (object) num3 + "_zgloszony_przez>", "zgłoszony przez " + xmlNode3.Attributes["komitet_nazwa"].InnerText, false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                      }
                    }
                  }
                }
              }
            }
          }
          docX.SaveAs(this.newFile);
        }
      }
      else
        this.newFile = docDefinitionPath;
      using (DocX docX = DocX.Load(this.newFile))
      {
        this.newFile = docDefinitionPath.Replace(".docx", "TMP.docx");
        for (int index1 = 1; index1 <= 3; ++index1)
        {
          XmlNode xmlNode2 = value.SelectSingleNode("/save/header");
          if (index1 == 2)
            xmlNode2 = ((value.SelectSingleNode("/save/form") ?? value.SelectSingleNode("/save/step3")) ?? value.SelectSingleNode("/save/step2")) ?? value.SelectSingleNode("/save/step1");
          if (index1 == 3)
            xmlNode2 = value.SelectSingleNode("/save/komisja_sklad");
          if (xmlNode2 != null)
          {
            foreach (XmlNode xmlNode3 in xmlNode2)
            {
              if (index1 == 3)
              {
                foreach (Novacode.Table table in docX.Tables)
                {
                  int index2 = 0;
                  foreach (Row row1 in table.Rows)
                  {
                    ++index2;
                    if (row1.Cells.Count > 1 && (row1.FindAll("<osoba_lp>").Count > 0 && xmlNode3.Attributes["obecny"].InnerText == "True"))
                    {
                      Row row2 = row1;
                      table.InsertRow(row2, index2);
                      row1.ReplaceText("<osoba_lp>", (string) (object) index2 + (object) ".", false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                      row1.ReplaceText("<osoba_ImieNazwisko>", HttpUtility.UrlDecode(xmlNode3.Attributes["nazwisko"].InnerText) + " " + HttpUtility.UrlDecode(xmlNode3.Attributes["imie"].InnerText) + " " + HttpUtility.UrlDecode(xmlNode3.Attributes["imie2"].InnerText) + " - " + xmlNode3.Attributes["funkcja"].InnerText, 0 != 0, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                    }
                  }
                }
              }
              else
              {
                int count1 = docX.FindAll("<" + xmlNode3.Name + "*").Count;
                int length1 = xmlNode3.InnerText.Length;
                int num2 = 2;
                if (count1 >= 1)
                {
                  ++count1;
                  if (length1 > 0)
                    docX.ReplaceText("<" + xmlNode3.Name + ">", xmlNode3.InnerText.Substring(length1 - 1, 1), false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                  else
                    docX.ReplaceText("<" + xmlNode3.Name + ">", "", false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                  for (int startIndex = length1 - 2; startIndex >= 0; --startIndex)
                  {
                    if (length1 > 0)
                      docX.ReplaceText("<" + (object) xmlNode3.Name + "*" + (string) (object) num2 + ">", xmlNode3.InnerText.Substring(startIndex, 1), 0 != 0, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                    else
                      docX.ReplaceText("<" + (object) xmlNode3.Name + "*" + (string) (object) num2 + ">", "", 0 != 0, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                    ++num2;
                  }
                  if (length1 < count1)
                  {
                    for (int index2 = length1; index2 <= count1; ++index2)
                    {
                      if (length1 > 0)
                        docX.ReplaceText("<" + (object) xmlNode3.Name + "*" + (string) (object) index2 + ">", "*", 0 != 0, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                      else
                        docX.ReplaceText("<" + (object) xmlNode3.Name + "*" + (string) (object) index2 + ">", "", 0 != 0, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                    }
                  }
                }
                if (count1 == 0)
                {
                  if (length1 > 0)
                    docX.ReplaceText("<" + xmlNode3.Name + ">", xmlNode3.InnerText, false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                  else
                    docX.ReplaceText("<" + xmlNode3.Name + ">", "", false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                }
                if (xmlNode3.Attributes.Count > 0)
                {
                  if (xmlNode3.Name.Substring(0, 11) == "Kandydtat_L")
                  {
                    flag1 = true;
                    if (xmlNode3.Attributes[0].Name == "id_kand")
                    {
                      foreach (Novacode.Table table in docX.Tables)
                      {
                        int index2 = 0;
                        foreach (Row row1 in table.Rows)
                        {
                          ++index2;
                          string str4 = xmlNode3.Name.Substring(0, 13);
                          if (str4.Substring(12, 1) == "_")
                            str4 = xmlNode3.Name.Substring(0, 12);
                          if (row1.FindAll("<" + str4 + "_lp>").Count > 0 | row1.FindAll("<" + str4.Replace("Kandydtat", "Lista")).Count > 0)
                          {
                            if (row1.FindAll("<" + str4 + "_lp>").Count > 0)
                            {
                              Row row2 = row1;
                              table.InsertRow(row2, index2);
                            }
                            foreach (XmlNode xmlNode4 in candidates.SelectSingleNode("/listy"))
                            {
                              foreach (XmlNode xmlNode6 in xmlNode4)
                              {
                                if (xmlNode6.Attributes["id_kand"].InnerText == xmlNode3.Attributes["id_kand"].InnerText)
                                {
                                  row1.ReplaceText("<" + str4.Replace("Kandydtat", "Lista") + ">", xmlNode4.Attributes["nrlisty"].InnerText, false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                                  row1.ReplaceText("<" + str4.Replace("Kandydtat", "Lista") + "_skrot>", xmlNode4.Attributes["komitet_nazwa"].InnerText, false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                                  row1.ReplaceText("<" + str4 + "_lp>", (string) (object) (index2 - 3) + (object) ".", false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                                  row1.ReplaceText("<" + str4 + "_nazwisko_imie>", xmlNode6.Attributes["nazwisko"].InnerText.ToUpper() + " " + xmlNode6.Attributes["imie1"].InnerText + " " + xmlNode6.Attributes["imie2"].InnerText, 0 != 0, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                                  if (xmlNode6.Attributes["kdt_plec"].InnerText == "K")
                                    row1.ReplaceText("<" + str4 + "_zgloszony_przez>", "zgłoszona przez " + xmlNode4.Attributes["komitet_skrot"].InnerText, false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                                  else
                                    row1.ReplaceText("<" + str4 + "_zgloszony_przez>", "zgłoszony przez " + xmlNode4.Attributes["komitet_skrot"].InnerText, false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                                  int count2 = row1.FindAll("<" + str4 + "_g").Count;
                                  int length2 = xmlNode3.InnerText.Length;
                                  int num3 = 2;
                                  if (count2 > 1)
                                  {
                                    if (length2 > 0)
                                      row1.ReplaceText("<" + str4 + "_g>", xmlNode3.InnerText.Substring(length2 - 1, 1), false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                                    else
                                      row1.ReplaceText("<" + str4 + "_g>", "", false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                                    for (int startIndex = length2 - 2; startIndex >= 0; --startIndex)
                                    {
                                      if (length2 > 0)
                                        row1.ReplaceText("<" + (object) str4 + "_g*" + (string) (object) num3 + ">", xmlNode3.InnerText.Substring(startIndex, 1), 0 != 0, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                                      else
                                        row1.ReplaceText("<" + (object) str4 + "_g*" + (string) (object) num3 + ">", "", 0 != 0, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                                      ++num3;
                                    }
                                    if (length2 < count2)
                                    {
                                      for (int index3 = length2; index3 <= count2; ++index3)
                                      {
                                        if (length2 > 0)
                                          row1.ReplaceText("<" + (object) str4 + "_g*" + (string) (object) index3 + ">", "*", 0 != 0, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                                        else
                                          row1.ReplaceText("<" + (object) str4 + "_g*" + (string) (object) index3 + ">", "", 0 != 0, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                                      }
                                    }
                                  }
                                  if (count2 == 1)
                                    row1.ReplaceText("<" + str4 + "_g>", xmlNode3.InnerText, false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                                }
                              }
                            }
                          }
                        }
                      }
                    }
                  }
                  else if (xmlNode3.Attributes[0].Name == "id_kand")
                  {
                    foreach (Novacode.Table table in docX.Tables)
                    {
                      int index2 = 0;
                      foreach (Row row1 in table.Rows)
                      {
                        ++index2;
                        if (row1.Cells.Count > 1 && (row1.FindAll("<kandydat_lp>").Count > 0 || row1.FindAll("<kandydat_nazwisko_imie>").Count > 0 || row1.FindAll("<kandydat_zgloszony_przez>").Count > 0 || row1.FindAll("<kandydat_g").Count > 0))
                        {
                          if (!flag2)
                          {
                            Row row2 = row1;
                            table.InsertRow(row2, index2);
                          }
                          foreach (XmlNode xmlNode4 in candidates.SelectSingleNode("/listy"))
                          {
                            foreach (XmlNode xmlNode6 in xmlNode4)
                            {
                              if (xmlNode6.Attributes["id_kand"].InnerText == xmlNode3.Attributes["id_kand"].InnerText)
                              {
                                ++num1;
                                row1.ReplaceText("<kandydat_lp>", (string) (object) num1 + (object) ".", false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                                row1.ReplaceText("<kandydat_nazwisko_imie>", xmlNode6.Attributes["nazwisko"].InnerText.ToUpper() + " " + xmlNode6.Attributes["imie1"].InnerText + " " + xmlNode6.Attributes["imie2"].InnerText, 0 != 0, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                                if (xmlNode6.Attributes["kdt_plec"].InnerText == "K")
                                  row1.ReplaceText("<kandydat_zgloszony_przez>", "zgłoszona przez " + xmlNode4.Attributes["oznaczenie_listy"].InnerText + ", Lista nr. " + xmlNode4.Attributes["nrlisty"].InnerText, false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                                else
                                  row1.ReplaceText("<kandydat_zgloszony_przez>", "zgłoszony przez " + xmlNode4.Attributes["oznaczenie_listy"].InnerText + ", Lista nr. " + xmlNode4.Attributes["nrlisty"].InnerText, false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                                string str4 = "";
                                List<int> all = row1.FindAll("<kandydat_g");
                                if (all.Count > 0)
                                  str4 = "<kandydat_g";
                                int count2 = all.Count;
                                int length2 = xmlNode3.InnerText.Length;
                                int num3 = 2;
                                if (count2 > 1)
                                {
                                  if (length2 > 0)
                                    row1.ReplaceText(str4 + ">", xmlNode3.InnerText.Substring(length2 - 1, 1), false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                                  else
                                    row1.ReplaceText(str4 + ">", "", false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                                  for (int startIndex = length2 - 2; startIndex >= 0; --startIndex)
                                  {
                                    if (length2 > 0)
                                      row1.ReplaceText(string.Concat(new object[4]
                                      {
                                        (object) str4,
                                        (object) "*",
                                        (object) num3,
                                        (object) ">"
                                      }), xmlNode3.InnerText.Substring(startIndex, 1), 0 != 0, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                                    else
                                      row1.ReplaceText(string.Concat(new object[4]
                                      {
                                        (object) str4,
                                        (object) "*",
                                        (object) num3,
                                        (object) ">"
                                      }), "", 0 != 0, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                                    ++num3;
                                  }
                                  if (length2 < count2)
                                  {
                                    for (int index3 = length2; index3 <= count2; ++index3)
                                    {
                                      if (length2 > 0)
                                        row1.ReplaceText(string.Concat(new object[4]
                                        {
                                          (object) str4,
                                          (object) "*",
                                          (object) index3,
                                          (object) ">"
                                        }), "*", 0 != 0, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                                      else
                                        row1.ReplaceText(string.Concat(new object[4]
                                        {
                                          (object) str4,
                                          (object) "*",
                                          (object) index3,
                                          (object) ">"
                                        }), "", 0 != 0, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
                                    }
                                  }
                                }
                                if (count2 == 1)
                                  row1.ReplaceText(str4 + ">", xmlNode3.InnerText, false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
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
        }
        foreach (Novacode.Table table in docX.Tables)
        {
          int num2 = 0;
          foreach (Row row in table.Rows)
          {
            if (row.Cells.Count > 1 && (row.FindAll("<osoba_lp>").Count > 0 || row.FindAll("<Kandydtat_L").Count > 0 || row.FindAll("<kandydat_").Count > 0))
              row.Remove();
            ++num2;
          }
        }
        docX.ReplaceText("<field_3_14>", "", false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
        docX.ReplaceText("<field_3_15>", "", false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
        docX.ReplaceText("<field_3_16>", "", false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
        docX.ReplaceText("<field_3_17>", "", false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
        docX.ReplaceText("<field_3_18>", "", false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
        docX.ReplaceText("<field_3_19>", "", false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
        docX.ReplaceText("<field_3_20>", "", false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
        if (controlSum != "")
        {
          this.GenerateBarcode(controlSum);
          for (int index = controlSum.Length / 4 - 1; index > 0; --index)
            controlSum = controlSum.Insert(index * 4, "-");
          docX.ReplaceText("<control_sum>", controlSum, false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
        }
        else
          docX.ReplaceText("<control_sum>", "", false, RegexOptions.None, (Novacode.Formatting) null, (Novacode.Formatting) null, MatchFormattingOptions.SubsetMatch);
        docX.SaveAs(this.newFile);
      }
      if (flag1)
      {
        for (int index = 1; index < 20; ++index)
          this.DocxDeleteTables(this.newFile);
      }
      if (this.imageBarCode != null && this.newFile != "")
        this.DocxInsertBarcode(this.newFile);
      if (this.imageBarCode != null && this.newFileErr != "")
        this.DocxInsertBarcode(this.newFileErr);
      if (!printToPDF)
      {
        if (this.printDialog1.ShowDialog() != DialogResult.OK)
          return;
        if (this.newFileErr != "")
          new Thread(new ThreadStart(this.PrintWarnings)).Start();
        new Thread(new ThreadStart(this.Print)).Start();
      }
      else
      {
        if (str1 != "" && this.newFileErr != "")
        {
          SaveFileDialog saveFileDialog = new SaveFileDialog();
          saveFileDialog.Title = "Zapisz raport ostrzeżeń jako PDF...";
          saveFileDialog.Filter = "Pliki PDF | *.pdf";
          saveFileDialog.DefaultExt = "pdf";
          saveFileDialog.AddExtension = true;
          saveFileDialog.FileName = Path.GetFileName(str1).Replace("TMP.docx", ".pdf");
          if (saveFileDialog.ShowDialog() == DialogResult.OK)
          {
            PDFEncoderParams pdfEncoderParams = new PDFEncoderParams();
            DocumentManager documentManager = new DocumentManager();
            documentManager.LoadDocument(this.newFileErr, "");
            if (saveFileDialog.FileName.Contains("."))
            {
              saveFileDialog.FileName.Remove(saveFileDialog.FileName.IndexOf("."), saveFileDialog.FileName.Length - 1 - saveFileDialog.FileName.IndexOf("."));
              saveFileDialog.FileName = saveFileDialog.FileName + ".pdf";
            }
            else
              saveFileDialog.FileName = saveFileDialog.FileName + ".pdf";
            documentManager.ConvertDocument((object) documentManager.Documents[0], (object) saveFileDialog.FileName, "pdf", (ConverterParams) null, (EncoderParams) pdfEncoderParams, "");
            documentManager.CloseAllDocuments();
          }
        }
        SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        saveFileDialog1.Title = "Zapisz protokół jako PDF...";
        saveFileDialog1.Filter = "Pliki PDF | *.pdf";
        saveFileDialog1.DefaultExt = "pdf";
        saveFileDialog1.AddExtension = true;
        saveFileDialog1.FileName = Path.GetFileName(this.newFile).Replace("TMP.docx", ".pdf");
        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
        {
          PDFEncoderParams pdfEncoderParams = new PDFEncoderParams();
          DocumentManager documentManager = new DocumentManager();
          documentManager.LoadDocument(this.newFile, "");
          if (saveFileDialog1.FileName.Contains("."))
          {
            saveFileDialog1.FileName.Remove(saveFileDialog1.FileName.IndexOf("."), saveFileDialog1.FileName.Length - 1 - saveFileDialog1.FileName.IndexOf("."));
            saveFileDialog1.FileName = saveFileDialog1.FileName + ".pdf";
          }
          else
            saveFileDialog1.FileName = saveFileDialog1.FileName + ".pdf";
          documentManager.ConvertDocument((object) documentManager.Documents[0], (object) saveFileDialog1.FileName, "pdf", (ConverterParams) null, (EncoderParams) pdfEncoderParams, "");
          documentManager.CloseAllDocuments();
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
      Stream stream = (Stream) new FileStream(this.newFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
      documentPrinter.Print(stream, "");
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
      Stream stream = (Stream) new FileStream(this.newFileErr, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
      documentPrinter.Print(stream, "");
      Application.ExitThread();
    }

    private void GenerateBarcode(string controlSum)
    {
      Code128BarcodeDraw code128WithChecksum = BarcodeDrawFactory.Code128WithChecksum;
      PictureBox pictureBox = new PictureBox();
      pictureBox.Image = ((BarcodeDraw) code128WithChecksum).Draw(controlSum, 40);
      MemoryStream memoryStream = new MemoryStream();
      pictureBox.Image.Save((Stream) memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
      this.imageBarCode = Convert.ToBase64String(memoryStream.ToArray());
    }

    private void DocxInsertBarcode(string docxPath)
    {
      using (WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(docxPath, true))
      {
        foreach (FooterPart footerPart in wordprocessingDocument.MainDocumentPart.FooterParts)
        {
          if (Enumerable.Count<ImagePart>(footerPart.ImageParts) > 0)
          {
            ImagePart imagePart = Enumerable.FirstOrDefault<ImagePart>(footerPart.ImageParts);
            Stream binaryDataStream = this.GetBinaryDataStream(this.imageBarCode);
            imagePart.FeedData(binaryDataStream);
            binaryDataStream.Close();
            ((OpenXmlPartRootElement) wordprocessingDocument.MainDocumentPart.Document).Save();
          }
        }
      }
    }

    public static string EncodeTo64(string toEncode)
    {
      return Convert.ToBase64String(Encoding.ASCII.GetBytes(toEncode));
    }

    private void DocxDeleteTables(string docxPath)
    {
      using (WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(docxPath, true))
      {
        foreach (DocumentFormat.OpenXml.Wordprocessing.Table table in wordprocessingDocument.MainDocumentPart.Document.Body.Elements<DocumentFormat.OpenXml.Wordprocessing.Table>())
        {
          if (table.InnerText.Contains("<Lista_"))
            table.Remove();
        }
        ((OpenXmlPartRootElement) wordprocessingDocument.MainDocumentPart.Document).Save();
      }
    }

    private bool isOneCandidate(XmlNode candidates)
    {
      XmlNode xmlNode = candidates.SelectSingleNode("/listy");
      return xmlNode.ChildNodes.Count <= 1 && xmlNode.ChildNodes.Count != 0 && xmlNode.FirstChild.ChildNodes.Count <= 1;
    }
  }
}
