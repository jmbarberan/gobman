using System;
using System.Linq;

namespace Intelligob.Escritorio.ModeloVista.General
{
    public class CapturaNumeroVM : BaseMV<Vistas.Interfaces.IVentanaDialogo>
    {
        private string titulo = "Digite un numero";
        public string Titulo
        {
            get { return titulo; }
            set { this.titulo = value; OnPropertyChanged("Titulo"); }
        }

        private System.Windows.Visibility mostrarEntero = System.Windows.Visibility.Visible;
        public System.Windows.Visibility MostrarEntero
        {
            get { return mostrarEntero; }
            set { this.mostrarEntero = value; OnPropertyChanged("MostrarEntero"); }
        }

        private System.Windows.Visibility mostrarDoble = System.Windows.Visibility.Collapsed;
        public System.Windows.Visibility MostrarDoble
        {
            get { return mostrarDoble; }
            set { this.mostrarDoble = value; OnPropertyChanged("MostrarDoble"); }
        }

        private int entero = 0;
        public int Entero
        {
            get { return this.entero; }
            set { this.entero = value; OnPropertyChanged("Entero"); }
        }

        private double doble = 0;
        public double Doble    
        {
            get { return this.doble; }
            set { this.doble = value; OnPropertyChanged("Doble"); }
        }

        public System.Windows.Input.ICommand CmdAceptar
        { get; internal set; }

        public CapturaNumeroVM(String pTitulo) : base(new Vistas.General.CapturaNumero())
        {
            this.Titulo = pTitulo;
            this.CrearComandos();
            this.MostrarVista();
        }

        public CapturaNumeroVM(String pTitulo, Boolean entVisible, Boolean dobVisible)
            : base(new Vistas.General.CapturaNumero())
        {
            this.Titulo = pTitulo;
            this.CrearComandos();
            
            if (entVisible)
                this.MostrarEntero = System.Windows.Visibility.Visible;
            else
                this.mostrarEntero = System.Windows.Visibility.Collapsed;

            if (dobVisible)
                this.MostrarDoble = System.Windows.Visibility.Visible;
            else
                this.MostrarDoble = System.Windows.Visibility.Collapsed;

            this.MostrarVista();
        }

        private void CrearComandos()
        {
            this.CmdAceptar = new Comandos.ComandoDelegado((o) => AceptarAccion(), (o) => AceptarHabilita());
        }

        private void MostrarVista()
        {
            this.Vista.Owner = App.Current.MainWindow;
            this.Vista.ShowDialog();
        }

        private void AceptarAccion()
        {
            this.Vista.DialogResult = true;
            this.Vista.Close();
        }

        private Boolean AceptarHabilita()
        {
            bool ret = true;
            if (MostrarEntero == System.Windows.Visibility.Visible && Entero <= 0)
                ret = false;
            if (ret)
            {
                if (MostrarDoble == System.Windows.Visibility.Visible && Doble <= 0)
                    ret = false;
            }
            return ret;
        }
    }
}
