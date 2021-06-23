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
                for (int j = 0; j < 8; j++)
                    Field[i, j] = new Cell();
        }

        public void ArrangeFiguresToStart()
        {
            for (int i = 0; i < 8; i++)
            {
                Field[i, 1].FigureInCell = new Pawn(true);
                Field[i, 1].IsEmpty = false;
                Field[i, 6].FigureInCell = new Pawn(false);
                Field[i, 6].IsEmpty = false;
            }
            ArrangeLineFigures(true);
            ArrangeLineFigures(false);
        }

        private void ArrangeLineFigures(bool isWhite)
        {
            int line = isWhite ? 0 : 7;
            Field[0, line].FigureInCell = new Rook(isWhite); 
            Field[1, line].FigureInCell = new Horse(isWhite);
            Field[2, line].FigureInCell = new Bishop(isWhite);
            Field[3, line].FigureInCell = new Queen(isWhite);
            Field[4, line].FigureInCell = new King(isWhite);
            Field[5, line].FigureInCell = new Bishop(isWhite);
            Field[6, line].FigureInCell = new Horse(isWhite);
            Field[7, line].FigureInCell = new Rook(isWhite);
            for (int i = 0; i < 8; i++)
            {
                Field[i, line].IsEmpty = false;
            }
        }
    }
}
