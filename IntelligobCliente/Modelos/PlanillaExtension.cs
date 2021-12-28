using System;
using System.Linq;

namespace Intelligob.Cliente.Referencia
{
    public partial class PlanillaDto
    {
        public string ContribuyentesCadena
        { get; set; }

        public double Saldo
        {
            get 
            {
                double mtotal = 0;
                double mpagos = 0;
                double mrecargos = 0;
                double mrebajas = 0;
                double mconvenios = 0;
                if (this.Total != null)
                    mtotal = (double)this.Total;
                if (this.Pagos != null)
                    mpagos = (double)this.Pagos;
                if (this.Rebajas != null)
                    mrebajas = (double)this.Rebajas;
                if (this.Recargos != null)
                    mrecargos = (double)this.Recargos;
                if (this.Convenios != null)
                    mconvenios = (double)this.Convenios;
                return mtotal + mrecargos - (mpagos + mrebajas + mconvenios);
            }
        }

        public double Parcial
        {
            get;
            set;
        }

        public string Beneficios
        { get; set; }

        public string CodigoExtendigo
        {            
            get 
            {
                String res = this.Codigo;
                if (this.Concepto <= 2)
                    res = ModeloCache.Instance.McClaves.Where(t => t.Tabla == 2 && t.Clave == 7).FirstOrDefault().Denominacion.Trim() + '-' + this.Codigo.Trim();
                return res;
            }
        }
    }
}
