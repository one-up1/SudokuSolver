using SudokuSolver.Logics;
using SudokuSolver.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SudokuSolver.Controllers
{
    public class SudokuController : Controller
    {
        private Solver solver = new Solver();
        private SudokuModel sudokuModel = new SudokuModel();
        private IEnumerable<Sudoku> SudokuList = Sudokus.MockData();
        private Stopwatch stopwatch = new Stopwatch();

        // GET: Sudoku
        public ActionResult Sudoku()
        {
            if (TempData["sudoku"] != null)
            {
                sudokuModel = TempData["sudoku"] as SudokuModel;
            }

            sudokuModel.Sudokus = SudokuList;

            if (sudokuModel.Cells == null)
            {
                sudokuModel.Cells = SudokuList.ElementAt(0).Cells;
            }

            return View(sudokuModel);
        }

        public ActionResult Solve(SudokuModel Model)
        {
            solver.Reset();
            stopwatch.Reset();

            stopwatch.Start();
            int[][] cells = solver.Solve(Model.Cells, Model.X);
            stopwatch.Stop();

            TempData["Iterations"] = solver.Iterations;
            TempData["NumbersTried"] = solver.NumbersTried;
            TempData["ElapsedMilliseconds"] = stopwatch.ElapsedMilliseconds;

            if (cells == null)
            {
                TempData["Message"] = "Invalid board";
            }
            else
            {
                Model.Cells = cells;
            }

            TempData["sudoku"] = Model;
            return RedirectToAction("Sudoku");
        }

        public ActionResult ChangeSudoku(int sudokuNumber = 2)
        {
            TempData["sudoku"] = new SudokuModel { Cells = SudokuList.ElementAt(sudokuNumber).Cells, SudokuId = sudokuNumber };
            return RedirectToAction("Sudoku");
        }

        public ActionResult CreateSudoku()
        {
            sudokuModel.Cells = solver.Create(SudokuList.ElementAt(2).Cells);
            TempData["sudoku"] = sudokuModel;
            return RedirectToAction("Sudoku");
        }
    }
}