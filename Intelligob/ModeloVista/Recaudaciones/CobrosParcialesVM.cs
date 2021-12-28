using Intelligob.Cliente.Referencia;
using Intelligob.Escritorio.Vistas.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Intelligob.Escritorio.ModeloVista.Recaudaciones
{
    public class CobrosParcialesVM : BaseMV<IVentanaDialogo>
    {
        private ObservableCollection<PlanillaDto> lPlanillas = new ObservableCollection<PlanillaDto>();
        public ObservableCollection<PlanillaDto> LPlanillas
        {
            get { return lPlanillas; }
            set { this.lPlanillas = value; OnPropertyChanged("LPlanillas"); }
        }

        private PlanillaDto seleccionado;
        public PlanillaDto Seleccionado
        {
            get { return this.seleccionado; }
            set { this.seleccionado = value; OnPropertyChanged("Seleccionado"); }
        }

        public System.Windows.Input.ICommand CmdAceptar { get; internal set; }

        public CobrosParcialesVM(List<PlanillaDto> pls) : base(new Intelligob.Escritorio.Vistas.Recaudaciones.CobrosParciales()) 
        {
            if (pls != null && pls.Count > 0)
                lPlanillas = new ObservableCollection<PlanillaDto>(pls);
            this.CmdAceptar = new Comandos.ComandoDelegado((o) => AceptarAccion());
            this.IniciarPagos();
            this.Vista.ShowDialog();
        }

        private void IniciarPagos()
        {
            if (LPlanillas.Count > 0)
            {
                foreach (PlanillaDto p in LPlanillas)
                {
                    p.Parcial = p.Saldo;
                }
            }
        }

        private void AceptarAccion()
        {
            this.Vista.DialogResult = true;
            this.Vista.Close();
        }

    }
}
