using LibraryManager._1_DataAccess.Data.Entities.Extensions;
using LibraryManager.Entities;
using Microsoft.EntityFrameworkCore;


namespace LibraryManager.Data;

public class LibraryManagerDbContext : DbContext
{
    public LibraryManagerDbContext(DbContextOptions<LibraryManagerDbContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Manager> Managers { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<PersonComments> PersonComments { get; set; }
}

