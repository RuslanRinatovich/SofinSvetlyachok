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
    /// Логика взаимодействия для AddSellWindow.xaml
    /// </summary>
    public partial class AddSellWindow : Window
    {
        public SellHistory currentItem { get; private set; }
        Agent currentAgent;
        // Передаем в конструктор агента для которого нужно добавить продажу
        public AddSellWindow(Agent a)
        {
            InitializeComponent();
            currentAgent = a;
            // создаем экземпляр класса история продаж SellHistory
            currentItem = new SellHistory();
            // Устанавливаем в качестве даты текущую
            currentItem.Date = DateTime.Today;
            // подгружаем данные о продукции в выпадающий список
            ComboBoxProduct.ItemsSource = ZevaBdEntities.GetContext().Products.ToList();
            // устанавливаем контекст данных
            DataContext = currentItem;
        }

        // метод для проверки введенных данных на корректность в поля
        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();
            if (ComboBoxProduct.SelectedIndex == -1)
                s.AppendLine("Не выбран продукт");

            if (!string.IsNullOrWhiteSpace(TextBoxCount.Text))
            {
                int x = 0;
                if (!int.TryParse(TextBoxCount.Text, out x))
                    s.AppendLine("Количество только число");
                else if (x <= 0)
                {
                    s.AppendLine("Количество не может быть отрицательным или равным нулю");
                }
            }
            if (DatePickerDate.SelectedDate is null)
                s.AppendLine("Укажите дату");

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
            // формируем поля возвращаемого объекта
            currentItem.Date = Convert.ToDateTime(DatePickerDate.SelectedDate);
            currentItem.Count = Convert.ToInt32(TextBoxCount.Text);
            int id = Convert.ToInt32(ComboBoxProduct.SelectedValue);

            currentItem.ProductId = id;
            currentItem.AgentId = currentAgent.AgentId;
            this.DialogResult = true;
        }

    }
}
