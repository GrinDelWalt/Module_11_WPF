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
using Module_11_WPF.Models;

namespace Module_11_WPF.Views.EmployeeWindiw
{
    /// <summary>
    /// Логика взаимодействия для NewWorker.xaml
    /// </summary>
    public partial class WindowWorker : Window
    {
        public EventHandler DelegatWindowWorker = delegate { };
        
        public WindowWorker()
        {
            InitializeComponent();
            EditResult = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EditResult = true;
            try
            {
                NameWorker = name.Text;
                Surname = surname.Text;
                Post = post.Text;
                Age = Convert.ToUInt32(age.Text);
                WorkingHours = Convert.ToUInt32(workingHours.Text);
                HourlyPayment = Convert.ToUInt32(hourlyPayment.Text);
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
            DialogResult = true;
        }
        public void EditWorker(Worker worker)
        {
            name.Text = worker.Name;
            surname.Text = worker.Surname;
            post.Text = worker.Post;
            age.Text = Convert.ToString(worker.Age);
            workingHours.Text = Convert.ToString(worker.WorkingHours);
            hourlyPayment.Text = Convert.ToString(worker.HourlyPayment);
            _id = worker.Id;
            _idDepartment = worker.IdDepartment;
        }

        public bool EditResult { get; set; }
        public string NameWorker { get { return _nameWorker; } set { _nameWorker = value; } }
        public string Surname { get { return _surname; } set { _surname = value; } }
        public string Post { get { return _post; } set { _post = value; } }
        public uint Age { get { return _age; } set { _age = value; } }
        public uint Id { get { return _id; } }
        public uint WorkingHours { get { return _workingHours; } set { _workingHours = value; } }
        public uint IdDepartment { get { return _idDepartment; } set { _idDepartment = value; } }

        public uint HourlyPayment 
        { 
            get 
            { 
                return _hourlyPayment; 
            } 
            set 
            { 
                _hourlyPayment = value; 
                DelegatWindowWorker(null, EventArgs.Empty); 
            } 
        }

        private string _nameWorker;
        private string _surname;
        private string _post;
        private uint _age;
        private uint _id;
        private uint _workingHours;
        private uint _hourlyPayment;
        private uint _idDepartment;
    }
}
