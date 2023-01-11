using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManager.Entities;
using LibraryManager.Repositories;

namespace LibraryManager.BookProvider;

public class BookProvider : IBookProvider
{
    private readonly IRepository<Book> _booksRepository;
    public BookProvider(IRepository<Book> booksRepository)
    {
        _booksRepository = booksRepository;
    }

    public decimal GetMinimumPriceOfAllBooks()
    {
        var books = _booksRepository.GetAllBooks();

        if (books.Count() > 0) return books.Select(item => item.Price).Min();
        else return 0;
    }

    public List<string> GetUniqueBookType()
    {
        var books = _booksRepository.GetAllBooks();
        var types = books.Select(book => book.Type).Distinct().ToList();
        return types;
    }

    public List<Book> OderByPriceUp()
    {
        var book = _booksRepository.GetAllBooks();
        var result = book.OrderBy(book => book.Price).ToList();
        return result;
    }
    public List<Book> OderByPriceDown()
    {
        var book = _booksRepository.GetAllBooks();
        var result = book.OrderByDescending(book => book.Price).ToList();
        return result;
    }

    public void GrupByAuthor()
    {
        var groups = _booksRepository.GetAllBooks()
            .GroupBy(book => book.Author)
            .Select(book => new
            {
                Name = book.Key,
                Books = book.ToList()
            })
            .ToList();

        foreach(var group in groups)
        {
            Console.WriteLine(group.Name);
            Console.WriteLine("=========");
            foreach(var book in group.Books)
            {
                Console.WriteLine($"\t{book.Title}: {book.Description}");
            }
            Console.WriteLine();
        }
    }


}