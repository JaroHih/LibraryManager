using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManager._1_DataAccess.Data.Entities.Extensions;
using LibraryManager.Entities;
using LibraryManager.Repositories;

namespace LibraryManager._2_ApplicationServices.PersonProvider
{
    public class PersonProvider : IPersonProvider
    {
        private readonly IRepository<Employee> _employeesRepository;
        private readonly IRepository<PersonComments> _personComment;
        public PersonProvider(IRepository<Employee> employeesRepository,
            IRepository<PersonComments> personComment)
        {
            _employeesRepository = employeesRepository;
            _personComment = personComment;
        }

        public void ShowPersonOptions()
        {
            Console.WriteLine("\n------------MENU--------------");
            Console.WriteLine("(1) Set grade");
            Console.WriteLine("(2) Add comments");
            Console.WriteLine("(3) Show employee grades");
            Console.WriteLine("(4) Show employee comments");
            Console.WriteLine("(5) Sort alphabetically by names");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("(6) Change Person data");
        }

        public void ShowPersonOptionMenu()
        {
            Console.WriteLine("\n------------EDIT--------------");
            Console.WriteLine("(1) Change Fist Name");
            Console.WriteLine("(2) Change Last Name");
            Console.WriteLine("(3) Change Salary");
            Console.WriteLine("(4) Change Status");
        }
        public void ShowPersonList()
        {
            var list = _employeesRepository.GetAllPerson();

            Console.WriteLine("-----------------PERSONS LIST-----------------");
            Console.WriteLine("ID    |     FullName     |    Salary    |   Status");
            foreach (var person in list)
            {
                Console.WriteLine($"{person.Id}       {person.FirstName} {person.LastName}       {person.Salary}$      {person.Status}");
            }
        }

        public void AddGradeToEmployee()
        {
            Console.WriteLine("\n");
            Console.Write("Enter personID to set grade: ");
            var sing = Console.ReadLine();
            int id;

            if (!int.TryParse(sing, out id))
            {
                Console.WriteLine("Incorrect value");
                Console.WriteLine("(q) Back");
                Console.ReadKey();
            }
            else
            {
                var personToGread = _employeesRepository.GetById(id);

                if(personToGread != null)
                {
                    double correctGreade;

                    Console.Write("Enter grade (0 - 10): ");
                    var greade = Console.ReadLine();

                    if (!double.TryParse(greade, out correctGreade))
                    {
                        Console.WriteLine("Incorrect value");
                    }
                    else
                    {
                        if (correctGreade >= 0 && correctGreade <= 10)
                        {
                            personToGread.Grade = correctGreade;
                            _employeesRepository.Save();
                        }
                        else
                        {
                            Console.WriteLine("Grade is out of range!");
                            Console.WriteLine("(q) Back");
                            Console.ReadKey();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Id is out of range!");
                    Console.WriteLine("(q) Back");
                    Console.ReadKey();
                }
            }
        }

        public void AddCommentToEmployee()
        {
            Console.WriteLine("\n");
            Console.Write("Enter personID to add comments: ");
            var sing = Console.ReadLine();
            int id;

            if (!int.TryParse(sing, out id))
            {
                Console.WriteLine("Incorrect value");
            }
            else
            {
                var personToComments = _employeesRepository.GetById(id);

                Console.WriteLine("Enter comments: ");
                var comments = Console.ReadLine();

                Console.WriteLine("\nAre you sure?");
                Console.WriteLine("\n(1) Yes     (2) No");
                var choice = Console.ReadKey();

                if (choice.Key == ConsoleKey.D1)
                {
                    var comment = new PersonComments()
                    {
                        Comments = comments,
                        PersonId = personToComments.Id
                    };
                    _personComment.Add(comment);
                    _personComment.Save();
                }
            }
        }

        public void ShowGrades()
        {
            Console.Clear();

            var list = _employeesRepository.GetAllPerson();

            Console.WriteLine("-----------------PERSONS LIST-----------------");
            Console.WriteLine("ID    |     FullName     |     Greade");
            foreach (var person in list)
            {
                Console.WriteLine($"{person.Id} {person.FirstName} {person.LastName}    {person.Grade}/10");
            }

            Console.WriteLine("\n(q) Back");
            Console.ReadKey();
        }

        public void ShowComments()
        {
            Console.Clear();

            var personList = _employeesRepository.GetAllPerson();
            var commentList = _personComment.GetAllComments();

            var CommentInPerson = personList.Join(
                commentList,
                x => x.Id,
                x => x.PersonId,
                (person, comment) =>
                new
                {
                    FullName = person.FirstName + " " + person.LastName,
                    comment.Comments
                })
                .GroupBy(x => x.FullName)
                .Select(person =>
            new
            {
                FullName = person.Key,
                Comment = person.ToList()
            });

            Console.WriteLine("============PersonCommnets============");
            foreach (var item in CommentInPerson)
            {
                Console.WriteLine($"\nPerson: {item.FullName}");
                Console.WriteLine($"==========");
                foreach (var comment in item.Comment)
                {
                    Console.WriteLine($"\t\"{comment.Comments}\"");
                }
            }

            Console.WriteLine("\n(q) Back");
            Console.ReadKey();
        }

        public void ShowPersonListOrderByName()
        {
            Console.Clear();

            var list = _employeesRepository.GetAllPerson()
                .OrderBy(person => person.LastName)
                .ThenBy(person => person.FirstName);

            Console.WriteLine("-----------------PERSONS LIST-----------------");
            Console.WriteLine("ID    |     FullName     |    Salary    |   Status");
            foreach (var person in list)
            {
                Console.WriteLine($"{person.Id}       {person.FirstName} {person.LastName}       {person.Salary}$      {person.Status}");
            }

            Console.WriteLine("\n(q) Back");
            Console.ReadKey();
        }
    }
}
