using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManager.Entities;
using LibraryManager.Repositories;

namespace LibraryManager._2_ApplicationServices.PersonProvider.ChangePersonData
{
    public class ChangePersonData : IChangePersonData
    {
        private readonly IRepository<Employee> _employeeRepository;
        public ChangePersonData(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void ChangeFirstName(Employee employeeToChange)
        {
            Console.WriteLine("Enter new First Name: ");
            var newFirstName = Console.ReadLine();

            Console.WriteLine("\nAre you sure?");
            Console.WriteLine("(1) Yes      (2) No");

            var sure = Console.ReadKey();
            if (sure.Key == ConsoleKey.D1)
            {

                employeeToChange.FirstName = newFirstName;
                _employeeRepository.Save();
            }
        }

        public void ChangeLastName(Employee employeeToChange)
        {
            Console.WriteLine("Enter new Last Name: ");
            var newLastName = Console.ReadLine();

            Console.WriteLine("\nAre you sure?");
            Console.WriteLine("(1) Yes      (2) No");

            var sure = Console.ReadKey();
            if (sure.Key == ConsoleKey.D1)
            {

                employeeToChange.LastName = newLastName;
                _employeeRepository.Save();
            }
        }

        public void ChangeSalary(Employee employeeToChange)
        {
            Console.WriteLine("Enter new Salary: ");
            var newSalary = Console.ReadLine();

            double correctSalary;

            if(!double.TryParse(newSalary, out correctSalary))
            {
                Console.WriteLine("Invalid value!");
            }
            else
            {
                Console.WriteLine("\nAre you sure?");
                Console.WriteLine("(1) Yes      (2) No");

                var sure = Console.ReadKey();
                if (sure.Key == ConsoleKey.D1)
                {

                    employeeToChange.Salary = correctSalary;
                    _employeeRepository.Save();
                }
            }
        }

        public void ChangeStatus(Employee employeeToChange)
        {
            Console.WriteLine("Enter new Status: (employee / manager)");
            var newStatus = Console.ReadLine();


            if (newStatus!="employee" || newStatus!="manager")
            {
                Console.WriteLine("Invalid status!");
            }
            else
            {
                Console.WriteLine("\nAre you sure?");
                Console.WriteLine("(1) Yes      (2) No");

                var sure = Console.ReadKey();
                if (sure.Key == ConsoleKey.D1)
                {

                    employeeToChange.Status = newStatus;
                    _employeeRepository.Save();
                }
            }
        }
    }
}
