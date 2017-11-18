using System;
using System.IO;
using System.Collections;
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
    public partial class SymtomsListViewPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }
        public ObservableCollection<String> ob;
        public String highlighted;
        public object Console { get; private set; }
        ChosenSynonymsNamesDataAccess csnDA;

        public SymtomsListViewPage()
        {
            InitializeComponent();
            csnDA = new ChosenSynonymsNamesDataAccess();
            highlighted = "0";

            TargetSynonymsVisibleDataAccess tsda = new TargetSynonymsVisibleDataAccess();
            IEnumerable<TargetSynonymsVisible> tsvIE = tsda.GetTargetSynonymsVisible();

            //BindingContext = this;
            //Items = ToObString(tsvIE);

            ob = ToObString(tsvIE);
            this.BindingContext = ob;
            System.Diagnostics.Debug.WriteLine(tsvIE.ToString());
            

        }

        public ObservableCollection<String> ToObString(IEnumerable<TargetSynonymsVisible> tsv)
        {
            ObservableCollection<String> obs = new ObservableCollection<string>();

            foreach (TargetSynonymsVisible element in tsv)
            {
                obs.Add(element.Name);
            }
            return obs;
        }

        /*async void Handle_ItemTapped(object sender, SelectedItemChangedEventArgs e)
        {
            
            if (e.SelectedItem == null)
                return;

            await DisplayAlert("Item Tapped", e.SelectedItem.ToString(), "OK");
            
            //Deselect Item
            ((ListView)sender).SelectedItem = null;
            
        }*/

        async void EnrtyFilter(object sender, TextChangedEventArgs e)
        {   
            this.BindingContext = ob.Where(w => w.Contains(e.NewTextValue));
        }

        async void Hanlde_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            await DisplayAlert("Item added", e.SelectedItem.ToString(), "Ok");

            csnDA.AddNewChosenSynonymsNames(e.SelectedItem.ToString(), 2);

        }

        public void OnMore(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("More Context Action", mi.CommandParameter + " more context action", "OK");
        }

        public void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            //DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");

            //doStuff
            //Debug.WriteLine("delete " + mi.CommandParameter.ToString());
            //items.Remove(mi.CommandParameter.ToString());
        }

    }
}