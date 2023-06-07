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
    /// Логика взаимодействия для AddProductMaterialWindow.xaml
    /// </summary>
    public partial class AddProductMaterialWindow : Window
    {
        public ProductMaterial currentItem { get; private set; }



        public AddProductMaterialWindow(Product m, ProductMaterial b)
        {
            InitializeComponent();

            List<Material> materials = ZevaBdEntities.GetContext().Materials.OrderBy(x => x.MaterialName).ToList();
            currentItem = b;
            currentItem.ProductId = m.ProductId;

            ComboMaterial.ItemsSource = materials;
            DataContext = currentItem;
            // }
        }


        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();

            if (ComboMaterial.SelectedIndex == -1)
                s.AppendLine("Не выбран материал");
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

            currentItem.MaterialId = (ComboMaterial.SelectedItem as Material).MaterialId;
            this.DialogResult = true;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (currentItem.ProductMaterialId != 0)
            {
                Material s = ZevaBdEntities.GetContext().Materials.FirstOrDefault(x => x.MaterialId == currentItem.MaterialId);
                // ComboSupplier.SelectedValue = s.SupplierId;
                //  ComboSupplier.SelectedItem = s;
                ComboMaterial.Text = s.MaterialName;
            }
        }
    }
}