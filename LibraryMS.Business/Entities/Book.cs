using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMS.Business
{
    public class Book
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string ISBN { get; set; }
        public int PublishedYear { get; set; }
        public int CopiesAvailable { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int TotalCopies { get; set; }
        public bool IsAvailable => CopiesAvailable > 0;

        public Book(string title, string author, string isbn, int publishedYear, int totalCopies)
            {
            Title = title;
            Author = author;
            ISBN = isbn;
            PublishedYear = publishedYear;
            TotalCopies = totalCopies;
            CopiesAvailable = totalCopies; // Initially, all copies are available
        }

    }
}
