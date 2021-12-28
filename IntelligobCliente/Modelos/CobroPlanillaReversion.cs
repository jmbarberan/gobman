using System;
using System.ComponentModel;
using System.Linq;

namespace Intelligob.Cliente.Modelos
{
    public class CobroPlanillaReversion : INotifyPropertyChanged
    {
        public Intelligob.Cliente.Referencia.CobroDto Cobro { get; set; }

        public Intelligob.Cliente.Referencia.CobroTransaccionDto Planilla { get; set; }        

        public DateTime? Fecha
        {
            get
            {
                return this.Cobro.Fecha;
            }
        }

        public Double? Pago
        { get { return this.Planilla.Valor; } }

        public Double? Rebajas
        { get { return this.Planilla.Rebajas; } }

        private bool estado = false;
        public bool Estado
        {
            get { return this.estado; }
            set { this.estado = value; OnPropertyChanged("Estado"); }
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
