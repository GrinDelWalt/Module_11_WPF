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

namespace Module_11_WPF
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class WindowSelection : Window
    {
        public EventHandler DelegatSelectionWindow = delegate { };

        private ReadOnlyCollection<object> _employees;

        public WindowSelection(ReadOnlyCollection<object> employees)
        {
            _employees = employees;
            InitializeComponent();
        }

        private void Button_Click_Director(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < _employees.Count; i++)
            {
                if (_employees[i].ToString() == "Module_11_WPF.Director" | _employees == null)
                {
                    MessageBox.Show(
                   "В депортамете есть директор",
                   this.Title,
                   MessageBoxButton.OK,
                   MessageBoxImage.Information
                   );
                    return;
                }
            }
            Selection = "Director";
            DialogResult = true;
        }
        private void Button_ClickWorker(object sender, RoutedEventArgs e)
        {
            Selection = "Worker";
            DialogResult = true;
        }
        private void Button_ClickIntern(object sender, RoutedEventArgs e)
        {
            Selection = "Intern";
            DialogResult = true;
        }

        public string Selection
        {
            get
            {
                return _selection;
            }
            set
            {
                _selection = value;
                DelegatSelectionWindow(null, EventArgs.Empty);
            }
        }

        private string _selection;
    }
}
