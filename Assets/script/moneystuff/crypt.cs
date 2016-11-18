using UnityEngine;
using System.Collections;
using System.IO;  
using System.Text;  
using System.Security.Cryptography;  
public class crypt : MonoBehaviour {
	public byte[] key2 = new byte[16];
	public byte[] key1 = new byte[16];
	public  byte[] IV = new byte[16];
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void checkDate()
	{
		byte[] haveData = loadcrypto ();

		if (haveData == null)
			haveData=DESDecrypt(haveData,key2,IV);
		else
			return ;
		haveData = DESEncrypt (haveData, key1, IV);
		
	}	 
	private byte[]  loadcrypto()
	{
		byte[] ss = new byte[16];
		return ss;
	}
		/// <summary>  
		/// 生成RSA私钥 公钥  
		/// </summary>  
		/// <param name="privateKey"></param>  
		/// <param name="publicKey"></param>  
		public static void RSAGenerateKey(ref string privateKey, ref string publicKey)  
		{  
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();  
			privateKey = rsa.ToXmlString(true);  
			publicKey = rsa.ToXmlString(false);  
		}  

		/// <summary>  
		/// 用RSA公钥 加密  
		/// </summary>  
		/// <param name="data"></param>  
		/// <param name="publicKey"></param>  
		/// <returns></returns>  
		public static byte[] RSAEncrypt(byte[] data, string publicKey)  
		{  
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();  
			rsa.FromXmlString(publicKey);  

			byte[] encryptData = rsa.Encrypt(data, false);  

			return encryptData;  
		}  


		/// <summary>  
		/// 用RSA私钥 解密  
		/// </summary>  
		/// <param name="data"></param>  
		/// <param name="privateKey"></param>  
		/// <returns></returns>  
		public static byte[] RSADecrypt(byte[] data, string privateKey)  
		{  
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();  
			rsa.FromXmlString(privateKey);  
			byte[] decryptData = rsa.Decrypt(data, false);  
			return decryptData;  
		}  

		/// <summary>  
		/// DES加密  
		/// </summary>  
		/// <param name="data">源数据</param>  
		/// <param name="desrgbKey"></param>  
		/// <param name="desrgbIV"></param>  
		/// <returns></returns>  
		public static byte[] DESEncrypt(byte[] data,byte[] desrgbKey,byte[] desrgbIV)  
		{  
			DESCryptoServiceProvider des = new DESCryptoServiceProvider();  
			MemoryStream memoryStream = new MemoryStream();  
			CryptoStream cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(desrgbKey, desrgbIV), CryptoStreamMode.Write);  
			cryptoStream.Write(data, 0, data.Length);  
			cryptoStream.FlushFinalBlock();  
			return memoryStream.ToArray();  
		}  

		/// <summary>  
		/// DES解密  
		/// </summary>  
		/// <param name="data">源数据</param>  
		/// <param name="desrgbKey"></param>  
		/// <param name="desrgbIV"></param>  
		/// <returns></returns>  
		public static byte[] DESDecrypt(byte[] data, byte[] desrgbKey, byte[] desrgbIV)  
		{  
			DESCryptoServiceProvider des = new DESCryptoServiceProvider();  
			MemoryStream memoryStream = new MemoryStream();  
			CryptoStream cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(desrgbKey, desrgbIV), CryptoStreamMode.Write);  
			cryptoStream.Write(data, 0, data.Length);  
			cryptoStream.FlushFinalBlock();  
			return memoryStream.ToArray();  
		}  
	}  
