using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManager.Entities;
using LibraryManager.Repositories;

namespace LibraryManager.BookProvider.ChangeBookData
{
    public class ChangeBookData : IChangeBookData
    {

        public void ChangeTitle(Book bookToChange, IRepository<Book> repository)
        {
            Console.WriteLine("Enter new title: ");
            var newTitle = Console.ReadLine();

            Console.WriteLine("\nAre you sure?");
            Console.WriteLine("(1) Yes      (2) No");

            var sure = Console.ReadKey();
            if (sure.Key == ConsoleKey.D1)
            {

                bookToChange.Title = newTitle;
                repository.Save();
            }
        }

        public void ChangeDescription(Book bookToChange, IRepository<Book> repository)
        {
            Console.WriteLine("Enter new description: ");
            var newDescription = Console.ReadLine();

            Console.WriteLine("\nAre you sure?");
            Console.WriteLine("(1) Yes      (2) No");

            var sure = Console.ReadKey();
            if (sure.Key == ConsoleKey.D1)
            {

                bookToChange.Description = newDescription;
                repository.Save();
            }
        }

        public void ChangeAuthor(Book bookToChange, IRepository<Book> repository)
        {
            Console.WriteLine("Enter new author: ");
            var newAuthor = Console.ReadLine();

            Console.WriteLine("\nAre you sure?");
            Console.WriteLine("(1) Yes      (2) No");

            var sure = Console.ReadKey();
            if (sure.Key == ConsoleKey.D1)
            {

                bookToChange.Author = newAuthor;
                repository.Save();
            }
        }

        public void ChangePrice(Book bookToChange, IRepository<Book> repository)
        {
            Console.WriteLine("Enter new price: ");
            var newPrice = decimal.Parse(Console.ReadLine());

            Console.WriteLine("\nAre you sure?");
            Console.WriteLine("(1) Yes      (2) No");

            var sure = Console.ReadKey();
            if (sure.Key == ConsoleKey.D1)
            {

                bookToChange.Price = newPrice;
                repository.Save();
            }
        }

        public void ChangeType(Book bookToChange, IRepository<Book> repository)
        {
            Console.WriteLine("Enter new type: ");
            var newType = Console.ReadLine();

            Console.WriteLine("\nAre you sure?");
            Console.WriteLine("(1) Yes      (2) No");

            var sure = Console.ReadKey();
            if (sure.Key == ConsoleKey.D1)
            {

                bookToChange.Type = newType;
                repository.Save();
            }
        }
    }
}
