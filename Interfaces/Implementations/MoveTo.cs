using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ase_Boose.Interfaces.Implementations
{
    internal class MoveTo : IShapeCommand
    {
        public void Execute(Canvas canvas, string[] arguments)
        {
            if (arguments.Length == 2 && int.TryParse(arguments[0], out int x) && int.TryParse(arguments[1], out int y))
            {
                canvas.CurrentPosition = new Point(x, y);
            }
            else
            {
                MessageBox.Show("Invalid arguments for 'moveto' command. Please provide valid coordinates.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
