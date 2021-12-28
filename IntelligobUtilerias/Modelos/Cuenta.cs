using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelligob.Utilerias.Modelos
{
    public class Cuenta
    {
        public int Id { get; set; }

        public string Codigo { get; set; }

        public String Denominacion { get; set; }

        public int Tipo { get; set; }

        public int Origen { get; set; }

        public int Titulo { get; set; }

        public int Naturaleza { get; set; }

        public int Subgrupo { get; set; }

        public int Nivel1 { get; set; }

        public int Nivel2 { get; set; }

        public int Nivel3 { get; set; }

        public int Nivel4 { get; set; }

        public int Nivel { get; set; }

        public int Estado { get; set; }

    }
}
