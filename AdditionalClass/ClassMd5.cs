// Decompiled with JetBrains decompiler
// Type: Kalkulator1.ClassMd5
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

using System;
using System.Security.Cryptography;
using System.Text;

namespace Kalkulator1
{
  internal class ClassMd5
  {
    public string CreateMD5Hash(string input)
    {
      byte[] hash = MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(input));
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < hash.Length; ++index)
        stringBuilder.Append(hash[index].ToString("X2"));
      return ((object) stringBuilder).ToString();
    }

    public string CreateMD5Hash2(string input)
    {
      return BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input))).Replace("-", "").ToLower();
    }
  }
}
