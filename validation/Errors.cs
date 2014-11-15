// Decompiled with JetBrains decompiler
// Type: Kalkulator1.validation.Errors
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

using System.Collections.Generic;

namespace Kalkulator1.validation
{
  public class Errors
  {
    private List<string> hardErrors;
    private List<string> hardWarning;
    private List<string> SoftErrors;

    public Errors()
    {
      this.hardErrors = new List<string>();
      this.hardWarning = new List<string>();
      this.SoftErrors = new List<string>();
    }

    public void addHardError(string error)
    {
      this.hardErrors.Add(error);
    }

    public void addHardWarning(string error)
    {
      this.hardWarning.Add(error);
    }

    public void addSoftError(string error)
    {
      this.SoftErrors.Add(error);
    }

    public string getHardErrorXml()
    {
      string str = "";
      for (int index = 0; index < this.hardErrors.Count; ++index)
      {
        if (this.hardErrors[index] != "")
          str = str + "<code>" + this.hardErrors[index] + "</code>";
      }
      return str;
    }

    public string getHardWarningXml()
    {
      string str = "";
      for (int index = 0; index < this.hardWarning.Count; ++index)
      {
        if (this.hardWarning[index] != "")
          str = str + "<code>" + this.hardWarning[index] + "</code>";
      }
      return str;
    }

    public string getSoftErrorXml()
    {
      string str = "";
      for (int index = 0; index < this.SoftErrors.Count; ++index)
      {
        if (this.SoftErrors[index] != "")
          str = str + "<code>" + this.SoftErrors[index] + "</code>";
      }
      return str;
    }

    public int getCountHardWarning()
    {
      return this.hardWarning.Count;
    }

    public string getItemHardWarning(int index)
    {
      return this.hardWarning[index];
    }

    public void addValueInHardError(string value)
    {
      for (int index = 0; index < this.hardErrors.Count; ++index)
      {
        if (this.hardErrors[index] == value)
          return;
      }
      this.hardErrors.Add(value);
    }

    public void addValueInHardWarning(string value)
    {
      for (int index = 0; index < this.hardWarning.Count; ++index)
      {
        if (this.hardWarning[index] == value)
          return;
      }
      this.hardWarning.Add(value);
    }

    public void addValueInSoftError(string value)
    {
      for (int index = 0; index < this.SoftErrors.Count; ++index)
      {
        if (this.SoftErrors[index] == value)
          return;
      }
      this.SoftErrors.Add(value);
    }
  }
}
