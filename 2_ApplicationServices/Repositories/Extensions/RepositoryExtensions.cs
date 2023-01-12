using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManager.Entities;

namespace LibraryManager.Repositories.Extensions
{
    public static class RepositoryExtensions
    {
        public static void AddBatchForEmployee(this IRepository<Employee> repository, Employee[] items)
        {
            foreach (var item in items)
            {
                repository.Add(item);
            }

            repository.Save();
        }
    }
}
