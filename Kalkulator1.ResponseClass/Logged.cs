using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
namespace Kalkulator1.ResponseClass
{
	public class Logged
	{
		private string elections;
		private string JST;
		private string rola;
		private int nr_obwodu;
		private string token;
		private string user;
		public Logged()
		{
			this.elections = "";
			this.JST = "";
			this.rola = "";
			this.nr_obwodu = -1;
			this.token = "";
			this.user = "";
		}
		public void getLoggetFromJson(string json)
		{
			try
			{
				JObject o = JObject.Parse(json);
				int jst = 0;
				this.elections = (string)o.SelectToken("elections");
				try
				{
					jst = (int)o.SelectToken("JST");
				}
				catch (System.Exception)
				{
					jst = System.Convert.ToInt32((string)o.SelectToken("JST"));
				}
				this.JST = jst.ToString();
				this.rola = (string)o.SelectToken("rola");
				this.nr_obwodu = (int)o.SelectToken("nr_obwodu");
				this.token = (string)o.SelectToken("token");
				try
				{
					this.user = (string)o.SelectToken("user");
				}
				catch (JsonReaderException)
				{
					this.user = "";
				}
				catch (System.ArgumentNullException)
				{
					this.user = "";
				}
			}
			catch (JsonReaderException)
			{
			}
			catch (System.ArgumentNullException)
			{
				this.elections = "";
				this.JST = "";
				this.rola = "";
				this.nr_obwodu = -1;
				this.token = "";
			}
		}
		public string getToken()
		{
			return this.token;
		}
		public string getUser()
		{
			return this.user;
		}
		public string getElections()
		{
			return this.elections;
		}
		public string getJns()
		{
			return this.JST;
		}
		public string getRola()
		{
			return this.rola;
		}
		public int getObwodu()
		{
			return this.nr_obwodu;
		}
	}
}
