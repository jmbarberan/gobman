using System;
using System.Collections.Generic;
using System.Linq;
using Intelligob.Cliente.Referencia;
using Intelligob.Cliente.Modelos;

namespace Intelligob.Cliente.Depositos
{
    public class RecaudacionesDep : DepositoBase
    {
        public RecaudacionesDep() : base(DepositosControl.Instance.Servicio) { }

        public RecaudacionesDep(IEntidades servicio) : base(servicio) { }

        public virtual PlanillaDto PlanillaPorEmision(int pAño, int pConcepto, int pServicio)
        {
            return this.Servicio.ReadPlanillasFiltered("", String.Format("año = {0} and concepto = {1} and servicio = {2} and estado = 0", pAño, pConcepto, pServicio)).FirstOrDefault();
        }

        /// <summary>
        /// Expandir titulos con los atributos asociados
        /// </summary>
        /// <param name="pla">planilla a expandir</param>
        public virtual void ExpandirPlanilla(PlanillaDto pla)
        {
            if (pla.Concepto != null && pla.Concepto > 0)
                pla.ConceptoNav = ModeloCache.Instance.McConceptos.Where(c => c.Id == pla.Concepto).FirstOrDefault(); //this.Servicio.ReadConcepto(String.Format(this.FormatoClave, pla.Concepto));
            var rq = Servicio.ReadPlanillaAtributosFiltered("", "planilla = " + pla.Id.ToString());
            pla.AtributosNav = rq; //.Where(a => a.Planilla == pla.Id).ToArray();
            pla.RubrosNav = Servicio.ReadPlanillaRubrosFiltered("", string.Format("estado = 0 and planilla = {0}", pla.Id)); //.Where(a => a.Planilla == pla.Id).ToArray();
            if (pla.RubrosNav.Count() > 0)
            {
                foreach (PlanillaRubroDto prub in pla.RubrosNav)
                {
                    if (prub.Rubro != null && prub.Rubro > 0)
                    {
                        prub.RubroNav = ModeloCache.Instance.McRubros.Where(r => r.Id == prub.Rubro).FirstOrDefault();
                    }
                }
            }


            if (!String.IsNullOrWhiteSpace(pla.Contribuyentes))
            {
                int i = 1;
                string cons = String.Empty;
                //string[] ss = pla.Contribuyentes.Split(new char[] { '[', ']' });
                string[] ss = pla.Contribuyentes.Trim().Split(']');
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
                pla.ContribuyentesCadena = cons;
            }
        }

        private void CalcularAdicionales(PlanillaDto pla)
        {
            DateTime hoy = this.HoyServidor();
            Boolean descAncianoAplica = false;
            Boolean descArtesanoAplica = false;
            Boolean descDiscapacidadAplica = false;
            double descAnciano = 0;
            double fracAnciano = 0;
            double descDiscapacidad = 0;
            double fracDiscapacidad = 0;
            double descArtesano = 0;
            double fracArtesano = 0;
            double descProntoPago = 0;
            List<int> pContribuyentes = new List<int>();
            String scons = pla.Contribuyentes.Substring(1, pla.Contribuyentes.Length - 2);
            String[] sacons = scons.Split(']');
            foreach (string c in sacons)
            {
                if (!String.IsNullOrWhiteSpace(c.Trim()))
                {
                    String m = c.Trim();
                    if (m.Contains("["))
                        m = m.Substring(1);
                    pContribuyentes.Add(Convert.ToInt32(m));
                }
            }
            List<PlanillaRubroDto> rubsadi = new List<PlanillaRubroDto>();

            #region Obtener procentajes de descuento
            if (pContribuyentes.Count > 0)
            {
                foreach (int cont in pContribuyentes)
                {
                    ContribuyentesRebajaDto cra = this.Servicio.ReadContribuyentesRebajasFiltered("", String.Format("contribuyente == {0} && rebaja == 1", cont)).FirstOrDefault();
                    if (cra != null)
                    {
                        descAncianoAplica = true;
                        RebajaDto ra = this.Servicio.ReadRebaja(String.Format(this.FormatoClave, 1));
                        if (ra != null && ra.Fraccion != null && ra.Fraccion > 0)
                            fracAnciano = (double)ra.Fraccion;
                    }

                    ContribuyentesRebajaDto crt = this.Servicio.ReadContribuyentesRebajas().Where(rebart => rebart.Contribuyente == cont && rebart.Rebaja == 3).FirstOrDefault();
                    if (crt != null)
                    {
                        descArtesanoAplica = true;
                        RebajaDto rs = this.Servicio.ReadRebaja(String.Format(this.FormatoClave, 3));
                        if (rs != null && rs.Fraccion != null && rs.Fraccion > 0)
                            fracArtesano = (double)rs.Fraccion;
                    }

                    ContribuyentesRebajaDto cr = this.Servicio.ReadContribuyentesRebajas().Where(c => c.Contribuyente == cont && c.Rebaja == 2).FirstOrDefault(); // Discapacidad
                    if (cr != null && cr.Fraccion != null && cr.Fraccion > 0)
                    {
                        descDiscapacidadAplica = true;
                        fracDiscapacidad = (double)cr.Fraccion;
                    }
                }
            }
            #endregion

            CoeficienteElementoDto eledesc = this.Servicio.ReadCoeficienteElementos().Where(e => e.Coeficiente == 3 && e.Clave == hoy.Month && e.EnteroDesde <= hoy.Day && e.EnteroHasta >= hoy.Day).FirstOrDefault();

            foreach (PlanillaRubroDto pr in pla.RubrosNav)
            {
                if (pr.Valor > 0)
                {
                    double desrubro = 0;

                    #region Calcular ley del Anciano
                    if (pr.RubroNav.RebajasCodigos != null && pr.RubroNav.RebajasCodigos.Contains("2.1")) // Ley del anciano (en rebaja)
                    {
                        if (descAncianoAplica)
                        {
                            // Comprobar si el rubro tiene descuento anciano personalizado
                            Double fraccionAnciano = fracAnciano;
                            Double dscAncianoParcial = 0;
                            RebajasRubroDto rrf = this.Servicio.ReadRebajasRubrosFiltered("", String.Format("rebaja = 1 and rubro = {0} and tipo = 1 and estado = 0", pr.Rubro)).FirstOrDefault();
                            if (rrf != null && rrf.Valor != null && rrf.Valor > 0)
                                fraccionAnciano = (double)rrf.Valor;
                            if (fraccionAnciano > 0)
                            {
                                // Verificar el saldo del rubro                                
                                if (pla.ConceptoNav.PagosParciales == true)
                                {
                                    double saldo = (double)(pr.Valor - pr.Pagos - pr.Rebajas);
                                    dscAncianoParcial = (saldo * fraccionAnciano) / 100;
                                    pr.Rebaja = dscAncianoParcial;
                                }
                                else
                                    dscAncianoParcial = (((double)pr.Valor * fraccionAnciano) / 100);

                                // Verificar si el rubro tiene tope
                                RebajasRubroDto rebrub = this.Servicio.ReadRebajasRubrosFiltered("", String.Format("rebaja = 1 and rubro = {0} and tipo = 2 and estado = 0", pr.Rubro)).FirstOrDefault();
                                if (rebrub != null && rebrub.Valor > 0)
                                {
                                    if (dscAncianoParcial > rebrub.Valor)
                                        dscAncianoParcial = (double)rebrub.Valor;
                                }

                                descAnciano = descAnciano + dscAncianoParcial;
                                desrubro = desrubro + dscAncianoParcial;
                            }
                        }
                    }
                    #endregion

                    #region Calcular Rebaja por Discapacidad
                    if (pr.RubroNav.RebajasCodigos != null && pr.RubroNav.RebajasCodigos.Contains("2.2")) // Discapacidad (en contribuyente)
                    {
                        if (descDiscapacidadAplica)
                        {
                            Double dscDisParcial = 0;
                            if (fracDiscapacidad > 0)
                            {
                                // Verificar el saldo del rubro                                
                                if (pla.ConceptoNav.PagosParciales == true)
                                {
                                    double saldo = (double)(pr.Valor - pr.Pagos - pr.Rebajas);
                                    dscDisParcial = (saldo * fracDiscapacidad) / 100;
                                    pr.Rebaja = dscDisParcial;
                                }
                                else
                                    dscDisParcial = (((double)pr.Valor * fracDiscapacidad) / 100);

                                // Verificar si el rubro tiene tope
                                RebajasRubroDto rebrub = this.Servicio.ReadRebajasRubrosFiltered("", String.Format("rebaja = 2 and rubro = {0} and tipo = 2 and estado = 0", pr.Rubro)).FirstOrDefault();
                                if (rebrub != null && rebrub.Valor > 0)
                                {
                                    if (dscDisParcial > rebrub.Valor)
                                        dscDisParcial = (double)rebrub.Valor;
                                }

                                descDiscapacidad = descDiscapacidad + dscDisParcial;
                                desrubro = desrubro + dscDisParcial;

                                /*double rubval = (double)pr.Valor;
                                 if (desrubro > 0)
                                     rubval = rubval - desrubro;
                                 descDiscapacidad = descDiscapacidad + ((rubval * fracDiscapacidad) / 100);
                                 desrubro = desrubro + ((rubval * fracDiscapacidad) / 100);*/
                            }
                        }
                    }
                    #endregion

                    #region Calcular descuento por ley del artesano
                    if (pr.RubroNav.RebajasCodigos != null && pr.RubroNav.RebajasCodigos.Contains("2.3")) // Artesano (en rebaja)
                    {
                        if (descArtesanoAplica)
                        {
                            Double fraccionArtesano = fracArtesano;
                            Double dscArtesanoParcial = 0;
                            RebajasRubroDto rrf = this.Servicio.ReadRebajasRubrosFiltered("", String.Format("rebaja = 3 and rubro = {0} and tipo = 1 and estado = 0", pr.Rubro)).FirstOrDefault();
                            if (rrf != null && rrf.Valor != null && rrf.Valor > 0)
                                fraccionArtesano = (double)rrf.Valor;
                            if (fraccionArtesano > 0)
                            {
                                // Verificar el saldo del rubro                                
                                if (pla.ConceptoNav.PagosParciales == true)
                                {
                                    double saldo = (double)(pr.Valor - pr.Pagos - pr.Rebajas);
                                    dscArtesanoParcial = (saldo * fraccionArtesano) / 100;
                                    pr.Rebaja = dscArtesanoParcial;
                                }
                                else
                                    dscArtesanoParcial = (((double)pr.Valor * fraccionArtesano) / 100);

                                RebajasRubroDto rebrub = this.Servicio.ReadRebajasRubrosFiltered("", String.Format("rebaja = 3 and rubro = {0} and tipo = 2 and estado = 0", pr.Rubro)).FirstOrDefault();
                                if (rebrub != null && rebrub.Valor > 0)
                                {
                                    if (dscArtesanoParcial > rebrub.Valor)
                                        dscArtesanoParcial = (double)rebrub.Valor;
                                }

                                descArtesano = descArtesano + dscArtesanoParcial;
                                desrubro = desrubro + dscArtesanoParcial;
                            }
                        }
                    }
                    #endregion

                    #region Calcular Descuento pronto pago
                    if (pr.RubroNav.RebajasCodigos != null && pr.RubroNav.RebajasCodigos.Contains("2.0")) // Pronto pago
                    {
                        if (pla.Año == hoy.Year && hoy.Month <= 6)
                        {
                            if (pr.Valor != null && pr.Valor > 0)
                            {
                                if (eledesc != null)
                                {
                                    double rubval = (double)pr.Valor;
                                    if (desrubro > 0)
                                        rubval = rubval - desrubro;
                                    if (rubval > 0)
                                    {
                                        descProntoPago = descProntoPago + ((rubval * (double)eledesc.Valor) / 100);
                                    }

                                }
                            }
                        }
                    }
                    #endregion

                    pr.Rebaja = desrubro;
                }

            }

            #region Respaldar rabaja anterior
            foreach (PlanillaRubroDto prab in pla.RubrosNav)
            {
                if (prab.Rubro == 4 || prab.Rubro == 5 || prab.Rubro == 29)
                    prab.Rebaja = Convert.ToDouble(prab.Valor);
            }
            #endregion

            #region Insertar o actualizar rubro de Descuento por ley del anciano
            if (descAnciano > 0)
            {
                Boolean ins = false;
                foreach (PlanillaRubroDto pra in pla.RubrosNav)
                {
                    if (pra.Rubro == 4)
                    {
                        ins = true;
                        pra.Valor = pra.Valor + Math.Round(descAnciano, 2);
                        break;
                    }
                }

                if (!ins)
                {
                    PlanillaRubroDto reb = new PlanillaRubroDto();
                    reb.Valor = Math.Round(descAnciano, 2);
                    reb.Planilla = pla.Id;
                    reb.Estado = 0;
                    reb.Id = 0;
                    reb.Origen = -1;
                    reb.Rubro = 4;
                    reb.RubroNav = this.Servicio.ReadRubro(String.Format(this.FormatoClave, 4));
                    rubsadi.Add(reb);
                }

                pla.Rebajas = pla.Rebajas + descAnciano;
            }
            #endregion

            #region Insertar o actualizar rubro de Rebaja por discapacidad
            if (descDiscapacidad > 0)
            {
                Boolean ins = false;
                foreach (PlanillaRubroDto pra in pla.RubrosNav)
                {
                    if (pra.Rubro == 5)
                    {
                        ins = true;
                        pra.Valor = pra.Valor + Math.Round(descDiscapacidad, 2);
                        break;
                    }
                }

                if (!ins)
                {
                    PlanillaRubroDto reb = new PlanillaRubroDto();
                    reb.Valor = Math.Round(descDiscapacidad, 2);
                    reb.Planilla = pla.Id;
                    reb.Estado = 0;
                    reb.Id = 0;
                    reb.Origen = -1;
                    reb.Rubro = 5;
                    reb.RubroNav = this.Servicio.ReadRubro(String.Format(this.FormatoClave, 5));
                    rubsadi.Add(reb);
                }

                pla.Rebajas = pla.Rebajas + descDiscapacidad;
            }
            #endregion

            #region Insertar o actualizar rubro de Descuento por Artesano
            if (descArtesano > 0)
            {
                Boolean ins = false;
                foreach (PlanillaRubroDto pra in pla.RubrosNav)
                {
                    if (pra.Rubro == 29)
                    {
                        ins = true;
                        pra.Valor = pra.Valor + Math.Round(descArtesano, 2);
                        break;
                    }
                }

                if (!ins)
                {
                    PlanillaRubroDto reb = new PlanillaRubroDto();
                    reb.Valor = Math.Round(descArtesano, 0);
                    reb.Planilla = pla.Id;
                    reb.Estado = 0;
                    reb.Id = 0;
                    reb.Origen = -1;
                    reb.Rubro = 19;
                    reb.RubroNav = this.Servicio.ReadRubro(String.Format(this.FormatoClave, 19));
                    rubsadi.Add(reb);
                }

                pla.Rebajas = pla.Rebajas + descArtesano;
            }
            #endregion

            #region Insertar Descuento por pronto pago
            if (descProntoPago > 0)
            {
                PlanillaRubroDto reb = new PlanillaRubroDto();
                reb.Valor = Math.Round(descProntoPago, 2);
                reb.Planilla = pla.Id;
                reb.Estado = 0;
                reb.Id = 0;
                reb.Origen = -1;
                reb.Rubro = 2;
                reb.RubroNav = this.Servicio.ReadRubro(String.Format(this.FormatoClave, 2));
                pla.Rebajas = pla.Rebajas + descProntoPago;
                rubsadi.Add(reb);
            }
            #endregion

            //pla.Rebajas = pla.Rebajas + (descAnciano + descDiscapacidad + descArtesano + descProntoPago);

            #region Calcular intereses
            if ((pla.Año < hoy.Year) || (pla.Año == hoy.Year && hoy.Month >= 7))
            {
                if (pla.ConceptoNav.RecargosCodigos != null && pla.ConceptoNav.RecargosCodigos.Contains("1.1"))
                {
                    CoeficientesDep cd = new CoeficientesDep();
                    CoeficienteElementoDto cp = cd.CoeficienteElementoPorClave(2, (int)pla.Año);
                    if (cp != null && cp.Valor > 0)
                    {
                        Double porc = (double)cp.Valor;
                        PlanillaRubroDto recg = new PlanillaRubroDto();
                        recg.Valor = Math.Round((pla.Saldo * porc / 100), 2);
                        recg.Planilla = pla.Id;
                        recg.Estado = 0;
                        recg.Id = 0;
                        recg.Origen = 1;
                        recg.Rubro = 1;
                        recg.RubroNav = this.Servicio.ReadRubro(String.Format(this.FormatoClave, 1));
                        pla.Recargos = recg.Valor;
                        rubsadi.Add(recg);
                    }

                }
            }
            #endregion

            if (rubsadi.Count > 0)
            {
                List<PlanillaRubroDto> rbs = pla.RubrosNav.ToList();
                rbs.AddRange(rubsadi);
                pla.RubrosNav = rbs.ToArray();
            }
        }

        /// <summary>
        /// Traer las planillas adeudadas por un contribuyente
        /// </summary>
        /// <param name="pCon">Id del contribuyente a consultar</param>
        /// <returns></returns>
        public virtual IEnumerable<PlanillaDto> TraerDeudaContribuyente(int pCon)
        {
            IEnumerable<PlanillaDto> plas = this.Servicio.ReadPlanillasFiltered("", String.Format("contribuyentes.contains(\"[{0}]\") and estado = 0", pCon))
                .OrderBy(p => p.Concepto)
                .OrderBy(p => p.Codigo)
                .OrderBy(p => p.Año);

            foreach (PlanillaDto pla in plas)
            {
                try
                {
                    ExpandirPlanilla(pla);
                    List<int> cons = new List<int>();
                    cons.Add(pCon);
                    CalcularAdicionales(pla);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            return plas;
        }

        /// <summary>
        /// Traer las planillas adeudadas de un codigo dado
        /// </summary>
        /// <param name="pCodigo">Codigo a consultar</param>
        /// <returns></returns>
        public virtual IEnumerable<PlanillaDto> TraerDeudaCodigo(string pCodigo)
        {
            IEnumerable<PlanillaDto> plas = Servicio.ReadPlanillasFiltered("", string.Format("codigo = \"{0}\" and estado = 0", pCodigo))
                .OrderBy(p => p.Contribuyentes)
                .OrderBy(p => p.Concepto)
                .OrderBy(p => p.Codigo)
                .OrderBy(p => p.Año);

            foreach (PlanillaDto pla in plas)
            {
                ExpandirPlanilla(pla);
                CalcularAdicionales(pla);
            }
            return plas;
        }

        public virtual IEnumerable<PlanillaDto> TraerDeudaParaBaja(int pContribuyente, int pAño, String pCodigo, int pConcepto)
        {
            string filtro = "estado = 0";
            if (pContribuyente > 0)
            {
                filtro = filtro + " and contribuyentes.contains(\"[" + Convert.ToString(pContribuyente) + "]\")";
            }

            if (pConcepto > 0)
            {
                filtro = filtro + String.Format(" and concepto = {0}", pConcepto);
            }

            if (pAño > 0)
            {
                filtro = filtro + " and año = " + Convert.ToString(pAño);
            }

            if (!String.IsNullOrWhiteSpace(pCodigo))
            {
                filtro = filtro + " and codigo = \"" + pCodigo + "\"";
            }
            IEnumerable<PlanillaDto> plas = this.Servicio.ReadPlanillasFiltered("", filtro)
                .OrderBy(p => p.Contribuyentes)
                .OrderBy(p => p.Concepto)
                .OrderBy(p => p.Codigo)
                .OrderBy(p => p.Año);

            foreach (PlanillaDto pla in plas)
            {
                ExpandirPlanilla(pla);
            }
            return plas;
        }

        public virtual void MarcarPlanilla(PlanillaDto pla)
        {
            int i = -1;
            if (pla.ConceptoNav == null)
                pla.ConceptoNav = this.Servicio.ReadConcepto(String.Format(this.FormatoClave, pla.Concepto));

            if (pla.ConceptoNav != null && pla.ConceptoNav.Consecutivo != null)
                i = (int)pla.ConceptoNav.Consecutivo;

            pla.Estado = 1;
            pla.FechaCancelacion = this.Servicio.Hoy();
            pla.SoporteNumero = SoporteNumero(i, true);
            pla.Pagos = pla.Saldo;
            foreach (PlanillaRubroDto rub in pla.RubrosNav)
            {
                if (rub.Id <= 0)
                {
                    this.Servicio.CreatePlanillaRubro(rub);
                }
            }
            this.Servicio.UpdatePlanilla(pla);

            String conceptoDen = String.Empty;
            if (pla.ConceptoNav != null)
                conceptoDen = pla.ConceptoNav.Denominacion;
            else
            {
                ConceptoDto cp = ModeloCache.Instance.McConceptos.Where(o => o.Id == pla.Concepto).FirstOrDefault();
                if (cp != null)
                    conceptoDen = cp.Denominacion;
            }

            Utiles.TablaCadena tb = new Utiles.TablaCadena();
            String enc = "Marcacion de planilla\r\n";
            enc = enc + "===================\r\n";
            tb.AddRow("Atributo     ", "Valor ");
            tb.AddRow("-------------", "----->");
            tb.AddRow("Id", pla.Id);
            tb.AddRow("Codigo", pla.Codigo);
            tb.AddRow("Contribuyente", pla.ContribuyentesCadena);
            tb.AddRow("Año", pla.Año);
            tb.AddRow("Concepto", conceptoDen);
            tb.AddRow("Soporte No.", pla.SoporteNumero);
            tb.AddRow("Valor", String.Format("{0:n2}", pla.Total));
            tb.AddRow("Recargos", String.Format("{0:n2}", pla.Recargos));
            tb.AddRow("Rebajas", String.Format("{0:n2}", pla.Rebajas));
            tb.AddRow("Fecha cobro", String.Format("{0:d}", pla.FechaCancelacion));
            this.CrearSeguimiento(tb, "Operacion de Cobro de planilla", Utiles.EntidadesEnum.EnPlanilla, enc);
        }

        public virtual void MarcarPlanillaParcial(PlanillaDto pla)
        {
            if (pla.Saldo == pla.Parcial)
            {
                pla.FechaCancelacion = this.Servicio.Hoy();
                pla.Estado = 1;
            }
            else
            {
                pla.FechaCancelacion = null;
                pla.Estado = 0;
            }

            pla.Pagos = pla.Pagos + pla.Parcial;
            foreach (PlanillaRubroDto rub in pla.RubrosNav)
            {
                if (rub.Id <= 0)
                {
                    this.Servicio.CreatePlanillaRubro(rub);
                }
                else
                {
                    this.Servicio.UpdatePlanillaRubro(rub);
                }
            }
            this.Servicio.UpdatePlanilla(pla);

            String conceptoDen = String.Empty;
            if (pla.ConceptoNav != null)
                conceptoDen = pla.ConceptoNav.Denominacion;
            else
            {
                ConceptoDto cp = ModeloCache.Instance.McConceptos.Where(o => o.Id == pla.Concepto).FirstOrDefault();
                if (cp != null)
                    conceptoDen = cp.Denominacion;
            }

            Utiles.TablaCadena tb = new Utiles.TablaCadena();
            String enc = "Abono a planilla\r\n";
            enc = enc + "================\r\n";
            tb.AddRow("Atributo     ", "Valor ");
            tb.AddRow("-------------", "----->");
            tb.AddRow("Id", pla.Id);
            tb.AddRow("Codigo", pla.Codigo);
            tb.AddRow("Contribuyente", pla.ContribuyentesCadena);
            tb.AddRow("Año", pla.Año);
            tb.AddRow("Concepto", conceptoDen);
            //tb.AddRow("Soporte No.", pla.SoporteNumero);
            tb.AddRow("Valor", String.Format("{0:n2}", pla.Parcial));
            tb.AddRow("Recargos", String.Format("{0:n2}", pla.Recargos));
            tb.AddRow("Rebajas", String.Format("{0:n2}", pla.Rebajas));
            tb.AddRow("Fecha cobro", String.Format("{0:d}", pla.FechaCancelacion));

            this.CrearSeguimiento(tb, "Operacion de Cobro de planilla", Utiles.EntidadesEnum.EnPlanilla, enc);
        }

        public virtual int CobroRegistrar(CobroDto cob)
        {
            int id = this.CobroCrear(cob);
            foreach (CobrosElementoDto e in cob.ElementosNav)
            {
                e.Cobro = id;
                this.CobroElementoCrear(e);
            }

            foreach (CobroTransaccionDto t in cob.TransaccionesNav)
            {
                int i = -1;
                if (t.PlanillaNav == null)
                {
                    PlanillaDto p = this.Servicio.ReadPlanilla(String.Format(this.FormatoClave, t.Transaccion));
                    this.ExpandirPlanilla(p);
                    t.PlanillaNav = p;
                }

                if (t.PlanillaNav.ConceptoNav != null)
                {
                    if (t.PlanillaNav.ConceptoNav.Consecutivo != null)
                        i = (int)t.PlanillaNav.ConceptoNav.Consecutivo;
                }
                else
                {
                    if (t.PlanillaNav.Concepto != null && t.PlanillaNav.Concepto > 0)
                    {
                        ConceptoDto c = this.Servicio.ReadConcepto(String.Format(this.FormatoClave, t.PlanillaNav.Concepto));
                        if (c != null)
                            i = (int)c.Consecutivo;
                    }
                }

                t.Cobro = id;
                t.SoporteNumero = SoporteNumero(i, true);
                this.CobroTransaccionCrear(t);

                PlanillaDto pl = this.Servicio.ReadPlanilla(String.Format(this.FormatoClave, t.Transaccion));
                if (pl != null)
                    this.ExpandirPlanilla(pl);

                String conden = String.Empty;
                if (pl.ConceptoNav != null)
                    conden = pl.ConceptoNav.Denominacion;

                Utiles.TablaCadena tb = new Utiles.TablaCadena();
                String enc = "Creacion Cobro de titulo\r\n";
                enc = enc + "========================\r\n";
                tb.AddRow("Atributo        ", "Valor ");
                tb.AddRow("----------------", "----->");
                tb.AddRow("Id", id);
                tb.AddRow("Fecha", String.Format("{0:d}", cob.Fecha));
                tb.AddRow("Planilla Id", t.Transaccion);
                tb.AddRow("Concepto", conden);
                tb.AddRow("Codigo", pl.Codigo);
                tb.AddRow("Año", pl.Año);
                tb.AddRow("Contribuyente(s)", pl.ContribuyentesCadena);
                tb.AddRow("Valor", String.Format("{0:n2}", t.Valor));
                tb.AddRow("Recargos", String.Format("{0:n2}", t.Recargos));
                tb.AddRow("Rebajas", String.Format("{0:n2}", t.Rebajas));
                tb.AddRow("Soporte numero", t.SoporteNumero);
                this.CrearSeguimiento(tb, "Operacion de Cobro de planilla", Utiles.EntidadesEnum.EnCobroTransaccion, enc);
            }

            foreach (CobrosRubroDto r in cob.RubrosNav)
            {
                r.Cobro = id;
                this.Servicio.CreateCobrosRubro(r);
            }

            return id;
        }

        public virtual PlanillaDto PlanillaPagada(int pPlanilla)
        {
            PlanillaDto p = this.Servicio.ReadPlanillasFiltered("", String.Format(this.FormatoClave, pPlanilla)).FirstOrDefault();
            this.ExpandirPlanilla(p);
            return p;
        }

        public virtual IEnumerable<PlanillaDto> PlanillasPagadas(int pContribuyente, int pAño, String pCodigo, int pConcepto, DateTime pFechaInicio, DateTime pFechaCorte)
        {
            String filtro = "estado = 1";
            if (pContribuyente > 0)
            {
                filtro = filtro + " and contribuyentes.contains(\"[" + Convert.ToString(pContribuyente) + "]\")";
            }

            if (pAño > 0)
            {
                filtro = filtro + " and año = " + Convert.ToString(pAño);
            }

            if (!String.IsNullOrWhiteSpace(pCodigo))
            {
                filtro = filtro + " and codigo = \"" + pCodigo + "\"";
            }

            if (pFechaInicio != null)
            {
                filtro = filtro + " and fechaCancelacion >= DateTime.Parse(\"" + Convert.ToString(pFechaInicio) + "\")";
            }

            if (pFechaCorte != null)
            {
                filtro = filtro + " and fechaCancelacion <= DateTime.Parse(\"" + Convert.ToString(pFechaCorte) + "\")";
            }

            IEnumerable<PlanillaDto> plas = this.Servicio.ReadPlanillasFiltered("", filtro)
                .OrderBy(p => p.Contribuyentes)
                .OrderBy(p => p.Concepto)
                .OrderBy(p => p.Codigo)
                .OrderBy(p => p.Año);

            foreach (PlanillaDto pla in plas)
            {
                try
                {
                    ExpandirPlanilla(pla);
                }
                catch { }
            }

            return plas;
        }

        public virtual IEnumerable<PlanillaDto> PlanillasPagadas(int pContribuyente, int pAño, String pCodigo, int pConcepto)
        {
            String filtro = "pagos > 0";
            if (pContribuyente > 0)
            {
                filtro = filtro + " and contribuyentes.contains(\"[" + Convert.ToString(pContribuyente) + "]\")";
            }

            if (pConcepto > 0)
            {
                filtro = filtro + String.Format(" and concepto = {0}", pConcepto);
            }

            if (pAño > 0)
            {
                filtro = filtro + " and año = " + Convert.ToString(pAño);
            }

            if (!String.IsNullOrWhiteSpace(pCodigo))
            {
                filtro = filtro + " and codigo = \"" + pCodigo + "\"";
            }

            IEnumerable<PlanillaDto> plas = this.Servicio.ReadPlanillasFiltered("", filtro)
                .OrderBy(p => p.Contribuyentes)
                .OrderBy(p => p.Concepto)
                .OrderBy(p => p.Codigo)
                .OrderBy(p => p.Año);

            foreach (PlanillaDto pla in plas)
            {
                try
                {
                    ExpandirPlanilla(pla);
                }
                catch { }
            }

            return plas;
        }

        public virtual void PlanillaRevertirCobro(int? pId, bool pCobro, bool pMarcarSoporte, String pDescripcion)
        {
            if (pId > 0)
            {
                PlanillaDto p = this.Servicio.ReadPlanilla(String.Format(this.FormatoClave, pId));
                this.ExpandirPlanilla(p);
                String conden = String.Empty;
                if (p.ConceptoNav != null)
                    conden = p.ConceptoNav.Denominacion;
                p.Pagos = 0;
                p.Rebajas = 0;
                p.Recargos = 0;
                p.Estado = 0;
                p.FechaCancelacion = null;
                this.Servicio.UpdatePlanilla(p);

                if (pMarcarSoporte)
                {
                    SoporteMovimientoDto sm = new SoporteMovimientoDto();
                    sm.Id = 0;
                    sm.Soporte = 0;
                    sm.Fecha = p.FechaCancelacion;
                    sm.Numero = p.SoporteNumero;
                    sm.Referencia = p.Id;
                    sm.Movimiento = 3; // Movimientos de anulacion
                    sm.Estado = 0;
                    sm.Descripcion = pDescripcion;
                    this.Servicio.CreateSoporteMovimiento(sm);
                }

                IEnumerable<PlanillaRubroDto> rubs = this.Servicio.ReadPlanillaRubrosFiltered("", String.Format("planilla = {0} and (rubro = 1 or rubro = 2 or rubro = 4 or rubro = 5 or rubro = 29)", pId));
                if (rubs.Count() > 0)
                {
                    this.Servicio.DeletePlanillaRubros(rubs.ToArray());
                }

                if (pCobro)
                {
                    CobroTransaccionDto cot = this.Servicio.ReadCobroTransaccionsFiltered("", String.Format("transaccion = {0} and estado = 0", pId)).FirstOrDefault();
                    cot.Estado = 2;

                    CobroDto cob = this.Servicio.ReadCobro(String.Format(this.FormatoClave, cot.Cobro));
                    CobrosElementoDto ele = this.Servicio.ReadCobrosElementosFiltered("", String.Format("cobro = {0} and estado = 0", cob.Id)).FirstOrDefault();
                    if ((cob.Valor - cot.Valor) <= 0)
                    {
                        cob.Estado = 2;
                        ele.Estado = 2;
                    }
                    else
                    {
                        cob.Valor = cob.Valor - cot.Valor;
                        ele.Valor = ele.Valor - cot.Valor;
                    }

                    this.Servicio.UpdateCobroTransaccion(cot);
                    this.Servicio.UpdateCobrosElemento(ele);
                    this.Servicio.UpdateCobro(cob);

                    Utiles.TablaCadena tb = new Utiles.TablaCadena();
                    String enc = "Revertir Cobro de titulo\r\n";
                    enc = enc + "========================\r\n";
                    tb.AddRow("Atributo        ", "Valor ");
                    tb.AddRow("----------------", "----->");
                    tb.AddRow("Id", cot.Id);
                    tb.AddRow("Fecha", String.Format("{0:d}", cob.Fecha));
                    tb.AddRow("Planilla Id", cot.Transaccion);
                    tb.AddRow("Concepto", conden);
                    tb.AddRow("Codigo", p.Codigo);
                    tb.AddRow("Año", p.Año);
                    tb.AddRow("Contribuyente(s)", p.ContribuyentesCadena);
                    tb.AddRow("Valor", String.Format("{0:n2}", cot.Valor));
                    tb.AddRow("Recargos", String.Format("{0:n2}", cot.Recargos));
                    tb.AddRow("Rebajas", String.Format("{0:n2}", cot.Rebajas));
                    tb.AddRow("Soporte No.", p.SoporteNumero);
                    tb.AddRow("Descripcion", pDescripcion);
                    this.CrearSeguimiento(tb, "Operacion de Cobro de planilla", Utiles.EntidadesEnum.EnCobroTransaccion, enc);
                }
            }
        }

        /// <summary>
        /// Traer CobrosTransacciones por planilla consultada
        /// </summary>
        /// <param name="pId">Id de la planilla</param>
        /// <param name="pEstado">estado del registro (9 = todos)</param>
        /// <returns></returns>
        public virtual IEnumerable<CobroTransaccionDto> CobrosPorPlanillaEstado(int pId, int pEstado)
        {
            if (pEstado == 9)
                return this.Servicio.ReadCobroTransaccionsFiltered("", String.Format("transaccion = {0}", pId));
            else
                return this.Servicio.ReadCobroTransaccionsFiltered("", String.Format("transaccion = {0} and estado = {1}", pId, pEstado));
        }

        public virtual CobroTransaccionDto CobroTransaccionUltimoPorPlanilla(int planilla)
        {
            return this.Servicio.ReadCobroTransaccionsFiltered("", String.Format("transaccion = {0} and estado = 0", planilla)).LastOrDefault();
        }

        public virtual IEnumerable<CobroPlanillaReversion> CobrosParcialesPorPlanillaEstado(int pId, int pEstado)
        {
            List<CobroPlanillaReversion> cob = new List<CobroPlanillaReversion>();
            IEnumerable<CobroTransaccionDto> cobros = CobrosPorPlanillaEstado(pId, pEstado);
            foreach (CobroTransaccionDto t in cobros)
            {
                CobroPlanillaReversion rev = new CobroPlanillaReversion();
                rev.Planilla = t;
                rev.Cobro = this.Servicio.ReadCobro(String.Format(this.FormatoClave, t.Cobro));
                rev.Cobro.RubrosNav = this.Servicio.ReadCobrosRubrosFiltered("", String.Format("cobro = {0} and referencia = {1} and estado = 0", rev.Cobro.Id, t.Transaccion));
                cob.Add(rev);
            }
            return cob;
        }

        public virtual CobroPlanillaPagoParcial CobroPlanillaParcial(int planilla)
        {
            CobroPlanillaPagoParcial ret = new CobroPlanillaPagoParcial();
            ret.Planilla = PlanillaPagada(planilla);
            ret.Cobro = CobroTransaccionUltimoPorPlanilla(planilla);
            return ret;
        }

        /// <summary>
        /// Revertir un cobro con todos sus elementos y transacciones
        /// </summary>
        /// <param name="pId">Id del cobro a revertir</param>
        public virtual void CobroRevertir(int pId)
        {
            CobroDto cob = this.Servicio.ReadCobro(String.Format(this.FormatoClave, pId));
            if (cob != null && cob.Id > 0)
            {
                IEnumerable<CobrosElementoDto> eles = this.Servicio.ReadCobrosElementosFiltered("", String.Format("cobro = {0}", cob.Id));
                foreach (CobrosElementoDto ele in eles)
                {
                    ele.Estado = 2;
                }
                this.Servicio.UpdateCobrosElementos(eles.ToArray());
                IEnumerable<CobroTransaccionDto> trns = this.Servicio.ReadCobroTransaccionsFiltered("", String.Format("cobro = {0}", cob.Id));
                foreach (CobroTransaccionDto trn in trns)
                {
                    trn.Estado = 2;

                    PlanillaDto p = this.Servicio.ReadPlanilla(String.Format(this.FormatoClave, trn.Transaccion));
                    this.ExpandirPlanilla(p);

                    String conden = String.Empty;
                    if (p.ConceptoNav != null)
                        conden = p.ConceptoNav.Denominacion;

                    Utiles.TablaCadena tb = new Utiles.TablaCadena();
                    String enc = "Revertir cobro de titulo\r\n";
                    enc = enc + "========================\r\n";
                    tb.AddRow("Atributo        ", "Valor ");
                    tb.AddRow("----------------", "----->");
                    tb.AddRow("Id", trn.Id);
                    tb.AddRow("Fecha", String.Format("{0:d}", cob.Fecha));
                    tb.AddRow("Planilla Id", trn.Id);
                    tb.AddRow("Contribuyente(s)", p.ContribuyentesCadena);
                    tb.AddRow("Concepto", conden);
                    tb.AddRow("Año", p.Año);
                    tb.AddRow("Codigo", p.Codigo);
                    tb.AddRow("Valor", String.Format("{0:n2}", trn.Valor));
                    tb.AddRow("Rebajas", String.Format("{0:n2}", trn.Rebajas));
                    tb.AddRow("Recargos", String.Format("{0:n2}", trn.Recargos));

                    this.CrearSeguimiento(tb, "Operacion de Reversion Cobro", Utiles.EntidadesEnum.EnCobroTransaccion, enc);
                }
                this.Servicio.UpdateCobroTransaccions(trns.ToArray());
            }
            cob.Estado = 2;

            this.Servicio.UpdateCobro(cob);
        }

        public virtual void CobroRevertirPorPlanilla(int cobro, int planilla, bool pMarcarSoporte, String pDescripcion)
        {
            PlanillaDto p = this.Servicio.ReadPlanilla(String.Format(this.FormatoClave, planilla));
            this.ExpandirPlanilla(p);
            IEnumerable<CobroTransaccionDto> trs = this.Servicio.ReadCobroTransaccionsFiltered("", String.Format("cobro = {0}", cobro));
            if (trs.Count() == 1)
            {
                CobroRevertir(cobro); // Esta metodo registra la pista de auditoria
                if (p.Estado == 1)
                    p.Estado = 0;
                p.Pagos = p.Pagos - trs.ElementAt(0).Valor;
                p.Rebajas = p.Rebajas - trs.ElementAt(0).Rebajas;
                p.Recargos = p.Recargos - trs.ElementAt(0).Recargos;

                if (pMarcarSoporte)
                {
                    CobroDto cb = this.Servicio.ReadCobro(String.Format(this.FormatoClave, cobro));

                    SoporteMovimientoDto sm = new SoporteMovimientoDto();
                    sm.Id = 0;
                    sm.Soporte = 0;
                    if (cb != null)
                        sm.Fecha = cb.Fecha;
                    sm.Numero = trs.ElementAt(0).SoporteNumero;
                    sm.Referencia = p.Id;
                    sm.Movimiento = 3; // Movimientos de anulacion
                    sm.Estado = 0;
                    sm.Descripcion = pDescripcion;
                    this.Servicio.CreateSoporteMovimiento(sm);
                }

                IEnumerable<CobrosRubroDto> crs = this.Servicio.ReadCobrosRubrosFiltered("", String.Format("cobro = {0} and referencia = {1}", cobro, planilla));
                foreach (CobrosRubroDto cr in crs)
                {
                    PlanillaRubroDto pr = this.Servicio.ReadPlanillaRubrosFiltered("", String.Format("planilla = {0} and rubro = {1}", planilla, cr.Rubro)).FirstOrDefault();
                    if (pr != null)
                    {
                        pr.RubroNav = this.Servicio.ReadRubro(String.Format(this.FormatoClave, pr.Rubro));
                        bool afecta = false;
                        if (pr.RubroNav != null && pr.RubroNav.Afectante != null)
                            afecta = pr.RubroNav.Afectante == 1;

                        /* Antes */
                        //if(pr.Rubro == 1 || pr.Rubro == 2 || pr.Rubro == 4 || pr.Rubro == 5 || pr.Rubro == 29)
                        if (afecta)
                        {
                            // Si el rubro revertido queda en valor 0 entonces eliminar
                            // caso contrario restar valor de valor
                            if (pr.Valor > cr.Valor)
                            {
                                pr.Valor = pr.Valor - cr.Valor;
                                this.Servicio.UpdatePlanillaRubro(pr);
                            }
                            else
                            {
                                this.Servicio.DeletePlanillaRubro(pr);
                            }
                        }
                        else
                        {
                            pr.Pagos = pr.Pagos - cr.Valor;
                            pr.Rebajas = pr.Rebajas - cr.Rebajas;
                            this.Servicio.UpdatePlanillaRubro(pr);
                        }
                    }
                    cr.Estado = 2;
                }

                this.Servicio.UpdateCobrosRubros(crs.ToArray());
            }
            else
            {
                CobroDto cob = this.Servicio.ReadCobro(String.Format(this.FormatoClave, cobro));
                double val = 0;
                double reb = 0;
                double inr = 0;
                IEnumerable<CobroTransaccionDto> trns = this.Servicio.ReadCobroTransaccionsFiltered("", String.Format("cobro = {0} and transaccion = {1}", cobro, planilla));
                foreach (CobroTransaccionDto trn in trns)
                {
                    if (pMarcarSoporte)
                    {
                        SoporteMovimientoDto sm = new SoporteMovimientoDto();
                        sm.Id = 0;
                        sm.Soporte = 0;
                        sm.Fecha = cob.Fecha;
                        sm.Numero = trn.SoporteNumero;
                        sm.Referencia = p.Id;
                        sm.Movimiento = 3; // Movimientos de anulacion
                        sm.Estado = 0;
                        sm.Descripcion = pDescripcion;
                        this.Servicio.CreateSoporteMovimiento(sm);
                    }

                    if (trn.Valor != null)
                        val = val + (double)trn.Valor;
                    if (trn.Rebajas != null)
                        reb = reb + (double)trn.Rebajas;
                    if (trn.Recargos != null)
                        inr = inr + (double)trn.Recargos;
                    trn.Estado = 2;

                    String conden = String.Empty;
                    if (p.ConceptoNav != null)
                        conden = p.ConceptoNav.Denominacion;

                    Utiles.TablaCadena tb = new Utiles.TablaCadena();
                    String enc = "Revertir cobro de titulo\r\n";
                    enc = enc + "========================\r\n";
                    tb.AddRow("Atributo        ", "Valor ");
                    tb.AddRow("----------------", "----->");
                    tb.AddRow("Id", trn.Id);
                    tb.AddRow("Fecha", cob.Fecha);
                    tb.AddRow("Planilla Id", trn.Id);
                    tb.AddRow("Contribuyente(s)", p.ContribuyentesCadena);
                    tb.AddRow("Concepto", conden);
                    tb.AddRow("Año", p.Año);
                    tb.AddRow("Codigo", p.Codigo);
                    tb.AddRow("Valor", String.Format("{0:n2}", trn.Valor));
                    tb.AddRow("Rebajas", String.Format("{0:n2}", trn.Rebajas));
                    tb.AddRow("Recargos", String.Format("{0:n2}", trn.Recargos));
                    tb.AddRow("Soporte No.", trn.SoporteNumero);
                    tb.AddRow("Descripcion", pDescripcion);

                    this.CrearSeguimiento(tb, "Operacion de Reversion Cobro", Utiles.EntidadesEnum.EnCobroTransaccion, enc);
                }
                this.Servicio.UpdateCobroTransaccions(trns.ToArray());

                if (cob.Valor == val + inr - reb) // el saldo del cobro es igual al valor a revertir
                {
                    cob.Estado = 2;
                    this.Servicio.UpdateCobro(cob);
                }
                else
                {
                    cob.Valor = cob.Valor - (val + inr - reb);
                    foreach (CobroTransaccionDto trn in trns)
                    {

                        trn.Estado = 2;
                        IEnumerable<CobrosRubroDto> crs = this.Servicio.ReadCobrosRubrosFiltered("", String.Format("cobro = {0} and referencia = {1}", cobro, planilla));
                        foreach (CobrosRubroDto cr in crs)
                        {
                            PlanillaRubroDto pr = this.Servicio.ReadPlanillaRubrosFiltered("", String.Format("planilla = {0} and rubro = {1}", planilla, cr.Rubro)).FirstOrDefault();
                            if (pr != null)
                            {
                                pr.Pagos = pr.Pagos - cr.Valor;
                                pr.Rebajas = pr.Rebajas - cr.Rebajas;
                                this.Servicio.UpdatePlanillaRubro(pr);
                            }
                            cr.Estado = 2;
                        }
                        this.Servicio.UpdateCobrosRubros(crs.ToArray());

                    }
                    this.Servicio.UpdateCobroTransaccions(trns.ToArray());
                }
                if (p.Estado == 1)
                    p.Estado = 0;
                p.Pagos = p.Pagos - val;
                p.Rebajas = p.Rebajas - reb;
                p.Recargos = p.Recargos - inr;
            }

            this.Servicio.UpdatePlanilla(p);
        }

        public virtual IEnumerable<PlanillaRubroDto> PlanillaPagadaRubros(int pPlanilla)
        {
            IEnumerable<PlanillaRubroDto> rubs = this.Servicio.ReadPlanillaRubrosFiltered("", String.Format("planilla = {0} and estado = 0", pPlanilla));
            foreach (PlanillaRubroDto rub in rubs)
            {
                if (rub.Rubro != null && rub.Rubro > 0)
                {
                    rub.RubroNav = ModeloCache.Instance.McRubros.Where(r => r.Id == rub.Rubro).FirstOrDefault();
                }
            }
            return rubs;
        }

        public virtual IEnumerable<PlanillaAtributoDto> PlanillaPagadaAtributos(int pPlanilla)
        {
            return this.Servicio.ReadPlanillaAtributosFiltered("", String.Format("planilla = {0} and estado = 0", pPlanilla));
        }

        /// <summary>
        /// Crear el registro de rubro de planilla
        /// </summary>
        /// <param name="rub">Rubro a registrar</param>
        public virtual void PRubroCrear(PlanillaRubroDto rub)
        {
            this.Servicio.CreatePlanillaRubro(rub);
        }

        public virtual void PRubroActualizar(PlanillaRubroDto rub)
        {
            this.Servicio.UpdatePlanillaRubro(rub);
        }

        /// <summary>
        /// Modificar planilla
        /// </summary>
        /// <param name="pla">Planilla a modificar</param>
        public virtual void PlanillaActualizar(PlanillaDto pla)
        {
            if (pla.ConceptoNav == null || pla.ContribuyentesCadena.Length <= 0)
                this.ExpandirPlanilla(pla);
            String conden = String.Empty;
            if (pla.ContribuyenteNav != null)
                conden = pla.ConceptoNav.Denominacion;

            PlanillaDto oldp = this.Servicio.ReadPlanilla(String.Format(this.FormatoClave, pla.Id));
            this.ExpandirPlanilla(oldp);
            String oldcon = String.Empty;
            if (oldp.ContribuyenteNav != null)
                oldcon = oldp.ConceptoNav.Denominacion;

            this.Servicio.UpdatePlanilla(pla);

            Utiles.TablaCadena tb = new Utiles.TablaCadena();
            String enc = "Modificacion de titulo\r\n";
            enc = enc + "========================\r\n";
            tb.AddRow("Atributo        ", "Valor nuevo ", "Valor ant.");
            tb.AddRow("----------------", "----------->", "---------->");
            tb.AddRow("Id", pla.Id, oldp.Id);
            tb.AddRow("Fecha Emision", pla.FechaEmision, oldp.FechaEmision);
            tb.AddRow("Contribuyente(s)", pla.ContribuyentesCadena, oldp.ContribuyentesCadena);
            tb.AddRow("Concepto", conden, oldcon);
            tb.AddRow("Año", pla.Año, oldp.Año);
            tb.AddRow("Codigo", pla.Codigo, oldp.Codigo);
            tb.AddRow("Valor", String.Format("{0:n2}", pla.Total), String.Format("{0:n2}", oldp.Total));
            tb.AddRow("Rebajas", String.Format("{0:n2}", pla.Rebajas), String.Format("{0:n2}", oldp.Rebajas));
            tb.AddRow("Recargos", String.Format("{0:n2}", pla.Recargos), String.Format("{0:n2}", oldp.Recargos));
            tb.AddRow("Fecha Cancelac.", String.Format("{0:d}", pla.FechaCancelacion), String.Format("{0:d}", oldp.FechaCancelacion));
            tb.AddRow("Estado", pla.Estado, oldp.Estado);

            this.CrearSeguimiento(tb, "Operacion de Reversion Cobro", Utiles.EntidadesEnum.EnCobroTransaccion, enc);
        }

        /// <summary>
        /// Crear registro de cobro
        /// </summary>
        /// <param name="cob">Cobro a crear</param>
        public virtual int CobroCrear(CobroDto cob)
        {
            string s = this.Servicio.CreateCobro(cob);
            s = s.Replace("Id=", "");
            return Convert.ToInt32(s);
        }

        /// <summary>
        /// Crear Entidad de cobro
        /// </summary>
        /// <param name="ele">Entidad a crear</param>
        public virtual void CobroElementoCrear(CobrosElementoDto ele)
        {
            this.Servicio.CreateCobrosElemento(ele);
        }

        /// <summary>
        /// Crear registro de transaccion cobrada
        /// </summary>
        /// <param name="t">transaccion a crear</param>
        public virtual void CobroTransaccionCrear(CobroTransaccionDto t)
        {
            this.Servicio.CreateCobroTransaccion(t);
        }

        public virtual IEnumerable<RebajaDto> RebajasPorEstado(int pEstado)
        {
            if (pEstado == 9)
                return this.Servicio.ReadRebajas();
            else
                return this.Servicio.ReadRebajas().Where(r => r.Estado == pEstado);
        }

        public virtual Double RebajaRubroPorcentaje(int pRebaja, int pRubro)
        {
            Double res = 0;
            RebajasRubroDto rrf = this.Servicio.ReadRebajasRubrosFiltered("", String.Format("rebaja = {0} and rubro = {1} and tipo = 1 and estado = 0", pRebaja, pRubro)).FirstOrDefault();
            if (rrf != null && rrf.Valor != null && rrf.Valor > 0)
            {
                res = (double)rrf.Valor;
            }
            else
            {
                RebajaDto reb = this.Servicio.ReadRebaja(String.Format(this.FormatoClave, pRebaja));
                if (reb != null && reb.Fraccion != null && reb.Fraccion > 0)
                    res = (double)reb.Fraccion;
            }
            return res;
        }

        public virtual Double RebajaRubroPorcentajeContribuyente(int pRebaja, int pRubro, int pContribuyente)
        {
            double d = 0;

            if (pRebaja > 0 && pContribuyente > 0)
            {
                ContribuyentesRebajaDto cr = this.Servicio.ReadContribuyentesRebajasFiltered("", String.Format("contribuyente = {0} and rebaja = {1}", pContribuyente, pRebaja)).FirstOrDefault();
                if (cr != null)
                {
                    RebajaDto reb = this.Servicio.ReadRebaja(string.Format(this.FormatoClave, pRebaja));
                    if (reb != null)
                    {
                        if (reb.Concepto == 0) // Nivel rebaja
                        {
                            if (reb.Fraccion != null)
                                d = (double)reb.Fraccion;
                        }
                        else
                        {
                            if (reb.Concepto == 1) // Nivel rubro
                            {
                                if (pRubro > 0)
                                {
                                    RebajasRubroDto rr = this.Servicio.ReadRebajasRubrosFiltered("", String.Format("rebaja = {0} and rubro = {1}", pRebaja, pRubro)).FirstOrDefault();
                                    if (rr != null && rr.Valor != null)
                                        d = (double)rr.Valor;
                                }
                            }
                            else
                            {
                                if (reb.Concepto == 2) // Nivel contribuyente
                                {
                                    if (cr.Fraccion != null)
                                        d = (double)cr.Fraccion;
                                }
                            }
                        }
                    }
                }
            }


            return d;
        }

        public virtual Double RebajaDiscapacidadPorcentaje(int pContribuyente)
        {
            Double res = 0;
            ContribuyentesRebajaDto cr = this.Servicio.ReadContribuyentesRebajasFiltered("", String.Format("contribuyente = {0} and rebaja = 2", pContribuyente)).FirstOrDefault(); // ().Where(c => c.Contribuyente == pContribuyente && c.Rebaja == 2).FirstOrDefault(); // Discapacidad
            if (cr != null && cr.Fraccion != null && cr.Fraccion > 0)
                res = (double)cr.Fraccion;
            return res;
        }

        public virtual Double RebajaTopePorRubro(int pRebaja, int pRubro)
        {
            double res = 0;
            RebajasRubroDto rebrub = this.Servicio.ReadRebajasRubrosFiltered("", String.Format("rebaja = {0} and rubro = {1} and tipo = 2 and estado = 0", pRebaja, pRubro)).FirstOrDefault();
            if (rebrub != null && rebrub.Valor > 0)
            {
                res = (double)rebrub.Valor;
            }
            return res;
        }

        public virtual DateTime HoyServidor()
        {
            DateTime hoy = DateTime.Today;
            try
            {
                hoy = this.Servicio.Hoy();
            }
            catch { }

            return hoy;
        }

        public virtual IEnumerable<PlanillaDto> DocumentosPorEmitir(int pModulo)
        {
            ConceptosDep cd = new ConceptosDep();
            List<PlanillaDto> res = new List<PlanillaDto>();
            IEnumerable<ConceptosDocumentoDto> cons = cd.DocumentosPorConceptoUnicos();
            foreach (ConceptosDocumentoDto c in cons)
            {
                if (c.Modulo == pModulo)
                {
                    IEnumerable<PlanillaDto> pls = this.Servicio.ReadPlanillasFiltered("", String.Format("concepto = {0} and estado = 1 and especie = 0", c.Concepto));
                    foreach (PlanillaDto p in pls)
                    {
                        this.ExpandirPlanilla(p);
                    }
                    res.AddRange(pls);
                }
            }
            return res;
        }

        public virtual IEnumerable<PlanillaDto> DocumentosPorHistorico(int pEstado, DateTime pFechaInicio, DateTime pFechaCorte)
        {
            String filtro = String.Empty;
            if (pEstado != 9)
            {
                filtro = filtro + String.Format(" and especie = {0}", pEstado);
            }

            if (pFechaInicio != null)
            {
                filtro = filtro + " and fechaEmision >= DateTime.Parse(\"" + Convert.ToString(pFechaInicio.Date) + "\")";
            }

            if (pFechaCorte != null)
            {
                filtro = filtro + " and fechaEmision <= DateTime.Parse(\"" + Convert.ToString(pFechaCorte.Date) + "\")";
            }

            ConceptosDep cd = new ConceptosDep();
            List<PlanillaDto> res = new List<PlanillaDto>();
            IEnumerable<ConceptosDocumentoDto> cons = cd.DocumentosPorConceptoUnicos();
            foreach (ConceptosDocumentoDto c in cons)
            {
                IEnumerable<PlanillaDto> pls = this.Servicio.ReadPlanillasFiltered("", String.Format("concepto = {0}" + filtro, c.Concepto));
                foreach (PlanillaDto p in pls)
                {
                    this.ExpandirPlanilla(p);
                }
                res.AddRange(pls);
            }
            return res;
        }

        public virtual PlanillaDto DocumentosPuedeEmitir(int pConcepto, int pBeneficiario)
        {
            IEnumerable<PlanillaDto> res = this.Servicio.ReadPlanillasFiltered("", String.Format("concepto = {0} and contribuyentes.contains(\"[{1}]\") and estado = 1 and especie = 0", pConcepto, pBeneficiario));
            if (res.Count() > 1)
            {
                return res.Last();
            }
            else
            {
                if (res.Count() == 1)
                    return res.ElementAt(0);
            }
            return null;
        }

        public virtual void DocumentoMarcarPorPlanilla(int pId)
        {
            PlanillaDto p = this.Servicio.ReadPlanilla(String.Format(FormatoClave, pId));
            this.ExpandirPlanilla(p);
            p.Especie = 1;

            String conden = String.Empty;
            if (p.ConceptoNav != null)
                conden = p.ConceptoNav.Denominacion;

            Utiles.TablaCadena tb = new Utiles.TablaCadena();
            String enc = "Impresion de documento habilitante\r\n";
            enc = enc + "==================================\r\n";
            tb.AddRow("Atributo        ", "Valor ");
            tb.AddRow("----------------", "----->");
            tb.AddRow("Id", p.Id);
            tb.AddRow("Fecha Emision", p.FechaEmision);
            tb.AddRow("Contribuyente(s)", p.ContribuyentesCadena);
            tb.AddRow("Concepto", conden);
            tb.AddRow("Valor", String.Format("{0:n2}", p.Total));

            this.CrearSeguimiento(tb, "Operacion de Documentos habilitantes", Utiles.EntidadesEnum.EnHabilitante, enc);

            this.Servicio.UpdatePlanilla(p);
        }

        public virtual void DocumentoDesmarcarPorPlanilla(int pId)
        {
            PlanillaDto p = this.Servicio.ReadPlanilla(String.Format(FormatoClave, pId));
            this.ExpandirPlanilla(p);
            p.Especie = 0;

            String conden = String.Empty;
            if (p.ConceptoNav != null)
                conden = p.ConceptoNav.Denominacion;

            Utiles.TablaCadena tb = new Utiles.TablaCadena();
            String enc = "Desmarcacion de documento habilitante\r\n";
            enc = enc + "=====================================\r\n";
            tb.AddRow("Atributo        ", "Valor ");
            tb.AddRow("----------------", "----->");
            tb.AddRow("Id", p.Id);
            tb.AddRow("Fecha Emision", p.FechaEmision);
            tb.AddRow("Contribuyente(s)", p.ContribuyentesCadena);
            tb.AddRow("Concepto", conden);
            tb.AddRow("Valor", String.Format("{0:n2}", p.Total));

            this.CrearSeguimiento(tb, "Operacion de Documentos habilitantes", Utiles.EntidadesEnum.EnHabilitante, enc);

            this.Servicio.UpdatePlanilla(p);
        }

        #region Soportes

        /// <summary>
        /// Traer una referencia a un soporte por us id
        /// </summary>
        /// <param name="pId">Id del soporte a consultar</param>
        /// <returns></returns>
        public SoporteDto SoportePorId(int pId)
        {
            return this.Servicio.ReadSoporte(String.Format(this.FormatoClave, pId));
        }

        /// <summary>
        /// Traer los soportes por el estado dado
        /// </summary>
        /// <param name="pEstado">Estado a consultar (9=TODOS)</param>
        /// <returns></returns>
        public IEnumerable<SoporteDto> SoportesPorEstado(int pEstado)
        {
            if (pEstado == 9)
            {
                return this.Servicio.ReadSoportes();
            }
            else
            {
                return this.Servicio.ReadSoportesFiltered("", String.Format("estado = {0}", pEstado));
            }
        }

        /// <summary>
        /// Traer el numero consecutivo actual del soporte dado
        /// </summary>
        /// <param name="pId">Id del sooporte consultado</param>
        /// <param name="pAumentar">Aumentar el numero</param>
        /// <returns></returns>
        public virtual int SoporteNumero(int pId, Boolean pAumentar)
        {
            SoporteDto s = this.SoportePorId(pId);
            if (s != null)
            {
                if (s.Numero != null)
                {
                    if (pAumentar)
                    {
                        s.Numero = s.Numero + 1;
                        this.ModificarSoporte(s);
                    }
                    return (int)s.Numero;
                }
                else
                {
                    if (pAumentar)
                    {
                        s.Numero = 2;
                        this.ModificarSoporte(s);
                    }
                    return 1;
                }
            }
            return -1;
        }

        /// <summary>
        /// Modificar soporte
        /// </summary>
        /// <param name="s">Soporte a guardar</param>
        public virtual void ModificarSoporte(SoporteDto s)
        {
            SoporteDto olds = SoportePorId(s.Id);
            this.Servicio.UpdateSoporte(s);

            Utiles.TablaCadena tb = new Utiles.TablaCadena();
            String enc = "Modificar Soporte\r\n";
            enc = enc + "=================\r\n";
            tb.AddRow("Atributo     ", "Valor Ant ", "Valor nuevo ");
            tb.AddRow("-------------", "--------->", "----------->");
            tb.AddRow("Id", s.Id, s.Id);
            tb.AddRow("Denominacion", s.Denominacion, olds.Denominacion);
            tb.AddRow("Año", s.Numero, olds.Numero);

            this.CrearSeguimiento(tb, "Operacion de Soportes de impresion", Utiles.EntidadesEnum.EnSoporte, enc);
        }


        /// <summary>
        /// Traer Registros de El Soporte dado en el periodo dado por tipo y estado
        /// </summary>
        /// <param name="pSoporte">Id del Soporte consultado</param>
        /// <param name="pFechaDesde">Fecha de inicio de consulta</param>
        /// <param name="pFechaHasta">Fecha de corte de consulta</param>
        /// <param name="pTipo">Tipo de movimiento a filtrar(<=0:TODOS)</param>
        /// <param name="pEstado">Estado del registro("9:TODOS")</param>
        /// <returns></returns>
        public virtual IEnumerable<SoporteMovimientoDto> SoportesPorFecha(int pSoporte, DateTime pFechaDesde, DateTime pFechaHasta, int pTipo, int pEstado)
        {
            String filtroPeriodo = "fecha >= DateTime.Parse(\"" + pFechaDesde.ToString() + "\") and fecha <= DateTime.Parse(\"" + pFechaHasta.ToString() + "\")";
            List<SoporteMovimientoDto> res = new List<SoporteMovimientoDto>();
            if (pTipo <= 0)
            {
                if (pEstado == 9)
                {
                    res.AddRange(this.Servicio.ReadSoporteMovimientosFiltered("", filtroPeriodo));
                }
                else
                {
                    res.AddRange(this.Servicio.ReadSoporteMovimientosFiltered("", String.Format(filtroPeriodo + " and estado = {0}", pEstado)));
                }
            }
            else
            {
                if (pEstado == 9)
                {
                    res.AddRange(this.Servicio.ReadSoporteMovimientosFiltered("", String.Format("movimiento = {0} and " + filtroPeriodo, pTipo)));
                }
                else
                {
                    res.AddRange(this.Servicio.ReadSoporteMovimientosFiltered("", String.Format("movimiento = {0} and estado = {1} and " + filtroPeriodo, pTipo, pEstado)));
                }
            }

            RepRecaudacionesDep rd = new RepRecaudacionesDep();
            IEnumerable<Referencia.RepRecaudacionesFechaDto> pls = rd.ReporteRecaudacionesFecha(pFechaDesde, pFechaHasta);
            foreach (Referencia.RepRecaudacionesFechaDto p in pls)
            {
                if (p.SoporteNumero > 0)
                {
                    SoporteMovimientoDto s = new SoporteMovimientoDto();
                    s.Id = p.Id;
                    s.Descripcion = String.Empty;
                    s.Fecha = p.Fecha;
                    s.Movimiento = 1;
                    s.Numero = p.SoporteNumero;
                    s.Referencia = p.Id;
                    s.Soporte = pSoporte;
                    res.Add(s);
                }
            }

            return res;
        }

        #endregion

        #region Notas de cobros

        /// <summary>
        /// Traer Notas de cobro por el periodo y estado requeridos
        /// </summary>
        /// <param name="pFechaInicio">Fecha de inicio de consulta</param>
        /// <param name="pFechaCorte">Fecha de corte de consulta</param>
        /// <param name="pEstado">Estado de la nota</param>
        /// <returns></returns>
        public virtual IEnumerable<Referencia.CobrosNotaDto> CobrosNotasPorFechaEstado(DateTime pFechaInicio, DateTime pFechaCorte, int pEstado)
        {
            IEnumerable<CobrosNotaDto> res;
            string filtro = String.Empty;

            if (pFechaInicio != null && pFechaCorte != null)
            {
                filtro = filtro + "fecha >= DateTime.Parse(\"" + Convert.ToString(pFechaInicio.Date) + "\") and fecha <= DateTime.Parse(\"" + Convert.ToString(pFechaCorte.Date) + "\")";
            }            
            
            if (pEstado != 9)
            {
                filtro = filtro + String.Format(" and estado = {0}", pEstado);
            }

            res = this.Servicio.ReadCobrosNotasFiltered("", filtro);
            return res;

        }
        
        public virtual int CrearNotaCobro(CobrosNotaDto pNotaCob)
        {
            string s = this.Servicio.CreateCobrosNota(pNotaCob);
            s = s.Replace("Id=", "");
            return Convert.ToInt32(s);
        }

        public virtual int CrearNotaCobEle(CobrosNotasElementoDto pCobEle)
        {
            string s = this.Servicio.CreateCobrosNotasElemento(pCobEle);
            s = s.Replace("Id=", "");
            return Convert.ToInt32(s);
        }

        public virtual void MarcarCobroNota(int pId, int pCobroGen)
        {
            String s = String.Format("Id={0}", pId);
            CobrosNotaDto n = this.Servicio.ReadCobrosNota(s);
            if (n != null)
            {
                n.Estado = 1;
                n.CobroGenerado = pCobroGen;
                n.CobroFecha = this.Servicio.Hoy();
                this.Servicio.UpdateCobrosNota(n);
            }
        }

        #endregion

        // Traer convenios de pagos
        // Crear convenio de pago
        // Generar titulo por dividendo de convenio de pago

        #region Comprobante de tickets

        public ConvenioDto ComprobanteTicketsPorNumero(int num, bool expandir)
        {
            ConvenioDto res = Servicio.ReadConveniosFiltered("", string.Format("tipo = 3 and numero = {0}", num)).FirstOrDefault();
            if (res != null && expandir)
            {
                if (res.Contribuyente != null && res.Contribuyente > 0)
                {
                    res.ContribuyenteNav = Servicio.ReadContribuyente(string.Format(FormatoClave, res.Contribuyente));
                }
            }
            return res;
        }

        public void MarcarComprobanteTickestPorId(int id, int origen)
        {
            ConvenioDto res = Servicio.ReadConvenio(string.Format(FormatoClave, id));
            if (res != null)
            {
                res.Estado = 1;
                res.FechaTerminacion = new DateTime();
                res.Concepto = origen;
                Servicio.UpdateConvenio(res);
            }
        }

        public int CrearComprobateTicketsPorTickets(List<int> ids)
        {
            int i = -1;

            // crear 

            return i;
        }

        public List<String> TraerTicketsPorMesLocal(int mes, int pid)
        {
            return new List<string>(Servicio.TicketsCompletosPuestoPeriodo(pid, new DateTime(), new DateTime()));
        }

        #endregion
    }
}
