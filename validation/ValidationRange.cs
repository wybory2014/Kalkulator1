// Decompiled with JetBrains decompiler
// Type: Kalkulator1.ValidationRange
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

namespace Kalkulator1
{
  internal class ValidationRange
  {
    private string fieldName;
    private int min;
    private int max;
    private bool existRange;

    public ValidationRange(string fieldName, int min, int max)
    {
      this.fieldName = fieldName;
      this.min = min;
      this.max = max;
      this.existRange = true;
    }

    public ValidationRange(string fieldName, int min)
    {
      this.fieldName = fieldName;
      this.min = min;
      this.max = int.MaxValue;
      this.existRange = true;
    }

    public ValidationRange(string fieldName)
    {
      this.fieldName = fieldName;
      this.min = 0;
      this.max = 0;
      this.existRange = false;
    }

    public int getMin()
    {
      return this.min;
    }

    public int getMax()
    {
      return this.max;
    }

    public string getFieldName()
    {
      return this.fieldName;
    }

    public bool getExistRange()
    {
      return this.existRange;
    }
  }
}
