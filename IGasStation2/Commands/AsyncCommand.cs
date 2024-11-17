using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGasStation2.Commands
{
    /// <summary>
    /// Asynchronous command.
    /// </summary>
    public class AsyncCommand : AbstractCommand
    {
        private readonly Func<object, Task> _execute;
        private readonly Func<object, bool> _canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncCommand"/> class.
        /// </summary>
        public AsyncCommand(Func<object, Task> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <inheritdoc/>
        protected override bool CanExecute(object p)
        {
            return _canExecute?.Invoke(p) ?? true;
        }

        /// <inheritdoc/>
        protected override void Execute(object p)
        {
            Task.Run(() => _execute(p));
        }
    }
}
