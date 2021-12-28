using System.Collections.ObjectModel;
using Intelligob.Escritorio.Vistas.Catastros;
using Intelligob.Escritorio.Vistas.Interfaces;
using System;
using System.Linq;
using Intelligob.Cliente.Referencia;
using System.Windows.Input;
using Intelligob.Escritorio.ModeloVista.Comandos;
using Intelligob.Cliente.Depositos;

namespace Intelligob.Escritorio.ModeloVista.Catastros
{
    public class PatenteComponenteSeleccionarVM : BaseMV<IVentanaDialogo>
    {
        private readonly ConceptosDep conceptosDep = new ConceptosDep();
        private ObservableCollection<ConceptoDto> lConceptos;
        public ObservableCollection<ConceptoDto> LConceptos
        {
            get { return this.lConceptos; }
            set { this.lConceptos = value; OnPropertyChanged("LConceptos"); }
        }

        private ConceptoDto seleccionado;
        public ConceptoDto Seleccionado
        { get { return this.seleccionado; } set { this.seleccionado = value; OnPropertyChanged("Seleccionado"); } }

        public ICommand CmdSeleccionar
        {
            get;
            internal set;
        }

        public PatenteComponenteSeleccionarVM() : base(new PatenteComponenteSeleccionar())
        {
            this.LConceptos = new ObservableCollection<ConceptoDto>(conceptosDep.ConceptosPorPatentes());
            this.CmdSeleccionar = new ComandoDelegado((o) => SeleccionarAccion(), (o) => SeleccionarHabilitado());
            this.Vista.ShowDialog();
        }

        private bool SeleccionarHabilitado()
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
