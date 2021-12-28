using Intelligob.Datos.Contenedores;
using Intelligob.Datos.Referencia1;
using Intelligob.Utiles;
using Intelligob.Utiles.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intelligob.Datos.Depositos
{
    public class SeguridadDep : DepositoBase
    {
        public SeguridadDep()
            : base(DepositosControl.Instancia.Servicio)
        { }
        public SeguridadDep(IContenedor pServicio)
            : base(pServicio)
        { }

        /// <summary> 
        /// Traer todos los usuarios
        /// </summary>
        public List<Usuario> UsuariosPorEstado(int pEstado)
        {
            if (pEstado == 9)
            {
                return this.Servicio.QUsuarios.ToList();
            }
            else
            {
                return this.Servicio.QUsuarios.Where(u => u.Estado == pEstado).ToList();
            }
        }

        /// <summary> 
        /// Validar si un codigo ya esta registrado con otro usuario
        /// </summary>
        public Boolean UsuarioCodigoRegistrado(string pCodigo, int pId)
        {
            Boolean ret = false;
            List<Usuario> usrs = this.Servicio.QUsuarios.Where(u => u.Id != pId && u.Codigo.ToUpper() == pCodigo.ToUpper()).ToList();
            if (usrs.Count > 0)
            {
                ret = true;
            }
            return ret;
        }

        /// <summary> 
        /// Registrar usuario nuevo
        /// </summary>
        public void UsuarioNuevo(Usuario usr)
        {
            Servicio.UsuarioCrear(usr);
        }

        /// <summary> 
        /// Modifca el estado de un Usuario para señalar si esta eliminado o activo */
        /// </summary>
        public void UsuarioModificarEstado(int pId, int pEstado)
        {
            try
            {
                Usuario usrModificado = Servicio.QUsuarios.Where(u => u.Id == pId).FirstOrDefault();
                usrModificado.Estado = pEstado;
                Guardar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>  
        /// Traer todas las funciones
        /// </summary>
        public List<Funcion> FuncionesPorEstado(int pEstado)
        {
            List<Funcion> fun = new List<Funcion>();
            if (pEstado == 9)
            {
                fun = this.Servicio.QFunciones.ToList();
            }
            else
            {
                fun = this.Servicio.QFunciones.Where(f => f.Estado == pEstado).ToList();
            }
            return fun;
        }

        public IEnumerable<Funcion> FuncionesExtendidoPorEstado(int pEstado)
        {
            if (pEstado == 9)
            {
                return this.Servicio.QFuncionesExtendido.ToList();
            }
            else
            {
                return this.Servicio.QFuncionesExtendido.Where(f => f.Estado == pEstado).ToList();
            }
        }

        public List<ModuloUsuario> ModulosPorUsuario(int pUsr)
        {
            return Servicio.QModulosUsuarios.Where(m => m.Usuario == pUsr).ToList();
        }

        /// <summary>        
        /// Retorna una lista de los privilegios asignados al usuario recibido
        /// </summary>
        public List<Privilegio> PrivilegiosPorUsuario(int pUsr)
        {
            List<Privilegio> mPriv = new List<Privilegio>();
            mPriv = this.Servicio.QPrivilegios.Where(p => p.Usuario == pUsr && p.Estado == 0).ToList();
            return mPriv;
        }

        public IEnumerable<Privilegio> PrivilegosExtendidoPorUsuario(int pUsr)
        {
            IEnumerable<Privilegio> lp = this.Servicio.QPrivilegios.Where(p => p.Usuario == pUsr);
            foreach(Privilegio p in lp)
            {
                p.FuncionNav = Servicio.QFuncionesExtendido.Where(f => f.Id == p.Funcion).FirstOrDefault();
            }
            return lp;
        }

        /// <summary>
        /// Traer el privilegio asignado a un usuario (Si no esta asignado retorna null)
        /// </summary>
        /// <param name="pFun">Id de la Funcion</param>
        /// <param name="pUsr">Id del Usuario</param>
        /// <returns></returns>
        public Privilegio PrivilegiosFuncionPorUsuario(int pFun, int pUsr)
        {
            return Servicio.QPrivilegios.Where(p => p.Estado == 0 && p.Usuario == pUsr && p.Funcion == pFun).FirstOrDefault();
        }

        /// <summary>
        /// Crear privilegio nuevo
        /// </summary>
        /// <param name="pri">Privilegios nuevos</param>
        public int PrivilegioCrear(Privilegio pri)
        {
            return this.Servicio.PrivilegioCrear(pri);
        }

        public void PrivilegiosEliminar(List<Privilegio> prvs)
        {
            TablaCadena tb = new TablaCadena();
            tb.AddRow("Operaciones ejecutadas:", "", "");
            tb.AddRow("Eliminado:", "", "");
            {
                foreach (Privilegio p in prvs)
                {
                    tb.AddRow("Tipo de objeto", "Privilegios/Usuario", "");
                    tb.AddRow("Id:", p.Id, "");
                    tb.AddRow("Atributo", "", "Original");
                    tb.AddRow("--------", "", "--------");
                    tb.AddRow("Usuario", "", p.UsuarioNav.Nombres);
                    tb.AddRow("Funcion", "", p.FuncionNav.Denominacion);
                    tb.AddRow("Permisos", "", p.Comandos);
                    Servicio.EliminarEntidad(p); // Eliminar privilefgio
                }
                Seguimiento s = new Seguimiento();
                s.Id = 0;
                s.Estado = 0;
                if (SesionUtiles.Instance.EsDesarrollador)
                    s.Usuario = 0;
                else
                    s.Usuario = SesionUtiles.Instance.UsuarioActivo.Id;
                s.Cliente = Configuracion.NombrePc;
                s.Direccion = Configuracion.IPLocal;
                s.Comentario = "Se quito este privilegio al usuario";
                s.Original = tb.Output();
                Servicio.SeguimientoCrear(s); // TODO Agregar privilegio
                this.Servicio.Guardar();
            }
        }

        /// <summary>
        /// Devuelve el usuario para el cual coinciden las credenciales proporcionadas
        /// </summary>
        public Usuario UsuarioPorCredencial(string pUsr, string pClave)
        {
            Usuario usr = null;
            IEnumerable<Usuario> ul = this.Servicio.QUsuarios.Where(u => u.Codigo == pUsr && u.Clave == Cifrador.RijndaelSimple.Encriptar(pClave));
            if (ul.Count() > 0)
            {
                usr = ul.ElementAt(0);
            }
            return usr;
        }

        public bool UsuariosDesarrolladorActivo()
        {
            bool res = false;
            try
            {
                Tabla tab = Servicio.QTablas.Where(t => t.Id == 1).FirstOrDefault();
                if (tab.Indice == 1)
                    res = true;
            }
            catch
            {
                res = false;
            }
            return res;
        }
    }
}
