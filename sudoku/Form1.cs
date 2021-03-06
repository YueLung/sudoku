﻿using System;
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
        public int searchCount = 0;
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

        #region Clear Button Event
        private void ClearBtn_Click(object sender, EventArgs e)
        {
            foreach(var c in this.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Text = "";
            }
        }
        #endregion

        private void CalculateBtn_Click(object sender, EventArgs e)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            List<List<int>> board = getBoardDataFromUI();
            List<List<List<int>>> possibleBoard = getPossibleListBoard(board);

            int[] pos = getFirstToFindPos(board);

            if (pos[0] == -1)
            {
                displayResultToUI(board);
            }
            else
            {
                List<List<int>> result = findAnswer(board, possibleBoard, pos[0], pos[1]);
                displayResultToUI(result);
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            SpendTimeLabel.Text = $" Search Count :  {searchCount} \r\n {elapsedMs.ToString()} ms";
        }

        private void displayResultToUI(List<List<int>> board)
        {
            for (int i = 0; i < board.Count(); i++)
            {
                for (int j = 0; j < board[i].Count(); j++)
                {
                    if (((TextBox)this.Controls.Find($"Pos_{i}_{j}", true)[0]).Text == "")
                    {
                        ((TextBox)this.Controls.Find($"Pos_{i}_{j}", true)[0]).ForeColor = Color.Red;
                    }
                    
                    ((TextBox)this.Controls.Find($"Pos_{i}_{j}", true)[0]).Text = board[i][j].ToString();
                }
            }
        }

        int[] getFirstToFindPos(List<List<int>> board)
        {
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

            return new int[2] { -1, -1 };
        }

        private List<List<int>> getBoardDataFromUI()
        {
            //============================================ test data ============================================
            //init board(those number for test)
            //List<List<int>> board = new List<List<int>>() { new List<int> { 0, 2, 6, 0, 0, 0, 3, 7, 8 },
            //                                                new List<int> { 0, 5, 8, 6, 3, 7, 4, 0, 0 },
            //                                                new List<int> { 0, 4, 7, 0, 0, 0, 5, 6, 1 },
            //                                                new List<int> { 0, 0, 0, 7, 2, 0, 9, 0, 0 },
            //                                                new List<int> { 0, 0, 0, 3, 0, 8, 2, 5, 0 },
            //                                                new List<int> { 8, 0, 2, 0, 0, 0, 0, 1, 0 },
            //                                                new List<int> { 4, 6, 9, 5, 0, 1, 0, 0, 0 },
            //                                                new List<int> { 0, 0, 1, 9, 0, 0, 7, 4, 0 },
            //                                                new List<int> { 0, 3, 0, 0, 4, 0, 0, 9, 0 }};

            //List<List<int>> board = new List<List<int>>() { new List<int> { 9, 0, 0, 0, 0, 7, 5, 0, 0 },
            //                                                new List<int> { 2, 0, 5, 6, 0, 0, 0, 0, 0 },
            //                                                new List<int> { 0, 0, 8, 0, 1, 0, 0, 0, 2 },
            //                                                new List<int> { 4, 9, 7, 0, 0, 2, 0, 0, 5 },
            //                                                new List<int> { 0, 0, 0, 0, 0, 8, 0, 0, 4 },
            //                                                new List<int> { 0, 0, 0, 7, 6, 0, 2, 0, 9 },
            //                                                new List<int> { 0, 0, 0, 8, 7, 0, 0, 0, 6 },
            //                                                new List<int> { 0, 1, 0, 2, 0, 0, 4, 0, 0 },
            //                                                new List<int> { 0, 6, 2, 3, 0, 9, 0, 0, 0 }};

            List<List<int>> board = new List<List<int>>() { new List<int> { 0, 0, 0, 6, 0, 0, 0, 2, 0 },
                                                            new List<int> { 8, 0, 1, 0, 0, 7, 9, 0, 0 },
                                                            new List<int> { 6, 0, 0, 0, 0, 4, 1, 0, 0},
                                                            new List<int> { 0, 0, 5, 0, 0, 8, 0, 0, 0},
                                                            new List<int> { 0, 2, 8, 5, 6, 0, 4, 0, 3 },
                                                            new List<int> { 0, 0, 0, 0, 0, 0, 0, 8, 0 },
                                                            new List<int> { 0, 0, 0, 0, 9, 0, 0, 0, 7 },
                                                            new List<int> { 0, 0, 0, 7, 0, 0, 0, 1, 0 },
                                                            new List<int> { 1, 5, 0, 0, 0, 0, 0, 0, 4 }};
            //============================================ test data ============================================

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
                if (i == 0)
                    res.Clear();   

                List<List<int>> rowList = new List<List<int>>();

                for (int j = 0; j < board[i].Count(); j++)
                {
                    if (board[i][j] == 0) // if board[i][j] is empty,
                    {
                        List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                        for (int k = 0; k < board[i].Count(); k++)
                        {
                            //移除橫排有的數字
                            list.Remove(board[i][k]);
                        }

                        for (int k = 0; k < board.Count(); k++)
                        {
                            //移除縱列有的數字
                            list.Remove(board[k][j]);
                        }

                        int x = (i / 3) * 3;
                        int y = (j / 3) * 3;
                        for (int k = 0; k < 9; k++)
                        {
                            //移除9宮格內有的數字
                            list.Remove(board[x + k / 3][y + k % 3]);
                        }

                        //list 數量為1 表示可以直接填這個數字
                        if (list.Count() == 1)
                        {
                            board[i][j] = list[0];
                            i = -1;
                            break;
                        }

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
            searchCount++;

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

            for (int i = 0; i < board[0].Count(); i++)
            {
                if (i == x)
                    continue;

                if (val == board[y][i])
                    return false;
            }

            for (int i = 0; i < board.Count(); i++)
            {
                if (i == y)
                    continue;

                if (val == board[i][x])
                    return false;
            }

            int ini_y = (y / 3) * 3;
            int ini_x = (x / 3) * 3;

            for (int i = 0; i < board.Count(); i++)
            {
                if (ini_y + i / 3 == y && ini_x + i % 3 == x)
                    continue;

                if (val == board[ini_y + i / 3][ini_x + i % 3])
                    return false;
            }

            return true;
        }
    }
}
