using Ase_Boose.Interfaces;
using Ase_Boose.Interfaces.Implementations;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace Ase_Boose
{
    public partial class Canvas : Form, ICanvas
    {
        private bool isFill = false;
        private Point Position = new Point(330, 250);
        private Pen PenColor = new Pen(Color.Black);
        private Color fillColor = Color.Black;
        public Shapemaker shapemaker;


        public Canvas()
        {
            InitializeComponent();
            CommandParser command = new CommandParser("");
            shapemaker = new Shapemaker(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

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


        public PictureBox PictureBox => throw new NotImplementedException();

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
