using Intelligob.Datos.Contenedores;
using Intelligob.Datos.Referencia1;
using Intelligob.Utiles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intelligob.Datos.Depositos
{
    public class CatastrosDep : DepositoBase
    {
        public CatastrosDep() : this(DepositosControl.Instancia.Servicio) { }

        public CatastrosDep(IContenedor pcon) : base(pcon) { }

        public IEnumerable<PredioBase> PrediosPorEstado(int pEstado)
        {
            IEnumerable<PredioBase> res;
            if (pEstado == 9)
            {
                res = this.Servicio.QPredios;
            }
            else
            {
                res = this.Servicio.QPredios.Where(p => p.Estado == pEstado).ToList();
            }
            return res;
        }

        /* Traer predios por contribuyente
        integer -> List<PredioBase>
        Traer los predios perteneciente a un contribuyente*/
        public List<PredioBase> PreTraerPorContribuyente(int pId, int pEstado, int pTipoPredio)
        {
            List<PredioBase> res = new List<PredioBase>();
            // Traer los PredioPropietario asociados al propietario buscado
            List<PredioPropietario> pros = this.Servicio.QPredioPropietarios.Where(p => p.Contribuyente == pId).ToList();
            foreach (PredioPropietario pro in pros)
            {
                // traer los datos de cada PredioBase encontrado en los propietarios
                PredioBase pb = new PredioBase();
                pb.Id = -1;
                if (pEstado == 9)
                {
                    if (pTipoPredio > 0)
                        pb = this.Servicio.QPredios.Where(p => p.Id == pro.Predio && p.FormatoCodigo == pTipoPredio).Single();
                    else
                        pb = this.Servicio.QPredios.Where(p => p.Id == pro.Predio).Single();
                }
                else
                {
                    if (pTipoPredio > 0)
                        pb = this.Servicio.QPredios.Where(p => p.Id == pro.Predio && p.Estado == pEstado && p.FormatoCodigo == pTipoPredio).Single();
                    else
                        pb = this.Servicio.QPredios.Where(p => p.Id == pro.Predio && p.Estado == pEstado).Single();
                }
                if (pb.Id > 0)
                {
                    res.Add(pb);
                }
            }
            foreach (PredioBase pre in res)
            {
                IEnumerable<PredioPropietario> lpro = this.Servicio.QPredioPropietarios.Where(p => p.Predio == pre.Id);
                foreach (PredioPropietario pro in lpro)
                {
                    pre.PropietariosNav.Add(pro);
                }
            }
            return res;
        }

        public List<PredioBase> PreTraerPorCodigo(String pCodigo, int pEstado, TipoBusquedaTexto pTipoBusqueda)
        {
            return this.PreTraerPorCodigo(pCodigo, pEstado, pTipoBusqueda, 0);
        }
        
        /* Traer predios por codigo
        String -> List<PredioBase>
        Traer los predios cuyo codigo coinciden con la cadena recibida (Patron de codigo)
        Se puede usar comodines para traer solo los predios cierto sector/manzana/poligono/etc. */
        public List<PredioBase> PreTraerPorCodigo(String pCodigo, int pEstado, TipoBusquedaTexto pTipoBusqueda, int pTipoPredio)
        {
            List<PredioBase> res = new List<PredioBase>();
            if (pTipoPredio == 0)
            {
                switch (pTipoBusqueda)
                {
                    case TipoBusquedaTexto.tbComenzando :
                        {
                            if (pEstado == 9)
                            {
                                res = this.Servicio.QPredios.Where(p => p.Codigo.Contains(pCodigo)).ToList();
                            }
                            else
                            {
                                res = this.Servicio.QPredios.Where(p => p.Codigo.Contains(pCodigo) && p.Estado == pEstado).ToList();
                            }
                            break;
                        }
                    case TipoBusquedaTexto.tbConteniendo :
                        {
                            if (pEstado == 9)
                            {
                                res = this.Servicio.QPredios.Where(p => p.Codigo.StartsWith(pCodigo)).ToList();
                            }
                            else
                            {
                                res = this.Servicio.QPredios.Where(p => p.Estado == p.Estado && p.Codigo.StartsWith(pCodigo)).ToList();
                            }
                            break;
                        }
                    default: // Codigo exacto
                        {
                            if (pEstado == 9)
                            {
                                res = this.Servicio.QPredios.Where(p => p.Codigo == pCodigo).ToList();
                            }
                            else
                            {
                                res = this.Servicio.QPredios.Where(p => p.Codigo == pCodigo && p.Estado == pEstado).ToList();
                            }
                            break;
                        }
                }
            }
            else
            {
                switch (pTipoBusqueda)
                {
                    case TipoBusquedaTexto.tbComenzando:
                        {
                            if (pEstado == 9)
                            {
                                res = this.Servicio.QPredios.Where(p => p.Codigo.Contains(pCodigo) && p.FormatoCodigo == pTipoPredio).ToList();
                            }
                            else
                            {
                                res = this.Servicio.QPredios.Where(p => p.Codigo.Contains(pCodigo) && p.FormatoCodigo == pTipoPredio && p.Estado == pEstado).ToList();
                            }
                            break;
                        }
                    case TipoBusquedaTexto.tbConteniendo:
                        {
                            if (pEstado == 9)
                            {
                                res = this.Servicio.QPredios.Where(p => p.Codigo.StartsWith(pCodigo) && p.FormatoCodigo == pTipoPredio).ToList();
                            }
                            else
                            {
                                res = this.Servicio.QPredios.Where(p => p.Codigo.StartsWith(pCodigo) && p.FormatoCodigo == pTipoPredio && p.Estado == p.Estado).ToList();
                            }
                            break;
                        }
                    default: // Codigo exacto
                        {
                            if (pEstado == 9)
                            {
                                res = this.Servicio.QPredios.Where(p => p.Codigo == pCodigo && p.FormatoCodigo == pTipoPredio).ToList();
                            }
                            else
                            {
                                res = this.Servicio.QPredios.Where(p => p.Codigo == pCodigo && p.FormatoCodigo == pTipoPredio && p.Estado == pEstado).ToList();
                            }
                            break;
                        }
                }
            }
            List<PredioPropietario> lpro = new List<PredioPropietario>();
            foreach(PredioBase pre in res)
            {
                lpro.Clear();
                lpro = Servicio.QPredioPropietarios.Where(p => p.Predio == pre.Id).ToList();
                foreach(PredioPropietario pro in lpro)
                {
                    pre.PropietariosNav.Add(pro);
                }
            }
            return res;
        }

        /* Traer por Id 
        integer -> PredioBase
        Traer un predio por su Id */
        public PredioBase PreTraerPorId(int pId)
        {
            return this.Servicio.QPredios.Where(p => p.Id == pId).FirstOrDefault();
        }

        public PredioTerreno TerrenoPorPredio(int pId)
        {
            return Servicio.QTerrenos.Where(t => t.Predio == pId).FirstOrDefault();
        }

        public IEnumerable<PredioPropietario> PropietariosPorPredio(int pId)
        {
            return Servicio.QPredioPropietarios.Where(p => p.Predio == pId);
        }

        public IEnumerable<PredioFrente> FrentesPorPredio(int  pId)
        {
            return this.Servicio.QPredioFrentes.Where(f => f.Predio == pId);
        }

        public IEnumerable<PredioFoto> FotosPorPredio(int pId)
        {
            return this.Servicio.QPredioFotos.Where(f => f.Predio == pId);
        }

        /// <summary>
        /// Traer los bloques y pisos de un predio con sus componentes de construccion
        /// </summary>
        /// <param name="pId">Id del predio</param>
        /// <returns>Bloques del predio</returns>
        public IEnumerable<PredioBloque> BloquesConstruccionPorPredio(int pId)
        {           
            IEnumerable<PredioBloque> bloques = BloquesPorPredio(pId);
            foreach (PredioBloque blq in bloques)
            {
                blq.PisosNav.Clear();
                foreach (PredioPiso p in PisosPorBloque(blq.Id).ToList())
                {
                    p.ConstruccionesNav.Clear();
                    foreach (PredioConstruccion c in ComponentesPorPiso(p.Id))
                    {
                        p.ConstruccionesNav.Add(c);
                    }
                    blq.PisosNav.Add(p);
                }                
            }
            return bloques;
        }

        public IEnumerable<PredioBloque> BloquesPorPredio(int pId)
        {
            return Servicio.QPredioBloques.Where(b => b.Predio == pId);
        }

        public IEnumerable<PredioPiso> PisosPorBloque(int pBloque)
        {
            return Servicio.QPredioPisos.Where(p => p.Bloque == pBloque);
        }

        public IEnumerable<PredioConstruccion> ComponentesPorPiso(int pPiso)
        {
            return Servicio.QPredioConstrucciones.Where(c => c.Piso == pPiso);
        }

        /* Nuevo predio
        PredioBase -> integer 
        Grabar nuevo PredioBase y devolver el Id del objeto insertado */
        public int PredioCrear(PredioBase pre)
        {
            try
            {
                this.Servicio.CrearEntidad(pre, "PredioBases");
                Guardar();
                return pre.Id;
            }
            catch (Exception ex)
            {
                CuadroMensajes.Alertar("Operacion incompleta", "Se ha producido el siguiente error:", ex.Message, "");
                return -1;
            }
        }

        public void TerrenoCrear(PredioTerreno ter)
        {
            try
            {
                this.Servicio.CrearEntidad(ter, "PredioTerrenos");
                Guardar();
            }
            catch (Exception ex)
            {
                CuadroMensajes.Alertar("Operacion incompleta", "Se ha producido el siguiente error:", ex.Message, "");
            }
        }

        public void TerrenoModificar(PredioTerreno ter)
        {
            try
            {
                this.Servicio.ActualizarEntidad(ter);
                this.Servicio.Guardar();
            }
            catch (Exception ex)
            {
                CuadroMensajes.Alertar("Operacion incompleta", "Se ha producido el siguiente error:", ex.Message, "");
            }
        }

        /* Modificar predio
        PredioBase -> nada
        Recibe un Predio modificado y guardar los cambios en la base de datos*/
        public void PredioModificar(PredioBase pre)
        {
            try 
            {
                this.Servicio.ModificarPredio(pre);
            }
            catch (Exception ex)
            {
                CuadroMensajes.Alertar("Operacion incompleta", "Se ha producido el siguiente error:", ex.Message, "");
            }
        }

        public void PreModificarEstado(PredioBase pre, int pEstado)
        {
            try
            {
                pre.Estado = pEstado;
                Servicio.ActualizarEntidad(pre);
                Servicio.Guardar();
            }
            catch (Exception ex)
            {
                CuadroMensajes.Alertar("Operacion incompleta", "Se ha producido el siguiente error:", ex.Message, "");
            }
        }

        /* Codigo registrado
        String, Integer, Integer -> Boolean
        Consulta en DB si el codigo recibido ya esta registrado con el mismo tipo (Urbano|Rustico) */
        public Boolean PreCodigoYaRegistrado(String pCodigo, int pId, int? pTipo)
        {
            Boolean ret = false;
            List<PredioBase> pres = new List<PredioBase>();
            pres = this.Servicio.QPredios.Where(p => p.Codigo == pCodigo && p.Id != pId && p.FormatoCodigo == pTipo).ToList();
            if (pres.Count > 0)
            {
                ret = true;
            }
            return ret;
        }

        /* Eliminar Terrenos
        PredioTerreno -> Nada
        Eliminar los terrenos recibidos */
        public void EliminarTerrenos(List<PredioTerreno> pTerrenos)
        {
            TablaCadena tb = new TablaCadena();
            tb.AddRow("Operaciones ejecutadas:", "", "");
            tb.AddRow("Eliminado:", "", "");
            foreach (PredioTerreno t in pTerrenos)
            {
                tb.AddRow("Tipo de objeto", "Predio/Terreno", "");
                tb.AddRow("Id:", t.Id, "");
                tb.AddRow("Atributo", "", "Original");
                tb.AddRow("--------", "", "--------");
                int? pre = 0;
                if (t.Predio == null)
                {
                    pre = t.Predio;
                }
                tb.AddRow("Predio", "", pre);
                tb.AddRow("Superficie", "", t.Superficie);
                tb.AddRow("Lindero norte", "", t.LinderoNorteNombres);
                tb.AddRow("Lindero sur", "", t.LinderoSurNombres);
                tb.AddRow("Lindero este", "", t.LinderoEsteNombres);
                tb.AddRow("Lindero oeste", "", t.LinderoOesteNombres);
                this.Servicio.EliminarEntidad(t);
            }
            Seguimiento s = new Seguimiento();
            s.Id = 0;
            s.Estado = 0;
            if (SesionUtiles.Instance.EsDesarrollador)
                s.Usuario = 0;
            else
                s.Usuario = SesionUtiles.Instance.UsuarioActivo.Id;
            s.Cliente = Configuracion.NombrePc;
            s.Direccion = Configuracion.IPLocal;
            s.Comentario = "Se elimino este terreno del predio";
            s.Original = tb.Output();
            this.Servicio.CrearEntidad(s, "Seguimientos");
        }

        /* Eliminar Propietarios
        PredioPropietario -> Nada 
        Eliminar los propietarios recibidos */
        public void EliminarPropietarios(List<PredioPropietario> pPropietarios)
        {
            TablaCadena tb = new TablaCadena();
            tb.AddRow("Operaciones ejecutadas:", "", "");
            tb.AddRow("Eliminado:", "", "");
            foreach (PredioPropietario p in pPropietarios)
            {
                tb.AddRow("Tipo de objeto", "Predio/Propietario", "");
                tb.AddRow("Id:", p.Id, "");
                tb.AddRow("Atributo", "", "Original");
                tb.AddRow("--------", "", "--------");
                int? pre = 0;
                if (p.Predio == null)
                {
                    pre = p.Predio;
                }
                tb.AddRow("Predio", "", pre);
                tb.AddRow("Contribuyente", "", p.Contribuyente);
                this.Servicio.EliminarEntidad(p);
            }
            Seguimiento s = new Seguimiento();
            s.Id = 0;
            s.Estado = 0;
            if (SesionUtiles.Instance.EsDesarrollador)
                s.Usuario = 0;
            else
                s.Usuario = SesionUtiles.Instance.UsuarioActivo.Id;
            s.Cliente = Configuracion.NombrePc;
            s.Direccion = Configuracion.IPLocal;
            s.Comentario = "Se quito este contribuyente como propietario del predio";
            s.Original = tb.Output();
            this.Servicio.CrearEntidad(s, "Seguimientos");
            this.Servicio.Guardar();
        }

        public void ModificarPropietarios(List<PredioPropietario> pPropietarios)
        {
            try
            {
                foreach (PredioPropietario p in pPropietarios)
                {
                    if (p.Id == 0)
                    {
                        this.Servicio.CrearEntidad(p, "PredioPropietarios");
                    }
                }
                this.Servicio.Guardar();
            }
            catch (Exception ex)
            {
                CuadroMensajes.Alertar("Operacion incompleta", "Se ha producido el siguiente error:", ex.Message, "");
            }
        }

        /* Eliminar Bloques
        PredioBloque -> Nada
        Eliminar los bloques recibidos */
        public void EliminarBloques(List<PredioBloque> pBloques)
        {
            TablaCadena tb = new TablaCadena();
            tb.AddRow("Operaciones ejecutadas:", "", "");
            tb.AddRow("Eliminado:", "", "");
            foreach (PredioBloque b in pBloques)
            {
                tb.AddRow("Tipo de objeto", "Predio/Bloque Cons.", "");
                tb.AddRow("Id:", b.Id, "");
                tb.AddRow("Atributo", "", "Original");
                tb.AddRow("--------", "", "--------");
                int? pre = 0;
                if (b.Predio == null)
                {
                    pre = b.Predio;
                }
                tb.AddRow("Predio", "", pre);
                tb.AddRow("Descripcion", "", b.Descripcion);
                this.Servicio.EliminarEntidad(b);
            }
            Seguimiento s = new Seguimiento();
            s.Id = 0;
            s.Estado = 0;
            if (SesionUtiles.Instance.EsDesarrollador)
                s.Usuario = 0;
            else
                s.Usuario = SesionUtiles.Instance.UsuarioActivo.Id;
            s.Cliente = Configuracion.NombrePc;
            s.Direccion = Configuracion.IPLocal;
            s.Comentario = "Se quito este bloque del predio";
            s.Original = tb.Output();
            this.Servicio.CrearEntidad(s, "Seguimientos");
            this.Servicio.Guardar();
        }

        public int CrearBloque(PredioBloque blq)
        {
            try
            {
                this.Servicio.CrearEntidad(blq, "PredioBloques");
                return blq.Id;
            }
            catch (Exception ex)
            {
                CuadroMensajes.Alertar("Operacion incompleta", "Se ha producido el siguiente error:", ex.Message, "");
                return -1;
            }
            
        }

        /* Eliminar Pisos
        PredioPiso -> Nada
        Eliminar los pisos recibidos */
        public void EliminarPisos(List<PredioPiso> pPisos)
        {
            TablaCadena tb = new TablaCadena();
            tb.AddRow("Operaciones ejecutadas:", "", "");
            tb.AddRow("Eliminado:", "", "");
            foreach (PredioPiso p in pPisos)
            {
                tb.AddRow("Tipo de objeto", "Predio/Piso Cons.", "");
                tb.AddRow("Id:", p.Id, "");
                tb.AddRow("Atributo", "", "Original");
                tb.AddRow("--------", "", "--------");
                int? blo = 0;
                if (p.Bloque == null)
                {
                    blo = p.Bloque;
                }
                tb.AddRow("Bloque Id", "", blo);
                tb.AddRow("Edad", "", p.EdadConstruccion);
                this.Servicio.EliminarEntidad(p);
            }
            Seguimiento s = new Seguimiento();
            s.Id = 0;
            s.Estado = 0;
            if (SesionUtiles.Instance.EsDesarrollador)
                s.Usuario = 0;
            else
                s.Usuario = SesionUtiles.Instance.UsuarioActivo.Id;
            s.Cliente = Configuracion.NombrePc;
            s.Direccion = Configuracion.IPLocal;
            s.Comentario = "Se quito este piso del predio";
            s.Original = tb.Output();
            this.Servicio.CrearEntidad(s, "Seguimientos");
            this.Guardar();
        }

        public int CrearPiso(PredioPiso piso)
        {
            try
            {
                this.Servicio.CrearEntidad(piso, "PredioPisos");
                return piso.Id;
            }
            catch (Exception ex)
            {
                CuadroMensajes.Alertar("Operacion incompleta", "Se ha producido el siguiente error:", ex.Message, "");
                return -1;
            }
        }

        /* Eliminar Frentes
        PredioFrente -> Nada
        Eliminar los frentes recibidos */
        public void EliminarFrentes(List<PredioFrente> pFrentes)
        {
            TablaCadena tb = new TablaCadena();
            tb.AddRow("Operaciones ejecutadas:", "", "");
            tb.AddRow("Eliminado:", "", "");
            foreach (PredioFrente f in pFrentes)
            {
                tb.AddRow("Tipo de objeto", "Predio/Frente", "");
                tb.AddRow("Id:", f.Id, "");
                tb.AddRow("Atributo", "", "Original");
                tb.AddRow("--------", "", "--------");
                int? pre = 0;
                if (f.Predio == null)
                {
                    pre = f.Predio;
                }
                tb.AddRow("Predio", "", pre);
                tb.AddRow("Extension", "", f.Superficie);
                this.Servicio.EliminarEntidad(f);
            }
            Seguimiento s = new Seguimiento();
            s.Id = 0;
            s.Estado = 0;
            if (SesionUtiles.Instance.EsDesarrollador)
                s.Usuario = 0;
            else
                s.Usuario = SesionUtiles.Instance.UsuarioActivo.Id;
            s.Cliente = Configuracion.NombrePc;
            s.Direccion = Configuracion.IPLocal;
            s.Comentario = "Se elimino este frente de predio";
            s.Original = tb.Output();
            this.Servicio.CrearEntidad(s, "Seguimientos");
            this.Servicio.Guardar();
        }

        public void ModificarFrentes(List<PredioFrente> pFrentes)
        {
            try 
            {
                foreach (PredioFrente f in pFrentes)
                {
                    if (f.Id > 0)
                    {
                        this.Servicio.ActualizarEntidad(f);
                    }
                    else
                    {
                        this.Servicio.CrearEntidad(f, "PredioFrentes");
                    }
                }
                this.Servicio.Guardar();
            }
            catch (Exception ex)
            {
                CuadroMensajes.Alertar("Operacion incompleta", "Se ha producido el siguiente error:", ex.Message, "");
            }
        }

        /* Eliminar Fotos
        PredioFoto -> Nada
        Eliminar las fotos recibidas */
        public void EliminarFotos(List<PredioFoto> pFotos)
        {
            TablaCadena tb = new TablaCadena();
            tb.AddRow("Operaciones ejecutadas:", "", "");
            tb.AddRow("Eliminado:", "", "");
            foreach (PredioFoto f in pFotos)
            {
                tb.AddRow("Tipo de objeto", "Predio/Foto", "");
                tb.AddRow("Id:", f.Id, "");
                tb.AddRow("Atributo", "", "Original");
                tb.AddRow("--------", "", "--------");
                int? pre = 0;
                if (f.Predio == null)
                {
                    pre = f.Predio;
                }
                tb.AddRow("Predio", "", pre);
                tb.AddRow("Descripcion", "", f.Descripcion);
                this.Servicio.EliminarEntidad(f);

            }
            Seguimiento s = new Seguimiento();
            s.Id = 0;
            s.Estado = 0;
            if (SesionUtiles.Instance.EsDesarrollador)
                s.Usuario = 0;
            else
                s.Usuario = SesionUtiles.Instance.UsuarioActivo.Id;
            s.Cliente = Configuracion.NombrePc;
            s.Direccion = Configuracion.IPLocal;
            s.Comentario = "Se elimino esta foto del predio";
            s.Original = tb.Output();
            this.Servicio.EliminarEntidad(s);
            this.Servicio.Guardar();
        }

        public void ModificarFotos(List<PredioFoto> pFotos)
        {
            try
            {
                foreach (PredioFoto f in pFotos)
                {
                    if (f.Id <= 0)
                    {
                        this.Servicio.CrearEntidad(f, "PredioFotos");
                    }
                }
                this.Servicio.Guardar();
            }
            catch (Exception ex)
            {
                CuadroMensajes.Alertar("Operacion incompleta", "Se ha producido el siguiente error:", ex.Message, "");
            }
            
        }

        /* Eliminar Contrucciones
        PredioContruccion -> Nada
        Eliminar las contrucciones recibidas */
        public void EliminarConstrucciones(List<PredioConstruccion> pConstrucciones)
        {
            TablaCadena tb = new TablaCadena();
            tb.AddRow("Operaciones ejecutadas:", "", "");
            tb.AddRow("Eliminado:", "", "");
            foreach (PredioConstruccion c in pConstrucciones)
            {
                tb.AddRow("Tipo de objeto", "Predio/Construccion", "");
                tb.AddRow("Id:", c.Id, "");
                tb.AddRow("Atributo", "", "Original");
                tb.AddRow("--------", "", "--------");
                int? piso = 0;
                if (c.Piso == null)
                {
                    piso = c.Piso;
                }
                tb.AddRow("Piso", "", piso);
                tb.AddRow("Componente", "", c.Clase + "-" + c.ConsElementoNav.Denominacion);
                this.Servicio.EliminarEntidad(c);
            }
            Seguimiento s = new Seguimiento();
            s.Id = 0;
            s.Estado = 0;
            if (SesionUtiles.Instance.EsDesarrollador)
                s.Usuario = 0;
            else
                s.Usuario = SesionUtiles.Instance.UsuarioActivo.Id;
            s.Cliente = Configuracion.NombrePc;
            s.Direccion = Configuracion.IPLocal;
            s.Comentario = "Se elimino esta elemento de construccion del predio";
            s.Original = tb.Output();
            this.Servicio.CrearEntidad(s, "Seguimientos");
            this.Servicio.Guardar();
        }

        public void CrearConstruccion(PredioConstruccion cons)
        {
            try
            {
                this.Servicio.CrearEntidad(cons, "PrediosConstrucciones");
                this.Servicio.Guardar();
            }
            catch (Exception ex)
            {
                CuadroMensajes.Alertar("Operacion incompleta", "Se ha producido el siguiente error:", ex.Message, "");
            }
        }
    
        public void GuardarBloques(IEnumerable<PredioBloque> bloques)
        {
            foreach (PredioBloque b in bloques)
            {
                int bid = 0;
                if (b.Id <= 0)
                    bid = this.CrearBloque(b);
                else
                    bid = b.Id;
                foreach (PredioPiso p in b.PisosNav)
                {
                    p.Bloque = bid;
                    int pid = 0;
                    if (p.Id <= 0)
                        pid = this.CrearPiso(p);
                    else
                    {
                        pid = p.Id;
                        this.Servicio.ActualizarEntidad(p);
                    }                    
                    foreach (PredioConstruccion c in p.ConstruccionesNav)
                    {
                        if (c.Id <= 0)
                        {
                            c.Piso = pid;
                            this.Servicio.CrearEntidad(c, "PredioConstrucciones");
                        }
                    }
                }
            }
            this.Guardar();
        }

    }
}
