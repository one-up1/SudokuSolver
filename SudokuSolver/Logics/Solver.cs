using SudokuSolver.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SudokuSolver.Logics
{
    public class Solver
    {
        private int iterations;
        private int numbersTried;

        public void Reset()
        {
            iterations = 0;
            numbersTried = 0;
        }

        public int[][] Solve(int[][] sudoku)
        {
            iterations++;
            for (int row = 0; row < sudoku.Length; row++)
            {
                for (int col = 0; col < sudoku[row].Length; col++)
                {
                    if (sudoku[row][col] == 0)
                    {
                        for (int val = 1; val <= 9; val++)
                        {
                            if (IsValid(sudoku, row, col, val))
                            {
                                sudoku[row][col] = val;
                                if (Solve(sudoku) == null)
                                {
                                    sudoku[row][col] = 0;
                                }
                                else
                                {
                                    return sudoku;
                                }
                            }
                        }
                        return null;
                    }
                }
            }
            return sudoku;
        }

        public int[][] Create(int[][] sudoku)
        {
            return sudoku;
        }

        private bool IsValid(int[][] sudoku, int row, int col, int val)
        {
            numbersTried++;
            for (int i = 0; i < 9; i++)
            {
                if (sudoku[i][col] == val)
                {
                    return false;
                }

                if (sudoku[row][i] == val)
                {
                    return false;
                }

                if (sudoku[3 * (row / 3) + i / 3][3 * (col / 3) + i % 3] == val)
                {
                    return false;
                }
            }
            return true;
        }

        public int Iterations { get { return iterations; } }

        public int NumbersTried { get { return numbersTried; } }
    }
}