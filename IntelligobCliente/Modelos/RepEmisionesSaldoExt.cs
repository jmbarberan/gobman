using System;
using System.Linq;

namespace Intelligob.Cliente.Referencia
{
    public partial class EmisionesSaldosItem
    {
        public Double origen
        {
            get 
            {
                int i = -1;
                if (this.estado.Substring(0, 1) == "1")
                    i = 1;
                return i;
            }
        }

        public Double ValorResumen
        {
            get 
            {
                Double d = 0;
                if (this.valor != null)
                    d = (Double)this.valor * this.origen;
                return d;
            }
        }
    }
}
