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

namespace Module_11_WPF
{
    
    public partial class WindowDirector : Window
    {
        public EventHandler DelegatWindowDirector = delegate { };
        public WindowDirector()
        {
            EditResult = false;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EditResult = true;
            try
            {
                NameDirector = nameDirector.Text;
                Surname = surname.Text;
                Age = Convert.ToUInt32(age.Text);
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
        public void EditDirector(Director director)
        {
            nameDirector.Text = director.Name;
            surname.Text = director.Surname;
            age.Text = Convert.ToString(director.Age);
            _id = director.Id;
        }
        
        public bool EditResult { get; set; }
        public string NameDirector { get { return _nameDirector; } private set { _nameDirector = value; } }
        public string Surname { get { return _surname; } private set { _surname = value; } }
        public uint Id { get { return _id; } }
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
        private string _nameDirector;
        private string _surname;
        private uint _age;
        private uint _id;
    }
}
