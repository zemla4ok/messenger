using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication2
{
    class User
    {
        public string Name      { get; private set; }
        public string SurName   { get; private set; }
        public string NickName  { get; private set; }
        public string Gender    { get; private set; }
        public string Birthday  { get; private set; }
        public string Country   { get; private set; }
        public string City      { get; private set; }
        public string Password  { get; private set; }

        //constructor
        public User(string pName, string pSurName, string pNickName, string pGender,
            string pBirthday, string pCountry, string pCity, string pPassword)
        {
            this.Name = pName;
            this.SurName = pSurName;
            this.NickName = pNickName;
            this.Gender = pGender;
            this.Birthday = pBirthday;
            this.Country = pCountry;
            this.City = pCity;
            this.Password = Crypt.CryptPassword(pPassword);
        }

        public User(string name, string password)
        {
            this.NickName = name;
            this.Password = Crypt.CryptPassword(password);
        }

        public User(string str)
        {
            string[] strings = str.Split(':');
            this.Name = strings[0];
            this.SurName = strings[1];
            this.NickName = strings[2];
            this.Gender = strings[3];
            this.Birthday = strings[4];
            this.Country = strings[5];
            this.City = strings[6];
            this.Password = strings[7];
        }

        public override string ToString()
        {
            return Name + ":" + SurName + ":" + NickName + ":" + Gender + ":" + Birthday + ":" + Country + ":" + City + ":" + Password;
        }

        public string GetLogAndPass()
        {
            return this.NickName + ":" + this.Password;
        }

        public void ChangePass(string pass)
        {
            this.Password = Crypt.CryptPassword(pass);
        }
    }
}