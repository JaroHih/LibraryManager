using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using LibraryManager.Entities;

namespace LibraryManager.Repositories.Extensions;

public class PersonInFile :  IPersonInFile 
{
    public const string personPath = "Resources\\Files\\EmployeesList.csv";
    public const string auditPath = "Resources\\Files\\AuditFile.csv";

    public void AddPersonToFile(Employee item)
    {
        using var auditFile = File.AppendText(auditPath);
        using var personListFile = File.AppendText(personPath);
        var time = DateTime.UtcNow;
        auditFile.WriteLine($"{time}-{item.FirstName}-{item.LastName}-Added");

        personListFile.WriteLine($"{item.Id}-{item.FirstName}-{item.LastName}-{item.Salary}-{item.Status}");

        ////////////CSV//////////////

        //var emplouyeesFile = new StreamWriter(personPath, true);
        //emplouyeesFile.WriteLine($"{item.Id}-{item.FirstName}-{item.LastName}-{item.Salary}-{item.Status}");
        //emplouyeesFile.Close();

        //var auditFilecsv = new StreamWriter(auditPath, true);
        //auditFilecsv.WriteLine($"{time}-{item.FirstName}-{item.LastName}-Added");
        //auditFilecsv.Close();
    }

    public void AddPersonToFileWithoutAudit(Employee item)
    {
        using var personListFile = File.AppendText(personPath);
        personListFile.WriteLine($"{item.Id}-{item.FirstName}-{item.LastName}-{item.Salary}-{item.Status}");

        ////////////CSV//////////////

        //var emplouyeesFile = new StreamWriter(personPath, true);
        //emplouyeesFile.WriteLine($"{item.Id}-{item.FirstName}-{item.LastName}-{item.Salary}-{item.Status}");
        //emplouyeesFile.Close();
    }

    public void RemovePersonFromFile(Employee item)
    {
        var time = DateTime.UtcNow;
        using (var auditFile = File.AppendText(auditPath))
        {
            auditFile.WriteLine($"{time}-{item.FirstName} {item.LastName}-Deleted");
        }

        string person = $"{item.Id}-{item.FirstName}-{item.LastName}-{item.Salary}-{item.Status}";
        File.WriteAllLines(personPath,
        File.ReadAllLines(personPath).Where(line => line != person));

        ////////////CSV//////////////

        //var auditFilecsv = new StreamWriter(auditPath, true);
        //auditFilecsv.WriteLine($"{time}-{item.FirstName} {item.LastName}-Deleted");
        //auditFilecsv.Close();

        //File.WriteAllLines(personPath,
        //File.ReadAllLines(personPath).Where(line => line != person));
    }

    public void CheckTheFiles(IRepository<Employee> repository)
    {
        var list = File.ReadAllLines(personPath)
            .Where(x => x.Length > 1);
        
        if (list.Count() > 0)
        {

            File.WriteAllText(personPath, String.Empty);

            foreach (var line in list)
            {
                var word = line.Split('-');
                if (word[4] == "employee")
                {
                    var item = new Employee { FirstName = word[1], LastName = word[2], Salary = int.Parse(word[3]), Status = word[4] };
                    repository.Add(item);
                    AddPersonToFileWithoutAudit(item);
                }
                else if (word[4] == "manager")
                {
                    var item = new Manager { FirstName = word[1], LastName = word[2], Salary = int.Parse(word[3]), Status = word[4] };
                    repository.Add(item);
                    AddPersonToFileWithoutAudit(item);
                }

            }

            repository.Save();

        }
    }
}

