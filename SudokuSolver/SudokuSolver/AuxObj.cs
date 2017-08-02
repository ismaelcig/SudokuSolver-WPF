﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class AuxObj
    {//Este objeto auxiliar se utiliza para comprobar cuando un nº aparece como posible en un único lugar
        public int Num { get; set; }//El nº que es
        public int Reps { get; set; }//La cantidad de veces que aparece en la Row/Col/Box, si =1, entonces ahí debe settearse
        public Cell Cell { get; set; }//Celda en la que se encuentra (para no tener que recorrer todas de nuevo)

        public AuxObj()
        {
            Reps = 0;
        }
        public AuxObj(int n, Cell c)
        {
            this.Num = n;
            this.Reps = 1;
            this.Cell = c;
        }
    }
}
