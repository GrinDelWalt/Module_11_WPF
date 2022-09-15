using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Module_11_WPF.Model;

namespace Module_11_WPF
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class WindowEmployeeTransfer : Window
    {
        private Employee _employee;
        private List<Department> _departmens;
        private ObservableCollection<Employee> _employees;
        private uint _idDepartment;
        public WindowEmployeeTransfer(Employee employee, List<Department> departments, ObservableCollection<Employee> employees)
        {
            _employee = employee;
            _employees = employees;
            _departmens = departments;
            InitializeComponent();
        }

        private void ListDepartments_Initialized(object sender, EventArgs e)
        {
            ListDepartments.ItemsSource = _departmens;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Department departmentSelected = (Department)ListDepartments.SelectedItem;
            if (GetListEmployee(departmentSelected.IdDepartment))
            {
                MesegeBoxInformational($"В этом департаменте есть {_employee.Post}");
                return;
            }
            if (departmentSelected.IdDepartment == 1)
            {
                MesegeBoxInformational("В управление компании нет такой должности");
                return;
            }
            ChackDepartment();
            _idDepartment = departmentSelected.IdDepartment;
            IEnumerable<Employee> employeesSort = _employees.Where(Sorting);
            DialogResult = true;
        }

        private bool GetListEmployee(uint idDepartment)
        {
            bool result = false;
            IEnumerable<Employee> employees = from employee in _employees
                                              where employee.IdDepartment == idDepartment
                                              where employee.ToString() == "Module_11_WPF.Director"
                                              select employee;
            foreach (var item in employees)
            {
                if (item.Post == _employee.Post)
                {
                    result = true;
                }
            }
            return result;
        }
        
        private void ChackDepartment()
        {
            Department department = (Department)ListDepartments.SelectedItem;
            uint idDepartment = department.IdDepartment;
            IEnumerable<Employee> employeeSort = from employee in _employees
                                             where employee.IdDepartment == idDepartment
                                             select employee;
            foreach (var element in employeeSort)
            {
                if (element == _employee)
                {
                    MesegeBoxInformational("Hевозможно перевести в этот же департамент");
                    return;
                }
            }
            _idDepartment = idDepartment;
            TransferEmployee();
        }
        private void TransferEmployee()
        {
            int index = _employees.IndexOf(_employee);
            _employees.Remove(_employee);
            _employee.IdDepartment = _idDepartment;
            _employees.Insert(index, _employee);
        }

        private bool Sorting(Employee employee)
        {
            return employee.IdDepartment == _idDepartment;
        }
        private void MesegeBoxInformational(string text)
        {
            MessageBox.Show(
                       text,
                       this.Title,
                       MessageBoxButton.OK,
                       MessageBoxImage.Information
                       );
        }
    }
}
