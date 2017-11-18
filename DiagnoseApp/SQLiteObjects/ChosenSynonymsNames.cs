using System.ComponentModel;
using SQLite;

namespace DiagnoseApp
{
    [Table("ChosenSynonymsNames")]
    public class ChosenSynonymsNames : INotifyPropertyChanged
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

        //Source
        private int _source;
        [NotNull]
        public int Source
        {
            get
            {
                return _source;
            }
            set
            {
                this._source = value;
                OnPropertyChanged(nameof(Source));
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
