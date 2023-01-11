using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManager.Entities;
using LibraryManager.Repositories;

namespace LibraryManager.BookProvider.ChangeBookData
{
    public interface IChangeBookData
    {
        void ChangeTitle(Book bookToChange, IRepository<Book> repository);
        void ChangeDescription(Book bookToChange, IRepository<Book> repository);
        void ChangeAuthor(Book bookToChange, IRepository<Book> repository);
        void ChangePrice(Book bookToChange, IRepository<Book> repository);
        void ChangeType(Book bookToChange, IRepository<Book> repository);
    }
}
