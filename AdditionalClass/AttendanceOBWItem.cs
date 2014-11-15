// Decompiled with JetBrains decompiler
// Type: Kalkulator1.AdditionalClass.AttendanceOBWItem
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

namespace Kalkulator1.AdditionalClass
{
  internal class AttendanceOBWItem
  {
    private int id;
    private string name;
    private long lwyb;

    public string ShortName
    {
      get
      {
        return this.id.ToString();
      }
    }

    public string LongName
    {
      get
      {
        return this.name;
      }
    }

    public AttendanceOBWItem()
    {
      this.id = 0;
      this.name = "";
      this.lwyb = 0L;
    }

    public AttendanceOBWItem(int id, string name, long lwyb)
    {
      this.id = id;
      this.name = name;
      this.lwyb = lwyb;
    }

    public int getId()
    {
      return this.id;
    }

    public string getName()
    {
      return this.name;
    }

    public long getLwyb()
    {
      return this.lwyb;
    }
  }
}
