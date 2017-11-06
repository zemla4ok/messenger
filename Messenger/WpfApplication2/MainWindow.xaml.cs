using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow regWind = new RegistrationWindow();
            regWind.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Validator.ValidTextBoxes(this.Name.Text, this.Pass.Password))
                    MessageBox.Show("Enter your nickname and password");
                else
                {
                    User thisUser = new User(this.Name.Text, this.Pass.Password);
                    ServerObject.SendMessage("11", thisUser.GetLogAndPass());
                    string exMess = ServerObject.GetMessage();
                    if (exMess == "false")
                    {
                        Menu menu = new Menu(this.Name.Text);
                        menu.Show();
                        this.Close();
                    }
                    else
                        MessageBox.Show("логин и/или пароль неверны");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}