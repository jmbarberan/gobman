using Intelligob.Escritorio.Vistas.Interfaces;
using System.Windows;
using System.Windows.Navigation;

namespace Intelligob.Escritorio.Vistas.General
{
    public sealed class Navegador
    {
        private static readonly Navegador instance = new Navegador();
        private Navegador() { }
        public static NavigationService NavigationService { get; set; }
        
        public static bool Cancel()
        {
            bool res = true;
            MessageBoxResult result = MessageBox.Show("¿Seguro de cancelar?", "Cancelar", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
                res = false;
            return res;
        }
    }

    public sealed class NavegadorFunciones
    {
        private static readonly NavegadorFunciones instance = new NavegadorFunciones();
        private NavegadorFunciones() { }
        public static NavigationService NavigationService { get; set; }
        public static IPagina PaginaInicial { get; set; }

        public static bool Cancel()
        {
            bool res = true;
            MessageBoxResult result = MessageBox.Show("¿Seguro de cancelar?", "Cancelar", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
                res = false;
            return res;
        }
    }
}
