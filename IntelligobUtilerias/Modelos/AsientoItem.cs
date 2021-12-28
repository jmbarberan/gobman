using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelligob.Utilerias.Modelos
{
    public class AsientoItem
    {
        public int Id { get; set; }
        
        public int AsientoId { get; set; }

        public int CuentaId { get; set; }
        
        public int Tipo { get; set; }
        
        public double Debe { get; set; }

        public double Haber { get; set; }

        public int Estado { get; set; }        
    }
}
