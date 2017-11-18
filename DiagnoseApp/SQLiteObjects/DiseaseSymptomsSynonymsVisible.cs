using SQLite;
using System.ComponentModel;

namespace DiagnoseApp
{
    [Table("DiseaseSymptomsSynonymsVisible")]
    public class DiseaseSymptomsSynonymsVisible : INotifyPropertyChanged
    {
        //DiseaseName
        private string _diseaseName;
        [PrimaryKey, AutoIncrement]
        public string DiseaseName
        {
            get
            {
                return _diseaseName;
            }
            set
            {
                this._diseaseName = value;
                OnPropertyChanged(nameof(DiseaseName));
            }
        }
        //SymptomName
        private string _symptomName;
        [PrimaryKey, AutoIncrement]
        public string SymptomName
        {
            get
            {
                return _symptomName;
            }
            set
            {
                this._symptomName = value;
                OnPropertyChanged(nameof(SymptomName));
            }
        }
        //SynonymNames
        private string _synonymNames;
        [NotNull]
        public string SynonymNames
        {
            get
            {
                return _synonymNames;
            }
            set
            {
                this._synonymNames = value;
                OnPropertyChanged(nameof(SynonymNames));
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
