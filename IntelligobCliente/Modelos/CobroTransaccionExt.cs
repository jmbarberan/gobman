using System;
using System.Linq;

namespace Intelligob.Cliente.Referencia
{
    public partial class CobroTransaccionDto
    {
        private PlanillaDto planillaNav;
        public PlanillaDto PlanillaNav
        {
            get { return this.planillaNav; }
            set { this.planillaNav = value; }
        }
    }
}
