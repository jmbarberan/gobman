using Intelligob.Datos.Contenedores;
using Intelligob.Datos.Referencia1;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intelligob.Datos.Depositos
{
    public class ConceptosDep: DepositoBase
    {
        public ConceptosDep() : base(DepositosControl.Instancia.Servicio) { }

        public ConceptosDep(IContenedor pControl) : base(pControl) { }

        public List<Concepto> TraerConceptosPatentes()
        {
            return Servicio.QConceptos.Where(c => c.Id >= 3 && c.Id <= 5).ToList();
        }

        /// <summary>
        /// Traer todos los conceptos
        /// </summary>
        /// <param name="pEstado">Estado del concepto (9 = todos)</param>
        /// <returns></returns>
        public List<Concepto> TraerConceptosTodos(int pEstado)
        {
            if (pEstado == 9)
            {
                return Servicio.QConceptos.ToList();
            }
            else
            {
                return Servicio.QConceptos
                    .Where(c => c.Estado == pEstado)
                    .OrderBy(c => c.Periodo)
                    .ToList();
            }
        }

        /// <summary>
        /// Traer conceptos cuyo nombre coincide con el parametro
        /// </summary>
        /// <param name="pNombre">Nombre del concepto o su aproximacion</param>
        /// <returns></returns>
        public List<Concepto> TraerConceptosPorNombre(String pNombre)
        {
            return Servicio.QConceptos
                .Where(c => c.Estado == 0 && c.Denominacion.Contains(pNombre))
                .OrderBy(c => c.Denominacion)
                .ToList();
        }

        /// <summary>
        /// Traer un concepto por us Id
        /// </summary>
        /// <param name="pId">El id del concepto buscado</param>
        /// <returns></returns>
        public Concepto TraerConceptoPorId(int pId)
        {
            Concepto ret = null;
            try
            {
                ret = Servicio.QConceptos.Where(c => c.Id == pId).FirstOrDefault();
            }
            catch //(Exception e)
            {
                ret = null;
            }
            return ret;
        }
    }
}
