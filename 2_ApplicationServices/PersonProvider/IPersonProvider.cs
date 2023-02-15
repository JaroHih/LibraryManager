using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager._2_ApplicationServices.PersonProvider
{
    public interface IPersonProvider
    {
        void ShowPersonOptions();
        void ShowPersonOptionMenu();
        void ShowPersonList();
        void AddGradeToEmployee();
        void AddCommentToEmployee();
        void RemovePersonComment();
        void ShowGrades();
        void ShowComments();
        void ShowPersonListOrderByName();
    }
}
