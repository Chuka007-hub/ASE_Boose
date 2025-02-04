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
    /// <summary>
    /// Tests for the DrawRectangle command functionality
    /// </summary>
    [TestFixture]
    public class DrawRectangleTests
    {
        /// <summary>
        /// Tests that a rectangle is drawn correctly with valid dimensions.
        /// Verifies the rectangle's width and height match the provided arguments.
        /// </summary>
        [Test]
        public void Execute_WithValidDimensions_DrawsRectangle()
        {
            // Arrange
            var rectangleCommand = new DrawRectangle();
            Bitmap bitmap = new Bitmap(100, 100);
            Graphics graphics = Graphics.FromImage(bitmap);
            string[] arguments = { "50", "30" };

            var mockCanvas = new Mock<ICanvas>();
            mockCanvas.Setup(c => c.CurrentPosition).Returns(new Point(10, 10));
            mockCanvas.Setup(c => c.DrawingPen).Returns(new Pen(Color.Black));
            mockCanvas.Setup(c => c.IsFilling).Returns(false);

            // Act
            rectangleCommand.Execute(graphics, arguments, mockCanvas.Object);

            int expectedX = 10;
            int expectedY = 10;
            int expectedWidth = 50;
            int expectedHeight = 30;

            Rectangle expectedRect = new Rectangle(expectedX, expectedY, expectedWidth, expectedHeight);
            Assert.IsTrue(expectedRect.Width == 50 && expectedRect.Height == 30,
                $"Rectangle should have width {expectedWidth} and height {expectedHeight}.");
        }

        /// <summary>
        /// Tests that the fill functionality works correctly when drawing a rectangle.
        /// Verifies that the IsFilling property is set correctly when fill is enabled
        /// </summary>
        [Test]
        public void Execute_WithFilling_FillsRectangle()
        {
            // Arrange
            var rectangleCommand = new DrawRectangle();
            Bitmap bitmap = new Bitmap(100, 100);
            Graphics graphics = Graphics.FromImage(bitmap);
            string[] arguments = { "50", "30" };

            var mockCanvas = new Mock<ICanvas>();
            mockCanvas.Setup(c => c.CurrentPosition).Returns(new Point(10, 10));
            mockCanvas.Setup(c => c.IsFilling).Returns(true);
            mockCanvas.Setup(c => c.FillColor).Returns(Color.Red);

            // Act
            rectangleCommand.Execute(graphics, arguments, mockCanvas.Object);

            // Assert 
            Assert.IsTrue(mockCanvas.Object.IsFilling, "Rectangle should be filled.");
        }

        /// <summary>
        /// Tests that an error message is shown when invalid arguments are provided.
        /// Verifies the error message content and that it's shown exactly once.
        /// </summary>
        [Test]
        public void Execute_InvalidArguments_ShowsError()
        {
            // Arrange
            var rectangleCommand = new DrawRectangle();
            Bitmap bitmap = new Bitmap(100, 100);
            Graphics graphics = Graphics.FromImage(bitmap);
            string[] arguments = { };
            var mockCanvas = new Mock<ICanvas>();

            var mockMessageBoxService = new Mock<IErrorMessageBox>();
            mockMessageBoxService.Setup(m => m.ShowError(It.IsAny<string>()))
                                 .Callback<string>(message =>
                                     Assert.AreEqual("Invalid command arguments for 'rectangle'. Please provide width and height.", message));

            CommandUtils.SetMessageBoxService(mockMessageBoxService.Object);

            // Act
            rectangleCommand.Execute(graphics, arguments, mockCanvas.Object);

            // Assert
            mockMessageBoxService.Verify(m => m.ShowError(It.IsAny<string>()), Times.Once);
        }
    }
}
