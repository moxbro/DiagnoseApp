using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using SQLite;

namespace DiagnoseApp
{
    class ChosenSynonymsNamesDataAccess
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<ChosenSynonymsNames> ChosenSynonymsNames { get; set; }

        public ChosenSynonymsNamesDataAccess(/*SQLiteConnection database*/)
        {
            //this.database = database;
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();

            //database.CreateTable<ChosenSynonymsNames>();

            this.ChosenSynonymsNames = new ObservableCollection<ChosenSynonymsNames>
                (database.Table<ChosenSynonymsNames>());
            
            // If the table is empty, initialize the collection
            //if (!database.Table<Customer>().Any())
            //{
            //    AddNewCustomer();
            //}
        }
            

       

        public void AddNewChosenSynonymsNames(String name, int source)
        {
            ChosenSynonymsNames csm = new ChosenSynonymsNames
            {
                Name = name,
                Source = source,
            };
            this.ChosenSynonymsNames.Add(csm);
            SaveChosenSynonymsName(csm);
        }

        public void InitialiseAgeSex(String sex, int age)
        {
            if (sex == "male")
                AddNewChosenSynonymsNames("SYMBOL_USER_GENDER_MALE", 1);
            else
                if (sex == "female")
                    AddNewChosenSynonymsNames("SYMBOL_USER_GENDER_FEMALE", 1);

            if (age > 99) age = 99;

            if(age > 0)
                AddNewChosenSynonymsNames("SYMBOL_USER_AGE_" + age.ToString("D2"), 1);
        }

        public int SaveChosenSynonymsName(ChosenSynonymsNames ChosenSynonymsNameInstance)
        {
            lock (collisionLock)
            {
                if (ChosenSynonymsNameInstance.Id != 0)
                {
                    database.Update(ChosenSynonymsNameInstance);
                    return ChosenSynonymsNameInstance.Id;
                }
                else
                {
                    database.Insert(ChosenSynonymsNameInstance);
                    return ChosenSynonymsNameInstance.Id;
                }
            }
        }


        //6. Liste der (bereits) ausgewählten Symptome/Synonyme anzeigen */
        //Hier am besten irgendwann nur nach 2 Filtern, 
        //da man sonst auch Geschlecht und Alter mit drinnen hat
        public IEnumerable<ChosenSynonymsNames> GetChosenSynonymsNames()
        {
            lock (collisionLock)
            {
                return database.Query<ChosenSynonymsNames>
                ("SELECT Id, Source, Name FROM ChosenSynonymsNames").AsEnumerable();
            }
        }

        /*
        public void DeleteAllChosenSynonymsNames()
        {
            lock (collisionLock)
            {
                database.DropTable<ChosenSynonymsNames>();
                database.CreateTable<ChosenSynonymsNames>();
            }
            this.ChosenSynonymsNames = null;
            this.ChosenSynonymsNames = new ObservableCollection<ChosenSynonymsNames>
                (database.Table<ChosenSynonymsNames>());
        }
        */

        // Datenbank konfigurieren
        /* 1. Die notwenigen PRAGMAs auslesen und als SQL in der App ausführen */
        public void configDatabase()
        {
            lock (collisionLock)
            {
                //var query = SELECT Value FROM Parameters WHERE Key = 'DatabasePragma';
            }
        }

    }
}
