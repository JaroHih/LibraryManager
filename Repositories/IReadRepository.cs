using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManager.Entities;

namespace LibraryManager.Repositories
{
    public interface IReadRepository<out T> where T : class, IEntity
    {
        T? GetById(int id);
        IEnumerable<T> GetAllPerson();
        IEnumerable<T> GetAllBooks();
    }
}
