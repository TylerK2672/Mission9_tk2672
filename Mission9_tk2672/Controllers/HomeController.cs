using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission9_tk2672.Models;
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

        public IActionResult Index()
        {
            var blah = repo.Books.ToList();

            return View(blah);
        }

        public DbSet<Book> Books { get; set; }
    }
}
