using System;
using System.Linq;

namespace Intelligob.Entidades
{
    public partial class Contribuyente : ICloneable
	{
        public String Presentacion
        {
            get
            {
                string c = "";
                if (this.Cedula.Length > 0)
                {
                    c = " (" + this.Cedula + ")";
                }
                return this.Nombres + c;
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
