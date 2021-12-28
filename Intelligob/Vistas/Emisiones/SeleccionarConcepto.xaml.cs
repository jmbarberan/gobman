using Intelligob.Escritorio.ModeloVista;
using Intelligob.Escritorio.Vistas.General;
using Intelligob.Escritorio.Vistas.Interfaces;
using System;
using System.Windows.Input;

namespace Intelligob.Escritorio.Vistas.Emisiones
{
    /// <summary>
    /// Lógica de interacción para SeleccionarConcepto.xaml
    /// </summary>
    public partial class SeleccionarConcepto : BaseDialogoVista, IVentanaDialogo
    {
        public SeleccionarConcepto()
        {
            InitializeComponent();
        }

        private void txBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (String.IsNullOrEmpty(this.txBusqueda.Text.Trim()))
                    ((SeleccionarConceptoVM)this.DataContext).CmdConsultar.Execute(null);
            }
        }
    }
}
