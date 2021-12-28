using System;
using System.Linq;
using System.Windows.Input;
using Intelligob.Cliente.Depositos;
using Intelligob.Escritorio.Vistas.Interfaces;
using System.Collections.ObjectModel;
using Intelligob.Cliente.Referencia;
using Intelligob.Escritorio.ModeloVista.Comandos;

namespace Intelligob.Escritorio.ModeloVista.Emisiones
{
    public class CoeficienteElementoSeleccionarVM : BaseMV<IVentanaDialogo>
    {
        readonly CoeficientesDep coeficientesDep = new CoeficientesDep();

        private ObservableCollection<CoeficienteElementoDto> lcElementos;        
        public ObservableCollection<CoeficienteElementoDto> LcElementos
        {
            get { return this.lcElementos; }
            set { this.lcElementos = value; OnPropertyChanged("LcElementos"); }
        }

        private CoeficienteElementoDto seleccionado;
        public CoeficienteElementoDto Seleccionado
        {
            get { return this.seleccionado; }
            set { this.seleccionado = value; OnPropertyChanged("Seleccionado"); }
        }

        public ICommand CmdSeleccionar
        { get; internal set; }

        public CoeficienteElementoSeleccionarVM(int pTipo) : base(new Vistas.Emisiones.SeleccionarCoeficienteElemento())
        {
            LcElementos = new ObservableCollection<CoeficienteElementoDto>(coeficientesDep.CoeficienteElementosPorTipo(pTipo));
            this.CmdSeleccionar = new ComandoDelegado((o) => SeleccionarAccion(), (o) => SeleccionarHabilitado());
            this.Vista.Owner = App.Current.MainWindow;
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
