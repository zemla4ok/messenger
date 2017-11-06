using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication2
{
    /// <summary>
    /// Логика взаимодействия для ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        Chat thisChat;
        public ChatWindow(string chatWith, string pThisUser)
        {
            InitializeComponent();
            this.ChatWith.Content = chatWith;
            thisChat = new Chat(pThisUser, chatWith);
            scroll.ScrollToBottom();

            //Thread updating = new Thread(this.UpdateMsgs);
        }

        private void UpdateMsgs()
        {
            try
            {
                while(true)
                {
                    ServerObject.SendMessage("19", thisChat.ToString());
                    MessageList list = new MessageList(ServerObject.GetMessage());
                    this.Dispatcher.Invoke((Action)(() =>
                    {

                    }));
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SendMessage(object sender, RoutedEventArgs e)
        {
            if (!Validator.ValidateMessage(this.Message.Text))
            {
                Message msg = new Message(thisChat.ChatName, thisChat.thisUserName, this.Message.Text);
                ServerObject.SendMessage("18", msg.ToString());
                
                TextBlock tb = new TextBlock();
                tb.Style = this.FindResource("2") as Style;
                tb.Text = this.Message.Text;
                Messages.Children.Add(tb);
                scroll.ScrollToBottom();
            }
            else
                MessageBox.Show("Please, enter the message");
            this.Message.Text = "Write message...";
        }

        private void Message_GotFocus(object sender, RoutedEventArgs e)
        {
            this.Message.Text = "";
        }
    }
}