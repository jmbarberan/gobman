using System.Collections;
using System.Linq;
using System.Windows.Input;
using Intelligob.Escritorio.ModeloVista.Recaudaciones;
using Intelligob.Escritorio.Vistas.Interfaces;

namespace Intelligob.Escritorio.Vistas.Recaudaciones
{
    /// <summary>
    /// Lógica de interacción para RecaudacionConsulta.xaml
    /// </summary>
    public partial class CobroConsulta : IPagina

    {
        public CobroConsulta()
        {
            InitializeComponent();
        }

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {                
                if (((CobroConsultaVM)this.DataContext).ConsultaPorCodigo)
                {
                    if (((CobroConsultaVM)this.DataContext).TextoBusqueda.Trim().Length > 0)
                    {
                        ((CobroConsultaVM)this.DataContext).CmdConsultar.Execute(null);
                    }
                }
            }
        }

        public void ModificarSeleccion(int tipo)
        {
            switch (tipo)
            {
                case 0: // nada
                    {

                        ((CobroConsultaVM)this.DataContext).ListaOcupada = true;
                        this.grdCobros.SelectedItems.Clear();
                        ((CobroConsultaVM)this.DataContext).ListaOcupada = false;
                        ((CobroConsultaVM)this.DataContext).TotalSeleccionado = 0;
                        break;
                    }
                case 1: // invertir
                    {
                        ((CobroConsultaVM)this.DataContext).ListaOcupada = true;
                        if (this.grdCobros.SelectedItems.Count <= this.grdCobros.Items.Count)
                            this.RevertirTodos();
                        else
                            RevertirVarios();                        
                        ((CobroConsultaVM)this.DataContext).CmdCalcular.Execute(null);
                        ((CobroConsultaVM)this.DataContext).ListaOcupada = false;
                        break;
                    }
                default: // seleccionar todo
                    {
                        ((CobroConsultaVM)this.DataContext).ListaOcupada = true;
                        this.grdCobros.SelectAll();
                        ((CobroConsultaVM)this.DataContext).CmdCalcular.Execute(null);
                        ((CobroConsultaVM)this.DataContext).ListaOcupada = false;
                        break;
                    }
            }
        }

        private void RevertirVarios()
        {
            var oldSelectedItems = this.grdCobros.SelectedItems.ToList();
            this.grdCobros.UnselectAll();

            foreach (var item in ((IList)this.grdCobros.ItemsSource))
            {
                if (!oldSelectedItems.Contains(item))
                    this.grdCobros.SelectedItems.Add(item);
            }
        }

        private void RevertirTodos()
        {
            var oldSelectedItems = this.grdCobros.SelectedItems.ToList();
            this.grdCobros.SelectAll();

            foreach (var item in oldSelectedItems)
            {
                this.grdCobros.SelectedItems.Remove(item);
            }
        }

        private void grdCobros_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            ((CobroConsultaVM)this.DataContext).CmdCalcular.Execute(null);
        }

        private void btReagrupar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Telerik.Windows.Controls.GridView.ColumnGroupDescriptor gdConcepto = new Telerik.Windows.Controls.GridView.ColumnGroupDescriptor();
            gdConcepto.Column = this.grdCobros.Columns["ConceptoNav.Denominacion"];
            gdConcepto.SortDirection = System.ComponentModel.ListSortDirection.Ascending;
            Telerik.Windows.Data.GroupDescriptor gdCodigo = new Telerik.Windows.Data.GroupDescriptor();
            gdCodigo.Member = "Codigo";
            gdCodigo.SortDirection = System.ComponentModel.ListSortDirection.Ascending;
            this.grdCobros.IsBusy = true;
            this.grdCobros.GroupDescriptors.Clear();
            this.grdCobros.GroupDescriptors.Add(gdConcepto);
            this.grdCobros.GroupDescriptors.Add(gdCodigo);
            this.grdCobros.IsBusy = false;
        } 
    }
}
