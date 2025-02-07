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
            if (arguments.Length == 2 && 
                double.TryParse(arguments[0], out double x) && 
                double.TryParse(arguments[1], out double y))
            {
                canvas.CurrentPosition = new Point((int)Math.Round(x), (int)Math.Round(y));
            }
            else
            {
                CommandUtils.ShowError("Invalid arguments for 'moveto' command. Please provide valid coordinates.");
            }
        }

    }
}
