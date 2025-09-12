using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMS.Business
{
    public interface ILoanRepository
    {
        public Loan GetOpenLoan(Guid libraryMemberId, Guid bookId);
        public List<Loan> GetAllOpenLoansByMember(Guid libraryMemberId);
        public void AddLoan(Loan loan);
        public void UpdateLoan(Loan loan);
        //for checking if a book is currently loaned out
        public bool HasOpenLoans(Guid bookId);
        public int GetTotalLoansCountForBook(Guid bookId);
    }
}
