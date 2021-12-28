using System;
using System.Collections.Generic;
using System.Linq;
//;
using Intelligob.Entidades;

namespace Intelligob.Servidor
{    
    public partial class Entidades : IEntidades, IEntidadesExtension
    { 
        public virtual DateTime Hoy()
        {
            return DateTime.Today;
        }
        
        public virtual Double CalcularAvaluoUrbano(int? pId, int? pActualizar)
        {
            double? v = 0;
            return this.UnitOfWork.CalcularAvaluoUrbano(pId, pActualizar, ref v);
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

            IQueryable<ConceptosDocumento> consulta = (from concepto in this.UnitOfWork.ConceptosDocumentos
                select concepto).Distinct();

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
                System.Windows.Media.Imaging.BitmapImage img = new System.Windows.Media.Imaging.BitmapImage(new Uri(sf));
                if (System.IO.File.Exists(sf))
                {
                    System.Windows.Media.Imaging.JpegBitmapEncoder enc = new System.Windows.Media.Imaging.JpegBitmapEncoder();
                    enc.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(img));
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                    {
                        enc.Save(ms);
                        l = ms.ToArray();
                    }
                }
            }            
            catch {}
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
    }
}