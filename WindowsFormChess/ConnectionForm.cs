using DoAnLapTrinhMang.WindowsFormChess.Commons;
using DoAnLapTrinhMang.WindowsFormChess.Models;
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
    public partial class ConnectionForm : Form
    {
        public ConnectionForm()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            InGameForm newGame = new InGameForm(false, false, txtIP.Text);
            Visible = false;
            if (!newGame.IsDisposed)
                newGame.ShowDialog();
            Visible = true;

        }

        private void btnSelfHost_Click(object sender, EventArgs e)
        {
            int selectedPlayTime = (int)cboPlayTime.SelectedValue;
            InGameForm newGame = new InGameForm(false, true, playTimeId: selectedPlayTime);
            Visible = false;
            if (!newGame.IsDisposed)
                newGame.ShowDialog();
            Visible = true;

        }

        private void ConnectionForm_Load(object sender, EventArgs e)
        {
            LoadComboPlayTime();
        }

        private void LoadComboPlayTime()
        {
            
            cboPlayTime.DataSource = PlayTimeManager.PlayTimes;
            cboPlayTime.DisplayMember = "Text";
            cboPlayTime.ValueMember = "Id";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
