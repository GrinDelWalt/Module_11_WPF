using System.Collections.Generic;
using Module_11_WPF.Infrastructure.Commands;
using Module_11_WPF.Models;
using Module_11_WPF.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace Module_11_WPF.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private List<Department> _departments;

        public List<Department> Departments
        {
            get => _departments;
            set => Set(ref _departments, value);
        }


        private ObservableCollection<Employee> _employees;

        public ObservableCollection<Employee> Employees
        {
            get => Employees;
            set => Set(ref _employees, value);
        }
        /// <summary>
        /// Заголовок Окна
        /// </summary>
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
            //set
            //{
            //    //if (Equals(_title, value)) return;
            //    //_title = value;
            //    //OnPropertyChanget();
            //}
        }
        public string StatusProgram
        {
            get => _statusProgram;
            set => Set(ref _statusProgram, value);
        }

        private string _statusProgram = "Гогов!";
        private string _title = "Test";

        public ICommand CloseApplicationCommand { get; }

        private bool CanCloseApplicationCommandExecuted(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        public MainWindowViewModel()
        {
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecuted);
            EmployeeManagement management = new EmployeeManagement();
            Employees = management.GetEmployees();
            DepartmentManagement department = new DepartmentManagement();
            Departments = department.GetDepartment();
        }
    }
}
