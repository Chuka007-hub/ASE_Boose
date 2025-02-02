using Ase_Boose.Interfaces;
using Ase_Boose.Interfaces.Implementations;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace Ase_Boose
{
    public partial class Canvas : Form, ICanvas
    {
        private bool isFill = false;
        private Point Position = new Point(320, 190);
        private Pen PenColor = new Pen(Color.Black);
        private Color fillColor = Color.Black;
        public Shapemaker shapemaker;
        public MultipleLineCommand multiLineCommands;



        public Canvas()
        {
            InitializeComponent();
            CommandParser command = new CommandParser("");
            shapemaker = new Shapemaker(this);
            multiLineCommands = new MultipleLineCommand(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string command = "reset";
            CommandParser parser = new CommandParser(command);
            shapemaker.ExecuteDrawing(parser);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string command = singleLineCommandBox.Text;
            CommandParser parser = new CommandParser(command);
            shapemaker.ExecuteDrawing(parser);
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            string command = "clear";
            CommandParser parser = new CommandParser(command);
            shapemaker.ExecuteDrawing(parser);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.IsFilling = checkBox1.Checked;

        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePosition = e.Location;
            this.Text = $"Mouse Position: {mousePosition.X}, {mousePosition.Y}";
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            int point = 5;
            int x = Position.X - point / 2;
            int y = Position.Y - point / 2;
            e.Graphics.FillEllipse(Brushes.Black, x, y, point, point);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            string scriptContent = richTextBox.Text;
            await Task.Run(() => multiLineCommands.ExecuteCommands(scriptContent));

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void HintButton_Click(object sender, EventArgs e)
        {
            string hintText = "COMMAND GUIDE:\n\n" +
                         "1. MoveTo x y  - Moves pen to (x, y) without drawing.\n" +
                         "2. DrawTo x y  - Draws a line to (x, y).\n" +
                         "3. Reset       - Resets the drawing position.\n" +
                         "4. Clear       - Clears the canvas.\n" +
                         "5. Pen color   - Changes pen color (e.g., Pen Red).\n" +
                         "6. Fill on/off - Enables/disables filling (Fill On/Off).\n" +
                         "7. Rectangle width height - Draws a rectangle.\n" +
                         "8. Circle radius - Draws a circle.\n" +
                         "9. Triangle x1 y1 x2 y2 x3 y3 - Draws a triangle.\n" +
                         "10. If condition - Starts an if-block.\n" +
                         "      Example:\n" +
                         "      If x > 10\n" +
                         "         DrawTo 50 50\n" +
                         "      EndIf\n" +
                         "11. EndIf - Ends an if-block.\n" +
                         "12. While condition - Starts a while loop.\n" +
                         "      Example:\n" +
                         "      While x < 100\n" +
                         "         DrawTo x 50\n" +
                         "         x = x + 10\n" +
                         "      EndWhile\n" +
                         "13. EndWhile - Ends a while loop.\n";

            pictureBox.Image = GenerateHintImage(hintText);
        }

        private Image GenerateHintImage(string text)
        {
            Bitmap bitmap = new Bitmap(400, 300);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                g.DrawString(text, new Font("Arial", 10), Brushes.Black, new PointF(10, 10));
            }
            return bitmap;
        }

        private void ImportScript_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text files|*.txt|All files|*.*";
                openFileDialog.Title = "Open Script File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = openFileDialog.FileName;
                    try
                    {
                        string scriptContent = File.ReadAllText(fileName);
                        richTextBox.Text = scriptContent;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error opening the file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void saveScript_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text files|*.txt|All files|*.*";
                saveFileDialog.Title = "Save Script File";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDialog.FileName;
                    try
                    {
                        File.WriteAllText(fileName, richTextBox.Text);
                        MessageBox.Show("Script saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error saving the file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public PictureBox DrawingPictureBox
        {
            get { return pictureBox; }
        }

        public Point CurrentPosition
        {
            get { return Position; }
            set
            {
                Position = value;
                pictureBox.Invalidate();
            }
        }
        public TextBox CommandTextBox
        {
            get { return singleLineCommandBox; }
        }


        public PictureBox PictureBox   {
            get { return pictureBox; }
        }

public Pen DrawingPen
        {
            get { return PenColor; }
            set { PenColor = value; }
        }


        public Color FillColor
        {
            get { return fillColor; }
            set { fillColor = value; }
        }

        public bool IsFilling
        {
            get { return isFill; }
            set { isFill = value; }
        }
    }
}
