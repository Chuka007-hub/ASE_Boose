using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ase_Boose.Interfaces.Implementations
{
    public class Reset : IShapeCommand
    {
        /// <summary>
        /// Resets the current position, drawing pen, and fill color on the canvas, and clears the command text box.
        /// </summary>
        /// <param name="canvas">The canvas on which the current position, pen color, and fill color will be set, and the command text box will be cleared.</param>
        /// <param name="argument">An array of arguments. This parameter is not used in this method.</param>
        public void Execute(ICanvas canvas, string[] argument)
        {
            // Set the current position to a default point (250, 180)
            canvas.CurrentPosition = new Point(250, 180);

            // Set the drawing pen color to black
            canvas.DrawingPen = new Pen(Color.Black);

            // Set the fill color to black
            canvas.FillColor = Color.Black;

            // Clear the command text box
            canvas.CommandTextBox.Invoke((MethodInvoker)(() => canvas.CommandTextBox.Clear()));
        }

    }
}
