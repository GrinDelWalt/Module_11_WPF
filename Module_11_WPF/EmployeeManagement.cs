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
        private WindowDirector _windowDirector;
        private WindowWorker _windowWorker;
        private WindowIntern _windowIntern;

        private ObservableCollection<Employee> _employees;
        private static uint _idEmployee;
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
            //_directors = new List<Director>();
            //_workers = new List<Worker>();
            _employees = new ObservableCollection<Employee>();
            _employees.CollectionChanged += EditEventEmployee;
        }
        public void NewDirektor(string name, string surName, uint age, uint idDepartment)
        {
            _employees.Add(new Director(name, surName, age, NextId(), idDepartment));
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
            object[] employeeObject = new object[1];
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    employeeObject = (object[])e.NewItems.SyncRoot;
                    employee = (Employee)employeeObject[0];
                    break;
                case NotifyCollectionChangedAction.Remove:
                    employeeObject = (object[])e.OldItems.SyncRoot;
                    employee = (Employee)employeeObject[0];
                    break;
                case NotifyCollectionChangedAction.Replace:
                    employeeObject = (object[])e.NewItems.SyncRoot;
                    employee = (Employee)employeeObject[0];
                    break;
            }
        }
        public void GetCountEmployee(uint idDepartment)
        {
            var employee = _employees.Where()
        }
        public bool Fild(Employee employee)
        {
            return employee.
        }
        private void EditSalaryDirector()
        {

        }
        public void Test()
        {
            NewDirektor("Grin", "Walt", 25, 1);
            NewDirektor("Jone", "Soer", 31, 2);
            NewDirektor("Piter", "Parker", 21, 3);
            NewDirektor("Toni", "Stark", 28, 4);

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
        public void EditEmployee(object employee)
        {
            string type = employee.ToString();
            switch (type)
            {
                case "Module_11_WPF.Worker":
                    _windowWorker = new WindowWorker();
                    _windowWorker.EditWorker((Worker)employee);
                    _windowWorker.DelegatWindowWorker += EventEditWorker;
                    _windowWorker.ShowDialog();
                    break;
                case "Module_11_WPF.Director":
                    _windowDirector = new WindowDirector();
                    _windowDirector.EditDirector((Director)employee);
                    _windowDirector.DelegatWindowDirector += EventEditDirector;
                    _windowDirector.ShowDialog();
                    break;
                case "Module_11_WPF.Intern":
                    _windowIntern = new WindowIntern();
                    _windowIntern.EditIntern((Intern)employee);
                    _windowIntern.DelegatIternWindow += EventEditIntern;
                    _windowIntern.ShowDialog();
                    break;
                default:
                    break;
            }
        }
        private void EventEditDirector(object sender, EventArgs e)
        {
            if (_windowDirector.EditResult)
            {
                EditDirector();
            }
        }
        private void EditDirector()
        {
            int index = GetIndexEditEmployee();
            Director director = new Director()
            {
                Name = _windowDirector.NameDirector,
                Surname = _windowDirector.Surname,
                Age = _windowDirector.Age,
            };

            _employees.Insert(index, director);
        }
        private void EventEditIntern(object sender, EventArgs e)
        {
            if (_windowIntern.EditResult)
            {
                EditIntern();
            }
        }
        private void EditIntern()
        {
            int index = GetIndexEditEmployee();
            Intern intern = new Intern()
            {
                Name = _windowIntern.NameIntern,
                Surname = _windowIntern.Surname,
                Age = _windowIntern.Age,
                Salary = _windowIntern.Salary,
            };
            _employees.Insert(index, intern);
        }
        private void EventEditWorker(object sender, EventArgs e)
        {
            if (_windowWorker.EditResult)
            {
                EditWorker();
            }
        }
        private void EditWorker()
        {
            int index = GetIndexEditEmployee();
            Worker worker = new Worker()
            {
                Name = _windowWorker.NameWorker,
                Surname = _windowWorker.Surname,
                Post = _windowWorker.Post,
                HourlyPayment = _windowWorker.HourlyPayment,
                WorkingHours = _windowWorker.WorkingHours,
                Age = _windowWorker.Age,
            };
            _employees.Insert(index, worker);
        }
        private int GetIndexEditEmployee()
        {
            IEnumerable<Employee> value = from employees in _employees
                                          where employees.Id == _windowIntern.Id
                                          select employees;
            Employee employee = (Employee)value.ToList()[0];
            int index = _employees.IndexOf(employee);
            return index;
        }
    }
}
