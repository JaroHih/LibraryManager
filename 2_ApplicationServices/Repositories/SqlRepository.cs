using LibraryManager._1_DataAccess.Data.Entities.Extensions;
using LibraryManager.Data;
using LibraryManager.Entities;
using LibraryManager.Repositories.PeronInFile;

namespace LibraryManager.Repositories;

public class SqlRepository<T> : PersonInFile, IRepository<T>
where T : class, IEntity, new()
{

    private readonly LibraryManagerDbContext _libraryManagerDbContext;

    public event EventHandler<Employee>? EmployeeAdded;
    public event EventHandler<Book>? ItemAdded;
    public event EventHandler<Employee>? EmployeeRemoved;
    public event EventHandler<Book>? ItemRemoved;

    public SqlRepository(LibraryManagerDbContext libraryManagerDbContext)
    {
        _libraryManagerDbContext = libraryManagerDbContext;
        _libraryManagerDbContext.Database.EnsureCreated();
    }
    public void Add(Employee item)
    {
        _libraryManagerDbContext.Employees.Add(new Employee()
        {
            FirstName = item.FirstName,
            LastName = item.LastName,
            Salary = item.Salary,
            Status = item.Status,
            Grade = 5
        }); 
        EmployeeAdded.Invoke(this, item);
    }

    public void Add(PersonComments item)
    {
        _libraryManagerDbContext.PersonComments.Add(new PersonComments
        {
            Comments = item.Comments,
            PersonId = item.PersonId
        });
    }

    public void Add(Book item)
    {
        _libraryManagerDbContext.Books.Add(new Book()
        {
            Title = item.Title,
            Description = item.Description,
            Author = item.Author,
            Type = item.Type,
            Price = item.Price
        }); 
        ItemAdded.Invoke(this, item);
    }


    public IEnumerable<T> GetAllPerson()
    {
        var List = _libraryManagerDbContext.Employees.ToList();
        return (IEnumerable<T>)List;
    }

    public IEnumerable<T> GetAllBooks()
    {
        var List = _libraryManagerDbContext.Books.ToList();
        return (IEnumerable<T>)List;
    }

    public IEnumerable<T> GetAllComments()
    {
        var list = _libraryManagerDbContext.PersonComments.ToList();
        return (IEnumerable<T>)list;
    }

    public T? GetById(int id)
    {
        var item = _libraryManagerDbContext.Find<T>(id);
        if (item == null) return null;
        else return item;
    }

    public void Remove(Employee item)
    {
        _libraryManagerDbContext.Remove(item);
        EmployeeRemoved.Invoke(this, item);
    }
    public void Remove(Book item)
    {
        _libraryManagerDbContext.Remove(item);
        ItemRemoved.Invoke(this, item);
    }

    public void Remove(PersonComments item)
    {
        _libraryManagerDbContext.Remove(item);
    }

    public void Save()
    {
        _libraryManagerDbContext.SaveChanges();
    }
}

