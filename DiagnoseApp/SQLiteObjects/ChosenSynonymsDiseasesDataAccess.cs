using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using SQLite;

namespace DiagnoseApp
{
    class ChosenSynonymsDiseasesDataAccess
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<ChosenSynonymsDiseases> ChosenSynonymsDiseases { get; set; }

        public ChosenSynonymsDiseasesDataAccess()
        {

            database = DependencyService.Get<IDatabaseConnection>().DbConnection();

            this.ChosenSynonymsDiseases = new ObservableCollection<ChosenSynonymsDiseases>
                (database.Table<ChosenSynonymsDiseases>());
        }

        public IEnumerable<ChosenSynonymsDiseases> GetChosenSynonymsDiseases()
        {
            lock (collisionLock)
            {
                return database.Query<ChosenSynonymsDiseases>
                ("SELECT DiseaseId, DiseaseName FROM ChosenSynonymsDiseases").AsEnumerable();
            }
        }
    }
}
