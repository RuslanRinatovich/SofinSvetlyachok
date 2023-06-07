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
using System.Windows.Shapes;
using WpfAppOrbit.Models;
namespace WpfAppOrbit.Windows
{
    /// <summary>
    /// Логика взаимодействия для SupplierTypeWindow.xaml
    /// </summary>
    public partial class SupplierTypeWindow : Window
    {
        public SupplierType currentItem { get; private set; }
        public SupplierTypeWindow(SupplierType p)
        {
            InitializeComponent();
            currentItem = p;
            this.DataContext = currentItem;
        }

        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();
            if (String.IsNullOrEmpty(TbSupplierName.Text))
                s.AppendLine("Введите тип");
            return s;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {

            StringBuilder _error = CheckFields();
            // если ошибки есть, то выводим ошибки в MessageBox
            // и прерываем выполнение 
            if (_error.Length > 0)
            {
                MessageBox.Show(_error.ToString());
                return;
            }
            this.DialogResult = true;
        }
    }
}