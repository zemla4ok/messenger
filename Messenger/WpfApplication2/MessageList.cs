using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication2
{
    class MessageList
    {
        List<Message> list = new List<Message>();

        public MessageList(string data)
        {
            string[] messages = data.Split(';');
            for (int i = 0; i < messages.Count() - 1; i++)
                this.list.Add(new Message(messages[i]));
        }


    }
}
