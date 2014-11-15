// Decompiled with JetBrains decompiler
// Type: Kalkulator1.AdditionalClass.AttendanceItem
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

namespace Kalkulator1.AdditionalClass
{
  internal class AttendanceItem
  {
    private string id;
    private string name;

    public string ShortName
    {
      get
      {
        return this.id;
      }
    }

    public string LongName
    {
      get
      {
        return this.name;
      }
    }

    public AttendanceItem()
    {
      this.id = "0";
      this.name = "";
    }

    public AttendanceItem(string id, string name)
    {
      this.id = id;
      this.name = name;
    }

    public string getId()
    {
      return this.id;
    }

    public string getName()
    {
      return this.name;
    }
  }
}
