using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
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
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
namespace Kalkulator1
{
	public class Certificate
	{
		private class Password : IPasswordFinder
		{
			private readonly char[] password;
			public Password(char[] word)
			{
				this.password = (char[])word.Clone();
			}
			public char[] GetPassword()
			{
				return (char[])this.password.Clone();
			}
		}
		private string keys;
		private string generatedCSR;
		private System.Security.Cryptography.X509Certificates.X509Certificate cert;
		private string path;
		public Certificate()
		{
			this.keys = "";
			this.generatedCSR = "";
			System.Security.Cryptography.X509Certificates.X509Certificate cert = new System.Security.Cryptography.X509Certificates.X509Certificate();
			this.path = System.IO.Path.GetTempPath() + "KBW";
		}
		public Certificate(string filename)
		{
			this.keys = "";
			this.generatedCSR = "";
			this.cert = new System.Security.Cryptography.X509Certificates.X509Certificate(filename);
			this.path = System.IO.Path.GetTempPath() + "KBW";
		}
		public string getCSR()
		{
			return this.generatedCSR;
		}
		public bool isActiveLicense(string license)
		{
			bool response = false;
			try
			{
				System.Security.Cryptography.X509Certificates.X509Certificate certtmp = new System.Security.Cryptography.X509Certificates.X509Certificate(license);
				System.DateTime a = new System.DateTime(1, 1, 1, 0, 0, 0);
				System.DateTime fromDate = System.Convert.ToDateTime(certtmp.GetEffectiveDateString());
				if (fromDate == a)
				{
					System.Threading.Thread.Sleep(1000);
					fromDate = System.Convert.ToDateTime(certtmp.GetEffectiveDateString());
				}
				System.DateTime toDate = System.Convert.ToDateTime(certtmp.GetExpirationDateString());
				if (toDate == a)
				{
					System.Threading.Thread.Sleep(1000);
					toDate = System.Convert.ToDateTime(certtmp.GetEffectiveDateString());
				}
				int result = System.DateTime.Compare(fromDate, System.DateTime.Now);
				int result2 = System.DateTime.Compare(System.DateTime.Now, toDate);
				if (result <= 0 && result2 <= 0)
				{
					response = true;
				}
			}
			catch (System.Security.Cryptography.CryptographicException)
			{
			}
			return response;
		}
		public void generateCSR(string electoralEampaign, string user, string password)
		{
			X509Name name = new X509Name("C=PL, ST=Mazowieckie, L=Warszawa, O=KBW, E=-, OU=" + electoralEampaign + ", CN=" + user);
			RsaKeyPairGenerator keyGenerator = new RsaKeyPairGenerator();
			RsaKeyGenerationParameters param = new RsaKeyGenerationParameters(BigInteger.ValueOf(3L), new SecureRandom(), 2048, 25);
			keyGenerator.Init(param);
			AsymmetricCipherKeyPair ackp = keyGenerator.GenerateKeyPair();
			Pkcs10CertificationRequest csr = new Pkcs10CertificationRequest("SHA1WITHRSA", name, ackp.Public, null, ackp.Private);
			System.Text.StringBuilder CSRPem = new System.Text.StringBuilder();
			PemWriter CSRPemWriter = new PemWriter(new System.IO.StringWriter(CSRPem));
			CSRPemWriter.WriteObject(csr);
			CSRPemWriter.Writer.Flush();
			this.generatedCSR = CSRPem.ToString();
			System.Text.StringBuilder PrivateKeyPem = new System.Text.StringBuilder();
			PemWriter PrivateKeyPemWriter = new PemWriter(new System.IO.StringWriter(PrivateKeyPem));
			PrivateKeyPemWriter.WriteObject(ackp.Private);
			CSRPemWriter.Writer.Flush();
			this.keys = PrivateKeyPem.ToString();
			AsymmetricKeyParameter publicKey = ackp.Public;
			AsymmetricKeyParameter privateKey = ackp.Private;
			System.IO.StringWriter sw = new System.IO.StringWriter();
			PemWriter pw = new PemWriter(sw);
			pw.WriteObject(privateKey, "AES-256-CBC", password.ToCharArray(), new SecureRandom());
			pw.Writer.Close();
			this.keys = sw.ToString();
			if (!System.IO.Directory.Exists(this.path + "\\Licenses"))
			{
				try
				{
					System.IO.Directory.CreateDirectory(this.path + "\\Licenses");
				}
				catch (System.ArgumentNullException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
				}
				catch (System.ArgumentException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
				}
				catch (System.UnauthorizedAccessException)
				{
					MessageBox.Show("Nie masz uprawnień do tworzenia katalogów. Otwórz aplikacje jako adnimistrator.", "Uwaga");
				}
				catch (System.IO.PathTooLongException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
				}
				catch (System.IO.DirectoryNotFoundException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
				}
				catch (System.NotSupportedException)
				{
					MessageBox.Show("Nieprawidłowy format ścieżki. Nie można utworzyć katalogu \"Licenses\"", "Error");
				}
				catch (System.IO.IOException)
				{
					MessageBox.Show("Nieprawidłowa ścieżka. Nie można utworzyć katalogu \"Licenses\"", "Error");
				}
			}
			string namefile = "";
			try
			{
				int tmp = System.IO.Directory.GetFiles(this.path + "\\Licenses", electoralEampaign.Replace("/", "_") + "_" + user + "*.csr").Length + 1;
				if (tmp > 1)
				{
					string num = tmp.ToString();
					namefile = string.Concat(new string[]
					{
						electoralEampaign.Replace("/", "_"),
						"_",
						user,
						" ",
						num
					});
				}
				else
				{
					namefile = electoralEampaign.Replace("/", "_") + "_" + user;
				}
			}
			catch (System.Exception)
			{
				namefile = electoralEampaign.Replace("/", "_") + "_" + user;
			}
			try
			{
				System.IO.StreamWriter sw2 = new System.IO.StreamWriter(this.path + "\\Licenses\\" + namefile + ".csr", false);
				sw2.Write(this.generatedCSR);
				sw2.Close();
				sw2 = new System.IO.StreamWriter(this.path + "\\Licenses\\" + namefile + ".key", false);
				sw2.Write(this.keys);
				sw2.Close();
			}
			catch (System.ArgumentNullException)
			{
				MessageBox.Show("Błędna ścieżka", "Error");
			}
			catch (System.ArgumentException)
			{
				MessageBox.Show("Błędna ścieżka", "Error");
			}
			catch (System.IO.DirectoryNotFoundException)
			{
				MessageBox.Show("Nie znaleziono katalogu", "Error");
			}
			catch (System.IO.PathTooLongException)
			{
				MessageBox.Show("Zbyt długa ścieżka do katalogu", "Error");
			}
			catch (System.IO.IOException)
			{
				MessageBox.Show("Podano ścieżke do pliku zamiast katalogu", "Error");
			}
			catch (System.UnauthorizedAccessException)
			{
				MessageBox.Show("Program nie posiada uprawnień do zapisywania plików. Otwórz aplikacje jako Administrator", "Error");
			}
			catch (System.Exception e)
			{
				MessageBox.Show("Nie można zapisać pliku. Orginal error: " + e.Message, "Error");
			}
		}
		public bool isActiveLicense()
		{
			bool response = false;
			try
			{
				System.DateTime fromDate = System.Convert.ToDateTime(this.cert.GetEffectiveDateString());
				System.DateTime toDate = System.Convert.ToDateTime(this.cert.GetExpirationDateString());
				int result = System.DateTime.Compare(fromDate, System.DateTime.Now);
				int result2 = System.DateTime.Compare(System.DateTime.Now, toDate);
				if (result <= 0 && result2 <= 0)
				{
					response = true;
				}
			}
			catch (System.Security.Cryptography.CryptographicException)
			{
			}
			return response;
		}
		public string getSubject()
		{
			return this.cert.Subject;
		}
		public Pkcs10CertificationRequest LoadCertificate(string pemFilenameCSR)
		{
			System.IO.StreamReader textReader = System.IO.File.OpenText(pemFilenameCSR);
			PemReader reader = new PemReader(textReader);
			return reader.ReadObject() as Pkcs10CertificationRequest;
		}
		public void RsaKeysGenerate(string PrivateKeyFilename, string PublicKeyFilename, string passw)
		{
			RsaKeyPairGenerator keyGenerator = new RsaKeyPairGenerator();
			RsaKeyGenerationParameters param = new RsaKeyGenerationParameters(BigInteger.ValueOf(3L), new SecureRandom(), 1024, 25);
			keyGenerator.Init(param);
			AsymmetricCipherKeyPair keyPair = keyGenerator.GenerateKeyPair();
			AsymmetricKeyParameter publicKey = keyPair.Public;
			AsymmetricKeyParameter privateKey = keyPair.Private;
			SubjectPublicKeyInfo publicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKey);
			Asn1Object aobject = publicKeyInfo.ToAsn1Object();
			byte[] pubInfoByte = aobject.GetEncoded();
			System.IO.FileStream fs = new System.IO.FileStream(PublicKeyFilename, System.IO.FileMode.Create, System.IO.FileAccess.Write);
			fs.Write(pubInfoByte, 0, pubInfoByte.Length);
			fs.Close();
			string alg = "1.2.840.113549.1.12.1.3";
			byte[] salt = new byte[]
			{
				1,
				2,
				3,
				4,
				5,
				6,
				7,
				8,
				9,
				10
			};
			int count = 1000;
			char[] password = passw.ToCharArray();
			EncryptedPrivateKeyInfo enPrivateKeyInfo = EncryptedPrivateKeyInfoFactory.CreateEncryptedPrivateKeyInfo(alg, password, salt, count, privateKey);
			byte[] priInfoByte = enPrivateKeyInfo.ToAsn1Object().GetEncoded();
			fs = new System.IO.FileStream(PrivateKeyFilename, System.IO.FileMode.Create, System.IO.FileAccess.Write);
			fs.Write(priInfoByte, 0, priInfoByte.Length);
			fs.Close();
		}
		public string DecodePublicKey(string filename)
		{
			byte[] dataKey = System.IO.File.ReadAllBytes(filename);
			AsymmetricKeyParameter asp = PublicKeyFactory.CreateKey(dataKey);
			System.IO.MemoryStream ms = new System.IO.MemoryStream();
			System.IO.TextWriter writer = new System.IO.StreamWriter(ms);
			System.IO.StringWriter stWrite = new System.IO.StringWriter();
			PemWriter pmw = new PemWriter(stWrite);
			pmw.WriteObject(asp);
			stWrite.Close();
			return stWrite.ToString();
		}
		public string DecodePrivateKey(string filename, string passw)
		{
			byte[] dataKey = System.IO.File.ReadAllBytes(filename);
			AsymmetricKeyParameter asp = PrivateKeyFactory.DecryptKey(passw.ToCharArray(), dataKey);
			System.IO.MemoryStream ms = new System.IO.MemoryStream();
			System.IO.TextWriter writer = new System.IO.StreamWriter(ms);
			System.IO.StringWriter stWrite = new System.IO.StringWriter();
			PemWriter pmw = new PemWriter(stWrite);
			pmw.WriteObject(asp);
			stWrite.Close();
			return stWrite.ToString();
		}
		public bool readKey(string pemFileName, string password)
		{
			bool result;
			try
			{
				System.IO.StreamReader sr = new System.IO.StreamReader(pemFileName);
				string file = sr.ReadToEnd();
				sr.Close();
				string privateKey = this.GetStringFromPEM(file, true);
				PemReader pr = new PemReader(new System.IO.StringReader(privateKey), new Certificate.Password(password.ToCharArray()));
				try
				{
					AsymmetricCipherKeyPair kp = pr.ReadObject() as AsymmetricCipherKeyPair;
					if (kp == null)
					{
						result = false;
						return result;
					}
				}
				catch (System.Exception)
				{
					result = false;
					return result;
				}
				result = true;
			}
			catch (System.Exception)
			{
				result = false;
			}
			return result;
		}
		private AsymmetricCipherKeyPair readKey1(string filename, string password)
		{
			AsymmetricCipherKeyPair result;
			try
			{
				System.IO.StreamReader sr = new System.IO.StreamReader(filename);
				string file = sr.ReadToEnd();
				sr.Close();
				string privateKey = this.GetStringFromPEM(file, true);
				PemReader pr = new PemReader(new System.IO.StringReader(privateKey), new Certificate.Password(password.ToCharArray()));
				try
				{
					AsymmetricCipherKeyPair kp = pr.ReadObject() as AsymmetricCipherKeyPair;
					result = kp;
				}
				catch (System.Exception)
				{
					result = null;
				}
			}
			catch (System.Exception)
			{
				result = null;
			}
			return result;
		}
		public string GetStringFromPEM(string pemString, bool iskey)
		{
			int start;
			int end;
			if (iskey)
			{
				start = pemString.IndexOf("-----BEGIN RSA PRIVATE KEY-----");
				end = pemString.IndexOf("-----END RSA PRIVATE KEY-----") + "-----END RSA PRIVATE KEY-----".Length - start;
			}
			else
			{
				string header = "-----BEGIN CERTIFICATE-----";
				string footer = "-----END CERTIFICATE-----";
				start = pemString.IndexOf(header);
				end = pemString.IndexOf(footer) + footer.Length;
			}
			return pemString.Substring(start, end);
		}
		public void SignXml(string XMLFileName, string SignedXMLFileName, string PrivateKeyPassword, string CertificateFileName)
		{
			System.IO.StreamReader sr = new System.IO.StreamReader(XMLFileName);
			string file = sr.ReadToEnd();
			sr.Close();
			XmlDocument doc = new XmlDocument();
			doc.PreserveWhitespace = false;
			doc.LoadXml(file);
			SignedXml signedXml = new SignedXml(doc);
			AsymmetricCipherKeyPair kp = this.readKey1(CertificateFileName, PrivateKeyPassword);
			AsymmetricKeyParameter privateKey = kp.Private;
			System.Security.Cryptography.RSA Key = DotNetUtilities.ToRSA((RsaPrivateCrtKeyParameters)privateKey);
			signedXml.SigningKey = Key;
			Reference reference = new Reference();
			reference.Uri = "";
			XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
			reference.AddTransform(env);
			XmlDsigC14NTransform c14t = new XmlDsigC14NTransform();
			reference.AddTransform(c14t);
			signedXml.AddReference(reference);
			KeyInfo keyInfo = new KeyInfo();
			System.Security.Cryptography.X509Certificates.X509Certificate MSCert = System.Security.Cryptography.X509Certificates.X509Certificate.CreateFromCertFile(CertificateFileName);
			keyInfo.AddClause(new KeyInfoX509Data(MSCert));
			signedXml.KeyInfo = keyInfo;
			signedXml.ComputeSignature();
			XmlElement xmlDigitalSignature = signedXml.GetXml();
			doc.DocumentElement.AppendChild(doc.ImportNode(xmlDigitalSignature, true));
			if (doc.FirstChild is XmlDeclaration)
			{
				doc.RemoveChild(doc.FirstChild);
			}
			XmlTextWriter xmltw = new XmlTextWriter(SignedXMLFileName, new System.Text.UTF8Encoding(false));
			doc.WriteTo(xmltw);
			xmltw.Close();
		}
		public void SignXmlText(string XMLFile, string SignedXMLFileName, string PrivateKeyPassword, string CertificateFileName)
		{
			XmlDocument doc = new XmlDocument();
			doc.PreserveWhitespace = false;
			doc.LoadXml(XMLFile);
			SignedXml signedXml = new SignedXml(doc);
			AsymmetricCipherKeyPair kp = this.readKey1(CertificateFileName, PrivateKeyPassword);
			AsymmetricKeyParameter privateKey = kp.Private;
			System.Security.Cryptography.RSA Key = DotNetUtilities.ToRSA((RsaPrivateCrtKeyParameters)privateKey);
			signedXml.SigningKey = Key;
			Reference reference = new Reference();
			reference.Uri = "";
			XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
			reference.AddTransform(env);
			XmlDsigC14NTransform c14t = new XmlDsigC14NTransform();
			reference.AddTransform(c14t);
			signedXml.AddReference(reference);
			KeyInfo keyInfo = new KeyInfo();
			System.Security.Cryptography.X509Certificates.X509Certificate MSCert = System.Security.Cryptography.X509Certificates.X509Certificate.CreateFromCertFile(CertificateFileName);
			keyInfo.AddClause(new KeyInfoX509Data(MSCert));
			signedXml.KeyInfo = keyInfo;
			signedXml.ComputeSignature();
			XmlElement xmlDigitalSignature = signedXml.GetXml();
			doc.DocumentElement.AppendChild(doc.ImportNode(xmlDigitalSignature, true));
			if (doc.FirstChild is XmlDeclaration)
			{
				doc.RemoveChild(doc.FirstChild);
			}
			XmlTextWriter xmltw = new XmlTextWriter(SignedXMLFileName, new System.Text.UTF8Encoding(false));
			doc.WriteTo(xmltw);
			xmltw.Close();
		}
	}
}
