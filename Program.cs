using System;
using System.Data.Common;
using System.Text;
using System.Diagnostics;


namespace Encryption
{
    class Program
    {
        static void Main(string[] args)
        {
            string key = GenerateKey();
            string stringToEncrypt = "Hello, World!Hello, World!Hello, World!Hello, World!Hello, World!Hello, World!Hello, World!Hello, World!";
            Console.WriteLine(stringToEncrypt);
            string encryptedString = EncryptString(stringToEncrypt, key);
            Console.WriteLine(encryptedString);
            string decryptedString = EncryptString(encryptedString, key);
            Console.WriteLine(decryptedString);
            string k = "ASDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD";
            string e = "ASDFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFOKFFFFFFFFFFFFFFF";
            GenTables();
            string a = VignereEncrypt(e, k);
            string b = VignereDecrypt(a, k);
            Console.WriteLine(e);
            Console.WriteLine(a);
            Console.WriteLine(b);
        }
        public static string GenerateKey(int maxLength = 128)
        {
            Random r = new Random();
            return GenerateKey(r.Next, maxLength);
        }
        public static string GenerateVigenereKey(int maxLength = 128)
        {
            Random r = new Random();
            StringBuilder key = new StringBuilder();
            for (int i = 0; i < maxLength; i++)
            {
                key.Append((char)(r.Next('Z'-'A' + 2) + 'A'));
            }
            return key.ToString();
        }
        public static float TestMethod(Action d)
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            d.DynamicInvoke();
            s.Stop();
            long mili = s.ElapsedMilliseconds;
            s.Reset();
            return mili;
        }
        public static string GenerateKey(Func<int, int> r, int maxLength = 128)
        {
            StringBuilder key = new StringBuilder();
            for (int i = 0; i < maxLength; i++)
            {
                key.Append((char)(r(256)));
            }
            return key.ToString();
        }
        public static string EncryptString(string s, string key)
        {
            char[] charArray = s.ToCharArray();
            for (int i = 0; i < s.Length; i++)
            {
                charArray[i] ^= key[i % key.Length];
            }
            return new string(charArray);
        }
        static string VignereEncrypt(string s, string key)
        {
            key = key.ToUpper();
            char[] c = s.ToUpper().ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                int k = 'A' + (c[i] - key[i % key.Length]);
                if (k < 0)
                {
                    k += 27;
                }
                c[i] = (char)(k);
            }
            return new string(c);
        }
        static string VignereDecrypt(string s, string key)
        {

            key = key.ToUpper();
            char[] c = s.ToUpper().ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                int k = c[i] + key[i % key.Length] - 'A';
                if (k < 0)
                {
                    k += 27;
                }
                c[i] = (char)(k);
            }
            return new string(c);
        }
        static char[,] VigenereEncryptTable = new char['Z' - 'A' + 1, 'Z' - 'A' + 1];
        static char[,] VigenereDecryptTable = new char['Z' - 'A' + 1, 'Z' - 'A' + 1];
        static public void GenTables()
        {
            for (int i = 'A'; i <= 'Z'; i++)
            {
                for (int j = 'A'; j <= 'Z'; j++)
                {
                    VigenereEncryptTable[i - 'A', j - 'A'] = (char)(i - j + 'A');
                    VigenereDecryptTable[i - 'A', j - 'A'] = (char)(i + j - 'A');
                }
            }
        }
        public static string VigEncrypt(string s, string k)
        {
            k = k.ToUpper();
            char[] c = s.ToUpper().ToCharArray();
            for (int i = 0; i < s.Length; i++)
            {
                c[i] = VigenereEncryptTable[c[i] - 'A', k[i % k.Length] - 'A'];
            }
            return new string(c);
        }
        public static string VigDecrypt(string s, string k)
        {
            k = k.ToUpper();
            char[] c = s.ToUpper().ToCharArray();
            for (int i = 0; i < s.Length; i++)
            {
                c[i] = VigenereDecryptTable[c[i] - 'A', k[i % k.Length] - 'A'];
            }
            return new string(c);
        }
    }
}
