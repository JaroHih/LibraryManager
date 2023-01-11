using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManager.Entities;
using LibraryManager.Repositories;

namespace LibraryManager.BookProvider
{
    public interface IBookProvider
    {
        List<string> GetUniqueBookType();
        decimal GetMinimumPriceOfAllBooks();
        List<Book> OderByPriceDown();
        List<Book> OderByPriceUp();
        void GrupByAuthor();
    }
}
