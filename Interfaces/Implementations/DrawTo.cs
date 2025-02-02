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
            if (arguments.Length == 2 && int.TryParse(arguments[0], out int x) && int.TryParse(arguments[1], out int y))
            {
                Point destination = new Point(x, y);
                using (Graphics graphics = canvas.PictureBox.CreateGraphics())
                {
                    graphics.DrawLine(canvas.DrawingPen, canvas.CurrentPosition, destination);
                }
                canvas.CurrentPosition = destination;
            }
            else
            {
                CommandUtils.ShowError("Invalid arguments for 'drawto' command. Please provide valid coordinates.");
            }
        }

    }

}
