using Ase_Boose.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ase_Boose.Interfaces.Implementations
{
    public class PenColor : IShapeCommand
    {
        private static readonly Dictionary<string, Color> ColorMap = GetColorDictionary();

        /// <summary>
        /// Retrieves a dictionary mapping color names to their corresponding <see cref="Color"/> values.
        /// </summary>
        /// <returns>A dictionary with color names as keys and <see cref="Color"/> objects as values.</returns>
        private static Dictionary<string, Color> GetColorDictionary()
        {
            return Enum.GetValues(typeof(KnownColor))
                .Cast<KnownColor>()
                .Where(kc => kc != KnownColor.Transparent)
                .ToDictionary(kc => kc.ToString().ToLower(), kc => Color.FromKnownColor(kc));
        }

        /// <summary>
        /// Sets the drawing and fill color of the canvas based on the provided color name.
        /// </summary>
        /// <param name="canvas">The canvas on which the pen color will be applied.</param>
        /// <param name="arguments">An array containing the color name as a string.</param>
        public void Execute(ICanvas canvas, string[] arguments)
        {
            if (arguments.Length >= 1)
            {
                string colorName = arguments[0].ToLower();

                if (ColorMap.TryGetValue(colorName, out Color newColor))
                {
                    canvas.DrawingPen = new Pen(newColor);
                    canvas.FillColor = newColor;
                }
                else
                {
                    CommandUtils.ShowError("Invalid pen color. Try using common color names.");
                    return;
                }
            }
            else
            {
                CommandUtils.ShowError("Invalid pen command. Please provide a valid color.");
                return;
            }
        }


    }
}
