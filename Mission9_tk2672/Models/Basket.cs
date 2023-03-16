using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mission9_tk2672.Models
{
    // this is our basket we use to check out
    public class Basket
    {
        public List<BasketLineItem> Items { get; set; } = new List<BasketLineItem>();

        // how we add an item to the basket
        public virtual void AddItem(Book book, int qty)
        {
            BasketLineItem line = Items
                .Where(b => b.Book.BookId == book.BookId)
                .FirstOrDefault();

            if (line == null)
            {
                Items.Add(new BasketLineItem
                {
                    Book = book,
                    Quantity = qty,
                });
            }
            else
            {
                line.Quantity += qty;
            }
        }
        
        public virtual void RemoveItem(Book book)
        {
            // Removes on item
            Items.RemoveAll(x => x.Book.BookId == book.BookId);
        }

        public virtual void ClearBasket ()
        {
            // Clears entire basket
            Items.Clear();
        }

        public virtual double CalculateTotal()
        {
            // I am setting the price to be 30 dollars per book
            double sum = Items.Sum(x => x.Quantity * 30);

            return sum;
        }
    }

    
    // could be in separate file
    public class BasketLineItem
    {
        [Key]
        public int LineId { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }

    }
}
