using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sudoku
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            iniLayout();
        }

        #region layout and textBox event
        public void iniLayout()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    TextBox textBox = new TextBox();

                    textBox.Size = new Size(42, 20);
                    textBox.BackColor = System.Drawing.SystemColors.Menu;
                    textBox.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
                    textBox.Location = new System.Drawing.Point(22 + (j * 57), 33 + (i * 51));
                    textBox.MaxLength = 1;
                    textBox.Name = $"Pos_{i}_{j}";
                    textBox.TabIndex = 0;
                    textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
                    textBox.KeyPress += textBox_KeyPress;

                    this.Controls.Add(textBox);
                }
            }
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        #endregion

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            foreach(var c in this.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Text = "";
            }
        }

        private void CalculateBtn_Click(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            List<List<int>> board = getBoardDataFromUI();
            List<List<List<int>>> possibelBoard = getPossibleListBoard(board);

            int[] pos = getFirstToFindPos(board);
            if (pos.Count() == 0) return;

            List<List<int>> result = findAnswer(board, possibelBoard, pos[0], pos[1]);
            displayResult2toUI(result);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            SpendTimeLabel.Text = elapsedMs.ToString() + "ms";
        }

        private void displayResult2toUI(List<List<int>> board)
        {
            for (int i = 0; i < board.Count(); i++)
            {
                for (int j = 0; j < board[i].Count(); j++)
                {
                    ((TextBox)this.Controls.Find($"Pos_{i}_{j}", true)[0]).Text = board[i][j].ToString();
                }
            }
        }

        int[] getFirstToFindPos(List<List<int>> board)
        {
            int[] pos = new int[2];

            for (int i = 0; i < board.Count(); i++)
            {
                for (int j = 0; j < board[i].Count(); j++)
                {
                    if (board[i][j] == 0)
                    {
                        return new int[2] { i, j };
                    }
                }
            }

            return pos;
        }

        private List<List<int>> getBoardDataFromUI()
        {
            //init board(those number for test)
            List<List<int>> board = new List<List<int>>() { new List<int> { 0, 2, 6, 0, 0, 0, 3, 7, 8 },
                                                            new List<int> { 0, 5, 8, 6, 3, 7, 4, 0, 0 },
                                                            new List<int> { 0, 4, 7, 0, 0, 0, 5, 6, 1 },
                                                            new List<int> { 0, 0, 0, 7, 2, 0, 9, 0, 0 },
                                                            new List<int> { 0, 0, 0, 3, 0, 8, 2, 5, 0 },
                                                            new List<int> { 8, 0, 2, 0, 0, 0, 0, 1, 0 },
                                                            new List<int> { 4, 6, 9, 5, 0, 1, 0, 0, 0 },
                                                            new List<int> { 0, 0, 1, 9, 0, 0, 7, 4, 0 },
                                                            new List<int> { 0, 3, 0, 0, 4, 0, 0, 9, 0 }};

            foreach (var c in this.Controls)
            {
                if (c is TextBox)
                {
                    string[] str = ((TextBox)c).Name.Split('_');

                    int val = 0;
                    bool isParsable = Int32.TryParse(((TextBox)c).Text, out val);

                    board[Convert.ToInt32(str[1])][Convert.ToInt32(str[2])] = val;
                }
            }

            return board;
        }

        private List<List<List<int>>> getPossibleListBoard(List<List<int>> board)
        {
            List<List<List<int>>> res = new List<List<List<int>>>();

            for (int i = 0; i < board.Count(); i++)
            {
                List<List<int>> rowList = new List<List<int>>();

                for (int j = 0; j < board[i].Count(); j++)
                {
                    if (board[i][j] == 0)
                    {
                        List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                        for (int k = 0; k < board[i].Count(); k++)
                        {
                            list.Remove(board[i][k]);
                        }

                        for (int k = 0; k < board.Count(); k++)
                        {
                            list.Remove(board[k][j]);
                        }

                        int x = (i / 3) * 3;
                        int y = (j / 3) * 3;
                        for (int k = 0; k < 9; k++)
                        {
                            list.Remove(board[x + k / 3][y + k % 3]);
                        }
  
                        //if (list.Count() == 1)
                        //{
                        //    board[i][j] = list[0];
                        //    list.RemoveAt(0);
                        //    i = 0;
                        //    j = 0;
                        //    res.Clear();
                        //    break;
                        //}

                        rowList.Add(list);
                    }
                    else
                    {
                        rowList.Add(new List<int>());
                    }
                }

                res.Add(rowList);
            }

            return res;
        }

        private List<List<int>> findAnswer(List<List<int>> board, List<List<List<int>>> possibleTable, int depth_y, int depth_x)
        {
            int a = 0;
            if (depth_y == 8 && depth_x == 8)
                a= 2;

            List<List<int>> copyBoard = getCopyBoard(board);

            List<List<int>> res = new List<List<int>>();

            if (depth_y == 9)
                return copyBoard;

            foreach (int val in possibleTable[depth_y][depth_x])
            {
                copyBoard[depth_y][depth_x] = val;

                if (isLegal(copyBoard, depth_y, depth_x))
                {
                    int next_x = depth_x;
                    int next_y = depth_y;

                    ++next_x;

                    if (next_x >= 9)
                    {
                        next_x = 0;
                        next_y++;
                    }

                    while (next_y < 9 && possibleTable[next_y][next_x].Count == 0)
                    {
                        ++next_x;

                        if (next_x >= 9)
                        {
                            next_x = 0;
                            next_y++;
                        }
                    }

                    var tmp = findAnswer(copyBoard, possibleTable, next_y, next_x);

                    if (tmp.Capacity != 0)
                    {
                        res = tmp;
                        return res;
                    }
                        
                }
            }

            return res;
        }

        private List<List<int>> getCopyBoard(List<List<int>> board)
        {
            List<List<int>> copyBoard = new List<List<int>>();
            for (int i = 0; i < board.Count(); i++)
            {
                List<int> rowList = new List<int>(board[i]);
                copyBoard.Add(rowList);
            }

            return copyBoard;
        }

        private bool isLegal(List<List<int>> board, int y, int x)
        {
            int val = board[y][x];

            for (int k = 0; k < board[0].Count(); k++)
            {
                if (k == x)
                    continue;

                if (val == board[y][k])
                    return false;
            }

            for (int k = 0; k < board.Count(); k++)
            {
                if (k == y)
                    continue;

                if (val == board[k][x])
                    return false;
            }

            int ini_y = (y / 3) * 3;
            int ini_x = (x / 3) * 3;

            for (int k = 0; k < board.Count(); k++)
            {
                if (ini_y + k / 3 == y && ini_x + k % 3 == x)
                    continue;

                if (val == board[ini_y + k / 3][ini_x + k % 3])
                    return false;
            }

            return true;
        }
    }
}
