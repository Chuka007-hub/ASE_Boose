using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ase_Boose.Interfaces.Implementations
{
    internal class DrawTo : IShapeCommand
    {
        public void Execute(Canvas canvas, string[] arguments)
        {
            if (arguments.Length == 2 && int.TryParse(arguments[0], out int x) && int.TryParse(arguments[1], out int y))
            {
                Point destination = new Point(x, y);
                using (Graphics graphics = canvas.DrawingPictureBox.CreateGraphics())
                {
                    graphics.DrawLine(canvas.DrawingPen, canvas.CurrentPosition, destination);
                }
                canvas.CurrentPosition = destination;
            }
            else
            {
                MessageBox.Show("Invalid arguments for 'drawto' command. Please provide valid coordinates.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
