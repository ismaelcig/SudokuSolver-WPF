using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SudokuSolver
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public static List<Cell> cells;//List containing all the cells
        public static Cell[,] cellsArray;//Array containing all the cells in position
        //Cells grouped by position
        public static Cell[,] array11 = new Cell[3, 3];
        public static Cell[,] array12 = new Cell[3, 3];
        public static Cell[,] array13 = new Cell[3, 3];
        public static Cell[,] array21 = new Cell[3, 3];
        public static Cell[,] array22 = new Cell[3, 3];
        public static Cell[,] array23 = new Cell[3, 3];
        public static Cell[,] array31 = new Cell[3, 3];
        public static Cell[,] array32 = new Cell[3, 3];
        public static Cell[,] array33 = new Cell[3, 3];

        public static List<Cell[,]> arrays;

        public static Cell selectedCell = null;
        public static bool solved = false;
        HashSet<int> Descartes;

        Thread t;
        delegate void UpdateInterface();
        UpdateInterface ui;
        int aux = -1;
        bool somethingChanged = false;

        public MainWindow()
        {
            InitializeComponent();
            ResetSudoku();
        }
        void UpdateCellNumber()
        {
            if (aux > 0)
            {//Si hay que actualizar el nº de la celda
                selectedCell.setNum(aux);
                selectedCell.grid.Background = Brushes.Beige;
                selectedCell.Solved = true;
                aux = -1;
            }
            else if (!selectedCell.Fixed && !selectedCell.Solved)
            {
                selectedCell.ShowPossibles();
            }
            //Marca como seleccionada
            Unselect();
            selectedCell.selected = true;
            selectedCell.SelectionChanged();
        }


        public static void Unselect()
        {
            foreach (Cell c in cellsArray)
            {
                if (!c.Fixed && !c.Solved)
                {
                    c.selected = false;
                    c.SelectionChanged();
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {//Sólo se pueden pulsar números
            if (e.Key == Key.Escape)
            {
                Application.Current.Shutdown();
            }
            else if (e.Key == Key.Delete)
            {
                selectedCell.Reset();
            }
            else if (!selectedCell.Fixed)
            {
                int keyVal = (int)e.Key;
                int value = -1;
                if (keyVal >= (int)Key.D0 && keyVal <= (int)Key.D9)
                {
                    value = (int)e.Key - (int)Key.D0;
                }
                else if (keyVal >= (int)Key.NumPad0 && keyVal <= (int)Key.NumPad9)
                {
                    value = (int)e.Key - (int)Key.NumPad0;
                }
                if (value > 0)
                {
                    selectedCell.setNum(value);
                }
            }
        }
        //TODO: MÉTODO RESOLVER SUDOKU
        private void buttSolve_Click(object sender, RoutedEventArgs e)
        {
            //Preparativos
            foreach (Cell c in cellsArray)
            {
                //Si tiene un nº fija la casilla
                if (c.Num > 0)
                {
                    c.Fix();
                }
            }
            //TODO: Comprobar que el Sudoku es válido

            t = new Thread(Solve);
            t.Start();
            //Mientras no lo haya resuelto, intenta llegar a una solución
        }

        void Solve()
        {
            #region Rows
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    selectedCell = cellsArray[i, j];
                    if (selectedCell.Fixed || selectedCell.Solved)
                    {//Una celda fija
                        if (!Descartes.Contains(selectedCell.Num))
                        {
                            Descartes.Add(selectedCell.Num);
                            somethingChanged = true;
                        }
                    }
                    else
                    {//Es una celda no fija
                        if (selectedCell.Num > 0)
                        {//Tiene un nº
                            if (!Descartes.Contains(selectedCell.Num))
                            {
                                Descartes.Add(selectedCell.Num);
                                somethingChanged = true;
                            }
                        }
                        else
                        {//Hay que hallar el nº
                            foreach (int n in Descartes)
                            {//Quita los descartes de la lista de posibilidades
                                if (selectedCell.Possible.Contains(n))
                                {
                                    selectedCell.Possible.Remove(n);
                                    somethingChanged = true;
                                }
                            }
                            if (selectedCell.Possible.Count == 1)
                            {
                                aux = selectedCell.Possible[0];
                                somethingChanged = true;
                            }
                        }
                    }
                    Dispatcher.Invoke(ui);
                }
                if (somethingChanged)
                {//Si algo ha cambiado, vuelve a recorrer la fila
                    i--;
                    somethingChanged = false;
                }
                else
                {
                    Descartes = new HashSet<int>();
                }
            }
            #endregion
            #region Columns
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    selectedCell = cellsArray[j, i];
                    if (selectedCell.Fixed || selectedCell.Solved)
                    {//Una celda fija
                        if (!Descartes.Contains(selectedCell.Num))
                        {
                            Descartes.Add(selectedCell.Num);
                            somethingChanged = true;
                        }
                    }
                    else
                    {//Es una celda no fija
                        if (selectedCell.Num > 0)
                        {//Tiene un nº
                            if (!Descartes.Contains(selectedCell.Num))
                            {
                                Descartes.Add(selectedCell.Num);
                                somethingChanged = true;
                            }
                        }
                        else
                        {//Hay que hallar el nº
                            foreach (int n in Descartes)
                            {//Quita los descartes de la lista de posibilidades
                                if (selectedCell.Possible.Contains(n))
                                {
                                    selectedCell.Possible.Remove(n);
                                    somethingChanged = true;
                                }
                            }
                            if (selectedCell.Possible.Count == 1)
                            {
                                aux = selectedCell.Possible[0];
                                somethingChanged = true;
                            }
                        }
                    }
                    Dispatcher.Invoke(ui);
                }
                if (somethingChanged)
                {//Si algo ha cambiado, vuelve a recorrer la columna
                    i--;
                    somethingChanged = false;
                }
                else
                {
                    Descartes = new HashSet<int>();
                }
            }
            #endregion
            #region Boxes
            foreach (Cell[,] item in arrays)
            {
                Descartes = new HashSet<int>();
                do
                {
                    somethingChanged = false;
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            selectedCell = item[i, j];
                            if (selectedCell.Fixed || selectedCell.Solved)
                            {//Una celda fija
                                if (!Descartes.Contains(selectedCell.Num))
                                {
                                    Descartes.Add(selectedCell.Num);
                                    somethingChanged = true;
                                }
                            }
                            else
                            {//Es una celda no fija
                                if (selectedCell.Num > 0)
                                {//Tiene un nº
                                    if (!Descartes.Contains(selectedCell.Num))
                                    {
                                        Descartes.Add(selectedCell.Num);
                                        somethingChanged = true;
                                    }
                                }
                                else
                                {//Hay que hallar el nº
                                    foreach (int n in Descartes)
                                    {//Quita los descartes de la lista de posibilidades
                                        if (selectedCell.Possible.Contains(n))
                                        {
                                            selectedCell.Possible.Remove(n);
                                            somethingChanged = true;
                                        }
                                    }
                                    if (selectedCell.Possible.Count == 1)
                                    {
                                        aux = selectedCell.Possible[0];
                                        somethingChanged = true;
                                    }
                                }
                            }
                            Dispatcher.Invoke(ui);
                        }
                    }
                } while (somethingChanged);
            }
            #endregion
            //Console.WriteLine(t.IsAlive);





            t.Abort();
        }

        private void buttClean_Click(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine(t.IsAlive);
            foreach (Cell item in cellsArray)
            {
                item.Reset();
            }
            ResetSudoku();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            cell00.Fix(5);
            cell01.Fix(3);
            cell04.Fix(7);
            cell10.Fix(6);
            cell13.Fix(1);
            cell14.Fix(9);
            cell15.Fix(5);
            cell21.Fix(9);
            cell22.Fix(8);
            cell27.Fix(6);
            cell30.Fix(8);
            cell34.Fix(6);
            cell38.Fix(3);
            cell40.Fix(4);
            cell43.Fix(8);
            cell45.Fix(3);
            cell48.Fix(1);
            cell50.Fix(7);
            cell54.Fix(2);
            cell58.Fix(6);
            cell61.Fix(6);
            cell66.Fix(2);
            cell67.Fix(8);
            cell73.Fix(4);
            cell75.Fix(9);
            cell78.Fix(5);
            cell84.Fix(8);
            cell87.Fix(7);
            cell88.Fix(9);
        }

        void ResetSudoku()
        {
            //Save cells in arrays
            #region Recuadro 11
            array11[0, 0] = cell00;
            array11[0, 1] = cell01;
            array11[0, 2] = cell02;
            array11[1, 0] = cell10;
            array11[1, 1] = cell11;
            array11[1, 2] = cell12;
            array11[2, 0] = cell20;
            array11[2, 1] = cell21;
            array11[2, 2] = cell22;
            #endregion
            #region Recuadro 12
            array12[0, 0] = cell03;
            array12[0, 1] = cell04;
            array12[0, 2] = cell05;
            array12[1, 0] = cell13;
            array12[1, 1] = cell14;
            array12[1, 2] = cell15;
            array12[2, 0] = cell23;
            array12[2, 1] = cell24;
            array12[2, 2] = cell25;
            #endregion
            #region Recuadro 13
            array13[0, 0] = cell06;
            array13[0, 1] = cell07;
            array13[0, 2] = cell08;
            array13[1, 0] = cell16;
            array13[1, 1] = cell17;
            array13[1, 2] = cell18;
            array13[2, 0] = cell26;
            array13[2, 1] = cell27;
            array13[2, 2] = cell28;
            #endregion
            #region Recuadro 21
            array21[0, 0] = cell30;
            array21[0, 1] = cell31;
            array21[0, 2] = cell32;
            array21[1, 0] = cell40;
            array21[1, 1] = cell41;
            array21[1, 2] = cell42;
            array21[2, 0] = cell50;
            array21[2, 1] = cell51;
            array21[2, 2] = cell52;
            #endregion
            #region Recuadro 22
            array22[0, 0] = cell33;
            array22[0, 1] = cell34;
            array22[0, 2] = cell35;
            array22[1, 0] = cell43;
            array22[1, 1] = cell44;
            array22[1, 2] = cell45;
            array22[2, 0] = cell53;
            array22[2, 1] = cell54;
            array22[2, 2] = cell55;
            #endregion
            #region Recuadro 23
            array23[0, 0] = cell36;
            array23[0, 1] = cell37;
            array23[0, 2] = cell38;
            array23[1, 0] = cell46;
            array23[1, 1] = cell47;
            array23[1, 2] = cell48;
            array23[2, 0] = cell56;
            array23[2, 1] = cell57;
            array23[2, 2] = cell58;
            #endregion
            #region Recuadro 31
            array31[0, 0] = cell60;
            array31[0, 1] = cell61;
            array31[0, 2] = cell62;
            array31[1, 0] = cell70;
            array31[1, 1] = cell71;
            array31[1, 2] = cell72;
            array31[2, 0] = cell80;
            array31[2, 1] = cell81;
            array31[2, 2] = cell82;
            #endregion
            #region Recuadro 32
            array32[0, 0] = cell63;
            array32[0, 1] = cell64;
            array32[0, 2] = cell65;
            array32[1, 0] = cell73;
            array32[1, 1] = cell74;
            array32[1, 2] = cell75;
            array32[2, 0] = cell83;
            array32[2, 1] = cell84;
            array32[2, 2] = cell85;
            #endregion
            #region Recuadro 33
            array33[0, 0] = cell66;
            array33[0, 1] = cell67;
            array33[0, 2] = cell68;
            array33[1, 0] = cell76;
            array33[1, 1] = cell77;
            array33[1, 2] = cell78;
            array33[2, 0] = cell86;
            array33[2, 1] = cell87;
            array33[2, 2] = cell88;
            #endregion
            //Save every cell
            //cells = new List<Cell>();
            cellsArray = new Cell[9, 9];
            int contCol = 0;
            int contRow = 0;
            foreach (Cell item in g.Children)
            {
                //cells.Add(item);
                //Save in array
                cellsArray[contRow, contCol] = item;
                contCol++;
                if (contCol == 9)
                {
                    contCol = 0;
                    contRow++;
                }
            }
            ui = new UpdateInterface(UpdateCellNumber);
            Descartes = new HashSet<int>();
            arrays = new List<Cell[,]> { array11, array12, array13, array21, array22, array23, array31, array32, array33 };
        }
    }
}
