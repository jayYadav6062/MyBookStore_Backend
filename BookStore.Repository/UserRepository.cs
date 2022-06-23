using BookStore.Models.ViewModels;
using BookStore.Models.Models;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Repository
{
    public class UserRepository : BaseRepository
    {
        public User Login(LoginModel model)
        {
            return _context.Users.FirstOrDefault(c => c.Email.Equals(model.Email.ToLower()) && c.Password.Equals(model.Password));
        }

        public User Register(RegisterModel model)
        {
            User user = new User()
            {
                Email = model.Email,
                Password = model.Password,
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Roleid = model.Roleid,
            };
            var entry = _context.Users.Add(user);
            _context.SaveChanges();
            return entry.Entity;
        }

        public ListResponse<User> GetUsers(int pageIndex, int pageSize, string keyword)
        {
            keyword = keyword?.ToLower().Trim();
            var query = _context.Users.Where(c
                => keyword == null
                || c.Firstname.ToLower().Contains(keyword)
                || c.Lastname.ToLower().Contains(keyword)
            ).AsQueryable();

            int totalRecords = query.Count();
            IEnumerable<User> users = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return new ListResponse<User>()
            {
                records = users,
                TotalRecords = totalRecords
            };
        }

        public User GetUser(int id)
        {
            return _context.Users.FirstOrDefault(w => w.Id == id);
        }

        public User GetRole(int Roleid)
        {
            return _context.Users.FirstOrDefault(w => w.Roleid == Roleid);
        }


        public User UpdateUser(User model)
        {
            if (model == null || model.Id <= 0)
            {
                return null;
            }

            User exist = GetUser(model.Id);
            if (exist != null)
            {
                var entry = _context.Update(model);
                _context.SaveChanges();

                return entry.Entity;
            }

            return null;
        }

        public bool DeleteUser(int id)
        {
            User user = _context.Users.FirstOrDefault(w => w.Id == id);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            _context.SaveChanges();

            return true;
        }
    }
}
