using SXC.Core.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SXC.Services
{
    public class BusinessBase
    {
        protected SxcDbContext _context;
        public BusinessBase(SxcDbContext DbContext)
        {
            _context = DbContext;
        }
    }
}
