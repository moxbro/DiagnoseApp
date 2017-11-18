using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace DiagnoseApp
{
    class Code : ContentPage
    {
    public ObservableCollection<Synonym> ob;
        public Code()
        {
            TargetSynonymsVisibleDataAccess tsda = new TargetSynonymsVisibleDataAccess();
            IEnumerable<TargetSynonymsVisible> tsvIE = tsda.GetTargetSynonymsVisible();
            
            //test2
            //BindingContext = this;
            //Items = ToObString(tsvIE);
            ob = ToObString(tsvIE);
            ob = ToObString(tsvIE);
            this.BindingContext = ob;
            
            // Create the ListView.
            ListView listView = new ListView
            {
                // Source of data items.
                ItemsSource = ob,

                // Define template for displaying each item.
                // (Argument of DataTemplate constructor is called for 
                //      each item; it must return a Cell derivative.)
                ItemTemplate = new DataTemplate(() =>
                {
                    // Create views with bindings for displaying each property.
                    Label nameLabel = new Label();
                    nameLabel.SetBinding(Label.TextProperty, "Name");

                    Label boolLabel = new Label();
                    boolLabel.SetBinding(Label.TextProperty, "Highlighted");
                    /*
                    BoxView boxView = new BoxView();
                    boxView.SetBinding(BoxView.ColorProperty, "FavoriteColor");
                    */
                    // Return an assembled ViewCell.
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Horizontal,
                            Children =
                                {
                                    //boxView,
                                    new StackLayout
                                    {
                                        VerticalOptions = LayoutOptions.Center,
                                        Spacing = 0,
                                        Children =
                                        {
                                            nameLabel,
                                            boolLabel
                                        }
                                        }
                                }
                        }
                    };
                })
            };

            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    //header,
                    listView
                }
            };
        }
        public ObservableCollection<Synonym> ToObString(IEnumerable<TargetSynonymsVisible> tsv)
        {
            ObservableCollection<Synonym> obs = new ObservableCollection<Synonym>();
            DiseaseSymptomsSynonymsVisibleDataAccess dssvDA = new DiseaseSymptomsSynonymsVisibleDataAccess();
            //IEnumerable<String> tmp = dssvDA.GetSynonymsfromDisease();


            foreach (TargetSynonymsVisible element in tsv)
            {
                
                obs.Add(new Synonym(element.Name, "false"));
            }
            return obs;
        }

        public class Synonym
        {
            public Synonym(string name, String highlighted)
            {
                this.Name = name;
                this.Highlighted = highlighted;
            }

            public string Name {private set; get; }

            public String Highlighted { private set; get; }
        };
    }
}