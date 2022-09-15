using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_11_WPF.Models
{
    public class Intern : Employee
    {
        public Intern(string name, string surname, uint idDepartment, uint age, uint salary, uint id) : base(name, surname, age, id, idDepartment)
        {
            _salary = salary;
            _post = "Стажер";
        }
        public Intern()
        {

        }
        public override uint Salary { get { return _salary; } set { _salary = value; } }
    }
}
