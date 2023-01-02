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
    }
}
