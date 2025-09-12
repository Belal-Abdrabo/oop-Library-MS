using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMS.Business
{
    public interface IBookRepository
    {
        public Book? GetBookById(Guid bookId);
        public List<Book> GetAllBooks();
        public List<Book> GetAllAvilableBooks();
        public void AddBook(Book book);

        public void UpdateBook(Book book);

        //(only if no open loans)
        public void RemoveBook(Guid bookId);
        public void DecrementAvailableCopies(Guid bookId);
        public void IncrementAvailableCopies(Guid bookId);
        Book GetBookByIsbn(string isbn);

    }
}
