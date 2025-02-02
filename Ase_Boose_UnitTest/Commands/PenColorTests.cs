using Ase_Boose.Interfaces.Implementations;
using Ase_Boose.Interfaces;
using Ase_Boose.Utils;
using Moq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ase_Boose_UnitTest.Commands
{
    public class PenColorTests
    {
        [Test]
        public void Execute_ValidColor_ChangesPenAndFillColor()
        {
            // Arrange
            var penColorCommand = new PenColor();
            var mockCanvas = new Mock<ICanvas>();
            mockCanvas.SetupProperty(c => c.DrawingPen);
            mockCanvas.SetupProperty(c => c.FillColor);

            string[] arguments = { "red" };

            // Act
            penColorCommand.Execute(mockCanvas.Object, arguments);

            // Assert
            Assert.AreEqual(Color.Red, mockCanvas.Object.DrawingPen.Color, "Pen color should be set to Red.");
            Assert.AreEqual(Color.Red, mockCanvas.Object.FillColor, "Fill color should be set to Red.");
        }

        [Test]
        public void Execute_InvalidColor_ShowsErrorMessage()
        {
            // Arrange
            var penColorCommand = new PenColor();
            var mockCanvas = new Mock<ICanvas>();

            string[] arguments = { "invalidcolor" };

            var mockMessageBoxService = new Mock<IErrorMessageBox>();
            mockMessageBoxService.Setup(m => m.ShowError(It.IsAny<string>()))
                                 .Callback<string>(message =>
                                     Assert.AreEqual("Invalid pen color. Try using common color names.", message));

            CommandUtils.SetMessageBoxService(mockMessageBoxService.Object);

            // Act
            penColorCommand.Execute(mockCanvas.Object, arguments);

            // Assert
            mockMessageBoxService.Verify(m => m.ShowError(It.IsAny<string>()), Times.Once);
        }


        [Test]
        public void Execute_NoArguments_ShowsErrorMessage()
        {
            // Arrange
            var penColorCommand = new PenColor();
            var mockCanvas = new Mock<ICanvas>();

            string[] arguments = { };

            var mockMessageBoxService = new Mock<IErrorMessageBox>();

            mockMessageBoxService.Setup(m => m.ShowError(It.IsAny<string>()))
                                 .Callback<string>(message =>
                                     Assert.AreEqual("Invalid pen command. Please provide a valid color.", message));

            // Set the mock service
            CommandUtils.SetMessageBoxService(mockMessageBoxService.Object);

            // Act
            penColorCommand.Execute(mockCanvas.Object, arguments);

            // Assert
            mockMessageBoxService.Verify(m => m.ShowError(It.IsAny<string>()), Times.Once);
        }

    }

}
