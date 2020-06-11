using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TechVault
{
   public class AESManager
   {
      //encryption key length in bytes [16,24,32]
      const int KEY_SIZE = 16;

      public static byte[] Encrypt(string plainText, string key)
      {
         byte[] encrypted;
         using ( AesManaged aes = new AesManaged() )
         {  
            byte[] KeyBytes = Encoding.ASCII.GetBytes(AdjustKeyLength(key));

            aes.Key = KeyBytes;
            aes.IV = KeyBytes;
            aes.Padding = PaddingMode.Zeros;
            ICryptoTransform encryptor = aes.CreateEncryptor(KeyBytes, KeyBytes);

            using (MemoryStream ms = new MemoryStream())
            {
                 using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                 {
                     using (StreamWriter sw = new StreamWriter(cs))
                     {
                        sw.WriteLine( plainText );
                     }
                     encrypted = ms.ToArray();
                 }
            }
         }
         return encrypted;
      }

      public static string Decrypt(byte[] encrypted, string key)
      {
         using (AesManaged aes = new AesManaged())
         {
            byte[] KeyBytes = Encoding.ASCII.GetBytes(AdjustKeyLength(key));

            aes.Key = KeyBytes;
            aes.IV = KeyBytes;
            aes.Padding = PaddingMode.Zeros;
            ICryptoTransform decryptor = aes.CreateDecryptor(KeyBytes, KeyBytes);

            using (MemoryStream ms = new MemoryStream(encrypted) )
            {
               using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read ) )
               {
                  using (StreamReader sr = new StreamReader(cs) )
                  {
                     return sr.ReadLine();
                  }
               }
            }
         }
      }

      private static string AdjustKeyLength(string key )
      {
         if ( key.Length < KEY_SIZE )
         {
            key = key.PadRight( KEY_SIZE );
         }
         else if ( key.Length > KEY_SIZE )
         {
            key = key.Remove( KEY_SIZE, ( key.Length - KEY_SIZE ) );
         }

         return key;
      }
   }
}
