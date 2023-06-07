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
    /// Логика взаимодействия для AddMaterialPage1.xaml
    /// </summary>
    public partial class AddMaterialPage1 : Page
    {
        private Material _currentItem = new Material();
        // путь к файлу
        private string _filePath = null;
        // название текущей главной фотографии
        private string _photoName = null;
        // текущая папка приложения
        private static string _currentDirectory = Directory.GetCurrentDirectory() + @"\Images\";

        public AddMaterialPage1(Material selectedItem)
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
                //  загрузка информации о продажах в DataGrid
                DtData.ItemsSource =   ZevaBdEntities.GetContext().MaterialSuppliers.Where(p => p.MaterialId == selectedItem.MaterialId).OrderBy(p => p.DeliveryDate).ToList();
                DtDataWriteOff.ItemsSource = ZevaBdEntities.GetContext().MaterialWriteOffs.Where(p => p.MaterialId == selectedItem.MaterialId).OrderBy(p => p.OperationDate).ToList();
            }
            // контекст данных текущий агент
            DataContext = _currentItem;

            _photoName = _currentItem.Image;
            //загрузка типов агентов
            ComboMaterialType.ItemsSource = ZevaBdEntities.GetContext().MaterialTypes.ToList();
            ComboUnitType.ItemsSource = ZevaBdEntities.GetContext().UnitTypes.ToList();

        }

        // Проверка полей на некорректные данные
        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentItem.MaterialName))
                s.AppendLine("Поле название пустое");
            if (_currentItem.MaterialType == null)
                s.AppendLine("Выберите тип");
            if (_currentItem.UnitType == null)
                s.AppendLine("Выберите единицу измерения");
            

          
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
            if (_currentItem.MaterialId == 0)
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
                ZevaBdEntities.GetContext().Materials.Add(_currentItem);
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

        // загрузка данных о продажах конкретного агента
        void LoadData(Material agent)
        {
            DtData.ItemsSource = ZevaBdEntities.GetContext().MaterialSuppliers.Where(p => p.MaterialId == agent.MaterialId).OrderBy(p => p.DeliveryDate).ToList();
            DtDataWriteOff.ItemsSource = ZevaBdEntities.GetContext().MaterialWriteOffs.Where(p => p.MaterialId == agent.MaterialId).OrderBy(p => p.OperationDate).ToList();
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

        // кнопка добавления новой продажи
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            //    если мы только добавляем нового агента, то продаж у него еще нет
                if (_currentItem.MaterialId == 0) return;

                // Открываем окно добавления новой продажи
                MaterialSupplierWindow1 window = new MaterialSupplierWindow1(_currentItem, new MaterialSupplier());
                // если в окне добавления продажи нажата кнопка ОК
                if (window.ShowDialog() == true)
                {
                // добавляем новую продажу


                    _currentItem.Count += window.currentItem.Count;
                    UpDownCount.Value = _currentItem.Count;
                    ZevaBdEntities.GetContext().MaterialSuppliers.Add(window.currentItem);
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
                    MaterialSupplier deletedItem = DtData.SelectedItem as MaterialSupplier;

                    _currentItem.Count -= deletedItem.Count;
                    UpDownCount.Value = _currentItem.Count;
                    ZevaBdEntities.GetContext().MaterialSuppliers.Remove(deletedItem);
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
                if (_currentItem.MaterialId == 0) return;
                // если ни одного объекта не выделено, выходим
                if (DtData.SelectedItem == null) return;
                // получаем выделенный объект
                MaterialSupplier selected = DtData.SelectedItem as MaterialSupplier;


                MaterialSupplierWindow1 window = new MaterialSupplierWindow1(_currentItem,
                    new MaterialSupplier
                    {
                        MaterialSupplierId = selected.MaterialSupplierId,
                        MaterialId = selected.MaterialId,
                        SupplierId = selected.SupplierId,
                        Quality = selected.Quality,
                        Count = selected.Count,
                        DeliveryDate =selected.DeliveryDate
                    }
                    );
                int k = selected.Count;


                if (window.ShowDialog() == true)
                {
                    selected = ZevaBdEntities.GetContext().MaterialSuppliers.Find(window.currentItem.MaterialSupplierId);
                    // получаем измененный объект
                    if (selected != null)
                    {

                        selected.MaterialSupplierId = window.currentItem.MaterialSupplierId;
                        selected.MaterialId = window.currentItem.MaterialId;
                        selected.SupplierId = window.currentItem.SupplierId;
                        selected.Quality = window.currentItem.Quality;
                        selected.Count = window.currentItem.Count;
                        selected.DeliveryDate = window.currentItem.DeliveryDate;

                        k = selected.Count - k;
                        _currentItem.Count += k;
                        UpDownCount.Value = _currentItem.Count;
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

        private void btnAddWriteOff_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //    если мы только добавляем нового агента, то продаж у него еще нет
                if (_currentItem.MaterialId == 0) return;

                // Открываем окно добавления новой продажи
                MaterialWriteOffWindow window = new MaterialWriteOffWindow(_currentItem, new MaterialWriteOff());
                // если в окне добавления продажи нажата кнопка ОК
                if (window.ShowDialog() == true)
                {
                    // добавляем новую продажу


                    _currentItem.Count -= window.currentItem.Count;
                    UpDownCount.Value = _currentItem.Count;
                    ZevaBdEntities.GetContext().MaterialWriteOffs.Add(window.currentItem);
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

        private void btnDeleteWriteOff_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                // если ни одного объекта не выделено, выходим
                if (DtDataWriteOff.SelectedItem == null) return;
                // получаем выделенный объект
                MessageBoxResult messageBoxResult = MessageBox.Show($"Удалить запись? ", "Удаление", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.OK)
                {
                    MaterialWriteOff deletedItem = DtDataWriteOff.SelectedItem as MaterialWriteOff;

                    _currentItem.Count += deletedItem.Count;
                    UpDownCount.Value = _currentItem.Count;
                    ZevaBdEntities.GetContext().MaterialWriteOffs.Remove(deletedItem);
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

        private void btnChangeWriteOff_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_currentItem.MaterialId == 0) return;
                // если ни одного объекта не выделено, выходим
                if (DtDataWriteOff.SelectedItem == null) return;
                // получаем выделенный объект
                MaterialWriteOff selected = DtDataWriteOff.SelectedItem as MaterialWriteOff;


                MaterialWriteOffWindow window = new MaterialWriteOffWindow(_currentItem,
                    new MaterialWriteOff
                    {
                        MaterialWriteOffId = selected.MaterialWriteOffId,
                        MaterialId = selected.MaterialId,
                        Reason = selected.Reason,
                        Count = selected.Count,
                        OperationDate = selected.OperationDate
                    }
                    );
                int k = selected.Count;


                if (window.ShowDialog() == true)
                {
                    selected = ZevaBdEntities.GetContext().MaterialWriteOffs.Find(window.currentItem.MaterialWriteOffId);
                    // получаем измененный объект
                    if (selected != null)
                    {

                        selected.MaterialWriteOffId = window.currentItem.MaterialWriteOffId;
                        selected.MaterialId = window.currentItem.MaterialId;
                        selected.Reason = window.currentItem.Reason;
                        selected.Count = window.currentItem.Count;
                        selected.OperationDate = window.currentItem.OperationDate;

                        k = k - selected.Count;
                        _currentItem.Count += k;
                        UpDownCount.Value = _currentItem.Count;
                        ZevaBdEntities.GetContext().Entry(selected).State = EntityState.Modified;
                        ZevaBdEntities.GetContext().SaveChanges();
                        LoadData(_currentItem);
                        MessageBox.Show("Запись изменена", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }
    }
}
