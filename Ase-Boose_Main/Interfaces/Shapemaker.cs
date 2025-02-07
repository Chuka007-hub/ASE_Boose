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
            { "pen", new PenColor() },
            { "fill", new fillColor() }
        };


        private readonly Dictionary<string, IGraphicsCommand> graphicsCommands = new()
        {
            { "rectangle", new DrawRectangle() },
            { "circle", new DrawCircle() },
            { "triangle", new DrawTriangle() },
            { "write", new WriteText() }
        };


        /// <summary>
        /// Executes drawing commands by parsing and executing basic or graphics-related commands.
        /// </summary>
        /// <param name="parser">The command parser containing the command and its arguments.</param>
        /// <exception cref="ArgumentNullException">Thrown when the parser is null.</exception>
        public void ExecuteDrawing(CommandParser parser)
        {
            if (parser != null)
            {
                string command = parser.Command.ToLower();

                if (basicCommands.ContainsKey(command))
                {
                    basicCommands[command].Execute(canvas, parser.Arguments);
                }
                else if (graphicsCommands.ContainsKey(command))
                {
                    canvas.AddDrawingCommand(g =>
                    {
                        graphicsCommands[command].Execute(g, parser.Arguments, canvas);
                    });
                }
                else
                {
                    MessageBox.Show($"Unrecognized command: {parser.Command}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



    }
}
