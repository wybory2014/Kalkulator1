using Kalkulator1.ResponseClass;
using System;
using System.Xml;
namespace Kalkulator1.instalClass
{
	public static class Version
	{
		private static string version = "2.0.0.102";
		public static string getVersion()
		{
			return "v. " + Version.version;
		}
		public static bool newApp()
		{
			string a = Version.version;
			Connection con = new Connection();
			if (con.IsAvailableNetworkActive())
			{
				string uri = "KALK/sysver/20141116/000000/SMD-sysver";
				ResponseData res = con.getRequestRegions(uri, 0);
				if (res.getCode().getcode() == 0)
				{
					XmlDocument versionXML = new XmlDocument();
					versionXML.LoadXml(res.getXml());
					XmlNode versionNode = versionXML.SelectSingleNode("/version/setup");
					if (versionNode != null)
					{
						a = versionNode.InnerText;
						a = a.Replace("KBW ", "");
					}
				}
			}
			bool result;
			if (Version.version == a)
			{
				result = true;
			}
			else
			{
				string[] a2 = a.Split(new char[]
				{
					'.'
				});
				string[] v = Version.version.Split(new char[]
				{
					'.'
				});
				if (a2.Length == v.Length)
				{
					for (int i = 0; i < a2.Length; i++)
					{
						if (System.Convert.ToInt32(a2[i]) > System.Convert.ToInt32(v[i]))
						{
							result = false;
							return result;
						}
					}
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}
	}
}
