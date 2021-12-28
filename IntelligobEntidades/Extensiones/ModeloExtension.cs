using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Telerik.OpenAccess.Data.Common;

namespace Intelligob.Entidades
{
    public partial class Modelo: IModeloExtension
    {
        public Double CalcularAvaluoRural(int? pREID, int? pACTUALIZAR, ref double? vAVALUO)
        {
            OAParameter parameterPREID = new OAParameter();
            parameterPREID.ParameterName = "PREID";
            if (pREID.HasValue)
            {
                parameterPREID.Value = pREID.Value;
            }
            else
            {
                parameterPREID.DbType = DbType.Int32;
                parameterPREID.Value = DBNull.Value;
            }

            OAParameter parameterPACTUALIZAR = new OAParameter();
            parameterPACTUALIZAR.ParameterName = "PACTUALIZAR";
            if (pACTUALIZAR.HasValue)
            {
                parameterPACTUALIZAR.Value = pACTUALIZAR.Value;
            }
            else
            {
                parameterPACTUALIZAR.DbType = DbType.Int32;
                parameterPACTUALIZAR.Value = DBNull.Value;
            }

            OAParameter parameterVAVALUO = new OAParameter();
            parameterVAVALUO.ParameterName = "VAVALUO";
            parameterVAVALUO.Direction = ParameterDirection.Output;
            if (vAVALUO.HasValue)
            {
                parameterVAVALUO.Value = vAVALUO.Value;
            }
            else
            {
                parameterVAVALUO.DbType = DbType.Double;
                parameterVAVALUO.Value = DBNull.Value;
            }

            Double queryResult = this.ExecuteScalar<Double>("\"PRERURAL_CALCULAR_VALPRO\"", CommandType.StoredProcedure, parameterPREID, parameterPACTUALIZAR, parameterVAVALUO);
            
            vAVALUO = parameterVAVALUO.Value == DBNull.Value
                ? default(double?)
                : (double?)parameterVAVALUO.Value;

            if (pACTUALIZAR == 1)
                this.SaveChanges();

            return queryResult;
        }

        public Double CalcularAvaluoUrbano(int? pREID, int? pACTUALIZAR, ref double? vALPRO)
        {
            OAParameter parameterPREID = new OAParameter();
            parameterPREID.ParameterName = "PREID";
            if (pREID.HasValue)
            {
                parameterPREID.Value = pREID.Value;
            }
            else
            {
                parameterPREID.DbType = DbType.Int32;
                parameterPREID.Value = DBNull.Value;
            }

            OAParameter parameterPACTUALIZAR = new OAParameter();
            parameterPACTUALIZAR.ParameterName = "PACTUALIZAR";
            if (pACTUALIZAR.HasValue)
            {
                parameterPACTUALIZAR.Value = pACTUALIZAR.Value;
            }
            else
            {
                parameterPACTUALIZAR.DbType = DbType.Int32;
                parameterPACTUALIZAR.Value = DBNull.Value;
            }

            OAParameter parameterVALPRO = new OAParameter();
            parameterVALPRO.ParameterName = "VALPRO";
            parameterVALPRO.Direction = ParameterDirection.Output;
            if (vALPRO.HasValue)
            {
                parameterVALPRO.Value = vALPRO.Value;
            }
            else
            {
                parameterVALPRO.DbType = DbType.Double;
                parameterVALPRO.Value = DBNull.Value;
            }

            Double queryResult = this.ExecuteScalar<Double>("\"PREURBANO_CALCULAR_VALPRO\"", CommandType.StoredProcedure, parameterPREID, parameterPACTUALIZAR, parameterVALPRO);

            vALPRO = parameterVALPRO.Value == DBNull.Value
                ? default(double?)
                : (double?)parameterVALPRO.Value;

            if (pACTUALIZAR == 1)
                this.SaveChanges();

            return queryResult;
        }

        public int UnificarContribuyentes(int? iDPERMANECE, int? iDELIMINADO)
		{
			OAParameter parameterIDPERMANECE = new OAParameter();
			parameterIDPERMANECE.ParameterName = "IDPERMANECE";
			if(iDPERMANECE.HasValue)
			{
				parameterIDPERMANECE.Value = iDPERMANECE.Value;
			}
			else
			{
				parameterIDPERMANECE.DbType = DbType.Int32;
				parameterIDPERMANECE.Value = DBNull.Value;
			}

			OAParameter parameterIDELIMINADO = new OAParameter();
			parameterIDELIMINADO.ParameterName = "IDELIMINADO";
			if(iDELIMINADO.HasValue)
			{
				parameterIDELIMINADO.Value = iDELIMINADO.Value;
			}
			else
			{
				parameterIDELIMINADO.DbType = DbType.Int32;
				parameterIDELIMINADO.Value = DBNull.Value;
			}

			int queryResult = this.ExecuteNonQuery("\"UNIFICAR_CONTRIBUYENTES\"", CommandType.StoredProcedure, parameterIDPERMANECE, parameterIDELIMINADO);

            this.SaveChanges();
	
			return queryResult;
        }

        public int EmitirImpRural(int? pID, int? nYEAR)
        {
            OAParameter parameterPID = new OAParameter();
            parameterPID.ParameterName = "PID";
            if (pID.HasValue)
            {
                parameterPID.Value = pID.Value;
            }
            else
            {
                parameterPID.DbType = DbType.Int32;
                parameterPID.Value = DBNull.Value;
            }

            OAParameter parameterNYEAR = new OAParameter();
            parameterNYEAR.ParameterName = "NYEAR";
            if (nYEAR.HasValue)
            {
                parameterNYEAR.Value = nYEAR.Value;
            }
            else
            {
                parameterNYEAR.DbType = DbType.Int32;
                parameterNYEAR.Value = DBNull.Value;
            }

            int queryResult = this.ExecuteNonQuery("\"EMITIR_IPRURAL\"", CommandType.StoredProcedure, parameterPID, parameterNYEAR);

            this.SaveChanges();

            return queryResult;
        }

        public int EmitirImpUrbano(int? pID, int? nYEAR)
        {
            OAParameter parameterPID = new OAParameter();
            parameterPID.ParameterName = "PID";
            if (pID.HasValue)
            {
                parameterPID.Value = pID.Value;
            }
            else
            {
                parameterPID.DbType = DbType.Int32;
                parameterPID.Value = DBNull.Value;
            }

            OAParameter parameterNYEAR = new OAParameter();
            parameterNYEAR.ParameterName = "NYEAR";
            if (nYEAR.HasValue)
            {
                parameterNYEAR.Value = nYEAR.Value;
            }
            else
            {
                parameterNYEAR.DbType = DbType.Int32;
                parameterNYEAR.Value = DBNull.Value;
            }

            int queryResult = this.ExecuteNonQuery("\"EMITIR_IPURBANO\"", CommandType.StoredProcedure, parameterPID, parameterNYEAR);

            this.SaveChanges();

            return queryResult;
        }

        public int EmitirConcepto(int pConcepto, String pParametros)
        {
            OAParameter paramID = new OAParameter();
            paramID.ParameterName = "ID";
            paramID.Direction = ParameterDirection.Output;
            paramID.DbType = DbType.Int32;
            paramID.Value = 0;

            OAParameter paramConcepto = new OAParameter();
            paramConcepto.ParameterName = "PCONCEPTO";
            paramConcepto.DbType = DbType.Int32;
            paramConcepto.Value = pConcepto;

            OAParameter paramParametros = new OAParameter();
            paramParametros.ParameterName = "PARAMETROS";
            paramParametros.DbType = DbType.String;
            paramParametros.Value = pParametros;

            OAParameter paramUsuario = new OAParameter();
            paramUsuario.ParameterName = "PUSUARIO";
            paramUsuario.DbType = DbType.Int32;
            paramUsuario.Value = 0;

            OAParameter paramComentarios = new OAParameter();
            paramComentarios.ParameterName = "PCOMENTARIOS";
            paramComentarios.DbType = DbType.String;
            paramComentarios.Value = "";

            int res = this.ExecuteScalar<int>("\"EMITIR_TITULO\"", CommandType.StoredProcedure, paramConcepto, paramParametros, paramUsuario, paramComentarios);

            this.SaveChanges();

            return res;
        }

        public IEnumerable<RubroCalcularConcepto> CalcularConcepto(int pConcepto, String pParametros)
        {
            using (Modelo dbContext = new Modelo())
            {
                OAParameter paramConcepto = new OAParameter();
                paramConcepto.ParameterName = "PCONCEPTO";
                paramConcepto.DbType = DbType.Int32;
                paramConcepto.Value = pConcepto;

                OAParameter paramTipos = new OAParameter();
                paramTipos.ParameterName = "PTIPOS";
                paramTipos.DbType = DbType.String;
                paramTipos.Value = "";  

                OAParameter paramParametros = new OAParameter();
                paramParametros.ParameterName = "PARAMETROS";
                paramParametros.DbType = DbType.String;
                paramParametros.Value = pParametros;

                // 3. Execute the query and consume the result.
                IEnumerable<RubroCalcularConcepto> res = dbContext.ExecuteQuery<RubroCalcularConcepto>(
                    "\"CALCULAR_CONCEPTO\"", CommandType.StoredProcedure, paramConcepto, paramTipos, paramParametros);

                return res;
            }
        }

        public String EmisionGeneralConcepto(int pConcepto)
        {
            OAParameter paramPeriodo = new OAParameter();
            paramPeriodo.ParameterName = "PERIODO";
            paramPeriodo.Direction = ParameterDirection.Output;
            paramPeriodo.DbType = DbType.String;
            paramPeriodo.Value = String.Empty;

            OAParameter paramConcepto = new OAParameter();
            paramConcepto.ParameterName = "PCONCEPTO";
            paramConcepto.DbType = DbType.Int32;
            paramConcepto.Value = pConcepto;

            using (Modelo dbContext = new Modelo())
            {
                using (OAConnection connection = dbContext.Connection)
                {
                    OACommand command = connection.CreateCommand();
                    command.CommandTimeout = 900;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "\"EMITIR_CONCEPTO_TODOS\"";
                    command.Parameters.Add(paramConcepto);
                    command.Parameters.Add(paramPeriodo);
                    string res = (string)command.ExecuteScalar();
                    //String res = this.ExecuteScalar<String>("\"EMITIR_CONCEPTO_TODOS\"", CommandType.StoredProcedure, paramConcepto);
                    dbContext.SaveChanges();
                    return res;
                }
            }            
        }

        public IEnumerable<ConceptosDocumento> DocumentosTraerConceptosUnicos()
        {
            using (Modelo contexto = new Modelo())
            {
                IQueryable<ConceptosDocumento> consulta = (from concepto in contexto.ConceptosDocumentos
                    select concepto).Distinct();
                return consulta;
            }            
        }

        public string ValidarConcepto(int pConcepto, string pParametros)
        {
            OAParameter paramConcepto = new OAParameter();
            paramConcepto.ParameterName = "PCONCEPTO";
            paramConcepto.DbType = DbType.Int32;
            paramConcepto.Value = pConcepto;

            OAParameter paramParametros = new OAParameter();
            paramParametros.ParameterName = "PARAMETROS";
            paramParametros.DbType = DbType.String;
            paramParametros.Value = pParametros;

            OAParameter parameterResultado = new OAParameter();
            parameterResultado.ParameterName = "RESULTADO";
            parameterResultado.Direction = ParameterDirection.Output;
            parameterResultado.DbType = DbType.String;
            parameterResultado.Value = "V";

            String queryResult = this.ExecuteScalar<String>("\"VALIDAR_CONCEPTO\"", CommandType.StoredProcedure, paramConcepto, paramParametros, parameterResultado);            

            return queryResult;
        }

        public IEnumerable<ResumenCatastralItem> RepResumenCatastralPorAño(int pAño)
        {
            OAParameter paramAño = new OAParameter();
            paramAño.ParameterName = "PYEAR";
            paramAño.DbType = DbType.Int32;
            paramAño.Value = pAño;

            using (Modelo contexto = new Modelo())
            {
                IEnumerable<ResumenCatastralItem> res = contexto.ExecuteQuery<ResumenCatastralItem>(
                    "\"REP_RESUMEN_CATASTRAL\"", CommandType.StoredProcedure, paramAño);

                return res;
            }
        }

        public IEnumerable<EmisionesSaldosItem> RepEmisionesSaldoFecha(DateTime pFechaCorte)
        {
            OAParameter paramFecha = new OAParameter();
            paramFecha.ParameterName = "PFECHA_CORTE";
            paramFecha.DbType = DbType.DateTime;
            paramFecha.Value = pFechaCorte;

            using (Modelo contexto = new Modelo())
            {
                IEnumerable<EmisionesSaldosItem> res = contexto.ExecuteQuery<EmisionesSaldosItem>(
                    "\"REP_EMISIONES_SALDO_FECHA\"", CommandType.StoredProcedure, paramFecha);

                return res;
            }
        }

        public IEnumerable<CuentaCorrienteItem> RepCuentaCorrienteCorte(DateTime pFechaCorte, int pConcepto)
        {
            OAParameter paramFecha = new OAParameter();
            paramFecha.ParameterName = "PFECHACORTE";
            paramFecha.DbType = DbType.DateTime;
            paramFecha.Value = pFechaCorte;

            OAParameter paramConcepto = new OAParameter();
            paramConcepto.ParameterName = "PCONCEPTO";
            paramConcepto.DbType = DbType.Int32;
            paramConcepto.Value = pConcepto;

            using (Modelo contexto = new Modelo())
            {
                IEnumerable<CuentaCorrienteItem> res = contexto.ExecuteQuery<CuentaCorrienteItem>(
                    "\"REP_CUENTA_CORRIENTE_CORTE\"", CommandType.StoredProcedure, paramFecha, paramConcepto);

                return res;
            }
        }

        public IEnumerable<CarpetaCatastralCorteItem> RepCarpetaCatastralCorte(DateTime pFechaCorte, Int32 pConcepto, int pAño)
        {
            OAParameter paramFecha = new OAParameter();
            paramFecha.ParameterName = "FECHA_CORTE";
            paramFecha.DbType = DbType.DateTime;
            paramFecha.Value = pFechaCorte;

            OAParameter paramConcepto = new OAParameter();
            paramConcepto.ParameterName = "CONCEPTO";
            paramConcepto.DbType = DbType.Int32;
            paramConcepto.Value = pConcepto;

            OAParameter paramAño = new OAParameter();
            paramAño.ParameterName = "AÑO";
            paramAño.DbType = DbType.Int32;
            paramAño.Value = pAño;

            using (Modelo contexto = new Modelo())
            {
                IEnumerable<CarpetaCatastralCorteItem> res = contexto.ExecuteQuery<CarpetaCatastralCorteItem>(
                    "\"REP_CARPETA_CATASTRAL_CORTE\"", CommandType.StoredProcedure, paramAño, paramConcepto, paramFecha);

                return res;
            }
        }

        public IEnumerable<BaseImponibleAño> BaseImponiblePorConceptoCodigo(int pConcepto, String pCodigo)
        {
            OAParameter paramConcepto = new OAParameter();
            paramConcepto.ParameterName = "PCONCETPO";
            paramConcepto.DbType = DbType.Int32;
            paramConcepto.Value = pConcepto;

            OAParameter paramCodigo = new OAParameter();
            paramCodigo.ParameterName = "PCODIGO";
            paramCodigo.DbType = DbType.String;
            paramCodigo.Value = pCodigo;

            using (Modelo contexto = new Modelo())
            {
                IEnumerable<BaseImponibleAño> res = contexto.ExecuteQuery<BaseImponibleAño>(
                    "\"BASE_IMPONIBLE_XCODIGO\"", CommandType.StoredProcedure, paramConcepto, paramCodigo);

                return res;
            }
        }

        public IEnumerable<ResumenEmisionesPeriodoItem> RepResumenEmisionesPeriodo(int pAño, DateTime pFechaInicio, DateTime pFechaCorte, int pPeriodo, Boolean pAfectantes)
        {
            OAParameter paramAño = new OAParameter();
            paramAño.ParameterName = "PAÑO";
            paramAño.DbType = DbType.Int32;
            paramAño.Value = pAño;

            OAParameter paramFechai = new OAParameter();
            paramFechai.ParameterName = "PFECHAINICIO";
            paramFechai.DbType = DbType.DateTime;
            paramFechai.Value = pFechaInicio;

            OAParameter paramFechac = new OAParameter();
            paramFechac.ParameterName = "PFECHACORTE";
            paramFechac.DbType = DbType.DateTime;
            paramFechac.Value = pFechaCorte;

            OAParameter paramPeriodo = new OAParameter();
            paramPeriodo.ParameterName = "PCONCEPTO";
            paramPeriodo.DbType = DbType.Int32;
            paramPeriodo.Value = pPeriodo;

            OAParameter paramAfectantes = new OAParameter();
            paramAfectantes.ParameterName = "PAFECTANTES";
            paramAfectantes.DbType = DbType.Int32;
            int i = 0;
            if (pAfectantes)
                i = 1;            
            paramAfectantes.Value = i;

            using (Modelo contexto = new Modelo())
            {
                IEnumerable<ResumenEmisionesPeriodoItem> res = contexto.ExecuteQuery<ResumenEmisionesPeriodoItem>(
                    "\"REP_RESUMEN_EMISIONES_PERIODO\"", CommandType.StoredProcedure, paramAño, paramFechai, paramFechac, paramPeriodo, paramAfectantes);                

                return res;
            }
        }
 
        public IEnumerable<RepCtaCteAnualItem> RepCuentaCorrienteAnualCorte(DateTime pFechaCorte, int pConcepto, int pAño)
        {
            OAParameter paramFecha = new OAParameter();
            paramFecha.ParameterName = "PFECHACORTE";
            paramFecha.DbType = DbType.DateTime;
            paramFecha.Value = pFechaCorte;

            OAParameter paramConcepto = new OAParameter();
            paramConcepto.ParameterName = "CONCEPTO";
            paramConcepto.DbType = DbType.Int32;
            paramConcepto.Value = pConcepto;

            OAParameter paramAño = new OAParameter();
            paramAño.ParameterName = "AÑO";
            paramAño.DbType = DbType.Int32;
            paramAño.Value = pAño;

            using (Modelo contexto = new Modelo())
            {
                IEnumerable<RepCtaCteAnualItem> res = contexto.ExecuteQuery<RepCtaCteAnualItem>(
                    "\"REP_CTA_CORRIENTE_ANUAL_CORTE\"", CommandType.StoredProcedure, paramAño, paramConcepto, paramFecha);

                return res;
            }
        }

        public IEnumerable<Intelligob.Utilerias.Modelos.Cuenta> CGCuentasPorEstado(int pEstado)
        {
            OAParameter param = new OAParameter();
            param.ParameterName = "estado";
            param.DbType = DbType.Int32;
            param.Value = pEstado;

            using (Modelo contexto = new Modelo())
            {
                IEnumerable<Intelligob.Utilerias.Modelos.Cuenta> res = contexto.ExecuteQuery<Intelligob.Utilerias.Modelos.Cuenta>(
                    "\"Select * from CUENTA where estado = @estado\"", CommandType.Text, param);
                return res;
            }
        }

        public Intelligob.Utilerias.Modelos.Cuenta CGCuentasPorId(int pId)
        {
            return null;
        }

        public IEnumerable<Intelligob.Utilerias.Modelos.AsientoItem> CGItemsPorAsiento(int pId)
        {
            return null;
        }

        public Intelligob.Utilerias.Modelos.Asiento CGAsientoUltimo()
        {
            return null;
        }

        public Intelligob.Utilerias.Modelos.Asiento CGAsientoPrimero()
        {
            return null;
        }

        public Intelligob.Utilerias.Modelos.Asiento CGAsientoSiguiente()
        {
            return null;
        }

        public Intelligob.Utilerias.Modelos.Asiento CGAsientoAnterior()
        {
            return null;
        }

        public Intelligob.Utilerias.Modelos.Asiento CGAsientoPorNumero()
        {
            return null;
        }

        public int CGAsientoNuevo(Intelligob.Utilerias.Modelos.Asiento asiento, IEnumerable<Intelligob.Utilerias.Modelos.AsientoItem> items)
        {
            return 0;
        }

        public IEnumerable<Intelligob.Utilerias.Modelos.AsientoItem> CGDiario(DateTime fdesde, DateTime fhasta)
        {
            return null;
        }
    }
}
