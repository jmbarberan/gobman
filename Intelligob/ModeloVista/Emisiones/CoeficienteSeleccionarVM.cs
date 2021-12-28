using Intelligob.Cliente.Depositos;
using Intelligob.Cliente.Referencia;
using Intelligob.Escritorio.ModeloVista.Comandos;
using Intelligob.Escritorio.Vistas.Emisiones;
using Intelligob.Escritorio.Vistas.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Intelligob.Escritorio.ModeloVista.Emisiones
{
    public class CoeficienteSeleccionarVM : BaseMV<IVentanaDialogo>
    {
        readonly CoeficientesDep coeficientesDep = new CoeficientesDep();

        private ObservableCollection<CoeficienteDto> lCoeficientes;
        public ObservableCollection<CoeficienteDto> LCoeficientes
        {
            get { return this.lCoeficientes; }
            set { this.lCoeficientes = value; OnPropertyChanged("LCoeficientes"); }
        }

        private CoeficienteDto seleccionado;
        public CoeficienteDto Seleccionado
        {
            get { return this.seleccionado; }
            set { this.seleccionado = value; OnPropertyChanged("Seleccionado"); }
        }

        public ICommand CmdSeleccionar
        {
            get;
            internal set;
        }

        public CoeficienteSeleccionarVM() : base(new SeleccionarCoeficiente()) 
        {
            this.LCoeficientes = new ObservableCollection<CoeficienteDto>(coeficientesDep.CoeficientesPorEstado(0));
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
