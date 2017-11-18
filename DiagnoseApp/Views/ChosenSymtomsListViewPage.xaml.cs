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
    public partial class ChosenSymtomsListViewPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }
        public object Console { get; private set; }
        ChosenSynonymsNamesVisibleDataAccess csnvDA;

        public ChosenSymtomsListViewPage()
        {
            InitializeComponent();
            csnvDA = new ChosenSynonymsNamesVisibleDataAccess();
            
            IEnumerable<ChosenSynonymsNamesVisible> csnvIE
                = csnvDA.GetChosenSynonymsNamesVisible();

            this.BindingContext = ToObString(csnvIE);
            System.Diagnostics.Debug.WriteLine(csnvIE.ToString());

        }
        
        public ObservableCollection<String> ToObString(IEnumerable<ChosenSynonymsNamesVisible> csnv)
        {
            ObservableCollection<String> obs = new ObservableCollection<string>();

            foreach(ChosenSynonymsNamesVisible element in csnv)
            {
                obs.Add(element.Name);
            }

            return obs;
        }


    async void Hanlde_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            await DisplayAlert("Item added", e.SelectedItem.ToString(), "Ok");

            csnvDA.DeleteChosenSynonymsNamesVisible(e.SelectedItem.ToString());

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