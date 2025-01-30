using Ase_Boose.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ase_Boose.Interfaces.Implementations
{
    internal class DrawTriangle : IGraphicsCommand
    {
        public void Execute(Graphics graphics, string[] arguments, ICanvas canvas)
        {
            if (arguments.Length == 2 &&
                int.TryParse(arguments[0], out int baseLength) &&
                int.TryParse(arguments[1], out int height))
            {
                int x = canvas.CurrentPosition.X;
                int y = canvas.CurrentPosition.Y;

                Point[] points =
                {
                new Point(x, y),
                new Point(x + baseLength, y),
                new Point(x + baseLength / 2, y - height)
            };

                if (canvas.IsFilling)
                {
                    using var brush = new SolidBrush(canvas.FillColor);
                    graphics.FillPolygon(brush, points);
                }
                else
                {
                    graphics.DrawPolygon(canvas.DrawingPen, points);
                }

                CommandUtils.ClearCommandTextBox(canvas);
            }
            else
            {
                CommandUtils.ShowError("Invalid command arguments for 'triangle'. Please provide base and height.");
            }
        }
    }
}
