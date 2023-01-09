using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManager.Entities;

namespace LibraryManager.Repositories
{
    public interface IWriteRepository<in T>
    {
        void Add(Employee item);
        void Add(Book item);
        void Remove(Employee item);
        void Remove(Book item);
        void Save();
    }
}
