using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace ItViteaVersleutelen
{
    public static class Cipher
    {
        private static byte[] saltByte = Encoding.UTF8.GetBytes("SaltBytes");

        public static string Encrypt(string plainText, string password)
        {
            return EncryptStringToBytes_Aes(plainText, password);
        }
        public static string Decrypt(string encryptedText, string password)
        {
                return DecryptStringFromBytes_Aes(encryptedText, password);
        }

        static string EncryptStringToBytes_Aes(string plainText, string password)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (password == null || password.Length <= 0)
                throw new ArgumentNullException("password");
            string encrypted;

            //try
            //{
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, saltByte);

                // Create an Aes object with the specified key and IV.
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

                    // Create an encryptor to perform the stream transform.
                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    // Create the streams used for encryption.
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        // prepend the IV
                        msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                        msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                //Write all data to the stream.
                                swEncrypt.Write(plainText);
                            }
                            encrypted = Convert.ToBase64String(msEncrypt.ToArray());
                        }
                    }
                }
                /*}
                catch (SystemException e)
                {
                    encrypted = String.Format("SystemException: {0}", e.Message.ToString());
                }
                catch (Exception e)
                {
                    encrypted = String.Format("Exception: {0}", e.Message.ToString());
                }*/
                // Return the encrypted bytes from the memory stream.
                return encrypted;
        }

        static string DecryptStringFromBytes_Aes(string cipherText, string password)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (password == null || password.Length <= 0)
                throw new ArgumentNullException("password");
       
            // Declare the string used to hold the decrypted text.
            string plaintext = null;

            //try
            //{
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, saltByte);

                byte[] bytes = Convert.FromBase64String(cipherText);

                // Create an Aes object with the specified key and IV.
                using (Aes aesAlg = Aes.Create())
                {
                    // Create the streams used for decryption.
                    using (MemoryStream msDecrypt = new MemoryStream(bytes))
                    {
                        aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                        // Get the initialization vector from the encrypted stream
                        aesAlg.IV = ReadByteArray(msDecrypt);

                        // Create a decryptor to perform the stream transform.
                        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {

                                // Read the decrypted bytes from the decrypting stream and place them in a string.
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            /*}
            catch (IOException e)
            {
                plaintext = String.Format("IOException: {0}", e.Message.ToString());
            }
            catch (Exception e)
            {
                plaintext = String.Format("Exception: {0}", e.Message.ToString());
            }*/
            return plaintext;
        }

        private static byte[] ReadByteArray(Stream s)
        {
            byte[] rawLength = new byte[sizeof(int)];
            if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
            {
                throw new SystemException("Stream did not contain properly formatted byte array");
            }

            byte[] buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
            if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
            {
                throw new SystemException("Did not read byte array properly");
            }

            return buffer;
        }
    }
}
