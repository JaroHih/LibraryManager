﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManager.Entities;

namespace LibraryManager.Repositories.Extensions
{
    public static class RepositoryExtensions
    {
    public static void AddBatch<T>(this IRepository<T> repository, T[] items)
        where T : class, IEntity, new()
        {
            foreach (var item in items)
            {
                repository.Add(item);
            }

            repository.Save();
        }
    }
}
