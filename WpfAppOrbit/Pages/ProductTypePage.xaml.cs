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
using WpfAppOrbit.Pages;
using System.Data.Entity;

namespace WpfAppOrbit.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductTypePage.xaml
    /// </summary>
    public partial class ProductTypePage : Page
    {
        List<ProductType> datas;
        public ProductTypePage()
        {

            InitializeComponent();
        }


        void LoadData()
        {
            try
            {
                DtData.ItemsSource = null;
                //загрузка обновленных данных
                ZevaBdEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                datas = ZevaBdEntities.GetContext().ProductTypes.OrderBy(p => p.ProductTypeName).ToList();
                DtData.ItemsSource = datas;
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //событие отображения данного Page
            // обновляем данные каждый раз когда активируется этот Page
            if (Visibility == Visibility.Visible)
            {
                LoadData();
            }
        }

        private void DataGridGoodLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                ProductTypeWindow window = new ProductTypeWindow(new ProductType());
                if (window.ShowDialog() == true)
                {
                    ZevaBdEntities.GetContext().ProductTypes.Add(window.currentItem);
                    ZevaBdEntities.GetContext().SaveChanges();
                    LoadData();
                    MessageBox.Show("Запись добавлена", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // если ни одного объекта не выделено, выходим
                if (DtData.SelectedItem == null) return;
                // получаем выделенный объект
                ProductType selected = DtData.SelectedItem as ProductType;


                ProductTypeWindow window = new ProductTypeWindow(
                    new ProductType
                    {
                        ProductTypeId = selected.ProductTypeId,
                        ProductTypeName = selected.ProductTypeName
                    }
                    );

                if (window.ShowDialog() == true)
                {
                    selected = ZevaBdEntities.GetContext().ProductTypes.Find(window.currentItem.ProductTypeId);
                    // получаем измененный объект
                    if (selected != null)
                    {

                        selected.ProductTypeId = window.currentItem.ProductTypeId;
                        selected.ProductTypeName = window.currentItem.ProductTypeName;
                        ZevaBdEntities.GetContext().Entry(selected).State = EntityState.Modified;
                        ZevaBdEntities.GetContext().SaveChanges();
                        LoadData();
                        MessageBox.Show("Запись изменена", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
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
                // если ни одного объекта не выделено, выходим
                if (DtData.SelectedItem == null) return;
                // получаем выделенный объект
                MessageBoxResult messageBoxResult = MessageBox.Show($"Удалить запись? ", "Удаление", MessageBoxButton.OKCancel,
MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.OK)
                {
                    ProductType deletedItem = DtData.SelectedItem as ProductType;



                    if (deletedItem.Products.Count > 0)
                    {
                        MessageBox.Show("Ошибка удаления, есть связанные записи", "Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    ZevaBdEntities.GetContext().ProductTypes.Remove(deletedItem);
                    ZevaBdEntities.GetContext().SaveChanges();
                    MessageBox.Show("Запись удалена", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка, есть связанные записи");
            }
            finally
            {
                LoadData();
            }
        }
    }
}
