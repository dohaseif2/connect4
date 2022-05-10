using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Timers;


namespace Connect4_Application
{
    public partial class Form1 : Form
    {
        Game game = new Game();
        PictureBox[,] map = new PictureBox[6, 7];
        Image player1;
        Image player2;
        Image empty;

        public Form1()
        {
            InitializeComponent();

            wireMap();
            player1 = Image.FromFile(@"C:\Users\Sroor For Laptop\OneDrive\سطح المكتب\red.png");
            player2 = Image.FromFile(@"C:\Users\Sroor For Laptop\OneDrive\سطح المكتب\yellow.png");
            empty = Image.FromFile(@"C:\Users\Sroor For Laptop\OneDrive\سطح المكتب\board.png");
            label2.Text = PlayerName.firstPlayer + " is playing";
            game.initializeBoard();
        }      

        public void wireMap()
        {
            map[0, 0] = emptyBox0;
            map[0, 1] = emptyBox1;
            map[0, 2] = emptyBox2;
            map[0, 3] = emptyBox3;
            map[0, 4] = emptyBox4;
            map[0, 5] = emptyBox5;
            map[0, 6] = emptyBox6;
            map[1, 0] = emptyBox7;
            map[1, 1] = emptyBox8;
            map[1, 2] = emptyBox9;
            map[1, 3] = emptyBox10;
            map[1, 4] = emptyBox11;
            map[1, 5] = emptyBox12;
            map[1, 6] = emptyBox13;
            map[2, 0] = emptyBox14;
            map[2, 1] = emptyBox15;
            map[2, 2] = emptyBox16;
            map[2, 3] = emptyBox17;
            map[2, 4] = emptyBox18;
            map[2, 5] = emptyBox19;
            map[2, 6] = emptyBox20;
            map[3, 0] = emptyBox21;
            map[3, 1] = emptyBox22;
            map[3, 2] = emptyBox23;
            map[3, 3] = emptyBox24;
            map[3, 4] = emptyBox25;
            map[3, 5] = emptyBox26;
            map[3, 6] = emptyBox27;
            map[4, 0] = emptyBox28;
            map[4, 1] = emptyBox29;
            map[4, 2] = emptyBox30;
            map[4, 3] = emptyBox31;
            map[4, 4] = emptyBox32;
            map[4, 5] = emptyBox33;
            map[4, 6] = emptyBox34;
            map[5, 0] = emptyBox35;
            map[5, 1] = emptyBox36;
            map[5, 2] = emptyBox37;
            map[5, 3] = emptyBox38;
            map[5, 4] = emptyBox39;
            map[5, 5] = emptyBox40;
            map[5, 6] = emptyBox41;
        }

        public void win()
        {
            game.checkWin();
            game.NextTurn();
            if (game.win == true)
            {
                if (game.currentTurn == "player1")
                {
                    MessageBox.Show(PlayerName.secondPlayer + " won!", "Connect4");
                    System.Windows.Forms.Application.Exit();
                }
                else
                {
                    MessageBox.Show(PlayerName.firstPlayer + " won!", "Connect4");
                    System.Windows.Forms.Application.Exit();
                }
            }
        }

        public void setColor(int column)
        {
            int row = game.updateBoard(column);

            if (row >= 0)
            {
                if (game.currentTurn == "player1")
                {
                    label2.Text = PlayerName.secondPlayer + " is playing";
                    map[row, column].Image = player1;
                    win();
                }
                else
                {
                    label2.Text = PlayerName.firstPlayer + " is playing";
                    map[row, column].Image = player2;
                    win();
                }
            }
        }

        private void column1_Click(object sender, EventArgs e)
        {
            setColor(0);
        }

        private void column2_Click(object sender, EventArgs e)
        {
            setColor(1);
        }

        private void column3_Click(object sender, EventArgs e)
        {
            setColor(2);
        }

        private void column4_Click(object sender, EventArgs e)
        {
            setColor(3);
        }

        private void column5_Click(object sender, EventArgs e)
        {
            setColor(4);
        }

        private void column6_Click(object sender, EventArgs e)
        {
            setColor(5);
        }

        private void column7_Click(object sender, EventArgs e)
        {
            setColor(6);
        }


    }

    class PlayerName
    {
        public static string firstPlayer;
        public static string secondPlayer;
    }

    class Game
    {
        public const int row = 6;
        public const int col = 7;
        public int rowHolder;
        string empty = "empty";
        string[,] board = new string[row, col];
        public string currentTurn = "player1";
        public bool win = false;

        public void NextTurn()
        {
            if (currentTurn == "player1")
            {
                currentTurn = "player2";
            }
            else
            {
                currentTurn = "player1";
            }
        }

        public void initializeBoard()
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    board[i, j] = empty;
                }
            }
        }

        public int updateBoard(int column)
        {
            rowHolder = -1;
            for (int i = 5; i >= 0; i--)
            {
                if (board[i, column] == empty && i > rowHolder)
                {
                    rowHolder = i;
                    board[rowHolder, column] = currentTurn;
                }
            }
            return rowHolder;
        }

        public void checkWin()
        {
            // veritcal win.
            for (int i = 0; i < row - 3; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (board[i, j] == currentTurn && board[i + 1, j] == currentTurn && board[i + 2, j] == currentTurn && board[i + 3, j] == currentTurn)
                    {
                        win = true;
                    }
                }
            }
            // Horizontal Win.
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col - 3; j++)
                {
                    if (board[i, j] == currentTurn && board[i, j + 1] == currentTurn && board[i, j + 2] == currentTurn && board[i, j + 3] == currentTurn)
                    {
                        win = true;
                    }
                }
            }
            // Down diagonal win.
            for (int i = 0; i <= row - 4; i++)
            {
                for (int j = 0; j <= col - 4; j++)
                {
                    if (board[i, j] == currentTurn && board[i + 1, j + 1] == currentTurn && board[i + 2, j + 2] == currentTurn && board[i + 3, j + 3] == currentTurn)
                    {
                        win = true;
                    }
                }
            }
            // Up diagonal win.
            for (int i = 3; i <= row - 1; i++)
            {
                for (int j = 0; j <= col - 4; j++)
                {
                    if (board[i, j] == currentTurn && board[i - 1, j + 1] == currentTurn && board[i - 2, j + 2] == currentTurn && board[i - 3, j + 3] == currentTurn)
                    {
                        win = true;
                    }
                }
            }
        }
    }
}