using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGasStation2.Commands
{
    /// <summary>
    /// Action command implementation.
    /// </summary>
    public class ActionCommand : AbstractCommand
    {
        private readonly Action<object> _execute;

        private readonly Func<object, bool> _canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionCommand"/> class.
        /// </summary>
        public ActionCommand(Action<object> execute, Func<object, bool> canExecute = null)
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
            _execute(p);
        }
    }
}
