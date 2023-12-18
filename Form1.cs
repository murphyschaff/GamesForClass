using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GamesForClass
{
    public partial class Form1 : Form
    {
        private TicTacToe ticTac;
        private War war;
        public Form1()
        {
            InitializeComponent();
        }
        /* Tic Tac Toe game */
        private void button1_Click(object sender, EventArgs e)
        {
            ticTac = new TicTacToe();
            ticTac.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            war = new War();
            war.Show();
        }
    }
}
