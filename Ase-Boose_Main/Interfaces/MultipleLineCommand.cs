using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Ase_Boose.Interfaces.Implementations;

namespace Ase_Boose.Interfaces
{
    public class MultipleLineCommand
    {

        private Canvas canvas;
        private readonly Dictionary<string, double> variables = new();
        private readonly ArrayCommand arrayHandler = new();
        private Dictionary<string, Method> methods = new();
        private readonly Shapemaker shapemaker;


        public MultipleLineCommand(Canvas canvas)
        {
            this.canvas = canvas;
            this.shapemaker = new Shapemaker(canvas);
        }

        /// <summary>
        /// Executes a series of commands from a script content, handling variables, loops, and conditionals.
        /// </summary>
        /// <param name="scriptContent">The script content containing commands to execute, separated by new lines.</param>
        public void ExecuteCommands(string scriptContent)
        {
            string[] lines = scriptContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
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
                    case "endwhile":
                        currentLine++;
                        break;

                    case "array":
                        HandleArrayDeclaration(line);
                        currentLine++;
                        break;
                    case "poke":
                    case "peek":
                        HandleArrayOperation(line);
                        currentLine++;
                        break;

                    case "for":
                        currentLine = ExecuteForLoop(lines, currentLine, variables);
                        break;

                    case "method":
                        HandleMethodDefinition(lines, currentLine);
                        currentLine = FindEndMethodLine(lines, currentLine) + 1;
                        break;
                    case "call":
                        HandleMethodCall(line, variables);
                        currentLine++;
                        break;

                    default:
                        if (line.StartsWith("int "))
                        {
                            string[] parts = line.Substring(4).Split('=');
                            string varName = parts[0].Trim();
                            if (parts.Length > 1)
                            {
                                int value = int.Parse(parts[1].Trim());
                                variables[varName] = value;
                            }
                            else
                            {
                                variables[varName] = 0; // Initialize without value
                            }
                            currentLine++;
                        }
                        else if (line.Contains("="))
                        {
                            ProcessVariableAssignment(line, variables);
                            currentLine++;
                        }
                        else
                        {
                            foreach (var variable in variables)
                            {
                                line = line.Replace(variable.Key, variable.Value.ToString());
                            }

                            if (IsRecognizedCommand(line))
                            {
                                canvas.Invoke((MethodInvoker)delegate
                                {
                                    CommandParser parser = new CommandParser(line);
                                    shapemaker.ExecuteDrawing(parser);
                                });
                            }
                            currentLine++;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Shows an error message box with the specified message.
        /// </summary>
        /// <param name="message">The message to display in the error dialog.</param>
        private void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Checks if the value contains arithmetic operations like +, -, *, or /.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>True if the value contains an arithmetic operation, otherwise false.</returns>
        private bool IsArithmeticExpression(string value)
        {
            return value.Contains("+") || value.Contains("-") || value.Contains("*") || value.Contains("/");
        }

        /// <summary>
        /// Processes a variable assignment, handling arithmetic expressions and increment operations.
        /// </summary>
        /// <param name="line">The assignment line to process.</param>
        /// <param name="variables">A dictionary containing all defined variables.</param>
        private void ProcessVariableAssignment(string line, Dictionary<string, double> variables)
        {
            string[] parts = line.Split('=');
            if (parts.Length == 2)
            {
                string varPart = parts[0].Trim();
                string valuePart = parts[1].Trim();

                string[] varDeclaration = varPart.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string varName = varDeclaration[varDeclaration.Length - 1];

                // Handle numeric expression evaluation
                try
                {
                    DataTable dt = new DataTable();
                    var result = dt.Compute(SubstituteVariableValues(valuePart, variables), "");
                    double value = Convert.ToDouble(result);
                    variables[varName] = value;
                }
                catch (Exception)
                {
                    ShowError($"Invalid expression: {valuePart}");
                }
            }
        }

        /// <summary>
        /// Executes a while loop based on the provided lines and variables, repeatedly executing commands until the condition fails.
        /// </summary>
        /// <param name="lines">The lines of the script to process.</param>
        /// <param name="startLine">The starting line of the loop.</param>
        /// <param name="variables">The dictionary of variables used in the script.</param>
        /// <returns>The line number after the while loop ends.</returns>
        private int ExecuteWhileLoop(string[] lines, int startLine, Dictionary<string, double> variables)
        {
            string condition = lines[startLine].Substring(6).Trim();
            int endLoopLine = FindEndLoopLine(lines, startLine);
            
            // Extract variable name from condition (e.g., "height > 50" -> "height")
            string variableName = condition.Split(new[] { '<', '>', '=', '!' })[0].Trim();
            
            // Ensure variable exists
            if (!variables.ContainsKey(variableName))
            {
                ShowError($"Variable '{variableName}' used in while loop is not defined");
                return endLoopLine + 1;
            }

            condition = SubstituteVariableValues(condition, variables);
            while (EvaluateCondition(condition, variables))
            {
                int currentLine = startLine + 1;
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

                condition = SubstituteVariableValues(condition, variables);
            }

            return endLoopLine + 1;
        }

        /// <summary>
        /// Evaluates an arithmetic expression using DataTable.Compute.
        /// </summary>
        /// <param name="expression">The arithmetic expression to evaluate.</param>
        /// <param name="variables">The dictionary of variables used in the expression.</param>
        /// <returns>The result of the expression as an integer, or int.MinValue if an error occurs.</returns>
        private int EvaluateArithmeticOperation(string expression, Dictionary<string, int> variables)
        {
            try
            {
                // Replace variable placeholders with actual values in the expression
                foreach (var variable in variables)
                {
                    expression = expression.Replace(variable.Key, variable.Value.ToString());
                }

                DataTable dt = new DataTable();
                var result = dt.Compute(expression, string.Empty);
                return Convert.ToInt32(result);
            }
            catch (Exception)
            {
                // Return a sentinel value to indicate an error
                return int.MinValue;
            }
        }

        /// <summary>
        /// Finds the line number where the 'endloop' statement is located for the while loop.
        /// </summary>
        /// <param name="lines">The script lines to search through.</param>
        /// <param name="startLine">The starting line of the loop.</param>
        /// <returns>The line number of the 'endloop' statement, or -1 if not found.</returns>
        private int FindEndLoopLine(string[] lines, int startLine)
        {
            for (int i = startLine + 1; i < lines.Length; i++)
            {
                if (lines[i].Trim().Equals("endloop", StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }

            // Show error if 'endloop' not found
            MessageBox.Show("Endloop not found for the while loop starting at line " + startLine,
                             "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return -1;
        }

        /// <summary>
        /// Executes the 'if' statement logic by evaluating the condition and executing the enclosed commands.
        /// </summary>
        /// <param name="lines">The script lines to process.</param>
        /// <param name="startLine">The line number where the 'if' statement begins.</param>
        /// <param name="variables">The dictionary of variables used in the script.</param>
        /// <returns>The line number after the 'if' block has been executed.</returns>
        private int ExecuteIfStatement(string[] lines, int startLine, Dictionary<string, double> variables)
        {
            string condition = lines[startLine].Substring(3).Trim();
            int endIfLine = FindEndIfLine(lines, startLine);
            
            if (EvaluateCondition(condition, variables))
            {
                int currentLine = startLine + 1;
                while (currentLine < endIfLine)
                {
                    string ifLine = lines[currentLine].Trim();
                    
                    // Replace variables with their values
                    foreach (var variable in variables)
                    {
                        ifLine = ifLine.Replace(variable.Key, variable.Value.ToString());
                    }

                    if (IsRecognizedCommand(ifLine))
                    {
                        canvas.Invoke((MethodInvoker)delegate
                        {
                            CommandParser parser = new CommandParser(ifLine);
                            shapemaker.ExecuteDrawing(parser);
                        });
                    }
                    currentLine++;
                }
            }
            return endIfLine + 1;
        }

        /// <summary>
        /// Finds the line number where the 'endif' statement is located.
        /// </summary>
        /// <param name="lines">The script lines to search through.</param>
        /// <param name="startLine">The line number where the 'if' statement starts.</param>
        /// <returns>The line number of the 'endif' statement.</returns>
        private int FindEndIfLine(string[] lines, int startLine)
        {
            for (int i = startLine + 1; i < lines.Length; i++)
            {
                if (lines[i].Trim().Equals("endif", StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }

            // If 'endif' is not found, show an error message
            MessageBox.Show("Endif not found for the if statement starting at line " + startLine,
                             "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return -1;
        }

        /// <summary>
        /// Evaluates the condition based on the provided condition string and variable values.
        /// The condition is compared using operators like '<', '>', '=', and '!='.
        /// </summary>
        /// <param name="condition">The condition to evaluate as a string.</param>
        /// <param name="variables">A dictionary of variable names and their corresponding integer values.</param>
        /// <returns>True if the condition is met, otherwise false.</returns>

        private bool EvaluateCondition(string condition, Dictionary<string, double> variables)
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

                if (variables.TryGetValue(variableName, out double variableValue))
                {
                    if (double.TryParse(valuePart, out double conditionValue))
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
                            return Math.Abs(variableValue - conditionValue) < 0.0001; // For floating point comparison
                        }
                        else if (condition.Contains("!="))
                        {
                            return Math.Abs(variableValue - conditionValue) >= 0.0001;
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

        /// <summary>
        /// Checks if the provided line contains a recognized command.
        /// A recognized command is one of the predefined set such as "moveto", "drawto", etc.
        /// </summary>
        /// <param name="line">The line to check for a recognized command.</param>
        /// <returns>True if the command is recognized, otherwise false.</returns>
        private bool IsRecognizedCommand(string line)
        {
            string command = line.Split(' ')[0].ToLower();

            var recognizedCommands = new HashSet<string> { "moveto", "drawto", "fill", "reset", "clear", "pen", "rectangle", "circle", "triangle" };

            return recognizedCommands.Contains(command);
        }

        /// <summary>
        /// Substitutes variable values in the given line with their corresponding values from the variables dictionary.
        /// The first token (command) is left unchanged, and all subsequent tokens are checked for variable substitution.
        /// </summary>
        /// <param name="line">The line containing the command and variable placeholders.</param>
        /// <param name="variables">A dictionary of variables and their integer values to substitute into the line.</param>
        /// <returns>The line with variable values substituted where applicable.</returns>

        private string SubstituteVariableValues(string expression, Dictionary<string, double> variables)
        {
            foreach (var variable in variables)
            {
                expression = expression.Replace(variable.Key, variable.Value.ToString(CultureInfo.InvariantCulture));
            }
            return expression;
        }

        private void HandleArrayDeclaration(string line)
        {
            string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 4 && parts[0] == "array")
            {
                arrayHandler.Execute(canvas, new[] { parts[1], parts[2], parts[3] });
            }
        }

        private void HandleArrayOperation(string line)
        {
            string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 5)
            {
                if (parts[0] == "poke")
                {
                    string arrayName = parts[1];
                    if (int.TryParse(parts[2], out int index) && double.TryParse(parts[4], out double value))
                    {
                        arrayHandler.Poke(arrayName, index, value);
                    }
                }
                else if (parts[0] == "peek")
                {
                    string varName = parts[1];
                    string arrayName = parts[3];
                    if (int.TryParse(parts[4], out int index))
                    {
                        double value = arrayHandler.Peek(arrayName, index);
                        variables[varName] = value;
                    }
                }
            }
        }

        private int ExecuteForLoop(string[] lines, int startLine, Dictionary<string, double> variables)
        {
            foreach (var result in ForLoop.Execute(lines, startLine, variables))
            {
                int lineNumber = result.lineNumber;
                string line = result.line;
                
                if (IsRecognizedCommand(line))
                {
                    string processedLine = SubstituteVariableValues(line, variables);
                    canvas.Invoke((MethodInvoker)delegate
                    {
                        CommandParser parser = new CommandParser(processedLine);
                        shapemaker.ExecuteDrawing(parser);
                    });
                }
                else if (IsVariableAssignment(line))
                {
                    ProcessVariableAssignment(line, variables);
                }
            }
            
            // Find the end of the for loop
            for (int i = startLine + 1; i < lines.Length; i++)
            {
                if (lines[i].Trim().ToLower() == "end for")
                {
                    return i + 1;
                }
            }
            return lines.Length;
        }

        private void HandleMethodDefinition(string[] lines, int startLine)
        {
            string line = lines[startLine];
            string[] parts = line.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            
            Method method = new Method
            {
                ReturnType = parts[1],
                Name = parts[2],
                Parameters = new List<(string type, string name)>(),
                StartLine = startLine
            };

            // Parse parameters
            for (int i = 3; i < parts.Length; i += 2)
            {
                method.Parameters.Add((parts[i], parts[i + 1]));
            }

            // Find end of method
            for (int i = startLine + 1; i < lines.Length; i++)
            {
                if (lines[i].Trim().ToLower() == "end method")
                {
                    method.EndLine = i;
                    break;
                }
            }

            methods[method.Name] = method;
        }

        private void HandleMethodCall(string line, Dictionary<string, double> variables)
        {
            string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string methodName = parts[1];
            
            if (methods.TryGetValue(methodName, out Method method))
            {
                double[] arguments = new double[parts.Length - 2];
                for (int i = 2; i < parts.Length; i++)
                {
                    if (variables.TryGetValue(parts[i], out double value))
                    {
                        arguments[i - 2] = value;
                    }
                    else
                    {
                        arguments[i - 2] = double.Parse(parts[i]);
                    }
                }

                double result = method.Execute(lines, variables, arguments);
                variables[methodName] = result;
            }
        }

        private int FindEndMethodLine(string[] lines, int startLine)
        {
            for (int i = startLine + 1; i < lines.Length; i++)
            {
                if (lines[i].Trim().ToLower() == "end method")
                {
                    return i;
                }
            }
            return lines.Length - 1;
        }

        private bool IsVariableAssignment(string line)
        {
            return line.Contains("=") && !line.Contains("==");
        }

    }
}
