using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Module_11_WPF.ViewModel.Base
{
    internal class ViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool _desposed;
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        protected virtual void Dispose(bool Disposing)
        {
            if (!Disposing || _desposed) return;
            _desposed = true;
            // Освобождение управляемых ресурсов
        }

        protected virtual void OnPropertyChanget([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanget(PropertyName);
            return true;
        }
    }
}
