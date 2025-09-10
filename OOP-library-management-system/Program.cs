

using LibraryMS.Business;
using LibraryMS.Data;

namespace OOP_library_management_system
{
    internal class Program
    {
        static void Main(string[] args)
        {
               

            var bookRepo = new InMemoryBookRepository();
            var memberRepo = new InMemoryMemberRepository();
            var loanRepo = new InMemoryLoanRepository();
            var libraryService = new LibraryService(bookRepo, memberRepo, loanRepo);

        }
    }
}
