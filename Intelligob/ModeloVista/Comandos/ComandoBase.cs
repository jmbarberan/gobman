using System;
using System.Linq;

namespace Intelligob.Escritorio.ModeloVista.Comandos
{ 
    public class CommandBase<T>
    {
        public event ExceptionEventHandler ExecuteFailed;

        private readonly Action<T> afterCommandAction;        

        public CommandBase(Action<T> afterCommandAction)
        {
            if (afterCommandAction == null)
            {
                throw new ArgumentNullException("afterCommandAction");
            }
            this.afterCommandAction = afterCommandAction;
        }

        protected Action<T> AfterCommandAction
        {
            get
            {
                return this.afterCommandAction;
            }
        }        

        protected virtual void OnException(Exception exception)
        {
            if (ExecuteFailed != null)
                ExecuteFailed(this, exception);
        }
    }
}
