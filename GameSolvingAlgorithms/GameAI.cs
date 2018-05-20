using GameSolvingAlgorithms.GameInterfaces;
using System;

namespace GameSolvingAlgorithms
{
    public class GameAI
    {
        private IGame game;
        private MinMax minMax;

        public GameAI(IGame game)
        {
            this.game = game;
             minMax = new MinMax();
        }

        public IMove GetMove(IPlayer player)
        {
            switch (player.GetAlgorithm())
            {
                case Algorithm.FirstPossibleMove:
                    return FirstPossibleMove();
                case Algorithm.RandomSearch:
                    return RandomSearch();
                case Algorithm.Greedy:
                    return Greedy();
                case Algorithm.BestMoves:
                    return BestMove();
                case Algorithm.MinMax:
                    return MinMax(player);
                case Algorithm.AlfaBeta:
                    return AlfaBeta(player);
            }
            return null;
        }

        private IMove FirstPossibleMove()
        {
            return game.GetBoard().GetFirstPossibleMove();
        }

        private IMove RandomSearch()
        {
            var moves = game.GetBoard().GetPossibleMoves();
            var rand = new Random();
            if (moves.Count > 0)
            {
                var i = rand.Next() % moves.Count;
                foreach (var move in moves)
                {
                    if (i == 0)
                    {
                        return move;
                    }
                    i--;
                }
            }
            return null;
        }

        private IMove Greedy()
        {
            return game.GetBoard().GetFirstScoredMove();
        }
    
        private IMove BestMove()
        {
            return game.GetBoard().GetBestMove();
        }

        private IMove MinMax(IPlayer activePlayer)
        {
            minMax.SetResultPlayer(game, activePlayer);
            return minMax.GetMinMaxMove(game, (IMove)null, activePlayer.GetMinMaxDepth());
        }

        private IMove AlfaBeta(IPlayer activePlayer)
        {
            int _depth = activePlayer.GetMinMaxDepth();
            if(_depth==100)
            {
                _depth = DynamicDepth();
            }

            minMax.SetResultPlayer(game, activePlayer);
            if (game.GetPlayers()[0].Equals(activePlayer))
            {
                return minMax.GetAlfaBetaMoveNew(game, null, _depth,
                    Double.MinValue, Double.MaxValue);
            }
            else
            {
                return minMax.GetAlfaBetaMoveNew(game, null, _depth, 
                    Double.MinValue, Double.MaxValue);
            }
        }

        private int DynamicDepth()
        {
            var size = game.GetBoard().GetBoardSize()/100;
            int depth = 1;
            if (game.GetBoard().GetPossibleMoves().Count < 5*size)
            {
                depth = 5;
            }
            else if (game.GetBoard().GetPossibleMoves().Count < 10* size)
            {
                depth = 4;
            }
            else if (game.GetBoard().GetPossibleMoves().Count < 17 * size)
            {
                depth = 3;
            }
            else if (game.GetBoard().GetPossibleMoves().Count < 30 * size)
            {
                depth = 2;
            }
            return depth;
        }
    }

    
    public enum Algorithm
    {
        FirstPossibleMove,
        RandomSearch,
        Greedy,
        BestMoves,
        MinMax,
        AlfaBeta
    }
}