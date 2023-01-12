using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManager.Entities;

namespace LibraryManager._1_DataAccess.Data.Entities.Extensions
{
    public class PersonComments : EntityBase
    {
        public string? Comments { get; set; }
        public int? PersonId { get; set; }
    }
}
