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
            { "fill", new fillColor() },

        };


        private readonly Dictionary<string, IGraphicsCommand> graphicsCommands = new()
        {
            { "rectangle", new DrawRectangle() },
            { "circle", new DrawCircle() },
            { "triangle", new DrawTriangle() }
        };


        /// <summary>
        /// Executes drawing commands by parsing and executing basic or graphics-related commands.
        /// </summary>
        /// <param name="parser">The command parser containing the command and its arguments.</param>
        /// <exception cref="ArgumentNullException">Thrown when the parser is null.</exception>
        public void ExecuteDrawing(CommandParser parser)
        {
            if (parser == null)
            {
                throw new ArgumentNullException(nameof(parser));
            }

            // Check if invoking on the UI thread is required
            if (canvas.InvokeRequired)
            {
                // Invoke the drawing execution on the UI thread
                canvas.Invoke(new Action(() => ExecuteDrawingInternal(parser)));
            }
            else
            {
                // Execute drawing directly if already on the UI thread
                ExecuteDrawingInternal(parser);
            }
        }

        /// <summary>
        /// Executes the drawing commands internally by checking the type of command and invoking the respective drawing logic.
        /// </summary>
        /// <param name="parser">The command parser containing the command and its arguments.</param>
        private void ExecuteDrawingInternal(CommandParser parser)
        {
            lock (_locker)
            {
                // Retrieve the command in lowercase format
                string command = parser.Command.ToLower();

                // Execute basic commands like 'moveto', 'drawto', 'reset', 'clear', 'pen', 'fill'
                if (basicCommands.ContainsKey(command))
                {
                    basicCommands[command].Execute(canvas, parser.Arguments);
                }
                // Execute graphics commands like 'rectangle', 'circle', 'triangle'
                else if (graphicsCommands.ContainsKey(command))
                {
                    using (Graphics graphics = canvas.PictureBox.CreateGraphics())
                    {
                        graphicsCommands[command].Execute(graphics, parser.Arguments, canvas);
                    }
                }
                // Show error if the command is unrecognized
                else
                {
                    MessageBox.Show($"Unrecognized command: {parser.Command}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



    }
}
