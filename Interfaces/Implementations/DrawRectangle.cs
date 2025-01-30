using Ase_Boose.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ase_Boose.Interfaces.Implementations
{
    internal class DrawRectangle : IGraphicsCommand
    {
        public void Execute(Graphics graphics, string[] arguments, ICanvas canvas)
        {
            if (arguments.Length == 2 &&
                int.TryParse(arguments[0], out int width) &&
                int.TryParse(arguments[1], out int height))
            {
                int x = canvas.CurrentPosition.X;
                int y = canvas.CurrentPosition.Y;

                if (canvas.IsFilling)
                {
                    using var brush = new SolidBrush(canvas.FillColor);
                    graphics.FillRectangle(brush, x, y, width, height);
                }
                else
                {
                    graphics.DrawRectangle(canvas.DrawingPen, x, y, width, height);
                }

                CommandUtils.ClearCommandTextBox(canvas);
            }
            else
            {
                CommandUtils.ShowError("Invalid command arguments for 'rectangle'. Please provide width and height.");
            }
        }
    }
}
