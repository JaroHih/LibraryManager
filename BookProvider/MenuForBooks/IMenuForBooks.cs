using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManager.BookProvider;
using LibraryManager.Entities;
using LibraryManager.Repositories;

namespace LibraryManager.UserComunication.MenuForBooks
{
    public interface IMenuForBooks
    {
        void ShowBookOptions();
        void ShowBooksList(IReadRepository<Book> repository);
        void ShowMininalPriceForBook(IRepository<Book> repository, IBookProvider bookProvider);
        void ShowAllTypes(IRepository<Book> repository, IBookProvider bookProvider);
        void SortGrowingByPrice(IReadRepository<Book> repository, IBookProvider bookProvider);
        void SortDecreasingByPrice(IReadRepository<Book> repository, IBookProvider bookProvider);
        void ShowAllDescription(IReadRepository<Book> repository);
        void GroupByAuthor(IBookProvider bookProvider);
    }
}
