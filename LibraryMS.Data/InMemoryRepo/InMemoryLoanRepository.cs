using LibraryMS.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMS.Data
{
    public class InMemoryLoanRepository : ILoanRepository
    {
        private List<Loan> _loans = new List<Loan>()
        {
            new Loan(DateTime.Now.AddDays(14), Guid.NewGuid(), Guid.NewGuid())
        };
        public void AddLoan(Loan loan)
        {
           if(loan != null) 
            { 
             _loans.Add(loan);
            }else 
                throw new ArgumentNullException(nameof(loan), "Loan cannot be null");
        }

        public List<Loan> GetAllOpenLoansByMember(Guid libraryMemberId)
        {
             return _loans.Where(l => l.LibraryMemberId == libraryMemberId && l.ReturnDate == null).ToList();
        }

        public Loan GetOpenLoan(Guid libraryMemberId, Guid bookId)
        {
            
            return _loans.FirstOrDefault(l => l.LibraryMemberId == libraryMemberId && l.BookId == bookId && l.ReturnDate == null);
        }

        public int GetTotalLoansCountForBook(Guid bookId)
        {
            return _loans.Count(l => l.BookId == bookId);
        }

        public bool HasOpenLoans(Guid bookId)
        {
            return _loans.Any(l => l.BookId == bookId && l.ReturnDate == null);
        }

        public void UpdateLoan(Loan loan)
        {
           var loanToUpdate = _loans.FirstOrDefault(l => l.Id == loan.Id);
            if(loanToUpdate != null)
            {
                loanToUpdate.ReturnDate = loan.ReturnDate;
                loanToUpdate.DueDate = loan.DueDate;
            }
        }
    }
}
