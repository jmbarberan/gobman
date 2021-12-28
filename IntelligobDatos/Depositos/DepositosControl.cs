using Intelligob.Datos.Contenedores;
using Intelligob.Datos.Referencia1;
using Intelligob.Utiles;
using System;
using System.Linq;

namespace Intelligob.Datos.Depositos
{
    public class DepositosControl : IRepositoryFactory
    {
        private static IRepositoryFactory instance;
        public static IRepositoryFactory Instancia
        {
            get
            {
                if (instance == null)
                {
                    instance = new DepositosControl();
                    instance.Servicio = new ContenedorDatos(new Modelo(new Uri(Configuracion.DireccionServidor)));
                }
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        public IContenedor Servicio
        {
            get;
            set;
        }
    }
}
