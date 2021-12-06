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
    /// Логика взаимодействия для PavilionsPage.xaml
    /// </summary>
    public partial class PavilionsPage : Page
    {
        public PavilionsPage()
        {
            InitializeComponent();
            var allFloors = KingITTEntities.GetContext().Pavilions.ToList();
            allFloors.Insert(0, new Pavilions
            {
                FloorPav = 0
            });
            TBoxSearch.ItemsSource = allFloors;
            allFloors.Insert(1, new Pavilions
            {
                FloorPav = 1
            });
            allFloors.Insert(2, new Pavilions
            {
                FloorPav = 2
            });
            allFloors.Insert(3, new Pavilions
            {
                FloorPav = 3
            });
            allFloors.Insert(4, new Pavilions
            {
                FloorPav = 4
            });
            TBoxSearch.SelectedIndex = 0;
        }

        private void UpdatePav()
        {
            var currentPav = KingITTEntities.GetContext().Pavilions.ToList();

            if (TBoxSearch.SelectedIndex > 0)
                currentPav = currentPav.Where(p => p.FloorPav == TBoxSearch.SelectedIndex).ToList();

            DGridPavilions.ItemsSource = currentPav.OrderBy(p => p.FloorPav).ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPavilions((sender as Button).DataContext as Pavilions));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPavilions(null));
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            var pavilionsForRemoving = DGridPavilions.SelectedItems.Cast<Pavilions>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {pavilionsForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    KingITTEntities.GetContext().Pavilions.RemoveRange(pavilionsForRemoving);
                    KingITTEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");

                    DGridPavilions.ItemsSource = KingITTEntities.GetContext().Pavilions.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {

                KingITTEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridPavilions.ItemsSource = KingITTEntities.GetContext().Pavilions.ToList();
                DGridSCinPav.ItemsSource = KingITTEntities.GetContext().SC.ToList();
            }
        }

        private void BtnPlace_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TBoxSearchTown_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePav();
        }

        private void TBoxSearchStatusSC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePav();
        }

        private void TBoxSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePav();
        }

        private void BtnRes_Click(object sender, RoutedEventArgs e)
        {
            //Manager.MainFrame = new Arenda();
        }
    }
}
