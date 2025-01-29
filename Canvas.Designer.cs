namespace Ase_Boose
{
    partial class Canvas
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            richTextBox1 = new RichTextBox();
            pictureBox = new PictureBox();
            singleLineCommandBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(197, 437);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(12, 375);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 1;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(218, 284);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 2;
            button3.Text = "button3";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(182, 375);
            button4.Name = "button4";
            button4.Size = new Size(75, 23);
            button4.TabIndex = 3;
            button4.Text = "button4";
            button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(116, 437);
            button5.Name = "button5";
            button5.Size = new Size(75, 23);
            button5.TabIndex = 4;
            button5.Text = "button5";
            button5.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(1, 3);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(292, 265);
            richTextBox1.TabIndex = 5;
            richTextBox1.Text = "";
            // 
            // pictureBox
            // 
            pictureBox.BackColor = SystemColors.ControlDarkDark;
            pictureBox.Location = new Point(299, 3);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(665, 514);
            pictureBox.TabIndex = 6;
            pictureBox.TabStop = false;
            // 
            // singleLineCommandBox
            // 
            singleLineCommandBox.BorderStyle = BorderStyle.FixedSingle;
            singleLineCommandBox.Location = new Point(2, 284);
            singleLineCommandBox.Name = "singleLineCommandBox";
            singleLineCommandBox.Size = new Size(210, 23);
            singleLineCommandBox.TabIndex = 7;
            singleLineCommandBox.TextChanged += textBox1_TextChanged;
            // 
            // Canvas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(966, 520);
            Controls.Add(singleLineCommandBox);
            Controls.Add(pictureBox);
            Controls.Add(richTextBox1);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Canvas";
            Text = "ASE_Boose";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private RichTextBox richTextBox1;
        private PictureBox pictureBox;
        private TextBox singleLineCommandBox;
    }
}
