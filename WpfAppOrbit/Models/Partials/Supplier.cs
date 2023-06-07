using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppOrbit.Models
{
    public partial class Supplier
    {
        public int GetRate
        {
            get
            {

                return Convert.ToInt32(Math.Truncate(Rate / 100.0 * 5));
            }
        }
    }
}
