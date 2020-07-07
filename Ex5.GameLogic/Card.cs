using System;
using System.Collections.Generic;
using System.Text;


namespace Ex5.GameLogic
{
    using System.Linq;

    public class Card
    {
        private Card m_Pair = null;
        private char m_Label;
        private bool m_Found = false;
        private bool m_Open = false;

        public Card(char i_Label)
        {
            m_Label = i_Label;
        }

        public char Label
        {
            get
            {
                return m_Label;

            }

            set
            {
                m_Label = value;
            }
        }

        public bool Found
        {
            get
            {
                return m_Found; 
            }

            set
            {
                m_Found = value;
            }
        }

        public bool Open
        {
            get
            {
                return m_Open;
            }

            set
            {
                m_Open = value;
            }
        }

        public Card Pair
        {
            get
            {
                return m_Pair;
            } 
        }

        public static List<Card> CreateCardList(int i_NumOfPairs)
        {
            List<Card> cardList = new List<Card>(i_NumOfPairs * 2 );
            List<char> labelList = new List<char>(i_NumOfPairs);
            Random cardLabel = new Random();


            // random labels
            do
            {
                char nextLabel = (char)cardLabel.Next(90 - i_NumOfPairs, 90);
                if(!labelList.Contains(nextLabel))
                {
                    labelList.Add(nextLabel);
                }
            }
            while(labelList.Count < i_NumOfPairs);
            
            // create cards
            foreach(char label in labelList)
            {
                Card c1 = new Card(label);
                Card c2 = new Card(label);
                c1.Set_Pair(c2);
                c2.Set_Pair(c1);
                cardList.Add(c1);
                cardList.Add(c2);
            }

            // shuffle cards
            Random shuffleRandom = new Random();
            cardList = cardList.OrderBy(i_Card => shuffleRandom.Next()).ToList();

            return cardList;
        }

        public void Set_Pair(Card i_PairCard)
        {
            m_Pair = i_PairCard;
        }

        public void CloseCard()
        {
            m_Open = false;
        }
    }
}
