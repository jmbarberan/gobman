using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelligob.Escritorio.ModeloVista.Recaudaciones
{
    public class CobroNotaVM : BaseMV<Vistas.Interfaces.IVentanaDialogo> //, System.ComponentModel.IDataErrorInfo
    {
        public CobroNotaVM() : base(new Vistas.Recaudaciones.CobroNota())
        {
            
        }

    }
}
