using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Intelligob.Escritorio.ModeloVista.Catastros
{
    public class PredioSeleccionarVM : BaseMV<Escritorio.Vistas.Interfaces.IVentanaDialogo>
    {
        private readonly Cliente.Depositos.CatastrosDep catastrosDep = new Cliente.Depositos.CatastrosDep();
        private readonly int tipoPredio = 0;

        private ObservableCollection<Cliente.Referencia.PredioBaseDto> lPredios;
        public ObservableCollection<Cliente.Referencia.PredioBaseDto> LPredios
        {
            get { return this.lPredios; }
            set { this.lPredios = value; OnPropertyChanged("LPredios"); }
        }

        private Cliente.Referencia.PredioBaseDto seleccionado;        
        public Cliente.Referencia.PredioBaseDto Seleccionado
        {
            get { return this.seleccionado; }
            set
            {
                this.seleccionado = value;
                OnPropertyChanged("Seleccionado");
            }
        }

        private Cliente.Referencia.ContribuyenteDto Contribuyente;

        private bool consultarContribuyente = false;
        public bool ConsultarPorContribuyente
        {
            get { return this.consultarContribuyente; }
            set { this.consultarContribuyente = value; OnPropertyChanged("ConsultarPorContribuyente"); }
        }

        private string textoBusqueda = "";
        public string TextoBusqueda
        {
            get { return this.textoBusqueda; }
            set { this.textoBusqueda = value; OnPropertyChanged("TextoBusqueda"); }
        }

        public ICommand CmdContribuyente
        { get; internal set; }

        public ICommand CmdBuscar
        { get; internal set; }

        public ICommand CmdSeleccionar
        { get; internal set; }        

        public PredioSeleccionarVM(int pTipoPredio) : base(new Escritorio.Vistas.Catastros.PredioSeleccionar())
        {
            this.tipoPredio = pTipoPredio;
            CrearComandos();
            this.MostrarVista();
        }

        public PredioSeleccionarVM(IEnumerable<Cliente.Referencia.PredioBaseDto> pPredios, int pPredioTipo) : base(new Escritorio.Vistas.Catastros.PredioSeleccionar())
        {
            this.tipoPredio = pPredioTipo;
            this.LPredios = new ObservableCollection<Cliente.Referencia.PredioBaseDto>(pPredios);
            this.ConsultarPorContribuyente = false;            
            CrearComandos();
            this.MostrarVista();
        }

        public PredioSeleccionarVM(Cliente.Referencia.ContribuyenteDto pContrib, IEnumerable<Cliente.Referencia.PredioBaseDto> pPredios) : base(new Escritorio.Vistas.Catastros.PredioSeleccionar())
        {
            this.tipoPredio = 9;
            this.LPredios = new ObservableCollection<Cliente.Referencia.PredioBaseDto>(pPredios);            
            this.ConsultarPorContribuyente = true;
            this.Contribuyente = pContrib;
            if (pContrib != null)
                this.TextoBusqueda = pContrib.Nombres;            
            CrearComandos();
            this.MostrarVista();
        }

        private void MostrarVista()
        {
            this.Vista.Owner = App.Current.MainWindow;
            this.Vista.ShowDialog();
        }

        private void CrearComandos()
        {
            this.CmdContribuyente = new Comandos.ComandoDelegado((o) => ContribuyenteAccion(), (o) => ContribuyenteHabilita());
            this.CmdBuscar = new Comandos.ComandoDelegado((o) => BuscarAccion(), (o) => BuscarHabilita());
            this.CmdSeleccionar = new Comandos.ComandoDelegado((o) => SeleccionarAccion(), (o) => SeleccionarHabilita());
        }

        private bool ContribuyenteHabilita()
        {
            return this.ConsultarPorContribuyente;
        }

        private bool BuscarHabilita()
        {
            bool res = false;
            if (ConsultarPorContribuyente)
            {
                if (Contribuyente != null && Contribuyente.Id > 0)
                    res = true;
            }
            else
            {
                if (TextoBusqueda.Length > 0)
                    res = true;
            }                
            return res;
        }

        private bool SeleccionarHabilita()
        {
            return this.Seleccionado != null && this.Seleccionado.Id > 0;
        }
    
        private void ContribuyenteAccion()
        {
            SeleccionarContribuyenteVM sc = new SeleccionarContribuyenteVM();
            if (sc.Vista.DialogResult == true)
            {
                this.Contribuyente = sc.Seleccionado;
                this.TextoBusqueda = sc.Seleccionado.Nombres;
                this.BuscarAccion();
            }
        }

        private void BuscarAccion()
        {
            if (ConsultarPorContribuyente)
            {
                if (Contribuyente != null && Contribuyente.Id > 0)
                {
                    LPredios = new ObservableCollection<Cliente.Referencia.PredioBaseDto>(catastrosDep.PrediosPorContribuyente(Contribuyente.Id, 0, this.tipoPredio, false));
                }
            }
            else
            {                
                LPredios = new ObservableCollection<Cliente.Referencia.PredioBaseDto>(catastrosDep.PrediosPorCodigo(TextoBusqueda, 0, Utiles.TipoBusquedaTexto.tbComenzando, this.tipoPredio, false));
            }
        }

        private void SeleccionarAccion()
        {            
            this.Vista.DialogResult = true;
            this.Vista.Close();
        }

    }
}
