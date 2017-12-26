using SQLite.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace proyecto_lavera.Sqlite
{
    class DataAccess : IDisposable
    {
        private SQLiteConnection connection;
        public DataAccess()
        {
            var config = DependencyService.Get<IConfig>();
            connection = new SQLiteConnection(config.Plataforma, Path.Combine(config.DirectorioDB, "Usuario.db3"));
            connection.CreateTable<Usuarios>();
        }
        public void InsertUsuario(Usuarios user)
        {
            connection.Insert(user);
        }
        public void UpdateUsuario(Usuarios user)
        {
            connection.Update(user);
        }
        public Usuarios GetUsuario()
        {
            return connection.Table<Usuarios>().FirstOrDefault();
        }

        public int getCountUsuario()
        {
            return connection.Table<Usuarios>().Count();
        }
        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
