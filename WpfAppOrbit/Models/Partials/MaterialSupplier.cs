using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppOrbit.Models
{
    public partial class MaterialSupplier
    {
        public int GetRate
        {
            get {

                return Convert.ToInt32(Math.Truncate(Quality / 100.0 * 5));
            }
        }
    }
}
