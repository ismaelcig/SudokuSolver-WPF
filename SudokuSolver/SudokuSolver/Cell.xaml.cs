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
        public HashSet<int> Possible;
        public bool selected;

        public Cell()
        {
            InitializeComponent();
            FullReset();
            MainWindow.cells.Add(this);
        }

        public void Reset()//Used right before launching the Solve method
        {
            Possible = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Solved = false;
            selected = false;
            Unfix();
        }

        public void FullReset()
        {
            lbl.Content = "";
            Num = -1;
            Reset();
        }

        public void SelectionChanged()
        {//Change color when selected/deselected
            if (Fixed)
            {
                if (selected)
                {
                    cell.Background = Brushes.SaddleBrown;//Select fixed cell
                }
                else
                {
                    cell.Background = Brushes.SandyBrown;//Unselect fixed cell
                }
            }
            else if (Solved)
            {
                if (selected)
                {
                    cell.Background = Brushes.Plum;//Select solved cell
                }
                else
                {
                    cell.Background = Brushes.Thistle;//Unselect solved cell
                }
            }
            else
            {
                if (selected)
                {
                    cell.Background = Brushes.Bisque;//Select cell
                }
                else
                {
                    cell.Background = Brushes.White;//Unselect cell
                }
            }
        }

        public void setNum(int n)
        {
            Num = n;
            lbl.FontSize = 35;
            lbl.Content = n;
            Solved = true;
            //If the number is already on use, set foreground to red
            //if (!Possible.Contains(n))
            //{//TODO: Complete logic for this case
            //    lbl.Foreground = Brushes.Red;
            //}
            //else
            //{
            //    lbl.Foreground = Brushes.Black;
            //}
            //For now I'll leave this here
            Possible.Clear();
            Possible.Add(n);
        }

        public void writeNum(int n)
        {
            Num = n;
            lbl.FontSize = 35;
            lbl.Content = n;
            Possible.Clear();
            Possible.Add(n);
        }

        //SomethingChanged check needed
        public void Impossible(int n)//TODO: Use this method
        {//Remove number from the list of possibilities
            if (Possible.Contains(n))
            {
                Possible.Remove(n);
            }
        }

        public void Fix()
        {
            Solved = false;
            Fixed = true;
            Possible.Clear();
            Possible.Add(Num);
            SelectionChanged();
        }

        public void Fix(int n)
        {
            Num = n;
            lbl.FontSize = 35;
            lbl.Content = n;
            Fix();
        }

        public void Unfix()
        {
            Fixed = false;
            SelectionChanged();
        }

        public void ShowPossibles()
        {
            lbl.Content = "";
            lbl.FontSize = 12;
            int cont = 0;
            foreach (int item in Possible)
            {
                if (cont==3)
                {
                    lbl.Content += "\n";
                    cont = 0;
                }
                lbl.Content += item + "  ";
                cont++;
            }
        }
        //TODO: Review
        private void cell_MouseDown(object sender, MouseButtonEventArgs e)
        {//Click Event
            if (!MainWindow.working)
            {//If the program isn't solving the sudoku, allow the user to do whatever he wants
                //Unselect selected Cell
                try
                {
                    MainWindow.selectedCell.selected = false;
                    MainWindow.selectedCell.SelectionChanged();
                }
                catch (Exception ex)
                {
                    //Will throw the first time, when selected cell is still null
                }
                //Mark this as selected
                MainWindow.selectedCell = this;
                selected = true;
                SelectionChanged();
            }
            else
            {
                MessageBox.Show("The program is running");
            }
        }
    }
}
