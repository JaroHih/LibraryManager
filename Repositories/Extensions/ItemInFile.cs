using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManager.Entities;

namespace LibraryManager.Repositories.Extensions;

public class ItemInFile : IItemInFile
{
    public const string audit = "Resources\\Files\\AuditFile.csv";
    public const string bookList = "Resources\\Files\\BooksList.csv";

    public void AddItemToFile(Book item)
    {
        using var auditFile = File.AppendText(audit);
        using var bookListFile = File.AppendText(bookList);
        var time = DateTime.UtcNow;
        auditFile.WriteLine($"{time}-'{item.Title}'-Added");

        bookListFile.WriteLine($"{item.Id}-{item.Title}-{item.Description}-{item.Author}-{item.Price}-{item.Type}");
    }

    public void ItemAddToFileWithoutAudit(Book item)
    {
        using var bookListFile = File.AppendText(bookList);
        bookListFile.WriteLine($"{item.Id}-{item.Title}-{item.Description}-{item.Author}-{item.Price}-{item.Type}");
    }

    public void RemoveItemFromFile(Book item)
    {
        using (var auditFile = File.AppendText(audit))
        {
            var time = DateTime.UtcNow;
            auditFile.WriteLine($"{time}-'{item.Title}'-Deleted");
        }

        string book = $"{item.Id}-{item.Title}-{item.Description}-{item.Author}-{item.Price}-{item.Type}";
        File.WriteAllLines(bookList,
            File.ReadAllLines(bookList).Where(line => line != book));
    }
    public void CheckTheFiles(IRepository<Book> repository)
    {
        var list = File.ReadAllLines(bookList);
        if (list.Length > 0)
        {
            File.WriteAllText(bookList, String.Empty);
            foreach (var line in list)
            {
                var word = line.Split('-');
                var item = new Book { Title = word[1], Description = word[2], Author = word[3], Price = decimal.Parse(word[4]), Type = word[5] };
                repository.Add(item);
                ItemAddToFileWithoutAudit(item);
            }
            repository.Save();
        }
    }

    public void GetAllFromFile()
    {
        var list = File.ReadAllLines(bookList);
        if (!(list.Length <= 0))
        {

            foreach (var book in list)
            {
                Console.WriteLine(book);
            }
        }
        else
        {
            Console.WriteLine("This list is empty!");
        }
    }


}

