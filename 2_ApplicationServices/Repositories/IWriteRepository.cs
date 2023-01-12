using LibraryManager._1_DataAccess.Data.Entities.Extensions;
using LibraryManager.Entities;

namespace LibraryManager.Repositories
{
    public interface IWriteRepository<in T>
    {
        void Add(Employee item);
        void Add(Book item);
        void Add(PersonComments item);
        void Remove(Employee item);
        void Remove(Book item);
        void Save();
    }
}
