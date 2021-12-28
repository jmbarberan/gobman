using System;
using System.Linq;
using Intelligob.Datos.Referencia1;
using System.Collections.Generic;
using Intelligob.Utiles;

namespace Intelligob.Datos.Contenedores
{
    public interface IContenedor
    {
        #region Consultas de Agua potable
        
        IQueryable<AguaPotable> QAguaCuentas { get; }

        IQueryable<AguaPotable> QAguaCuentasExpandido { get; }

        IQueryable<AguaServicio> QAguaServiciosExpandido { get; }

        IQueryable<AguaLectura> QAguaLecturas { get; }

        IQueryable<AguaLectura> QAguaLecturasExpandido { get; }

        int AguaCuentaCrear(AguaPotable pNuevo);

        int AguaServicioCrear(AguaServicio pSrv);

        void AguaCuentaModificar(AguaPotable pModificado, IEnumerable<AguaServicio> pSrvEliminados);

        #endregion

        #region Consultas de Emisiones

        IQueryable<CoeficienteElemento> QCoeficientesElementos { get; }

        IQueryable<Concepto> QConceptos { get; }

        IQueryable<Coeficiente> QCoeficientes { get; }

        IQueryable<CoeficienteElemento> QCoeficienteElementos { get; }

        #endregion

        #region Consultas Tablas

        IQueryable<Tabla> QTablas { get; }

        IQueryable<TablaClave> QTablaClaves { get; } 

        #endregion

        #region Seguridad
        IQueryable<Usuario> QUsuarios { get; }
        int UsuarioCrear(Usuario usr);
        IQueryable<Privilegio> QPrivilegios { get; }

        IQueryable<Funcion> QFunciones { get; }

        IQueryable<Funcion> QFuncionesExtendido { get; }

        IQueryable<ModuloUsuario> QModulosUsuarios { get; }

        int SeguimientoCrear(Seguimiento pSeg);

        int PrivilegioCrear(Privilegio prv);

        #endregion

        #region Catastros
        IQueryable<PredioBase> QPredios { get; }
        IQueryable<PredioTerreno> QTerrenos { get; }
        IQueryable<PredioBase> QPrediosExtendido { get; }
        IQueryable<PredioBloque> QPredioBloques { get; }
        IQueryable<PredioFrente> QPredioFrentes { get; }
        IQueryable<PredioPiso> QPredioPisos { get; }
        IQueryable<PredioConstruccion> QPredioConstrucciones { get; }
        IQueryable<PredioPropietario> QPredioPropietarios { get; }        
        IQueryable<PredioFoto> QPredioFotos { get; }

        PredioBase PredioPorId(int id);
        int PredioCrear(PredioBase pre);
        void ModificarPredio(PredioBase predio);
        IEnumerable<PredioBase> PrediosPorCodigo(String pCodigo, int pEstado, TipoBusquedaTexto pTipoBusqueda, int pTipoPredio);
        #endregion

        IQueryable<Contribuyente> QContribuyentes { get; }
        int ContribuyenteCrear(Contribuyente pNuevo);
        void CrearEntidad(object pEntidad, string pClase);
        void ActualizarEntidad(object pEntidad);
        void EliminarEntidad(object pEntidad);
        void Guardar();
    }
}
