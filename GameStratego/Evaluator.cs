using GameSolvingAlgorithms.GameInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStratego
{
    public static class Evaluator
    {
        public static int RateMove(IField field, IBoard board)
        {
            return CountDiagonal(field, board, Line.Horizontal) +
                 CountDiagonal(field, board, Line.Vertical) +
                 CountDiagonal(field, board, Line.DiagonalLeft) +
                 CountDiagonal(field, board, Line.DiagonalRight);
        }

        private static int CountDiagonal(IField field, IBoard board, Line line)
        {
            int couter = 1;
            switch (line)
            {
                case Line.Horizontal:
                    couter = (CountDiagonalPart(field, board, 1, 0, ref couter) && CountDiagonalPart(field, board, -1, 0, ref couter)) ?
                        couter : 0;
                    break;
                case Line.Vertical:
                    couter = (CountDiagonalPart(field, board, 0, 1, ref couter) && CountDiagonalPart(field, board, 0, -1, ref couter)) ?
                        couter : 0;
                    break;
                case Line.DiagonalLeft:
                    couter = (CountDiagonalPart(field, board, 1, 1, ref couter) && CountDiagonalPart(field, board, -1, -1, ref couter)) ?
                        couter : 0;
                    break;
                case Line.DiagonalRight:
                    couter = (CountDiagonalPart(field, board, 1, -1, ref couter) && CountDiagonalPart(field, board, -1, 1, ref couter)) ?
                        couter : 0;
                    break;
            }
            return couter > 1 ? couter : 0;
        }

        private static bool CountDiagonalPart(IField field, IBoard board, int xChange, int yChange, ref int counter)
        {
            int x = field.GetX() + xChange;
            int y = field.GetY() + yChange;

            while ((x >= 0 && y >= 0) && (x < board.GetBoardSize() && y < board.GetBoardSize()))
            {
                if (board.GetBoard()[new Field(x, y)] != null)
                {
                    counter++;
                }
                else
                {
                    return false;
                }
                x += xChange;
                y += yChange;
            }
            return true;
        }
    }

    enum Line
    {
        Vertical,
        Horizontal,
        DiagonalLeft,
        DiagonalRight
    }
}