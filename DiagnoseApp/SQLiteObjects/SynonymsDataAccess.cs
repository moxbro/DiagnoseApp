using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;
//using SQLite.Net;
using SQLite;

namespace DiagnoseApp
{
    class SynonymsDataAccess
    {
        public Object obi;
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<Synonyms> Synonyms { get; set; }

        public SynonymsDataAccess()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();

            //database.CreateTable<Synonyms>();

            this.Synonyms = new ObservableCollection<Synonyms>
                (database.Table<Synonyms>());
        }

        /* 4. Liste mit Synonyms anzeigen */
        public IEnumerable<Synonyms> GetSynonyms()
        {
            lock (collisionLock)
            {
                return database.Query<Synonyms>
                ("SELECT Id, Name, SymptomId FROM Synonyms").AsEnumerable();
            }
        }
    }

}
