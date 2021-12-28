using System;
using System.ComponentModel;
using System.Linq;

namespace Intelligob.Cliente.Modelos
{
    public class Informe : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public String Nombre { get; set; }

        public String Descripccion { get; set; }        

        public String IconoUri { get; set; }

        private Type informeType;                

        public Type InformeType
        {
            get { return this.informeType; }
            set { this.informeType = value; }
        }

        public string Clase
        {
            get
            {
                string s = String.Empty;
                if (this.informeType != null)
                    s = this.informeType.AssemblyQualifiedName;
                return s;
            }
        }
    
        public int Indice {get; set;}

        public string Definicion { get; set; }

        public System.Collections.ObjectModel.ObservableCollection<Utiles.ElementoSeleccion> Alternativos { get; set; }

        private Utiles.ElementoSeleccion seleccionado;
        public Utiles.ElementoSeleccion Seleccionado
        {
            get { return this.seleccionado; }
            set { this.seleccionado = value; OnPropertyChanged("Seleccionado"); }
        }

        public Informe(String nom, String des, Type tipo, int indice, String icono, String pDefinicion, Utiles.ElementoSeleccion[] palernativos)
        {
            Nombre = nom;
            Descripccion = des;
            InformeType = tipo;
            Indice = indice;
            IconoUri = icono;
            Definicion = pDefinicion;
            if (palernativos != null)
                Alternativos = new System.Collections.ObjectModel.ObservableCollection<Utiles.ElementoSeleccion>(palernativos);
        }

        public Informe(String nom, String des, Type tipo, int indice, String icono, Utiles.ElementoSeleccion[] palernativos)
        {
            Nombre = nom;
            Descripccion = des;
            InformeType = tipo;
            Indice = indice;
            IconoUri = icono;
            if (palernativos != null)
                Alternativos = new System.Collections.ObjectModel.ObservableCollection<Utiles.ElementoSeleccion>(palernativos);
        }

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
