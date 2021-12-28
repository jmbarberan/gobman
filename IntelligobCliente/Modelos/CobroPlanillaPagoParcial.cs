using System;
using System.Linq;

namespace Intelligob.Cliente.Modelos
{
    public class CobroPlanillaPagoParcial
    {
        public Intelligob.Cliente.Referencia.PlanillaDto Planilla { get; set; }

        public Intelligob.Cliente.Referencia.CobroTransaccionDto Cobro { get; set; }

        public Double SaldoAnterior
        {
            get
            {
                Double cobro = 0;
                Double rebaja = 0;
                if (Cobro != null)
                {
                    if (Cobro.Valor != null)
                        cobro = (Double)Cobro.Valor;
                    if (Cobro.Rebajas != null)
                        rebaja = (Double)Cobro.Rebajas;
                }
                
                return Planilla.Saldo + cobro + rebaja;
            }
        }

        public String ContribuyentesCadena
        {
            get { return this.Planilla.ContribuyentesCadena; }
            
        }

        public Double Saldo
        {
            get { return this.Planilla.Saldo; }
        }
    }
}
