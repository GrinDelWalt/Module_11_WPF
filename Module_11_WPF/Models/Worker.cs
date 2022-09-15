using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_11_WPF.Models
{
    public class Worker : Employee
    {
        public Worker()
        {

        }
        public Worker(string name, string surname, uint idDepartment, uint age, uint workingHours, uint hourlyPayment, uint id, string post) : base(name, surname, age, id, idDepartment)
        {
            _post = post;
            _idDepartment = idDepartment;
            _workingHours = workingHours;
            _hourlyPayment = hourlyPayment;
            GetSalary();
        }
        public new uint Salary
        {
            get
            {
                return _salary;
            }
        }
        private void GetSalary()
        {
            _salary = _workingHours * _hourlyPayment;
        }
        public uint WorkingHours { get { return _workingHours; } set { _workingHours = value; } }

        public uint HourlyPayment { get { return _hourlyPayment; } set { _hourlyPayment = value; } }

        private uint _workingHours;
        private uint _hourlyPayment;
    }
}
