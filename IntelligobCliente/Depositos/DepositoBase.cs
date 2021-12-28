using System;
using System.Linq;

namespace Intelligob.Cliente.Depositos
{
    public class DepositoBase : IDisposable
    {
        public String FormatoClave = "Id={0}";

        public DepositoBase(Referencia.IEntidades servicio)
        {
            this.Servicio = servicio;
        }

        public Referencia.IEntidades Servicio
        {
            get;
            private set;
        }

        public void CrearSeguimiento(Utiles.TablaCadena pTabla, String pComentario, Utiles.EntidadesEnum pEntidad, String pEncabezado)
        {
            Referencia.SeguimientoDto seg = new Referencia.SeguimientoDto();
            if (SesionUtiles.Instance.EsDesarrollador)
                seg.Usuario = null;
            else
                seg.Usuario = SesionUtiles.Instance.UsuarioActivo.Id;
            seg.Cliente = Utiles.Configuracion.NombrePc;
            seg.Direccion = Utiles.Configuracion.IPLocal;
            seg.Comentario = pComentario;
            seg.Original = pEncabezado + pTabla.Output();
            seg.Fecha = this.Servicio.Hoy();
            seg.Entidad = Convert.ToInt32(pEntidad);
            seg.InterfazRed = Utiles.Configuracion.DireccionesMac;
            seg.Estado = 0;

            this.Servicio.CreateSeguimiento(seg);
        }

        public void Dispose()
        {
            this.Servicio = null;
        }
    }
}
