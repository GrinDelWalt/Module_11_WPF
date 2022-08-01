using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_11_WPF
{
    public class Director : Employee
    {
        public Director()
        {

        }
        public Director(string name, string surname, uint age, uint id, uint idDepartment) : base(name, surname, age, id, idDepartment)
        {
            _name = name;
            _surnamel = surname;
            _age = age;
            _id = id;
            _post = "Директор";
            _idDepartment = idDepartment;
        }
        
        public new uint Salary
        {
            get
            {
                //SumSalaryWorkers();
                return _salary = 1300;
            }
        }
    }
}
