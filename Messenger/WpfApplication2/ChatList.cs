using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication2
{
    class ChatList
    {
        List<Chat> list = new List<Chat>();

        public ChatList(string data)
        {
            string[] chats = data.Split(';');
            for (int i = 0; i < chats.Count() - 1; i++)
                this.list.Add(new Chat(chats[i]));
        }

        public List<Chat> GetChats()
        {
            return this.list;
        }
    }
}