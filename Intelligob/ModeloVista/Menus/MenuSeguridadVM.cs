using Intelligob.Cliente;
using Intelligob.Cliente.Depositos;
using Intelligob.Escritorio.ModeloVista.Comandos;
using Intelligob.Escritorio.Vistas.General;
using Intelligob.Escritorio.Vistas.Interfaces;
using Intelligob.Escritorio.Vistas.Seguridad;
using Intelligob.Utiles;
using System;
using System.Linq;
using System.Windows.Input;

namespace Intelligob.Escritorio.ModeloVista
{
    public class MenuSeguridadVM : BaseMV<IPagina>
    {
        private readonly SeguridadDep seguridadDep = new SeguridadDep();
        private bool usuariosHabilitado = false;
        private bool supervisionHabilitado = false;
        private bool claveHabilitado = false;
        public ICommand CmdMostrarUsuarios
        {
            get;
            internal set;
        }

        public ICommand CmdMostrarSupervision
        {
            get;
            internal set;
        }

        public ICommand CmdClave
        { get; internal set; }

        public MenuSeguridadVM() : this(new MenuSeguridad()) { }

        public MenuSeguridadVM(IPagina pVista)
            : base(pVista)
        {
            this.ProcesarPrivilegios();
            this.CmdMostrarUsuarios = new ComandoDelegado((o) => this.AccionMostrarUsuarios(), (o) => this.HabilitaUsuarios());
            this.CmdMostrarSupervision = new ComandoDelegado((o) => this.AccionMostrarSupervision(), (o) => this.HabilitaSupervision());
            this.CmdClave = new ComandoDelegado((o) => AccionClave(), (o) => this.HablitaClave());
        }

        private void ProcesarPrivilegios()
        {
            if (SesionUtiles.Instance.EsDesarrollador || SesionUtiles.Instance.UsuarioActivo.Id == 1)
                this.usuariosHabilitado = true;

            if (SesionUtiles.Instance.EsDesarrollador || SesionUtiles.Instance.UsuarioActivo.Id == 1)
                this.supervisionHabilitado = true;

            if (SesionUtiles.Instance.EsDesarrollador || SesionUtiles.Instance.UsuarioActivo.Id == 1 || seguridadDep.PrivilegiosFuncionPorUsuario(9, SesionUtiles.Instance.UsuarioActivo.Id) != null)
                this.claveHabilitado = true;
        }

        private void AccionMostrarUsuarios()
        {
            UsuariosListaVM ul = new UsuariosListaVM();
            Navegador.NavigationService.Navigate(ul.Vista);
        }

        private void AccionMostrarSupervision()
        {
            //
        }

        private void AccionClave()
        {
            String clave = "";
            Clave cve = new Clave();
            cve.Title = "Contraseña nueva";
            cve.ShowDialog();
            if (cve.DialogResult == true)
            {
                clave = cve.Contraseña;
            }
            if (String.IsNullOrWhiteSpace(clave))
            {
                CuadroMensajes.Alertar("Operacion incompleta", "Atencion", "No se puede guardar una contraseña vacia", "");
            }
            else
            {
                CuadroMensajes.Aceptar("Informacion", "Comfirmar clave", "Vuelva a Digitar la clave para confirmar su correcta escritura", "", TaskDialogInterop.VistaTaskDialogIcon.Information);
                Clave cve1 = new Clave();
                cve1.Contraseña = "";
                cve1.Title = "Confirmar contraseña";
                cve1.ShowDialog();
                if (cve1.DialogResult == true)
                {
                    if (clave == cve1.Contraseña)
                    {
                        clave = Intelligob.Utilerias.Cifrador.RijndaelSimple.Encriptar(clave);
                        seguridadDep.UsuarioModificarClave(SesionUtiles.Instance.UsuarioActivo.Id, clave);
                    }
                }
            }
        }

        private bool HabilitaUsuarios()
        {
            return this.usuariosHabilitado;
        }

        private bool HabilitaSupervision()
        {
            return this.supervisionHabilitado;
        }

        private bool HablitaClave()
        {
            return this.claveHabilitado;
        }

    }
}
