using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intelligob.Cliente.Modelos
{
    public class CobroRap
    {
        public CobroRap()
        {
            Id = 0;
            Indice = 0;
            Referencia = 0;
            Fecha = DateTime.Today;
            Numero = 0;
            Mes = 1;
            Valor = 0;
            Adicional = 0;
            Denominacion = String.Empty;
        }

        public CobroRap(int numero, double valor)
        {
            Id = 0;
            Indice = 0;
            Referencia = 0;
            Fecha = DateTime.Today;
            Numero = numero;
            Mes = 1;
            Valor = valor;
            Adicional = 0;
            Denominacion = String.Empty;
        }

        public CobroRap(int numero, double valor, int mes)
        {
            Id = 0;
            Indice = 0;
            Referencia = 0;
            Fecha = DateTime.Today;
            Numero = numero;
            Mes = mes;
            Valor = valor;
            Adicional = 0;
            Denominacion = string.Empty;
        }

        public CobroRap(int numero, double valor, string denominacion)
        {
            Id = 0;
            Indice = 0;
            Referencia = 0;
            Fecha = DateTime.Today;
            Numero = numero;
            Mes = 1;
            Valor = valor;
            Adicional = 0;
            Denominacion = denominacion;
        }

        public int Id { get; set; }
        public int Indice { get; set; }
        public int Referencia { get; set; }
        public DateTime Fecha { get; set; }
        public int Numero { get; set; }
        public int Mes { get; set; }
        public Double Valor { get; set; }
        public Double Adicional { get; set; }
        public String Denominacion { get; set; }
    }
}
