using GameSolvingAlgorithms.GameInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStratego
{
    public class Move : IMove
    {
        private IField fieldToMove;
        private IFigure figure;
        private double points;
        private double pointsForMove;

        public Move(IField field, IFigure figure)
        {
            this.fieldToMove = field;
            this.figure = figure;
        }

        public IField GetField()
        {
            return fieldToMove;
        }

        public IFigure GetFigure()
        {
            return figure;
        }

        public double GetPoints()
        {
            return points;
        }

        public double GetPointsForMove()
        {
            return pointsForMove;
        }

        public void SetPoints(double points)
        {
            this.points = points;
        }

        public void SetPointsForMove(double pointsForMove)
        {
            this.pointsForMove = pointsForMove;
        }
    }
}
