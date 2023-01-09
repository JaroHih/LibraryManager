using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManager.BookProvider;
using LibraryManager.Entities;
using LibraryManager.Repositories;

namespace LibraryManager
{
    public interface IUserCommunication
    {
        void ShowMenu();
        void Back();
        void ShowAllPerson(IReadRepository<Employee> repository);
        void AddEmployee(IWriteRepository<Employee> sqlRepositoryOfPersons);
        void AddManager(IWriteRepository<Manager> sqlRepositoryOfPersons);
        void RemovePerson(IRepository<Employee> repository);
        void ShowAllBooks(IReadRepository<Book> repository, IBookProvider bookProvider);
        void AddBook(IWriteRepository<Book> repository);
        void RemoveBook(IRepository<Book> repository);
    }
}
