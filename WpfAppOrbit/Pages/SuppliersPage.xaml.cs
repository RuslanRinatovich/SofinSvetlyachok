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
    /// Логика взаимодействия для SuppliersPage.xaml
    /// </summary>
    public partial class SuppliersPage : Page
    {
        List<Supplier> suppliers;
        int _itemcount = 0;
        public SuppliersPage()
        {
            InitializeComponent();
        }
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            // открытие редактирования агента
            // передача выбранного агента в AddSupplierPage
           Manager.MainFrame.Navigate(new AddSupplierPage((sender as Button).DataContext as Supplier));
        }

        private void UpdateData()
        {
            var currentSuppliers =
                ZevaBdEntities.GetContext().Suppliers.OrderBy(p => p.SupplierName).ToList();
            // выбор только тех агентов, которые принадлежат данному типу
            if (ComboType.SelectedIndex > 0)
                currentSuppliers = currentSuppliers.Where(p => p.SupplierTypeId == (ComboType.SelectedItem as SupplierType).SupplierTypeId).ToList();
            // выбор тех агентов, в названии которых есть поисковая строка
            currentSuppliers = currentSuppliers.Where(p => p.SupplierName.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            // сортировка
            if (ComboSort.SelectedIndex >= 0)
            {
                // сортировка по возрастанию названия
                if (ComboSort.SelectedIndex == 0)
                    currentSuppliers = currentSuppliers.OrderBy(p => p.SupplierName).ToList();
                // сортировка по убыванию названия
                if (ComboSort.SelectedIndex == 1)
                    currentSuppliers = currentSuppliers.OrderByDescending(p => p.SupplierName).ToList();
                // сортировка по возрастанию скидки
                if (ComboSort.SelectedIndex == 2)
                    currentSuppliers = currentSuppliers.OrderBy(p => p.Rate).ToList();
                // сортировка по убыванию номера цеха
                if (ComboSort.SelectedIndex == 3)
                    currentSuppliers = currentSuppliers.OrderByDescending(p => p.Rate).ToList();
                // сортировка по возрастанию минимальной стоимости
            }


            // В качестве источника данных присваиваем список данных
            // пересчитываем список страниц
            suppliers = currentSuppliers;
            DataGridSuppliers.ItemsSource = currentSuppliers;
            // отображение количества записей
            TextBlockCount.Text = $" Результат запроса: {currentSuppliers.Count} записей из {_itemcount}";

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
                DataGridSuppliers.ItemsSource = null;
                //загрузка обновленных данных
                ZevaBdEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                suppliers = ZevaBdEntities.GetContext().Suppliers.OrderBy(p => p.SupplierName).ToList();
                _itemcount = suppliers.Count;
                DataGridSuppliers.ItemsSource = suppliers;
                // загрузка данных в combobox + добавление дополнительной строки
                var materialTypes = ZevaBdEntities.GetContext().SupplierTypes.OrderBy(p => p.SupplierTypeName).ToList();
                materialTypes.Insert(0, new SupplierType
                {
                    SupplierTypeName = "Все типы"
                }
                );
                ComboType.ItemsSource = materialTypes;
                ComboType.SelectedIndex = 0;
            }
        }

        private void BtnAddClick(object sender, RoutedEventArgs e)
        {
            // открытие  AddSupplierPage для добавления новой записи
            Manager.MainFrame.Navigate(new AddSupplierPage(null));
        }

        private void BtnDeleteClick(object sender, RoutedEventArgs e)
        {
            // удаление выбранного агента из таблицы
            //получаем все выделенные товары
            var selectedSuppliers = DataGridSuppliers.SelectedItems.Cast<Supplier>().ToList();
            // вывод сообщения с вопросом Удалить запись?
            MessageBoxResult messageBoxResult = MessageBox.Show($"Удалить {selectedSuppliers.Count()} записей???",
          "Удаление", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            //если пользователь нажал ОК пытаемся удалить запись
            if (messageBoxResult == MessageBoxResult.OK)
            {
                try
                {
                    // берем из списка удаляемых товаров один элемент
                    Supplier x = selectedSuppliers[0];
                    // проверка, есть ли у агента в таблице о продажах связанные записи
                    // если да, то выбрасывается исключение и удаление прерывается
                    if (x.MaterialSuppliers.Count > 0)
                        throw new Exception("Есть записи в продажах");
                    //ищем записи в таблице Точки продаж, с которой связан этот агента
                    ZevaBdEntities.GetContext().Suppliers.Remove(x);
                    //сохраняем изменения
                    ZevaBdEntities.GetContext().SaveChanges();
                    MessageBox.Show("Записи удалены");
                    suppliers = ZevaBdEntities.GetContext().Suppliers.OrderBy(p => p.SupplierName).ToList();
                    DataGridSuppliers.ItemsSource = null;
                    DataGridSuppliers.ItemsSource = suppliers;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            // удаление выбранного агента из таблицы
            //получаем все выделенные товары
            Supplier deleted = (sender as Button).DataContext as Supplier;
            // вывод сообщения с вопросом Удалить запись?
            MessageBoxResult messageBoxResult = MessageBox.Show($"Удалить запись???",
          "Удаление", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            //если пользователь нажал ОК пытаемся удалить запись
            if (messageBoxResult == MessageBoxResult.OK)
            {
                try
                {

                    if (deleted.MaterialSuppliers.Count > 0)
                        throw new Exception("Есть записи в продажах");
                    //ищем записи в таблице Точки продаж, с которой связан этот агента
                    ZevaBdEntities.GetContext().Suppliers.Remove(deleted);
                    //сохраняем изменения
                    ZevaBdEntities.GetContext().SaveChanges();
                    MessageBox.Show("Записи удалены");
                    suppliers = ZevaBdEntities.GetContext().Suppliers.OrderBy(p => p.SupplierName).ToList();
                    DataGridSuppliers.ItemsSource = null;
                    DataGridSuppliers.ItemsSource = suppliers;
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
            string fileName = AppDomain.CurrentDomain.BaseDirectory + "\\" + "Suppliers" + ".xltx";
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


                foreach (Supplier supplier in suppliers)
                {
                    xlSheet.Cells[row, 1] = (i + 1).ToString();
                    string s;
                    // DateTime y = Convert.ToDateTime(dtOrders.Rows[i].Cells[1].Value);
                    xlSheet.Cells[row, 2] = supplier.SupplierId.ToString();
                    s = "";


                    xlSheet.Cells[row, 3] = supplier.SupplierName.ToString();
                    xlSheet.Cells[row, 4] = supplier.SupplierType.SupplierTypeName.ToString();
                    xlSheet.Cells[row, 5] = supplier.INN.ToString();
                    s = "0%";
                    if (supplier.Rate != 0) s = supplier.Rate.ToString() + "%";
                    xlSheet.Cells[row, 6] = s;
                    xlSheet.Cells[row, 7] = supplier.WorkDate.ToShortDateString();
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

        private void BtnSupplierTypes_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new SupplierTypesPage());
        }

        private void BtnUnitTypes_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new UnitTypesPage());
        }


    }
}
