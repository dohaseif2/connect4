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

namespace Connect4_Application_with_AI
{
    public partial class Form1 : Form
    {
        public PictureBox[,] map = new PictureBox[6, 7];        
        public Image player = Image.FromFile("images/Red.png");
        public Image computer = Image.FromFile("images/yellow.png");
        public Image _empty = global::Connect4_Application_with_AI.Properties.Resources.board;
        public const int row = 6;
        public const int col = 7;
        int empty = 0;
        int[,] Board = new int[row, col];
        public int currentTurn = 1;
        public bool won = false;
        public const int HUMAN_PIECE = 1;
        public const int COMPUTER_PIECE = 2;
        public const int WINDOW_LENGTH = 4;
        public const int NONE = -1;
        public const int COMPUTER_VALUE = -2;
        public const int HUMAN_VALUE = -3;
        public int depth = 6;

        public Form1()
        {
            InitializeComponent();

            wireMap();
            initializeBoard();
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

        public void setColor(int column)
        {
            int r = getNextFreeRow(Board, column);

            if (r >= 0)
            {
                if (currentTurn == 1)
                {

                    map[r, column].Image = player;
                    drop_piece(Board, r, column, HUMAN_PIECE);
                    win(HUMAN_PIECE);
                }
                else
                {
                    map[r, column].Image = computer;
                    drop_piece(Board, r, column, COMPUTER_PIECE);
                    win(COMPUTER_PIECE);
                }
            }
        }

        private void comp(int[,] board)
        {

            if (currentTurn == 2)
            {
                int columnIndex = minMax(board, this.depth, int.MinValue, int.MaxValue, true)[0];
                setColor(columnIndex);
            }

        }

        public void NextTurn()
        {
            if (currentTurn == 1)
            {
                currentTurn = 2;
                comp(Board);
            }
            else
            {
                currentTurn = 1;
            }
        }

        public void initializeBoard()
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Board[i, j] = empty;
                }
            }
        }

        private bool isValidLocation(int[,] board, int column)
        {
            return board[0, column] == 0;
        }

        private int[] getValidLocations(int[,] board)
        {
            List<int> validLocations = new List<int>();
            for (int i = 0; i < col; i++)
            {
                if (isValidLocation(board, i))
                {
                    validLocations.Add(i);
                }
            }
            return validLocations.ToArray();
        }

        private int getNextFreeRow(int[,] board, int column)
        {
            for (int r = row - 1; r >= 0; r--)
            {
                if (board[r, column] == 0)
                {
                    return r;
                }

            }
            return -1;
        }

        private int count(int[] array, int p)
        {
            int cnt = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == p)
                {
                    cnt++;
                }
            }
            return cnt;
        }

        private int evaluate_board(int[] array, int piece)
        {
            int score = 0;
            int opp_piece = HUMAN_PIECE;
            if (piece == HUMAN_PIECE)
                opp_piece = COMPUTER_PIECE;

            if (count(array, piece) == 4)
            {
                score += 100;
            }
            else
                if (count(array, piece) == 3 && count(array, empty) == 1)
                {
                    score += 5;
                }
                else if (count(array, piece) == 2 && count(array, empty) == 2)
                {
                    score += 2;
                }

            if (count(array, opp_piece) == 3 && count(array, empty) == 1)
            {
                score -= 4;
            }
            return score;
        }

        public int[] subArray(int[] array, int start, int end)
        {
            int j = 0;
            int[] subArray = new int[10];
            for (int i = start; i < end; i++)
            {
                subArray[j] = array[i];
                j++;
            }
            return subArray;
        }
        private int score_position(int[,] board, int piece)
        {
            int score = 0;
            List<int> center_array = new List<int>();
            List<int> row_array = new List<int>();
            List<int> col_array = new List<int>();
            List<int> window = new List<int>();

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    center_array.Add(board[i, j]);
                }
            }

            score += count(center_array.ToArray(), piece) * 3;


            //score Horizontal 
            for (int r = 0; r < row; r++)
            {

                for (int j = 0; j < col; j++)
                {

                    row_array.Add(board[r, j]);

                }

                for (int c = 0; c < col - 3; c++)
                {


                    score += evaluate_board(subArray(row_array.ToArray(), c, c + 4), piece);

                }
            }

            //score vertical
            for (int c = 0; c < col; c++)
            {

                for (int j = 0; j < row; j++)
                {

                    col_array.Add(board[j, c]);

                }

                for (int r = 0; r < row - 3; r++)
                {


                    score += evaluate_board(subArray(col_array.ToArray(), c, c + 4), piece);

                }
            }

            //score positive sloped diag
            for (int r = 0; r < row - 3; r++)
            {

                for (int j = 0; j < col - 3; j++)
                {

                    for (int i = 0; i < WINDOW_LENGTH; i++)

                        window.Add(board[r + i, j + i]);
                    score += evaluate_board(window.ToArray(), piece);

                }
            }

            //score positive sloped diag
            for (int r = 0; r < row - 3; r++)
            {

                for (int j = 0; j < col - 3; j++)
                {

                    for (int i = 0; i < WINDOW_LENGTH; i++)

                        window.Add(board[r + 3 - i, j + i]);
                    score += evaluate_board(window.ToArray(), piece);

                }
            }

            return score;
        }

        private bool winning_move(int[,] board, int piece)
        {
            //horizontal
            for (int c = 0; c < col - 3; c++)
            {
                for (int r = 0; r < row; r++)
                {
                    if (board[r, c] == piece && board[r, c + 1] == piece && board[r, c + 2] == piece && board[r, c + 3] == piece)
                    {
                        won = true;
                        return true;
                    }
                }
            }

            //vertical
            for (int c = 0; c < col; c++)
            {
                for (int r = 0; r < row - 3; r++)
                {
                    if (board[r, c] == piece && board[r + 1, c] == piece && board[r + 2, c] == piece && board[r + 3, c] == piece)
                    {
                        won = true;
                        return true;
                    }
                }
            }


            //positively sloped diagonals
            for (int c = 0; c < col - 3; c++)
            {
                for (int r = 0; r < row - 3; r++)
                {
                    if (board[r, c] == piece && board[r + 1, c + 1] == piece && board[r + 2, c + 2] == piece && board[r + 3, c + 3] == piece)
                    {
                        won = true;
                        return true;
                    }
                }
            }

            //negatively sloped diagonals
            for (int c = 0; c < col - 3; c++)
            {
                for (int r = 3; r < row; r++)
                {
                    if (board[r, c] == piece && board[r - 1, c + 1] == piece && board[r - 2, c + 2] == piece && board[r - 3, c + 3] == piece)
                    {
                        won = true;
                        return true;
                    }
                }
            }
            return false;

        }

        private bool isTerminalNode(int[,] board)
        {
            return winning_move(board, HUMAN_PIECE) || winning_move(board, COMPUTER_PIECE) || getValidLocations(board).Length == 0;
        }

        private void drop_piece(int[,] board, int row, int column, int piece)
        {

            board[row, column] = piece;

        }


        private int[,] copyTo(int[,] board)
        {
            int[,] temp = new int[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    temp[i, j] = board[i, j];
                }
            }
            return temp;
        }

        private int[] minMax(int[,] board, int depth, int alpha, int beta, bool maximizingPlayer)
        {

            int[] valid_locations = getValidLocations(board);
            bool is_terminal = isTerminalNode(board);
            List<int> values = new List<int>();

            if (depth == 0 || is_terminal)
            {
                if (is_terminal)
                {
                    if (winning_move(board, COMPUTER_PIECE))
                    {
                        values.Add(NONE);
                        values.Add(COMPUTER_VALUE);
                        return values.ToArray();
                    }
                    else
                    {

                        if (winning_move(board, HUMAN_PIECE))
                        {
                            values.Add(NONE);
                            values.Add(HUMAN_VALUE);
                            return values.ToArray();
                        }
                        else
                        {
                            values.Add(NONE);
                            values.Add(0);
                            return values.ToArray();
                        }
                    }

                }
                else
                {
                    values.Add(NONE);
                    values.Add(score_position(board, COMPUTER_PIECE));
                    return values.ToArray();
                }

            }

            if (maximizingPlayer)
            {
                int value = int.MinValue;
                Random rand = new Random();
                int index = rand.Next(valid_locations.Length);
                int column = valid_locations[index];
                int[,] temp = new int[row, col];
                for (int c = 0; c < valid_locations.Length; c++)
                {
                    int _row = getNextFreeRow(board, valid_locations[c]);
                    Console.WriteLine("Minmax" + row);
                    temp = copyTo(board);
                    Console.WriteLine("Temp from min max");
                    drop_piece(temp, _row, valid_locations[c], COMPUTER_PIECE);
                    int new_score = minMax(temp, depth - 1, alpha, beta, false)[1];
                    if (new_score > value)
                    {
                        value = new_score;
                        column = valid_locations[c];
                    }
                    alpha = Math.Max(alpha, value);
                    if (alpha >= beta)
                    {
                        break;
                    }

                }
                values.Add(column);
                values.Add(value);
                return values.ToArray();
            }
            else
            {
                int value = int.MaxValue;
                Random rand = new Random();
                int index = rand.Next(valid_locations.Length);
                int column = valid_locations[index];
                int[,] temp = new int[row, col];
                for (int c = 0; c < valid_locations.Length; c++)
                {
                    int _row = getNextFreeRow(board, valid_locations[c]);

                    temp = copyTo(board);
                    drop_piece(temp, _row, valid_locations[c], HUMAN_PIECE);
                    int new_score = minMax(temp, depth - 1, alpha, beta, true)[1];
                    if (new_score < value)
                    {
                        value = new_score;
                        column = valid_locations[c];
                    }
                    beta = Math.Min(beta, value);
                    if (alpha >= beta)
                    {
                        break;
                    }

                }
                values.Add(column);
                values.Add(value);
                return values.ToArray();
            }


        }

        public void win(int piece)
        {
            
            
            if (winning_move(Board, piece))
            {
                if (currentTurn == 1)
                {
                    MessageBox.Show("You won!", "Connect4");
                    System.Windows.Forms.Application.Exit();
                }
                else
                {
                    MessageBox.Show("computer won!", "Connect4");
                    System.Windows.Forms.Application.Exit();
                }
            }
            else
                NextTurn();
        }



    }

}
