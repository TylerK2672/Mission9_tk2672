using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission9_tk2672.Models;
using Mission9_tk2672.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission9_tk2672.Controllers
{
    public class HomeController : Controller
    {
        private IBookstoreRepository repo;

        public HomeController (IBookstoreRepository temp)
        {
            repo = temp;
        }

        public IActionResult Index(string category, int pageNum = 1)
        {
            int pageSize = 10;


            // Bundling up stuff to be sent to the index as a model (since we can only have one)
            var x = new BooksViewModel
            {
                Books = repo.Books
                .Where(b => b.Category == category || category == null)
                .OrderBy(b => b.Title)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    // ? is the if statement, : is the else statement
                    TotalNumProjects = 
                        (category == null 
                            ? repo.Books.Count() 
                            : repo.Books.Where(x => x.Category == category).Count()),
                    BooksPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };

            return View(x);
        }

        public DbSet<Book> Books { get; set; }
    }
}
