using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Interactivity;
using Telerik.Windows.Controls;

namespace Intelligob.Escritorio.Vistas.General
{
    public class MultiseleccionExtension : Behavior<RadGridView>
    {
        private readonly RadGridView grid = null;
        private readonly INotifyCollectionChanged selectedItems = null;

        public static readonly DependencyProperty SelectedItemsProperty
            = DependencyProperty.RegisterAttached("SelectedItems", typeof(INotifyCollectionChanged), typeof(MultiseleccionExtension),
                new PropertyMetadata(new PropertyChangedCallback(OnSelectedItemsPropertyChanged)));

        public static void SetSelectedItems(DependencyObject dependencyObject, INotifyCollectionChanged selectedItems)
        {
            dependencyObject.SetValue(SelectedItemsProperty, selectedItems);
        }

        public static INotifyCollectionChanged GetSelectedItems(DependencyObject dependencyObject)
        {
            return (INotifyCollectionChanged)dependencyObject.GetValue(SelectedItemsProperty);
        }

        private static void OnSelectedItemsPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            RadGridView grid = dependencyObject as RadGridView;
            INotifyCollectionChanged selectedItems = e.NewValue as INotifyCollectionChanged;

            if (grid != null && selectedItems != null)
            {
                MultiseleccionExtension behavior = new MultiseleccionExtension(grid, selectedItems);
                behavior.Attach();
            }
        }

        private void Attach()
        {
            if (grid != null && selectedItems != null)
            {
                Transfer(GetSelectedItems(grid) as IList, grid.SelectedItems);

                selectedItems.CollectionChanged -= ContextSelectedItems_CollectionChanged;
                selectedItems.CollectionChanged += ContextSelectedItems_CollectionChanged;

                grid.SelectedItems.CollectionChanged -= GridSelectedItems_CollectionChanged;
                grid.SelectedItems.CollectionChanged += GridSelectedItems_CollectionChanged;
            }
        }

        public MultiseleccionExtension(RadGridView grid, INotifyCollectionChanged selectedItems)
        {
            this.grid = grid;
            this.selectedItems = selectedItems;
        }

        void ContextSelectedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UnsubscribeFromEvents();

            Transfer(GetSelectedItems(grid) as IList, grid.SelectedItems);

            SubscribeToEvents();
        }

        void GridSelectedItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UnsubscribeFromEvents();

            Transfer(grid.SelectedItems, GetSelectedItems(grid) as IList);

            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            grid.SelectedItems.CollectionChanged += GridSelectedItems_CollectionChanged;

            if (GetSelectedItems(grid) != null)
            {
                GetSelectedItems(grid).CollectionChanged += ContextSelectedItems_CollectionChanged;
            }
        }

        private void UnsubscribeFromEvents()
        {
            grid.SelectedItems.CollectionChanged -= GridSelectedItems_CollectionChanged;

            if (GetSelectedItems(grid) != null)
            {
                GetSelectedItems(grid).CollectionChanged -= ContextSelectedItems_CollectionChanged;
            }
        }

        public static void Transfer(IList source, IList target)
        {
            if (source == null || target == null)
                return;

            target.Clear();

            foreach (object o in source)
            {
                target.Add(o);
            }
        }
    }
}
