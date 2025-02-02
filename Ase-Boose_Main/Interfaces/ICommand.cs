using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ase_Boose.Interfaces
{
    public interface ICommand
    {
        void Execute(Canvas canvas, string[] arguments);
    }
}
