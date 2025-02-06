using Ase_Boose.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ase_Boose.Interfaces.Implementations
{
    public class DrawRectangle : IGraphicsCommand
    {
     /// <summary>
     /// Executes the command to draw a rectangle on the canvas.
     /// </summary>
     /// <param name="graphics">The graphics object used for drawing.</param>
     /// <param name="arguments">An array of arguments containing width and height of the rectangle.</param>
     /// <param name="canvas">The canvas on which the rectangle will be drawn.</param>
        public void Execute(Graphics graphics, string[] arguments, ICanvas canvas)
        {
            if (arguments.Length == 2 &&
                int.TryParse(arguments[0], out int width) &&
                int.TryParse(arguments[1], out int height))
            {
                int x = canvas.CurrentPosition.X;
                int y = canvas.CurrentPosition.Y;

                canvas.AddDrawingCommand(g =>
                {
                    if (canvas.IsFilling)
                    {
                        using var brush = new SolidBrush(canvas.FillColor);
                        g.FillRectangle(brush, x, y, width, height);
                    }
                    else
                    {
                        g.DrawRectangle(canvas.DrawingPen, x, y, width, height);
                    }
                });

                CommandUtils.ClearCommandTextBox(canvas);
            }
            else
            {
                CommandUtils.ShowError("Invalid command arguments for 'rectangle'. Please provide width and height.");
            }
        }

    }
}
