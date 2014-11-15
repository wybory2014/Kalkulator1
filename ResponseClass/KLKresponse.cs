// Decompiled with JetBrains decompiler
// Type: Kalkulator1.ResponseClass.KLKresponse
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

using System;

namespace Kalkulator1.ResponseClass
{
  public class KLKresponse
  {
    private bool saved;
    private Code c;
    private Exception e;

    public KLKresponse()
    {
      this.saved = false;
      this.c = new Code();
      this.e = new Exception();
    }

    public void setCode(Code code)
    {
      this.c = code;
    }

    public void setSaved(bool saved)
    {
      this.saved = saved;
    }

    public void setException(Exception e)
    {
      this.e = e;
    }

    public bool isSaved()
    {
      if (this.c.getcode() == 0)
        return this.saved;
      else
        return false;
    }

    public Code getCode()
    {
      return this.c;
    }

    public Exception getException()
    {
      return this.e;
    }
  }
}
