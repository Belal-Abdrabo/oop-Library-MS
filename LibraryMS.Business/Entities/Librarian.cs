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
        public bool IsDeleted { get; set; } = false;


        public Librarian(string name, string employeeId, LibrarianRoles role)
        {
            Id = Guid.NewGuid();
            Name = name;
            EmployeeId = employeeId;
            Role = role;
            IsActive = true; // New librarians are active by default
        }

    }
    
}
