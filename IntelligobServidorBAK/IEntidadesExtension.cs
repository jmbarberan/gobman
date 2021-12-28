using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace Intelligob.Servidor
{
    [ServiceContract]
    public interface IEntidadesExtension
    {
        [OperationContract]
        DateTime Hoy();

        [OperationContract]
        Double CalcularAvaluoUrbano(int? pId, int? pActualizar);

        [OperationContract]
        Double CalcularAvaluoRural(int? pId, int? pActualizar);

        [OperationContract]
        int UnificarContribuyentes(int? iPermanece, int? iElimina);

        [OperationContract]
        int EmitirImpUrbano(int? predioId, int? pAño);

        [OperationContract]
        int EmitirImpRural(int? predioId, int? pAño);

        [OperationContract]
        int EmitirPorConcepto(int pConcepto, String pParametros);

        [OperationContract]
        IEnumerable<Intelligob.Entidades.RubroCalcularConcepto> CalcularPorConcepto(int pConcepto, String pParametros);

        [OperationContract]
        String EmisionGeneralporConcepto(int pConcepto);

        [OperationContract]
        IEnumerable<Dto.ConceptosDocumentoDto> DocumentosPorConceptoUnico();

        [OperationContract]
        int NCreditoNumeroSigue();

        [OperationContract]
        int ComprobanteCajaSigue(int pTipo);

        [OperationContract]
        String ValidarConcepto(int pConcepto, String pParametros);

        [OperationContract]
        IEnumerable<Intelligob.Entidades.ResumenCatastralItem> ResumenCatastralPredial(int pAño);

        [OperationContract]
        Dto.PredioFotoDto LogoX48();

        [OperationContract]
        IEnumerable<Intelligob.Entidades.EmisionesSaldosItem> ReporteEmisionesSaldoFecha(DateTime pFechaCorte);

        [OperationContract]
        IEnumerable<Intelligob.Entidades.CuentaCorrienteItem> ReporteCuentaCorrienteCorte(DateTime pFechaCorte, int pConcepto);

        [OperationContract]
        IEnumerable<Intelligob.Entidades.BaseImponibleAño> ConsultaBaseImponibleConceptoCodigo(int pConcepto, string pCodigo);

        [OperationContract]
        IEnumerable<Intelligob.Entidades.CarpetaCatastralCorteItem> ReporteCarpetaCatastralCorte(DateTime pFechaCorte, int pConcepto, int pAño);

        [OperationContract]
        IEnumerable<Intelligob.Entidades.ResumenEmisionesPeriodoItem> ReporteResumenEmisionesPeriodo(int pAño, DateTime pFechaIncio, DateTime pFechaCorte, int pPeriodo, Boolean pAfectantes);

        [OperationContract]
        IEnumerable<Intelligob.Entidades.RepCtaCteAnualItem> ReporteCtaCteAnualCorte(DateTime pFechaCorte, int pConcepto, int pAño);
    }
}
