using Intelligob.Escritorio.ModeloVista.Comandos;
using Intelligob.Cliente.Referencia;
using Intelligob.Cliente.Depositos;
using Intelligob.Escritorio.Vistas.General;
using Intelligob.Escritorio.Vistas.Interfaces;
using Intelligob.Utiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Intelligob.Escritorio.ModeloVista
{
    class SeleccionarContribuyenteVM : BaseMV<IVentanaDialogo>, IDataErrorInfo
    {
        private readonly ContribuyentesDep clienteWeb;
        private ContribuyenteDto seleccionado;

        private bool textoActivo = true;
        public bool TextoActivo
        {
            get { return this.textoActivo; }
            set { this.textoActivo = value; OnPropertyChanged("TextoActivo"); }
        }

        public ContribuyenteDto Seleccionado
        {
            get { return this.seleccionado; }
            set { this.seleccionado = value; OnPropertyChanged("Seleccionado"); }
        }

        private ObservableCollection<ContribuyenteDto> lContribuyentes;
        public ObservableCollection<ContribuyenteDto> LContribuyentes
        {
            get { return lContribuyentes; }
            private set
            {
                lContribuyentes = value;
                OnPropertyChanged("LContribuyentes");
            }
        }

        public List<KeyValuePair<string, TipoBusquedaTexto>> ListaTipoBusqueda
        {
            get { return EnumTipoBusquedaTexto.TraerLista(); }
        }
        private TipoBusquedaTexto tipoBusqueda = TipoBusquedaTexto.tbComenzando;
        public TipoBusquedaTexto TipoBusqueda
        {
            get { return tipoBusqueda; }
            set
            {
                tipoBusqueda = value;
                OnPropertyChanged("TipoBusqueda");
            }
        }

        private string textoBusqueda;
        public string TextoBusqueda
        {
            get { return this.textoBusqueda; }
            set
            {
                textoBusqueda = value;
                OnPropertyChanged("TextoBusqueda");
            }
        }

        private bool mostrarEliminados = false;
        public bool MostrarEliminados
        {
            get { return mostrarEliminados; }
            set { mostrarEliminados = value; OnPropertyChanged("MostrarEliminados"); }
        }

        private bool buscarPorCedula = false;
        public bool BuscarPorCedula
        {
            get { return buscarPorCedula; }
            set { buscarPorCedula = value; OnPropertyChanged("BuscarPorCedula"); }
        }

        public ICommand CmdSeleccionar
        {
            get;
            internal set;
        }

        private bool puedeConsultar;
        public ICommand CmdConsultar
        {
            get;
            internal set;
        }

        public SeleccionarContribuyenteVM() : this(new SeleccionarContribuyente(), DepositosControl.Instance.ContribuyentesDepositoCrear()) { }

        public SeleccionarContribuyenteVM(List<int> pListaConIds)
            : base(new SeleccionarContribuyente())
        {
            if (this.clienteWeb == null)
            {
                this.clienteWeb = DepositosControl.Instance.ContribuyentesDepositoCrear();
            }
            List<ContribuyenteDto> pListaContribs = new List<ContribuyenteDto>();
            foreach(int i in pListaConIds)
            {
                ContribuyenteDto c = this.clienteWeb.ContribuyentePorId(i);
                pListaContribs.Add(c);
            }            
            LContribuyentes = new ObservableCollection<ContribuyenteDto>(pListaContribs);
            this.TextoActivo = false;
            this.clienteWeb = DepositosControl.Instance.ContribuyentesDepositoCrear();
            this.CmdConsultar = new ComandoDelegado((o) => AccionConsultar(), (o) => HabilitaConsultar());
            this.CmdSeleccionar = new ComandoDelegado((o) => AccionSeleccionar(), (o) => HabilitaSeleccionar());
            this.MostrarVista();
        }

        public SeleccionarContribuyenteVM(IEnumerable<ContribuyenteDto> pListaContribs) 
            : base(new SeleccionarContribuyente()) 
        {
            if (this.clienteWeb == null)
            {
                this.clienteWeb = DepositosControl.Instance.ContribuyentesDepositoCrear();
            }            
            LContribuyentes = new ObservableCollection<ContribuyenteDto>(pListaContribs);
            this.TextoActivo = false;
            this.clienteWeb = DepositosControl.Instance.ContribuyentesDepositoCrear();
            this.CmdConsultar = new ComandoDelegado((o) => AccionConsultar(), (o) => HabilitaConsultar());
            this.CmdSeleccionar = new ComandoDelegado((o) => AccionSeleccionar(), (o) => HabilitaSeleccionar());
            this.MostrarVista();
        }

        public SeleccionarContribuyenteVM(IVentanaDialogo pVista, ContribuyentesDep pClienteWeb) : base(pVista)
        {
            if (pClienteWeb != null)
            {
                this.clienteWeb = pClienteWeb;
            }
            else
            {
                this.clienteWeb = DepositosControl.Instance.ContribuyentesDepositoCrear();
            }
            this.CmdConsultar = new ComandoDelegado((o) => AccionConsultar(), (o) => HabilitaConsultar());
            this.CmdSeleccionar = new ComandoDelegado((o) => AccionSeleccionar(), (o) => HabilitaSeleccionar());
            this.MostrarVista();
        }

        private void MostrarVista()
        {
            this.Vista.Owner = App.Current.MainWindow;
            this.Vista.ShowDialog();
        }

        #region Acciones y habilitadores de comandos
        
        private bool HabilitaConsultar()
        {
            return this.puedeConsultar;
        }

        private void AccionConsultar()
        {
            int mEstado = 0;
            if (this.MostrarEliminados == true)
            {
                mEstado = 9;
            }
            if (this.BuscarPorCedula)
                LContribuyentes = new ObservableCollection<ContribuyenteDto>(clienteWeb.ContribuyentesPorCedulaEstado(this.TextoBusqueda, mEstado));
            else
                LContribuyentes = new ObservableCollection<ContribuyenteDto>(clienteWeb.ContribuyentesPorNombre(this.TextoBusqueda, TipoBusqueda, mEstado));
        }

        private bool HabilitaSeleccionar()
        {
            return this.Seleccionado != null;
        }

        private void AccionSeleccionar()
        {
            if (this.Seleccionado.Id > 0)
            {
                this.Vista.DialogResult = true;
                this.Vista.Close();
            }
        }

        #endregion

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get 
            {
                string error = String.Empty;
                if (columnName == "TextoBusqueda")
                {
                    if (String.IsNullOrWhiteSpace(TextoBusqueda))
                        error = "Digite el nombre a buscar o una aproximacion";
                }
                puedeConsultar = error == String.Empty;
                return error;
            }
        }
    }
}
