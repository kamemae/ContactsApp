using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ContactsApp.MVVM.Models {
    public class Contact : INotifyPropertyChanged {
        private string _name;
        private string _surname;
        public string Name {
            get => _name;
            set {
                if(_name != value) {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Surname {
            get => _surname;
            set {
                if(_surname != value) {
                    _surname = value;
                    OnPropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}