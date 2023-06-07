using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppOrbit.Models
{
    public partial class Product
    {
        /// <summary>
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
        public string MaterialsItems
        {
            get
            {
                List<string> materials = new List<string>();
                foreach (ProductMaterial x in ProductMaterials)
                {
                    materials.Add(x.Material.MaterialName);
                }
                return String.Join(", ", materials);

            }
        }
    }
}
