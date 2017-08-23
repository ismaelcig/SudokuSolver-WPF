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
        public Cell[,] array = new Cell[3, 3];

        public Box()
        {
            InitializeComponent();
            #region FillArray
            array[0, 0] = cell00;
            array[0, 1] = cell01;
            array[0, 2] = cell02;
            array[1, 0] = cell10;
            array[1, 1] = cell11;
            array[1, 2] = cell12;
            array[2, 0] = cell20;
            array[2, 1] = cell21;
            array[2, 2] = cell22;
#endregion
        }
    }
}
