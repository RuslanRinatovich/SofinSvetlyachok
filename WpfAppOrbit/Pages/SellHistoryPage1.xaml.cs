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
using WpfAppOrbit.Windows;

namespace WpfAppOrbit.Pages
{
    /// <summary>
    /// Логика взаимодействия для SellHistoryPage1.xaml
    /// </summary>
    public partial class SellHistoryPage1 : Page
    {
        public SellHistoryPage1(Agent agent)
        {
            InitializeComponent();
            LoadData(agent);

        }
        private void DataGridGoodLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }


        // загрузка данных в DataGrid и ComboBox
        void LoadData(Agent agent)
        {
            DtData.ItemsSource = ZevaBdEntities.GetContext().SellHistories.Where(p => p.AgentId == agent.AgentId).OrderBy(p => p.Date).ToList();
            ComboAgents.ItemsSource = ZevaBdEntities.GetContext().Agents.OrderBy(p => p.AgentName).ToList(); ;
            ComboAgents.SelectedIndex = 0;
            ComboAgents.SelectedValue = agent.AgentId;
        }
        // фильтрация продаж по агенту
        private void ComboAgentsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboAgents.SelectedIndex >= 0)
            {
                int agentId = Convert.ToInt32(ComboAgents.SelectedValue);
                var x = ZevaBdEntities.GetContext().SellHistories.Where(p => p.AgentId == agentId).OrderBy(p => p.Date).ToList();
                DtData.ItemsSource = x;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Agent g = ComboAgents.SelectedItem as Agent;
            AddSellWindow window = new AddSellWindow(g);
            if (window.ShowDialog() == true)
            {
                ZevaBdEntities.GetContext().SellHistories.Add(window.currentItem);
                ZevaBdEntities.GetContext().SaveChanges();

                MessageBox.Show("Запись добавлена", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData(g);
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
                Agent g = ComboAgents.SelectedItem as Agent;

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


                    LoadData(g);
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
