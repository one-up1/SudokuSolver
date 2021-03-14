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
        private int comparisons;

        public void Reset()
        {
            iterations = 0;
            numbersTried = 0;
            comparisons = 0;
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
                if (Equals(sudoku[i][col], val))
                {
                    return false;
                }

                if (Equals(sudoku[row][i], val))
                {
                    return false;
                }

                if (Equals(sudoku[3 * (row / 3) + i / 3][3 * (col / 3) + i % 3], val))
                {
                    return false;
                }
            }
            return true;
        }

        private bool Equals(int i, int val)
        {
            comparisons++;
            return i != 0 && i == val;
        }

        public int Iterations { get { return iterations; } }

        public int NumbersTried { get { return numbersTried; } }

        public int Comparisons { get { return comparisons; } }

        /*private static bool SolveRecursive(int[][] sudoku)
        {
            List<int> numbers = new List<int>();
            for (int r = 0; r < sudoku.Length; r++)
            {
                int[] row = sudoku[r];
                AddNumbers(numbers, row);

                for (int c = 0; c < row.Length; c++)
                {
                    if (row[c] == 0)
                    {
                        foreach (int num in numbers)
                        {
                            if (!Contains(sudoku, c, num))
                            {
                                row[c] = num;
                                //if (SolveRecursive(sudoku))
                                //{
                                    break;
                                //}
                                //else
                                //{
                                //    row[c] = 0;
                                //}
                            }
                            //return false;
                        }
                        numbers.Remove(row[c]);
                    }
                }
            }
            return true;
        }

        private static void AddNumbers(List<int> numbers, int[] existing)
        {
            numbers.Clear();
            for (int i = 1; i <= 9; i++)
            {
                if (Array.IndexOf(existing, i) == -1)
                {
                    numbers.Add(i);
                }
            }
        }

        private static bool Contains(int[][] sudoku, int c, int num)
        {
            for (int i = 0; i < sudoku.Length; i++)
            {
                if (sudoku[i][c] == num)
                {
                    return true;
                }
            }
            return false;
        }*/
    }
}