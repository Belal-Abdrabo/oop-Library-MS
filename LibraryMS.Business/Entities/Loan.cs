using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMS.Business
{
    public class Loan
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Guid LibraryMemberId { get; set; } // library member ID
        public string BorrowerName { get; set; } // library member
        public DateTime BorrowedOn { get; set; } // date when the book was borrowed
        public DateTime DueDate { get; set; } // date when the book is due to be returned
        public DateTime? ReturnDate { get; set; }

        //public Loan(DateTime dueDate, Guid bookId , string libraryMemberName, Guid libraryMemberId)
        //{
        //    Id = Guid.NewGuid();
        //    LoanDate = DateTime.Now;
        //    DueDate = dueDate;
        //    BookId = bookId;
        //    BorrowerName = libraryMemberName;
        //    LibraryMemberId = libraryMemberId;
        //    ReturnDate = null;
        //}
    }
}
