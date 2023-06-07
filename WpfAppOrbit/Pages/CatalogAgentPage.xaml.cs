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
    /// Логика взаимодействия для CatalogAgentPage.xaml
    /// </summary>
    public partial class CatalogAgentPage : Page
    {
            int _itemcount = 0;
        int _allitemcount = 0;
        int _pagenumber = 0;
        int _pagecount = 0;

        List<Agent> agents;

        public CatalogAgentPage()
        {
            InitializeComponent();
            // скрываем кнопку Изменить приоритет на
            BtnChangePriority.Visibility = Visibility.Collapsed;

            // загрузка данных в combobox + добавление дополнительной строки
            var agentTypes = ZevaBdEntities.GetContext().AgentTypes.OrderBy(p => p.AgentTypeName).ToList();
            agentTypes.Insert(0, new AgentType
            {
                AgentTypeName = "Все типы"
            }
            );
            ComboType.ItemsSource = agentTypes;
            ComboType.SelectedIndex = 0;
        }


        // Создание списка страниц
        public void InitializeListBoxPages()
        {
            // очишаем список
            ListBoxPageCount.Items.Clear();
            // узнаем количество страниц нужное для отображения данного количества записей
            _pagecount = _itemcount / 10;
            if (_itemcount % 10 != 0)
                _pagecount++;
            // добавляем в Листбокс элементы - номера страниц
            for (int i = 1; i <= _pagecount; i++)
            {
                ListBoxItem itm = new ListBoxItem();
                itm.Content = i.ToString();

                ListBoxPageCount.Items.Add(itm);
            }

        }

        // подгрузка данных из БД об агентах
        void LoadData()
        {
            ZevaBdEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            // загрузка данных в listview сортируем по названию

            agents = ZevaBdEntities.GetContext().Agents.OrderBy(p => p.AgentName).ToList();
            // отображение количества записей
            _allitemcount = agents.Count;
            _itemcount = _allitemcount;

            ListBoxAgents.ItemsSource = ZevaBdEntities.GetContext().Agents.OrderBy(p => p.AgentName).ToList();
            _pagenumber = 1;
            InitializeListBoxPages();

            // Метод GetRange позволяет выбрать из списка данных элементы 
            // Создает неполную копию диапазона элементов исходного списка List<T>.
            // GetRange (int index, int count);
            // index -  Отсчитываемый от нуля индекс списка List<T>, с которого начинается диапазон.
            // count -  Число элементов в диапазоне
            // если указать count за размером списка будет ошибка.
            int k = agents.Count - (_pagenumber - 1) * 10;
            if (k < 10)
                ListBoxAgents.ItemsSource = agents.GetRange((_pagenumber - 1) * 10, k);
            else
                ListBoxAgents.ItemsSource = agents.GetRange((_pagenumber - 1) * 10, 10);

            TextBlockCount.Text = $" Результат запроса: {_itemcount} записей из {_allitemcount}";
        }

        private void PageIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //обновление данных после каждой активации окна
            if (Visibility == Visibility.Visible)
            {
                LoadData();
            }
        }
        // Поиск агентов, которые содержат данную поисковую строку в имени или в почте или в телефоне
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


            // В качестве источника данных присваиваем список данных
            // пересчитываем список страниц
            _pagenumber = 1;
            agents = currentAgents;
            ListBoxAgents.ItemsSource = currentAgents;
            _itemcount = currentAgents.Count;
            InitializeListBoxPages();
            int k = agents.Count - (_pagenumber - 1) * 10;
            if (k < 10)
                ListBoxAgents.ItemsSource = agents.GetRange((_pagenumber - 1) * 10, k);
            else
                ListBoxAgents.ItemsSource = agents.GetRange((_pagenumber - 1) * 10, 10);
            // отображение количества записей
            TextBlockCount.Text = $" Результат запроса: {currentAgents.Count} записей из {_allitemcount}";

        }
        // сортировка агентов 
        private void ComboSortSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        // обработка выбора номера страницы которую надо отбразить
        private void ListBoxPageCount_SelectionChanged(object sender,
        SelectionChangedEventArgs e)
        {
            if (ListBoxPageCount.SelectedItems.Count == 0)
                return;
            ListBoxItem lbi = ((sender as ListBox).SelectedItem as ListBoxItem);

            _pagenumber = Convert.ToInt32(lbi.Content);

            int k = agents.Count - (_pagenumber - 1) * 10;
            if (k < 10)
                ListBoxAgents.ItemsSource = agents.GetRange((_pagenumber - 1) * 10, k);
            else
                ListBoxAgents.ItemsSource = agents.GetRange((_pagenumber - 1) * 10, 10);
            TextBlockCount.Text = $" Результат запроса: {_itemcount} записей из {_allitemcount}";
        }
        //кнопки перемещения между страницами
        private void BtnPrev_Click(object sender, RoutedEventArgs e)
        {
            if ((_pagenumber > 1))
                _pagenumber--;
            ListBoxPageCount.SelectedIndex = _pagenumber - 1;
        }
        //кнопки перемещения между страницами
        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            if ((_pagenumber < _pagecount))
                _pagenumber++;
            ListBoxPageCount.SelectedIndex = _pagenumber - 1;
        }

        // Открытие окна изменения приоритета
        private void BtnChangePriority_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //получаем всех выделенных агентов
                var selectedAgents = ListBoxAgents.SelectedItems.Cast<Agent>().ToList();

                if (selectedAgents.Count == 0) return;
                // получаем максимальное значение приоритета среди выделенных агентов
                string maxA = selectedAgents.Max(t => t.Priority).ToString();
                // открытие окна изменения приоритета
                ChangePriorityWindow window = new ChangePriorityWindow(maxA);
                if (window.ShowDialog() == true)
                {
                    // если была нажата кнопка Изменить, меняем значение приоритета у выделенных агентов
                    foreach (Agent agent in selectedAgents)
                    {
                        agent.Priority = window.GetPriority;
                    }
                    // сохраняем изменения
                    ZevaBdEntities.GetContext().SaveChanges();

                    MessageBox.Show("Записи изменены", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                    // загружаем измененные данные из бд
                    int p = _pagenumber;
                    LoadData();
                    // применяем фильтрацию и поиск
                    UpdateData();
                    //ListBoxPageCount.SelectedIndex = 0;
                    ListBoxPageCount.SelectedIndex = p - 1;
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }
        // при выделении в списке более одного агента 
        // становится доступна кнопка Изменить приоритет на
        private void ListBoxServices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxAgents.SelectedItems.Count > 1)
            {
                BtnChangePriority.Visibility = Visibility.Visible;
            }
            else
            {
                BtnChangePriority.Visibility = Visibility.Collapsed;
            }
        }
    }
}

