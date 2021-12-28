using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Intelligob.Escritorio.ModeloVista.Recaudaciones
{
    public class CajaUsuariosVM : BaseMV<Vistas.Interfaces.IVentanaDialogo>
    {
        private readonly int cajaId = 0;
        private readonly Cliente.Depositos.CajasDep cajasDep = new Cliente.Depositos.CajasDep();

        private readonly List<Cliente.Referencia.CajasUsuarioDto> lCUsuariosEliminados = new List<Cliente.Referencia.CajasUsuarioDto>();
        private ObservableCollection<Cliente.Referencia.CajasUsuarioDto> lCUsuarios;

        public ObservableCollection<Cliente.Referencia.CajasUsuarioDto> LCUsuarios
        {
            get { return this.lCUsuarios; }
            set { this.lCUsuarios = value; OnPropertyChanged("LCUsuarios"); }
        }

        private Cliente.Referencia.CajasUsuarioDto seleccionado;
        public Cliente.Referencia.CajasUsuarioDto Seleccionado
        {
            get { return this.seleccionado; }
            set { this.seleccionado = value; OnPropertyChanged("Seleccionado"); }
        }

        public System.Windows.Input.ICommand CmdGuardar { get; internal set; }
        public System.Windows.Input.ICommand CmdAgregar { get; internal set; }
        public System.Windows.Input.ICommand CmdQuitar { get; internal set; }

        public CajaUsuariosVM(int pCajaId) : base(new Vistas.Recaudaciones.CajaUsuarios())
        {
            this.cajaId = pCajaId;
            IEnumerable<Cliente.Referencia.CajasUsuarioDto> cus = cajasDep.CajaUsuariosPorCajaEstado(pCajaId, 0, true);
            LCUsuarios = new ObservableCollection<Cliente.Referencia.CajasUsuarioDto>(cus);
            this.CrearComandos();
            this.Vista.Owner = App.Current.MainWindow;
            this.Vista.ShowDialog();
        }

        private void CrearComandos()
        {
            this.CmdAgregar = new Comandos.ComandoDelegado((o) => this.AgregarAccion());
            this.CmdQuitar = new Comandos.ComandoDelegado((o) => this.QuitarAccion(), (o) => this.QuitarHabilita());
            this.CmdGuardar = new Comandos.ComandoDelegado((o) => this.GuardarAccion());
        }

        private bool QuitarHabilita()
        {
            return this.Seleccionado != null;
        }

        private void AgregarAccion()
        {
            Seguridad.UsuarioSeleccionarVM usel = new Seguridad.UsuarioSeleccionarVM(false, false);
            if (usel.Vista.DialogResult == true)
            {
                Boolean ins = false;
                foreach (Cliente.Referencia.CajasUsuarioDto u in LCUsuarios)
                {
                    if (u.Usuario == usel.Seleccionado.Id)
                    {
                        ins = true;
                        break;
                    }
                }
                if (!ins)
                {
                    Cliente.Referencia.CajasUsuarioDto cu = new Cliente.Referencia.CajasUsuarioDto();
                    cu.Id = 0;
                    cu.Usuario = usel.Seleccionado.Id;
                    cu.UsuarioNav = usel.Seleccionado;
                    cu.Caja = this.cajaId;
                    cu.Estado = 0;
                    LCUsuarios.Add(cu);
                }
            }
        }

        private void QuitarAccion()
        {
            if (this.Seleccionado.Id > 0)
            {
                Cliente.Referencia.CajasUsuarioDto a = this.Seleccionado;
                this.lCUsuariosEliminados.Add(a);
            }
            LCUsuarios.Remove(this.Seleccionado);
        }

        private void GuardarAccion()
        {
            cajasDep.CajaUsuariosModificar(this.LCUsuarios, this.lCUsuariosEliminados);
            this.Vista.DialogResult = true;
            this.Vista.Close();
        }
    }
}
