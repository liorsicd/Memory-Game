using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex5.GameUi
{
    public class Program
    {
        public static void Main()
        {
            Application.EnableVisualStyles();
            MainGameBoard mainGame = new MainGameBoard();
            mainGame.ShowDialog();
        }
    }
}
