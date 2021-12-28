using System;
using System.Linq;

namespace Intelligob.Cliente.Referencia
{
    public partial class PredioBaseDto
    {
        public string PropietariosCadena
        {
            get
            {
                string nom = "";
                if (PropietariosNav != null && PropietariosNav.Length > 0 && PropietariosNav[0].ContribuyenteNav != null)
                {
                    foreach (PredioPropietarioDto p in PropietariosNav)
                    {
                        if (nom.Length > 0)
                            nom = nom + ", " + p.ContribuyenteNav.Nombres;
                        else
                            nom = nom + p.ContribuyenteNav.Nombres;
                    }
                }
                return nom;
            }
        }
    }
}
