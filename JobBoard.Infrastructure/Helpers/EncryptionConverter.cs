using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class EncryptionConverter : ValueConverter<string?, string?>
{
	public EncryptionConverter()
		: base(
			plainText => Encrypt(plainText),
			encryptedText => Decrypt(encryptedText))
	{
	}

	// this key for testing never put your encryption key in the code

	private static readonly string key = "71c577c4fe8647fca39accfcccc78a03"; // Must be 32 bytes for AES-256

	private static string Encrypt(string plainText)
	{
		if (plainText == null) return null;

		using var aes = Aes.Create();
		aes.Key = Encoding.UTF8.GetBytes(key);
		aes.GenerateIV();

		using var encryptor = aes.CreateEncryptor();
		var plainBytes = Encoding.UTF8.GetBytes(plainText);
		var cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

		// Combine IV + CipherText
		var result = new byte[aes.IV.Length + cipherBytes.Length];
		Buffer.BlockCopy(aes.IV, 0, result, 0, aes.IV.Length);
		Buffer.BlockCopy(cipherBytes, 0, result, aes.IV.Length, cipherBytes.Length);

		return Convert.ToBase64String(result);
	}

	private static string Decrypt(string encryptedText)
	{
		if (encryptedText == null) return null;

		var fullCipher = Convert.FromBase64String(encryptedText);
		using var aes = Aes.Create();
		aes.Key = Encoding.UTF8.GetBytes(key);

		// Extract IV
		var iv = new byte[aes.BlockSize / 8];
		var cipher = new byte[fullCipher.Length - iv.Length];
		Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
		Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, cipher.Length);

		aes.IV = iv;

		using var decryptor = aes.CreateDecryptor();
		var plainBytes = decryptor.TransformFinalBlock(cipher, 0, cipher.Length);
		return Encoding.UTF8.GetString(plainBytes);
	}
}
