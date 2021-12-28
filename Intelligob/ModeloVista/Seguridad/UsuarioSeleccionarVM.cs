using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Intelligob.Escritorio.ModeloVista.Seguridad
{
    public class UsuarioSeleccionarVM : BaseMV<Vistas.Interfaces.IVentanaDialogo>
    {
        private readonly Cliente.Depositos.SeguridadDep seguridadDep = new Cliente.Depositos.SeguridadDep();
        private ObservableCollection<Cliente.Referencia.UsuarioDto> lUsuarios;
        public ObservableCollection<Cliente.Referencia.UsuarioDto> LUsuarios
        {
            get { return this.lUsuarios; }
            set
            {
                this.lUsuarios = value;
                OnPropertyChanged("LUsuarios");
            }
        }

        private Cliente.Referencia.UsuarioDto seleccionado;
        public Cliente.Referencia.UsuarioDto Seleccionado
        {
            get { return this.seleccionado; }
            set { this.seleccionado = value; OnPropertyChanged("Seleccionado"); }
        }

        public System.Windows.Input.ICommand CmdSeleccionar { get; internal set; }

        public UsuarioSeleccionarVM() : this(null, false, false) { }

        public UsuarioSeleccionarVM(bool pMostrarAdministrador) : this(null, false, pMostrarAdministrador) { }

        public UsuarioSeleccionarVM(bool pMostrarEliminados, bool pMostrarAdministrador) : this(null, pMostrarEliminados, pMostrarAdministrador) { }

        public UsuarioSeleccionarVM(IEnumerable<Cliente.Referencia.UsuarioDto> pUsuarios, bool pMostrarEliminados, bool pMostrarAdministrador) : base(new Vistas.Seguridad.UsuarioSeleccionar())
        {
            if (pUsuarios == null)
            {
                int est = 0;
                if (pMostrarEliminados)
                    est = 9;
                pUsuarios = seguridadDep.UsuariosPorEstado(est, pMostrarAdministrador);
            }
            this.LUsuarios = new ObservableCollection<Cliente.Referencia.UsuarioDto>(pUsuarios);
            this.CmdSeleccionar = new Comandos.ComandoDelegado((o) => SeleccionarAccion(), (o) => SeleccionarHabilita());
            this.Vista.Owner = App.Current.MainWindow;
            this.Vista.ShowDialog();
        }

        private bool SeleccionarHabilita()
        {
            return this.Seleccionado != null;
        }

        private void SeleccionarAccion()
        {
            this.Vista.DialogResult = true;
            this.Vista.Close();
        }
    }
}
