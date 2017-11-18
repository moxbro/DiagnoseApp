using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using SQLite;

namespace DiagnoseApp
{
    class ChosenSynonymsNamesVisibleDataAccess
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<ChosenSynonymsNamesVisible> ChosenSynonymsNamesVisible { get; set; }

        public ChosenSynonymsNamesVisibleDataAccess()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();

            this.ChosenSynonymsNamesVisible = new ObservableCollection<ChosenSynonymsNamesVisible>
                (database.Table<ChosenSynonymsNamesVisible>());
        }

        /* 6. Liste der (bereits) ausgewählten Symptome/Synonyme anzeigen */
        public IEnumerable<ChosenSynonymsNamesVisible> GetChosenSynonymsNamesVisible()
        {
            lock (collisionLock)
            {
                return database.Query<ChosenSynonymsNamesVisible>
                ("SELECT Id, Name FROM ChosenSynonymsNamesVisible").AsEnumerable();
            }
        }

        public void DeleteAllChosenSynonymsNamesVisible()
        {
            lock (collisionLock)
            {
                database.Query<ChosenSynonymsNamesVisible>
                ("DELETE FROM ChosenSynonymsNamesVisible;");
            }
        }

        public void DeleteChosenSynonymsNamesVisible(string name)
        {
            lock (collisionLock)
            {
                database.Query<ChosenSynonymsNamesVisible>
                ("DELETE FROM ChosenSynonymsNames WHERE Name = ?", name);
            }
        }

    }
}
