using System.Collections.Generic;
using System.Linq;

namespace Intelligob.Entidades
{
    public partial class Modelo
    {
        public AguaPotable AguaCuentaFicha(int pCuentaId)
        {
            return this.AguaPotables.Where(a => a.Id == pCuentaId).FirstOrDefault();
        }

        public IEnumerable<AguaServicio> AguaServiciosPorCuenta(int pCuentaId)
        {
            return this.AguaServicios.Where(a => a.Cuenta == pCuentaId);
        }
    }
}
