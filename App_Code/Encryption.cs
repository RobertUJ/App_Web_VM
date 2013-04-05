using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualBasic;
using Microsoft.SqlServer.Server;

namespace App_VM.App_Code
{
    public static class Encryption
    {
        public static string Text_Encryption(string text, int bits, string encryption_key)
        {
            StringBuilder result = new StringBuilder("");

            try
            {
                RSACryptoServiceProvider rsacsp = new RSACryptoServiceProvider(bits);
                rsacsp.FromXmlString(encryption_key);
                int key = bits / 8;
                Byte[] bites = Encoding.UTF32.GetBytes(text);
                int max_length = key - 42;
                int data_length = bites.Length;
                int iterations = data_length / max_length;

                for (int i = 0; i <= iterations; i++)
                {
                    int total_bytes = (data_length - max_length * i > max_length) ? max_length : data_length - max_length * i;
                    Byte[] temp_bytes = new Byte[total_bytes];
                    Buffer.BlockCopy(bites, max_length * i, temp_bytes, 0, temp_bytes.Length);
                    Byte[] encrypted_bytes = rsacsp.Encrypt(temp_bytes, true);
                    Array.Reverse(encrypted_bytes);
                    result.Append(Convert.ToBase64String(encrypted_bytes));
                }

            }
            catch (Exception e)
            {
                result.Append("<Error>" + e.Message + "</Error>");
            }
            return result.ToString();
        }

        public static string Text_Decryption(string text, int bits, string encryption_key)
        {
            string result = String.Empty;
            ArrayList list = new ArrayList();

            try
            {
                RSACryptoServiceProvider rsacsp = new RSACryptoServiceProvider(bits);
                rsacsp.FromXmlString(encryption_key);
                int blockSizeBase64 = (bits / 8 % 3 != 0) ?  (((bits / 8) / 3) * 4) + 4 : ((bits / 8) / 3) * 4;
                int iterations = text.Length / blockSizeBase64;

                for (int i = 0; i < iterations; i++)
                {
                    Byte[] encrypted_bytes = Convert.FromBase64String(text.Substring(blockSizeBase64 * i, blockSizeBase64));
                    Array.Reverse(encrypted_bytes);
                    list.AddRange(rsacsp.Decrypt(encrypted_bytes, true));
                }

            }
            catch (Exception e)
            {
                result = "<Error>" + e.Message + "</Error>";
            }

          
            result = Encoding.UTF32.GetString((Byte[])list.ToArray(typeof(Byte)));

            return result;
        }
      
    }
}