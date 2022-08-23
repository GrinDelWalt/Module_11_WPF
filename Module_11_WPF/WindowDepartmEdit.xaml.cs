using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Module_11_WPF
{
    /// <summary>
    /// Логика взаимодействия для WindowDepartmEdit.xaml
    /// </summary>
    public partial class WindowDepartmEdit : Window
    {
        public EventHandler DelegatDepartmentNameEdit = delegate { };
        public EventHandler DelegatDeleteDepartment = delegate { };
        public EventHandler DelegatRecordDepartment = delegate { };

        private Department _department;
        private List<Department> _departments;
        public WindowDepartmEdit(Department department, List<Department> departmnens)
        {
            _departments = departmnens;
            _department = department;
            InitializeComponent();
        }



        private void Button_EditLocationDepartment(object sender, RoutedEventArgs e)
        {
            Department department = (Department)listDepartment.SelectedItem;

            _department.NameDepartment = nameDepartment.Text;
            if (_department.NameDepartment != _departments[0].NameDepartment)
            {
                if (listDepartment.SelectedItem == null)
                {
                    MesegeBoxInformational("Выбирите расположение департамента");
                    return;
                }
                if (_department == department)
                {
                    MesegeBoxInformational("Невозможно переместить департамент в этот же департамент");
                    return;
                }
                DepartmnetEdit = _department;
                department.Departments.Add(_department);
            }
            ICollectionView dataView =
              CollectionViewSource.GetDefaultView(listDepartment.ItemsSource);
            dataView.Refresh();

            DialogResult = true;
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
        public string NameDepartment
        {
            get { return _nameDepartment; }
            set
            {
                _nameDepartment = value;
                DelegatDepartmentNameEdit(null, EventArgs.Empty);
            }
        }
        public Department DepartmnetEdit
        {
            get { return _departmentEdit; }
            set
            {
                _departmentEdit = value;
                DelegatDeleteDepartment(null, EventArgs.Empty);
            }
        }
        public Department RecorgDepartment
        {
            get { return _recordDepartment; }
            set
            {
                _recordDepartment = value;
                DelegatRecordDepartment(null, EventArgs.Empty);
            }
        }

        private Department _recordDepartment;
        private string _nameDepartment;
        private Department _departmentEdit;

        private void listDepartment_Initialized(object sender, EventArgs e)
        {
            nameDepartment.Text = _department.NameDepartment;
            if (_department.NameDepartment != _departments[0].NameDepartment)
            {
                listDepartment.ItemsSource = _departments;
            }
        }
    }
}
