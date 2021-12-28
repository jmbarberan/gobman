using Intelligob.Escritorio.Vistas.Interfaces;
using System.ComponentModel;

namespace Intelligob.Escritorio.ModeloVista
{
    public class BaseMV<TVista> : INotifyPropertyChanged where TVista : IVista
    {
        private readonly TVista vista;
        public TVista Vista
        {
            get
            {
                return this.vista;
            }
        }

        public BaseMV(TVista pVista)
        {
            this.vista = pVista;
            this.vista.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
