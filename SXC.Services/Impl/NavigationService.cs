using SXC.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Newtonsoft.Json;

namespace SXC.Services.Impl
{
    public class NavigationService : ServiceBase
    {
        public async Task<List<Navigation>> GetAll1() 
        {
            using (var db = base.NewDB())
            {
                return await db.Navigations.ToListAsync();
            }
        }

        public List<Navigation> GetAll()
        {
            using (var db = base.NewDB())
            {
                return db.Navigations.ToList();
            }
        }

        public string GetJsonAll()
        {
            var res = GetAll();

            return JsonConvert.SerializeObject(res);
        }
        public string GetJsonNavigation()
        {
            var res = GetAll();

            return JsonConvert.SerializeObject(res);
        }

        public string CreateNavigations(Navigation nav)
        {
            using (var db = base.NewDB())
            {
                db.Navigations.Add(nav);

                db.SaveChanges();
            }

            return "";
        }
    }
}
