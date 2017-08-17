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

        public static Cell selectedCell;//To keep track of the cell we're in
        public static bool isSudokuSolved;//To know when the problem is solved
        HashSet<int> discards;//To keep track of the nº already on use inside the Row/Col/Box

        Thread t;//Thread where we solve the sudoku
        delegate void UpdateInterface();//Delegate to Keep the User informed of what's happening
        delegate void DiscardNumbers();
        delegate void CheckBoxesDel();
        UpdateInterface ui;
        DiscardNumbers dn;
        CheckBoxesDel cb;
        int aux;//Auxiliar nº to update cells from Thread
        bool somethingChanged;//To know when the program can't find any more discards

        List<AuxObj> auxObjList;//Auxiliar objects to find when a nº can only be placed in one cell in the Row/Col/Box
        int contChanges;//To know when the program can't find anything else

        public static bool working;//To keep the user from changing something while the program is finding a solution
        //TODO: Set working = false at the end
        /*************************************************************************************************************************************************/
        public MainWindow()
        {
            InitializeComponent();
            ui = new UpdateInterface(UpdateCell);
            dn = new DiscardNumbers(Discard);
            cb = new CheckBoxesDel(CheckBoxes);
            //Save all the cells in the list
            foreach (Box b in BoxContainer.Children)
            {
                foreach (Cell c in b.cells)
                {
                    cells.Add(c);
                }
            }
            ResetSudoku();
        }


        void ResetSudoku()
        {
            discards = new HashSet<int>();
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
        void UpdateCell()
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
            selectedCell.Select();
            selectedCell.SelectionChanged();
        }

        void CheckBoxes()
        {
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
        }
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
                //Discard();
                Dispatcher.Invoke(dn);
                #region CheckPossibles
                //if (contChanges == 0)
                //{

                //If the program can't keep discarding nºm
                //it starts to look if there's a nº in 1 Row/Col/Box that can only be placed in that Cell
                #region CheckBoxes
                Dispatcher.Invoke(cb);

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
                    working = false;
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
                        cont = cont - 9;
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
            Box11.cell11.writeNum(5);
            Box11.cell12.writeNum(3);
            Box11.cell21.writeNum(6);
            Box11.cell23.writeNum(9);
            Box11.cell33.writeNum(8);
            Box12.cell21.writeNum(7);
            Box12.cell12.writeNum(1);
            Box12.cell22.writeNum(9);
            Box12.cell23.writeNum(5);
            Box13.cell23.writeNum(6);
            Box21.cell11.writeNum(8);
            Box21.cell21.writeNum(4);
            Box21.cell31.writeNum(7);
            Box22.cell12.writeNum(6);
            Box22.cell21.writeNum(8);
            Box22.cell23.writeNum(3);
            Box22.cell32.writeNum(2);
            Box23.cell13.writeNum(3);
            Box23.cell23.writeNum(1);
            Box23.cell33.writeNum(6);
            Box31.cell12.writeNum(6);
            Box32.cell21.writeNum(4);
            Box32.cell23.writeNum(9);
            Box32.cell32.writeNum(8);
            Box33.cell11.writeNum(2);
            Box33.cell12.writeNum(8);
            Box33.cell23.writeNum(5);
            Box33.cell32.writeNum(7);
            Box33.cell33.writeNum(9);
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
                    //Discard();
                    Dispatcher.Invoke(dn);
                }
            }
            auxObjList = new List<AuxObj>();
        }

        private void showMe_Click(object sender, RoutedEventArgs e)
        {
            foreach (Cell item in cells)
            {
                if (item.Num > 0)
                {
                    item.Fix();
                }
            }
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
