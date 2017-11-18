using SQLite;


using Xamarin.Forms;
//
//https://msdn.microsoft.com/de-de/magazine/mt736454.aspx
//
namespace DiagnoseApp
{
    //Hier wird ein Interface implementiert, 
    //welches platformspezifisch die Verbindung zu Datenbank herstellt
    public interface IDatabaseConnection
    {
        SQLiteConnection DbConnection();

    }
}