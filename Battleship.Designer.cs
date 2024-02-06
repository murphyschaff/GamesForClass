namespace GamesForClass
{
    partial class Battleship
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
            this.label3 = new System.Windows.Forms.Label();
            this.shipSelection = new System.Windows.Forms.DomainUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.guessFeedback = new System.Windows.Forms.Label();
            this.playerFeedback = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.autoPlaceShips = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(387, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(247, 55);
            this.label3.TabIndex = 2;
            this.label3.Text = "Battleship!";
            // 
            // shipSelection
            // 
            this.shipSelection.AllowDrop = true;
            this.shipSelection.Items.Add("Aircraft Carrier (Size: 4)");
            this.shipSelection.Items.Add("Battleship (Size: 3)");
            this.shipSelection.Items.Add("Destroyer (Size: 2)");
            this.shipSelection.Items.Add("Submarine (Size: 1)");
            this.shipSelection.Location = new System.Drawing.Point(733, 535);
            this.shipSelection.Name = "shipSelection";
            this.shipSelection.Size = new System.Drawing.Size(120, 20);
            this.shipSelection.TabIndex = 3;
            this.shipSelection.Text = "Select Ship";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(58, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(410, 29);
            this.label7.TabIndex = 12;
            this.label7.Text = "1     2     3     4     5     6     7     8     9   :Y";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(606, 73);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(404, 29);
            this.label8.TabIndex = 13;
            this.label8.Text = "1     2     3     4     5     6     7     8     9  :Y";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(31, 103);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 290);
            this.label9.TabIndex = 14;
            this.label9.Text = "1\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7\r\n8\r\n9\r\nX";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(579, 103);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 290);
            this.label10.TabIndex = 15;
            this.label10.Text = "1\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7\r\n8\r\n9\r\nX";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(445, 573);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(120, 35);
            this.button4.TabIndex = 16;
            this.button4.Text = "Reset";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // guessFeedback
            // 
            this.guessFeedback.AutoSize = true;
            this.guessFeedback.Location = new System.Drawing.Point(416, 484);
            this.guessFeedback.Name = "guessFeedback";
            this.guessFeedback.Size = new System.Drawing.Size(0, 13);
            this.guessFeedback.TabIndex = 17;
            // 
            // playerFeedback
            // 
            this.playerFeedback.AutoSize = true;
            this.playerFeedback.Location = new System.Drawing.Point(731, 510);
            this.playerFeedback.Name = "playerFeedback";
            this.playerFeedback.Size = new System.Drawing.Size(0, 13);
            this.playerFeedback.TabIndex = 10;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(151, 557);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(285, 39);
            this.label12.TabIndex = 19;
            this.label12.Text = "Key:\r\nA: Aircraft Carrier, B: Battleship, D: Destroyer, S: Submarine\r\nO: Miss, H:" +
    " Hit";
            // 
            // autoPlaceShips
            // 
            this.autoPlaceShips.Location = new System.Drawing.Point(733, 561);
            this.autoPlaceShips.Name = "autoPlaceShips";
            this.autoPlaceShips.Size = new System.Drawing.Size(120, 35);
            this.autoPlaceShips.TabIndex = 20;
            this.autoPlaceShips.Text = "Auto Place Ships!";
            this.autoPlaceShips.UseVisualStyleBackColor = true;
            this.autoPlaceShips.Click += new System.EventHandler(this.autoPlaceShips_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(445, 526);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(120, 35);
            this.startButton.TabIndex = 21;
            this.startButton.Text = "Start Game!";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Visible = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // Battleship
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 617);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.autoPlaceShips);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.guessFeedback);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.playerFeedback);
            this.Controls.Add(this.shipSelection);
            this.Controls.Add(this.label3);
            this.MinimumSize = new System.Drawing.Size(18, 45);
            this.Name = "Battleship";
            this.Text = "Games for Class: Battleship";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DomainUpDown shipSelection;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label guessFeedback;
        private System.Windows.Forms.Label playerFeedback;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button autoPlaceShips;
        private System.Windows.Forms.Button startButton;
    }
}