using Intelligob.Cliente.Referencia;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intelligob.Cliente.Depositos
{
    public class RepRecaudacionesDep : DepositoBase
    {
        public RepRecaudacionesDep() : base(DepositosControl.Instance.Servicio) { }

        public RepRecaudacionesDep(IEntidades servicio) : base(servicio) { }

        public IEnumerable<RepRecaudacionesFechaDto> ReporteRecaudacionesFecha(DateTime pFechaInicio, DateTime pFechaCorte)
        {
            return this.Servicio.ReadRepRecaudacionesFechasFiltered("fecha", "estado = 0 and fecha >= DateTime.Parse(\"" + pFechaInicio.ToString() + "\") and fecha <= DateTime.Parse(\"" + pFechaCorte.ToString() + "\")");         
        }

        public IEnumerable<RepRecaudacionesFechaDto> ReporteRecaudacionTicketsFecha(DateTime pFechaInicio, DateTime pFechaCorte)
        {
            return this.Servicio.ReadRepRecaudacionesFechasFiltered("fecha", "estado = 0 and formaPago = 1 and fecha >= DateTime.Parse(\"" + pFechaInicio.ToString() + "\") and fecha <= DateTime.Parse(\"" + pFechaCorte.ToString() + "\")");
        }

        public IEnumerable<RepRecaudacionesFechaDto> ReporteRecaudacionTicketsLocal(int pLocal, DateTime pFechaInicio, DateTime pFechaCorte)
        {
            if (pLocal > 0)
            {
                MercadoDto m = Servicio.ReadMercadosFiltered("", string.Format("puesto = {0}", pLocal)).FirstOrDefault();
                if (m != null)
                    return Servicio.ReadRepRecaudacionesFechasFiltered("fecha", "estado = -1 and rebajas = " + m.Id.ToString() + " and formaPago = 1 and fecha >= DateTime.Parse(\"" + String.Format("{0:yyyy-MM-dd}", pFechaInicio) + "\") and fecha <= DateTime.Parse(\"" + String.Format("{0:yyyy-MM-dd}", pFechaCorte) + "\")");
                else
                    return Servicio.ReadRepRecaudacionesFechasFiltered("", "estado = -1 and formaPago = 1 and fecha >= DateTime.Parse(\"" + String.Format("{0:yyyy-MM-dd}", pFechaInicio) + "\") and fecha <= DateTime.Parse(\"" + String.Format("{0:yyyy-MM-dd}", pFechaCorte) + "\")");
            }
            else
            {
                return Servicio.ReadRepRecaudacionesFechasFiltered("", "estado = -1 and formaPago = 1 and fecha >= DateTime.Parse(\"" + String.Format("{0:yyyy-MM-dd}", pFechaInicio) + "\") and fecha <= DateTime.Parse(\"" + String.Format("{0:yyyy-MM-dd}", pFechaCorte) + "\")");
            }    
        }

        public IEnumerable<RepRecaudacionesFechaDto> ReporteRecaudacionTicketsNumero(DateTime pFechaInicio, DateTime pFechaCorte)
        {
            return this.Servicio.ReadRepRecaudacionesFechasFiltered("soporteNumero", "estado = -1 and formaPago = 1 and fecha >= DateTime.Parse(\"" + pFechaInicio.ToString() + "\") and fecha <= DateTime.Parse(\"" + pFechaCorte.ToString() + "\")");
        }

        public IEnumerable<RepRecaudacionesFechaDto> ReporteRecaudacionesComponentes(DateTime pFechaInicio, DateTime pFechaCorte)
        {
            return this.Servicio.ReadRepRecaudacionesFechasFiltered("conceptoden", "estado = 0 and fecha >= DateTime.Parse(\"" + pFechaInicio.ToString() + "\") and fecha <= DateTime.Parse(\"" + pFechaCorte.ToString() + "\")");
        }

        public IEnumerable<RepRecaudacionesFechaDto> ReporteRecaudacionesDetallado(String pOrden, DateTime pFechaInicio, DateTime pFechaCorte)
        {
            return this.Servicio.ReadRepRecaudacionesFechasFiltered(pOrden, "estado = 0 and fecha >= DateTime.Parse(\"" + pFechaInicio.ToString() + "\") and fecha <= DateTime.Parse(\"" + pFechaCorte.ToString() + "\")");
        }

        public IEnumerable<RepRecaudacionesCompDetalleDto> ReporteRecaudaComponentesDetallado(DateTime pFechaInicio, DateTime pFechaCorte, int pConcepto, int pEstado)
        {
            return this.Servicio.ReadRepRecaudacionesCompDetallesFiltered("", "estado = " + pEstado.ToString() + " and conceptoid = " + pConcepto.ToString() + " and fecha >= DateTime.Parse(\"" + pFechaInicio.ToString() + "\") and fecha <= DateTime.Parse(\"" + pFechaCorte.ToString() + "\")");
        }

        public IEnumerable<RepRecaudacionesCompDetalleDto> ReporteBajasComponentesDetallado(DateTime pFechaInicio, DateTime pFechaCorte, int pConcepto)
        {
            return this.Servicio.ReadRepRecaudacionesCompDetallesFiltered("", "estado = 2 and conceptoid = " + pConcepto.ToString() + " and fecha >= DateTime.Parse(\"" + pFechaInicio.ToString() + "\") and fecha <= DateTime.Parse(\"" + pFechaCorte.ToString() + "\")");
        }

        public IEnumerable<ReporteDto> ReportesPorModuloEstado(int pModulo, int pEstado)
        {
            IEnumerable<ReporteDto> res;
            if (pEstado == 9)
            {
                if (pModulo > 0)
                    res = this.Servicio.ReadReportesFiltered("denominacion", String.Format("modulo = {0}", pModulo));
                else
                    return this.Servicio.ReadReportes().OrderBy(r => r.Denominacion);
            }
            else
            {
                if (pModulo > 0)
                    res = this.Servicio.ReadReportesFiltered("denominacion", String.Format("modulo = {0} and estado = {1}", pModulo, pEstado));
                else
                    res = this.Servicio.ReadReportesFiltered("denominacion", String.Format("estado = {0}", pEstado));
            }
            foreach (Cliente.Referencia.ReporteDto r in res)
            {
                r.ModuloNav = this.Servicio.ReadModulo(String.Format(this.FormatoClave, r.Modulo));
            }
            return res;
        }

        public int CrearReporte(ReporteDto rep)
        {
            int i = 0;
            string s = this.Servicio.CreateReporte(rep);
            try
            {
                s = s.Replace("Id=", "");
                i = Convert.ToInt32(s);
            }
            catch
            {
                i = -1;
            }
            return i;
        }

        public void ModificarReporte(ReporteDto rep)
        {
            this.Servicio.UpdateReporte(rep);
        }

        public ReporteDto ReportePorNombre(String pNombre)
        {
            return this.Servicio.ReadReportesFiltered("", String.Format("denominacion = {0}", pNombre)).FirstOrDefault();
        }        

        public ReporteDto ReportePorId(int pId)
        {
            return this.Servicio.ReadReporte(String.Format(this.FormatoClave, pId));
        }

        public IEnumerable<RepCuentaCorrienteResumenDto> CuentaCorrienteResumen(string pOrden)
        {
            switch (pOrden)
            {
                case "deuda":
                    {
                        return this.Servicio.ReadRepCuentaCorrienteResumens().OrderByDescending(o => o.Deuda);
                    }
                case "titulos":
                    {
                        return this.Servicio.ReadRepCuentaCorrienteResumens().OrderByDescending(o => o.Conteo);
                    }
                default:
                    {
                        return this.Servicio.ReadRepCuentaCorrienteResumens().OrderBy(o => o.Nombres);
                    }
            }                
        }        
        
        public IEnumerable<RepCuentaCorrienteDetalleDto> CuentaCorrienteDetalle(int pConcepto)
        {
            return this.Servicio.ReadRepCuentaCorrienteDetallesFiltered("nombres, codigo", String.Format("concepto = {0}", pConcepto));
        }

        public IEnumerable<CuentaCorrienteItem> CuentaCorrienteCorte(DateTime pFechaCorte, int pConcepto)
        {
            return this.Servicio.ReporteCuentaCorrienteCorte(pFechaCorte, pConcepto);
        }        
        
        public IEnumerable<BaseImponibleAño> BaseImponiblePorConceptoCodigo(int pConcepto, string pCodigo)
        {
            return this.Servicio.ConsultaBaseImponibleConceptoCodigo(pConcepto, pCodigo);
        }    
        
        public IEnumerable<ResumenEmisionesPeriodoItem> ResumenEmisionesPeriodo(int pAño, DateTime pFechaInicio, DateTime pFechaCorte, int pPeriodo, Boolean pAfectantes)
        {
            return this.Servicio.ReporteResumenEmisionesPeriodo(pAño, pFechaInicio, pFechaCorte, pPeriodo, pAfectantes);
        }
        
        public IEnumerable<Utiles.ElementoSeleccion> EstadosTitulo()
        {
            List<Utiles.ElementoSeleccion> r = new List<Utiles.ElementoSeleccion>();             
            r.Add(new Utiles.ElementoSeleccion(1, "Recaudaciones"));
            r.Add(new Utiles.ElementoSeleccion(2, "Bajas"));
            return r.AsEnumerable();
        }

        public IEnumerable<RepCtaCteAnualItem> CuentaCorrienteAnualCorte(DateTime pFechaCorte, int pConcepto, int pAño)
        {
            return this.Servicio.ReporteCtaCteAnualCorte(pFechaCorte, pConcepto, pAño);
        }

        public IEnumerable<RepReversionesFechaDto> RepReversionesPorFecha(DateTime pFechaInicio, DateTime pFechaCorte)
        {
            return Servicio.ReadRepReversionesFechasFiltered("", "estado = 2 and fecha >= DateTime.Parse(\"" + pFechaInicio.ToString() + "\") and fecha <= DateTime.Parse(\"" + pFechaCorte.ToString() + "\")");
        }
        
    }
}
