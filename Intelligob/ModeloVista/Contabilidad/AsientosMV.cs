using Intelligob.Escritorio.Vistas.Contabilidad;
using Intelligob.Escritorio.Vistas.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelligob.Escritorio.ModeloVista.Contabilidad
{
    public class AsientosMV : BaseMV<IPagina>
    {
        public AsientosMV() : base(new Asientos())  { }
    }
}
