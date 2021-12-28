using System;
using System.Linq;

namespace Intelligob.Datos.Referencia1
{
    public partial class PredioBase
    {
        public string PropietariosCadena
        {
            get 
            {
                string nom = "";
                if (PropietariosNav.Count > 0 && PropietariosNav[0].ContribuyenteNav != null)
                {
                    foreach(PredioPropietario p in PropietariosNav)
                    {
                        if (nom.Length > 0)
                            nom = nom + ", " + p.ContribuyenteNav.Nombres;
                        else
                            nom = nom + p.ContribuyenteNav.Nombres;
                    }
                    /*int c = PropietariosNav.Count() - 1;
                    string mas = "";
                    if (c > 2)
                    {
                        c = 2;
                        mas = " +";
                    }
                    for (int i = 0; i <= c; i++)
                    {
                        if (i == c)
                        {
                            nom = nom + PropietariosNav[i].ContribuyenteNav.Nombres;
                        }
                        else
                        {
                            nom = nom + PropietariosNav[i].ContribuyenteNav.Nombres + ", ";
                        }
                    }
                    nom = nom + mas;*/
                }
                return nom;
            }
        }
    }
}
