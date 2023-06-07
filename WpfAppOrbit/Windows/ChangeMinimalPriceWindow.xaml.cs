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

namespace WpfAppOrbit.Windows
{
    /// <summary>
    /// Логика взаимодействия для ChangeMinimalPriceWindow.xaml
    /// </summary>
    public partial class ChangeMinimalPriceWindow : Window
    {
        // это свойстсво позволяет вернуть значение поля Приоритет
        public double GetMinimalPrice { get; set; }
        // в конструктор формы передается текстовое представление максимального значения
        // приоритета среди выделенных агентов
        public ChangeMinimalPriceWindow(string value)
        {
            InitializeComponent();
            TextBoxCount.Text = value;
        }
        // метод проверки поля на корректные данные
        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(TextBoxCount.Text))
            {
                double x = 0;
                if (!double.TryParse(TextBoxCount.Text, out x))
                    s.AppendLine("Стоимость только число");
                else if (x < 0)
                {
                    s.AppendLine("Стоимость не может быть отрицательной");
                }
            }

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
            // свойство GetPriority получает значение из TextBox
            GetMinimalPrice = Convert.ToDouble(TextBoxCount.Text);
            // Закрывается окно со статусом успешно
            this.DialogResult = true;
        }
    }
}