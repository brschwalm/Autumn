using System;
using System.Diagnostics;
using System.Security.Cryptography;
using Autumn.Mvc.Infrastructure.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Autumn.Mvc.Infrastructure.Tests
{
	[TestClass]
	public class RSAHelperTests
	{
		//Use this test to generate a new public/private keypair
		//[TestMethod]
		public void RSAParameters()
		{
			var rsa = new RSACryptoServiceProvider();
			var privateParameters = rsa.ExportParameters(true);
			var publicParameters = rsa.ExportParameters(false);

			Debug.WriteLine(rsa.ToXmlString(true));
			Debug.WriteLine(rsa.ToXmlString(false));
		}

		[TestMethod]
		public void VerifyToken()
		{
			string token = "My User Token";
			var encryptedToken = RsaHelper.Encrypt(token);
			var decryptedToken = RsaHelper.Decrypt(encryptedToken);

			Assert.AreEqual(token, decryptedToken);
		}

	}
}
