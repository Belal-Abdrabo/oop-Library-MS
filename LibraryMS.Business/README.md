# Simple Library Management System

This project is a simple console-based library management system built with C# and clean design principles. It is structured using a layered architecture to be flexible, scalable, and easy to maintain. It serves as an excellent example of applying object-oriented programming (OOP) and software design patterns in a practical application.


## Features

- **Book Management:** Add, update, and retrieve books. The system enforces business rules, such as a minimum number of copies for new books.
- **Member Management:** Add new members and manage their library card status (activate/deactivate).
- **Loan Management:** Handle the borrowing and returning of books with due dates.
- **Fine Calculation:** Automatically calculates fines for overdue books based on a defined policy.
- **Data Validation:** Enforces strict business rules to prevent invalid operations (e.g., borrowing an unavailable book, deactivating a card with open loans or fines).

## Project Architecture

The project is structured into distinct layers to separate concerns, making the code more organized and easier to test:

* **Console App (Presentation Layer):** The entry point of the application. It handles user input and output and communicates with the business layer.
* **LibraryMS.Business (Business Layer):** Contains all the core business logic and rules. This layer is independent of the data storage and UI. The `LibraryService` class is the main orchestrator of the system's functions.
* **LibraryMS.Data (Data Layer):** Manages data persistence. It contains `Repositories` that handle CRUD (Create, Read, Update, Delete) operations. Currently, `InMemoryRepositories` are used for simplicity and rapid testing.

## Technologies Used

* C#
* .NET

## How to Run

1.  Clone the repository to your local machine.
2.  Open the project in Visual Studio.
3.  Run the project directly. The `Program.cs` file contains a comprehensive test case that demonstrates all the system's core functionalities.

## Future Enhancements

* **Database Integration:** Replace the `InMemoryRepositories` with a real database (e.g., SQL Server or SQLite) to persist data.
* **API Development:** Build a web API using ASP.NET Core to expose the `LibraryService` methods, allowing other applications (like a web or mobile app) to use the system.
* **Unit Testing:** Implement a full suite of unit tests to ensure every component of the system works correctly.
* **User Interface:** Create a more user-friendly interface to replace the console app.