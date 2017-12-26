using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_lavera.Sqlite
{
    class Usuarios
    {
        public Usuarios(String nombre,String email,int id_user)
        {
            this.Name = nombre;
            this.Email = email;
            this.Id_User = id_user;
        }
        public Usuarios()
        {
           
        }
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Id_User { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
    }
}
