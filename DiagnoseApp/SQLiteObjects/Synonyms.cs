using System.ComponentModel;
//using SQLite.Net.Attributes;
using SQLite;

namespace DiagnoseApp
{
    [Table("Synonyms")]
    class Synonyms : INotifyPropertyChanged
    {
        //ID
        private int _id;
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                this._id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        //Name
        private string _name;
        [NotNull]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                this._name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        //Target
        private string _symptomId;
        public string SymptomId
        {
            get
            {
                return _symptomId;
            }
            set
            {
                this._name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
