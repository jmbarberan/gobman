using System;
using System.Linq;
using Intelligob.Cliente.Referencia;
using System.Collections.Generic;
using Intelligob.Utiles;

namespace Intelligob.Cliente.Depositos
{
    public class CatastrosDep : DepositoBase
    {
        #region Operaciones de predios
        public CatastrosDep() : base(DepositosControl.Instance.Servicio) { }
        public CatastrosDep(IEntidades servicio) : base(servicio) { }

        public virtual PredioBaseDto PredioPorId(int pId)
        {
            string filtro = string.Format(this.FormatoClave, pId);
            Intelligob.Cliente.Referencia.PredioBaseDto p = this.Servicio.ReadPredioBase(filtro);
            p.DominioNav = ModeloCache.Instance.McClaves.Where(c => c.Id == p.Dominio).FirstOrDefault();
            return p;
            //return this.Servicio.ReadPredioBasesFiltered(String.Empty, filtro).FirstOrDefault();
        }
        
        public virtual void PredioModificar(PredioBaseDto pre)
        {
            PredioBaseDto oldpre = this.Servicio.ReadPredioBase(String.Format(this.FormatoClave, pre.Id));
            oldpre.PropietariosNav = this.Servicio.ReadPredioPropietariosFiltered("", String.Format("predio = {0}", oldpre.Id));
            this.Servicio.UpdatePredioBase(pre);

            String sdom = String.Empty;
            if (pre.DominioNav != null)
                sdom = pre.DominioNav.Denominacion;
            else
            {
                if (pre.Dominio != null && pre.Dominio > 0)
                {
                    TablaClaveDto edom = ModeloCache.Instance.McClaves.Where(o => o.Id == pre.Dominio).FirstOrDefault();
                    if (edom != null)
                        sdom = edom.Denominacion;
                }
            }

            String soldom = String.Empty;
            if (oldpre.Dominio != null && oldpre.Dominio > 0)
            {
                TablaClaveDto eodom = ModeloCache.Instance.McClaves.Where(o => o.Id == oldpre.Dominio).FirstOrDefault();
                if (eodom != null)
                    soldom = eodom.Denominacion;
            }

            Utiles.TablaCadena tb = new Utiles.TablaCadena();
            String enc = "Modificar predio\r\n";
            enc = enc + "================\r\n";
            tb.AddRow("Atributo       ", "Valor Ant.", "Valor nuevo");
            tb.AddRow("---------------", "----------", "-----------");
            tb.AddRow("Id", pre.Id, pre.Id);
            tb.AddRow("Codigo", pre.Codigo, oldpre.Codigo);
            tb.AddRow("Propietario(s)", pre.PropietariosCadena, oldpre.PropietariosCadena);
            tb.AddRow("Ubicacion", pre.Ubicacion, oldpre.Ubicacion);
            tb.AddRow("Direccion", pre.Direccion, oldpre.Direccion);
            tb.AddRow("Escritura", pre.Escritura, oldpre.Escritura);
            tb.AddRow("Tipo de Dominio", sdom, soldom);
            if (pre.FormatoCodigo == 1)
            {
                tb.AddRow("Nombre inmueble", pre.NombreInmueble, oldpre.NombreInmueble);
            }
            this.CrearSeguimiento(tb, "Operacion de Modificacion de predio", Utiles.EntidadesEnum.EnPredios, enc);
        }

        public virtual int PredioCrear(PredioBaseDto pre)
        {
            String s = this.Servicio.CreatePredioBase(pre);
            s = s.Replace("Id=", "");

            String sdom = String.Empty;
            if (pre.DominioNav != null)
                sdom = pre.DominioNav.Denominacion;
            else
            {
                if (pre.Dominio > 0)
                {
                    TablaClaveDto edom = ModeloCache.Instance.McClaves.Where(o => o.Id == pre.Dominio).FirstOrDefault();
                    if (edom != null)
                        sdom = edom.Denominacion;
                }
            }           
            
            Utiles.TablaCadena tb = new Utiles.TablaCadena();
            String enc = "Registrar nuevo predio\r\n";
            enc = enc  + "==========================>\r\n";
            tb.AddRow("Atributo        ", "Valor ");
            tb.AddRow("----------------", "----->");
            tb.AddRow("Id", s);
            tb.AddRow("Codigo", pre.Codigo);
            tb.AddRow("Propietario(s)", pre.PropietariosCadena);
            tb.AddRow("Ubicacion", pre.Ubicacion);
            tb.AddRow("Direccion", pre.Direccion);
            tb.AddRow("Escritura", pre.Escritura);
            tb.AddRow("Tipo de Dominio", sdom);
            if (pre.FormatoCodigo == 1)
            {
                tb.AddRow("Nombre inmueble", pre.NombreInmueble);
            }
            this.CrearSeguimiento(tb, "Operacion de creacion de predio", Utiles.EntidadesEnum.EnPredios, enc);

            return Convert.ToInt32(s);
        }

        public virtual IEnumerable<PredioBaseDto> PrediosPorContribuyente(int pContribuyente, int pEstado, int pTipoPredio)
        {
            return this.PrediosPorContribuyente(pContribuyente, pEstado, pTipoPredio, true);
        }

        public virtual IEnumerable<PredioBaseDto> PrediosPorContribuyente(int pContribuyente, int pEstado, int pTipoPredio, bool pRelacionados)
        {
            List<PredioBaseDto> res = new List<PredioBaseDto>(); 
            // Traer los PredioPropietario asociados al propietario buscado
            IEnumerable<PredioPropietarioDto> pros = this.Servicio.ReadPredioPropietariosFiltered("", String.Format("contribuyente = {0}", pContribuyente));
            foreach (PredioPropietarioDto pro in pros)
            {
                // traer los datos de cada PredioBase encontrado en los propietarios
                PredioBaseDto pb = new PredioBaseDto();
                pb.Id = -1;
                if (pEstado == 9)
                {
                    if (pTipoPredio == 9)
                        pb = this.Servicio.ReadPredioBasesFiltered("", String.Format("id = {0}", pro.Predio)).FirstOrDefault(); //.Where(p => p.Id == pro.Predio).FirstOrDefault();                        
                    else
                        pb = this.Servicio.ReadPredioBasesFiltered("", String.Format("id = {0} and formatoCodigo = {1}", pro.Predio, pTipoPredio)).FirstOrDefault(); //.Where(p => p.Id == pro.Predio && p.FormatoCodigo == pTipoPredio).FirstOrDefault();
                }
                else
                {
                    if (pTipoPredio == 9)
                        pb = this.Servicio.ReadPredioBasesFiltered("", String.Format("id = {0} and estado = {1}", pro.Predio, pEstado)).FirstOrDefault(); //.Where(p => p.Id == pro.Predio && p.Estado == pEstado).FirstOrDefault();                        
                    else
                        pb = this.Servicio.ReadPredioBasesFiltered("", String.Format("id = {0} and formatoCodigo = {1} and estado = {2}", pro.Predio, pTipoPredio, pEstado)).FirstOrDefault(); //.Where(p => p.Id == pro.Predio && p.Estado == pEstado && p.FormatoCodigo == pTipoPredio).FirstOrDefault();                        
                }
                if (pb != null && pb.Id > 0)
                {
                    res.Add(pb);
                }
            }
            foreach (PredioBaseDto pre in res)
            {
                pre.PropietariosNav = PropietariosPorPredio(pre.Id).ToArray();                
            }
            if (res.Count > 0 && pRelacionados)
                this.PrediosTraerRelacionados(res);
            return res;
        }

        public virtual IEnumerable<PredioBaseDto> PrediosPorCodigo(String pCodigo, int pEstado, TipoBusquedaTexto pTipoBusqueda, int pTipoPredio)
        {
            return this.PrediosPorCodigo(pCodigo, pEstado, pTipoBusqueda, pTipoPredio, true);
        }

        public virtual IEnumerable<PredioBaseDto> PrediosPorCodigo(String pCodigo, int pEstado, TipoBusquedaTexto pTipoBusqueda, int pTipoPredio, bool pRelacionados)
        {            
            PredioBaseDto[] res;
            if (pTipoPredio == 9)
            {
                switch (pTipoBusqueda)
                {
                    case TipoBusquedaTexto.tbComenzando :
                        {
                            if (pEstado == 9)
                            {
                                res = this.Servicio.ReadPredioBasesFiltered("codigo", String.Format("codigo.StartsWith(\"{0}\")", pCodigo));
                            }
                            else
                            {
                                res = this.Servicio.ReadPredioBasesFiltered("codigo", String.Format("codigo.StartsWith(\"{0}\") and estado = {1}", pCodigo, pEstado));
                            }
                            break;
                        }
                    case TipoBusquedaTexto.tbConteniendo :
                        {
                            if (pEstado == 9)
                            {
                                res = this.Servicio.ReadPredioBasesFiltered("codigo", String.Format("codigo.Contains(\"{0}\")", pCodigo));
                            }
                            else
                            {
                                res = this.Servicio.ReadPredioBasesFiltered("codigo", String.Format("codigo.Contains(\"{0}\") and estado = {1}", pCodigo, pEstado));
                            }
                            break;
                        }
                    default: // Codigo exacto
                        {
                            if (pEstado == 9)
                            {
                                res = this.Servicio.ReadPredioBasesFiltered("codigo", String.Format("codigo = {0}", pCodigo));
                            }
                            else
                            {
                                res = this.Servicio.ReadPredioBasesFiltered("codigo", String.Format("codigo = {0} and estado = {1}", pCodigo, pEstado));
                            }
                            break;
                        }
                }
            }
            else
            {
                switch (pTipoBusqueda)
                {
                    case TipoBusquedaTexto.tbComenzando:
                        {
                            if (pEstado == 9)
                            {
                                res = this.Servicio.ReadPredioBasesFiltered("codigo", String.Format("codigo.StartsWith(\"{0}\") and formatoCodigo = {1}", pCodigo, pTipoPredio));
                            }
                            else
                            {
                                res = this.Servicio.ReadPredioBasesFiltered("codigo", String.Format("codigo.StartsWith(\"{0}\") and formatoCodigo = {1} and estado = {2}", pCodigo, pTipoPredio, pEstado));
                            }
                            break;
                        }
                    case TipoBusquedaTexto.tbConteniendo:
                        {
                            if (pEstado == 9)
                            {
                                res = this.Servicio.ReadPredioBasesFiltered("codigo", String.Format("codigo.Contains(\"{0}\") and formatoCodigo = {1}", pCodigo, pTipoPredio));
                            }
                            else
                            {
                                res = this.Servicio.ReadPredioBasesFiltered("codigo", String.Format("codigo.Contains(\"{0}\") and formatoCodigo = {1} and estado = {2}", pCodigo, pTipoPredio, pEstado));
                            }
                            break;
                        }                    
                    default: // Codigo exacto
                        {
                            if (pEstado == 9)
                            {
                                res = this.Servicio.ReadPredioBasesFiltered("codigo", String.Format("codigo = {0} and formatoCodigo = {1}", pCodigo, pTipoPredio));
                            }
                            else
                            {
                                res = this.Servicio.ReadPredioBasesFiltered("codigo", String.Format("codigo = {0} and formatoCodigo = {1} and estado = {2}", pCodigo, pTipoPredio, pEstado));
                            }
                            break;
                        }
                }
            }
            if (res != null)
            {
                foreach (PredioBaseDto pre in res)
                {
                    pre.PropietariosNav = PropietariosPorPredio(pre.Id).ToArray();
                }
                if (res.Count() > 0 && pRelacionados)
                    this.PrediosTraerRelacionados(res);
            }            
            return res;
        }

        public virtual void PrediosTraerRelacionados(IEnumerable<PredioBaseDto> predios)
        {
            foreach(PredioBaseDto pre in predios)
            {
                if (pre.ViaMaterial != null && pre.ViaMaterial > 0)
                    pre.ViaMaterialNav = ModeloCache.Instance.McClaves.Where(c => c.Id == pre.ViaMaterial).FirstOrDefault();
                if (pre.Dominio != null && pre.Dominio > 0)
                    pre.DominioNav = ModeloCache.Instance.McClaves.Where(c => c.Id == pre.Dominio).FirstOrDefault();
                if (pre.ModoPropiedad != null && pre.ModoPropiedad > 0)
                    pre.ModoPropiedadNav = ModeloCache.Instance.McClaves.Where(c => c.Id == pre.ModoPropiedad).FirstOrDefault();
                if (pre.TipoPropiedad != null && pre.TipoPropiedad > 0)
                    pre.TipoPropiedadNav = ModeloCache.Instance.McClaves.Where(c => c.Id == pre.TipoPropiedad).FirstOrDefault();
                if (pre.PredioAgua != null && pre.PredioAgua > 0)
                    pre.PreAguaNav = ModeloCache.Instance.McClaves.Where(c => c.Id == pre.PredioAgua).FirstOrDefault();
                if (pre.PredioAlcantarillado != null && pre.PredioAlcantarillado > 0)
                    pre.PreAlcantarilladoNav = ModeloCache.Instance.McClaves.Where(c => c.Id == pre.PredioAlcantarillado).FirstOrDefault();
            }
        }

        public virtual IEnumerable<PredioPropietarioDto> PropietariosPorPredio(int pId)
        {
            IEnumerable<PredioPropietarioDto> pros = this.Servicio.ReadPredioPropietariosFiltered("", String.Format("predio = {0}", pId));
            foreach(PredioPropietarioDto p in pros)
            {
                p.ContribuyenteNav = this.Servicio.ReadContribuyente(String.Format(this.FormatoClave, p.Contribuyente));
                if (p.Contribuyente != null && p.Contribuyente > 0)
                    p.ContribuyenteNav.PersoneriaNav = ModeloCache.Instance.McClaves.Where(t => t.Id == p.ContribuyenteNav.Personeria).FirstOrDefault();
            }
            return pros;
        }

        public virtual PredioTerrenoDto TerrenoPorPredio(int pId)
        {
            PredioTerrenoDto ter = this.Servicio.ReadPredioTerrenosFiltered("", String.Format("predio = {0}", pId)).FirstOrDefault(); 
            if (ter != null)
            {
                if (ter.CalidadSuelo != null && ter.CalidadSuelo > 0)
                    ter.CalidadSueloNav = ModeloCache.Instance.McClaves.Where(c => c.Id == ter.CalidadSuelo).FirstOrDefault();
                if (ter.NivelRazante != null && ter.NivelRazante > 0)
                    ter.RazanteNav = ModeloCache.Instance.McClaves.Where(c => c.Id == ter.NivelRazante).FirstOrDefault();
                if (ter.LocalizacionManzana != null && ter.LocalizacionManzana > 0)
                    ter.LocManzanaNav = ModeloCache.Instance.McClaves.Where(c => c.Id == ter.LocalizacionManzana).FirstOrDefault();
            }
            return ter;
        }

        public virtual IEnumerable<PredioTerrenoDto> TerrenosPorPredio(int pId)
        {
            IEnumerable<PredioTerrenoDto> ters = this.Servicio.ReadPredioTerrenosFiltered("", String.Format("predio = {0}", pId));
            foreach(PredioTerrenoDto t in ters)
            {
                if (t.CalidadSuelo != null && t.CalidadSuelo > 0)
                    t.CalidadSueloNav = ModeloCache.Instance.McClaves.Where(c => c.Id == t.CalidadSuelo).FirstOrDefault();
                if (t.NivelRazante != null && t.NivelRazante > 0)
                    t.RazanteNav = ModeloCache.Instance.McClaves.Where(c => c.Id == t.NivelRazante).FirstOrDefault();
                if (t.LocalizacionManzana != null && t.LocalizacionManzana > 0)
                    t.LocManzanaNav = ModeloCache.Instance.McClaves.Where(c => c.Id == t.LocalizacionManzana).FirstOrDefault();
            }
            return ters;
        }

        public virtual int TerrenoCrear(PredioTerrenoDto ter)
        {
            String s1 = this.Servicio.CreatePredioTerreno(ter);
            String s = s1.Replace("Id=", "");

            int fcod = 0;
            String cod = String.Empty;
            if (ter.Predio > 0)
            {
                PredioBaseDto p = this.Servicio.ReadPredioBase(s1);
                if (p != null)
                {
                    cod = p.Codigo;
                    fcod = (int)p.FormatoCodigo;
                }
            }

            String newlocman = String.Empty;
            String newcalsue = String.Empty;
            String newnivraz = String.Empty;
            if (fcod == 0)
            {
                if (ter.LocManzanaNav != null)
                    newlocman = ter.LocManzanaNav.Denominacion;
                else
                {
                    if (ter.LocalizacionManzana != null && ter.LocalizacionManzana > 0)
                    {
                        TablaClaveDto tloma = ModeloCache.Instance.McClaves.Where(o => o.Id == ter.LocalizacionManzana).FirstOrDefault();
                        if (tloma != null)
                            newlocman = tloma.Denominacion;
                    }
                }

                if (ter.CalidadSueloNav != null)
                    newcalsue = ter.CalidadSueloNav.Denominacion;
                else
                {
                    if (ter.CalidadSuelo != null && ter.CalidadSuelo > 0)
                    {
                        TablaClaveDto tcalsue = ModeloCache.Instance.McClaves.Where(o => o.Id == ter.CalidadSuelo).FirstOrDefault();
                        if (tcalsue != null)
                            newcalsue = tcalsue.Denominacion;
                    }
                }

                if (ter.RazanteNav != null)
                    newnivraz = ter.RazanteNav.Denominacion;
                else
                {
                    if (ter.NivelRazante != null && ter.NivelRazante > 0)
                    {
                        TablaClaveDto tnivraz = ModeloCache.Instance.McClaves.Where(o => o.Id == ter.NivelRazante).FirstOrDefault();
                        if (tnivraz != null)
                            newnivraz = tnivraz.Denominacion;
                    }
                }
            }

            Utiles.TablaCadena tb = new Utiles.TablaCadena();
            String enc = "Registrar nuevo terreno de predio\r\n";
            enc = enc +  "=================================>\r\n";
            tb.AddRow("Atributo        ", "Valor ");
            tb.AddRow("----------------", "----->");
            tb.AddRow("Id", s);
            tb.AddRow("Predio Id", ter.Predio);
            tb.AddRow("Predio Codigo", cod);
            tb.AddRow("Superficie", ter.Superficie);
            tb.AddRow("Frente", ter.Frente);
            tb.AddRow("Fondo", ter.Fondo);
            if (fcod == 1)
            {
                tb.AddRow("Clase de tierra", ter.ClaseTierra);
                tb.AddRow("Zona homogenea", ter.ZonaHomogenea);
            }
            else
            {
                if (fcod == 0)
                {
                    tb.AddRow("Local. manzana", newlocman);
                    tb.AddRow("Calidad suelo", newcalsue);
                    tb.AddRow("Nivel razante", newnivraz);
                }
            }
            tb.AddRow("Lindero Norte", ter.LinderoNorteNombres);
            tb.AddRow("Lind Norte Extn.", ter.LinderoNorteExtension);
            tb.AddRow("Lindero Sur", ter.LinderoSurNombres);
            tb.AddRow("Lind Sur Extn.", ter.LinderoSurExtension);
            tb.AddRow("Lindero Este", ter.LinderoEsteNombres);
            tb.AddRow("Lind Este Extn.", ter.LinderoEsteExtension);
            tb.AddRow("Lindero Oeste", ter.LinderoOesteNombres);
            tb.AddRow("Lind Oeste Extn.", ter.LinderoOesteExtension);
            tb.AddRow("Numero lados", ter.NumeroLados);
            tb.AddRow("Numero Angulos", ter.NumeroAngulosRectos);
            tb.AddRow("Perimetro", ter.Perimetro);
            this.CrearSeguimiento(tb, "Operacion crear terreno de predio", Utiles.EntidadesEnum.EnPrediosTerrenos, enc);

            return Convert.ToInt32(s);
        }

        public virtual void TerrenoModificar(PredioTerrenoDto ter)
        {
            this.Servicio.UpdatePredioTerreno(ter);

            #region Generar pista de auditoria

            int fcod = 0;
            String cod = String.Empty;
            if (ter.Predio > 0)
            {
                PredioBaseDto p = this.Servicio.ReadPredioBase(String.Format(FormatoClave, ter.Predio));
                if (p != null)
                {
                    cod = p.Codigo;
                    fcod = (int)p.FormatoCodigo;
                }
            }

            String locman = String.Empty;
            String calsue = String.Empty;
            String nivraz = String.Empty;
            if (fcod == 0)
            {
                if (ter.LocManzanaNav != null)
                    locman = ter.LocManzanaNav.Denominacion;
                else
                {
                    if (ter.LocalizacionManzana != null && ter.LocalizacionManzana > 0)
                    {
                        TablaClaveDto tloma = ModeloCache.Instance.McClaves.Where(o => o.Id == ter.LocalizacionManzana).FirstOrDefault();
                        if (tloma != null)
                            locman = tloma.Denominacion;
                    }
                }

                if (ter.CalidadSueloNav != null)
                    calsue = ter.CalidadSueloNav.Denominacion;
                else
                {
                    if (ter.CalidadSuelo != null && ter.CalidadSuelo > 0)
                    {
                        TablaClaveDto tcalsue = ModeloCache.Instance.McClaves.Where(o => o.Id == ter.CalidadSuelo).FirstOrDefault();
                        if (tcalsue != null)
                            calsue = tcalsue.Denominacion;
                    }
                }

                if (ter.RazanteNav != null)
                    nivraz = ter.RazanteNav.Denominacion;
                else
                {
                    if (ter.NivelRazante != null && ter.NivelRazante > 0)
                    {
                        TablaClaveDto tnivraz = ModeloCache.Instance.McClaves.Where(o => o.Id == ter.NivelRazante).FirstOrDefault();
                        if (tnivraz != null)
                            nivraz = tnivraz.Denominacion;
                    }
                }
            }

            Utiles.TablaCadena tb = new Utiles.TablaCadena();
            String enc = "Registrar nuevo terreno de predio\r\n";
            enc = enc + "=================================>\r\n";
            tb.AddRow("Atributo        ", "Valor ");
            tb.AddRow("----------------", "----->");
            tb.AddRow("Id", ter.Id);
            tb.AddRow("Predio Id", ter.Predio);
            tb.AddRow("Predio Codigo", cod);
            tb.AddRow("Superficie", ter.Superficie);
            tb.AddRow("Frente", ter.Frente);
            tb.AddRow("Fondo", ter.Fondo);
            if (fcod == 1)
            {
                tb.AddRow("Clase de tierra", ter.ClaseTierra);
                tb.AddRow("Zona homogenea", ter.ZonaHomogenea);
            }
            else
            {
                if (fcod == 0)
                {
                    tb.AddRow("Local. manzana", locman);
                    tb.AddRow("Calidad suelo", calsue);
                    tb.AddRow("Nivel razante", nivraz);
                }
            }
            tb.AddRow("Lindero Norte", ter.LinderoNorteNombres);
            tb.AddRow("Lind Norte Extn.", ter.LinderoNorteExtension);
            tb.AddRow("Lindero Sur", ter.LinderoSurNombres);
            tb.AddRow("Lind Sur Extn.", ter.LinderoSurExtension);
            tb.AddRow("Lindero Este", ter.LinderoEsteNombres);
            tb.AddRow("Lind Este Extn.", ter.LinderoEsteExtension);
            tb.AddRow("Lindero Oeste", ter.LinderoOesteNombres);
            tb.AddRow("Lind Oeste Extn.", ter.LinderoOesteExtension);
            tb.AddRow("Numero lados", ter.NumeroLados);
            tb.AddRow("Numero Angulos", ter.NumeroAngulosRectos);
            tb.AddRow("Perimetro", ter.Perimetro);
            this.CrearSeguimiento(tb, "Operacion crear terreno de predio", Utiles.EntidadesEnum.EnPrediosTerrenos, enc);

            #endregion
        }

        public virtual IEnumerable<PredioFrenteDto> FrentesPorPredio(int pId)
        {
            return this.Servicio.ReadPredioFrentesFiltered("", String.Format("predio = {0}", pId));
        }

        public virtual IEnumerable<PredioFotoDto> FotosPorPredio(int pId)
        {
            return this.Servicio.ReadPredioFotosFiltered("", String.Format("predio = {0}", pId));
        }

        public virtual IEnumerable<PredioBloqueDto> BloquesPorPredio(int pId)
        {
            return Servicio.ReadPredioBloquesFiltered("", String.Format("predio = {0}", pId));
        }

        public virtual IEnumerable<PredioPisoDto> PisosPorBloque(int pBloque)
        {
            IEnumerable<PredioPisoDto> pisos = this.Servicio.ReadPredioPisosFiltered("", String.Format("bloque = {0}", pBloque));
            foreach(PredioPisoDto p in pisos)
            {
                if (p.Conservacion != null && p.Conservacion > 0)
                    p.ConservacionNav = ModeloCache.Instance.McClaves.Where(c => c.Id == p.Conservacion).FirstOrDefault();
            }
            return pisos;
        }

        public virtual IEnumerable<PredioConstruccionDto> ComponentesPorPiso(int pPiso)
        {
            IEnumerable<PredioConstruccionDto> cons = this.Servicio.ReadPredioConstruccionsFiltered("", String.Format("piso = {0}", pPiso));
            foreach(PredioConstruccionDto c in cons)
            {
                if (c.Elemento != null && c.Elemento > 0)
                    c.ConsElementoNav = ModeloCache.Instance.McClaves.Where(e => e.Id == c.Elemento).FirstOrDefault();
            }
            return cons;
        }

        public virtual IEnumerable<PredioBloqueDto> BloquesConstruccionPorPredio(int pId)
        {
            IEnumerable<PredioBloqueDto> bloques = this.BloquesPorPredio(pId);
            foreach (PredioBloqueDto blq in bloques)
            {
                blq.PisosNav = PisosPorBloque(blq.Id).ToArray();
                foreach (PredioPisoDto p in blq.PisosNav)
                {
                    p.ConstruccionesNav = ComponentesPorPiso(p.Id).ToArray();
                }
            }
            return bloques;
        }

        public virtual Boolean PredioCodigoRegistrado(String pCodigo, int pId, int? pTipo)
        {
            Boolean ret = false;
            IEnumerable<PredioBaseDto> pres = this.Servicio.ReadPredioBasesFiltered("", String.Format("codigo = \"{0}\" and id != {1} and formatoCodigo = {2}", pCodigo, pId, pTipo));
            if (pres != null && pres.Count() > 0)
            {
                ret = true;
            }
            return ret;
        }

        public virtual void PropietariosEliminar(IEnumerable<PredioPropietarioDto> pPropietarios)
        {
            this.Servicio.DeletePredioPropietarios(pPropietarios.ToArray());

            foreach (PredioPropietarioDto pp in pPropietarios)
            {
                String cod = String.Empty;
                if (pp.Predio != null && pp.Predio > 0)
                {
                    PredioBaseDto p = this.Servicio.ReadPredioBase(String.Format(this.FormatoClave, pp.Predio));
                    if (p != null)
                        cod = p.Codigo;
                }

                String nom = String.Empty;
                if (pp.ContribuyenteNav != null && pp.ContribuyenteNav.Nombres != null)
                    nom = pp.ContribuyenteNav.Nombres;
                else
                {
                    if (pp.Contribuyente != null && pp.Contribuyente > 0)
                    {
                        ContribuyenteDto c = this.Servicio.ReadContribuyente(String.Format(FormatoClave, pp.Contribuyente));
                        if (c != null)
                            nom = c.Nombres;
                    }
                    
                }

                TablaCadena tb = new TablaCadena();
                String enc = "Eliminacion de propietario de predio\r\n";
                enc = enc + "====================================\r\n";
                tb.AddRow("Atributo        ", "Valor ");
                tb.AddRow("----------------", "----->");
                tb.AddRow("Id", pp.Id);
                tb.AddRow("Predio Id", pp.Predio);
                tb.AddRow("Predio Codigo", cod);
                tb.AddRow("Contribuyente Id", pp.Contribuyente);
                tb.AddRow("Nombres", nom);

                this.CrearSeguimiento(tb, "Operacion de Mantenimiento de propietarios de predios", EntidadesEnum.EnPrediosPropietarios, enc);
            }
            
        }

        public virtual void PropietariosModificar(IEnumerable<PredioPropietarioDto> pPropietarios)
        {
            foreach(PredioPropietarioDto p in pPropietarios)
            {
                if (p.Id == 0)
                {
                    String opNom = String.Empty;
                    int opId =  0;
                    /*PredioPropietarioDto po = this.Servicio.ReadPredioPropietario(String.Format(this.FormatoClave, p.Id));*/
                    if (p.Contribuyente != null && p.Contribuyente > 0)
                    {
                        opId = (int)p.Contribuyente;
                        ContribuyenteDto oc = this.Servicio.ReadContribuyente(string.Format(FormatoClave, p.Contribuyente));
                        if (oc != null && oc.Nombres != null)
                            opNom = oc.Nombres;

                    }
                    String s = this.Servicio.CreatePredioPropietario(p);
                    s = s.Replace(FormatoClave, "");
                    int i = 0;
                    try
                    {
                        i = Convert.ToInt32(s);
                    }
                    catch { }

                    String cod = String.Empty;
                    if (p.Predio != null && p.Predio > 0)
                    {
                        PredioBaseDto pb = this.Servicio.ReadPredioBase(String.Format(this.FormatoClave, p.Predio));
                        if (pb != null)
                            cod = pb.Codigo;
                    }

                    String nom = String.Empty;
                    if (p.ContribuyenteNav != null && p.ContribuyenteNav.Nombres != null)
                        nom = p.ContribuyenteNav.Nombres;
                    else
                    {
                        if (p.Contribuyente != null && p.Contribuyente > 0)
                        {
                            ContribuyenteDto c = this.Servicio.ReadContribuyente(String.Format(FormatoClave, p.Contribuyente));
                            if (c != null)
                                nom = c.Nombres;
                        }

                    }

                    TablaCadena tb = new TablaCadena();
                    String enc = "Modificacion de propietario de predio\r\n";
                    enc = enc + "=====================================\r\n";
                    tb.AddRow("Atributo        ", "Valor Anterior ", "Valor nuevo ");
                    tb.AddRow("----------------", "-------------->", "----------->");
                    tb.AddRow("Id", i, i);
                    tb.AddRow("Predio Id", p.Predio, p.Predio);
                    tb.AddRow("Contribuyente Id", p.Contribuyente, opId);
                    tb.AddRow("Nombres", nom, opNom);

                    this.CrearSeguimiento(tb, "Operacion de Mantenimiento de propietarios de predios", EntidadesEnum.EnPrediosPropietarios, enc);
                }
                else
                {
                    if (p.Id > 0)
                    {
                        this.Servicio.UpdatePredioPropietario(p);

                        String cod = String.Empty;
                        if (p.Predio != null && p.Predio > 0)
                        {
                            PredioBaseDto pb = this.Servicio.ReadPredioBase(String.Format(this.FormatoClave, p.Predio));
                            if (p != null)
                                cod = pb.Codigo;
                        }

                        String nom = String.Empty;
                        if (p.ContribuyenteNav != null && p.ContribuyenteNav.Nombres != null)
                            nom = p.ContribuyenteNav.Nombres;
                        else
                        {
                            if (p.Contribuyente != null && p.Contribuyente > 0)
                            {
                                ContribuyenteDto c = this.Servicio.ReadContribuyente(String.Format(FormatoClave, p.Contribuyente));
                                if (c != null)
                                    nom = c.Nombres;
                            }

                        }

                        TablaCadena tb = new TablaCadena();
                        String enc = "Crecion de propietario de predio\r\n";
                        enc = enc + "================================\r\n";
                        tb.AddRow("Atributo        ", "Valor ");
                        tb.AddRow("----------------", "----->");
                        tb.AddRow("Id", p.Id);
                        tb.AddRow("Predio Id", p.Predio);
                        tb.AddRow("Predio Codigo", cod);
                        tb.AddRow("Contribuyente Id", p.Contribuyente);
                        tb.AddRow("Nombres", nom);

                        this.CrearSeguimiento(tb, "Operacion de Mantenimiento de propietarios de predios", EntidadesEnum.EnPrediosPropietarios, enc);
                    }
                }
            }
        }

        public virtual void BloquesEliminar(IEnumerable<PredioBloqueDto> pBloques)
        {
            this.Servicio.DeletePredioBloques(pBloques.ToArray());
            foreach(PredioBloqueDto pb in pBloques)
            {
                String cod = String.Empty;
                if (pb.Predio != null && pb.Predio > 0)
                {
                    PredioBaseDto p = this.Servicio.ReadPredioBase(String.Format(this.FormatoClave, pb.Predio));
                    if (p != null)
                        cod = p.Codigo;
                }

                TablaCadena tb = new TablaCadena();
                String enc = "Eliminacion de bloque de predio\r\n";
                enc = enc + "===============================\r\n";
                tb.AddRow("Atributo     ", "Valor ");
                tb.AddRow("-------------", "----->");
                tb.AddRow("Id", pb.Id);
                tb.AddRow("Predio Id", pb.Predio);
                tb.AddRow("Predio Codigo", cod);
                tb.AddRow("Descripcion", pb.Descripcion);

                this.CrearSeguimiento(tb, "Operacion de Mantenimiento de propietarios de predios", EntidadesEnum.EnPrediosBloques, enc);
            }
            
        }

        public virtual int BloqueCrear(PredioBloqueDto blq)
        {
            String s = this.Servicio.CreatePredioBloque(blq);
            s = s.Replace("Id=", "");

            String cod = String.Empty;
            if (blq.Predio != null && blq.Predio > 0)
            {
                PredioBaseDto p = this.Servicio.ReadPredioBase(String.Format(this.FormatoClave, blq.Predio));
                if (p != null)
                    cod = p.Codigo;
            }

            TablaCadena tb = new TablaCadena();
            String enc = "Creacion de bloque de predio\r\n";
            enc = enc + "============================\r\n";
            tb.AddRow("Atributo     ", "Valor ");
            tb.AddRow("-------------", "----->");
            tb.AddRow("Id", blq.Id);
            tb.AddRow("Predio Id", blq.Predio);
            tb.AddRow("Predio Codigo", cod);
            tb.AddRow("Descripcion", blq.Descripcion);

            this.CrearSeguimiento(tb, "Operacion de Mantenimiento de propietarios de predios", EntidadesEnum.EnPrediosBloques, enc);

            return Convert.ToInt32(s);
        }

        public virtual void PisosEliminar(IEnumerable<PredioPisoDto> pPisos)
        {
            this.Servicio.DeletePredioPisos(pPisos.ToArray());

            foreach(PredioPisoDto ps in pPisos)
            {
                String sbl = String.Empty;
                if (ps.Bloque != null && ps.Bloque > 0)
                {
                    PredioBloqueDto p = this.Servicio.ReadPredioBloque(String.Format(this.FormatoClave, ps.Bloque));
                    if (p != null)
                        sbl = p.Descripcion;
                }

                String consv = String.Empty;
                if (ps.ConservacionNav != null)
                    consv = ps.ConservacionNav.Denominacion;
                else
                {
                    TablaClaveDto d = ModeloCache.Instance.McClaves.Where((o) => o.Id == ps.Conservacion).FirstOrDefault();
                    if (d != null)
                        consv = d.Denominacion;
                }

                TablaCadena tb = new TablaCadena();
                String enc = "Eliminacion de pisos de predio\r\n";
                enc = enc + "==============================\r\n";
                tb.AddRow("Atributo           ", "Valor ");
                tb.AddRow("-------------------", "----->");
                tb.AddRow("Id", ps.Id);
                tb.AddRow("Bloque Id", ps.Bloque);
                tb.AddRow("Bloque dscrp.", sbl);
                tb.AddRow("Piso dscrp.", ps.Descripcion);
                tb.AddRow("Conservacion Id", ps.Conservacion);
                tb.AddRow("Conservacion Dscrp.", consv);

                this.CrearSeguimiento(tb, "Operacion de Mantenimiento de pisos de predios", EntidadesEnum.EnPrediosPisos, enc);
            }   
        }

        public virtual int PisoCrear(PredioPisoDto pPiso)
        {
            String s = this.Servicio.CreatePredioPiso(pPiso);
            s = s.Replace("Id=", "");

            String sbl = String.Empty;
            if (pPiso.Bloque != null && pPiso.Bloque > 0)
            {
                PredioBloqueDto p = this.Servicio.ReadPredioBloque(String.Format(this.FormatoClave, pPiso.Bloque));
                if (p != null)
                    sbl = p.Descripcion;
            }

            String consv = String.Empty;
            if (pPiso.ConservacionNav != null)
                consv = pPiso.ConservacionNav.Denominacion;
            else
            {
                TablaClaveDto d = ModeloCache.Instance.McClaves.Where((o) => o.Id == pPiso.Conservacion).FirstOrDefault();
                if (d != null)
                    consv = d.Denominacion;
            }

            TablaCadena tb = new TablaCadena();
            String enc = "Creacion de bloque de predio\r\n";
            enc = enc + "============================\r\n";
            tb.AddRow("Atributo           ", "Valor ");
            tb.AddRow("-------------------", "----->");
            tb.AddRow("Id", s);
            tb.AddRow("Bloque Id", pPiso.Bloque);
            tb.AddRow("Bloque dscrp.", sbl);
            tb.AddRow("Piso dscrp.", pPiso.Descripcion);
            tb.AddRow("Conservacion Id", pPiso.Conservacion);
            tb.AddRow("Conservacion Dscrp.", consv);
            tb.AddRow("Superficie", pPiso.Superficie);

            this.CrearSeguimiento(tb, "Operacion de Mantenimiento de pisos de bloque de construccion", EntidadesEnum.EnPrediosPisos, enc);

            return Convert.ToInt32(s);
        }

        public virtual void TerrenosEliminar(IEnumerable<PredioTerrenoDto> pTerrenos)
        {
            this.Servicio.DeletePredioTerrenos(pTerrenos.ToArray());

            foreach(PredioTerrenoDto pp in pTerrenos)
            {
                int? tipo = 0;
                String cod = String.Empty;
                if (pp.Predio != null && pp.Predio > 0)
                {
                    PredioBaseDto p = this.Servicio.ReadPredioBase(String.Format(this.FormatoClave, pp.Predio));
                    if (p != null)
                        cod = p.Codigo;
                    tipo = p.FormatoCodigo;
                }                

                String nom = String.Empty;
                
                TablaCadena tb = new TablaCadena();
                String enc = "Eliminacion de terreno de predio\r\n";
                enc = enc + "================================\r\n";
                tb.AddRow("Atributo       ", "Valor ");
                tb.AddRow("---------------", "----->");
                tb.AddRow("Id", pp.Id);
                tb.AddRow("Predio Id", pp.Predio);
                tb.AddRow("Predio Codigo", cod);
                if (tipo == 1)
                    tb.AddRow("Clase de tierra", pp.ClaseTierra);
                tb.AddRow("Superficie", String.Format("{0:n4}", pp.Superficie));
                tb.AddRow("Lindero Norte", pp.LinderoNorteNombres);
                tb.AddRow("Lindero Sur", pp.LinderoSurNombres);
                tb.AddRow("Lindero Este", pp.LinderoEsteNombres);
                tb.AddRow("Lindero Oeste", pp.LinderoOesteNombres);

                this.CrearSeguimiento(tb, "Operacion de Mantenimiento de terrenos de predios", EntidadesEnum.EnPrediosTerrenos, enc);
            }

            
        }

        public virtual void TerrenosModificar(IEnumerable<PredioTerrenoDto> pTerrenos)
        {
            foreach (PredioTerrenoDto t in pTerrenos)
            {
                String pcod = String.Empty;
                int tipopre = 0;
                if (t.Predio != null && t.Predio > 0)
                {
                    PredioBaseDto p = this.Servicio.ReadPredioBase(String.Format(this.FormatoClave, t.Predio));
                    if (p != null)
                    {
                        if (p.Codigo != null)
                            pcod = p.Codigo;
                        if (p.FormatoCodigo != null)
                            tipopre = (int)p.FormatoCodigo;
                    }
                }

                if (t.Id > 0)
                {
                    PredioTerrenoDto oter = this.Servicio.ReadPredioTerreno(String.Format(this.FormatoClave,t.Id));
                    this.Servicio.UpdatePredioTerreno(t);

                    TablaCadena tb = new TablaCadena();
                    String enc = "Modificacion de terreno predial\r\n";
                    enc = enc + "===============================\r\n";
                    tb.AddRow("Atributo       ", "Valor Anterior ", "Valor nuevo ");
                    tb.AddRow("---------------", "-------------->", "----------->");
                    tb.AddRow("Id", t.Id, t.Id);
                    tb.AddRow("Predio Id", t.Predio, t.Predio);
                    tb.AddRow("Predio Codigo", pcod, pcod);
                    tb.AddRow("Superficie", String.Format("{0:n4}", t.Superficie), String.Format("{0:n4}", oter.Superficie));
                    if (tipopre == 1)
                    {
                        tb.AddRow("Clase de tierra", t.ClaseTierra, oter.ClaseTierra);
                        tb.AddRow("Zona homogenea", t.ZonaHomogenea, oter.ZonaHomogenea);
                    }
                    tb.AddRow("Lindero norte", t.LinderoNorteNombres, oter.LinderoNorteNombres);
                    tb.AddRow("Lindero sur", t.LinderoSurNombres, oter.LinderoSurNombres);
                    tb.AddRow("Lindero este", t.LinderoEsteNombres, oter.LinderoEsteNombres);
                    tb.AddRow("Lindero oeste", t.LinderoOesteNombres, oter.LinderoOesteNombres);

                    this.CrearSeguimiento(tb, "Operacion de Mantenimiento de terrenos de predios", EntidadesEnum.EnPrediosTerrenos, enc);
                }
                else
                {
                    String s = this.Servicio.CreatePredioTerreno(t);
                    int i = Convert.ToInt32(s.Replace("Id=", ""));

                    TablaCadena tb = new TablaCadena();
                    String enc = "Creacion de terreno predial\r\n";
                    enc = enc + "===========================\r\n";
                    tb.AddRow("Atributo", "Valor");
                    tb.AddRow("--------", "-----");
                    tb.AddRow("Id", i);
                    tb.AddRow("Predio Id", t.Predio, t.Predio);
                    tb.AddRow("Predio Codigo", pcod, pcod);
                    tb.AddRow("Superficie", String.Format("{0:n4}", t.Superficie));
                    if (tipopre == 1)
                    {
                        tb.AddRow("Clase de tierra", t.ClaseTierra);
                        tb.AddRow("Zona homogenea", t.ZonaHomogenea);
                    }
                    tb.AddRow("Lindero Norte", t.LinderoNorteNombres);
                    tb.AddRow("Lindero Sur", t.LinderoSurNombres);
                    tb.AddRow("Lindero Este", t.LinderoEsteNombres);
                    tb.AddRow("Lindero Oeste", t.LinderoOesteNombres);

                    this.CrearSeguimiento(tb, "Operacion de Mantenimiento de terrenos de predios", EntidadesEnum.EnPrediosTerrenos, enc);
                }
            }
        }

        public virtual void FrentesEliminar(IEnumerable<PredioFrenteDto> pFrentes)
        {
            this.Servicio.DeletePredioFrentes(pFrentes.ToArray());
            
            foreach (PredioFrenteDto pf in pFrentes)
            {
                String pcod = String.Empty;
                if (pf.Predio != null && pf.Predio > 0)
                {
                    PredioBaseDto p = this.Servicio.ReadPredioBase(String.Format(this.FormatoClave, pf.Predio));
                    if (p != null)
                    {
                        if (p.Codigo != null)
                            pcod = p.Codigo;
                    }
                }

                TablaCadena tb = new TablaCadena();
                String enc = "Eliminacion de frente de predio\r\n";
                enc = enc + "===============================\r\n";
                tb.AddRow("Atributo     ", "Valor ");
                tb.AddRow("-------------", "----->");
                tb.AddRow("Id", pf.Id);
                tb.AddRow("Predio Id", pf.Predio);
                tb.AddRow("Predio Codigo", pcod);
                tb.AddRow("Frente", pf.Frente);
                tb.AddRow("Superficie", String.Format("{0:n4}", pf.Superficie));
                
                this.CrearSeguimiento(tb, "Operacion de Frentes de terrenos de predios", EntidadesEnum.EnPrediosFrentes, enc);
            }
        }

        public virtual void FrentesModificar(IEnumerable<PredioFrenteDto> pFrentes)
        {
            foreach (PredioFrenteDto f in pFrentes)
            {
                String pcod = String.Empty;
                if (f.Predio != null && f.Predio > 0)
                {
                    PredioBaseDto p = this.Servicio.ReadPredioBase(String.Format(this.FormatoClave, f.Predio));
                    if (p != null)
                    {
                        if (p.Codigo != null)
                            pcod = p.Codigo;
                    }
                }

                if (f.Id > 0)
                {
                    PredioFrenteDto orif = this.Servicio.ReadPredioFrente(String.Format(this.FormatoClave, f.Id));

                    this.Servicio.UpdatePredioFrente(f);

                    TablaCadena tb = new TablaCadena();
                    String enc = "Modificacion de frente de predio\r\n";
                    enc = enc + "================================\r\n";
                    tb.AddRow("Atributo     ", "Valor Anterior ", "Valor nuevo ");
                    tb.AddRow("-------------", "-------------->", "----------->");
                    tb.AddRow("Id", f.Id, f.Id);
                    tb.AddRow("Predio Id", f.Predio, f.Predio);
                    tb.AddRow("Predio Codigo", pcod, pcod);
                    tb.AddRow("Lindero norte", f.Frente, orif.Frente);
                    tb.AddRow("Superficie", String.Format("{0:n4}", f.Superficie), String.Format("{0:n4}", orif.Superficie));

                    this.CrearSeguimiento(tb, "Operacion de Frentes de terrenos de predios", EntidadesEnum.EnPrediosFrentes, enc);

                }
                else
                {
                    String s = this.Servicio.CreatePredioFrente(f);
                    int i = Convert.ToInt32(s.Replace("Id=", ""));

                    TablaCadena tb = new TablaCadena();
                    String enc = "Creacion de frente de predio\r\n";
                    enc = enc + "===============================\r\n";
                    tb.AddRow("Atributo     ", "Valor ");
                    tb.AddRow("-------------", "----->");
                    tb.AddRow("Id", i);
                    tb.AddRow("Predio Id", f.Predio);
                    tb.AddRow("Predio Codigo", pcod);
                    tb.AddRow("Frente", f.Frente);
                    tb.AddRow("Superficie", String.Format("{0:n4}", f.Superficie));

                    this.CrearSeguimiento(tb, "Operacion de Frentes de terrenos de predios", EntidadesEnum.EnPrediosFrentes, enc);
                }
            }
        }

        public virtual void FotosEliminar(IEnumerable<PredioFotoDto> pFotos)
        {
            this.Servicio.DeletePredioFotos(pFotos.ToArray());
            
            foreach (PredioFotoDto f in pFotos)
            {
                String pcod = String.Empty;
                if (f.Predio != null && f.Predio > 0)
                {
                    PredioBaseDto p = this.Servicio.ReadPredioBase(String.Format(this.FormatoClave, f.Predio));
                    if (p != null)
                    {
                        if (p.Codigo != null)
                            pcod = p.Codigo;
                    }
                }

                TablaCadena tb = new TablaCadena();
                String enc = "Eliminacion de foto de predio\r\n";
                enc = enc + "===============================\r\n";
                tb.AddRow("Atributo    ", "Valor ");
                tb.AddRow("------------", "----->");
                tb.AddRow("Id", f.Id);
                tb.AddRow("Predio Id", f.Predio);
                tb.AddRow("Predio Codigo", pcod);
                tb.AddRow("Descripcion", f.Descripcion);

                this.CrearSeguimiento(tb, "Operacion de Fotos de predios", EntidadesEnum.EnPrediosFotos, enc);
            }
        }

        public virtual void FotosModificar(IEnumerable<PredioFotoDto> pFotos)
        {
            foreach (PredioFotoDto f in pFotos)
            {
                String pcod = String.Empty;
                if (f.Predio != null && f.Predio > 0)
                {
                    PredioBaseDto p = this.Servicio.ReadPredioBase(String.Format(this.FormatoClave, f.Predio));
                    if (p != null)
                    {
                        if (p.Codigo != null)
                            pcod = p.Codigo;
                    }
                }

                if (f.Id <= 0)
                {
                    this.Servicio.CreatePredioFoto(f);

                    TablaCadena tb = new TablaCadena();
                    String enc = "Creacion de foto de predio\r\n";
                    enc = enc + "==========================\r\n";
                    tb.AddRow("Atributo     ", "Valor ");
                    tb.AddRow("-------------", "----->");
                    tb.AddRow("Id", f.Id);
                    tb.AddRow("Predio Id", f.Predio);
                    tb.AddRow("Predio Codigo", pcod);
                    tb.AddRow("Descripcion", f.Descripcion);

                    this.CrearSeguimiento(tb, "Operacion de mantenimiento de fotos de predios", EntidadesEnum.EnPrediosFotos, enc);
                }
                else
                {
                    PredioFotoDto orif = this.Servicio.ReadPredioFoto(String.Format(this.FormatoClave, f.Id));
                    this.Servicio.UpdatePredioFoto(f);

                    TablaCadena tb = new TablaCadena();
                    String enc = "Modificacion de foto de predio\r\n";
                    enc = enc + "================================\r\n";
                    tb.AddRow("Atributo     ", "Valor Anterior ", "Valor nuevo ");
                    tb.AddRow("-------------", "-------------->", "----------->");
                    tb.AddRow("Id", f.Id, f.Id);
                    tb.AddRow("Predio Id", f.Predio, f.Predio);
                    tb.AddRow("Predio Codigo", pcod, pcod);
                    tb.AddRow("Descripcion", f.Descripcion, orif.Descripcion);

                    this.CrearSeguimiento(tb, "Operacion de Mantenimiento de fotos de predios", EntidadesEnum.EnPrediosFotos, enc);
                }
            }
        }

        public virtual void ConstruccionesEliminar(IEnumerable<PredioConstruccionDto> pConstrucciones)
        {
            this.Servicio.DeletePredioConstruccions(pConstrucciones.ToArray());
            
            foreach(PredioConstruccionDto c in pConstrucciones)
            {
                String pisdes = String.Empty;
                String blqdes = String.Empty;
                String precod = String.Empty;
                int preid = 0;
                String eledes = String.Empty;
                if (c.Elemento != null && c.Elemento > 0)
                {
                    TablaClaveDto e = ModeloCache.Instance.McClaves.Where(t => t.Id == c.Elemento).FirstOrDefault();
                    if (e != null)
                    {
                        eledes = e.Denominacion;
                    }
                }                    
                if (c.Piso != null && c.Piso > 0)
                {
                    PredioPisoDto p = this.Servicio.ReadPredioPiso(String.Format(this.FormatoClave, c.Piso));
                    if (p != null && p.Bloque != null && p.Bloque > 0)
                    {
                        pisdes = p.Descripcion;
                        PredioBloqueDto b = this.Servicio.ReadPredioBloque(String.Format(this.FormatoClave, p.Bloque));
                        if (b != null && b.Predio != null && b.Predio > 0)
                        {
                            blqdes = b.Descripcion;
                            PredioBaseDto d = this.Servicio.ReadPredioBase(String.Format(this.FormatoClave, b.Predio));
                            if (d != null)
                            {
                                preid = d.Id;
                                precod = d.Codigo;
                            }
                        }
                    }
                }

                TablaCadena tb = new TablaCadena();
                String enc = "Eliminacion de componente de construccion de predio\r\n";
                enc = enc + "===================================================\r\n";
                tb.AddRow("Atributo           ", "Valor ");
                tb.AddRow("-------------------", "----->");
                tb.AddRow("Id", c.Id);
                tb.AddRow("Predio Id", preid);
                tb.AddRow("Predio Codigo", precod);
                tb.AddRow("Bloque Desc.", blqdes);
                tb.AddRow("Piso Desc.", pisdes);
                tb.AddRow("Clase de componente", c.Clase);
                tb.AddRow("Componente Id", c.Elemento);
                tb.AddRow("Componente Desc.", eledes);

                this.CrearSeguimiento(tb, "Operacion de componentes de construccion de predios", EntidadesEnum.EnPrediosComponentes, enc);
            }
        }

        public virtual int ConstruccionCrear(PredioConstruccionDto cons)
        {
            string s = this.Servicio.CreatePredioConstruccion(cons);
            s = s.Replace("Id=", "");

            String pisdes = String.Empty;
            String blqdes = String.Empty;
            String precod = String.Empty;
            int preid = 0;
            String eledes = String.Empty;
            if (cons.Elemento != null && cons.Elemento > 0)
            {
                TablaClaveDto e = ModeloCache.Instance.McClaves.Where(t => t.Id == cons.Elemento).FirstOrDefault();
                if (e != null)
                {
                    eledes = e.Denominacion;
                }
            }
            if (cons.Piso != null && cons.Piso > 0)
            {
                PredioPisoDto p = this.Servicio.ReadPredioPiso(String.Format(this.FormatoClave, cons.Piso));
                if (p != null && p.Bloque != null && p.Bloque > 0)
                {
                    pisdes = p.Descripcion;
                    PredioBloqueDto b = this.Servicio.ReadPredioBloque(String.Format(this.FormatoClave, p.Bloque));
                    if (b != null && b.Predio != null && b.Predio > 0)
                    {
                        blqdes = b.Descripcion;
                        PredioBaseDto d = this.Servicio.ReadPredioBase(String.Format(this.FormatoClave, b.Predio));
                        if (d != null)
                        {
                            preid = d.Id;
                            precod = d.Codigo;
                        }
                    }
                }
            }

            TablaCadena tb = new TablaCadena();
            String enc = "Creacion de componente de construccion de predio\r\n";
            enc = enc + "================================================\r\n";
            tb.AddRow("Atributo           ", "Valor ");
            tb.AddRow("-------------------", "----->");
            tb.AddRow("Id", s);
            tb.AddRow("Predio Id", preid);
            tb.AddRow("Predio Codigo", precod);
            tb.AddRow("Bloque Desc.", blqdes);
            tb.AddRow("Piso Desc.", pisdes);
            tb.AddRow("Clase de componente", cons.Clase);
            tb.AddRow("Componente Id", cons.Elemento);
            tb.AddRow("Componente Desc.", eledes);

            this.CrearSeguimiento(tb, "Operacion de componentes de construccion de predios", EntidadesEnum.EnPrediosComponentes, enc);

            return Convert.ToInt32(s);
        }

        public virtual void BloquesGuardar(IEnumerable<PredioBloqueDto> bloques)
        {
            foreach (PredioBloqueDto b in bloques)
            {
                int bid = 0;
                if (b.Id <= 0)
                    bid = this.BloqueCrear(b);
                else
                    bid = b.Id;
                foreach (PredioPisoDto p in b.PisosLista)
                {
                    p.Bloque = bid;
                    int pid = 0;
                    if (p.Id <= 0)
                        pid = this.PisoCrear(p);
                    else
                    {
                        PredioPisoDto oldp = this.Servicio.ReadPredioPiso(String.Format(this.FormatoClave, p.Id));
                        pid = p.Id;
                        this.Servicio.UpdatePredioPiso(p);

                        String sbl = String.Empty;
                        if (p.Bloque != null && p.Bloque > 0)
                        {
                            PredioBloqueDto pb = this.Servicio.ReadPredioBloque(String.Format(this.FormatoClave, p.Bloque));
                            if (pb != null)
                                sbl = pb.Descripcion;
                        }

                        String sobl = String.Empty;
                        if (oldp.Bloque != null && oldp.Bloque > 0)
                        {
                            PredioBloqueDto oldpb = this.Servicio.ReadPredioBloque(String.Format(this.FormatoClave, oldp.Bloque));
                            if (oldpb != null)
                                sobl = oldpb.Descripcion;
                        }

                        String consv = String.Empty;
                        if (p.ConservacionNav != null)
                            consv = p.ConservacionNav.Denominacion;
                        else
                        {
                            TablaClaveDto d = ModeloCache.Instance.McClaves.Where((o) => o.Id == p.Conservacion).FirstOrDefault();
                            if (d != null)
                                consv = d.Denominacion;
                        }

                        String oldconsv = String.Empty;
                        if (oldp.ConservacionNav != null)
                            oldconsv = oldp.ConservacionNav.Denominacion;
                        else
                        {
                            TablaClaveDto oldd = ModeloCache.Instance.McClaves.Where((o) => o.Id == oldp.Conservacion).FirstOrDefault();
                            if (oldd != null)
                                oldconsv = oldd.Denominacion;
                        }

                        TablaCadena tb = new TablaCadena();
                        String enc = "Modificacion de piso de predio\r\n";
                        enc = enc + "==============================\r\n";
                        tb.AddRow("Atributo           ", "Valor Ant. ", "Valor nuevo ");
                        tb.AddRow("-------------------", "---------->", "----------->");
                        tb.AddRow("Id", pid, pid);
                        tb.AddRow("Bloque Id", oldp.Bloque, oldp.Bloque);
                        tb.AddRow("Bloque dscrp.", sbl, sobl);
                        tb.AddRow("Piso dscrp.", p.Descripcion, oldp.Descripcion);
                        tb.AddRow("Conservacion Id", p.Conservacion, oldp.Conservacion);
                        tb.AddRow("Conservacion Dscrp.", consv, oldconsv);
                        tb.AddRow("Superficie", p.Superficie, oldp.Superficie);

                        this.CrearSeguimiento(tb, "Operacion de Mantenimiento de pisos de bloque de construccion", EntidadesEnum.EnPrediosPisos, enc);
                    }
                    foreach (PredioConstruccionDto c in p.ConstruccionesLista)
                    {
                        if (c.Id <= 0)
                        {
                            c.Piso = pid;
                            this.ConstruccionCrear(c);
                        }
                    }
                }
            }
        }

        public virtual IEnumerable<Referencia.ResumenCatastralItem> ResumenCatastralPredialPorAño(int pAño)
        {
            return this.Servicio.ResumenCatastralPredial(pAño);
        }

        public virtual IEnumerable<Referencia.RepPredioDto> PrediosConsultaPorTipoEstado(int pTipoConsulta, int pEstado)
        {
            if (pEstado == 9)
                return this.Servicio.ReadRepPrediosFiltered("codigo", String.Format("formatoCodigo = {0}", pTipoConsulta));
            else
                return this.Servicio.ReadRepPrediosFiltered("codigo", String.Format("formatoCodigo = {0} and estado = {1}", pTipoConsulta, pEstado));
        }

        public virtual Double PredioUrbanoCalcularAvaluo(int? pId, bool pActualizar)
        {
            int? i = 0;
            if (pActualizar)
                i = 1;
            return this.Servicio.CalcularAvaluoUrbano(pId, i);
        }

        public virtual Double PredioRuralCalcularAvaluo(int? pId, bool pActualizar)
        {
            int? i = 0;
            if (pActualizar)
                i = 1;
            return this.Servicio.CalcularAvaluoRural(pId, i);
        }

        #endregion

        #region Operaciones de Patentes

        /* Traer patentes por contribuyente
        integer -> List<Patente>
        Traer las patentes registradas por un contribuyente*/
        public virtual IEnumerable<PatenteDto> PatentePorContribuyente(int pId, int pEstado)
        {
            IEnumerable<PatenteDto> res = new List<PatenteDto>();
            if (pEstado == 9)
            {
                res = this.Servicio.ReadPatentesFiltered("", String.Format("contribuyente = {0}", pId));
            }
            else
            {
                res = this.Servicio.ReadPatentesFiltered("", String.Format("contribuyente = {0} and estado = {1}", pId, pEstado));
            }
            foreach (PatenteDto p in res)
            {
                if (p.Contribuyente != null && p.Contribuyente > 0)
                    p.ContribuyenteNav = this.Servicio.ReadContribuyente(String.Format(this.FormatoClave, p.Contribuyente));
            }
            return res;
        }

        /* Traer patentes por codigo
        String -> List<Patentes>
        Traer las patentes cuyo codigo coinciden con la cadena recibida (Patron de codigo)
        Se puede usar comodines para traer solo los predios cierto sector/manzana/poligono/etc. */
        public virtual IEnumerable<PatenteDto> PatentePorCodigo(String pCodigo, int pEstado, TipoBusquedaTexto pTipoBusqueda)
        {
            IEnumerable<PatenteDto> res = new List<PatenteDto>();
            switch (pTipoBusqueda)
            {
                case TipoBusquedaTexto.tbConteniendo:
                    {
                        if (pEstado == 9)
                        {
                            res = this.Servicio.ReadPatentesFiltered("codigo", String.Format("codigo.Contains(\"{0}\")", pCodigo));
                        }
                        else
                        {
                            res = this.Servicio.ReadPatentesFiltered("codigo", String.Format("codigo.Contains(\"{0}\") and estado = {1}", pCodigo, pEstado));
                        }
                        break;
                    }
                case TipoBusquedaTexto.tbComenzando:
                    {
                        if (pEstado == 9)
                        {
                            res = this.Servicio.ReadPatentesFiltered("codigo", String.Format("codigo.StartsWith(\"{0}\")", pCodigo));
                        }
                        else
                        {
                            res = this.Servicio.ReadPatentesFiltered("codigo", String.Format("codigo.StartsWith(\"{0}\") and estado = {1}", pCodigo, pEstado));
                        }
                        break;
                    }
                default: // Codigo igual
                    {
                        if (pEstado == 9)
                        {
                            res = this.Servicio.ReadPatentesFiltered("codigo", String.Format("codigo = {0}", pCodigo));
                        }
                        else
                        {
                            res = this.Servicio.ReadPatentesFiltered("codigo", String.Format("codigo = {0} and estado = {1}", pCodigo, pEstado));
                        }
                        break;
                    }
            }
            foreach(PatenteDto p in res)
            {
                if (p.Contribuyente != null && p.Contribuyente > 0)
                    p.ContribuyenteNav = this.Servicio.ReadContribuyente(String.Format(this.FormatoClave, p.Contribuyente));
            }
            return res;
        }

        /* Traer por Id 
        integer -> Patente
        Traer una patente por su Id */
        public virtual PatenteDto PatentePorId(int pId)
        {
            return Servicio.ReadPatente(String.Format(this.FormatoClave, pId));
        }

        /* Nueva Patente
        Registrar patente nueva */
        public virtual int PatenteNuevo(PatenteDto pat)
        {
            string s = this.Servicio.CreatePatente(pat);
            s = s.Replace("Id=", "");

            String nom = "S/N";
            if (pat.ContribuyenteNav != null)
                nom = pat.ContribuyenteNav.Nombres;
            else
            {
                if (pat.Contribuyente != null)
                {
                    ContribuyenteDto con = this.Servicio.ReadContribuyente(String.Format(FormatoClave, pat.Contribuyente));
                    if (con != null)
                        nom = con.Nombres;
                }
            }
            
            TablaCadena tb = new TablaCadena();
            String enc = "Creacion de Patente municipal\r\n";
            enc = enc + "=============================\r\n";
            tb.AddRow("Atributo        ", "Valor ");
            tb.AddRow("----------------", "----->");
            tb.AddRow("Id", s);            
            tb.AddRow("Codigo", pat.Codigo);
            tb.AddRow("Contribuyente Id", pat.Contribuyente);
            tb.AddRow("Nombres", nom);
            tb.AddRow("Direccion", pat.Direccion);
            tb.AddRow("Tipo negocio", pat.NombreComercial);
            tb.AddRow("Observaciones", pat.Observaciones);

            this.CrearSeguimiento(tb, "Operacion de mantenimiento de Patentes", EntidadesEnum.EnPatentes, enc);

            return Convert.ToInt32(s);
        }

        public virtual void PatenteComponentesModificar(IEnumerable<PatentesComponenteDto> componentes, IEnumerable<PatentesComponenteDto> eliminados)
        {
            foreach (PatentesComponenteDto c in componentes)
            {
                if (c.Id <= 0)
                {
                    String s = this.Servicio.CreatePatentesComponente(c);
                    s = s.Replace("Id=", "");

                    PatenteDto pat = this.Servicio.ReadPatente(String.Format(this.FormatoClave, c.Patente));
                    ConceptoDto rub = this.Servicio.ReadConcepto(String.Format(this.FormatoClave, c.Concepto));

                    TablaCadena tb = new TablaCadena();
                    String enc = "Creacion de Componentes de Patentes\r\n";
                    enc = enc + "===================================\r\n";
                    tb.AddRow("Atributo      ", "Valor ");
                    tb.AddRow("--------------", "----->");
                    tb.AddRow("Id", s);
                    tb.AddRow("Patente Id", c.Patente);
                    tb.AddRow("Codigo", pat.Codigo);
                    tb.AddRow("Concepto", rub.Denominacion);
                    tb.AddRow("Base imponible", String.Format("{0:n2}", c.BaseImponible));

                    this.CrearSeguimiento(tb, "Operacion de mantenimiento de Patentes", EntidadesEnum.EnPatentes, enc);
                }
                else
                {
                    PatentesComponenteDto oldc = this.Servicio.ReadPatentesComponente(String.Format(this.FormatoClave, c.Id));
                    this.Servicio.UpdatePatentesComponente(c);

                    PatenteDto pat = this.Servicio.ReadPatente(String.Format(this.FormatoClave, c.Patente));
                    ConceptoDto rub = c.ConceptoNav;
                    if (rub == null)
                        rub = this.Servicio.ReadConcepto(String.Format(this.FormatoClave, c.Concepto));
                    ConceptoDto oldrub = this.Servicio.ReadConcepto(String.Format(this.FormatoClave, oldc.Concepto));

                    TablaCadena tb = new TablaCadena();
                    String enc = "Creacion de Componentes de Patentes\r\n";
                    enc = enc + "===================================\r\n";
                    tb.AddRow("Atributo      ", "Valor Ant. ", "Valor nuevo ");
                    tb.AddRow("--------------", "---------->", "----------->");
                    tb.AddRow("Id", c.Id, c.Id);
                    tb.AddRow("Patente Id", c.Patente, oldc.Patente);
                    tb.AddRow("Codigo", pat.Codigo, pat.Codigo);
                    tb.AddRow("Concepto", rub.Denominacion, oldrub.Denominacion);
                    tb.AddRow("Base imponible", String.Format("{0:n2}", c.BaseImponible), String.Format("{0:n2}", oldc.BaseImponible));

                    this.CrearSeguimiento(tb, "Operacion de mantenimiento de Patentes", EntidadesEnum.EnPatentes, enc);
                }
            }

            this.Servicio.DeletePatentesComponentes(eliminados.ToArray());

        }

        /* Modificar Estado de patente
        integer, integer -> Nada
        Modifca el estado de una PatenteDto para señalar si esta eliminado o activo */
        public virtual void PatenteModificar(PatenteDto pat)
        {
            try
            {
                PatenteDto oldp = this.Servicio.ReadPatente(String.Format(this.FormatoClave, pat.Id));
                this.Servicio.UpdatePatente(pat);

                String oldnom = "S/N";
                if (oldp != null && oldp.Contribuyente != null && oldp.Contribuyente > 0)
                {
                    ContribuyenteDto con = this.Servicio.ReadContribuyente(String.Format(FormatoClave, oldp.Contribuyente));
                    if (con != null)
                        oldnom = con.Nombres;
                }

                String nom = "S/N";
                if (pat.ContribuyenteNav != null)
                    nom = pat.ContribuyenteNav.Nombres;
                else
                {
                    if (pat.Contribuyente != null)
                    {
                        ContribuyenteDto con = this.Servicio.ReadContribuyente(String.Format(FormatoClave, pat.Contribuyente));
                        if (con != null)
                            nom = con.Nombres;
                    }
                }

                TablaCadena tb = new TablaCadena();
                String enc = "Modificacion de Patente municipal\r\n";
                enc = enc + "=============================\r\n";
                tb.AddRow("Atributo        ", "Valor ");
                tb.AddRow("----------------", "----->");
                tb.AddRow("Id", pat.Id, pat.Id);
                tb.AddRow("Codigo", pat.Codigo, oldp.Codigo);
                tb.AddRow("Contribuyente Id", pat.Contribuyente, oldp.Contribuyente);
                tb.AddRow("Nombres", nom, oldnom);
                tb.AddRow("Direccion", pat.Direccion, oldp.Direccion);
                tb.AddRow("Tipo negocio", pat.NombreComercial, oldp.NombreComercial);
                tb.AddRow("Observaciones", pat.Observaciones, oldp.Observaciones);

                this.CrearSeguimiento(tb, "Operacion de mantenimiento de patentes", EntidadesEnum.EnPatentes, enc);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /* Codigo registrado
        String, Integer, Integer -> Boolean
        Consulta en DB si el codigo recibido ya esta registrado */
        public virtual Boolean PatenteCodigoRegistrado(String pCodigo, int pId)
        {
            Boolean ret = false;
            if (this.Servicio.ReadPatentesFiltered("", String.Format("estado = {0} and codigo = \"{1}\" and id = {2}", 0, pCodigo, pId)).Count() > 0)
            {
                ret = true;
            }
            return ret;
        }

        /// <summary>
        /// Traer los componentes que se aplican a un negocio
        /// </summary>
        /// <param name="patenteId">Id de la patente consultada</param>
        /// <returns></returns>
        public virtual IEnumerable<PatentesComponenteDto> PatenteComponentesPorId(int patenteId)
        {
            IEnumerable<PatentesComponenteDto> res = this.Servicio.ReadPatentesComponentes().Where(p => p.Patente == patenteId);
            foreach (PatentesComponenteDto c in res)
            {
                if (c.CoeficientesIndice != null && c.CoeficientesIndice > 0)
                    c.CoeficienteEleNav = ModeloCache.Instance.McCoeficienteElementos.Where(e => e.Id == c.CoeficientesIndice).FirstOrDefault();
                if (c.CoeficientesTipo != null && c.CoeficientesTipo > 0)
                    c.CoeficienteNav = ModeloCache.Instance.McCoeficientes.Where(n => n.Id == c.CoeficientesTipo).FirstOrDefault();
                if (c.Concepto != null && c.Concepto > 0)
                    c.ConceptoNav = ModeloCache.Instance.McConceptos.Where(t => t.Id == c.Concepto).FirstOrDefault();
            }
            return res;
        }

        

        #endregion

        #region Operaciones de puestos de mercado

        public int MercadoNuevo(MercadoDto e)
        {
            String s = this.Servicio.CreateMercado(e);
            s = s.Replace("Id=", "");
            return Convert.ToInt32(s);
        }

        public void MercadoModificar(MercadoDto e)
        {
            this.Servicio.UpdateMercado(e);
        }

        public void MercadoAlterarEstado(int id, int pEst)
        {
            MercadoDto m = Servicio.ReadMercado(String.Format(FormatoClave, id));
            if (m != null)
            {
                m.Estado = pEst;
                Servicio.UpdateMercado(m);
            }
        }

        public IEnumerable<MercadoDto> MercadoPuestos(bool eli, bool des, int num)
        {
            IEnumerable<MercadoDto> res = new List<MercadoDto>(); 
            if (eli && des)
            {
                if (num > 0)
                    res = Servicio.ReadMercados();
                else
                    res = Servicio.ReadMercadosFiltered("", String.Format("puesto = {0}", num));
            }
            else
            {
                String filtro = "estado = 0";
                if (eli)
                {
                    filtro = filtro + " or estado = 2";
                }

                if (des)
                {
                    filtro = filtro + " or estado = 1";
                }
                if (num > 0)
                    filtro = "(" + filtro + String.Format(") and puesto = {0}", num);
                res = Servicio.ReadMercadosFiltered("", filtro);
            }
            foreach (MercadoDto m in res)
            {
                m.ContribuyenteNav = this.Servicio.ReadContribuyente(String.Format(this.FormatoClave, m.Contribuyente));
            }
            
            return res;
        }

        public MercadoDto MercadoPorPuesto(int p)
        {
            return Servicio.ReadMercadosFiltered("", String.Format("puesto = {0}", p)).FirstOrDefault();
        }

        #endregion

    }
}
