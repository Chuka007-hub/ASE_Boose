using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ase_Boose.Interfaces.Implementations
{
    public class MoveTo : IShapeCommand
    {
        /// <summary>
        /// Moves the current position of the canvas to the specified coordinates.
        /// </summary>
        /// <param name="canvas">The canvas where the position will be updated.</param>
        /// <param name="arguments">An array containing the x and y coordinates as strings.</param>
        public void Execute(ICanvas canvas, string[] arguments)
        {
            if (arguments.Length == 2 && int.TryParse(arguments[0], out int x) && int.TryParse(arguments[1], out int y))
            {
                canvas.CurrentPosition = new Point(x, y);
            }
            else
            {
                MessageBox.Show("Invalid arguments for 'moveto' command. Please provide valid coordinates.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
