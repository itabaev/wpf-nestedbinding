using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfNestedBinding.Sample
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _a = "A";
        private string _b = "B";
        private string _c = "C";
        private string _d = "D";
        private string _e = "E";
        private string _f = "F";

        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string A
        {
            get { return _a; }
            set
            {
                if (value == _a) return;
                _a = value;
                OnPropertyChanged();
            }
        }

        public string B
        {
            get { return _b; }
            set
            {
                if (value == _b) return;
                _b = value;
                OnPropertyChanged();
            }
        }

        public string C
        {
            get { return _c; }
            set
            {
                if (value == _c) return;
                _c = value;
                OnPropertyChanged();
            }
        }

        public string D
        {
            get { return _d; }
            set
            {
                if (value == _d) return;
                _d = value;
                OnPropertyChanged();
            }
        }

        public string E
        {
            get { return _e; }
            set
            {
                if (value == _e) return;
                _e = value;
                OnPropertyChanged();
            }
        }

        public string F
        {
            get { return _f; }
            set
            {
                if (value == _f) return;
                _f = value;
                OnPropertyChanged();
            }
        }
    }
}
