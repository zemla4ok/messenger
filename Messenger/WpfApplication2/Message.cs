using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication2
{
    class Message
    {
        public string ChatName { get; private set; }
        public string User { get; private set; }
        public string Msg { get; private set; }

        public Message(string pChatName, string pUser, string pMsg)
        {
            this.ChatName = pChatName;
            this.User = pUser;
            this.Msg = pMsg;
        }

        public Message(string data)
        {
            string[] info = data.Split(':');
            this.ChatName = info[0];
            this.User = info[1];
            this.Msg = info[2];
        }

        public override string ToString()
        {
            return ChatName + ":" + User + ":" + Msg;
        }
    }
}
