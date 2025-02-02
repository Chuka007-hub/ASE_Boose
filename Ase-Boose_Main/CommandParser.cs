using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ase_Boose
{
    public class CommandParser
    {
        public string Command { get; private set; } = "";
        public string[] Arguments { get; private set; } = Array.Empty<string>();

        /// <summary>
        /// Initializes a new instance of the CommandParser class with the specified command text.
        /// </summary>
        /// <param name="commandText">The text of the command to parse.</param>
        public CommandParser(string commandText)
        {
            if (string.IsNullOrWhiteSpace(commandText))
            {
                Command = "";
                Arguments = Array.Empty<string>();
            }
            else
            {
                ParseCommand(commandText);
            }
        }

        /// <summary>
        /// Parses the command text to extract the command and its arguments.
        /// </summary>
        /// <param name="commandText">The text of the command to parse.</param>
        private void ParseCommand(string commandText)
        {
            var commandParts = SplitCommand(commandText);

            if (commandParts.Count > 0)
            {
                Command = commandParts[0];
                Arguments = commandParts.Count > 1 ? commandParts.GetRange(1, commandParts.Count - 1).ToArray() : Array.Empty<string>();
            }
        }

        /// <summary>
        /// Splits a command string into parts, respecting quoted arguments.
        /// </summary>
        /// <param name="commandText">The full command string.</param>
        /// <returns>A list of command parts.</returns>
        private List<string> SplitCommand(string commandText)
        {
            var parts = new List<string>();
            var currentPart = new System.Text.StringBuilder();
            bool inQuotes = false;

            foreach (char c in commandText)
            {
                if (c == '\"')
                {
                    inQuotes = !inQuotes;
                    continue;
                }

                if (char.IsWhiteSpace(c) && !inQuotes)
                {
                    if (currentPart.Length > 0)
                    {
                        parts.Add(currentPart.ToString());
                        currentPart.Clear();
                    }
                }
                else
                {
                    currentPart.Append(c);
                }
            }

            if (currentPart.Length > 0)
            {
                parts.Add(currentPart.ToString());
            }

            return parts;
        }
    }
}
