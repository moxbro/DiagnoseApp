using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using SQLite;
using System;

namespace DiagnoseApp
{
    class DiseaseSymptomsSynonymsVisibleDataAccess
    {
        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<DiseaseSymptomsSynonymsVisible> DiseaseSymptomsSynonymsVisible { get; set; }

        public DiseaseSymptomsSynonymsVisibleDataAccess()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();

            this.DiseaseSymptomsSynonymsVisible = new ObservableCollection<DiseaseSymptomsSynonymsVisible>
                (database.Table<DiseaseSymptomsSynonymsVisible>());
        }

        public List<String> GetSynonymsfromDisease(String diseaseName)
        {
            List<String> Symtoms = new List<string>();
            lock (collisionLock)
            {
                IEnumerable<DiseaseSymptomsSynonymsVisible> tmp = 
                    database.Query<DiseaseSymptomsSynonymsVisible>
                ("SELECT SymptomName, SynonymNames FROM DiseaseSymptomsSynonymsVisible WHERE DiseaseName = ?", diseaseName).AsEnumerable();
                foreach (DiseaseSymptomsSynonymsVisible Element in tmp)
                {
                    Symtoms.Add(Element.SymptomName);
                    //Mal schauen ob das passt...
                    if (Element.SynonymNames != null)
                    {
                        String[] synonyms = Element.SynonymNames.Split(new String[] { "; " }, StringSplitOptions.RemoveEmptyEntries);
                        foreach(String s in synonyms)
                        {
                            Symtoms.Add(s);
                        }
                    }
                }
            }
            return Symtoms;
        }

        public List<String> GetSynonymsfromAllDiseases()
        {
            return null;
        }


    }
}
