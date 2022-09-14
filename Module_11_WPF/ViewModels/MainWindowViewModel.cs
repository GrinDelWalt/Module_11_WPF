using Module_11_WPF.ViewModels.Base;

namespace Module_11_WPF.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private string _title = "Test";
        /// <summary>
        /// Заголовок Окна
        /// </summary>
        public string Title
        {
            get => _title;
            //set
            //{
            //    //if (Equals(_title, value)) return;
            //    //_title = value;
            //    //OnPropertyChanget();
            //}
            set => Set(ref _title, value);
        }
    }
}
