using LibraryMS.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMS.Data
{
    public class InMemoryBookRepository : IBookRepository
    {
        private List<Book> _books = new List<Book>()
        {
            new Book("Author 2","Title 2","ISBN002",2002 ,20),
            new Book("Author 3", "Title 3", "ISBN003", 2003 , 31),
            new Book("Author 4","Title 4", "ISBN004", 2004 , 10),
        
        };
        public void AddBook(Book book)
        {
            if(book == null)
            {
                throw new ArgumentNullException(nameof(book), "Book cannot be null");
            }
          
            _books.Add(book);
        }

        public void DecrementAvailableCopies(Guid bookId)
        {
            _books.FirstOrDefault(b => b.Id == bookId).CopiesAvailable--;
        }
        public void IncrementAvailableCopies(Guid bookId)
        {
            _books.FirstOrDefault(b => b.Id == bookId).CopiesAvailable++;
        }
        public List<Book> GetAllAvilableBooks()
        {
         
            return _books.Where(b => b.IsAvailable).ToList();
        }

        public List<Book> GetAllBooks()
        {
            return _books.ToList();
        }

        public Book? GetBookById(Guid bookId)
        {
           return _books.FirstOrDefault(b => b.Id == bookId);
        }

        public Book GetBookByIsbn(string isbn)
        {
            return _books.FirstOrDefault(b => b.ISBN == isbn);
        }
        public void RemoveBook(Guid bookId)
        {
           var bookToremove = _books.FirstOrDefault(b => b.Id == bookId);
            if(bookToremove != null)
            {
                bookToremove.IsDeleted = true;
            }
        }

        public void UpdateBook(Book book)
        {
            var bookToUpdate = _books.FirstOrDefault(b => b.Id == book.Id);

            // Check for null to prevent errors, but don't throw an exception
            if (bookToUpdate != null)
            {
                bookToUpdate.Title = book.Title;
                bookToUpdate.Author = book.Author;
                bookToUpdate.ISBN = book.ISBN;
                bookToUpdate.PublishedYear = book.PublishedYear;
                bookToUpdate.CopiesAvailable = book.CopiesAvailable;
                bookToUpdate.TotalCopies = book.TotalCopies;
            }
        }
    }
}
