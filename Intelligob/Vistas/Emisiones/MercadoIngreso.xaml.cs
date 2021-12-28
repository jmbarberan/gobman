using Intelligob.Escritorio.ModeloVista.Emisiones;
using Intelligob.Escritorio.ModeloVista.General;
using Intelligob.Escritorio.Vistas.General;
using Intelligob.Escritorio.Vistas.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Intelligob.Escritorio.Vistas.Emisiones
{
    /// <summary>
    /// Interaction logic for MercadoIngreso.xaml
    /// </summary>
    public partial class MercadoIngreso : IVentanaDialogo
    {
        public MercadoIngreso()
        {
            InitializeComponent();
        }

        private void DataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var uie = e.OriginalSource as UIElement;

            if (e.Key == Key.Enter)
            {
                //dgTickets.CurrentCell = new DataGridCellInfo(dgTickets.Items[0], dgTickets.Columns[3]);
                var cel = dgTickets.CurrentCell; 
                if (cel.Column.DisplayIndex == 0)
                {
                    // Cargar Puesto                    
                    e.Handled = true;
                    uie.MoveFocus(
                    new TraversalRequest(
                    FocusNavigationDirection.Next));
                }
                else
                {
                    if (cel.Column.DisplayIndex == 3)
                    {
                        
                    }
                    else
                    {
                        e.Handled = true;
                        uie.MoveFocus(
                        new TraversalRequest(
                        FocusNavigationDirection.Next));
                    }
                }
                
                
            }
        }

        private static DataGridCell GetCell(DataGrid dataGrid, DataGridRow rowContainer, int column)
        {
            if (rowContainer != null)
            {
                DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(rowContainer);
                if (presenter != null)
                    return presenter.ItemContainerGenerator.ContainerFromIndex(column) as DataGridCell;
            }
            return null;
        }

        private static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                    return (T)child;
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        private void dgTickets_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            DataGridRow row = dgTickets.ItemContainerGenerator.ContainerFromItem(CollectionView.NewItemPlaceholder) as DataGridRow;
            if (row != null)
            {
                dgTickets.SelectedItem = row.DataContext;
                DataGridCell cell = GetCell(dgTickets, row, 0);
                if (cell != null)
                    dgTickets.CurrentCell = new DataGridCellInfo(cell);
            }
        }

        private void dgTickets_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.DisplayIndex == 0)
            {
                var editedTextbox = e.EditingElement as TextBox;
                var o = 0;
                if (editedTextbox != null)
                {
                    try
                    {
                        o = Convert.ToInt32(editedTextbox.Text);
                        ((MercadoIngresoVM)DataContext).CargarLocal(o);
                    }
                    catch (Exception ex)
                    {
                        o = 0;
                    }
                }
            }   
        }
    }
}
