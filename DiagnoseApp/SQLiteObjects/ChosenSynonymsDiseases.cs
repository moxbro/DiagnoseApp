using System.ComponentModel;
using SQLite;

namespace DiagnoseApp
{
    [Table("ChosenSynonymsDiseases")]
    public class ChosenSynonymsDiseases : INotifyPropertyChanged
    {
        //ID
        private int _diseaseId;
        [PrimaryKey, AutoIncrement]
        public int DiseaseId
        {
            get
            {
                return _diseaseId;
            }
            set
            {
                this._diseaseId = value;
                OnPropertyChanged(nameof(DiseaseId));
            }
        }
        //Name
        private string _diseaseName;
        [NotNull]
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
