using System;
using System.Linq;

namespace Intelligob.Reportes.Emisiones
{
    public partial class CarpetaCatastralAnual
    {
        public const Boolean EsInforme = true;

        public const int Modulo = 5;

        public const int Funcion = 26;

        public const String Nombre = "Carpeta catastral anual";

        public const String Alternativos = "Carpeta catastral por codigo;Intelligob.Reportes.Emisiones.CarpetaCatastralCodigo, IntelligobReportes, Version=3.0.0.0, Culture=neutral, PublicKeyToken=null" + "@" +
                                          "Carpeta catastral por nombres;Intelligob.Reportes.Emisiones.CarpetaCatastralAnual, IntelligobReportes, Version=3.0.0.0, Culture=neutral, PublicKeyToken=null";
    }
}
