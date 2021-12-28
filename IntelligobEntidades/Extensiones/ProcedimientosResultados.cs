using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelligob.Entidades
{
    public partial class RepResumenEmisionesPerItem
    {
        private String concepto;
        private String rubro;
        private Double valor;
        private int propiedad;

        public String Concepto
        { get { return concepto; } set { this.concepto = value; } }

        public String Rubro
        { get { return rubro; } set { rubro = value; } }

        public Double Valor
        { get { return valor; } set { valor = value; } }

        public int Propiedad
        { get { return propiedad; } set { propiedad = value; } }
    }
}
