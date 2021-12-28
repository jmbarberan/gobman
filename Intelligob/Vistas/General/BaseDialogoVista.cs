using System.Windows;

namespace Intelligob.Escritorio.Vistas.General
{
    public class BaseDialogoVista : Window
    {
        public BaseDialogoVista()
        {
            this.ShowInTaskbar = false;
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            this.ShowInTaskbar = false;
            this.Owner = App.Current.MainWindow;
        }
    }
}
