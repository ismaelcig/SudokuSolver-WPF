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
    /// Lógica de interacción para Box.xaml
    /// </summary>
    public partial class Box : UserControl
    {
        public Cell[,] cells= new Cell[3,3];


        public Box()
        {
            InitializeComponent();
            GetChildCells();
        }

        void GetChildCells()
        {
            cells[0, 0] = cell11;
            cells[0, 1] = cell12;
            cells[0, 2] = cell13;
            cells[1, 0] = cell21;
            cells[1, 1] = cell22;
            cells[1, 2] = cell23;
            cells[2, 0] = cell31;
            cells[2, 1] = cell32;
            cells[2, 2] = cell33;
        }
    }
}
