using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManager.Entities;

namespace LibraryManager.Repositories.Extensions
{
    public interface IPersonInFile
    {
        void AddPersonToFile(Employee item);
        void RemovePersonFromFile(Employee item);
        void CheckTheFiles(IRepository<Employee> repository);
    }
}
