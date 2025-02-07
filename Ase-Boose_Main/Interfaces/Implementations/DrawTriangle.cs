using Ase_Boose.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ase_Boose.Interfaces.Implementations
{
    public class DrawTriangle : IGraphicsCommand
    {
        /// <summary>
        /// Executes the command to draw a triangle on the canvas.
        /// </summary>
        /// <param name="graphics">The graphics object used for drawing.</param>
        /// <param name="arguments">An array of arguments containing the base length and height of the triangle.</param>
        /// <param name="canvas">The canvas on which the triangle will be drawn.</param>
        public void Execute(Graphics graphics, string[] arguments, ICanvas canvas)
        {
            if (arguments.Length == 2 &&
                double.TryParse(arguments[0], out double baseLength) &&
                double.TryParse(arguments[1], out double height))
            {
                int x = canvas.CurrentPosition.X;
                int y = canvas.CurrentPosition.Y;
                int intBase = (int)Math.Round(baseLength);
                int intHeight = (int)Math.Round(height);

                Point[] points =
                {
                    new Point(x, y),
                    new Point(x + intBase, y),
                    new Point(x + intBase / 2, y - intHeight)
                };

                canvas.AddDrawingCommand(g =>
                {
                    if (canvas.IsFilling)
                    {
                        using var brush = new SolidBrush(canvas.FillColor);
                        g.FillPolygon(brush, points);
                    }
                    else
                    {
                        g.DrawPolygon(canvas.DrawingPen, points);
                    }
                });

                CommandUtils.ClearCommandTextBox(canvas);
            }
            else
            {
                CommandUtils.ShowError("Invalid command arguments for 'triangle'. Please provide base and height.");
            }
        }
    }
}
