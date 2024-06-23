namespace GamesForClass
{
    partial class sudoku
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(sudoku));
            this.title = new System.Windows.Forms.Label();
            this.startReset = new System.Windows.Forms.Button();
            this.difficultyLabel = new System.Windows.Forms.Label();
            this.easyRadio = new System.Windows.Forms.RadioButton();
            this.mediumRadio = new System.Windows.Forms.RadioButton();
            this.hardRadio = new System.Windows.Forms.RadioButton();
            this.test = new System.Windows.Forms.Label();
            this.checkButton = new System.Windows.Forms.Button();
            this.feedback = new System.Windows.Forms.Label();
            this.line1 = new System.Windows.Forms.Label();
            this.line2 = new System.Windows.Forms.Label();
            this.line3 = new System.Windows.Forms.Label();
            this.line4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(285, 9);
            this.title.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(201, 55);
            this.title.TabIndex = 52;
            this.title.Text = "Sudoku!";
            this.title.Click += new System.EventHandler(this.title_Click);
            // 
            // startReset
            // 
            this.startReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startReset.Location = new System.Drawing.Point(335, 611);
            this.startReset.Name = "startReset";
            this.startReset.Size = new System.Drawing.Size(136, 53);
            this.startReset.TabIndex = 53;
            this.startReset.Text = "Start";
            this.startReset.UseVisualStyleBackColor = true;
            this.startReset.Click += new System.EventHandler(this.startReset_Click);
            // 
            // difficultyLabel
            // 
            this.difficultyLabel.AutoSize = true;
            this.difficultyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.difficultyLabel.Location = new System.Drawing.Point(8, 559);
            this.difficultyLabel.Name = "difficultyLabel";
            this.difficultyLabel.Size = new System.Drawing.Size(165, 24);
            this.difficultyLabel.TabIndex = 54;
            this.difficultyLabel.Text = "Difficulty Selection:";
            // 
            // easyRadio
            // 
            this.easyRadio.AutoSize = true;
            this.easyRadio.Checked = true;
            this.easyRadio.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.easyRadio.Location = new System.Drawing.Point(12, 586);
            this.easyRadio.Name = "easyRadio";
            this.easyRadio.Size = new System.Drawing.Size(59, 22);
            this.easyRadio.TabIndex = 55;
            this.easyRadio.TabStop = true;
            this.easyRadio.Text = "Easy";
            this.easyRadio.UseVisualStyleBackColor = true;
            this.easyRadio.CheckedChanged += new System.EventHandler(this.easyRadio_CheckedChanged);
            // 
            // mediumRadio
            // 
            this.mediumRadio.AutoSize = true;
            this.mediumRadio.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mediumRadio.Location = new System.Drawing.Point(12, 614);
            this.mediumRadio.Name = "mediumRadio";
            this.mediumRadio.Size = new System.Drawing.Size(79, 22);
            this.mediumRadio.TabIndex = 56;
            this.mediumRadio.Text = "Medium";
            this.mediumRadio.UseVisualStyleBackColor = true;
            this.mediumRadio.CheckedChanged += new System.EventHandler(this.mediumRadio_CheckedChanged);
            // 
            // hardRadio
            // 
            this.hardRadio.AutoSize = true;
            this.hardRadio.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hardRadio.Location = new System.Drawing.Point(12, 642);
            this.hardRadio.Name = "hardRadio";
            this.hardRadio.Size = new System.Drawing.Size(58, 22);
            this.hardRadio.TabIndex = 57;
            this.hardRadio.Text = "Hard";
            this.hardRadio.UseVisualStyleBackColor = true;
            this.hardRadio.CheckedChanged += new System.EventHandler(this.hardRadio_CheckedChanged);
            // 
            // test
            // 
            this.test.AutoSize = true;
            this.test.Location = new System.Drawing.Point(643, 42);
            this.test.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(0, 13);
            this.test.TabIndex = 58;
            // 
            // checkButton
            // 
            this.checkButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkButton.Location = new System.Drawing.Point(613, 441);
            this.checkButton.Name = "checkButton";
            this.checkButton.Size = new System.Drawing.Size(92, 36);
            this.checkButton.TabIndex = 59;
            this.checkButton.Text = "Check";
            this.checkButton.UseVisualStyleBackColor = true;
            this.checkButton.Visible = false;
            this.checkButton.Click += new System.EventHandler(this.checkButton_Click);
            // 
            // feedback
            // 
            this.feedback.AutoSize = true;
            this.feedback.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.feedback.Location = new System.Drawing.Point(306, 571);
            this.feedback.Name = "feedback";
            this.feedback.Size = new System.Drawing.Size(0, 24);
            this.feedback.TabIndex = 60;
            // 
            // line1
            // 
            this.line1.AutoSize = true;
            this.line1.BackColor = System.Drawing.Color.Black;
            this.line1.Location = new System.Drawing.Point(231, 75);
            this.line1.MinimumSize = new System.Drawing.Size(4, 475);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(4, 475);
            this.line1.TabIndex = 61;
            this.line1.Visible = false;
            // 
            // line2
            // 
            this.line2.AutoSize = true;
            this.line2.BackColor = System.Drawing.Color.Black;
            this.line2.Location = new System.Drawing.Point(391, 75);
            this.line2.MinimumSize = new System.Drawing.Size(4, 475);
            this.line2.Name = "line2";
            this.line2.Size = new System.Drawing.Size(4, 475);
            this.line2.TabIndex = 62;
            this.line2.Visible = false;
            // 
            // line3
            // 
            this.line3.AutoSize = true;
            this.line3.BackColor = System.Drawing.Color.Black;
            this.line3.Font = new System.Drawing.Font("Microsoft Sans Serif", 2.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.line3.Location = new System.Drawing.Point(75, 231);
            this.line3.MinimumSize = new System.Drawing.Size(475, 1);
            this.line3.Name = "line3";
            this.line3.Size = new System.Drawing.Size(475, 4);
            this.line3.TabIndex = 63;
            this.line3.Visible = false;
            // 
            // line4
            // 
            this.line4.AutoSize = true;
            this.line4.BackColor = System.Drawing.Color.Black;
            this.line4.Font = new System.Drawing.Font("Microsoft Sans Serif", 2.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.line4.Location = new System.Drawing.Point(75, 391);
            this.line4.MinimumSize = new System.Drawing.Size(475, 1);
            this.line4.Name = "line4";
            this.line4.Size = new System.Drawing.Size(475, 4);
            this.line4.TabIndex = 64;
            this.line4.Visible = false;
            // 
            // sudoku
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 675);
            this.Controls.Add(this.line4);
            this.Controls.Add(this.line3);
            this.Controls.Add(this.line2);
            this.Controls.Add(this.line1);
            this.Controls.Add(this.feedback);
            this.Controls.Add(this.checkButton);
            this.Controls.Add(this.test);
            this.Controls.Add(this.hardRadio);
            this.Controls.Add(this.mediumRadio);
            this.Controls.Add(this.easyRadio);
            this.Controls.Add(this.difficultyLabel);
            this.Controls.Add(this.startReset);
            this.Controls.Add(this.title);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "sudoku";
            this.Text = "GamesForClass: Sudoku";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Button startReset;
        private System.Windows.Forms.Label difficultyLabel;
        private System.Windows.Forms.RadioButton easyRadio;
        private System.Windows.Forms.RadioButton mediumRadio;
        private System.Windows.Forms.RadioButton hardRadio;
        private System.Windows.Forms.Label test;
        private System.Windows.Forms.Button checkButton;
        private System.Windows.Forms.Label feedback;
        private System.Windows.Forms.Label line1;
        private System.Windows.Forms.Label line2;
        private System.Windows.Forms.Label line3;
        private System.Windows.Forms.Label line4;
    }
}