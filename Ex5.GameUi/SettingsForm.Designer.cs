namespace Ex5.GameUi
{
    public partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_first_name = new System.Windows.Forms.Label();
            this.label_second_name = new System.Windows.Forms.Label();
            this.label_board_size = new System.Windows.Forms.Label();
            this.textBox_first_player = new System.Windows.Forms.TextBox();
            this.textBox_second_player = new System.Windows.Forms.TextBox();
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_friend_or_comp = new System.Windows.Forms.Button();
            this.btn_board_size = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_first_name
            // 
            this.label_first_name.AutoSize = true;
            this.label_first_name.Location = new System.Drawing.Point(28, 22);
            this.label_first_name.Name = "label_first_name";
            this.label_first_name.Size = new System.Drawing.Size(92, 13);
            this.label_first_name.TabIndex = 0;
            this.label_first_name.Text = "First Player Name:";
            // 
            // label_second_name
            // 
            this.label_second_name.AutoSize = true;
            this.label_second_name.Location = new System.Drawing.Point(28, 57);
            this.label_second_name.Name = "label_second_name";
            this.label_second_name.Size = new System.Drawing.Size(110, 13);
            this.label_second_name.TabIndex = 1;
            this.label_second_name.Text = "Second Player Name:";
            // 
            // label_board_size
            // 
            this.label_board_size.AutoSize = true;
            this.label_board_size.Location = new System.Drawing.Point(28, 92);
            this.label_board_size.Name = "label_board_size";
            this.label_board_size.Size = new System.Drawing.Size(61, 13);
            this.label_board_size.TabIndex = 2;
            this.label_board_size.Text = "Board Size:";
            // 
            // textBox_first_player
            // 
            this.textBox_first_player.Location = new System.Drawing.Point(150, 19);
            this.textBox_first_player.Name = "textBox_first_player";
            this.textBox_first_player.Size = new System.Drawing.Size(100, 20);
            this.textBox_first_player.TabIndex = 0;
            // 
            // textBox_second_player
            // 
            this.textBox_second_player.Enabled = false;
            this.textBox_second_player.Location = new System.Drawing.Point(150, 57);
            this.textBox_second_player.Name = "textBox_second_player";
            this.textBox_second_player.Size = new System.Drawing.Size(100, 20);
            this.textBox_second_player.TabIndex = 2;
            this.textBox_second_player.Text = "-computer-";
            // 
            // btn_start
            // 
            this.btn_start.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btn_start.ForeColor = System.Drawing.Color.Black;
            this.btn_start.Location = new System.Drawing.Point(314, 142);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(78, 23);
            this.btn_start.TabIndex = 4;
            this.btn_start.Text = "Start!";
            this.btn_start.UseVisualStyleBackColor = false;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_friend_or_comp
            // 
            this.btn_friend_or_comp.Location = new System.Drawing.Point(280, 51);
            this.btn_friend_or_comp.Name = "btn_friend_or_comp";
            this.btn_friend_or_comp.Size = new System.Drawing.Size(103, 23);
            this.btn_friend_or_comp.TabIndex = 1;
            this.btn_friend_or_comp.Text = "Against A Friend";
            this.btn_friend_or_comp.UseVisualStyleBackColor = true;
            this.btn_friend_or_comp.Click += new System.EventHandler(this.btn_friend_or_comp_Click);
            // 
            // btn_board_size
            // 
            this.btn_board_size.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(255)))));
            this.btn_board_size.Location = new System.Drawing.Point(31, 108);
            this.btn_board_size.Name = "btn_board_size";
            this.btn_board_size.Size = new System.Drawing.Size(89, 57);
            this.btn_board_size.TabIndex = 3;
            this.btn_board_size.UseVisualStyleBackColor = false;
            this.btn_board_size.Click += new System.EventHandler(this.btn_board_size_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 176);
            this.Controls.Add(this.btn_board_size);
            this.Controls.Add(this.btn_friend_or_comp);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.textBox_second_player);
            this.Controls.Add(this.textBox_first_player);
            this.Controls.Add(this.label_board_size);
            this.Controls.Add(this.label_second_name);
            this.Controls.Add(this.label_first_name);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Memory Game - Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_first_name;
        private System.Windows.Forms.Label label_second_name;
        private System.Windows.Forms.Label label_board_size;
        private System.Windows.Forms.TextBox textBox_first_player;
        private System.Windows.Forms.TextBox textBox_second_player;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_friend_or_comp;
        private System.Windows.Forms.Button btn_board_size;
    }
}