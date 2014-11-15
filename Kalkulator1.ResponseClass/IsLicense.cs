using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
namespace Kalkulator1.ResponseClass
{
	public class IsLicense
	{
		private Code c;
		private string crt;
		public IsLicense()
		{
			this.c = new Code();
			this.crt = "";
		}
		public void getFromJson(string json)
		{
			try
			{
				JObject o = JObject.Parse(json);
				this.crt = (string)o.SelectToken("crt");
				try
				{
					this.c = new Code((int)o.SelectToken("code"));
				}
				catch (JsonReaderException)
				{
					if (this.crt != null && this.crt != "")
					{
						this.c = new Code(0);
					}
					else
					{
						this.c = new Code(3);
					}
				}
				catch (System.ArgumentNullException)
				{
					if (this.crt != null && this.crt != "")
					{
						this.c = new Code(0);
					}
					else
					{
						this.c = new Code(3);
					}
				}
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
		public string getCrt()
		{
			return this.crt;
		}
	}
}
