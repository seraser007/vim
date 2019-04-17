using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Windows.Forms;

namespace VIM
{
    public class LicenseCheck
    {
        private string keyA = "DA39A3EE";
        private string keyB = "5E6B";
        private string keyC = "4B0D3255BFEF";
        private string keyD = "95601890AFD80709";
        private static string keyFull = "";
        private static string password = "";
        private static string vector = "";
        
        private string xStrS = "$";
        private string xStrPer="%";
        private string xStrAt = "@";
        private string xStrAnd = "&";
        private string xStrNum = "#";

        private static string startName = "";
        private static string startDate = "";

        public LicenseCheck(string keyStr)
        {
            if (keyStr.Length == 0)
                keyFull = keyA + keyB + keyC + keyD;
            else
                keyFull = keyStr;

            startName = xStrS + xStrS + xStrPer + xStrPer + xStrAt + xStrAt; //"$$%%@@"
            startDate = xStrPer + xStrPer + xStrAnd + xStrAnd + xStrNum + xStrNum; //"%%&&##"

            password = CreatePassword(keyFull);
            vector = CreateVector(keyFull);

        }

        private string CreatePassword(string key)
        {
            //Create password

            PasswordDeriveBytes pword = new PasswordDeriveBytes(key, null);

            byte[] passKey = pword.GetBytes(256 / 8);

            return ByteArrayToHexString(passKey);
        }

        private string CreateVector(string key)
        {
            //Create vector

            string vectorStr = Reverse(key);

            PasswordDeriveBytes pword = new PasswordDeriveBytes(vectorStr, null);

            byte[] passKey = pword.GetBytes(16);

            return ByteArrayToHexString(passKey);
        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public void ReadLicense(string LicenseString, ref string licenseOwner, ref DateTime licenseExpire)
        {
            string clearLicense = LicenseString.Substring(0, 17) + LicenseString.Substring(19, 10) + LicenseString.Substring(31);

            string decript = "";
            try
            {
                decript = DecryptStringFromBytes(Convert.FromBase64String(clearLicense), HexStringToByteArray(password), HexStringToByteArray(vector));
            }
            catch
            {
                MessageBox.Show("Illegal license key", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int psnOwner = decript.IndexOf(startName);
            int psnDate = decript.IndexOf(startDate);

            if ((psnOwner < 0) || (psnDate < 0))
                return;

            string ownerStr = decript.Substring(psnOwner + 6, psnDate - psnOwner - 6);

            string dateStr = decript.Substring(psnDate + 6);

            dateStr = dateStr.Substring(0, 4) + "-" + dateStr.Substring(4, 2) + "-" + dateStr.Substring(6, 2);

            licenseOwner = ownerStr;
            licenseExpire = Convert.ToDateTime(dateStr);

        }

        private static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Rijndael object
            // with the specified key and IV.
            using (Rijndael rijAlg = Rijndael.Create())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }

        public static string ByteArrayToHexString(byte[] Bytes)
        {
            StringBuilder Result = new StringBuilder(Bytes.Length * 2);
            string HexAlphabet = "0123456789ABCDEF";

            foreach (byte B in Bytes)
            {
                Result.Append(HexAlphabet[(int)(B >> 4)]);
                Result.Append(HexAlphabet[(int)(B & 0xF)]);
            }

            return Result.ToString();
        }

        public static byte[] HexStringToByteArray(string Hex)
        {
            byte[] Bytes = new byte[Hex.Length / 2];
            int[] HexValue = new int[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 
                0x06, 0x07, 0x08, 0x09, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

            for (int x = 0, i = 0; i < Hex.Length; i += 2, x += 1)
            {
                Bytes[x] = (byte)(HexValue[Char.ToUpper(Hex[i + 0]) - '0'] << 4 |
                                  HexValue[Char.ToUpper(Hex[i + 1]) - '0']);
            }

            return Bytes;
        }

    }
}
