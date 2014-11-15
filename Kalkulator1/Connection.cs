using Kalkulator1.ResponseClass;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
namespace Kalkulator1
{
	public class Connection
	{
		private string[] servers;
		private string[] servers2;
		private int timeout;
		public Connection()
		{
			this.servers = new string[1];
			this.servers[0] = "https://syswyb.kbw.gov.pl/";
			this.servers2 = new string[1];
			this.servers2[0] = "http://klk.kbw.gov.pl/";
			this.timeout = 30000;
		}
		public bool IsAvailableNetworkActive()
		{
			bool result;
			if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
			{
				System.Net.NetworkInformation.NetworkInterface[] interfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
				bool bWynik = (
					from face in interfaces
					where face.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up
					where face.NetworkInterfaceType != System.Net.NetworkInformation.NetworkInterfaceType.Tunnel && face.NetworkInterfaceType != System.Net.NetworkInformation.NetworkInterfaceType.Loopback
					select face.GetIPv4Statistics()).Any((System.Net.NetworkInformation.IPv4InterfaceStatistics statistics) => statistics.BytesReceived > 0L && statistics.BytesSent > 0L);
				result = bWynik;
			}
			else
			{
				result = false;
			}
			return result;
		}
		public bool getRequestKlk(string uri, string savePath, int i)
		{
			bool result;
			if (i < this.servers.Length)
			{
				System.Uri target = new System.Uri(this.servers[i] + uri);
				System.Net.WebRequest req = System.Net.WebRequest.Create(target);
				req.Timeout = this.timeout;
				req.Method = "GET";
				req.PreAuthenticate = true;
				req.ContentType = "application/x-www-form-urlencoded";
				try
				{
					System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)req.GetResponse();
					if (System.IO.File.Exists(savePath) && response != null && System.DateTime.Compare(System.IO.File.GetLastWriteTime(savePath), response.LastModified) >= 0)
					{
						response.Close();
						result = false;
						return result;
					}
					if (response != null)
					{
						System.IO.Stream receiveStream = response.GetResponseStream();
						System.Text.Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
						System.IO.StreamReader readStream = new System.IO.StreamReader(receiveStream, encode);
						string a = "";
						char[] read = new char[256];
						for (int count = readStream.Read(read, 0, 256); count > 0; count = readStream.Read(read, 0, 256))
						{
							string str = new string(read, 0, count);
							a += str;
						}
						response.Close();
						readStream.Close();
						try
						{
							System.IO.StreamWriter sw = new System.IO.StreamWriter(savePath, false);
							sw.Write(a);
							sw.Close();
						}
						catch (System.Exception)
						{
							result = false;
							return result;
						}
					}
					else
					{
						this.getRequestKlk(uri, savePath, i++);
					}
				}
				catch (System.Net.WebException)
				{
					i++;
					result = this.getRequestKlk(uri, savePath, i);
					return result;
				}
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}
		public KLKresponse getRequestKBWKlk(string uri, string savePath, int i)
		{
			KLKresponse res = new KLKresponse();
			KLKresponse result;
			if (i < this.servers2.Length)
			{
				System.Uri target = new System.Uri(this.servers2[i] + uri);
				System.Net.WebRequest req = System.Net.WebRequest.Create(target);
				req.Timeout = this.timeout;
				req.Method = "GET";
				req.PreAuthenticate = true;
				req.ContentType = "application/x-www-form-urlencoded";
				try
				{
					System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)req.GetResponse();
					if (System.IO.File.Exists(savePath) && response != null && System.DateTime.Compare(System.IO.File.GetLastWriteTime(savePath), response.LastModified) >= 0)
					{
						response.Close();
						res.setCode(new Code(0));
						result = res;
						return result;
					}
					if (response != null)
					{
						System.IO.Stream receiveStream = response.GetResponseStream();
						System.Text.Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
						System.IO.StreamReader readStream = new System.IO.StreamReader(receiveStream, encode);
						string a = "";
						char[] read = new char[256];
						for (int count = readStream.Read(read, 0, 256); count > 0; count = readStream.Read(read, 0, 256))
						{
							string str = new string(read, 0, count);
							a += str;
						}
						response.Close();
						readStream.Close();
						res.setCode(new Code(0));
						try
						{
							System.IO.StreamWriter sw = new System.IO.StreamWriter(savePath, false);
							sw.Write(a);
							sw.Close();
							res.setSaved(true);
						}
						catch (System.Exception e)
						{
							res.setException(e);
							result = res;
							return result;
						}
					}
					else
					{
						this.getRequestKlk(uri, savePath, i++);
					}
				}
				catch (System.Net.WebException)
				{
					i++;
					result = this.getRequestKBWKlk(uri, savePath, i);
					return result;
				}
				result = res;
			}
			else
			{
				result = res;
			}
			return result;
		}
		public KLKresponse getRequestKBWKlkDocx(string uri, string savePath, int i)
		{
			int bytesProcessed = 0;
			KLKresponse res = new KLKresponse();
			KLKresponse result;
			if (i < this.servers2.Length)
			{
				System.Uri target = new System.Uri(this.servers2[i] + uri);
				System.Net.WebRequest req = System.Net.WebRequest.Create(target);
				req.Timeout = this.timeout;
				req.Method = "GET";
				req.PreAuthenticate = true;
				req.ContentType = "application/x-www-form-urlencoded";
				try
				{
					System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)req.GetResponse();
					if (System.IO.File.Exists(savePath) && response != null && System.DateTime.Compare(System.IO.File.GetLastWriteTime(savePath), response.LastModified) >= 0)
					{
						response.Close();
						res.setCode(new Code(0));
						result = res;
						return result;
					}
					if (response != null)
					{
						System.IO.Stream receiveStream = response.GetResponseStream();
						System.IO.Stream localStream = System.IO.File.Create(savePath);
						byte[] buffer = new byte[1024];
						int bytesRead;
						do
						{
							bytesRead = receiveStream.Read(buffer, 0, buffer.Length);
							localStream.Write(buffer, 0, bytesRead);
							bytesProcessed += bytesRead;
						}
						while (bytesRead > 0);
						response.Close();
						localStream.Close();
					}
					else
					{
						this.getRequestKlk(uri, savePath, i++);
					}
				}
				catch (System.Net.WebException)
				{
					i++;
					result = this.getRequestKBWKlk(uri, savePath, i);
					return result;
				}
				result = res;
			}
			else
			{
				result = res;
			}
			return result;
		}
		public ResponseData getRequestRegions(string uri, int i)
		{
			ResponseData res = new ResponseData();
			ResponseData result;
			if (i < this.servers2.Length)
			{
				System.Uri target = new System.Uri(this.servers2[i] + uri);
				System.Net.WebRequest req = System.Net.WebRequest.Create(target);
				req.Timeout = this.timeout;
				req.Method = "GET";
				req.PreAuthenticate = true;
				req.ContentType = "application/x-www-form-urlencoded";
				try
				{
					System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)req.GetResponse();
					if (response != null)
					{
						System.IO.Stream receiveStream = response.GetResponseStream();
						System.Text.Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
						System.IO.StreamReader readStream = new System.IO.StreamReader(receiveStream, encode);
						string a = "";
						char[] read = new char[256];
						for (int count = readStream.Read(read, 0, 256); count > 0; count = readStream.Read(read, 0, 256))
						{
							string str = new string(read, 0, count);
							a += str;
						}
						response.Close();
						readStream.Close();
						res.setXml(a);
					}
					else
					{
						this.getRequestRegions(uri, i++);
					}
				}
				catch (System.Net.WebException)
				{
					i++;
					result = this.getRequestRegions(uri, i);
					return result;
				}
				result = res;
			}
			else
			{
				result = res;
			}
			return result;
		}
		public Logged postLog(string post, int i)
		{
			Logged result3;
			if (i < this.servers.Length)
			{
				byte[] rv = System.Text.Encoding.UTF8.GetBytes(post);
				System.Uri target = new System.Uri(this.servers[i] + "users/login");
				System.Net.WebRequest req = System.Net.WebRequest.Create(target);
				req.Timeout = this.timeout;
				req.Method = "POST";
				req.PreAuthenticate = true;
				req.ContentType = "application/x-www-form-urlencoded";
				req.ContentLength = (long)rv.Length;
				System.IO.Stream os = req.GetRequestStream();
				os.Write(rv, 0, rv.Length);
				os.Close();
				try
				{
					System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)req.GetResponse();
					string a = "";
					if (response == null)
					{
						i++;
						result3 = this.postLog(post, i);
						return result3;
					}
					System.IO.Stream receiveStream = response.GetResponseStream();
					System.Text.Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
					System.IO.StreamReader readStream = new System.IO.StreamReader(receiveStream, encode);
					char[] read = new char[256];
					for (int count = readStream.Read(read, 0, 256); count > 0; count = readStream.Read(read, 0, 256))
					{
						string str = new string(read, 0, count);
						a += str;
					}
					if (a != "0")
					{
						Logged result = new Logged();
						result.getLoggetFromJson(a);
						result3 = result;
						return result3;
					}
					response.Close();
					result3 = new Logged();
					return result3;
				}
				catch (System.Net.WebException)
				{
					i++;
					result3 = this.postLog(post, i);
					return result3;
				}
			}
			Logged result2 = new Logged();
			result3 = result2;
			return result3;
		}
		public IsLicense postCheckLicense(string uri, string post, int i)
		{
			IsLicense result2;
			if (i < this.servers.Length)
			{
				byte[] rv = System.Text.Encoding.UTF8.GetBytes(post);
				System.Uri target = new System.Uri(this.servers[i] + uri);
				System.Net.WebRequest req = System.Net.WebRequest.Create(target);
				req.Timeout = this.timeout;
				req.Method = "POST";
				req.PreAuthenticate = true;
				req.ContentType = "application/x-www-form-urlencoded";
				req.ContentLength = (long)rv.Length;
				System.IO.Stream os = req.GetRequestStream();
				os.Write(rv, 0, rv.Length);
				os.Close();
				try
				{
					System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)req.GetResponse();
					string a = "";
					if (response == null)
					{
						i++;
						result2 = this.postCheckLicense(uri, post, i);
						return result2;
					}
					System.IO.Stream receiveStream = response.GetResponseStream();
					System.Text.Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
					System.IO.StreamReader readStream = new System.IO.StreamReader(receiveStream, encode);
					char[] read = new char[256];
					for (int count = readStream.Read(read, 0, 256); count > 0; count = readStream.Read(read, 0, 256))
					{
						string str = new string(read, 0, count);
						a += str;
					}
					if (a != "")
					{
						IsLicense result = new IsLicense();
						result.getFromJson(a);
						result2 = result;
						return result2;
					}
					response.Close();
					result2 = new IsLicense();
					return result2;
				}
				catch (System.Net.WebException)
				{
					i++;
					result2 = this.postCheckLicense(uri, post, i);
					return result2;
				}
			}
			result2 = new IsLicense();
			return result2;
		}
		public Code postReq(string uri, string post, int i)
		{
			Code result3;
			try
			{
				if (i < this.servers.Length)
				{
					byte[] rv = System.Text.Encoding.UTF8.GetBytes(post);
					System.Uri target = new System.Uri(this.servers[i] + uri);
					System.Net.WebRequest req = System.Net.WebRequest.Create(target);
					req.Timeout = this.timeout;
					req.Method = "POST";
					req.PreAuthenticate = true;
					req.ContentType = "application/x-www-form-urlencoded";
					req.ContentLength = (long)rv.Length;
					System.IO.Stream os = req.GetRequestStream();
					os.Write(rv, 0, rv.Length);
					os.Close();
					try
					{
						System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)req.GetResponse();
						string a = "";
						if (response == null)
						{
							i++;
							result3 = this.postReq(uri, post, i);
							return result3;
						}
						System.IO.Stream receiveStream = response.GetResponseStream();
						System.Text.Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
						System.IO.StreamReader readStream = new System.IO.StreamReader(receiveStream, encode);
						char[] read = new char[256];
						for (int count = readStream.Read(read, 0, 256); count > 0; count = readStream.Read(read, 0, 256))
						{
							string str = new string(read, 0, count);
							a += str;
						}
						if (a != "")
						{
							ResponseData result = new ResponseData();
							result.getFromJson(a);
							Code result2 = new Code(result.getCode().getcode());
							result3 = result2;
							return result3;
						}
						response.Close();
						result3 = new Code();
						return result3;
					}
					catch (System.Net.WebException e_190)
					{
						i++;
						result3 = this.postReq(uri, post, i);
						return result3;
					}
				}
			}
			catch (System.Net.WebException e_190)
			{
				i++;
				result3 = this.postReq(uri, post, i);
				return result3;
			}
			result3 = new Code();
			return result3;
		}
		public ResponseData postSendCode(string uri, string post, int i)
		{
			ResponseData result2;
			if (i < this.servers.Length)
			{
				byte[] rv = System.Text.Encoding.UTF8.GetBytes(post);
				System.Uri target = new System.Uri(this.servers[i] + uri);
				System.Net.WebRequest req = System.Net.WebRequest.Create(target);
				req.Method = "POST";
				req.PreAuthenticate = true;
				req.Timeout = this.timeout;
				req.ContentType = "application/x-www-form-urlencoded";
				req.ContentLength = (long)rv.Length;
				System.IO.Stream os = req.GetRequestStream();
				os.Write(rv, 0, rv.Length);
				os.Close();
				try
				{
					System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)req.GetResponse();
					string a = "";
					if (response == null)
					{
						i++;
						result2 = this.postSendCode(uri, post, i);
						return result2;
					}
					System.IO.Stream receiveStream = response.GetResponseStream();
					System.Text.Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
					System.IO.StreamReader readStream = new System.IO.StreamReader(receiveStream, encode);
					char[] read = new char[256];
					for (int count = readStream.Read(read, 0, 256); count > 0; count = readStream.Read(read, 0, 256))
					{
						string str = new string(read, 0, count);
						a += str;
					}
					if (a != "")
					{
						ResponseData result = new ResponseData();
						result.getFromJson(a);
						result2 = result;
						return result2;
					}
					response.Close();
					result2 = new ResponseData();
					return result2;
				}
				catch (System.Net.WebException)
				{
					i++;
					result2 = this.postSendCode(uri, post, i);
					return result2;
				}
			}
			result2 = new ResponseData();
			return result2;
		}
		public ResponseData postImport(string uri, string post, int i)
		{
			ResponseData result2;
			if (i < this.servers.Length)
			{
				byte[] rv = System.Text.Encoding.UTF8.GetBytes(post);
				System.Uri target = new System.Uri(this.servers[i] + uri);
				System.Net.WebRequest req = System.Net.WebRequest.Create(target);
				req.Method = "POST";
				req.PreAuthenticate = true;
				req.Timeout = this.timeout;
				req.ContentType = "application/x-www-form-urlencoded";
				req.ContentLength = (long)rv.Length;
				System.IO.Stream os = req.GetRequestStream();
				os.Write(rv, 0, rv.Length);
				os.Close();
				try
				{
					System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)req.GetResponse();
					string a = "";
					if (response == null)
					{
						i++;
						result2 = this.postImport(uri, post, i);
						return result2;
					}
					System.IO.Stream receiveStream = response.GetResponseStream();
					System.Text.Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
					System.IO.StreamReader readStream = new System.IO.StreamReader(receiveStream, encode);
					char[] read = new char[256];
					for (int count = readStream.Read(read, 0, 256); count > 0; count = readStream.Read(read, 0, 256))
					{
						string str = new string(read, 0, count);
						a += str;
					}
					if (a != "")
					{
						ResponseData result = new ResponseData();
						result.getFromJson(a);
						result2 = result;
						return result2;
					}
					response.Close();
					result2 = new ResponseData();
					return result2;
				}
				catch (System.Net.WebException)
				{
					i++;
					result2 = this.postImport(uri, post, i);
					return result2;
				}
			}
			result2 = new ResponseData();
			return result2;
		}
	}
}
