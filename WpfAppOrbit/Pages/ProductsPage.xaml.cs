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
using Excel = Microsoft.Office.Interop.Excel;
namespace WpfAppOrbit.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        List<Product> products;
        int _itemcount = 0;
        public ProductsPage()
        {
            InitializeComponent();
        }
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            // открытие редактирования агента
            // передача выбранного агента в AddProductPage
            Manager.MainFrame.Navigate(new AddProductPage((sender as Button).DataContext as Product));
        }

        private void UpdateData()
        {
            var currentProducts =
                ZevaBdEntities.GetContext().Products.OrderBy(p => p.ProductName).ToList();
            // выбор только тех агентов, которые принадлежат данному типу
            if (ComboType.SelectedIndex > 0)
                currentProducts = currentProducts.Where(p => p.ProductTypeId == (ComboType.SelectedItem as ProductType).ProductTypeId).ToList();
            // выбор тех агентов, в названии которых есть поисковая строка
            currentProducts = currentProducts.Where(p => p.ProductName.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            // сортировка
            if (ComboSort.SelectedIndex >= 0)
            {
                // сортировка по возрастанию названия
                if (ComboSort.SelectedIndex == 0)
                    currentProducts = currentProducts.OrderBy(p => p.ProductName).ToList();
                // сортировка по убыванию названия
                if (ComboSort.SelectedIndex == 1)
                    currentProducts = currentProducts.OrderByDescending(p => p.ProductName).ToList();
                // сортировка по возрастанию скидки
                if (ComboSort.SelectedIndex == 2)
                    currentProducts = currentProducts.OrderBy(p => p.WorkshopId).ToList();
                // сортировка по убыванию номера цеха
                if (ComboSort.SelectedIndex == 3)
                    currentProducts = currentProducts.OrderByDescending(p => p.WorkshopId).ToList();
                // сортировка по возрастанию минимальной стоимости
                if (ComboSort.SelectedIndex == 4)
                    currentProducts = currentProducts.OrderBy(p => p.MinimalPrice).ToList();
                // сортировка по убыванию минимальной стоимости
                if (ComboSort.SelectedIndex == 5)
                    currentProducts = currentProducts.OrderByDescending(p => p.MinimalPrice).ToList();
            }


            // В качестве источника данных присваиваем список данных
            // пересчитываем список страниц
            products = currentProducts;
            DataGridProducts.ItemsSource = currentProducts;
            // отображение количества записей
            TextBlockCount.Text = $" Результат запроса: {currentProducts.Count} записей из {_itemcount}";

        }
        private void ComboSortSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        } // Поиск агентов, которые содержат данную поисковую строку в имени или в почте или в телефоне
        private void TBoxSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }
        // Поиск агентов конкретного типа
        private void ComboTypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }
        private void PageIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //событие отображения данного Page
            // обновляем данные каждый раз когда активируется этот Page
            if (Visibility == Visibility.Visible)
            {
                DataGridProducts.ItemsSource = null;
                //загрузка обновленных данных
                ZevaBdEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                products = ZevaBdEntities.GetContext().Products.OrderBy(p => p.ProductName).ToList();
                _itemcount = products.Count;
                DataGridProducts.ItemsSource = products;
                // загрузка данных в combobox + добавление дополнительной строки
                var productTypes = ZevaBdEntities.GetContext().ProductTypes
                    .OrderBy(p => p.ProductTypeName).ToList();
                productTypes.Insert(0, new ProductType
                {
                    ProductTypeName = "Все типы"
                }
                );
                ComboType.ItemsSource = productTypes;
                ComboType.SelectedIndex = 0;
            }
        }

        private void BtnAddClick(object sender, RoutedEventArgs e)
        {
            // открытие  AddProductPage для добавления новой записи
           Manager.MainFrame.Navigate(new AddProductPage(null));
        }

        private void BtnDeleteClick(object sender, RoutedEventArgs e)
        {
            // удаление выбранного агента из таблицы
            //получаем все выделенные товары
            var selectedProducts = DataGridProducts.SelectedItems.Cast<Product>().ToList();
            // вывод сообщения с вопросом Удалить запись?
            MessageBoxResult messageBoxResult = MessageBox.Show($"Удалить {selectedProducts.Count()} записей???",
          "Удаление", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            //если пользователь нажал ОК пытаемся удалить запись
            if (messageBoxResult == MessageBoxResult.OK)
            {
                try
                {
                    // берем из списка удаляемых товаров один элемент
                    Product x = selectedProducts[0];
                    // проверка, есть ли у агента в таблице о продажах связанные записи
                    // если да, то выбрасывается исключение и удаление прерывается
                    if (x.ProductMaterials.Count > 0)
                        throw new Exception("Есть записи в продажах");
                    //ищем записи в таблице Точки продаж, с которой связан этот агента
                    ZevaBdEntities.GetContext().Products.Remove(x);
                    //сохраняем изменения
                    ZevaBdEntities.GetContext().SaveChanges();
                    MessageBox.Show("Записи удалены");
                    products = ZevaBdEntities.GetContext().Products.OrderBy(p => p.ProductName).ToList();
                    DataGridProducts.ItemsSource = null;
                    DataGridProducts.ItemsSource = products;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        // отображение номеров строк в DataGrid
        private void DataGridAgetsLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void BtnGet_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnWriteOff_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BtnExcel_Click(object sender, RoutedEventArgs e)
        {
            PrintExcel();
        }
        private void PrintExcel()
        {
            string fileName = AppDomain.CurrentDomain.BaseDirectory + "\\" + "Products" + ".xltx";
            Excel.Application xlApp = new Excel.Application();
            Excel.Worksheet xlSheet = new Excel.Worksheet();
            try
            {
                //добавляем книгу
                xlApp.Workbooks.Open(fileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                          Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                          Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                          Type.Missing, Type.Missing);
                //делаем временно неактивным документ
                xlApp.Interactive = false;
                xlApp.EnableEvents = false;
                Excel.Range xlSheetRange;
                //выбираем лист на котором будем работать (Лист 1)
                xlSheet = (Excel.Worksheet)xlApp.Sheets[1];
                //Название листа
                xlSheet.Name = "Список поставщиков";
                int row = 2;
                int i = 0;


                foreach (Product product in products)
                {
                    xlSheet.Cells[row, 1] = (i + 1).ToString();
                    string s;
                    // DateTime y = Convert.ToDateTime(dtOrders.Rows[i].Cells[1].Value);
                    xlSheet.Cells[row, 2] = product.Artikul.ToString();
                    s = "";


                    xlSheet.Cells[row, 3] = product.ProductName.ToString();
                    xlSheet.Cells[row, 4] = product.ProductType.ProductTypeName.ToString();
                    //xlSheet.Cells[row, 5] = product.UnitType.UnitTypeName.ToString();
                    s = "";
                    if (product.PeopleCount != 0) s = product.PeopleCount.ToString();
                    xlSheet.Cells[row, 5] = s;

                    s = "";
                    if (product.WorkshopId != 0) s = product.WorkshopId.ToString();
                    xlSheet.Cells[row, 6] = s;
                    s = "";
                    if (product.MinimalPrice != 0) s = product.MinimalPrice.ToString();
                    xlSheet.Cells[row, 7] = s;

                    row++;
                    Excel.Range r = xlSheet.get_Range("A" + row.ToString(), "G" + row.ToString());
                    r.Insert(Excel.XlInsertShiftDirection.xlShiftDown);
                    i++;
                }




                row--;
                xlSheetRange = xlSheet.get_Range("A2:G" + (row + 1).ToString(), Type.Missing);
                xlSheetRange.Borders.LineStyle = true;
                //xlSheet.Cells[row + 1, 9] = "=SUM(I2:I" + row.ToString() + ")";

                //xlSheet.Cells[row + 1, 8] = "ИТОГО:";
                row++;

                //выбираем всю область данных*/
                xlSheetRange = xlSheet.UsedRange;
                //выравниваем строки и колонки по их содержимому
                xlSheetRange.Columns.AutoFit();
                xlSheetRange.Rows.AutoFit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                //Показываем ексель
                xlApp.Visible = true;
                xlApp.Interactive = true;
                xlApp.ScreenUpdating = true;
                xlApp.UserControl = true;
            }
        }
        private void BtnProductTypes_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ProductTypePage());
        }

        private void BtnUnitTypes_Click(object sender, RoutedEventArgs e)
        {
         //   Manager.MainFrame.Navigate(new UnitTypesPage());
        }

        private void BtnSell_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new SellHistoryPage((sender as Button).DataContext as Product));
        }

        private void BtnWorkshops_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new WorkshopsPage());
        }
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            // удаление выбранного агента из таблицы
            //получаем все выделенные товары
            Product deleted = (sender as Button).DataContext as Product;
            // вывод сообщения с вопросом Удалить запись?
            MessageBoxResult messageBoxResult = MessageBox.Show($"Удалить запись???",
          "Удаление", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            //если пользователь нажал ОК пытаемся удалить запись
            if (messageBoxResult == MessageBoxResult.OK)
            {
                try
                {

                    if (deleted.ProductMaterials.Count > 0)
                        throw new Exception("Есть записи в продажах");
                    //ищем записи в таблице Точки продаж, с которой связан этот агента
                    ZevaBdEntities.GetContext().Products.Remove(deleted);
                    //сохраняем изменения
                    ZevaBdEntities.GetContext().SaveChanges();
                    MessageBox.Show("Записи удалены");
                    products = ZevaBdEntities.GetContext().Products.OrderBy(p => p.ProductName).ToList();
                    DataGridProducts.ItemsSource = null;
                    DataGridProducts.ItemsSource = products;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
