// Decompiled with JetBrains decompiler
// Type: Kalkulator1.ErrorProviderExtensions
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

using System.Collections.Generic;
using System.Windows.Forms;

namespace Kalkulator1
{
  public static class ErrorProviderExtensions
  {
    private static int count;
    private static int countWarning;
    private static int countHardWarning;
    private static Dictionary<string, string[]> errors;
    private static Dictionary<string, string[]> warning;
    private static Dictionary<string, string[]> hardWarning;

    public static void SetErrorWithCount(this ErrorProvider ep, Control c, string message, string code)
    {
      if (ErrorProviderExtensions.errors == null)
        ErrorProviderExtensions.errors = new Dictionary<string, string[]>();
      if (ErrorProviderExtensions.warning == null)
        ErrorProviderExtensions.warning = new Dictionary<string, string[]>();
      if (ErrorProviderExtensions.hardWarning == null)
        ErrorProviderExtensions.hardWarning = new Dictionary<string, string[]>();
      if (message == "")
      {
        try
        {
          if (!(ErrorProviderExtensions.errors[c.Name][0] != "NULL") || !(ErrorProviderExtensions.errors[c.Name][0] == code) || !(ErrorProviderExtensions.errors[c.Name][1] != ""))
            return;
          ErrorProviderExtensions.errors[c.Name][0] = "NULL";
          ErrorProviderExtensions.errors[c.Name][1] = message;
          --ErrorProviderExtensions.count;
          ep.SetError(c, message);
        }
        catch (KeyNotFoundException ex)
        {
          ErrorProviderExtensions.errors.Add(c.Name, new string[2]
          {
            "NULL",
            message
          });
          ep.SetError(c, message);
        }
      }
      else
      {
        try
        {
          if (ErrorProviderExtensions.errors[c.Name][1] == "")
          {
            ErrorProviderExtensions.errors[c.Name][0] = code;
            ErrorProviderExtensions.errors[c.Name][1] = message;
            ++ErrorProviderExtensions.count;
            ep.SetError(c, message);
          }
        }
        catch (KeyNotFoundException ex)
        {
          ErrorProviderExtensions.errors.Add(c.Name, new string[2]
          {
            code,
            message
          });
          ++ErrorProviderExtensions.count;
          ep.SetError(c, message);
        }
      }
    }

    public static void SetErrorWithCount(this ErrorProvider ep, Control c, string message, int type, string code)
    {
      if (ErrorProviderExtensions.errors == null)
        ErrorProviderExtensions.errors = new Dictionary<string, string[]>();
      if (ErrorProviderExtensions.warning == null)
        ErrorProviderExtensions.warning = new Dictionary<string, string[]>();
      if (ErrorProviderExtensions.hardWarning == null)
        ErrorProviderExtensions.hardWarning = new Dictionary<string, string[]>();
      if (message == "")
      {
        try
        {
          if ((type == 0 || type == 1) && (ErrorProviderExtensions.errors[c.Name][0] != "NULL" && ErrorProviderExtensions.errors[c.Name][0] == code && ErrorProviderExtensions.errors[c.Name][1] != ""))
          {
            ErrorProviderExtensions.errors[c.Name][0] = "NULL";
            ErrorProviderExtensions.errors[c.Name][1] = message;
            --ErrorProviderExtensions.count;
            ep.SetError(c, message);
          }
          if (type == 2 && (ErrorProviderExtensions.hardWarning[c.Name][0] != "NULL" && ErrorProviderExtensions.hardWarning[c.Name][0] == code && ErrorProviderExtensions.hardWarning[c.Name][1] != ""))
          {
            ErrorProviderExtensions.hardWarning[c.Name][0] = "NULL";
            ErrorProviderExtensions.hardWarning[c.Name][1] = message;
            --ErrorProviderExtensions.countHardWarning;
            ep.SetError(c, message);
          }
          if (type != 3 || (!(ErrorProviderExtensions.warning[c.Name][0] != "NULL") || !(ErrorProviderExtensions.warning[c.Name][0] == code) || !(ErrorProviderExtensions.warning[c.Name][1] != "")))
            return;
          ErrorProviderExtensions.warning[c.Name][0] = "NULL";
          ErrorProviderExtensions.warning[c.Name][1] = message;
          --ErrorProviderExtensions.countWarning;
          ep.SetError(c, message);
        }
        catch (KeyNotFoundException ex)
        {
          if (type == 0 || type == 1)
          {
            ErrorProviderExtensions.errors.Add(c.Name, new string[2]
            {
              "NULL",
              message
            });
            ep.SetError(c, message);
          }
          if (type == 2)
          {
            ErrorProviderExtensions.hardWarning.Add(c.Name, new string[2]
            {
              "NULL",
              message
            });
            ep.SetError(c, message);
          }
          if (type != 3)
            return;
          ErrorProviderExtensions.warning.Add(c.Name, new string[2]
          {
            "NULL",
            message
          });
          ep.SetError(c, message);
        }
      }
      else
      {
        try
        {
          if (ep.GetError(c) == "")
          {
            if ((type == 0 || type == 1) && (ErrorProviderExtensions.errors[c.Name][0] != "NULL" && ErrorProviderExtensions.errors[c.Name][0] == code && ErrorProviderExtensions.errors[c.Name][1] == ""))
            {
              ErrorProviderExtensions.errors[c.Name][0] = code;
              ErrorProviderExtensions.errors[c.Name][1] = message;
              ++ErrorProviderExtensions.count;
              ep.SetError(c, message);
            }
            if (type == 2 && (ErrorProviderExtensions.hardWarning[c.Name][0] != "NULL" && ErrorProviderExtensions.hardWarning[c.Name][0] == code && ErrorProviderExtensions.hardWarning[c.Name][1] == ""))
            {
              ErrorProviderExtensions.hardWarning[c.Name][0] = code;
              ErrorProviderExtensions.hardWarning[c.Name][1] = message;
              ++ErrorProviderExtensions.countHardWarning;
              ep.SetError(c, message);
            }
            if (type == 3 && (ErrorProviderExtensions.warning[c.Name][0] != "NULL" && ErrorProviderExtensions.warning[c.Name][0] == code && ErrorProviderExtensions.warning[c.Name][1] == ""))
            {
              ErrorProviderExtensions.warning[c.Name][0] = code;
              ErrorProviderExtensions.warning[c.Name][1] = message;
              ++ErrorProviderExtensions.countWarning;
              ep.SetError(c, message);
            }
          }
        }
        catch (KeyNotFoundException ex)
        {
          if (type == 0 || type == 1)
          {
            ErrorProviderExtensions.errors.Add(c.Name, new string[2]
            {
              code,
              message
            });
            ++ErrorProviderExtensions.count;
            ep.SetError(c, message);
          }
          if (type == 2)
          {
            ErrorProviderExtensions.hardWarning.Add(c.Name, new string[2]
            {
              code,
              message
            });
            ++ErrorProviderExtensions.countHardWarning;
            ep.SetError(c, message);
          }
          if (type == 3)
          {
            ErrorProviderExtensions.warning.Add(c.Name, new string[2]
            {
              code,
              message
            });
            ++ErrorProviderExtensions.countWarning;
            ep.SetError(c, message);
          }
        }
      }
    }

    public static Dictionary<string, string[]> getErrors(this ErrorProvider ep)
    {
      return ErrorProviderExtensions.errors;
    }

    public static Dictionary<string, string[]> getHardWarnings(this ErrorProvider ep)
    {
      return ErrorProviderExtensions.hardWarning;
    }

    public static Dictionary<string, string[]> getWarnings(this ErrorProvider ep)
    {
      return ErrorProviderExtensions.warning;
    }

    public static bool HasErrors(this ErrorProvider ep)
    {
      return ErrorProviderExtensions.count != 0;
    }

    public static int GetErrorCount(this ErrorProvider ep)
    {
      return ErrorProviderExtensions.count;
    }

    public static int GetHardWarningCount(this ErrorProvider ep)
    {
      return ErrorProviderExtensions.countHardWarning;
    }

    public static int GetWarningCount(this ErrorProvider ep)
    {
      return ErrorProviderExtensions.countWarning;
    }

    public static void clearErrors(this ErrorProvider ep)
    {
      ErrorProviderExtensions.count = 0;
      ErrorProviderExtensions.countWarning = 0;
      ErrorProviderExtensions.countHardWarning = 0;
      ErrorProviderExtensions.errors = new Dictionary<string, string[]>();
      ErrorProviderExtensions.warning = new Dictionary<string, string[]>();
      ErrorProviderExtensions.hardWarning = new Dictionary<string, string[]>();
      ep.Clear();
    }
  }
}
