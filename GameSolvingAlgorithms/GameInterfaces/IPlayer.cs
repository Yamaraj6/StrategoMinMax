using System;

namespace GameSolvingAlgorithms.GameInterfaces
{
    public interface IPlayer
    {
        void AddPoints(double points);
        double GetPoints();
        PlayerType GetPlayerType();
        Algorithm GetAlgorithm();
        int GetMinMaxDepth();
    }

    public enum PlayerType
    {
        CPU,
        Human
    }
}
