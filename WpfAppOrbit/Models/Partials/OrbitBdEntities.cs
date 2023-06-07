using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppOrbit.Models
{
    public partial class ZevaBdEntities: DbContext
    {
        private static ZevaBdEntities _context;

        public static ZevaBdEntities GetContext()
        {
            if (_context == null)
            {
                _context = new ZevaBdEntities();
            }
            return _context;
        }
    }
}
