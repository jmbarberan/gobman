using System;
using System.ComponentModel;
using System.Windows;

namespace Intelligob.Escritorio.Vistas.Seguridad
{
    public class ComandosPrivilegioAuxiliar : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private System.Windows.Visibility mVisibilidad;
        private Boolean mSeleccionado;
        private string mEtiqueta;
        private int mIndice;

        public ComandosPrivilegioAuxiliar()
        {
            mVisibilidad = Visibility.Hidden;
            mSeleccionado = false;
            mEtiqueta = "Deshabilitado";
        }

        public System.Windows.Visibility Visibilidad
        {
            get { return mVisibilidad; }
            set
            {
                mVisibilidad = value;
                OnPropertyChanged("Visibilidad");
            }
        }

        public Boolean Seleccionado
        {
            get { return mSeleccionado; }
            set
            {
                mSeleccionado = value;
                OnPropertyChanged("Seleccionado");
            }
        }

        public string Etiqueta
        {
            get { return mEtiqueta; }
            set
            {
                mEtiqueta = value;
                OnPropertyChanged("Etiqueta");
            }
        }

        public int Indice
        {
            get { return mIndice; }
            set
            {
                mIndice = value;
                OnPropertyChanged("Indice");
            }
        }

        protected void OnPropertyChanged(string propiedad)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propiedad));
            }
        }
    }
}
