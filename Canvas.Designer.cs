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
            ImportScript = new Button();
            ClearButton = new Button();
            richTextBox = new RichTextBox();
            pictureBox = new PictureBox();
            singleLineCommandBox = new TextBox();
            checkBox1 = new CheckBox();
            saveScript = new Button();
            HintButton = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
            // 
            // ResetButton
            // 
            ResetButton.BackColor = Color.Red;
            ResetButton.ForeColor = SystemColors.ControlLightLight;
            ResetButton.Location = new Point(203, 383);
            ResetButton.Name = "ResetButton";
            ResetButton.Size = new Size(89, 32);
            ResetButton.TabIndex = 0;
            ResetButton.Text = "Reset";
            ResetButton.UseVisualStyleBackColor = false;
            ResetButton.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.MediumSpringGreen;
            button2.Location = new Point(204, 274);
            button2.Name = "button2";
            button2.Size = new Size(89, 32);
            button2.TabIndex = 1;
            button2.Text = "Run Script";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // RunButton
            // 
            RunButton.BackColor = Color.Lime;
            RunButton.Location = new Point(217, 352);
            RunButton.Name = "RunButton";
            RunButton.Size = new Size(75, 23);
            RunButton.TabIndex = 2;
            RunButton.Text = "Run";
            RunButton.UseVisualStyleBackColor = false;
            RunButton.Click += button3_Click;
            // 
            // ImportScript
            // 
            ImportScript.BackColor = Color.Crimson;
            ImportScript.ForeColor = Color.MediumSpringGreen;
            ImportScript.Location = new Point(1, 274);
            ImportScript.Name = "ImportScript";
            ImportScript.Size = new Size(89, 32);
            ImportScript.TabIndex = 3;
            ImportScript.Text = "Import Script";
            ImportScript.UseVisualStyleBackColor = false;
            // 
            // ClearButton
            // 
            ClearButton.BackColor = SystemColors.WindowFrame;
            ClearButton.ForeColor = SystemColors.ButtonHighlight;
            ClearButton.Location = new Point(1, 383);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(89, 32);
            ClearButton.TabIndex = 4;
            ClearButton.Text = "Clear";
            ClearButton.UseVisualStyleBackColor = false;
            ClearButton.Click += ClearButton_Click;
            // 
            // richTextBox
            // 
            richTextBox.BackColor = SystemColors.Info;
            richTextBox.Location = new Point(1, 3);
            richTextBox.Name = "richTextBox";
            richTextBox.Size = new Size(292, 265);
            richTextBox.TabIndex = 5;
            richTextBox.Text = "";
            richTextBox.TextChanged += richTextBox1_TextChanged;
            // 
            // pictureBox
            // 
            pictureBox.BackColor = SystemColors.ControlLightLight;
            pictureBox.Location = new Point(299, 3);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(665, 412);
            pictureBox.TabIndex = 6;
            pictureBox.TabStop = false;
            pictureBox.Paint += pictureBox_Paint;
            pictureBox.MouseMove += pictureBox_MouseMove;
            // 
            // singleLineCommandBox
            // 
            singleLineCommandBox.BorderStyle = BorderStyle.FixedSingle;
            singleLineCommandBox.Location = new Point(1, 352);
            singleLineCommandBox.Name = "singleLineCommandBox";
            singleLineCommandBox.PlaceholderText = "Single Line Command";
            singleLineCommandBox.Size = new Size(210, 23);
            singleLineCommandBox.TabIndex = 7;
            singleLineCommandBox.TextChanged += textBox1_TextChanged;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.BackColor = SystemColors.ControlDarkDark;
            checkBox1.Location = new Point(1, 327);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(73, 19);
            checkBox1.TabIndex = 8;
            checkBox1.Text = "Fill Color";
            checkBox1.UseVisualStyleBackColor = false;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // saveScript
            // 
            saveScript.BackColor = Color.DarkViolet;
            saveScript.ForeColor = Color.MediumSpringGreen;
            saveScript.Location = new Point(96, 274);
            saveScript.Name = "saveScript";
            saveScript.Size = new Size(89, 32);
            saveScript.TabIndex = 9;
            saveScript.Text = "Save";
            saveScript.UseVisualStyleBackColor = false;
            // 
            // HintButton
            // 
            HintButton.Location = new Point(110, 389);
            HintButton.Name = "HintButton";
            HintButton.Size = new Size(75, 23);
            HintButton.TabIndex = 10;
            HintButton.Text = "Hint";
            HintButton.UseVisualStyleBackColor = true;
            HintButton.Click += HintButton_Click;
            // 
            // Canvas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Thistle;
            ClientSize = new Size(966, 424);
            Controls.Add(HintButton);
            Controls.Add(saveScript);
            Controls.Add(checkBox1);
            Controls.Add(singleLineCommandBox);
            Controls.Add(pictureBox);
            Controls.Add(richTextBox);
            Controls.Add(ClearButton);
            Controls.Add(ImportScript);
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
        private Button ImportScript;
        private Button ClearButton;
        private RichTextBox richTextBox;
        private PictureBox pictureBox;
        private TextBox singleLineCommandBox;
        private CheckBox checkBox1;
        private Button saveScript;
        private Button HintButton;
    }
}
