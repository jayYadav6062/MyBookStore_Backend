using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Repository
{
    public class BookRepository : BaseRepository
    {
        public ListResponse<Book> GetBooks(string keyword, int pageIndex, int pageSize)
        {
            keyword = keyword?.ToLower()?.Trim();
            var query = _context.Books.Include(c => c.Category).Where(c => keyword == null || c.Name.ToLower().Contains(keyword)).AsQueryable();
            var totalReocrds = query.Count();
            List<Book> books = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new ListResponse<Book>()
            {
                records = books,
                TotalRecords = totalReocrds,
            };
        }

        public Book GetBook(int id)
        {
            return _context.Books.FirstOrDefault(c => c.Id == id);
        }

        public Book AddBook(Book book)
        {
            var entry = _context.Books.Add(book);
            _context.SaveChanges();
            return entry.Entity;
        }

        public Book UpdateBook(Book book)
        {
            var entry = _context.Books.Update(book);
            _context.SaveChanges();
            return entry.Entity;
        }

        public bool DeleteBook(int id)
        {
            var book = _context.Books.FirstOrDefault(c => c.Id == id);
            if (book == null)
                return false;

            _context.Books.Remove(book);
            _context.SaveChanges();
            return true;
        }
    }
}
