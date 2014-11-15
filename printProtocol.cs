// Decompiled with JetBrains decompiler
// Type: Kalkulator1.printProtocol
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace Kalkulator1
{
  public class printProtocol
  {
    private string header;
    private string footer;
    private string body;
    private string additionaContent;
    private string raport;
    private codeBar code;
    private bool isSigned;
    private string path;
    private Dictionary<string, string> errors;

    public printProtocol()
    {
      this.path = Path.GetTempPath() + "KBW";
      this.header = "<!DOCTYPE HTML><html><head><meta charset='UTF-8'><title></title><link rel='stylesheet' type='text/css' href='" + Path.GetDirectoryName(Application.StartupPath) + "\\tmp\\printTmp\\css\\styl.css'>";
      printProtocol printProtocol1 = this;
      string str1 = printProtocol1.header + "<script type=\"text/javascript\" src=\"" + Path.GetDirectoryName(Application.StartupPath) + "\\tmp\\printTmp\\jquery-1.11.1.min.js\"></script>";
      printProtocol1.header = str1;
      printProtocol printProtocol2 = this;
      string str2 = printProtocol2.header + "</head><body>";
      printProtocol2.header = str2;
      this.footer = "</body></html>";
      this.body = "";
      this.additionaContent = "";
      this.code = new codeBar();
      this.isSigned = false;
      this.setErrorsTab();
    }

    public printProtocol(string controlSum, PictureBox p)
    {
      this.path = Path.GetTempPath() + "KBW";
      this.header = "<!DOCTYPE HTML><html><head><meta charset='UTF-8'><title></title><link rel='stylesheet' type='text/css' href='" + Path.GetDirectoryName(Application.StartupPath) + "\\tmp\\printTmp\\css\\styl.css'>";
      printProtocol printProtocol1 = this;
      string str1 = printProtocol1.header + "<script type=\"text/javascript\" src=\"" + Path.GetDirectoryName(Application.StartupPath) + "\\tmp\\printTmp\\jquery-1.11.1.min.js\"></script>";
      printProtocol1.header = str1;
      printProtocol printProtocol2 = this;
      string str2 = printProtocol2.header + "</head><body>";
      printProtocol2.header = str2;
      this.body = "";
      this.additionaContent = "";
      this.code = new codeBar();
      this.code.generateCode(controlSum);
      this.code.draw(p);
      this.isSigned = true;
      this.setErrorsTab();
    }

    public string getProtocol(XmlDocument design, XmlDocument value, XmlDocument candidates)
    {
      bool flag1 = false;
      string str1 = "";
      string xpath = "";
      string str2 = "";
      XmlNode xmlNode1 = value.SelectSingleNode("/save/step");
      if (xmlNode1 != null && xmlNode1.InnerText == "0")
        this.isSigned = true;
      bool flag2 = false;
      string str3 = "<table>";
      string str4 = "</table>";
      try
      {
        foreach (XmlNode xmlNode2 in design.SelectSingleNode("/protokol_info"))
        {
          if (xmlNode2.Name == "fields")
          {
            XmlNode namedItem1 = xmlNode2.Attributes.GetNamedItem("type");
            if (namedItem1 != null && namedItem1.Value == "header")
            {
              string str5 = "";
              foreach (XmlNode xmlNode3 in xmlNode2)
              {
                bool flag3 = false;
                if (xmlNode3.Name == "box" && xmlNode3.Attributes.GetNamedItem("design") != null && xmlNode3.Attributes.GetNamedItem("design").Value == "border")
                  flag3 = true;
                if (flag3)
                  str5 = str5 + str3;
                foreach (XmlNode xmlNode4 in xmlNode3)
                {
                  if (flag3)
                  {
                    if (xmlNode4.Name == "title")
                    {
                      XmlNode namedItem2 = xmlNode4.Attributes.GetNamedItem("colspan");
                      str5 = namedItem2 == null ? str5 + "<tr><td>" : str5 + "<tr><td colspan=\"" + namedItem2.Value + "\" class=\"center\">";
                      XmlNode namedItem3 = xmlNode4.Attributes.GetNamedItem("bold");
                      str5 = namedItem3 == null || !(namedItem3.Value == "true") ? str5 + xmlNode4.InnerXml : str5 + "<b>" + xmlNode4.InnerXml + "</b>";
                      str5 = str5 + "</td></tr>";
                    }
                    if (xmlNode4.Name == "row")
                    {
                      str5 = str5 + "<tr>";
                      for (int index1 = 0; index1 < xmlNode4.ChildNodes.Count; ++index1)
                      {
                        XmlNode namedItem2 = xmlNode4.ChildNodes[index1].Attributes.GetNamedItem("name");
                        XmlNode namedItem3 = xmlNode4.ChildNodes[index1].Attributes.GetNamedItem("colspan");
                        str5 = namedItem3 == null ? str5 + "<td>" : str5 + "<td colspan=\"" + namedItem3.Value + "\">";
                        if (namedItem2 != null)
                          str5 = str5 + namedItem2.Value;
                        str5 = str5 + "</td>";
                        XmlNode namedItem4 = xmlNode4.ChildNodes[index1].Attributes.GetNamedItem("colspan_save");
                        XmlNode namedItem5 = xmlNode4.ChildNodes[index1].Attributes.GetNamedItem("char");
                        XmlNode namedItem6 = xmlNode4.ChildNodes[index1].Attributes.GetNamedItem("saveCheck");
                        XmlNode namedItem7 = xmlNode4.ChildNodes[index1].Attributes.GetNamedItem("save_as");
                        if (namedItem7 != null)
                        {
                          string str6 = "";
                          XmlNode xmlNode5 = value.SelectSingleNode("/save/hardWarning");
                          XmlNode xmlNode6 = value.SelectSingleNode("/save/softError");
                          XmlNode xmlNode7 = value.SelectSingleNode("/save/header/" + namedItem7.Value);
                          if (xmlNode7 != null)
                          {
                            if (namedItem6 != null && namedItem6.Value.ToLower() == "true" && (xmlNode5 != null && xmlNode5.ChildNodes.Count != 0 || xmlNode6 != null && xmlNode6.ChildNodes.Count != 0))
                              str6 = " class=\"warningCheck\" ";
                            if (namedItem5 != null && namedItem5.Value == "true" && (namedItem4 != null && namedItem4.Value != "") && Convert.ToInt32(namedItem4.Value) > 0 && xmlNode7.InnerText.Length <= Convert.ToInt32(namedItem4.Value))
                            {
                              char[] chArray = xmlNode7.InnerText.ToCharArray();
                              string str7 = "";
                              for (int index2 = 0; index2 < chArray.Length; ++index2)
                                str7 = str7 + "<td colspan=\"1\" " + str6 + ">" + chArray[index2].ToString() + "</td>";
                              string str8 = " ";
                              XmlNode namedItem8 = xmlNode4.ChildNodes[index1].Attributes.GetNamedItem("fill");
                              if (namedItem8 != null && namedItem8.Value != "")
                                str8 = namedItem8.Value;
                              int num = Convert.ToInt32(namedItem4.Value) - chArray.Length;
                              for (int index2 = 0; index2 < num; ++index2)
                                str7 = "<td colspan=\"1\">" + str8 + "</td>" + str7;
                              str5 = str5 + str7;
                            }
                            else
                            {
                              if (namedItem4 != null)
                                str5 = str5 + "<td colspan=\"" + namedItem4.Value + "\"" + str6 + ">";
                              else
                                str5 = str5 + "<td" + str6 + ">";
                              str5 = str5 + xmlNode7.InnerText;
                            }
                          }
                          else if (namedItem4 != null)
                            str5 = str5 + "<td colspan=\"" + namedItem4.Value + "\"" + str6 + ">";
                          else
                            str5 = str5 + "<td" + str6 + ">";
                        }
                        else
                          str5 = namedItem4 == null ? str5 + "<td>" : str5 + "<td colspan=\"" + namedItem4.Value + "\">";
                        str5 = str5 + "</td>";
                      }
                      str5 = str5 + "</tr>";
                    }
                  }
                  else
                  {
                    if (xmlNode4.Name == "title")
                    {
                      XmlNode namedItem2 = xmlNode4.Attributes.GetNamedItem("class");
                      string str6 = "";
                      if (namedItem2 != null && namedItem2.Value != "")
                        str6 = " class=\"" + namedItem2.Value + "\"";
                      XmlNode namedItem3 = xmlNode4.Attributes.GetNamedItem("bold");
                      if (namedItem3 != null && namedItem3.Value == "true")
                        str5 = str5 + "<h1" + str6 + "><b>" + xmlNode4.InnerXml + "</b></h1>";
                      else
                        str5 = str5 + "<h1>" + xmlNode4.InnerXml + "</h1>";
                    }
                    if (xmlNode4.Name == "row")
                    {
                      str5 = str5 + "<p>";
                      for (int index = 0; index < xmlNode4.ChildNodes.Count; ++index)
                      {
                        XmlNode namedItem2 = xmlNode4.ChildNodes[index].Attributes.GetNamedItem("name");
                        if (namedItem2 != null)
                          str5 = str5 + namedItem2.Value + " ";
                        XmlNode namedItem3 = xmlNode4.ChildNodes[index].Attributes.GetNamedItem("save_as");
                        if (namedItem3 != null)
                        {
                          XmlNode xmlNode5 = value.SelectSingleNode("/save/header/" + namedItem3.Value);
                          if (xmlNode5 != null)
                            str5 = str5 + xmlNode5.InnerText + " ";
                        }
                      }
                      str5 = str5 + "</p>";
                    }
                  }
                }
                if (!flag2)
                {
                  printProtocol printProtocol = this;
                  string str6 = printProtocol.raport + str5;
                  printProtocol.raport = str6;
                  flag2 = true;
                }
                if (flag3)
                  str5 = str5 + str4;
              }
              printProtocol printProtocol1 = this;
              string str9 = printProtocol1.body + str5;
              printProtocol1.body = str9;
            }
            if (namedItem1 != null && namedItem1.Value == "calculator")
            {
              string str5 = "";
              if (xmlNode1 != null)
              {
                string innerText = xmlNode1.InnerText;
                if (innerText == "0" || innerText == "4")
                  xpath = "/save/form";
                if (innerText == "1" || innerText == "2" || innerText == "3")
                  xpath = "/save/step1";
              }
              bool flag3 = false;
              bool flag4 = false;
              foreach (XmlNode xmlNode3 in xmlNode2)
              {
                XmlNode namedItem2 = xmlNode3.Attributes.GetNamedItem("design");
                flag3 = flag4;
                flag4 = namedItem2 != null && namedItem2.Value == "border";
                if (!flag3 && flag4)
                  str5 = str5 + str3;
                if (flag3 && !flag4)
                  str5 = str5 + str4;
                if (xmlNode3.Name == "title")
                {
                  XmlNode namedItem3 = xmlNode3.Attributes.GetNamedItem("bold");
                  if (flag4)
                  {
                    XmlNode namedItem4 = xmlNode3.Attributes.GetNamedItem("colspan");
                    str5 = namedItem4 == null ? str5 + "<tr><td>" : str5 + "<tr><td colspan=\"" + namedItem4.Value + "\" class=\"center\">";
                    str5 = namedItem3 == null || !(namedItem3.Value == "true") ? str5 + xmlNode3.InnerXml : str5 + "<b>" + xmlNode3.InnerXml + "</b>";
                    str5 = str5 + "</td><tr>";
                  }
                  else
                    str5 = namedItem3 == null || !(namedItem3.Value == "true") ? str5 + "<h1>" + xmlNode3.InnerXml + "</h1>" : str5 + "<h1><b>" + xmlNode3.InnerXml + "</b></h1>";
                }
                if (xmlNode3.Name == "description")
                {
                  XmlNode namedItem3 = xmlNode3.Attributes.GetNamedItem("bold");
                  if (flag4)
                  {
                    XmlNode namedItem4 = xmlNode3.Attributes.GetNamedItem("colspan");
                    str5 = namedItem4 == null ? str5 + "<tr><td>" : str5 + "<tr><td colspan=\"" + namedItem4.Value + "\">";
                    str5 = namedItem3 == null || !(namedItem3.Value == "true") ? str5 + xmlNode3.InnerXml : str5 + "<b>" + xmlNode3.InnerXml + "</b>";
                    str5 = str5 + "</td><tr>";
                  }
                  else
                    str5 = namedItem3 == null || !(namedItem3.Value == "true") ? str5 + "<p>" + xmlNode3.InnerXml + "</p>" : str5 + "<p><b>" + xmlNode3.InnerXml + "</b></p>";
                }
                if (xmlNode3.Name == "note")
                {
                  XmlNode namedItem3 = xmlNode3.Attributes.GetNamedItem("bold");
                  if (flag4)
                  {
                    XmlNode namedItem4 = xmlNode3.Attributes.GetNamedItem("colspan");
                    str5 = namedItem4 == null ? str5 + "<tr><td>" : str5 + "<tr><td colspan=\"" + namedItem4.Value + "\">";
                    str5 = namedItem3 == null || !(namedItem3.Value == "true") ? str5 + xmlNode3.InnerXml : str5 + "<b>Uwaga!</b><b>" + xmlNode3.InnerXml + "</b>";
                    str5 = str5 + "</td><tr>";
                  }
                  else
                    str5 = namedItem3 == null || !(namedItem3.Value == "true") ? str5 + "<table class=\"noborder\"><tr><td colspan=\"2\"><b>Uwaga!</b></td><td colspan=\"22\">" + xmlNode3.InnerXml + "</td></tr></table>" : str5 + "<table class=\"noborder\" ><tr><td colspan=\"2\"><b>Uwaga!</b></td><td colspan=\"22\"><b>" + xmlNode3.InnerXml + "</b></td></tr></table>";
                }
                if (xmlNode3.Name == "field")
                {
                  string str6 = "";
                  XmlNode namedItem3 = xmlNode3.Attributes.GetNamedItem("lp");
                  string str7;
                  if (flag4)
                  {
                    string str8 = str6 + "<tr>";
                    XmlNode namedItem4 = xmlNode3.Attributes.GetNamedItem("colspan");
                    XmlNode namedItem5 = xmlNode3.Attributes.GetNamedItem("colspan_save");
                    XmlNode namedItem6 = xmlNode3.Attributes.GetNamedItem("colspan_nr");
                    if (namedItem3 != null && namedItem3.Value != "" && (namedItem6 != null && namedItem6.Value != "") && Convert.ToInt32(namedItem6.Value) > 0)
                      str8 = str8 + "<td colspan =\"" + namedItem6.Value + "\" class=\"lp\">" + namedItem3.Value + ". </td>";
                    for (int index1 = 0; index1 < xmlNode3.ChildNodes.Count; ++index1)
                    {
                      string str9 = xmlNode3.ChildNodes[index1].InnerXml.Replace(Environment.NewLine, "<br>");
                      if (xmlNode3.ChildNodes[index1].Name == "name" && namedItem4 != null && namedItem4.Value != "" && Convert.ToInt32(namedItem4.Value) > 0)
                        str8 = str8 + "<td colspan =\"" + namedItem4.Value + "\">" + str9 + "</td>";
                      if (xmlNode3.ChildNodes[index1].Name == "save_as")
                      {
                        XmlNode namedItem7 = xmlNode3.ChildNodes[index1].Attributes.GetNamedItem("char");
                        XmlNode xmlNode4 = value.SelectSingleNode(xpath + "/" + xmlNode3.ChildNodes[index1].InnerText);
                        if (xmlNode4 != null)
                        {
                          if (namedItem7 != null && namedItem7.Value == "true" && (namedItem5 != null && namedItem5.Value != "") && Convert.ToInt32(namedItem5.Value) > 0 && xmlNode4.InnerText.Length <= Convert.ToInt32(namedItem5.Value))
                          {
                            char[] chArray = xmlNode4.InnerText.ToCharArray();
                            string str10 = "";
                            for (int index2 = 0; index2 < chArray.Length; ++index2)
                              str10 = str10 + "<td colspan=\"1\">" + chArray[index2].ToString() + "</td>";
                            string str11 = " ";
                            XmlNode namedItem8 = xmlNode3.ChildNodes[index1].Attributes.GetNamedItem("fill");
                            if (namedItem8 != null && namedItem8.Value != "")
                              str11 = namedItem8.Value;
                            int num = Convert.ToInt32(namedItem5.Value) - chArray.Length;
                            for (int index2 = 0; index2 < num; ++index2)
                              str10 = "<td colspan=\"1\">" + str11 + "</td>" + str10;
                            str8 = str8 + str10;
                          }
                          else
                            str8 = (namedItem5 == null ? str8 + "<td>" : str8 + "<td colspan=\"" + namedItem5.Value + "\">") + xmlNode4.InnerText + "</td>";
                        }
                        else
                          str8 = namedItem5 == null ? str8 + "<td> </td>" : str8 + "<td colspan=\"" + namedItem5.Value + "\"> </td>";
                      }
                    }
                    str7 = str8 + "</tr>";
                  }
                  else
                  {
                    string str8 = str6 + "<p>";
                    if (namedItem3 != null && namedItem3.Value != "")
                      str8 = str8 + namedItem3.Value + " ";
                    for (int index = 0; index < xmlNode3.ChildNodes.Count; ++index)
                    {
                      if (xmlNode3.ChildNodes[index].Name == "name")
                        str8 = str8 + xmlNode3.ChildNodes[index].InnerXml + " ";
                      if (xmlNode3.ChildNodes[index].Name == "save_as")
                      {
                        XmlNode xmlNode4 = value.SelectSingleNode(xpath + "/" + xmlNode3.ChildNodes[index].InnerText);
                        if (xmlNode4 != null)
                          str8 = str8 + xmlNode4.Value;
                      }
                    }
                    str7 = str8 + "</p>";
                  }
                  str5 = str5 + str7;
                }
              }
              if (!flag3 && flag4)
                str5 = str5 + str3;
              if (flag3 && !flag4)
                str5 = str5 + str4;
              printProtocol printProtocol = this;
              string str12 = printProtocol.body + str5;
              printProtocol.body = str12;
            }
            if (namedItem1 != null && namedItem1.Value == "additional-calculator")
            {
              string str5 = "";
              if (xmlNode1 != null)
              {
                string innerText = xmlNode1.InnerText;
                if (innerText == "0" || innerText == "4")
                  xpath = "/save/form";
                if (innerText == "3")
                  xpath = "/save/step3";
              }
              bool flag3 = false;
              foreach (XmlNode xmlNode3 in xmlNode2)
              {
                XmlNode namedItem2 = xmlNode3.Attributes.GetNamedItem("design");
                bool flag4 = flag3;
                flag3 = namedItem2 != null && namedItem2.Value == "border";
                if (!flag4 && flag3)
                  str5 = str5 + str3;
                if (flag4 && !flag3)
                  str5 = str5 + str4;
                if (xmlNode3.Name == "title")
                {
                  if (xmlNode1 != null && xmlNode1.InnerText == "0")
                    str5 = "<div class=\"break\">&#8194</div>";
                  XmlNode namedItem3 = xmlNode3.Attributes.GetNamedItem("bold");
                  if (flag3)
                  {
                    XmlNode namedItem4 = xmlNode3.Attributes.GetNamedItem("colspan");
                    str5 = namedItem4 == null ? str5 + "<tr><td>" : str5 + "<tr><td colspan=\"" + namedItem4.Value + "\" class=\"center\">";
                    str5 = namedItem3 == null || !(namedItem3.Value == "true") ? str5 + xmlNode3.InnerXml : str5 + "<b>" + xmlNode3.InnerXml + "</b>";
                    str5 = str5 + "</td><tr>";
                  }
                  else
                    str5 = namedItem3 == null || !(namedItem3.Value == "true") ? str5 + "<h1>" + xmlNode3.InnerXml + "</h1>" : str5 + "<h1><b>" + xmlNode3.InnerXml + "</b></h1>";
                }
                if (xmlNode3.Name == "field")
                {
                  XmlNode namedItem3 = xmlNode3.Attributes.GetNamedItem("data");
                  if (namedItem3 != null && namedItem3.Value == "kandydaci")
                  {
                    str5 = xmlNode1 == null || !(xmlNode1.InnerText == "0") ? "<div id=\"breakTab2\"> &#8194 </div>" : "<div class=\"break\"> &#8194 </div>";
                    XmlNode xmlNode4 = candidates.SelectSingleNode("/listy");
                    if (xmlNode3.Name == "field")
                    {
                      XmlNode namedItem4 = xmlNode3.Attributes.GetNamedItem("lp");
                      foreach (XmlNode itemChild1 in xmlNode3)
                      {
                        if (itemChild1.Name == "name")
                        {
                          XmlNode namedItem5 = itemChild1.Attributes.GetNamedItem("bold");
                          if (flag3)
                          {
                            str5 = str5 + "<tr>";
                            XmlNode namedItem6 = itemChild1.Attributes.GetNamedItem("colspan_nr");
                            if (namedItem4 != null && namedItem4.Value != "" && (namedItem6 != null && namedItem6.Value != "") && Convert.ToInt32(namedItem6.Value) > 0)
                              str5 = str5 + "<td colspan =\"" + namedItem6.Value + "\">" + namedItem4.Value + ". </td>";
                            XmlNode namedItem7 = itemChild1.Attributes.GetNamedItem("colspan");
                            str5 = namedItem7 == null ? str5 + "<td>" : str5 + "<td colspan=\"" + namedItem7.Value + "\">";
                            str5 = namedItem5 == null || !(namedItem5.Value == "true") ? str5 + itemChild1.InnerXml : str5 + "<b>" + itemChild1.InnerXml + "</b>";
                            str5 = str5 + "</td><tr>";
                          }
                          else
                          {
                            str5 = str5 + "<p>";
                            if (namedItem4 != null && namedItem4.Value != "")
                              str5 = str5 + namedItem4.Value + ". ";
                            str5 = namedItem5 == null || !(namedItem5.Value == "true") ? str5 + itemChild1.InnerXml + "</p>" : str5 + "<b>" + itemChild1.InnerXml + "</b></p>";
                          }
                        }
                        if (itemChild1.Name == "note")
                        {
                          XmlNode namedItem5 = xmlNode3.Attributes.GetNamedItem("design");
                          flag3 = namedItem5 != null && namedItem5.Value == "border";
                          if (!flag4 && flag3)
                            str5 = str5 + str3;
                          if (flag4 && !flag3)
                            str5 = str5 + str4;
                          XmlNode namedItem6 = itemChild1.Attributes.GetNamedItem("bold");
                          if (flag3)
                          {
                            XmlNode namedItem7 = itemChild1.Attributes.GetNamedItem("colspan");
                            str5 = namedItem7 == null ? str5 + "<tr><td>" : str5 + "<tr><td colspan=\"" + namedItem7.Value + "\">";
                            str5 = namedItem6 == null || !(namedItem6.Value == "true") ? str5 + itemChild1.InnerXml : str5 + "<b>Uwaga!</b><b>" + itemChild1.InnerXml + "</b>";
                            str5 = str5 + "</td><tr>";
                          }
                          else
                            str5 = namedItem6 == null || !(namedItem6.Value == "true") ? str5 + "<p></p><table class=\"noborder\"><tr><td colspan=\"2\"><b>Uwaga!</b></td><td colspan=\"22\">" + itemChild1.InnerXml + "</td></tr></table>" : str5 + "<table class=\"noborder\" ><tr><td colspan=\"2\"><b>Uwaga!</b></td><td colspan=\"22\"><b>" + itemChild1.InnerXml + "</b></td></tr></table>";
                        }
                        string str6;
                        string str7;
                        string str8;
                        string str9;
                        string str10;
                        if (itemChild1.Name == "patternrows")
                        {
                          string str11 = "";
                          XmlNode namedItem5 = itemChild1.Attributes.GetNamedItem("design");
                          flag4 = flag3;
                          flag3 = namedItem5 != null && namedItem5.Value == "border";
                          if (!flag4 && flag3)
                            str11 = str11 + str3;
                          if (flag4 && !flag3)
                            str11 = str11 + str4;
                          XmlNode namedItem6 = itemChild1.Attributes.GetNamedItem("colspan");
                          XmlNode namedItem7 = itemChild1.Attributes.GetNamedItem("colspan_save");
                          XmlNode namedItem8 = itemChild1.Attributes.GetNamedItem("colspan_nr");
                          XmlNode namedItem9 = itemChild1.Attributes.GetNamedItem("lp");
                          List<Field> patternField = new List<Field>();
                          List<Field> list = this.readPatternCandidate(itemChild1, patternField);
                          int num1 = 1;
                          foreach (XmlNode lista in xmlNode4)
                          {
                            if (flag3)
                              str11 = num1 % 17 != 0 ? str11 + "<tr>" : str11 + "</table><div class=\"break\"> &#8194 </div><table><tr>";
                            for (int index1 = 0; index1 < list.Count; ++index1)
                            {
                              str6 = "";
                              str7 = "";
                              str8 = "";
                              str9 = "";
                              str10 = "";
                              string str12 = "";
                              string patternCandidate1 = this.getStatusBasePatternCandidate(list[index1], lista);
                              if (patternCandidate1 == "A" || patternCandidate1 == "R")
                              {
                                string patternCandidate2 = this.getImie1BasePatternCandidate(list[index1], lista);
                                string patternCandidate3 = this.getImie2BasePatternCandidate(list[index1], lista);
                                string patternCandidate4 = this.getNazwiskoBasePatternCandidate(list[index1], lista);
                                string patternCandidate5 = this.getKomitetBasePatternCandidate(list[index1], lista);
                                string patternCandidate6 = this.getName2BasePatternCandidate(list[index1], lista);
                                if (list[index1].getName1() != "")
                                  str12 = list[index1].getName1() + " ";
                                string str13 = "<b>" + str12 + patternCandidate4 + patternCandidate2 + patternCandidate3 + "</b><br>" + patternCandidate6 + patternCandidate5;
                                if (flag3)
                                {
                                  if (list[index1].getDataType() == "text" && list[index1].getSave() == "")
                                  {
                                    if (namedItem9 != null && namedItem9.Value == "auto")
                                    {
                                      if (namedItem8 != null && namedItem8.Value != "" && Convert.ToInt32(namedItem8.Value) > 0)
                                        str11 = str11 + "<td colspan=\"" + namedItem8.Value + "\" class=\"lp\">" + num1.ToString() + ". </td>";
                                      else
                                        str11 = str11 + "<td>" + num1.ToString() + "</td>";
                                    }
                                    if (namedItem6 != null && namedItem6.Value != "" && Convert.ToInt32(namedItem6.Value) > 0)
                                      str11 = str11 + "<td colspan=\"" + namedItem6.Value + "\">" + str13 + "</td>";
                                    else
                                      str11 = str11 + "<td>" + str13 + "</td>";
                                  }
                                  if (list[index1].getDataType() == "number" && list[index1].getSave() != "")
                                  {
                                    string str14 = list[index1].getSave().Replace("X", num1.ToString());
                                    XmlNode xmlNode5 = value.SelectSingleNode(xpath + "/" + str14);
                                    if (xmlNode5 != null)
                                    {
                                      if (patternCandidate1 != "R")
                                      {
                                        if (list[index1].isChar() && namedItem7 != null && (namedItem7.Value != "" && Convert.ToInt32(namedItem7.Value) > 0) && xmlNode5.InnerText.Length <= Convert.ToInt32(namedItem7.Value))
                                        {
                                          char[] chArray = xmlNode5.InnerText.ToCharArray();
                                          string str15 = "";
                                          for (int index2 = 0; index2 < chArray.Length; ++index2)
                                            str15 = str15 + "<td colspan=\"1\">" + chArray[index2].ToString() + "</td>";
                                          string str16 = " ";
                                          if (list[index1].getFill() != "")
                                            str16 = list[index1].getFill();
                                          int num2 = Convert.ToInt32(namedItem7.Value) - chArray.Length;
                                          for (int index2 = 0; index2 < num2; ++index2)
                                            str15 = "<td colspan=\"1\">" + str16 + "</td>" + str15;
                                          str11 = str11 + str15;
                                        }
                                        else
                                        {
                                          str11 = namedItem7 == null ? str11 + "<td>" : str11 + "<td colspan=\"" + namedItem7.Value + "\">";
                                          str11 = str11 + xmlNode5.InnerText;
                                          str11 = str11 + "</td>";
                                        }
                                      }
                                      else
                                      {
                                        str11 = namedItem7 == null ? str11 + "<td>" : str11 + "<td colspan=\"" + namedItem7.Value + "\">";
                                        str11 = str11 + "Skreślony";
                                        str11 = str11 + "</td>";
                                      }
                                    }
                                    else
                                    {
                                      str11 = namedItem7 == null ? str11 + "<td>" : str11 + "<td colspan=\"" + namedItem7.Value + "\">";
                                      str11 = !(patternCandidate1 == "R") ? str11 + " " : str11 + "Skreślony";
                                      str11 = str11 + "</td>";
                                    }
                                    str11 = str11 + "</tr>";
                                  }
                                }
                                else
                                {
                                  str11 = str11 + "<p>";
                                  if (list[index1].getDataType() == "text" && list[index1].getSave() == "")
                                  {
                                    if (namedItem9 != null && namedItem9.Value == "auto")
                                      str11 = str11 + num1.ToString() + " ";
                                    str11 = str11 + str13 + " ";
                                  }
                                  if (list[index1].getDataType() == "number" && list[index1].getSave() != "")
                                  {
                                    string str14 = list[index1].getSave().Replace("X", num1.ToString());
                                    XmlNode xmlNode5 = value.SelectSingleNode(xpath + "/" + str14);
                                    if (xmlNode5 != null)
                                      str11 = !(patternCandidate1 != "R") ? str11 + "Skreślony" : str11 + xmlNode5.Value;
                                  }
                                  str11 = str11 + "</p>";
                                }
                              }
                            }
                            ++num1;
                          }
                          if (flag3)
                            str11 = str11 + "</tr>";
                          str5 = str5 + str11;
                        }
                        if (itemChild1.Name == "patternrow")
                        {
                          string str11 = "";
                          XmlNode namedItem5 = itemChild1.Attributes.GetNamedItem("design");
                          flag4 = flag3;
                          flag3 = namedItem5 != null && namedItem5.Value == "border";
                          if (!flag4 && flag3)
                            str11 = str11 + str3;
                          if (flag4 && !flag3)
                            str11 = str11 + str4;
                          XmlNode namedItem6 = itemChild1.Attributes.GetNamedItem("colspan");
                          XmlNode namedItem7 = itemChild1.Attributes.GetNamedItem("colspan_save");
                          XmlNode namedItem8 = itemChild1.Attributes.GetNamedItem("colspan_nr");
                          XmlNode namedItem9 = itemChild1.Attributes.GetNamedItem("lp");
                          List<Field> patternField = new List<Field>();
                          List<Field> list = this.readPatternCandidate(itemChild1, patternField);
                          XmlNode namedItem10 = itemChild1.FirstChild.Attributes.GetNamedItem("design");
                          XmlNode namedItem11 = itemChild1.FirstChild.Attributes.GetNamedItem("class");
                          string str12 = "";
                          if (namedItem11 != null && namedItem11.Value != "" && namedItem10 == null)
                            str12 = " class=\"" + namedItem11.Value + "\"";
                          if (namedItem11 != null && namedItem11.Value != "" && namedItem10 != null && namedItem10.Value != "")
                            str12 = " class=\"" + namedItem11.Value + " " + namedItem10.Value + "\"";
                          if (namedItem11 == null && namedItem10 != null && namedItem10.Value != "")
                            str12 = " class=\"" + namedItem10.Value + "\"";
                          int num1 = 1;
                          if (flag3)
                            str11 = str11 + "<tr>";
                          for (int index1 = 0; index1 < list.Count; ++index1)
                          {
                            string name1 = list[index1].getName1();
                            if (flag3)
                            {
                              if (namedItem9 != null && namedItem9.Value == "auto")
                              {
                                if (namedItem8 != null && namedItem8.Value != "" && Convert.ToInt32(namedItem8.Value) > 0)
                                  str11 = str11 + "<td colspan=\"" + namedItem8.Value + "\">" + num1.ToString() + "</td>";
                                else
                                  str11 = str11 + "<td>" + num1.ToString() + "</td>";
                              }
                              string str13;
                              if (namedItem6 != null && namedItem6.Value != "" && Convert.ToInt32(namedItem6.Value) > 0)
                                str13 = str11 + "<td colspan=\"" + namedItem6.Value + "\"" + str12 + ">" + name1 + "</td>";
                              else
                                str13 = str11 + "<td" + str12 + ">" + name1 + "</td>";
                              string save = list[index1].getSave();
                              XmlNode xmlNode5 = value.SelectSingleNode(xpath + "/" + save);
                              if (xmlNode5 != null)
                              {
                                if (list[index1].isChar() && namedItem7 != null && (namedItem7.Value != "" && Convert.ToInt32(namedItem7.Value) > 0) && xmlNode5.InnerText.Length <= Convert.ToInt32(namedItem7.Value))
                                {
                                  char[] chArray = xmlNode5.InnerText.ToCharArray();
                                  string str14 = "";
                                  for (int index2 = 0; index2 < chArray.Length; ++index2)
                                    str14 = str14 + "<td colspan=\"1\">" + chArray[index2].ToString() + "</td>";
                                  string str15 = " ";
                                  if (list[index1].getFill() != "")
                                    str15 = list[index1].getFill();
                                  int num2 = Convert.ToInt32(namedItem7.Value) - chArray.Length;
                                  for (int index2 = 0; index2 < num2; ++index2)
                                    str14 = "<td colspan=\"1\">" + str15 + "</td>" + str14;
                                  str11 = str13 + str14;
                                }
                                else
                                  str11 = (namedItem7 == null ? str13 + "<td>" : str13 + "<td colspan=\"" + namedItem7.Value + "\">") + xmlNode5.InnerText + "</td>";
                              }
                              else
                                str11 = (namedItem7 == null ? str13 + "<td>" : str13 + "<td colspan=\"" + namedItem7.Value + "\">") + " " + "</td>";
                            }
                            else
                            {
                              string str13 = str11 + "<p>";
                              if (list[index1].getDataType() == "text" && list[index1].getSave() == "")
                              {
                                if (namedItem9 != null && namedItem9.Value == "auto")
                                  str13 = str13 + num1.ToString() + " ";
                                str13 = str13 + name1 + " ";
                              }
                              if (list[index1].getDataType() == "number" && list[index1].getSave() != "")
                              {
                                string str14 = list[index1].getSave().Replace("X", num1.ToString());
                                XmlNode xmlNode5 = value.SelectSingleNode(xpath + "/" + str14);
                                if (xmlNode5 != null)
                                  str13 = str13 + xmlNode5.Value;
                              }
                              str11 = str13 + "</p>";
                            }
                            ++num1;
                          }
                          if (flag3)
                            str11 = str11 + "</tr>";
                          str5 = str5 + str11;
                        }
                        if (itemChild1.Name == "patternlist")
                        {
                          XmlNode namedItem5 = itemChild1.Attributes.GetNamedItem("design");
                          bool flag5 = false;
                          if (namedItem5 != null && namedItem5.Value == "border")
                          {
                            str5 = str5 + str3;
                            flag5 = true;
                          }
                          int num1 = 1;
                          foreach (XmlNode lista in xmlNode4)
                          {
                            foreach (XmlNode itemChild2 in itemChild1)
                            {
                              if (itemChild2.Name == "title")
                              {
                                XmlNode namedItem6 = itemChild2.Attributes.GetNamedItem("bold");
                                XmlNode namedItem7 = itemChild2.Attributes.GetNamedItem("nr");
                                string str11 = itemChild2.InnerXml;
                                if (namedItem7 != null)
                                  str11 = namedItem7.Value + " " + num1.ToString() + " " + str11;
                                if (flag5)
                                {
                                  XmlNode namedItem8 = itemChild2.Attributes.GetNamedItem("colspan");
                                  str5 = namedItem8 == null ? str5 + "<tr><td>" : str5 + "<tr><td colspan=\"" + namedItem8.Value + "\">";
                                  str5 = namedItem6 == null || !(namedItem6.Value == "true") ? str5 + str11 : str5 + "<b>" + str11 + "</b>";
                                  str5 = str5 + "</td></tr>";
                                }
                                else
                                  str5 = namedItem6 == null || !(namedItem6.Value == "true") ? str5 + "<h1>" + str11 + "</h1>" : str5 + "<h1><b>" + str11 + "</b></h1>";
                              }
                              if (itemChild2.Name == "patternrow")
                              {
                                XmlNode namedItem6 = itemChild2.Attributes.GetNamedItem("colspan");
                                XmlNode namedItem7 = itemChild2.Attributes.GetNamedItem("colspan_save");
                                List<Field> patternField = new List<Field>();
                                List<Field> list = this.readPatternCandidate(itemChild2, patternField);
                                XmlNode namedItem8 = itemChild2.FirstChild.Attributes.GetNamedItem("class");
                                string str11 = "";
                                if (namedItem8 != null && namedItem8.Value != "")
                                  str11 = " class=\"" + namedItem8.Value + "\"";
                                for (int index1 = 0; index1 < list.Count; ++index1)
                                {
                                  if (!list[index1].needImportData())
                                  {
                                    if (flag5)
                                    {
                                      str5 = str5 + "<tr>";
                                      if (namedItem6 != null && namedItem6.Value != "" && Convert.ToInt32(namedItem6.Value) > 0)
                                        str5 = str5 + "<td colspan=\"" + namedItem6.Value + "\"" + str11 + ">" + list[index1].getName1() + "</td>";
                                      else
                                        str5 = str5 + "<td" + str11 + ">" + list[index1].getName1() + "</td>";
                                      string str12 = list[index1].getSave().Replace("Y", num1.ToString());
                                      XmlNode xmlNode5 = value.SelectSingleNode(xpath + "/" + str12);
                                      if (xmlNode5 != null)
                                      {
                                        if (list[index1].isChar() && namedItem7 != null && (namedItem7.Value != "" && Convert.ToInt32(namedItem7.Value) > 0) && xmlNode5.InnerText.Length <= Convert.ToInt32(namedItem7.Value))
                                        {
                                          char[] chArray = xmlNode5.InnerText.ToCharArray();
                                          string str13 = "";
                                          for (int index2 = 0; index2 < chArray.Length; ++index2)
                                            str13 = str13 + "<td colspan=\"1\">" + chArray[index2].ToString() + "</td>";
                                          string str14 = " ";
                                          if (list[index1].getFill() != "")
                                            str14 = list[index1].getFill();
                                          int num2 = Convert.ToInt32(namedItem7.Value) - chArray.Length;
                                          for (int index2 = 0; index2 < num2; ++index2)
                                            str13 = "<td colspan=\"1\">" + str14 + "</td>" + str13;
                                          str5 = str5 + str13;
                                        }
                                        else
                                        {
                                          str5 = namedItem7 == null ? str5 + "<td>" : str5 + "<td colspan=\"" + namedItem7.Value + "\">";
                                          str5 = str5 + xmlNode5.InnerText;
                                          str5 = str5 + "</td>";
                                        }
                                      }
                                      else
                                      {
                                        str5 = namedItem7 == null ? str5 + "<td>" : str5 + "<td colspan=\"" + namedItem7.Value + "\">";
                                        str5 = str5 + " ";
                                        str5 = str5 + "</td>";
                                      }
                                      str5 = str5 + "</tr>";
                                    }
                                    else
                                    {
                                      str5 = str5 + "<p>";
                                      str5 = str5 + list[index1].getName1();
                                      string str12 = list[index1].getSave().Replace("Y", num1.ToString());
                                      XmlNode xmlNode5 = value.SelectSingleNode(xpath + "/" + str12);
                                      if (xmlNode5 != null)
                                        str5 = str5 + " " + xmlNode5.InnerText;
                                      str5 = str5 + "</p>";
                                    }
                                  }
                                }
                              }
                              if (itemChild2.Name == "patternrows")
                              {
                                XmlNode namedItem6 = itemChild2.Attributes.GetNamedItem("colspan");
                                XmlNode namedItem7 = itemChild2.Attributes.GetNamedItem("colspan_save");
                                List<Field> patternField = new List<Field>();
                                XmlNode namedItem8 = itemChild2.Attributes.GetNamedItem("colspan_nr");
                                XmlNode namedItem9 = itemChild2.Attributes.GetNamedItem("lp");
                                List<Field> list = this.readPatternCandidate(itemChild2, patternField);
                                int num2 = 1;
                                for (int index1 = 0; index1 < lista.ChildNodes.Count; ++index1)
                                {
                                  if (flag5)
                                    str5 = str5 + "<tr>";
                                  for (int index2 = 0; index2 < list.Count; ++index2)
                                  {
                                    str6 = "";
                                    str7 = "";
                                    str8 = "";
                                    str10 = "";
                                    string str11 = "";
                                    str9 = "";
                                    string patternCandidate1 = this.getStatusBasePatternCandidate(list[index2], lista);
                                    if (patternCandidate1 == "A" || patternCandidate1 == "R")
                                    {
                                      string patternCandidate2 = this.getImie1BasePatternCandidate(list[index2], lista);
                                      string patternCandidate3 = this.getImie2BasePatternCandidate(list[index2], lista);
                                      string patternCandidate4 = this.getNazwiskoBasePatternCandidate(list[index2], lista);
                                      string patternCandidate5 = this.getKomitetBasePatternCandidate(list[index2], lista);
                                      string patternCandidate6 = this.getName2BasePatternCandidate(list[index2], lista);
                                      if (list[index2].getName1() != "")
                                        str11 = list[index2].getName1() + " ";
                                      string str12 = str11 + patternCandidate4 + patternCandidate2 + patternCandidate3 + patternCandidate6 + patternCandidate5;
                                      if (flag5)
                                      {
                                        if (list[index2].getDataType() == "text" && list[index2].getSave() == "")
                                        {
                                          if (namedItem9 != null && namedItem9.Value == "auto")
                                          {
                                            if (namedItem8 != null && namedItem8.Value != "" && Convert.ToInt32(namedItem8.Value) > 0)
                                              str5 = str5 + "<td colspan=\"" + namedItem8.Value + "\" class=\"lp\">" + num2.ToString() + "</td>";
                                            else
                                              str5 = str5 + "<td>" + num2.ToString() + "</td>";
                                          }
                                          if (namedItem6 != null && namedItem6.Value != "" && Convert.ToInt32(namedItem6.Value) > 0)
                                            str5 = str5 + "<td colspan=\"" + namedItem6.Value + "\">" + str12 + "</td>";
                                          else
                                            str5 = str5 + "<td>" + str12 + "</td>";
                                        }
                                        if (list[index2].getDataType() == "number" && list[index2].getSave() != "")
                                        {
                                          string str13 = list[index2].getSave().Replace("X", num2.ToString()).Replace("Y", num1.ToString());
                                          XmlNode xmlNode5 = value.SelectSingleNode(xpath + "/" + str13);
                                          if (xmlNode5 != null)
                                          {
                                            if (patternCandidate1 != "R")
                                            {
                                              if (list[index2].isChar() && namedItem7 != null && (namedItem7.Value != "" && Convert.ToInt32(namedItem7.Value) > 0) && xmlNode5.InnerText.Length <= Convert.ToInt32(namedItem7.Value))
                                              {
                                                char[] chArray = xmlNode5.InnerText.ToCharArray();
                                                string str14 = "";
                                                for (int index3 = 0; index3 < chArray.Length; ++index3)
                                                  str14 = str14 + "<td colspan=\"1\">" + chArray[index3].ToString() + "</td>";
                                                string str15 = " ";
                                                if (list[index2].getFill() != "")
                                                  str15 = list[index2].getFill();
                                                int num3 = Convert.ToInt32(namedItem7.Value) - chArray.Length;
                                                for (int index3 = 0; index3 < num3; ++index3)
                                                  str14 = "<td colspan=\"1\">" + str15 + "</td>" + str14;
                                                str5 = str5 + str14;
                                              }
                                              else
                                              {
                                                str5 = namedItem7 == null ? str5 + "<td>" : str5 + "<td colspan=\"" + namedItem7.Value + "\">";
                                                str5 = str5 + xmlNode5.InnerText;
                                                str5 = str5 + "</td>";
                                              }
                                              str5 = str5 + "</tr>";
                                            }
                                            else
                                            {
                                              str5 = namedItem7 == null ? str5 + "<td>" : str5 + "<td colspan=\"" + namedItem7.Value + "\">";
                                              str5 = str5 + "</td></tr>";
                                            }
                                          }
                                          else
                                          {
                                            str5 = namedItem7 == null ? str5 + "<td>" : str5 + "<td colspan=\"" + namedItem7.Value + "\">";
                                            str5 = !(patternCandidate1 == "R") ? str5 + " " : str5 + "Skreślony";
                                            str5 = str5 + "</td>";
                                            str5 = str5 + "</tr>";
                                          }
                                        }
                                      }
                                      else
                                      {
                                        str5 = str5 + "<p>";
                                        if (list[index2].getDataType() == "text" && list[index2].getSave() == "")
                                        {
                                          if (namedItem9 != null && namedItem9.Value == "auto")
                                            str5 = str5 + num2.ToString() + " ";
                                          str5 = str5 + str12 + " ";
                                        }
                                        if (list[index2].getDataType() == "number" && list[index2].getSave() != "")
                                        {
                                          string str13 = list[index2].getSave().Replace("X", num2.ToString());
                                          XmlNode xmlNode5 = value.SelectSingleNode(xpath + "/" + str13);
                                          if (xmlNode5 != null)
                                          {
                                            str5 = str5 + xmlNode5.Value;
                                            str5 = !(patternCandidate1 != "R") ? str5 + "Skreślony" : str5 + xmlNode5.Value;
                                          }
                                        }
                                        str5 = str5 + "</p>";
                                      }
                                    }
                                  }
                                  ++num2;
                                }
                              }
                            }
                            if (flag5)
                              str5 = str5 + str4;
                            if (xmlNode4.ChildNodes.Count > num1)
                              str5 = str5 + "<p></p>" + str3;
                            ++num1;
                          }
                        }
                      }
                    }
                  }
                  else if (xmlNode3.Name == "field")
                  {
                    string str6 = "";
                    XmlNode namedItem4 = xmlNode3.Attributes.GetNamedItem("lp");
                    XmlNode namedItem5 = xmlNode3.Attributes.GetNamedItem("przypis");
                    if (flag3)
                    {
                      string str7 = str6 + "<tr>";
                      XmlNode namedItem6 = xmlNode3.Attributes.GetNamedItem("colspan");
                      XmlNode namedItem7 = xmlNode3.Attributes.GetNamedItem("colspan_save");
                      XmlNode namedItem8 = xmlNode3.Attributes.GetNamedItem("colspan_nr");
                      if (namedItem4 != null && namedItem4.Value != "" && (namedItem8 != null && namedItem8.Value != "") && Convert.ToInt32(namedItem8.Value) > 0)
                        str7 = str7 + "<td colspan =\"" + namedItem8.Value + "\" class=\"lp\">" + namedItem4.Value + "</td>";
                      for (int index1 = 0; index1 < xmlNode3.ChildNodes.Count; ++index1)
                      {
                        if (xmlNode3.ChildNodes[index1].Name == "name" && namedItem6 != null && namedItem6.Value != "" && Convert.ToInt32(namedItem6.Value) > 0)
                        {
                          string str8 = "";
                          if (namedItem5 != null && namedItem5.Value != "")
                            str8 = "<sup>" + namedItem5.Value + "</sup> ";
                          str7 = str7 + "<td colspan =\"" + str8 + namedItem6.Value + "\">" + xmlNode3.ChildNodes[index1].InnerXml + "</td>";
                        }
                        if (xmlNode3.ChildNodes[index1].Name == "save_as")
                        {
                          XmlNode namedItem9 = xmlNode3.ChildNodes[index1].Attributes.GetNamedItem("char");
                          XmlNode xmlNode4 = value.SelectSingleNode(xpath + "/" + xmlNode3.ChildNodes[index1].InnerText);
                          if (xmlNode4 != null)
                          {
                            if (namedItem9 != null && namedItem9.Value == "true" && (namedItem7 != null && namedItem7.Value != "") && Convert.ToInt32(namedItem7.Value) > 0 && xmlNode4.InnerText.Length <= Convert.ToInt32(namedItem7.Value))
                            {
                              char[] chArray = xmlNode4.InnerText.ToCharArray();
                              string str8 = "";
                              for (int index2 = 0; index2 < chArray.Length; ++index2)
                                str8 = str8 + "<td colspan=\"1\">" + chArray[index2].ToString() + "</td>";
                              string str9 = " ";
                              XmlNode namedItem10 = xmlNode3.ChildNodes[index1].Attributes.GetNamedItem("fill");
                              if (namedItem10 != null && namedItem10.Value != "")
                                str9 = namedItem10.Value;
                              int num = Convert.ToInt32(namedItem7.Value) - chArray.Length;
                              for (int index2 = 0; index2 < num; ++index2)
                                str8 = "<td colspan=\"1\">" + str9 + "</td>" + str8;
                              str7 = str7 + str8;
                            }
                            else
                              str7 = (namedItem7 == null ? str7 + "<td>" : str7 + "<td colspan=\"" + namedItem7.Value + "\">") + xmlNode4.InnerText + "</td>";
                          }
                          else
                            str7 = namedItem7 == null ? str7 + "<td> </td>" : str7 + "<td colspan=\"" + namedItem7.Value + "\"> </td>";
                        }
                      }
                      string str10 = str7 + "</tr>";
                      str5 = str5 + str10;
                      printProtocol printProtocol = this;
                      string str11 = printProtocol.additionaContent + str10;
                      printProtocol.additionaContent = str11;
                    }
                    else
                    {
                      string str7 = str6 + "<p class=\"question\">";
                      for (int index = 0; index < xmlNode3.ChildNodes.Count; ++index)
                      {
                        if (xmlNode3.ChildNodes[index].Name == "name")
                        {
                          string str8 = "";
                          if (namedItem4 != null && namedItem4.Value != "")
                            str8 = namedItem4.Value + ". ";
                          string str9 = "";
                          if (namedItem5 != null && namedItem5.Value != "")
                            str9 = "<sup>" + namedItem5.Value + "</sup>";
                          str7 = str7 + str8 + str9 + xmlNode3.ChildNodes[index].InnerXml + " ";
                        }
                        if (xmlNode3.ChildNodes[index].Name == "save_as")
                        {
                          XmlNode namedItem6 = xmlNode3.ChildNodes[index].Attributes.GetNamedItem("newParagraph");
                          if (namedItem6 != null && namedItem6.Value == "true")
                            str7 = str7 + "</p><p class=\"response\">";
                          XmlNode xmlNode4 = value.SelectSingleNode(xpath + "/" + xmlNode3.ChildNodes[index].InnerText);
                          if (xmlNode4 != null)
                            str7 = str7 + xmlNode4.InnerXml;
                        }
                      }
                      string str10 = str7 + "</p>";
                      printProtocol printProtocol = this;
                      string str11 = printProtocol.additionaContent + str10;
                      printProtocol.additionaContent = str11;
                    }
                  }
                }
              }
              printProtocol printProtocol1 = this;
              string str17 = printProtocol1.body + str5;
              printProtocol1.body = str17;
            }
            if (namedItem1 != null && namedItem1.Value == "committee-calculator")
            {
              string str5 = "";
              XmlNode xmlNode3 = value.SelectSingleNode("/save");
              string str6 = "";
              if (xmlNode1 != null)
              {
                str6 = xmlNode1.InnerText;
                if (str6 == "0" || str6 == "4")
                  xpath = "/save/komisja_sklad";
                xmlNode3 = value.SelectSingleNode(xpath);
              }
              foreach (XmlNode xmlNode4 in xmlNode2)
              {
                XmlNode namedItem2 = xmlNode4.Attributes.GetNamedItem("data");
                XmlNode namedItem3 = xmlNode4.Attributes.GetNamedItem("design");
                bool flag3 = namedItem3 != null && namedItem3.Value == "border";
                xmlNode1 = value.SelectSingleNode("/save/step");
                if (namedItem2 != null && namedItem2.Value == "komisja_info")
                {
                  foreach (XmlNode xmlNode5 in xmlNode4)
                  {
                    if (xmlNode5.Name == "name")
                    {
                      XmlNode namedItem4 = xmlNode5.Attributes.GetNamedItem("bold");
                      if (flag3)
                      {
                        XmlNode namedItem5 = xmlNode5.Attributes.GetNamedItem("colspan");
                        str5 = namedItem5 == null ? str5 + "<tr><td>" : str5 + "<tr><td colspan=\"" + namedItem5.Value + "\">";
                        str5 = namedItem4 == null || !(namedItem4.Value == "true") ? str5 + xmlNode5.InnerXml : str5 + "<b>" + xmlNode5.InnerXml + "</b>";
                        str5 = str5 + "</td><tr>";
                      }
                      else
                        str5 = namedItem4 == null || !(namedItem4.Value == "true") ? str5 + "<p>" + xmlNode5.InnerXml + "</p>" : str5 + "<p><b>" + xmlNode5.InnerXml + "</b></p>";
                    }
                    if (str6 != "0" && str6 != "4" && !flag1)
                    {
                      str5 = str5 + "<table class=\"noborder\">";
                      for (int index = 0; index < 8; ++index)
                      {
                        str5 = str5 + "<tr><td colspan=\"14\"><span class=\"dot\">................................</span>";
                        string str7 = "";
                        if (index == 0)
                          str7 = "<br><center>(podpis)</center>";
                        str5 = str5 + "</td><td colspan=\"10\"><center class=\"dot\">................................</center>" + str7 + "</td></tr>";
                      }
                      str5 = str5 + "</table><br>";
                      str5 = str5 + "<center><img src=\"" + Path.GetDirectoryName(Application.StartupPath) + "\\tmp\\printTmp\\stampPlace.png\"></center>";
                      flag1 = true;
                    }
                    if (xmlNode5.Name == "patternrows")
                    {
                      for (int index = 0; index < xmlNode5.ChildNodes.Count; ++index)
                      {
                        XmlNode namedItem4 = xmlNode5.ChildNodes[index].Attributes.GetNamedItem("lp");
                        int num = 1;
                        if (xmlNode3 != null && xmlNode1 != null && (xmlNode1.InnerText == "0" || xmlNode1.InnerText == "4"))
                        {
                          char[] chArray = new char[7]
                          {
                            ' ',
                            '.',
                            '-',
                            ':',
                            ',',
                            '_',
                            '\n'
                          };
                          str5 = str5 + "<table class=\"noborder\">";
                          foreach (XmlNode xmlNode6 in xmlNode3)
                          {
                            if (xmlNode6.Attributes.GetNamedItem("obecny") != null && xmlNode6.Attributes.GetNamedItem("obecny").Value.ToLower() == "true")
                            {
                              string str7 = "";
                              if (namedItem4 != null && namedItem4.Value == "auto")
                                str7 = num.ToString() + ") ";
                              str5 = str5 + "<tr><td colspan=\"14\">" + str7;
                              if (xmlNode5.ChildNodes[index].Attributes.GetNamedItem("nazwisko") != null && xmlNode6.Attributes.GetNamedItem("nazwisko") != null)
                                str5 = str5 + xmlNode6.Attributes.GetNamedItem("nazwisko").Value + " ";
                              if (xmlNode5.ChildNodes[index].Attributes.GetNamedItem("imie") != null && xmlNode6.Attributes.GetNamedItem("imie") != null)
                                str5 = str5 + xmlNode6.Attributes.GetNamedItem("imie").Value + " ";
                              if (xmlNode5.ChildNodes[index].Attributes.GetNamedItem("imie2") != null && xmlNode6.Attributes.GetNamedItem("imie2") != null)
                              {
                                string str8 = xmlNode6.Attributes.GetNamedItem("imie2").Value.Trim(chArray);
                                if (str8 == "brak")
                                  str8 = "";
                                if (str8 != "")
                                  str5 = str5 + str8 + " ";
                              }
                              if (xmlNode5.ChildNodes[index].Attributes.GetNamedItem("funkcja") != null && xmlNode6.Attributes.GetNamedItem("funkcja") != null)
                              {
                                string str8 = xmlNode6.Attributes.GetNamedItem("funkcja").Value;
                                if (str8 == "CZŁONEK")
                                  str8 = "członek komisji";
                                else if (str8 == "PRZEWODNICZĄCY")
                                  str8 = "przewodniczący komisji";
                                else if (str8 == "ZASTĘPCA")
                                  str8 = "zastępca przewodniczącego";
                                str5 = str5 + str8 + " ";
                              }
                              string str9 = "";
                              if (num == 1)
                                str9 = "<br><center>(podpis)</center>";
                              str5 = str5 + "</td><td colspan=\"10\"><center class=\"dot\">................................</center>" + str9 + "</td></tr>";
                              ++num;
                            }
                          }
                          str5 = str5 + "</table>";
                          str5 = str5 + "<center><img src=\"" + Path.GetDirectoryName(Application.StartupPath) + "\\tmp\\printTmp\\stampPlace.png\"></center>";
                        }
                      }
                    }
                  }
                }
              }
              printProtocol printProtocol = this;
              string str10 = printProtocol.additionaContent + str5;
              printProtocol.additionaContent = str10;
            }
            if (namedItem1 != null && namedItem1.Value == "additional-table")
            {
              str2 = "";
              string str5 = "";
              if (xmlNode1 != null)
              {
                string innerText = xmlNode1.InnerText;
                if (innerText == "0" || innerText == "4")
                  xpath = "/save/form";
                if (innerText == "1" || innerText == "2" || innerText == "3")
                  xpath = "/save/step1";
              }
              bool flag3 = false;
              bool flag4 = false;
              foreach (XmlNode xmlNode3 in xmlNode2)
              {
                XmlNode namedItem2 = xmlNode3.Attributes.GetNamedItem("design");
                flag3 = flag4;
                flag4 = namedItem2 != null && namedItem2.Value == "border";
                if (!flag3 && flag4)
                  str5 = str5 + str3;
                if (flag3 && !flag4)
                  str5 = str5 + str4;
                if (xmlNode3.Name == "title")
                {
                  XmlNode namedItem3 = xmlNode3.Attributes.GetNamedItem("bold");
                  if (flag4)
                  {
                    XmlNode namedItem4 = xmlNode3.Attributes.GetNamedItem("colspan");
                    str5 = namedItem4 == null ? str5 + "<tr><td>" : str5 + "<tr><td colspan=\"" + namedItem4.Value + "\" class=\"center\">";
                    str5 = namedItem3 == null || !(namedItem3.Value == "true") ? str5 + xmlNode3.InnerXml : str5 + "<b>" + xmlNode3.InnerXml + "</b>";
                    str5 = str5 + "</td><tr>";
                  }
                  else
                    str5 = namedItem3 == null || !(namedItem3.Value == "true") ? str5 + "<h1>" + xmlNode3.InnerXml + "</h1>" : str5 + "<h1><b>" + xmlNode3.InnerXml + "</b></h1>";
                }
                if (xmlNode3.Name == "description")
                {
                  XmlNode namedItem3 = xmlNode3.Attributes.GetNamedItem("bold");
                  if (flag4)
                  {
                    XmlNode namedItem4 = xmlNode3.Attributes.GetNamedItem("colspan");
                    str5 = namedItem4 == null ? str5 + "<tr><td>" : str5 + "<tr><td colspan=\"" + namedItem4.Value + "\">";
                    str5 = namedItem3 == null || !(namedItem3.Value == "true") ? str5 + xmlNode3.InnerXml : str5 + "<b>" + xmlNode3.InnerXml + "</b>";
                    str5 = str5 + "</td><tr>";
                  }
                  else
                    str5 = namedItem3 == null || !(namedItem3.Value == "true") ? str5 + "<p>" + xmlNode3.InnerXml + "</p>" : str5 + "<p><b>" + xmlNode3.InnerXml + "</b></p>";
                }
                if (xmlNode3.Name == "note")
                {
                  if (xmlNode3.InnerText != "")
                  {
                    XmlNode namedItem3 = xmlNode3.Attributes.GetNamedItem("bold");
                    if (flag4)
                    {
                      XmlNode namedItem4 = xmlNode3.Attributes.GetNamedItem("colspan");
                      str5 = namedItem4 == null ? str5 + "<tr><td>" : str5 + "<tr><td colspan=\"" + namedItem4.Value + "\">";
                      str5 = namedItem3 == null || !(namedItem3.Value == "true") ? str5 + xmlNode3.InnerXml : str5 + "<b>Uwaga!</b><b>" + xmlNode3.InnerXml + "</b>";
                      str5 = str5 + "</td><tr>";
                    }
                    else
                      str5 = namedItem3 == null || !(namedItem3.Value == "true") ? str5 + "<table class=\"noborder\"><tr><td colspan=\"2\"><b>Uwaga!</b></td><td colspan=\"22\">" + xmlNode3.InnerXml + "</td></tr></table>" : str5 + "<table class=\"noborder\" ><tr><td colspan=\"2\"><b>Uwaga!</b></td><td colspan=\"22\"><b>" + xmlNode3.InnerXml + "</b></td></tr></table>";
                  }
                  else if (flag4)
                  {
                    XmlNode namedItem3 = xmlNode3.Attributes.GetNamedItem("colspan");
                    str5 = namedItem3 == null ? str5 + "<tr><td>" : str5 + "<tr><td colspan=\"" + namedItem3.Value + "\">";
                    str5 = str5 + " <td></tr>";
                  }
                  else
                    str5 = str5 + "<p></p>";
                }
                if (xmlNode3.Name == "field")
                {
                  string str6 = "";
                  XmlNode namedItem3 = xmlNode3.Attributes.GetNamedItem("lp");
                  string str7;
                  if (flag4)
                  {
                    string str8 = str6 + "<tr>";
                    XmlNode namedItem4 = xmlNode3.Attributes.GetNamedItem("colspan");
                    XmlNode namedItem5 = xmlNode3.Attributes.GetNamedItem("colspan_save");
                    XmlNode namedItem6 = xmlNode3.Attributes.GetNamedItem("colspan_nr");
                    if (namedItem3 != null && namedItem3.Value != "" && (namedItem6 != null && namedItem6.Value != "") && Convert.ToInt32(namedItem6.Value) > 0)
                      str8 = str8 + "<td colspan =\"" + namedItem6.Value + "\" class=\"lp\">" + namedItem3.Value + ". </td>";
                    for (int index1 = 0; index1 < xmlNode3.ChildNodes.Count; ++index1)
                    {
                      string str9 = xmlNode3.ChildNodes[index1].InnerXml.Replace(Environment.NewLine, "<br>");
                      if (xmlNode3.ChildNodes[index1].Name == "name" && namedItem4 != null && namedItem4.Value != "" && Convert.ToInt32(namedItem4.Value) > 0)
                        str8 = str8 + "<td colspan =\"" + namedItem4.Value + "\">" + str9 + "</td>";
                      if (xmlNode3.ChildNodes[index1].Name == "save_as")
                      {
                        XmlNode namedItem7 = xmlNode3.ChildNodes[index1].Attributes.GetNamedItem("char");
                        XmlNode xmlNode4 = value.SelectSingleNode(xpath + "/" + xmlNode3.ChildNodes[index1].InnerText);
                        if (xmlNode4 != null)
                        {
                          if (namedItem7 != null && namedItem7.Value == "true" && (namedItem5 != null && namedItem5.Value != "") && Convert.ToInt32(namedItem5.Value) > 0 && xmlNode4.InnerText.Length <= Convert.ToInt32(namedItem5.Value))
                          {
                            char[] chArray = xmlNode4.InnerText.ToCharArray();
                            string str10 = "";
                            for (int index2 = 0; index2 < chArray.Length; ++index2)
                              str10 = str10 + "<td colspan=\"1\">" + chArray[index2].ToString() + "</td>";
                            string str11 = " ";
                            XmlNode namedItem8 = xmlNode3.ChildNodes[index1].Attributes.GetNamedItem("fill");
                            if (namedItem8 != null && namedItem8.Value != "")
                              str11 = namedItem8.Value;
                            int num = Convert.ToInt32(namedItem5.Value) - chArray.Length;
                            for (int index2 = 0; index2 < num; ++index2)
                              str10 = "<td colspan=\"1\">" + str11 + "</td>" + str10;
                            str8 = str8 + str10;
                          }
                          else
                            str8 = (namedItem5 == null ? str8 + "<td>" : str8 + "<td colspan=\"" + namedItem5.Value + "\">") + xmlNode4.InnerText + "</td>";
                        }
                        else
                          str8 = namedItem5 == null ? str8 + "<td> </td>" : str8 + "<td colspan=\"" + namedItem5.Value + "\"> </td>";
                      }
                    }
                    str7 = str8 + "</tr>";
                  }
                  else
                  {
                    string str8 = str6 + "<p>";
                    if (namedItem3 != null && namedItem3.Value != "")
                      str8 = str8 + namedItem3.Value + " ";
                    for (int index = 0; index < xmlNode3.ChildNodes.Count; ++index)
                    {
                      if (xmlNode3.ChildNodes[index].Name == "name")
                        str8 = str8 + xmlNode3.ChildNodes[index].InnerXml + " ";
                      if (xmlNode3.ChildNodes[index].Name == "save_as")
                      {
                        XmlNode xmlNode4 = value.SelectSingleNode(xpath + "/" + xmlNode3.ChildNodes[index].InnerText);
                        if (xmlNode4 != null)
                          str8 = str8 + xmlNode4.Value;
                      }
                    }
                    str7 = str8 + "</p>";
                  }
                  str5 = str5 + str7;
                }
              }
              if (!flag3 && flag4)
                str5 = str5 + str3;
              if (flag3 && !flag4)
                str5 = str5 + str4;
              str2 = str2 + str5;
            }
          }
          if (xmlNode2.Name == "przypis")
          {
            string str5 = "<p class=\"sup\">";
            XmlNode namedItem = xmlNode2.Attributes.GetNamedItem("name");
            if (namedItem != null)
              str5 = str5 + "<sup>" + namedItem.Value + "</sup>" + xmlNode2.InnerXml;
            string str6 = str5 + "</p>";
            printProtocol printProtocol = this;
            string str7 = printProtocol.additionaContent + str6;
            printProtocol.additionaContent = str7;
          }
        }
        List<string>[] error = this.getError(value);
        try
        {
          printProtocol printProtocol1 = this;
          string str5 = printProtocol1.raport + "</table>";
          printProtocol1.raport = str5;
          printProtocol printProtocol2 = this;
          string str6 = printProtocol2.raport + "<h1><b>RAPORT OSTRZEŻEŃ W PROTOKOLE GŁOSOWANIA</b></h1>";
          printProtocol2.raport = str6;
          if (error.Length == 2)
          {
            if (error[0].Count > 0)
            {
              printProtocol printProtocol3 = this;
              string str7 = printProtocol3.raport + "<h1><b>Ostrzeżenia blokujące wydruk</b></h1>";
              printProtocol3.raport = str7;
              for (int index = 0; index < error[0].Count; ++index)
              {
                printProtocol printProtocol4 = this;
                string str8 = printProtocol4.raport + "<p><b>" + error[0][index] + "</b> " + this.errors[error[0][index]] + "</p>";
                printProtocol4.raport = str8;
              }
            }
            if (error[1].Count > 0)
            {
              printProtocol printProtocol3 = this;
              string str7 = printProtocol3.raport + "<h1><b>Ostrzeżenia</b></h1>";
              printProtocol3.raport = str7;
              for (int index = 0; index < error[1].Count; ++index)
              {
                printProtocol printProtocol4 = this;
                string str8 = printProtocol4.raport + "<p><b>" + error[1][index] + "</b> " + this.errors[error[1][index]] + "</p>";
                printProtocol4.raport = str8;
              }
            }
            printProtocol printProtocol5 = this;
            string str9 = printProtocol5.raport + "<p></p><p> Stanowisko Komisji:</p>";
            printProtocol5.raport = str9;
            printProtocol printProtocol6 = this;
            string str10 = printProtocol6.raport + "<p class=\"dot\">..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... ..... .....</p>";
            printProtocol6.raport = str10;
            printProtocol printProtocol7 = this;
            string str11 = printProtocol7.raport + "<p> Przy sporządzeniu raportu ostrzeżeń byli członkowie Komisji:</p>";
            printProtocol7.raport = str11;
            printProtocol printProtocol8 = this;
            string str12 = printProtocol8.raport + "<p class=\"dot\"> ........................................ ........................................<br>";
            printProtocol8.raport = str12;
            printProtocol printProtocol9 = this;
            string str13 = printProtocol9.raport + "........................................ ........................................<br>";
            printProtocol9.raport = str13;
            printProtocol printProtocol10 = this;
            string str14 = printProtocol10.raport + "........................................ ........................................<br>";
            printProtocol10.raport = str14;
            printProtocol printProtocol11 = this;
            string str15 = printProtocol11.raport + "........................................ ........................................</p>";
            printProtocol11.raport = str15;
            printProtocol printProtocol12 = this;
            string str16 = printProtocol12.raport + "<div id=\"breakTab1\" class=\"break\"> &#8194 </div>";
            printProtocol12.raport = str16;
          }
        }
        catch (Exception ex)
        {
        }
        string str18 = "<br><br><br><br><br><br><table class=\"noborder\"><tr><td colspan=\"14\"></td><td colspan=\"10\"><center class=\"dot\">................................</center> <br><center>(podpis przewodniczącego / zastępcy przewodniczącego<sup>*</sup>) komisji – osoby, której nazwisko zostało podane w pkt 3 zestawienia)</center></td></tr></table><br><br><br><br><br><br><br><sup>*</sup> Zbędne pominąć";
        XmlNode xmlNode8 = value.SelectSingleNode("/save/step");
        if (xmlNode8 != null && xmlNode8.InnerText == "0")
        {
          printProtocol printProtocol = this;
          string str5 = printProtocol.body + this.converToPage();
          printProtocol.body = str5;
          string str6 = str2 + str18 + "<div id=\"breakTab\" class=\"break\"> &#8194 </div>";
          if (error[0].Count > 0 || error[1].Count > 0)
            str6 = str6 + this.raport;
          this.body = str6 + this.body;
        }
        else
        {
          printProtocol printProtocol = this;
          string str5 = printProtocol.body + this.additionaContent;
          printProtocol.body = str5;
          string str6 = str2 + "<div id=\"breakTab\" class=\"break\"> &#8194 </div>";
          if (error[0].Count > 0 || error[1].Count > 0)
            str6 = str6 + this.raport;
          this.body = str6 + this.body;
        }
      }
      catch (XmlException ex)
      {
        int num = (int) MessageBox.Show("Nieprawidłowy XML", "Błąd");
      }
      catch (NullReferenceException ex)
      {
        int num = (int) MessageBox.Show("Podanno inny xml niz header", "Błąd");
      }
      return str1;
    }

    public List<string>[] getError(XmlDocument value)
    {
      List<string>[] listArray = new List<string>[2];
      XmlNode xmlNode1 = value.SelectSingleNode("/save/hardWarning");
      XmlNode xmlNode2 = value.SelectSingleNode("/save/softError");
      listArray[0] = new List<string>();
      if (xmlNode1 != null)
      {
        foreach (XmlNode xmlNode3 in xmlNode1)
        {
          for (int index1 = 0; index1 < xmlNode3.ChildNodes.Count; ++index1)
          {
            bool flag = true;
            for (int index2 = 0; index2 < listArray[0].Count; ++index2)
            {
              if (xmlNode3.ChildNodes[index1].InnerText == listArray[0][index2])
              {
                flag = false;
                break;
              }
            }
            if (flag)
              listArray[0].Add(xmlNode3.ChildNodes[index1].InnerText);
          }
        }
      }
      if (listArray[0].Count > 1)
        listArray[0].Sort();
      listArray[1] = new List<string>();
      if (xmlNode2 != null)
      {
        foreach (XmlNode xmlNode3 in xmlNode2)
        {
          for (int index1 = 0; index1 < xmlNode3.ChildNodes.Count; ++index1)
          {
            bool flag = true;
            for (int index2 = 0; index2 < listArray[1].Count; ++index2)
            {
              if (xmlNode3.ChildNodes[1].InnerText == listArray[1][index2])
              {
                flag = false;
                break;
              }
            }
            if (flag)
              listArray[1].Add(xmlNode3.ChildNodes[index1].InnerText);
          }
        }
      }
      if (listArray[1].Count > 1)
        listArray[1].Sort();
      return listArray;
    }

    private void setErrorsTab()
    {
      try
      {
        this.errors = new Dictionary<string, string>();
        this.errors.Add("INW", "Nie wypełnione pole/a \"Imię\"");
        this.errors.Add("NNW", "Nie wypełnione pole/a \"Nazwisko\"");
        this.errors.Add("ErrorNull", "Pole puste");
        this.errors.Add("ErrorType", "Nie prawidłowy format danych");
        this.errors.Add("ErrorEqual", "Odpowiednie pola z kroku 1 i 2 nie są sobie równe");
        this.errors.Add("SNT02", "Liczba kart do głosowania wydanych wyborcom (z pkt 3) nie może być większa od liczby wyborców uprawnionych do głosowania (wpisanych do spisu wyborców według stanu w chwili zakończenia głosowania – z pkt 1)");
        this.errors.Add("SNT02a", "W przypadku obwodu wyznaczonego dla głosowania korespondencyjnego suma liczby kart do głosowania wydanych wyborcom (pkt 3) i liczby wysłanych pakietów wyborczych (pkt 1a) nie może być większa od liczby wyborców uprawnionych do głosowania (wpisanych do spisu wyborców według stanu w chwili zakończenia głosowania – pkt 1).");
        this.errors.Add("SNT03", "Liczba wyborców głosujących przez pełnomocnika (z pkt 3a) nie może być większa od liczby kart do głosowania wydanych wyborcom (z pkt 3)");
        this.errors.Add("SNT03a", "W przypadku obwodów głosowania innych niż stałe utworzone w kraju liczba wyborców głosujących przez pełnomocnika (z pkt 3a) musi wynosić 0.");
        this.errors.Add("SNT04", "Liczba wysłanych pakietów wyborczych (pkt 1a) nie może być większa od liczby kart do głosowania wydanych wyborcom (pkt 3). Nie dotyczy obwodów utworzonych za granicą.");
        this.errors.Add("SNT04a", "Liczba wysłanych pakietów wyborczych (pkt 1a) w przypadku obwodowych komisji wyborczych niewyznaczonych dla głosowania korespondencyjnego musi wynosić 0.");
        this.errors.Add("SNT05", "Suma liczb z pkt. 6. i 7. <b>musi</b> być równa liczbie z pkt. 5.");
        this.errors.Add("SNT06", "Suma liczb z pkt. 8. i 9. <b>musi</b> być równa liczbie z pkt. 7.");
        this.errors.Add("SNT07", "Liczba głosów ważnych (pkt 9) musi być równa sumie liczb głosów ważnych łącznie oddanych na wszystkich kandydatów – w przypadku głosowania na więcej niż jednego kandydata.");
        this.errors.Add("SNT08", "Wartość w rubryce „Razem” w pkt 10 protokołu musi być równa sumie liczb głosów oddanych na kandydatów.");
        this.errors.Add("SNT10a", "W obwodach nieobjętych głosowaniem korespondencyjnym liczby w zestawieniu dotyczące głosowania korespondencyjnego (z zestawienia 5) muszą być równe 0.");
        this.errors.Add("SNT10b", "W obwodach nieobjętych głosowaniem korespondencyjnym liczby w zestawieniu dotyczące głosowania korespondencyjnego (z zestawienia 5) muszą być równe 0.");
        this.errors.Add("SNT10c", "W obwodach nieobjętych głosowaniem korespondencyjnym liczby w zestawieniu dotyczące głosowania korespondencyjnego (z zestawienia 5) muszą być równe 0.");
        this.errors.Add("SNT10d", "W obwodach nieobjętych głosowaniem korespondencyjnym liczby w zestawieniu dotyczące głosowania korespondencyjnego (z zestawienia 5) muszą być równe 0.");
        this.errors.Add("SNT10e", "W obwodach nieobjętych głosowaniem korespondencyjnym liczby w zestawieniu dotyczące głosowania korespondencyjnego (z zestawienia 5) muszą być równe 0.");
        this.errors.Add("SNT10f", "W obwodach nieobjętych głosowaniem korespondencyjnym liczby w zestawieniu dotyczące głosowania korespondencyjnego (z zestawienia 5) muszą być równe 0.");
        this.errors.Add("SNT11", "Suma liczby osób głosujących na podstawie zaświadczenia (z zestawienia 4) i zawartej w protokole liczby osób głosujących przez pełnomocnika (z pkt 3a) nie może być większa od liczby wydanych kart do głosowania (z pkt 3)");
        this.errors.Add("SNT12", "Suma liczby kopert zwrotnych bez oświadczenia (z zaświadczenia 6) i liczby kopert z niepodpisanym zaświadczeniem (z zestawienia 7) nie może być większa od liczby otrzymanych kopert zwrotnych (z zestawienia 5).");
        this.errors.Add("SNT13", "Suma liczby kopert zwrotnych bez koperty na kartę do głosowania (z zestawienia 8) oraz liczby kopert zwrotnych z niezaklejoną kopertą na kartę do głosowania (z zestawienia 9) nie może być większa od liczby otrzymanych kopert zwrotnych (z zestawienia 5).");
        this.errors.Add("SNT14", "Liczba kopert na karty do głosowania wrzuconych do urny nie może być większa niż liczba otrzymanych kopert zwrotnych pomniejszona o liczbę kopert zwrotnych nie spełniających warunków");
        this.errors.Add("SNTO01", "Liczba wyborców w obwodzie stałym, bez osób dopisanych do spisu na podstawie zaświadczenia o prawie do głosowania (czyli pkt 1 protokołu minus pkt 4 zestawienia) nie powinna być większa od 110% szacowanej liczby wyborców (liczby wyborców ustalonej w meldunku wyborczym).");
        this.errors.Add("SNTO01a", "Liczba wyborców w obwodzie stałym, bez osób dopisanych do spisu na podstawie zaświadczenia o prawie do głosowania (czyli pkt 1 protokołu minus pkt 4 zestawienia) nie powinna być mniejsza od 90% szacowanej liczby wyborców (liczby wyborców ustalonej w meldunku wyborczym).");
        this.errors.Add("SNTO02", "Liczba uprawnionych do głosowania (z pkt 1) nie powinna być mniejsza niż liczba kart ważnych (z pkt 7)");
        this.errors.Add("SNTO03", "Suma liczby osób głosujących przez pełnomocnika (pkt 3a) i liczby osób głosujących na podstawie zaświadczenia (pkt 4 zestawienia) nie powinna być równa liczbie wydanych kart do głosowania (pkt 3).");
        this.errors.Add("SNTO04", "Suma liczby wydanych przez komisję kart do głosowania (z pkt 3) i liczby kart niewykorzystanych (z pkt 4) nie powinna być większa od liczby kart do głosowania otrzymanych przez komisję (z pkt 2)");
        this.errors.Add("SNTO05", "Liczba kart nieważnych (pkt 6) jest większa od 0, a liczba głosów nieważnych (pkt 8) równa jest 0. Prawdopodobnie głosy nieważne zostały wpisane jako karty nieważne");
        this.errors.Add("SNTO06", "W obwodzie niekorespondencyjnym liczba kart wyjętych z urny (pkt 5) nie powinna być większa od liczby wyborców, którym wydano karty do głosowania (pkt 3).");
        this.errors.Add("SNTO06a", "W obwodzie korespondencyjnym liczba kart wyjętych z urny (pkt 5) pomniejszona o liczbę kart wyjętych z kopert na karty do głosowania (pkt 5a) nie powinna być większa od liczby wyborców, którym wydano karty do głosowania (pkt 3).");
        this.errors.Add("SNTO07", "Liczba otrzymanych kart do głosowania (z pkt 2) w obwodach stałych utworzonych w kraju nie powinna odbiegać o więcej niż 10 punktów procentowych od przewidywanego nakładu kart do głosowania.");
        this.errors.Add("SNTO07a", "Liczba otrzymanych kart do głosowania (z pkt 2) w obwodach stałych utworzonych w kraju nie powinna odbiegać o więcej niż 10 punktów procentowych od przewidywanego nakładu kart do głosowania.");
        this.errors.Add("SNTO12", "Liczba otrzymanych kopert zwrotnych (z zestawienia 5) nie powinna być większa, niż liczba wysłanych pakietów wyborczych (z pkt 1a).");
        this.errors.Add("SNTO13", "Liczba kart wyjętych z kopert na karty do głosowania (z pkt 5a) nie powinna być większa od liczby wyborców, którym wysłano pakiety wyborcze (z pkt 1a).");
        this.errors.Add("SNTO14", "Liczba kart do głosowania wyjętych z kopert na karty do głosowania (z pkt 5a) nie powinna być większa od liczby kopert na kartę do głosowania wrzuconych do urny (z zaświadczenia 10)");
        this.errors.Add("SNM01", "W obwodzie stałym liczba kart wydanych (pkt 3) nie powinna przekraczać 90% liczby wyborców (pkt 1).");
        this.errors.Add("SNM01a", "W obwodzie stałym przeznaczonym do głosowania korespondencyjnego suma liczby kart wydanych (pkt 3) i wysłanych pakietów wyborczych (pkt 1a) nie powinna przekraczać 90% liczby wyborców (pkt 1).");
        this.errors.Add("SNM02", "Liczba uprawnionych do głosowania (z pkt 1) powinna być niezerowa");
        this.errors.Add("SNM03", "Liczba kart wydanych (z pkt 3) powinna być niezerowa.");
        this.errors.Add("SNM04", "Liczba kart wyjętych z urny (z pkt 5) powinna być niezerowa.");
        this.errors.Add("SNM05", "Liczba kart ważnych (z pkt 7) powinna być niezerowa.");
        this.errors.Add("SNM06", "Liczba kart ważnych wyjętych z urny (z pkt 7) nie powinna być większa od liczby wyborców uprawnionych do głosowania (z pkt 1).");
        this.errors.Add("SNM07", "Liczba kart ważnych (z pkt 7) powinna być równa liczbie osób, którym wydano karty (z pkt 3)");
        this.errors.Add("SNM07a", "W obwodzie korespondencyjnym liczba kart ważnych (pkt 7) pomniejszona o liczbę wysłanych pakietów wyborczych (pkt 1a) powinna być równa liczbie osób, którym wydano karty (pkt 3)");
        this.errors.Add("SNM08", "Liczba kart nieważnych (z pkt 6) nie powinna być większa od zera");
        this.errors.Add("SNM09", "Protokół obwodowy powinien mieć wprowadzony skład obwodowej komisji wyborczej");
        this.errors.Add("SNM010", "Liczba członków komisji  (wliczając przewodniczącego i zastępcę) powinna mieścić się w ustawowych widełkach.");
        this.errors.Add("SNM011", "Komisja powinna mieć przewodniczącego");
        this.errors.Add("SNM012", "Komisja powinna mieć wiceprzewodniczącego");
        this.errors.Add("SNM013", "Protokół powinna podpisać przynajmniej połowa składu komisji");
        this.errors.Add("SNM014", "Protokół powinien podpisać przewodniczący bądź zastępca");
      }
      catch (Exception ex)
      {
      }
    }

    private List<Field> readPatternCandidate(XmlNode itemChild, List<Field> patternField)
    {
      foreach (XmlNode xmlNode in itemChild)
      {
        if (xmlNode.Name == "fildpatern")
        {
          XmlNode namedItem1 = xmlNode.Attributes.GetNamedItem("name");
          XmlNode namedItem2 = xmlNode.Attributes.GetNamedItem("name2");
          XmlNode namedItem3 = xmlNode.Attributes.GetNamedItem("name2v2");
          XmlNode namedItem4 = xmlNode.Attributes.GetNamedItem("plec");
          XmlNode namedItem5 = xmlNode.Attributes.GetNamedItem("status");
          XmlNode namedItem6 = xmlNode.Attributes.GetNamedItem("imie");
          XmlNode namedItem7 = xmlNode.Attributes.GetNamedItem("imie2");
          XmlNode namedItem8 = xmlNode.Attributes.GetNamedItem("nazwisko");
          XmlNode namedItem9 = xmlNode.Attributes.GetNamedItem("data");
          XmlNode namedItem10 = xmlNode.Attributes.GetNamedItem("komitet-wyborczy");
          XmlNode namedItem11 = xmlNode.Attributes.GetNamedItem("save_as");
          XmlNode namedItem12 = xmlNode.Attributes.GetNamedItem("char");
          XmlNode namedItem13 = xmlNode.Attributes.GetNamedItem("fill");
          XmlNode namedItem14 = xmlNode.Attributes.GetNamedItem("display");
          string name1 = "";
          if (namedItem1 != null)
            name1 = namedItem1.Value;
          string name2 = "";
          if (namedItem2 != null)
            name2 = namedItem2.Value;
          string name2v2 = "";
          if (namedItem3 != null)
            name2v2 = namedItem3.Value;
          string status = "";
          if (namedItem5 != null)
            status = namedItem5.Value;
          string plec = "";
          if (namedItem4 != null)
            plec = namedItem4.Value;
          string imie1 = "";
          if (namedItem6 != null)
            imie1 = namedItem6.Value;
          string imie2 = "";
          if (namedItem7 != null)
            imie2 = namedItem7.Value;
          string nazwisko = "";
          if (namedItem8 != null)
            nazwisko = namedItem8.Value;
          string dataType = "";
          if (namedItem9 != null)
            dataType = namedItem9.Value;
          string save_as = "";
          if (namedItem11 != null)
            save_as = namedItem11.Value;
          string komitet = "";
          if (namedItem10 != null)
            komitet = namedItem10.Value;
          string display = "";
          if (namedItem14 != null)
            display = namedItem14.Value;
          string name3 = "";
          if (namedItem12 != null)
            name3 = namedItem12.Value;
          string lista = "";
          if (namedItem13 != null)
            lista = namedItem13.Value;
          patternField.Add(new Field(name1, name2, name2v2, plec, status, imie1, imie2, nazwisko, dataType, save_as, komitet, name3, lista, display));
        }
      }
      return patternField;
    }

    private string getImie2BasePatternCandidate(Field patternFieldI, XmlNode lista)
    {
      string str = "";
      if (patternFieldI.getImie2() == patternFieldI.getImie2().Replace("parent:", ""))
      {
        if (lista.FirstChild.Attributes.GetNamedItem(patternFieldI.getImie2()) != null)
          str = lista.FirstChild.Attributes.GetNamedItem(patternFieldI.getImie2()).Value + " ";
      }
      else
      {
        string name = patternFieldI.getImie2().Replace("parent:", "");
        if (lista.Attributes.GetNamedItem(name) != null)
          str = lista.Attributes.GetNamedItem(name).Value + " ";
      }
      return str;
    }

    private string getImie1BasePatternCandidate(Field patternFieldI, XmlNode lista)
    {
      string str = "";
      if (patternFieldI.getImie1() == patternFieldI.getImie1().Replace("parent:", ""))
      {
        if (lista.FirstChild.Attributes.GetNamedItem(patternFieldI.getImie1()) != null)
          str = lista.FirstChild.Attributes.GetNamedItem(patternFieldI.getImie1()).Value + " ";
      }
      else
      {
        string name = patternFieldI.getImie1().Replace("parent:", "");
        if (lista.Attributes.GetNamedItem(name) != null)
          str = lista.Attributes.GetNamedItem(name).Value + " ";
      }
      return str;
    }

    private string getNazwiskoBasePatternCandidate(Field patternFieldI, XmlNode lista)
    {
      string str = "";
      if (patternFieldI.getNazwisko() == patternFieldI.getNazwisko().Replace("parent:", ""))
      {
        if (lista.FirstChild.Attributes.GetNamedItem(patternFieldI.getNazwisko()) != null)
          str = lista.FirstChild.Attributes.GetNamedItem(patternFieldI.getNazwisko()).Value + " ";
      }
      else
      {
        string name = patternFieldI.getNazwisko().Replace("parent:", "");
        if (lista.Attributes.GetNamedItem(name) != null)
          str = lista.Attributes.GetNamedItem(name).Value + " ";
      }
      return str;
    }

    private string getKomitetBasePatternCandidate(Field patternFieldI, XmlNode lista)
    {
      string str = "";
      if (patternFieldI.getKomitet() == patternFieldI.getKomitet().Replace("parent:", ""))
      {
        if (lista.FirstChild.Attributes.GetNamedItem(patternFieldI.getKomitet()) != null)
          str = lista.FirstChild.Attributes.GetNamedItem(patternFieldI.getKomitet()).Value;
      }
      else
      {
        string name = patternFieldI.getKomitet().Replace("parent:", "");
        if (lista.Attributes.GetNamedItem(name) != null)
          str = lista.Attributes.GetNamedItem(name).Value;
      }
      return str;
    }

    private string getName2BasePatternCandidate(Field patternFieldI, XmlNode lista)
    {
      string str = "";
      if (patternFieldI.getPlec() == patternFieldI.getPlec().Replace("parent:", ""))
      {
        if (lista.FirstChild.Attributes.GetNamedItem(patternFieldI.getPlec()) != null)
          str = !(lista.FirstChild.Attributes.GetNamedItem(patternFieldI.getPlec()).Value.ToUpper() == "M") ? patternFieldI.getName2v2() + " " : patternFieldI.getName2() + " ";
      }
      else
      {
        string name = patternFieldI.getPlec().Replace("parent:", "");
        if (lista.Attributes.GetNamedItem(name) != null)
          str = !(lista.Attributes.GetNamedItem(name).Value.ToUpper() == "M") ? patternFieldI.getName2v2() + " " : patternFieldI.getName2() + " ";
        else if (patternFieldI.getName2() != "")
          str = patternFieldI.getName2() + " ";
      }
      return str;
    }

    private string getStatusBasePatternCandidate(Field patternFieldI, XmlNode lista)
    {
      string str = "A";
      if (patternFieldI.getStatus() == patternFieldI.getStatus().Replace("parent:", ""))
      {
        if (lista.FirstChild.Attributes.GetNamedItem(patternFieldI.getStatus()) != null)
          str = lista.FirstChild.Attributes.GetNamedItem(patternFieldI.getStatus()).Value;
      }
      else
      {
        string name = patternFieldI.getStatus().Replace("parent:", "");
        if (lista.Attributes.GetNamedItem(name) != null)
          str = lista.Attributes.GetNamedItem(name).Value;
      }
      return str;
    }

    private string converToPage()
    {
      string str1 = "";
      int num1 = 50;
      int num2 = 100;
      int num3 = 0;
      int num4 = 1;
      string input = "";
      bool flag1 = false;
      bool flag2 = false;
      for (int startIndex = 0; startIndex < this.additionaContent.Length; ++startIndex)
      {
        char ch1 = this.additionaContent[startIndex];
        if ((int) this.additionaContent[startIndex] == 10)
        {
          str1 = str1 + "<br>";
          ++num4;
          num3 = 0;
        }
        char ch2;
        if ((int) this.additionaContent[startIndex] == 60)
        {
          str1 = str1 + (object) this.additionaContent[startIndex];
          flag1 = true;
          ch2 = this.additionaContent[startIndex];
          input = ch2.ToString();
        }
        else if ((int) this.additionaContent[startIndex] == 62)
        {
          flag1 = false;
          str1 = str1 + (object) this.additionaContent[startIndex];
          string str2 = input;
          ch2 = this.additionaContent[startIndex];
          string str3 = ch2.ToString();
          input = str2 + str3;
          if (input == "<br>")
          {
            ++num4;
            num3 = 0;
          }
          if (input == "<li>")
          {
            num4 += 2;
            num3 = 0;
            flag2 = false;
          }
          if (Regex.IsMatch(input, "^\\<p"))
          {
            num4 += 2;
            num3 = 0;
            string str4 = "<p class=\"question\">";
            flag2 = input == str4;
          }
          if (input == "</tr>")
          {
            ++num4;
            num3 = 0;
            flag2 = false;
          }
          if (Regex.IsMatch(input, "^\\<img"))
          {
            num4 += 10;
            num3 = 0;
            flag2 = false;
            if (num4 >= num1)
            {
              str1 = str1 + "<br><br><br><br><br><br><br>";
              num4 = 1;
              num3 = 0 + 1;
            }
          }
          if (Regex.IsMatch(input, "^\\<img class="))
          {
            num3 = 0;
            flag2 = false;
          }
          if (input == "<script>")
            return str1 + this.additionaContent.Substring(startIndex);
        }
        else if (flag1)
        {
          string str2 = input;
          ch2 = this.additionaContent[startIndex];
          string str3 = ch2.ToString();
          input = str2 + str3;
          str1 = str1 + (object) this.additionaContent[startIndex];
        }
        else if (num4 == num1)
        {
          string str2 = str1 + "<br><br><br><br><br><br><br>";
          num4 = 1;
          int num5 = 0;
          str1 = str2 + (object) this.additionaContent[startIndex];
          num3 = num5 + 1;
        }
        else
        {
          str1 = str1 + (object) this.additionaContent[startIndex];
          ++num3;
          if (num3 == num2)
          {
            if ((int) this.additionaContent[startIndex] != 32)
            {
              for (++startIndex; (int) this.additionaContent[startIndex] != 32 && !flag1 && (int) this.additionaContent[startIndex + 1] != 60; ++startIndex)
              {
                if ((int) this.additionaContent[startIndex] == 60)
                  flag1 = true;
                if ((int) this.additionaContent[startIndex] == 62)
                  flag1 = false;
                str1 = str1 + (object) this.additionaContent[startIndex];
              }
              str1 = str1 + (object) this.additionaContent[startIndex] + "<br>";
              ++num4;
              num3 = 0;
              if (flag2)
              {
                str1 = str1 + "&#8194 &#8194 &#8194 ";
                num3 = 5;
              }
              if (num4 == num1)
              {
                str1 = str1 + "<br><br><br><br><br><br><br>";
                num4 = 1;
                num3 = 0;
              }
            }
            else
            {
              str1 = str1 + "<br>";
              ++num4;
              num3 = 0;
              if (flag2)
              {
                str1 = str1 + "&#8194 &#8194 &#8194 ";
                num3 = 5;
              }
              if (num4 == num1)
              {
                str1 = str1 + "<br><br><br><br><br><br><br>";
                num4 = 1;
                num3 = 0;
              }
            }
          }
        }
      }
      return str1;
    }

    public string getPage()
    {
      try
      {
        if (!Directory.Exists(this.path + "\\tmp"))
        {
          try
          {
            Directory.CreateDirectory(this.path + "\\tmp");
          }
          catch (ArgumentNullException ex)
          {
            int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"tmp\"", "Error");
          }
          catch (ArgumentException ex)
          {
            int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"tmp\"", "Error");
          }
          catch (UnauthorizedAccessException ex)
          {
            int num = (int) MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
          }
          catch (PathTooLongException ex)
          {
            int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"tmp\"", "Error");
          }
          catch (DirectoryNotFoundException ex)
          {
            int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"tmp\"", "Error");
          }
          catch (NotSupportedException ex)
          {
            int num = (int) MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"tmp\"", "Error");
          }
          catch (IOException ex)
          {
            int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"tmp\"", "Error");
          }
        }
        if (!Directory.Exists(this.path + "\\tmp\\printTmp"))
        {
          try
          {
            Directory.CreateDirectory(this.path + "\\tmp\\printTmp");
          }
          catch (ArgumentNullException ex)
          {
            int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"printTmp\"", "Error");
          }
          catch (ArgumentException ex)
          {
            int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"printTmp\"", "Error");
          }
          catch (UnauthorizedAccessException ex)
          {
            int num = (int) MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
          }
          catch (PathTooLongException ex)
          {
            int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"printTmp\"", "Error");
          }
          catch (DirectoryNotFoundException ex)
          {
            int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"printTmp\"", "Error");
          }
          catch (NotSupportedException ex)
          {
            int num = (int) MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"printTmp\"", "Error");
          }
          catch (IOException ex)
          {
            int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"printTmp\"", "Error");
          }
        }
        if (!Directory.Exists(this.path + "\\tmp\\printTmp\\css"))
        {
          try
          {
            Directory.CreateDirectory(this.path + "\\tmp\\printTmp\\css");
          }
          catch (ArgumentNullException ex)
          {
            int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"css\"", "Error");
          }
          catch (ArgumentException ex)
          {
            int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"css\"", "Error");
          }
          catch (UnauthorizedAccessException ex)
          {
            int num = (int) MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
          }
          catch (PathTooLongException ex)
          {
            int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"css\"", "Error");
          }
          catch (DirectoryNotFoundException ex)
          {
            int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"css\"", "Error");
          }
          catch (NotSupportedException ex)
          {
            int num = (int) MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"css\"", "Error");
          }
          catch (IOException ex)
          {
            int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"css\"", "Error");
          }
        }
        string path = this.path + "\\tmp\\printTmp\\print.html";
        StreamWriter streamWriter = new StreamWriter(path, false);
        if (this.isSigned)
        {
          string str1 = "<img class=\"divFooter\" src=\"" + this.path + "\\tmp\\printTmp\\code.png\">";
          string str2 = "<script>jQuery( document ).ready(function(){ var i=0; jQuery('div.break').each(function(){ i++; var p = jQuery(this).position();  var x = (1114*i) - p.top;   jQuery(this).css('margin-bottom', x);});}); </script>";
          streamWriter.Write(this.header + this.body + str1 + str2 + this.footer);
        }
        else
        {
          string str = "<script> jQuery( document ).ready(function(){ var i=0; jQuery('#breakTab').each(function(){ i++; var p = jQuery(this).position();  var x = (950*i) - p.top;   jQuery(this).css('margin-bottom', x);});  jQuery('#breakTab1').each(function(){ i++; var p = jQuery(this).position();  var x = (950*i) - p.top;   jQuery(this).css('margin-bottom', x);}); jQuery('#breakTab3').each(function(){ i++; var p = jQuery(this).position();  var x = (950*i) - p.top;   jQuery(this).css('margin-bottom', x);}); jQuery('#breakTab2').each(function(){ i++; var p = jQuery(this).position();  var x = (950*i) - p.top;   jQuery(this).css('margin-bottom', x);});});  </script>";
          streamWriter.Write(this.header + this.body + str + this.footer);
        }
        streamWriter.Close();
        return path;
      }
      catch (UnauthorizedAccessException ex)
      {
        int num = (int) MessageBox.Show("Nie mozna przygotowac protokołu do druku. otwórz aplikacje jako Administrator", "Uwaga");
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Nie mozna przygotowac protokołu do druku. Orginal exception: " + ex.Message, "Error");
      }
      return "";
    }

    public string getPreView()
    {
      string path = this.path + "\\tmp\\printTmp\\preview.html";
      StreamWriter streamWriter = new StreamWriter(path, false);
      string str = "<script> jQuery( document ).ready(function(){ var i=0; jQuery('#breakTab').each(function(){ i++; var p = jQuery(this).position();  var x = (950*i) - p.top;   jQuery(this).css('margin-bottom', x);});  jQuery('#breakTab1').each(function(){ i++; var p = jQuery(this).position();  var x = (950*i) - p.top;   jQuery(this).css('margin-bottom', x);}); jQuery('#breakTab3').each(function(){ i++; var p = jQuery(this).position();  var x = (950*i) - p.top;   jQuery(this).css('margin-bottom', x);}); jQuery('#breakTab2').each(function(){ i++; var p = jQuery(this).position();  var x = (950*i) - p.top;   jQuery(this).css('margin-bottom', x);});});  </script>";
      streamWriter.Write(this.header + this.body + str + this.footer);
      streamWriter.Close();
      return path;
    }
  }
}
