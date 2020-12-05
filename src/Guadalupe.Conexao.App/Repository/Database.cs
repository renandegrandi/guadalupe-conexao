using Guadalupe.Conexao.App.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.App.Repository
{
    public static class Database
    {
        #region Constantes

        public const string DatabaseFilename = "missao.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;
        #endregion

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }

        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(DatabasePath, Flags);
        });

        public static SQLiteAsyncConnection DB => lazyInitializer.Value;

        public static Task InitializeAsync()
        {
            var tables = new List<Task>();

            if (!DB.TableMappings.Any(m => m.MappedType.Name == typeof(Person).Name))
                tables.Add(DB.CreateTableAsync<Person>());

            if (!DB.TableMappings.Any(m => m.MappedType.Name == typeof(User).Name))
                tables.Add(DB.CreateTableAsync<User>());

            if (!DB.TableMappings.Any(m => m.MappedType.Name == typeof(Notice).Name))
                tables.Add(DB.CreateTableAsync<Notice>());

            return Task.WhenAll(tables);
        }
    }
}
