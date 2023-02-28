using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission9_tk2672.Models.ViewModels
{
    public class PageInfo
    {
        public int TotalNumProjects { get; set; }
        public int BooksPerPage { get; set; }
        public int CurrentPage { get; set; }

        // Determine number of pages needed
        public int TotalPages => (int) Math.Ceiling((double) TotalNumProjects / BooksPerPage);
    }
}
