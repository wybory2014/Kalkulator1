// Decompiled with JetBrains decompiler
// Type: Kalkulator1.validation.KBWErrorProvider
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Kalkulator1.validation
{
  public class KBWErrorProvider
  {
    private Dictionary<string, Dictionary<string, KBWValue>> errors;
    private Dictionary<string, Dictionary<string, KBWValue>> warning;
    private Dictionary<string, Dictionary<string, KBWValue>> hardWarning;

    public KBWErrorProvider()
    {
      this.errors = new Dictionary<string, Dictionary<string, KBWValue>>();
      this.warning = new Dictionary<string, Dictionary<string, KBWValue>>();
      this.hardWarning = new Dictionary<string, Dictionary<string, KBWValue>>();
    }

    public bool hasErrors()
    {
      return Enumerable.Count<KeyValuePair<string, Dictionary<string, KBWValue>>>((IEnumerable<KeyValuePair<string, Dictionary<string, KBWValue>>>) this.errors) > 0;
    }

    public bool hasError(Control c)
    {
      return this.errors.ContainsKey(c.Name) && this.errors[c.Name].Count > 0 || this.warning.ContainsKey(c.Name) && this.warning[c.Name].Count > 0 || this.hardWarning.ContainsKey(c.Name) && this.hardWarning[c.Name].Count > 0;
    }

    public bool hasHardWarning()
    {
      return Enumerable.Count<KeyValuePair<string, Dictionary<string, KBWValue>>>((IEnumerable<KeyValuePair<string, Dictionary<string, KBWValue>>>) this.hardWarning) > 0;
    }

    public bool hasWarning()
    {
      return Enumerable.Count<KeyValuePair<string, Dictionary<string, KBWValue>>>((IEnumerable<KeyValuePair<string, Dictionary<string, KBWValue>>>) this.warning) > 0;
    }

    public void clearErrors()
    {
      this.errors.Clear();
      this.warning.Clear();
      this.hardWarning.Clear();
    }

    public Dictionary<string, Dictionary<string, KBWValue>> getErrors()
    {
      return this.errors;
    }

    public Dictionary<string, Dictionary<string, KBWValue>> getHardWarnings()
    {
      return this.hardWarning;
    }

    public Dictionary<string, Dictionary<string, KBWValue>> getWarnings()
    {
      return this.warning;
    }

    public void SetErrorWithCount(Control c, string message, string code, string step)
    {
      if (message == "")
      {
        try
        {
          this.errors[c.Name].Remove(code);
          if (this.errors[c.Name].Count != 0)
            return;
          this.errors.Remove(c.Name);
        }
        catch (KeyNotFoundException ex)
        {
        }
      }
      else
      {
        try
        {
          this.errors[c.Name][code] = new KBWValue(message, step);
        }
        catch (KeyNotFoundException ex1)
        {
          try
          {
            this.errors[c.Name].Add(code, new KBWValue(message, step));
          }
          catch (KeyNotFoundException ex2)
          {
            this.errors.Add(c.Name, new Dictionary<string, KBWValue>());
            this.errors[c.Name].Add(code, new KBWValue(message, step));
          }
        }
      }
    }

    public void SetErrorWithCount(Control c, string message, int type, string code, string step)
    {
      if (message == "")
      {
        try
        {
          if (type == 0 || type == 1)
          {
            this.errors[c.Name].Remove(code);
            if (this.errors[c.Name].Count == 0)
              this.errors.Remove(c.Name);
          }
          if (type == 2)
          {
            this.hardWarning[c.Name].Remove(code);
            if (this.hardWarning[c.Name].Count == 0)
              this.hardWarning.Remove(c.Name);
          }
          if (type != 3)
            return;
          this.warning[c.Name].Remove(code);
          if (this.warning[c.Name].Count == 0)
            this.warning.Remove(c.Name);
        }
        catch (KeyNotFoundException ex)
        {
        }
      }
      else
      {
        try
        {
          if (type == 0 || type == 1)
            this.errors[c.Name][code] = new KBWValue(message, step);
          if (type == 2)
            this.hardWarning[c.Name][code] = new KBWValue(message, step);
          if (type == 3)
            this.warning[c.Name][code] = new KBWValue(message, step);
        }
        catch (KeyNotFoundException ex1)
        {
          try
          {
            if (type == 0 || type == 1)
              this.errors[c.Name].Add(code, new KBWValue(message, step));
            if (type == 2)
              this.hardWarning[c.Name].Add(code, new KBWValue(message, step));
            if (type == 3)
              this.warning[c.Name].Add(code, new KBWValue(message, step));
          }
          catch (KeyNotFoundException ex2)
          {
            if (type == 0 || type == 1)
            {
              this.errors.Add(c.Name, new Dictionary<string, KBWValue>());
              this.errors[c.Name].Add(code, new KBWValue(message, step));
            }
            if (type == 2)
            {
              this.hardWarning.Add(c.Name, new Dictionary<string, KBWValue>());
              this.hardWarning[c.Name].Add(code, new KBWValue(message, step));
            }
            if (type == 3)
            {
              this.warning.Add(c.Name, new Dictionary<string, KBWValue>());
              this.warning[c.Name].Add(code, new KBWValue(message, step));
            }
          }
        }
      }
    }
  }
}
