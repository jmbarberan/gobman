using System.Windows.Input;
using Intelligob.Cliente.Depositos;
using Intelligob.Cliente.Referencia;
using Intelligob.Escritorio.Vistas.General;
using Intelligob.Escritorio.Vistas.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Intelligob.Escritorio.ModeloVista.General
{
    public class ContribuyentesUnificarVM : BaseMV<IVista>
    {
        private readonly ContribuyentesDep contribuyentesDep = new ContribuyentesDep();

        private readonly ContribuyenteDto conPermanece;

        private readonly Action ocultarModal;

        public String PermaneceNombres
        {
            get { return this.conPermanece.Nombres; }
        }

        private ContribuyenteDto seleccionado;
        public ContribuyenteDto Seleccionado
        {
            get { return this.seleccionado; }
            set { this.seleccionado = value; OnPropertyChanged("Seleccionado"); }
        }

        private ObservableCollection<ContribuyenteDto> lContribuyentes = new ObservableCollection<ContribuyenteDto>();
        public ObservableCollection<ContribuyenteDto> LContribuyentes
        {
            get { return this.lContribuyentes; }
            set { this.lContribuyentes = value; OnPropertyChanged("LContribuyentes"); }
        }

        public ICommand CmdAgregar
        { get; internal set; }

        public ICommand CmdQuitar
        { get; internal set; }

        public ICommand CmdUnificar
        { get; internal set; }

        public ICommand CmdCancelar
        { get; internal set; }

        public ContribuyentesUnificarVM(ContribuyenteDto con, Action pOcultar) : this(con, new ContribuyentesUnificar(), pOcultar) { }

        public ContribuyentesUnificarVM(ContribuyenteDto con, IControlUsuario vista, Action pOcultar) : base(vista)
        {
            this.conPermanece = con;
            this.CmdAgregar = new Comandos.ComandoDelegado((o) => AgregarAccion());
            this.CmdQuitar = new Comandos.ComandoDelegado((o) => QuitarAccion(), (o) => QuitarHabilita());
            this.CmdUnificar = new Comandos.ComandoDelegado((o) => UnificarAccion(), (o) => UnificarHabilita());
            this.ocultarModal = pOcultar;
        }

        private void AgregarAccion()
        {                        
            SeleccionarContribuyenteVM sc = new SeleccionarContribuyenteVM();
            if (sc.Vista.DialogResult == true)
            {
                bool b = false;
                foreach (ContribuyenteDto con in LContribuyentes)
                {
                    if (con.Id == sc.Seleccionado.Id)
                    {
                        b = true;
                        break;
                    }
                }
                if (sc.Seleccionado.Id == this.conPermanece.Id)
                    b = true;
                if (!b)
                    this.LContribuyentes.Add(sc.Seleccionado);
            }
        }

        private void QuitarAccion()
        {
            LContribuyentes.Remove(Seleccionado);
        }

        private void UnificarAccion()
        {
            TaskDialogInterop.TaskDialogResult res = Intelligob.Utiles.CuadroMensajes.Preguntar("Unificar contribuyentes", "Confirme esta operacion", "La unificacion de contribuyentes es irreversible ¿Seguro de continuaar?"); 
            if (res.CustomButtonResult == 0)
            {
                contribuyentesDep.UnificarContribuyentes(this.conPermanece.Id, LContribuyentes.ToList());
                this.ocultarModal();
            }
        }

        private void CancelarAccion()
        {
            this.ocultarModal();
        }

        private bool QuitarHabilita()
        {
            return this.Seleccionado != null;
        }

        private bool UnificarHabilita()
        {
            return this.LContribuyentes.Count > 0;
        }
    }
}
