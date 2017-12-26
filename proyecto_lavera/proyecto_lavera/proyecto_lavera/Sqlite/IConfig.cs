using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_lavera.Sqlite
{
   public interface IConfig
    {
        String DirectorioDB { get; }
        ISQLitePlatform Plataforma { get; }
    }
}
