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
            ResetButton = new Button();
            button2 = new Button();
            RunButton = new Button();
            button4 = new Button();
            ClearButton = new Button();
            richTextBox1 = new RichTextBox();
            pictureBox = new PictureBox();
            singleLineCommandBox = new TextBox();
            checkBox1 = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
            // 
            // ResetButton
            // 
            ResetButton.BackColor = Color.Red;
            ResetButton.ForeColor = SystemColors.ControlLightLight;
            ResetButton.Location = new Point(182, 455);
            ResetButton.Name = "ResetButton";
            ResetButton.Size = new Size(89, 32);
            ResetButton.TabIndex = 0;
            ResetButton.Text = "Reset";
            ResetButton.UseVisualStyleBackColor = false;
            ResetButton.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(12, 368);
            button2.Name = "button2";
            button2.Size = new Size(89, 32);
            button2.TabIndex = 1;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            // 
            // RunButton
            // 
            RunButton.BackColor = Color.Lime;
            RunButton.Location = new Point(218, 284);
            RunButton.Name = "RunButton";
            RunButton.Size = new Size(75, 23);
            RunButton.TabIndex = 2;
            RunButton.Text = "Run";
            RunButton.UseVisualStyleBackColor = false;
            RunButton.Click += button3_Click;
            // 
            // button4
            // 
            button4.ForeColor = SystemColors.ControlText;
            button4.Location = new Point(182, 368);
            button4.Name = "button4";
            button4.Size = new Size(89, 32);
            button4.TabIndex = 3;
            button4.Text = "button4";
            button4.UseVisualStyleBackColor = true;
            // 
            // ClearButton
            // 
            ClearButton.BackColor = SystemColors.WindowFrame;
            ClearButton.ForeColor = SystemColors.ButtonHighlight;
            ClearButton.Location = new Point(12, 455);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(89, 32);
            ClearButton.TabIndex = 4;
            ClearButton.Text = "Clear";
            ClearButton.UseVisualStyleBackColor = false;
            ClearButton.Click += ClearButton_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.BackColor = SystemColors.Info;
            richTextBox1.Location = new Point(1, 3);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(292, 265);
            richTextBox1.TabIndex = 5;
            richTextBox1.Text = "";
            // 
            // pictureBox
            // 
            pictureBox.BackColor = SystemColors.ControlLightLight;
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
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(17, 320);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(73, 19);
            checkBox1.TabIndex = 8;
            checkBox1.Text = "Fill Color";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // Canvas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(966, 520);
            Controls.Add(checkBox1);
            Controls.Add(singleLineCommandBox);
            Controls.Add(pictureBox);
            Controls.Add(richTextBox1);
            Controls.Add(ClearButton);
            Controls.Add(button4);
            Controls.Add(RunButton);
            Controls.Add(button2);
            Controls.Add(ResetButton);
            Name = "Canvas";
            Text = "ASE_Boose";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ResetButton;
        private Button button2;
        private Button RunButton;
        private Button button4;
        private Button ClearButton;
        private RichTextBox richTextBox1;
        private PictureBox pictureBox;
        private TextBox singleLineCommandBox;
        private CheckBox checkBox1;
    }
}
