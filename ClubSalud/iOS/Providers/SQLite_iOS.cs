using System;
using System.IO;
using SQLite;
using Xamarin.Forms;
using ClubSalud.iOS.Providers;
using ClubSalud.Providers;

[assembly: Dependency(typeof(SQLite_iOS))]
namespace ClubSalud.iOS.Providers
{
    class SQLite_iOS : ISQLite
    {
        public SQLite_iOS()
        {
        }

        #region ISQLite implementation

        public SQLiteConnection GetConnection()
        {
            string dbName = "ClubSalud.db3";
            string physicRoute = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "..", "Library", dbName);
            return new SQLiteConnection(physicRoute);
        }

        #endregion
    }
}
