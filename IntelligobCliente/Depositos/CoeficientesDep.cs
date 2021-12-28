using System;
using System.Collections.Generic;
using System.Linq;
using Intelligob.Cliente.Referencia;

namespace Intelligob.Cliente.Depositos
{
    public class CoeficientesDep : DepositoBase
    {
        public CoeficientesDep() : base(DepositosControl.Instance.Servicio) { }
        public CoeficientesDep(IEntidades servicio) : base(servicio) { }

        public virtual CoeficienteDto CoeficientePorId(int pId)
        {
            return this.Servicio.ReadCoeficientes().Where(c => c.Id == pId).FirstOrDefault();
        }

        /// <summary>
        /// Traer lista de coeficientes por estado
        /// </summary>
        /// <param name="pEstado">Estado del registro (9 = TODOS)</param>
        /// <returns></returns>
        public virtual IEnumerable<CoeficienteDto> CoeficientesPorEstado(int pEstado)
        {
            if (pEstado == 9)
                return this.Servicio.ReadCoeficientes();
            else
                return this.Servicio.ReadCoeficientes().Where(c => c.Estado == 0);
        }

        public virtual CoeficienteElementoDto CoeficienteElementoPorId(int pId)
        {
            return this.Servicio.ReadCoeficienteElementos().Where(e => e.Id == pId).FirstOrDefault();
        }

        public virtual IEnumerable<CoeficienteElementoDto> CoeficienteElementosPorTipo(int pTipo)
        {
            return this.CoeficienteElementosPorTipoEstado(pTipo, 0);
        }

        /// <summary>
        /// Traer elementos de coeficiente por tipo y estado del registro
        /// </summary>
        /// <param name="pTipo">Tipo de coeficiente</param>
        /// <param name="pEstado">Estado del registro (9 = TODOS)</param>
        /// <returns>Lista de elementos de coeficiente</returns>
        public virtual IEnumerable<CoeficienteElementoDto> CoeficienteElementosPorTipoEstado(int pTipo, int pEstado)
        {
            if (pEstado == 9)
                return this.Servicio.ReadCoeficienteElementosFiltered("", String.Format("coeficiente = {0}", pTipo));
            else
                return this.Servicio.ReadCoeficienteElementosFiltered("", String.Format("estado = {0} and coeficiente = {1}", pEstado, pTipo));
        }

        /// <summary>
        /// Traer elementos de coeficiente por tipo y estado del registro ordenado por campo
        /// </summary>
        /// <param name="pTipo">Tipo de coeficiente</param>
        /// <param name="pEstado">Estado del registro (9 = TODOS)</param>
        /// <param name="pOrden">Orden de los registros</param>
        /// <returns></returns>
        public virtual IEnumerable<CoeficienteElementoDto> CoeficienteElementosPorTipoEstadoOrden(int pTipo, int pEstado, string pOrden)
        {
            if (pEstado == 9)
                return this.Servicio.ReadCoeficienteElementosFiltered(pOrden, String.Format("coeficiente = {0}", pTipo));
            else
                return this.Servicio.ReadCoeficienteElementosFiltered(pOrden, String.Format("estado = {0} and coeficiente = {1}", pEstado, pTipo));
        }

        /// <summary>
        /// Traer el elemento de una tabla de coeficiente por tipo y clave
        /// </summary>
        /// <param name="pTipo">Tipo de coeficiente</param>
        /// <param name="pClave">Clave del elemento</param>
        /// <returns></returns>
        public virtual CoeficienteElementoDto CoeficienteElementoPorClave(int pTipo, int pClave)
        {
            return this.Servicio.ReadCoeficienteElementos().Where(c => c.Coeficiente == pTipo && c.Clave == pClave).FirstOrDefault();
        }

        /// <summary>
        /// Traer todos los elementos de coeficiente para el cache
        /// </summary>
        /// <param name="pEstado"></param>
        /// <returns></returns>
        public virtual IEnumerable<CoeficienteElementoDto> CoeficienteElementosPorEstado(int pEstado)
        {
            if (pEstado == 9)
                return this.Servicio.ReadCoeficienteElementos();
            else
                return this.Servicio.ReadCoeficienteElementosFiltered("", String.Format("estado = {0}", pEstado));
        }
    
        /// <summary>
        /// Guardar los cambios efectuados en los elementos recibidos
        /// </summary>
        /// <param name="pElementos">Elementos de coeficiente a guardar</param>
        public virtual void CoeficienteElementosGuardar(IEnumerable<CoeficienteElementoDto> pElementos)
        {
            foreach(CoeficienteElementoDto ce in pElementos)
            {
                if (ce.Id <= 0)
                    this.Servicio.CreateCoeficienteElemento(ce);
                else
                    this.Servicio.UpdateCoeficienteElemento(ce);
            }
            ModeloCache.Instance.ResetTablaCache("mcCoeficienteElementos");
        }
    }
}
