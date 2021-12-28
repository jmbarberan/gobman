using System;
using System.Linq;

namespace Intelligob.Escritorio.ModeloVista.General
{
    public class CapturaTextoVM : BaseMV<Vistas.Interfaces.IVentanaDialogo>
    {
        private string titulo = "Comentarios";
        public string Titulo
        {
            get { return titulo; }
            set { this.titulo = value; OnPropertyChanged("Titulo"); }
        }

        private string texto = String.Empty;
        public String Texto
        {
            get { return texto; }
            set { this.texto = value; OnPropertyChanged("Texto"); }
        }

        public System.Windows.Input.ICommand CmdAceptar
        { get; internal set; }

        public CapturaTextoVM(String pTitulo) : base(new Vistas.General.CapturaTexto())
        {
            this.titulo = pTitulo;
            this.CmdAceptar = new Comandos.ComandoDelegado((c) => AceptarAccion(), (c) => AceptarHabilita());
        }

        private void AceptarAccion()
        {
            this.Vista.DialogResult = true;
            this.Vista.Close();
        }

        private Boolean AceptarHabilita()
        {
            return this.Texto.Length > 0;
        }
    }
}
