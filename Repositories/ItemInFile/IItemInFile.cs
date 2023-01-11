using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManager.Entities;

namespace LibraryManager.Repositories.ItemInFile
{
    public interface IItemInFile
    {
        void AddItemToFile(Book item);
        void RemoveItemFromFile(Book item);
        void CheckTheFiles(IRepository<Book> repository);
        void GetAllFromFile();
    }
}
