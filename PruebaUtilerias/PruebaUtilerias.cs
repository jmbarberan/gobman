using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PruebaUtilerias
{
    [TestClass]
    public class PruebaUtilerias
    {
        [TestMethod]
        public void ProbarEnum()
        {            
            /**/

            Intelligob.Utiles.EntidadesEnum f = (Intelligob.Utiles.EntidadesEnum)Enum.GetValues(typeof(Intelligob.Utiles.EntidadesEnum)).GetValue(1);

            String descripcion = String.Empty;

            System.Reflection.FieldInfo fieldInfo = f.GetType().GetField(f.ToString());
            System.ComponentModel.DescriptionAttribute[] attributos = (System.ComponentModel.DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);

            if (attributos != null && attributos.Length > 0)
            {
                descripcion = attributos[0].Description;
            }
            Assert.IsTrue(descripcion == "Contribuyentes");

            //Assert.IsTrue(f == Intelligob.Utiles.EntidadesEnum.EnContribuyentes);
        }
    
        [TestMethod]
        public void ProbarTextoTabla()
        {
            Intelligob.Utiles.TablaCadena tb = new Intelligob.Utiles.TablaCadena();

            String titulo =   "Modificar Contribuyente\r\n";
            titulo = titulo + "=======================\r\n";

            tb.AddRow("Atributo", "Original", "Modificado");
            tb.AddRow("--------", "--------", "----------");
            tb.AddRow("Id", "1", "1");
            tb.AddRow("Personeria ID", "111", "111");
            tb.AddRow("Personeria", "Natural", "Natural");
            tb.AddRow("Nombres", "Martin Barberan", "Martin Barberan");
            tb.AddRow("Cedula", "0912539069", "0912639069");
            tb.AddRow("Direccion", "", "El Fortin Blq. 9");
            tb.AddRow("Estado", "0", "0");

            String s = tb.Output();
            s = titulo + s;
            Assert.IsTrue(s.Length > 0);
        }
    }
}
