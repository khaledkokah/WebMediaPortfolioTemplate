using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using WebMediaPortfolioTemplate.Classes;

//Simple module for encryption/decryption of passwords in database
namespace WebMediaPortfolioTemplate.Classes
{
    public static class Encryption
    {
        public static string Encrypt(string clearText)
        {
            //Change this for any value
            string EncryptionKey = AppConstants.encryptionKey;
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public static string Decrypt(string cipherText)
        {
            try
            {
                string EncryptionKey = AppConstants.encryptionKey;
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
                return cipherText;
            }
            catch
            {
                return "";
            }


        }

        public static string Hash(string clearText)
        {
            //Create the salt value with a cryptographic PRNG
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            //Create the Rfc2898DeriveBytes and get the hash value:
            var pbkdf2 = new Rfc2898DeriveBytes(clearText, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            //Combine the salt and password bytes for later use:
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            //Turn the combined salt+hash into a string for storage
            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            return savedPasswordHash;
        }

        //Va
        public static string ValidatePassword(string username, string password)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(AppConstants.connStr))
                {
                    using (SqlCommand cmd = new SqlCommand("", con))
                    {
                        cmd.CommandText = "Select Password FROM COM_USER WHERE USERNAME=@USERNAME";
                        cmd.Parameters.AddWithValue("@USERNAME", username);
                        DataTable dt = DBManager.FillDataTable(cmd);
                        if (dt.Rows.Count == 0) return "";

                        /* Fetch the stored value */
                        string savedPasswordHash = dt.Rows[0]["Password"].ToString();
                        /* Extract the bytes */
                        byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
                        /* Get the salt */
                        byte[] salt = new byte[16];
                        Array.Copy(hashBytes, 0, salt, 0, 16);
                        /* Compute the hash on the password the user entered */
                        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
                        byte[] hash = pbkdf2.GetBytes(20);
                        /* Compare the results */
                        for (int i = 0; i < 20; i++)
                            if (hashBytes[i + 16] != hash[i])
                                return "";

                        return "1";

                    }

                }

            }
            catch (Exception ex)
            {
                return "";
            }
        }

        //Decrypt the user password
        public static string GetPass(string username, string password)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(AppConstants.connStr))
                {
                    using (SqlCommand cmd = new SqlCommand("", con))
                    {
                        cmd.CommandText = "Select Password FROM COM_USER WHERE USERNAME=@USERNAME";
                        cmd.Parameters.AddWithValue("@USERNAME", username);
                        DataTable dt = DBManager.FillDataTable(cmd);
                        if (dt.Rows.Count == 0) return "";

                        /* Fetch the stored value */
                        string savedPasswordHash = dt.Rows[0]["Password"].ToString();
                        /* Extract the bytes */
                        byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
                        /* Get the salt */
                        byte[] salt = new byte[16];
                        Array.Copy(hashBytes, 0, salt, 0, 16);
                        /* Compute the hash on the password the user entered */
                        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
                        byte[] hash = pbkdf2.GetBytes(20);
                        /* Compare the results */
                        for (int i = 0; i < 20; i++)
                            if (hashBytes[i + 16] != hash[i])
                                return "";

                        //Correct password
                        return dt.Rows[0]["Password"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

    }
}