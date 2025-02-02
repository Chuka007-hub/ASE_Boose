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
    public class FillColorTest
    {
        [Test]
        public void Execute_MissingArgument_ShowsErrorMessage()
        {
            // Arrange
            var fillColorCommand = new fillColor();
            var mockCanvas = new Mock<ICanvas>();

            string[] arguments = { };

            var mockMessageBoxService = new Mock<IErrorMessageBox>();

            mockMessageBoxService.Setup(m => m.ShowError(It.IsAny<string>()))
                                 .Callback<string>(message =>
                                     Assert.AreEqual("Missing argument for 'fill' command. Use 'on' or 'off'.", message));

            CommandUtils.SetMessageBoxService(mockMessageBoxService.Object);

            // Act
            fillColorCommand.Execute(mockCanvas.Object, arguments);

            // Assert
            mockMessageBoxService.Verify(m => m.ShowError(It.IsAny<string>()), Times.Once);
        }

    }

}
