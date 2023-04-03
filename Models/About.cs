using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bloc_de_notas_app.Models
{
    internal class About
    {
        public string Tittle => AppInfo.Name;
        public string Version => AppInfo.VersionString;
        public string MoreInfoUrl => "https://learn.microsoft.com/es-es/dotnet/maui/what-is-maui?view=net-maui-7.0";
        public string Messagge => $"Aplicación creada con .NET MAUI\n By Erick Guadalupe Madrigal";
    }
}
