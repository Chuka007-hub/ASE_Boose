using Ase_Boose.Utils;
using System.Drawing;

namespace Ase_Boose.Interfaces.Implementations
{
    public class WriteText : IGraphicsCommand
    {
        public void Execute(Graphics graphics, string[] arguments, ICanvas canvas)
        {
            if (arguments.Length >= 1)
            {
                string text = string.Join(" ", arguments);
                
                if (canvas.IsFilling)
                {
                    using var brush = new SolidBrush(canvas.DrawingPen.Color);
                    graphics.DrawString(text, new Font("Arial", 12), brush, canvas.CurrentPosition);
                }
                else
                {
                    graphics.DrawString(text, new Font("Arial", 12), new SolidBrush(canvas.DrawingPen.Color), 
                        canvas.CurrentPosition);
                }

                CommandUtils.ClearCommandTextBox(canvas);
            }
            else
            {
                CommandUtils.ShowError("Invalid write command. Please provide text to write.");
            }
        }
    }
}
