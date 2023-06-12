using DoAnLapTrinhMang.WindowsFormChess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnLapTrinhMang.WindowsFormChess.Commons
{
    public static class PlayTimeManager
    {
        public static List<PlayTime> PlayTimes = new List<PlayTime>()
        {
            new PlayTime() {  Id = 0, Text = "Không hạn chế", Min = -1, Sec = -1 },
            new PlayTime() {  Id = 1, Text = "30 giây", Min = 0, Sec = 30 },
            new PlayTime() {  Id = 2, Text = "2 Phút 30 giây", Min = 2, Sec = 30 },
            new PlayTime() {  Id = 3, Text = "5 Phút", Min = 5, Sec = 0 },
            new PlayTime() {  Id = 4, Text = "15 Phút", Min = 0, Sec = 30 },
            new PlayTime() {  Id = 5, Text = "2 Giờ", Min = 120, Sec = 0}
        };

    }
}
