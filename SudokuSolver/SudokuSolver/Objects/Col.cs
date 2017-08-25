using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Objects
{
    public class Col//TODO: User this wherever it can save a few lines of code
    {
        public Cell[] Cells { get; set; }

        public Col()
        {
            Cells = new Cell[9];
        }

        public Col(Cell cell)//Finds the col where "cell" belongs to, and saves it in Cells
        {
            int cNum = -1;
            for (int r = 0; r < 9; r++)
            {
                for (int c = 0; c < 9; c++)
                {
                    if (MainWindow.cellsArray[r, c] == cell)
                    {
                        cNum = c;
                    }
                }
            }
            for (int r = 0; r < 9; r++)
            {
                this.Cells[r] = MainWindow.cellsArray[r, cNum];
            }
        }
    }
}
