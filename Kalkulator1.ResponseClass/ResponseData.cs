using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
namespace Kalkulator1.ResponseClass
{
	public class ResponseData
	{
		private Code c;
		private string xml;
		public ResponseData()
		{
			this.c = new Code();
			this.xml = "";
		}
		public void getFromJson(string json)
		{
			try
			{
				JObject o = JObject.Parse(json);
				this.c = new Code((int)o.SelectToken("code"));
				this.xml = (string)o.SelectToken("xml");
			}
			catch (JsonReaderException)
			{
				this.c = new Code(3);
			}
			catch (System.ArgumentNullException)
			{
			}
		}
		public void setXml(string xml)
		{
			try
			{
				this.c = new Code(0);
				this.xml = xml;
			}
			catch (JsonReaderException)
			{
				this.c = new Code(3);
			}
			catch (System.ArgumentNullException)
			{
			}
		}
		public Code getCode()
		{
			return this.c;
		}
		public string getXml()
		{
			return this.xml;
		}
	}
}
