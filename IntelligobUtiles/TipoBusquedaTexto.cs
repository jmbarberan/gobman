using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Intelligob.Utiles
{
    public enum TipoBusquedaTexto
    {
        [Description("Comenzando con")]
        tbComenzando,
        [Description("Conteniendo")]
        tbConteniendo,
        [Description("Igual a")]
        tbIgual
    }

    public static class EnumTipoBusquedaTexto
    {
        public static List<KeyValuePair<string, TipoBusquedaTexto>> TraerLista()
        {
            List<KeyValuePair<string, TipoBusquedaTexto>> tipoBusquedaLista = new List<KeyValuePair<string, TipoBusquedaTexto>>();
            foreach (TipoBusquedaTexto level in Enum.GetValues(typeof(TipoBusquedaTexto)))
            {
                string description;
                FieldInfo fieldInfo = level.GetType().GetField(level.ToString());
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes != null && attributes.Length > 0) { description = attributes[0].Description; }
                else { description = string.Empty; }
                KeyValuePair<string, TipoBusquedaTexto> typeKeyValue =
                new KeyValuePair<string, TipoBusquedaTexto>(description, level);
                tipoBusquedaLista.Add(typeKeyValue);
            }
            return tipoBusquedaLista;
        }
    }
  
}
