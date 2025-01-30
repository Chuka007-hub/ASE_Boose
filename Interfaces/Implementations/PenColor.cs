using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ase_Boose.Interfaces.Implementations
{
    internal class PenColor : IShapeCommand
    {
        private static readonly Dictionary<string, Color> ColorMap = GetColorDictionary();

        private static Dictionary<string, Color> GetColorDictionary()
        {
            return Enum.GetValues(typeof(KnownColor))
                .Cast<KnownColor>()
                .Where(kc => kc != KnownColor.Transparent) 
                .ToDictionary(kc => kc.ToString().ToLower(), kc => Color.FromKnownColor(kc));
        }

        public void Execute(Canvas canvas, string[] argument)
        {
            if (argument.Length >= 1)
            {
                string colorName = argument[0].ToLower();

                if (ColorMap.TryGetValue(colorName, out Color newColor))
                {
                    canvas.DrawingPen = new Pen(newColor);
                    canvas.FillColor = newColor;
                }
                else
                {
                    MessageBox.Show("Invalid pen color. Try using common color names.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Invalid pen command. Please provide a valid color.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
