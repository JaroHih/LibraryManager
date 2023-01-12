using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManager._1_DataAccess.Data.Entities.Extensions;
using LibraryManager.BookProvider;
using LibraryManager.BookProvider.ChangeBookData;
using LibraryManager.Entities;
using LibraryManager.Repositories;
using LibraryManager.UserComunication.MenuForBooks;

namespace LibraryManager.UserComunication
{
    public interface IUserCommunication
    {
        void ShowMenu();
        void Back();
        void ShowAllPerson(IRepository<Employee> repository, IRepository<PersonComments> personComment);
        void AddEmployee(IWriteRepository<Employee> sqlRepositoryOfPersons);
        void AddManager(IWriteRepository<Manager> sqlRepositoryOfPersons);
        void RemovePerson(IRepository<Employee> repository);
        void ShowAllBooks(IRepository<Book> repository, IBookProvider bookProvider, IMenuForBooks menuForBooks, IChangeBookData changeBookData);
        void AddBook(IWriteRepository<Book> repository);
        void RemoveBook(IRepository<Book> repository, IMenuForBooks menuForBooks);
    }
}
