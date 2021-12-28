using System;
using System.Linq;

namespace Intelligob.Entidades
{
    public partial class AguaPotable : ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
