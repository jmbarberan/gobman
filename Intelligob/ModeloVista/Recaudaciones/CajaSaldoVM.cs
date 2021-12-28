using System;
using System.Linq;

namespace Intelligob.Escritorio.ModeloVista.Recaudaciones
{
    public class CajaSaldoVM : BaseMV<Vistas.Interfaces.IVentanaDialogo>, System.ComponentModel.IDataErrorInfo
    {        
        private readonly Cliente.Depositos.CajasDep cajasDep = new Cliente.Depositos.CajasDep();
        private Cliente.Referencia.CajaComprobanteDto saldoCaja;
        public Double Valor
        {
            get 
            {
                if (this.saldoCaja != null)
                    return (double)this.saldoCaja.Valor;
                return 0;
            }
            set 
            {
                if (this.saldoCaja != null)
                {
                    this.saldoCaja.Valor = value; 
                    OnPropertyChanged("Valor"); 
                }
            }
        }
        public DateTime? Fecha
        {
            get
            {
                return this.saldoCaja.Fecha;
            }
            set
            {
                if (this.saldoCaja != null)
                {
                    this.saldoCaja.Fecha = value;
                    OnPropertyChanged("Fecha");
                }
            }
        }
        public String CajaDescripcion
        {
            get 
            {
                if (this.saldoCaja != null && this.saldoCaja.CajaNav != null)
                {
                    String s = " - (Nuevo saldo)";
                    if (this.saldoCaja.Id > 0)
                        s = " - (Saldo registrado)";
                    return this.saldoCaja.CajaNav.Descripcion + s;
                }
                return String.Empty;
            }
        }

        public System.Windows.Input.ICommand CmdBuscar { get; internal set; }
        public System.Windows.Input.ICommand CmdGuardar { get; internal set; }

        public CajaSaldoVM(Cliente.Referencia.CajaComprobanteDto pSaldoCaja, Cliente.Referencia.CajaDto pCaja)
            : base(new Vistas.Recaudaciones.CajaSaldo())
        {
            this.saldoCaja = pSaldoCaja;
            if (this.saldoCaja == null)
            {
                this.saldoCaja = new Cliente.Referencia.CajaComprobanteDto();
                this.saldoCaja.Tipo = 2;
                this.saldoCaja.Id = 0;
                this.saldoCaja.CajaNav = pCaja;
                if (pCaja != null)
                    this.saldoCaja.Caja = pCaja.Id;
                else
                    this.saldoCaja.Caja = 0;
                this.saldoCaja.Valor = 0;
                this.saldoCaja.Fecha = DateTime.Today;
                OnPropertyChanged("Fecha");
            }
            this.CmdBuscar = new Comandos.ComandoDelegado((o) => this.BuscarAccion(), (o) => this.BuscarHabilita());
            this.CmdGuardar = new Comandos.ComandoDelegado((o) => this.GuardarAccion(), (o) => this.GuardarHabilita());
            this.Vista.Owner = App.Current.MainWindow;
            this.Vista.ShowDialog();
        }        

        private bool BuscarHabilita()
        {
            return this.saldoCaja.CajaNav != null && this.saldoCaja.CajaNav.Id > 0 && this.Fecha != null;
        }

        private void BuscarAccion()
        {
            Cliente.Referencia.CajaComprobanteDto cm = cajasDep.SaldoCajaPorFechaCaja((DateTime)this.Fecha, this.saldoCaja.CajaNav.Id);
            if (cm != null)
            {
                this.saldoCaja = cm;
                OnPropertyChanged("Valor");
                OnPropertyChanged("CajaDescripcion");
            }
            else
                Utiles.CuadroMensajes.Aceptar("Informacion", "No se encontraron registros", "No se encontro registro de saldo de caja en la fecha seleccionada", "", TaskDialogInterop.VistaTaskDialogIcon.Information);
        }

        private bool GuardarHabilita()
        {
            return String.IsNullOrEmpty(this["Valor"]) && String.IsNullOrEmpty(this["Fecha"]);
        }

        private void GuardarAccion()
        {
            // TODO Validar si existe un registro de saldo de caja para esta fecha y preguntar si desea sobreescribirla
            // si existe no se puded guardar otro para la misma fecha y la misma caja
            if (this.saldoCaja.Id > 0)
            {
                this.cajasDep.ComprobanteCajaModificar(this.saldoCaja);
            }
            else
            {
                this.cajasDep.ComprobanteCajaNuevo(this.saldoCaja);
            }
            this.Vista.Close();
        }

        public string this[string columna]
        {
            get
            {
                string error = String.Empty;
                if (columna == "Valor")
                {
                    if (this.Valor == 0)
                        error = "Debe digitar el valor del conteo";
                }
                else
                {
                    if (columna == "Fecha")
                    {
                        String s = String.Empty;
                        if (this.saldoCaja.CajaNav != null && this.saldoCaja.CajaNav.Cierre != null)
                        {
                            if (this.Fecha <= this.saldoCaja.CajaNav.Cierre)
                                s = "Esta fecha esta dentro de un periodo cerrado";
                        }
                        if (String.IsNullOrWhiteSpace(s))
                        {
                            // Validar si ya existe un registro de esta caja para esta fecha
                            if (this.saldoCaja.CajaNav != null && this.saldoCaja.CajaNav.Id > 0)
                            {
                                if (cajasDep.SaldoCajaRegistradoPorFecha(this.saldoCaja, (DateTime)this.Fecha, 9))
                                s = "Ya existe un saldo registrado en este periodo";
                            }
                        }
                        error = s;
                    }
                }
                return error;
            }
        }

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
