using System;
using System.Collections.Generic;

namespace Ase_Boose.Interfaces.Implementations
{
    public class Method
    {
        public string Name { get; set; }
        public string ReturnType { get; set; }
        public List<(string type, string name)> Parameters { get; set; }
        public int StartLine { get; set; }
        public int EndLine { get; set; }

        public double Execute(string[] lines, Dictionary<string, double> globalVariables, double[] arguments)
        {
            var localVariables = new Dictionary<string, double>(globalVariables);
            
            // Set parameter values
            for (int i = 0; i < Parameters.Count; i++)
            {
                localVariables[Parameters[i].name] = arguments[i];
            }

            // Execute method body
            for (int i = StartLine + 1; i < EndLine; i++)
            {
                string line = lines[i].Trim();
                if (line.StartsWith(Name + "="))
                {
                    string expression = line.Substring(Name.Length + 1).Trim();
                    expression = SubstituteVariableValues(expression, localVariables);
                    return EvaluateExpression(expression);
                }
            }
            return 0;
        }

        private string SubstituteVariableValues(string expression, Dictionary<string, double> variables)
        {
            foreach (var variable in variables)
            {
                expression = expression.Replace(variable.Key, variable.Value.ToString());
            }
            return expression;
        }

        private double EvaluateExpression(string expression)
        {
            var dt = new System.Data.DataTable();
            var result = dt.Compute(expression, "");
            return Convert.ToDouble(result);
        }
    }
} 