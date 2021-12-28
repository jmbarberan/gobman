using Intelligob.Escritorio.ModeloVista.Comandos;
using Intelligob.Cliente.Referencia;
using Intelligob.Cliente.Depositos;
using Intelligob.Escritorio.Vistas.Emisiones;
using Intelligob.Escritorio.Vistas.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Intelligob.Escritorio.ModeloVista
{
    public class SeleccionarConceptoVM : BaseMV<IVentanaDialogo>, IDataErrorInfo
    {
        private readonly ConceptosDep clienteWeb;
        private ConceptoDto seleccionado;

        public ConceptoDto Seleccionado
        {
            get { return this.seleccionado; }
            set { this.seleccionado = value; OnPropertyChanged("Seleccionado"); }
        }

        private ObservableCollection<ConceptoDto> lConceptos;
        public ObservableCollection<ConceptoDto> LConceptos
        {
            get { return lConceptos; }
            private set
            {
                lConceptos = value;
                OnPropertyChanged("LConceptos");
            }
        }

        private string textoBusqueda;
        public string TextoBusqueda
        {
            get { return this.textoBusqueda; }
            set { this.textoBusqueda = value; OnPropertyChanged("TextoBusqueda"); }
        }

        private bool puedeConsultar;
        public ICommand CmdConsultar
        {
            get;
            internal set;
        }

        public ICommand CmdSeleccionar
        {
            get;
            internal set;
        }

        public SeleccionarConceptoVM() : this(new SeleccionarConcepto(), DepositosControl.Instance.ConceptosDepositoCrear()) { }

        public SeleccionarConceptoVM(ConceptosDep pClienteWeb) : this(new SeleccionarConcepto(), pClienteWeb) { }

        public SeleccionarConceptoVM(IVentanaDialogo pVista, ConceptosDep pClienteWeb)
            : base(pVista)
        {
            if (pClienteWeb != null)
                this.clienteWeb = pClienteWeb;
            else
                this.clienteWeb = DepositosControl.Instance.ConceptosDepositoCrear();
            this.LConceptos = new ObservableCollection<ConceptoDto>(clienteWeb.ConceptosPorEstado(0));
            this.CmdConsultar = new ComandoDelegado((o) => AccionConsultar(), (o) => HabilitaConsultar());
            this.CmdSeleccionar = new ComandoDelegado((o) => AccionSeleccionar(), (o) => HabilitaSeleccionar());
            this.MostrarVista();
        }

        private void MostrarVista()
        {
            this.Vista.Owner = App.Current.MainWindow;
            this.Vista.ShowDialog();
        }

        private bool HabilitaConsultar()
        {
            return this.puedeConsultar;
        }

        private void AccionConsultar()
        {
            // TODO Consultar conceptos
        }

        private bool HabilitaSeleccionar()
        {
            return this.seleccionado != null;
        }

        private void AccionSeleccionar()
        {
            if (this.Seleccionado.Id > 0)
            {
                this.Vista.DialogResult = true;
                this.Vista.Close();
            }
        }

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
                    error = "Digite el texto a buscar";
                }
                this.puedeConsultar = error == String.Empty;
                return error;
            }
        }
    }
}
