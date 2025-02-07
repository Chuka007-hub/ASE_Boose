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
        /// Sets the drawing and fill color of the canvas based on the provided color name or RGB values.
        /// </summary>
        /// <param name="canvas">The canvas on which the pen color will be applied.</param>
        /// <param name="arguments">An array containing the color name or RGB values.</param>
        public void Execute(ICanvas canvas, string[] arguments)
        {
            if (arguments.Length == 1)
            {
                // Handle named color
                string colorName = arguments[0].ToLower();
                if (ColorMap.TryGetValue(colorName, out Color newColor))
                {
                    canvas.DrawingPen = new Pen(newColor);
                    canvas.FillColor = newColor;
                }
                else
                {
                    CommandUtils.ShowError("Invalid pen color. Try using common color names or RGB values (e.g., pen 255 176 176).");
                    return;
                }
            }
            else if (arguments.Length == 3)
            {
                // Handle RGB values
                if (int.TryParse(arguments[0], out int r) && 
                    int.TryParse(arguments[1], out int g) && 
                    int.TryParse(arguments[2], out int b))
                {
                    if (r >= 0 && r <= 255 && g >= 0 && g <= 255 && b >= 0 && b <= 255)
                    {
                        Color newColor = Color.FromArgb(r, g, b);
                        canvas.DrawingPen = new Pen(newColor);
                        canvas.FillColor = newColor;
                    }
                    else
                    {
                        CommandUtils.ShowError("RGB values must be between 0 and 255.");
                        return;
                    }
                }
                else
                {
                    CommandUtils.ShowError("Invalid RGB values. Please provide numbers between 0 and 255.");
                    return;
                }
            }
            else
            {
                CommandUtils.ShowError("Invalid pen command. Use either a color name or RGB values (e.g., pen red or pen 255 176 176).");
                return;
            }
        }
    }
}
