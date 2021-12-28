using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Intelligob.Cliente.Referencia
{
    public partial class PredioBloqueDto : INotifyPropertyChanged
    {
        private ObservableCollection<PredioPisoDto> pisosLista;
        public ObservableCollection<PredioPisoDto> PisosLista
        {
            get
            {
                if (this.pisosLista == null)
                {
                    if (this.PisosNav != null)
                        this.pisosLista = new ObservableCollection<PredioPisoDto>(this.PisosNav);
                    else
                        this.pisosLista = new ObservableCollection<PredioPisoDto>();
                }
                return this.pisosLista;
            }
            set
            {
                this.pisosLista = value;
                RaisePropertyChanged("PisosLista");
            }
        }
    }

    public partial class PredioPisoDto
    {
        private ObservableCollection<PredioConstruccionDto> construccionesLista;
        public ObservableCollection<PredioConstruccionDto> ConstruccionesLista
        {
            get 
            {
                if (this.construccionesLista == null)
                {
                    if (this.ConstruccionesNav != null)
                        this.construccionesLista = new ObservableCollection<PredioConstruccionDto>(this.ConstruccionesNav);
                    else
                        this.construccionesLista = new ObservableCollection<PredioConstruccionDto>();
                }
                return this.construccionesLista;                
            }
            set
            {
                this.construccionesLista = value;
                RaisePropertyChanged("ConstruccionesLista");
            }
        }
    }
}
