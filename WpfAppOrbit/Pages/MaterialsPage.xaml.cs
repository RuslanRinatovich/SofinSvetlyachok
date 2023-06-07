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
    /// Логика взаимодействия для MaterialsPage.xaml
    /// </summary>
    public partial class MaterialsPage : Page
    {
        List<Material> materials;
        int _itemcount = 0;
        public MaterialsPage()
        {
            InitializeComponent();
        }
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            // открытие редактирования агента
            // передача выбранного агента в AddMaterialPage
            Manager.MainFrame.Navigate(new AddMaterialPage1((sender as Button).DataContext as Material));
        }

        private void UpdateData()
        {
            var currentMaterials =
                ZevaBdEntities.GetContext().Materials.OrderBy(p => p.MaterialName).ToList();
            // выбор только тех агентов, которые принадлежат данному типу
            if (ComboType.SelectedIndex > 0)
                currentMaterials = currentMaterials.Where(p => p.MaterialTypeId == (ComboType.SelectedItem as MaterialType).MaterialTypeId).ToList();
            // выбор тех агентов, в названии которых есть поисковая строка
            currentMaterials = currentMaterials.Where(p => p.MaterialName.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            // сортировка
            if (ComboSort.SelectedIndex >= 0)
            {
                // сортировка по возрастанию названия
                if (ComboSort.SelectedIndex == 0)
                    currentMaterials = currentMaterials.OrderBy(p => p.MaterialName).ToList();
                // сортировка по убыванию названия
                if (ComboSort.SelectedIndex == 1)
                    currentMaterials = currentMaterials.OrderByDescending(p => p.MaterialName).ToList();
                // сортировка по возрастанию скидки
                if (ComboSort.SelectedIndex == 2)
                    currentMaterials = currentMaterials.OrderBy(p => p.Count).ToList();
                // сортировка по убыванию номера цеха
                if (ComboSort.SelectedIndex == 3)
                    currentMaterials = currentMaterials.OrderByDescending(p => p.Count).ToList();
                // сортировка по возрастанию минимальной стоимости
                if (ComboSort.SelectedIndex == 4)
                    currentMaterials = currentMaterials.OrderBy(p => p.Price).ToList();
                // сортировка по убыванию минимальной стоимости
                if (ComboSort.SelectedIndex == 5)
                    currentMaterials = currentMaterials.OrderByDescending(p => p.Price).ToList();
            }


            // В качестве источника данных присваиваем список данных
            // пересчитываем список страниц
            materials = currentMaterials;
            DataGridMaterials.ItemsSource = currentMaterials;
            // отображение количества записей
            TextBlockCount.Text = $" Результат запроса: {currentMaterials.Count} записей из {_itemcount}";

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
                DataGridMaterials.ItemsSource = null;
                //загрузка обновленных данных
                ZevaBdEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                materials = ZevaBdEntities.GetContext().Materials.OrderBy(p => p.MaterialName).ToList();
                _itemcount = materials.Count;
                DataGridMaterials.ItemsSource = materials;
                // загрузка данных в combobox + добавление дополнительной строки
                var materialTypes = ZevaBdEntities.GetContext().MaterialTypes
                    .OrderBy(p => p.MaterialTypeName).ToList();
                materialTypes.Insert(0, new MaterialType
                {
                    MaterialTypeName = "Все типы"
                }
                );
                ComboType.ItemsSource = materialTypes;
                ComboType.SelectedIndex = 0;
            }
        }

        private void BtnAddClick(object sender, RoutedEventArgs e)
        {
            // открытие  AddMaterialPage для добавления новой записи
            Manager.MainFrame.Navigate(new AddMaterialPage1(null));
        }

        private void BtnDeleteClick(object sender, RoutedEventArgs e)
        {
            // удаление выбранного агента из таблицы
            //получаем все выделенные товары
            var selectedMaterials = DataGridMaterials.SelectedItems.Cast<Material>().ToList();
            // вывод сообщения с вопросом Удалить запись?
            MessageBoxResult messageBoxResult = MessageBox.Show($"Удалить {selectedMaterials.Count()} записей???",
          "Удаление", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            //если пользователь нажал ОК пытаемся удалить запись
            if (messageBoxResult == MessageBoxResult.OK)
            {
                try
                {
                    // берем из списка удаляемых товаров один элемент
                    Material x = selectedMaterials[0];
                    // проверка, есть ли у агента в таблице о продажах связанные записи
                    // если да, то выбрасывается исключение и удаление прерывается
                    if (x.MaterialSuppliers.Count > 0)
                        throw new Exception("Есть записи в продажах");
                    //ищем записи в таблице Точки продаж, с которой связан этот агента
                    ZevaBdEntities.GetContext().Materials.Remove(x);
                    //сохраняем изменения
                    ZevaBdEntities.GetContext().SaveChanges();
                    MessageBox.Show("Записи удалены");
                    materials = ZevaBdEntities.GetContext().Materials.OrderBy(p => p.MaterialName).ToList();
                    DataGridMaterials.ItemsSource = null;
                    DataGridMaterials.ItemsSource = materials;
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
            string fileName = AppDomain.CurrentDomain.BaseDirectory + "\\" + "Materials" + ".xltx";
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


                foreach (Material material in materials)
                {
                    xlSheet.Cells[row, 1] = (i + 1).ToString();
                    string s;
                    // DateTime y = Convert.ToDateTime(dtOrders.Rows[i].Cells[1].Value);
                    xlSheet.Cells[row, 2] = material.MaterialId.ToString();
                    s = "";


                    xlSheet.Cells[row, 3] = material.MaterialName.ToString();
                    xlSheet.Cells[row, 4] = material.MaterialType.MaterialTypeName.ToString();
                    xlSheet.Cells[row, 5] = material.UnitType.UnitTypeName.ToString();
                    s = "";
                    if (material.CountInPack != 0) s = material.CountInPack.ToString();
                    xlSheet.Cells[row, 6] = s;
                    s = "";
                    if (material.MinimalCount != 0) s = material.MinimalCount.ToString();
                    xlSheet.Cells[row, 7] = s;
                    s = "";
                    if (material.Count != 0) s = material.Count.ToString();
                    xlSheet.Cells[row, 8] = s;
                    s = "";
                    if (material.Price != 0) s = material.Price.ToString();
                    xlSheet.Cells[row, 9] = s;

                    xlSheet.Cells[row, 10] = material.Description;
                    row++;
                    Excel.Range r = xlSheet.get_Range("A" + row.ToString(), "J" + row.ToString());
                    r.Insert(Excel.XlInsertShiftDirection.xlShiftDown);
                    i++;
                }




                row--;
                xlSheetRange = xlSheet.get_Range("A2:J" + (row + 1).ToString(), Type.Missing);
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
        private void BtnMaterialTypes_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new MaterialTypesPage());
        }

        private void BtnUnitTypes_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new UnitTypesPage());
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            // удаление выбранного агента из таблицы
            //получаем все выделенные товары
            Material deleted = (sender as Button).DataContext as Material;
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
                    ZevaBdEntities.GetContext().Materials.Remove(deleted);
                    //сохраняем изменения
                    ZevaBdEntities.GetContext().SaveChanges();
                    MessageBox.Show("Записи удалены");
                    materials = ZevaBdEntities.GetContext().Materials.OrderBy(p => p.MaterialName).ToList();
                    DataGridMaterials.ItemsSource = null;
                    DataGridMaterials.ItemsSource = materials;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
