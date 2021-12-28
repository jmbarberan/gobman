using Intelligob.Cliente.Depositos;
using Intelligob.Escritorio.Vistas.Interfaces;
using Intelligob.Escritorio.Vistas.Recaudaciones;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Intelligob.Escritorio.ModeloVista.Recaudaciones
{
    public class CobroComprobanteVM : BaseMV<IVentanaDialogo> 
    {
        private readonly List<int> lContribuyentes;
        private readonly Cliente.Depositos.EmisionesDep emisionesDep = new Cliente.Depositos.EmisionesDep();
        private readonly Cliente.Depositos.CajasDep cajasDep = new Cliente.Depositos.CajasDep();
        private int cajaId = 0;
        private string cajaDen = "N/D";
        IEnumerable<Cliente.Referencia.CajasUsuarioDto> cajasTodas = null;

        private readonly int ntitulosSeleccionados = 0;

        private ObservableCollection<Cliente.Modelos.ElementoPago> lAbonos = new ObservableCollection<Cliente.Modelos.ElementoPago>();
        public ObservableCollection<Cliente.Modelos.ElementoPago> LAbonos
        {
            get { return this.lAbonos; }
            set { this.lAbonos = value; OnPropertyChanged("LAbonos"); }
        }

        private Cliente.Modelos.ElementoPago abonoSeleccionado = null;
        public Cliente.Modelos.ElementoPago AbonoSeleccionado
        {
            get { return this.abonoSeleccionado; }
            set { this.abonoSeleccionado = value; OnPropertyChanged("AbonoSeleccionado"); }
        }

        public String TitulosSeleccionados
        {
            get { return String.Format("{0} Titulos seleccionados, Total: ", ntitulosSeleccionados); }
        }

        private readonly double montoPagar = 0;
        public double MontoPagar
        {
            get { return this.montoPagar; }
        }

        private Double totalAbonos = 0;
        public Double TotalAbonos
        {
            get { return this.totalAbonos; }
            set { this.totalAbonos = value; OnPropertyChanged("TotalAbonos"); ValidarDiferencia(); }
        }
        
        public Double Diferencia
        {
            get { return this.MontoPagar - this.TotalAbonos; }
        }

        public System.Windows.Visibility CrearNCredito
        {
            get 
            {
                if (this.Diferencia < 0)
                    return System.Windows.Visibility.Visible;
                else
                    return System.Windows.Visibility.Hidden;
            }
        }

        private Boolean crearNCreditoMarca = false;
        public Boolean CrearNCreditoMarca
        {
            get { return crearNCreditoMarca; }
            set { this.crearNCreditoMarca = value; OnPropertyChanged("CrearNCreditoMarca"); }
        }

        private String aviso = String.Empty;
        public String Aviso
        {
            get { return this.aviso; }
            set { this.aviso = value; OnPropertyChanged("Aviso"); OnPropertyChanged("AvisoVisibilidad"); }
        }

        private int avisoNivel = 1;
        public int AvisoNivel
        {
            get { return this.avisoNivel; }
            set { this.avisoNivel = value; OnPropertyChanged("AvisoNivel"); }
        }

        public System.Windows.Visibility AvisoVisibilidad
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(this.aviso))
                    return System.Windows.Visibility.Visible;
                else
                    return System.Windows.Visibility.Collapsed;
            }
        }        

        public System.Windows.Input.ICommand CmdAgregar
        { get; internal set; }

        public System.Windows.Input.ICommand CmdModificar
        { get; internal set; }

        public System.Windows.Input.ICommand CmdQuitar
        { get; internal set; }

        public System.Windows.Input.ICommand CmdAceptar
        { get; internal set; }

        public System.Windows.Input.ICommand CmdEfectivo
        { get; internal set; }

        public System.Windows.Input.ICommand CmdTickets
        { get; internal set; }

        public CobroComprobanteVM(int pTitulosSel, double pMonto, List<int> pLCons) : base(new CobroComprobante())
        {
            this.lContribuyentes = pLCons;
            this.ntitulosSeleccionados = pTitulosSel;
            this.montoPagar = pMonto;
            this.CmdAgregar = new Comandos.ComandoDelegado((o) => AgregarAccion());
            this.CmdQuitar = new Comandos.ComandoDelegado((o) => QuitarAccion(), (o) => QuitarHabilita());
            this.CmdModificar = new Comandos.ComandoDelegado((o) => ModificarAccion(), (o) => ModificarHabilita());
            this.CmdAceptar = new Comandos.ComandoDelegado((o) => AceptarAccion(), (o) => AceptarHabilita());
            this.CmdEfectivo = new Comandos.ComandoDelegado((o) => EfectivoAccion());
            this.CmdTickets = new Comandos.ComandoDelegado((o) => TicketsAccion());
            this.TraerCajaAsociada();
            this.ComprobarNCredito();
            this.Vista.Owner = App.Current.MainWindow;
            this.Vista.ShowDialog();
        }

        public CobroComprobanteVM(int pTitulosSel, double pMonto) : this(pTitulosSel, pMonto, null) { }

        private void ValidarDiferencia()
        {
            if (this.Diferencia < 0)
            {
                int i = -1;
                foreach(Cliente.Modelos.ElementoPago ep in LAbonos)
                {
                    if (ep.Tipo == 2)
                    {
                        if (ep.Valor > this.Diferencia)
                        {
                            i = 1;
                            this.AbonoSeleccionado = ep;
                            break;
                        }
                        else
                        {
                            if (ep.Valor == this.Diferencia)
                            {
                                i = 2;
                                this.AbonoSeleccionado = ep;
                                break;
                            }
                            else // El valor es menor
                            {
                                this.AbonoSeleccionado = ep;
                                i = 3;
                            }
                        }
                    }
                    else
                    {
                        i = 0;
                    }
                }
                switch (i)
                {
                    case 0:
                        {                            
                            AvisoNivel = 1;
                            Aviso = "El total de componentes de pago excede el monto a pagar, marque la opcion [Crear Nota de credito] para continuar o corrija el valor de los componentes";
                            OnPropertyChanged("CrearNCredito");
                            break;
                        }
                    case 1:
                        {
                            AvisoNivel = 1;
                            Aviso = "El total de componentes excede el monto a pagar, puede reducir el valor de la nota de credito seleccionada para continuar";
                            break;
                        }
                    case 2:
                        {
                            AvisoNivel = 1;
                            Aviso = "El total de componentes excede el monto a pagar, puede quitar la nota de credito seleccionada o modificar el valor de otro componente para continuar";
                            break;
                        }
                    case 3:
                        {
                            AvisoNivel = 2;
                            Aviso = "El total de componentes excede el monto a pagar, puede quitar la nota de credito seleccionada y modificar el valor de otro componente para continuar no se debe crear una Nota de credito por el excedente de otra";
                            break;
                        }
                    default :
                        {
                            AvisoNivel = 1;
                            Aviso = "";
                            OnPropertyChanged("CrearNCredito");
                            break;
                        }
                }                
            }
            else
            {
                AvisoNivel = 0;
                Aviso = "";
                OnPropertyChanged("CrearNCredito");
            }
            OnPropertyChanged("Diferencia");
        }

        private void ComprobarNCredito()
        {
            bool b = false;
            foreach(int i in lContribuyentes)
            {
                IEnumerable<Cliente.Referencia.ConvenioDto> c = emisionesDep.NCreditosPorContribuyenteEstado(i, 0, false);
                if (c.Count() > 0)
                {
                    b = true;
                    break;
                }
            }
            if (b)
            {
                AvisoNivel = 0;
                Aviso = "Existen Notas de credito registradas a favor del contribuyente, considere su aplicacion de acuerdo a los procedimientos establecidos";
            }
        }

        private void AbrirComponente(Cliente.Modelos.ElementoPago ele, Boolean nuevo)
        {
            FormaPagoVM fp;
            if (ele.Tipo == 2 && ele.NotaCreditoId > 0)
                fp = new FormaPagoVM(ele, ele.NotaCreditoId);
            else
                fp = new FormaPagoVM(ele, lContribuyentes);
            if (fp.Vista.DialogResult == true)
            {
                if (nuevo)
                    this.LAbonos.Add(ele);
                else
                    this.AbonoSeleccionado.CopiarDe(ele);
                this.CalcularAbonos();
            }
        }        

        private void TraerCajaAsociada()
        {            
            if (!Cliente.SesionUtiles.Instance.EsDesarrollador && Cliente.SesionUtiles.Instance.UsuarioActivo != null)
            {
                cajasTodas = this.cajasDep.CajasPorUsuarioEstado(Cliente.SesionUtiles.Instance.UsuarioActivo.Id, 0, true);
            }
            else
            {
                if (Cliente.SesionUtiles.Instance.EsDesarrollador)
                {
                    List<Cliente.Referencia.CajasUsuarioDto> cuso = new List<Cliente.Referencia.CajasUsuarioDto>();
                    IEnumerable<Cliente.Referencia.CajaDto> cajasExistentes = this.cajasDep.CajasPorEstado(0);
                    foreach (Cliente.Referencia.CajaDto c in cajasExistentes)
                    {
                        Cliente.Referencia.CajasUsuarioDto cu = new Cliente.Referencia.CajasUsuarioDto();
                        cu.Caja = c.Id;
                        cu.CajaNav = c;
                        cu.Id = c.Id;
                        cu.Usuario = -1;
                        cuso.Add(cu);
                    }
                    cajasTodas = cuso.AsEnumerable();
                }
            }
            if (cajasTodas != null && cajasTodas.Count() > 0)
            {
                if (cajasTodas.Count() == 1)
                {
                    cajaId = (int)cajasTodas.ElementAt(0).Caja;
                    String s = "Desarrollador";
                    if (cajasTodas.ElementAt(0).Usuario != null && cajasTodas.ElementAt(0).Usuario > 0)
                        s = cajasTodas.ElementAt(0).UsuarioNav.Nombres;
                    cajaDen = cajasTodas.ElementAt(0).CajaNav.Descripcion + " - " + s;
                }                
            }            
        }

        private bool AceptarHabilita()
        {
            bool res = true;
            res = Math.Round(this.Diferencia, 2) == 0 || (Math.Round(this.Diferencia, 2) < 0 && this.CrearNCreditoMarca);
            return res;
        }

        private void AceptarAccion()
        {
            this.Vista.DialogResult = true;
            this.Vista.Close();
        }

        private void EfectivoAccion()
        {
            if (this.cajaId > 0)
            {
                Cliente.Modelos.ElementoPago ele = new Cliente.Modelos.ElementoPago();
                ele.CajaId = this.cajaId;
                ele.Caja = this.cajaDen;
                ele.Valor = this.MontoPagar;
                this.LAbonos.Add(ele);
            }
            else
            {
                IEnumerable<Cliente.Referencia.CajaDto> cs = cajasDep.CajasPorEstado(0);
                if (cs.Count() > 0)
                {
                    if (cs.Count() == 1)
                    {
                        String s = "Desarrollador";
                        if (!Cliente.SesionUtiles.Instance.EsDesarrollador && Cliente.SesionUtiles.Instance.UsuarioActivo == null)
                            s = Cliente.SesionUtiles.Instance.UsuarioActivo.Codigo;
                        Cliente.Modelos.ElementoPago ele = new Cliente.Modelos.ElementoPago();
                        ele.CajaId = cs.ElementAtOrDefault(0).Id;
                        ele.Caja = cs.ElementAtOrDefault(0).Descripcion + " - " + s;
                        ele.Valor = this.MontoPagar;
                        this.LAbonos.Add(ele);
                    }
                    else
                    {
                        CajaSeleccionarVM cajasel = new CajaSeleccionarVM(null);
                        if (cajasel.Vista.DialogResult == true)
                        {
                            String s = "Desarrollador";
                            if (!Cliente.SesionUtiles.Instance.EsDesarrollador && Cliente.SesionUtiles.Instance.UsuarioActivo == null)
                                s = Cliente.SesionUtiles.Instance.UsuarioActivo.Codigo;
                            Cliente.Modelos.ElementoPago ele = new Cliente.Modelos.ElementoPago();
                            ele.CajaId = cajasel.Seleccionado.Id;
                            ele.Caja = cajasel.Seleccionado.Descripcion + " - " + s;
                            ele.Valor = this.MontoPagar;
                            this.LAbonos.Add(ele);
                        }
                        else
                            Utiles.CuadroMensajes.Alertar("Atencion", "No ha seleccionado la caja", "No se puede completar el proceso porque no ha seleccionado una caja para el cobro", "");
                    }
                }
                else
                {
                    Utiles.CuadroMensajes.Alertar("Atencion", "No hay cajas habilitadas", "No se encontraron cajas habilitadas para esta funcion", "");
                }
            }
            this.Vista.DialogResult = true;
            this.Vista.Close();
        }

        private void TicketsAccion()
        {
            General.CapturaNumeroVM cn = new General.CapturaNumeroVM("Digite el numero del mes a cobrar");
            if (cn.Vista.DialogResult == true)
            {
                int num = cn.Entero;
                using (RecaudacionesDep rd  = new RecaudacionesDep())
                {
                    //List<String>
                }
            }
        }

        private void CalcularAbonos()
        {
            TotalAbonos = 0;
            foreach (Cliente.Modelos.ElementoPago e in LAbonos)
            {
                TotalAbonos = this.TotalAbonos + e.Valor;
            }
        }

        public void AgregarAccion()
        {
            this.AbrirComponente(new Cliente.Modelos.ElementoPago(), true);            
        }

        public void QuitarAccion()
        {
            Cliente.Modelos.ElementoPago ele = new Cliente.Modelos.ElementoPago();
            ele.CopiarDe(this.AbonoSeleccionado);
            this.TotalAbonos = this.TotalAbonos - ele.Valor;
            this.LAbonos.Remove(this.AbonoSeleccionado);
        }

        public void ModificarAccion()
        {
            Cliente.Modelos.ElementoPago ep = new Cliente.Modelos.ElementoPago();
            ep.CopiarDe(this.AbonoSeleccionado);
            this.AbrirComponente(ep, false);
        }

        public Boolean QuitarHabilita()
        {
            return this.AbonoSeleccionado != null;
        }

        public Boolean ModificarHabilita()
        {
            return this.AbonoSeleccionado != null;
        }
    }
}
