using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gerber_demex.models;

namespace Gerber_demex.helpersClass
{
    internal class Helper
    {
        private static tehnEntities _context;
        public static tehnEntities GetContext()
        {
            if (_context == null)
            {
                _context = new tehnEntities();// делаем контекст базы доступной для использования
            }
            return _context;
        }
    }
}
