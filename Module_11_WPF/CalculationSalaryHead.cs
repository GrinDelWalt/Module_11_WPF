using Module_11_WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_11_WPF
{
    internal class CalculationSalaryHead
    {
        private ObservableCollection<Employee> _employees;
        public CalculationSalaryHead(ObservableCollection<Employee> employees)
        {
            _employees = employees;
        }
    
        public void EditSalary(Employee employeeEditSalary)
        {
            int index = _employees.IndexOf(employeeEditSalary);
            EditSalaryDeputyHeadDepartment(employeeEditSalary, index);
            EditSalaryHeadDepartment(employeeEditSalary, index);
            EditSalaryDeputyDirector(index);
            EditSalaryDirector(index);
        }

        public ObservableCollection<Employee> GetEmployees()
        {
            return _employees;
        }

        private void EditSalaryDeputyHeadDepartment(Employee employeeEditSalary, int index)
        {
            IEnumerable<Employee> employees = from employee in _employees
                                              where employee.IdDepartment == employeeEditSalary.IdDepartment
                                              where employee.Post != "Глава Департамента"
                                              where employee.Post != "Заместитель главы департамента"
                                              select employee;

            IEnumerable<Employee> director = SerchHeadDepartment(employeeEditSalary, "Заместитель главы департамента");
            foreach (var element in director)
            {
                element.Salary = CalculationSalary(employees);
            }
        }
        private void EditSalaryHeadDepartment(Employee employeeEditSalary, int index)
        {
            IEnumerable<Employee> employees = from employee in _employees
                                              where employee.IdDepartment == employeeEditSalary.IdDepartment
                                              where employee.Post != "Глава Департамента"
                                              select employee;
            IEnumerable<Employee> director = SerchHeadDepartment(employeeEditSalary, "Глава Департамента");
            foreach (var element in director)
            {
                element.Salary = CalculationSalary(employees);
            }
        }
        private IEnumerable<Employee> SerchHeadDepartment(Employee employeeSelectid, string post)
        {
            IEnumerable<Employee> director = from employee in _employees
                                             where employee.IdDepartment == employeeSelectid.IdDepartment
                                             where employee.Post == post
                                             select employee;
            return director;
        }
        private void EditSalaryDeputyDirector(int index)
        {
            IEnumerable<Employee> employees = from employee in _employees
                                              where employee.Post != "Директор"
                                              where employee.Post != "Заместитель директора"
                                              select employee;
            IEnumerable<Employee> director = SerchDirector("Заместитель директора");
            foreach (var element in director)
            {
                element.Salary = CalculationSalary(employees);
            }
        }


        private void EditSalaryDirector(int index)
        {
            IEnumerable<Employee> employees = from employee in _employees
                                              where employee.Post != "Директор"
                                              select employee;
            IEnumerable<Employee> director = SerchDirector("Директор");
            foreach (var element in director)
            {
                element.Salary = CalculationSalary(employees);
            }
        }
        private uint CalculationSalary(IEnumerable<Employee> employees)
        {
            uint sumSalary = 0;

            foreach (Employee item in employees)
            {
                sumSalary += item.Salary;
            }
            double sum = 0.15 * sumSalary;
            sumSalary = (uint)sum;
            return sumSalary;
        }
        private IEnumerable<Employee> SerchDirector(string post)
        {
            IEnumerable<Employee> director = from employee in _employees
                where employee.Post == post
                select employee;
            return director;
        }
    }
}
