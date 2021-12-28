using Intelligob.Cliente.Referencia;
using System;
using System.ComponentModel;
using System.Linq;

namespace Intelligob.Cliente
{
    public class SesionUtiles : INotifyPropertyChanged
    {
        static readonly SesionUtiles mInstance = new SesionUtiles();
        public static SesionUtiles Instance
        {
            get { return mInstance; }
        }

        public SesionUtiles() { }

        private UsuarioDto musr;
        private Boolean des;

        public UsuarioDto UsuarioActivo
        {
            get { return musr; }
            set { musr = value; OnPropertyChanged("UsuarioActivo"); }
        }

        public Boolean EsDesarrollador
        {
            get { return des; }
            set { des = value; OnPropertyChanged("EsDesarrollador"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
