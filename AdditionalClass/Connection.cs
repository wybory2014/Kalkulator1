// Decompiled with JetBrains decompiler
// Type: Kalkulator1.Connection
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

using Kalkulator1.ResponseClass;
using System;
using System.Collections.Generic;
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
      if (NetworkInterface.GetIsNetworkAvailable())
        return Enumerable.Any<IPv4InterfaceStatistics>(Enumerable.Select<NetworkInterface, IPv4InterfaceStatistics>(Enumerable.Where<NetworkInterface>(Enumerable.Where<NetworkInterface>((IEnumerable<NetworkInterface>) NetworkInterface.GetAllNetworkInterfaces(), (Func<NetworkInterface, bool>) (face => face.OperationalStatus == OperationalStatus.Up)), (Func<NetworkInterface, bool>) (face => face.NetworkInterfaceType != NetworkInterfaceType.Tunnel && face.NetworkInterfaceType != NetworkInterfaceType.Loopback)), (Func<NetworkInterface, IPv4InterfaceStatistics>) (face => face.GetIPv4Statistics())), (Func<IPv4InterfaceStatistics, bool>) (statistics => statistics.BytesReceived > 0L && statistics.BytesSent > 0L));
      else
        return false;
    }

    public bool getRequestKlk(string uri, string savePath, int i)
    {
      if (i >= this.servers.Length)
        return false;
      WebRequest webRequest = WebRequest.Create(new Uri(this.servers[i] + uri));
      webRequest.Timeout = this.timeout;
      webRequest.Method = "GET";
      webRequest.PreAuthenticate = true;
      webRequest.ContentType = "application/x-www-form-urlencoded";
      try
      {
        HttpWebResponse httpWebResponse = (HttpWebResponse) webRequest.GetResponse();
        if (!System.IO.File.Exists(savePath) || httpWebResponse == null || DateTime.Compare(System.IO.File.GetLastWriteTime(savePath), httpWebResponse.LastModified) < 0)
        {
          if (httpWebResponse != null)
          {
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding("utf-8"));
            string str1 = "";
            char[] buffer = new char[256];
            for (int length = streamReader.Read(buffer, 0, 256); length > 0; length = streamReader.Read(buffer, 0, 256))
            {
              string str2 = new string(buffer, 0, length);
              str1 = str1 + str2;
            }
            httpWebResponse.Close();
            streamReader.Close();
            try
            {
              StreamWriter streamWriter = new StreamWriter(savePath, false);
              streamWriter.Write(str1);
              streamWriter.Close();
            }
            catch (Exception ex)
            {
              return false;
            }
          }
          else
            this.getRequestKlk(uri, savePath, i++);
        }
        else
        {
          httpWebResponse.Close();
          return false;
        }
      }
      catch (WebException ex)
      {
        ++i;
        return this.getRequestKlk(uri, savePath, i);
      }
      return true;
    }

    public KLKresponse getRequestKBWKlk(string uri, string savePath, int i)
    {
      KLKresponse klKresponse = new KLKresponse();
      if (i >= this.servers2.Length)
        return klKresponse;
      WebRequest webRequest = WebRequest.Create(new Uri(this.servers2[i] + uri));
      webRequest.Timeout = this.timeout;
      webRequest.Method = "GET";
      webRequest.PreAuthenticate = true;
      webRequest.ContentType = "application/x-www-form-urlencoded";
      try
      {
        HttpWebResponse httpWebResponse = (HttpWebResponse) webRequest.GetResponse();
        if (!System.IO.File.Exists(savePath) || httpWebResponse == null || DateTime.Compare(System.IO.File.GetLastWriteTime(savePath), httpWebResponse.LastModified) < 0)
        {
          if (httpWebResponse != null)
          {
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding("utf-8"));
            string str1 = "";
            char[] buffer = new char[256];
            for (int length = streamReader.Read(buffer, 0, 256); length > 0; length = streamReader.Read(buffer, 0, 256))
            {
              string str2 = new string(buffer, 0, length);
              str1 = str1 + str2;
            }
            httpWebResponse.Close();
            streamReader.Close();
            klKresponse.setCode(new Code(0));
            try
            {
              StreamWriter streamWriter = new StreamWriter(savePath, false);
              streamWriter.Write(str1);
              streamWriter.Close();
              klKresponse.setSaved(true);
            }
            catch (Exception ex)
            {
              klKresponse.setException(ex);
              return klKresponse;
            }
          }
          else
            this.getRequestKlk(uri, savePath, i++);
        }
        else
        {
          httpWebResponse.Close();
          klKresponse.setCode(new Code(0));
          return klKresponse;
        }
      }
      catch (WebException ex)
      {
        ++i;
        return this.getRequestKBWKlk(uri, savePath, i);
      }
      return klKresponse;
    }

    public KLKresponse getRequestKBWKlkDocx(string uri, string savePath, int i)
    {
      int num = 0;
      KLKresponse klKresponse = new KLKresponse();
      if (i >= this.servers2.Length)
        return klKresponse;
      WebRequest webRequest = WebRequest.Create(new Uri(this.servers2[i] + uri));
      webRequest.Timeout = this.timeout;
      webRequest.Method = "GET";
      webRequest.PreAuthenticate = true;
      webRequest.ContentType = "application/x-www-form-urlencoded";
      try
      {
        HttpWebResponse httpWebResponse = (HttpWebResponse) webRequest.GetResponse();
        if (!System.IO.File.Exists(savePath) || httpWebResponse == null || DateTime.Compare(System.IO.File.GetLastWriteTime(savePath), httpWebResponse.LastModified) < 0)
        {
          if (httpWebResponse != null)
          {
            Stream responseStream = httpWebResponse.GetResponseStream();
            Stream stream = (Stream) System.IO.File.Create(savePath);
            byte[] buffer = new byte[1024];
            int count;
            do
            {
              count = responseStream.Read(buffer, 0, buffer.Length);
              stream.Write(buffer, 0, count);
              num += count;
            }
            while (count > 0);
            httpWebResponse.Close();
            stream.Close();
          }
          else
            this.getRequestKlk(uri, savePath, i++);
        }
        else
        {
          httpWebResponse.Close();
          klKresponse.setCode(new Code(0));
          return klKresponse;
        }
      }
      catch (WebException ex)
      {
        ++i;
        return this.getRequestKBWKlk(uri, savePath, i);
      }
      return klKresponse;
    }

    public ResponseData getRequestRegions(string uri, int i)
    {
      ResponseData responseData = new ResponseData();
      if (i >= this.servers2.Length)
        return responseData;
      WebRequest webRequest = WebRequest.Create(new Uri(this.servers2[i] + uri));
      webRequest.Timeout = this.timeout;
      webRequest.Method = "GET";
      webRequest.PreAuthenticate = true;
      webRequest.ContentType = "application/x-www-form-urlencoded";
      try
      {
        HttpWebResponse httpWebResponse = (HttpWebResponse) webRequest.GetResponse();
        if (httpWebResponse != null)
        {
          StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding("utf-8"));
          string xml = "";
          char[] buffer = new char[256];
          for (int length = streamReader.Read(buffer, 0, 256); length > 0; length = streamReader.Read(buffer, 0, 256))
          {
            string str = new string(buffer, 0, length);
            xml = xml + str;
          }
          httpWebResponse.Close();
          streamReader.Close();
          responseData.setXml(xml);
        }
        else
          this.getRequestRegions(uri, i++);
      }
      catch (WebException ex)
      {
        ++i;
        return this.getRequestRegions(uri, i);
      }
      return responseData;
    }

    public Logged postLog(string post, int i)
    {
      if (i >= this.servers.Length)
        return new Logged();
      byte[] bytes = Encoding.UTF8.GetBytes(post);
      WebRequest webRequest = WebRequest.Create(new Uri(this.servers[i] + "users/login"));
      webRequest.Timeout = this.timeout;
      webRequest.Method = "POST";
      webRequest.PreAuthenticate = true;
      webRequest.ContentType = "application/x-www-form-urlencoded";
      webRequest.ContentLength = (long) bytes.Length;
      Stream requestStream = webRequest.GetRequestStream();
      requestStream.Write(bytes, 0, bytes.Length);
      requestStream.Close();
      try
      {
        HttpWebResponse httpWebResponse = (HttpWebResponse) webRequest.GetResponse();
        string json = "";
        if (httpWebResponse != null)
        {
          StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding("utf-8"));
          char[] buffer = new char[256];
          for (int length = streamReader.Read(buffer, 0, 256); length > 0; length = streamReader.Read(buffer, 0, 256))
          {
            string str = new string(buffer, 0, length);
            json = json + str;
          }
          if (json != "0")
          {
            Logged logged = new Logged();
            logged.getLoggetFromJson(json);
            return logged;
          }
          else
          {
            httpWebResponse.Close();
            return new Logged();
          }
        }
        else
        {
          ++i;
          return this.postLog(post, i);
        }
      }
      catch (WebException ex)
      {
        ++i;
        return this.postLog(post, i);
      }
    }

    public IsLicense postCheckLicense(string uri, string post, int i)
    {
      if (i >= this.servers.Length)
        return new IsLicense();
      byte[] bytes = Encoding.UTF8.GetBytes(post);
      WebRequest webRequest = WebRequest.Create(new Uri(this.servers[i] + uri));
      webRequest.Timeout = this.timeout;
      webRequest.Method = "POST";
      webRequest.PreAuthenticate = true;
      webRequest.ContentType = "application/x-www-form-urlencoded";
      webRequest.ContentLength = (long) bytes.Length;
      Stream requestStream = webRequest.GetRequestStream();
      requestStream.Write(bytes, 0, bytes.Length);
      requestStream.Close();
      try
      {
        HttpWebResponse httpWebResponse = (HttpWebResponse) webRequest.GetResponse();
        string json = "";
        if (httpWebResponse != null)
        {
          StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding("utf-8"));
          char[] buffer = new char[256];
          for (int length = streamReader.Read(buffer, 0, 256); length > 0; length = streamReader.Read(buffer, 0, 256))
          {
            string str = new string(buffer, 0, length);
            json = json + str;
          }
          if (json != "")
          {
            IsLicense isLicense = new IsLicense();
            isLicense.getFromJson(json);
            return isLicense;
          }
          else
          {
            httpWebResponse.Close();
            return new IsLicense();
          }
        }
        else
        {
          ++i;
          return this.postCheckLicense(uri, post, i);
        }
      }
      catch (WebException ex)
      {
        ++i;
        return this.postCheckLicense(uri, post, i);
      }
    }

    public Code postReq(string uri, string post, int i)
    {
      WebException webException;
      try
      {
        if (i < this.servers.Length)
        {
          byte[] bytes = Encoding.UTF8.GetBytes(post);
          WebRequest webRequest = WebRequest.Create(new Uri(this.servers[i] + uri));
          webRequest.Timeout = this.timeout;
          webRequest.Method = "POST";
          webRequest.PreAuthenticate = true;
          webRequest.ContentType = "application/x-www-form-urlencoded";
          webRequest.ContentLength = (long) bytes.Length;
          Stream requestStream = webRequest.GetRequestStream();
          requestStream.Write(bytes, 0, bytes.Length);
          requestStream.Close();
          try
          {
            HttpWebResponse httpWebResponse = (HttpWebResponse) webRequest.GetResponse();
            string json = "";
            if (httpWebResponse != null)
            {
              StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding("utf-8"));
              char[] buffer = new char[256];
              for (int length = streamReader.Read(buffer, 0, 256); length > 0; length = streamReader.Read(buffer, 0, 256))
              {
                string str = new string(buffer, 0, length);
                json = json + str;
              }
              if (json != "")
              {
                ResponseData responseData = new ResponseData();
                responseData.getFromJson(json);
                return new Code(responseData.getCode().getcode());
              }
              else
              {
                httpWebResponse.Close();
                return new Code();
              }
            }
            else
            {
              ++i;
              return this.postReq(uri, post, i);
            }
          }
          catch (WebException ex)
          {
            webException = ex;
            ++i;
            return this.postReq(uri, post, i);
          }
        }
      }
      catch (WebException ex)
      {
        webException = ex;
        ++i;
        return this.postReq(uri, post, i);
      }
      return new Code();
    }

    public ResponseData postSendCode(string uri, string post, int i)
    {
      if (i >= this.servers.Length)
        return new ResponseData();
      byte[] bytes = Encoding.UTF8.GetBytes(post);
      WebRequest webRequest = WebRequest.Create(new Uri(this.servers[i] + uri));
      webRequest.Method = "POST";
      webRequest.PreAuthenticate = true;
      webRequest.Timeout = this.timeout;
      webRequest.ContentType = "application/x-www-form-urlencoded";
      webRequest.ContentLength = (long) bytes.Length;
      Stream requestStream = webRequest.GetRequestStream();
      requestStream.Write(bytes, 0, bytes.Length);
      requestStream.Close();
      try
      {
        HttpWebResponse httpWebResponse = (HttpWebResponse) webRequest.GetResponse();
        string json = "";
        if (httpWebResponse != null)
        {
          StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding("utf-8"));
          char[] buffer = new char[256];
          for (int length = streamReader.Read(buffer, 0, 256); length > 0; length = streamReader.Read(buffer, 0, 256))
          {
            string str = new string(buffer, 0, length);
            json = json + str;
          }
          if (json != "")
          {
            ResponseData responseData = new ResponseData();
            responseData.getFromJson(json);
            return responseData;
          }
          else
          {
            httpWebResponse.Close();
            return new ResponseData();
          }
        }
        else
        {
          ++i;
          return this.postSendCode(uri, post, i);
        }
      }
      catch (WebException ex)
      {
        ++i;
        return this.postSendCode(uri, post, i);
      }
    }

    public ResponseData postImport(string uri, string post, int i)
    {
      if (i >= this.servers.Length)
        return new ResponseData();
      byte[] bytes = Encoding.UTF8.GetBytes(post);
      WebRequest webRequest = WebRequest.Create(new Uri(this.servers[i] + uri));
      webRequest.Method = "POST";
      webRequest.PreAuthenticate = true;
      webRequest.Timeout = this.timeout;
      webRequest.ContentType = "application/x-www-form-urlencoded";
      webRequest.ContentLength = (long) bytes.Length;
      Stream requestStream = webRequest.GetRequestStream();
      requestStream.Write(bytes, 0, bytes.Length);
      requestStream.Close();
      try
      {
        HttpWebResponse httpWebResponse = (HttpWebResponse) webRequest.GetResponse();
        string json = "";
        if (httpWebResponse != null)
        {
          StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding("utf-8"));
          char[] buffer = new char[256];
          for (int length = streamReader.Read(buffer, 0, 256); length > 0; length = streamReader.Read(buffer, 0, 256))
          {
            string str = new string(buffer, 0, length);
            json = json + str;
          }
          if (json != "")
          {
            ResponseData responseData = new ResponseData();
            responseData.getFromJson(json);
            return responseData;
          }
          else
          {
            httpWebResponse.Close();
            return new ResponseData();
          }
        }
        else
        {
          ++i;
          return this.postImport(uri, post, i);
        }
      }
      catch (WebException ex)
      {
        ++i;
        return this.postImport(uri, post, i);
      }
    }
  }
}
