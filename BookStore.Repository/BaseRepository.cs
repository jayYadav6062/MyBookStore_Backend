using BookStore.Models.ViewModels;

namespace BookStore.Repository
{
    public class BaseRepository
    {
        protected readonly BookstoreContext _context = new BookstoreContext();
    }
}
