using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    public class Cell
    {
        public bool IsEmpty { get; set; } = true;
        public IFigure FigureInCell { get; set; }
    }
    public class Board
    {
        public Cell[,] Field = new Cell[8, 8];
        public Board()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Field[i, j] = new Cell();
                }
            }
        }
    }
}
