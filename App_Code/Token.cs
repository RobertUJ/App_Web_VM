using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace App_VM.App_Code
{
    public class Token
    {
        public Token()
        {

        }

        private String[] Letters =
        {
            String.Empty,
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "L",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T"
        };

        public string random_key(bool isPrimary=true)
        {
            string result = String.Empty;
            Random random = null;
            int letter = 1;
            int number = 1;
          
            Byte[] seed = new Byte[3];
            RNGCryptoServiceProvider rngcsp = new RNGCryptoServiceProvider();
            rngcsp.GetBytes(seed);
            random = new Random(BitConverter.ToInt32(seed, 0));
            letter = (isPrimary) ? random.Next(1, 11) : random.Next(11, 21); // Letter.lenght = 20 + 1    
            number = (isPrimary)? random.Next(1, 11) : 11;

            result = Letters[letter] + number.ToString();

            return result;
        }


    }
}