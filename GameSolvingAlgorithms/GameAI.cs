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
            IMove move;
            int _depth = activePlayer.GetMinMaxDepth();
            if (_depth == 100)
            {
                _depth = DynamicDepth();
                if (_depth == 1)
                {
                    return game.GetBoard().GetFirstScoredMove();
                }
            }
            minMax.SetResultPlayer(game, activePlayer);
            if (game.GetPlayers()[0].Equals(activePlayer))
            {
                move = minMax.GetAlfaBetaMoveNew(game, null, _depth,
                    Double.MinValue, Double.MaxValue);
            }
            else
            {
                move = minMax.GetAlfaBetaMoveNew(game, null, _depth, 
                    Double.MinValue, Double.MaxValue);
            }
            return move;
        }

        private int DynamicDepth()
        {
            float size =  (game.GetBoard().GetBoardSize()* game.GetBoard().GetBoardSize()) / 100f;
            int depth = 1;
            if (game.GetBoard().GetPossibleMoves().Count < 5+(size*0.1f))
            {
                depth = 5;
            }
            else if (game.GetBoard().GetPossibleMoves().Count < 10+ (size*0.25f))
            {
                depth = 4;
            }
            else if (game.GetBoard().GetPossibleMoves().Count < 15+ (size*0.5f))
            {
                depth = 3;
            }
            else if (game.GetBoard().GetPossibleMoves().Count <= 25 + (size * 1))
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

    public enum Heuristic
    {
        Normal,
        CenterBetter,
        Deeper
    }
}