using System;
using System.Linq;

namespace Intelligob.Entidades
{
    public partial class AguaServicio : ICloneable
	{
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
