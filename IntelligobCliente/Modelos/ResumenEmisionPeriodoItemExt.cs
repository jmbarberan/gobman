using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelligob.Cliente.Referencia
{
    public partial class ResumenEmisionesPeriodoItem
    {
        public Double Propio
        {
            get
            {
                if (propiedad == 0 && valor != null)
                    return (Double)valor;
                return 0;
            }
        }

        public Double Ajeno
        {
            get
            {
                if (propiedad == 1 && valor != null)
                    return (Double)valor;
                return 0;
            }
        }

        public Double Suma
        {
            get
            {
                return Propio + Ajeno;
            }
        }
    }
}
