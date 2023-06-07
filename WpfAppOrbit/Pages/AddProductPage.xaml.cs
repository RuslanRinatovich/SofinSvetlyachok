using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using WpfAppOrbit.Models;
using WpfAppOrbit.Windows;
namespace WpfAppOrbit.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddProductPage.xaml
    /// </summary>
    public partial class AddProductPage : Page
    {
        private Product _currentItem = new Product();
        // путь к файлу
        private string _filePath = null;
        // название текущей главной фотографии
        private string _photoName = null;
        // текущая папка приложения
        private static string _currentDirectory = Directory.GetCurrentDirectory() + @"\Images\";

        public AddProductPage(Product selectedItem)
        {
            InitializeComponent();
            // если передано null, то мы добавляем нового агент
            if (selectedItem != null)
            {
                _currentItem = selectedItem;
                if (_currentItem.Image is null)
                {
                    _filePath = null;
                }
                else
                {
                    _filePath = _currentDirectory + _currentItem.Image;
                }
                DtData.ItemsSource = ZevaBdEntities.GetContext().ProductMaterials.Where(p => p.ProductId == selectedItem.ProductId).ToList();
            }
            // контекст данных текущий агент
            DataContext = _currentItem;
            _photoName = _currentItem.Image;
            //загрузка типов агентов
            ComboProductType.ItemsSource = ZevaBdEntities.GetContext().ProductTypes.ToList();
            ComboWorkshop.ItemsSource = ZevaBdEntities.GetContext().Workshops.ToList();

        }

        // Проверка полей на некорректные данные
        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentItem.ProductName))
                s.AppendLine("Поле название пустое");
            if (_currentItem.ProductType == null)
                s.AppendLine("Выберите тип");
            if (_currentItem.Workshop == null)
                s.AppendLine("Выберите цех");



            return s;
        }
        private void BtnSaveClick(object sender, RoutedEventArgs e)
        {
            StringBuilder _error = CheckFields();
            // если ошибки есть, то выводим ошибки в MessageBox
            // и прерываем выполнение 
            if (_error.Length > 0)
            {
                MessageBox.Show(_error.ToString());
                return;
            }
            // проверка полей прошла успешно
            if (_currentItem.ProductId == 0)
            {
                // добавление нового агента
                // формируем новое название файла картинки,
                // так как в папке может быть файл с тем же именем
                if (_filePath != null)
                {

                    string photo = ChangePhotoName();
                    // путь куда нужно скопировать файл
                    string dest = _currentDirectory + photo;
                    File.Copy(_filePath, dest);
                    _currentItem.Image = photo;
                }

                // добавляем агента в БД
                ZevaBdEntities.GetContext().Products.Add(_currentItem);
            }
            //try
            //{
                // если изменилось изображение
                if (_filePath != null)
                {

                    string photo = ChangePhotoName();
                    string dest = _currentDirectory + photo;
                    File.Copy(_filePath, dest);
                    _currentItem.Image = photo;
                }
                // Сохраняем изменения в БД
                ZevaBdEntities.GetContext().SaveChanges();
                MessageBox.Show("Запись Изменена");
                // Возвращаемся на предыдущую форму
                Manager.MainFrame.GoBack();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //}
        }

            // загрузка фото 
        private void BtnLoadClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //Диалог открытия файла
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif";
                // диалог вернет true, если файл был открыт
                if (op.ShowDialog() == true)
                {
                    // проверка размера файла
                    // по условию файл дожен быть не более 2Мб.
                    
                    ImagePhoto.Source = new BitmapImage(new Uri(op.FileName));
                    _photoName = op.SafeFileName;
                    _filePath = op.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                _filePath = null;
            }
        }
        //подбор имени файла
        string ChangePhotoName()
        {
            string x = _currentDirectory + _photoName;
            string photoname = _photoName;
            int i = 0;
            if (File.Exists(x))
            {
                while (File.Exists(x))
                {
                    i++;
                    x = _currentDirectory + i.ToString() + photoname;
                }
                photoname = i.ToString() + photoname;
            }
            return photoname;
        }
        // номера строк для DATA grid
        private void DtDataLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
        void LoadData(Product agent)
        {
            DtData.ItemsSource = ZevaBdEntities.GetContext().ProductMaterials.Where(p => p.ProductId == agent.ProductId).ToList();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //    если мы только добавляем нового агента, то продаж у него еще нет
                if (_currentItem.ProductId == 0) return;

                // Открываем окно добавления новой продажи
                AddProductMaterialWindow window = new AddProductMaterialWindow(_currentItem, new ProductMaterial());
                // если в окне добавления продажи нажата кнопка ОК
                if (window.ShowDialog() == true)
                {
                    // добавляем новую продажу


                    
                    ZevaBdEntities.GetContext().ProductMaterials.Add(window.currentItem);
                    ZevaBdEntities.GetContext().SaveChanges();
                    MessageBox.Show("Запись добавлена", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                    // после добавления продажи
                    // подгружаем измененные данные
                    LoadData(_currentItem);
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
                    ProductMaterial deletedItem = DtData.SelectedItem as ProductMaterial;

                
                    ZevaBdEntities.GetContext().ProductMaterials.Remove(deletedItem);
                    ZevaBdEntities.GetContext().SaveChanges();
                    // после удаления продажи
                    // подгружаем измененные данные
                    LoadData(_currentItem);
                    MessageBox.Show("Запись удалена", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            if (_currentItem.ProductId == 0) return;
            // если ни одного объекта не выделено, выходим
            if (DtData.SelectedItem == null) return;
            // получаем выделенный объект
            ProductMaterial selected = DtData.SelectedItem as ProductMaterial;


            AddProductMaterialWindow window = new AddProductMaterialWindow(_currentItem,
                new ProductMaterial
                {
                    ProductMaterialId = selected.ProductMaterialId,
                    MaterialId = selected.MaterialId,
                    ProductId = selected.ProductId,
                    
                    Count = selected.Count
                }
                );
            int k = selected.Count;


            if (window.ShowDialog() == true)
            {
                selected = ZevaBdEntities.GetContext().ProductMaterials.Find(window.currentItem.ProductMaterialId);
                // получаем измененный объект
                if (selected != null)
                {

                    selected.ProductMaterialId = window.currentItem.ProductMaterialId;
                    selected.MaterialId = window.currentItem.MaterialId;
                    selected.ProductId = window.currentItem.ProductId;
                    selected.Count = window.currentItem.Count;
                    ZevaBdEntities.GetContext().Entry(selected).State = EntityState.Modified;
                    ZevaBdEntities.GetContext().SaveChanges();
                    LoadData(_currentItem);
                    MessageBox.Show("Запись изменена", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            //}
            //catch
            //{
            //    MessageBox.Show("Ошибка");
            //}
        }
    }
}
