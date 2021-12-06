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

namespace KingIT
{
    /// <summary>
    /// Логика взаимодействия для Login2.xaml
    /// </summary>
    public partial class Login2 : Page
    {
        public Login2()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var Users = KingITTEntities.GetContext().Workers.ToList();
            var count = 0;

            bool Prov = false;
            foreach (Workers Login in Users)
            {
                if ((Nikname.Text == Login.Login) && (Password.Password == Login.Password))
                {
                    Nicknameeeee.Prov = true;
                    Nicknameeeee.Login = Nikname.Text;
                    Nicknameeeee.Status = (int)Login.IDRole;
                    Prov = true;

                    Manager.MainFrame.Navigate(new Menu());

                    break;

                }
            }
            if (Prov == false)
            {
                Manager.MainFrame.Navigate(new Login2());
                StringBuilder errors = new StringBuilder();

                if (Prov == false)
                    errors.AppendLine("Ошибка 1: Некорректные логин или пароль!!");
                count++;

                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }

            }

        }

        private void button1_Click(object sender, RoutedEventArgs e)

        {
            String allowchar = " ";
            allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";
            allowchar += "1,2,3,4,5,6,7,8,9,0";
            char[] a = { ',' };
            String[] ar = allowchar.Split();
            String pwd = " ";
            string temp = " ";
            Random r = new Random();
            for (int i = 0; i < 6; i++)
            {
                temp = ar[(r.Next(0, ar.Length))];
                pwd += temp;
            }
            textBox1.Text = pwd;
        }

        private void Regin_Click_1(object sender, RoutedEventArgs e)
        {
            //Manager.MainFrame.Navigate(new Regin());
            //MainFramee.Navigate(new Regin());
            //Manager.MainFramee = MainFramee;
        }
    }
}
