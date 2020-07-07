namespace Ex5.GameUi
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    partial class MainGameBoard
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
        private void InitializeComponent(Tuple<int, int> i_Size)
        {
            this.SuspendLayout();
            // 
            // MainGameBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Size size = computeSize(i_Size);
            this.ClientSize = new System.Drawing.Size(size.Width, size.Height);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainGameBoard";
            this.Text = "Memory Game";
            this.ResumeLayout(false);
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        #endregion

        private Size computeSize(Tuple<int, int> i_Size)
        {
            int width = i_Size.Item2 * 70 + (i_Size.Item2 + 1) * 10;
            int height = i_Size.Item1 *70 + (i_Size.Item2 + 1) * 10 +100;

            return new Size(width,height);
        }
    }

    
}