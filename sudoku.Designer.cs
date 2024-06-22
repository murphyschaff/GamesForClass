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
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(380, 11);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(249, 69);
            this.title.TabIndex = 52;
            this.title.Text = "Sudoku!";
            // 
            // startReset
            // 
            this.startReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startReset.Location = new System.Drawing.Point(447, 752);
            this.startReset.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.startReset.Name = "startReset";
            this.startReset.Size = new System.Drawing.Size(181, 65);
            this.startReset.TabIndex = 53;
            this.startReset.Text = "Start";
            this.startReset.UseVisualStyleBackColor = true;
            this.startReset.Click += new System.EventHandler(this.startReset_Click);
            // 
            // difficultyLabel
            // 
            this.difficultyLabel.AutoSize = true;
            this.difficultyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.difficultyLabel.Location = new System.Drawing.Point(11, 688);
            this.difficultyLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.difficultyLabel.Name = "difficultyLabel";
            this.difficultyLabel.Size = new System.Drawing.Size(215, 29);
            this.difficultyLabel.TabIndex = 54;
            this.difficultyLabel.Text = "Difficulty Selection:";
            // 
            // easyRadio
            // 
            this.easyRadio.AutoSize = true;
            this.easyRadio.Checked = true;
            this.easyRadio.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.easyRadio.Location = new System.Drawing.Point(16, 721);
            this.easyRadio.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.easyRadio.Name = "easyRadio";
            this.easyRadio.Size = new System.Drawing.Size(72, 28);
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
            this.mediumRadio.Location = new System.Drawing.Point(16, 756);
            this.mediumRadio.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mediumRadio.Name = "mediumRadio";
            this.mediumRadio.Size = new System.Drawing.Size(100, 28);
            this.mediumRadio.TabIndex = 56;
            this.mediumRadio.Text = "Medium";
            this.mediumRadio.UseVisualStyleBackColor = true;
            this.mediumRadio.CheckedChanged += new System.EventHandler(this.mediumRadio_CheckedChanged);
            // 
            // hardRadio
            // 
            this.hardRadio.AutoSize = true;
            this.hardRadio.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hardRadio.Location = new System.Drawing.Point(16, 790);
            this.hardRadio.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.hardRadio.Name = "hardRadio";
            this.hardRadio.Size = new System.Drawing.Size(72, 28);
            this.hardRadio.TabIndex = 57;
            this.hardRadio.Text = "Hard";
            this.hardRadio.UseVisualStyleBackColor = true;
            this.hardRadio.CheckedChanged += new System.EventHandler(this.hardRadio_CheckedChanged);
            // 
            // test
            // 
            this.test.AutoSize = true;
            this.test.Location = new System.Drawing.Point(25, 23);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(0, 16);
            this.test.TabIndex = 58;
            // 
            // sudoku
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 831);
            this.Controls.Add(this.test);
            this.Controls.Add(this.hardRadio);
            this.Controls.Add(this.mediumRadio);
            this.Controls.Add(this.easyRadio);
            this.Controls.Add(this.difficultyLabel);
            this.Controls.Add(this.startReset);
            this.Controls.Add(this.title);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
    }
}