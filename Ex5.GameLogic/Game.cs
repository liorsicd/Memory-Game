using System;
using System.Collections.Generic;

namespace Ex5.GameLogic
{
    public delegate void OpenCardHandler(Tuple<int, int> i_Position);
    
    public class Game
    {
        private readonly Player r_Player1;
        private readonly Player r_Player2;
        private readonly Card[] r_OpenCards;
        private Board m_Board;
        private Player m_CurrentTurn = null;
        
        public Game(string i_NamePlayer1, string i_NamePlayer2, bool i_IsComp)
        {
            r_Player1 = new Player(i_NamePlayer1,false);
            r_Player2 = i_IsComp ? new Player(i_NamePlayer2,true) : new Player(i_NamePlayer2, false);
            this.r_OpenCards  = new Card[2];
        }

        public Player CurrentTurn
        {
            get
            {
                return m_CurrentTurn;
            }

            set
            {
                m_CurrentTurn = value;
            }
        }

        public Board Board
        {
            get
            { 
                return m_Board;
            } 
        }

        public Player Player1
        {
            get
            {
                return this.r_Player1;
            }
        }

        public Player Player2
        {
            get
            {
                return this.r_Player2;
            }
        }


        public bool InitializeGame(int i_Rows, int i_Cols)
        {
            r_Player1.Score = 0;
            r_Player2.Score = 0;

            bool returnValue = Board.ValidBoardSize(i_Rows, i_Cols);
            if(returnValue)
            {
                m_Board = new Board(i_Rows, i_Cols);
                m_CurrentTurn = r_Player1;
                this.r_OpenCards[0] = null;
                this.r_OpenCards[1] = null; 
                r_Player2.ComputerCardsMemory.Clear();
            }

            return returnValue;
        }

        public void PlayCompTurn()
        {
            

            // computer player remember cards that opened during the game, and try to find match using the memory 
            Tuple<int, int> position1  = null;
            Tuple<int, int> position2 = null;

            // check if there is pair in memory
            foreach(KeyValuePair<Card, Tuple<int, int>> c in r_Player2.ComputerCardsMemory)
            {
                if(r_Player2.ComputerCardsMemory.ContainsKey(c.Key.Pair))
                {
                    position1 = c.Value;
                    r_Player2.ComputerCardsMemory.TryGetValue(c.Key.Pair, out position2);
                    break;
                }
            }

            if(position1 == null)
            {
                position1 = m_Board.GenerateRandomPosition();
            }

            OpenCard(position1);
            Card firstCard = m_Board.GetCard(position1);

            // check if the pair of first card is in the memory
            if(position2 == null)
            {
                if(r_Player2.ComputerCardsMemory.ContainsKey(firstCard.Pair))
                {
                    r_Player2.ComputerCardsMemory.TryGetValue(firstCard.Pair, out position2);
                }
                else
                {
                    position2 = m_Board.GenerateRandomPosition();
                }
            }

            OpenCard(position2);
        }


        public event OpenCardHandler CardOpened;

        public void OpenCard(Tuple<int, int> i_Position)
        {
            bool returnValue = true;

            if(!m_Board.IsValidPosition(i_Position))
            {
                returnValue = false;
            }
            else if (m_Board.GetCard(i_Position).Open || m_Board.GetCard(i_Position).Found)
            {
                returnValue = false;
            }

            if(returnValue)
            {
                m_Board.GetCard(i_Position).Open = true;
                int index = this.r_OpenCards[0] != null ? 1 : 0;
                this.r_OpenCards[index] = m_Board.GetCard(i_Position);
                
                // add card to computer memory if it is not opened yet.
                if(r_Player2.CompPlayer && !r_Player2.ComputerCardsMemory.ContainsKey(m_Board.GetCard(i_Position)))
                {
                    r_Player2.ComputerCardsMemory.Add(m_Board.GetCard(i_Position), i_Position);
                }
            }

            OnCardOpened(i_Position);
        }

        protected virtual void OnCardOpened(Tuple<int, int> i_Position)
        {
            CardOpened?.Invoke(i_Position);
        }

        public bool IsMatch()
        {
            bool match = this.r_OpenCards[0].Pair == this.r_OpenCards[1] && this.r_OpenCards[1].Pair == this.r_OpenCards[0];

            if(match)
            {
                CurrentTurn.Score += 1;
                this.r_OpenCards[0].Found = true;
                this.r_OpenCards[1].Found = true;
                m_Board.LeftCardsNumber -= 2;

                r_Player2.ComputerCardsMemory.Remove(this.r_OpenCards[0]);
                r_Player2.ComputerCardsMemory.Remove(this.r_OpenCards[1]);
            }
            else
            {
                CurrentTurn = CurrentTurn == r_Player1 ? r_Player2 : r_Player1;
                this.r_OpenCards[0].CloseCard();
                this.r_OpenCards[1].CloseCard();
            }

            this.r_OpenCards[0] = null;
            this.r_OpenCards[1] = null;


            return match;
        }


        public bool IsGameOver()
        {
            return m_Board.LeftCardsNumber == 0;
        }

        public Player EndGame()
        {
            Player tie = new Player("tie", false);
            tie.Score = r_Player1.Score;

            // check winner
            Player winner = r_Player1.Score > r_Player2.Score ? r_Player1 : r_Player2;

            // check if tie
            winner = r_Player1.Score != r_Player2.Score ? winner : tie;

            return winner;
        }
    }
}
