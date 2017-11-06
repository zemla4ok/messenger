using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;

namespace WpfApplication2
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        private User thisUser;
        Thread update;

        public Menu(string UserName)
        {
            try
            {
                InitializeComponent();
                this.UserName.Text = UserName;
                ServerObject.SendMessage("12", UserName);
                thisUser = new User(ServerObject.GetMessage());
                this.UserInfo.Text = thisUser.Name + "  " + thisUser.SurName + "  aka  " + thisUser.NickName;
                this.Birthday.Text = thisUser.Birthday;
                this.Gender.Text = thisUser.Gender;
                this.Country.Text = thisUser.Country;
                this.City.Text = thisUser.City;

                //all users
                ServerObject.SendMessage("16", "");
                this.DataGrid.ItemsSource = new UserList(ServerObject.GetMessage()).GetUsers();

                //all chats                
                update = new Thread(this.UpdateChats);
                update.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        private void ChangePass(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Validator.ValidTextBoxes(this.OldPassword.Password, this.NewPass.Password, this.RepNewPass.Password))
                    MessageBox.Show("Passwords not input");
                else if (!Validator.CheckPassword(this.NewPass.Password, this.RepNewPass.Password))
                    MessageBox.Show("New passwords non Equal");
                else if (!Validator.CheckPassword(thisUser.Password, Crypt.CryptPassword(this.OldPassword.Password)))
                    MessageBox.Show("Old passwords non Equal");
                else
                {
                    try
                    {
                        thisUser.ChangePass(this.NewPass.Password);
                        ServerObject.SendMessage("13", thisUser.ToString());
                        MessageBox.Show("Password is changed");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CloseMess(object sender, RoutedEventArgs e)
        {
            update.Abort();
            this.Close();
        }

        private void Searching(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Validator.ValidTextBoxes(this.SearchBy.Text, this.TextToSearch.Text))
                    MessageBox.Show("Data not input");
                SearchObject sObj = new SearchObject(this.TextToSearch.Text, this.SearchBy.Text);
                ServerObject.SendMessage("14", sObj.ToString());
                this.DataGrid.ItemsSource = new UserList(ServerObject.GetMessage()).GetUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Row_DoubleClick(object sender, RoutedEventArgs e)
        {
            Information info = new Information(this.DataGrid.CurrentCell.Item.ToString());
            info.Show();
        }

        private void CreateChat(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.DataGrid.SelectedItems.Count > 4)
                    MessageBox.Show("нужно 4 и меньше");
                else if (this.DataGrid.SelectedItems.Count == 0)
                    MessageBox.Show("Не выбраны польщователи");
                else
                {
                    UserList list = new UserList(this.DataGrid.SelectedItems.Cast<User>().ToList());
                    ServerObject.SendMessage("15", thisUser.NickName + ";" + list.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Chat_DoubleClick(object sender, RoutedEventArgs e)
        {
            ChatWindow chatWind = new ChatWindow(((Chat)this.Chats.CurrentItem).ChatWith, thisUser.NickName);
            chatWind.Show();
        }

        private void UpdateChats()
        {
            try
            {
                while (true)
                {
                    ServerObject.SendMessage("17", thisUser.NickName);
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        this.Chats.ItemsSource = new ChatList(ServerObject.GetMessage()).GetChats();
                    }));
                    Thread.Sleep(5000);
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
//TODO: 109, 112 to English;