using Intelligob.Cliente.Modelos;
using Intelligob.Cliente.Referencia;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intelligob.Cliente.Depositos
{
    public class EmisionesDep : DepositoBase
    {
        public EmisionesDep() : base(DepositosControl.Instance.Servicio) { }

        public EmisionesDep(IEntidades servicio) : base(servicio) { }

        public IEnumerable<RubroDto> RubrosPorEstado(int pEstado)
        {
            if (pEstado == 9)
            {
                return this.Servicio.ReadRubros();
            }
            else
            {
                return this.Servicio.ReadRubros().Where(r => r.Estado == pEstado);
            }
        }

        public IEnumerable<ConceptosEmisionDto> ParametrosEmisionPorConcepto(int pConcepto)
        {            
            return this.Servicio.ReadConceptosEmisionsFiltered("indice", String.Format("concepto = {0}", pConcepto));
        }

        public IEnumerable<RubroCalcularConcepto> CalcularRubrosPorConcepto(int pConcepto, IEnumerable<ConceptosEmisionDto> pParametros)
        {
            String sp = String.Empty;
            foreach (ConceptosEmisionDto c in pParametros)
            {
                if (c.Calcula == 1)
                {
                    if (sp.Trim().Length > 0)
                        sp = sp + ", ";
                    if (c.TipoDato == 1)
                        sp = sp + c.Identificador.ToString();
                    else
                        sp = sp + c.Valor.ToString();
                }
            }
            IEnumerable<Intelligob.Cliente.Referencia.RubroCalcularConcepto> rs = this.Servicio.CalcularPorConcepto(pConcepto, sp);
            return rs;
        }

        public int EmitirTituloPorConcepto(int pConcepto, IEnumerable<ConceptosEmisionDto> pParametros)
        {
            String sp = String.Empty;
            foreach (ConceptosEmisionDto c in pParametros)
            {
                if (c.Emite == 1)
                {
                    if (sp.Trim().Length > 0)
                        sp = sp + ", ";
                    switch(c.TipoDato)
                    {
                        case 1: { sp = sp + c.Identificador.ToString(); break; }
                        case 2: { sp = sp + c.Valor.ToString(); break; }
                    }                    
                }
            }

            int i = this.Servicio.EmitirPorConcepto(pConcepto, sp);

            String conDen = "N/D";
            ConceptoDto con = this.Servicio.ReadConcepto(String.Format(this.FormatoClave, pConcepto));
            if (con != null)
            {
                conDen = con.Denominacion;
            }

            String cons = String.Empty;
            Double val = 0.00;
            PlanillaDto pla = this.Servicio.ReadPlanilla(String.Format(this.FormatoClave, i));
            if (pla != null && pla.Total != null)
            {
                val = (Double)pla.Total;
                cons = pla.ContribuyentesCadena;
            }

            Utiles.TablaCadena tb = new Utiles.TablaCadena();
            String enc = "Emision individual\r\n";
            enc =  enc + "==================\r\n";
            tb.AddRow("Atributo", "Valor");
            tb.AddRow("--------", "-----");
            tb.AddRow("Concepto", conDen);
            tb.AddRow("Argumentos", sp);
            tb.AddRow("Titulo Id", i);
            tb.AddRow("Contribuyente", cons);
            tb.AddRow("Valor", String.Format("{0:n2}", val));

            SeguridadDep seg = new SeguridadDep();
            seg.CrearSeguimiento(tb, "Operacion de Emision de titulos", Utiles.EntidadesEnum.EnEmision, enc);

            return i;
        }
        
        public void EmisionGeneralPorConcepto(int pConcepto)
        {
            String per = this.Servicio.EmisionGeneralporConcepto(pConcepto);

            String conDen = "N/D";
            ConceptoDto con = this.Servicio.ReadConcepto(String.Format(this.FormatoClave, pConcepto));
            if (con != null)
            {
                conDen = con.Denominacion;
            }

            Utiles.TablaCadena tb = new Utiles.TablaCadena();
            String enc = "Emision general\r\n";
            enc =  enc + "===============\r\n";
            tb.AddRow("Atributo", "Valor");
            tb.AddRow("--------", "-----");
            tb.AddRow("Concepto", conDen);
            tb.AddRow("Periodo", per);
            
            SeguridadDep seg = new SeguridadDep();
            seg.CrearSeguimiento(tb, "Operacion de Emision de titulos", Utiles.EntidadesEnum.EnEmision, enc);
        }

        public String ValidarEmisionConcepto(int pConcepto, IEnumerable<ConceptosEmisionDto> lParametros)
        {
            string sp = String.Empty;
            foreach (ConceptosEmisionDto c in lParametros)
            {
                if (c.Validar == 1)
                {
                    if (sp.Trim().Length > 0)
                        sp = sp + ", ";
                    switch (c.TipoDato)
                    {
                        case 1: { sp = sp + c.Identificador.ToString(); break; }
                        case 2: { sp = sp + c.Valor.ToString(); break; }
                    }
                }
            }
            return this.Servicio.ValidarConcepto(pConcepto, sp);
        }

        #region Notas de Credito

        /// <summary>
        /// Traer Notas de credito por contribuyente y estado
        /// </summary>
        /// <param name="pContribuyente">Contribuyente a consultar</param>
        /// <param name="pEstado">Estado de las notas (9 = TODOS)</param>
        /// <param name="pExpandir">Expandir para incluir entidades relacionadas</param>
        /// <returns></returns>
        public IEnumerable<ConvenioDto> NCreditosPorContribuyenteEstado(int pContribuyente, int pEstado, bool pExpandir)
        {
            IEnumerable<ConvenioDto> res;
            if (pEstado == 9)
            {
                res = Servicio.ReadConveniosFiltered("", String.Format("tipo = 2 and contribuyente = {0}", pContribuyente));                                
            }
            else
            {
                res = Servicio.ReadConveniosFiltered("", String.Format("tipo = 2 and contribuyente = {0} and estado = {1}", pContribuyente, pEstado));
            }
            if (pExpandir)
            {
                foreach (ConvenioDto cnv in res)
                {
                    if (cnv.Contribuyente != null && cnv.Contribuyente > 0)
                    {
                        cnv.ContribuyenteNav = Servicio.ReadContribuyente(string.Format(FormatoClave, cnv.Contribuyente));
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// Aplicar un valor pagado a nota de credito
        /// </summary>
        /// <param name="pId">Id de la nota de credito</param>
        /// <param name="pValor">Valor a pagar</param>
        public void NCreditoAplicarPago(int pId, double pValor, int pCobro)
        {
            ConvenioDto nc = this.Servicio.ReadConvenio(String.Format(this.FormatoClave, pId));
            if (nc != null)
            {
                if (nc.Pagos != null)
                {
                    nc.Pagos = nc.Pagos + pValor;
                    nc.Emisiones = nc.Emisiones + 1;
                }
                else
                {
                    nc.Pagos = pValor;
                    nc.Emisiones = 1;
                }
                if ((double)(nc.Valor - nc.Pagos) == 0)
                    nc.Estado = 1;
                this.Servicio.UpdateConvenio(nc);

                // Registrar cobro pagado con esta NC
                ConvenioTransaccionDto nct = new ConvenioTransaccionDto();
                nct.Convenio = pId;
                nct.Transaccion = pCobro;
                nct.Planilla = null;
                nct.Valor = pValor;
                nct.Estado = 0;
                this.Servicio.CreateConvenioTransaccion(nct);
            }
        }

        /// <summary>
        /// Crear Nota de Credito
        /// </summary>
        /// <param name="pNCredito">Nota de credito a crear</param>
        public void NotaCreditoCrear(ConvenioDto pNCredito)
        {
            pNCredito.Numero = Servicio.NCreditoNumeroSigue(); // Traer ultimo numero + 1
        }

        #endregion

        #region Reportes
        public IEnumerable<PlanillaDto> PlanillasPorBajas(DateTime? pFechaInicio, DateTime? pFechaCorte)
        {
            IEnumerable<PlanillaDto> pls;
            pls = this.Servicio.ReadPlanillasFiltered("", "estado = 2 and fechaCancelacion >= DateTime.Parse(\"" + pFechaInicio.ToString() + "\") and fechaCancelacion <=  DateTime.Parse(\"" + pFechaCorte.ToString() + "\")");
            foreach (PlanillaDto p in pls)
            {
                if (p.Concepto != null && p.Concepto > 0)
                    p.ConceptoNav = ModeloCache.Instance.McConceptos.Where(c => c.Id == p.Concepto).FirstOrDefault();

                if (!String.IsNullOrWhiteSpace(p.Contribuyentes))
                {
                    int i = 1;
                    string cons = String.Empty;
                    //string[] ss = pla.Contribuyentes.Split(new char[] { '[', ']' });
                    string[] ss = p.Contribuyentes.Trim().Split(']');
                    foreach (String s in ss)
                    {
                        if (i <= 2)
                        {
                            if (!String.IsNullOrWhiteSpace(s))
                            {
                                String m = s.Trim().Substring(1);
                                ContribuyenteDto c = this.Servicio.ReadContribuyente(String.Format(this.FormatoClave, m));
                                if (cons.Length > 0)
                                    cons = cons + ", ";

                                cons = cons + c.Nombres;
                            }
                        }
                        else
                        {
                            cons = cons + String.Format("... {0} mas", ss.Length - 3);
                            break;
                        }
                        i = i + 1;
                    }
                    p.ContribuyentesCadena = cons;
                }

            }
            return pls;
        }

        public IEnumerable<CarpetaCatastralCorteItem> CarpetaCatastralPorAñoConcepto(int año, int concepto, string orden)
        {
            IEnumerable<CarpetaCatastralCorteItem> res;
            if (orden == "codigo")
                res = this.Servicio.ReporteCarpetaCatastralCorte(DateTime.Today, concepto, año).OrderBy(o => o.codigo).OrderBy(o => o.nombres).OrderBy(o => o.concepto_id);
            else
                res = this.Servicio.ReporteCarpetaCatastralCorte(DateTime.Today, concepto, año).OrderBy(o => o.nombres).OrderBy(o => o.nombres).OrderBy(o => o.concepto_id);
            return res;
        }

        public IEnumerable<CarpetaCatastralCorteItem> CarpetaCatastralPorAñoConceptoFecha(int año, int concepto, string orden, DateTime pFechaCorte)
        {
            IEnumerable<CarpetaCatastralCorteItem> res;
            if (orden == "codigo")
                res = this.Servicio.ReporteCarpetaCatastralCorte(pFechaCorte, concepto, año).OrderBy(o => o.codigo).OrderBy(o => o.nombres).OrderBy(o => o.concepto_id);
            else
                res = this.Servicio.ReporteCarpetaCatastralCorte(pFechaCorte, concepto, año).OrderBy(o => o.nombres).OrderBy(o => o.codigo).OrderBy(o => o.concepto_id);
            return res;
        }

        public IEnumerable<RepEmisionesSaldoDto> EmisionesSaldo(int concepto)
        {
            if (concepto > 0)
                return this.Servicio.ReadRepEmisionesSaldosFiltered("denominacion, año, estadoden", String.Format("concepto = {0}", concepto));
            else
                return this.Servicio.ReadRepEmisionesSaldosFiltered("denominacion, año, estadoden", "");
        }

        public IEnumerable<EmisionesSaldosItem> EmisionesSaldoFecha(int pConcepto, DateTime pFechaCorte, Boolean pEmisionesNuevas)
        {
            if (pEmisionesNuevas)
            {
                int añoc = pFechaCorte.Year;
                if (pConcepto > 0)
                    return this.Servicio.ReporteEmisionesSaldoFecha(pFechaCorte).Where(c => c.concepto_id == pConcepto && c.año <= añoc);
                else
                    return this.Servicio.ReporteEmisionesSaldoFecha(pFechaCorte).Where(c => c.año <= añoc);
            }
            else
            {
                if (pConcepto > 0)
                    return this.Servicio.ReporteEmisionesSaldoFecha(pFechaCorte).Where(c => c.concepto_id == pConcepto);
                else
                    return this.Servicio.ReporteEmisionesSaldoFecha(pFechaCorte);
            }
        }
        
        //public IEnumerable<Intelligob.Cliente.Referencia.>

        #endregion

        #region Contribucion especial de Mejoras

        /// <summary>
        /// Expandir un elemento de mejora cargando su predio y contribuyente
        /// </summary>
        /// <param name="pElemento">Elmento a ser expandido</param>
        private void ExpandirMejoraElemento(MejoraElementoDto pElemento)
        {
            if (pElemento != null)
            {
                if (pElemento.Predio != null && pElemento.Predio > 0)
                    pElemento.PredioNav = this.Servicio.ReadPredioBase(String.Format(this.FormatoClave, pElemento.Predio));
                if (pElemento.Contribuyente != null && pElemento.Contribuyente > 0)
                    pElemento.ContribuyenteNav = this.Servicio.ReadContribuyente(String.Format(this.FormatoClave, pElemento.Contribuyente));
            }
        }

        private IEnumerable<MejoraDto> MejorasPorEstado(int pEstado)
        {
            if (pEstado == 9)
                return this.Servicio.ReadMejoras();
            else
                return this.Servicio.ReadMejorasFiltered("", String.Format("estado = {0}", pEstado));
        }

        private IEnumerable<MejoraElementoDto> ElementosPorMejoraEstado(int pMejora, int pEstado)
        {
            IEnumerable<MejoraElementoDto> eles = null;

            if (pEstado == 9)
                eles = this.Servicio.ReadMejoraElementosFiltered("", String.Format("mejora = {0}", pMejora));
            else
                eles = this.Servicio.ReadMejoraElementosFiltered("", String.Format("mejora = {0} and estado = {1}", pMejora, pEstado));

            foreach(MejoraElementoDto e in eles)
            {
                this.ExpandirMejoraElemento(e);
            }

            return eles;
        }

        #endregion
    
        public int CrearPlanillaMercado(PlanillaDto p, PlanillaRubroDto r, PlanillaMesDto m, List<PlanillaAtributoDto> ats, List<Intelligob.Cliente.Modelos.CobroRap> cobs, int local)
        {
            int ret = 0;

            if (p.Id > 0)
            {
                ret = p.Id;
            }
            else
            {
                p.Usuario = 0;
                String sid = Servicio.CreatePlanilla(p);
                sid = sid.Replace("Id=", "");
                ret = Convert.ToInt32(sid);
                r.Planilla = ret;
                m.Planilla = ret;
                Servicio.CreatePlanillaRubro(r);
                Servicio.CreatePlanillaMes(m);

                foreach (PlanillaAtributoDto a in ats)
                {
                    a.Planilla = ret;
                    Servicio.CreatePlanillaAtributo(a);
                }
            }

            foreach(CobroRap c in cobs)
            {
                CobroDto cob = new CobroDto();
                cob.Fecha = c.Fecha;
                cob.FormaPago = 1;
                if (SesionUtiles.Instance.EsDesarrollador)
                    cob.Usuario = 1;
                else
                    cob.Usuario = SesionUtiles.Instance.UsuarioActivo.Id;
                cob.Valor = c.Valor;
                cob.Estado = 0;
                String cid = Servicio.CreateCobro(cob);
                cid = cid.Replace("Id=", "");
                int coi = Convert.ToInt32(cid);

                CobrosElementoDto ele = new CobrosElementoDto();
                ele.Cobro = coi;
                ele.Entidad = 1;
                ele.FormaPago = 1;
                ele.Valor = c.Valor;
                ele.Estado = 0;
                Servicio.CreateCobrosElemento(ele);

                CobroTransaccionDto tra = new CobroTransaccionDto();
                tra.Cobro = coi;
                tra.SoporteNumero = c.Numero;
                tra.Transaccion = ret;
                tra.Valor = c.Valor;
                tra.Estado = 0;
                tra.Rebajas = local;
                tra.Recargos = 0;
                Servicio.CreateCobroTransaccion(tra);

                CobrosRubroDto rub = new CobrosRubroDto();
                rub.Cobro = coi;
                rub.Rubro = 45;
                rub.Referencia = ret;
                rub.Rebajas = 0;
                rub.Origen = 1;
                rub.Valor = c.Valor;
                Servicio.CreateCobrosRubro(rub);

                PlanillaDto pl = Servicio.ReadPlanilla(String.Format(FormatoClave, ret));
                if (pl != null)
                {
                    if (pl.Pagos != null)
                        pl.Pagos = pl.Pagos + c.Valor;
                    else
                        pl.Pagos = c.Valor;
                    Servicio.UpdatePlanilla(pl);
                }
            }

            return ret;
        }

        public void PagarPlanillaMercado(int loc, int mid, int num, double val, DateTime fec)
        {
            List<PlanillaDto> pls = Servicio.ReadPlanillasFiltered("", String.Format("concepto = 14 and estado = 0 and servicio = {0}", mid)).ToList();
            if (pls.Count > 0)
            {
                int a = pls.Min(m => m.Año).Value;
                PlanillaDto px = pls.Where(w => w.Año == a).FirstOrDefault();
                pls.Remove(px);
                double pg = 0;
                double sal = 0;
                if (px.Saldo >= val)
                {
                    pg = val;
                }
                else
                {
                    pg = px.Saldo;
                    sal = val - pg;
                }
                CobroDto coba = new CobroDto
                {
                    Fecha = fec,
                    FormaPago = 1,
                    Valor = pg,
                    Estado = 0
                };
                if (SesionUtiles.Instance.EsDesarrollador)
                    coba.Usuario = 1;
                else
                    coba.Usuario = SesionUtiles.Instance.UsuarioActivo.Id;
                
                String cida = Servicio.CreateCobro(coba);
                cida = cida.Replace("Id=", "");
                int coia = Convert.ToInt32(cida);

                CobrosElementoDto elea = new CobrosElementoDto
                {
                    Cobro = coia,
                    Entidad = 1,
                    FormaPago = 1,
                    Valor = pg,
                    Estado = 0
                };
                Servicio.CreateCobrosElemento(elea);

                CobroTransaccionDto trax = new CobroTransaccionDto
                {
                    Cobro = coia,
                    SoporteNumero = num,
                    Transaccion = px.Id,
                    Valor = pg,
                    Estado = 0,
                    Rebajas = mid,
                    Recargos = 0
                };
                Servicio.CreateCobroTransaccion(trax);

                CobrosRubroDto ruba = new CobrosRubroDto();
                ruba.Cobro = coia;
                ruba.Rubro = 45;
                ruba.Referencia = px.Id;
                ruba.Rebajas = 0;
                ruba.Origen = 1;
                ruba.Valor = pg;
                Servicio.CreateCobrosRubro(ruba);

                PlanillaDto plax = Servicio.ReadPlanilla(String.Format(FormatoClave, px.Id));
                if (plax != null)
                {
                    if (plax.Pagos != null)
                        plax.Pagos = plax.Pagos + pg;
                    else
                        plax.Pagos = pg;
                    if (plax.Total == plax.Pagos)
                        plax.Estado = 1;
                    Servicio.UpdatePlanilla(plax);
                }
                while (sal > 0)
                {
                    if (pls.Count > 0)
                    {
                        int a1 = pls.Min(m => m.Año).Value;
                        PlanillaDto p1 = pls.Where(w => w.Año == a1).FirstOrDefault();
                        if (p1.Saldo >= sal)
                        {
                            pg = sal;
                            sal = 0;
                        }
                        else
                        {
                            pg = p1.Saldo;
                            sal = sal - pg;
                        }
                        CobroDto cob1 = new CobroDto();
                        cob1.Fecha = fec;
                        cob1.FormaPago = 1;
                        if (SesionUtiles.Instance.EsDesarrollador)
                            cob1.Usuario = 1;
                        else
                            cob1.Usuario = SesionUtiles.Instance.UsuarioActivo.Id;
                        cob1.Valor = pg;
                        cob1.Estado = 0;
                        String cid1 = Servicio.CreateCobro(cob1);
                        cid1 = cid1.Replace("Id=", "");
                        int coi1 = Convert.ToInt32(cid1);

                        CobrosElementoDto ele1 = new CobrosElementoDto();
                        ele1.Cobro = coi1;
                        ele1.Entidad = 1;
                        ele1.FormaPago = 1;
                        ele1.Valor = pg;
                        ele1.Estado = 0;
                        Servicio.CreateCobrosElemento(ele1);

                        CobroTransaccionDto tra1 = new CobroTransaccionDto();
                        tra1.Cobro = coi1;
                        tra1.SoporteNumero = num;
                        tra1.Transaccion = p1.Id;
                        tra1.Valor = pg;
                        tra1.Estado = 0;
                        tra1.Rebajas = mid;
                        tra1.Recargos = 0;
                        Servicio.CreateCobroTransaccion(tra1);

                        CobrosRubroDto rub1 = new CobrosRubroDto();
                        rub1.Cobro = coi1;
                        rub1.Rubro = 45;
                        rub1.Referencia = p1.Id;
                        rub1.Rebajas = 0;
                        rub1.Origen = 1;
                        rub1.Valor = pg;
                        Servicio.CreateCobrosRubro(rub1);

                        PlanillaDto pl1 = Servicio.ReadPlanilla(String.Format(FormatoClave, p1.Id));
                        if (pl1 != null)
                        {
                            if (pl1.Pagos != null)
                                pl1.Pagos = pl1.Pagos + pg;
                            else
                                pl1.Pagos = pg;
                            if (pl1.Pagos == pl1.Total)
                                pl1.Estado = 1;
                            Servicio.UpdatePlanilla(pl1);
                        }
                        pls.Remove(p1);
                    }
                }
            }
        }

        public void RegistrarTicketMercado(int loc, int mid, int num, double val, DateTime fec)
        {
            CobroDto coba = new CobroDto
            {
                Fecha = fec,                
                FormaPago = mid, // Id del puesto de mercado
                Valor = val,
                FormaPagoAtributos = num.ToString(),
                Estado = 3
            };
            if (SesionUtiles.Instance.EsDesarrollador)
                coba.Usuario = 1;
            else
                coba.Usuario = SesionUtiles.Instance.UsuarioActivo.Id;

            /*string cida =*/ Servicio.CreateCobro(coba);
            //cida = cida.Replace("Id=", "");
            //int coia = Convert.ToInt32(cida);
            
            // Crear el registro de ticket no en cobro
            // Se requiere saber el numero de ticket, la fecha de cobro, numero de local y el valor
            // Debo tomar le parametro de valor mensual de mercado
            // Paramertros: # de local, Id del local, # del ticket, Valor recaudado, Fecha de recaudacion
        }

        public IEnumerable<CobroDto> TicketsCompletos()
        {
            //Servicio.CobrosCountFiltered("");
            return Servicio.ReadCobrosFiltered("", "");
            //return null;
        }

        public List<CobroRap> CuentaCorrienteMercado(int puesto)
        {
            List<CobroRap> res = new List<CobroRap>();
            MercadoDto mc = Servicio.ReadMercadosFiltered("", String.Format("puesto = {0}", puesto)).FirstOrDefault();
            if (mc != null)
            {                
                IEnumerable<PlanillaDto> pls = Servicio.ReadPlanillasFiltered("", String.Format("concepto = 14 and estado = 0 and servicio = {0}", mc.Id));
                foreach(PlanillaDto p in pls)
                {
                    if (p != null)
                    {
                        if (!String.IsNullOrWhiteSpace(p.Contribuyentes))
                        {
                            int i = 1;
                            string cons = String.Empty;
                            string[] ss = p.Contribuyentes.Trim().Split(']');
                            foreach (String s in ss)
                            {
                                if (i <= 2)
                                {
                                    if (!String.IsNullOrWhiteSpace(s))
                                    {
                                        String m = s.Trim().Substring(1);
                                        ContribuyenteDto c = this.Servicio.ReadContribuyente(String.Format(this.FormatoClave, m));
                                        if (cons.Length > 0)
                                            cons = cons + ", ";
                                        if (c != null)
                                            cons = cons + c.Nombres;
                                    }
                                }
                                else
                                {
                                    cons = cons + String.Format("... {0} mas", ss.Length - 3);
                                    break;
                                }
                                i = i + 1;
                            }
                            p.ContribuyentesCadena = cons;
                        }
                        CobroRap r = new CobroRap()
                        {
                            Id = p.Id,
                            Denominacion = p.ContribuyentesCadena,
                            Valor = (double)p.Total,
                            Mes = puesto,
                            Referencia = p.Id,
                            Fecha = new DateTime((int)p.Año, 1, 1),
                            Indice = (int)p.Año,
                            Adicional = 0
                        };
                        res.Add(r);

                        IEnumerable<CobroTransaccionDto> cts = Servicio.ReadCobroTransaccionsFiltered("", String.Format("transaccion = {0}", p.Id));
                        foreach(CobroTransaccionDto ct in cts)
                        {
                            CobroDto cob = Servicio.ReadCobro(String.Format(FormatoClave, ct.Cobro));
                            CobroRap cobr = new CobroRap()
                            {
                                Id = ct.Id,
                                Referencia = r.Id,
                                Denominacion = p.ContribuyentesCadena,
                                Valor = 0,
                                Mes = 0,
                                Indice = (int)ct.SoporteNumero,
                                Fecha = (DateTime)cob.Fecha,
                                Adicional = (double)ct.Valor,
                            };
                            res.Add(cobr);
                        }
                    }
                }
            }
            return res.OrderBy(o => o.Fecha).ToList();
        }

        public PlanillaDto PlanillaMercadoPorAñoLocal(int anio, int local)
        {
            return Servicio.ReadPlanillasFiltered("", String.Format("concepto = 14 and año = {0} and servicio = {1}", anio, local)).FirstOrDefault();
        }

        public MercadoDto MercadoPuestoPorNumero(int pNum)
        {
            MercadoDto m = Servicio.ReadMercados().Where((w) => w.Puesto == pNum).FirstOrDefault();
            if (m != null && m.Contribuyente > 0)
            {
                ContribuyenteDto c = this.Servicio.ReadContribuyente(String.Format(this.FormatoClave, m.Contribuyente));
                m.ContribuyenteNav = c;
            }
            return m;
        }
    
    }    
}
