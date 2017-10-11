using ClubSalud.Droid.Providers;
using ClubSalud.Providers;
using SQLite;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(SQLite_Android))]
namespace ClubSalud.Droid.Providers
{
    public class SQLite_Android : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            string dbName = "ClubSalud.db3";
            string physicRoute = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);
            return new SQLiteConnection(physicRoute);
        }
    }
}