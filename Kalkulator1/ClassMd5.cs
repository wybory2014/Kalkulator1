using System;
using System.Security.Cryptography;
using System.Text;
namespace Kalkulator1
{
	internal class ClassMd5
	{
		public string CreateMD5Hash(string input)
		{
			System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
			byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
			byte[] hashBytes = md5.ComputeHash(inputBytes);
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int i = 0; i < hashBytes.Length; i++)
			{
				sb.Append(hashBytes[i].ToString("X2"));
			}
			return sb.ToString();
		}
		public string CreateMD5Hash2(string input)
		{
			System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
			byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
			byte[] hashBytes = md5.ComputeHash(inputBytes);
			return System.BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
		}
	}
}
