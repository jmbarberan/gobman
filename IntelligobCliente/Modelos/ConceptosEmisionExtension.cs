using System;
using System.Globalization;
using System.Linq;

namespace Intelligob.Cliente.Referencia
{
    public partial class ConceptosEmisionDto
    {
        private int identificador;
        public int Identificador
        {
            get { return this.identificador; }
            set { this.identificador = value; RaisePropertyChanged("Identificador"); RaisePropertyChanged("Presentacion"); }
        }

        private double valor;
        public double Valor
        {
            get { return this.valor; }
            set { this.valor = value; RaisePropertyChanged("Valor"); RaisePropertyChanged("Presentacion"); }
        }

        private string cadena;
        public string Cadena
        {
            get { return this.cadena; }
            set { this.cadena = value; RaisePropertyChanged("Cadena"); }
        }

        private string seleccionadoPresentacion;
        public string SeleccionadoPresentacion
        {
            get { return this.seleccionadoPresentacion; }
            set { this.seleccionadoPresentacion = value; RaisePropertyChanged("SeleccionadoPresentacion"); RaisePropertyChanged("Presentacion"); } 
        }

        private string seleccionadoInfo;
        public string SeleccionadoInfo
        {
            get { return this.seleccionadoInfo; }
            set { this.seleccionadoInfo = value; RaisePropertyChanged("SeleccionadoInfo"); }
        }

        public string Presentacion
        {            
            get 
            {
                String s = String.Empty;
                switch (this.TipoDato)
                {
                    case 1:
                        {
                            if (this.Origen == 1)
                            {
                                if (this.Periodico == 2)
                                {
                                    DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;
                                    s = formatoFecha.GetMonthName(this.Identificador);                                    
                                    TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
                                    s = textInfo.ToTitleCase(s);
                                }                                    
                                else
                                    s = this.Identificador.ToString();
                            }
                            else
                                s = SeleccionadoPresentacion;
                            break;
                        }
                    case 2:
                        {
                            if (this.Origen == 1)
                                s = this.Valor.ToString();
                            else
                                s = SeleccionadoPresentacion;
                            break;
                        }

                }

                return s;
            }
            set 
            {
                switch (this.TipoDato)
                {
                    case 1:
                        {
                            if (this.Origen == 1)
                            {
                                try
                                {
                                    this.Identificador = Convert.ToInt32(value);
                                }
                                catch
                                {
                                    this.Identificador = 0;
                                }
                            }
                                
                            break;
                        }
                    case 2:
                        {
                            if (this.Origen == 1)
                            {
                                try
                                {
                                    this.Valor = Convert.ToDouble(value);
                                }
                                catch 
                                {
                                    this.Valor = 0;
                                }
                            }
                                
                            break;
                        }
                }
                this.RaisePropertyChanged("Presentacion");
            }
        }
        
    }
}
