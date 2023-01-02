using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using LibraryManager.Entities;

namespace LibraryManager.Repositories.Extensions;

public class PersonInFile<T> where T : class, IEntity
{
    public const string audit = "audit.txt";
    public const string personList = "PersonList.txt";

    public static int FileLinesLength()
    {
        var lines = File.ReadAllLines(personList);
        return lines.Length;
    }

    public static void AddPersonToFile(T item)
    {
        using var auditFile = File.AppendText(audit);
        using var personListFile = File.AppendText(personList);
        var time = DateTime.UtcNow;
        auditFile.WriteLine($"{time}-{item.FirstName}-{item.LastName}-Added");

        personListFile.WriteLine($"{item.Id}-{item.FirstName}-{item.LastName}-{item.Salary}-{item.Status}");
    }

    public void RemovePersonFromFile(T item)
    {
        using (var auditFile = File.AppendText(audit))
        {
            var time = DateTime.UtcNow;
            auditFile.WriteLine($"{time}-{item.FirstName} {item.LastName}-Deleted");
        }

        string person = $"{item.Id}-{item.FirstName}-{item.LastName}-{item.Salary}-{item.Status}";
        File.WriteAllLines(personList,
            File.ReadAllLines(personList).Where(line => line != person));
    }

    public void CheckTheFiles(IRepository<Employee> repository)
    {
        var list = File.ReadAllLines(personList);
        if (list.Length > 0)
        {
            File.WriteAllText(personList, String.Empty);
            foreach (var line in list)
            {
                var word = line.Split('-');
                if(word[4]=="employee") repository.Add(new Employee { FirstName = word[1], LastName = word[2], Salary = int.Parse(word[3]), Status = word[4] });
                else if(word[4] == "manager") repository.Add(new Manager { FirstName = word[1], LastName = word[2], Salary = int.Parse(word[3]), Status = word[4]});
            }
            repository.Save();
        }
    }

    public void GetAllFromFile()
    {
        var list = File.ReadAllLines(personList);
        if (!(list.Length <= 0))
        {
            Console.WriteLine("-----------------PERSONS LIST-----------------");
            Console.WriteLine("ID    |   FullName    |    Books    |   Status");
            foreach (var person in list)
            {
                Console.WriteLine(person);
            }
        }
        else
        {
            Console.WriteLine("This list is empty!");
        }
    }
}

