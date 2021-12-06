using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Login());
            Manager.MainFrame = MainFrame;
            
            //ImportSC();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void ImportSC()
        {
            var ImageSC = Directory.GetFiles(@"C:\Users\Геляяяяяя\Desktop\УП 02.01\Image ТЦ");

            var Image = new SC();

            //try
            //{
            //    Image.ImageSC = File.ReadAllBytes(ImageSC.FirstOrDefault(p => p.Contains(Image.NameSC)));
           // }
           // catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            KingITTEntities.GetContext().SC.Add(Image);
            KingITTEntities.GetContext().SaveChanges();

        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                BtnBack.Visibility = Visibility.Visible;
            }
            else
            {
                BtnBack.Visibility = Visibility.Hidden;
            }
        }
    }
}
