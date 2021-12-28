using System;
using System.Linq;

namespace Intelligob.Cliente.Referencia
{
    public partial class CajaComprobanteDto
    {
        private TablaClaveDto categoriaNav;
        public TablaClaveDto CategoriaNav
        {
            get 
            {
                if (this.categoriaNav == null)
                {
                    if (this.Categoria != null && this.Categoria > 0)
                    {
                        categoriaNav = ModeloCache.Instance.McClaves.Where(c => c.Id == this.Categoria).FirstOrDefault();
                    }
                }
                return categoriaNav;
            }
        }
    }
}
