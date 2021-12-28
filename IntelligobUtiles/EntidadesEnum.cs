using System.ComponentModel;

namespace Intelligob.Utiles
{
    public enum EntidadesEnum
    {
        [Description("Cero")] // 0
        EnCero,
        [Description("Contribuyentes")]        
        EnContribuyentes,
        [Description("Rebajas")]
        EnRebajas,

        #region Seguridad

        [Description("Usuarios")] // 3
        EnUsuarios,

        [Description("Privilegios")]
        EnPrivilegios,

        #endregion

        #region Agua Potable

        [Description("Cuenta de Agua Potable")] // 5
        EnAguaCuenta,

        [Description("Lectura de consumo de agua")]
        EnAguaLectura,

        [Description("Servicios complementarios de agua")]
        EnAguaServicios,

        #endregion

        #region Catastro

        [Description("Patentes municipales")] // 8
        EnPatentes,

        [Description("Predios")]
        EnPredios,
        
        [Description("Propietarios de predios")]
        EnPrediosPropietarios,
        
        [Description("Componentes de construccion de predios")]
        EnPrediosComponentes,
        
        [Description("Bloques de construccion")]
        EnPrediosBloques,

        [Description("Pisos de bloque de construccion")]
        EnPrediosPisos,

        [Description("Terrenos de predios")]
        EnPrediosTerrenos,

        [Description("Frentes de predios")]
        EnPrediosFrentes,

        [Description("Fotos de predios")]
        EnPrediosFotos,

        #endregion

        #region Cajas

        [Description("Cajas")] // 17
        EnCajas,
        [Description("Usuarios cajeros")]
        EnCajaUsuarios,
        [Description("Movimiento de caja")]
        EnCajaMovimientos,

        #endregion

        [Description("Emision de titulos")] // 20
        EnEmision,

        [Description("Planilla de titulo")]
        EnPlanilla,

        [Description("Cobro de planilla")]
        EnCobroTransaccion,

        [Description("Documentos habilitantes")]
        EnHabilitante,

        [Description("Soportes de impresion")]
        EnSoporte,

        [Description("Movimientos de soporte de impresion")]
        EnSoporteMovimientos
    }    
}