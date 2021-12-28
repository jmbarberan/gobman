using Intelligob.Escritorio.ModeloVista.Comandos;
using Intelligob.Cliente.Referencia;
using Intelligob.Cliente.Depositos;
using Intelligob.Escritorio.Vistas.General;
using Intelligob.Escritorio.Vistas.Interfaces;
using Intelligob.Utiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Intelligob.Cliente;

namespace Intelligob.Escritorio.ModeloVista
{
    public class ValidarIngresoVM : BaseMV<IPagina>, IDataErrorInfo
    {
        private readonly SeguridadDep clienteWeb = new SeguridadDep();
        private readonly ComandoDelegado cmdIngresar1;

        public ComandoDelegado CmdIngresar
        {
            get { return this.cmdIngresar1; }            
        }

        private string usrCodigo;
        public string UsrCodigo
        {
            get { return this.usrCodigo; }
            set { this.usrCodigo = value; OnPropertyChanged("UsrCodigo"); }
        }

        private string usrClave;
        public string UsrClave
        {
            get { return this.usrClave; }
            set { this.usrClave = value; OnPropertyChanged("UsrClave"); }
        }

        private bool puedeIngresar;
        public bool PuedeIngresar
        {
            get { return this.puedeIngresar; }
            set { this.puedeIngresar = value; OnPropertyChanged("PuedeIngresar"); }
        }

        public ValidarIngresoVM() : this(new ValidarIngreso()) { }        

        public ValidarIngresoVM(IPagina pVista)
            : base(pVista)
        {            
            this.cmdIngresar1 = new ComandoDelegado(AccionIngresar);
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {            
            get 
            {
                String error = String.Empty;
                if (columnName == "UsrCodigo")
                {
                    if (String.IsNullOrWhiteSpace(this.usrCodigo))
                        error = "Se requiere el codigo de usuario";
                }
                else
                {
                    if (columnName == "UsrClave")
                    {
                        if (String.IsNullOrWhiteSpace(this.usrClave))
                            error = "Se requiere la contraseña de acceso";
                    }
                }
                PuedeIngresar = error == String.Empty;
                return error;
            }
        }

        public void AccionIngresar(object parameter)
        {
            Boolean v = false;
            if (this.usrCodigo.Trim().ToUpper() == "DESARROLLADOR")
            {
                // Validar Desarrollador
                if (clienteWeb.UsuariosDesarrolladorActivo())
                {
                    SesionUtiles.Instance.EsDesarrollador = true;
                    SesionUtiles.Instance.UsuarioActivo = null;                    
                    v = true;
                }
                else
                {
                    CuadroMensajes.Alertar("No puede ingresar", "Credenciales invalidas", "El acceso para este Usuario no esta habilitado", "");
                }
            }
            else
            {
                if (!String.IsNullOrWhiteSpace(this.usrClave))
                { 
                    
                    UsuarioDto u = clienteWeb.UsuarioPorCredencial(UsrCodigo, UsrClave);
                    if (u != null)
                    {
                        if (u.Id > 0)
                        {
                            SesionUtiles.Instance.UsuarioActivo = u;
                            SesionUtiles.Instance.EsDesarrollador = false;
                            v = true;
                        }
                    }
                    else
                    {
                        CuadroMensajes.Alertar("No puede ingresar", "Credenciales invalidas", "El Codigo de usuario y/o contraseña digitado son incorrectos", "");
                    }
                }
                else
                {
                    CuadroMensajes.Alertar("No puede validar", "Contraseña vacia", "Debe digitar la contraseña de acceso al sistema", "");
                }
            }
            if (v == true)
            {
                this.CargarModulosUsuarioActivo();
                Configuracion.InsPriAuxiliar.ModuloVisibilidad = System.Windows.Visibility.Visible;
                Inicio i = new Inicio();
                Navegador.NavigationService.Navigate(i);
                String nom = Configuracion.MenuInicial;
                nom = nom.Replace("VM", "");
                nom = nom.Replace("Menu", "ModVis");
                object ovm = null;
                if (Configuracion.InsPriAuxiliar.ModuloHabilitado(nom))
                {
                    nom = Configuracion.MenuInicial;
                }
                else
                {
                    nom = Configuracion.InsPriAuxiliar.ModuloPredeterminado();
                    if (String.IsNullOrWhiteSpace(nom))
                    {
                        nom = "VacioVM";
                    }
                }
                ovm = Activator.CreateInstance("Intelligob", "Intelligob.Escritorio.ModeloVista." + nom).Unwrap();
                BaseMV<IPagina> vm = (BaseMV<IPagina>)ovm;
                NavegadorFunciones.NavigationService.Navigate(vm.Vista);
            }
        }

        public void CargarModulosUsuarioActivo()
        {
            Configuracion.InsPriAuxiliar.ModulosTodos(System.Windows.Visibility.Collapsed);
            if (SesionUtiles.Instance.EsDesarrollador)
            {
                Configuracion.InsPriAuxiliar.ModulosTodos(System.Windows.Visibility.Visible);
                NavegadorFunciones.PaginaInicial = (new MenuSeguridadVM()).Vista;
            }
            else
            {
                if (SesionUtiles.Instance.UsuarioActivo.Id == 1)
                {
                    Configuracion.InsPriAuxiliar.ModVisSeguridad = System.Windows.Visibility.Visible;
                    NavegadorFunciones.PaginaInicial = (new MenuSeguridadVM()).Vista;
                }
                else
                {
                    IEnumerable<ModuloUsuarioDto> m = clienteWeb.ModulosPorUsuario(SesionUtiles.Instance.UsuarioActivo.Id);
                    foreach (ModuloUsuarioDto mu in m)
                    {
                        if (mu.Boton != null && !String.IsNullOrWhiteSpace(mu.Boton))
                        {
                            Configuracion.InsPriAuxiliar.HabilitarModulo(mu.Boton);
                        }
                    }
                }
            }
        }
    }
}
