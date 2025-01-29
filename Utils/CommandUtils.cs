using Ase_Boose.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ase_Boose.Utils
{
    public static class CommandUtils
    {
        public static void ClearCommandTextBox(ICanvas canvas)
        {
            canvas.CommandTextBox.Invoke((MethodInvoker)(() => canvas.CommandTextBox.Clear()));
        }

        public static void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
