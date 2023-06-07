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
    /// Логика взаимодействия для MaterialSupplierWindow1.xaml
    /// </summary>
    public partial class MaterialSupplierWindow1 : Window
    {
        public MaterialSupplier currentItem { get; private set; }



        public MaterialSupplierWindow1(Material m, MaterialSupplier b)
        {
            InitializeComponent();

            List<Supplier> suppliers = ZevaBdEntities.GetContext().Suppliers.OrderBy(x => x.SupplierName).ToList();
            currentItem = b;
            
            if (b.MaterialSupplierId == 0)
            {
                currentItem.DeliveryDate = DateTime.Now;
                
            }
            currentItem.MaterialId = m.MaterialId;


            //MessageBox.Show(s);
            //DatePickerDate.Value = DateTime.Now;

            //if (b.MaterialSupplierId != 0)
            //{
            //    Supplier s = ZevaBdEntities.GetContext().Suppliers.FirstOrDefault(x => x.SupplierId == b.SupplierId);
            //    ComboSupplier.SelectedValue = s.SupplierId;
            //ComboSupplier.SelectedItem = s;
            //ComboSupplier.Text = s.SupplierName;
            ComboSupplier.ItemsSource = suppliers;
            DataContext = currentItem;
           

            // }
        }


        private StringBuilder CheckFields()
        {
            StringBuilder s = new StringBuilder();
            if (DatePickerDate.Value == null)
                s.AppendLine("Не выбрана дата");
            if (ComboSupplier.SelectedIndex == -1)
                s.AppendLine("Не выбран поставщик");
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
            
            currentItem.DeliveryDate = Convert.ToDateTime(DatePickerDate.Value.ToString());
            currentItem.SupplierId = (ComboSupplier.SelectedItem as Supplier).SupplierId;
            this.DialogResult = true;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (currentItem.MaterialSupplierId != 0)
            {
                Supplier s = ZevaBdEntities.GetContext().Suppliers.FirstOrDefault(x => x.SupplierId == currentItem.SupplierId);
               // ComboSupplier.SelectedValue = s.SupplierId;
              //  ComboSupplier.SelectedItem = s;
                ComboSupplier.Text = s.SupplierName;
            }
        }
    }
}