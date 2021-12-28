using Intelligob.Escritorio.Vistas;
using Intelligob.Escritorio.Vistas.Interfaces;
using Intelligob.Utiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Printing;
using System.Printing.Interop;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows.Interop;

namespace Intelligob.Escritorio.ModeloVista
{
    public class ImpresionAjustesVM : BaseMV<IVentanaDialogo>
    {
        public System.Drawing.Printing.PrinterSettings AjustesImpresion;
        //public PrintQueue ImpresoraActual = LocalPrintServer.GetDefaultPrintQueue();
        public ICommand CmdAceptar
        { get; internal set; }

        public ICommand CmdAjustes
        { get; internal set; }

        private string seleccionada;
        public string Seleccionada 
        {
            get { return this.seleccionada; }
            set
            {
                this.seleccionada = value;
                OnPropertyChanged("Seleccionada");
                CargarTamañosPapelImpresora();
            }
        }

        private short copias = 1;
        public short Copias 
        {
            get { return this.copias; }
            set
            {
                this.copias = value;
                OnPropertyChanged("Copias");
            }
        }

        private bool intercalar = false;
        public bool Intercalar
        {
            get { return this.intercalar; }
            set
            {
                this.intercalar = value;
                OnPropertyChanged("Intercalar");
            }
        }

        private int iorientacion = 0;
        public int IOrientacion
        { get { return this.iorientacion; } set { this.iorientacion = value; OnPropertyChanged("IOrientacion"); } }


        private PageOrientation Orientacion
        {
            get 
            {
                if (this.IOrientacion == 0)
                    return PageOrientation.Landscape;
                else
                    return PageOrientation.Portrait;
            }
        }

        private System.Drawing.Printing.PaperSize tamaño;
        public System.Drawing.Printing.PaperSize Tamaño
        {
            get { return tamaño; }
            set { tamaño = value; OnPropertyChanged("Tamaño"); }
        }

        private ObservableCollection<System.Drawing.Printing.PaperSize> ltamaños;
        public ObservableCollection<System.Drawing.Printing.PaperSize> LTamaños
        {
            get { return ltamaños; }
            set { ltamaños = value; OnPropertyChanged("Tamaños"); }
        }

        public ImpresionAjustesVM() : base(new ImpresionAjustes())
        {
            AjustesImpresion = new System.Drawing.Printing.PrinterSettings();
            Seleccionada = AjustesImpresion.PrinterName;

            CmdAceptar = new Comandos.ComandoDelegado((o) => AccionAceptar());
            CmdAjustes = new Comandos.ComandoDelegado((o) => AccionAjustes());
        }

        private void AccionAceptar()
        {
            AjustesImpresion.Copies = Copias;
            AjustesImpresion.Collate = Intercalar;
            AjustesImpresion.PrinterName = Seleccionada;
            AjustesImpresion.DefaultPageSettings.PaperSize = Tamaño;
            
            this.Vista.DialogResult = true;
            this.Vista.Close();
        }

        [DllImport("winspool.Drv", EntryPoint = "DocumentPropertiesW", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern int DocumentProperties(IntPtr hwnd, IntPtr hPrinter, [MarshalAs(UnmanagedType.LPWStr)] string pDeviceName, IntPtr pDevModeOutput, IntPtr pDevModeInput, int fMode);
        private void AccionAjustes()
        {
            try
            {
                PrintQueue impresoraActual = new PrintQueue(new PrintServer(), Seleccionada);
                var ptc = new PrintTicketConverter(Seleccionada, impresoraActual.ClientPrintSchemaVersion);                
                var mainWindowPtr = new WindowInteropHelper(App.Current.MainWindow).Handle;
                var myDevMode = ptc.ConvertPrintTicketToDevMode(impresoraActual.UserPrintTicket, BaseDevModeType.UserDefault);
                var pinnedDevMode = GCHandle.Alloc(myDevMode, GCHandleType.Pinned);
                var pDevMode = pinnedDevMode.AddrOfPinnedObject();
                var result = DocumentProperties(mainWindowPtr, IntPtr.Zero, impresoraActual.FullName, pDevMode, pDevMode, 14);                
                if (result == 1)
                {
                    impresoraActual.UserPrintTicket = ptc.ConvertDevModeToPrintTicket(myDevMode);
                    impresoraActual.UserPrintTicket.PageOrientation = Orientacion;
                    //impresoraActual.UserPrintTicket.PageMediaSize.
                    //PageMediaSize p = impresoraActual.DefaultPrintTicket.PageMediaSize;
                    
                    pinnedDevMode.Free();
                }
            }
            catch (Exception ex)
            {
                CuadroMensajes.Alertar("Operacion incompleta", "Se produjo el siguiente error:", ex.Message, "");
            }
        }

        private void CargarTamañosPapelImpresora()
        {
            List<System.Drawing.Printing.PaperSize> listaTam = new List<System.Drawing.Printing.PaperSize>();
            foreach (System.Drawing.Printing.PaperSize p in AjustesImpresion.PaperSizes)
            {
                listaTam.Add(p);
            }
            LTamaños = new ObservableCollection<System.Drawing.Printing.PaperSize>(listaTam);
        }
    }
}
