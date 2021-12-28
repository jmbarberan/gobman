using System;
using System.Collections.Generic;
using System.Linq;
using Intelligob.Cliente.Referencia;

namespace Intelligob.Cliente.Depositos
{
    public class CajasDep : DepositoBase
    {
        public CajasDep() : base(DepositosControl.Instance.Servicio) { }

        public CajasDep(IEntidades servicio) : base(servicio) { }

        /// <summary>
        /// Expandir caja usuario con entidades asociadas
        /// </summary>
        /// <param name="cu">CajaUsuario a expandir</param>
        private void ExpandirCajaUsuario(CajasUsuarioDto cu)
        {
            if (cu.Usuario != null && cu.Usuario > 0)
                cu.UsuarioNav = this.Servicio.ReadUsuario(String.Format(this.FormatoClave, cu.Usuario));
            if (cu.Caja != null && cu.Caja > 0)
                cu.CajaNav = this.Servicio.ReadCaja(String.Format(this.FormatoClave, cu.Caja));
        }

        /// <summary>
        /// Traer cajas asociadas a el usuario consultado
        /// </summary>
        /// <param name="pUsuario">Usuario a consultar (0 = DESARROLLADOR)</param>
        /// <param name="pEstado">Estado de los registros (9 = TODOS)</param>
        /// <param name="pExtender">Extender con las entidades foraneas</param>
        /// <returns>Lista de Cajas por Usuario</returns>
        public IEnumerable<CajasUsuarioDto> CajasPorUsuarioEstado(int pUsuario, int pEstado, Boolean pExtender)
        {
            IEnumerable<CajasUsuarioDto> cs = null;
            if (pUsuario == 0)
            {
                if (pEstado == 9)
                    cs = this.Servicio.ReadCajasUsuarios().OrderBy(o => o.Caja);
                else
                    cs = this.Servicio.ReadCajasUsuariosFiltered("", String.Format("estado = {0}", pEstado));
            }
            else
            {
                if (pEstado == 9)
                    cs = this.Servicio.ReadCajasUsuariosFiltered("", String.Format("usuario = {0}", pUsuario));
                else                    
                    cs = this.Servicio.ReadCajasUsuariosFiltered("", String.Format("usuario = {0} and estado = {1}", pUsuario, pEstado));
            }
            
            if (pExtender)
            {
                foreach (CajasUsuarioDto cu in cs)
                {
                    this.ExpandirCajaUsuario(cu);
                }
            }
            return cs;
        }

        /// <summary>
        /// Traer usuarios asociados a caja
        /// </summary>
        /// <param name="pCaja">Caja a consultar</param>
        /// <param name="pEstado">Estado del registro (9 = TODOS)</param>
        /// <param name="pExtender">Extender con las entidades foraneas</param>
        /// <returns></returns>
        public IEnumerable<CajasUsuarioDto> CajaUsuariosPorCajaEstado(int pCaja, int pEstado, Boolean pExtender)
        {
            IEnumerable<CajasUsuarioDto> cs = null;
            
            if (pEstado == 9)
                cs = this.Servicio.ReadCajasUsuariosFiltered("", String.Format("caja = {0}", pCaja));
            else
                cs = this.Servicio.ReadCajasUsuariosFiltered("", String.Format("caja = {0} and estado = {1}", pCaja, pEstado));
            
            if (pExtender)
            {
                foreach (CajasUsuarioDto cu in cs)
                {
                    this.ExpandirCajaUsuario(cu);
                }
            }
            return cs;
        }

        /// <summary>
        /// Traer Cajas por el estado 
        /// </summary>
        /// <param name="pEstado">Estado de las cajas (9 = TODOS)</param>
        /// <returns>Lista de cajas</returns>
        public IEnumerable<CajaDto> CajasPorEstado(int pEstado)
        {
            if (pEstado == 9)
            { return this.Servicio.ReadCajas().OrderBy(o => o.Descripcion); }
            else
            { return this.Servicio.ReadCajasFiltered("descripcion", String.Format("estado = {0}", pEstado)); }
        }
    
        /// <summary>
        /// Guarda los cambios efectuados en la caja recibida
        /// </summary>
        /// <param name="pCaja">Caja a guardar</param>
        public void ModificarCaja(CajaDto pCaja)
        {
            CajaDto oldcaja = this.Servicio.ReadCaja(String.Format(this.FormatoClave, pCaja.Id));

            this.Servicio.UpdateCaja(pCaja);

            Utiles.TablaCadena tb = new Utiles.TablaCadena();
            String enc = "Modificar caja\r\n";
            enc = enc +  "==============\r\n";
            tb.AddRow("Atributo", "Original", "Modificado");
            tb.AddRow("--------", "--------", "----------");
            tb.AddRow("Id", oldcaja.Id, pCaja.Id);
            tb.AddRow("Codigo", oldcaja.Codigo, pCaja.Codigo);
            tb.AddRow("Descripcion", oldcaja.Descripcion, pCaja.Descripcion);
            tb.AddRow("F. Cierre", oldcaja.Cierre, pCaja.Cierre);
            tb.AddRow("Saldo", oldcaja.Saldo, pCaja.Saldo);
            tb.AddRow("Estado", oldcaja.Estado, pCaja.Estado);

            this.CrearSeguimiento(tb, "Operacion de Mantenimiento de cajas", Utiles.EntidadesEnum.EnCajas, enc);
        }

        /// <summary>
        /// Modificar usuarios asociados a cajas
        /// </summary>
        /// <param name="pCUsuarios">Lista de usuarios agregados</param>
        /// <param name="pEliminados">Lista de usuarios eliminados</param>
        public void CajaUsuariosModificar(IEnumerable<CajasUsuarioDto> pCUsuarios, IEnumerable<CajasUsuarioDto> pEliminados)
        {
            foreach (CajasUsuarioDto cu in pCUsuarios)
            {
                if (cu.Id <= 0)
                {
                    String s = this.Servicio.CreateCajasUsuario(cu);
                    s = s.Replace("Id=", "");

                    String caja = "N/D";
                    if (cu.CajaNav != null)
                    {
                        caja = cu.CajaNav.Descripcion;
                    }
                    else
                    {
                        CajaDto oc = this.Servicio.ReadCaja(String.Format(this.FormatoClave, cu.Caja));
                        if (oc != null)
                        {
                            caja = oc.Descripcion;
                        }
                    }

                    String usr = "N/D";
                    if (cu.UsuarioNav != null)
                    {
                        usr = cu.UsuarioNav.Nombres;
                    }
                    else
                    {
                        UsuarioDto ou = this.Servicio.ReadUsuario(String.Format(this.FormatoClave, cu.Usuario));
                        if (ou != null)
                        {
                            usr = ou.Nombres;
                        }
                    }

                    Utiles.TablaCadena tb = new Utiles.TablaCadena();
                    String enc = "Registrar nuevo usuario cajero\r\n";
                    enc  = enc + "==============================\r\n";
                    tb.AddRow("Atributo", "Valor");
                    tb.AddRow("--------", "-----");
                    tb.AddRow("Id", s);
                    tb.AddRow("Caja Id", cu.Caja);
                    tb.AddRow("Caja", caja);
                    tb.AddRow("Usuario Id", cu.Usuario);
                    tb.AddRow("Usuario", usr);

                    this.CrearSeguimiento(tb, "Operacion de modificacion de usuario cajero", Utiles.EntidadesEnum.EnCajaUsuarios, enc);
                }
            }
            
            this.Servicio.DeleteCajasUsuarios(pEliminados.ToArray());
            foreach(CajasUsuarioDto cu in pEliminados)
            {
                String caja = "N/D";
                if (cu.CajaNav != null)
                {
                    caja = cu.CajaNav.Descripcion;
                }
                else
                {
                    CajaDto oc = this.Servicio.ReadCaja(String.Format(this.FormatoClave, cu.Caja));
                    if (oc != null)
                    {
                        caja = oc.Descripcion;
                    }
                }

                String usr = "N/D";
                if (cu.UsuarioNav != null)
                {
                    usr = cu.UsuarioNav.Nombres;
                }
                else
                {
                    UsuarioDto ou = this.Servicio.ReadUsuario(String.Format(this.FormatoClave, cu.Usuario));
                    if (ou != null)
                    {
                        usr = ou.Nombres;
                    }
                }

                Utiles.TablaCadena tb = new Utiles.TablaCadena();
                String enc = "Registrar usuario cajero eliminado\r\n";
                enc =  enc + "==================================\r\n";
                tb.AddRow("Atributo", "Valor");
                tb.AddRow("--------", "-----");
                tb.AddRow("Id", cu.Id);
                tb.AddRow("Caja Id", cu.Caja);
                tb.AddRow("Caja", caja);
                tb.AddRow("Usuario Id", cu.Usuario);
                tb.AddRow("Usuario", usr);

                this.CrearSeguimiento(tb, "Operacion de eliminacion de usuarios cajeros", Utiles.EntidadesEnum.EnCajaUsuarios, enc);
            }
        }

        /// <summary>
        /// Verificar si xiste una caja buscando su codigo
        /// </summary>
        /// <param name="pCodigo">Codigo a buscar</param>
        /// <param name="pId">Id de la caja</param>
        /// <returns></returns>
        public bool CajaExistePorCodigo(String pCodigo, int pId)
        {
            bool res = false;
            IEnumerable<CajaDto> cs = this.Servicio.ReadCajasFiltered("", String.Format("codigo = \"{0}\" and id != {1}", pCodigo, pId));
            if (cs.Count() > 0)
                res = true;
            return res;
        }

        /// <summary>
        /// Crear caja
        /// </summary>
        /// <param name="pCaja">Caja que se creara</param>
        /// <returns></returns>
        public int CajaCrear(CajaDto pCaja)
        {
            String s = this.Servicio.CreateCaja(pCaja);            
            s = s.Replace("Id=", "");

            Utiles.TablaCadena tb = new Utiles.TablaCadena();
            String enc = "Registrar nueva caja\r\n";
            enc =  enc + "====================\r\n";
            tb.AddRow("Atributo", "Valor");
            tb.AddRow("--------", "-----");
            tb.AddRow("Id", s);
            tb.AddRow("Codigo", pCaja.Codigo);
            tb.AddRow("Descripcion", pCaja.Descripcion);

            this.CrearSeguimiento(tb, "Operacion de creacion de caja", Utiles.EntidadesEnum.EnCajas, enc);

            return Convert.ToInt32(s);
        }

        /// <summary>
        /// Traer caja por su Id
        /// </summary>
        /// <param name="pId">Id de la caja a buscar</param>
        /// <returns></returns>
        public CajaDto CajaPorId(int pId)
        {
            return this.Servicio.ReadCaja(String.Format(this.FormatoClave, pId));
        }
    
        /// <summary>
        /// Validar si se ha registrado un saldo de caja en determinada fecha
        /// </summary>
        /// <param name="pCajaSaldo">Saldo de caja a consultar</param>
        /// <param name="pFecha">Fecha de registro</param>
        /// <param name="pEstado">Estado del registro</param>
        /// <returns></returns>
        public bool SaldoCajaRegistradoPorFecha(CajaComprobanteDto pCajaSaldo, DateTime pFecha, int pEstado)
        {
            bool res = false;

            IEnumerable<CajaComprobanteDto> cms;
            if (pEstado == 9)
                cms = this.Servicio.ReadCajaComprobantesFiltered
                    ("", String.Format("tipo = 3 and caja = {0} and fecha = DateTime.Parse(\"{1}\") and id != {2}", pCajaSaldo.Caja, pFecha, pCajaSaldo.Id));
            else
                cms = this.Servicio.ReadCajaComprobantesFiltered
                    ("", String.Format("tipo = 3 and estado = {0} and caja = {1} and fecha = DateTime.Parse(\"{2}\") and id != {3}", pEstado, pCajaSaldo.Caja, pFecha, pCajaSaldo.Id));
            
            if (cms.Count() > 0)
                res = true;
            
            return res;
        }

        /// <summary>
        /// Consultar si se encuentra registrado un saldo de una caja en la fecha recibida
        /// </summary>
        /// <param name="pFecha">Fecha de consulta</param>
        /// <param name="pCaja">Caja a consultar</param>
        /// <returns></returns>
        public CajaComprobanteDto SaldoCajaPorFechaCaja(DateTime pFecha, int pCaja)
        {
            return this.Servicio.ReadCajaComprobantesFiltered("", String.Format("tipo = 3 and caja = {0} and fecha = DateTime.Parse(\"{1}\")", pCaja, pFecha)).FirstOrDefault();

        }
    
        /// <summary>
        /// Registrar comprobante de caja
        /// </summary>
        /// <param name="pComprobante">Comprobante nuevo</param>
        /// <returns></returns>
        public int ComprobanteCajaNuevo(CajaComprobanteDto pComprobante)
        {
            string s = this.Servicio.CreateCajaComprobante(pComprobante);
            s = s.Replace("Id=", "");

            String caja = "N/D";
            if (pComprobante.CajaNav != null)
            {
                caja = pComprobante.CajaNav.Descripcion;
            }
            else
            {
                CajaDto oc = this.Servicio.ReadCaja(String.Format(this.FormatoClave, pComprobante.Caja));
                if (oc != null)
                {
                    caja = oc.Descripcion;
                }
            }

            String tipo = "N/D";
            if (pComprobante.TipoNav != null)
            {
                tipo = pComprobante.TipoNav.Denominacion;
            }
            else
            {
                TablaClaveDto tp = this.Servicio.ReadTablaClave(String.Format(this.FormatoClave, pComprobante.Tipo));
                if (tp != null)
                    tipo = tp.Denominacion;
            }
            
            int num = this.Servicio.NCreditoNumeroSigue();            

            Utiles.TablaCadena tb = new Utiles.TablaCadena();
            String enc = "Registrar nuevo saldo de caja\r\n";
            enc =  enc + "=============================\r\n";
            tb.AddRow("Atributo", "Valor");
            tb.AddRow("--------", "-----");
            tb.AddRow("Id", s);
            tb.AddRow("Caja Id", pComprobante.Caja);
            tb.AddRow("Caja", caja);
            tb.AddRow("Tipo", tipo);
            tb.AddRow("Numero", num);
            tb.AddRow("Fecha", pComprobante.Fecha);
            tb.AddRow("Valor", pComprobante.Valor);
            if (pComprobante.Categoria != null && pComprobante.Categoria > 0)
            {
                tb.AddRow("Categoria Id", pComprobante.Categoria);
                if (pComprobante.CategoriaNav.Denominacion != null)
                    tb.AddRow("Categoria", pComprobante.CategoriaNav.Denominacion);
            }
            tb.AddRow("Descripcion", pComprobante.Descripcion);

            this.CrearSeguimiento(tb, "Operacion de modificacion de usuario cajero", Utiles.EntidadesEnum.EnCajaMovimientos, enc);
            return Convert.ToInt32(s);
        }

        /// <summary>
        /// Modificar comprobante de caja
        /// </summary>
        /// <param name="pComprobante">Comprobante a modificar</param>
        public void ComprobanteCajaModificar(CajaComprobanteDto pComprobante)
        {
            CajaComprobanteDto oldsc = this.Servicio.ReadCajaComprobante(String.Format(this.FormatoClave, pComprobante.Id));
            this.Servicio.UpdateCajaComprobante(pComprobante);

            String caja = "N/D";
            if (pComprobante.CajaNav != null)
            {
                caja = pComprobante.CajaNav.Descripcion;
            }
            else
            {
                CajaDto oc = this.Servicio.ReadCaja(String.Format(this.FormatoClave, pComprobante.Caja));
                if (oc != null)
                {
                    caja = oc.Descripcion;
                }
            }

            String oldcaja = "N/D";
            if (oldsc.Caja == pComprobante.Caja)
            {
                oldcaja = caja;
            }
            else
            {
                CajaDto oc = this.Servicio.ReadCaja(String.Format(this.FormatoClave, oldsc.Caja));
                if (oc != null)
                {
                    caja = oc.Descripcion;
                }
            }            

            String tipo = "N/D";
            if (pComprobante.TipoNav != null)
            {
                tipo = pComprobante.TipoNav.Denominacion;
            }
            else
            {
                TablaClaveDto tp = this.Servicio.ReadTablaClave(String.Format(this.FormatoClave, pComprobante.Tipo));
                if (tp != null)
                    tipo = tp.Denominacion;
            }

            String otipo = "N/D";
            if (oldsc.TipoNav != null)
            {
                otipo = oldsc.TipoNav.Denominacion;
            }
            else
            {
                TablaClaveDto tp = this.Servicio.ReadTablaClave(String.Format(this.FormatoClave, oldsc.Tipo));
                if (tp != null)
                    otipo = tp.Denominacion;
            }

            Utiles.TablaCadena tb = new Utiles.TablaCadena();
            String enc = "Modificar comprobante de caja\r\n";
            enc =  enc + "=======================\r\n";
            tb.AddRow("Atributo", "Original", "Nuevo");
            tb.AddRow("--------", "--------", "-----");
            tb.AddRow("Id", oldsc.Id, pComprobante.Id);
            tb.AddRow("Caja Id", oldsc.Caja, pComprobante.Caja);
            tb.AddRow("Caja", oldcaja, caja);
            tb.AddRow("Tipo", otipo, tipo);
            tb.AddRow("Numero", oldsc.Numero, pComprobante.Numero);
            tb.AddRow("Fecha", oldsc.Fecha, pComprobante.Fecha);
            tb.AddRow("Valor", oldsc.Valor, pComprobante.Valor);
            if (pComprobante.Categoria != null && pComprobante.Categoria > 0)
            {
                tb.AddRow("Categoria Id", oldsc.Categoria, pComprobante.Categoria);
                String den = "N/D";
                String oden = "N/D";
                if (pComprobante.CategoriaNav != null && pComprobante.CategoriaNav.Denominacion != null)
                    den = pComprobante.CategoriaNav.Denominacion;
                if (oldsc.CategoriaNav != null && oldsc.CategoriaNav.Denominacion != null)
                    tb.AddRow("Categoria", oden, den);
            }
            tb.AddRow("Descripcion", oldsc.Descripcion, pComprobante.Descripcion);

            this.CrearSeguimiento(tb, "Operacion de modificacion de saldo de caja", Utiles.EntidadesEnum.EnCajaMovimientos, enc);
        }
    

    }
}
