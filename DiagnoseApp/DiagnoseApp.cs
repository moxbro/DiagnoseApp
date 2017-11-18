using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace DiagnoseApp
{
    public class DiagnoseApp : Application
    {
        public DiagnoseApp()
        {
            //ChosenSynonymsNamesDataAccess da = new ChosenSynonymsNamesDataAccess();
            MainPage = new NavigationPage(new MainPage());
            //MainPage = new NavigationPage(new IDatabaseConnectionHelper());
        }
    }
}
