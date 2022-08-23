using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
            Employee employee;
            uint id = 0;
            object[] employeeObject = new object[1];
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    employeeObject = (object[])e.NewItems.SyncRoot;
                    employee = (Employee)employeeObject[0];
                    id = employee.IdDepartment;
                    _idDepartment = id;
                    EditSalary(employee);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    employeeObject = (object[])e.OldItems.SyncRoot;
                    employee = (Employee)employeeObject[0];
                    id = employee.IdDepartment;
                    _idDepartment = id;
                    EditSalary(employee);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    employeeObject = (object[])e.NewItems.SyncRoot;
                    employee = (Employee)employeeObject[0];
                    id = employee.IdDepartment;
                    _idDepartment = id;
                    EditSalary(employee);
                    break;
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
        private void EditSalary(Employee employeeEditSalary)
        {
            int index = _employees.IndexOf(employeeEditSalary);
            EditSalaryDeputyHeadDepartment(employeeEditSalary, index);
            EditSalaryHeadDepartment(employeeEditSalary, index);
            EditSalaryDeputyDirector(index);
            EditSalaryDirector(index);
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
        private IEnumerable<Employee> SerchDirector( string post)
        {
            IEnumerable<Employee> director = from employee in _employees
                                             where employee.Post == post
                                             select employee;
            return director;
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
