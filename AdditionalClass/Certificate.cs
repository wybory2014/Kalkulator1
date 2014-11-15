// Decompiled with JetBrains decompiler
// Type: Kalkulator1.Certificate
// Assembly: Kalkulator1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3174950B-2661-44B0-AE1D-C1E9AD8AB005
// Assembly location: D:\Kalkulator\Kalkulator1.exe

using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace Kalkulator1
{
  public class Certificate
  {
    private string keys;
    private string generatedCSR;
    private System.Security.Cryptography.X509Certificates.X509Certificate cert;
    private string path;

    public Certificate()
    {
      this.keys = "";
      this.generatedCSR = "";
      System.Security.Cryptography.X509Certificates.X509Certificate x509Certificate = new System.Security.Cryptography.X509Certificates.X509Certificate();
      this.path = Path.GetTempPath() + "KBW";
    }

    public Certificate(string filename)
    {
      this.keys = "";
      this.generatedCSR = "";
      this.cert = new System.Security.Cryptography.X509Certificates.X509Certificate(filename);
      this.path = Path.GetTempPath() + "KBW";
    }

    public string getCSR()
    {
      return this.generatedCSR;
    }

    public bool isActiveLicense(string license)
    {
      bool flag = false;
      try
      {
        System.Security.Cryptography.X509Certificates.X509Certificate x509Certificate = new System.Security.Cryptography.X509Certificates.X509Certificate(license);
        DateTime dateTime = new DateTime(1, 1, 1, 0, 0, 0);
        DateTime t1 = Convert.ToDateTime(x509Certificate.GetEffectiveDateString());
        if (t1 == dateTime)
        {
          Thread.Sleep(1000);
          t1 = Convert.ToDateTime(x509Certificate.GetEffectiveDateString());
        }
        DateTime t2 = Convert.ToDateTime(x509Certificate.GetExpirationDateString());
        if (t2 == dateTime)
        {
          Thread.Sleep(1000);
          t2 = Convert.ToDateTime(x509Certificate.GetEffectiveDateString());
        }
        if (DateTime.Compare(t1, DateTime.Now) <= 0 && DateTime.Compare(DateTime.Now, t2) <= 0)
          flag = true;
      }
      catch (CryptographicException ex)
      {
      }
      return flag;
    }

    public void generateCSR(string electoralEampaign, string user, string password)
    {
      X509Name subject = new X509Name("C=PL, ST=Mazowieckie, L=Warszawa, O=KBW, E=-, OU=" + electoralEampaign + ", CN=" + user);
      RsaKeyPairGenerator keyPairGenerator = new RsaKeyPairGenerator();
      RsaKeyGenerationParameters generationParameters = new RsaKeyGenerationParameters(BigInteger.ValueOf(3L), new SecureRandom(), 2048, 25);
      keyPairGenerator.Init((KeyGenerationParameters) generationParameters);
      AsymmetricCipherKeyPair asymmetricCipherKeyPair = keyPairGenerator.GenerateKeyPair();
      Pkcs10CertificationRequest certificationRequest = new Pkcs10CertificationRequest("SHA1WITHRSA", subject, asymmetricCipherKeyPair.Public, (Asn1Set) null, asymmetricCipherKeyPair.Private);
      StringBuilder sb1 = new StringBuilder();
      PemWriter pemWriter1 = new PemWriter((TextWriter) new StringWriter(sb1));
      pemWriter1.WriteObject((object) certificationRequest);
      pemWriter1.Writer.Flush();
      this.generatedCSR = ((object) sb1).ToString();
      StringBuilder sb2 = new StringBuilder();
      new PemWriter((TextWriter) new StringWriter(sb2)).WriteObject((object) asymmetricCipherKeyPair.Private);
      pemWriter1.Writer.Flush();
      this.keys = ((object) sb2).ToString();
      AsymmetricKeyParameter @public = asymmetricCipherKeyPair.Public;
      AsymmetricKeyParameter @private = asymmetricCipherKeyPair.Private;
      StringWriter stringWriter = new StringWriter();
      PemWriter pemWriter2 = new PemWriter((TextWriter) stringWriter);
      pemWriter2.WriteObject((object) @private, "AES-256-CBC", password.ToCharArray(), new SecureRandom());
      pemWriter2.Writer.Close();
      this.keys = stringWriter.ToString();
      if (!Directory.Exists(this.path + "\\Licenses"))
      {
        try
        {
          Directory.CreateDirectory(this.path + "\\Licenses");
        }
        catch (ArgumentNullException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
        }
        catch (ArgumentException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
        }
        catch (UnauthorizedAccessException ex)
        {
          int num = (int) MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
        }
        catch (PathTooLongException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
        }
        catch (DirectoryNotFoundException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
        }
        catch (NotSupportedException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"Licenses\"", "Error");
        }
        catch (IOException ex)
        {
          int num = (int) MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
        }
      }
      string str1;
      try
      {
        int num = Directory.GetFiles(this.path + "\\Licenses", electoralEampaign.Replace("/", "_") + "_" + user + "*.csr").Length + 1;
        if (num > 1)
        {
          string str2 = num.ToString();
          str1 = electoralEampaign.Replace("/", "_") + "_" + user + " " + str2;
        }
        else
          str1 = electoralEampaign.Replace("/", "_") + "_" + user;
      }
      catch (Exception ex)
      {
        str1 = electoralEampaign.Replace("/", "_") + "_" + user;
      }
      try
      {
        StreamWriter streamWriter1 = new StreamWriter(this.path + "\\Licenses\\" + str1 + ".csr", false);
        streamWriter1.Write(this.generatedCSR);
        streamWriter1.Close();
        StreamWriter streamWriter2 = new StreamWriter(this.path + "\\Licenses\\" + str1 + ".key", false);
        streamWriter2.Write(this.keys);
        streamWriter2.Close();
      }
      catch (ArgumentNullException ex)
      {
        int num = (int) MessageBox.Show("Błędna ścieżka", "Error");
      }
      catch (ArgumentException ex)
      {
        int num = (int) MessageBox.Show("Błędna ścieżka", "Error");
      }
      catch (DirectoryNotFoundException ex)
      {
        int num = (int) MessageBox.Show("Nie znaleziono katalogu", "Error");
      }
      catch (PathTooLongException ex)
      {
        int num = (int) MessageBox.Show("Zbyt długa ścieżka do katalogu", "Error");
      }
      catch (IOException ex)
      {
        int num = (int) MessageBox.Show("Podano ścieżke do pliku zamiast katalogu", "Error");
      }
      catch (UnauthorizedAccessException ex)
      {
        int num = (int) MessageBox.Show("Program nie posiada uprawnień do zapisywania plików. Otwórz aplikacje jako Administrator", "Error");
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Nie można zapisać pliku. Orginal error: " + ex.Message, "Error");
      }
    }

    public bool isActiveLicense()
    {
      bool flag = false;
      try
      {
        if (DateTime.Compare(Convert.ToDateTime(this.cert.GetEffectiveDateString()), DateTime.Now) <= 0 && DateTime.Compare(DateTime.Now, Convert.ToDateTime(this.cert.GetExpirationDateString())) <= 0)
          flag = true;
      }
      catch (CryptographicException ex)
      {
      }
      return flag;
    }

    public string getSubject()
    {
      return this.cert.Subject;
    }

    public Pkcs10CertificationRequest LoadCertificate(string pemFilenameCSR)
    {
      return new PemReader((TextReader) File.OpenText(pemFilenameCSR)).ReadObject() as Pkcs10CertificationRequest;
    }

    public void RsaKeysGenerate(string PrivateKeyFilename, string PublicKeyFilename, string passw)
    {
      RsaKeyPairGenerator keyPairGenerator = new RsaKeyPairGenerator();
      RsaKeyGenerationParameters generationParameters = new RsaKeyGenerationParameters(BigInteger.ValueOf(3L), new SecureRandom(), 1024, 25);
      keyPairGenerator.Init((KeyGenerationParameters) generationParameters);
      AsymmetricCipherKeyPair asymmetricCipherKeyPair = keyPairGenerator.GenerateKeyPair();
      AsymmetricKeyParameter @public = asymmetricCipherKeyPair.Public;
      AsymmetricKeyParameter @private = asymmetricCipherKeyPair.Private;
      byte[] encoded1 = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(@public).ToAsn1Object().GetEncoded();
      FileStream fileStream1 = new FileStream(PublicKeyFilename, FileMode.Create, FileAccess.Write);
      fileStream1.Write(encoded1, 0, encoded1.Length);
      fileStream1.Close();
      string algorithm = "1.2.840.113549.1.12.1.3";
      byte[] salt = new byte[10]
      {
        (byte) 1,
        (byte) 2,
        (byte) 3,
        (byte) 4,
        (byte) 5,
        (byte) 6,
        (byte) 7,
        (byte) 8,
        (byte) 9,
        (byte) 10
      };
      int iterationCount = 1000;
      char[] passPhrase = passw.ToCharArray();
      byte[] encoded2 = EncryptedPrivateKeyInfoFactory.CreateEncryptedPrivateKeyInfo(algorithm, passPhrase, salt, iterationCount, @private).ToAsn1Object().GetEncoded();
      FileStream fileStream2 = new FileStream(PrivateKeyFilename, FileMode.Create, FileAccess.Write);
      fileStream2.Write(encoded2, 0, encoded2.Length);
      fileStream2.Close();
    }

    public string DecodePublicKey(string filename)
    {
      AsymmetricKeyParameter key = PublicKeyFactory.CreateKey(File.ReadAllBytes(filename));
      TextWriter textWriter = (TextWriter) new StreamWriter((Stream) new MemoryStream());
      StringWriter stringWriter = new StringWriter();
      new PemWriter((TextWriter) stringWriter).WriteObject((object) key);
      stringWriter.Close();
      return stringWriter.ToString();
    }

    public string DecodePrivateKey(string filename, string passw)
    {
      byte[] encryptedPrivateKeyInfoData = File.ReadAllBytes(filename);
      AsymmetricKeyParameter asymmetricKeyParameter = PrivateKeyFactory.DecryptKey(passw.ToCharArray(), encryptedPrivateKeyInfoData);
      TextWriter textWriter = (TextWriter) new StreamWriter((Stream) new MemoryStream());
      StringWriter stringWriter = new StringWriter();
      new PemWriter((TextWriter) stringWriter).WriteObject((object) asymmetricKeyParameter);
      stringWriter.Close();
      return stringWriter.ToString();
    }

    public bool readKey(string pemFileName, string password)
    {
      try
      {
        StreamReader streamReader = new StreamReader(pemFileName);
        string pemString = streamReader.ReadToEnd();
        streamReader.Close();
        PemReader pemReader = new PemReader((TextReader) new StringReader(this.GetStringFromPEM(pemString, true)), (IPasswordFinder) new Certificate.Password(password.ToCharArray()));
        try
        {
          if (!(pemReader.ReadObject() is AsymmetricCipherKeyPair))
            return false;
        }
        catch (Exception ex)
        {
          return false;
        }
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    private AsymmetricCipherKeyPair readKey1(string filename, string password)
    {
      try
      {
        StreamReader streamReader = new StreamReader(filename);
        string pemString = streamReader.ReadToEnd();
        streamReader.Close();
        PemReader pemReader = new PemReader((TextReader) new StringReader(this.GetStringFromPEM(pemString, true)), (IPasswordFinder) new Certificate.Password(password.ToCharArray()));
        try
        {
          return pemReader.ReadObject() as AsymmetricCipherKeyPair;
        }
        catch (Exception ex)
        {
          return (AsymmetricCipherKeyPair) null;
        }
      }
      catch (Exception ex)
      {
        return (AsymmetricCipherKeyPair) null;
      }
    }

    public string GetStringFromPEM(string pemString, bool iskey)
    {
      int startIndex;
      int length;
      if (iskey)
      {
        startIndex = pemString.IndexOf("-----BEGIN RSA PRIVATE KEY-----");
        length = pemString.IndexOf("-----END RSA PRIVATE KEY-----") + "-----END RSA PRIVATE KEY-----".Length - startIndex;
      }
      else
      {
        string str1 = "-----BEGIN CERTIFICATE-----";
        string str2 = "-----END CERTIFICATE-----";
        startIndex = pemString.IndexOf(str1);
        length = pemString.IndexOf(str2) + str2.Length;
      }
      return pemString.Substring(startIndex, length);
    }

    public void SignXml(string XMLFileName, string SignedXMLFileName, string PrivateKeyPassword, string CertificateFileName)
    {
      StreamReader streamReader = new StreamReader(XMLFileName);
      string xml1 = streamReader.ReadToEnd();
      streamReader.Close();
      XmlDocument document = new XmlDocument();
      document.PreserveWhitespace = false;
      document.LoadXml(xml1);
      SignedXml signedXml = new SignedXml(document);
      RSA rsa = DotNetUtilities.ToRSA((RsaPrivateCrtKeyParameters) this.readKey1(CertificateFileName, PrivateKeyPassword).Private);
      signedXml.SigningKey = (AsymmetricAlgorithm) rsa;
      Reference reference = new Reference();
      reference.Uri = "";
      XmlDsigEnvelopedSignatureTransform signatureTransform = new XmlDsigEnvelopedSignatureTransform();
      reference.AddTransform((Transform) signatureTransform);
      XmlDsigC14NTransform dsigC14Ntransform = new XmlDsigC14NTransform();
      reference.AddTransform((Transform) dsigC14Ntransform);
      signedXml.AddReference(reference);
      KeyInfo keyInfo = new KeyInfo();
      System.Security.Cryptography.X509Certificates.X509Certificate fromCertFile = System.Security.Cryptography.X509Certificates.X509Certificate.CreateFromCertFile(CertificateFileName);
      keyInfo.AddClause((KeyInfoClause) new KeyInfoX509Data(fromCertFile));
      signedXml.KeyInfo = keyInfo;
      signedXml.ComputeSignature();
      XmlElement xml2 = signedXml.GetXml();
      document.DocumentElement.AppendChild(document.ImportNode((XmlNode) xml2, true));
      if (document.FirstChild is XmlDeclaration)
        document.RemoveChild(document.FirstChild);
      XmlTextWriter xmlTextWriter = new XmlTextWriter(SignedXMLFileName, (Encoding) new UTF8Encoding(false));
      document.WriteTo((XmlWriter) xmlTextWriter);
      xmlTextWriter.Close();
    }

    public void SignXmlText(string XMLFile, string SignedXMLFileName, string PrivateKeyPassword, string CertificateFileName)
    {
      XmlDocument document = new XmlDocument();
      document.PreserveWhitespace = false;
      document.LoadXml(XMLFile);
      SignedXml signedXml = new SignedXml(document);
      RSA rsa = DotNetUtilities.ToRSA((RsaPrivateCrtKeyParameters) this.readKey1(CertificateFileName, PrivateKeyPassword).Private);
      signedXml.SigningKey = (AsymmetricAlgorithm) rsa;
      Reference reference = new Reference();
      reference.Uri = "";
      XmlDsigEnvelopedSignatureTransform signatureTransform = new XmlDsigEnvelopedSignatureTransform();
      reference.AddTransform((Transform) signatureTransform);
      XmlDsigC14NTransform dsigC14Ntransform = new XmlDsigC14NTransform();
      reference.AddTransform((Transform) dsigC14Ntransform);
      signedXml.AddReference(reference);
      KeyInfo keyInfo = new KeyInfo();
      System.Security.Cryptography.X509Certificates.X509Certificate fromCertFile = System.Security.Cryptography.X509Certificates.X509Certificate.CreateFromCertFile(CertificateFileName);
      keyInfo.AddClause((KeyInfoClause) new KeyInfoX509Data(fromCertFile));
      signedXml.KeyInfo = keyInfo;
      signedXml.ComputeSignature();
      XmlElement xml = signedXml.GetXml();
      document.DocumentElement.AppendChild(document.ImportNode((XmlNode) xml, true));
      if (document.FirstChild is XmlDeclaration)
        document.RemoveChild(document.FirstChild);
      XmlTextWriter xmlTextWriter = new XmlTextWriter(SignedXMLFileName, (Encoding) new UTF8Encoding(false));
      document.WriteTo((XmlWriter) xmlTextWriter);
      xmlTextWriter.Close();
    }

    private class Password : IPasswordFinder
    {
      private readonly char[] password;

      public Password(char[] word)
      {
        this.password = (char[]) word.Clone();
      }

      public char[] GetPassword()
      {
        return (char[]) this.password.Clone();
      }
    }
  }
}
