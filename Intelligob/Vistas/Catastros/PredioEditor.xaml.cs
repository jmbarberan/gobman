using Intelligob.Cliente.Referencia;
using Intelligob.Escritorio.ModeloVista;
using Intelligob.Escritorio.Vistas.General;
using Intelligob.Escritorio.Vistas.Interfaces;
using System;

namespace Intelligob.Escritorio.Vistas
{
    /// <summary>
    /// Lógica de interacción para PredioEditor.xaml
    /// </summary>
    public partial class PredioEditor : BaseDialogoVista, IVentanaDialogo, IVentanaMetodo
    {
        public PredioEditor()
        {
            
        }

        private void tvBloques_SelectedItemChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            ((PredioEditorVM)this.DataContext).PisoSeleccionado = null;
            ((PredioEditorVM)this.DataContext).BloqueSeleccionado = null;
            Boolean b = false;
            if (e.NewValue != null)
            {
                if (e.NewValue.GetType() == typeof(PredioPisoDto))
                {
                    ((PredioEditorVM)this.DataContext).PisoSeleccionado = (PredioPisoDto)e.NewValue;
                    b = true;
                }
                else
                {
                    if (e.NewValue.GetType() == typeof(PredioBloqueDto))
                    {
                        ((PredioEditorVM)this.DataContext).BloqueSeleccionado = (PredioBloqueDto)e.NewValue;
                    }
                }
                ((PredioEditorVM)this.DataContext).EsPisoSeleccionado = b;
            }
            
        }

        private void tvConsComponentes_SelectedItemChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            ((PredioEditorVM)this.DataContext).ComponenteSeleccionado = null;
            if (e.NewValue != null)
            {
                if (e.NewValue.GetType() == typeof(ConstruccionElemento))
                {
                    ((PredioEditorVM)this.DataContext).ComponenteSeleccionado = (ConstruccionElemento)e.NewValue;
                }
            }
            
        }

        /*

        private static object SalidaOperacion(object obj)
        {
            ((DispatcherFrame)obj).Continue = false;
            return null;
        }

        private static void EsperarPrioridad(DispatcherPriority prioridad)
        {
            DispatcherFrame dFrame = new DispatcherFrame();
            DispatcherOperation dOperacion = Dispatcher.CurrentDispatcher.BeginInvoke(prioridad, new DispatcherOperationCallback(SalidaOperacion), dFrame);
            Dispatcher.PushFrame(dFrame);
            if (dOperacion.Status != DispatcherOperationStatus.Completed)
            {
                dOperacion.Abort();
            }
        }

        */


        public void Ejecutar()
        {
            InitializeComponent();
        }

        private void DataGrid_Selected(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
