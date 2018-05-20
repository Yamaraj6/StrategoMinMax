using System;
using System.Collections.Generic;
using System.Text;

namespace GameSolvingAlgorithms.GameInterfaces
{
    public interface IMove
    {
        IField GetField();
        IFigure GetFigure();
        double GetPoints();
        void SetPoints(double points);
        double GetPointsForMove();
        void SetPointsForMove(double pointsForMove);
    }
}
