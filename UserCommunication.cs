using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManager.BookProvider;
using LibraryManager.Entities;
using LibraryManager.Repositories;
using LibraryManager.Repositories.Extensions;

namespace LibraryManager;

public class UserCommunication : IUserCommunication
{
    private readonly IPersonInFile _personInFile;
    private readonly IItemInFile _itemInFile;

    public UserCommunication(IPersonInFile personInFile, IItemInFile itemInFile)
    {
        _personInFile = personInFile;
        _itemInFile = itemInFile;
    }

    public void ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("|*----------------MENU----------------*|\n");
        Console.WriteLine("(1)  Show all employees");
        Console.WriteLine("(2)  Add new employee");
        Console.WriteLine("(3)  Add new managner");
        Console.WriteLine("(4)  Remove person\n");
        Console.WriteLine("|*------------------------------------*|\n");
        Console.WriteLine("(5)  Show all books");
        Console.WriteLine("(6)  Add new book");
        Console.WriteLine("(7)  Remove book\n");
        Console.WriteLine("(q)  Exite\n");
    }

    public void Back()
    {
        Console.WriteLine("\n(1)  Continue");
        Console.WriteLine("(2)  Back");
    }

    public void ShowAllPerson(IReadRepository<Employee> repository)
    {
        while (true)
        {
            Console.Clear();

            var list = repository.GetAllPerson();

            Console.WriteLine("-----------------PERSONS LIST-----------------");
            Console.WriteLine("ID    |     FullName     |    Salary    |   Status");
            foreach (var person in list)
            {
                Console.WriteLine($"{person.Id}       {person.FirstName} {person.LastName}       {person.Salary}$      {person.Status}");
            }

            Console.WriteLine("\n(q)  Back");
            var input = Console.ReadKey();
            if (input.Key == ConsoleKey.Q) break;
        }
    }

    public void AddEmployee(IWriteRepository<Employee> sqlRepositoryOfPersons)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("----NEW EMPLOYEE----");
            Console.WriteLine("First Name: ");
            var firstName = Console.ReadLine();
            Console.WriteLine("Second Name: ");
            var secondName = Console.ReadLine();
            Console.WriteLine("Salary: ");

            double salary;
            if (!double.TryParse(Console.ReadLine(), out salary))
            {
                Console.WriteLine("Incorrect value");
            }
            else
            {
                var newPerson = new Employee { FirstName = firstName, LastName = secondName, Salary = salary, Status = "employee" };

                sqlRepositoryOfPersons.Add(newPerson);
                _personInFile.AddPersonToFile(newPerson);
                sqlRepositoryOfPersons.Save();
            }

            Back();
            var input = Console.ReadKey();
            if (input.Key == ConsoleKey.D2) break;
        }
    }

    public void AddManager(IWriteRepository<Manager> sqlRepositoryOfPersons)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("----NEW MANAGER----");
            Console.WriteLine("First Name: ");
            var firstName = Console.ReadLine();
            Console.WriteLine("Second Name: ");
            var secondName = Console.ReadLine();
            Console.WriteLine("Salary: ");
            double salary;

            if (!double.TryParse(Console.ReadLine(), out salary))
            {
                Console.WriteLine("Incorrect value");
            }
            else
            {
                var newPerson = new Manager { FirstName = firstName, LastName = secondName, Salary = salary, Status = "manager" };

                sqlRepositoryOfPersons.Add(newPerson);
                _personInFile.AddPersonToFile(newPerson);
                sqlRepositoryOfPersons.Save();
            }

            Back();
            var input = Console.ReadKey();
            if (input.Key == ConsoleKey.D2) break;
        }
    }

    public void RemovePerson(IRepository<Employee> repository)
    {
        while (true)
        {
            Console.Clear();

            var listPerson = repository.GetAllPerson();

            Console.WriteLine("-----------------PERSONS LIST-----------------");
            Console.WriteLine("ID    |     FullName     |    Salary    |   Status");
            foreach (var person in listPerson)
            {
                Console.WriteLine($"{person.Id}       {person.FirstName} {person.LastName}       {person.Salary}$      {person.Status}");
            }

            Console.WriteLine("\n");
            Console.Write("Enter Id to delete person: ");
            var sing = Console.ReadLine();
            int id;

            if (!int.TryParse(sing, out id))
            {
                Console.WriteLine("Incorrect value");
            }
            else
            {
                var list = repository.GetAllPerson();
                var error = true;

                foreach (var person in list)
                {
                    if (person.Id == id)
                    {
                        var personToDelete = repository.GetById(id);
                        Console.Clear();
                        Console.WriteLine($"Remove: {personToDelete.FirstName} {personToDelete.LastName}");
                        error = false;

                        Console.WriteLine("\nAre you sure?");
                        Console.WriteLine("\n(1) Yes     (2) No");
                        var choice = Console.ReadKey();

                        if (choice.Key == ConsoleKey.D1)
                        {
                            Console.Clear();
                            repository.Remove(personToDelete);
                            _personInFile.RemovePersonFromFile(personToDelete);
                            repository.Save();
                        }
                        else
                        {
                            Console.Clear();
                        }
                    }
                }

                if (error)
                {
                    Console.Clear();
                    Console.WriteLine("Unknown identifier, try again");
                }
            }

            Back();
            var input = Console.ReadKey();
            if (input.Key == ConsoleKey.D2) break;
        }
    }
    void ShowBooksList(IReadRepository<Book> repository)
    {
        var list = repository.GetAllBooks();

        Console.WriteLine("-----------------BOOKS LIST-----------------");
        Console.WriteLine("ID    |   Title    |    Description    |   Author    |    Price    |    Type");
        foreach (var book in list)
        {
            if (!(book.Description.Length >= 15))
            {
                string shortDescription = book.Description.Remove(book.Description.Length);

                Console.WriteLine($"{book.Id}       \"{book.Title}\"     {shortDescription}       {book.Author}      {book.Price}$   {book.Type}");
            }
            else
            {
                string shortDescription = book.Description.Remove(15);

                Console.WriteLine($"{book.Id}       \"{book.Title}\"     {shortDescription}...       {book.Author}      {book.Price}$   {book.Type}");
            }
        }


    }

    void ShowBookOptions()
    {
        Console.WriteLine("\n------------MENU--------------");
        Console.WriteLine("(1) Select only minimum price");
        Console.WriteLine("(2) ALl accessible types");
        Console.WriteLine("(3) Sort growing by price");
        Console.WriteLine("(4) Sort decreasing by price");
        Console.WriteLine("(5) Show all description");
    }

    public void ShowAllBooks(IReadRepository<Book> repository, IBookProvider bookProvider)
    {
        while (true)
        {
            Console.Clear();

            var list = repository.GetAllBooks();

            ShowBooksList(repository);
            ShowBookOptions();

            Console.WriteLine("\n(q)  Back");
            var choiceInMethod = Console.ReadKey();

            if (choiceInMethod.Key == ConsoleKey.D1)
            {
                Console.Clear();

                ShowBooksList(repository);

                Console.WriteLine("\n------------MENU--------------");
                Console.WriteLine($"The lowest price for a book is {bookProvider.GetMinimumPriceOfAllBooks()}$");
                Console.WriteLine("\n(q)  Back");
                Console.ReadKey();
            }
            else if (choiceInMethod.Key == ConsoleKey.D2)
            {
                Console.Clear();

                ShowBooksList(repository);

                Console.WriteLine("\n------------MENU--------------");

                foreach (var type in bookProvider.GetUniqueBookType())
                {
                    Console.WriteLine(type);
                }

                Console.WriteLine("\n(q)  Back");
                Console.ReadKey();
            }
            else if (choiceInMethod.Key == ConsoleKey.D3)
            {
                Console.Clear();

                Console.WriteLine("-----------------BOOKS LIST----------------- (Sort growing by price)");
                Console.WriteLine("ID    |   Title    |    Description    |   Author    |    Price    |    Type");
                foreach (var book in bookProvider.OderByPriceUp())
                {
                    Console.WriteLine($"{book.Id}       \"{book.Title}\" {book.Description}       {book.Author}      {book.Price}$   {book.Type}");
                }

                Console.WriteLine("\n(q)  Back");
                Console.ReadKey();
            }
            else if (choiceInMethod.Key == ConsoleKey.D4)
            {
                Console.Clear();

                Console.WriteLine("-----------------BOOKS LIST----------------- (Sort decreasing by price)");
                Console.WriteLine("ID    |   Title    |    Description    |   Author    |    Price    |    Type");
                foreach (var book in bookProvider.OderByPriceDown())
                {
                    Console.WriteLine($"{book.Id}       \"{book.Title}\" {book.Description}       {book.Author}      {book.Price}$   {book.Type}");
                }

                Console.WriteLine("\n(q)  Back");
                Console.ReadKey();
            }
            else if (choiceInMethod.Key == ConsoleKey.D5)
            {
                Console.Clear();

                Console.WriteLine("-----------------BOOKS LIST-----------------");
                Console.WriteLine("ID    |   Title    |    Description");
                foreach (var book in list)
                {
                    Console.WriteLine($"{book.Id}       \"{book.Title}\"    {book.Description}");
                }

                Console.WriteLine("\n(q)  Back");
                Console.ReadKey();
            }
            else if (choiceInMethod.Key == ConsoleKey.Q) break;
        }
    }

    public void AddBook(IWriteRepository<Book> repository)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("----NEW BOOK----");
            Console.WriteLine("Title: ");
            var title = Console.ReadLine();
            Console.WriteLine("Description: ");
            var description = Console.ReadLine();
            Console.WriteLine("Author: ");
            var author = Console.ReadLine();
            Console.WriteLine("type: (Horror, Action, Mindset, Thriller)");
            var type = Console.ReadLine();
            Console.WriteLine("Price: ($$,$$)");

            decimal correctPrice;
            if (!decimal.TryParse(Console.ReadLine(), out correctPrice))
            {
                Console.WriteLine("Incorrect value");
            }
            else
            {
                var newBook = new Book { Title = title, Description = description, Author = author, Price = correctPrice, Type = type };

                repository.Add(newBook);
                _itemInFile.AddItemToFile(newBook);
                repository.Save();
            }

            Back();
            var input = Console.ReadKey();
            if (input.Key == ConsoleKey.D2) break;
        }
    }

    public void RemoveBook(IRepository<Book> repository)
    {
        while (true)
        {
            Console.Clear();
            ShowBooksList(repository);

            Console.WriteLine("\n");
            Console.Write("Enter Id to delete book: ");
            var sing = Console.ReadLine();
            int id;

            if (!int.TryParse(sing, out id))
            {
                Console.WriteLine("Incorrect value");
            }
            else
            {
                var list = repository.GetAllBooks();
                var error = true;

                foreach (var book in list)
                {
                    if (book.Id == id)
                    {
                        var bookToDelete = repository.GetById(id);
                        Console.Clear();
                        Console.WriteLine($"Remove: {book.Title} Author: {book.Author}");
                        error = false;

                        Console.WriteLine("\nAre you sure?");
                        Console.WriteLine("\n(1) Yes     (2) No");
                        var choice = Console.ReadKey();

                        if (choice.Key == ConsoleKey.D1)
                        {
                            Console.Clear();
                            repository.Remove(bookToDelete);
                            _itemInFile.RemoveItemFromFile(bookToDelete);
                            repository.Save();
                        }
                        else
                        {
                            Console.Clear();
                        }
                    }
                }

                if (error)
                {
                    Console.Clear();
                    Console.WriteLine("Unknown identifier, try again");
                }
            }

            Back();
            var input = Console.ReadKey();
            if (input.Key == ConsoleKey.D2) break;
        }
    }
}

