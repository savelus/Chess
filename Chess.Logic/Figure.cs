using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Chess
{
    public interface IFigure
    {
        public abstract bool IsWhite { get; set;  }
        public abstract char ShortName { get; }
        public abstract bool IsCorrectMove(Point start, Point end, Board board);
    }

    public class Figure
    {
        protected bool CheckLine(Point start, Point end, Board board)
        {
            if (start.X == end.X)
            {
                for (int i = Math.Min(start.Y, end.Y) + 1; i < Math.Max(start.Y, end.Y) - 1; i++)
                {
                    if (!board.Field[start.X, i].IsEmpty) return false;
                }
            }
            else if (start.Y == end.Y)
            {
                for (int i = Math.Min(start.X, end.X) + 1; i < Math.Max(start.X, end.X) - 1; i++)
                {
                    if (!board.Field[start.Y, i].IsEmpty) return false;
                }
            }
            else
            {
                for (int i = Math.Min(start.X, end.X) + 1; i < Math.Max(start.X, end.X) - 1 ; i++)
                {
                    for (int j = Math.Min(start.Y, end.Y) +1; j < Math.Max(start.Y, end.Y) - 1; j++)
                    {
                        if (!board.Field[i, j].IsEmpty) return false;
                    }
                }
            }
            return true;
        }

        protected bool CheckEndPoint(Point end, Board board, bool isWhite)
        {
            return board.Field[end.X, end.Y].IsEmpty || isWhite != board.Field[end.X, end.Y].FigureInCell.IsWhite;
        }
    }
    public class Pawn : Figure, IFigure
    {
        public char ShortName => 'P';

        public bool IsWhite { get; set; }

        //public readonly bool IsWhite;
        public Pawn(bool isWhite)
        {
            IsWhite = isWhite;
        }
        public bool IsCorrectMove(Point start, Point end, Board board)
        {
            if (IsWhite)
            {
                if (start.X == end.X)
                {
                    return board.Field[end.X, end.Y].IsEmpty 
                        && ((start.Y == 1 && end.Y == 3 && board.Field[end.X, 2].IsEmpty) 
                            || (end.Y - start.Y == 1));
                }
                else
                {
                    return (!board.Field[end.X, end.Y].IsEmpty && !board.Field[end.X, end.Y].FigureInCell.IsWhite) 
                        && end.Y - start.Y == 1 && Math.Abs(end.X - start.X) == 1;
                }
            }
            else 
            {
                if(start.X == end.X)
                {
                    return board.Field[end.X, end.Y].IsEmpty 
                        && ((start.Y == 6 && end.Y == 4 && board.Field[end.X, 5].IsEmpty) 
                            || (end.Y - start.Y == -1));
                }
                else 
                {
                    return (!board.Field[end.X, end.Y].IsEmpty && board.Field[end.X, end.Y].FigureInCell.IsWhite) 
                        && end.Y - start.Y == -1 && Math.Abs(end.X - start.X) == 1;
                }
            }
        }
    }

    public class Rook : Figure, IFigure
    {
        public char ShortName => 'R';

        public bool IsWhite { get; set; }

        public Rook(bool isWhite)
        {
            IsWhite = isWhite;
        }

        public bool IsCorrectMove(Point start, Point end, Board board)
        {
            return CheckEndPoint(end, board, IsWhite)
                && (start.X == end.X || start.Y == end.Y) 
                && CheckLine(start, end, board);        }
    }

    public class Horse : Figure, IFigure
    {
        public bool IsWhite { get; set; }

        public char ShortName => 'H';

        public Horse(bool isWhite)
        {
            IsWhite = isWhite;
        }

        public bool IsCorrectMove(Point start, Point end, Board board)
        {
            return CheckEndPoint(end, board, IsWhite)
                && ((Math.Abs(start.X - end.X) == 2 && Math.Abs(start.Y - end.Y) == 3) 
                    || (Math.Abs(start.X - end.X) == 3 && Math.Abs(start.Y - end.Y) == 2));
        }
    }

    public class Bishop : Figure, IFigure
    {
        public bool IsWhite { get; set; }

        public char ShortName => 'B';

        public Bishop(bool isWhite)
        {
            IsWhite = isWhite;
        }

        public bool IsCorrectMove(Point start, Point end, Board board)
        {
            return (Math.Abs(start.X - end.X) == Math.Abs(start.Y - end.Y))
                && CheckEndPoint(end, board, IsWhite)
                && CheckLine(start, end, board);
        }
    }

    public class King : Figure, IFigure
    {
        public bool IsWhite { get; set; }

        public char ShortName => 'K';

        public King(bool isWhite)
        {
            IsWhite = isWhite;
        }

        public bool IsCorrectMove(Point start, Point end, Board board)
        {
            return CheckEndPoint(end, board, IsWhite)
                && (((start.X == end.X) && Math.Abs(start.Y - end.Y) == 1)
                    || ((start.Y == end.Y) && Math.Abs(start.X - end.X) == 1)
                    || (Math.Abs(start.X - end.X) == 1 && Math.Abs(start.Y - end.Y) == 1));
        }
    }

    public class Queen : Figure, IFigure
    {
        public bool IsWhite { get; set; }

        public char ShortName => 'K';

        public Queen(bool isWhite)
        {
            
            IsWhite = isWhite;
        }

        public bool IsCorrectMove(Point start, Point end, Board board)
        {
            return CheckEndPoint(end, board, IsWhite)
                && (start.X == end.X || start.Y == end.Y || Math.Abs(start.X - end.X) == Math.Abs(start.Y - end.Y))
                && CheckLine(start, end, board);
        }
    }
}
