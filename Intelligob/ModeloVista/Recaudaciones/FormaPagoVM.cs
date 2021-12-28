using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Intelligob.Escritorio.ModeloVista.Recaudaciones
{
    public class FormaPagoVM : BaseMV<Vistas.Interfaces.IVentanaDialogo>, System.ComponentModel.IDataErrorInfo 
    {
        private readonly List<int> lContribuyentes;
        private readonly Cliente.Depositos.CajasDep cajasDep = new Cliente.Depositos.CajasDep();
        private readonly Cliente.Depositos.EmisionesDep emisionesDep = new Cliente.Depositos.EmisionesDep();
        private Double valorSaldoNC = 0;

        private System.Windows.Visibility visiblidadCajaSel = System.Windows.Visibility.Collapsed;
        
        public System.Windows.Visibility VisiblidadCajaSel
        {
            get { return this.visiblidadCajaSel; }
            set { this.visiblidadCajaSel = value; OnPropertyChanged("VisiblidadCajaSel"); }
        }

        private IEnumerable<Cliente.Referencia.TablaClaveDto> formasPago;
        public IEnumerable<Cliente.Referencia.TablaClaveDto> FormasPago
        {
            get { return this.formasPago; }
            set { this.formasPago = value; OnPropertyChanged("FormasPago"); }
        }

        private Cliente.Modelos.ElementoPago elementoPago = new Cliente.Modelos.ElementoPago();
        public Cliente.Modelos.ElementoPago OelementoPago
        {
            get { return this.elementoPago; }
            set { this.elementoPago = value; OnPropertyChanged("OelementoPago"); }
        }
        
        public Double Valor
        {
            get { return this.OelementoPago.Valor; }
            set { this.OelementoPago.Valor = value; OnPropertyChanged("Valor"); }
        }

        public int Tipo
        {
            get { return this.OelementoPago.Tipo; }
            set { this.OelementoPago.Tipo = value; OnPropertyChanged("Tipo"); OnPropertyChanged("Valor"); }
        }
                
        public String CajaRecaudacion
        {
            get 
            {
                String s = "N/D";
                if (!Cliente.SesionUtiles.Instance.EsDesarrollador && Intelligob.Cliente.SesionUtiles.Instance.UsuarioActivo != null)
                s = Intelligob.Cliente.SesionUtiles.Instance.UsuarioActivo.Nombres;
                return s;
            }
        }

        public System.Windows.Controls.Frame NavegadorFP;        

        public ICommand CmdAceptar
        { get; internal set; }

        public ICommand CmdCajaSeleccionar
        { get; internal set; }

        public ICommand CmdNotaCreditoSeleccionar
        { get; internal set; }

        public FormaPagoVM(Cliente.Modelos.ElementoPago pEle) : this(pEle, null) { }

        public FormaPagoVM(Cliente.Modelos.ElementoPago pEle, List<int> pLcons) : base(new Vistas.Recaudaciones.FormaPago())
        {            
            if (pLcons != null)
                this.lContribuyentes = pLcons;
            this.elementoPago = pEle;
            this.CrearComandos();
            this.TraerCajaAsociada();
            this.MostrarVista();
        }

        public FormaPagoVM(Cliente.Modelos.ElementoPago pEle, int pNCreditoId) : base(new Vistas.Recaudaciones.FormaPago())
        {
            if (pNCreditoId > 0)
            {
                Cliente.Referencia.ConvenioDto c = emisionesDep.Servicio.ReadConvenio(String.Format(emisionesDep.FormatoClave, pNCreditoId));
                if (c != null)
                {
                    double d = (double)c.Valor;
                    if (c.Pagos != null)
                        d = d - (double)c.Pagos;
                    this.valorSaldoNC = d;
                }
            }            
            this.elementoPago = pEle;
            this.CrearComandos();
            this.TraerCajaAsociada();
            this.MostrarVista();
        }

        private void MostrarVista()
        {
            this.Vista.Owner = App.Current.MainWindow;
            this.Vista.ShowDialog();
        }        

        private bool Valido()
        {
            // Verficiar datos de caja / cheque / Nota credito
            Boolean res = true;
            switch(this.Tipo)
            {
                case 1: // Cheque
                    {
                        if (String.IsNullOrWhiteSpace(this.OelementoPago.Nombres))
                            res = false;
                        else
                        {
                            if (String.IsNullOrWhiteSpace(this.OelementoPago.Cuenta))
                                res = false;
                            else
                            {
                                if (String.IsNullOrWhiteSpace(this.OelementoPago.Banco))
                                    res = false;
                                else
                                {
                                    if (OelementoPago.Numero <= 0)
                                        res = false;
                                }
                            }
                        }
                        break; 
                    }
                case 2: // Nota credito
                    {
                        if (this.OelementoPago.NotaCreditoId <= 0)
                            res = false;
                        break; 
                    }
                default: // Efectivo
                    {
                        if (this.OelementoPago.CajaId <= 0)
                            res = false;
                        break; 
                    }
            }
            return res;
        }

        private void CrearComandos()
        {
            this.CmdAceptar = new Comandos.ComandoDelegado((a) => AceptarAccion(), (a) => AceptarHabilita());            
            this.CmdCajaSeleccionar = new Comandos.ComandoDelegado(a => CajaSeleccionarAccion());
            this.CmdNotaCreditoSeleccionar = new Comandos.ComandoDelegado(a => NotaCreditoSeleccionarAccion());            
        }

        private void TraerCajaAsociada()
        {
            IEnumerable<Cliente.Referencia.CajasUsuarioDto> cus = null;
            if (! Cliente.SesionUtiles.Instance.EsDesarrollador && Cliente.SesionUtiles.Instance.UsuarioActivo != null)
            {
                cus = this.cajasDep.CajasPorUsuarioEstado(Cliente.SesionUtiles.Instance.UsuarioActivo.Id, 0, true);
            }
            else
            {
                if (Cliente.SesionUtiles.Instance.EsDesarrollador)
                {
                    List<Cliente.Referencia.CajasUsuarioDto> cuso = new List<Cliente.Referencia.CajasUsuarioDto>();
                    IEnumerable<Cliente.Referencia.CajaDto> cajasExistentes = this.cajasDep.CajasPorEstado(0);
                    foreach(Cliente.Referencia.CajaDto c in cajasExistentes)
                    {
                        Cliente.Referencia.CajasUsuarioDto cu = new Cliente.Referencia.CajasUsuarioDto();
                        cu.Caja = c.Id;
                        cu.CajaNav = c;
                        cu.Id = c.Id;
                        cu.Usuario = -1;
                        cuso.Add(cu);
                    }
                    cus = cuso.AsEnumerable();
                }
            }
            if (cus != null && cus.Count() > 0)
            {
                if (cus.Count() == 1)
                {
                    OelementoPago.CajaId = (int)cus.ElementAt(0).Caja;
                    String s = "Desarrollador";
                    if (cus.ElementAt(0).Usuario != null && cus.ElementAt(0).Usuario > 0)
                        s = cus.ElementAt(0).UsuarioNav.Nombres;
                    OelementoPago.Caja = cus.ElementAt(0).CajaNav.Descripcion + " - " + s;
                }
                else
                {
                    if (cus.Count() > 1)
                    {
                        this.VisiblidadCajaSel = System.Windows.Visibility.Visible;
                    }
                }
            }
            else
            {
                IEnumerable<Cliente.Referencia.CajaDto> cajasExi = this.cajasDep.CajasPorEstado(0);
                if (cajasExi != null && cajasExi.Count() > 0)
                {
                    this.VisiblidadCajaSel = System.Windows.Visibility.Visible;
                }
            }
        }
        
        private Boolean AceptarHabilita()
        {
            Boolean res = true;
            if (this.Valor <= 0)
                res = false;
            else
            {
                switch (this.Tipo)
                {
                    case 1: // Cheque
                        {
                            if (String.IsNullOrWhiteSpace(this.OelementoPago.Nombres))
                                res = false;
                            else
                            {
                                if (String.IsNullOrWhiteSpace(this.OelementoPago.Cuenta))
                                    res = false;
                                else
                                {
                                    if (String.IsNullOrWhiteSpace(this.OelementoPago.Banco))
                                        res = false;
                                    else
                                    {
                                        if (OelementoPago.Numero <= 0)
                                            res = false;
                                    }
                                }
                            }
                            break;
                        }
                    case 2: // Nota credito
                        {
                            if (this.OelementoPago.NotaCreditoId <= 0)
                                res = false;
                            break;
                        }
                    default: // Efectivo
                        {
                            if (this.OelementoPago.CajaId <= 0)
                                res = false;
                            break;
                        }
                }
            }            
            return res;
        }

        private void AceptarAccion()
        { 
            if (Valido())
            {
                this.Vista.DialogResult = true;
                this.Vista.Close();
            }
            else
            {
                Utiles.CuadroMensajes.Alertar("Atencion", "No se puede continuar", "debe llenar o seleccionar la informacion necesaria", String.Empty);
            }
        }        

        private void CajaSeleccionarAccion()
        {
            CajaSeleccionarVM cajasel = new CajaSeleccionarVM(null);
           if (cajasel.Vista.DialogResult == true)
           {
               String s = "Desarrollador";
               if (!Cliente.SesionUtiles.Instance.EsDesarrollador)
                   s = Cliente.SesionUtiles.Instance.UsuarioActivo.Nombres;
               this.OelementoPago.Caja = cajasel.Seleccionado.Descripcion + " - " + s;
               this.OelementoPago.CajaId = cajasel.Seleccionado.Id;
           }
        }

        private void NotaCreditoSeleccionarAccion()
        {
            Emisiones.NotaCreditoSeleccionarVM ns = new Emisiones.NotaCreditoSeleccionarVM(this.lContribuyentes);
            if (ns.Vista.DialogResult == true)
            {
                double d = (double)ns.Seleccionado.Valor;
                if (ns.Seleccionado.Pagos != null)
                    d = d - (double)ns.Seleccionado.Pagos;
                if (d > 0)
                {
                    this.OelementoPago.NotaCreditoId = ns.Seleccionado.Id;
                    this.OelementoPago.NotaCredito = String.Format("No. {0}  Emitida {1:d}", ns.Seleccionado.Numero, ns.Seleccionado.FechaEmision);
                    this.Valor = d;
                    this.valorSaldoNC = d;
                    this.OnPropertyChanged("Valor");
                }
                else 
                {
                    Utiles.CuadroMensajes.Alertar("Atencion", "Nota de credito no aplicable", "No se puede aplicar esta nota de credito por que no tiene saldo", "");
                }
            }
        }

        public string this[string campo]
        {
            get
            {
                String error = String.Empty;
                if (campo == "Valor")
                {                    
                    if (this.Valor <= 0)
                    {                        
                        error = "Debe digitar el valor";
                    }
                    else
                    {
                        if (this.Tipo == 2)
                        {
                            if (this.OelementoPago.NotaCreditoId > 0)
                            {
                                if (this.Valor > this.valorSaldoNC)
                                {
                                    error = "Este valor no puede exceder el saldo de la Nota de credito";
                                }
                            }
                            else
                            {
                                error = "Seleccione una Nota de credito";
                            }
                        }
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
