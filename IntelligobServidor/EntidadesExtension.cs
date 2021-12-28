namespace Intelligob.Servidor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    //;
    using Intelligob.Entidades;

    public partial class Entidades : IEntidadesExtension
    {
        public virtual DateTime Hoy()
        {
            return DateTime.Today;
        }

        public virtual Double CalcularAvaluoUrbano(int? pId, int? pActualizar)
        {
            double? v = 0;
            return UnitOfWork.CalcularAvaluoUrbano(pId, pActualizar, ref v);
        }

        public virtual Double CalcularAvaluoRural(int? pId, int? pActualizar)
        {
            double? v = 0;
            return this.UnitOfWork.CalcularAvaluoRural(pId, pActualizar, ref v);
        }

        public virtual int UnificarContribuyentes(int? iPermanece, int? iElimina)
        {
            return this.UnitOfWork.UnificarContribuyentes(iPermanece, iElimina);
        }

        public int EmitirImpUrbano(int? predioId, int? pAño)
        {
            return this.UnitOfWork.EmitirImpUrbano(predioId, pAño);
        }

        public int EmitirImpRural(int? predioId, int? pAño)
        {
            return this.UnitOfWork.EmitirImpRural(predioId, pAño);
        }

        public int EmitirPorConcepto(int pConcepto, String pParametros)
        {
            return this.UnitOfWork.EmitirConcepto(pConcepto, pParametros);
        }

        public IEnumerable<RubroCalcularConcepto> CalcularPorConcepto(int pConcepto, String pParametros)
        {
            return this.UnitOfWork.CalcularConcepto(pConcepto, pParametros);
        }

        public String EmisionGeneralporConcepto(int pConcepto)
        {
            return this.UnitOfWork.EmisionGeneralConcepto(pConcepto);
        }

        public IEnumerable<Dto.ConceptosDocumentoDto> DocumentosPorConceptoUnico()
        {
            Assemblers.ConceptosDocumentoAssembler ensamblador = new Assemblers.ConceptosDocumentoAssembler();

            IQueryable<ConceptosDocumento> consulta = (from concepto in UnitOfWork.ConceptosDocumentos
                                                       select concepto).Distinct();

            return ensamblador.Assemble(consulta);
        }

        public IEnumerable<Dto.PlanillaDto> PlanillasDeudaPorCodigo(string pCodigo)
        {
            Assemblers.PlanillaAssembler ensamblador = new Assemblers.PlanillaAssembler();

            IQueryable<Planilla> consulta = UnitOfWork.Planillas.Where(w => w.Estado == 0 && w.Codigo == pCodigo);
            //IQueryable<Planilla> consulta = (from pla in UnitOfWork.Planillas
            //                                           select pla).;

            return ensamblador.Assemble(consulta);
        }

        public int NCreditoNumeroSigue()
        {
            int? n = 0;
            n = (from convenio in this.UnitOfWork.Convenios
                 select convenio).Max((o) => o.Numero);
            if (n != null)
                n = n + 1;
            else
                n = 1;
            return (int)n;
        }

        public int ComprobanteCajaSigue(int pTipo)
        {
            int? n = 0;
            n = (from comp in this.UnitOfWork.CajaComprobantes select comp).Where((c) => c.Tipo == pTipo).Max((o) => o.Numero);

            if (n != null)
                n = n + 1;
            else
                n = 1;
            return (int)n;
        }

        public string ValidarConcepto(int pConcepto, string pParametros)
        {
            return this.UnitOfWork.ValidarConcepto(pConcepto, pParametros);
        }

        public IEnumerable<Intelligob.Entidades.ResumenCatastralItem> ResumenCatastralPredial(int pAño)
        {
            return this.UnitOfWork.RepResumenCatastralPorAño(pAño);
        }

        public Dto.PredioFotoDto LogoX48()
        {
            byte[] l = null;
            try
            {
                String sf = System.Configuration.ConfigurationManager.AppSettings.Get("Logo48").ToString();
                if (System.IO.File.Exists(sf))
                {
                    System.Windows.Media.Imaging.BitmapImage img = new System.Windows.Media.Imaging.BitmapImage(new Uri(sf));
                    System.Windows.Media.Imaging.JpegBitmapEncoder enc = new System.Windows.Media.Imaging.JpegBitmapEncoder();
                    enc.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(img));
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                    {
                        enc.Save(ms);
                        l = ms.ToArray();
                    }
                }
            }
            catch
            {
                return null;
            }
            return new Dto.PredioFotoDto(0, 0, 0, "", l, 0, null);
        }

        public IEnumerable<Intelligob.Entidades.EmisionesSaldosItem> ReporteEmisionesSaldoFecha(DateTime pFechaCorte)
        {
            return this.UnitOfWork.RepEmisionesSaldoFecha(pFechaCorte);
        }

        public IEnumerable<Intelligob.Entidades.CuentaCorrienteItem> ReporteCuentaCorrienteCorte(DateTime pFechaCorte, int pConcepto)
        {
            return this.UnitOfWork.RepCuentaCorrienteCorte(pFechaCorte, pConcepto);
        }

        public IEnumerable<Intelligob.Entidades.BaseImponibleAño> ConsultaBaseImponibleConceptoCodigo(int pConcepto, string pCodigo)
        {
            return this.UnitOfWork.BaseImponiblePorConceptoCodigo(pConcepto, pCodigo);
        }

        public IEnumerable<Intelligob.Entidades.CarpetaCatastralCorteItem> ReporteCarpetaCatastralCorte(DateTime pFechaCorte, int pConcepto, int pAño)
        {
            return this.UnitOfWork.RepCarpetaCatastralCorte(pFechaCorte, pConcepto, pAño);
        }

        public IEnumerable<Intelligob.Entidades.ResumenEmisionesPeriodoItem> ReporteResumenEmisionesPeriodo(int pAño, DateTime pFechaIncio, DateTime pFechaCorte, int pPeriodo, Boolean pAfectantes)
        {
            return this.UnitOfWork.RepResumenEmisionesPeriodo(pAño, pFechaIncio, pFechaCorte, pPeriodo, pAfectantes);
        }

        public IEnumerable<Intelligob.Entidades.RepCtaCteAnualItem> ReporteCtaCteAnualCorte(DateTime pFechaCorte, int pConcepto, int pAño)
        {
            return this.UnitOfWork.RepCuentaCorrienteAnualCorte(pFechaCorte, pConcepto, pAño);
        }

        public IEnumerable<Intelligob.Utilerias.Modelos.Cuenta> CGCuentasPorEstado(int pEstado)
        {
            return this.UnitOfWork.CGCuentasPorEstado(pEstado);
        }

        public List<Dto.CobroDto> RepTicketsAcumulados(DateTime desde, DateTime hasta, bool completo)
        {
            List<Dto.CobroDto> res = new List<Dto.CobroDto>();
            var tickets = (from t in UnitOfWork.Cobros
                           where t.Estado == 3 && t.Fecha <= desde && t.Fecha >= hasta
                           group t by new { PuestoId = t.FormaPago, PuestoNumero = t.FormaPagoAtributos } into g
                           select new Dto.CobroDto()
                           {
                               FormaPago = g.Key.PuestoId,
                               FormaPagoAtributos = g.Key.PuestoNumero,
                               Valor = g.Sum(s => s.Valor),
                           }
            );
            if (completo)
                return tickets.Where(w => w.Valor == 30).ToList();
            else
                return tickets.ToList();
        }

        public int CrearCobroDeTicketsCompletos(int PuestoId, DateTime desde, DateTime hasta)
        {
            return 0;
        }

        public List<string> TicketsCompletosPuestoPeriodo(int puesto, DateTime desde, DateTime hasta)
        {
            List<string> res = new List<string>();
            var tickets = (from t in UnitOfWork.Cobros
                           where t.Estado == 3 && t.Fecha <= desde && t.Fecha >= hasta && t.FormaPago == puesto
                           select new Dto.CobroDto()
                           {
                               FormaPago = t.FormaPago,
                               FormaPagoAtributos = t.FormaPagoAtributos,
                               Valor = t.Valor
                           }
            );
            return tickets.Select(s => s.FormaPagoAtributos).ToList();
        }

        public List<Dto.CobroDto> RepTicketsCobroDiario(DateTime desde, DateTime hasta)
        {
            return null;
        }
    }
}