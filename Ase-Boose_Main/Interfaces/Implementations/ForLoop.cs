using System;
using System.Collections.Generic;
using Ase_Boose.Utils;

namespace Ase_Boose.Interfaces.Implementations
{
    public class ForLoop
    {
        public static IEnumerable<(int lineNumber, string line)> Execute(string[] lines, int startLine, Dictionary<string, double> variables)
        {
            string line = lines[startLine];
            string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            
            if (parts.Length < 7) // for count = 1 to 10 step 2
            {
                CommandUtils.ShowError("Invalid for loop syntax");
                yield break;
            }

            string varName = parts[1];
            if (!double.TryParse(parts[3], out double start) || 
                !double.TryParse(parts[5], out double end) || 
                (parts.Length >= 8 && !double.TryParse(parts[7], out double step)))
            {
                CommandUtils.ShowError("Invalid numeric values in for loop");
                yield break;
            }

            double stepValue = parts.Length >= 8 ? double.Parse(parts[7]) : 1;

            int endForLine = FindEndForLine(lines, startLine);
            if (endForLine == -1)
            {
                CommandUtils.ShowError("Missing 'end for'");
                yield break;
            }

            variables[varName] = start;
            while ((stepValue > 0 && variables[varName] <= end) || 
                   (stepValue < 0 && variables[varName] >= end))
            {
                for (int i = startLine + 1; i < endForLine; i++)
                {
                    string currentLine = lines[i].Trim();
                    if (!string.IsNullOrEmpty(currentLine))
                    {
                        yield return (i, currentLine);
                    }
                }
                variables[varName] += stepValue;
            }
        }

        private static int FindEndForLine(string[] lines, int startLine)
        {
            for (int i = startLine + 1; i < lines.Length; i++)
            {
                if (lines[i].Trim().ToLower() == "end for")
                {
                    return i;
                }
            }
            return -1;
        }
    }
} 