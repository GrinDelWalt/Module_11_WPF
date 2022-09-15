using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Module_11_WPF.Views.Menu
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
            int countHead = 0; 
            for (int i = 0; i < _employees.Count; i++)
            {
                if (_employees[i].ToString() == "Module_11_WPF.Director")
                {
                    countHead++;
                   
                }
                if (countHead == 2)
                {
                    MessageBoxInformation("В департамете есть управленцы");
                    return;
                }
            }
            Selection = "Director";
            DialogResult = true;
        }
        private void MessageBoxInformation(string text)
        {
            MessageBox.Show(
                  "В депортамете есть директор",
                  this.Title,
                  MessageBoxButton.OK,
                  MessageBoxImage.Information
                  );
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
