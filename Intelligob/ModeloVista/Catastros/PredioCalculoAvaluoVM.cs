using System;
using System.Linq;
using Intelligob.Escritorio.Vistas.Interfaces;
using Intelligob.Cliente.Depositos;
using System.Windows.Input;
using Intelligob.Utiles;
using Intelligob.Escritorio.ModeloVista.Comandos;

namespace Intelligob.Escritorio.ModeloVista.Catastros
{
    class PredioCalculoAvaluoVM : BaseMV<IVentanaDialogo>
    {
        readonly CatastrosDep catastrosDep = new CatastrosDep();

        public Intelligob.Cliente.Referencia.PredioBaseDto predio;

        public double? ValorActual
        {
            get { return this.predio.ValorPropiedad; }
        }

        private double valorNuevo = 0;
        public Double ValorNuevo
        {
            get { return this.valorNuevo; }
            set { this.valorNuevo = value; OnPropertyChanged("ValorNuevo"); }
        }

        public ICommand CmdCalcular
        { get; internal set; }

        public ICommand CmdGuardar
        { get; internal set; }

        public PredioCalculoAvaluoVM(Intelligob.Cliente.Referencia.PredioBaseDto pre) : this(pre, new Intelligob.Escritorio.Vistas.Catastros.PredioCalculoAvaluo()) { }

        public PredioCalculoAvaluoVM(Intelligob.Cliente.Referencia.PredioBaseDto pre, IVentanaDialogo vista) : base(vista)
        {
            this.predio = pre;
            this.CmdCalcular = new ComandoDelegado((o) => Calcular());
            this.CmdGuardar = new ComandoDelegado((o) => Guardar());
            this.Vista.Owner = App.Current.MainWindow;
            this.Vista.ShowDialog();
        }

        private void Calcular()
        {
            if (predio.FormatoCodigo == 0)
                this.ValorNuevo = catastrosDep.PredioUrbanoCalcularAvaluo(predio.Id, false);
            else
                this.ValorNuevo = catastrosDep.PredioRuralCalcularAvaluo(predio.Id, false);
        }

        private void Guardar()
        {
            bool guardar = true;
            if (ValorNuevo <= 0)
            {
                TaskDialogInterop.TaskDialogResult res = CuadroMensajes.Preguntar("Catastro predial", "Confirme esta operacion", "El valor calulado no parace valido ¿Seguro de guardar este valor?");
                if (res.CustomButtonResult != 0)
                    guardar = false;
            }
            if (guardar)
            {
                if (predio.FormatoCodigo == 0)
                    catastrosDep.PredioUrbanoCalcularAvaluo(predio.Id, true);
                else
                    catastrosDep.PredioRuralCalcularAvaluo(predio.Id, true);
            }
            this.Vista.DialogResult = true;
            this.Vista.Close();
        }
    }
}
