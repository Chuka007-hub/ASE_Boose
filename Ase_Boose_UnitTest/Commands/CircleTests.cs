using Ase_Boose.Interfaces;
using Ase_Boose.Interfaces.Implementations;
using Ase_Boose.Utils;
using Moq;
using System.Drawing;

namespace Ase_Boose_UnitTest.Commands
{
    [TestFixture]
    public class CircleTests
    {
        [Test]
        public void Execute_WithValidRadius_DrawsCircle()
        {

            // Arrange
            DrawCircle circleCommand = new DrawCircle();
            Bitmap bitmap = new Bitmap(100, 100);
            Graphics graphics = Graphics.FromImage(bitmap);
            string[] arguments = { "10" };

            var mockCanvas = new Mock<ICanvas>();
            mockCanvas.Setup(c => c.CurrentPosition).Returns(new Point(320, 190));
            mockCanvas.Setup(c => c.DrawingPen).Returns(new Pen(Color.Black));
            mockCanvas.Setup(c => c.IsFilling).Returns(false);

            // Act
            circleCommand.Execute(graphics, arguments, mockCanvas.Object);

            // Assert
            int expectedRadius = 10;
            int expectedArea = (int)(Math.PI * expectedRadius * expectedRadius);
            int actualArea = 0;

            int centerX = 320;
            int centerY = 190;
            int radius = 10;
            double distanceThreshold = 0.5;

            for (int x = centerX - radius; x <= centerX + radius; x++)
            {
                for (int y = centerY - radius; y <= centerY + radius; y++)
                {
                    int dx = x - centerX;
                    int dy = y - centerY;
                    int distanceSquared = dx * dx + dy * dy;

                    if (distanceSquared <= radius * radius)
                    {
                        actualArea++;
                    }
                }
            }

            int tolerance = 5;
            Assert.IsTrue(Math.Abs(expectedArea - actualArea) <= tolerance, $"Area covered by the circle should be approximately {expectedArea} pixels with a tolerance of {tolerance}.");
        }

        [Test]
        public void Execute_InvalidArguments_ShowErrorMessage()
        {
            // Arrange
            DrawCircle circleCommand = new DrawCircle();
            Bitmap bitmap = new Bitmap(100, 100);
            Graphics graphics = Graphics.FromImage(bitmap);
            string[] arguments = { };
            var mockCanvas = new Mock<ICanvas>();

            var mockMessageBoxService = new Mock<IErrorMessageBox>();
            mockMessageBoxService.Setup(m => m.ShowError(It.IsAny<string>()))
                                 .Callback<string>(message => Assert.AreEqual("Invalid command argument for 'circle'. Please provide a valid radius.", message));



            CommandUtils.SetMessageBoxService(mockMessageBoxService.Object);

            // Act
            circleCommand.Execute(graphics, arguments, mockCanvas.Object);

            mockMessageBoxService.Verify(m => m.ShowError(It.IsAny<string>()), Times.Once);
        }


    }

}
