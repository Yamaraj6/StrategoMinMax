using System;

namespace GameSolvingAlgorithms.GameInterfaces
{
    public interface IPlayer
    {
        void AddPoints(double points);
        double GetPoints();
        PlayerType GetPlayerType();
        Algorithm GetAlgorithm();
        void SetAlgorithm(Algorithm algorithm);
        Heuristic GetHeuristic();
        void SetHeuristic(Heuristic heuristic);
        int GetMinMaxDepth();
    }

    public enum PlayerType
    {
        CPU,
        Human
    }
}
