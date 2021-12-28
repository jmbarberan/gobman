using System;
using System.Linq;

namespace Intelligob.Cliente.Referencia
{
    public partial class SeguimientoDto
    {
        String EntidadDescripcion
        {
            get 
            {
                String s = "N/D";
                if (this.Entidad != null)
                    s =  Utiles.Configuracion.Instance.EntidadPorId((int)this.Entidad);
                return s;
            }
        }
    }
}
