using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnLapTrinhMang.WindowsFormChess
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void Start_btn_Click(object sender, EventArgs e)
        {
            Visible = false;
            InGameForm inGame = new InGameForm(true, false);
            if (!inGame.IsDisposed)
                inGame.ShowDialog();
            Visible = true;
        }

        private void Connection_btn_Click(object sender, EventArgs e)
        {
            Visible = false;
            ConnectionForm connectionForm = new ConnectionForm();
            if (!connectionForm.IsDisposed)
                connectionForm.ShowDialog();
            Visible = true;

        }

        private void btnViewSaveHistory_Click(object sender, EventArgs e)
        {
            Visible = false;
            ScoreHistoryForm scoreHistoryForm = new ScoreHistoryForm();
            if (!scoreHistoryForm.IsDisposed)
                scoreHistoryForm.ShowDialog();
            Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Visible = false;
            Onlineconect Onlineconect  = new Onlineconect ();
            if (!Onlineconect.IsDisposed)
                Onlineconect.ShowDialog();
            Visible = true;
        }
    }
}
