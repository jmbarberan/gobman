using Intelligob.Escritorio.Vistas.Contabilidad;
using Intelligob.Escritorio.Vistas.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelligob.Escritorio.ModeloVista.Contabilidad
{
    public class DiarioMV : BaseMV<IPagina>
    {
        public DiarioMV() : base(new Diario()) { }


    }
}
