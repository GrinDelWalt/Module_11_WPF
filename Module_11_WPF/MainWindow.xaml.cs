using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Module_11_WPF
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


        private uint _idDepartment;
        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

        public MainWindow()
        {
            _departmentManagement = new DepartmentManagement();
            _employeeManagement = new EmployeeManagement();
            
            _departmentManagement.Test();
            _employeeManagement.Test();

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

        private void MenuItemDeleta_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            object employee = ListEmployee.SelectedItem;
            if (employee != null)
            {
                _employeeManagement.DelateEmployee(employee);
            }
            RefreshList();
        }
        private void Button_ClickEdit(object sender, RoutedEventArgs e)
        {
            object employee = ListEmployee.SelectedItem;
            if (employee != null)
            {
                _employeeManagement.EditEmployee(employee);
                RefreshList();
            }
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
                    _windowDirector = new WindowDirector();
                    _windowDirector.DelegatWindowDirector += EventNewDirector;
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
        private void EventNewDirector(object sender, EventArgs e)
        {
            string name = _windowDirector.NameDirector;
            string surmane = _windowDirector.Surname;
            uint age = _windowDirector.Age;
            _employeeManagement.NewDirektor(name, surmane, age, _idDepartment);
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

            uint idDepartment;

            idDepartment = department.IdDepartment;
            
            int count = _employeeManagement.GetCountEmployee(idDepartment);


            if (count != 0)
            {
                MesegeBoxInformational("В депортаме остались сотрудники, переведите или увольте их.");
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
            ObservableCollection<Department> departments = _departmentManagement.GetDepartment();
            departments.Add(_windowDepartmEdit.RecorgDepartment);
        }
    }

}
