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
        private readonly IFinePolicy _finePolicy;
        private const int MIN_COPIES_COUNT = 10;
        private const int MIN_PUBLISHED_YEAR = 1450;
        public LibraryService(IBookRepository bookRepository, IMemberRepository memberRepository, ILoanRepository loanRepository, IFinePolicy finePolicy)
        {
            if (bookRepository == null)
            {
                throw new ArgumentNullException(nameof(bookRepository), "Book repository cannot be null");
            }
            if (memberRepository == null)
            {
                throw new ArgumentNullException(nameof(memberRepository), "Member repository cannot be null");
            }
            if (loanRepository == null)
            {
                throw new ArgumentNullException(nameof(loanRepository), "Loan repository cannot be null");
            }
            if (finePolicy == null)
            {
                throw new ArgumentNullException(nameof(finePolicy), "Fine policy cannot be null");
            }


            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
            _loanRepository = loanRepository;
            _finePolicy = finePolicy;
        }

        #region Circulation
        public Loan Borrow(Guid libraryMemberId, Guid bookId, DateTime onTime)
        {
            //first check if the member exists , is active and his fine balance
            var member = _memberRepository.GetMemberById(libraryMemberId);
            if (member == null)
            {
                throw new ArgumentException("Member does not exist");
            }
            if (!member.LibraryCard.IsActive)
            {
                throw new InvalidOperationException("Library card is not active");
            }
            if (member.LibraryCard.FineBalance > 0)
            {
                throw new InvalidOperationException("Member has outstanding fines");
            }
            //check if the book exists and is available
            var book = _bookRepository.GetBookById(bookId);
            if (book == null)
            {
                throw new ArgumentException("Book is not exist");
            }
            if (book.IsDeleted)
            {
                throw new InvalidOperationException("Book is deleted");
            }
            if (!book.IsAvailable)
            {
                throw new InvalidOperationException("no Copies Available");
            }
            //create a new loan
            var loan = new Loan(onTime, bookId, libraryMemberId);
            _loanRepository.AddLoan(loan);
            //update book copiesavailable 
            _bookRepository.DecrementAvailableCopies(bookId);
            return loan;
        }

        public decimal Return(Guid libraryMemberId, Guid bookId, DateTime returnedDate)
        {

            var member = _memberRepository.GetMemberById(libraryMemberId);

            if (member == null)
            {
                throw new ArgumentException("Member does not exist");
            }

            var book = _bookRepository.GetBookById(bookId);

            if (book == null)
            {
                throw new ArgumentException("Book is not exist");
            }

            var returnedLoan = _loanRepository.GetOpenLoan(libraryMemberId, bookId);
            if (returnedLoan == null)
            {
                throw new InvalidOperationException("No open loan found for this member and book");
            }
            //calculate fine if any
            var fine = _finePolicy.CalculateFine(returnedLoan.DueDate, returnedDate);
            if (fine > 0)
            {
                member.LibraryCard.FineBalance += fine;
                _memberRepository.UpdateMember(member);
            }
            //update loan
            returnedLoan.ReturnDate = returnedDate;
            _loanRepository.UpdateLoan(returnedLoan);
            //update book copies
            _bookRepository.IncrementAvailableCopies(bookId);
            return fine;

        }
        #endregion
        #region Catalog

        public void AddBook(Book book)
        {
            //validation of book object
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book), "Book cannot be null");
            }
            if (string.IsNullOrWhiteSpace(book.ISBN))
            {
                throw new ArgumentException("ISBN cannot be null or empty");
            }
            if (_bookRepository.GetBookByIsbn(book.ISBN) != null)
            {
                throw new ArgumentException("Book with the same ISBN already exists");
            }
            if (string.IsNullOrWhiteSpace(book.Author) || string.IsNullOrWhiteSpace(book.Title))
            {
                throw new ArgumentException("Title And Author Name is Required");
            }
            if (book.TotalCopies <= MIN_COPIES_COUNT)
            {
                throw new ArgumentException("Copies must be greater than 10");
            }
            if (book.PublishedYear < MIN_PUBLISHED_YEAR || book.PublishedYear > DateTime.Now.Year)
            {
                throw new ArgumentException("Published year is not valid");
            }
            _bookRepository.AddBook(book);
        }
        public List<Book> GetAllAvailableBooks()
        {
            var availableBooks = _bookRepository.GetAllAvilableBooks();

            return availableBooks;
        }

        public List<Book> GetAllBooks()
        {
            var allBooks = _bookRepository.GetAllBooks();

            return allBooks;
        }
        public BookDetailsDto? GetBookById(Guid bookId)
        {
            if (bookId == Guid.Empty)
            {
                throw new ArgumentException("Book ID cannot be empty", nameof(bookId));
            }
            var book = _bookRepository.GetBookById(bookId);
            if (book == null) {
                throw new ArgumentException("Book not found", nameof(bookId));
            }
            return new BookDetailsDto
            {
                Title = book.Title,
                Author = book.Author,
                PublishedYear = book.PublishedYear,
                IsAvailable = book.CopiesAvailable > 0
            }; ;
        }
        public void RemoveBook(Guid bookId)
        {
            var bookRemove = _bookRepository.GetBookById(bookId);
            if (bookRemove == null)
            {
                throw new ArgumentException("Book not found", nameof(bookId));
            }
            var openLoans = _loanRepository.HasOpenLoans(bookId);
            if (openLoans)
            {
                throw new InvalidOperationException("Cannot remove book with open loans");
            }
            bookRemove.IsDeleted = true;
            _bookRepository.UpdateBook(bookRemove);
        }
        public void UpdateBook(Book book)
        {
            var existingBook = _bookRepository.GetBookById(book.Id);
            if (existingBook == null)
            {
                throw new ArgumentException("Book not found", nameof(book.Id));
            }
            //if (existingBook.ISBN != book.ISBN)
            //{
            //    throw new ArgumentException("ISBN cannot be change");
            //}
            //if (existingBook.Title != book.Title || existingBook.Author != book.Author)
            //{
            //    throw new ArgumentException("Title And Author Name can`t be changed");
            //}
            //if(existingBook.PublishedYear != book.PublishedYear)
            //{
            //    throw new ArgumentException("Published Year can`t be changed");
            //}
            if (existingBook.ISBN != book.ISBN ||
                existingBook.Title != book.Title ||
                existingBook.Author != book.Author ||
                existingBook.PublishedYear != book.PublishedYear)
                {
                    throw new ArgumentException("Core book properties (ISBN, Title, Author, Published Year) cannot be changed.");
                }
            var newAvailableCopies =book.TotalCopies - _loanRepository.GetTotalLoansCountForBook(book.Id);
            if (newAvailableCopies < 0)
            {
                throw new ArgumentException("Total copies cannot be less than the number of loans");
            }
            existingBook.TotalCopies = book.TotalCopies;
            existingBook.CopiesAvailable = newAvailableCopies;


            _bookRepository.UpdateBook(existingBook);
        }
        #endregion
        #region Queries 

        public List<BorrowedBookDTO> GetAllOpenLoansByMember(Guid libraryMemberId)
        {
            var member = _memberRepository.GetMemberById(libraryMemberId);
            if (member == null)
            {
                throw new ArgumentException("Member does not exist");
            }
            var borrowedBooks = new List<BorrowedBookDTO>();
            var openLoans = _loanRepository.GetAllOpenLoansByMember(libraryMemberId);
            foreach(var loan in openLoans)
            {
                var book = _bookRepository.GetBookById(loan.BookId);
                if (book != null)
                {
                 borrowedBooks.Add(new BorrowedBookDTO
                 {
                     BookTitle = book.Title,
                     DueDate = loan.DueDate,
                     MemberName = member.Name,
                     Author = book.Author,
                     BorrowedOn = loan.BorrowedOn
                 });
                }

            }
            return borrowedBooks;
        }

        #endregion
        #region Admin

        public void ActivateCard(Guid libraryMemberId)
        {
            var member = _memberRepository.GetMemberById(libraryMemberId);
            if (member == null)
            {
                throw new ArgumentException("Member does not exist");
            }
            if (member.LibraryCard == null)
            {
                throw new InvalidOperationException("Member does not have a library card");
            }
            if (member.LibraryCard.IsActive)
            {
                throw new InvalidOperationException("Library card is already active");
            }
            member.LibraryCard.IsActive = true;
            _memberRepository.UpdateMember(member);
        }
        public void DeactivateCard(Guid libraryMemberId)
        {
            var member = _memberRepository.GetMemberById(libraryMemberId);
            if(member == null)
            {
                throw new ArgumentException("Member does not exist");
            }
            if(member.LibraryCard == null)
            {
                throw new InvalidOperationException("Member does not have a library card");
            } 
            if(member.LibraryCard.FineBalance > 0)
            {
                throw new InvalidOperationException("Cannot deactivate card with outstanding fines");
            }
            if(_loanRepository.GetAllOpenLoansByMember(libraryMemberId).Count > 0)
            {
                throw new InvalidOperationException("Cannot deactivate card with open loans");
            }
            if(!member.LibraryCard.IsActive)
            {
                throw new InvalidOperationException("Library card is already deactivated");
            }
            member.LibraryCard.IsActive = false;
            _memberRepository.UpdateMember(member);
        }
        #endregion

     

    }
}
