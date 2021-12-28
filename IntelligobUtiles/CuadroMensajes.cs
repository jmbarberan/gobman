using System;
using System.Windows;
using TaskDialogInterop;

namespace Intelligob.Utiles
{
    public static class CuadroMensajes
    {
        /// <summary>
        /// Hacer una pregunta al usuario con chequeo y devolver el resultado
        /// </summary>
        /// <param name="pTituloV">Titulo de la ventana</param>
        /// <param name="pInstruccion">Instruccion principal</param>
        /// <param name="pContenido">Contenido del mensaje</param>
        /// <param name="pChequeo">Texto de verificacion</param>
        /// <returns>Resultado Custom buton 0 = Si 1 = No y verification checked</returns>
        public static TaskDialogResult PreguntarChequeo(String pTituloV, String pInstruccion, String pContenido, String pChequeo)
        {
            TaskDialogOptions pregunta = new TaskDialogOptions();
            pregunta.Owner = Application.Current.MainWindow;
            pregunta.Title = pTituloV;
            pregunta.MainInstruction = pInstruccion;
            pregunta.Content = pContenido;
            pregunta.CustomButtons = new string[] { "&Si", "&No" };
            pregunta.DefaultButtonIndex = 1;
            pregunta.MainIcon = VistaTaskDialogIcon.Warning;
            if (pChequeo.Length > 0)
            {
                pregunta.VerificationText = pChequeo;
            }
            TaskDialogResult res = TaskDialog.Show(pregunta);
            return res;
        }

        /// <summary>
        /// Hacer una pregunta al usuario y devolver el resultado
        /// </summary>
        /// <param name="pTituloV">Titulo de la ventana</param>
        /// <param name="pInstruccion">Instruccion principal</param>
        /// <param name="pContenido">Contenido del mensaje</param>
        /// <returns>Resultado Custom buton 0 = Si 1 = No y verification checked y verification checked</returns>
        public static TaskDialogResult Preguntar(String pTituloV, String pInstruccion, String pContenido)
        {            
            return PreguntarChequeo(pTituloV, pInstruccion, pContenido, "");
        }

        /// <summary>
        /// Mostrar mensaje de alerta al usuario
        /// </summary>
        /// <param name="pTituloV">Titulo de la ventana</param>
        /// <param name="pInstruccion">Instruccion principal</param>
        /// <param name="pContenido">Contenido del mensaje</param>
        /// <param name="pChequeo">Texto de verificacion</param>
        /// <returns>Resultado custom buton = 0 y verification checked</returns>
        public static TaskDialogResult Alertar(String pTituloV, String pInstruccion, String pContenido, String pChequeo)
        {
            return Aceptar(pTituloV, pInstruccion, pContenido, pChequeo, VistaTaskDialogIcon.Shield);
        }

        /// <summary>
        /// Mostrar mensaje de tarea exitosa
        /// </summary>
        /// <param name="pTituloV">Titulo de la ventana</param>
        /// <param name="pInstruccion">Instruccion principal</param>
        /// <param name="pContenido">Contenido del mensaje</param>
        /// <param name="pChequeo">Texto de verificacion</param>
        /// <returns>Solo retorna custom boton = 0 y verification checked</returns>
        public static TaskDialogResult Exito(String pTituloV, String pInstruccion, String pContenido, String pChequeo)
        {
            return Aceptar(pTituloV, pInstruccion, pContenido, pChequeo, VistaTaskDialogIcon.Information);
        }

        public static TaskDialogResult Aceptar(String pTituloV, String pInstruccion, String pContenido, String pChequeo, VistaTaskDialogIcon pIcono)
        {
            TaskDialogOptions men = new TaskDialogOptions();
            men.Owner = Application.Current.MainWindow;
            men.Title = pTituloV;
            if (pInstruccion.Length > 0)
            {
                men.MainInstruction = pInstruccion;
            }
            men.Content = pContenido;
            men.CustomButtons = new string[] { "&Aceptar" };
            men.DefaultButtonIndex = 1;
            men.MainIcon = pIcono;
            if (pChequeo.Length > 0)
            {
                men.VerificationText = pChequeo;
            }
            TaskDialogResult res = TaskDialog.Show(men);
            return res;
        }

    }
}
