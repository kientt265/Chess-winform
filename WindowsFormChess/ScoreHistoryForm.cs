using DoAnLapTrinhMang.WindowsFormChess.Helpers;
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
    public partial class ScoreHistoryForm : Form
    {
        public ScoreHistoryForm()
        {
            InitializeComponent();
        }

        GameScores _gameScores = new GameScores();
        private void ScoreHistoryForm_Load(object sender, EventArgs e)
        {
            LoadAllSaveFiles();
        }

        private void LoadAllSaveFiles()
        {
            List<string> files = _gameScores.GetAllSaveFiles();

            foreach (string file in files)
            {
                lstSavedFiles.Items.Add(file);
            }
        }

        private void lstSavedFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSavedFiles.SelectedItems.Count > 0)
            {
                string selectedFile = lstSavedFiles.SelectedItems[0].ToString();
                string fileContent = _gameScores.ReadFromFile(selectedFile);
                rtResult.Text = fileContent;
            }
        }
    }
}
