using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ase_Boose.Interfaces.Implementations
{
    internal class fillColor : IShapeCommand
    {
        public void Execute(Canvas canvas, string[] argument)
        {
            if (argument.Length == 0)
            {
                MessageBox.Show("Missing argument for 'fill' command. Use 'on' or 'off'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Invalid fill mode. Use 'on' or 'off'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

             canvas.CommandTextBox.Invoke((MethodInvoker)(() => canvas.CommandTextBox.Clear()));
        }
    }
}
