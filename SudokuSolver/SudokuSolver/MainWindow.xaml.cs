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
        public static List<Cell> cells = new List<Cell>();//List containing all the cells

        //public static Cell[,] cellsArray;//Array containing all the cells in position
        //Cells grouped by position
        //public static Cell[,] array11 = new Cell[3, 3];
        //public static Cell[,] array12 = new Cell[3, 3];
        //public static Cell[,] array13 = new Cell[3, 3];
        //public static Cell[,] array21 = new Cell[3, 3];
        //public static Cell[,] array22 = new Cell[3, 3];
        //public static Cell[,] array23 = new Cell[3, 3];
        //public static Cell[,] array31 = new Cell[3, 3];
        //public static Cell[,] array32 = new Cell[3, 3];
        //public static Cell[,] array33 = new Cell[3, 3];
        //public static List<Cell[,]> arrays;

        public static Cell selectedCell;//To keep track of the cell we're in
        public static bool isSudokuSolved;//To know when the problem is solved
        HashSet<int> discards;//To keep track of the nº already on use inside the Row/Col/Box

        Thread t;//Thread where we solve the sudoku
        delegate void UpdateInterface();//Delegate to Keep the User informed of what's happening
        UpdateInterface ui;
        int aux;//Auxiliar nº to update cells from Thread
        bool somethingChanged;//To know when the program can't find any more discards

        List<AuxObj> auxObjList;//Auxiliar objects to find when a nº can only be placed in one cell in the Row/Col/Box
        int contChanges;//To know when the program can't find anything else

        //delegate void InterfaceDebug();
        //InterfaceDebug id;

        public static bool working;//To keep the user from changing something while the program is finding a solution

        /*************************************************************************************************************************************************/
        public MainWindow()
        {
            InitializeComponent();
            ui = new UpdateInterface(UpdateCellNumber);
            ResetSudoku();
        }


        void ResetSudoku()
        {
            //Save cells in arrays
            #region Recuadro 11
            /*array11[0, 0] = cell00;
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
            array33[2, 2] = cell88;*/
            #endregion
            //Save every cell
            //cellsArray = new Cell[9, 9];
            /*int contCol = 0;
            int contRow = 0;
            foreach (Cell item in g.Children)
            {
                //Save in array
                cellsArray[contRow, contCol] = item;
                contCol++;
                if (contCol == 9)
                {
                    contCol = 0;
                    contRow++;
                }
            }*/
            //id = new InterfaceDebug(InterfaceDebugger);
            //cells = new List<Cell>();
            foreach (Box b in BoxContainer.Children)
            {
                foreach (Cell c in b.cells)
                {
                    cells.Add(c);
                }
            }
            discards = new HashSet<int>();
            //arrays = new List<Cell[,]> { array11, array12, array13, array21, array22, array23, array31, array32, array33 };
            selectedCell = null;
            isSudokuSolved = false;
            discards = new HashSet<int>();
            aux = -1;
            somethingChanged = false;
            auxObjList = new List<AuxObj>();
            contChanges = -1;
            working = false;
        }


        #region Delegates
        void UpdateCellNumber()
        {
            if (aux > 0)
            {//Si hay que actualizar el nº de la celda
                selectedCell.setNum(aux);
                aux = -1;
            }
            else if (!selectedCell.Fixed && !selectedCell.Solved)
            {
                selectedCell.ShowPossibles();
            }
            //Marca como seleccionada   -> ?????
            //Unselect();
            //selectedCell.selected = true;
            //selectedCell.SelectionChanged();
        }
        
        //void InterfaceDebugger()
        //{
        //    if (selectedCell.Opacity == 1)
        //    {
        //        selectedCell.Opacity = 0.5;
        //    }
        //    else
        //    {
        //        selectedCell.Opacity = 1;
        //    }
        //}
#endregion

        //public static void Unselect()
        //{
        //    foreach (Cell c in cellsArray)
        //    {
        //        if (!c.Fixed && !c.Solved)
        //        {
        //            c.selected = false;
        //            c.SelectionChanged();
        //        }
        //    }
        //}

        //KeyDown Event (Only: Esc, Supr, and Number keys)
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)//Esc closes the app
            {
                Application.Current.Shutdown();
            }
            else if (e.Key == Key.Delete)//Supr clears the selected cell
            {
                selectedCell.FullReset();
            }
            else if (!selectedCell.Fixed && selectedCell != null)//Sets the new number
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
                    selectedCell.writeNum(value);
                }
            }
        }//TODO: Clear everything but values

        //TODO: MÉTODO RESOLVER SUDOKU
        private void buttSolve_Click(object sender, RoutedEventArgs e)
        {
            //Preparations
            ResetSudoku();
            foreach (Cell c in cells)
            {
                c.Reset();
                //If the cell has a nº, fix it
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
            working = true;
            bool solved = false;
            while (!solved)//TODO: Check method to know when it's solved
            {
                Discard();
                #region CheckPossibles
                //if (contChanges == 0)
                //{

                //If the program can't keep discarding nºm
                //it starts to look if there's a nº in 1 Row/Col/Box that can only be placed in that Cell
                #region CheckBoxes
                //TODO: Refactoriza esto, pls
                foreach (Box b in BoxContainer.Children)
                {
                    for (int c = 0; c < 3; c++)
                    {//For each Row inside that Box
                        for (int d = 0; d < 3; d++)
                        {//For each Col in that Row
                            LoadAuxObjList(b.cells[c, d]);
                        }
                    }
                }
                CheckAuxObjList();

                //foreach (Cell[,] item in arrays)
                //{
                //    //auxObjList = new List<AuxObj>();
                //    for (int i = 0; i < 3; i++)
                //    {
                //        for (int j = 0; j < 3; j++)
                //        {
                //            LoadAuxObjList(cellsArray[i, j]);
                //        }
                //    }
                //    CheckAuxObjList();
                //}
                #endregion
                #region CheckRows
                int cont = 0;//Since now I don't have an array with all the cells, I have to do something different
                for (int i = 0; i < 9; i++)
                    {
                    for (int j = 0; j < 9; j++)
                    {
                        LoadAuxObjList(cells[cont]);
                        cont++;
                    }
                    CheckAuxObjList();
                }
                #endregion
                #region CheckCols
                //Since now I don't have an array with all the cells, I have to do something different
                int index;
                for (int col = 0; col < 9; col++)//I want to go trough the 9 Cols
                {
                    //I need to know the index of the first Cell, that will be col
                    index = col;
                    for (int row = 0; row < 9; row++)//And I want 9 Cells from each Col
                    {
                        LoadAuxObjList(cells[index]);
                        //The next Cell in the Col will be index+9
                        index = index + 9;
                    }
                    CheckAuxObjList();
                }
                #endregion
                //}
                #endregion

                var result = MessageBox.Show("Keep finding solution?", "", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No)
                {
                    solved = true;
                }
            }
        }

        void Discard()
        {//Search every cell trying to discard as many nº as it can -> Order: Box, Row, Col
            while (contChanges != 0)
            {
                contChanges = 0;//To know when the program can't discard any more numbers
                #region Boxes
                foreach (Box b in BoxContainer.Children)//Search in every Box
                {//TODO: SolveError: System.InvalidOperationException: 'El subproceso que realiza la llamada no puede obtener acceso a este objeto porque el propietario es otro subproceso.'
                    //DO with Delegate
                    discards = new HashSet<int>();//To know what nº are already being used in the box
                    do
                    {
                        somethingChanged = false;
                        for (int row = 0; row < 3; row++)
                        {
                            for (int col = 0; col < 3; col++)
                            {
                                selectedCell = b.cells[row, col];//Select a cell from the box
                                Dispatcher.Invoke(ui);//Show cell as selected
                                if (selectedCell.Fixed || selectedCell.Solved)
                                {//It's a fixed/solved cell
                                    if (!discards.Contains(selectedCell.Num))
                                    {
                                        discards.Add(selectedCell.Num);//Adds the nº on use to the discards list
                                        somethingChanged = true;
                                    }
                                }
                                else
                                {//It's a free cell
                                    foreach (int n in discards)
                                    {//Removes discards from Possible
                                        if (selectedCell.Possible.Contains(n))
                                        {
                                            selectedCell.Possible.Remove(n);
                                            somethingChanged = true;
                                            contChanges++;
                                        }
                                    }
                                    if (selectedCell.Possible.Count == 1)//If there's only 1 nº in Possibles, write it in the cell
                                    {
                                        aux = selectedCell.Possible.First();//Save that nº in aux, and the delegate will update it
                                        somethingChanged = true;
                                        contChanges++;
                                        Dispatcher.Invoke(ui);
                                    }
                                }
                            }
                        }
                    } while (somethingChanged);//Keep going while it finds more changes
                }
                #endregion
                discards = new HashSet<int>();
                #region Rows
                int cont = 0;//Since now I don't have an array with all the cells, I have to do something different
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        CheckCell(cells[cont]);
                        cont++;
                    }
                    if (somethingChanged)
                    {//Si algo ha cambiado, vuelve a recorrer la fila
                        i--;
                        somethingChanged = false;
                    }
                    else
                    {
                        discards = new HashSet<int>();
                        auxObjList = new List<AuxObj>();//TODO: Review need(?)
                    }
                }
                #endregion
                #region Cols
                //Since now I don't have an array with all the cells, I have to do something different
                int index;
                for (int col = 0; col < 9; col++)//I want to go trough the 9 Cols
                {
                    //I need to know the index of the first Cell, that will be col
                    index = col;
                    for (int row = 0; row < 9; row++)//And I want 9 Cells from each Col
                    {
                        CheckCell(cells[index]);
                        //The next Cell in the Col will be index+9
                        index = index + 9;
                    }
                    if (somethingChanged)
                    {//Si algo ha cambiado, vuelve a recorrer la columna
                        col--;
                        somethingChanged = false;
                    }
                    else
                    {
                        discards = new HashSet<int>();
                        auxObjList = new List<AuxObj>();//TODO: Review need(?)
                    }
                }
                #endregion
            }
            
        }



        private void buttClean_Click(object sender, RoutedEventArgs e)
        {
            //ResetSudoku();
            //foreach (Cell item in cellsArray)
            //{
            //    item.Reset();
            //}
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {//Fills with a Sudoku example
            //cell00.Fix(5);
            //cell01.Fix(3);
            //cell04.Fix(7);
            //cell10.Fix(6);
            //cell13.Fix(1);
            //cell14.Fix(9);
            //cell15.Fix(5);
            //cell21.Fix(9);
            //cell22.Fix(8);
            //cell27.Fix(6);
            //cell30.Fix(8);
            //cell34.Fix(6);
            //cell38.Fix(3);
            //cell40.Fix(4);
            //cell43.Fix(8);
            //cell45.Fix(3);
            //cell48.Fix(1);
            //cell50.Fix(7);
            //cell54.Fix(2);
            //cell58.Fix(6);
            //cell61.Fix(6);
            //cell66.Fix(2);
            //cell67.Fix(8);
            //cell73.Fix(4);
            //cell75.Fix(9);
            //cell78.Fix(5);
            //cell84.Fix(8);
            //cell87.Fix(7);
            //cell88.Fix(9);
        }

        void CheckCell(Cell c)
        {
            selectedCell = c;
            Dispatcher.Invoke(ui);//Show cell as selected
            if (selectedCell.Fixed || selectedCell.Solved)
            {//A Cell that is fixed or solved
                if (!discards.Contains(selectedCell.Num))//TODO: Move this inside a method, used in more than one place
                {
                    discards.Add(selectedCell.Num);
                    somethingChanged = true;
                }
            }
            else
            {//It's a free Cell
                foreach (int n in discards)
                {//Removes discards from Possible
                    if (selectedCell.Possible.Contains(n))
                    {
                        selectedCell.Possible.Remove(n);
                        somethingChanged = true;
                        contChanges++;
                    }
                }
                if (selectedCell.Possible.Count == 1)
                {
                    aux = selectedCell.Possible.First();
                    somethingChanged = true;
                    contChanges++;
                    Dispatcher.Invoke(ui);
                }
                
            }
        }

        void LoadAuxObjList(Cell c)//Insert in the AuxObjList the Possible numbers of this cell
        {
            selectedCell = c;
            Dispatcher.Invoke(ui);
            if (!selectedCell.Fixed && !selectedCell.Solved)
            {
                foreach (int n in selectedCell.Possible)
                {
                    if (auxObjList.Where(z => z.Num == n).Count() == 0)
                    {//If that nº isn't on the List, create
                        auxObjList.Add(new AuxObj(n, selectedCell));
                        somethingChanged = true;
                        contChanges++;
                    }
                    else
                    {//If it exists, +1Rep
                        auxObjList.Single(z => z.Num == n).Reps++;
                    }
                }
            }
            //Dispatcher.Invoke(ui);
        }

        void CheckAuxObjList()
        {//When it finishes a Row/Col/Box
            foreach (AuxObj aobj in auxObjList)
            {
                if (aobj.Reps == 1)
                {//It means that aobj.Num can only be placed in aobj.Cell
                    aux = aobj.Num;
                    selectedCell = aobj.Cell;
                    //somethingChanged = true;
                    Dispatcher.Invoke(ui);
                    //When a new number is discovered, it should see what can be discarded
                    Discard();
                }
            }
            auxObjList = new List<AuxObj>();
        }

        private void showMe_Click(object sender, RoutedEventArgs e)
        {
            //foreach (Cell cell in cellsArray)
            //{
            //    Console.WriteLine("");
            //    foreach (int n in cell.Possible)
            //    {
            //        Console.Write(n+" ");
            //    }
            //}
        }

        //Debo ir comprobando las listas de Posibles
        //En caso de que un nº aparezca una única vez como posible en toda la Row/Col/Box
        //SetNum
    }
}
