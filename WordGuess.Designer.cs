namespace GamesForClass
{
    partial class WordGuess
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WordGuess));
            this.title = new System.Windows.Forms.Label();
            this.test = new System.Windows.Forms.Label();
            this.feedback = new System.Windows.Forms.Label();
            this.buttonBackground = new System.Windows.Forms.Label();
            this.resetButton = new System.Windows.Forms.Label();
            this.fourLetter = new System.Windows.Forms.RadioButton();
            this.fiveLetter = new System.Windows.Forms.RadioButton();
            this.sixLetter = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.randomLetter = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(242, 9);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(379, 69);
            this.title.TabIndex = 51;
            this.title.Text = "Word Guess!";
            this.title.Click += new System.EventHandler(this.title_Click);
            // 
            // test
            // 
            this.test.AutoSize = true;
            this.test.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.test.Location = new System.Drawing.Point(41, 37);
            this.test.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(0, 25);
            this.test.TabIndex = 56;
            // 
            // feedback
            // 
            this.feedback.AutoSize = true;
            this.feedback.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.feedback.Location = new System.Drawing.Point(16, 577);
            this.feedback.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.feedback.Name = "feedback";
            this.feedback.Size = new System.Drawing.Size(0, 29);
            this.feedback.TabIndex = 57;
            // 
            // buttonBackground
            // 
            this.buttonBackground.AutoSize = true;
            this.buttonBackground.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.buttonBackground.Location = new System.Drawing.Point(664, 565);
            this.buttonBackground.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.buttonBackground.MinimumSize = new System.Drawing.Size(200, 86);
            this.buttonBackground.Name = "buttonBackground";
            this.buttonBackground.Size = new System.Drawing.Size(200, 86);
            this.buttonBackground.TabIndex = 59;
            // 
            // resetButton
            // 
            this.resetButton.AutoSize = true;
            this.resetButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.resetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetButton.Location = new System.Drawing.Point(680, 577);
            this.resetButton.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.resetButton.MinimumSize = new System.Drawing.Size(165, 60);
            this.resetButton.Name = "resetButton";
            this.resetButton.Padding = new System.Windows.Forms.Padding(27, 12, 27, 12);
            this.resetButton.Size = new System.Drawing.Size(165, 63);
            this.resetButton.TabIndex = 60;
            this.resetButton.Text = "Start";
            this.resetButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // fourLetter
            // 
            this.fourLetter.AutoSize = true;
            this.fourLetter.Checked = true;
            this.fourLetter.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fourLetter.Location = new System.Drawing.Point(136, 618);
            this.fourLetter.Name = "fourLetter";
            this.fourLetter.Size = new System.Drawing.Size(126, 33);
            this.fourLetter.TabIndex = 61;
            this.fourLetter.TabStop = true;
            this.fourLetter.Text = "4 Letters";
            this.fourLetter.UseVisualStyleBackColor = true;
            this.fourLetter.CheckedChanged += new System.EventHandler(this.fourLetter_CheckedChanged);
            // 
            // fiveLetter
            // 
            this.fiveLetter.AutoSize = true;
            this.fiveLetter.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fiveLetter.Location = new System.Drawing.Point(268, 619);
            this.fiveLetter.Name = "fiveLetter";
            this.fiveLetter.Size = new System.Drawing.Size(126, 33);
            this.fiveLetter.TabIndex = 62;
            this.fiveLetter.Text = "5 Letters";
            this.fiveLetter.UseVisualStyleBackColor = true;
            this.fiveLetter.CheckedChanged += new System.EventHandler(this.fiveLetter_CheckedChanged);
            // 
            // sixLetter
            // 
            this.sixLetter.AutoSize = true;
            this.sixLetter.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sixLetter.Location = new System.Drawing.Point(400, 619);
            this.sixLetter.Name = "sixLetter";
            this.sixLetter.Size = new System.Drawing.Size(126, 33);
            this.sixLetter.TabIndex = 63;
            this.sixLetter.Text = "6 Letters";
            this.sixLetter.UseVisualStyleBackColor = true;
            this.sixLetter.CheckedChanged += new System.EventHandler(this.sixLetter_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 619);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 29);
            this.label1.TabIndex = 64;
            this.label1.Text = "Word Size:";
            // 
            // randomLetter
            // 
            this.randomLetter.AutoSize = true;
            this.randomLetter.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.randomLetter.Location = new System.Drawing.Point(531, 619);
            this.randomLetter.Name = "randomLetter";
            this.randomLetter.Size = new System.Drawing.Size(125, 33);
            this.randomLetter.TabIndex = 65;
            this.randomLetter.Text = "Random";
            this.randomLetter.UseVisualStyleBackColor = true;
            this.randomLetter.CheckedChanged += new System.EventHandler(this.randomLetter_CheckedChanged);
            // 
            // WordGuess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 657);
            this.Controls.Add(this.randomLetter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sixLetter);
            this.Controls.Add(this.fiveLetter);
            this.Controls.Add(this.fourLetter);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.buttonBackground);
            this.Controls.Add(this.feedback);
            this.Controls.Add(this.test);
            this.Controls.Add(this.title);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "WordGuess";
            this.Text = "GamesForClass: Word Guess";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label test;
        private System.Windows.Forms.Label feedback;
        private System.Windows.Forms.Label buttonBackground;
        private System.Windows.Forms.Label resetButton;
        private System.Windows.Forms.RadioButton fourLetter;
        private System.Windows.Forms.RadioButton fiveLetter;
        private System.Windows.Forms.RadioButton sixLetter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton randomLetter;
    }
}