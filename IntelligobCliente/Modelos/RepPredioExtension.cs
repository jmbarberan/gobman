using System;
using System.Linq;

namespace Intelligob.Cliente.Referencia
{
    public partial class RepPredioDto
    {
        private TablaClaveDto dominioNav;
        public TablaClaveDto DominioNav
        {
            get 
            {
                if (dominioNav != null)
                {
                    return this.dominioNav;
                }
                else
                {
                    if (this.Dominio != null && this.Dominio > 0)
                        this.dominioNav = ModeloCache.Instance.McClaves.Where(o => o.Id == this.Dominio).FirstOrDefault();
                }
                return this.dominioNav;
            }
        }

        private TablaClaveDto tipoPropiedadNav;
        public TablaClaveDto TipoPropiedadNav
        {
            get
            {
                if (tipoPropiedadNav != null)
                {
                    return this.tipoPropiedadNav;
                }
                else
                {
                    if (this.TipoPropiedad != null && this.TipoPropiedad > 0)
                        this.tipoPropiedadNav = ModeloCache.Instance.McClaves.Where(o => o.Id == this.TipoPropiedad).FirstOrDefault();                    
                }
                return this.tipoPropiedadNav;
            }
        }
    }
}
