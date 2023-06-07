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
using Excel = Microsoft.Office.Interop.Excel;

namespace WpfAppOrbit.Pages
{
    /// <summary>
    /// Логика взаимодействия для AgentsPage.xaml
    /// </summary>
    public partial class AgentsPage : Page
    {
        int _itemcount = 0;
        List<Agent> agents;
        public AgentsPage()
        {
            InitializeComponent();
        }

        private void TBoxSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }
        // Поиск агентов конкретного типа
        private void ComboTypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }
        /// <summary>
        /// Метод для фильтрации и сортировки данных
        /// </summary>
        private void UpdateData()
        {
            var currentAgents = ZevaBdEntities.GetContext().Agents.OrderBy(p => p.AgentName).ToList();
            // выбор только тех агентов, которые принадлежат данному типу
            if (ComboType.SelectedIndex > 0)
                currentAgents = currentAgents.Where(p => p.AgentTypeId == (ComboType.SelectedItem as AgentType).AgentTypeId).ToList();
            // выбор тех агентов, в названии которых есть поисковая строка
            currentAgents = currentAgents.Where(p => p.AgentName.ToLower().Contains(TBoxSearch.Text.ToLower())
            || p.Phone.ToLower().Contains(TBoxSearch.Text.ToLower()) ||
            p.Email.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            // сортировка
            if (ComboSort.SelectedIndex >= 0)
            {
                // сортировка по возрастанию названия
                if (ComboSort.SelectedIndex == 0)
                    currentAgents = currentAgents.OrderBy(p => p.AgentName).ToList();
                // сортировка по убыванию названия
                if (ComboSort.SelectedIndex == 1)
                    currentAgents = currentAgents.OrderByDescending(p => p.AgentName).ToList();
                // сортировка по возрастанию скидки
                if (ComboSort.SelectedIndex == 2)
                    currentAgents = currentAgents.OrderBy(p => p.Discount).ToList();
                // сортировка по убыванию номера цеха
                if (ComboSort.SelectedIndex == 3)
                    currentAgents = currentAgents.OrderByDescending(p => p.Discount).ToList();
                // сортировка по возрастанию минимальной стоимости
                if (ComboSort.SelectedIndex == 4)
                    currentAgents = currentAgents.OrderBy(p => p.Priority).ToList();
                // сортировка по убыванию минимальной стоимости
                if (ComboSort.SelectedIndex == 5)
                    currentAgents = currentAgents.OrderByDescending(p => p.Priority).ToList();
            }
            agents = currentAgents;

           
            TextBlockCount.Text = $" Результат запроса: {currentAgents.Count} записей из {_itemcount}";

        }
        // сортировка агентов 
        private void ComboSortSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            // открытие редактирования агента
            // передача выбранного агента в AddAgentPage
            Manager.MainFrame.Navigate(new AddAgentPage((sender as Button).DataContext as Agent));
        }

        private void PageIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //событие отображения данного Page
            // обновляем данные каждый раз когда активируется этот Page
            if (Visibility == Visibility.Visible)
            {
                DataGridAgents.ItemsSource = null;
                //загрузка обновленных данных
                ZevaBdEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                agents = ZevaBdEntities.GetContext().Agents.OrderBy(p => p.AgentName).ToList();
                DataGridAgents.ItemsSource = agents;
                _itemcount = agents.Count;
            }
        }

        private void BtnAddClick(object sender, RoutedEventArgs e)
        {
            // открытие  AddAgentPage для добавления новой записи
            Manager.MainFrame.Navigate(new AddAgentPage(null));
        }

        private void BtnDeleteClick(object sender, RoutedEventArgs e)
        {
            // удаление выбранного агента из таблицы
            //получаем все выделенные товары
            var selectedAgents = DataGridAgents.SelectedItems.Cast<Agent>().ToList();
            // вывод сообщения с вопросом Удалить запись?
            MessageBoxResult messageBoxResult = MessageBox.Show($"Удалить {selectedAgents.Count()} записей???",
          "Удаление", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            //если пользователь нажал ОК пытаемся удалить запись
            if (messageBoxResult == MessageBoxResult.OK)
            {
                try
                {
                    // берем из списка удаляемых товаров один элемент
                    Agent x = selectedAgents[0];
                    // проверка, есть ли у агента в таблице о продажах связанные записи
                    // если да, то выбрасывается исключение и удаление прерывается
                    if (x.SellHistories.Count > 0)
                        throw new Exception("Есть записи в продажах");
                    //ищем записи в таблице Точки продаж, с которой связан этот агента
                    var agentPoints = ZevaBdEntities.GetContext().AgentPoints.Where(p => p.AgentId == x.AgentId).ToList();
                    // удаляем эти записи
                    ZevaBdEntities.GetContext().AgentPoints.RemoveRange(agentPoints);
                    // удаляем товара
                    //ищем записи в таблице история изменения приоритета, с которой связан этот агента
                    var historyPriority = ZevaBdEntities.GetContext().PriorityHistories.Where(p => p.AgentId == x.AgentId).ToList();
                    // удаляем эти записи
                    ZevaBdEntities.GetContext().PriorityHistories.RemoveRange(historyPriority);
                    // удаляем агента
                    ZevaBdEntities.GetContext().Agents.Remove(x);
                    //сохраняем изменения
                    ZevaBdEntities.GetContext().SaveChanges();
                    MessageBox.Show("Записи удалены");
                    agents = ZevaBdEntities.GetContext().Agents.OrderBy(p => p.AgentName).ToList();
                    DataGridAgents.ItemsSource = null;
                    DataGridAgents.ItemsSource = agents;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            // удаление выбранного агента из таблицы
            //получаем все выделенные товары
            Agent deleted = (sender as Button).DataContext as Agent;
            // вывод сообщения с вопросом Удалить запись?
            MessageBoxResult messageBoxResult = MessageBox.Show($"Удалить запись???",
          "Удаление", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            //если пользователь нажал ОК пытаемся удалить запись
            if (messageBoxResult == MessageBoxResult.OK)
            {
                try
                {

                    if (deleted.Orders.Count > 0)
                        throw new Exception("Есть записи в продажах");
                    //ищем записи в таблице Точки продаж, с которой связан этот агента
                    ZevaBdEntities.GetContext().Agents.Remove(deleted);
                    //сохраняем изменения
                    ZevaBdEntities.GetContext().SaveChanges();
                    MessageBox.Show("Записи удалены");
                    agents = ZevaBdEntities.GetContext().Agents.OrderBy(p => p.AgentName).ToList();
                    DataGridAgents.ItemsSource = null;
                    DataGridAgents.ItemsSource = agents;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        // отображение номеров строк в DataGrid
        private void DataGridAgetsLoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void BtnSellsClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new SellHistoryPage1((sender as Button).DataContext as Agent));
        }

        private void BtnAgentTypes_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AgentTypesPage());
        }

        private void BtnExcel_Click(object sender, RoutedEventArgs e)
        {
            PrintExcel();
        }
        private void PrintExcel()
        {
            string fileName = AppDomain.CurrentDomain.BaseDirectory + "\\" + "Agents" + ".xltx";
            Excel.Application xlApp = new Excel.Application();
            Excel.Worksheet xlSheet = new Excel.Worksheet();
            try
            {
                //добавляем книгу
                xlApp.Workbooks.Open(fileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                          Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                          Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                          Type.Missing, Type.Missing);
                //делаем временно неактивным документ
                xlApp.Interactive = false;
                xlApp.EnableEvents = false;
                Excel.Range xlSheetRange;
                //выбираем лист на котором будем работать (Лист 1)
                xlSheet = (Excel.Worksheet)xlApp.Sheets[1];
                //Название листа
                xlSheet.Name = "Список поставщиков";
                int row = 2;
                int i = 0;


                foreach (Agent agent in agents)
                {
                    xlSheet.Cells[row, 1] = (i + 1).ToString();
                    string s;
                    // DateTime y = Convert.ToDateTime(dtOrders.Rows[i].Cells[1].Value);
                    xlSheet.Cells[row, 2] = agent.AgentId.ToString();
                    s = "";


                    xlSheet.Cells[row, 3] = agent.AgentName.ToString();
                    xlSheet.Cells[row, 4] = agent.AgentType.AgentTypeName.ToString();
                    xlSheet.Cells[row, 5] = agent.ManagerFIO.ToString();
                    xlSheet.Cells[row, 6] = agent.INN.ToString();
                    xlSheet.Cells[row, 7] = agent.KPP.ToString();
                    xlSheet.Cells[row, 8] = agent.LegalAddress.ToString();
                    xlSheet.Cells[row, 9] = agent.Phone.ToString();
                    xlSheet.Cells[row, 10] = agent.Email.ToString();
                    s = "";
                    if (agent.Priority != 0) s = agent.Priority.ToString();
                    xlSheet.Cells[row, 11] = s;
                   
                    row++;
                    Excel.Range r = xlSheet.get_Range("A" + row.ToString(), "K" + row.ToString());
                    r.Insert(Excel.XlInsertShiftDirection.xlShiftDown);
                    i++;
                }




                row--;
                xlSheetRange = xlSheet.get_Range("A2:K" + (row + 1).ToString(), Type.Missing);
                xlSheetRange.Borders.LineStyle = true;
                //xlSheet.Cells[row + 1, 9] = "=SUM(I2:I" + row.ToString() + ")";

                //xlSheet.Cells[row + 1, 8] = "ИТОГО:";
                row++;

                //выбираем всю область данных*/
                xlSheetRange = xlSheet.UsedRange;
                //выравниваем строки и колонки по их содержимому
                xlSheetRange.Columns.AutoFit();
                xlSheetRange.Rows.AutoFit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                //Показываем ексель
                xlApp.Visible = true;
                xlApp.Interactive = true;
                xlApp.ScreenUpdating = true;
                xlApp.UserControl = true;
            }
        }
    }
}

