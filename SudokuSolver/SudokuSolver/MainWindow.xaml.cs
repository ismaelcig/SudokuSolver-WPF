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
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<Cell> cells;
        public static Cell selectedCell = null;
        public MainWindow()
        {
            InitializeComponent();

            cells = new List<Cell>();
            foreach (Cell item in g.Children)
            {
                cells.Add(item);
            }
        }
        public static void UpdateSelection()
        {
            foreach (Cell c in cells)
            {
                c.selected = false;
                c.SelectionChanged();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {//Sólo se pueden pulsar números
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
}
