using System;
using System.Collections.Generic;
using System.Text;
using GameSolvingAlgorithms;
using GameSolvingAlgorithms.GameInterfaces;

namespace GameStratego
{
    public class Game : IGame
    {
        public IPlayer[] players { get; private set; }
        public int playerWithMove = 0;
        private IMove lastMove;
        private IBoard board;
        private GameAI gameAI;

        public Game(IPlayer[] players, int boardSize)
        {
            this.players = players;
            board = new StrategoBoard(boardSize);
            gameAI = new GameAI(this);
        }

        public Game(IPlayer[] players, IBoard board)
        {
            this.players = players;
            this.board = board;
        }

        public IMove AIMakeMove(IPlayer player)
        {
            IMove move = gameAI.GetMove(player);
            
            if (move != null)
            {
                MakeMove(move);
            }
            return move;
        }

        public void MakeMove(IMove move)
        {
            players[playerWithMove].AddPoints(board.MakeMove(move));
            lastMove = move; 
            ChangePlayerWithMove();
        }

        public void UndoMove(IMove move)
        {
            ChangePlayerWithMove();
            players[playerWithMove].AddPoints(board.UndoMove(move));
        }
        
        public IPlayer[] GetPlayers()
        {
            return players;
        }

        public IBoard GetBoard()
        {
            return board;
        }

        private void ChangePlayerWithMove()
        {
            playerWithMove = playerWithMove == 0 ? 1 : 0;
        }

        public double CountResult(int resultPlayer)
        {
            double result = 0;
            if (resultPlayer == 0)
            {
                result = players[0].GetPoints() - players[1].GetPoints();
            }
            else
            {
                result = players[1].GetPoints() - players[0].GetPoints();
            }


            if (resultPlayer != -1)
            {
                var _moves = board.GetOccupiedPlaces();
                var _boardSize = board.GetBoardSize();
                foreach (var _move in _moves)
                {
                    if (_move.GetField().GetX() != 0 && _move.GetField().GetX() != _boardSize - 1 &&
                    _move.GetField().GetY() != 0 && _move.GetField().GetY() != _boardSize - 1 &&
                    _move.GetField().GetX() != 1 && _move.GetField().GetX() != _boardSize - 2 &&
                        _move.GetField().GetY() != 1 && _move.GetField().GetY() != _boardSize - 2)
                    {
                        result++;
                    }
                    else
                    {
                        result--;
                    }
                }
            }



            // +1 głębokości za 14 sek
            //var _move = board.GetBestMove();
            //if (_move != null)
            //{
            //    if (resultPlayer != playerWithMove)
            //    {
            //        result -= Evaluator.RateMove(_move.GetField(), board);
            //    }
            //    else
            //    {
            //        result += Evaluator.RateMove(_move.GetField(), board);
            //    }
            //}
            return result;
        }



        public int GetPlayerWithMove()
        {
            return playerWithMove;
        }
    }
}