using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Intelligob.Cliente.Modelos
{
    public partial class Consulta : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private String iconoUri;
        public String IconoUri 
        {
            get { return iconoUri; }
            set { iconoUri = value; OnPropertyChanged("IconoUri"); }
        }

        private String nombre;
        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; OnPropertyChanged("Nombre"); }
        }

        private Type tipoConsulta;
        public Type TipoConsulta
        {
            get { return tipoConsulta; }
            set { tipoConsulta = value; OnPropertyChanged("TipoConsulta"); }
        }   

        public Consulta(String pNombre, String pIcono, Type pTipo)
        {
            Nombre = pNombre;
            IconoUri = pIcono;
            TipoConsulta = pTipo;
        }

    }
}
