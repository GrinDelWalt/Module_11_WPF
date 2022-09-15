using Module_11_WPF.Infrastructure.Commands;
using Module_11_WPF.ViewModels.Base;
using System.Windows;
using System.Windows.Input;

namespace Module_11_WPF.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        /// <summary>
        /// Заголовок Окна
        /// </summary>
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
            //set
            //{
            //    //if (Equals(_title, value)) return;
            //    //_title = value;
            //    //OnPropertyChanget();
            //}
        }
        public string StatusProgram
        {
            get => _statusProgram;
            set => Set(ref _statusProgram, value);
        }

        private string _statusProgram = "Гогов!";
        private string _title = "Test";

        public ICommand CloseApplicationCommand { get; }

        private bool CanCloseApplicationCommandExecuted(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        public MainWindowViewModel()
        {
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecuted);
        }
    }
}
