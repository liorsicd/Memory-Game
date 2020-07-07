using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex5.GameLogic
{
    public class Board
    {
        private const int k_MaxRows = 6;
        private const int k_MaxCols = 6;
        private const int k_MinRows = 4;
        private const int k_MinCols = 4;

        private readonly Tuple<int, int>[] r_FoundPositions;
        private readonly Random r_RandPosition;
        private Card[,] m_CardsBoard;
        private int m_LeftCardsNumber;
        private List<Card> m_CardList;

        public Board(int i_Rows, int i_Cols)
        {
            m_CardsBoard = new Card[i_Rows, i_Cols];
            m_LeftCardsNumber = i_Cols * i_Rows;
            this.r_FoundPositions = new Tuple<int, int>[i_Rows * i_Cols];
            this.createGameBoard( i_Rows, i_Cols);
            this.r_RandPosition = new Random();

        }

        public int LeftCardsNumber
        {
            get
            {
                return m_LeftCardsNumber;
            }

            set
            {
                m_LeftCardsNumber = value;
            }
        }

        public Card[,] CardsBoard
        {
            get
            {
                return m_CardsBoard;
            }

            set
            {
                m_CardsBoard = value;
            }
        }

        public List<Card> CardList
        {
            get
            {
                return this.m_CardList;
            }
        }

        private void createGameBoard(int i_Rows, int i_Cols)
        {
            int numOfPairs = i_Rows * i_Cols / 2;
            this.m_CardList= Card.CreateCardList(numOfPairs);

            for(int i = 0; i < i_Rows; i++)
            {
                for(int j = 0; j < i_Cols; j++)
                {
                    m_CardsBoard[i, j] = this.m_CardList[(i * i_Cols) + j];
                }
            }
        }

        public static List<Tuple<int, int>> GetOptionalBoardSizes()
        {
            List<Tuple<int,int>> list = new List<Tuple<int, int>>();
            for(int i = k_MinRows; i <= k_MaxRows; i++)
            {
                for(int j = k_MinCols; j <= k_MaxCols; j++)
                {
                    if(((i * j) % 2) == 0)
                    {
                        list.Add(new Tuple<int, int>(i, j));
                    }
                }
            }

            return list;
        }

        public static bool ValidBoardSize(int i_Rows, int i_Cols)
        {
            bool returnValue = (i_Rows * i_Cols) % 2 == 0;
            returnValue = returnValue && !(i_Rows > k_MaxRows || i_Cols > k_MaxCols);
            returnValue = returnValue && !(i_Rows < k_MinRows || i_Cols < k_MinCols);

            return returnValue;
        }

        public Card GetCard(Tuple<int, int> i_Position)
        {
            return m_CardsBoard[i_Position.Item1, i_Position.Item2];
        }

        public bool IsValidPosition(Tuple<int, int> i_Position)
        {
            bool returnValue = i_Position.Item1 < CardsBoard.GetLength(0);
            returnValue = returnValue && i_Position.Item2 < CardsBoard.GetLength(1);
            returnValue = returnValue && i_Position.Item1 >= 0 && i_Position.Item2 >= 0;

            return returnValue;
        }

        public Tuple<int, int> GenerateRandomPosition()
        {
            Tuple<int, int> newPosition;
            int rowPos;
            int colPos;

            do
            {   
                rowPos = this.r_RandPosition.Next(m_CardsBoard.GetLength(0));
                colPos = this.r_RandPosition.Next(m_CardsBoard.GetLength(1));
                newPosition = new Tuple<int, int>(rowPos, colPos);
            }
            while(this.r_FoundPositions.Contains(newPosition) || CardsBoard[rowPos, colPos].Open);

            return newPosition;
        }
    }
}
