using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connect4_Application
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Sets the player name variables to the text box inputs.
            PlayerName.firstPlayer = textBox1.Text;
            PlayerName.secondPlayer = textBox2.Text;
            // Switches to the main game form.
            Form1 form = new Form1();
            this.Visible = false;
            form.ShowDialog();
            System.Windows.Forms.Application.Exit();
        }

        
    }
}
