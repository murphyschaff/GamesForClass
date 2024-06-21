﻿namespace GamesForClass
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
            this.mediumRadio.TabStop = true;
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
            this.hardRadio.TabStop = true;
            this.hardRadio.Text = "Hard";
            this.hardRadio.UseVisualStyleBackColor = true;
            this.hardRadio.CheckedChanged += new System.EventHandler(this.hardRadio_CheckedChanged);
            // 
            // sudoku
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 675);
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
    }
}