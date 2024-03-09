namespace GamesForClass
{
    partial class Minesweeper
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Minesweeper));
            this.title = new System.Windows.Forms.Label();
            this.test = new System.Windows.Forms.Label();
            this.background = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.easyCheck = new System.Windows.Forms.CheckBox();
            this.mediumCheck = new System.Windows.Forms.CheckBox();
            this.hardCheck = new System.Windows.Forms.CheckBox();
            this.startButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.remainingBombs = new System.Windows.Forms.Label();
            this.resultsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(398, 18);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(324, 55);
            this.title.TabIndex = 0;
            this.title.Text = "Minesweeper!";
            this.title.Click += new System.EventHandler(this.title_Click);
            // 
            // test
            // 
            this.test.AutoSize = true;
            this.test.Location = new System.Drawing.Point(18, 18);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(0, 13);
            this.test.TabIndex = 1;
            // 
            // background
            // 
            this.background.AutoSize = true;
            this.background.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.background.Location = new System.Drawing.Point(12, 73);
            this.background.MinimumSize = new System.Drawing.Size(1100, 500);
            this.background.Name = "background";
            this.background.Size = new System.Drawing.Size(1100, 500);
            this.background.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 600);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Difficulty Select:";
            // 
            // easyCheck
            // 
            this.easyCheck.AutoSize = true;
            this.easyCheck.Checked = true;
            this.easyCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.easyCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.easyCheck.Location = new System.Drawing.Point(145, 599);
            this.easyCheck.Name = "easyCheck";
            this.easyCheck.Size = new System.Drawing.Size(63, 24);
            this.easyCheck.TabIndex = 4;
            this.easyCheck.Text = "Easy";
            this.easyCheck.UseVisualStyleBackColor = true;
            this.easyCheck.CheckedChanged += new System.EventHandler(this.easyCheck_CheckedChanged);
            // 
            // mediumCheck
            // 
            this.mediumCheck.AutoSize = true;
            this.mediumCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mediumCheck.Location = new System.Drawing.Point(214, 599);
            this.mediumCheck.Name = "mediumCheck";
            this.mediumCheck.Size = new System.Drawing.Size(84, 24);
            this.mediumCheck.TabIndex = 5;
            this.mediumCheck.Text = "Medium";
            this.mediumCheck.UseVisualStyleBackColor = true;
            this.mediumCheck.CheckedChanged += new System.EventHandler(this.mediumCheck_CheckedChanged);
            // 
            // hardCheck
            // 
            this.hardCheck.AutoSize = true;
            this.hardCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hardCheck.Location = new System.Drawing.Point(304, 599);
            this.hardCheck.Name = "hardCheck";
            this.hardCheck.Size = new System.Drawing.Size(63, 24);
            this.hardCheck.TabIndex = 6;
            this.hardCheck.Text = "Hard";
            this.hardCheck.UseVisualStyleBackColor = true;
            this.hardCheck.CheckedChanged += new System.EventHandler(this.hardCheck_CheckedChanged);
            // 
            // startButton
            // 
            this.startButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.Location = new System.Drawing.Point(375, 588);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(126, 41);
            this.startButton.TabIndex = 7;
            this.startButton.Text = "Start Game!";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetButton.Location = new System.Drawing.Point(996, 588);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(126, 41);
            this.resetButton.TabIndex = 8;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(517, 598);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Mines Remaining:";
            // 
            // remainingBombs
            // 
            this.remainingBombs.AutoSize = true;
            this.remainingBombs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remainingBombs.Location = new System.Drawing.Point(657, 598);
            this.remainingBombs.Name = "remainingBombs";
            this.remainingBombs.Size = new System.Drawing.Size(0, 20);
            this.remainingBombs.TabIndex = 10;
            // 
            // resultsLabel
            // 
            this.resultsLabel.AutoSize = true;
            this.resultsLabel.BackColor = System.Drawing.SystemColors.Control;
            this.resultsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resultsLabel.Location = new System.Drawing.Point(801, 593);
            this.resultsLabel.Name = "resultsLabel";
            this.resultsLabel.Size = new System.Drawing.Size(0, 39);
            this.resultsLabel.TabIndex = 11;
            // 
            // Minesweeper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 641);
            this.Controls.Add(this.resultsLabel);
            this.Controls.Add(this.remainingBombs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.hardCheck);
            this.Controls.Add(this.mediumCheck);
            this.Controls.Add(this.easyCheck);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.test);
            this.Controls.Add(this.title);
            this.Controls.Add(this.background);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Minesweeper";
            this.Text = "GamesForClass: Minesweeper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label test;
        private System.Windows.Forms.Label background;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox easyCheck;
        private System.Windows.Forms.CheckBox mediumCheck;
        private System.Windows.Forms.CheckBox hardCheck;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label remainingBombs;
        private System.Windows.Forms.Label resultsLabel;
    }
}