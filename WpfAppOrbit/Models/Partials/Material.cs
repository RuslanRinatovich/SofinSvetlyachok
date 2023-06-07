using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppOrbit.Models
{
    public partial class Material
    { /// <summary>
      /// Возвращает абсолютный путь к изображению
      /// </summary>

        public string GetPhoto
        {
            get
            {
                if (Image is null)
                    return Directory.GetCurrentDirectory() + @"\Images\picture.png";
                return Directory.GetCurrentDirectory() + @"\Images\" + Image.Trim();
            }
        }
      

        /// <summary>
        /// Задает цвет фона агента зеленый для агентов со скидкой более 25%
        /// </summary>
        public string GetColor
        {
            get
            {
                if (Count < MinimalCount)
                    return "#f19292";
                if (Count >= MinimalCount * 3)
                    return "#c9fcbb";
                return "#fff";

            }
        }
        public string GetInfo
        {
            get
            {
                return $"{MaterialType.MaterialTypeName} | {MaterialName} ";
            }
        }

        public string GetSuppliers
                {
            get 
            {
                List<string> vs = new List<string>();
                string s = "";
                foreach (MaterialSupplier x in MaterialSuppliers)
                {
                    //if (!(vs.Contains(x.Supplier.SupplierName)))
                    //{
                        vs.Add(x.Supplier.SupplierName);
                    s += x.Supplier.SupplierName;
                    //}
                    
                }

                return "Поставщики: " + String.Join(", ", vs.OrderBy(x => x).ToList());
                //return "Поставщикк" + MaterialSuppliers.Count.ToString() + vs.Count.ToString() + s;

            }
        }

        public string GetMinimumCount
        {

            get
                {
                return $"Минимальное количество: {MinimalCount} {UnitType.UnitTypeName}";

            }
        }

        public string GetStoreCount
        {

            get
            {
                return $"Остаток: {Count} {UnitType.UnitTypeName}";

            }
        }
    }
}
