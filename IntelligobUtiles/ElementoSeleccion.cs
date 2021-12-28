using System;
using System.Linq;

namespace Intelligob.Utiles
{
    public class ElementoSeleccion
    {
        public int Id
        { get; set; }

        public int Clave
        { get; set; }

        public string Codigo
        { get; set; }

        public string Denominacion
        { get; set; }

        public ElementoSeleccion()
        { }

        public ElementoSeleccion(int pId, int pClave, string pCodigo, string pDenominacion)
        {
            this.Id = pId;
            this.Clave = pClave;
            this.Codigo = pCodigo;
            this.Denominacion = pDenominacion;
        }

        public ElementoSeleccion(int pId, String pDenominacion)
        {
            this.Id = pId;
            this.Denominacion = pDenominacion;
        }
    }
}
