// Decompiled with JetBrains decompiler
// Type: Kalkulator1.ValidationPatern
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace Kalkulator1
{
  internal class ValidationPatern
  {
    private ErrorType type;
    private string validationFor;
    private string paternLeft;
    private string paternRight;
    private string note;
    private string id;
    private Operation operation;
    private List<string[]> fields;
    private List<string> variable;

    public ValidationPatern()
    {
      this.validationFor = "";
      this.type = ErrorType.Null;
      this.paternLeft = "";
      this.paternRight = "";
      this.note = "";
      this.operation = Operation.Null;
      this.fields = new List<string[]>();
      this.variable = new List<string>();
      this.id = "";
    }

    public ValidationPatern(string validationFor, string paternLeft, string paternRight, string note, string id, Operation operation, ErrorType type)
    {
      this.validationFor = validationFor;
      this.type = type;
      this.paternLeft = paternLeft;
      this.paternRight = paternRight;
      this.note = note;
      this.operation = operation;
      this.fields = new List<string[]>();
      this.variable = new List<string>();
      this.id = id;
    }

    public void SetValidationPatern(string validationFor, string paternLeft, string paternRight, string note, string id, Operation operation, ErrorType type)
    {
      this.validationFor = validationFor;
      this.type = type;
      this.paternLeft = paternLeft;
      this.paternRight = paternRight;
      this.note = note;
      this.operation = operation;
      this.id = id;
    }

    public int getFieldsCount()
    {
      return this.fields.Count;
    }

    public string getNameField(int index)
    {
      return this.fields[index][0];
    }

    public string getFromField(int index)
    {
      return this.fields[index][1];
    }

    public string getNote()
    {
      return this.note;
    }

    public ErrorType getErrorType()
    {
      return this.type;
    }

    public string getId()
    {
      return this.id;
    }

    public bool containVariables(int index)
    {
      return this.fields[index].Length > 2;
    }

    public void addField(string fieldName, string from)
    {
      this.fields.Add(new string[2]
      {
        fieldName,
        from
      });
    }

    public void addField(string fieldName, string from, bool forVariable)
    {
      if (forVariable)
        this.fields.Add(new string[3]
        {
          fieldName,
          from,
          "V"
        });
      else
        this.fields.Add(new string[2]
        {
          fieldName,
          from
        });
    }

    public void addVariable(string name)
    {
      this.variable.Add(name);
    }

    private void addField(string[] field)
    {
      this.fields.Add(field);
    }

    public bool valid(List<string[]> fieldsValues)
    {
      bool flag = true;
      string str1 = this.paternLeft;
      string str2 = this.paternRight;
      string left = str1.Replace(" and ", " AND ");
      string right = str2.Replace(" and ", " AND ");
      if (this.variable.Count == 0)
      {
        for (int index = 0; index < fieldsValues.Count; ++index)
        {
          left = left.Replace(fieldsValues[index][0], fieldsValues[index][1]);
          right = right.Replace(fieldsValues[index][0], fieldsValues[index][1]);
        }
        return this.check(left, right);
      }
      else
      {
        if (this.variable.Count == 1)
        {
          List<string> list = new List<string>();
          for (int index = 0; index < fieldsValues.Count; ++index)
          {
            if (this.fields[index].Length > 2)
            {
              list.Add(fieldsValues[index][1]);
            }
            else
            {
              left = left.Replace(fieldsValues[index][0], fieldsValues[index][1]);
              right = right.Replace(fieldsValues[index][0], fieldsValues[index][1]);
            }
          }
          string str3 = left;
          string str4 = right;
          if (Regex.IsMatch(str3, "sum") || Regex.IsMatch(str4, "sum"))
          {
            Match match1 = Regex.Match(str3, "sum\\(.+\\)");
            Match match2 = Regex.Match(str4, "sum\\(.+\\)");
            if (match2.Success && match2.Value.Replace("(", "").Replace(")", "").Replace("sum", "") == this.variable[0])
            {
              int num = 0;
              for (int index = 0; index < list.Count; ++index)
              {
                try
                {
                  num += Convert.ToInt32(list[index]);
                }
                catch (FormatException ex)
                {
                }
                catch (OverflowException ex)
                {
                }
              }
              str4 = str4.Replace(match2.Value, num.ToString());
            }
            if (match1.Success && match1.Value.Replace("(", "").Replace(")", "").Replace("sum", "") == this.variable[0])
            {
              int num = 0;
              for (int index = 0; index < list.Count; ++index)
              {
                try
                {
                  num += Convert.ToInt32(list[index]);
                }
                catch (FormatException ex)
                {
                }
                catch (OverflowException ex)
                {
                }
              }
              str3 = str3.Replace(match1.Value, num.ToString());
            }
            return this.check(str3, str4);
          }
          else
          {
            for (int index = 0; index < list.Count; ++index)
            {
              string str5 = left;
              string str6 = right;
              if (!this.check(str5.Replace(this.variable[0], list[index]), str6.Replace(this.variable[0], list[index])))
                return false;
            }
          }
        }
        if (this.variable.Count == 2 && this.fields.Count == 3)
        {
          try
          {
            string str3 = this.variable[0].Substring(0, this.variable[0].Length - 1);
            string str4 = this.variable[1].Substring(0, this.variable[1].Length - 1);
            for (int index = 0; index < fieldsValues.Count; ++index)
            {
              Match match1 = Regex.Match(fieldsValues[index][0], "^" + str3 + ".$");
              if (match1.Success)
              {
                left = left.Replace(this.variable[0], fieldsValues[index][1]);
                right = right.Replace(this.variable[0], fieldsValues[index][1]);
              }
              Match match2 = Regex.Match(fieldsValues[index][0], "^" + str4 + ".$");
              if (match2.Success)
              {
                left = left.Replace(this.variable[1], fieldsValues[index][1]);
                right = right.Replace(this.variable[1], fieldsValues[index][1]);
              }
              if (!match1.Success && !match2.Success)
              {
                left = left.Replace(fieldsValues[index][0], fieldsValues[index][1]);
                right = right.Replace(fieldsValues[index][0], fieldsValues[index][1]);
              }
            }
          }
          catch (Exception ex)
          {
          }
          return this.check(left, right);
        }
        else
        {
          if (this.variable.Count == 2)
          {
            List<string> list1 = new List<string>();
            List<List<string>> list2 = new List<List<string>>();
            string[] strArray1 = this.paternLeft.Split('_');
            string str3 = strArray1[0].Replace("sumByY(", "");
            string[] strArray2 = this.paternRight.Split('_');
            string str4 = strArray2[0].Replace("sumByY(", "");
            for (int index1 = 0; index1 < fieldsValues.Count; ++index1)
            {
              if (this.fields[index1].Length > 2)
              {
                if (Regex.Match(this.fields[index1][0], str3 + ".+").Success)
                {
                  if (strArray1.Length > 2)
                  {
                    string[] strArray3 = this.fields[index1][0].Split('_');
                    if (strArray3[1].Length > 1)
                    {
                      try
                      {
                        int index2 = Convert.ToInt32(strArray3[1].Substring(1)) - 1;
                        if (list2.Count < index2 + 1)
                        {
                          while (list2.Count < index2 + 1)
                            list2.Add(new List<string>());
                        }
                        list2[index2].Add(fieldsValues[index1][1]);
                      }
                      catch (Exception ex)
                      {
                      }
                    }
                  }
                  else
                    list1.Add(fieldsValues[index1][1]);
                }
                else if (Regex.Match(this.paternRight, str4 + ".+_").Success)
                {
                  if (strArray2.Length > 2)
                  {
                    string[] strArray3 = this.fields[index1][0].Split('_');
                    if (strArray3[1].Length > 1)
                    {
                      try
                      {
                        int index2 = Convert.ToInt32(strArray3[1].Substring(1)) - 1;
                        if (list2.Count < index2 + 1)
                        {
                          while (list2.Count < index2 + 1)
                            list2.Add(new List<string>());
                        }
                        list2[index2].Add(fieldsValues[index1][1]);
                      }
                      catch (Exception ex)
                      {
                      }
                    }
                  }
                  else
                    list1.Add(fieldsValues[index1][1]);
                }
              }
              else
              {
                left = left.Replace(fieldsValues[index1][0], fieldsValues[index1][1]);
                right = right.Replace(fieldsValues[index1][0], fieldsValues[index1][1]);
              }
            }
            string input1 = left;
            string input2 = right;
            if (Regex.IsMatch(input1, "sumBy") || Regex.IsMatch(input2, "sumBy"))
            {
              List<long> list3 = new List<long>();
              Match match1 = Regex.Match(input1, "sumBy.\\(.+\\)");
              Match match2 = Regex.Match(input2, "sumBy.\\(.+\\)");
              char ch;
              if (match2.Success)
              {
                ch = match2.Value.Replace("sumBy", "")[0];
                if (match2.Value.Replace("(", "").Replace(")", "").Replace("sumBy" + ch.ToString(), "") == this.variable[1] && strArray2.Length > 2)
                {
                  for (int index1 = 0; index1 < list2.Count; ++index1)
                  {
                    while (list3.Count < list2.Count)
                      list3.Add(0L);
                    for (int index2 = 0; index2 < list2[index1].Count; ++index2)
                    {
                      List<long> list4;
                      int index3;
                      (list4 = list3)[index3 = index1] = list4[index3] + Convert.ToInt64(list2[index1][index2]);
                    }
                  }
                }
                if (Enumerable.Count<long>((IEnumerable<long>) list3) == Enumerable.Count<string>((IEnumerable<string>) list1))
                {
                  flag = true;
                  for (int index = 0; index < list1.Count; ++index)
                  {
                    if (strArray1.Length < 3 && strArray2.Length > 2)
                    {
                      string oldValue = left;
                      string str5 = right;
                      flag = this.check(oldValue.Replace(oldValue, list1[index]), str5.Replace(match2.Value, list3[index].ToString()));
                      if (!flag)
                        return flag;
                    }
                  }
                }
                else
                  flag = false;
              }
              if (match1.Success)
              {
                ch = match2.Value.Replace("sumBy", "")[0];
                if (match2.Value.Replace("(", "").Replace(")", "").Replace("sumBy" + ch.ToString(), "") == this.variable[0] && strArray1.Length > 2)
                {
                  for (int index1 = 0; index1 < list2.Count; ++index1)
                  {
                    while (list3.Count < list2.Count)
                      list3.Add(0L);
                    for (int index2 = 0; index2 < list2[index1].Count; ++index2)
                    {
                      List<long> list4;
                      int index3;
                      (list4 = list3)[index3 = index1] = list4[index3] + Convert.ToInt64(list2[index1][index2]);
                    }
                  }
                }
                if (Enumerable.Count<long>((IEnumerable<long>) list3) == Enumerable.Count<string>((IEnumerable<string>) list1))
                {
                  flag = true;
                  for (int index = 0; index < list1.Count; ++index)
                  {
                    if (strArray2.Length < 3 && strArray1.Length > 2)
                    {
                      string str5 = left;
                      string oldValue = right;
                      flag = this.check(str5.Replace(match1.Value, list3[index].ToString()), oldValue.Replace(oldValue, list1[index]));
                      if (!flag)
                        return flag;
                    }
                  }
                }
                else
                  flag = false;
              }
            }
          }
          return flag;
        }
      }
    }

    private bool check(string left, string right)
    {
      DataTable dataTable = new DataTable();
      try
      {
        int num1;
        int num2;
        if (Regex.IsMatch(left, "AND") || Regex.IsMatch(right, "AND"))
        {
          string[] strArray1 = left.Split(new string[1]
          {
            "AND"
          }, StringSplitOptions.RemoveEmptyEntries);
          string[] strArray2 = right.Split(new string[1]
          {
            "AND"
          }, StringSplitOptions.RemoveEmptyEntries);
          bool flag1 = true;
          bool flag2 = true;
          for (int index = 0; index < strArray1.Length; ++index)
          {
            strArray1[index] = strArray1[index].Replace("less=", "<=");
            strArray1[index] = strArray1[index].Replace("more=", ">=");
            if (Regex.IsMatch(strArray1[index], "!="))
            {
              string[] strArray3 = strArray1[index].Split(new string[1]
              {
                "!="
              }, StringSplitOptions.RemoveEmptyEntries);
              if (strArray3.Length == 2)
              {
                strArray3[0] = strArray3[0].Replace("(", "");
                strArray3[0] = strArray3[0].Replace(")", "");
                strArray3[1] = strArray3[1].Replace("(", "");
                strArray3[1] = strArray3[1].Replace(")", "");
                try
                {
                  if (Convert.ToInt32(strArray3[0]) == Convert.ToInt32(strArray3[1]))
                  {
                    flag1 = false;
                    break;
                  }
                }
                catch (FormatException ex)
                {
                  flag1 = false;
                  break;
                }
              }
              else
              {
                flag1 = false;
                break;
              }
            }
            else if (Regex.IsMatch(strArray1[index], "=="))
            {
              string[] strArray3 = strArray1[index].Split(new string[1]
              {
                "=="
              }, StringSplitOptions.RemoveEmptyEntries);
              if (strArray3.Length == 2)
              {
                strArray3[0] = strArray3[0].Replace("(", "");
                strArray3[0] = strArray3[0].Replace(")", "");
                strArray3[1] = strArray3[1].Replace("(", "");
                strArray3[1] = strArray3[1].Replace(")", "");
                try
                {
                  if (Convert.ToInt32(strArray3[0]) != Convert.ToInt32(strArray3[1]))
                  {
                    flag1 = false;
                    break;
                  }
                }
                catch (FormatException ex)
                {
                  flag1 = false;
                  break;
                }
              }
              else
              {
                flag1 = false;
                break;
              }
            }
            else if (!Convert.ToBoolean(dataTable.Compute(strArray1[index], "")))
            {
              flag1 = false;
              break;
            }
          }
          for (int index = 0; index < strArray2.Length; ++index)
          {
            strArray2[index] = strArray2[index].Replace("less=", "<=");
            strArray2[index] = strArray2[index].Replace("more=", ">=");
            if (Regex.IsMatch(strArray2[index], "!="))
            {
              string[] strArray3 = strArray2[index].Split(new string[1]
              {
                "!="
              }, StringSplitOptions.RemoveEmptyEntries);
              if (strArray3.Length == 2)
              {
                strArray3[0] = strArray3[0].Replace("(", "");
                strArray3[0] = strArray3[0].Replace(")", "");
                strArray3[1] = strArray3[1].Replace("(", "");
                strArray3[1] = strArray3[1].Replace(")", "");
                try
                {
                  if (Convert.ToInt32(strArray3[0]) == Convert.ToInt32(strArray3[1]))
                  {
                    flag2 = false;
                    break;
                  }
                }
                catch (FormatException ex)
                {
                  flag2 = false;
                  break;
                }
              }
              else
              {
                flag2 = false;
                break;
              }
            }
            else if (Regex.IsMatch(strArray2[index], "=="))
            {
              string[] strArray3 = strArray2[index].Split(new string[1]
              {
                "=="
              }, StringSplitOptions.RemoveEmptyEntries);
              if (strArray3.Length == 2)
              {
                strArray3[0] = strArray3[0].Replace("(", "");
                strArray3[0] = strArray3[0].Replace(")", "");
                strArray3[1] = strArray3[1].Replace("(", "");
                strArray3[1] = strArray3[1].Replace(")", "");
                try
                {
                  if (Convert.ToInt32(strArray3[0]) != Convert.ToInt32(strArray3[1]))
                  {
                    flag2 = false;
                    break;
                  }
                }
                catch (FormatException ex)
                {
                  flag2 = false;
                  break;
                }
              }
              else
              {
                flag2 = false;
                break;
              }
            }
            else if (!Convert.ToBoolean(dataTable.Compute(strArray2[index], "")))
            {
              flag2 = false;
              break;
            }
          }
          if (this.operation == Operation.Equal)
            return flag1 == flag2;
          else if (this.operation == Operation.Different)
            return flag1 != flag2;
        }
        else if (Regex.IsMatch(left, "max") || Regex.IsMatch(right, "max"))
        {
          num1 = 0;
          num2 = 0;
          if (Regex.IsMatch(left, "max"))
          {
            Match match = Regex.Match(left, "max\\(.+\\)");
            if (match.Success)
            {
              string[] strArray = match.Value.Replace("(", "").Replace(")", "").Replace("max", "").Split(',');
              if (strArray.Length != 2)
                return false;
              int num3 = Convert.ToInt32(dataTable.Compute(strArray[0], ""));
              int num4 = Convert.ToInt32(dataTable.Compute(strArray[1], ""));
              left = num3 < num4 ? left.Replace(match.Value, num4.ToString()) : left.Replace(match.Value, num3.ToString());
            }
          }
          else
            num2 = Convert.ToInt32(dataTable.Compute(left, ""));
          if (Regex.IsMatch(right, "max"))
          {
            Match match = Regex.Match(right, "max\\(.+\\)");
            if (match.Success)
            {
              string[] strArray = match.Value.Replace("(", "").Replace(")", "").Replace("max", "").Split(',');
              if (strArray.Length != 2)
                return false;
              int num3 = Convert.ToInt32(dataTable.Compute(strArray[0], ""));
              int num4 = Convert.ToInt32(dataTable.Compute(strArray[1], ""));
              right = num3 < num4 ? right.Replace(match.Value, num4.ToString()) : right.Replace(match.Value, num3.ToString());
            }
          }
          else
            num1 = Convert.ToInt32(dataTable.Compute(right, ""));
          int num5 = Convert.ToInt32(dataTable.Compute(left, ""));
          int num6 = Convert.ToInt32(dataTable.Compute(right, ""));
          if (this.operation == Operation.Equal)
            return num5 == num6;
          else if (this.operation == Operation.Different)
            return num5 != num6;
          else if (this.operation == Operation.Less)
            return num5 < num6;
          else if (this.operation == Operation.LessOrEqual)
            return num5 <= num6;
          else if (this.operation == Operation.More)
            return num5 >= num6;
          else if (this.operation == Operation.MoreOrEqual)
            return num5 >= num6;
        }
        else if (Regex.IsMatch(left, "min") || Regex.IsMatch(right, "min"))
        {
          num1 = 0;
          num2 = 0;
          if (Regex.IsMatch(left, "min"))
          {
            Match match = Regex.Match(left, "min\\(.+\\)");
            if (match.Success)
            {
              string[] strArray = match.Value.Replace("(", "").Replace(")", "").Replace("min", "").Split(',');
              if (strArray.Length != 2)
                return false;
              int num3 = Convert.ToInt32(dataTable.Compute(strArray[0], ""));
              int num4 = Convert.ToInt32(dataTable.Compute(strArray[1], ""));
              left = num3 > num4 ? left.Replace(match.Value, num4.ToString()) : left.Replace(match.Value, num3.ToString());
            }
          }
          else
            num2 = Convert.ToInt32(dataTable.Compute(left, ""));
          if (Regex.IsMatch(right, "min"))
          {
            Match match = Regex.Match(right, "min\\(.+\\)");
            if (match.Success)
            {
              string[] strArray = match.Value.Replace("(", "").Replace(")", "").Replace("min", "").Split(',');
              if (strArray.Length != 2)
                return false;
              int num3 = Convert.ToInt32(dataTable.Compute(strArray[0], ""));
              int num4 = Convert.ToInt32(dataTable.Compute(strArray[1], ""));
              right = num3 > num4 ? right.Replace(match.Value, num4.ToString()) : right.Replace(match.Value, num3.ToString());
            }
          }
          else
            num1 = Convert.ToInt32(dataTable.Compute(right, ""));
          int num5 = Convert.ToInt32(dataTable.Compute(left, ""));
          int num6 = Convert.ToInt32(dataTable.Compute(right, ""));
          if (this.operation == Operation.Equal)
            return num5 == num6;
          else if (this.operation == Operation.Different)
            return num5 != num6;
          else if (this.operation == Operation.Less)
            return num5 < num6;
          else if (this.operation == Operation.LessOrEqual)
            return num5 <= num6;
          else if (this.operation == Operation.More)
            return num5 >= num6;
          else if (this.operation == Operation.MoreOrEqual)
            return num5 >= num6;
        }
        else
        {
          if (Regex.IsMatch(left, "\\(.+\\)") || Regex.IsMatch(right, "\\(.+\\)"))
          {
            MatchCollection matchCollection1 = Regex.Matches(left, "\\(.+\\)");
            for (int index = 0; index < matchCollection1.Count; ++index)
            {
              string expression = matchCollection1[index].Value.Replace("(", "").Replace(")", "");
              double num3 = Convert.ToDouble(dataTable.Compute(expression, ""));
              left = left.Replace(matchCollection1[index].Value, num3.ToString());
            }
            left = left.Replace(",", ".");
            MatchCollection matchCollection2 = Regex.Matches(right, "\\(.+\\)");
            for (int index = 0; index < matchCollection2.Count; ++index)
            {
              string expression = matchCollection2[index].Value.Replace("(", "").Replace(")", "");
              double num3 = Convert.ToDouble(dataTable.Compute(expression, ""));
              right = right.Replace(matchCollection2[index].Value, num3.ToString());
            }
            right = right.Replace(",", ".");
          }
          int num4 = Convert.ToInt32(dataTable.Compute(left, ""));
          int num5 = Convert.ToInt32(dataTable.Compute(right, ""));
          if (this.operation == Operation.Equal)
            return num4 == num5;
          else if (this.operation == Operation.Different)
            return num4 != num5;
          else if (this.operation == Operation.Less)
            return num4 < num5;
          else if (this.operation == Operation.LessOrEqual)
            return num4 <= num5;
          else if (this.operation == Operation.More)
            return num4 >= num5;
          else if (this.operation == Operation.MoreOrEqual)
            return num4 >= num5;
        }
      }
      catch (InvalidCastException ex)
      {
        return false;
      }
      catch (EvaluateException ex)
      {
        return false;
      }
      catch (SyntaxErrorException ex)
      {
        return false;
      }
      return false;
    }
  }
}
