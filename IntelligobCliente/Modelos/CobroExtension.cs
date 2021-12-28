using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Intelligob.Cliente.Referencia
{
    public class CobroExtension
    {
        private ObservableCollection<object> planillasSeleccionadas;
        public ObservableCollection<object> PlanillasSeleccionadas
        {
            get
            {
                if (planillasSeleccionadas == null)
                {
                    planillasSeleccionadas = new ObservableCollection<object>();
                }
                return planillasSeleccionadas;
            }
        }
    }
}
