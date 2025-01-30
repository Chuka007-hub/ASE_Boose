using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ase_Boose.Interfaces.Implementations
{
    internal class ClearScreen : IShapeCommand
    {
        public void Execute(Canvas canvas, string[] args)
        {
            if (canvas == null) return;

            canvas.DrawingPictureBox.Invoke((MethodInvoker)delegate
            {
                using (Graphics g = canvas.DrawingPictureBox.CreateGraphics())
                {
                    g.Clear(canvas.DrawingPictureBox.BackColor);
                }
                canvas.DrawingPictureBox.Invalidate(); 
            });

            canvas.CommandTextBox.Invoke((MethodInvoker)delegate
            {
                canvas.CommandTextBox.Clear();
            });
        }
    }
}
