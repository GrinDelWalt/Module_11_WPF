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

namespace Module_11_WPF.Views.DepartmentWindow
{
    /// <summary>
    /// Логика взаимодействия для WindowDepartment.xaml
    /// </summary>
    public partial class WindowNewDepartment : Window
    {
        public EventHandler DelegatWindowDepartment = delegate { };
        private List<Department> _departments;
        private Department _departmnet;
        public WindowNewDepartment(List<Department> departments)
        {
            _departments = departments;
            InitializeComponent();
        }

        private void ListDepartment_Initialized(object sender, EventArgs e)
        {
            ListDepartment.ItemsSource = _departments;
        }

        private void ListDepartment_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            _departmnet = (Department)ListDepartment.SelectedItem;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Department department;
            if (_departmnet == null)
            {
                try
                {
                    NameDep = NameDepartment.Text;
                }
                catch (Exception)
                {
                    MessageBox.Show(
                                     "Неверный ввод",
                                     this.Title,
                                     MessageBoxButton.OK,
                                     MessageBoxImage.Information
                                     );
                    return;
                }
            }
            else
            {
                department = (Department)ListDepartment.SelectedItem;
                NewDepartmentInDepartment(department);
            }
            DialogResult = true;
        }
        private void NewDepartmentInDepartment(Department department)
        {
            department.NewDepartment(NameDepartment.Text);
        }
        public string NameDep
        {
            get
            {
                return _nameDepartmtnt;
            }
            set
            {
                _nameDepartmtnt = value;
                DelegatWindowDepartment(null, EventArgs.Empty);
            }
        }
        private string _nameDepartmtnt;
    }
}
