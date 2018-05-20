using GameSolvingAlgorithms;
using GameSolvingAlgorithms.GameInterfaces;
using GameStratego;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Stratego
{
    class GameManager
    {
        private Dictionary<int,Button> buttons;
        private Game game;
        private int playerTurn = 0;
        private Grid boardGrid;
        private TextBlock[] playersPoints;
        private Stopwatch watch;

        public GameManager(Grid boardGrid, TextBlock[] playersPoints)
        {
            this.boardGrid = boardGrid;
            this.playersPoints = playersPoints;
        }

        public void GenerateBoard(int size)
        {
            for (int row = 0; row < size; row++)
            {
                boardGrid.RowDefinitions.Add(new RowDefinition());
                boardGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            buttons = new Dictionary<int, Button>();
            for (int y = 0; y < size; ++y)
            {
                for (int x = 0; x < size; ++x)
                {
                    Button button = new Button()
                    {
                        Padding = new Thickness(100)
                    };
                    button.Name = string.Format("btn" + x + "_" + y);
                    button.Click += new RoutedEventHandler(button_Click);
                    buttons.Add(10000*x + y, button);
                    this.boardGrid.Children.Add(button);
                    Grid.SetRow(button, y);
                    Grid.SetColumn(button, x);
                }
            }
        }

        void button_Click(object sender, RoutedEventArgs e)
        {
            string[] numbers = Regex.Split((sender as Button).Name, @"\D+");
            game.MakeMove(new Move(
                new Field(Convert.ToInt32(numbers[1]), Convert.ToInt32(numbers[2])),
                new StrategoFigure()));
            UpdateGUI(sender as Button);
            CPUMove();
        }

        private void UpdateGUI(Button btn)
        {
            playersPoints[playerTurn].Text = "" + game.players[playerTurn].GetPoints();
            btn.Click -= new RoutedEventHandler(button_Click);
            //btn.Dispatcher.Invoke(delegate
            //{
                   switch (playerTurn)
                    {
                        case 0:
                            btn.Background = Brushes.Green;
                            playerTurn = 1;
                            break;
                        case 1:
                            btn.Background = Brushes.Red;
                            playerTurn = 0;
                            break;
                    }
                //});
            //return 42;
        }

        public void RemoveBoard()
        {
            boardGrid.Children.Clear();
            boardGrid.RowDefinitions.Clear();
            boardGrid.ColumnDefinitions.Clear();
        }

        public void NewGame(int boardSize, ComboBox[] playersInfo, ComboBox[] algorithmsInfo, TextBox[] minMaxDepth, Stopwatch watch)
        {
            this.watch = watch;
            if (boardSize > 1)
            {
                playerTurn = 0;
                RemoveBoard();
                GenerateBoard(boardSize);
                playersPoints[0].Text = "0";
                playersPoints[1].Text = "0";
                game = new Game(new Player[] {
                    NewPlayer(playersInfo[0], algorithmsInfo[0], minMaxDepth[0]),
                    NewPlayer(playersInfo[1], algorithmsInfo[1], minMaxDepth[1])},
                    boardSize);
                CPUMove();
            }
        }

        private async void CPUMove()
        {
            while (game.players[playerTurn].GetPlayerType() == PlayerType.CPU)
            {
                IMove move = null;
                var task = Task.Run(() => 
                {
                       move = game.AIMakeMove(game.players[playerTurn]);
                });
                await task;

                if (move == null)
                {
                    watch.Stop();
                    return;
                }
                var btn = buttons[10000 * move.GetField().GetX() + move.GetField().GetY()];
                UpdateGUI(btn);
            }
        }      
       

        private Player NewPlayer(ComboBox playerInfo, ComboBox algorithmInfo, TextBox minMaxDepth)
        {
            if((PlayerType)((ComboBoxItem)playerInfo.SelectedValue).Content==PlayerType.CPU)
            {
                if((Algorithm)((ComboBoxItem)algorithmInfo.SelectedValue).Content == Algorithm.MinMax
                    || (Algorithm)((ComboBoxItem)algorithmInfo.SelectedValue).Content == Algorithm.AlfaBeta)
                {
                    return new Player((PlayerType)((ComboBoxItem)playerInfo.SelectedValue).Content,
                        (Algorithm)((ComboBoxItem)algorithmInfo.SelectedValue).Content,
                        Convert.ToInt32(minMaxDepth.Text));
                }
                return new Player((PlayerType)((ComboBoxItem)playerInfo.SelectedValue).Content,
                    (Algorithm)((ComboBoxItem)algorithmInfo.SelectedValue).Content);
            }
            return new Player((PlayerType)((ComboBoxItem)playerInfo.SelectedValue).Content);
        }
    }
}