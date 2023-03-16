using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mission9_tk2672.Infrastructure;
using Mission9_tk2672.Models;

namespace Mission9_tk2672.Pages
{
    public class AddBookModel : PageModel
    {

        //doing things we would have typically done in the controller, but now we don't have one so we do it here
        private IBookstoreRepository repo { get; set; }

        public AddBookModel (IBookstoreRepository temp, Basket b)
        {
            repo = temp;
            basket = b;
        }

        // from the basket classs
        public Basket basket { get; set; }
        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            
        }

        public IActionResult OnPost(int bookId, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookId);
            
            basket.AddItem(b, 1);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }

        public IActionResult OnPostRemove (int bookId, string returnUrl)
        {
            basket.RemoveItem(basket.Items.First(x => x.Book.BookId == bookId).Book);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
