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
    /// Логика взаимодействия для ChangePriorityWindow.xaml
    /// </summary>
    public partial class ChangePriorityWindow : Window
    {
        // это свойстсво позволяет вернуть значение поля Приоритет
        public short GetPriority { get; set; }
        // в конструктор формы передается текстовое представление максимального значения
        // приоритета среди выделенных агентов
        public ChangePriorityWindow(string value)
        {
            InitializeComponent();
            TextBoxPriority.Text = value;
        }
        // метод проверки поля на корректные данные
        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(TextBoxPriority.Text))
            {
                int x = 0;
                if (!int.TryParse(TextBoxPriority.Text, out x))
                    s.AppendLine("Количество только число");
                else if (x < 0)
                {
                    s.AppendLine("Количество не может быть отрицательным");
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
            GetPriority = Convert.ToInt16(TextBoxPriority.Text);
            // Закрывается окно со статусом успешно
            this.DialogResult = true;
        }
    }
}

