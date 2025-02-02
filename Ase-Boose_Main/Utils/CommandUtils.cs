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
        private static IErrorMessageBox _messageBox;

        public static void SetMessageBoxService(IErrorMessageBox messageBoxService)
        {
            _messageBox = messageBoxService;
        }

        /// <summary>
        /// Clears the command text box on the canvas, ensuring thread safety by invoking the action on the UI thread if necessary.
        /// </summary>
        /// <param name="canvas">The canvas containing the command text box to be cleared.</param>
        public static void ClearCommandTextBox(ICanvas canvas)
        {
            if (canvas?.CommandTextBox != null)
            {
                // Invoke the method to clear the CommandTextBox on the UI thread
                canvas.CommandTextBox.Invoke((MethodInvoker)(() => canvas.CommandTextBox.Clear()));
            }
        }

        /// <summary>
        /// Displays an error message using a message box, if the message box object is available.
        /// </summary>
        /// <param name="message">The error message to be displayed.</param>
        public static void ShowError(string message)
        {
            // Show the error message if _messageBox is not null
            _messageBox?.ShowError(message);
        }

    }

}
