using Intelligob.Datos.Contenedores;
using Intelligob.Datos.Referencia1;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intelligob.Datos.Depositos
{
    public class CoeficientesDep : DepositoBase
    {
        public CoeficientesDep() : base(DepositosControl.Instancia.Servicio) { }

        public CoeficientesDep(IContenedor pDatos) : base(pDatos) { }

        public Coeficiente CoeficientePorId(int pId)
        {
            return Servicio.QCoeficientes.Where(c => c.Id == pId).FirstOrDefault();
        }

        public List<Coeficiente> CoeficientesTodos()
        {
            return Servicio.QCoeficientes.Where(c => c.Estado == 0).ToList();
        }

        public CoeficienteElemento CoeficienteElementoPorId(int pId)
        {
            return Servicio.QCoeficienteElementos.Where(e => e.Id == pId).FirstOrDefault();
        }

        public List<CoeficienteElemento> CoeficienteElementosPorTipo(int pTipo)
        {
            return Servicio.QCoeficienteElementos.Where(e => e.Estado == 0 && e.Coeficiente == pTipo).ToList();
        }

    }
}
