using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.PortableExecutable;
using LibraryManager.Data;
using LibraryManager.Entities;
using LibraryManager.Repositories;
using LibraryManager.Repositories.Extensions;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore.Diagnostics;
using LibraryManager.BookProvider; 
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager;

public class App : IApp
{
    private readonly IRepository<Employee> _employreesRepository;
    private readonly IRepository<Book> _booksRepository;    
    private readonly IBookProvider _bookProvider;
    private readonly IUserCommunication _userCommunication;
    private readonly IItemInFile _itemInFilel;
    private readonly IPersonInFile _personInFile;


    public App(IRepository<Employee> employreesRepository,
               IRepository<Book> booksRepository,
        IBookProvider bookProvider,
        IUserCommunication userCommunication,
        IPersonInFile personInFile,
        IItemInFile itemInFile
        )
    {
        _employreesRepository = employreesRepository;
        _booksRepository = booksRepository;
        _bookProvider = bookProvider;
        _userCommunication = userCommunication;
        _personInFile = personInFile;
        _itemInFilel = itemInFile;
    }

    public void Run()
    {

        _employreesRepository.EmployeeAdded += EventOnPersonAdded;
        _employreesRepository.EmployeeRemoved += EventOnPersonRemoved;
        _booksRepository.ItemAdded += EventOnItemAdded;
        _booksRepository.ItemRemoved += EventOnItemRemoved;

        static void EventOnPersonAdded(object? sender, Employee item)
        {
            if (item.Status == "manager") Console.WriteLine($"Manager added => {item.FirstName} from {sender?.GetType().Name}");
            else Console.WriteLine($"Employee added => {item.FirstName} from {sender?.GetType().Name}");
        }

        static void EventOnPersonRemoved(object? sender, Employee item)
        {
            if (item.Status == "manager") Console.WriteLine($"Manager removed => {item.FirstName} from {sender?.GetType().Name}");
            else Console.WriteLine($"Employee removed => {item.FirstName} from {sender?.GetType().Name}");
        }

        static void EventOnItemAdded(object? sender, Book item)
        {
            Console.WriteLine($"Book added => {item.Title} from {sender?.GetType().Name}");
        }

        static void EventOnItemRemoved(object? sender, Book item)
        {
            Console.WriteLine($"Book removed => {item.Title} from {sender?.GetType().Name}");
        }

        while (true)
        {
            _userCommunication.ShowMenu();
            var choice = Console.ReadKey();

            if (choice.Key == ConsoleKey.D1)
            {
                _userCommunication.ShowAllPerson(_employreesRepository);
            }
            else if (choice.Key == ConsoleKey.D2)
            {
                _userCommunication.AddEmployee(_employreesRepository);
            }
            else if (choice.Key == ConsoleKey.D3)
            {
                _userCommunication.AddManager(_employreesRepository);
            }
            else if (choice.Key == ConsoleKey.D4)
            {
                _userCommunication.RemovePerson(_employreesRepository);
            }
            else if (choice.Key == ConsoleKey.D5)
            {
                _userCommunication.ShowAllBooks(_booksRepository, _bookProvider);
            }
            else if (choice.Key == ConsoleKey.D6)
            {
                _userCommunication.AddBook(_booksRepository);
            }
            else if ( choice.Key == ConsoleKey.D7)
            {
                _userCommunication.RemoveBook(_booksRepository);
            }
            else if (choice.Key == ConsoleKey.Q) break;
        }
    }
}

