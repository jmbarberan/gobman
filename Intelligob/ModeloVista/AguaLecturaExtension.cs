using Intelligob.Cliente.Depositos;
using Intelligob.Cliente.Referencia;
using System;
using System.ComponentModel;
using System.Linq;

namespace Intelligob.Escritorio.ModeloVista
{
    public class AguaLecturaExtension : INotifyPropertyChanged
    {
        public AguaLecturaExtension(AguaLecturaDto lectura, int mes)
        {
            if (lectura != null)
            {
                ELectura = lectura;
            }
            if (mes > 0)
            {
                this.MesConsulta = mes;
            }
        }

        public AguaLecturaDto ELectura;
        public int MesConsulta = 1;
        private readonly AguaDep aguaDep = new AguaDep();

        public double? LecturaAnterior
        {
            get
            {
                switch (MesConsulta)
                {
                    case 1: 
                        {
                            AguaLecturaDto l = aguaDep.LecturaTraerPorCuentaAño((int)ELectura.Cuenta, (int)ELectura.Año - 1);
                            return l.Mes12;
                        }
                    case 2: { return ELectura.Mes1; }
                    case 3: { return ELectura.Mes2; }
                    case 4: { return ELectura.Mes3; }
                    case 5: { return ELectura.Mes4; }
                    case 6: { return ELectura.Mes5; }
                    case 7: { return ELectura.Mes6; }
                    case 8: { return ELectura.Mes7; }
                    case 9: { return ELectura.Mes8; }
                    case 10: { return ELectura.Mes9; }
                    case 11: { return ELectura.Mes10; }
                    case 12: { return ELectura.Mes11; }
                    default: { return 0; }
                }
            }
        }

        public double? LecturaActual
        {
            get 
            {
                switch (MesConsulta)
                {
                    case 1: { return ELectura.Mes1; }
                    case 2: { return ELectura.Mes2; }
                    case 3: { return ELectura.Mes3; }
                    case 4: { return ELectura.Mes4; }
                    case 5: { return ELectura.Mes5; }
                    case 6: { return ELectura.Mes6; }
                    case 7: { return ELectura.Mes7; }
                    case 8: { return ELectura.Mes8; }
                    case 9: { return ELectura.Mes9; }
                    case 10: { return ELectura.Mes10; }
                    case 11: { return ELectura.Mes11; }
                    case 12: { return ELectura.Mes12; }
                    default: { return 0; }
                }
            }
            set
            {
                switch (MesConsulta)
                {                    
                    case 1: { ELectura.Mes1 = value; break; }
                    case 2: { ELectura.Mes2 = value; break; }
                    case 3: { ELectura.Mes3 = value; break; }
                    case 4: { ELectura.Mes4 = value; break; }
                    case 5: { ELectura.Mes5 = value; break; }
                    case 6: { ELectura.Mes6 = value; break; }
                    case 7: { ELectura.Mes7 = value; break; }
                    case 8: { ELectura.Mes8 = value; break; }
                    case 9: { ELectura.Mes9 = value; break; }
                    case 10: { ELectura.Mes10 = value; break; }
                    case 11: { ELectura.Mes11 = value; break; }
                    case 12: { ELectura.Mes12 = value; break; }
                }
                
                OnPropertyChanged("LecturaActual");
                OnPropertyChanged("LecturaMenor");
                OnPropertyChanged("Consumo");
            }
        }

        public double? Consumo
        {
            get
            {
                return LecturaActual - LecturaAnterior;
            }
        }

        public Boolean LecturaMenor
        {
            get
            {
                return LecturaAnterior >= LecturaActual;

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
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
