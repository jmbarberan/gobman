using System.Configuration;

namespace Intelligob.Utilerias
{
    public static class ConexionDatos
    {
        public static string Servidor 
        {
            get { return ConfigurationManager.AppSettings.Get("Servidor").ToString(); } 
        }

        public static string Usuario 
        {
            get { return ConfigurationManager.AppSettings.Get("Usuario").ToString(); }
        }

        public static string Basedatos 
        {
            get { return ConfigurationManager.AppSettings.Get("BaseDatos").ToString(); }  
        }

        public static string Clave 
        { 
            get
            {
                string encriptada = ConfigurationManager.AppSettings.Get("Clave").ToString();
                return Cifrador.RijndaelSimple.Desencriptar(encriptada);
            }
        }

        public static string Opciones 
        {
            get { return ConfigurationManager.AppSettings.Get("Opciones").ToString(); } 
        }

        public static string Proveedor 
        {
            get { return ConfigurationManager.AppSettings.Get("Proveedor").ToString(); } 
        }

        public static string CadenaConexion
        {
            get
            {
                return Opciones + "data source=" + Servidor + ";initial catalog=" + Basedatos + ";user id=" + Usuario + ";password=" + Clave;
            }
        }
    }
}
