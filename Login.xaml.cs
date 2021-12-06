using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace KingIT
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {


        public Login()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var Users = KingITTEntities.GetContext().Workers.ToList();

            bool Prov = false;
            foreach (Workers Login in Users)
            {
                
                if ((Nikname.Text.ToUpper() == Login.Login.ToUpper()) && (Password.Password.ToUpper() == Login.Password.ToUpper()))
                {
                    Nicknameeeee.Prov = true;
                    Nicknameeeee.Login = Nikname.Text;
                    Nicknameeeee.Status = (int)Login.IDRole;
                    Prov = true;

                    Manager.MainFrame.Navigate(new Menu());

                    break;

                }
            }
            //count++;
            if (Prov == false)
            {
                Nicknameeeee.count++;
                if (Nicknameeeee.count >= 3 && CaptchaBox.Text != CaptchaBlock.Text)
                {
                    MessageBox.Show("Неверные данные");
                    Random random = new Random();
                    CaptchaBlock.Text = random.Next(99999).ToString();
                }
                else
                {

                    if (Nicknameeeee.count == 2 || Nicknameeeee.count > 2)
                    {

                        Random random = new Random();
                        CaptchaBlock.Text = random.Next(99999).ToString();
                        Captcha.Visibility = Visibility.Visible;
                        CaptchaBox.Visibility = Visibility.Visible;
                        Grid.SetRow(Entering, 6);


                    }
                    Manager.MainFrame.Navigate(new Login());
                    StringBuilder errors = new StringBuilder();

                    if (Prov == false)
                        errors.AppendLine("Ошибка 1: Некорректные логин или пароль!!");

                    if (errors.Length > 0)
                    {
                        MessageBox.Show(errors.ToString());
                        return;
                    }


                }

            }
        }

    }
}
