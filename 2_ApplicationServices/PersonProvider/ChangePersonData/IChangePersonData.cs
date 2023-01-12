using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManager.Entities;

namespace LibraryManager._2_ApplicationServices.PersonProvider.ChangePersonData
{
    public interface IChangePersonData
    {
        void ChangeFirstName(Employee employeeToChange);
        void ChangeLastName(Employee employeeToChange);
        void ChangeSalary(Employee employeeToChange);
        void ChangeStatus(Employee employeeToChange);

    }
}
