using System;
using System.Linq;

namespace Intelligob.Cliente.Referencia
{
    public partial class ConvenioDto
    {
        public Double Saldo
        {
            get
            {
                Double d = 0;
                if (this.Valor != null)
                {
                    d = (double)this.Valor;
                    if (this.Pagos != null)
                        d = d - (double)this.Pagos;
                }
                return d;
            }
        }
    }
}
