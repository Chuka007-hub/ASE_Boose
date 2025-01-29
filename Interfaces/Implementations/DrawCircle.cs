using Ase_Boose.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ase_Boose.Interfaces.Implementations
{
    internal class DrawCircle : IGraphicsCommand
    {
        public void Execute(Graphics graphics, string[] arguments, ICanvas canvas)
        {
            if (arguments.Length == 1 && int.TryParse(arguments[0], out int radius))
            {
                int x = canvas.CurrentPosition.X - radius;
                int y = canvas.CurrentPosition.Y - radius;

                if (canvas.IsFilling)
                {
                    using var brush = new SolidBrush(canvas.FillColor);
                    graphics.FillEllipse(brush, x, y, 2 * radius, 2 * radius);
                }
                else
                {
                    graphics.DrawEllipse(canvas.DrawingPen, x, y, 2 * radius, 2 * radius);
                }

                CommandUtils.ClearCommandTextBox(canvas);
            }
            else
            {
                CommandUtils.ShowError("Invalid command argumnet for 'circle'. Please provide a valid radius.");
            }
        }
    }
}
