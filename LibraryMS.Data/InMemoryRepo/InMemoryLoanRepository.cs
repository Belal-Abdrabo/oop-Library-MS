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
        public void AddLoan(Loan loan)
        {
            throw new NotImplementedException();
        }

        public List<Loan> GetAllOpenLoansByMember(Guid libraryMemberId)
        {
            throw new NotImplementedException();
        }

        public Loan GetOpenLoan(Guid libraryMemberId, Guid bookId)
        {
            throw new NotImplementedException();
        }

        public void UpdateLoan(Loan loan)
        {
            throw new NotImplementedException();
        }
    }
}
