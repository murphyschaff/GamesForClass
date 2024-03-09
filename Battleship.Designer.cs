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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Battleship));
            this.label3 = new System.Windows.Forms.Label();
            this.shipSelection = new System.Windows.Forms.DomainUpDown();
            this.button4 = new System.Windows.Forms.Button();
            this.guessFeedback = new System.Windows.Forms.Label();
            this.playerFeedback = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.autoPlaceShips = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.userShipInfo = new System.Windows.Forms.Label();
            this.cpuShipInfo = new System.Windows.Forms.Label();
            this.test = new System.Windows.Forms.Label();
            this.submarineRadio = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.normalRadio = new System.Windows.Forms.RadioButton();
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
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // shipSelection
            // 
            this.shipSelection.AllowDrop = true;
            this.shipSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shipSelection.Items.Add("Aircraft Carrier (Size: 4)");
            this.shipSelection.Items.Add("Battleship (Size: 3)");
            this.shipSelection.Items.Add("Destroyer (Size: 2)");
            this.shipSelection.Items.Add("Submarine (Size: 1)");
            this.shipSelection.Location = new System.Drawing.Point(683, 606);
            this.shipSelection.Name = "shipSelection";
            this.shipSelection.Size = new System.Drawing.Size(228, 22);
            this.shipSelection.TabIndex = 3;
            this.shipSelection.Text = "Select Ship";
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(437, 638);
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
            this.guessFeedback.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guessFeedback.Location = new System.Drawing.Point(391, 566);
            this.guessFeedback.Name = "guessFeedback";
            this.guessFeedback.Size = new System.Drawing.Size(0, 31);
            this.guessFeedback.TabIndex = 17;
            // 
            // playerFeedback
            // 
            this.playerFeedback.AutoSize = true;
            this.playerFeedback.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerFeedback.Location = new System.Drawing.Point(634, 581);
            this.playerFeedback.Name = "playerFeedback";
            this.playerFeedback.Size = new System.Drawing.Size(0, 16);
            this.playerFeedback.TabIndex = 10;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(50, 628);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(353, 48);
            this.label12.TabIndex = 19;
            this.label12.Text = "Key:\r\nA: Aircraft Carrier, B: Battleship, D: Destroyer, S: Submarine\r\nO: Miss, H:" +
    " Hit";
            // 
            // autoPlaceShips
            // 
            this.autoPlaceShips.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoPlaceShips.Location = new System.Drawing.Point(732, 632);
            this.autoPlaceShips.Name = "autoPlaceShips";
            this.autoPlaceShips.Size = new System.Drawing.Size(120, 36);
            this.autoPlaceShips.TabIndex = 20;
            this.autoPlaceShips.Text = "Auto Place!";
            this.autoPlaceShips.UseVisualStyleBackColor = true;
            this.autoPlaceShips.Click += new System.EventHandler(this.autoPlaceShips_Click);
            // 
            // startButton
            // 
            this.startButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.Location = new System.Drawing.Point(437, 591);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(120, 35);
            this.startButton.TabIndex = 21;
            this.startButton.Text = "Start Game!";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Visible = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 476);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 29);
            this.label1.TabIndex = 22;
            this.label1.Text = "Enemy Ship Information:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(579, 476);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(250, 29);
            this.label2.TabIndex = 23;
            this.label2.Text = "Your Ship Information:";
            // 
            // userShipInfo
            // 
            this.userShipInfo.AutoSize = true;
            this.userShipInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userShipInfo.Location = new System.Drawing.Point(579, 505);
            this.userShipInfo.Name = "userShipInfo";
            this.userShipInfo.Size = new System.Drawing.Size(0, 29);
            this.userShipInfo.TabIndex = 24;
            // 
            // cpuShipInfo
            // 
            this.cpuShipInfo.AutoSize = true;
            this.cpuShipInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cpuShipInfo.Location = new System.Drawing.Point(12, 505);
            this.cpuShipInfo.Name = "cpuShipInfo";
            this.cpuShipInfo.Size = new System.Drawing.Size(0, 29);
            this.cpuShipInfo.TabIndex = 25;
            // 
            // test
            // 
            this.test.AutoSize = true;
            this.test.Location = new System.Drawing.Point(50, 20);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(0, 13);
            this.test.TabIndex = 26;
            // 
            // submarineRadio
            // 
            this.submarineRadio.AutoSize = true;
            this.submarineRadio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submarineRadio.Location = new System.Drawing.Point(214, 598);
            this.submarineRadio.Name = "submarineRadio";
            this.submarineRadio.Size = new System.Drawing.Size(131, 20);
            this.submarineRadio.TabIndex = 28;
            this.submarineRadio.Text = "Single Submarine";
            this.submarineRadio.UseVisualStyleBackColor = true;
            this.submarineRadio.CheckedChanged += new System.EventHandler(this.submarineRadio_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(50, 600);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 16);
            this.label4.TabIndex = 29;
            this.label4.Text = "Game Mode:";
            // 
            // normalRadio
            // 
            this.normalRadio.AutoSize = true;
            this.normalRadio.Checked = true;
            this.normalRadio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.normalRadio.Location = new System.Drawing.Point(141, 598);
            this.normalRadio.Name = "normalRadio";
            this.normalRadio.Size = new System.Drawing.Size(69, 20);
            this.normalRadio.TabIndex = 30;
            this.normalRadio.TabStop = true;
            this.normalRadio.Text = "Normal";
            this.normalRadio.UseVisualStyleBackColor = true;
            this.normalRadio.CheckedChanged += new System.EventHandler(this.normalRadio_CheckedChanged);
            // 
            // Battleship
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 680);
            this.Controls.Add(this.normalRadio);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.submarineRadio);
            this.Controls.Add(this.test);
            this.Controls.Add(this.cpuShipInfo);
            this.Controls.Add(this.userShipInfo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.autoPlaceShips);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.guessFeedback);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.playerFeedback);
            this.Controls.Add(this.shipSelection);
            this.Controls.Add(this.label3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(18, 45);
            this.Name = "Battleship";
            this.Text = "Games for Class: Battleship";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DomainUpDown shipSelection;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label guessFeedback;
        private System.Windows.Forms.Label playerFeedback;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button autoPlaceShips;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label userShipInfo;
        private System.Windows.Forms.Label cpuShipInfo;
        private System.Windows.Forms.Label test;
        private System.Windows.Forms.RadioButton submarineRadio;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton normalRadio;
    }
}