using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelligob.Utilerias.Modelos
{
    public class Asiento
    {
        public int Id { get; set; }
        
        public int Tipo { get; set; }

        public int Numero { get; set; }

        public DateTime Fecha { get; set; }

        public String Descripcion { get; set; }

        public int Origen { get; set; }

        public int Referencia { get; set; }

        public double Debe { get; set; }

        public double Haber { get; set; }

        public int Estado { get; set; }
    }
}
