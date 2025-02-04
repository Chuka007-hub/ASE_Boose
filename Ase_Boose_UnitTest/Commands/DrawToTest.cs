using Ase_Boose.Interfaces.Implementations;
using Ase_Boose.Interfaces;
using Ase_Boose.Utils;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ase_Boose_UnitTest.Commands
{
    /// <summary>
    /// Tests for the DrawTo command functionality
    /// </summary>
    public class DrawToTest
    {
        /// <summary>
        /// Tests that the DrawTo command shows an error message when invalid arguments are provided.
        /// Verifies that the error message is displayed exactly once with the correct text
        /// </summary>
        [Test]
        public void Execute_InvalidArguments_ShowsErrorMessage()
        {
            // Arrange
            var mockCanvas = new Mock<ICanvas>();
            var mockMessageBoxService = new Mock<IErrorMessageBox>();

            mockMessageBoxService.Setup(m => m.ShowError(It.IsAny<string>()))
                                 .Callback<string>(message =>
                                 {
                                     Assert.AreEqual("Invalid arguments for 'drawto' command. Please provide valid coordinates.", message);
                                 });

            CommandUtils.SetMessageBoxService(mockMessageBoxService.Object);

            var drawToCommand = new DrawTo();
            string[] arguments = { "invalid", "argument" };

            // Act
            drawToCommand.Execute(mockCanvas.Object, arguments);

            // Assert
            mockMessageBoxService.Verify(m => m.ShowError(It.IsAny<string>()), Times.Once);
        }



    }
}
