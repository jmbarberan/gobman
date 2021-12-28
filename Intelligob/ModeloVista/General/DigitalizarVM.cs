using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.Imaging;


namespace Intelligob.Escritorio.ModeloVista.General
{
    public class DigitalizarVM : BaseMV<Escritorio.Vistas.Interfaces.IControlUsuario>
    {
        private Action<byte[]> ProcesarDigitalizacion;
        private Action OcultarAccion;
        public bool DigitalizacionCompleta = false;

        private System.Windows.Visibility progresoVisibilidad = System.Windows.Visibility.Collapsed;
        public System.Windows.Visibility ProgresoVisibilidad
        {
            get { return progresoVisibilidad; }
            set { progresoVisibilidad = value; OnPropertyChanged("ProgresoVisibilidad"); }
        }

        public ICommand CmdAdquirir { get; internal set; }
        public ICommand CmdCancelar { get; internal set; }

        private List<Utiles.Scanner> escaneres = new List<Utiles.Scanner>();
        public List<Utiles.Scanner> Escaneres
        {
            get { return this.escaneres; }
            set { this.escaneres = value; OnPropertyChanged("Escaneres"); }
        }

        private Utiles.Scanner seleccionado = null;
        public Utiles.Scanner Seleccionado
        {
            get { return this.seleccionado; }
            set { this.seleccionado = value; OnPropertyChanged("Seleccionado"); }
        }        

        public DigitalizarVM(Action pOcultarAccion) : base(new Vistas.General.Digitalizar())
        {
            Iniciar(pOcultarAccion);
        }

        public DigitalizarVM(Action pOcultarAccion, Action<byte[]> procesarDig) : base(new Vistas.General.Digitalizar())
        {
            this.ProcesarDigitalizacion = procesarDig;
            Iniciar(pOcultarAccion);
        }

        private void Iniciar(Action pOcultar)
        {
            this.OcultarAccion = pOcultar;
            CmdCancelar = new Comandos.ComandoDelegado((o) => CancelarAccion());
            CmdAdquirir = new Comandos.ComandoDelegado((o) => AdquirirAccion(), (O) => AdquirirHabilita());
            
            this.Escaneres = Utiles.Configuracion.Escanneres;
        }

        private void OnBackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var backgroundWorker = sender as BackgroundWorker;
            backgroundWorker.DoWork -= this.OnBackgroundWorkerDoWork;
            backgroundWorker.RunWorkerCompleted -= OnBackgroundWorkerRunWorkerCompleted;            
            this.ProgresoVisibilidad = System.Windows.Visibility.Collapsed;            
            if (e.Result != null)
            {
                this.DigitalizacionCompleta = true;
                ProcesarDigitalizacion((byte[])e.Result);
            }
            this.OcultarAccion();
        }

        private byte[] DigitalizacionAsincronica()
        {
            if (Seleccionado != null)
            {
                var o = Seleccionado.Scan();
                if (o != null)
                    return (byte[])o.FileData.get_BinaryData();
            }

            return null;
        }

        private void OnBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = this.DigitalizacionAsincronica();
        }

        private Boolean AdquirirHabilita()
        {
            return this.Seleccionado != null;
        }

        private void CancelarAccion()
        {
            this.OcultarAccion();
        }

        private void AdquirirAccion()
        {            
            if (Seleccionado != null)
            {
                this.ProgresoVisibilidad = System.Windows.Visibility.Visible;
                var backgroundWorker = new BackgroundWorker();
                backgroundWorker.DoWork += this.OnBackgroundWorkerDoWork;
                backgroundWorker.RunWorkerCompleted += OnBackgroundWorkerRunWorkerCompleted;
                backgroundWorker.RunWorkerAsync();                
            }
        }

    }
}
