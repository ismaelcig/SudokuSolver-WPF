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
        int aux = -1;
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

        /*************************************************************************************************************************************************/
        public MainWindow()
        {
            InitializeComponent();
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
        }
#region Delegates
        void UpdateCellNumber()
        {
            if (aux > 0)
            {//Si hay que actualizar el nº de la celda
                selectedCell.setNum(aux);
                //selectedCell.grid.Background = Brushes.Thistle;
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
                foreach (Cell cell in box.array)
                {
                    LoadAuxObjList(cell);
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
                        selectedCell.Possible.Remove(value);
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
                        c.Possible = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                    }
                }
                //TODO: Comprobar que el Sudoku es válido

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
        {
            tries = 3;
            bool solved = false;
            while (!solved)
            {
                Descartar();
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
                        LoadAuxObjList(cellsArray[i, j]);
                    }
                    CheckAuxObjList();
                }
                //CheckCols
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        LoadAuxObjList(cellsArray[j, i]);
                    }
                    CheckAuxObjList();
                }
                //}
                #endregion
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
                        solved = true;
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
        }

        void Descartar()
        {//Intenta descartar nº hasta que sólo quede uno en cada celda -> Orden: Box, Row, Col
            while (contChanges != 0)
            {
                contChanges = 0;//Para saber cuando el programa ya no es capaz de descartar más nº posibles
                #region Boxes
                foreach (Box box in boxes)
                {
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
                    Group = new List<Cell>();//Fill the group with the cells we want
                    for (int i = 0; i < 9; i++)
                    {
                        Group.Add(cellsArray[i, j]);
                    }
                    CheckGroup();
                }
                #endregion
            }

        }

        void CheckGroup()
        {
            Descartes = new HashSet<int>();
            do
            {
                somethingChanged = false;
                foreach (Cell cell in Group)//For each cell
                {
                    CheckCell(cell);
                }
            } while (somethingChanged);
        }

        void CheckCell(Cell c)
        {
            selectedCell = c;
            Dispatcher.Invoke(ui);
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
            }
            Dispatcher.Invoke(ui);
        }

        void LoadAuxObjList(Cell c)
        {
            selectedCell = c;
            Dispatcher.Invoke(id);
            if (!selectedCell.Fixed && !selectedCell.Solved)
            {
                foreach (int n in selectedCell.Possible)
                {
                    if (auxObjList.Where(z => z.Num == n).Count() == 0)
                    {//Si no existe ningún objeto en la lista con este nº, lo crea
                        auxObjList.Add(new AuxObj(n, selectedCell));
                        somethingChanged = true;
                        contChanges++;
                    }
                    else
                    {//Si ya existe, +1Rep
                        auxObjList.Single(z => z.Num == n).Reps++;
                    }
                }
            }
            Dispatcher.Invoke(id);
        }

        void CheckAuxObjList()
        {//Cuando acaba de recorrer una Row/Col/Box
            foreach (AuxObj aobj in auxObjList)
            {
                if (aobj.Reps == 1)
                {//Significa que aobj.Num sólo puede ir en aobj.Cell
                    aux = aobj.Num;
                    selectedCell = aobj.Cell;
                    //somethingChanged = true;
                    Dispatcher.Invoke(ui);
                    //Cuando descubre algún número, debería mirar qué cosas puede descartar
                    Descartar();
                }
            }
            auxObjList = new List<AuxObj>();
        }

        public void CheckSudokuError()
        {//This method will Check if the Sudoku is correct
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

        }

        void CheckGroupError()
        {
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
                    }
                }
            }
            Dispatcher.Invoke(sc);//Selection Changed for every cell
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

        private void button_Click(object sender, RoutedEventArgs e)
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
            cellsArray[0, 1].Fix(1);
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
            cellsArray[8, 1].Fix(6);
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
