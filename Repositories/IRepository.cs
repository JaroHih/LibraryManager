using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManager.Entities;

namespace LibraryManager.Repositories
{
    public interface IRepository<T> : IWriteRepository<T>, IReadRepository<T> 
        where T : class, IEntity, new()
    {
        public event EventHandler<Employee>? EmployeeAdded;
        public event EventHandler<Book>? ItemAdded;
        public event EventHandler<Employee>? EmployeeRemoved;
        public event EventHandler<Book>? ItemRemoved;
    }
}
