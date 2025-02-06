using System;
using System.Collections.Generic;

namespace Ase_Boose.Interfaces.Implementations
{
    public class ArrayCommand : IShapeCommand
    {
        private Dictionary<string, Array> intArrays = new();
        private Dictionary<string, Array> realArrays = new();

        public void Execute(ICanvas canvas, string[] arguments)
        {
            if (arguments.Length < 3)
            {
                CommandUtils.ShowError("Invalid array command. Format: array <type> <name> <size>");
                return;
            }

            string type = arguments[0];
            string name = arguments[1];
            if (!int.TryParse(arguments[2], out int size))
            {
                CommandUtils.ShowError("Invalid array size");
                return;
            }

            switch (type.ToLower())
            {
                case "int":
                    intArrays[name] = new int[size];
                    break;
                case "real":
                    realArrays[name] = new double[size];
                    break;
                default:
                    CommandUtils.ShowError("Invalid array type. Use 'int' or 'real'");
                    break;
            }
        }

        public void Poke(string arrayName, int index, double value)
        {
            if (intArrays.ContainsKey(arrayName))
            {
                ((int[])intArrays[arrayName])[index] = (int)value;
            }
            else if (realArrays.ContainsKey(arrayName))
            {
                ((double[])realArrays[arrayName])[index] = value;
            }
            else
            {
                CommandUtils.ShowError($"Array {arrayName} not found");
            }
        }

        public double Peek(string arrayName, int index)
        {
            if (intArrays.ContainsKey(arrayName))
            {
                return ((int[])intArrays[arrayName])[index];
            }
            else if (realArrays.ContainsKey(arrayName))
            {
                return ((double[])realArrays[arrayName])[index];
            }
            else
            {
                CommandUtils.ShowError($"Array {arrayName} not found");
                return 0;
            }
        }
    }
}
