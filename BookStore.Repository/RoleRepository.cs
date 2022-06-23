using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class RoleRepository : BaseRepository
    {
        public ListResponse<Role> GetRoles(int pageIndex, int pageSize, string keyword)
        {
            keyword = keyword?.ToLower().Trim();
            var query = _context.Roles.Where(c
                => keyword == null
                || c.Name.ToLower().Contains(keyword)
            ).AsQueryable();

            int totalRecords = query.Count();
            IEnumerable<Role> roles = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return new ListResponse<Role>()
            {
                records = roles,
                TotalRecords = totalRecords
            };
        }

        
        public Role GetRole(int Id)
        {
            return _context.Roles.FirstOrDefault(w => w.Id == Id);
        }
    }
}
