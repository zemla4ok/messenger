using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication2
{
    class User
    {
        public string Name     { get; private set; }
        public string SurName  { get; private set; }
        public string NickName { get; private set; }
        public string Gender   { get; private set; }
        public string Birthday { get; private set; }
        public string Country  { get; private set; }
        public string City     { get; private set; }
        public string Password { get; private set; }

        public User(string pName, string pSurName, string pNickName, string pGender,
            string pBirthday, string pCountry, string pCity, string pPassword)
        {
            this.Name = pName;
            this.SurName = pSurName;
            this.NickName = pNickName;
            this.Gender = pGender;
            this.Birthday = pGender;
            this.Country = pCountry;
            this.City = pCity;
            this.Password = pPassword;
        }
    }
}