using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnLapTrinhMang.WindowsFormChess.Helpers
{
    public class GameScores
    {
        private string binFolder;
        private string scorePath;
        public GameScores()
        {
            binFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            scorePath = Path.Combine(binFolder, "Scores");
            if (!Directory.Exists(scorePath))
                Directory.CreateDirectory(scorePath);
        }

        public bool WriteToFile(DateTime saveTime, string winner, int blackScore, int whiteScore)
        {
            
            //Int32 unixTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            var guid = Guid.NewGuid().ToString();
            var saveFileName = Path.Combine(scorePath, guid + ".txt");

            using (StreamWriter writetext = new StreamWriter(saveFileName))
            {
                writetext.WriteLine("TIME : " + saveTime.ToString("dd/MM/yyyy HH:mm:ss"));
                writetext.WriteLine("WINNER : " + winner);
                writetext.WriteLine("BLACKSCORE : " + blackScore.ToString());
                writetext.WriteLine("WHITESCORE : " + whiteScore.ToString());
            }

            return true;
        }

        public string ReadFromFile(string file)
        {
            using (StreamReader sr = new StreamReader(file))
            {
                string line = sr.ReadToEnd();
                return line;
            }
        }

        public List<string> GetAllSaveFiles()
        {
            DirectoryInfo di = new DirectoryInfo(scorePath);
            FileInfo[] fiArray = di.GetFiles();
            Array.Sort(fiArray, (x, y) => Comparer<DateTime>.Default.Compare(x.CreationTime, y.CreationTime));
            List<string> files = new List<string>();
            foreach (FileInfo fi in fiArray.Reverse())
            {
                files.Add(fi.FullName);
            }
            return files;
        }
    }
}
