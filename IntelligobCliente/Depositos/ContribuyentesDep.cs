using Intelligob.Cliente.Referencia;
using System;
using System.Collections.Generic;
using System.Linq;
using Intelligob.Utiles;
using System.Text.RegularExpressions;

namespace Intelligob.Cliente.Depositos
{
    public class ContribuyentesDep : DepositoBase
    {
        public ContribuyentesDep() : base(DepositosControl.Instance.Servicio) { }
        public ContribuyentesDep(IEntidades servicio) : base(servicio) { }

        /// <summary>
        /// Recibe un objeto Contribuyente (ya validado) lo persiste y retorna el Id insertado
        /// </summary>
        /// <param name="con">Contribuyente a guardar</param>
        /// <returns></returns>
        public virtual int ContribuyenteNuevo(ContribuyenteDto con)
        {
            string s = this.Servicio.CreateContribuyente(con);
            s = s.Replace("Id=", "");

            TablaCadena tb = new TablaCadena();            
            String enc = "Nuevo contribuyente\r\n";
            enc  = enc + "===================\r\n";
            tb.AddRow("Atributo", "Valor");
            tb.AddRow("--------", "-----");
            tb.AddRow("Id", s);
            tb.AddRow("Nombres", con.Nombres);
            tb.AddRow("Cedula", con.Cedula);
            tb.AddRow("Direccion", con.Direccion);

            this.CrearSeguimiento(tb, "Operacion de Mantenimiento de Contribuyentes", EntidadesEnum.EnContribuyentes, enc);

            return Convert.ToInt32(s);
        }

        /// <summary>
        /// Recibe un Contribuyente modificado y persiste los cambios
        /// </summary>
        /// <param name="con">Contribuyente a modificar</param>
        public void ContribuyenteModificar(ContribuyenteDto con)
        {
            ContribuyenteDto oldcon = this.ContribuyentePorId(con.Id);            
            this.Servicio.UpdateContribuyente(con);

            String per = String.Empty;
            if (con.PersoneriaNav != null)
            {
                per = con.PersoneriaNav.Denominacion;
            }
            else
            {
                TablaClaveDto tper = this.Servicio.ReadTablaClave(String.Format(this.FormatoClave, con.Personeria));
                if (tper != null)
                    per = tper.Denominacion;
            }

            String oldper = String.Empty;
            TablaClaveDto toper = this.Servicio.ReadTablaClave(String.Format(this.FormatoClave, con.Personeria));
            if (toper != null)
                oldper = toper.Denominacion;
            
            TablaCadena tb = new TablaCadena();                        
            String enc = "Modificar contribuyente\r\n";
            enc =  enc + "=======================\r\n";
            tb.AddRow("Atributo", "Original", "Modificado");
            tb.AddRow("--------", "--------", "----------");
            tb.AddRow("Id", oldcon.Id, con.Id);
            tb.AddRow("Personeria ID", oldcon.Personeria, con.Personeria);
            tb.AddRow("Personeria", oldper, per);
            tb.AddRow("Nombres", oldcon.Nombres, con.Nombres);
            tb.AddRow("Cedula", oldcon.Cedula, con.Cedula);
            tb.AddRow("Direccion", oldcon.Direccion, con.Direccion);
            tb.AddRow("Estado", oldcon.Estado, con.Estado);

            this.CrearSeguimiento(tb, "Operacion de Mantenimiento de contribuyentes", EntidadesEnum.EnContribuyentes, enc);
            
        }

        /// <summary>
        /// Retornar todos los contribuyentes cuyo nombre coincide con el tipo de busqueda a la cadena recibida, la cadena recibida 
        /// y el estado si es filtrado
        /// </summary>
        /// <param name="pNombre">Nombres a buscar</param>
        /// <param name="pTipoCoincidencia">Tipo de coincidencia de texto</param>
        /// <param name="pEstado">Estado del contribuyente (9 = Todos)</param>
        /// <returns></returns>
        public virtual IEnumerable<ContribuyenteDto> ContribuyentesPorNombre(String pNombre, TipoBusquedaTexto pTipoCoincidencia, int pEstado)
        {
            IEnumerable<ContribuyenteDto> res;
            switch (pTipoCoincidencia)
            {
                case TipoBusquedaTexto.tbConteniendo:
                    {
                        pNombre = pNombre.Trim();
                        pNombre = Regex.Replace(pNombre, @"\s+", "%");
                        if (!pNombre.StartsWith("%"))
                        {
                            pNombre = "%" + pNombre;
                        }
                        if (!pNombre.EndsWith("%"))
                        {
                            pNombre = pNombre + "%";
                        }
                        if (pEstado < 9)
                            res = Servicio.ReadContribuyentesFiltered("", String.Format("estado = {0} and nombres.contains(\"{1}\")", pEstado, pNombre)).OrderBy(c => c.Nombres);
                        else
                            res = Servicio.ReadContribuyentesFiltered("", String.Format("nombres.contains(\"{0}\")", pNombre)).OrderBy(c => c.Nombres);
                        break;
                    }
                case TipoBusquedaTexto.tbIgual:
                    {
                        if (pEstado < 9)
                            res = Servicio.ReadContribuyentesFiltered("", String.Format("estado = {0} and nombres = \"{1}\"", pEstado, pNombre)).OrderBy(c => c.Nombres);
                        else
                            res = Servicio.ReadContribuyentesFiltered("", String.Format("nombres = \"{0}\"", pNombre)).OrderBy(c => c.Nombres);
                        break;
                    }
                default: // Comenzando
                    {
                        if (pEstado < 9)
                            res = Servicio.ReadContribuyentesFiltered("", String.Format("estado = {0} and nombres.startsWith(\"{1}\")", pEstado, pNombre)).OrderBy(c => c.Nombres);
                        else
                            res = Servicio.ReadContribuyentesFiltered("", String.Format("nombres.startsWith(\"{0}\")", pNombre));
                        break;
                    }
            }
            foreach(ContribuyenteDto c in res)
            {
                if (c.Personeria != null && c.Personeria > 0)
                c.PersoneriaNav = ModeloCache.Instance.McClaves.Where(t => t.Id == c.Personeria).FirstOrDefault();
            }
            return res;
        }

        /// <summary>
        /// Retornar todos los contribuyentes cuyos numero de cedula coincida con la cadena recibida
        /// </summary>
        /// <param name="pCedula">No. de cedula a buscar</param>
        /// <returns></returns>
        public virtual IEnumerable<ContribuyenteDto> ContribuyentesPorCedula(String pCedula)
        {
            IEnumerable<ContribuyenteDto> res = this.ContribuyentesPorCedulaEstado(pCedula, 9);
            foreach (ContribuyenteDto c in res)
            {
                if (c.Personeria != null && c.Personeria > 0)
                    c.PersoneriaNav = ModeloCache.Instance.McClaves.Where(t => t.Id == c.Personeria).FirstOrDefault();
            }
            return res;
        }

        /// <summary>
        /// Retornar todos los contribuyentes cuyos numero de cedula coincida con la cadena recibida y filtrado por estado
        /// </summary>
        /// <param name="pCedula">No. de cedula</param>
        /// <param name="pEstado">Estado del contribuyente</param>
        /// <returns></returns>
        public virtual IEnumerable<ContribuyenteDto> ContribuyentesPorCedulaEstado(String pCedula, int pEstado)
        {
            IEnumerable<ContribuyenteDto> res;
            if (pEstado == 9)
                res = Servicio.ReadContribuyentes().Where(c => c.Cedula == pCedula);
            else
                res = Servicio.ReadContribuyentes().Where(c => c.Cedula == pCedula && c.Estado == pEstado);
            foreach(ContribuyenteDto c in res)
            {
                if (c.Personeria != null && c.Personeria > 0)
                    c.PersoneriaNav = ModeloCache.Instance.McClaves.Where(t => t.Id == c.Personeria).FirstOrDefault();
            }
            return res;
        }

        /// <summary>
        /// Consultar si un contribuyente ya esta registrado con el nombre recibido
        /// </summary>
        /// <param name="pContribuyente">Contribuyente a comprobar</param>
        /// <returns></returns>
        public virtual Boolean ContribuyenteNombreRegistrado(ContribuyenteDto pContribuyente)
        {
            Boolean res = false;
            List<ContribuyenteDto> enc = Servicio.ReadContribuyentes().Where(c => c.Nombres == pContribuyente.Nombres && c.Id != pContribuyente.Id).ToList();
            if (enc.Count > 0)
            {
                res = true;
            }
            return res;
        }

        /// <summary>
        /// Consultar si se encunetra registrado un contribuyente con la cedula de contribuyente recibido
        /// </summary>
        /// <param name="pContribuyente">Contribuyente a consultar</param>
        /// <returns></returns>
        public virtual Boolean ContribuyenteCedulaRegistrada(ContribuyenteDto pContribuyente)
        {
            Boolean res = false;
            List<ContribuyenteDto> enc = this.Servicio.ReadContribuyentes().Where(c => c.Cedula == pContribuyente.Cedula && c.Id != pContribuyente.Id).ToList();
            if (enc.Count() > 0)
            {
                res = true;
            }
            return res;
        }

        /// <summary>
        /// Crear rebaja a contribuyente
        /// </summary>
        /// <param name="pReb">Rebaja a crear</param>
        public virtual void RebajaContribuyenteCrear(ContribuyentesRebajaDto pReb)
        {
            String s = this.Servicio.CreateContribuyentesRebaja(pReb);
            s = s.Replace("Id=", "");

            String nom = "S/N";
            if (pReb != null)
            {
                ContribuyenteDto c = this.Servicio.ReadContribuyente(String.Format(this.FormatoClave, pReb.Contribuyente));
                nom = c.Nombres;
            }

            String reb = "N/D";
            if (pReb.RebajaNav == null)
            {
                
                if (pReb.Rebaja != null)
                {
                    RebajaDto rebo = this.Servicio.ReadRebaja(String.Format(this.FormatoClave, pReb.Rebaja));
                    reb = rebo.Denominacion;
                }
            }
            else
            {
                reb = pReb.RebajaNav.Denominacion;
            }

            TablaCadena tb = new TablaCadena();
            String enc = "Creacion de rebaja a contribuyente\r\n";
            enc =  enc + "==================================\r\n";
            tb.AddRow("Atributo", "Valor");
            tb.AddRow("--------", "-----");
            tb.AddRow("Id", s);
            tb.AddRow("Contribuyente Id", pReb.Contribuyente);
            tb.AddRow("Nombres", nom);
            tb.AddRow("Rebaja Id", pReb.Rebaja);
            tb.AddRow("Rebaja", reb);
            tb.AddRow("Porcentaje", pReb.Fraccion);

            this.CrearSeguimiento(tb, "Operacion de Mantenimiento de rebajas a contribuyentes", EntidadesEnum.EnRebajas, enc);
        }

        /// <summary>
        /// Eliminar rebaja a contribuyente
        /// </summary>
        /// <param name="pRebs">Rebaja a eliminar</param>
        public virtual void RebajaContribuyenteEliminar(ContribuyentesRebajaDto[] pRebs)
        {
            this.Servicio.DeleteContribuyentesRebajas(pRebs);

            foreach (ContribuyentesRebajaDto pReb in pRebs)
            {
                String nom = "S/N";
                if (pReb != null)
                {
                    ContribuyenteDto c = this.Servicio.ReadContribuyente(String.Format(this.FormatoClave, pReb.Contribuyente));
                    nom = c.Nombres;
                }

                String reb = "N/D";
                if (pReb.RebajaNav == null)
                {

                    if (pReb.Rebaja != null)
                    {
                        RebajaDto rebo = this.Servicio.ReadRebaja(String.Format(this.FormatoClave, pReb.Rebaja));
                        reb = rebo.Denominacion;
                    }
                }
                else
                {
                    reb = pReb.RebajaNav.Denominacion;
                }

                TablaCadena tb = new TablaCadena();
                String enc = "Eliminacion de rebaja a contribuyente\r\n";
                enc =  enc + "=====================================\r\n";
                tb.AddRow("Atributo", "Valor");
                tb.AddRow("--------", "-----");
                tb.AddRow("Id", pReb.Id);
                tb.AddRow("Contribuyente Id", pReb.Contribuyente);
                tb.AddRow("Nombres", nom);
                tb.AddRow("Rebaja Id", pReb.Rebaja);
                tb.AddRow("Rebaja", reb);
                tb.AddRow("Porcentaje", pReb.Fraccion);

                this.CrearSeguimiento(tb, "Operacion de Mantenimiento de rebajas a contribuyentes", EntidadesEnum.EnRebajas, enc);
            }
        }

        /// <summary>
        /// Modificar rebaja a contribuyente
        /// </summary>
        /// <param name="pReb">Rebaja a modificar</param>
        public virtual void RebajaContribuyenteActualizar(ContribuyentesRebajaDto pReb)
        {
            this.Servicio.UpdateContribuyentesRebaja(pReb);

            String nom = "S/N";
            if (pReb != null)
            {
                ContribuyenteDto c = this.Servicio.ReadContribuyente(String.Format(this.FormatoClave, pReb.Contribuyente));
                nom = c.Nombres;
            }

            String reb = "N/D";
            if (pReb.RebajaNav == null)
            {

                if (pReb.Rebaja != null)
                {
                    RebajaDto rebo = this.Servicio.ReadRebaja(String.Format(this.FormatoClave, pReb.Rebaja));
                    reb = rebo.Denominacion;
                }
            }
            else
            {
                reb = pReb.RebajaNav.Denominacion;
            }

            ContribuyentesRebajaDto oldReb = this.Servicio.ReadContribuyentesRebaja(String.Format(this.FormatoClave, pReb.Id));

            TablaCadena tb = new TablaCadena();
            String enc = "Modificacion de rebaja a contribuyente\r\n";
            enc =  enc + "======================================\r\n";
            tb.AddRow("Atributo", "Original", "Modificado");
            tb.AddRow("--------", "--------", "----------");
            tb.AddRow("Id", pReb.Id, "");
            tb.AddRow("Contribuyente Id", pReb.Contribuyente, pReb.Contribuyente);
            tb.AddRow("Nombres", nom, nom);
            tb.AddRow("Rebaja Id", pReb.Rebaja, pReb.Rebaja);
            tb.AddRow("Rebaja", reb, reb);
            tb.AddRow("Porcentaje", pReb.Fraccion, oldReb.Fraccion);

            this.CrearSeguimiento(tb, "Operacion de Mantenimiento de rebajas a contribuyentes", EntidadesEnum.EnRebajas, enc);
        }

        /// <summary>
        /// Traer Rebaja por contribuyente y estado
        /// </summary>
        /// <param name="pContribuyente">Contribuyente a consultar</param>
        /// <param name="pEstado">Estado de la rebaja (9 = TODOS)</param>
        /// <returns></returns>
        public virtual IEnumerable<ContribuyentesRebajaDto> RebajasPorContribuyenteEstado(int pContribuyente, int pEstado)
        {
            IEnumerable<ContribuyentesRebajaDto> res = this.Servicio.ReadContribuyentesRebajasFiltered("", String.Format("contribuyente = {0} and estado = {1}", pContribuyente, pEstado));
            foreach (ContribuyentesRebajaDto reb in res)
            {
                reb.RebajaNav = ModeloCache.Instance.McRebajas.Where(r => r.Id == reb.Rebaja).FirstOrDefault();
            }
            return res;
        }

        /// <summary>
        /// Consultar si un contribuyentem es beneficiario de la rebaja recibida
        /// </summary>
        /// <param name="pContribuyente">contribuyente a consultar</param>
        /// <param name="pRebaja">Rebaja a consultar</param>
        /// <returns></returns>
        public virtual Boolean RebajaContribuyenteBeneficio(int pContribuyente, int pRebaja)
        {
            bool res = false;

            IEnumerable<ContribuyentesRebajaDto> rebs = this.Servicio.ReadContribuyentesRebajasFiltered("", String.Format("contribuyente = {0} and rebaja = {1} and estado = 0", pContribuyente, pRebaja));
            if (rebs != null)
            {
                if (rebs.Count() > 0 && rebs.FirstOrDefault() != null)
                    res = true;
            }

            return res;
        }

        /// <summary>
        /// Unificar uno o varios contribuyentes a otro
        /// </summary>
        /// <param name="pPermanece">Contribuyente que permanece</param>
        /// <param name="pEliminados">Lista de los Contribuyentes que son reemplazados</param>
        public void UnificarContribuyentes(int? pPermanece, List<ContribuyenteDto> pEliminados)
        {
            String nom = String.Empty;
            ContribuyenteDto cp = this.Servicio.ReadContribuyente(String.Format(this.FormatoClave, pPermanece));
            if (cp != null)
                nom = cp.Nombres;

            foreach (ContribuyenteDto c in pEliminados)
            {
                this.Servicio.UnificarContribuyentes(pPermanece, c.Id);

                TablaCadena tb = new TablaCadena();
                String enc = "Unificacion de contribuyentes\r\n";
                enc =  enc + "=============================\r\n";
                tb.AddRow("Atributo", "Valor");
                tb.AddRow("--------", "-----");
                tb.AddRow("Permanente Id", pPermanece.ToString());
                tb.AddRow("Permanente nombres", nom);
                tb.AddRow("Eliminado Id", c.Id);
                tb.AddRow("Eliminado nombres", c.Nombres);

                this.CrearSeguimiento(tb, "Operacion de Mantenimiento de rebajas a contribuyentes", EntidadesEnum.EnContribuyentes, enc);
            }
        }

        /// <summary>
        /// Traer un contribuyente por su ID
        /// </summary>
        /// <param name="pId">ID del contribuyente buscado</param>
        /// <returns></returns>
        public ContribuyenteDto ContribuyentePorId(int pId)
        {
            return this.Servicio.ReadContribuyente(String.Format(FormatoClave, pId));
        }
    }
}
