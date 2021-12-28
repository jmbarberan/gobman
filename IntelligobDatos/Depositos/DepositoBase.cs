using System;
using Intelligob.Datos.Contenedores;

namespace Intelligob.Datos.Depositos
{
    public abstract class DepositoBase : IDisposable
    {
        public IContenedor Servicio
        {
            get;
            private set;
        }

        public DepositoBase(IContenedor pServicio)
        {
            this.Servicio = pServicio;
        }

        public void Guardar()
        {
            this.Servicio.Guardar();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
