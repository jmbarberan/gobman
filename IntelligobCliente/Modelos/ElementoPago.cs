using System;
using System.ComponentModel;
using System.Linq;

namespace Intelligob.Cliente.Modelos
{
    public class ElementoPago : INotifyPropertyChanged
    {
        public int Id = 0;
        public int CajaId = 0;
        public int NotaCreditoId = 0;

        private int tipo = 0;
        public int Tipo
        {
            get { return this.tipo; }
            set { this.tipo = value; OnPropertyChanged("Tipo"); }
        }

        private Double valor = 0;
        public Double Valor
        {
            get { return this.valor; }
            set { this.valor = value; OnPropertyChanged("Valor"); }
        }

        private Referencia.ContribuyenteDto contribuyente;
        public Referencia.ContribuyenteDto Contribuyente
        {
            get { return this.contribuyente; }
            set { this.contribuyente = value; OnPropertyChanged("Contribuyente"); }
        }

        private int numero = 0;
        public int Numero
        {
            get { return this.numero; }
            set { this.numero = value; OnPropertyChanged("Numero"); }
        }        

        private String codigo;
        public String Codigo
        {
            get { return this.codigo; }
            set { this.codigo = value; OnPropertyChanged("Codigo"); }
        }

        private String cuenta;
        public String Cuenta
        {
            get { return this.cuenta; }
            set { this.cuenta = value; OnPropertyChanged("Cuenta"); }
        }

        private String banco;
        public String Banco
        {
            get { return this.banco; }
            set { this.banco = value; OnPropertyChanged("Banco"); }
        }        

        private String nombres;
        public String Nombres
        {
            get { return this.nombres; }
            set { this.nombres = value; OnPropertyChanged("Nombres"); }
        }

        private DateTime fecha = DateTime.Today;
        public DateTime Fecha
        {
            get { return this.fecha; }
            set { this.fecha = value; OnPropertyChanged("Fecha"); }
        }

        private String notaCredito;
        public String NotaCredito
        {
            get { return this.notaCredito; }
            set { this.notaCredito = value; OnPropertyChanged("NotaCredito"); }
        }

        private String caja;
        public String Caja
        {
            get { return this.caja; }
            set { this.caja = value; OnPropertyChanged("Caja"); }
        }

        public String Presentacion
        {
            get
            {
                string res = "N/D";
                switch (this.Tipo)
                {
                    case 1:
                        {
                            res = "Cheque: " + Numero.ToString() + " de " + this.Banco;
                            break;
                        }
                    case 2:
                        {
                            res = "N. Credito: " + NotaCredito;
                            break;
                        }
                    default:
                        {
                            res = "Efectivo: " + Caja;
                            break;
                        }
                }
                return res;
            }
        }

        public void CopiarDe(ElementoPago ele)
        {
            this.Id = ele.Id;
            this.Tipo = ele.Tipo;
            this.Valor = ele.Valor;
            this.CajaId = ele.CajaId;
            this.NotaCreditoId = ele.NotaCreditoId;
            this.Banco = ele.Banco;
            this.Caja = ele.Caja;
            this.Codigo = ele.Codigo;
            this.Contribuyente = ele.Contribuyente;
            this.Cuenta = ele.Cuenta;
            this.Fecha = ele.Fecha;
            this.Nombres = ele.Nombres;
            this.NotaCredito = ele.NotaCredito;
            this.Numero = ele.Numero;            
        }

        #region Control de cambios de interfaz
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
