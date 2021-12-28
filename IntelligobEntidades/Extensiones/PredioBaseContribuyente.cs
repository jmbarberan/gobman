using System;
using System.Linq;

namespace Intelligob.Entidades
{
    public partial class PredioBase
    {
        public string NombresPropietario
        {
            get 
            {
                string nom = "";
                if (PropietariosNav.Count > 0 && PropietariosNav[0].ContribuyenteNav != null)
                {
                    int c = PropietariosNav.Count() - 1;
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
                            nom = nom + PropietariosNav[i].ContribuyenteNav.Nombres + "; ";
                        }
                    }
                    nom = nom + mas;
                }
                return nom;
            }
        }
    }
}
