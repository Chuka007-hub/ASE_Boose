using Ase_Boose.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ase_Boose.Interfaces.Implementations
{
    public class DrawCircle : IGraphicsCommand
    {
        /// <summary>
        /// Executes the command to draw a circle on the canvas.
        /// </summary>
        /// <param name="graphics">The graphics object used for drawing.</param>
        /// <param name="arguments">An array of arguments, where the first argument should be the radius of the circle.</param>
        /// <param name="canvas">The canvas on which the circle will be drawn.</param>
        public void Execute(Graphics graphics, string[] arguments, ICanvas canvas)
        {
            if (arguments.Length == 1 && int.TryParse(arguments[0], out int radius))
            {
                int x = canvas.CurrentPosition.X - radius;
                int y = canvas.CurrentPosition.Y - radius;

                canvas.AddDrawingCommand(g =>
                {
                    if (canvas.IsFilling)
                    {
                        using var brush = new SolidBrush(canvas.FillColor);
                        g.FillEllipse(brush, x, y, 2 * radius, 2 * radius);
                    }
                    else
                    {
                        g.DrawEllipse(canvas.DrawingPen, x, y, 2 * radius, 2 * radius);
                    }
                });

                CommandUtils.ClearCommandTextBox(canvas);
            }
            else
            {
                CommandUtils.ShowError("Invalid command argument for 'circle'. Please provide a valid radius.");
            }
        }

    }
}
