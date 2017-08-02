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
            Possible = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            selected = false;
        }

        public void SelectionChanged()
        {//Change color when selected/deselected
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
        }

        public void setNum(int n)
        {
            Num = n;
            lbl.FontSize = 35;
            lbl.Content = n.ToString();
            Solved = true;
            Possible.Clear();
            Possible.Add(n);
            //TODO: If the number is already on use, set background to red
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
            Fixed = true;
            grid.Background = Brushes.SandyBrown;
            setNum(n);
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

        private void lbl_MouseDown(object sender, MouseButtonEventArgs e)
        {//Click Event
            //Deselect other cells
            MainWindow.Unselect();
            //Select this cell
            selected = true;
            MainWindow.selectedCell = this;
            SelectionChanged();
        }
    }
}
