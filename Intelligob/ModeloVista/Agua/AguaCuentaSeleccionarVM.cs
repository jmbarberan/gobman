using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Intelligob.Escritorio.ModeloVista.Agua
{
    public class AguaCuentaSeleccionarVM : BaseMV<Escritorio.Vistas.Interfaces.IVentanaDialogo>
    {
        private readonly Cliente.Depositos.AguaDep aguaDep = new Cliente.Depositos.AguaDep();

        private ObservableCollection<Cliente.Referencia.AguaPotableDto> lcuentas = new ObservableCollection<Cliente.Referencia.AguaPotableDto>();
        public ObservableCollection<Cliente.Referencia.AguaPotableDto> LCuentas
        {
            get { return lcuentas; }
            set { this.lcuentas = value; OnPropertyChanged("LCuentas"); }
        }

        private Cliente.Referencia.AguaPotableDto seleccionado;
        public Cliente.Referencia.AguaPotableDto Seleccionado
        {
            get { return this.seleccionado; }
            set { this.seleccionado = value; OnPropertyChanged("Seleccionado"); }
        }

        private bool buscarPorContribuyente = false;
        public bool BuscarPorContribuyente
        {
            get { return this.buscarPorContribuyente; }
            set { this.buscarPorContribuyente = value; OnPropertyChanged("BuscarPorContribuyente"); OnPropertyChanged("TextoBusqueda"); }
        }

        private string textoBusqueda = String.Empty;
        public string TextoBusqueda
        {
            get { return this.textoBusqueda; }
            set { this.textoBusqueda = value; OnPropertyChanged("TextoBusqueda"); }
        }

        public ICommand CmdBuscar
        { get; internal set; }

        public ICommand CmdContribuyente
        { get; internal set; }

        public ICommand CmdSeleccionar
        { get; internal set; }

        private Cliente.Referencia.ContribuyenteDto contribuyenteBusqueda = null;

        public AguaCuentaSeleccionarVM(System.Collections.Generic.IEnumerable<Cliente.Referencia.AguaPotableDto> pListaCuentas) : base(new Escritorio.Vistas.Agua.AguaCuentaSeleccionar())
        {
            this.LCuentas = new ObservableCollection<Cliente.Referencia.AguaPotableDto>(pListaCuentas);
            this.CrearComandos();
            this.MostrarVista();
        }

        public AguaCuentaSeleccionarVM() : base(new Escritorio.Vistas.Agua.AguaCuentaSeleccionar())
        {
            this.CrearComandos();
            this.MostrarVista();
        }

        private void CrearComandos()
        {
            this.CmdBuscar = new Comandos.ComandoDelegado((o) => AccionBuscar(), (o) => HabilitaBuscar());
            this.CmdContribuyente = new Comandos.ComandoDelegado((o) => AccionContribuyente(), (o) => HabilitaContribuyente());
            this.CmdSeleccionar = new Comandos.ComandoDelegado((o) => AccionSeleccionar(), (o) => HabilitaSeleccionar());
        }

        private void MostrarVista()
        {
            this.Vista.Owner = App.Current.MainWindow;
            this.Vista.ShowDialog();
        }

        private bool HabilitaBuscar()
        {
            bool res = false;
            if (BuscarPorContribuyente)
            {
                if (contribuyenteBusqueda != null && contribuyenteBusqueda.Id > 0)
                    res = true;
            }
            else
            {
                if (TextoBusqueda.Length > 0)
                    res = true;
            }
            return res;
        }

        private bool HabilitaContribuyente()
        {
            return this.BuscarPorContribuyente;
        }

        private bool HabilitaSeleccionar()
        {
            return this.Seleccionado != null;
        }

        private void AccionBuscar()
        {
            if (BuscarPorContribuyente)
            {
                if (contribuyenteBusqueda != null && contribuyenteBusqueda.Id > 0)
                {
                    LCuentas = new ObservableCollection<Cliente.Referencia.AguaPotableDto>(aguaDep.CuentasPorContribuyente(contribuyenteBusqueda.Id, 0));
                }
            }
            else
            {
                LCuentas = new ObservableCollection<Cliente.Referencia.AguaPotableDto>(aguaDep.CuentasPorCodigo(this.TextoBusqueda, 0, 1));
            }
        }

        private void AccionSeleccionar()
        {
            this.Vista.DialogResult = true;
            this.Vista.Close();
        }

        private void AccionContribuyente()
        {
            SeleccionarContribuyenteVM sc = new SeleccionarContribuyenteVM();
            if (sc.Vista.DialogResult == true)
            {
                this.contribuyenteBusqueda = sc.Seleccionado;
                this.TextoBusqueda = sc.Seleccionado.Nombres;
                this.AccionBuscar();
            }
        }
    }
}
