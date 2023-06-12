using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnLapTrinhMang.WindowsFormChess
{
    public static class NetworkComProtocol
    {
        public const int MOV_CMD = 0x0;
        public const int UNDO_REQ = 0x1;
        public const int UNDO_ACCEPT = 0x2;
        public const int PAIR_REQ = 0xF0;
        public const int PAIR_ACCEPT = 0xF1 ;
        // Play Time
        public const int GET_PLAY_TIME = 0xF2;
        public const int SET_PLAY_TIME = 0xF3;
    }
}
