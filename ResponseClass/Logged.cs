// Decompiled with JetBrains decompiler
// Type: Kalkulator1.ResponseClass.Logged
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Kalkulator1.ResponseClass
{
  public class Logged
  {
    private string elections;
    private string JST;
    private string rola;
    private int nr_obwodu;
    private string token;
    private string user;

    public Logged()
    {
      this.elections = "";
      this.JST = "";
      this.rola = "";
      this.nr_obwodu = -1;
      this.token = "";
      this.user = "";
    }

    public void getLoggetFromJson(string json)
    {
      try
      {
        JObject jobject = JObject.Parse(json);
        this.elections = (string) jobject.SelectToken("elections");
        int num;
        try
        {
          num = (int) jobject.SelectToken("JST");
        }
        catch (Exception ex)
        {
          num = Convert.ToInt32((string) jobject.SelectToken("JST"));
        }
        this.JST = num.ToString();
        this.rola = (string) jobject.SelectToken("rola");
        this.nr_obwodu = (int) jobject.SelectToken("nr_obwodu");
        this.token = (string) jobject.SelectToken("token");
        try
        {
          this.user = (string) jobject.SelectToken("user");
        }
        catch (JsonReaderException ex)
        {
          this.user = "";
        }
        catch (ArgumentNullException ex)
        {
          this.user = "";
        }
      }
      catch (JsonReaderException ex)
      {
      }
      catch (ArgumentNullException ex)
      {
        this.elections = "";
        this.JST = "";
        this.rola = "";
        this.nr_obwodu = -1;
        this.token = "";
      }
    }

    public string getToken()
    {
      return this.token;
    }

    public string getUser()
    {
      return this.user;
    }

    public string getElections()
    {
      return this.elections;
    }

    public string getJns()
    {
      return this.JST;
    }

    public string getRola()
    {
      return this.rola;
    }

    public int getObwodu()
    {
      return this.nr_obwodu;
    }
  }
}
