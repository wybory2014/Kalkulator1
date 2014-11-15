// Decompiled with JetBrains decompiler
// Type: Kalkulator1.ResponseClass.ResponseData
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Kalkulator1.ResponseClass
{
  public class ResponseData
  {
    private Code c;
    private string xml;

    public ResponseData()
    {
      this.c = new Code();
      this.xml = "";
    }

    public void getFromJson(string json)
    {
      try
      {
        JObject jobject = JObject.Parse(json);
        this.c = new Code((int) jobject.SelectToken("code"));
        this.xml = (string) jobject.SelectToken("xml");
      }
      catch (JsonReaderException ex)
      {
        this.c = new Code(3);
      }
      catch (ArgumentNullException ex)
      {
      }
    }

    public void setXml(string xml)
    {
      try
      {
        this.c = new Code(0);
        this.xml = xml;
      }
      catch (JsonReaderException ex)
      {
        this.c = new Code(3);
      }
      catch (ArgumentNullException ex)
      {
      }
    }

    public Code getCode()
    {
      return this.c;
    }

    public string getXml()
    {
      return this.xml;
    }
  }
}
