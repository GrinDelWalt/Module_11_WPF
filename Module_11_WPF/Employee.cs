using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_11_WPF
{
    public abstract class Employee
    {
        public Employee()
        {

        }
        public Employee(string name, string surname, uint age, uint id, uint idDepartment)
        {
            _name = name;
            _surnamel = surname;
            _age = age;
            _id = id;
            _idDepartment = idDepartment;
        }
        public string Name { get { return _name; } set { _name = value; } }
        public string Surname { get { return _surnamel; } set { _surnamel = value; } }
        public string Post { get { return _post; } set { _post = value; } }
        public uint IdDepartment { get { return _idDepartment; } set { _idDepartment = value; } }
        public uint Age { get { return _age; } set { _age = value; } }
        public uint Salary { get { return _salary; } set { _salary = value; } }
        public uint Id { get { return _id; } }
        
        protected uint _id;
        protected uint _idDepartment;
        protected string _name;
        protected string _surnamel;
        protected string _post;
        protected uint _age;
        protected uint _salary;
    }
}
