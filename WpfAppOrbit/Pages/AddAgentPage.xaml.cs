using Microsoft.Win32;
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
using WpfAppOrbit.Models;
using WpfAppOrbit.Windows;

namespace WpfAppOrbit.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddAgentPage.xaml
    /// </summary>
    public partial class AddAgentPage : Page
    {
        private Agent _currentAgent = new Agent();
        // путь к файлу
        private string _filePath = null;
        // название текущей главной фотографии
        private string _photoName = null;
        // текущая папка приложения
        private static string _currentDirectory =
       Directory.GetCurrentDirectory() + @"\Images\";

        public AddAgentPage(Agent selectedAgent)
        {
            InitializeComponent();
            // если передано null, то мы добавляем нового агент
            if (selectedAgent != null)
            {
                _currentAgent = selectedAgent;
                _filePath = _currentDirectory + _currentAgent.Logo;
                //  загрузка информации о продажах в DataGrid
                DtData.ItemsSource =
                 ZevaBdEntities.GetContext().SellHistories.
                 Where(p => p.AgentId == selectedAgent.AgentId).OrderBy(p => p.Date).ToList();
            }
            // контекст данных текущий агент
            DataContext = _currentAgent;
            _photoName = _currentAgent.Logo;
            //загрузка типов агентов
            ComboAgentType.ItemsSource = ZevaBdEntities.GetContext().AgentTypes.ToList();
        }
        // Проверка полей на некорректные данные
        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentAgent.AgentName))
                s.AppendLine("Поле название пустое");
            if (_currentAgent.AgentType == null)
                s.AppendLine("Выберите тип");
           

            if (string.IsNullOrWhiteSpace(_photoName))
                s.AppendLine("фото не выбрано пустое");
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
            if (_currentAgent.AgentId == 0)
            {
                // добавление нового агента, формируем новое название файла картинки,
                // так как в папке может быть файл с тем же именем
                string photo = ChangePhotoName();
                // путь куда нужно скопировать файл
                string dest = _currentDirectory + photo;
                File.Copy(_filePath, dest);
                _currentAgent.Logo = photo;
                // добавляем агента в БД
                ZevaBdEntities.GetContext().Agents.Add(_currentAgent);
            }
            try
            { // если изменилось изображение
                if (_filePath != null)
                {
                    string photo = ChangePhotoName();
                    string dest = _currentDirectory + photo;
                    File.Copy(_filePath, dest);
                    _currentAgent.Logo = photo;
                }
                ZevaBdEntities.GetContext().SaveChanges();  // Сохраняем изменения в БД
                MessageBox.Show("Запись Изменена");
                Manager.MainFrame.GoBack();  // Возвращаемся на предыдущую форму
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        // загрузка данных о продажах конкретного агента
        void LoadData(Agent agent)
        {
            DtData.ItemsSource = ZevaBdEntities.GetContext().SellHistories.Where(p => p.AgentId == agent.AgentId).OrderBy(p => p.Date).ToList();
        }
        // загрузка фото 
        private void BtnLoadClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //Диалог открытия файла
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
                // диалог вернет true, если файл был открыт
                if (op.ShowDialog() == true)
                {
                    // проверка размера файла
                    // по условию файл дожен быть не более 2Мб.
                    FileInfo fileInfo = new FileInfo(op.FileName);
                    if (fileInfo.Length > (1024 * 1024 * 2))
                    {
                        // размер файла меньше 2Мб. Поэтому выбрасывается новое исключение
                        throw new Exception("Размер файла должен быть меньше 2Мб");
                    }
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
            {// если мы добавляем нового агента, то продаж у него еще нет
                if (_currentAgent.AgentId == 0) return;
                // Открываем окно добавления новой продажи
                AddSellWindow window = new AddSellWindow(_currentAgent);
                // если в окне добавления продажи нажата кнопка ОК
                if (window.ShowDialog() == true)
                {
                    // добавляем новую продажу
                    ZevaBdEntities.GetContext().SellHistories.Add(window.currentItem);
                    ZevaBdEntities.GetContext().SaveChanges();
                    MessageBox.Show("Запись добавлена", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                    // после добавления продажи
                    // подгружаем измененные данные
                    LoadData(_currentAgent);
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
                    SellHistory deletedItem = DtData.SelectedItem as SellHistory;
                    ZevaBdEntities.GetContext().SellHistories.Remove(deletedItem);
                    ZevaBdEntities.GetContext().SaveChanges();
                    // после удаления продажи
                    // подгружаем измененные данные
                    LoadData(_currentAgent);
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
