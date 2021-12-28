using Intelligob.Datos.Contenedores;
using Intelligob.Datos.Referencia1;
using Intelligob.Utiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Intelligob.Datos.Depositos
{
    public class ContribuyentesDep : DepositoBase
    {
        public ContribuyentesDep() : base(DepositosControl.Instancia.Servicio) { }

        public ContribuyentesDep(IContenedor pServicio)
            : base(pServicio)
        { }

        /* Traer Contribuyente Por Id
        Integer -> Contribuyente
        Recibe un ID de un contribuyente y lo retorna como resultado en caso de no encontrar coincidencia el ID = -1*/
        public Contribuyente CtbTraerPorId(int? pId)
        {

            Contribuyente cEncontrado = null;
                try
                { cEncontrado = Servicio.QContribuyentes.Where(c => c.Id == pId).FirstOrDefault(); }
                catch (Exception ex)
                { throw ex; }
            
            return cEncontrado;

        }

        /* Nuevo        
        Contribuyente -> Integer
        Recibe un objeto Contribuyente (ya validado) lo persiste y retorna el Id insertado */
        public int? CtbNuevo(Contribuyente con)
        {
            //contexto.Add(con);
            Guardar();
            return con.Id;
        }

        /* Modificar 
        Contribuyente -> Nada
        Recibe un Contribuyente modificado y persiste los cambios */
        public void CtbModificar(Contribuyente con)
        {
            try
            {
                Guardar();
            }
            catch (Exception ex) { throw ex; }
        }

        /* Modificar Estado
        Integer -> Nada
        Recibe el Id de un contribuyente y actualiza Estado a el valor del argumente Estado recibido a dicho contribuyente (Eliminacion/Restauracion logica)*/
        public void CtbModificarEstado(int pId, int pEstado)
        {
            try
            {
                Contribuyente conEliminado = Servicio.QContribuyentes.Where(c => c.Id == pId).FirstOrDefault();
                conEliminado.Estado = pEstado;
                Guardar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /* Buscar por Nombre 
        String, Integer, Integer -> List<Contribuyente>
        Retornar todos los contribuyentes cuyo nombre coincide con el tipo de busqueda a la cadena recibida, la cadena recibida 
        y el estado si es filtrado */
        public List<Contribuyente> CtbBuscarPorNombres(String pNombre, TipoBusquedaTexto pTipoCoincidencia, int? pEstado)
        {
            switch (pTipoCoincidencia)
            {
                case TipoBusquedaTexto.tbComenzando: // Comienza con
                    {
                        if (pEstado < 9)
                            return Servicio.QContribuyentes.Where(c => (c.Estado == pEstado && c.Nombres.StartsWith(pNombre))).ToList();
                        else
                            return Servicio.QContribuyentes.Where(c => c.Nombres.StartsWith(pNombre)).ToList();
                    }
                case TipoBusquedaTexto.tbIgual: // Exactamente igual
                    {
                        if (pEstado < 9)
                            return Servicio.QContribuyentes.Where(c => (c.Estado == pEstado && c.Nombres == pNombre)).ToList();
                        else
                            return Servicio.QContribuyentes.Where(c => c.Nombres == pNombre).ToList();
                    }
                default: // Contiene
                    {
                        pNombre = pNombre.Trim();
                        pNombre = Regex.Replace(pNombre, @"\s+", "%");
                        if (!pNombre.StartsWith("%"))
                        {
                            pNombre = "%" + pNombre;
                        }
                        if (!pNombre.EndsWith("%"))
                        {
                            pNombre = pNombre + "%";
                        }
                        if (pEstado < 9)
                            return Servicio.QContribuyentes.Where(c => (c.Estado == pEstado && c.Nombres.Contains(pNombre))).ToList();
                        else
                            return Servicio.QContribuyentes.Where(c => c.Nombres.Contains(pNombre)).ToList();
                    }
            }
        }

        /* Buscar por Cedula 
        String -> List<Contribuyente>
        Retornar todos los contribuyentes cuyos numero de cedula coincida con la cadena recibida */
        public List<Contribuyente> CtbBuscarPorCedula(String pCedula)
        {
            return Servicio.QContribuyentes.Where(c => c.Cedula == pCedula).ToList();
        }

        public List<Contribuyente> CtbBuscarPorCedula(String pCedula, int pEstado)
        {
            if (pEstado == 9)
                return Servicio.QContribuyentes.Where(c => c.Cedula == pCedula).ToList();
            else
                return Servicio.QContribuyentes.Where(c => c.Cedula == pCedula && c.Estado == pEstado).ToList();
        }

        /* Esta registrado el Nombre
        Contribuyente -> Boolean
        Consultar si un contribuyente ya esta registrado con el nombre recibido */
        public Boolean CtbNombreRegistrado(Contribuyente pContribuyente)
        {
            Boolean res = false;
            var enc = Servicio.QContribuyentes.Where(c => c.Nombres == pContribuyente.Nombres).ToList();
            if (enc.Count > 0)
            {
                foreach (Contribuyente c in enc)
                {
                    if (c.Id != pContribuyente.Id)
                    {
                        res = true;
                    }
                }
            }
            return res;
        }

        /* Esta regitrada cedula 
        Contribuyente -> Boolean
        Consultar si se encunetra registrado un contribuyente con la cedula de contribuyente recibido */
        public Boolean CtbCedulaRegistrada(Contribuyente pContribuyente)
        {
            Boolean res = false;
            List<Contribuyente> enc = this.Servicio.QContribuyentes.Where(c => c.Cedula == pContribuyente.Cedula).ToList();
            if (enc.Count() > 0)
            {
                foreach (Contribuyente c in enc)
                {
                    if (c.Id != pContribuyente.Id)
                    {
                        res = true;
                    }
                }
            }
            return res;
        }
    }
}
