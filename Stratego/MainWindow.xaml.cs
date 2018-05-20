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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Stratego
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameManager game;

        public MainWindow()
        {
            InitializeComponent();
            InitializeComboBoxes();
            game = new GameManager(boardGrid, new TextBlock[] { points1, points2 });
        }

        private void InitializeComboBoxes()
        {
            var playerTypes = Enum.GetValues(typeof(PlayerType));
            var algorithmTypes = Enum.GetValues(typeof(Algorithm));
            AddItemsToComboBox(playerCB1, playerTypes);
            AddItemsToComboBox(playerCB2, playerTypes);
            AddItemsToComboBox(algorithmCB1, algorithmTypes);
            AddItemsToComboBox(algorithmCB2, algorithmTypes);
        }

        private void AddItemsToComboBox(ComboBox comboBox, Array items)
        {
            foreach (var item in items)
            {
                var cbItem = new ComboBoxItem();
                cbItem.Content = item;
                comboBox.Items.Add(cbItem);
                comboBox.SelectedIndex = 0;
            }
        }

        private void player_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StackPanel algorithm;
            if (((ComboBox)sender).Name == "playerCB1")
            {
                algorithm = algorithmSP1;
            }
            else
            {
                algorithm = algorithmSP2;
            }

            if ((ComboBoxItem)((ComboBox)sender).SelectedValue != null &&
                ((ComboBoxItem)((ComboBox)sender).SelectedValue).Content.ToString() == "CPU")
            {
                ((StackPanel)((ComboBox)sender).Parent).Children.Remove(algorithm);
                ((StackPanel)((ComboBox)sender).Parent).Children.Add(algorithm);
            }
            else
            {
                ((StackPanel)((ComboBox)sender).Parent).Children.Remove(algorithm);
            }
        }

        private void algorithm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StackPanel minMaxDepth;
            if (((ComboBox)sender).Name == "algorithmCB1")
            {
                minMaxDepth = minMaxDepthSP1;
            }
            else
            {
                minMaxDepth = minMaxDepthSP2;
            }
            var a = ((StackPanel)((ComboBox)sender).Parent);
            if ((ComboBoxItem)((ComboBox)sender).SelectedValue != null &&
                (((ComboBoxItem)((ComboBox)sender).SelectedValue).Content.ToString() == "MinMax"||
                ((ComboBoxItem)((ComboBox)sender).SelectedValue).Content.ToString() == "AlfaBeta"))
            {
                ((StackPanel)((StackPanel)((ComboBox)sender).Parent).Parent).Children.Remove(minMaxDepth);
                ((StackPanel)((StackPanel)((ComboBox)sender).Parent).Parent).Children.Add(minMaxDepth);
            }
            else
            {
                ((StackPanel)((StackPanel)((ComboBox)sender).Parent).Parent).Children.Remove(minMaxDepth);
            }
        }

        private void boardSizeTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        private async void startBtn_Click(object sender, RoutedEventArgs e)
        {
            if (boardSizeTB.Text == "" ||
                (minMaxDepthSP1.Parent != null && depthTB1.Text == "") ||
                (minMaxDepthSP1.Parent != null && depthTB1.Text != "" && Convert.ToInt32(depthTB1.Text) < 1) ||
                (minMaxDepthSP2.Parent != null && depthTB2.Text == "") ||
                (minMaxDepthSP2.Parent != null && depthTB2.Text != "" && Convert.ToInt32(depthTB2.Text) < 1))
            {
                return;
            }

            var watch = Stopwatch.StartNew();
            TimerStart(watch);

            game.NewGame(Convert.ToInt32(boardSizeTB.Text),
                        new ComboBox[] { playerCB1, playerCB2 },
                        new ComboBox[] { algorithmCB1, algorithmCB2 },
                        new TextBox[] { depthTB1, depthTB2 },
                        watch);
         //   watch.Stop();
            timeTb.Text = "KURWA";
        }

        private async void TimerStart(Stopwatch watch)
        {
            var task= Task.Run(async () =>
            {
                while (watch.IsRunning)
                {
                    Dispatcher.Invoke(() => timeTb.Text = "" + (float)watch.ElapsedMilliseconds/1000+ " sek");
                }
            });
            await task;
        }
    }
}