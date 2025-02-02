using Ase_Boose.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ase_Boose.Interfaces.Implementations
{
    
    /// <summary>
    /// Represents a command to enable or disable filling for shapes.
    /// </summary>
    public class fillColor : IShapeCommand
    {
        /// <summary>
        /// Executes the fill color command, enabling or disabling shape filling on the canvas.
        /// </summary>
        /// <param name="canvas">The canvas on which the fill mode will be applied.</param>
        /// <param name="argument">An array containing the fill mode argument ('on' or 'off').</param>
        public void Execute(ICanvas canvas, string[] argument)
        {
            if (argument.Length == 0)
            {
                CommandUtils.ShowError("Missing argument for 'fill' command. Use 'on' or 'off'.");
                return;
            }

            string fillMode = argument[0].ToLower();
            if (fillMode == "on")
            {
                canvas.IsFilling = true;
                canvas.FillColor = canvas.DrawingPen.Color;
            }
            else if (fillMode == "off")
            {
                canvas.IsFilling = false;
            }
            else
            {
                CommandUtils.ShowError("Invalid fill mode. Use 'on' or 'off'.");
                return;
            }

            CommandUtils.ClearCommandTextBox(canvas);
        }
    }


}
