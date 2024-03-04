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
            this.label1 = new System.Windows.Forms.Label();
            this.autoPlayerMove = new System.Windows.Forms.Button();
            this.winCondition = new System.Windows.Forms.Label();
            this.simulateButton = new System.Windows.Forms.Button();
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
            this.test.Size = new System.Drawing.Size(0, 13);
            this.test.TabIndex = 53;
            // 
            // feedback
            // 
            this.feedback.AutoSize = true;
            this.feedback.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.feedback.Location = new System.Drawing.Point(920, 316);
            this.feedback.Name = "feedback";
            this.feedback.Size = new System.Drawing.Size(62, 50);
            this.feedback.TabIndex = 54;
            this.feedback.Text = "You:\r\nCPU:\r\n";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(920, 281);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 25);
            this.label1.TabIndex = 55;
            this.label1.Text = "Pieces Left";
            // 
            // autoPlayerMove
            // 
            this.autoPlayerMove.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoPlayerMove.Location = new System.Drawing.Point(942, 585);
            this.autoPlayerMove.Name = "autoPlayerMove";
            this.autoPlayerMove.Size = new System.Drawing.Size(136, 72);
            this.autoPlayerMove.TabIndex = 56;
            this.autoPlayerMove.Text = "Make Move for Me";
            this.autoPlayerMove.UseVisualStyleBackColor = true;
            this.autoPlayerMove.Visible = false;
            this.autoPlayerMove.Click += new System.EventHandler(this.autoPlayerMove_Click);
            // 
            // winCondition
            // 
            this.winCondition.AutoSize = true;
            this.winCondition.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.winCondition.Location = new System.Drawing.Point(920, 366);
            this.winCondition.Name = "winCondition";
            this.winCondition.Size = new System.Drawing.Size(0, 25);
            this.winCondition.TabIndex = 57;
            // 
            // simulateButton
            // 
            this.simulateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simulateButton.Location = new System.Drawing.Point(942, 663);
            this.simulateButton.Name = "simulateButton";
            this.simulateButton.Size = new System.Drawing.Size(136, 38);
            this.simulateButton.TabIndex = 58;
            this.simulateButton.Text = "Simulate";
            this.simulateButton.UseVisualStyleBackColor = true;
            this.simulateButton.Visible = false;
            this.simulateButton.Click += new System.EventHandler(this.simulateButton_Click);
            // 
            // Checkers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1138, 723);
            this.Controls.Add(this.simulateButton);
            this.Controls.Add(this.winCondition);
            this.Controls.Add(this.autoPlayerMove);
            this.Controls.Add(this.label1);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button autoPlayerMove;
        private System.Windows.Forms.Label winCondition;
        private System.Windows.Forms.Button simulateButton;
    }
}