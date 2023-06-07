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
    /// Логика взаимодействия для SellWindow.xaml
    /// </summary>
    public partial class SellWindow : Window
    {
        public SellHistory currentItem { get; private set; }
        Product product;


        public SellWindow(SellHistory p, Product g)
        {
            InitializeComponent();

            List<Agent> agents = ZevaBdEntities.GetContext().Agents.ToList();

            ComboBoxAgent.ItemsSource = agents;
            TbProduct.Text = g.ProductName;
            currentItem = p;
            currentItem.Date = DateTime.Now;
            currentItem.ProductId = g.ProductId;
            product = g;

            DataContext = currentItem;
        }


        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();
            if (UpDownCount.Value is null)
                s.AppendLine("Количество не задано");
            if (DateTimeDateSell.Value is null)
                s.AppendLine("укажите дату продажи");
            if (ComboBoxAgent.SelectedIndex == -1)
                s.AppendLine("Не выбран агент");
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
            currentItem.Date = Convert.ToDateTime(DateTimeDateSell.Value.ToString());
            currentItem.Count = Convert.ToInt32(UpDownCount.Value);
            currentItem.AgentId = (ComboBoxAgent.SelectedItem as Agent).AgentId;
            //   currentItem.CategoryId = Convert.ToInt32(ComboCategory.SelectedValue);
            this.DialogResult = true;
        }


    }
}