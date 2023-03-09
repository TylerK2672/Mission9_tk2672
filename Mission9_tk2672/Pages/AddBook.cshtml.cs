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

        public AddBookModel (IBookstoreRepository temp)
        {
            repo = temp;
        }

        // from the basket classs
        public Basket basket { get; set; }
        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();
        }

        public IActionResult OnPost(int bookId, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookId);

            basket = HttpContext.Session.GetJson<Basket>("basket") ?? new Basket();
            basket.AddItem(b, 1);

            HttpContext.Session.SetJson("basket", basket);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
