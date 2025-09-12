using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMS.Business
{
    public interface ILibraryService
    {
        #region Catalog
        public void AddBook(Book book);
        public void UpdateBook(Book book);

        public void RemoveBook(Guid bookId);
        public List<Book> GetAllBooks();
        public List<Book> GetAllAvailableBooks();
        public BookDetailsDto? GetBookById(Guid bookId);

        #endregion
        #region Circulation

        public Loan Borrow(Guid libraryMimberId, Guid bookId, DateTime onTime);

        public decimal Return(Guid libraryMemberId, Guid bookId, DateTime returnedOnTime);
        #endregion
        #region Queries 
        public List<BorrowedBookDTO> GetAllOpenLoansByMember(Guid libraryMemberId);
        #endregion
        #region Admin
        public void ActivateCard(Guid libraryMemberId);
        public void DeactivateCard(Guid libraryMemberId);
        #endregion
    }
}