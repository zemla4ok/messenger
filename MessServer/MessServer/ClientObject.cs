using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MessServer
{
    public class ClientObject
    {
        public TcpClient client;
        public ClientObject(TcpClient tcpClient)
        {
            this.client = tcpClient;
        }

        private string GetMessage(NetworkStream stream)
        {
            byte[] data = new byte[64];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (stream.DataAvailable);
            return builder.ToString();
        }

        private void SendMessage(string message)
        {
            NetworkStream stream = null;
            try
            {
                stream = client.GetStream();
                byte[] data = new byte[64];
                if (message == null)
                    message = "false";
                data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Process()
        {
            NetworkStream stream = null;
            try
            {
                stream = client.GetStream();
                while (true)
                {
                    string message = GetMessage(stream);
                    Console.WriteLine(message);
                    string id = message.Substring(0, 2);
                    message = message.Remove(0, 2);
                    switch (Convert.ToInt32(id))
                    {
                        case 10:        //registration
                            this.SendMessage(Queryes.Registration(message));
                            break;
                        case 11:        //authorisation
                            this.SendMessage(Queryes.Authorisation(message));
                            break;
                        case 12:        //Information About this user
                            this.SendMessage(Queryes.MyInfo(message));
                            break;
                        case 13:        //New password
                            Queryes.UpdatePass(message);
                            break;
                        case 14:        //search users
                            this.SendMessage(Queryes.Search(message));
                            break;
                        case 15:        //creating chat
                            Queryes.CreateChat(message);
                            break;
                        case 16:        //select all users
                            this.SendMessage(Queryes.SelctAllUsers());
                            break;
                        case 17:
                            this.SendMessage(Queryes.SelectChats(message));
                            break;
                        case 18:
                            Queryes.CreateMsg(message);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
                if (client != null)
                    client.Close();
            }

        }
    }
}