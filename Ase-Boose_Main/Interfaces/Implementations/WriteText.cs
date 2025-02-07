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
                
                canvas.AddDrawingCommand(g =>
                {
                    using (Font font = new Font("Arial", 12))
                    {
                        g.DrawString(text, font, new SolidBrush(canvas.DrawingPen.Color), 
                            canvas.CurrentPosition);
                    }
                });

                CommandUtils.ClearCommandTextBox(canvas);
            }
            else
            {
                CommandUtils.ShowError("Invalid write command. Please provide text to write.");
            }
        }
    }
}
