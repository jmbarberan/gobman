using System;
using System.Linq;
using System.Collections.Generic;
using Intelligob.Cliente.Referencia;
using Intelligob.Cliente;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Intelligob.Cliente.Depositos;
using Intelligob.Escritorio.ModeloVista.Comandos;
using Intelligob.Escritorio.Vistas.Interfaces;

namespace Intelligob.Escritorio.ModeloVista.General
{
    public class ContribuyenteRebajasVM : BaseMV<IVista>
    {
        private readonly ContribuyentesDep contribuyentesDep = new ContribuyentesDep();
        private readonly int contribuyenteId = 0;

        private readonly ObservableCollection<RebajaDto> lrebajas = new ObservableCollection<RebajaDto>(ModeloCache.Instance.McRebajas.Where(r => r.Estado == 0));

        readonly Action ocultarModal;

        public ObservableCollection<RebajaDto> LRebajas
        {
            get { return lrebajas; }
        }

        private ObservableCollection<ContribuyentesRebajaDto> lrebajasContribuyente = new ObservableCollection<ContribuyentesRebajaDto>();
        public ObservableCollection<ContribuyentesRebajaDto> LRebajasContribuyente
        {
            get { return this.lrebajasContribuyente; }
            set { this.lrebajasContribuyente = value; OnPropertyChanged("LRebajasContribuyente"); }
        }

        private readonly List<ContribuyentesRebajaDto> lrebajasEliminadas = new List<ContribuyentesRebajaDto>();

        private RebajaDto rebajaSeleccionada;
        public RebajaDto RebajaSeleccionada
        {
            get { return this.rebajaSeleccionada; }
            set { this.rebajaSeleccionada = value; OnPropertyChanged("RebajaSeleccionada"); }
        }

        private ContribuyentesRebajaDto rebajaConSeleccionada;
        public ContribuyentesRebajaDto RebajaConSeleccionada
        {
            get { return this.rebajaConSeleccionada; }
            set { this.rebajaConSeleccionada = value; OnPropertyChanged("RebajaConSeleccionada"); }
        }

        public ICommand CmdAgregar
        { get; internal set; }

        public ICommand CmdQuitar
        { get; internal set; }

        public ICommand CmdGuardar
        { get; internal set; }

        public ICommand CmdCancelar
        { get; internal set; }

        public ContribuyenteRebajasVM(int pContribuyente, Action pOcultar) : this(new Intelligob.Escritorio.Vistas.General.ContribuyenteRebajas(), pContribuyente, pOcultar) { }

        public ContribuyenteRebajasVM(Escritorio.Vistas.Interfaces.IVentanaDialogo vista, int pContribuyente)
            : base(vista)
        {
            this.contribuyenteId = pContribuyente;
            LRebajasContribuyente = new ObservableCollection<ContribuyentesRebajaDto>(contribuyentesDep.RebajasPorContribuyenteEstado(contribuyenteId, 0));
            CrearComandos();
            ((IVentanaDialogo)this.Vista).Owner = App.Current.MainWindow;
            ((IVentanaDialogo)this.Vista).ShowDialog();
        }

        public ContribuyenteRebajasVM(Escritorio.Vistas.Interfaces.IControlUsuario vista, int pContribuyente, Action pOcultar)
            : base(vista)
        {
            this.contribuyenteId = pContribuyente;
            LRebajasContribuyente = new ObservableCollection<ContribuyentesRebajaDto>(contribuyentesDep.RebajasPorContribuyenteEstado(contribuyenteId, 0));
            CrearComandos();
            this.ocultarModal = pOcultar;
        }

        private void CrearComandos()
        {
            this.CmdAgregar = new ComandoDelegado((o) => AgregarAccion(), (o) => HabilitaAgregar());
            this.CmdQuitar = new ComandoDelegado((o) => QuitarAccion(), (o) => HabilitaQuitar());
            this.CmdGuardar = new ComandoDelegado((o) => GuardarAccion());
            this.CmdCancelar = new ComandoDelegado((o) => CancelarAccion());
        }

        private bool HabilitaAgregar()
        {
            return this.RebajaSeleccionada != null;
        }

        private bool HabilitaQuitar()
        {
            return this.RebajaConSeleccionada != null;
        }

        private void AgregarAccion()
        {
            Boolean b = false;
            foreach (ContribuyentesRebajaDto ri in LRebajasContribuyente)
            {
                if (ri.Rebaja == RebajaSeleccionada.Id)
                {
                    b = true;
                    break;
                }
            }
            if (!b)
            {
                ContribuyentesRebajaDto r = new ContribuyentesRebajaDto();
                r.Id = 0;
                r.Contribuyente = this.contribuyenteId;
                r.Rebaja = RebajaSeleccionada.Id;
                r.Fraccion = 0;
                r.Estado = 0;
                r.RebajaNav = ModeloCache.Instance.McRebajas.Where(rb => rb.Id == RebajaSeleccionada.Id).FirstOrDefault();
                LRebajasContribuyente.Add(r);
            }
        }

        private void QuitarAccion()
        {
            if (RebajaConSeleccionada != null)
            {
                if (RebajaConSeleccionada.Id > 0)
                    lrebajasEliminadas.Add(RebajaConSeleccionada);
                LRebajasContribuyente.Remove(RebajaConSeleccionada);
                RebajaConSeleccionada = null;
            }
        }

        private void GuardarAccion()
        {
            if (LRebajasContribuyente.Count > 0)
            {
                foreach (ContribuyentesRebajaDto reb in LRebajasContribuyente)
                {
                    if (reb.Id > 0)
                    {
                        contribuyentesDep.RebajaContribuyenteActualizar(reb);
                    }
                    else
                    {
                        contribuyentesDep.RebajaContribuyenteCrear(reb);
                    }
                }
            }
            if (lrebajasEliminadas.Count > 0)
            {
                contribuyentesDep.RebajaContribuyenteEliminar(lrebajasEliminadas.ToArray());
            }
            this.ocultarModal();
        }

        private void CancelarAccion()
        {
            if (this.ocultarModal != null)
                this.ocultarModal();
        }
    }
}
