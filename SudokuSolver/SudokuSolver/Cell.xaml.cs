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
        public bool selected = false;
        public Cell()
        {
            InitializeComponent();
            lbl.Content = "";
        }

        public void SelectionChanged()
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

        public void setNum(int n)
        {
            lbl.Content = n.ToString();
            //TODO: If the number is already on use, set background to red
        }

        private void lbl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Deselect other cells
            MainWindow.UpdateSelection();
            //Select this cell
            selected = true;
            MainWindow.selectedCell = this;
            SelectionChanged();
        }
    }
}
