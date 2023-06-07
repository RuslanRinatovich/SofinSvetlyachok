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
    /// Логика взаимодействия для MaterialWriteOffWindow.xaml
    /// </summary>
    public partial class MaterialWriteOffWindow : Window
    {
        public MaterialWriteOff currentItem { get; private set; }



        public MaterialWriteOffWindow(Material m, MaterialWriteOff b)
        {
            InitializeComponent();

            currentItem = b;

            if (b.MaterialWriteOffId == 0)
            {
                currentItem.OperationDate = DateTime.Now;

            }
            currentItem.MaterialId = m.MaterialId;
            DataContext = currentItem;

        }


        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();
            if (DatePickerDate.Value == null)
                s.AppendLine("Не выбрана дата");
            if (String.IsNullOrEmpty(TbReason.Text))
                s.AppendLine("Введите причину");
            return s;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder _error = CheckFields();
            // если ошибки есть, то выводим ошибки в MessageBox
            // и прерываем выполнение 
            if (_error.Length > 0)
            {
                MessageBox.Show(_error.ToString());
                return;
            }

            currentItem.OperationDate = Convert.ToDateTime(DatePickerDate.Value.ToString());
            this.DialogResult = true;
        }

      
    }
}