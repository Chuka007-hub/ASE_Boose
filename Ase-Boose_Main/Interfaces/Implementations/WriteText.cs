using Ase_Boose.Utils;

namespace Ase_Boose.Interfaces.Implementations
{
    public class WriteText : IShapeCommand
    {
        public void Execute(ICanvas canvas, string[] arguments)
        {
            if (arguments.Length >= 1)
            {
                string text = string.Join(" ", arguments);
                using (Graphics graphics = canvas.PictureBox.CreateGraphics())
                {
                    using (Font font = new Font("Arial", 12))
                    {
                        graphics.DrawString(text, font, new SolidBrush(canvas.DrawingPen.Color), 
                            canvas.CurrentPosition);
                    }
                }
            }
            else
            {
                CommandUtils.ShowError("Invalid write command. Please provide text to write.");
            }
        }
    }
}
