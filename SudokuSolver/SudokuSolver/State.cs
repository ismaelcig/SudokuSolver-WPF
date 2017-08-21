using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{//This class will allow to save the state of a sudoku so we can go back to it
    //A comboBox will be added to allow the user to change between saved states
    public class State
    {
        string name;
        DateTime time;
        Cell[,] array;

        public State()//Falta nombre y añadir a la lista
        {
            //Time = DateTime.Now;
            //Array = new Cell[9, 9];
            //for (int i = 0; i < 9; i++)
            //{
            //    for (int j = 0; j < 9; j++)
            //    {
            //        Array[i, j] = MainWindow.cellsArray[i, j];
            //    }
            //}
        }

        public State(string str)//Hace todo automáticamente
        {
            //Time = DateTime.Now;
            //if (str != "")
            //    Name = str;
            //else
            //    Name = Time.ToLongTimeString();
            
            //Array = new Cell[9, 9];
            //for (int i = 0; i < 9; i++)
            //{
            //    for (int j = 0; j < 9; j++)
            //    {
            //        Array[i, j] = MainWindow.cellsArray[i, j];
            //    }
            //}
            //MainWindow.states.Add(this);
        }

        public string Name { get => name; set => name = value; }
        public DateTime Time { get => time; set => time = value; }
        public Cell[,] Array { get => array; set => array = value; }
    }
}
