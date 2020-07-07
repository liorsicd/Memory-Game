using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Ex5.GameUi
{
    using System.IO;
    using System.Net;
    using Ex5.GameLogic;
    using Timer = System.Windows.Forms.Timer;

    public partial class MainGameBoard : Form
    {
        private readonly SettingsForm r_SettingsForm = new SettingsForm();
        private readonly Game r_Game;
        private readonly Timer r_TimerCheckMatch = new Timer();
        private readonly Timer r_TimerCompTurn = new Timer();

        private Tuple<int, int> m_Card1;
        private Tuple<int, int> m_Card2;

        private Label m_CurrentTurn;
        private Label m_Player1;
        private Label m_Player2;
        private bool m_IsLoggedIn = false;
        private bool m_IsMatch = false;
        private int m_ClickCounter = 0;

        private Button[,] m_GameBoardButtons;

        public MainGameBoard()
        {
            if(this.ensureLoggedIn())
            {
                this.r_Game = new Game(
                    this.r_SettingsForm.Player1Name,
                    this.r_SettingsForm.Player2Name,
                    this.r_SettingsForm.IsComp);
                this.initBoard();

                this.r_TimerCheckMatch.Interval = 1000;
                this.r_TimerCompTurn.Interval = 1000;
                this.r_TimerCheckMatch.Tick += checkMatch;
                this.r_TimerCheckMatch.Tick += this.closeCards;
                this.r_TimerCheckMatch.Tick += this.matchFound;
                this.r_TimerCompTurn.Tick += this.playCompTurn;

                this.r_Game.CardOpened += this.openCard;
                InitializeComponent(this.r_SettingsForm.BoardSize);
            }
        }

        private bool ensureLoggedIn()
        {
            bool returnValue = false;
            if(!this.m_IsLoggedIn)
            {
                if(this.r_SettingsForm.ShowDialog() == DialogResult.OK)
                {
                    returnValue = true;
                }
            }

            return returnValue;
        }

        private void initBoard()
        {
            Tuple<int, int> boardSize = this.r_SettingsForm.BoardSize;
            this.r_Game.InitializeGame(boardSize.Item1, boardSize.Item2);

            int rows = this.r_Game.Board.CardsBoard.GetLength(0);
            int cols = this.r_Game.Board.CardsBoard.GetLength(1);

            this.m_GameBoardButtons = new Button[rows, cols];
            List<Button> buttonsWithImage = new List<Button>();

            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < cols; j++)
                {
                    this.m_GameBoardButtons[i, j] = new Button
                                                        {
                                                            Width = 70,
                                                            Height = 70,
                                                            Name = this.r_Game.Board.CardsBoard[i, j].Label.ToString(),
                                                            Tag = new Tuple<int, int>(i, j),
                                                            FlatAppearance = { BorderSize = 3 },
                                                            TabStop = false,
                                                            ImageList = new ImageList() { ImageSize = new Size(80, 80), ColorDepth = ColorDepth.Depth32Bit}
                                                        };

                    this.m_GameBoardButtons[i, j].Click += Card_Click;

                    bool found = false;
                    foreach (Button b in buttonsWithImage)
                    {
                        if (b.Name == m_GameBoardButtons[i, j].Name)
                        {
                            this.m_GameBoardButtons[i, j].ImageList.Images.Add(b.ImageList.Images[0]);
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        Image newImage = this.getImageFromUrl();
                        this.m_GameBoardButtons[i, j].ImageList.Images.Add(newImage);

                        buttonsWithImage.Add(this.m_GameBoardButtons[i, j]);
                    }


                    if (j == 0)
                    {
                        if(i == 0)
                        {
                            this.m_GameBoardButtons[i, j].Top = 10;
                            this.m_GameBoardButtons[i, j].Left = 10;
                        }
                        else
                        {
                            this.m_GameBoardButtons[i, j].Top = this.m_GameBoardButtons[i - 1, j].Bottom + 10;
                            this.m_GameBoardButtons[i, j].Left = 10;
                        }

                    }
                    else
                    {
                        this.m_GameBoardButtons[i, j].Top = this.m_GameBoardButtons[i, j - 1].Top;
                        this.m_GameBoardButtons[i, j].Left = this.m_GameBoardButtons[i, j - 1].Right + 10;
                    }

                    this.Controls.Add(this.m_GameBoardButtons[i, j]);
                }
            }

            m_CurrentTurn = new Label
                                {
                                    Left = 10,
                                    Text = $@"Current Player: {this.r_Game.CurrentTurn.Name}",
                                    Top = this.m_GameBoardButtons[rows - 1, cols - 1].Bottom + 10,
                                    BackColor = Color.Aquamarine,
                                    AutoSize = true
                                };

            m_Player1 = new Label
                            {
                                Left = 10,
                                Text = $@"{this.r_Game.Player1.Name}: {this.r_Game.Player1.Score} Pairs ",
                                Top = this.m_CurrentTurn.Top + 30,
                                BackColor = Color.Aquamarine,
                                AutoSize = true
                            };

            m_Player2 = new Label
                            {
                                Left = 10,
                                Text = $@"{this.r_Game.Player2.Name}: {this.r_Game.Player2.Score} Pairs ",
                                Top = this.m_Player1.Top + 30,
                                BackColor = Color.BlueViolet,
                                AutoSize = true
                            };


            this.Controls.AddRange(new Control[] { m_Player1, m_Player2 , m_CurrentTurn });
        }

        private Image getImageFromUrl()
        {
            WebClient webClient = new WebClient();
            byte[] bytes = webClient.DownloadData("https://picsum.photos/80");
            MemoryStream stream = new MemoryStream(bytes);
            Image img = Image.FromStream(stream);
            return img;
        }

        private void checkMatch(object sender, EventArgs e)
        {
            if(this.m_ClickCounter == 2)
            {
                System.Threading.Thread.Sleep(500);
                this.m_IsMatch = this.r_Game.IsMatch();
            }
        }

        private void matchFound(object sender, EventArgs e)
        {
            if(m_IsMatch)
            {
                this.m_GameBoardButtons[this.m_Card1.Item1, this.m_Card1.Item2].Enabled = false;
                this.m_GameBoardButtons[this.m_Card2.Item1, this.m_Card2.Item2].Enabled = false;

                this.m_GameBoardButtons[this.m_Card1.Item1, this.m_Card1.Item2].FlatStyle = FlatStyle.Flat;
                this.m_GameBoardButtons[this.m_Card2.Item1, this.m_Card2.Item2].FlatStyle = FlatStyle.Flat;

                this.m_GameBoardButtons[this.m_Card1.Item1, this.m_Card1.Item2].FlatAppearance.BorderColor =
                    this.getCurrentPlayerColor();

                this.m_GameBoardButtons[this.m_Card2.Item1, this.m_Card2.Item2].FlatAppearance.BorderColor =
                    this.getCurrentPlayerColor();

                m_Player1.Text = $@"{this.r_Game.Player1.Name}: {this.r_Game.Player1.Score} Pairs ";
                m_Player2.Text = $@"{this.r_Game.Player2.Name}: {this.r_Game.Player2.Score} Pairs ";

                this.m_ClickCounter = 0;
                this.m_IsMatch = false;

                if(this.r_Game.IsGameOver())
                {
                    showGameOver();
                }
            }

            this.r_TimerCheckMatch.Stop();
            this.r_TimerCompTurn.Start();
        }

        private void closeCards(object sender, EventArgs e)
        {
            if(!m_IsMatch)
            {
                this.m_GameBoardButtons[this.m_Card1.Item1, this.m_Card1.Item2].BackgroundImage = null;
                this.m_GameBoardButtons[this.m_Card2.Item1, this.m_Card2.Item2].BackgroundImage = null;

                m_CurrentTurn.Text = $@"Current Player: {this.r_Game.CurrentTurn.Name}";
                this.m_CurrentTurn.BackColor = this.getCurrentPlayerColor();

                this.m_ClickCounter = 0;
            }
        }

        private void playCompTurn(object sender, EventArgs e)
        {
            if(this.r_Game.CurrentTurn.CompPlayer)
            {
                System.Threading.Thread.Sleep(1000);
                this.r_Game.PlayCompTurn();
            }
            
            this.r_TimerCompTurn.Stop();
        }

        private void showGameOver()
        {
            this.r_TimerCheckMatch.Stop();
            string winner = this.r_Game.EndGame().Name;
            string tie = winner == "tie" ? "Tie Game" : $"the winner is: {winner}";
            string message =
                $"Game Over! {tie}{Environment.NewLine}Do you want to play again?";

            string title = "Game Over";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if(result == DialogResult.No)
            {
                Environment.Exit(0);
            }
            else
            {
                Controls.Clear();
                this.initBoard();
            }
        }

        private Color getCurrentPlayerColor()
        {
            return this.r_Game.CurrentTurn == this.r_Game.Player1 ? Color.Aquamarine : Color.BlueViolet;
        }

        private void openCard(Tuple<int, int> i_Position)
        {
            if(this.m_ClickCounter < 2)
            {
                m_GameBoardButtons[i_Position.Item1, i_Position.Item2].BackgroundImage =
                    m_GameBoardButtons[i_Position.Item1, i_Position.Item2].ImageList.Images[0];

                switch(this.m_ClickCounter)
                {
                    case 0:
                        this.m_Card1 = i_Position;
                        this.m_ClickCounter++;
                        break;
                    case 1:
                        this.m_Card2 = i_Position;
                        this.m_ClickCounter++;
                        this.r_TimerCheckMatch.Start();
                        break;
                }
            }
        }

        private void Card_Click(object sender, EventArgs e)
        {
            if(!this.r_Game.CurrentTurn.CompPlayer && this.m_ClickCounter < 2)
            {
                Tuple<int, int> pos = (Tuple<int, int>)(sender as Button)?.Tag;
                this.r_Game.OpenCard(pos);
                this.m_CurrentTurn.Focus();
            }
        }
    }
}
