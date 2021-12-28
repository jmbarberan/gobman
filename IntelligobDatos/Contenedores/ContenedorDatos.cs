using Intelligob.Datos.Referencia1;
using System;
using System.Linq;

namespace Intelligob.Datos.Contenedores
{
    public partial class ContenedorDatos : IContenedor
    {
        private Modelo controlDatos;

        public Modelo ControlDatos
        {
            get
            {
                return this.controlDatos;
            }
            set
            {
                this.controlDatos = value;
            }
        }

        public void Guardar()
        {
            this.ControlDatos.SaveChanges();
        }

        public ContenedorDatos(Modelo pModelo)
        {
            this.ControlDatos = pModelo;
        }

        public IQueryable<CoeficienteElemento> QCoeficientesElementos
        {
            get { return this.ControlDatos.CoeficienteElementos; }
        }

        public IQueryable<Concepto> QConceptos
        {
            get { return this.ControlDatos.Conceptos; }
        }

        public IQueryable<Coeficiente> QCoeficientes 
        { 
            get
            {
                return this.ControlDatos.Coeficientes;
            }
        }

        public IQueryable<CoeficienteElemento> QCoeficienteElementos 
        {
            get { return this.ControlDatos.CoeficienteElementos; }
        }

        public void CrearEntidad(object pEntidad, string pClase)
        {
            ControlDatos.AddObject(pClase, pEntidad);
        }

        public void ActualizarEntidad(object pEntidad)
        {
            ControlDatos.UpdateObject(pEntidad);
        }

        public void EliminarEntidad(object pEntidad)
        {
            ControlDatos.DeleteObject(pEntidad);
        }

        public IQueryable<Contribuyente> QContribuyentes
        {
            get { return this.ControlDatos.Contribuyentes; }
        }

        public IQueryable<Tabla> QTablas
        {
            get { return this.ControlDatos.Tablas; }
        }

        public IQueryable<TablaClave> QTablaClaves
        {
            get { return this.ControlDatos.TablaClaves; }
        }

        public IQueryable<Usuario> QUsuarios
        {
            get { return this.ControlDatos.Usuarios; }
        }
        
        public int UsuarioCrear(Usuario usr)
        {
            this.CrearEntidad(usr, "Usuarios");
            ControlDatos.SaveChanges();
            return usr.Id;
        }
        
        public IQueryable<Privilegio> QPrivilegios
        {
            get { return this.ControlDatos.Privilegios; }
        }

        public IQueryable<Funcion> QFunciones
        {
            get { return this.ControlDatos.Funciones; }
        }

        public IQueryable<ModuloUsuario> QModulosUsuarios
        {
            get { return this.ControlDatos.ModuloUsuarios; }
        }

        public int ContribuyenteCrear(Contribuyente pNuevo)
        {
            ControlDatos.AddToContribuyentes(pNuevo);
            ControlDatos.SaveChanges();
            return pNuevo.Id;
        }

        public int SeguimientoCrear(Seguimiento pSeg)
        {
            controlDatos.AddToSeguimientos(pSeg);
            ControlDatos.SaveChanges();
            return pSeg.Id;
        }
    
        public int PrivilegioCrear(Privilegio prv)
        {
            ControlDatos.AddToPrivilegios(prv);
            ControlDatos.SaveChanges();
            return prv.Id;
        }

        public IQueryable<Funcion> QFuncionesExtendido 
        {
            get { return ControlDatos.Funciones.Expand("ComandosNav"); }
        }        
    }
}
