using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Intelligob.Escritorio.ModeloVista.Recaudaciones
{

    public class ReversionCobrosParcialesVM : BaseMV<Intelligob.Escritorio.Vistas.Interfaces.IVentanaDialogo>
    {
        ObservableCollection<Intelligob.Cliente.Modelos.CobroPlanillaReversion> lCobros = new ObservableCollection<Cliente.Modelos.CobroPlanillaReversion>();

        public ObservableCollection<Intelligob.Cliente.Modelos.CobroPlanillaReversion> LCobros
        {
            get { return this.lCobros; }
            set { this.lCobros = value; OnPropertyChanged("LCobros"); }
        }

        private Intelligob.Cliente.Modelos.CobroPlanillaReversion seleccionado;

        public Intelligob.Cliente.Modelos.CobroPlanillaReversion Seleccionado
        {
            get { return this.seleccionado; }
            set { this.seleccionado = value; OnPropertyChanged("Seleccionado"); }
        }

        private string descripcion = String.Empty;
        public String Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; OnPropertyChanged("Descripcion"); }
        }

        public ICommand CmdSeleccionar
        { get; internal set; }

        private bool PuedeSeleccionar()
        {
            return this.Seleccionado != null;
        }

        private void Seleccionar()
        {
            this.Vista.DialogResult = true;
            this.Vista.Close();
        }

        public ReversionCobrosParcialesVM(IEnumerable<Intelligob.Cliente.Modelos.CobroPlanillaReversion> cobros)
            : this(cobros, new Intelligob.Escritorio.Vistas.Recaudaciones.ReversionCobrosParciales()) {}

        public ReversionCobrosParcialesVM(IEnumerable<Intelligob.Cliente.Modelos.CobroPlanillaReversion> cobros, Intelligob.Escritorio.Vistas.Interfaces.IVentanaDialogo vista) : base(vista)
        {
            this.LCobros = new ObservableCollection<Cliente.Modelos.CobroPlanillaReversion>(cobros);
            this.CmdSeleccionar = new Comandos.ComandoDelegado((o) => Seleccionar(), (o) => PuedeSeleccionar());
            this.Vista.ShowDialog();
        }
    }
}
