using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Objects
{
    public class Row//TODO: User this wherever it can save a few lines of code
    {
        public Cell[] Cells { get; set; }

        public Row()
        {
            Cells = new Cell[9];
        }

        public Row(Cell cell)//Finds the row where "cell" belongs to, and saves it in Cells
        {
            Cells = new Cell[9];
            int rNum = -1;
            for (int r = 0; r < 9; r++)
            {
                for (int c = 0; c < 9; c++)
                {
                    if (MainWindow.cellsArray[r, c] == cell)
                    {
                        rNum = r;
                    }
                }
            }
            for (int c = 0; c < 9; c++)
            {
                this.Cells[c] = MainWindow.cellsArray[rNum, c];
            }
        }


    }
}
