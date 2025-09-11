using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMS.Business
{
    public class Librarian : User
    {
        public string EmployeeId { get; set; }
        public LibrarianRoles Role { get; set; }
        public bool IsActive { get; set; }
    }
    
}
