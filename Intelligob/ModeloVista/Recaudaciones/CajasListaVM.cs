using System.Collections.ObjectModel;
using Intelligob.Cliente;

namespace Intelligob.Escritorio.ModeloVista.Recaudaciones
{
    public class CajasListaVM : BaseMV<Vistas.Interfaces.IPagina>
    {
        private readonly Cliente.Depositos.CajasDep cajasDep = new Cliente.Depositos.CajasDep();
        private readonly Cliente.Depositos.SeguridadDep seguridadDep = new Cliente.Depositos.SeguridadDep();

        private string barraEstado = "Listo";
        public string BarraEstado
        {
            get { return this.barraEstado; }
            set { this.barraEstado = value; OnPropertyChanged("BarraEstado"); }
        }

        private ObservableCollection<Cliente.Referencia.CajaDto> lCajas;
        public ObservableCollection<Cliente.Referencia.CajaDto> LCajas
        {
            get { return this.lCajas; }
            set { this.lCajas = value; OnPropertyChanged("LCajas"); }
        }

        private Cliente.Referencia.CajaDto seleccionado;
        public Cliente.Referencia.CajaDto Seleccionado
        {
            get { return this.seleccionado; }
            set 
            {
                this.seleccionado = value;
                if (this.seleccionado != null)
                {
                    this.BarraEstado = string.Format("{0} Seleccionado", this.Seleccionado.Descripcion);
                }
                OnPropertyChanged("Seleccionado"); 
            }
        }

        private int filtro = 0;
        public int Filtro
        {
            get { return this.filtro; }
            set { this.filtro = value; OnPropertyChanged("Filtro"); }
        }

        private bool nuevo;
        private bool modificar;
        private bool eliminar;
        private bool restaurar;
        /*private bool saldo;
        private bool cuadre;*/
        private bool usuario;
        
        public System.Windows.Input.ICommand CmdBuscar { get; internal set; }
        public System.Windows.Input.ICommand CmdNuevo { get; internal set; }
        public System.Windows.Input.ICommand CmdModificar { get; internal set; }
        public System.Windows.Input.ICommand CmdEliminar { get; internal set; }
        public System.Windows.Input.ICommand CmdRestaurar { get; internal set; }
        //public System.Windows.Input.ICommand CmdSaldo { get; internal set; }
        //public System.Windows.Input.ICommand CmdCuadre { get; internal set; }
        public System.Windows.Input.ICommand CmdUsuario { get; internal set; }
        public System.Windows.Input.ICommand CmdRegresar { get; internal set; }
        public System.Windows.Input.ICommand CmdAdelantar { get; internal set; }

        
        public CajasListaVM() : base(new Vistas.Recaudaciones.CajasLista())
        {
            LCajas = new ObservableCollection<Cliente.Referencia.CajaDto>(cajasDep.CajasPorEstado(0));
            this.ProcesarPrivilegios();
            this.CrearComandos();
        }

        private void CrearComandos()
        {
            this.CmdBuscar = new Comandos.ComandoDelegado((o) => this.BuscarAccion());
            this.CmdNuevo = new Comandos.ComandoDelegado((o) => this.NuevoAccion(), (o) => this.NuevoHabilita());
            this.CmdModificar = new Comandos.ComandoDelegado((o) => this.ModificarAccion(), (o) => this.ModificarHabilita());
            this.CmdEliminar = new Comandos.ComandoDelegado((o) => this.EliminarAccion(), (o) => this.EliminarHabilita());
            this.CmdRestaurar = new Comandos.ComandoDelegado((o) => this.RestaurarAccion(), (o) => this.RestaurarHabilita());
            //this.CmdSaldo = new Comandos.ComandoDelegado((o) => this.SaldoAccion(), (o) => this.SaldoHabilita());
            //this.CmdCuadre = new Comandos.ComandoDelegado((o) => this.CuadreAccion(), (o) => this.CuadreHabilita());
            this.CmdUsuario = new Comandos.ComandoDelegado((o) => this.UsuarioAccion(), (o) => this.UsuarioHabilita());
            this.CmdRegresar = new Comandos.ComandoDelegado((r) => this.RegresarAccion(), (r) => this.RegresarHabilita());
            this.CmdAdelantar = new Comandos.ComandoDelegado((a) => this.AdelantarAccion(), (a) => this.AdelantarHabilita());
        }

        private void ProcesarPrivilegios()
        {
            this.nuevo = false;
            this.modificar = false;
            this.eliminar = false;
            this.restaurar = false;
            /*this.saldo = false;
            this.cuadre = false;*/
            this.usuario = false;

            string c = "";
            if (!SesionUtiles.Instance.EsDesarrollador)
            {
                Cliente.Referencia.PrivilegioDto p = seguridadDep.PrivilegiosFuncionPorUsuario(38, SesionUtiles.Instance.UsuarioActivo.Id);
                if (p != null && p.Comandos != null)
                    c = p.Comandos;
            }

            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("1"))
                this.nuevo = true;
            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("2"))
                this.modificar = true;
            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("3"))
                this.eliminar = true;
            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("4"))
                this.restaurar = true;
            /*if (SesionUtiles.Instance.EsDesarrollador || c.Contains("5"))
                this.saldo = true;
            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("6"))
                this.cuadre = true;*/
            if (SesionUtiles.Instance.EsDesarrollador || c.Contains("5"))
                this.usuario = true;
        }

        #region Habilitadores de comandos

        private bool NuevoHabilita()
        {
            return this.nuevo;
        }

        private bool ModificarHabilita()
        {
            return this.modificar && this.Seleccionado != null && this.Seleccionado.Estado == 0;
        }

        private bool EliminarHabilita()
        {
            return this.eliminar && this.Seleccionado != null && this.Seleccionado.Estado == 0;
        }

        private bool RestaurarHabilita()
        {
            return this.restaurar && this.Seleccionado != null && this.Seleccionado.Estado == 2;
        }

        /*private bool SaldoHabilita()
        {
            return this.saldo && this.Seleccionado != null && this.Seleccionado.Estado == 0;
        }

        private bool CuadreHabilita()
        {
            return this.cuadre && this.Seleccionado != null && this.Seleccionado.Estado == 0;
        }*/

        private bool UsuarioHabilita()
        {
            return this.usuario && this.Seleccionado != null && this.Seleccionado.Estado == 0;
        }

        private bool RegresarHabilita()
        {
            return Vistas.General.Navegador.NavigationService.CanGoBack;
        }

        private bool AdelantarHabilita()
        {
            return Vistas.General.Navegador.NavigationService.CanGoForward;
        }

        #endregion

        #region Acciones de comandos

        private void BuscarAccion()
        {
            int f = this.Filtro;
            if (this.Filtro == 1)
                f = 9;
            this.LCajas = new ObservableCollection<Cliente.Referencia.CajaDto>(cajasDep.CajasPorEstado(f));
        }

        private void NuevoAccion()
        {
            CajaEditorVM ce = new CajaEditorVM();
            if (ce.Vista.DialogResult == true)
            {
                this.BuscarAccion();
                this.BarraEstado = string.Format("Se creo {0}", ce.Descripcion);
            }
        }

        private void ModificarAccion()
        {
            Cliente.Referencia.CajaDto c = cajasDep.CajaPorId(this.Seleccionado.Id);            
            CajaEditorVM ce = new CajaEditorVM(c);
            if (ce.Vista.DialogResult == true)
            {
                this.BarraEstado = string.Format("{0} Ha sido modificada", ce.Descripcion);
                this.BuscarAccion();
            }
        }

        private void EliminarAccion()
        {
            TaskDialogInterop.TaskDialogResult res = Utiles.CuadroMensajes.Preguntar("Cajas", "Confirme esta operacion", "Se eliminara esta caja y no se podra utilizar en los procesos del sistema ¿Seguro de continuar?");
            if (res.CustomButtonResult == 0)
            {
                this.Seleccionado.Estado = 2;
                this.cajasDep.ModificarCaja(this.Seleccionado);
                this.BuscarAccion();
            }
        }

        private void RestaurarAccion()
        {
            TaskDialogInterop.TaskDialogResult res = Utiles.CuadroMensajes.Preguntar("Cajas", "Confirme esta operacion", "Se restaurara esta caja y estara habilitada para los procesos del sistema ¿Seguro de continuar?");
            if (res.CustomButtonResult == 0)
            {
                this.Seleccionado.Estado = 0;
                this.cajasDep.ModificarCaja(this.Seleccionado);
                this.BuscarAccion();
            }
        }

        /*private void SaldoAccion()
        {
            CajaSaldoVM cs = new CajaSaldoVM(null, this.Seleccionado);
        }

        private void CuadreAccion()
        {
            // TODO Implementar cuadre de caja
        }*/

        private void UsuarioAccion()
        {
            CajaUsuariosVM cu = new CajaUsuariosVM(this.Seleccionado.Id);
        }

        private void RegresarAccion()
        {
            Vistas.General.Navegador.NavigationService.GoBack();
        }

        private void AdelantarAccion()
        {
            Vistas.General.Navegador.NavigationService.GoForward();
        }

        #endregion

    }
}
