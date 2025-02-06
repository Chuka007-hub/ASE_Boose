using System;
using System.Collections.Generic;

namespace Ase_Boose.Interfaces.Implementations
{
    public class ForLoop
    {
        public static int Execute(string[] lines, int startLine, Dictionary<string, double> variables)
        {
            string line = lines[startLine];
            string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            
            if (parts.Length < 7) // for count = 1 to 10 step 2
            {
                CommandUtils.ShowError("Invalid for loop syntax");
                return startLine + 1;
            }

            string varName = parts[1];
            double start = double.Parse(parts[3]);
            double end = double.Parse(parts[5]);
            double step = parts.Length >= 8 ? double.Parse(parts[7]) : 1;

            int endForLine = FindEndForLine(lines, startLine);
            if (endForLine == -1)
            {
                CommandUtils.ShowError("Missing 'end for'");
                return lines.Length;
            }

            variables[varName] = start;
            while ((step > 0 && variables[varName] <= end) || 
                   (step < 0 && variables[varName] >= end))
            {
                for (int i = startLine + 1; i < endForLine; i++)
                {
                    string currentLine = lines[i].Trim();
                    if (!string.IsNullOrEmpty(currentLine))
                    {
                        yield return (i, currentLine);
                    }
                }
                variables[varName] += step;
            }

            return endForLine + 1;
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