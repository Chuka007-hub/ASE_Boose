using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ase_Boose.Interfaces.Implementations
{
    public class ErrorMessageBox : IErrorMessageBox
    {
        /// <summary>
        /// Displays an error message in a message box.
        /// </summary>
        /// <param name="message">The error message to be displayed.</param>
        public void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
