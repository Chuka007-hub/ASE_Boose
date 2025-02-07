using Ase_Boose.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ase_Boose.Interfaces.Implementations
{
    public class DrawTo : IShapeCommand
    {
        /// <summary>
        /// Executes the 'drawto' command, drawing a line from the current position to the specified coordinates.
        /// </summary>
        /// <param name="canvas">The canvas on which the line will be drawn.</param>
        /// <param name="arguments">An array of arguments containing the destination X and Y coordinates.</param>
        public void Execute(ICanvas canvas, string[] arguments)
        {
            if (arguments.Length == 2 && 
                double.TryParse(arguments[0], out double x) && 
                double.TryParse(arguments[1], out double y))
            {
                Point start = canvas.CurrentPosition;
                Point destination = new Point((int)Math.Round(x), (int)Math.Round(y));
                
                // Store the drawing command
                canvas.AddDrawingCommand(g => g.DrawLine(canvas.DrawingPen, start, destination));
                
                canvas.CurrentPosition = destination;
            }
            else
            {
                CommandUtils.ShowError("Invalid arguments for 'drawto' command. Please provide valid coordinates.");
            }
        }

    }

}
