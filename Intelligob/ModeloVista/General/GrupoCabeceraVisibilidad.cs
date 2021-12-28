using System.ComponentModel;
using System;
using System.Linq;
using Intelligob.Utiles;

namespace Intelligob.Escritorio.ModeloVista.General
{
    public class GrupoCabeceraVisibilidad : INotifyPropertyChanged
    {
        public bool ShowGroupHeaderColumnAggregates
        {
            get
            {
                return Configuracion.CobrosMostrarColumnasAgregados;
            }
            set
            {
                if (Configuracion.CobrosMostrarColumnasAgregados != value)
                {
                    Configuracion.CobrosMostrarColumnasAgregados = value;

                    OnPropertyChanged("ShowGroupHeaderColumnAggregates");
                }
            }
        }

        public bool ShowHeaderAggregates
        {
            get
            {
                return Configuracion.CobrosMostrarCabeceraAgregados;
            }
            set
            {
                if (Configuracion.CobrosMostrarCabeceraAgregados != value)
                {
                    Configuracion.CobrosMostrarCabeceraAgregados = value;

                    OnPropertyChanged("ShowHeaderAggregates");
                }
            }
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
