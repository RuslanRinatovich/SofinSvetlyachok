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
using WpfAppOrbit.Models;
using WpfAppOrbit.Windows;

namespace WpfAppOrbit.Pages
{
    /// <summary>
    /// Логика взаимодействия для SellHistoryPage.xaml
    /// </summary>
    public partial class SellHistoryPage : Page
    {
        public SellHistoryPage(Product product)
        {
            InitializeComponent();
            LoadData(product);

        }
        // загрузка данных в DataGrid и ComboBox
        void LoadData(Product product)
        {
            DataGridSells.ItemsSource = ZevaBdEntities.GetContext().SellHistories.Where(p => p.ProductId == product.ProductId).OrderBy(p => p.Date).ToList();
            ComboProducts.ItemsSource = ZevaBdEntities.GetContext().Products.OrderBy(p => p.ProductName).ToList(); ;
            ComboProducts.SelectedIndex = 0;
            ComboProducts.SelectedValue = product.ProductId;
            GridProduct.DataContext = product;
        }
        // фильтрация продаж по товару
        private void ComboProductsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboProducts.SelectedIndex > 0)
            {
                int productId = Convert.ToInt32(ComboProducts.SelectedValue);
                var x = ZevaBdEntities.GetContext().SellHistories.Where(p => p.ProductId == productId).OrderBy(p => p.Date).ToList();
                DataGridSells.ItemsSource = x;
                GridProduct.DataContext = ComboProducts.SelectedItem;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Product g = ComboProducts.SelectedItem as Product;

                SellWindow window = new SellWindow(new SellHistory(), g);
                if (window.ShowDialog() == true)
                {
                    ZevaBdEntities.GetContext().SellHistories.Add(window.currentItem);
                    ZevaBdEntities.GetContext().SaveChanges();

                    MessageBox.Show("Запись добавлена", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadData(g);

                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product g = ComboProducts.SelectedItem as Product;

                // если ни одного объекта не выделено, выходим
                if (DataGridSells.SelectedItem == null) return;
                // получаем выделенный объект
                MessageBoxResult messageBoxResult = MessageBox.Show($"Удалить запись? ", "Удаление", MessageBoxButton.OKCancel,
MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.OK)
                {
                    SellHistory deletedItem = DataGridSells.SelectedItem as SellHistory;
                    ZevaBdEntities.GetContext().SellHistories.Remove(deletedItem);
                    ZevaBdEntities.GetContext().SaveChanges();


                    LoadData(g);
                    MessageBox.Show("Запись удалена", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
