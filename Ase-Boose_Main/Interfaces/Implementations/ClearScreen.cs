using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ase_Boose.Interfaces.Implementations
{
    public class ClearScreen : IShapeCommand
    {
        /// <summary>
        /// Executes the command to clear the canvas and reset the command text box.
        /// </summary>
        /// <param name="canvas">The canvas on which the operation is performed.</param>
        /// <param name="args">The command arguments (not used in this method).</param>
        public void Execute(ICanvas canvas, string[] args)
        {
            if (canvas == null) return;

            canvas.ClearDrawings();
            canvas.CommandTextBox.Invoke((MethodInvoker)delegate
            {
                canvas.CommandTextBox.Clear();
            });
        }

    }
}
