using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Chess
{

    public abstract class Figure
    {
        public abstract bool IsWhite { get; set; }
        public abstract char ShortName { get; }
        public abstract bool IsCorrectMove(Point start, Point end, Board board);
        protected bool CheckLine(Point start, Point end, Board board)
        {
            if (start.X == end.X)
            {
                for (int i = Math.Min(start.Y, end.Y) + 1; i < Math.Max(start.Y, end.Y); i++)
                {
                    if (!board.Field[start.X, i].IsEmpty) return false;
                }
            }
            else if (start.Y == end.Y)
            {
                for (int i = Math.Min(start.X, end.X) + 1; i < Math.Max(start.X, end.X); i++)
                {
                    if (!board.Field[start.Y, i].IsEmpty) return false;
                }
            }
            else
            {
                if ((start.X < end.X && start.Y < end.Y) || (start.X > end.X && start.Y > end.Y))
                {
                    
                    for (int i = Math.Min(start.X, end.X) + 1; i < Math.Max(start.X, end.X);)
                    {
                        for (int j = Math.Min(start.Y, end.Y) + 1; j < Math.Max(start.Y, end.Y); j++)
                        {
                            if (!board.Field[i, j].IsEmpty) return false;
                            i++;
                        }
                    }
                }
                else
                {
                    for (int i = Math.Min(start.X, end.X) + 1; i < Math.Max(start.X, end.X); i++)
                    {
                        for (int j = Math.Max(start.Y, end.Y) - 1; j > Math.Min(start.Y, end.Y) ; j--)
                        {
                            if (!board.Field[i, j].IsEmpty) return false;
                            i++;
                        }
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
    public class Pawn : Figure
    {
        public override char ShortName => 'P';

        public override bool IsWhite { get; set; }

        public Pawn(bool isWhite)
        {
            IsWhite = isWhite;
        }
        public override bool IsCorrectMove(Point start, Point end, Board board)
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
                if (start.X == end.X)
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

        public override string ToString()
        {
            return ShortName.ToString();
        }
    }

    public class Rook : Figure
    {
        public override char ShortName => 'R';

        public override bool IsWhite { get; set; }

        public Rook(bool isWhite)
        {
            IsWhite = isWhite;
        }

        public override bool IsCorrectMove(Point start, Point end, Board board)
        {
            return CheckEndPoint(end, board, IsWhite)
                && (start.X == end.X || start.Y == end.Y) 
                && CheckLine(start, end, board); 
        }
        public override string ToString()
        {
            return ShortName.ToString();
        }
    }

    public class Horse : Figure
    {
        public override bool IsWhite { get; set; }

        public override char ShortName => 'H';

        public Horse(bool isWhite)
        {
            IsWhite = isWhite;
        }

        public override bool IsCorrectMove(Point start, Point end, Board board)
        {
            return CheckEndPoint(end, board, IsWhite)
                && ((Math.Abs(start.X - end.X) == 1 && Math.Abs(start.Y - end.Y) == 2) 
                    || (Math.Abs(start.X - end.X) == 2 && Math.Abs(start.Y - end.Y) == 1));
        }
        public override string ToString()
        {
            return ShortName.ToString();
        }
    }

    public class Bishop : Figure
    {
        public override bool IsWhite { get; set; }

        public override char ShortName => 'B';

        public Bishop(bool isWhite)
        {
            IsWhite = isWhite;
        }

        public override bool IsCorrectMove(Point start, Point end, Board board)
        {
            return (Math.Abs(start.X - end.X) == Math.Abs(start.Y - end.Y))
                && CheckEndPoint(end, board, IsWhite)
                && CheckLine(start, end, board);
        }
        public override string ToString()
        {
            return ShortName.ToString();
        }
    }

    public class King : Figure
    {
        public override bool IsWhite { get; set; }

        public override char ShortName => 'K';

        public King(bool isWhite)
        {
            IsWhite = isWhite;
        }

        public override bool IsCorrectMove(Point start, Point end, Board board)
        {
            return CheckEndPoint(end, board, IsWhite)
                && (((start.X == end.X) && Math.Abs(start.Y - end.Y) == 1)
                    || ((start.Y == end.Y) && Math.Abs(start.X - end.X) == 1)
                    || (Math.Abs(start.X - end.X) == 1 && Math.Abs(start.Y - end.Y) == 1));
        }
        public override string ToString()
        {
            return ShortName.ToString();
        }
    }

    public class Queen : Figure
    {
        public override bool IsWhite { get; set; }

        public override char ShortName => 'Q';

        public Queen(bool isWhite)
        {
            
            IsWhite = isWhite;
        }

        public override bool IsCorrectMove(Point start, Point end, Board board)
        {
            return CheckEndPoint(end, board, IsWhite)
                && (start.X == end.X || start.Y == end.Y || Math.Abs(start.X - end.X) == Math.Abs(start.Y - end.Y))
                && CheckLine(start, end, board);
        }
        public override string ToString()
        {
            return ShortName.ToString();
        }
    }
}
