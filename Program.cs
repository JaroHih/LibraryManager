using System.Reflection.PortableExecutable;
using LibraryManager.Data;
using LibraryManager.Entities;
using LibraryManager.Repositories;
using LibraryManager.Repositories.Extensions;

var repository = new SqlRepository<Employee>(new LibraryManagerDbContext());
repository.ItemAdded += EventOnItemAdded;
repository.ItemRemoved += EventOnItemRemoved;

static void EventOnItemAdded(object? sender, Employee item)
{
    Console.WriteLine($"Employee added => {item.FirstName} from {sender.GetType().Name}");
}

static void EventOnItemRemoved(object? sender, Employee item)
{
    Console.WriteLine($"Employee removed => {item.FirstName} from {sender.GetType().Name}");
}

repository.CheckTheFiles(repository);

while (true)
{
    ShowMenu();
    var choice = Console.ReadKey();

    if (choice.Key == ConsoleKey.D1)
    {
        ShowAllPerson(repository);
    }
    else if (choice.Key == ConsoleKey.D2)
    {
        AddEmployee(repository);
    }
    else if (choice.Key == ConsoleKey.D3)
    {
        AddManager(repository);
    }
    else if (choice.Key == ConsoleKey.D4)
    {
        RemovePerson(repository);
    }
    else if (choice.Key == ConsoleKey.Q) break;
}

void ShowMenu()
{
    Console.Clear();
    Console.WriteLine("|*----------------MENU----------------*|\n");
    Console.WriteLine("(1)  Show all readers and employees");
    Console.WriteLine("(2)  Add new employee");
    Console.WriteLine("(3)  Add new manager");
    Console.WriteLine("(4)  Delete the person");
    Console.WriteLine("(q)  Exite\n");
}

static void Back()
{
    Console.WriteLine("\n(1)  Continue");
    Console.WriteLine("(2)  Back");
}

static void ShowAllPerson(IReadRepository<IEntity> repository)
{
    Console.Clear();

    var list = repository.GetAll();

    Console.WriteLine("-----------------PERSONS LIST-----------------");
    Console.WriteLine("ID    |     FullName     |    Salary    |   Status");
    foreach (var person in list)
    {
        Console.WriteLine($"{person.Id}       {person.FirstName} {person.LastName}       {person.Salary}      {person.Status}");
    }

    Console.WriteLine("\n(q)  Back");
    var choiceInMethod = Console.ReadKey();
}

static void AddEmployee(IWriteRepository<Employee> sqlRepositoryOfPersons)
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("----NEW EMPLOYEE----");
        Console.WriteLine("First Name: ");
        var firstName = Console.ReadLine();
        Console.WriteLine("Second Name: ");
        var secondName = Console.ReadLine();
        Console.WriteLine("Salary: ");
        var salary = double.Parse(Console.ReadLine());

        sqlRepositoryOfPersons.Add(new Employee { FirstName = firstName, LastName = secondName, Salary = salary, Status = "employee" });
        sqlRepositoryOfPersons.Save();

        Back();
        var input = Console.ReadKey();
        if (input.Key == ConsoleKey.D2) break;
    }
}

static void AddManager(IWriteRepository<Manager> sqlRepositoryOfPersons)
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("----NEW MANAGER----");
        Console.WriteLine("First Name: ");
        var firstName = Console.ReadLine();
        Console.WriteLine("Second Name: ");
        var secondName = Console.ReadLine();
        Console.WriteLine("Salary: ");
        var salary = double.Parse(Console.ReadLine());

        sqlRepositoryOfPersons.Add(new Manager { FirstName = firstName, LastName = secondName, Salary = salary, Status = "manager" });
        sqlRepositoryOfPersons.Save();

        Back();
        var input = Console.ReadKey();
        if (input.Key == ConsoleKey.D2) break;
    }
}

static void RemovePerson(IRepository<Employee> repository)
{
    while (true)
    {
        Console.Clear();


        var listPerson = repository.GetAll();

        Console.WriteLine("-----------------PERSONS LIST-----------------");
        Console.WriteLine("ID    |     FullName     |    Salary    |   Status");
        foreach (var person in listPerson)
        {
            Console.WriteLine($"{person.Id}       {person.FirstName} {person.LastName}       {person.Salary}      {person.Status}");
        }

        Console.WriteLine("\n");
        Console.Write("Enter Id to delete person: ");
        var sing = Console.ReadLine();

        var id = int.Parse(sing);

        var list = repository.GetAll();
        var error = true;

        foreach (var person in list)
        {
            if (person.Id == id)
            {
                var personToDelete = repository.GetById(id);
                Console.Clear();
                Console.WriteLine($"Remove: {personToDelete.FirstName} {personToDelete.LastName}");
                error = false;

                Console.WriteLine("\nAre you sure?");
                Console.WriteLine("\n(1) Yes     (2) No");
                var choice = Console.ReadKey();

                if (choice.Key == ConsoleKey.D1)
                {
                    Console.Clear();
                    repository.Remove(personToDelete);
                    repository.Save();
                }
                else
                {
                    Console.Clear();
                }
            }
        }

        Back();
        var input = Console.ReadKey();
        if (input.Key == ConsoleKey.D2) break;
    }
}
