using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApplication2
{
    class UserList
    {
        List<User> lst = new List<User>();

        public UserList(List<User> pList)
        {
            this.lst = pList;
        }

        public UserList(string data)
        {
            try
            {
                string[] users = data.Split(';');

                for (int i = 0; i < users.Count() - 1; i++)
                    this.lst.Add(new User(users[i]));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public List<User> GetUsers()
        {
            return lst;
        }

        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < lst.Count; i++)
                result += lst[i].NickName + ";";
            return result.Substring(0, result.Count() - 1);
        }
    }
}
