using Intelligob.Cliente.Referencia;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intelligob.Cliente.Depositos
{
    public class AguaDep : DepositoBase
    {
        #region Operaciones de cuentas de agua

        public AguaDep() : base(DepositosControl.Instance.Servicio) { }

        public AguaDep(IEntidades servicio) : base(servicio) { }

        /// <summary>
        /// Consultar cuentas de agua por estado
        /// </summary>
        /// <param name="pEstado">Filtro de estado</param>
        /// <returns></returns>
        public virtual IEnumerable<AguaPotableDto> CuentasPorEstado(int pEstado)
        {
            IEnumerable<AguaPotableDto> ctas;
            if (pEstado == 9)
                ctas = this.Servicio.ReadAguaPotables();
            else
                ctas = this.Servicio.ReadAguaPotablesFiltered("codigo", String.Format("estado = {0}", pEstado)); 
            foreach(AguaPotableDto c in ctas)
            {
                if (c.Categoria != null && c.Categoria > 0)
                    c.CategoriaNav = ModeloCache.Instance.McCoeficienteElementos.Where(e => e.Id == c.Categoria).FirstOrDefault();
                if (c.Contribuyente != null && c.Contribuyente > 0)
                    c.ContribuyenteNav = this.Servicio.ReadContribuyente(String.Format(this.FormatoClave, c.Contribuyente));
                if (c.FormaPropiedad != null && c.FormaPropiedad > 0)
                    c.FormaPropiedadNav = ModeloCache.Instance.McClaves.Where(f => f.Id == c.FormaPropiedad).FirstOrDefault();
                if (c.TipoDominio != null && c.TipoDominio > 0)
                    c.DominioNav = ModeloCache.Instance.McClaves.Where(d => d.Id == c.TipoDominio).FirstOrDefault();
                if (c.ServicioEstado != null && c.ServicioEstado > 0)
                    c.EstadoServicioNav = ModeloCache.Instance.McClaves.Where(s => s.Id == c.ServicioEstado).FirstOrDefault();                
            }
            return ctas;
        }

        /// <summary>
        /// Traer las patentes registradas por un contribuyente
        /// </summary>
        /// <param name="pId">Id del contribuyente</param>
        /// <param name="pEstado">Estado para filtrar los registros</param>
        /// <returns>Lista de Cuentas de Agua</returns>
        public virtual IEnumerable<AguaPotableDto> CuentasPorContribuyente(int pId, int pEstado)
        {
            IEnumerable<AguaPotableDto> res;
            if (pEstado == 9)
            {
                res = Servicio.ReadAguaPotablesFiltered("codigo", String.Format("contribuyente = {0}", pId));
            }
            else
            {
                res = Servicio.ReadAguaPotablesFiltered("codigo", String.Format("contribuyente = {0} and estado = {1}", pId, pEstado));
            }
            this.ExpandirCuentas(res);
            return res;
        }

        /// <summary>
        /// Traer las patentes cuyo codigo coinciden con la cadena recibida (Patron de codigo)
        /// Se puede usar comodines para traer solo las cuentas de cierto sector/manzana/poligono/etc
        /// </summary>
        /// <param name="pCodigo">Codigo a buscar</param>
        /// <param name="pEstado">Estado para filtrar los registros</param>
        /// <param name="pTipoBusqueda">Criterio de busqueda a aplicar</param>
        /// <returns>Lista de Cuentas de Agua</returns>
        public virtual IEnumerable<AguaPotableDto> CuentasPorCodigo(String pCodigo, int pEstado, int pTipoBusqueda)
        {
            IEnumerable<AguaPotableDto> res;
            switch (pTipoBusqueda)
            {
                case 1: // Conteniendo
                    {
                        if (pEstado == 9)
                        {
                            res = Servicio.ReadAguaPotablesFiltered("codigo" , String.Format("codigo.Contains(\"{0}\")", pCodigo));
                        }
                        else
                        {
                            res = Servicio.ReadAguaPotablesFiltered("codigo", String.Format("codigo.Contains(\"{0}\") and estado = {1}", pCodigo, pEstado));
                        }
                        break;
                    }
                case 2: // Comenzando con
                    {
                        if (pEstado == 9)
                        {
                            res = Servicio.ReadAguaPotablesFiltered("", String.Format("Codigo.StartsWith(\"{0}\")", pCodigo));
                        }
                        else
                        {
                            res = Servicio.ReadAguaPotablesFiltered("", String.Format("Codigo.StartsWith(\"{0}\") and estado = {1}", pCodigo, pEstado));
                        }
                        break;
                    }
                default: // Codigo exacto
                    {
                        if (pEstado == 9)
                        {
                            res = Servicio.ReadAguaPotablesFiltered("codigo", String.Format("codigo = \"{0}\"", pCodigo));
                        }
                        else
                        {
                            res = Servicio.ReadAguaPotablesFiltered("codigo", String.Format("codigo = \"{0}\" and estado = {1}", pCodigo, pEstado));
                        }
                        break;
                    }
            }
            this.ExpandirCuentas(res);
            return res;
        }


        public virtual AguaPotableDto CuentaPorId(int pId)
        {
            AguaPotableDto c = this.Servicio.ReadAguaPotable(String.Format(this.FormatoClave, pId));
            if (c.Categoria != null && c.Categoria > 0)
                c.CategoriaNav = ModeloCache.Instance.McCoeficienteElementos.Where(e => e.Id == c.Categoria).FirstOrDefault();
            if (c.Contribuyente != null && c.Contribuyente > 0)
                c.ContribuyenteNav = this.Servicio.ReadContribuyente(String.Format(this.FormatoClave, c.Contribuyente));
            if (c.FormaPropiedad != null && c.FormaPropiedad > 0)
                c.FormaPropiedadNav = ModeloCache.Instance.McClaves.Where(f => f.Id == c.FormaPropiedad).FirstOrDefault();
            if (c.TipoDominio != null && c.TipoDominio > 0)
                c.DominioNav = ModeloCache.Instance.McClaves.Where(d => d.Id == c.TipoDominio).FirstOrDefault();
            if (c.ServicioEstado != null && c.ServicioEstado > 0)
                c.EstadoServicioNav = ModeloCache.Instance.McClaves.Where(s => s.Id == c.ServicioEstado).FirstOrDefault();
            return c;
        }

        public virtual AguaPotableDto CuentaMinPorId(int pId)
        {
            AguaPotableDto c = this.Servicio.ReadAguaPotable(String.Format(this.FormatoClave, pId));            
            if (c.Contribuyente != null && c.Contribuyente > 0)
                c.ContribuyenteNav = this.Servicio.ReadContribuyente(String.Format(this.FormatoClave, c.Contribuyente));
            return c;
        }

        private void ExpandirCuentas(IEnumerable<AguaPotableDto> cuentas)
        {
            foreach(AguaPotableDto c in cuentas)
            {
                    if (c.Categoria != null && c.Categoria > 0)
                        c.CategoriaNav = ModeloCache.Instance.McCoeficienteElementos.Where(e => e.Id == c.Categoria).FirstOrDefault();
                    if (c.Contribuyente != null && c.Contribuyente > 0)
                        c.ContribuyenteNav = this.Servicio.ReadContribuyente(String.Format(this.FormatoClave, c.Contribuyente));
                    if (c.FormaPropiedad != null && c.FormaPropiedad > 0)
                        c.FormaPropiedadNav = ModeloCache.Instance.McClaves.Where(f => f.Id == c.FormaPropiedad).FirstOrDefault();
                    if (c.TipoDominio != null && c.TipoDominio > 0)
                        c.DominioNav = ModeloCache.Instance.McClaves.Where(d => d.Id == c.TipoDominio).FirstOrDefault();
                    if (c.ServicioEstado != null && c.ServicioEstado > 0)
                        c.EstadoServicioNav = ModeloCache.Instance.McClaves.Where(s => s.Id == c.ServicioEstado).FirstOrDefault();
                
            }
        }

        /// <summary>
        /// Registrar nueva cuenta de Agua potable
        /// </summary>
        /// <param name="pCta">Registro a guardar</param>
        /// <returns>AguaPotable</returns>
        public virtual int CuentaCrear(AguaPotableDto pCta)
        {
            string sid = this.Servicio.CreateAguaPotable(pCta);
            sid = sid.Replace("Id=", "");

            String nom = String.Empty;
            if (pCta.ContribuyenteNav != null)
            {
                nom = pCta.ContribuyenteNav.Nombres;
            }
            else
            {
                ContribuyenteDto c = this.Servicio.ReadContribuyente(String.Format(this.FormatoClave, pCta.Contribuyente));
                if (c != null)
                    nom = c.Nombres;
            }

            String cat = "N/D";
            if (pCta.CategoriaNav != null)
            {
                cat = pCta.CategoriaNav.Denominacion;
            }
            else
            {
                CoeficienteElementoDto cc = this.Servicio.ReadCoeficienteElemento(String.Format(this.FormatoClave, pCta.Categoria));
                cat = cc.Denominacion;
            }

            Utiles.TablaCadena tb = new Utiles.TablaCadena();
            String enc = "Registrar nueva cuenta de Agua\r\n";
            enc = enc  + "==============================\r\n";
            tb.AddRow("Atributo", "Valor");
            tb.AddRow("--------", "-----");
            tb.AddRow("Id", sid);
            tb.AddRow("Codigo", pCta.Codigo);
            tb.AddRow("Contribuyente ID", pCta.Contribuyente);
            tb.AddRow("Nombres", nom);
            tb.AddRow("Categoria ID", pCta.Categoria);
            tb.AddRow("Categoria", cat);

            this.CrearSeguimiento(tb, "Operacion de Mantenimiento de cuenta de agua", Utiles.EntidadesEnum.EnAguaCuenta, enc);

            return Convert.ToInt32(sid);
        }

        /// <summary>
        /// Modifca/Persistir una cuenta de Agua con sus modificaciones
        /// </summary>
        /// <param name="pId">Id de la cuenta a modificar</param>
        /// <param name="pEstado">Estado al que se actualizara</param>
        public virtual void CuentaModificar(AguaPotableDto cta)
        {
            AguaPotableDto oldcta = this.Servicio.ReadAguaPotable(String.Format(this.FormatoClave, cta.Id));
            
            String nom = "S/N";
            if (cta.ContribuyenteNav != null)
            {
                nom = cta.ContribuyenteNav.Nombres;
            }
            else
            {
                ContribuyenteDto ctb = this.Servicio.ReadContribuyente(String.Format(this.FormatoClave, cta.Contribuyente));
                if (ctb != null)
                {
                    nom = ctb.Nombres;
                }
            }
            String categoria = "N/D";
            if (cta.CategoriaNav != null)
                categoria = cta.CategoriaNav.Denominacion;
            else
            {
                CoeficienteElementoDto cc = this.Servicio.ReadCoeficienteElemento(String.Format(this.FormatoClave, cta.Categoria));
                categoria = cc.Denominacion;
            }

            String oldnom = "S/N";
            if (cta.Contribuyente == oldcta.Contribuyente)
                oldnom = nom;
            else
            {
                ContribuyenteDto ctb = this.Servicio.ReadContribuyente(String.Format(this.FormatoClave, oldcta.Contribuyente));
                if (ctb != null)
                {
                    oldnom = ctb.Nombres;
                }
            }

            String oldcategoria = "N/D";
            if (cta.Categoria == oldcta.Categoria)
                oldcategoria = categoria;
            else
            {
                CoeficienteElementoDto occ = this.Servicio.ReadCoeficienteElemento(String.Format(this.FormatoClave, oldcta.Categoria));
                oldcategoria = occ.Denominacion;
            }


            if (cta.Estado == null)
                cta.Estado = 0;
            int pEstado = (int)cta.Estado;
            if (pEstado == 1)
            {
                TablaClaveDto tes = null;
                try
                {
                    tes = ModeloCache.Instance.McClaves.Where(t => t.Tabla == 22 && t.Clave == 2).FirstOrDefault();
                }
                catch
                {
                    tes = null;
                }
                if (tes != null)
                {
                    cta.ServicioEstado = tes.Id;
                }
            }
            else
            {
                if (pEstado == 0)
                {
                    TablaClaveDto tes = null;
                    try
                    {
                        tes = CuentaServicioActivo();
                    }
                    catch
                    {
                        tes = null;
                    }
                    if (tes != null)
                    {
                        cta.ServicioEstado = tes.Id;
                    }
                }
            }            
            if (cta.ServiciosNav != null)
            {
                foreach (AguaServicioDto s in cta.ServiciosNav)
                {
                    if (s.Id > 0)
                    {
                        this.Servicio.CreateAguaServicio(s);
                    }
                    else
                    {
                        this.Servicio.UpdateAguaServicio(s);
                    }
                }
            }
            this.Servicio.UpdateAguaPotable(cta);

            Utiles.TablaCadena tb = new Utiles.TablaCadena();
            String enc  = "Modificar cuenta de agua potable\r\n";
            enc = enc  +  "================================\r\n";
            tb.AddRow("Atributo", "Original", "Modificado");
            tb.AddRow("--------", "--------", "----------");
            tb.AddRow("Id", oldcta.Id, cta.Id);
            tb.AddRow("Codigo", oldcta.Codigo, cta.Codigo);
            tb.AddRow("Contribuyente ID", oldcta.Contribuyente, cta.Contribuyente);
            tb.AddRow("Nombres", oldnom, nom);
            tb.AddRow("Categoria ID", oldcta.Categoria, cta.Categoria);
            tb.AddRow("Categoria", oldcategoria, categoria);
            tb.AddRow("Estado", oldcta.Estado, cta.Estado);

            this.CrearSeguimiento(tb, "Operacion de Mantenimiento de contribuyentes", Utiles.EntidadesEnum.EnAguaCuenta, enc);
        }

        /// <summary>
        /// Consulta si el codigo de cuenta ya esta registrado
        /// </summary>
        /// <param name="pCodigo">Codigo a validar</param>
        /// <param name="pId">Id de la cuenta que se guarda</param>
        /// <returns>Logico</returns>
        public virtual Boolean CuentaCodigoRegistrado(String pCodigo, int pId)
        {
            Boolean ret = false;
            IEnumerable<AguaPotableDto> ctas = this.Servicio.ReadAguaPotablesFiltered("", String.Format("codigo = \"{0}\" and id != {1}", pCodigo, pId));  ///.Where(a => a.Codigo == pCodigo && a.Id != pId).ToList();
            if (ctas.Count() > 0)
            {
                ret = true;
            }
            return ret;            
        }

        #endregion

        #region Operaciones de servicios complementarios de cuenta
        /// <summary>
        /// Traer los servicioes complemetarios de la cuenta de agua
        /// </summary>
        /// <param name="pCuenta">Cuenta a consultar</param>
        /// <returns></returns>
        public virtual IEnumerable<AguaServicioDto> AguaServiciosPorCuenta(int pCuenta)
        {
            IEnumerable<AguaServicioDto> ss = Servicio.ReadAguaServicios().Where(s => s.Cuenta == pCuenta);
            foreach(AguaServicioDto s in ss)
            {
                if (s.Concepto != null && s.Concepto > 0)
                    s.ConceptoNav = ModeloCache.Instance.McConceptos.Where(c => c.Id == s.Concepto).FirstOrDefault();
                if (s.Estado != null && s.Estado > 0)
                    s.EstadoNav = ModeloCache.Instance.McClaves.Where(e => e.Id == s.Estado).FirstOrDefault();
            }
            return ss;
        }

        /// <summary>
        /// Guardar los cambios en la lista de servicios complementarios
        /// </summary>
        /// <param name="servicios"></param>
        /// <param name="srvEliminados"></param>
        public virtual void CuentaServiciosModificar(IEnumerable<AguaServicioDto> servicios, IEnumerable<AguaServicioDto> srvEliminados)
        {
            foreach (AguaServicioDto srv in servicios)
            {
                if (srv.Id <= 0)
                {
                    String s = this.Servicio.CreateAguaServicio(srv);
                    s = s.Replace("Id=", "");
                    
                    String con = "N/D";
                    if (srv.ConceptoNav != null)
                    {
                        con = srv.ConceptoNav.Denominacion;
                    }
                    else
                    {
                        if (srv.Concepto != null && srv.Concepto > 0)
                        {
                            ConceptoDto oc = this.Servicio.ReadConcepto(String.Format(this.FormatoClave, srv.Concepto));
                            if (oc != null)
                                con = oc.Denominacion;
                        }
                    }

                    String cod = "N/D";
                    if (srv.CuentaNav != null)
                    {
                        cod = srv.CuentaNav.Codigo;
                    }
                    else
                    {
                        if (srv.Cuenta != null && srv.Cuenta > 0)
                        {
                            AguaPotableDto cta = this.Servicio.ReadAguaPotable(String.Format(this.FormatoClave, srv.Cuenta));
                            if (cta != null)
                                cod = cta.Codigo;
                        }
                    }

                    Utiles.TablaCadena tb = new Utiles.TablaCadena();
                    String enc = "Registrar servicio en cuenta de Agua\r\n";
                    enc = enc +  "====================================\r\n";
                    tb.AddRow("Atributo", "Valor");
                    tb.AddRow("--------", "-----");
                    tb.AddRow("Id", s);
                    tb.AddRow("Cuenta Id", srv.Cuenta);
                    tb.AddRow("Codigo", cod);
                    tb.AddRow("Concepto Id", srv.Concepto);
                    tb.AddRow("Concepto", con);

                    this.CrearSeguimiento(tb, "Operacion de Mantenimiento de servicios de agua", Utiles.EntidadesEnum.EnAguaServicios, enc);
                }
                else
                {
                    AguaServicioDto olds = this.Servicio.ReadAguaServicio(String.Format(this.FormatoClave, srv.Id));
                    this.Servicio.UpdateAguaServicio(srv);
                                        
                    String con = "N/D";
                    if (srv.ConceptoNav != null)
                    {
                        con = srv.ConceptoNav.Denominacion;
                    }
                    else
                    {
                        if (srv.Concepto != null && srv.Concepto > 0)
                        {
                            ConceptoDto oc = this.Servicio.ReadConcepto(String.Format(this.FormatoClave, srv.Concepto));
                            if (oc != null)
                                con = oc.Denominacion;
                        }
                    }

                    String oldcon = "N/D";
                    if (srv.Concepto == olds.Concepto)
                    {
                        oldcon = con;
                    }
                    else
                    {
                        if (olds.Concepto != null && olds.Concepto > 0)
                        {
                            ConceptoDto oldc = this.Servicio.ReadConcepto(String.Format(this.FormatoClave, olds.Concepto));
                            if (oldc != null)
                                oldcon = oldc.Denominacion;
                        }
                    }

                    String cod = "N/D";
                    if (srv.CuentaNav != null)
                    {
                        cod = srv.CuentaNav.Codigo;
                    }
                    else
                    {
                        if (srv.Cuenta != null && srv.Cuenta > 0)
                        {
                            AguaPotableDto cta = this.Servicio.ReadAguaPotable(String.Format(this.FormatoClave, srv.Cuenta));
                            if (cta != null)
                                cod = cta.Codigo;
                        }
                    }

                    String oldcod = "N/D";
                    if (srv.Cuenta == olds.Cuenta)
                    {
                        oldcod = cod;
                    }
                    else
                    {
                        if (olds.Cuenta != null && olds.Cuenta > 0)
                        {
                            AguaPotableDto octa = this.Servicio.ReadAguaPotable(String.Format(this.FormatoClave, olds.Cuenta));
                            if (octa != null)
                                oldcod = octa.Codigo;
                        }
                    }

                    String esn = "N/D";
                    if (srv.EstadoNav != null)
                    {
                        esn = srv.EstadoNav.Denominacion;
                    }
                    else
                    {
                        if (srv.Estado != null && srv.Estado > 0)
                        {
                            TablaClaveDto tc = this.Servicio.ReadTablaClave(String.Format(this.FormatoClave, srv.Estado));
                            if (tc != null)
                                esn = tc.Denominacion;
                        }
                    }

                    String oldesn = "N/D";
                    if (srv.Estado == olds.Estado)
                    {
                        oldesn = esn;
                    }
                    else
                    {
                        if (olds.Estado != null && olds.Estado > 0)
                        {
                            TablaClaveDto otc = this.Servicio.ReadTablaClave(String.Format(this.FormatoClave, olds.Estado));
                            if (otc != null)
                                oldesn = otc.Denominacion;
                        }
                    }

                    Utiles.TablaCadena tb = new Utiles.TablaCadena();
                    String enc = "Registrar servicio en cuenta de Agua\r\n";
                    enc = enc +  "====================================\r\n";
                    tb.AddRow("Atributo", "Original", "Nuevo");
                    tb.AddRow("Id", olds.Id, srv.Id);
                    tb.AddRow("Cuenta Id", olds.Cuenta, srv.Cuenta);
                    tb.AddRow("Codigo", oldcod, cod);
                    tb.AddRow("Concepto Id", olds.Concepto, srv.Concepto);
                    tb.AddRow("Concepto", oldcon, con);
                    tb.AddRow("Estado Id", olds.Estado, srv.Estado);
                    tb.AddRow("Estado", oldesn, esn);

                    this.CrearSeguimiento(tb, "Operacion de Mantenimiento de servicios de agua", Utiles.EntidadesEnum.EnAguaServicios, enc);
                }
            }

            this.Servicio.DeleteAguaServicios(srvEliminados.ToArray());

            foreach (AguaServicioDto seli in srvEliminados)
            {
                String con = "N/D";
                if (seli.ConceptoNav != null)
                {
                    con = seli.ConceptoNav.Denominacion;
                }
                else
                {
                    if (seli.Concepto != null && seli.Concepto > 0)
                    {
                        ConceptoDto oc = this.Servicio.ReadConcepto(String.Format(this.FormatoClave, seli.Concepto));
                        if (oc != null)
                            con = oc.Denominacion;
                    }
                }

                String cod = "N/D";
                if (seli.CuentaNav != null)
                {
                    cod = seli.CuentaNav.Codigo;
                }
                else
                {
                    if (seli.Cuenta != null && seli.Cuenta > 0)
                    {
                        AguaPotableDto cta = this.Servicio.ReadAguaPotable(String.Format(this.FormatoClave, seli.Cuenta));
                        if (cta != null)
                            cod = cta.Codigo;
                    }
                }

                String esn = "N/D";
                if (seli.EstadoNav != null)
                {
                    esn = seli.EstadoNav.Denominacion;
                }
                else
                {
                    if (seli.Estado != null && seli.Estado > 0)
                    {
                        TablaClaveDto tc = this.Servicio.ReadTablaClave(String.Format(this.FormatoClave, seli.Estado));
                        if (tc != null)
                            esn = tc.Denominacion;
                    }
                }

                Utiles.TablaCadena tb = new Utiles.TablaCadena();
                String enc = "Eliminar servicio en cuenta de Agua\r\n";
                enc = enc +  "====================================\r\n";
                tb.AddRow("Atributo", "Valor");
                tb.AddRow("--------", "-----");
                tb.AddRow("Id", seli.Id);
                tb.AddRow("Cuenta Id", seli.Cuenta);
                tb.AddRow("Codigo", cod);
                tb.AddRow("Concepto Id", seli.Concepto);
                tb.AddRow("Concepto", con);
                tb.AddRow("Estado Id", seli.Estado);
                tb.AddRow("Estado", esn);

                this.CrearSeguimiento(tb, "Operacion de Mantenimiento de servicios de agua", Utiles.EntidadesEnum.EnAguaServicios, enc);
            }
        }

        public virtual TablaClaveDto CuentaServicioActivo()
        {
            return ModeloCache.Instance.McClaves.Where(t => t.Tabla == 22 && t.Clave == 0).FirstOrDefault();
        }

        #endregion

        #region Operaciones de lecturas de medidores

        /// <summary>
        /// Traer las lecturas habilitadas del año dado
        /// </summary>
        /// <param name="pAño">Año de consulta</param>
        /// <returns></returns>
        public IEnumerable<AguaLecturaDto> LecturasTraerPorAño(int pAño)
        {
            IEnumerable<AguaLecturaDto> res = this.Servicio.ReadAguaLecturas().Where(l => l.Año == pAño && l.Estado == 0);
            foreach (AguaLecturaDto l in res)
            {
                if (l.Cuenta != null && l.Cuenta > 0)
                {
                    l.CuentaAguaNav = this.CuentaMinPorId((int)l.Cuenta);
                }
            }
            return res;
        }

        /// <summary>
        /// Traer las lecturas habilitadas por codigo
        /// </summary>
        /// <param name="pAño">Año de consulta</param>
        /// <param name="pCodigo">Expresion de codigo de busca</param>
        /// <returns></returns>
        public IEnumerable<AguaLecturaDto> LecturasTraerPorAñoCodigo(int pAño, string pCodigo)
        {
            IEnumerable<AguaLecturaDto> les = this.Servicio.ReadAguaLecturasFiltered("", String.Format("año == {0} and estado = {1} and codigo.Contains(\"{2}\")", pAño, 0, pCodigo));
            foreach (AguaLecturaDto l in les)
            {
                if (l.Cuenta != null && l.Cuenta > 0)
                {
                    l.CuentaAguaNav = this.CuentaMinPorId((int)l.Cuenta);
                }
            }
            return les;
        }

        /// <summary>
        /// Traer la lectura de la cuenta y el año dados
        /// </summary>
        /// <param name="pCta">Cuenta a consultar</param>
        /// <param name="pAño">Año a consultar</param>
        /// <returns></returns>
        public AguaLecturaDto LecturaTraerPorCuentaAño(int pCta, int pAño)
        {
            AguaLecturaDto lec = this.Servicio.ReadAguaLecturas().Where(l => l.Cuenta == pCta && l.Año == pAño).FirstOrDefault();
            if (lec.Cuenta != null && lec.Cuenta > 0)
            {
                lec.CuentaAguaNav = this.CuentaMinPorId((int)lec.Cuenta);
            }
            return lec;
        }

        /// <summary>
        /// Traer lectura por el Id
        /// </summary>
        /// <param name="pId">Id de lectura</param>
        /// <returns></returns>
        public AguaLecturaDto LecturaTraerPorId(int pId)
        {
            AguaLecturaDto lec = this.Servicio.ReadAguaLecturas().Where(l => l.Id == pId).FirstOrDefault();
            if (lec.Cuenta != null && lec.Cuenta > 0)
            {
                lec.CuentaAguaNav = this.CuentaMinPorId((int)lec.Cuenta);
            }
            return lec;
        }

        #endregion
    
    }
}
