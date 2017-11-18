using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using SQLite;

namespace DiagnoseApp
{
    class TargetSynonymsVisibleDataAccess
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<TargetSynonymsVisible> TargetSynonymsVisible { get; set; }

        public TargetSynonymsVisibleDataAccess()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();

            //database.CreateTable<TargetSynonymsVisible>();

            this.TargetSynonymsVisible = new ObservableCollection<TargetSynonymsVisible>
                (database.Table<TargetSynonymsVisible>());
        }

        /* 4. Liste mit Symptomauswahl anzeigen */
        public IEnumerable<TargetSynonymsVisible> GetTargetSynonymsVisible()
        {
            lock (collisionLock)
            {
                return database.Query<TargetSynonymsVisible>
                ("SELECT Id, Name FROM TargetSynonymsVisible").AsEnumerable();
            }
        }

    }
}
