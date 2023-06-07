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

namespace WpfAppOrbit.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddSupplierPage.xaml
    /// </summary>
    public partial class AddSupplierPage : Page
    {
        private Supplier _currentItem = new Supplier();

        public AddSupplierPage(Supplier selectedItem)
        {
            InitializeComponent();
            // если передано null, то мы добавляем нового агент
            if (selectedItem != null)
            {
                _currentItem = selectedItem;
            }
            // контекст данных текущий агент
            DataContext = _currentItem;

            ComboSupplierType.ItemsSource = ZevaBdEntities.GetContext().SupplierTypes.ToList();
        }

        // Проверка полей на некорректные данные
        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentItem.SupplierName))
                s.AppendLine("Поле название пустое");
            if (_currentItem.SupplierType == null)
                s.AppendLine("Выберите тип");
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
            if (_currentItem.SupplierId == 0)
            {
                ZevaBdEntities.GetContext().Suppliers.Add(_currentItem);
            }
            try
            {
                ZevaBdEntities.GetContext().SaveChanges();
                MessageBox.Show("Запись Изменена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void DtDataLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}