
using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Repository
{
    public class CategoryRepository : BaseRepository
    {
        public ListResponse<Category> GetCategories(int pageIndex, int pageSize, string keyword)
        {
            keyword = keyword?.ToLower()?.Trim();
            var query = _context.Categories.Where(c => keyword == null || c.Name.ToLower().Contains(keyword)).AsQueryable();
            var totalReocrds = query.Count();
            List<Category> categories = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new ListResponse<Category>()
            {
                records = categories,
                TotalRecords = totalReocrds,
            };
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);

        }

        public Category AddCategory(Category category)
        {
            if (category == null)
            {
                return null;
            }

            Category exist = GetCategory(category.Id);
            if (exist == null)
            {
                var entry = _context.Categories.Add(category);
                _context.SaveChanges();
                return entry.Entity;
            }

            return null;
        }

        public Category UpdateCategory(Category category)
        {

            if (category == null)
            {
                return null;
            }

            Category exist = GetCategory(category.Id);
            if (exist == null)
            {
                var entry = _context.Categories.Update(category);
                _context.SaveChanges();
                return entry.Entity;
            }

            return null;

            
        }

        public bool DeleteCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return false;

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return true;
        }
    }
}
