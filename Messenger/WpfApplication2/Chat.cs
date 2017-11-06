using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication2
{
    class Chat
    {
        public string ChatWith { get; private set; }
        public string Msg { get; private set; }

        public string ChatName { get; private set; }
        public string thisUserName { get; private set; }

        public Chat(string pThisUser, string pChatWith)
        {
            this.thisUserName = pThisUser;
            this.ChatName = pThisUser;
            string[] str = pChatWith.Split(' ');
            foreach (string s in str)
                this.ChatName += s;
        }

        public Chat(string data)
        {
            string[] UserNames = data.Split(':');
            this.ChatWith = "";
            for (int i = 0; i < UserNames.Count(); i++)
                if (UserNames[i] != "")
                    this.ChatWith += UserNames[i] + " ";
            this.Msg = "";
        }
    }
}
//TODO: in constructor set Msg;