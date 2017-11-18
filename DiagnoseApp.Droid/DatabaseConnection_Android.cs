using System;
using DiagoseApp.Droid;
using System.IO;
using Xamarin.Forms;
using SQLite;
using DiagnoseApp;
using DiagnoseApp.Droid;

//
//https://msdn.microsoft.com/en-us/magazine/mt736454.aspx
//https://www.techierathore.com/2016/05/sqlite-with-xamarin-forms-step-by-step-guide/
//

[assembly: Dependency(typeof(DatabaseConnection_Android))]
namespace DiagoseApp.Droid
{
    public class DatabaseConnection_Android : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "DbDiagnosis.dbf";
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.
              SpecialFolder.Personal), dbName);
            Console.WriteLine(path);

            // This is where we copy in the prepopulated database
            Console.WriteLine(path);
            if (!File.Exists(path))
            {
                //Es kann auch daran liegen, das im Beispiel das andere SQLite nuget verwendet wird
                var s = Forms.Context.Resources.OpenRawResource(Resource.Raw.DbDiagnosis); // RESOURCE NAME ###
                // create a write stream
                FileStream writeStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                // write to the stream
                ReadWriteStream(s, writeStream);
            }
            return new SQLiteConnection(path);
        }

        /// &lt;summary&gt;
        /// helper method to get the database out of /raw/ and into the user filesystem
        /// &lt;/summary&gt;

        void ReadWriteStream(Stream readStream, Stream writeStream)
        {
            int Length = 256;
            Byte[] buffer = new Byte[Length];
            int bytesRead = readStream.Read(buffer, 0, Length);
            // write the required bytes
            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = readStream.Read(buffer, 0, Length);
            }
            readStream.Close();
            writeStream.Close();
        }


    }
}