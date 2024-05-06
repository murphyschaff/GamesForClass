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
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(171, 9);
            this.title.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(304, 55);
            this.title.TabIndex = 51;
            this.title.Text = "Word Guess!";
            this.title.Click += new System.EventHandler(this.title_Click);
            // 
            // test
            // 
            this.test.AutoSize = true;
            this.test.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.test.Location = new System.Drawing.Point(31, 30);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(0, 20);
            this.test.TabIndex = 56;
            // 
            // feedback
            // 
            this.feedback.AutoSize = true;
            this.feedback.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.feedback.Location = new System.Drawing.Point(12, 469);
            this.feedback.Name = "feedback";
            this.feedback.Size = new System.Drawing.Size(0, 24);
            this.feedback.TabIndex = 57;
            // 
            // buttonBackground
            // 
            this.buttonBackground.AutoSize = true;
            this.buttonBackground.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.buttonBackground.Location = new System.Drawing.Point(500, 455);
            this.buttonBackground.MinimumSize = new System.Drawing.Size(150, 70);
            this.buttonBackground.Name = "buttonBackground";
            this.buttonBackground.Size = new System.Drawing.Size(150, 70);
            this.buttonBackground.TabIndex = 59;
            // 
            // resetButton
            // 
            this.resetButton.AutoSize = true;
            this.resetButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.resetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetButton.Location = new System.Drawing.Point(512, 465);
            this.resetButton.Name = "resetButton";
            this.resetButton.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.resetButton.Size = new System.Drawing.Size(126, 51);
            this.resetButton.TabIndex = 60;
            this.resetButton.Text = "Reset";
            this.resetButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // WordGuess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 525);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.buttonBackground);
            this.Controls.Add(this.feedback);
            this.Controls.Add(this.test);
            this.Controls.Add(this.title);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
    }
}