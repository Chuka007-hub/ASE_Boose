using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ase_Boose.Interfaces
{
    public class MultipleLineCommand
    {

        private Canvas canvas;
        private readonly Dictionary<string, int> variables = new();


        public MultipleLineCommand(Canvas canvas)
        {
            this.canvas = canvas;
        }

        public void ExecuteCommands(string scriptContent)
        {
            string[] lines = scriptContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            var variables = new Dictionary<string, int>();
            int currentLine = 0;

            while (currentLine < lines.Length)
            {
                string line = lines[currentLine].Trim();
                string command = line.Split(' ')[0].ToLower();

                switch (command)
                {
                    case "while":
                        currentLine = ExecuteWhileLoop(lines, currentLine, variables);
                        break;

                    case "if":
                        currentLine = ExecuteIfStatement(lines, currentLine, variables);
                        break;

                    case "endif":
                        currentLine++;
                        break;

                    default:
                        if (IsVariableAssignment(line))
                        {
                            ProcessVariableAssignment(line, variables);
                        }
                        else if (IsRecognizedCommand(line))
                        {
                            ExecuteCommand(line, variables);
                        }
                        currentLine++;
                        break;
                }
            }
        }

        private void ExecuteCommand(string line, Dictionary<string, int> variables)
        {
            line = SubstituteVariableValues(line, variables);
            canvas.Invoke((MethodInvoker)delegate
            {
                CommandParser parser = new CommandParser(line);
                canvas.shapemaker.ExecuteDrawing(parser);
            });
        }


        private bool IsVariableAssignment(string line)
        {
            return line.Contains("=");
        }


        private void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool IsArithmeticExpression(string value)
        {
            return value.Contains("+") || value.Contains("-") || value.Contains("*") || value.Contains("/");
        }

        private void ProcessVariableAssignment(string line, Dictionary<string, int> variables)
        {
            string[] parts = line.Split('=');
            if (parts.Length == 2)
            {
                string variableName = parts[0].Trim();
                string value = parts[1].Trim();

                if (value.EndsWith("++"))
                {
                    value = value.TrimEnd('+');
                    if (variables.TryGetValue(value, out int previousValue))
                    {
                        variables[variableName] = previousValue + 1;
                    }
                    else
                    {
                        ShowError($"Variable '{value}' not found.");
                    }
                }
                else if (IsArithmeticExpression(value))
                {
                    int result = EvaluateArithmeticOperation(value, variables);
                    if (result != int.MinValue)
                    {
                        variables[variableName] = result;
                    }
                    else
                    {
                        ShowError($"Error evaluating arithmetic expression: {value}");
                    }
                }
                else if (int.TryParse(value, out int numericValue))
                {
                    variables[variableName] = numericValue;
                }
                else if (variables.TryGetValue(value, out int variableValue))
                {
                    variables[variableName] = variableValue;
                }
                else
                {
                    ShowError($"Invalid assignment: {line}");
                }
            }
            else
            {
                ShowError($"Invalid assignment: {line}");
            }
        }


        private int ExecuteWhileLoop(string[] lines, int startLine, Dictionary<string, int> variables)
        {
            string condition = lines[startLine].Substring(6); 

            int endLoopLine = FindEndLoopLine(lines, startLine);

            int currentLine = startLine + 1;

            while (EvaluateCondition(condition, variables))
            {
                while (currentLine < endLoopLine)
                {
                    string loopLine = lines[currentLine].Trim();

                    loopLine = SubstituteVariableValues(loopLine, variables);

                    if (IsRecognizedCommand(loopLine))
                    {
                        canvas.Invoke((MethodInvoker)delegate
                        {
                            CommandParser parser = new CommandParser(loopLine);
                            canvas.shapemaker.ExecuteDrawing(parser);
                        });
                    }
                    else if (IsVariableAssignment(loopLine))
                    {
                        ProcessVariableAssignment(loopLine, variables);
                    }
                    currentLine++;
                }

                currentLine = startLine + 1;

                condition = lines[startLine].Substring(6).Trim();
                condition = SubstituteVariableValues(condition, variables);
            }

            return endLoopLine + 1;
        }



        private int EvaluateArithmeticOperation(string expression, Dictionary<string, int> variables)
        {
            try
            {
                DataTable dt = new DataTable();
                var result = dt.Compute(expression, "");
                return Convert.ToInt32(result);
            }
            catch (Exception)
            {
                return int.MinValue;
            }
        }


        private int FindEndLoopLine(string[] lines, int startLine)
        {
            for (int i = startLine + 1; i < lines.Length; i++)
            {
                if (lines[i].Trim().Equals("endloop", StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }
            MessageBox.Show("Endloop not found for the while loop starting at line " + startLine, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return -1; 
        }

          private int ExecuteIfStatement(string[] lines, int startLine, Dictionary<string, int> variables)
        {
            string condition = lines[startLine].Substring(3).Trim(); 

            int endIfLine = FindEndIfLine(lines, startLine);

            if (EvaluateCondition(condition, variables))
            {
                int currentLine = startLine + 1;
                while (currentLine < endIfLine)
                {
                    string ifLine = lines[currentLine].Trim();

                    ifLine = SubstituteVariableValues(ifLine, variables);

                    if (IsRecognizedCommand(ifLine))
                    {
                        canvas.Invoke((MethodInvoker)delegate
                        {
                            CommandParser parser = new CommandParser(ifLine);
                            canvas.shapemaker.ExecuteDrawing(parser);
                        });
                    }
                    currentLine++;
                }
            }

            return endIfLine + 1;
        }






        private int FindEndIfLine(string[] lines, int startLine)
        {
            for (int i = startLine + 1; i < lines.Length; i++)
            {
                if (lines[i].Trim().Equals("endif", StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }

            MessageBox.Show("Endif not found for the if statement starting at line " + startLine, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return -1;
        }



       private bool EvaluateCondition(string condition, Dictionary<string, int> variables)
        {
            string[] parts;
            if (condition.Contains("!="))
            {
                parts = condition.Split(new[] { '!', '=' }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                parts = condition.Split(new[] { '<', '>', '=' }, StringSplitOptions.RemoveEmptyEntries);
            }

            if (parts.Length == 2)
            {
                string variableName = parts[0].Trim();
                string valuePart = parts[1].Trim();

                if (variables.TryGetValue(variableName, out int variableValue))
                {
                    if (int.TryParse(valuePart, out int conditionValue))
                    {
                        if (condition.Contains("<"))
                        {
                            return variableValue < conditionValue;
                        }
                        else if (condition.Contains(">"))
                        {
                            return variableValue > conditionValue;
                        }
                        else if (condition.Contains("="))
                        {
                            return variableValue == conditionValue;
                        }
                        else if (condition.Contains("!="))
                        {
                            return variableValue != conditionValue;
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Invalid number format in condition: {valuePart}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show($"Variable '{variableName}' is not defined.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show($"Invalid condition format: {condition}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }


     private bool IsRecognizedCommand(string line)
        {
            string command = line.Split(' ')[0].ToLower();

            var recognizedCommands = new HashSet<string> { "moveto", "drawto", "fill", "reset", "clear", "pen", "rectangle", "circle", "triangle" };

            return recognizedCommands.Contains(command);
        }


        private string SubstituteVariableValues(string line, Dictionary<string, int> variables)
        {
            string[] tokens = line.Split(' ');

            for (int i = 1; i < tokens.Length; i++) 
            {
                if (variables.ContainsKey(tokens[i])) 
                {
                    tokens[i] = variables[tokens[i]].ToString();
                }
            }

            return string.Join(" ", tokens);
        }


    }
}
