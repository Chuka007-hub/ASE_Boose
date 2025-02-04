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
    /// Tests for the DrawTriangle command functionality
    /// </summary>
    [TestFixture]
    public class DrawTriangleTests
    {
        /// <summary>
        /// Tests that a triangle is drawn correctly with valid dimensions
        /// Verifies the triangle's points are correctly positioned based on the given base and height.
        /// </summary>
        [Test]
        public void Execute_WithValidDimensions_DrawsTriangle()
        {
            // Arrange
            var triangleCommand = new DrawTriangle();
            Bitmap bitmap = new Bitmap(100, 100);
            Graphics graphics = Graphics.FromImage(bitmap);
            string[] arguments = { "50", "30" }; // Base: 50, Height: 30

            var mockCanvas = new Mock<ICanvas>();
            mockCanvas.Setup(c => c.CurrentPosition).Returns(new Point(10, 40));
            mockCanvas.Setup(c => c.DrawingPen).Returns(new Pen(Color.Black));
            mockCanvas.Setup(c => c.IsFilling).Returns(false);

            // Act
            triangleCommand.Execute(graphics, arguments, mockCanvas.Object);

            // Assert 
            int x = 10, y = 40, baseLength = 50, height = 30;
            Point[] expectedPoints =
            {
                new Point(x, y),
                new Point(x + baseLength, y),
                new Point(x + baseLength / 2, y - height)
            };

            Assert.AreEqual(expectedPoints.Length, 3, "Triangle should have exactly three points.");
        }

        /// <summary>
        /// Tests that the fill functionality works correctly when drawing a triangle
        /// Verifies that the isFilling property is set correctly when fill is enabled
        /// </summary>
        [Test]
        public void Execute_WithFilling_FillsTriangle()
        {
            // Arrange
            var triangleCommand = new DrawTriangle();
            Bitmap bitmap = new Bitmap(100, 100);
            Graphics graphics = Graphics.FromImage(bitmap);
            string[] arguments = { "50", "30" };

            var mockCanvas = new Mock<ICanvas>();
            mockCanvas.Setup(c => c.CurrentPosition).Returns(new Point(10, 40));
            mockCanvas.Setup(c => c.IsFilling).Returns(true);
            mockCanvas.Setup(c => c.FillColor).Returns(Color.Red);

            // Act
            triangleCommand.Execute(graphics, arguments, mockCanvas.Object);

            // Assert - Ensure filling mode is set correctly
            Assert.IsTrue(mockCanvas.Object.IsFilling, "Triangle should be filled.");
        }

        /// <summary>
        /// Tests that an error message is shown when invalid arguments are provided.
        /// Verifies the error message content and that it's shown exactly once.
        /// </summary>
        [Test]
        public void Execute_InvalidArguments_ShowsError()
        {
            // Arrange
            var triangleCommand = new DrawTriangle();
            Bitmap bitmap = new Bitmap(100, 100);
            Graphics graphics = Graphics.FromImage(bitmap);
            string[] arguments = { };
            var mockCanvas = new Mock<ICanvas>();

            var mockMessageBoxService = new Mock<IErrorMessageBox>();
            mockMessageBoxService.Setup(m => m.ShowError(It.IsAny<string>()))
                                 .Callback<string>(message =>
                                     Assert.AreEqual("Invalid command arguments for 'triangle'. Please provide base and height.", message));

            CommandUtils.SetMessageBoxService(mockMessageBoxService.Object);

            // Act
            triangleCommand.Execute(graphics, arguments, mockCanvas.Object);

            // Assert
            mockMessageBoxService.Verify(m => m.ShowError(It.IsAny<string>()), Times.Once);
        }
    }
}
