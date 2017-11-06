using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApplication2
{
    static class ServerObject
    {
        const int port = 8888;
        const string address = "127.0.0.1";
        private static TcpClient client = new TcpClient(address, port);

        public static void SendMessage(string id, string message)
        {
            try
            {
                if (client == null)
                    throw new Exception("Can't connetct to server");
                NetworkStream stream = client.GetStream();
                byte[] data = Encoding.Unicode.GetBytes(id + message);
                stream.Write(data, 0, data.Length);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static string GetMessage()
        {
            string message = "";
            try
            {
                if (client == null)
                    throw new Exception("Can't connetct to server");
                NetworkStream stream = client.GetStream();
                byte[] data = new byte[64];
                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                do
                {
                    bytes = stream.Read(data, 0, data.Length);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                } while (stream.DataAvailable);
                message = builder.ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return message;
        }
    }
}