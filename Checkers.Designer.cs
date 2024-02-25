namespace GamesForClass
{
    partial class Checkers
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
            this.title = new System.Windows.Forms.Label();
            this.boardBackground = new System.Windows.Forms.Label();
            this.startReset = new System.Windows.Forms.Button();
            this.test = new System.Windows.Forms.Label();
            this.feedback = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(435, 5);
            this.title.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(241, 55);
            this.title.TabIndex = 50;
            this.title.Text = "Checkers!";
            // 
            // boardBackground
            // 
            this.boardBackground.AutoSize = true;
            this.boardBackground.BackColor = System.Drawing.Color.SlateGray;
            this.boardBackground.Location = new System.Drawing.Point(235, 60);
            this.boardBackground.MinimumSize = new System.Drawing.Size(650, 650);
            this.boardBackground.Name = "boardBackground";
            this.boardBackground.Size = new System.Drawing.Size(650, 650);
            this.boardBackground.TabIndex = 51;
            // 
            // startReset
            // 
            this.startReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startReset.Location = new System.Drawing.Point(38, 281);
            this.startReset.Name = "startReset";
            this.startReset.Size = new System.Drawing.Size(136, 53);
            this.startReset.TabIndex = 52;
            this.startReset.Text = "Start";
            this.startReset.UseVisualStyleBackColor = true;
            this.startReset.Click += new System.EventHandler(this.startReset_Click);
            // 
            // test
            // 
            this.test.AutoSize = true;
            this.test.Location = new System.Drawing.Point(54, 49);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(35, 13);
            this.test.TabIndex = 53;
            this.test.Text = "label1";
            // 
            // feedback
            // 
            this.feedback.AutoSize = true;
            this.feedback.Location = new System.Drawing.Point(922, 304);
            this.feedback.Name = "feedback";
            this.feedback.Size = new System.Drawing.Size(35, 13);
            this.feedback.TabIndex = 54;
            this.feedback.Text = "label2";
            // 
            // Checkers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1138, 723);
            this.Controls.Add(this.feedback);
            this.Controls.Add(this.test);
            this.Controls.Add(this.startReset);
            this.Controls.Add(this.boardBackground);
            this.Controls.Add(this.title);
            this.Name = "Checkers";
            this.Text = "GamesForClass: Checkers";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label boardBackground;
        private System.Windows.Forms.Button startReset;
        private System.Windows.Forms.Label test;
        private System.Windows.Forms.Label feedback;
    }
}