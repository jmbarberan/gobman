using System;
using System.Linq;

namespace Intelligob.Cliente.Referencia
{
    public partial class PlanillaRubroDto
    {
        public string RubroDen
        {
            get 
            {
                String res = String.Empty;
                if (this.RubroNav != null)
                {
                    return this.RubroNav.Denoninacion; 
                }
                return res;
            }
        }

        private Double rebaja = 0;
        public Double Rebaja
        {
            get { return this.rebaja; }
            set { this.rebaja = value; } 
        }

        public Double Saldo
        {
            get { return Convert.ToDouble(this.Valor - this.Pagos - this.Rebajas); }
        }

        private Double abono = 0;
        public Double Abono
        {
            get { return this.abono; }
            set { this.abono = value; }
        }

        private Double rebajaAbono = 0;
        public Double RebajaAbono
        {
            get { return this.rebajaAbono; }
            set { this.rebajaAbono = value; }
        }

        public Double SaldoAbono
        {
            get { return this.Saldo - this.Rebaja; }
        }

        public Double ValorRedondeado
        {
            get 
            {
                if (this.Valor != null)
                    return Math.Round((Double)this.Valor, 2); 
                return 0;
            }
        }

    }
}
