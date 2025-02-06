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
        private readonly Canvas canvas;
        private readonly Dictionary<string, int> variables = new();
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

                    default:
                        if (line.StartsWith("int "))
                        {
                            // Handle variable declaration and assignment
                            string[] parts = line.Substring(4).Split('=');
                            string varName = parts[0].Trim();
                            int value = int.Parse(parts[1].Trim());
                            variables[varName] = value;
                            currentLine++;
                        }
                        else
                        {
                            // Replace variables with their values in the command
                            foreach (var variable in variables)
                            {
                                line = line.Replace(variable.Key, variable.Value.ToString());
                            }

                            // Execute the command
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
        private void ProcessVariableAssignment(string line, Dictionary<string, int> variables)
        {
            string[] parts = line.Split('=');
            if (parts.Length == 2)
            {
                string variableName = parts[0].Trim();
                string value = parts[1].Trim();

                if (value.EndsWith("++"))
                {
                    // Handle increment operation
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
                    // Evaluate arithmetic expression
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
                    // Direct assignment of integer value
                    variables[variableName] = numericValue;
                }
                else if (variables.TryGetValue(value, out int variableValue))
                {
                    // Assign value of another variable
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

        /// <summary>
        /// Executes a while loop based on the provided lines and variables, repeatedly executing commands until the condition fails.
        /// </summary>
        /// <param name="lines">The lines of the script to process.</param>
        /// <param name="startLine">The starting line of the loop.</param>
        /// <param name="variables">The dictionary of variables used in the script.</param>
        /// <returns>The line number after the while loop ends.</returns>
        private int ExecuteWhileLoop(string[] lines, int startLine, Dictionary<string, int> variables)
        {
            // Extract the condition from the 'while' statement
            string condition = lines[startLine].Substring(6).Trim();

            // Find the line where the loop ends
            int endLoopLine = FindEndLoopLine(lines, startLine);

            int currentLine = startLine + 1;

            // Continue executing the loop as long as the condition evaluates to true
            while (EvaluateCondition(condition, variables))
            {
                while (currentLine < endLoopLine)
                {
                    string loopLine = lines[currentLine].Trim();
                    loopLine = SubstituteVariableValues(loopLine, variables);

                    // Check if the line is a recognized command or variable assignment
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

                // Reset the current line to the start of the loop and re-check the condition
                currentLine = startLine + 1;
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
        private int ExecuteIfStatement(string[] lines, int startLine, Dictionary<string, int> variables)
        {
            // Extract the condition for the 'if' statement
            string condition = lines[startLine].Substring(3).Trim();

            // Find the end line for the 'if' block
            int endIfLine = FindEndIfLine(lines, startLine);

            // Evaluate the condition
            if (EvaluateCondition(condition, variables))
            {
                int currentLine = startLine + 1;
                while (currentLine < endIfLine)
                {
                    string ifLine = lines[currentLine].Trim();
                    ifLine = SubstituteVariableValues(ifLine, variables);

                    // Process recognized commands or variable assignments
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

        /// <summary>
        /// Determines if the line represents a variable assignment.
        /// </summary>
        /// <param name="line">The line to check.</param>
        /// <returns>True if the line contains a variable assignment, otherwise false.</returns>
        private bool IsVariableAssignment(string line)
        {
            return line.Contains("=");
        }
    }
}
