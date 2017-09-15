using SudokuSolver.Objects;
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
        #region Declarations
        public static MainWindow mw;
        //public static List<Cell> cells;//List containing all the cells
        public static Cell[,] cellsArray;//Array containing all the cells in position
        public static Cell[,] auxArray;//Array to save state of the Sudoku before trying random numbers

        public static Cell selectedCell = null;
        public static bool solved = false;
        HashSet<int> Descartes;
        List<int> UsedNumbers;//Use this in place of Descartes(?)

        Thread t;
        delegate void UpdateInterface();
        UpdateInterface ui;
        //int aux = -1;
        bool somethingChanged = false;

        List<AuxObj> auxObjList = new List<AuxObj>();
        int contChanges = -1;
        
        delegate void InterfaceDebug();
        InterfaceDebug id;

        //delegate void DiscardDelegate();
        //DiscardDelegate dd;
        delegate void CheckBoxesDel();
        CheckBoxesDel cb;
        delegate void SelectChanged();
        SelectChanged sc;

        List<Box> boxes = new List<Box>();

        bool error = false;//Bool to know if the Sudoku gave an error (Has no solution)
        //public static List<State> states;
        //State prevState = null;
        //bool refreshing = false;
        int tries = 3;
        List<Cell> Group;//It can be a Row/Col/Box

        int errortab = 0;
        #endregion
        /*************************************************************************************************************************************************/
        public MainWindow()
        {
            InitializeComponent();
            #region Initializations
            //Save every cell
            cellsArray = new Cell[9, 9];
            auxArray = new Cell[9, 9];
            //TODO: Find another way to do this
            #region FillCellsArray
            //Box00
            cellsArray[0, 0] = Sudoku.box00.array[0, 0];
            cellsArray[0, 1] = Sudoku.box00.array[0, 1];
            cellsArray[0, 2] = Sudoku.box00.array[0, 2];
            cellsArray[1, 0] = Sudoku.box00.array[1, 0];
            cellsArray[1, 1] = Sudoku.box00.array[1, 1];
            cellsArray[1, 2] = Sudoku.box00.array[1, 2];
            cellsArray[2, 0] = Sudoku.box00.array[2, 0];
            cellsArray[2, 1] = Sudoku.box00.array[2, 1];
            cellsArray[2, 2] = Sudoku.box00.array[2, 2];
            //Box01
            cellsArray[0, 3] = Sudoku.box01.array[0, 0];
            cellsArray[0, 4] = Sudoku.box01.array[0, 1];
            cellsArray[0, 5] = Sudoku.box01.array[0, 2];
            cellsArray[1, 3] = Sudoku.box01.array[1, 0];
            cellsArray[1, 4] = Sudoku.box01.array[1, 1];
            cellsArray[1, 5] = Sudoku.box01.array[1, 2];
            cellsArray[2, 3] = Sudoku.box01.array[2, 0];
            cellsArray[2, 4] = Sudoku.box01.array[2, 1];
            cellsArray[2, 5] = Sudoku.box01.array[2, 2];
            //Box02
            cellsArray[0, 6] = Sudoku.box02.array[0, 0];
            cellsArray[0, 7] = Sudoku.box02.array[0, 1];
            cellsArray[0, 8] = Sudoku.box02.array[0, 2];
            cellsArray[1, 6] = Sudoku.box02.array[1, 0];
            cellsArray[1, 7] = Sudoku.box02.array[1, 1];
            cellsArray[1, 8] = Sudoku.box02.array[1, 2];
            cellsArray[2, 6] = Sudoku.box02.array[2, 0];
            cellsArray[2, 7] = Sudoku.box02.array[2, 1];
            cellsArray[2, 8] = Sudoku.box02.array[2, 2];

            //Box10
            cellsArray[3, 0] = Sudoku.box10.array[0, 0];
            cellsArray[3, 1] = Sudoku.box10.array[0, 1];
            cellsArray[3, 2] = Sudoku.box10.array[0, 2];
            cellsArray[4, 0] = Sudoku.box10.array[1, 0];
            cellsArray[4, 1] = Sudoku.box10.array[1, 1];
            cellsArray[4, 2] = Sudoku.box10.array[1, 2];
            cellsArray[5, 0] = Sudoku.box10.array[2, 0];
            cellsArray[5, 1] = Sudoku.box10.array[2, 1];
            cellsArray[5, 2] = Sudoku.box10.array[2, 2];
            //Box11
            cellsArray[3, 3] = Sudoku.box11.array[0, 0];
            cellsArray[3, 4] = Sudoku.box11.array[0, 1];
            cellsArray[3, 5] = Sudoku.box11.array[0, 2];
            cellsArray[4, 3] = Sudoku.box11.array[1, 0];
            cellsArray[4, 4] = Sudoku.box11.array[1, 1];
            cellsArray[4, 5] = Sudoku.box11.array[1, 2];
            cellsArray[5, 3] = Sudoku.box11.array[2, 0];
            cellsArray[5, 4] = Sudoku.box11.array[2, 1];
            cellsArray[5, 5] = Sudoku.box11.array[2, 2];
            //Box12
            cellsArray[3, 6] = Sudoku.box12.array[0, 0];
            cellsArray[3, 7] = Sudoku.box12.array[0, 1];
            cellsArray[3, 8] = Sudoku.box12.array[0, 2];
            cellsArray[4, 6] = Sudoku.box12.array[1, 0];
            cellsArray[4, 7] = Sudoku.box12.array[1, 1];
            cellsArray[4, 8] = Sudoku.box12.array[1, 2];
            cellsArray[5, 6] = Sudoku.box12.array[2, 0];
            cellsArray[5, 7] = Sudoku.box12.array[2, 1];
            cellsArray[5, 8] = Sudoku.box12.array[2, 2];

            //Box20
            cellsArray[6, 0] = Sudoku.box20.array[0, 0];
            cellsArray[6, 1] = Sudoku.box20.array[0, 1];
            cellsArray[6, 2] = Sudoku.box20.array[0, 2];
            cellsArray[7, 0] = Sudoku.box20.array[1, 0];
            cellsArray[7, 1] = Sudoku.box20.array[1, 1];
            cellsArray[7, 2] = Sudoku.box20.array[1, 2];
            cellsArray[8, 0] = Sudoku.box20.array[2, 0];
            cellsArray[8, 1] = Sudoku.box20.array[2, 1];
            cellsArray[8, 2] = Sudoku.box20.array[2, 2];
            //Box21
            cellsArray[6, 3] = Sudoku.box21.array[0, 0];
            cellsArray[6, 4] = Sudoku.box21.array[0, 1];
            cellsArray[6, 5] = Sudoku.box21.array[0, 2];
            cellsArray[7, 3] = Sudoku.box21.array[1, 0];
            cellsArray[7, 4] = Sudoku.box21.array[1, 1];
            cellsArray[7, 5] = Sudoku.box21.array[1, 2];
            cellsArray[8, 3] = Sudoku.box21.array[2, 0];
            cellsArray[8, 4] = Sudoku.box21.array[2, 1];
            cellsArray[8, 5] = Sudoku.box21.array[2, 2];
            //Box22
            cellsArray[6, 6] = Sudoku.box22.array[0, 0];
            cellsArray[6, 7] = Sudoku.box22.array[0, 1];
            cellsArray[6, 8] = Sudoku.box22.array[0, 2];
            cellsArray[7, 6] = Sudoku.box22.array[1, 0];
            cellsArray[7, 7] = Sudoku.box22.array[1, 1];
            cellsArray[7, 8] = Sudoku.box22.array[1, 2];
            cellsArray[8, 6] = Sudoku.box22.array[2, 0];
            cellsArray[8, 7] = Sudoku.box22.array[2, 1];
            cellsArray[8, 8] = Sudoku.box22.array[2, 2];
            #endregion
            ui = new UpdateInterface(UpdateCellNumber);
            id = new InterfaceDebug(InterfaceDebugger);
            //dd = new DiscardDelegate(Descartar);
            cb = new CheckBoxesDel(CheckBoxes);
            sc = new SelectChanged(SelChanged);
            foreach (Box box in Sudoku.SudokuGrid.Children)
            {
                boxes.Add(box);
            }
            stateComboBox.DisplayMemberPath = "Name";
            ResetSudoku();
            mw = this;
            #endregion
        }
#region Delegates
        void UpdateCellNumber()
        {
            if (selectedCell.Possible.Count == 1)
            {//Si hay que actualizar el nº de la celda
                selectedCell.setNum(selectedCell.Possible[0]);
                selectedCell.Solved = true;
                contChanges++;//TODO: revisar que no de error
                //aux = -1;
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
        
        void InterfaceDebugger()
        {
            if (selectedCell.Opacity == 1)
            {
                selectedCell.Opacity = 0.5;
            }
            else
            {
                selectedCell.Opacity = 1;
            }
        }

        void CheckBoxes()
        {
            foreach (Box box in Sudoku.SudokuGrid.Children)
            {
                if (error)
                    break;
                foreach (Cell cell in box.array)
                {
                    LoadAuxObjList(cell, null);
                }
                CheckAuxObjList();
            }
        }

        void SelChanged()
        {
            foreach (Cell cell in cellsArray)
            {
                cell.SelectionChanged();
            }
        }
        #endregion
#region
        void ResetSudoku()
        {
            Descartes = new HashSet<int>();
            error = false;
            //states = new List<State>();
        }

        public static void Unselect()
        {
            foreach (Cell c in cellsArray)
            {
                c.selected = false;
                c.SelectionChanged();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {//Sólo se pueden pulsar números
            if (selectedCell != null)
            {
                if (e.Key == Key.Escape)
                {
                    Application.Current.Shutdown();
                }
                else if (e.Key == Key.Delete)
                {
                    selectedCell.Reset();
                }
                else if (e.Key == Key.Enter)
                {
                    Solve();
                }
                else if (!selectedCell.Fixed && !selectedCell.Solved)
                {
                    int keyVal = (int)e.Key;
                    int value = -1;
                    if (keyVal >= (int)Key.D0 && keyVal <= (int)Key.D9)
                    {//Si pulsa un nº en el teclado superior, lo borra como posibilidad
                        value = (int)e.Key - (int)Key.D0;
                        if (selectedCell.Possible.Contains(value))
                            selectedCell.Possible.Remove(value);
                        else
                            selectedCell.Possible.Add(value);
                        //Solve();
                    }
                    else if (keyVal >= (int)Key.NumPad0 && keyVal <= (int)Key.NumPad9)
                    {//Si pulsa un nº en el teclado numérico, lo settea
                        value = (int)e.Key - (int)Key.NumPad0;
                        selectedCell.writeNum(value);
                    }
                }
            }
        }
#endregion
        private void buttSolve_Click(object sender, RoutedEventArgs e)
        {
            CheckSudokuError();
            if (!error)
            {
                //Preparativos
                foreach (Cell c in cellsArray)
                {
                    //Si tiene un nº fija la casilla
                    if (c.Num > 0)
                    {
                        c.Fix();
                    }
                    else
                    {
                        //c.Possible = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                    }
                }
                t = new Thread(Solve);
                t.Start();
                //Mientras no lo haya resuelto, intenta llegar a una solución
            }
            else
            {
                MessageBox.Show("El Sudoku es erróneo");
            }
        }



        /******************************************************************************************************/
        //                                          MAIN METHODS                                              //
        /******************************************************************************************************/
        void Solve()
        {//THE METHOD THAT SOLVES THE SUDOKU
            for (int i = 0; i < errortab; i++)
            {
                Console.Write("    ");
            }
            Console.WriteLine("Entrando en Solve()");
            errortab++;
            tries = 3;
            bool solved = false;
            while (!solved && !error)
            {
                Discard();
                #region CheckPossibles
                //if (contChanges == 0)
                //{
                //Si después de todo lo anterior, el programa ya no es capaz de seguir descartando posibilidades,
                //busca si en alguna Row/Col/Box hay algún nº que sólo es posible colocarlo en una celda concreta
                //CheckBoxes
                Dispatcher.Invoke(cb);
                //CheckRows
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        LoadAuxObjList(cellsArray[i, j], null);
                    }
                    CheckAuxObjList();
                }
                //CheckCols
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        LoadAuxObjList(cellsArray[j, i], null);
                    }
                    CheckAuxObjList();
                }
                //}
                #endregion
                CheckCase3();
                //Comprobación
                int unsolvedCells = 81;
                foreach (Cell cell in cellsArray)
                {
                    if (cell.Solved || cell.Fixed)
                    {
                        unsolvedCells--;
                    }
                }
                if (unsolvedCells > 0)
                {
                    tries--;
                    if (tries == 0)
                        //solved = true;//TODO: Don't call that "solved"
                        break;
                    //var result = MessageBox.Show("Keep finding solution?", "" + unsolvedCells + " unsolved cells", MessageBoxButton.YesNo);
                    //if (result == MessageBoxResult.No)
                    //{
                    //    solved = true;
                    //}
                }
                else
                {
                    MessageBox.Show("Solved");
                    solved = true;
                }

            }
            errortab--;
            for (int i = 0; i < errortab; i++)
            {
                Console.Write("    ");
            }
            Console.WriteLine("Saliendo de Solve()");
        }

        void Discard()
        {//Intenta descartar nº hasta que sólo quede uno en cada celda -> Orden: Box, Row, Col
            for (int i = 0; i < errortab; i++)
            {
                Console.Write("    ");
            }
            Console.WriteLine("Entrando en Discard()");
            errortab++;
            while (contChanges != 0 && !error)
            {
                contChanges = 0;//Para saber cuando el programa ya no es capaz de descartar más nº posibles
                #region Boxes
                foreach (Box box in boxes)
                {
                    if (error)
                        break;
                    //Descartes = new HashSet<int>();
                    Group = new List<Cell>();//Fill the group with the cells we want
                    foreach (Cell cell in box.array)
                    {
                        Group.Add(cell);
                    }
                    CheckGroup();
                }
                #endregion
                #region Rows
                for (int i = 0; i < 9; i++)
                {
                    if (error)
                        break;
                    Group = new List<Cell>();//Fill the group with the cells we want
                    for (int j = 0; j < 9; j++)
                    {
                        Group.Add(cellsArray[i, j]);
                    }
                    CheckGroup();
                }
                #endregion
                #region Cols
                for (int j = 0; j < 9; j++)
                {
                    if (error)
                        break;
                    Group = new List<Cell>();//Fill the group with the cells we want
                    for (int i = 0; i < 9; i++)
                    {
                        Group.Add(cellsArray[i, j]);
                    }
                    CheckGroup();
                }
                #endregion
            }
            errortab--;
            for (int i = 0; i < errortab; i++)
            {
                Console.Write("    ");
            }
            Console.WriteLine("Saliendo de Discard()");
        }

        void CheckGroup()
        {
            for (int i = 0; i < errortab; i++)
            {
                Console.Write("    ");
            }
            Console.WriteLine("Entrando en CheckGroup()");
            errortab++;
            Descartes = new HashSet<int>();
            do
            {
                somethingChanged = false;
                foreach (Cell cell in Group)//For each cell
                {
                    CheckCell(cell);
                    if (error)
                        break;
                }
            } while (somethingChanged && !error);
            errortab--;
            for (int i = 0; i < errortab; i++)
            {
                Console.Write("    ");
            }
            Console.WriteLine("Saliendo de CheckGroup()");
        }

        void CheckCell(Cell c)
        {
            for (int i = 0; i < errortab; i++)
            {
                Console.Write("    ");
            }
            Console.WriteLine("Entrando en CheckCell()");
            errortab++;
            selectedCell = c;
            //Dispatcher.Invoke(ui);

            /***********************************************************/
            if (selectedCell.Num > 0 && !Descartes.Contains(selectedCell.Num))
            {//If the cell has a nº (Fixed or Solved) and it isn't in Descartes, add it
                Descartes.Add(selectedCell.Num);
                somethingChanged = true;
            }
            else
            {//The number has to be found
                foreach (int n in Descartes)
                {//Quita los descartes de la lista de posibilidades
                    if (selectedCell.Possible.Contains(n))
                    {
                        selectedCell.Possible.Remove(n);
                        somethingChanged = true;
                        contChanges++;
                    }
                }
                //if (selectedCell.Possible.Count == 1)
                //{
                //    aux = selectedCell.Possible[0];
                //    somethingChanged = true;
                //    contChanges++;
                //}
            }
            /***********************************************************/

            /*
            if (selectedCell.Fixed || selectedCell.Solved)
            {//Una celda fija o que ya tiene un nº
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
                            contChanges++;
                        }
                    }
                    if (selectedCell.Possible.Count == 1)
                    {
                        aux = selectedCell.Possible[0];
                        somethingChanged = true;
                        contChanges++;
                    }
                }
            }*/
            Dispatcher.Invoke(ui);
            errortab--;
            for (int i = 0; i < errortab; i++)
            {
                Console.Write("    ");
            }
            Console.WriteLine("Saliendo de CheckCell()");
        }

        void LoadAuxObjList(Cell c, Cell[] subgr)
        {
            for (int i = 0; i < errortab; i++)
            {
                Console.Write("    ");
            }
            Console.WriteLine("Entrando en LoadAuxObjList(Cell, Cell[])");
            errortab++;
            selectedCell = c;
            //Dispatcher.Invoke(id);
            if (!selectedCell.Fixed && !selectedCell.Solved)
            {
                foreach (int n in selectedCell.Possible)
                {
                    if (auxObjList.Where(z => z.Num == n).Count() == 0)
                    {//Si no existe ningún objeto en la lista con este nº, lo crea
                        if (subgr == null)//Si no le paso un subgrupo, significa que viene de CheckPossibles
                            auxObjList.Add(new AuxObj(n, selectedCell));//Guarda en el objeto la celda
                        else//Significa que viene de CheckCase3
                            auxObjList.Add(new AuxObj(n, subgr));//Guarda en el objeto el subgrupo
                        somethingChanged = true;
                        contChanges++;
                    }
                    else
                    {//Si ya existe, +1Rep
                        if (subgr == null || auxObjList.Single(z => z.Num == n).Subgr != subgr)
                            auxObjList.Single(z => z.Num == n).Reps++;
                        //Esto es una simplificación de:
                        /*
                        if (subgr == null)
                            auxObjList.Single(z => z.Num == n).Reps++;
                        else
                        //Un nº puede aparecer varias veces en el mismo subgr, sólo me interesa saber si aparece en varios subgr
                            if (auxObjList.Single(z => z.Num == n).Subgr != subgr)
                                auxObjList.Single(z => z.Num == n).Reps++;
                        */
                    }
                }
            }
            //Dispatcher.Invoke(id);
            Dispatcher.Invoke(ui);
            errortab--;
            for (int i = 0; i < errortab; i++)
            {
                Console.Write("    ");
            }
            Console.WriteLine("Saliendo de LoadAuxObjList(Cell, Cell[])");
        }
        //Este método se usa en CheckCase3
        void LoadAuxObjList(Cell[] subgr)
        {
            for (int i = 0; i < errortab; i++)
            {
                Console.Write("    ");
            }
            Console.WriteLine("Entrando en LoadAuxObjList(Cell[])");
            errortab++;
            foreach (Cell cell in subgr)
            {
                LoadAuxObjList(cell, subgr);
            }
            errortab--;
            for (int i = 0; i < errortab; i++)
            {
                Console.Write("    ");
            }
            Console.WriteLine("Saliendo de LoadAuxObjList(Cell[])");
        }

        void CheckAuxObjList()
        {//Cuando acaba de recorrer una Row/Col/Box
            for (int i = 0; i < errortab; i++)
            {
                Console.Write("    ");
            }
            Console.WriteLine("Entrando en CheckAuxObjList()");
            errortab++;
            foreach (AuxObj aobj in auxObjList)
            {
                if (error)
                    break;
                if (aobj.Reps == 1 && aobj.Cell != null)
                {//Significa que aobj.Num sólo puede ir en aobj.Cell
                    selectedCell = aobj.Cell;
                    foreach (int n in selectedCell.Possible)
                    {
                        if (n != aobj.Num)
                            selectedCell.Possible.Remove(n);
                    }
                    //somethingChanged = true;
                    Dispatcher.Invoke(ui);
                    //Cuando descubre algún número, debería mirar qué cosas puede descartar
                    Discard();
                }
            }
            auxObjList = new List<AuxObj>();
            errortab--;
            for (int i = 0; i < errortab; i++)
            {
                Console.Write("    ");
            }
            Console.WriteLine("Saliendo de CheckAuxObjList()");
        }

        public void CheckSudokuError()
        {//This method will Check if the Sudoku is correct
            for (int i = 0; i < errortab; i++)
            {
                Console.Write("    ");
            }
            Console.WriteLine("Entrando en CheckSudokuError()");
            errortab++;
            error = false;
            foreach (Cell cell in cellsArray)
            {
                cell.Error = false;
            }
            //Check for duplicates
            #region Boxes
            foreach (Box box in boxes)
            {
                //UsedNumbers = new List<int>();
                Group = new List<Cell>();//Fill the group with the cells we want
                foreach (Cell cell in box.array)
                {
                    Group.Add(cell);
                }
                CheckGroupError();
            }
            #endregion
            #region Rows
            for (int i = 0; i < 9; i++)
            {
                Group = new List<Cell>();//Fill the group with the cells we want
                for (int j = 0; j < 9; j++)
                {
                    Group.Add(cellsArray[i, j]);
                }
                CheckGroupError();
            }
            #endregion
            #region Cols
            for (int j = 0; j < 9; j++)
            {
                Group = new List<Cell>();//Fill the group with the cells we want
                for (int i = 0; i < 9; i++)
                {
                    Group.Add(cellsArray[i, j]);
                }
                CheckGroupError();
            }
            #endregion
            if (error)
                if (t.IsAlive)
                    t.Abort();
            errortab--;
            for (int i = 0; i < errortab; i++)
            {
                Console.Write("    ");
            }
            Console.WriteLine("Saliendo de CheckSudokuError()");
        }

        void CheckGroupError()
        {
            for (int i = 0; i < errortab; i++)
            {
                Console.Write("    ");
            }
            Console.WriteLine("Entrando en CheckGroupError()");
            errortab++;
            //Check for repeated cells
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (Group[i].Num > 0 && Group[i] != Group[j] && Group[i].Num == Group[j].Num)
                    {//If CellA.Num == CellB.Num
                        //Mark both as error
                        Group[i].Error = true;
                        Group[j].Error = true;
                        error = true;
                        Console.WriteLine("");
                        Console.WriteLine("ERROR = TRUE;");
                        Console.WriteLine("");
                    }
                }
            }
            Dispatcher.Invoke(sc);//Selection Changed for every cell
            errortab--;
            for (int i = 0; i < errortab; i++)
            {
                Console.Write("    ");
            }
            Console.WriteLine("Saliendo de CheckGroupError()");
        }

        void CheckCase3()
        {
            for (int i = 0; i < errortab; i++)
            {
                Console.Write("    ");
            }
            Console.WriteLine("Entrando en CheckCase3()");
            errortab++;
            #region Boxes
            foreach (Box box in boxes)
            {
                Group = new List<Cell>();
                for (int r = 0; r < 3; r++)
                {
                    for (int c = 0; c < 3; c++)
                    {
                        Group.Add(box.array[r, c]);
                    }
                }
                Case3("Box");
            }
            #endregion
            #region Rows
            for (int i = 0; i < 9; i++)
            {
                Group = new List<Cell>();//Fill the group with the cells we want
                for (int j = 0; j < 9; j++)
                {
                    Group.Add(cellsArray[i, j]);
                }
                Case3("Row");
            }
            #endregion
            #region Cols
            for (int j = 0; j < 9; j++)
            {
                Group = new List<Cell>();//Fill the group with the cells we want
                for (int i = 0; i < 9; i++)
                {
                    Group.Add(cellsArray[i, j]);
                }
                Case3("Col");
            }
            #endregion
            errortab--;
            for (int i = 0; i < errortab; i++)
            {
                Console.Write("    ");
            }
            Console.WriteLine("Saliendo de CheckCase3()");
        }

        //Descarta el nº "n" de toda la Box menos las celdas del subgr
        void Discard(int n, Cell[] subgr, string origen)
        {
            for (int i = 0; i < errortab; i++)
            {
                Console.Write("    ");
            }
            Console.WriteLine("Entrando en Discard(int, Cell[], string)");
            errortab++;
            if (origen == "Row" || origen == "Col")
            {
                foreach (Cell cell in FindBox(subgr[0]).array)
                {
                    if (!subgr.Contains(cell))
                    {//If that cell isn't in the subgr, discard "n"
                        cell.Possible.Remove(n);
                    }
                    selectedCell = cell;
                    Dispatcher.Invoke(ui);
                }
            }
            else if (origen == "Box")
            {//Comprueba si debe descartar en horizontal/vertical, y luego descarta "n" en todas las celdas de la Row/Col
             //que no estén en subgr
             //1º Compruebo si debo descartar en horizontal
                List<Cell> AuxGroup = new List<Cell>();
                Row row = new Row(subgr[0]);
                if (row.Cells.Contains(subgr[1]))
                {//Debo descartar en horizontal
                    foreach (Cell cell in row.Cells)
                    {
                        AuxGroup.Add(cell);
                    }
                }
                else
                {//Debo descartar en vertical
                    Col col = new Col(subgr[0]);
                    foreach (Cell cell in col.Cells)
                    {
                        AuxGroup.Add(cell);
                    }
                }
                //2º Descarto "n" en todas las celdas de Group excepto las que están en subgr
                foreach (Cell cell in AuxGroup)
                {
                    if (!subgr.Contains(cell))
                    {
                        cell.Possible.Remove(n);
                    }
                    selectedCell = cell;
                    Dispatcher.Invoke(ui);
                }
            }
            errortab--;
            for (int i = 0; i < errortab; i++)
            {
                Console.Write("    ");
            }
            Console.WriteLine("Saliendo de Discard(int, Cell[], string)");
        }

        //Find the box that contains a Cell
        Box FindBox(Cell cell)
        {
            for (int i = 0; i < errortab; i++)
            {
                Console.Write("    ");
            }
            Console.WriteLine("Entrando en FindBox()");
            errortab++;
            foreach (Box box in boxes)
            {
                foreach (Cell boxCell in box.array)
                {
                    if (boxCell == cell)
                    {//When the Box that contains the cell is found, return the box
                        Console.WriteLine("Saliendo de FindBox() return Box");
                        return box;
                    }
                }
            }
            errortab--;
            for (int i = 0; i < errortab; i++)
            {
                Console.Write("    ");
            }
            Console.WriteLine("Saliendo de FindBox() return null");
            return null;//If everything is ok, this shouldn't be reached
        }

        void Case3(string origen)
        {
            for (int i = 0; i < errortab; i++)
            {
                Console.Write("    ");
            }
            Console.WriteLine("Entrando en Case3()");
            errortab++;
            Cell[] subgr1 = new Cell[3] { Group[0], Group[1], Group[2] };
            Cell[] subgr2 = new Cell[3] { Group[3], Group[4], Group[5] };
            Cell[] subgr3 = new Cell[3] { Group[6], Group[7], Group[8] };
            //Revisa que nums pueden aparecer en cada subgrupo
            auxObjList = new List<AuxObj>();
            LoadAuxObjList(subgr1);
            LoadAuxObjList(subgr2);
            LoadAuxObjList(subgr3);
            //Ahora está la lista AuxObjList cargada con AuxObj que guardan el subgr al que pertenece el nº
            //Si algún nº sólo puede ir en un subgrupo(Si su auxObj.Reps == 1)
            foreach (AuxObj ao in auxObjList)
            {
                if (ao.Reps == 1)
                {//Es un nº que obligatoriamente tiene que estar en ao.Subgr, debe ser descartado en el resto de celdas de su Box
                    Discard(ao.Num, ao.Subgr, origen);
                }
            }

            if (origen == "Box")
            {//In this case, it needs to do  a bit more
                Cell[] subgr4 = new Cell[3] { Group[0], Group[3], Group[6] };
                Cell[] subgr5 = new Cell[3] { Group[1], Group[4], Group[7] };
                Cell[] subgr6 = new Cell[3] { Group[2], Group[5], Group[8] };
                LoadAuxObjList(subgr4);
                LoadAuxObjList(subgr5);
                LoadAuxObjList(subgr6);
                foreach (AuxObj ao in auxObjList)
                {
                    if (ao.Reps == 1)
                    {
                        Discard(ao.Num, ao.Subgr, origen);
                    }
                }
            }
            errortab--;
            for (int i = 0; i < errortab; i++)
            {
                Console.Write("    ");
            }
            Console.WriteLine("Saliendo de Case3()");
        }

        void NakedSubset()
        {
            for (int i = 0; i < errortab; i++)
            {
                Console.Write("    ");
            }
            Console.WriteLine("Entrando en NakedSubset()");
            errortab++;
            //TODO: FINISH; Include this in CheckGroup() or when checking auxObjList
            List<Cell> auxCellList2 = new List<Cell>();
            List<Cell> auxCellList3 = new List<Cell>();
            foreach (Cell cell in Group)
            {
                if (cell.Possible.Count == 2)
                {
                    auxCellList2.Add(cell);
                }
                else if (cell.Possible.Count == 3)
                {
                    auxCellList3.Add(cell);
                }
            }
            if (auxCellList2.Count == 2)
            {
                auxCellList2[0].Possible.Equals(auxCellList2[1].Possible);
                auxCellList2[0].grid.Background = Brushes.Red;
            }
            errortab--;
            for (int i = 0; i < errortab; i++)
            {
                Console.Write("    ");
            }
            Console.WriteLine("Saliendo de NakedSubset()");
        }

        /******************************************************************************************************/
        //                                      MAIN METHODS END                                              //
        /******************************************************************************************************/



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

        private void buttFill_Click(object sender, RoutedEventArgs e)
        {//Fills with a Sudoku example
            #region Easy-Normal
            /*cellsArray[0, 0].Fix(5);
            cellsArray[0, 1].Fix(3);
            cellsArray[0, 4].Fix(7);
            cellsArray[1, 0].Fix(6);
            cellsArray[1, 3].Fix(1);
            cellsArray[1, 4].Fix(9);
            cellsArray[1, 5].Fix(5);
            cellsArray[2, 1].Fix(9);
            cellsArray[2, 2].Fix(8);
            cellsArray[2, 7].Fix(6);
            cellsArray[3, 0].Fix(8);
            cellsArray[3, 4].Fix(6);
            cellsArray[3, 8].Fix(3);
            cellsArray[4, 0].Fix(4);
            cellsArray[4, 3].Fix(8);
            cellsArray[4, 5].Fix(3);
            cellsArray[4, 8].Fix(1);
            cellsArray[5, 0].Fix(7);
            cellsArray[5, 4].Fix(2);
            cellsArray[5, 8].Fix(6);
            cellsArray[6, 1].Fix(6);
            cellsArray[6, 6].Fix(2);
            cellsArray[6, 7].Fix(8);
            cellsArray[7, 3].Fix(4);
            cellsArray[7, 5].Fix(9);
            cellsArray[7, 8].Fix(5);
            cellsArray[8, 4].Fix(8);
            cellsArray[8, 7].Fix(7);
            cellsArray[8, 8].Fix(9);*/
            #endregion
            #region Hard
            /*cellsArray[0, 1].Fix(1);
            cellsArray[0, 2].Fix(4);
            cellsArray[0, 6].Fix(2);
            cellsArray[0, 8].Fix(7);
            cellsArray[1, 2].Fix(7);
            cellsArray[1, 3].Fix(4);
            cellsArray[1, 5].Fix(9);
            cellsArray[2, 0].Fix(6);
            cellsArray[2, 3].Fix(1);
            cellsArray[2, 4].Fix(7);
            cellsArray[2, 6].Fix(4);
            cellsArray[3, 0].Fix(2);
            cellsArray[3, 6].Fix(9);
            cellsArray[3, 7].Fix(6);
            cellsArray[4, 1].Fix(3);
            cellsArray[4, 2].Fix(6);
            cellsArray[5, 3].Fix(6);
            cellsArray[5, 4].Fix(8);
            cellsArray[5, 8].Fix(4);
            cellsArray[6, 3].Fix(7);
            cellsArray[6, 4].Fix(3);
            cellsArray[6, 7].Fix(8);
            cellsArray[6, 8].Fix(5);
            cellsArray[7, 2].Fix(5);
            cellsArray[7, 5].Fix(8);
            cellsArray[7, 7].Fix(3);
            cellsArray[8, 1].Fix(6);//*/
            #endregion
            #region VeryHard
            cellsArray[0, 2].Fix(5);
            cellsArray[0, 7].Fix(8);
            cellsArray[0, 8].Fix(3);
            cellsArray[1, 0].Fix(4);
            cellsArray[1, 3].Fix(3);
            cellsArray[1, 4].Fix(9);
            cellsArray[2, 1].Fix(6);
            cellsArray[2, 3].Fix(4);
            cellsArray[2, 6].Fix(1);
            cellsArray[3, 4].Fix(7);
            cellsArray[3, 7].Fix(5);
            //cellsArray[3, 8].Fix(9);
            cellsArray[4, 0].Fix(3);
            cellsArray[4, 3].Fix(1);
            cellsArray[4, 8].Fix(2);
            cellsArray[5, 1].Fix(2);
            cellsArray[5, 2].Fix(9);
            cellsArray[5, 8].Fix(7);
            cellsArray[6, 5].Fix(6);
            cellsArray[7, 0].Fix(6);
            cellsArray[7, 1].Fix(8);
            cellsArray[8, 2].Fix(7);
            cellsArray[8, 3].Fix(9);
            cellsArray[8, 4].Fix(4);
            cellsArray[8, 6].Fix(2);//*/
            #endregion
            #region Extremo
            /*cellsArray[0, 0].Fix(4);
            cellsArray[0, 2].Fix(3);
            cellsArray[0, 3].Fix(5);
            cellsArray[0, 7].Fix(2);
            cellsArray[1, 4].Fix(6);
            cellsArray[1, 5].Fix(1);
            cellsArray[2, 0].Fix(7);
            cellsArray[3, 4].Fix(8);
            cellsArray[3, 5].Fix(9);
            cellsArray[3, 6].Fix(5);
            cellsArray[4, 3].Fix(3);
            cellsArray[4, 6].Fix(8);
            cellsArray[5, 0].Fix(2);
            cellsArray[6, 3].Fix(4);
            cellsArray[6, 7].Fix(7);
            cellsArray[7, 1].Fix(9);
            cellsArray[7, 6].Fix(6);
            cellsArray[8, 1].Fix(1);//*/
            #endregion
        }

        private void buttSave_Click(object sender, RoutedEventArgs e)
        {
            //State state = new State(txtState.Text);
            //txtState.Clear();
            //buttSave.Focus();
            //RefreshCombo();
        }

        private void stateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (!refreshing)
            //{
            //    if (prevState != null)
            //    {
            //        prevState.Name = prevState.Time.ToLongTimeString();
            //    }
            //    SetState((State)stateComboBox.SelectedItem);
            //    State state = new State();
            //    prevState = state;
            //    RefreshCombo();
            //}
        }

        //void SetState(State state)
        //{
        //    for (int i = 0; i < 9; i++)
        //    {
        //        for (int j = 0; j < 9; j++)
        //        {
        //            cellsArray[i, j] = state.Array[i, j];
        //        }
        //    }
        //    string str = String.Concat("Using: ", state.Name);
        //    str = String.Concat(state.Name, " at " + DateTime.Now);
        //    state.Name = str;
        //}

        private void buttUnfix_Click(object sender, RoutedEventArgs e)
        {
            foreach (Cell cell in cellsArray)
            {
                cell.Solved = false;
                cell.Fixed = false;
                cell.selected = false;
                cell.SelectionChanged();
            }
        }

        private void txtState_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //selectedCell.selected = false;
            //selectedCell.SelectionChanged();
            //selectedCell = null;
            //txtState.Focus();
        }

        //void RefreshCombo()
        //{
        //    refreshing = true;
        //    stateComboBox.ItemsSource = null;
        //    stateComboBox.ItemsSource = states;
        //}
    }
}
