namespace GamesForClass
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.minesweeper = new System.Windows.Forms.Button();
            this.checkersButton = new System.Windows.Forms.Button();
            this.loadingLabel = new System.Windows.Forms.Label();
            this.STTTbutton = new System.Windows.Forms.Button();
            this.supriseButton = new System.Windows.Forms.Button();
            this.wordGuessButton = new System.Windows.Forms.Button();
            this.sudokuButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(250, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(291, 78);
            this.label1.TabIndex = 0;
            this.label1.Text = "Games For Class\r\nBy Murphy Schaff";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(153, 171);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(151, 49);
            this.button1.TabIndex = 1;
            this.button1.Text = "Tic-Tac-Toe";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(343, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 29);
            this.label2.TabIndex = 2;
            this.label2.Text = "Games:";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(153, 236);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(151, 49);
            this.button2.TabIndex = 3;
            this.button2.Text = "War";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(153, 301);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(151, 49);
            this.button3.TabIndex = 4;
            this.button3.Text = "Battleship";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(468, 171);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(151, 49);
            this.button4.TabIndex = 5;
            this.button4.Text = "Yahtzee";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // minesweeper
            // 
            this.minesweeper.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minesweeper.Location = new System.Drawing.Point(468, 236);
            this.minesweeper.Name = "minesweeper";
            this.minesweeper.Size = new System.Drawing.Size(151, 49);
            this.minesweeper.TabIndex = 6;
            this.minesweeper.Text = "Minesweeper";
            this.minesweeper.UseVisualStyleBackColor = true;
            this.minesweeper.Click += new System.EventHandler(this.minesweeper_Click);
            // 
            // checkersButton
            // 
            this.checkersButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkersButton.Location = new System.Drawing.Point(468, 301);
            this.checkersButton.Name = "checkersButton";
            this.checkersButton.Size = new System.Drawing.Size(151, 49);
            this.checkersButton.TabIndex = 7;
            this.checkersButton.Text = "Checkers";
            this.checkersButton.UseVisualStyleBackColor = true;
            this.checkersButton.Click += new System.EventHandler(this.checkersButton_Click);
            // 
            // loadingLabel
            // 
            this.loadingLabel.AutoSize = true;
            this.loadingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadingLabel.Location = new System.Drawing.Point(332, 249);
            this.loadingLabel.Name = "loadingLabel";
            this.loadingLabel.Size = new System.Drawing.Size(0, 25);
            this.loadingLabel.TabIndex = 8;
            // 
            // STTTbutton
            // 
            this.STTTbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.STTTbutton.Location = new System.Drawing.Point(311, 228);
            this.STTTbutton.Name = "STTTbutton";
            this.STTTbutton.Size = new System.Drawing.Size(151, 66);
            this.STTTbutton.TabIndex = 9;
            this.STTTbutton.Text = "Super Tic-Tac-Toe";
            this.STTTbutton.UseVisualStyleBackColor = true;
            this.STTTbutton.Click += new System.EventHandler(this.STTTbutton_Click);
            // 
            // supriseButton
            // 
            this.supriseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.supriseButton.Location = new System.Drawing.Point(326, 362);
            this.supriseButton.Name = "supriseButton";
            this.supriseButton.Size = new System.Drawing.Size(124, 32);
            this.supriseButton.TabIndex = 10;
            this.supriseButton.Text = "Suprise Me!";
            this.supriseButton.UseVisualStyleBackColor = true;
            this.supriseButton.Click += new System.EventHandler(this.supriseButton_Click);
            // 
            // wordGuessButton
            // 
            this.wordGuessButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wordGuessButton.Location = new System.Drawing.Point(311, 301);
            this.wordGuessButton.Name = "wordGuessButton";
            this.wordGuessButton.Size = new System.Drawing.Size(151, 49);
            this.wordGuessButton.TabIndex = 11;
            this.wordGuessButton.Text = "Word Guess";
            this.wordGuessButton.UseVisualStyleBackColor = true;
            this.wordGuessButton.Click += new System.EventHandler(this.wordGuessButton_Click);
            // 
            // sudokuButton
            // 
            this.sudokuButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sudokuButton.Location = new System.Drawing.Point(311, 171);
            this.sudokuButton.Name = "sudokuButton";
            this.sudokuButton.Size = new System.Drawing.Size(151, 49);
            this.sudokuButton.TabIndex = 12;
            this.sudokuButton.Text = "Sudoku";
            this.sudokuButton.UseVisualStyleBackColor = true;
            this.sudokuButton.Click += new System.EventHandler(this.sudokuButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.sudokuButton);
            this.Controls.Add(this.wordGuessButton);
            this.Controls.Add(this.supriseButton);
            this.Controls.Add(this.STTTbutton);
            this.Controls.Add(this.loadingLabel);
            this.Controls.Add(this.checkersButton);
            this.Controls.Add(this.minesweeper);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "GamesForClass";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button minesweeper;
        private System.Windows.Forms.Button checkersButton;
        private System.Windows.Forms.Label loadingLabel;
        private System.Windows.Forms.Button STTTbutton;
        private System.Windows.Forms.Button supriseButton;
        private System.Windows.Forms.Button wordGuessButton;
        private System.Windows.Forms.Button sudokuButton;
    }
}

