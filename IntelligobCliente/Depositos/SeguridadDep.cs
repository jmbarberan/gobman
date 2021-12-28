using System;
using System.Collections.Generic;
using System.Linq;
using Intelligob.Cliente.Referencia;
//using Intelligob.Utiles;
using Intelligob.Utilerias;

namespace Intelligob.Cliente.Depositos
{
    public class SeguridadDep : DepositoBase
    {
        public SeguridadDep() : base(DepositosControl.Instance.Servicio) { }
        public SeguridadDep(Intelligob.Cliente.Referencia.IEntidades servicio) : base(servicio) { }

        public IEnumerable<UsuarioDto> UsuariosPorEstado(int pEstado)
        {
            return this.UsuariosPorEstado(pEstado, true);
        }

        /// <summary>
        /// Traer todos los usuarios por estado
        /// </summary>
        /// <param name="pEstado">Estado de los usuarios (9 = TODOS)</param>
        /// <returns></returns>
        public IEnumerable<UsuarioDto> UsuariosPorEstado(int pEstado, bool pMostrarAdministrador)
        {
            if (pEstado == 9)
            {
                if (pMostrarAdministrador)
                    return this.Servicio.ReadUsuarios();
                else
                    return this.Servicio.ReadUsuariosFiltered("nombres", "id > 1");
            }
            else
            {
                if (pMostrarAdministrador)
                    return this.Servicio.ReadUsuariosFiltered("nombres", String.Format("estado = {0}", pEstado));
                else
                    return this.Servicio.ReadUsuariosFiltered("nombres", String.Format("id > 1 and estado = {0}", pEstado));
            }
        }        

        /// <summary> 
        /// Validar si un codigo ya esta registrado con otro usuario
        /// </summary>
        public Boolean UsuarioCodigoRegistrado(string pCodigo, int pId)
        {
            Boolean ret = false;
            List<UsuarioDto> usrs = this.Servicio.ReadUsuarios().Where(u => u.Id != pId && u.Codigo.ToUpper() == pCodigo.ToUpper()).ToList();
            if (usrs.Count > 0)
            {
                ret = true;
            }
            return ret;
        }

        /// <summary> 
        /// Registrar usuario nuevo
        /// </summary>
        public virtual int UsuarioNuevo(UsuarioDto usr)
        {
            string sid = this.Servicio.CreateUsuario(usr);
            sid = sid.Replace("Id=", "");

            String caduca = "No";
            String caducaFecha = "N/A";
            if (usr.Caduca)
            {
                caduca = "Si";
                if (usr.CaducaFecha != null)
                {
                    caducaFecha = String.Format("{0:d}", usr.CaducaFecha); 
                }
            }

            Utiles.TablaCadena tb = new Utiles.TablaCadena();
            String enc = "Crear usuario nuevo\r\n";
            enc = enc + "===================\r\n";
            tb.AddRow("Atributo", "Valor");
            tb.AddRow("--------", "-----");
            tb.AddRow("Usuario Id", usr.Id);
            tb.AddRow("Codigo", usr.Codigo);
            tb.AddRow("Nombres", usr.Nombres);
            tb.AddRow("Caduca", caduca);
            tb.AddRow("Fecha caducidad", caducaFecha);

            this.CrearSeguimiento(tb, "Operacion de Mantenimiento de usuarios", Utiles.EntidadesEnum.EnUsuarios, enc);

            return Convert.ToInt32(sid);
        }

        /// <summary> 
        /// Modifcar el estado de un Usuario para señalar si esta eliminado o activo */
        /// </summary>
        public virtual void UsuarioModificar(UsuarioDto usr)
        {
            UsuarioDto oldusr = this.Servicio.ReadUsuario(String.Format(this.FormatoClave, usr.Id));
            this.Servicio.UpdateUsuario(usr);

            String caduca = "No";
            String caducaFecha = "N/A";
            if (usr.Caduca)
            {
                caduca = "Si";
                if (usr.CaducaFecha != null)
                {
                    caducaFecha = String.Format("{0:d}", usr.CaducaFecha);
                }
            }

            String oldCaduca = "No";
            String oldcaducaFecha = "N/A";
            if (oldusr.Caduca)
            {
                oldCaduca = "Si";
                if (oldusr.CaducaFecha != null)
                {
                    oldcaducaFecha = String.Format("{0:d}", oldusr.CaducaFecha);
                }
            }

            Utiles.TablaCadena tb = new Utiles.TablaCadena();
            String enc = "Modificar cuenta de agua potable\r\n";
            enc =  enc + "================================\r\n";
            tb.AddRow("Atributo", "Original", "Modificado");
            tb.AddRow("--------", "--------", "----------");
            tb.AddRow("Id", oldusr.Id, usr.Id);
            tb.AddRow("Codigo", oldusr.Codigo, usr.Codigo);
            tb.AddRow("Nombres", oldusr.Nombres, usr.Nombres);
            tb.AddRow("Caduca", oldCaduca, caduca);
            tb.AddRow("Caduca Fecha", oldcaducaFecha, caducaFecha);
            tb.AddRow("Estado", oldusr.Estado, usr.Estado);

            this.CrearSeguimiento(tb, "Operacion de Mantenimiento de usuarios", Utiles.EntidadesEnum.EnUsuarios, enc);
        }

        /// <summary>
        /// Modificar la clave del usuario determinado
        /// </summary>
        /// <param name="Usr"></param>
        /// <param name="clave"></param>
        public virtual void UsuarioModificarClave(int usrId, string clave)
        {
            String nom = "N/D";
            UsuarioDto usr = Servicio.ReadUsuario(String.Format(this.FormatoClave, usrId));
            usr.Clave = clave;
            nom = usr.Nombres;
            Servicio.UpdateUsuario(usr);

            Utiles.TablaCadena tb = new Utiles.TablaCadena();
            String enc = "Modificar clave de usuario\r\n";
            enc =  enc + "==========================\r\n";
            tb.AddRow("Atributo", "Valor");
            tb.AddRow("--------", "-----");
            tb.AddRow("Usuario Id", usrId);
            tb.AddRow("Usuario", nom);

            this.CrearSeguimiento(tb, "Operacion de Mantenimiento de usuarios", Utiles.EntidadesEnum.EnUsuarios, enc);
        }

        /// <summary>  
        /// Traer todas las funciones
        /// </summary>
        public virtual IEnumerable<FuncionDto> FuncionesPorEstado(int pEstado)
        {
            IEnumerable<FuncionDto> fun = new List<FuncionDto>();
            if (pEstado == 9)
            {
                fun = this.Servicio.ReadFuncions();
            }
            else
            {
                fun = this.Servicio.ReadFuncionsFiltered("", String.Format("Estado = {0}", pEstado));
            }
            foreach (FuncionDto f in fun)
            {
                f.ComandosNav = this.Servicio.ReadComandos().Where(c => c.Funcion == f.Id && f.Estado == 0).ToArray();
            }
            return fun;
        }

        /// <summary>
        /// Traer todos los comandos filtrando por su estado 
        /// </summary>
        /// <param name="pEstado">Estado a filtrar</param>
        /// <returns></returns>
        public virtual IEnumerable<ComandoDto> ComandosPorEstado(int pEstado)
        {
            if (pEstado == 9)
            {
                return this.Servicio.ReadComandos();
            }
            else
            {
                return this.Servicio.ReadComandos().Where(c => c.Estado == pEstado);
            }
        }

        /// <summary>
        /// Traer todos los modulos registrados a un usuario
        /// </summary>
        /// <param name="pUsr">Usuario a consultar</param>
        /// <returns></returns>
        public virtual IEnumerable<ModuloUsuarioDto> ModulosPorUsuario(int pUsr)
        {
            return Servicio.ReadModuloUsuariosFiltered("", String.Format("usuario = {0} And estado = 0", pUsr));
            //return Servicio.ReadModuloUsuarios().Where(m => m.Estado == 0 && m.Usuario == pUsr);
        }

        /// <summary>        
        /// Retorna una lista de los privilegios asignados al usuario recibido
        /// </summary>
        public virtual IEnumerable<PrivilegioDto> PrivilegiosPorUsuario(int pUsr)
        {
            IEnumerable<PrivilegioDto> lp = this.Servicio.ReadPrivilegios().Where(p => p.Usuario == pUsr && p.Estado == 0);
            foreach(PrivilegioDto p in lp)
            {
                p.FuncionNav = ModeloCache.Instance.McFunciones.Where(f => f.Id == p.Funcion).FirstOrDefault();
            }
            return lp;
        }

        /// <summary>
        /// Traer el privilegio asignado a un usuario (Si no esta asignado retorna null)
        /// </summary>
        /// <param name="pFun">Id de la Funcion</param>
        /// <param name="pUsr">Id del Usuario</param>
        /// <returns></returns>
        public virtual PrivilegioDto PrivilegiosFuncionPorUsuario(int pFun, int pUsr)
        {
            return Servicio.ReadPrivilegios().Where(p => p.Estado == 0 && p.Usuario == pUsr && p.Funcion == pFun).FirstOrDefault();
        }

        /// <summary>
        /// Crear privilegio nuevo
        /// </summary>
        /// <param name="pri">Privilegios nuevos</param>
        public virtual int PrivilegioCrear(PrivilegioDto pri)
        {            
            if (pri.UsuarioNav == null && pri.Usuario != null)
            {
                pri.UsuarioNav = this.Servicio.ReadUsuario(String.Format(this.FormatoClave, pri.Usuario));
            }

            if (pri.FuncionNav == null && pri.Funcion != null)
            {
                pri.FuncionNav = this.Servicio.ReadFuncion(String.Format(this.FormatoClave, pri.Funcion));
            }

            string s = this.Servicio.CreatePrivilegio(pri);
            s = s.Replace("Id=", "");

            String coms = String.Empty;
            IEnumerable<ComandoDto> lcoms = this.Servicio.ReadComandosFiltered("", String.Format("funcion = {0}", pri.Funcion));
            foreach (ComandoDto c in lcoms)
            {
                if (pri.Comandos.Contains(c.Indice.ToString()))
                    coms = coms + c.Denominacion + ", ";
            }
            if (coms.Length == 0)
                coms = "Ninguno";
            else
            {
                coms = coms.Substring(0, coms.Length - 3);
            }

            Utiles.TablaCadena tb = new Utiles.TablaCadena();
            String enc = "Asignar privilegios a usuario\r\n";
            enc =  enc + "=============================\r\n";
            tb.AddRow("Atributo", "Valor");
            tb.AddRow("--------", "-----");
            tb.AddRow("Id", s);
            tb.AddRow("Usuario Id", pri.Usuario);
            tb.AddRow("Usuario", pri.UsuarioNav.Nombres);
            tb.AddRow("Funcion Id", pri.Funcion);
            tb.AddRow("Funcion", pri.FuncionNav.Denominacion);
            tb.AddRow("Permisos", coms);
            
            this.CrearSeguimiento(tb, "Modificar privilegios de usuario", Utiles.EntidadesEnum.EnPrivilegios, enc);

            return pri.Id;
        }

        public virtual void PrivilegioModificar(PrivilegioDto pri)
        {
            PrivilegioDto oldpri = this.Servicio.ReadPrivilegio(String.Format(this.FormatoClave, pri.Id));
            this.Servicio.UpdatePrivilegio(pri);

            if (pri.UsuarioNav == null && pri.Usuario != null)
            {
                pri.UsuarioNav = this.Servicio.ReadUsuario(String.Format(this.FormatoClave, pri.Usuario));
            }

            if (pri.FuncionNav == null && pri.Funcion != null)
            {
                pri.FuncionNav = this.Servicio.ReadFuncion(String.Format(this.FormatoClave, pri.Funcion));
            }

            oldpri.UsuarioNav = this.Servicio.ReadUsuario(String.Format(this.FormatoClave, oldpri.Usuario));
            oldpri.FuncionNav = this.Servicio.ReadFuncion(String.Format(this.FormatoClave, oldpri.Funcion));

            String coms = String.Empty;
            IEnumerable<ComandoDto> lcoms = this.Servicio.ReadComandosFiltered("", String.Format("funcion = {0}", pri.Funcion));
            foreach (ComandoDto c in lcoms)
            {
                if (pri.Comandos.Contains(c.Indice.ToString()))
                    coms = coms + c.Denominacion + ", ";
            }
            if (coms.Length == 0)
                coms = "Ninguno";
            else
            {
                coms = coms.Substring(0, coms.Length - 3);
            }

            String oldcoms = String.Empty;
            IEnumerable<ComandoDto> olcoms = this.Servicio.ReadComandosFiltered("", String.Format("funcion = {0}", pri.Funcion));
            foreach (ComandoDto oc in olcoms)
            {
                if (pri.Comandos.Contains(oc.Indice.ToString()))
                    oldcoms = oldcoms + oc.Denominacion + ", ";
            }
            if (oldcoms.Length == 0)
                oldcoms = "Ninguno";
            else
            {
                oldcoms = oldcoms.Substring(0, oldcoms.Length - 3);
            }

            Utiles.TablaCadena tb = new Utiles.TablaCadena();
            String enc = "Modificar privilegios de usuario\r\n";
            enc =  enc + "================================\r\n";
            tb.AddRow("Atributo", "Original", "Modificado");
            tb.AddRow("--------", "--------", "----------");
            tb.AddRow("Id", oldpri.Id, pri.Id);
            tb.AddRow("Usuario Id", oldpri.Usuario, pri.Usuario);
            tb.AddRow("Usuario", oldpri.UsuarioNav.Nombres, pri.UsuarioNav.Nombres);
            tb.AddRow("Funcion Id", oldpri.Funcion, pri.Funcion);
            tb.AddRow("Funcion", oldpri.FuncionNav.Denominacion, pri.FuncionNav.Denominacion);
            tb.AddRow("Permisos Originales", coms);
            tb.AddRow("Permisos Nuevos", coms);
            
            this.CrearSeguimiento(tb, "Modificar privilegios de usuarios", Utiles.EntidadesEnum.EnPrivilegios, enc);
        }

        /// <summary>
        /// Eliminar los privilegios quitados a un usuario
        /// </summary>
        /// <param name="prvs">Privilegios a eliminar</param>
        public virtual void PrivilegiosEliminar(List<PrivilegioDto> prvs)
        {            
            foreach (PrivilegioDto p in prvs)
            {
                if (p.UsuarioNav == null && p.Usuario != null)
                {
                    p.UsuarioNav = this.Servicio.ReadUsuario(String.Format(this.FormatoClave, p.Usuario));
                }

                if (p.FuncionNav == null && p.Funcion != null)
                {
                    p.FuncionNav = this.Servicio.ReadFuncion(String.Format(this.FormatoClave, p.Funcion));
                }

                String coms = String.Empty;
                IEnumerable<ComandoDto> lcoms = this.Servicio.ReadComandosFiltered("", String.Format("funcion = {0}", p.Funcion));
                foreach (ComandoDto c in lcoms)
                {
                    if (p.Comandos.Contains(c.Indice.ToString()))
                        coms = coms + c.Denominacion + ", ";
                }
                if (coms.Length == 0)
                    coms = "Ninguno";
                else
                {
                    coms = coms.Substring(0, coms.Length - 3);
                }

                Utiles.TablaCadena tb = new Utiles.TablaCadena();
                String enc = "Modificar privilegios de usuario\r\n";
                enc  = enc + "================================\r\n";
                tb.AddRow("Atributo", "Original");
                tb.AddRow("--------", "--------");
                tb.AddRow("Id:", p.Id);
                tb.AddRow("Usuario", p.UsuarioNav.Nombres);
                tb.AddRow("Funcion", p.FuncionNav.Denominacion);
                tb.AddRow("Permisos", coms);
                this.CrearSeguimiento(tb, "Se quito este privilegio al usuario", Utiles.EntidadesEnum.EnPrivilegios, enc);
            }
            Servicio.DeletePrivilegios(prvs.ToArray());            
        }

        /// <summary>
        /// Devuelve el usuario para el cual coinciden las credenciales proporcionadas
        /// </summary>
        public virtual UsuarioDto UsuarioPorCredencial(string pUsr, string pClave)
        {
            UsuarioDto usr = null;
            IEnumerable<UsuarioDto> ul = this.Servicio.ReadUsuarios().Where(u => u.Codigo == pUsr && u.Clave == Cifrador.RijndaelSimple.Encriptar(pClave));
            if (ul.Count() > 0)
            {
                usr = ul.ElementAt(0);
            }
            return usr;
        }

        /// <summary>
        /// Saber si el usuario desarrollador ha sido habilitado
        /// </summary>
        /// <returns></returns>    
        public virtual bool UsuariosDesarrolladorActivo()
        {
            bool res = false;
            try
            {
                TablaDto tab = Servicio.ReadTabla(String.Format(this.FormatoClave, 1));
                if (tab.Indice == 1)
                    res = true;
            }
            catch
            {
                res = false;
            }
            return res;
        }

        public virtual void UsuarioDesarrolladorAcceso(int pAcceso)
        {
            try
            {
                TablaDto tab = Servicio.ReadTabla(String.Format(this.FormatoClave, 1));
                tab.Indice = pAcceso;
                this.Servicio.UpdateTabla(tab);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
        public virtual IEnumerable<Cliente.Referencia.ModuloDto> ModulosPorEstado(int pEstado)
        {
            return this.Servicio.ReadModulosFiltered("denominacion", String.Format("estado = {0}", pEstado));
        }        
    
        public virtual Cliente.Referencia.ModuloDto ModuloPorId(int pId)
        {
            return this.Servicio.ReadModulo(String.Format(this.FormatoClave, pId));
        }
    }
}
