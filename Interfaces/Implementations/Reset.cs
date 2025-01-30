using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ase_Boose.Interfaces.Implementations
{
    internal class Reset : IShapeCommand
    {
        public void Execute(Canvas canvas, string[] argument)
        {
            canvas.CurrentPosition = new Point(250, 180);
            canvas.DrawingPen = new Pen(Color.Black);
            canvas.FillColor = Color.Black;

            canvas.CommandTextBox.Invoke((MethodInvoker)(() => canvas.CommandTextBox.Clear()));
        }
    }
}
