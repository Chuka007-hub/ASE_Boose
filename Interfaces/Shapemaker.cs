using Ase_Boose.Interfaces.Implementations;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ase_Boose.Interfaces
{
    public class Shapemaker
    {
        private Canvas canvas;
        private readonly object _locker = new();

        public Shapemaker(Canvas canvas)
        {
            this.canvas = canvas;
        }


        private readonly Dictionary<string, IShapeCommand> basicCommands = new()
        {
            { "moveto", new MoveTo() },
            { "drawto", new DrawTo() },
            { "reset", new Reset() },
            { "clear", new ClearScreen() },
            { "pen", new PenColor() }
        };


        private readonly Dictionary<string, IGraphicsCommand> graphicsCommands = new()
        {
           // { "rectangle", new DrawRectangle() },
            { "circle", new DrawCircle() },
            //{ "triangle", new DrawTriangle() }
        };


        public void ExecuteDrawing(CommandParser parser)
        {
            if (parser == null)
            {
                throw new ArgumentNullException(nameof(parser));
            }

            if (canvas.InvokeRequired)
            {
                canvas.Invoke(new Action(() => ExecuteDrawingInternal(parser)));
            }
            else
            {
                ExecuteDrawingInternal(parser);
            }
        }


        private void ExecuteDrawingInternal(CommandParser parser)
        {
            lock (_locker)
            {
                using Graphics graphics = canvas.DrawingPictureBox.CreateGraphics();
                string command = parser.Command.ToLower();

                if (basicCommands.ContainsKey(command))
                {
                    basicCommands[command].Execute(canvas, parser.Arguments);
                }
                else if (graphicsCommands.ContainsKey(command))
                {
                    graphicsCommands[command].Execute(graphics, parser.Arguments, canvas);
                }
                else
                {
                    MessageBox.Show($"Unrecognized command: {parser.Command}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


    }
}
