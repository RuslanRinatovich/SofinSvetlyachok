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
    /// Логика взаимодействия для CatalogMaterialPage.xaml
    /// </summary>
    public partial class CatalogMaterialPage : Page
    {
        int _itemcount = 0;
        int _allitemcount = 0;
        int _pagenumber = 0;
        int _pagecount = 0;

        List<Material> materials;

        public CatalogMaterialPage()
        {
            InitializeComponent();
            // скрываем кнопку Изменить приоритет на
            BtnChangePriority.Visibility = Visibility.Collapsed;

            // загрузка данных в combobox + добавление дополнительной строки
            var materialTypes = ZevaBdEntities.GetContext().MaterialTypes
                .OrderBy(p => p.MaterialTypeName).ToList();
            materialTypes.Insert(0, new MaterialType
            {
                MaterialTypeName = "Все типы"
            }
            );
            ComboType.ItemsSource = materialTypes;
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

            materials = ZevaBdEntities.GetContext().Materials.OrderBy(p => p.MaterialName).ToList();
            // отображение количества записей
            _allitemcount = materials.Count;
            _itemcount = _allitemcount;

            _pagenumber = 1;
            InitializeListBoxPages();

            // Метод GetRange позволяет выбрать из списка данных элементы 
            // Создает неполную копию диапазона элементов исходного списка List<T>.
            // GetRange (int index, int count);
            // index -  Отсчитываемый от нуля индекс списка List<T>, с которого начинается диапазон.
            // count -  Число элементов в диапазоне
            // если указать count за размером списка будет ошибка.
            int k = materials.Count - (_pagenumber - 1) * 10;
            if (k < 10)
                ListBoxMaterials.ItemsSource = materials.GetRange((_pagenumber - 1) * 10, k);
            else
                ListBoxMaterials.ItemsSource = materials.GetRange((_pagenumber - 1) * 10, 10);

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
            var currentMaterials =
                ZevaBdEntities.GetContext().Materials.OrderBy(p => p.MaterialName).ToList();
            // выбор только тех агентов, которые принадлежат данному типу
            if (ComboType.SelectedIndex > 0)
                currentMaterials = currentMaterials.Where(p => p.MaterialTypeId == (ComboType.SelectedItem as MaterialType).MaterialTypeId).ToList();
            // выбор тех агентов, в названии которых есть поисковая строка
            currentMaterials = currentMaterials.Where(p => p.MaterialName.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            // сортировка
            if (ComboSort.SelectedIndex >= 0)
            {
                // сортировка по возрастанию названия
                if (ComboSort.SelectedIndex == 0)
                    currentMaterials = currentMaterials.OrderBy(p => p.MaterialName).ToList();
                // сортировка по убыванию названия
                if (ComboSort.SelectedIndex == 1)
                    currentMaterials = currentMaterials.OrderByDescending(p => p.MaterialName).ToList();
                // сортировка по возрастанию скидки
                if (ComboSort.SelectedIndex == 2)
                    currentMaterials = currentMaterials.OrderBy(p => p.Count).ToList();
                // сортировка по убыванию номера цеха
                if (ComboSort.SelectedIndex == 3)
                    currentMaterials = currentMaterials.OrderByDescending(p => p.Count).ToList();
                // сортировка по возрастанию минимальной стоимости
                if (ComboSort.SelectedIndex == 4)
                    currentMaterials = currentMaterials.OrderBy(p => p.Price).ToList();
                // сортировка по убыванию минимальной стоимости
                if (ComboSort.SelectedIndex == 5)
                    currentMaterials = currentMaterials.OrderByDescending(p => p.Price).ToList();
            }


            // В качестве источника данных присваиваем список данных
            // пересчитываем список страниц
            _pagenumber = 1;
            materials = currentMaterials;
            ListBoxMaterials.ItemsSource = currentMaterials;
            _itemcount = currentMaterials.Count;
            InitializeListBoxPages();
            int k = materials.Count - (_pagenumber - 1) * 10;
            if (k < 10)
                ListBoxMaterials.ItemsSource = materials.GetRange((_pagenumber - 1) * 10, k);
            else
                ListBoxMaterials.ItemsSource = materials.GetRange((_pagenumber - 1) * 10, 10);
            // отображение количества записей
            TextBlockCount.Text = $" Результат запроса: {currentMaterials.Count} записей из {_allitemcount}";

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

            int k = materials.Count - (_pagenumber - 1) * 10;
            if (k < 10)
                ListBoxMaterials.ItemsSource = materials.GetRange((_pagenumber - 1) * 10, k);
            else
                ListBoxMaterials.ItemsSource = materials.GetRange((_pagenumber - 1) * 10, 10);
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
                var selectedAgents = ListBoxMaterials.SelectedItems.Cast<Material>().ToList();

                if (selectedAgents.Count == 0) return;
                // получаем максимальное значение приоритета среди выделенных агентов
                string maxA = selectedAgents.Max(t => t.MinimalCount).ToString();
                // открытие окна изменения приоритета
                ChangeMinimalWindow window = new ChangeMinimalWindow(maxA);
                if (window.ShowDialog() == true)
                {
                    // если была нажата кнопка Изменить, меняем значение приоритета у выделенных агентов
                    foreach (Material material in selectedAgents)
                    {
                        material.MinimalCount = window.GetCount;
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
        private void ListBoxMaterials_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxMaterials.SelectedItems.Count > 1)
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
