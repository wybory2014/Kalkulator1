// Decompiled with JetBrains decompiler
// Type: Kalkulator1.ResponseClass.IsLicense
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Kalkulator1.ResponseClass
{
  public class IsLicense
  {
    private Code c;
    private string crt;

    public IsLicense()
    {
      this.c = new Code();
      this.crt = "";
    }

    public void getFromJson(string json)
    {
      try
      {
        JObject jobject = JObject.Parse(json);
        this.crt = (string) jobject.SelectToken("crt");
        try
        {
          this.c = new Code((int) jobject.SelectToken("code"));
        }
        catch (JsonReaderException ex)
        {
          if (this.crt != null && this.crt != "")
            this.c = new Code(0);
          else
            this.c = new Code(3);
        }
        catch (ArgumentNullException ex)
        {
          if (this.crt != null && this.crt != "")
            this.c = new Code(0);
          else
            this.c = new Code(3);
        }
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

    public string getCrt()
    {
      return this.crt;
    }
  }
}
