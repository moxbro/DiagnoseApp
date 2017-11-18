using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiagnoseApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowDesisesListViewPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }
        public object Console { get; private set; }
        ChosenSynonymsDiseasesDataAccess csdDA;
        DiseaseSymptomsSynonymsVisibleDataAccess dssvDA;

        public ShowDesisesListViewPage()
        {
            InitializeComponent();
            
            //Diseases
            csdDA = new ChosenSynonymsDiseasesDataAccess();
            IEnumerable<ChosenSynonymsDiseases> csdDa = csdDA.GetChosenSynonymsDiseases();

            this.BindingContext = ToObString(csdDa);

            //Disease Symptoms
            dssvDA = new DiseaseSymptomsSynonymsVisibleDataAccess();
            //List<String> symptoms = dssvDA.GetSynonymsfromDisease();

            //this.BindingContext = symptoms;
            
            
        }

        public ObservableCollection<String> ToObString(IEnumerable<ChosenSynonymsDiseases> tsv)
        {
            ObservableCollection<String> obs = new ObservableCollection<string>();

            foreach (ChosenSynonymsDiseases element in tsv)
            {
                obs.Add(element.DiseaseName);
            }

            return obs;
        }

        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            List<String> synonyms = dssvDA.GetSynonymsfromDisease(e.SelectedItem.ToString());
            String tmp = "";
            foreach (String s in synonyms)
            {
                tmp += s;
            }

            await DisplayAlert("Item Tapped", tmp, "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}