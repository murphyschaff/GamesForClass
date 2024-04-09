namespace GamesForClass
{
    partial class STTT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(STTT));
            this.title = new System.Windows.Forms.Label();
            this.testButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.feedback = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(216, 9);
            this.title.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(424, 55);
            this.title.TabIndex = 51;
            this.title.Text = "Super Tic-Tac-Toe";
            // 
            // testButton
            // 
            this.testButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.testButton.Location = new System.Drawing.Point(12, 9);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(50, 50);
            this.testButton.TabIndex = 52;
            this.testButton.Text = "T";
            this.testButton.UseVisualStyleBackColor = true;
            // 
            // resetButton
            // 
            this.resetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetButton.Location = new System.Drawing.Point(747, 328);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(122, 45);
            this.resetButton.TabIndex = 54;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // feedback
            // 
            this.feedback.AutoSize = true;
            this.feedback.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.feedback.Location = new System.Drawing.Point(274, 576);
            this.feedback.Name = "feedback";
            this.feedback.Size = new System.Drawing.Size(0, 25);
            this.feedback.TabIndex = 55;
            // 
            // STTT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 621);
            this.Controls.Add(this.feedback);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.testButton);
            this.Controls.Add(this.title);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "STTT";
            this.Text = "GamesForClass: Super Tic-Tac-Toe";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Label feedback;
    }
}