
namespace Intelligob.Escritorio.Vistas.Interfaces
{
    public interface IVentanaDialogo: IVista
    {
        void Show();
        void Close();
        bool IsVisible { get; }
        bool? ShowDialog();
        bool? DialogResult { get; set; }
        System.Windows.Window Owner { get; set; }
    }
}
