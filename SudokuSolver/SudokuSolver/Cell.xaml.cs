using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Lógica de interacción para Cell.xaml
    /// </summary>
    public partial class Cell : UserControl
    {
        //Declarations
        public int Num;
        public bool Fixed = false;
        public bool Solved = false;
        public bool Marked = false;
        public bool Error = false;
        public List<int> Possible;
        public bool selected;

        public Cell()
        {
            InitializeComponent();
            Reset();
        }

        public void Reset()
        {
            lbl.Content = "";
            Num = -1;
            Unfix();
            //Possible = new List<int>();
            Possible = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            selected = false;
        }

        public void SelectionChanged()
        {//Change color when selected/deselected
            #region Marked
            //Show as marked when a number is selected
            if (Marked)
            {
                border.BorderBrush = Brushes.Lime;
                border.BorderThickness = new Thickness(2);
            }
            else
            {
                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(0.5);
            }
            #endregion
            #region Error
            //Show nº in red when cell gives error
            if (Error)
                lbl.Foreground = Brushes.Red;
            else
                lbl.Foreground = Brushes.Black;
            #endregion
            #region Background
            if (!Fixed && !Solved)
            {
                if (selected)
                {
                    grid.Background = Brushes.Bisque;
                }
                else
                {
                    grid.Background = Brushes.White;
                }
            }
            else if (Fixed)
            {
                if (selected)
                    grid.Background = Brushes.SaddleBrown;
                else
                    grid.Background = Brushes.SandyBrown;
            }
            else if (Solved)
            {
                if (selected)
                    grid.Background = Brushes.Plum;
                else
                    grid.Background = Brushes.Thistle;
            }
            #endregion
        }

        public void writeNum(int n)
        {//Writes user input
            Num = n;
            lbl.FontSize = 35;
            lbl.Content = n.ToString();
            MainWindow.mw.CheckSudokuError();
        }


        public void setNum(int n)
        {
            Num = n;
            lbl.FontSize = 35;
            lbl.Content = n.ToString();
            Solved = true;
            Possible.Clear();
            Possible.Add(n);
            //TODO: Check needed here?
            //MainWindow.mw.CheckSudokuError();
        }

        //SomethingChanged check needed
        /*public void Impossible(int n)
        {//Remove number from the list of possibilities
            if (Possible.Contains(n))
            {
                Possible.Remove(n);
            }
        }*/

        public void Fix()
        {
            Fixed = true;
            grid.Background = Brushes.SandyBrown;
            Possible.Clear();
            Possible.Add(Num);
        }

        public void Fix(int n)
        {
            grid.Background = Brushes.SandyBrown;
            setNum(n);
            Solved = false;
            Fixed = true;
            Possible.Clear();
            Possible.Add(n);
        }

        public void Unfix()
        {
            Fixed = false;
            grid.Background = Brushes.White;
        }

        public void ShowPossibles()
        {
            lbl.Content = "";
            lbl.FontSize = 12;
            int cont = 0;
            for (int i = 1; i < 10; i++)
            {
                if (cont == 3)
                {
                    lbl.Content += "\n";
                    cont = 0;
                }
                if (Possible.Contains(i))
                {
                    lbl.Content += i + "  ";
                }
                else
                {
                    lbl.Content += "   ";
                }
                cont++;
            }
        }

        private void cell_MouseDown(object sender, MouseButtonEventArgs e)
        {//Click Event
            if (this.Num != -1)
            {//Si tiene un nº
                foreach (Cell cell in MainWindow.cellsArray)
                {
                    if (cell.Possible.Contains(Num))
                        cell.Marked = true;
                    else
                        cell.Marked = false;
                    cell.SelectionChanged();
                }
            }
            else
            {
                foreach (Cell cell in MainWindow.cellsArray)
                {
                    cell.Marked = false;
                    cell.SelectionChanged();
                }
            }
            //Deselect other cells
            MainWindow.Unselect();
            //Select this cell
            selected = true;
            MainWindow.selectedCell = this;
            SelectionChanged();
        }
    }
}
