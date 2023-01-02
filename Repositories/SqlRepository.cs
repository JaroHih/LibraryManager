using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManager.Entities;
using LibraryManager.Repositories.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Repositories;

    public class SqlRepository<T> : PersonInFile<T>, IRepository<T> where T : class, IEntity, new()
{
    private readonly DbSet<T> _dbSet;
    private readonly DbContext _dbContext;

    public event EventHandler<T> ItemAdded;
    public event EventHandler<T> ItemRemoved;

    public SqlRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();

    }
    public void Add(T item)
    {
        _dbSet.Add(item);
        AddPersonToFile(item);
        ItemAdded.Invoke(this, item);
    }

    public IEnumerable<T> GetAll()
    {
        return _dbSet.ToList().OrderBy(item => item.Id);
    }

    public T? GetById(int id)
    {
        return _dbSet.Find(id);
    }

    public void Remove(T item)
    {
        _dbSet.Remove(item);
        RemovePersonFromFile(item);
        ItemRemoved.Invoke(this, item);
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }
}

