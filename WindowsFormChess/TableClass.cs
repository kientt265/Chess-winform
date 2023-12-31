﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnLapTrinhMang.WindowsFormChess
{
    class TableClass
    {
        public int[,] Table { get; set; }
        public int[,] PossibleMoves { get; set; }
        public int[,] AllPossibleMoves { get; set; }
        public bool WhiteStaleUp=false;
        public bool BlackStaleUp=false;
        public bool CancelLastMove=false;

        public bool MarkStale(ClickUserClass[,]TableBackground, int[,] Table, int[,] WhiteStaleArray, int[,] BlackStaleArray)
        {

            int WhiteKingPositionI = 0;
            int WhiteKingPositionJ = 0;
            int BlackKingPositionI = 0;
            int BlackKingPositionJ = 0;
            WhiteStaleUp = false;
            BlackStaleUp = false;


            for (int a = 0; a < TableSize.TABLE_HEIGHT; a++)
            {
                for (int b = 0; b < TableSize.TABLE_WIDTH; b++)
                {
                    if (Table[a, b] == ChessType.WKing)
                    {
                        WhiteKingPositionI = a;
                        WhiteKingPositionJ = b;
                    }
                    if (Table[a, b] == ChessType.BKing)
                    {
                        BlackKingPositionI = a;
                        BlackKingPositionJ = b;
                    }
                }
            }
            if (WhiteStaleArray[WhiteKingPositionI, WhiteKingPositionJ] == 2)
            {
                TableBackground[WhiteKingPositionI, WhiteKingPositionJ].BackColor = Color.Red;
                WhiteStaleUp = true;
                return true;
            }
            if (BlackStaleArray[BlackKingPositionI, BlackKingPositionJ] == 2)
            {
                TableBackground[BlackKingPositionI, BlackKingPositionJ].BackColor = Color.Red;
                BlackStaleUp = true;
                return true;
            }
            return false;

        }
        public int NotValidMoveChecker(int[,] Table, int[,] WhiteStaleArray, int[,] BlackStaleArray)
        {

            int WhiteKingPositionI = 0;
            int WhiteKingPositionJ = 0;
            int BlackKingPositionI = 0;
            int BlackKingPositionJ = 0;

            for (int a = 0; a < TableSize.TABLE_HEIGHT; a++)
            {
                for (int b = 0; b < TableSize.TABLE_WIDTH; b++)
                {
                    if (Table[a, b] == ChessType.WKing)
                    {
                        WhiteKingPositionI = a;
                        WhiteKingPositionJ = b;
                    }
                    if (Table[a, b] == ChessType.BKing)
                    {
                        BlackKingPositionI = a;
                        BlackKingPositionJ = b;
                    }
                }
            }
            if (WhiteStaleArray[WhiteKingPositionI, WhiteKingPositionJ] == 2)
            {
                return 1;
            }
            if (BlackStaleArray[BlackKingPositionI, BlackKingPositionJ] == 2)
            {
                return 2;
            }
            return 3;

        }
    }
}
