using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Intelligob.Escritorio.ModeloVista.Catastros
{
    public class PatenteSeleccionarVM : BaseMV<Escritorio.Vistas.Interfaces.IVentanaDialogo>
    {
        private readonly Cliente.Depositos.CatastrosDep catastrosDep = new Cliente.Depositos.CatastrosDep();

        private ObservableCollection<Cliente.Referencia.PatenteDto> lpatentes = new ObservableCollection<Cliente.Referencia.PatenteDto>();
        public ObservableCollection<Cliente.Referencia.PatenteDto> LPatentes
        {
            get { return lpatentes; }
            set { this.lpatentes = value; OnPropertyChanged("LPatentes"); }
        }

        private Cliente.Referencia.PatenteDto seleccionado;
        public Cliente.Referencia.PatenteDto Seleccionado
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

        public PatenteSeleccionarVM(System.Collections.Generic.IEnumerable<Cliente.Referencia.PatenteDto> pListaPatentes) : base(new Escritorio.Vistas.Catastros.PatenteSeleccion()) 
        {
            this.LPatentes = new ObservableCollection<Cliente.Referencia.PatenteDto>(pListaPatentes);
            this.CrearComandos();
            this.MostrarVista();
        }

        public PatenteSeleccionarVM() : base(new Escritorio.Vistas.Catastros.PatenteSeleccion())
        {
            this.CrearComandos();
            this.MostrarVista();
        }

        private void MostrarVista()
        {
            this.Vista.Owner = App.Current.MainWindow;
            this.Vista.ShowDialog();
        }

        private void CrearComandos()
        {
            this.CmdBuscar = new Comandos.ComandoDelegado((o) => AccionBuscar(), (o) => HabilitaBuscar());
            this.CmdContribuyente = new Comandos.ComandoDelegado((o) => AccionContribuyente(), (o) => HabilitaContribuyente());
            this.CmdSeleccionar = new Comandos.ComandoDelegado((o) => AccionSeleccionar(), (o) => HabilitaSeleccionar());
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
                    LPatentes = new ObservableCollection<Cliente.Referencia.PatenteDto>(catastrosDep.PatentePorContribuyente(contribuyenteBusqueda.Id, 0));
                }
            }
            else
            {
                LPatentes = new ObservableCollection<Cliente.Referencia.PatenteDto>(catastrosDep.PatentePorCodigo(this.TextoBusqueda, 0, Utiles.TipoBusquedaTexto.tbComenzando));
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
