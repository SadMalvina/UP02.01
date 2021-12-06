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
    /// Логика взаимодействия для SCPage.xaml
    /// </summary>
    public partial class SCPage : Page
    {
        public SCPage()
        {
            InitializeComponent();
            UpdateSC();

            var allStatus = KingITTEntities.GetContext().StatusSC.ToList();
            allStatus.Insert(0, new StatusSC
            {
                NameStatusSC = "Все статусы"
            });
            StatusSCBox.ItemsSource = allStatus;
            StatusSCBox.SelectedIndex = 0;
        }

        private void UpdateSC()
        {
            var currentSC = KingITTEntities.GetContext().SC.ToList();
            if (StatusSCBox.SelectedIndex > 0)
                currentSC = currentSC.Where(p => p.IDStatusSC == StatusSCBox.SelectedIndex).ToList();

            currentSC = currentSC.Where(p => p.TownSC.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            DGridSC.ItemsSource = currentSC.OrderBy(p => p.TownSC).ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPageSC((sender as Button).DataContext as SC));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPageSC(null));
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            var scForRemoving = DGridSC.SelectedItems.Cast<SC>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {scForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    KingITTEntities.GetContext().SC.RemoveRange(scForRemoving);
                    KingITTEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");

                    DGridSC.ItemsSource = KingITTEntities.GetContext().SC.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }

        }

       // private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
       // {
       //     if (Visibility == Visibility.Visible)
       //     {
       //         KingITTEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
       //         DGridSC.ItemsSource = KingITTEntities.GetContext().SC.ToList();
       //     }
       // }

        private void BtnPlace_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void StatusSCBox_SelectionChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSC();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSC();
        }

        private void StatusSCBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (StatusSCBox.SelectedIndex != 4)
            UpdateSC();
        }
    }
}
