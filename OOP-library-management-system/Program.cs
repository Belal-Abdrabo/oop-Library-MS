

using LibraryMS.Business;
using LibraryMS.Business.Entities;
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
            var perDayFine = new PerDayFinePolicy();
            var libraryService = new LibraryService(bookRepo, memberRepo, loanRepo,perDayFine);


            Console.WriteLine("--- Test Case Start ---");

            // Add a new member
           
            var newMember = new LibraryMember("Belal");
            memberRepo.AddMember(newMember);
            Console.WriteLine($"Member '{newMember.Name}' added with ID: {newMember.Id}");

            // Add a book with a due date of tomorrow for the first test
            
            var book1 = new Book( "Clean Code", "Robert C. Martin","1314414", 2008, 44);
            libraryService.AddBook(book1);
            Console.WriteLine($"Book '{book1.Title}' added with ID: {book1.Id}");

            // Add a second book to test on-time return
           
            var book2 = new Book( "Design Patterns", "Erich Gamma", "67890", 1994, 21);
            libraryService.AddBook(book2);
            Console.WriteLine($"Book '{book2.Title}' added with ID: {book2.Id}");

            // 2. Test Borrowing
            // Borrow book 1
            Console.WriteLine("\n--- Testing Borrowing ---");
            try
            {
                libraryService.Borrow(newMember.Id, book1.Id, DateTime.Today.AddDays(7));
                Console.WriteLine($"Successfully borrowed '{book1.Title}'. Due date: {DateTime.Today.AddDays(7):d}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Borrowing failed: {ex.Message}");
            }

            // Try to borrow the same book again (should fail)
            try
            {
                libraryService.Borrow(newMember.Id, book1.Id, DateTime.Today.AddDays(7));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Borrowing '{book1.Title}' again failed as expected: {ex.Message}");
            }

            // 3. Test On-Time Return (for book 2)
            // Borrow book 2 and return it immediately
            Console.WriteLine("\n--- Testing On-Time Return ---");
            libraryService.Borrow(newMember.Id, book2.Id, DateTime.Today.AddDays(7));
            try
            {
                var fine = libraryService.Return(newMember.Id, book2.Id, DateTime.Today);
                Console.WriteLine($"Successfully returned '{book2.Title}'. Fine calculated: ${fine}");
                Console.WriteLine($"Member fine balance is now: ${memberRepo.GetMemberById(newMember.Id).LibraryCard.FineBalance}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"On-time return failed: {ex.Message}");
            }

            // 4. Test Late Return (for book 1)
            // Simulate passing time and return book 1 late
            Console.WriteLine("\n--- Testing Late Return ---");
            var returnDate = DateTime.Today.AddDays(10);
            try
            {
                var fine = libraryService.Return(newMember.Id, book1.Id, returnDate);
                Console.WriteLine($"Successfully returned '{book1.Title}' late. Return date: {returnDate:d}");
                Console.WriteLine($"Fine calculated: ${fine}");
                Console.WriteLine($"Member fine balance is now: ${memberRepo.GetMemberById(newMember.Id).LibraryCard.FineBalance}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Late return failed: {ex.Message}");
            }

            // 5. Test Deactivate Card (should fail due to fine)
            Console.WriteLine("\n--- Testing Card Deactivation ---");
            try
            {
                libraryService.DeactivateCard(newMember.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Card deactivation failed as expected: {ex.Message}");
            }

            Console.WriteLine("\n--- Test Case Finished ---");
        }
    }
}
