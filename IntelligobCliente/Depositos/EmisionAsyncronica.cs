using System;
using System.Linq;
using System.Threading;

namespace Intelligob.Cliente.Depositos
{    
    public class EmisionAsyncronica : System.ComponentModel.BackgroundWorker
    { 
        static readonly EmisionAsyncronica mInstance = new EmisionAsyncronica();
        public static EmisionAsyncronica Instance
        {
            get { return mInstance; }
        }

        public bool Ocupado = false;

        public EmisionAsyncronica()
        {
            this.WorkerReportsProgress = true;
            this.WorkerSupportsCancellation = true;
        }

        protected override void OnDoWork(System.ComponentModel.DoWorkEventArgs e)
        {            
            base.OnDoWork(e);
            int concepto = (int)e.Argument;
            ConceptosDep c = new ConceptosDep();
            c.MarcarEmitiendoPorId(concepto, true);
            EmisionesDep d = new EmisionesDep();
            d.EmisionGeneralPorConcepto(concepto);            
            Thread.Sleep(1000);
        }

        protected override void OnRunWorkerCompleted(System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            Utiles.CuadroMensajes.Aceptar("Emision general", "Operacion completa", "La emision se ha completado satisfactoriamente", "", TaskDialogInterop.VistaTaskDialogIcon.Information);
            base.OnRunWorkerCompleted(e);
        }
    }
}
