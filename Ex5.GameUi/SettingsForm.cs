using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Ex5.GameUi
{
    public partial class SettingsForm : Form
    {
        private readonly List<Tuple<int, int>> r_ListOfBoardSizes = null;
        private bool m_IsCompPlayer = true;
        private int m_IndexOfCurrentBoardSize = 0;

        public SettingsForm()
        {
            InitializeComponent();
            this.r_ListOfBoardSizes = GameLogic.Board.GetOptionalBoardSizes();
            this.btn_board_size.Text = $@"{this.r_ListOfBoardSizes[this.m_IndexOfCurrentBoardSize].Item1} X {this.r_ListOfBoardSizes[this.m_IndexOfCurrentBoardSize].Item2}";
        }

        public string Player1Name
        {
            get
            {
                return this.textBox_first_player.Text;
            }
        }

        public string Player2Name
        {
            get
            {
                return this.textBox_second_player.Text;
            }
        }

        public bool IsComp
        {
            get
            {
                return this.m_IsCompPlayer;
            }
        }

        public Tuple<int, int> BoardSize
        {
            get
            {
                return r_ListOfBoardSizes[this.m_IndexOfCurrentBoardSize];
            }
        }

        private void btn_friend_or_comp_Click(object sender, EventArgs e)
        {
            if(this.m_IsCompPlayer)
            {
                this.btn_friend_or_comp.Text = @"Against Computer";
                this.textBox_second_player.Enabled = true;
                this.textBox_second_player.Text = string.Empty;
            }
            else
            {
                this.btn_friend_or_comp.Text = @"Against a Friend";
                this.textBox_second_player.Enabled = false;
                this.textBox_second_player.Text = @"-computer-";
            }

            this.m_IsCompPlayer = !this.m_IsCompPlayer;
        }

        private void btn_board_size_Click(object sender, EventArgs e)
        {
            if(this.m_IndexOfCurrentBoardSize < this.r_ListOfBoardSizes.Count - 1)
            {
                this.m_IndexOfCurrentBoardSize++;
            }
            else
            {
                this.m_IndexOfCurrentBoardSize = 0;
            }

            string currentBoardSize =
                $"{this.r_ListOfBoardSizes[this.m_IndexOfCurrentBoardSize].Item1} X {this.r_ListOfBoardSizes[this.m_IndexOfCurrentBoardSize].Item2}";

            this.btn_board_size.Text = currentBoardSize;
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
