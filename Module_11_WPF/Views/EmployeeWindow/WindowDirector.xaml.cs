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
using Module_11_WPF.Models;

namespace Module_11_WPF.Views.EmployeeWindiw
{

    public partial class WindowDirector : Window
    {
        public EventHandler DelegatWindowDirector = delegate { };

        private Director _director;

        private ObservableCollection<Employee> _employees;
        private List<string> _listPosts;
        private bool _companySelected;
        public WindowDirector(uint idDepartment)
        {
            EditResult = false;
            InitializeComponent();
        }
        public void ResultSelected(bool result)
        {
            _companySelected = result;
        }
        
        public void GetEmployee(ObservableCollection<Employee> employees)
        {
            _employees = employees;
        }
        public void FillingListPost(List<string> Posts)
        {
            listPost.ItemsSource = Posts;
            _listPosts = Posts;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EditResult = true;
            try
            {
                NameDirector = nameDirector.Text;
                Surname = surname.Text;
                Post = (string)listPost.SelectedItem;
                Age = Convert.ToUInt32(age.Text);
                if (listPost.SelectedItem == null)
                {
                    MessageBoxInformation("Выберите должность");
                    return;
                }
            }
            catch (Exception)
            {
                MessageBoxInformation("Неверный ввод");
                return;
            }
            DialogResult = true;
        }
        private void MessageBoxInformation(string text)
        {
            MessageBox.Show(
                 text,
                 this.Title,
                 MessageBoxButton.OK,
                 MessageBoxImage.Information
                 );
        }
        public void EditDirector(Director director)
        {
            _director = director;
            nameDirector.Text = director.Name;
            surname.Text = director.Surname;
            age.Text = Convert.ToString(director.Age);
            _id = director.Id;
            int index = _listPosts.IndexOf(director.Post); 
            listPost.SelectedIndex = index;
            _idDepartment = director.IdDepartment;
        }
        public uint IdDepartment { get { return _idDepartment; } set { _idDepartment = value; } }

        public bool EditResult { get; set; }
        public string NameDirector { get { return _nameDirector; } private set { _nameDirector = value; } }
        public string Surname { get { return _surname; } private set { _surname = value; } }
        public uint Id { get { return _id; } }
        public string Post { get { return _post; } set { _post = value; } }
        public uint Age 
        { 
            get
            {
                return _age;
            }
            set 
            {
                _age = value;
                DelegatWindowDirector(null, EventArgs.Empty); 
            } 
        }
        private string _post;
        private string _nameDirector;
        private string _surname;
        private uint _age;
        private uint _id;
        private uint _idDepartment;
    }
}
