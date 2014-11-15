// Decompiled with JetBrains decompiler
// Type: Kalkulator1.validation.KBWValue
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

namespace Kalkulator1.validation
{
  public class KBWValue
  {
    private string message;
    private string step;

    public KBWValue()
    {
      this.message = "";
      this.step = "";
    }

    public KBWValue(string message, string step)
    {
      this.message = message;
      this.step = step;
    }

    public static bool operator ==(KBWValue c1, KBWValue c2)
    {
      return c1.getMessage() == c2.getMessage() && c1.getStep() == c2.getStep();
    }

    public static bool operator !=(KBWValue c1, KBWValue c2)
    {
      return !(c1.getMessage() == c2.getMessage()) || !(c1.getStep() == c2.getStep());
    }

    public string getMessage()
    {
      return this.message;
    }

    public string getStep()
    {
      return this.step;
    }
  }
}
