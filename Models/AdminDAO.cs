using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ListBlog.Models
{
    public class AdminDAO
    {
        private listblogEntities7 db = new listblogEntities7();

        public Admin GetAdmin(string login, string password)
        {
            var admin = db.Admins.Where(x => x.Login.Equals(login) && x.Password.Equals(password)).FirstOrDefault();
            return admin;
        }


        public void ChangePassword(string login, string password)
        {
            var items = db.Admins.Where(x => x.Login.Equals(login)).FirstOrDefault();
            items.Password = GetMD5(password);
            db.SaveChanges();
        }

        public string GetMD5(string text)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(Encoding.UTF8.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                str.Append(result[i].ToString("x2"));
            }
            return str.ToString();

        }
        

    }
}