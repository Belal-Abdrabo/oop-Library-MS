using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMS.Business
{
    public class BorrowedBookDTO
    {
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public string MemberName { get; set; } 
        public DateTime DueDate { get; set; }
        public DateTime BorrowedOn { get; set; } 
    }
}
