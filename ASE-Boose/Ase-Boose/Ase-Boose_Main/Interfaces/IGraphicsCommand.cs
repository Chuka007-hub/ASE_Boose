using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ase_Boose.Interfaces
{
    public interface IGraphicsCommand
    {
        void Execute(Graphics graphics, string[] arguments, ICanvas canvas);
    }
}
