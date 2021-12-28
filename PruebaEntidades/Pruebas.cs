using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PruebaEntidades
{
    [TestClass]
    public class Pruebas
    {
        [TestMethod]
        public void CrearCarpetaCatastral()
        {
            Intelligob.Entidades.CarpetaCatastralAnual car = new Intelligob.Entidades.CarpetaCatastralAnual();
            car.Codigo = "1-2-2-5";
            Assert.IsTrue(car.Codigo == "2005");
        }

        [TestMethod]
        public void ConsultarAvaluosXcodigo()
        {
            
        }

        [TestMethod]
        public void ConsultarEntidades()
        {
            /*System.Text.StringBuilder strbuf = new System.Text.StringBuilder();
            System.Reflection.Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();

            foreach (System.Reflection.Assembly assembly in assemblies)
            {
                string name = assembly.FullName;
                strbuf.Append(name).Append("\r\n");

                // Check if the current assembly
                // is marked with the EnhancedExAttribute.
                object[] customAttributes = assembly.GetCustomAttributes(typeof(Telerik.OpenAccess.RT.EnhancedExAttribute), false);

                if (customAttributes.Length == 1)
                {
                    strbuf.Append("  Enhanced!\r\n");
                    // Print all persistent capable classes in the assembly.
                    foreach (Type t in assembly.GetTypes())
                        if (typeof(Telerik.OpenAccess.SPI.dataobjects.PersistenceCapable)
                                .IsAssignableFrom(t))
                            strbuf.Append("  Persistent Type: ")
                                    .Append(t.FullName).Append("\r\n");
                }
            }*/

            IQueryable<Intelligob.Entidades.ConceptosDocumento> consulta = null;
            using (Intelligob.Entidades.Modelo contexto = new Intelligob.Entidades.Modelo("character set=ISO8859_1;data source=localhost;initial catalog=intelligob;user id=SYSDBA;password=masterkey"))
            {
                consulta = (from concepto in contexto.ConceptosDocumentos select concepto).Distinct();
            }

            //string res = strbuf.ToString();
            Assert.IsNotNull(consulta);
        }
    }
}
