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
        public void AddBook(Book book)
        {
            throw new NotImplementedException();
        }

        public List<Book> GetAllAvilableBooks()
        {
            throw new NotImplementedException();
        }

        public List<Book> GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public Book? GetBookById(Guid bookId)
        {
            throw new NotImplementedException();
        }

        public bool HasOpenLoans(Guid bookId)
        {
            throw new NotImplementedException();
        }

        public void RemoveBook(Guid bookId)
        {
            throw new NotImplementedException();
        }

        public void UpdateBook(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
