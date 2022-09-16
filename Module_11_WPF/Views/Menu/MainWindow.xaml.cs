using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Module_11_WPF.Models;
using Module_11_WPF.Views.DepartmentWindow;
using Module_11_WPF.Views.EmployeeWindiw;

namespace Module_11_WPF.Views.Menu
{


    public partial class MainWindow : Window
    {
        private DepartmentManagement _departmentManagement;
        private EmployeeManagement _employeeManagement;
        private WindowSelection _windowSelection;
        private WindowDirector _windowDirector;
        private WindowWorker _windowWorker;
        private WindowIntern _windowIntern;
        private WindowNewDepartment _windowDepartment;
        private WindowDepartmEdit _windowDepartmEdit;
        private WindowEmployeeTransfer _windowEmployeeTransfer;

        private List<string> _listPostDirector;
        private List<string> _listPostHead;

        private uint _idDepartment;
        private GridViewColumnHeader _lastHeaderClicked = null;
        private ListSortDirection _lastDirection = ListSortDirection.Ascending;

        public MainWindow()
        {
            DeserializationJSON deserialization = new DeserializationJSON();
            deserialization.DeserializJSON();

            //_employeeManagement = deserialization.GetEmployeeManagement();
            //_departmentManagement = deserialization.GetDepartmentManagement();
            //_departmentManagement.Test();
            //_employeeManagement.Test();

            ReadListPost();
            InitializeComponent();
        }

        private void Menu_Initialized(object sender, EventArgs e)
        {
            Menu.ItemsSource = _departmentManagement.GetDepartment();
        }

        private void ButtonName_Click(object sender, RoutedEventArgs e)
        {
            Button buttonText = (Button)e.Source;
            string text = buttonText.Content.ToString();
        }

        private void Menu_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Department department = (Department)e.NewValue;
            _idDepartment = department.IdDepartment;
            ListEmployee.ItemsSource = _employeeManagement.GetEmployees().Where(fildEmployee);
        }
        private bool fildEmployee(Employee employee)
        {
            return employee.IdDepartment == _idDepartment;
        }
        void GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader content = (GridViewColumnHeader)e.OriginalSource;

            Binding bilding = (Binding)content.Column.DisplayMemberBinding;
            string path = bilding.Path.Path;
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != _lastHeaderClicked)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        if (_lastDirection == ListSortDirection.Ascending)
                        {
                            direction = ListSortDirection.Descending;
                        }
                        else
                        {
                            direction = ListSortDirection.Ascending;
                        }
                    }

                    Sort(path, direction);

                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }
            }
        }

        private void Sort(string sortBy, ListSortDirection direction)
        {
            ListEmployee.Items.SortDescriptions.Clear();
            ListEmployee.Items.SortDescriptions.Add(new SortDescription(sortBy, direction));
        }

        private void MenuItemEdit_Click(object sender, RoutedEventArgs e)
        {
            _windowSelection.Owner = this;
            _windowSelection.ShowDialog();
        }

        private void Button_ClickDeleteEmployee(object sender, RoutedEventArgs e)
        {
            object employee = ListEmployee.SelectedItem;
            if (employee != null)
            {
                _employeeManagement.DelateEmployee(employee);
            }
            RefreshList();
        }
        private void Button_ClickEditEmployee(object sender, RoutedEventArgs e)
        {
            object employee = ListEmployee.SelectedItem;
            if (employee == null)
            {
                MesegeBoxInformational("Выбирите сотрудника");
                return;
            }
            Department department = (Department)Menu.SelectedItem;
            EditEmployee(employee, department.IdDepartment);
            RefreshList();
        }
        public void EditEmployee(object employee, uint idDepartment)
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
                    _windowDirector = new WindowDirector(idDepartment);
                    _windowDirector.GetEmployee(_employeeManagement.GetEmployees());
                    _windowDirector.DelegatWindowDirector += EventEditDirector;
                    Department department = (Department)Menu.SelectedItem;
                    CheckEmployee(department, true);
                    _windowDirector.EditDirector((Director)employee);
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
        private void EventEditWorker(object sender, EventArgs e)
        {
            if (_windowWorker.EditResult)
            {
                EditWorker();
            }
        }
        private void EventEditIntern(object sender, EventArgs e)
        {
            if (_windowIntern.EditResult)
            {
                EditIntern();
            }
        }
        private void EditDirector()
        {
            Director director = new Director()
            {
                Name = _windowDirector.NameDirector,
                Surname = _windowDirector.Surname,
                Age = _windowDirector.Age,
                IdDepartment = _windowDirector.IdDepartment,

            };

            _employeeManagement.EditDirector(director, _windowDirector.Id);
        }
        private void EditWorker()
        {
            Worker worker = new Worker()
            {
                Name = _windowWorker.NameWorker,
                Surname = _windowWorker.Surname,
                Post = _windowWorker.Post,
                HourlyPayment = _windowWorker.HourlyPayment,
                WorkingHours = _windowWorker.WorkingHours,
                Age = _windowWorker.Age,
                IdDepartment = _windowWorker.IdDepartment,
            };
            _employeeManagement.EditWorker(worker, _windowWorker.Id);
        }
        private void EditIntern()
        {
            Intern intern = new Intern()
            {
                Name = _windowIntern.NameIntern,
                Surname = _windowIntern.Surname,
                Age = _windowIntern.Age,
                Salary = _windowIntern.Salary,
                IdDepartment = _windowIntern.IdDepartment,
            };
            _employeeManagement.EditIntern(intern, _windowIntern.Id);
        }
        private void Button_Click_NewEmployee(object sender, RoutedEventArgs e)
        {
            if (Menu.SelectedItem != null)
            {
                ReadOnlyCollection<object> employee = ListEmployee.ItemContainerGenerator.Items;
                Department department = (Department)Menu.SelectedItem;

                try
                {
                    _idDepartment = department.IdDepartment;
                    _windowSelection = new WindowSelection(employee);
                    _windowSelection.DelegatSelectionWindow += EventValueSelectidWindowEmployee;
                    _windowSelection.ShowDialog();

                    RefreshList();
                }
                catch (Exception error)
                {
                    MessageBox.Show(
                       Convert.ToString(error),
                       this.Title,
                       MessageBoxButton.OK,
                       MessageBoxImage.Information
                       );
                    return;
                }
            }
            else
            {
                MesegeBoxInformational("Выбирете департамент");
                return;
            }
        }
        private void EventValueSelectidWindowEmployee(object sender, EventArgs e)
        {
            StartWindowNewEmployee(_windowSelection.Selection);
        }
        private void StartWindowNewEmployee(string resultSelectid)
        {
            switch (resultSelectid)
            {
                case "Director":
                    Department department = (Department)Menu.SelectedItem;
                    _windowDirector = new WindowDirector(department.IdDepartment);
                    _windowDirector.DelegatWindowDirector += EventNewDirector;
                    _windowDirector.GetEmployee(_employeeManagement.GetEmployees());
                    CheckEmployee(department, false);
                    _windowDirector.ShowDialog();

                    break;
                case "Worker":
                    _windowWorker = new WindowWorker();
                    _windowWorker.DelegatWindowWorker += EventNewWorker;
                    _windowWorker.ShowDialog();
                    break;
                case "Intern":
                    _windowIntern = new WindowIntern();
                    _windowIntern.DelegatIternWindow += EventNewIntern;
                    _windowIntern.ShowDialog();
                    break;
                default:
                    break;
            }
        }
        private void ReadListPost()
        {
            _listPostDirector = new List<string>() { "Директор", "Заместитель директора" };
            _listPostHead = new List<string>() { "Глава Департамента", "Заместитель главы департамента" };
        } 
        
        private void CheckEmployee(Department department, bool checkAction)
        {
            List<Department> departments = _departmentManagement.GetDepartment();
            if (checkAction)
            {
                if (department == departments[0])
                {
                    _windowDirector.FillingListPost(_listPostDirector);
                }
                else
                {
                    _windowDirector.FillingListPost(_listPostHead);
                }
            }
            else
            {
                if (department == departments[0])
                {
                    CheckPost(department, _listPostDirector);
                }
                else
                {
                    CheckPost(department, _listPostHead);
                }
            }
            
        }
        private void CheckPost(Department department, List<string> listPostHeads)
        {
            IEnumerable<Employee> heads = from employee in _employeeManagement.GetEmployees()
                                          where employee.IdDepartment == department.IdDepartment
                                          where employee.ToString() == "Module_11_WPF.Director"
                                          select employee;
            foreach (var head in heads)
            {
                if (listPostHeads.Contains(head.Post))
                {
                    listPostHeads.Remove(head.Post);
                }
            }
            _windowDirector.FillingListPost(listPostHeads);
        }
        private void EventNewDirector(object sender, EventArgs e)
        {
            string name = _windowDirector.NameDirector;
            string surmane = _windowDirector.Surname;
            string post = _windowDirector.Post;
            uint age = _windowDirector.Age;
            _employeeManagement.NewDirektor(name, surmane, age, _idDepartment, post);
        }
        private void EventNewWorker(object sender, EventArgs e)
        {
            string name = _windowWorker.NameWorker;
            string surname = _windowWorker.Surname;
            string post = _windowWorker.Post;
            uint age = _windowWorker.Age;
            uint workingHours = _windowWorker.WorkingHours;
            uint hourlyPayment = _windowWorker.HourlyPayment;
            _employeeManagement.NewWorker(name, surname, _idDepartment, age, workingHours, hourlyPayment, post);
        }
        private void EventNewIntern(object sender, EventArgs e)
        {
            string name = _windowIntern.NameIntern;
            string surname = _windowIntern.Surname;
            uint age = _windowIntern.Age;
            uint salary = _windowIntern.Salary;
            _employeeManagement.NewIntern(name, surname, _idDepartment, age, salary);
        }
        private void RefreshList()
        {
            ICollectionView dataView =
              CollectionViewSource.GetDefaultView(ListEmployee.ItemsSource);
            dataView.Refresh();
        }

        private void Button_DelateDepartment(object sender, RoutedEventArgs e)
        {
            if (null == Menu.SelectedItem)
            {
                MesegeBoxInformational("Выбирете департамент");
                return;
            }
            Department department = (Department)Menu.SelectedItem;
            List<Department> departments = _departmentManagement.GetDepartment();

            if (department.NameDepartment == departments[0].NameDepartment)
            {
                MesegeBoxInformational("Невозможно удалить компанию");
                return;
            }

            uint idDepartment;

            idDepartment = department.IdDepartment;

            int count = _employeeManagement.GetCountEmployee(idDepartment);


            if (count != 0)
            {
                MesegeBoxInformational("В депортаме остались сотрудники.");
                return;
            }
            _departmentManagement.DelateDepartment(department);
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

        private void Button_NewDepartment(object sender, RoutedEventArgs e)
        {
            _windowDepartment = new WindowNewDepartment(_departmentManagement.GetDepartment());
            _windowDepartment.DelegatWindowDepartment += EventNewDepartment;
            _windowDepartment.ShowDialog();
            ICollectionView dataView =
              CollectionViewSource.GetDefaultView(Menu.ItemsSource);
            dataView.Refresh();
        }
        private void EventNewDepartment(object sender, EventArgs e)
        {
            _departmentManagement.NewDepartment(_windowDepartment.NameDep);
        }

        private void Button_EditDepartment(object sender, RoutedEventArgs e)
        {
            if (Menu.SelectedItem == null)
            {
                MesegeBoxInformational("Выбирете департамент");
                return;
            }
            _windowDepartmEdit = new WindowDepartmEdit((Department)Menu.SelectedItem, _departmentManagement.GetDepartment());
            _windowDepartmEdit.DelegatDeleteDepartment += EventDelateDepartment;
            _windowDepartmEdit.ShowDialog();
        }
        private void EventDelateDepartment(object sender, EventArgs e)
        {
            _departmentManagement.DelateDepartment(_windowDepartmEdit.DepartmnetEdit);
        }
        private void EventRecordDepartment(object sender, EventArgs e)
        {
            List<Department> departments = _departmentManagement.GetDepartment();
            departments[0].Departments.Add(_windowDepartmEdit.RecorgDepartment);
        }

        private void Button_EmployeeTransfer(object sender, RoutedEventArgs e)
        {
            Employee employee = (Employee)ListEmployee.SelectedItem;
            if (employee == null)
            {
                MesegeBoxInformational("Выбирите сотрудника");
                return;
            }

            if (employee.IdDepartment == 1)
            {
                MesegeBoxInformational("Главу и заместителя главы компании невозможно перевести");
                return;
            }
            _windowEmployeeTransfer = new WindowEmployeeTransfer(employee, _departmentManagement.GetDepartment(), _employeeManagement.GetEmployees());
            _windowEmployeeTransfer.ShowDialog();
            RefreshList();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            SerializationJSON serelase = new SerializationJSON();
            serelase.Serelase(_departmentManagement.GetDepartment(), _employeeManagement.GetEmployees());
        }
    }

}
