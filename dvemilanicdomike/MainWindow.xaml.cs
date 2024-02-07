using System.Reflection.Emit;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dvemilanicdomike
{
    public partial class MainWindow : Window
    {
        private Button[,] buttons;
        private char currentPlayer;
        private bool gameEnded;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            buttons = new Button[3, 3];
            buttons[0, 0] = but1;
            buttons[0, 1] = but2;
            buttons[0, 2] = but3;
            buttons[1, 0] = but4;
            buttons[1, 1] = but5;
            buttons[1, 2] = but6;
            buttons[2, 0] = but7;
            buttons[2, 1] = but8;
            buttons[2, 2] = but9;

            gameEnded = false;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    buttons[i, j].Content = "";
                    buttons[i, j].IsEnabled = true;
                    buttons[i, j].Click += but9_Click;
                }
            }

            currentPlayer = 'X';
            UpdateStatusText();
        }

        private void but9_Click(object sender, RoutedEventArgs e)
        {
            if (gameEnded)
                return;

            Button button = (Button)sender;
            int row = -1;
            int column = -1;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (buttons[i, j] == button)
                    {
                        row = i;
                        column = j;
                        break;
                    }
                }

                if (row != -1 && column != -1)
                    break;
            }

            if (row != -1 && column != -1)
            {
                if (buttons[row, column].IsEnabled)
                {
                    buttons[row, column].Content = currentPlayer.ToString();
                    buttons[row, column].IsEnabled = false;

                    if (CheckWin() || CheckDraw())
                    {
                        gameEnded = true;
                        foreach (Button btn in buttons)
                        {
                            btn.IsEnabled = false;
                        }
                    }
                    else
                    {
                        currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                        if (currentPlayer == 'O')
                        {
                            MakeRobotMove();
                            if (CheckWin() || CheckDraw())
                            {
                                gameEnded = true;
                                foreach (Button btn in buttons)
                                {
                                    btn.IsEnabled = false;
                                }
                            }
                            else
                            {
                                currentPlayer = 'X';
                            }
                        }
                    }

                    UpdateStatusText();
                }
            }
        }

        private void MakeRobotMove()
        {
            Random random = new Random();
            int row, column;

            do
            {
                row = random.Next(3);
                column = random.Next(3);
            } while (!buttons[row, column].IsEnabled);

            buttons[row, column].Content = currentPlayer.ToString();
            buttons[row, column].IsEnabled = false;
        }

        private bool CheckWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if (buttons[i, 0].Content.ToString() == currentPlayer.ToString() &&
                    buttons[i, 1].Content.ToString() == currentPlayer.ToString() &&
                    buttons[i, 2].Content.ToString() == currentPlayer.ToString())
                {
                    return true;
                }

                if (buttons[0, i].Content.ToString() == currentPlayer.ToString() &&
                    buttons[1, i].Content.ToString() == currentPlayer.ToString() &&
                    buttons[2, i].Content.ToString() == currentPlayer.ToString())
                {
                    return true;
                }
            }

            if (buttons[0, 0].Content.ToString() == currentPlayer.ToString() &&
                buttons[1, 1].Content.ToString() == currentPlayer.ToString() &&
                buttons[2, 2].Content.ToString() == currentPlayer.ToString())
            {
                return true;
            }

            if (buttons[0, 2].Content.ToString() == currentPlayer.ToString() &&
                buttons[1, 1].Content.ToString() == currentPlayer.ToString() &&
                buttons[2, 0].Content.ToString() == currentPlayer.ToString())
            {
                return true;
            }

            return false;
        }

        private bool CheckDraw()
        {
            foreach (Button btn in buttons)
            {
                if (btn.IsEnabled)
                    return false;
            }

            return true;
        }

        private void UpdateStatusText()
        {
            if (gameEnded)
            {
                if (CheckWin())
                {
                    Zagolovok.Text = $"Игрок {currentPlayer} выиграл!";
                }
                else if (CheckDraw())
                {
                    Zagolovok.Text = "Ничья(";
                }
            }
            else
            {
                Zagolovok.Text = $"Ходит: {currentPlayer}";
            }
        }

        private void but_Click(object sender, RoutedEventArgs e)
        {
            InitializeGame();
        }
    }
}