using GameSolvingAlgorithms;
using GameSolvingAlgorithms.GameInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStratego
{
    public class Player : IPlayer
    {
        private double points;
        private PlayerType playerType;
        private Algorithm algorithm;
        private int minMaxDepth;

        public Player(PlayerType playerType)
        {
            points = 0;
            this.playerType = playerType;
        }

        public Player(PlayerType playerType, Algorithm algorithm)
        {
            points = 0;
            this.playerType = playerType;
            this.algorithm = algorithm;
        }

        public Player(PlayerType playerType, Algorithm algorithm, int minMaxDepth)
        {
            points = 0;
            this.playerType = playerType;
            this.algorithm = algorithm;
            this.minMaxDepth = minMaxDepth;
        }

        public Player(IPlayer player)
        {
            this.points = player.GetPoints();
            this.playerType = player.GetPlayerType();
            this.algorithm = player.GetAlgorithm();
            this.minMaxDepth = player.GetMinMaxDepth();
        }

        public void AddPoints(double points)
        {
            this.points+=points;
        }

        public Algorithm GetAlgorithm()
        {
            return algorithm;
        }

        public PlayerType GetPlayerType()
        {
            return playerType;
        }

        public double GetPoints()
        {
            return points;
        }

        public int GetMinMaxDepth()
        {
            return minMaxDepth;
        }
    }
}