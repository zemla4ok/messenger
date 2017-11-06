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
using System.Windows.Shapes;

namespace WpfApplication2
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!Validator.ValidTextBoxes(this.Name.Text, this.Surname.Text, this.NickName.Text,
                this.Pass.Password, this.Pass2.Password, this.Date.Text, this.Country.Text, this.City.Text)
                || !Validator.ValidRadio((bool)this.Male.IsChecked, (bool)this.Female.IsChecked))
                MessageBox.Show("Please, enter the information");
            else if (!Validator.CheckPassword(this.Pass.Password, this.Pass2.Password))
                MessageBox.Show("Please, enter equal passwords");
            else
            {
                User user = new User(this.Name.Text, this.Surname.Text, this.NickName.Text,
                    this.Male.IsChecked == true ? this.Male.Name : this.Female.Name,
                    this.Date.Text, this.Country.Text, this.City.Text, this.Pass.Password);
                ServerObject.SendMessage("10", user.ToString());

                string exMess = ServerObject.GetMessage();
                if (exMess == "false")
                {
                    MainWindow mainWind = new MainWindow();
                    mainWind.Show();
                    this.Close();
                }
                else
                    MessageBox.Show("Такой никнейм уже существует");   
            }
        }
    }
}
//TODO: make button "Back";