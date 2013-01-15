using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Autumn.Mvc.Infrastructure.Resources;

namespace Autumn.Mvc.Infrastructure.Helpers
{
	public class RsaHelper
	{
		//private static string _privateKey = "";
		//private static string _publicKey = "";
		private static UnicodeEncoding _encoder = new UnicodeEncoding();

		public static string Decrypt(string data)
		{
			var rsa = new RSACryptoServiceProvider();
			var dataArray = data.Split(new char[] {',' });
			byte[] dataByte = new byte[dataArray.Length];

			for(int i = 0; i < dataArray.Length; i++)
			{
				dataByte[i] = Convert.ToByte(dataArray[i]);
			}

			rsa.FromXmlString(Strings.RSAPrivateKey);
			var decryptedByte = rsa.Decrypt(dataByte, false);
			return _encoder.GetString(decryptedByte);
		}

		public static string Encrypt(string data)
		{
			var rsa = new RSACryptoServiceProvider();
			rsa.FromXmlString(Strings.RSAPublicKey);

			var dataToEncrypt = _encoder.GetBytes(data);
			var encryptedByteArray = rsa.Encrypt(dataToEncrypt, false).ToArray();
			var length = encryptedByteArray.Count();
			var item = 0;
			var sb = new StringBuilder();
			foreach (var x in encryptedByteArray)
			{
				item++;
				sb.Append(x);

				if (item < length)
					sb.Append(",");
			}

			return sb.ToString();
		}
	}
}
