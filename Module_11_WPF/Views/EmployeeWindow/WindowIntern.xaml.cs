using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для WindowNewIntern.xaml
    /// </summary>
    public partial class WindowIntern : Window
    {
        public EventHandler DelegatIternWindow = delegate { };

        public WindowIntern()
        {
            InitializeComponent();
            EditResult = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EditResult = true;
            try
            {
                NameIntern = name.Text;
                Surname = surname.Text;
                Age = Convert.ToUInt32(age.Text);
                Salary = Convert.ToUInt32(salary.Text);
            }
            catch (Exception error)
            {
                MessageBox.Show(
                  error.Message,
                  this.Title,
                  MessageBoxButton.OK,
                  MessageBoxImage.Information
                  );
                return;
            }
            DialogResult = true;
        }
        public void EditIntern(Intern intern)
        {
            name.Text = intern.Name;
            surname.Text = intern.Surname;
            age.Text = Convert.ToString(intern.Age);
            salary.Text = Convert.ToString(intern.Salary);
            _id = intern.Id;
            _idDepartment = intern.IdDepartment;
        }
        public bool EditResult { get; set; }
        public string NameIntern { get { return _nameIntern; } set { _nameIntern = value; } }
        public string Surname { get { return _surname; } set { _surname = value; } }
        public uint Age { get { return _age; } set { _age = value; } }
        public uint Id { get { return _id; } }
        public uint IdDepartment { get { return _idDepartment; } set { _idDepartment = value; } }
        public uint Salary
        {
            get
            {
                return _salary;
            }
            set
            {
                _salary = value;
                DelegatIternWindow(null, EventArgs.Empty);
            }
        }

        private string _nameIntern;
        private string _surname;
        private uint _age;
        private uint _id;
        private uint _salary;
        private uint _idDepartment;
    }
}
