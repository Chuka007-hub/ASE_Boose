# ASEBoose

ASEBoose is a graphical drawing application that allows users to create shapes, lines, and patterns using command-based inputs. It supports both basic drawing commands and advanced conditional logic for programmatic shape creation.

## Features
- **Basic Commands**: MoveTo, DrawTo, Reset, Clear
- **Pen & Fill Controls**: Change pen color, toggle shape filling
- **Shapes**: Rectangle, Circle, Triangle
- **Conditional Statements**: If-Else conditions for dynamic drawing
- **Loops**: While loops for repetitive patterns
- **Multiline Command Execution**

## Installation
### Prerequisites
- Windows OS (Recommended)
- .NET Framework (for running the application)

### Steps
1. Clone the repository:
   ```sh
   git clone https://github.com/yourrepo/ASE_Boose.git
   ```
2. Open the project in Visual Studio.
3. Build and run the application.

## Usage
### Basic Commands
| Command | Description |
|---------|-------------|
| `MoveTo x y` | Moves pen to coordinates (x, y) without drawing |
| `DrawTo x y` | Draws a line from current position to (x, y) |
| `Reset` | Resets the drawing position |
| `Clear` | Clears the entire canvas |
| `Pen color` | Changes pen color (e.g., `Pen Red`) |
| `Fill On/Off` | Enables/disables shape filling |

### Shape Commands
| Command | Description |
|---------|-------------|
| `Rectangle width height` | Draws a rectangle with given dimensions |
| `Circle radius` | Draws a circle with the given radius |
| `Triangle x1 y1` | Draws a triangle using two coordinate points |

### Conditional Statements
| Command | Description |
|---------|-------------|
| `If condition` | Starts a conditional block |
| `EndIf` | Ends an If block |
| `While condition` | Starts a while loop |
| `EndWhile` | Ends a while loop |

### Example Commands
#### Simple Drawing:
```
MoveTo 50 50
DrawTo 100 100
Rectangle 40 30
Circle 25
```
#### Using Conditions:
```
If x > 10
    DrawTo 50 50
EndIf
```
#### Using Loops:
```
While x < 100
    DrawTo x 50
    x = x + 10
EndWhile
```

## Contributing
1. Fork the repository.
2. Create a new branch (`feature-xyz`).
3. Commit your changes.
4. Push to your branch and submit a pull request.

## Contact
For inquiries, reach out to **[Chuka]** at **https://github.com/Chuka007-hub/**.

