using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Models
{
    public class CartBook
    {
        public CartBook() { }

        public CartBook(Cart cart)
        {
            Id = cart.Id;
            UserId = cart.Userid;
            book = new BookModel(cart.Book);
            Quantity = cart.Quantity;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int Bookid { get; set; }
        public int Quantity { get; set; }

        public BookModel book { get; set; }
    }
}
