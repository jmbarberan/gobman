using Intelligob.Escritorio.Vistas.General;
using Intelligob.Escritorio.Vistas.Interfaces;
using System;
using System.Linq;

namespace Intelligob.Escritorio.ModeloVista
{
    public class VacioVM : BaseMV<IPagina>
    {
        public VacioVM() : this(new Vacio())
        { }

        public VacioVM(IPagina pVista)
            : base(pVista)
        { }
    }
}
