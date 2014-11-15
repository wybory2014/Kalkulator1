// Decompiled with JetBrains decompiler
// Type: Kalkulator1.instalClass.Version
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

using Kalkulator1;
using Kalkulator1.ResponseClass;
using System;
using System.Xml;

namespace Kalkulator1.instalClass
{
  public static class Version
  {
    private static string version = "2.0.0.91";

    public static string getVersion()
    {
      return "v. " + Version.version;
    }

    public static bool newApp()
    {
      string str = Version.version;
      Connection connection = new Connection();
      if (connection.IsAvailableNetworkActive())
      {
        string uri = "KALK/sysver/20141116/000000/SMD-sysver";
        ResponseData requestRegions = connection.getRequestRegions(uri, 0);
        if (requestRegions.getCode().getcode() == 0)
        {
          XmlDocument xmlDocument = new XmlDocument();
          xmlDocument.LoadXml(requestRegions.getXml());
          XmlNode xmlNode = xmlDocument.SelectSingleNode("/version/setup");
          if (xmlNode != null)
            str = xmlNode.InnerText.Replace("KBW ", "");
        }
      }
      if (Version.version == str)
        return true;
      string[] strArray1 = str.Split('.');
      string[] strArray2 = Version.version.Split('.');
      if (strArray1.Length != strArray2.Length)
        return false;
      for (int index = 0; index < strArray1.Length; ++index)
      {
        if (Convert.ToInt32(strArray1[index]) > Convert.ToInt32(strArray2[index]))
          return false;
      }
      return true;
    }
  }
}
