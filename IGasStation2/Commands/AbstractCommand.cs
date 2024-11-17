using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IGasStation2.Commands
{
    public abstract class AbstractCommand : ICommand
    {
        /// <inheritdoc/>
        event EventHandler ICommand.CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <inheritdoc/>
        bool ICommand.CanExecute(object p)
        {
            return CanExecute(p);
        }

        /// <inheritdoc/>
        void ICommand.Execute(object p)
        {
            Execute(p);
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="p"> Data used by the command. If the command does not require data to be passed,
        ///    this object can be set to null.</param>
        /// <returns> true if this command can be executed; otherwise, false. </returns>
        protected virtual bool CanExecute(object p)
        {
            return true;
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="p"> Data used by the command. If the command does not require data to be passed,
        ///     this object can be set to null. </param>
        protected abstract void Execute(object p);
    }
}
