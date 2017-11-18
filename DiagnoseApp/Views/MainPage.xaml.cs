using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiagnoseApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        //DatabaseConnectionHelper ConnectionHelper = new DatabaseConnectionHelper();
        ChosenSynonymsNamesDataAccess csnDa = new ChosenSynonymsNamesDataAccess();
        TargetSynonymsVisibleDataAccess tsvDa = new TargetSynonymsVisibleDataAccess();
        ChosenSynonymsNamesVisibleDataAccess csnvDA = new ChosenSynonymsNamesVisibleDataAccess();
        SynonymsDataAccess sDa = new SynonymsDataAccess();


        Label countLabel, dataLabel, TargetSynonymsLabel;
        Entry ageEntry;
        Switch sex;

        public MainPage()
        {
            BackgroundColor = Color.White;

            ageEntry = new Entry
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Start,
                Keyboard = Keyboard.Numeric
            };


            sex = new Switch
            {
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.End
            };

            Button ViewObjects = new Button
            {
                Text = "Count",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                //HorizontalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.FromHex("428BCA"),
                BorderRadius = 0
            };
            ViewObjects.Clicked += OnViewObjectsClicked;

            Button AddObject = new Button
            {
                Text = "Add",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.FromHex("428BCA")
            };
            AddObject.Clicked += OnAddObjectClicked;

            Button ShowSymptoms = new Button
            {
                Text = "ShowSymptoms",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.FromHex("428BCA")
            };
            ShowSymptoms.Clicked += OnShowSymptoms;

            Button ShowChosenSymptoms = new Button
            {
                Text = "ShowChosenSymptoms",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.FromHex("428BCA")
            };
            ShowChosenSymptoms.Clicked += OnShowChosenSymptoms;

            Button DeleteCSN = new Button
            {
                Text = "DeleteCSN",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                TextColor = Color.Red,
                BackgroundColor = Color.FromHex("428BCA")
            };
            DeleteCSN.Clicked += OnDeleteCSN;

            Button ShowDesises = new Button
            {
                Text = "ShowDesises",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.FromHex("428BCA")
            };
            ShowDesises.Clicked += OnShowDesises;

            countLabel = new Label
            {
                Text = "null",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,             
            };

            dataLabel = new Label
            {
                Text = "null",
                //Font = Font.SystemFontOfSize(NamedSize.Large),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };

            TargetSynonymsLabel = new Label
            {
                Text = "null",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };


            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    //Buttons
                    ViewObjects,
                    ageEntry,
                    sex,
                    AddObject,
                    DeleteCSN,
                    ShowSymptoms,
                    ShowChosenSymptoms,
                    ShowDesises,
                    //Labels
                    countLabel,
                    dataLabel,
                    TargetSynonymsLabel
                }
            };
        }

        void OnViewObjectsClicked(object sender, EventArgs e)
        {
            IEnumerable<ChosenSynonymsNames> scn = csnDa.GetChosenSynonymsNames();
            IEnumerable<TargetSynonymsVisible> tsv = tsvDa.GetTargetSynonymsVisible();
            IEnumerable<Synonyms> s = sDa.GetSynonyms();
            countLabel.Text = "csn: " + scn.Count().ToString()
                            + " tsv: " + tsv.Count().ToString()
                            + " s: " + s.Count().ToString();

            if (scn.Any())
            {
                dataLabel.Text = scn.First().Name;
            }
            else
            {
                dataLabel.Text = "null";
            }

            TargetSynonymsLabel.Text = tsv.First().Name;
        }

        void OnAddObjectClicked(object sender, EventArgs e)
        {
            int age;

            Int32.TryParse(ageEntry.Text, out age);

            System.Diagnostics.Debug.WriteLine(age);

            if (sex.IsToggled)
                csnDa.InitialiseAgeSex("male", age);
            else
                csnDa.InitialiseAgeSex("female", age);

            //OnViewObjectsClicked();
        }

        void OnDeleteCSN(object sender, EventArgs e)
        {
            csnvDA.DeleteAllChosenSynonymsNamesVisible();
        }

        async void OnShowDesises(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ShowDesisesListViewPage());
        }

        async void OnShowChosenSymptoms(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChosenSymtomsListViewPage());
        }

        async void OnShowSymptoms(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new SymtomsListViewPage());
            await Navigation.PushAsync(new Code());
        }
    }
}