using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMS.Business
{
    public class LibraryService : ILibraryService
    {

        private readonly IBookRepository _bookRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly ILoanRepository _loanRepository;

        public LibraryService(IBookRepository bookRepository, IMemberRepository memberRepository, ILoanRepository loanRepository)
        {
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
            _loanRepository = loanRepository;
        }

        public void ActivateCard(Guid libraryMemberId)
        {
            _bookRepository.GetBookById(libraryMemberId);
        }

        public void AddBook(Book book)
        {
            throw new NotImplementedException();
        }

        public Loan Borrow(Guid libraryMimberId, Guid bookId, DateTime onTime)
        {
            throw new NotImplementedException();
        }

        public void DeactivateCard(Guid libraryMemberId)
        {
            throw new NotImplementedException();
        }

        public List<Book> GetAllAvailableBooks()
        {
            throw new NotImplementedException();
        }

        public List<Book> GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public List<Loan> GetAllOpenLoansByMember(Guid libraryMemberId)
        {
            throw new NotImplementedException();
        }

        public Book? GetBookById(Guid bookId)
        {
            throw new NotImplementedException();
        }

        public void RemoveBook(Guid bookId)
        {
            throw new NotImplementedException();
        }

        public decimal Return(Guid libraryMemberId, Guid bookId, DateTime returnedOnTime)
        {
            throw new NotImplementedException();
        }

        public void UpdateBook(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
