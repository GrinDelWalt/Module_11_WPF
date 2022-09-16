using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Module_11_WPF.Models;

namespace Module_11_WPF
{

    class EmployeeManagement
    {
        private ObservableCollection<Employee> _employees;
        private static uint _idEmployee;
        private uint _idDepartment;

        private uint _idDepartmentSelection;
        static EmployeeManagement()
        {
            _idEmployee = 0;
        }
        private static uint NextId()
        {
            return ++_idEmployee;
        }
        public EmployeeManagement()
        {
            _employees = new ObservableCollection<Employee>();
            _employees.CollectionChanged += EditEventEmployee;
        }
        public void NewDirektor(string name, string surName, uint age, uint idDepartment, string post)
        {
            _employees.Add(new Director(name, surName, age, NextId(), idDepartment, post));
        }
        public void NewWorker(string name, string surname, uint idDepartment, uint age, uint workingHours, uint hourlyPayment, string post)
        {
            _employees.Add(new Worker(name, surname, idDepartment, age, workingHours, hourlyPayment, NextId(), post));
        }
        public void NewIntern(string name, string surname, uint idDepartment, uint age, uint salary)
        {
            _employees.Add(new Intern(name, surname, idDepartment, age, salary, NextId()));
        }
        private void EditEventEmployee(object? sender, NotifyCollectionChangedEventArgs e)
        {
            object employeeObject = null;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    employeeObject = (object)e.NewItems.SyncRoot;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    employeeObject = (object)e.OldItems.SyncRoot;
                    break;
                case NotifyCollectionChangedAction.Replace:
                    employeeObject = (object)e.NewItems.SyncRoot;
                    break;
            }
            EditSalary(employeeObject);
        }

        private void EditSalary(object employeesObject)
        {
            try
            {
                CalculationSalaryHead calc = new CalculationSalaryHead(_employees);
                Employee employee = (Employee)employeesObject;
                calc.EditSalary(employee);
                _employees = calc.GetEmployees();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public int GetCountEmployee(uint idDepartment)
        {
            _idDepartmentSelection = idDepartment;
            var employee = _employees.Where(Fild);
            int count = employee.Count();
            return count;
        }
        public bool Fild(Employee employee)
        {
            return employee.IdDepartment == _idDepartmentSelection;
        }

        public void Test()
        {
            NewDirektor("Grin", "Walt", 25, 1, "Директор");
            NewDirektor("Jone", "Soer", 31, 2, "Глава Департамента");
            NewDirektor("Piter", "Parker", 21, 3, "Глава Департамента");
            NewDirektor("Toni", "Stark", 28, 4, "Глава Департамента");

            NewWorker("Danial", "Wail", 2, 23, 60, 15, "Worker");
            NewWorker("Gone", "Smit", 2, 34, 45, 20, "Worker");
            NewWorker("Kristofer", "Nolan", 2, 54, 40, 40, "Worker");
            NewWorker("Jim", "Pit", 2, 23, 50, 22, "Worker");

            NewIntern("Oleg", "Lobanov", 2, 33, 600);
            
        }
        public ObservableCollection<Employee> GetEmployees()
        {
            return _employees;
        }
        
        public void DelateEmployee(object employee)
        {
            _employees.Remove((Employee)employee);
        }
        public void EditDirector(Director director, uint id)
        {
            _employees.Insert(GetIndexEditEmployee(id), director);
        }
        public void EditWorker(Worker worker, uint id)
        {
            _employees.Insert(GetIndexEditEmployee(id), worker);
        }
        public void EditIntern(Intern Intern, uint id)
        {
            _employees.Insert(GetIndexEditEmployee(id), Intern);
        }
        public int GetIndexEditEmployee(uint id)
        {
            IEnumerable<Employee> value = from employees in _employees
                                          where employees.Id == id
                                          select employees;
            Employee employee = value.ToList()[0];
            int index = _employees.IndexOf(employee);
            return index;
        }
        private Director GetDirector(IEnumerable<Employee> employees)
        {
            IEnumerable<Employee> director = employees.Where(PredicatGetDirector);
            Director directorToList = (Director)director.ToList()[0];
            return directorToList;
        }
        private bool PredicatGetDirector(Employee director)
        {
            return director.Post == "Директор";
        }
        private bool GetEmployeesById(Employee employee)
        {
            return employee.IdDepartment == _idDepartment;
        }
    }
}
