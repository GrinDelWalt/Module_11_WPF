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
       
        public List<Department> Departments { get; set; }
        

        private ObservableCollection<Employee> _employees;

        public ObservableCollection<Employee> Employees
        {
            get => _employees;
            set => Set(ref _employees, value);
        }
        /// <summary>
        /// Заголовок Окна
        /// </summary>
        
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
            EmployeeManagement management = new EmployeeManagement();
            Employees = management.GetEmployees();
            management.Test();
            DepartmentManagement department = new DepartmentManagement();
            department.Test();
            Departments = department.GetDepartment();
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecuted);
        }
    }
}
