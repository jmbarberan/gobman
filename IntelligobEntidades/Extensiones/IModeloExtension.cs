using System;
using System.Collections.Generic;
using System.Linq;

namespace Intelligob.Entidades
{
    public interface IModeloExtension
    {
        Double CalcularAvaluoRural(int? pREID, int? pACTUALIZAR, ref double? vAVALUO);

        Double CalcularAvaluoUrbano(int? pREID, int? pACTUALIZAR, ref double? vALPRO);

        int UnificarContribuyentes(int? iDPERMANECE, int? iDELIMINADO);

        int EmitirImpRural(int? pID, int? nYEAR);
        int EmitirImpUrbano(int? pID, int? nYEAR);

        IEnumerable<RubroCalcularConcepto> CalcularConcepto(int pConcepto, String pParametros);

        int EmitirConcepto(int pConcepto, String pParametros);

        String EmisionGeneralConcepto(int pConcepto);

        IEnumerable<ConceptosDocumento> DocumentosTraerConceptosUnicos();

        String ValidarConcepto(int pConcepto, string pParametros);

        IEnumerable<ResumenCatastralItem> RepResumenCatastralPorAño(int pAño);

        IEnumerable<EmisionesSaldosItem> RepEmisionesSaldoFecha(DateTime pFechaCorte);

        IEnumerable<CuentaCorrienteItem> RepCuentaCorrienteCorte(DateTime pFechaCorte, int pConcepto);

        IEnumerable<BaseImponibleAño> BaseImponiblePorConceptoCodigo(int pConcepto, String pCodigo);

        IEnumerable<CarpetaCatastralCorteItem> RepCarpetaCatastralCorte(DateTime pFechaCorte, Int32 pConcepto, int pAño);

        IEnumerable<ResumenEmisionesPeriodoItem> RepResumenEmisionesPeriodo(int pAño, DateTime pFechaInicio, DateTime pFechaCorte, int pPeriodo, Boolean pAfectantes);

        IEnumerable<RepCtaCteAnualItem> RepCuentaCorrienteAnualCorte(DateTime pFechaCorte, int pConcepto, int pAño);

        IEnumerable<Intelligob.Utilerias.Modelos.Cuenta> CGCuentasPorEstado(int pEstado); 

        Intelligob.Utilerias.Modelos.Cuenta CGCuentasPorId(int pId);

        IEnumerable<Intelligob.Utilerias.Modelos.AsientoItem> CGItemsPorAsiento(int pId);

        Intelligob.Utilerias.Modelos.Asiento CGAsientoUltimo();

        Intelligob.Utilerias.Modelos.Asiento CGAsientoPrimero();

        Intelligob.Utilerias.Modelos.Asiento CGAsientoSiguiente();

        Intelligob.Utilerias.Modelos.Asiento CGAsientoAnterior();

        Intelligob.Utilerias.Modelos.Asiento CGAsientoPorNumero();

        int CGAsientoNuevo(Intelligob.Utilerias.Modelos.Asiento asiento, IEnumerable<Intelligob.Utilerias.Modelos.AsientoItem> items);

        IEnumerable<Intelligob.Utilerias.Modelos.AsientoItem> CGDiario(DateTime fdesde, DateTime fhasta);
    }
}
