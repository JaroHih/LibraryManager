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
    public class MenuForBooks : IMenuForBooks
    {
        public void ShowBookOptions()
        {
            Console.WriteLine("\n------------MENU--------------");
            Console.WriteLine("(1) Select only minimum price");
            Console.WriteLine("(2) ALl accessible types");
            Console.WriteLine("(3) Sort growing by price");
            Console.WriteLine("(4) Sort decreasing by price");
            Console.WriteLine("(5) Show all description");
            Console.WriteLine("(6) Group by author");

            Console.WriteLine("-------------------------------");
            Console.WriteLine("(7) Change book data");
        }

        public void ShowBooksList(IReadRepository<Book> repository)
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

        public void ShowMininalPriceForBook(IRepository<Book> repository, IBookProvider bookProvider)
        {
            Console.Clear();

            ShowBooksList(repository);

            Console.WriteLine("\n------------MENU--------------");
            Console.WriteLine($"The lowest price for a book is {bookProvider.GetMinimumPriceOfAllBooks()}$");
            Console.WriteLine("\n(q)  Back");
            Console.ReadKey();
        }

        public void ShowAllTypes(IRepository<Book> repository, IBookProvider bookProvider)
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

        public void SortGrowingByPrice(IReadRepository<Book> repository, IBookProvider bookProvider)
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

        public void SortDecreasingByPrice(IReadRepository<Book> repository, IBookProvider bookProvider)
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

        public void ShowAllDescription(IReadRepository<Book> repository)
        {
            Console.Clear();

            var list = repository.GetAllBooks();

            Console.WriteLine("-----------------BOOKS LIST-----------------");
            Console.WriteLine("ID    |   Title    |    Description");
            foreach (var book in list)
            {
                Console.WriteLine($"{book.Id}       \"{book.Title}\"    {book.Description}");
            }

            Console.WriteLine("\n(q)  Back");
            Console.ReadKey();
        }

        public void GroupByAuthor(IBookProvider bookProvider)
        {
            Console.Clear();

            Console.WriteLine("-----------------AUTHOR LIST-----------------");
            bookProvider.GrupByAuthor();

            Console.WriteLine("\n(q)  Back");
            Console.ReadKey();
        }
    }
}
