using System;
using System.Collections.Generic;
using System.Linq;
using Intelligob.Cliente.Referencia;

namespace Intelligob.Cliente.Depositos
{
    public class ConceptosDep : DepositoBase
    {
        public ConceptosDep() : base(DepositosControl.Instance.Servicio) { }
        public ConceptosDep(IEntidades servicio) : base(servicio) {}

        public IEnumerable<ConceptoDto> ConceptosPorPatentes()
        {
            return Servicio.ReadConceptos().Where(c => c.Id >= 3 && c.Id <= 5);
        }

        /// <summary>
        /// Traer todos los conceptos
        /// </summary>
        /// <param name="pEstado">Estado del concepto (9 = todos)</param>
        /// <returns></returns>
        public IEnumerable<ConceptoDto> ConceptosPorEstado(int pEstado)
        {
            if (pEstado == 9)
            {
                return Servicio.ReadConceptos();
            }
            else
            {
                return Servicio.ReadConceptos()
                    .Where(c => c.Estado == pEstado)
                    .OrderBy(c => c.Periodo);
            }
        }

        /// <summary>
        /// Traer conceptos cuyo nombre coincide con el parametro
        /// </summary>
        /// <param name="pNombre">Nombre del concepto o su aproximacion</param>
        /// <returns></returns>
        public IEnumerable<ConceptoDto> ConceptosPorNombre(String pNombre)
        {
            return Servicio.ReadConceptos()
                .Where(c => c.Estado == 0 && c.Denominacion.Contains(pNombre))
                .OrderBy(c => c.Denominacion);
        }

        /// <summary>
        /// Traer un concepto por su Id
        /// </summary>
        /// <param name="pId">El id del concepto buscado</param>
        /// <returns></returns>
        public ConceptoDto ConceptoPorId(int pId)
        {
            return Servicio.ReadConcepto(String.Format(this.FormatoClave, pId));
        }
    
        /// <summary>
        /// Traer conceptos para carpeta catastral
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ConceptoDto> ConceptosPorCarpetaCatastral()
        {
            return ModeloCache.Instance.McConceptos.Where(c => c.Estado == 0 && c.Periodo == 1 && c.MenuEmisiones > 0);
        }

        public IEnumerable<ConceptoDto> ConceptosAnualesTodos()
        {
            List<ConceptoDto> res = this.ConceptosPorCarpetaCatastral().ToList();
            ConceptoDto c = new ConceptoDto();
            c.Id = 0;
            c.Denominacion = "CONSULTAR TODOS";
            res.Add(c);
            return res;
        }

        public IEnumerable<ConceptoDto> ConceptosParaEmision()
        {
            SeguridadDep segDep = new SeguridadDep();
            List<ConceptoDto> res = new List<ConceptoDto>();
            IEnumerable<ConceptoDto> lc = this.Servicio.ReadConceptosFiltered("denominacion", "menuEmisiones > 0");
            foreach(ConceptoDto c in lc)
            {
                int fun = -1;
                if (c.MenuEmisiones != null)
                    fun = (int)c.MenuEmisiones;
                if (SesionUtiles.Instance.EsDesarrollador || segDep.PrivilegiosFuncionPorUsuario(fun, SesionUtiles.Instance.UsuarioActivo.Id) != null)
                {
                    IEnumerable<ConceptosEmisionDto> ps = this.Servicio.ReadConceptosEmisionsFiltered("indice", String.Format("concepto = {0}", c.Id));
                    foreach (ConceptosEmisionDto p in ps)
                    {
                        // Obtener el valor por defecto de origen DIGITADO                    
                        if (p.Origen == 1)
                        {
                            switch (p.OrigenTipo)
                            {
                                case 1:
                                    {
                                        break;
                                    }
                                case 2:
                                    {
                                        TablaClaveDto cve = this.Servicio.ReadTablaClave(String.Format(FormatoClave, p.Referencia)); //Intelligob.Cliente.ModeloCache.Instance.McClaves.Where(t => t.Id == p.Referencia).FirstOrDefault();
                                        p.Identificador = (int)cve.Superior;
                                        p.Valor = (double)cve.Valor;
                                        break;
                                    }
                                case 3:
                                    {
                                        break;
                                    }
                            }
                        }
                        else
                        {
                            p.SeleccionadoPresentacion = "Seleccionar";
                        }
                    }
                    c.EmisionParametros = ps.ToArray();
                    res.Add(c);
                }
                
            }
            return res;
        }
    
        public IEnumerable<ConceptosDocumentoDto> DocumentosPorConceptoUnicos()
        {
            return this.Servicio.DocumentosPorConceptoUnico();
        }

        /// <summary>
        /// Traer Documentos habilitantes por concepto
        /// </summary>
        /// <param name="pConcepto">Concepto a consultar</param>
        /// <param name="pEstado">Estado del registro (9 = todos)</param>
        /// <returns></returns>
        public IEnumerable<ConceptosDocumentoDto> DocumentosPorConceptoEstado(int pConcepto, int pEstado)
        {
            if (pEstado == 9)
                return this.Servicio.ReadConceptosDocumentosFiltered("", String.Format("concepto = {0}", pConcepto));
            else
                return this.Servicio.ReadConceptosDocumentosFiltered("", String.Format("concepto = {0} and estado = {1}", pConcepto, pEstado));
        }
    
        /// <summary>
        /// Marcar un concepto de emision periodica como emitiendo
        /// </summary>
        /// <param name="pConcepto">Concepto</param>
        /// <param name="pEmitiendo">Estado de emision</param>
        public void MarcarEmitiendoPorId(int pConcepto, bool pEmitiendo)
        {
            ConceptoDto c = this.ConceptoPorId(pConcepto);
            if (pEmitiendo)
                c.Estado = 8;
            else
                c.Estado = 0;
            this.Servicio.UpdateConcepto(c);
        }
    }
}
