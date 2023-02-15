using LibraryManager._1_DataAccess.Data.Entities.Extensions;
using LibraryManager._2_ApplicationServices.PersonProvider;
using LibraryManager._2_ApplicationServices.PersonProvider.ChangePersonData;
using LibraryManager.BookProvider;
using LibraryManager.BookProvider.ChangeBookData;
using LibraryManager.Entities;
using LibraryManager.Repositories;
using LibraryManager.Repositories.ItemInFile;
using LibraryManager.Repositories.PeronInFile;
using LibraryManager.UserComunication.MenuForBooks;

namespace LibraryManager.UserComunication;

public class UserCommunication : IUserCommunication
{
    private readonly IPersonInFile _personInFile;
    private readonly IItemInFile _itemInFile;
    private readonly IMenuForBooks _menuForBooks;
    private readonly IChangeBookData _changeBookData;
    private readonly IPersonProvider _personProvider;
    private readonly IChangePersonData _changePersonData;

    public UserCommunication(IPersonInFile personInFile,
        IItemInFile itemInFile,
        IMenuForBooks menuForBooks,
        IChangeBookData changeBookData,
        IPersonProvider personProvider,
        IChangePersonData changePersonData)
    {
        _personInFile = personInFile;
        _itemInFile = itemInFile;
        _menuForBooks = menuForBooks;
        _changeBookData = changeBookData;
        _personProvider = personProvider;
        _changePersonData = changePersonData;
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

    public void ChangePersonData(IRepository<Employee> repository)
    {
        while (true)
        {
            Console.Clear();

            _personProvider.ShowPersonList();

            Console.Write("\nEnter id to change Employee: (or q to exit) ");

            var choice = Console.ReadLine();
            if (choice.ToLower() == "q") break;

            int employeekId;
            int.TryParse(choice, out employeekId);
            Console.Clear();

            var employeeToChange = repository.GetById(employeekId);
            if (employeeToChange != null)
            {
                Console.WriteLine($"Full Name: {employeeToChange.FirstName} {employeeToChange.LastName}");
                Console.WriteLine($"Salary: {employeeToChange.Salary} Status: {employeeToChange.Status}");

                _personProvider.ShowPersonOptionMenu();

                var end = Console.ReadKey();
                Console.Clear();

                if (end.Key == ConsoleKey.D1)
                {
                    _changePersonData.ChangeFirstName(employeeToChange);
                }
                else if (end.Key == ConsoleKey.D2)
                {
                    _changePersonData.ChangeLastName(employeeToChange);
                }
                else if (end.Key == ConsoleKey.D3)
                {
                    _changePersonData.ChangeSalary(employeeToChange);
                }
                else if (end.Key == ConsoleKey.D4)
                {
                    _changePersonData.ChangeStatus(employeeToChange);
                }
                else if (end.Key == ConsoleKey.Q) break;
            }
            else
            {
                Console.WriteLine("Incorrect value");

                Back();
                var end = Console.ReadKey();

                if (end.Key == ConsoleKey.D2) break;
            }
        }
    }

    public void ShowAllPerson(IRepository<Employee> repository, IRepository<PersonComments> personComment)
    {
        while (true)
        {
            Console.Clear();

            _personProvider.ShowPersonList();

            _personProvider.ShowPersonOptions();

            Console.WriteLine("\n(q)  Back");

            var input = Console.ReadKey();

            if (input.Key == ConsoleKey.D1)
            {
                _personProvider.AddGradeToEmployee();
            }
            else if (input.Key == ConsoleKey.D2)
            {
                _personProvider.AddCommentToEmployee();
            }
            else if (input.Key == ConsoleKey.D3)
            {
                _personProvider.ShowGrades();
            }
            else if (input.Key == ConsoleKey.D4)
            {
                _personProvider.ShowComments();
                Console.WriteLine("\n(q) Back");
                Console.ReadKey();
            }
            else if (input.Key == ConsoleKey.D5)
            {
                _personProvider.ShowPersonListOrderByName();
            }
            else if (input.Key == ConsoleKey.D6)
            {
                ChangePersonData(repository);
            }
            else if(input.Key == ConsoleKey.D7)
            {
                _personProvider.RemovePersonComment();
            }
            else if (input.Key == ConsoleKey.Q) break;
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

            _personProvider.ShowPersonList();

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

    void ChangeBookData(IRepository<Book> repository, IMenuForBooks menuForBooks, IChangeBookData changeBookData)
    {
        while (true)
        {
            Console.Clear();

            menuForBooks.ShowBooksList(repository);

            Console.Write("\nEnter id to change book: (or q to exit) ");

            var choice = Console.ReadLine();
            if (choice.ToLower() == "q") break;

            int bookId;
            int.TryParse(choice, out bookId);
            Console.Clear();

            var bookToChange = repository.GetById(bookId);
            if (bookToChange != null)
            {
                Console.WriteLine($"Author: {bookToChange.Author} Title: {bookToChange.Title}");
                Console.WriteLine($"Price: {bookToChange.Price} Type: {bookToChange.Type}");

                Console.WriteLine("-----------EDIT-----------");
                Console.WriteLine("(1) Change Title");
                Console.WriteLine("(2) Change Description");
                Console.WriteLine("(3) Change Author");
                Console.WriteLine("(4) Change Price");
                Console.WriteLine("(5) Change Type");
                Console.WriteLine("\n(q)  Back");

                var end = Console.ReadKey();
                Console.Clear();

                if (end.Key == ConsoleKey.D1)
                {
                    changeBookData.ChangeTitle(bookToChange, repository);
                }
                else if (end.Key == ConsoleKey.D2)
                {
                    changeBookData.ChangeDescription(bookToChange, repository);
                }
                else if (end.Key == ConsoleKey.D3)
                {
                    changeBookData.ChangeAuthor(bookToChange, repository);
                }
                else if (end.Key == ConsoleKey.D4)
                {
                    changeBookData.ChangePrice(bookToChange, repository);
                }
                else if (end.Key == ConsoleKey.D5)
                {
                    changeBookData.ChangeType(bookToChange, repository);
                }
                else if (end.Key == ConsoleKey.Q) break;
            }
            else
            {
                Console.WriteLine("Incorrect value");

                Back();
                var end = Console.ReadKey();

                if (end.Key == ConsoleKey.D2) break;
            }
        }
    }


    public void ShowAllBooks(IRepository<Book> repository, IBookProvider bookProvider, IMenuForBooks menuForBooks, IChangeBookData changeBookData)
    {
        while (true)
        {
            Console.Clear();

            var list = repository.GetAllBooks();

            menuForBooks.ShowBooksList(repository);
            menuForBooks.ShowBookOptions();

            Console.WriteLine("\n(q)  Back");
            var choiceInMethod = Console.ReadKey();

            if (choiceInMethod.Key == ConsoleKey.D1)
            {
                menuForBooks.ShowMininalPriceForBook(repository, bookProvider);
            }
            else if (choiceInMethod.Key == ConsoleKey.D2)
            {
                menuForBooks.ShowAllTypes(repository, bookProvider);
            }
            else if (choiceInMethod.Key == ConsoleKey.D3)
            {
                menuForBooks.SortGrowingByPrice(repository, bookProvider);
            }
            else if (choiceInMethod.Key == ConsoleKey.D4)
            {
                menuForBooks.SortDecreasingByPrice(repository, bookProvider);
            }
            else if (choiceInMethod.Key == ConsoleKey.D5)
            {
                menuForBooks.ShowAllDescription(repository);
            }
            else if (choiceInMethod.Key == ConsoleKey.D6)
            {
                menuForBooks.GroupByAuthor(bookProvider);
            }
            else if (choiceInMethod.Key == ConsoleKey.D7)
            {
                ChangeBookData(repository, menuForBooks, changeBookData);
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

    public void RemoveBook(IRepository<Book> repository, IMenuForBooks menuForBooks)
    {
        while (true)
        {
            Console.Clear();
            menuForBooks.ShowBooksList(repository);

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

