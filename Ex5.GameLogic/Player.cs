using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex5.GameLogic

{
    public class Player
    {
        private readonly string r_PlayerName;
        private readonly bool r_ComputerPlayer;
        private int m_PlayerScore = 0;
        private Dictionary<Card, Tuple<int, int>> m_ComputerCardsMemory;
        
        public Player(string i_PlayerName, bool i_ComputerPlayer)
        {
            r_PlayerName = i_PlayerName;
            r_ComputerPlayer = i_ComputerPlayer;
            m_ComputerCardsMemory = new Dictionary<Card, Tuple<int, int>>();
        }

        public int Score
        {
            get
            {
                return m_PlayerScore;
            }

            set
            {
                m_PlayerScore = value;
            }
        }

        public string Name
        {
            get
            {
                return r_PlayerName;
            }
        }

        public bool CompPlayer
        {
            get
            {
                return r_ComputerPlayer;
            }
        }

        public Dictionary<Card, Tuple<int, int>> ComputerCardsMemory
        {
            get
            {
                return m_ComputerCardsMemory;
            }

            set
            {
                m_ComputerCardsMemory = value;
            }
        }
    }
}