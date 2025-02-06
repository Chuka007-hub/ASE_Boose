using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ase_Boose.Interfaces
{
    public interface ICanvas
    {
        void AddDrawingCommand(Action<Graphics> command);
        void ClearDrawings();
        Point CurrentPosition { get; set; }
        TextBox CommandTextBox { get; }
        PictureBox PictureBox { get; }
        Pen DrawingPen { get; set; }
        Color FillColor { get; set; }
        bool IsFilling { get; set; }
    }
}
