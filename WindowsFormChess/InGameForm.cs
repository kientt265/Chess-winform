using DoAnLapTrinhMang.WindowsFormChess.Commons;
using DoAnLapTrinhMang.WindowsFormChess.Helpers;
using DoAnLapTrinhMang.WindowsFormChess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace DoAnLapTrinhMang.WindowsFormChess
{
    public partial class InGameForm : Form
    {
        #region Pieces
        BlackPawn blackPawn = new BlackPawn();
        BlackRook1 blackRook2 = new BlackRook1();
        BlackRook2 blackRook1 = new BlackRook2();
        BlackKnight blackKnight = new BlackKnight();
        BlackKnight2 blackKnight2 = new BlackKnight2();
        BlackBishop blackBishop = new BlackBishop();
        BlackBishop2 blackBishop2 = new BlackBishop2();
        BlackQueen blackQueen = new BlackQueen();
        BlackKing blackKing = new BlackKing();
        WhitePawn whitePawn = new WhitePawn();
        WhiteRook1 whiteRook2 = new WhiteRook1();
        WhiteRook2 whiteRook1 = new WhiteRook2();
        WhiteKnight whiteKnight = new WhiteKnight();
        WhiteKnight2 whiteKnight2 = new WhiteKnight2();
        WhiteBishop whiteBishop = new WhiteBishop();
        WhiteBishop2 whiteBishop2 = new WhiteBishop2();
        WhiteQueen whiteQueen = new WhiteQueen();
        WhiteKing whiteKing = new WhiteKing();
        #endregion
        #region bools
        public bool BlackRookMoved1 = true;
        public bool BlackRookMoved2 = true;
        public bool BlackKingMoved = true;
        public bool WhiteRookMoved1 = true;
        public bool WhiteRookMoved2 = true;
        public bool WhiteKingMoved = true;
        public bool _isSingleGame = false;
        public bool WhiteTurn = true;
        public bool NotAllowedMove = false;
        public bool OtherPlayerTurn = false;
        public bool GameOver = false;
        public bool enablesocket = true;
        private bool _isServerStarted;
        #endregion
        #region Socket
        private Socket sock;
        private BackgroundWorker MessageReceiver = new BackgroundWorker();
        private TcpListener server = null;
        private TcpClient client;
        #endregion
        #region integers
        public int BeforeMove_I;
        public int BeforeMove_J;
        public int LastMovedPiece = 0;
        public int Moves = 0;
        public int Castling = 0;
        public int Promotionvalue = 0;
        public int PromotedPiece { get; set; }
        CountDownTimer timer = new CountDownTimer();
        #endregion
            
        ClickUserClass[,] TableBackground;
        TableClass tableClass = new TableClass();
        public int[,] WhiteStaleArray = new int[TableSize.TABLE_WIDTH, TableSize.TABLE_HEIGHT];
        public int[,] BlackStaleArray = new int[TableSize.TABLE_WIDTH, TableSize.TABLE_HEIGHT];
        public Stack<int[,]> TableHistory = new Stack<int[,]>();
        private static TimeSpan _playTimeSpan = new TimeSpan(0, 0, 0);
        private int _playTimeMin = 0;
        private int _playTimeSec = 0;
        GameScores _gameScores = new GameScores();
        public InGameForm(bool isSingleGame, bool isHost, string ip = null, int playTimeId = 0)
        {
            InitializeComponent();

            lblBlackResult.Text = "";
            lblWhiteResult.Text = "";
            lblCountDownTime.ForeColor = Color.Green;
            lblCountDownTime.Text = "Wait ...";

            // Nếu chơi có thời gian thì khởi tạo timer
            timer.TimeChanged += () => lblCountDownTime.Text = timer.TimeLeftMsStr;
            timer.CountDownFinished += () =>
            {
                DisplayTimeoutResult(true);
            };

            timer.StepMs = 77;

            if (playTimeId > 0)
            {
                var playTime = PlayTimeManager.PlayTimes
                    .Where(x => x.Id == playTimeId)
                    .FirstOrDefault();

                if (playTime != null)
                {
                    _playTimeMin = playTime.Min;
                    _playTimeSec = playTime.Sec;
                    _playTimeSpan = new TimeSpan(0, playTime.Min, playTime.Sec);
                    timer.SetTime(_playTimeSpan);
                }
            } else
            {
                lblCountDownTime.Text = "";
            }


            _isSingleGame = isSingleGame;
            // its need for the Lan games

            if (!_isSingleGame)
            {
                MessageReceiver.DoWork += MessageReceiver_DoWork;

                if (isHost)
                {
                    // Start the server
                    server = new TcpListener(System.Net.IPAddress.Any, 5732);
                    Thread thread = new Thread(new ThreadStart(() =>
                    {
                        server.Start();

                        while (_isServerStarted)
                        {
                            try
                            {
                                sock = server.AcceptSocket();
                            }
                            catch (SocketException sockEx)
                            {
                                System.Diagnostics.Debug.WriteLine(sockEx.Message);
                                _isServerStarted = false;
                            }
                        }
                    }));

                    thread.Start();
                    _isServerStarted = true;
                    MessageReceiver.RunWorkerAsync();
                }
                else // Client
                {
                    try
                    {
                        client = new TcpClient(ip, 5732);
                        sock = client.Client;
                        SendGetPlayTime();
                        timer.Start();
                        MessageReceiver.RunWorkerAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        Close();
                    }
                }
            }

            // Vẽ nền bàn cờ
            InitBoardGame();

        }

        private void SendGetPlayTime()
        {
            if (sock == null)
                return;

            byte[] data = { NetworkComProtocol.GET_PLAY_TIME };
            sock.Send(data);
        }

        private void DisplayTimeoutResult(bool isTimeOut = false)
        {
            if (isTimeOut)
            {
                // Kết thúc game
                GameOver = true;
                var time = DateTime.Now;
                lblCountDownTime.Text = "Timeout !!!";

                if (_blackPoint > _whitePoint)
                {
                    lblBlackResult.ForeColor = Color.Green;
                    lblBlackResult.Text = "WIN";
                    lblWhiteResult.ForeColor = Color.Red;
                    lblWhiteResult.Text = "LOOSE";
                    _gameScores.WriteToFile(time, "BLACK", _blackPoint, _whitePoint);
                    return;
                }

                if (_blackPoint < _whitePoint)
                {
                    lblBlackResult.ForeColor = Color.Red;
                    lblBlackResult.Text = "LOOSE";
                    lblWhiteResult.ForeColor = Color.Green;
                    lblWhiteResult.Text = "WIN";
                    _gameScores.WriteToFile(time, "WHITE", _blackPoint, _whitePoint);
                    return;
                }


                lblBlackResult.ForeColor = Color.Green;
                lblBlackResult.Text = "PAIR";
                lblWhiteResult.ForeColor = Color.Green;
                lblWhiteResult.Text = "PAIR";
                lblCountDownTime.ForeColor = Color.Red;

                _gameScores.WriteToFile(time, "PAIR", _blackPoint, _whitePoint);

            }
        }

        private void InitBoardGame()
        {
            //this is how every game will start, every pieces have an own number
            tableClass.Table = new int[TableSize.TABLE_WIDTH, TableSize.TABLE_HEIGHT]
            {
                { ChessType.BRook1, ChessType.BKnight1, ChessType.BBishop1, ChessType.BQueen, ChessType.BKing, ChessType.BBishop2, ChessType.BKnight2, ChessType.BRook2},
                { ChessType.BPawn , ChessType.BPawn   , ChessType.BPawn, ChessType.BPawn, ChessType.BPawn, ChessType.BPawn, ChessType.BPawn, ChessType.BPawn},
                { ChessType.Empty , ChessType.Empty   , ChessType.Empty, ChessType.Empty, ChessType.Empty, ChessType.Empty, ChessType.Empty, ChessType.Empty},
                { ChessType.Empty , ChessType.Empty   , ChessType.Empty, ChessType.Empty, ChessType.Empty, ChessType.Empty, ChessType.Empty ,ChessType.Empty},
                { ChessType.Empty , ChessType.Empty   , ChessType.Empty, ChessType.Empty, ChessType.Empty, ChessType.Empty, ChessType.Empty ,ChessType.Empty},
                { ChessType.Empty , ChessType.Empty   , ChessType.Empty, ChessType.Empty, ChessType.Empty, ChessType.Empty, ChessType.Empty ,ChessType.Empty},
                { ChessType.WPawn , ChessType.WPawn   , ChessType.WPawn, ChessType.WPawn, ChessType.WPawn, ChessType.WPawn, ChessType.WPawn, ChessType.WPawn},
                { ChessType.WRook1, ChessType.WKnight1, ChessType.WBishop1, ChessType.WQueen, ChessType.WKing, ChessType.WBishop2, ChessType.WKnight2, ChessType.WRook2},
            };


            TableBackground = new ClickUserClass[TableSize.TABLE_WIDTH, TableSize.TABLE_HEIGHT];
            tableClass.PossibleMoves = new int[TableSize.TABLE_WIDTH, TableSize.TABLE_HEIGHT];
            tableClass.AllPossibleMoves = new int[TableSize.TABLE_WIDTH, TableSize.TABLE_HEIGHT];

            CreateBoardCellEvent();
            ResetBoardColors();
            ResetBoardPossibleMoves();
            DrawBoardPieces();
        }


        private void CreateBoardCellEvent()
        {
            for (int row = 0; row < TableSize.TABLE_HEIGHT; row++)
            {
                for (int col = 0; col < TableSize.TABLE_WIDTH; col++)
                {
                    TableBackground[row, col] = new ClickUserClass();
                    TableBackground[row, col].Parent = this;
                    TableBackground[row, col].Location = new Point(col * 50 + 50, row * 50 + 50);
                    TableBackground[row, col].pozX = row;
                    TableBackground[row, col].pozY = col;
                    TableBackground[row, col].Size = new Size(50, 50);
                    TableBackground[row, col].Click += new EventHandler(ClickUserClass_Click);
                }
            }
        }

        //in this method we check our table int array, and draw the right pieces to the right numbers
        //like 05 is equals to black queen
        //we will call this method after every moves
        // Đặt các quân cờ lên bàn cờ
        public void DrawBoardPieces()
        {
            int row, col;
            for (row = 0; row < TableSize.TABLE_HEIGHT; row++)
            {
                for (col = 0; col < TableSize.TABLE_WIDTH; col++)
                {
                    switch (tableClass.Table[row, col])
                    {
                        case ChessType.Empty:
                            TableBackground[row, col].BackgroundImage
                                = null; break;
                        case ChessType.BPawn:
                            TableBackground[row, col].BackgroundImage
                                = System.Drawing.Image.FromFile("Kepek\\SotetParaszt.png"); break;
                        case ChessType.BRook1:
                            TableBackground[row, col].BackgroundImage
                                = System.Drawing.Image.FromFile("Kepek\\SotetBastya.png"); break;
                        case ChessType.BKnight1:
                            TableBackground[row, col].BackgroundImage
                                = System.Drawing.Image.FromFile("Kepek\\SotetHuszar.png"); break;
                        case ChessType.BBishop1:
                            TableBackground[row, col].BackgroundImage
                                = System.Drawing.Image.FromFile("Kepek\\SotetFuto.png"); break;
                        case ChessType.BQueen:
                            TableBackground[row, col].BackgroundImage
                                = System.Drawing.Image.FromFile("Kepek\\SotetKiralyno.png"); break;
                        case ChessType.BKing:
                            TableBackground[row, col].BackgroundImage
                                = System.Drawing.Image.FromFile("Kepek\\SotetKiraly.png"); break;
                        case ChessType.BBishop2:
                            TableBackground[row, col].BackgroundImage
                                = System.Drawing.Image.FromFile("Kepek\\SotetFuto.png"); break;
                        case ChessType.BKnight2:
                            TableBackground[row, col].BackgroundImage
                                = System.Drawing.Image.FromFile("Kepek\\SotetHuszar.png"); break;
                        case ChessType.BRook2:
                            TableBackground[row, col].BackgroundImage
                                = System.Drawing.Image.FromFile("Kepek\\SotetBastya.png"); break;
                        case ChessType.WPawn:
                            TableBackground[row, col].BackgroundImage
                                = System.Drawing.Image.FromFile("Kepek\\VilagosParaszt.png"); break;
                        case ChessType.WRook1:
                            TableBackground[row, col].BackgroundImage
                                = System.Drawing.Image.FromFile("Kepek\\VilagosBastya.png"); break;
                        case ChessType.WKnight1:
                            TableBackground[row, col].BackgroundImage
                                = System.Drawing.Image.FromFile("Kepek\\VilagosHuszar.png"); break;
                        case ChessType.WBishop1:
                            TableBackground[row, col].BackgroundImage
                                = System.Drawing.Image.FromFile("Kepek\\VilagosFuto.png"); break;
                        case ChessType.WQueen:
                            TableBackground[row, col].BackgroundImage
                                = System.Drawing.Image.FromFile("Kepek\\VilagosKiralyno.png"); break;
                        case ChessType.WKing:
                            TableBackground[row, col].BackgroundImage
                                = System.Drawing.Image.FromFile("Kepek\\VilagosKiraly.png"); break;
                        case ChessType.WBishop2:
                            TableBackground[row, col].BackgroundImage
                                = System.Drawing.Image.FromFile("Kepek\\VilagosFuto.png"); break;
                        case ChessType.WKnight2:
                            TableBackground[row, col].BackgroundImage
                                = System.Drawing.Image.FromFile("Kepek\\VilagosHuszar.png"); break;
                        case ChessType.WRook2:
                            TableBackground[row, col].BackgroundImage
                                = System.Drawing.Image.FromFile("Kepek\\VilagosBastya.png"); break;
                    }
                }
            }
            StaleArrays();
            tableClass.MarkStale(TableBackground, tableClass.Table, WhiteStaleArray, BlackStaleArray);
        }

        //in this event we will give the position of the selected piece to our method
        void ClickUserClass_Click(object sender, EventArgs e)
        {
            AfterClickOnTable((sender as ClickUserClass).pozX, (sender as ClickUserClass).pozY);
        }

        /*in the PossibleMoves array 0 is equals to empty cells, 1 = selectable pieces, 2 is the cells where u can move with your selected piece
         And 3 is the piece that u already selected*/
        public void AfterClickOnTable(int row, int col)
        {
            // Kiểm tra trạng thái di chuyển của quân cờ hiện tại

            if (GameOver)
                return;

            switch (tableClass.PossibleMoves[row, col])
            {
                //Here we selected a piece so it will call the methods that calculate the avalaible moves
                //And we save the location of the selected piece, its will be very important in the future
                case 1:
                    PossibleMovesByPieces(tableClass.Table[row, col], row, col);
                    BeforeMove_I = row;
                    BeforeMove_J = col;
                    break;
                //this case will be true if the player moved somewhere with his piece
                //we reset our "Moves" integers, then call our very fancy method
                case 2:
                    Moves = 0;
                    // Di chuyển quân cờ
                    SuccesfulMove(row, col);
                    break;
                //this is that situation when the player reclick his piece and the board will be clean
                case 3:
                    EndMove();
                    break;
            }
            tableClass.MarkStale(TableBackground, tableClass.Table, WhiteStaleArray, BlackStaleArray);
        }

        //this method will calculate the avalaible moves of the selected piece
        //x is equals to the value of the selected piece, the i and the j is the position of it
        public void PossibleMovesByPieces(int piece, int row, int col)
        {
            //to call this method is important because we have to clear the previously selections, if we choose another pieces
            EndMove();

            //here with the switch we will add the right move positions to our PossibleMoves array as value 2, with use of our classes
            switch (piece)
            {
                case ChessType.BPawn:
                    tableClass.PossibleMoves = blackPawn.GetPossibleMoves(tableClass.Table, tableClass.PossibleMoves, row, col, WhiteTurn, OtherPlayerTurn);
                    break;
                case ChessType.BRook1:
                    tableClass.PossibleMoves = blackRook1.GetPossibleMoves(tableClass.Table, tableClass.PossibleMoves, row, col, WhiteTurn, OtherPlayerTurn);
                    break;
                case ChessType.BKnight1:
                    tableClass.PossibleMoves = blackKnight.GetPossibleMoves(tableClass.Table, tableClass.PossibleMoves, row, col, WhiteTurn, OtherPlayerTurn);
                    break;
                case ChessType.BBishop1:
                    tableClass.PossibleMoves = blackBishop.GetPossibleMoves(tableClass.Table, tableClass.PossibleMoves, row, col, WhiteTurn, OtherPlayerTurn);
                    break;
                case ChessType.BQueen:
                    tableClass.PossibleMoves = blackQueen.GetPossibleMoves(tableClass.Table, tableClass.PossibleMoves, row, col, WhiteTurn, OtherPlayerTurn);
                    break;
                case ChessType.BKing:
                    tableClass.PossibleMoves = blackKing.GetPossibleMoves(tableClass.Table, tableClass.PossibleMoves, row, col, WhiteTurn, BlackKingMoved, BlackRookMoved1, BlackRookMoved2, OtherPlayerTurn);
                    break;
                case ChessType.BRook2:
                    tableClass.PossibleMoves = blackRook2.GetPossibleMoves(tableClass.Table, tableClass.PossibleMoves, row, col, WhiteTurn, OtherPlayerTurn);
                    break;
                case ChessType.BKnight2:
                    tableClass.PossibleMoves = blackKnight.GetPossibleMoves(tableClass.Table, tableClass.PossibleMoves, row, col, WhiteTurn, OtherPlayerTurn);
                    break;
                case ChessType.BBishop2:
                    tableClass.PossibleMoves = blackBishop.GetPossibleMoves(tableClass.Table, tableClass.PossibleMoves, row, col, WhiteTurn, OtherPlayerTurn);
                    break;
                case ChessType.WPawn:
                    tableClass.PossibleMoves = whitePawn.GetPossibleMoves(tableClass.Table, tableClass.PossibleMoves, row, col, WhiteTurn, OtherPlayerTurn);
                    break;
                case ChessType.WRook1:
                    tableClass.PossibleMoves = whiteRook1.GetPossibleMoves(tableClass.Table, tableClass.PossibleMoves, row, col, WhiteTurn, OtherPlayerTurn);
                    break;
                case ChessType.WKnight1:
                    tableClass.PossibleMoves = whiteKnight.GetPossibleMoves(tableClass.Table, tableClass.PossibleMoves, row, col, WhiteTurn, OtherPlayerTurn);
                    break;
                case ChessType.WBishop1:
                    tableClass.PossibleMoves = whiteBishop.GetPossibleMoves(tableClass.Table, tableClass.PossibleMoves, row, col, WhiteTurn, OtherPlayerTurn);
                    break;
                case ChessType.WQueen:
                    tableClass.PossibleMoves = whiteQueen.GetPossibleMoves(tableClass.Table, tableClass.PossibleMoves, row, col, WhiteTurn, OtherPlayerTurn);
                    break;
                case ChessType.WKing:
                    tableClass.PossibleMoves = whiteKing.GetPossibleMoves(tableClass.Table, tableClass.PossibleMoves, row, col, WhiteTurn, WhiteKingMoved, WhiteRookMoved1, WhiteRookMoved2, OtherPlayerTurn);
                    break;
                case ChessType.WRook2:
                    tableClass.PossibleMoves = whiteRook2.GetPossibleMoves(tableClass.Table, tableClass.PossibleMoves, row, col, WhiteTurn, OtherPlayerTurn);
                    break;
                case ChessType.WKnight2:
                    tableClass.PossibleMoves = whiteKnight2.GetPossibleMoves(tableClass.Table, tableClass.PossibleMoves, row, col, WhiteTurn, OtherPlayerTurn);
                    break;
                case ChessType.WBishop2:
                    tableClass.PossibleMoves = whiteBishop2.GetPossibleMoves(tableClass.Table, tableClass.PossibleMoves, row, col, WhiteTurn, OtherPlayerTurn);
                    break;
            }
            //the position of the selected piece will get value 3
            tableClass.PossibleMoves[row, col] = 3;
            //we should not enable impossible moves, like let our king in danger, so we have to validate moves
            RemoveMoveThatNotPossible(piece, row, col);
            //now everything is fine, lets draw the possible moves
            ShowPossibleMoves();
        }
        //in this method we will delete that moves that not enable in chess
        //x is the value of the piece, and now the a and the b is the position, because i was in autopilot and i use "i,j" in for cycles
        public void RemoveMoveThatNotPossible(int x, int a, int b)
        {
            for (int row = 0; row < TableSize.TABLE_HEIGHT; row++)
            {
                for (int col = 0; col < TableSize.TABLE_WIDTH; col++)
                {
                    //we will check every available moves, so every time when the array is 2
                    if (tableClass.PossibleMoves[row, col] == 2)
                    {
                        /*we have to save the original position, because we will simulate that what will happen in every available move position and then
                         we need to restore it*/
                        int SelectedPiece = tableClass.Table[row, col];
                        //here we simulate the new position
                        tableClass.Table[row, col] = x;
                        tableClass.Table[a, b] = 0;
                        //we have 2 arrays with every possible stale situation, and with this method calling we refresh it with our new simulation positions
                        StaleArrays();
                        //this two condition will check that the opponents stale array contains the positon of the king
                        //if theres invalid moves then one of them statement will be true, and then it delete that moves
                        if (tableClass.NotValidMoveChecker(tableClass.Table, WhiteStaleArray, BlackStaleArray) == 1 && WhiteTurn)
                        {
                            tableClass.PossibleMoves[row, col] = 0;
                            if (row == 7 && col == 3 && x == 16)
                            {
                                tableClass.PossibleMoves[7, 2] = 0;
                            }
                        }
                        if (tableClass.NotValidMoveChecker(tableClass.Table, WhiteStaleArray, BlackStaleArray) == 2 && !WhiteTurn)
                        {
                            tableClass.PossibleMoves[row, col] = 0;
                            if (row == 0 && col == 3 && x == 6)
                            {
                                tableClass.PossibleMoves[0, 2] = 0;
                            }
                        }
                        //at here we restore everything
                        tableClass.Table[row, col] = SelectedPiece;
                        tableClass.Table[a, b] = x;
                        StaleArrays();
                    }
                }
            }
        }
        //lets color cells
        public void ShowPossibleMoves()
        {
            int row, col;
            for (row = 0; row < TableSize.TABLE_HEIGHT; row++)
            {
                for (col = 0; col < TableSize.TABLE_WIDTH; col++)
                {
                    //we color the avalaibles move cells to yellow *array value 2*
                    if (tableClass.PossibleMoves[row, col] == 2)
                    {
                       // TableBackground[row, col].BackColor = Color.yellow;
                    }
                    //the background of the selected cell will be blue at here
                    if (tableClass.PossibleMoves[row, col] == 3)
                    {
                        TableBackground[row, col].BackColor = Color.Blue;
                    }
                }
            }
        }

        public void ResetBoardPossibleMoves()
        {
            int row, col;
            //we check cells and if there are pieces that position will get value 1 in the PossibleMoves array
            //its important because you can only click that cells where the value is 1
            for (row = 0; row < TableSize.TABLE_HEIGHT; row++)
            {
                for (col = 0; col < TableSize.TABLE_WIDTH; col++)
                {
                    if (tableClass.Table[row, col] != 0)
                    {
                        tableClass.PossibleMoves[row, col] = 1;
                    }
                    else
                    {
                        tableClass.PossibleMoves[row, col] = 0;
                    }
                }
            }
        }

        public void ResetBoardColors()
        {
            int row, col;

            //tai thiet lap mau ban co 
            for (row = 0; row < TableSize.TABLE_HEIGHT; row++)
            {
                for (col = 0; col < TableSize.TABLE_WIDTH; col++)
                {
                    if (row % 2 == 0)
                    {
                        if (col % 2 == 1)
                        {
                            TableBackground[row, col].BackColor = Color.Brown;
                        }
                        else
                        {
                            TableBackground[row, col].BackColor = Color.White;
                        }
                    }
                    else
                    {
                        if (col % 2 == 1)
                        {
                            TableBackground[row, col].BackColor = Color.White;
                        }
                        else
                        {
                            TableBackground[row, col].BackColor = Color.Brown;
                        }
                    }

                    TableBackground[row, col].BackgroundImageLayout = ImageLayout.Center;
                }
            }
        }

        //lets recolor the board after every click
        public void EndMove()
        {

            ResetBoardPossibleMoves();

            ResetBoardColors();

            // Lưu lại trạng thái cũ
            tableClass.MarkStale(TableBackground, tableClass.Table, WhiteStaleArray, BlackStaleArray);
        }

        //luu buoc di chuyen
        public void StaleArrays()
        {
            //its very simple, we save every possible moves by players
            WhiteStaleArray = new int[TableSize.TABLE_WIDTH, TableSize.TABLE_HEIGHT];
            WhiteStaleArray = blackPawn.IsStale(tableClass.Table, WhiteStaleArray);
            WhiteStaleArray = blackRook1.IsStale(tableClass.Table, WhiteStaleArray);
            WhiteStaleArray = blackRook2.IsStale(tableClass.Table, WhiteStaleArray);
            WhiteStaleArray = blackKnight.IsStale(tableClass.Table, WhiteStaleArray);
            WhiteStaleArray = blackKnight2.IsStale(tableClass.Table, WhiteStaleArray);
            WhiteStaleArray = blackBishop.IsStale(tableClass.Table, WhiteStaleArray);
            WhiteStaleArray = blackBishop2.IsStale(tableClass.Table, WhiteStaleArray);
            WhiteStaleArray = blackQueen.IsStale(tableClass.Table, WhiteStaleArray);

            BlackStaleArray = new int[TableSize.TABLE_WIDTH, TableSize.TABLE_HEIGHT];
            BlackStaleArray = whitePawn.IsStale(tableClass.Table, BlackStaleArray);
            BlackStaleArray = whiteRook1.IsStale(tableClass.Table, BlackStaleArray);
            BlackStaleArray = whiteRook2.IsStale(tableClass.Table, BlackStaleArray);
            BlackStaleArray = whiteKnight.IsStale(tableClass.Table, BlackStaleArray);
            BlackStaleArray = whiteKnight2.IsStale(tableClass.Table, BlackStaleArray);
            BlackStaleArray = whiteBishop.IsStale(tableClass.Table, BlackStaleArray);
            BlackStaleArray = whiteBishop2.IsStale(tableClass.Table, BlackStaleArray);
            BlackStaleArray = whiteQueen.IsStale(tableClass.Table, BlackStaleArray);

        }

        //phương pháp này sẽ xảy ra khi một trong những người chơi thực hiện một nước đi hợp lệ
        //, nó sẽ có được vị trí của quân sau khi di chuyển
        public void SuccesfulMove(int toRow, int toCol)
        {
            enablesocket = true;
            //
            int[,] tmp = (int[,])tableClass.Table.Clone();
            // Lưu lại trạng thái bàn cờ
            TableHistory.Push(tmp);

            // Nếu chơi online thì lấy chỉ mục vị trí trước của quân cờ

            if (!_isSingleGame)
            {
                // Lan, chúng ta phải lưu rằng quân cờ đã được di chuyển
                // điều này rất quan trọng vì chúng ta phải gửi dữ liệu này
                // dưới dạng byte cho kẻ thù bằng socket

                for (int piece = 0; piece < 20; piece++)
                {
                    if (tableClass.Table[BeforeMove_I, BeforeMove_J] == piece)
                    {
                        LastMovedPiece = piece;
                    }
                }
            }
            // kiểm tra xem việc nhập thành (hoán đổi vua rook) có khả thi không
            // và nếu một trong những con tốt đã vượt qua bàn cờ (thăng chức)
            // , chúng ta cũng nên làm điều gì đó

            CastlingAndPawnPromotionChecker(toRow, toCol);

            // Nếu vị trí trước đó chứa quân cờ thì cộng điểm 
            UpdatePoints(toRow, toCol);

            //and this is the point where we give the moved piece to the new position
            tableClass.Table[toRow, toCol] = tableClass.Table[BeforeMove_I, BeforeMove_J];



            //these if-s is for the castling, and we will send the "Castling" integer to the enemy, if we play on Lan
            //if Castling is 1 the black player did castling, if its 2, then the white one did it

            if (tableClass.Table[BeforeMove_I, BeforeMove_J] == ChessType.BKing)
            {
                if (toRow == 0 && toCol == 2)
                {
                    tableClass.Table[0, 3] = ChessType.BRook1;
                    tableClass.Table[0, 0] = ChessType.Empty;
                }
                if (toRow == 0 && toCol == 6)
                {
                    tableClass.Table[0, 5] = ChessType.BRook1;
                    tableClass.Table[0, 7] = ChessType.Empty;
                }
                Castling = 1;
            }

            if (tableClass.Table[BeforeMove_I, BeforeMove_J] == ChessType.WKing)
            {
                if (toRow == 7 && toCol == 2)
                {
                    tableClass.Table[7, 3] = ChessType.WRook1;
                    tableClass.Table[7, 0] = ChessType.Empty;
                }
                if (toRow == 7 && toCol == 6)
                {
                    tableClass.Table[7, 5] = ChessType.WRook1;
                    tableClass.Table[7, 7] = ChessType.Empty;
                }
                Castling = 2;
            }

            //naturally we should null the previously position of the piece
            tableClass.Table[BeforeMove_I, BeforeMove_J] = ChessType.Empty;

            //we call our drawing method
            DrawBoardPieces();
            //we should reset the color of the board at this point as well
            EndMove();
            //now we will save every possible move of the player who will turn in an array
            //that will be important because if that array is null that means the player got checkmate that equals to the game is over
            EveryPossibleMoves();
            //after we got our array we will check that is there checkmate or not
            CheckMateChecker(toRow, toCol);

            //nếu trò chơi Lan của nó, chúng tôi gọi phương thức này với vị trí mới
            if (enablesocket && !_isSingleGame && !GameOver)
            {
                SendMove(toRow, toCol);
            }

            //the opponent turn
            WhiteTurn = !WhiteTurn;
        }

        private int _blackPoint = 0;
        private int _whitePoint = 0;
        private void UpdatePoints(int toRow, int toCol)
        {
            int pointToAdd = 0;

            int oldPiece = tableClass.Table[toRow, toCol];

            if (oldPiece == ChessType.WPawn || oldPiece == ChessType.BPawn)
                pointToAdd = 50;

            if (oldPiece == ChessType.BKnight1
                || oldPiece == ChessType.BKnight2
                || oldPiece == ChessType.WKnight1
                || oldPiece == ChessType.WKnight2)
                pointToAdd = 150;

            if (oldPiece == ChessType.BRook1
                || oldPiece == ChessType.BRook2
                || oldPiece == ChessType.WRook1
                || oldPiece == ChessType.WRook2
                || oldPiece == ChessType.BBishop1
                || oldPiece == ChessType.BBishop2
                || oldPiece == ChessType.WBishop1
                || oldPiece == ChessType.WBishop2)
                pointToAdd = 200;

            if (oldPiece == ChessType.BQueen
                || oldPiece == ChessType.WQueen)
                pointToAdd = 300;

            // Cộng điểm cho quân trắng
            if (oldPiece >= 1 && oldPiece <= 9)
            {
                _whitePoint += pointToAdd;
                Invoke((MethodInvoker)delegate
                {
                    lblWhitePoint.Text = _whitePoint.ToString();
                });
            }

            // Cộng điểm cho quân đen
            if (oldPiece >= 11 && oldPiece <= 19)
            {
                _blackPoint += pointToAdd;
                Invoke((MethodInvoker)delegate
                {
                    lblBlackPoint.Text = _blackPoint.ToString();
                });
            }
        }

        //several bools
        public void CastlingAndPawnPromotionChecker(int i, int j)
        {
            //if one of the player moved with his rook or king then they can not make castling anymore
            //we check this in this switch
            switch (tableClass.Table[BeforeMove_I, BeforeMove_J])
            {
                case ChessType.BRook1:
                    BlackRookMoved1 = false;
                    break;
                case ChessType.BRook2:
                    BlackRookMoved2 = false;
                    break;
                case ChessType.WRook1:
                    WhiteRookMoved1 = false;
                    break;
                case ChessType.WRook2:
                    WhiteRookMoved2 = false;
                    break;
                case ChessType.BKing:
                    BlackKingMoved = false;
                    break;
                case ChessType.WKing:
                    WhiteKingMoved = false;
                    break;
            }

            // kích hoạt một hình thức khác mà chúng tôi có thể chọn
            if (tableClass.Table[BeforeMove_I, BeforeMove_J] == ChessType.BPawn)
            {
                if (i == 7)
                {
                    PromotionForm Promotion = new PromotionForm(false);
                    Promotion.ShowDialog();
                    tableClass.Table[BeforeMove_I, BeforeMove_J] = Promotion.PromotedPiece;
                    Promotionvalue = Promotion.PromotedPiece;
                }
            }
            if (tableClass.Table[BeforeMove_I, BeforeMove_J] == ChessType.WPawn)
            {
                if (i == 0)
                {
                    PromotionForm Promotion = new PromotionForm(true);
                    Promotion.ShowDialog();
                    tableClass.Table[BeforeMove_I, BeforeMove_J] = Promotion.PromotedPiece;
                    Promotionvalue = Promotion.PromotedPiece;
                }
            }
        }
        //nhận được mọi chuyển động của người chơi
        public void EveryPossibleMoves()
        {
            int row = 0;
            int col = 0;
            //we should reset the array after every move
            tableClass.AllPossibleMoves = new int[TableSize.TABLE_WIDTH, TableSize.TABLE_HEIGHT];
            //this need because we need that player who isn't turned yet
            WhiteTurn = !WhiteTurn;
            //pieces
            for (int index = 1; index < TableSize.TABLE_MAX_INDEX; index++)
            {
                //and positions
                for (row = 0; row < TableSize.TABLE_HEIGHT; row++)
                {
                    for (col = 0; col < TableSize.TABLE_WIDTH; col++)
                    {
                        if (tableClass.Table[row, col] == index)
                        {
                            //similar like earlier switchs
                            switch (index)
                            {
                                case ChessType.BPawn:
                                    tableClass.AllPossibleMoves = blackPawn.GetPossibleMoves(tableClass.Table, tableClass.AllPossibleMoves, row, col, WhiteTurn, OtherPlayerTurn);
                                    break;
                                case ChessType.BRook1:
                                    tableClass.AllPossibleMoves = blackRook1.GetPossibleMoves(tableClass.Table, tableClass.AllPossibleMoves, row, col, WhiteTurn, OtherPlayerTurn);
                                    break;
                                case ChessType.BKnight1:
                                    tableClass.AllPossibleMoves = blackKnight.GetPossibleMoves(tableClass.Table, tableClass.AllPossibleMoves, row, col, WhiteTurn, OtherPlayerTurn);
                                    break;
                                case ChessType.BBishop1:
                                    tableClass.AllPossibleMoves = blackBishop.GetPossibleMoves(tableClass.Table, tableClass.AllPossibleMoves, row, col, WhiteTurn, OtherPlayerTurn);
                                    break;
                                case ChessType.BQueen:
                                    tableClass.AllPossibleMoves = blackQueen.GetPossibleMoves(tableClass.Table, tableClass.AllPossibleMoves, row, col, WhiteTurn, OtherPlayerTurn);
                                    break;
                                case ChessType.BKing:
                                    tableClass.AllPossibleMoves = blackKing.GetPossibleMoves(tableClass.Table, tableClass.AllPossibleMoves, row, col, WhiteTurn, BlackKingMoved, BlackRookMoved1, BlackRookMoved2, OtherPlayerTurn);
                                    break;
                                case ChessType.BRook2:
                                    tableClass.AllPossibleMoves = blackRook2.GetPossibleMoves(tableClass.Table, tableClass.AllPossibleMoves, row, col, WhiteTurn, OtherPlayerTurn);
                                    break;
                                case ChessType.BKnight2:
                                    tableClass.AllPossibleMoves = blackKnight.GetPossibleMoves(tableClass.Table, tableClass.AllPossibleMoves, row, col, WhiteTurn, OtherPlayerTurn);
                                    break;
                                case ChessType.BBishop2:
                                    tableClass.AllPossibleMoves = blackBishop2.GetPossibleMoves(tableClass.Table, tableClass.AllPossibleMoves, row, col, WhiteTurn, OtherPlayerTurn);
                                    break;
                                case ChessType.WPawn:
                                    tableClass.AllPossibleMoves = whitePawn.GetPossibleMoves(tableClass.Table, tableClass.AllPossibleMoves, row, col, WhiteTurn, OtherPlayerTurn);
                                    break;
                                case ChessType.WRook1:
                                    tableClass.AllPossibleMoves = whiteRook1.GetPossibleMoves(tableClass.Table, tableClass.AllPossibleMoves, row, col, WhiteTurn, OtherPlayerTurn);
                                    break;
                                case ChessType.WKnight1:
                                    tableClass.AllPossibleMoves = whiteKnight.GetPossibleMoves(tableClass.Table, tableClass.AllPossibleMoves, row, col, WhiteTurn, OtherPlayerTurn);
                                    break;
                                case ChessType.WBishop1:
                                    tableClass.AllPossibleMoves = whiteBishop.GetPossibleMoves(tableClass.Table, tableClass.AllPossibleMoves, row, col, WhiteTurn, OtherPlayerTurn);
                                    break;
                                case ChessType.WQueen:
                                    tableClass.AllPossibleMoves = whiteQueen.GetPossibleMoves(tableClass.Table, tableClass.AllPossibleMoves, row, col, WhiteTurn, OtherPlayerTurn);
                                    break;
                                case ChessType.WKing:
                                    tableClass.AllPossibleMoves = whiteKing.GetPossibleMoves(tableClass.Table, tableClass.AllPossibleMoves, row, col, WhiteTurn, WhiteKingMoved, WhiteRookMoved1, WhiteRookMoved2, OtherPlayerTurn);
                                    break;
                                case ChessType.WRook2:
                                    tableClass.AllPossibleMoves = whiteRook2.GetPossibleMoves(tableClass.Table, tableClass.AllPossibleMoves, row, col, WhiteTurn, OtherPlayerTurn);
                                    break;
                                case ChessType.WKnight2:
                                    tableClass.AllPossibleMoves = whiteKnight2.GetPossibleMoves(tableClass.Table, tableClass.AllPossibleMoves, row, col, WhiteTurn, OtherPlayerTurn);
                                    break;
                                case ChessType.WBishop2:
                                    tableClass.AllPossibleMoves = whiteBishop2.GetPossibleMoves(tableClass.Table, tableClass.AllPossibleMoves, row, col, WhiteTurn, OtherPlayerTurn);
                                    break;
                            }
                            //okay we got moves by pieces, now we should delete that are invalids
                            RemoveMoveThatNotPossible2(index, row, col);
                        }
                    }
                }
            }
            //and now we change back it
            WhiteTurn = !WhiteTurn;
        }
        //cho phép xóa các nước đi không hợp lệ
        public void RemoveMoveThatNotPossible2(int x, int a, int b)
        {
            //you already know this
            for (int row = 0; row < TableSize.TABLE_HEIGHT; row++)
            {
                for (int col = 0; col < TableSize.TABLE_WIDTH; col++)
                {
                    //you can remember that value 2 is the possible moves
                    if (tableClass.AllPossibleMoves[row, col] == 2)
                    {
                        //and this is very familiar than the "RemoveMoveThatNotPossible", lets simulate
                        int lastHitPiece = tableClass.Table[row, col];

                        tableClass.Table[row, col] = x;
                        tableClass.Table[a, b] = 0;
                        StaleArrays();
                        //invalid move
                        if (tableClass.NotValidMoveChecker(tableClass.Table, WhiteStaleArray, BlackStaleArray) == 1 && WhiteTurn)
                        {
                            tableClass.AllPossibleMoves[row, col] = 0;
                        }
                        //invalid move
                        if (tableClass.NotValidMoveChecker(tableClass.Table, WhiteStaleArray, BlackStaleArray) == 2 && !WhiteTurn)
                        {
                            tableClass.AllPossibleMoves[row, col] = 0;
                        }
                        //this is a valid move, so we increment the Moves integer
                        //if Moves not equals to 0, than its not checkmate
                        if (tableClass.NotValidMoveChecker(tableClass.Table, WhiteStaleArray, BlackStaleArray) == 3)
                        {
                            Moves++;
                        }
                        tableClass.Table[row, col] = lastHitPiece;
                        tableClass.Table[a, b] = x;
                        StaleArrays();
                    }
                }
            }
            tableClass.AllPossibleMoves = new int[TableSize.TABLE_WIDTH, TableSize.TABLE_HEIGHT];
        }

        //Quyết định rằng trò chơi có kết thúc hay không
        public void CheckMateChecker(int a, int b)
        {
            if (Moves == 0)
            {
                GameOver = true;
                //trò chơi kết thúc, nhưng tuy nhiên chúng tôi gửi cho đối thủ
                if (enablesocket && !_isSingleGame)
                {
                    SendMove(a, b);
                }

                if (WhiteTurn)
                {
                    _gameScores.WriteToFile(DateTime.Now, "BLACK", _blackPoint, _whitePoint);
                } else
                {
                    _gameScores.WriteToFile(DateTime.Now, "WHITE", _blackPoint, _whitePoint);
                }
                
                MessageBox.Show("You Win!");
            }
        }
        //socket things
        private void MessageReceiver_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (GameOver)
                    continue;

                if (sock == null)
                    continue;

                byte[] buffer = new byte[200];
                sock.Receive(buffer);
                switch (buffer[0])
                {
                    case NetworkComProtocol.MOV_CMD: //Di chuyen
                        ReceiveMove(buffer);
                        break;
                    case NetworkComProtocol.UNDO_REQ: //Yeu cau UNDO
                        DialogResult result
                            = MessageBox.Show(
                                "Đối thủ muốn đi lại"
                                , "Thông báo"
                                , MessageBoxButtons.YesNo
                                , MessageBoxIcon.Exclamation);

                        switch (result)
                        {
                            case DialogResult.Yes:
                                //Phan hoi
                                byte[] data = { NetworkComProtocol.UNDO_ACCEPT };
                                sock.Send(data);
                                this.undo();
                                //TODO: thuc hien undo
                                break;
                            case DialogResult.No:
                                break;
                        }
                        break;
                    case NetworkComProtocol.UNDO_ACCEPT:
                        //Thuc hien UNDO
                        this.undo();
                        //MessageBox.Show("Da UNDO");
                        break;
                    case NetworkComProtocol.PAIR_REQ: // Yêu cầu đầu hàng
                        result = MessageBox.Show("Đối thủ muốn đầu hàng ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                        switch (result)
                        {
                            case DialogResult.Yes:
                                byte[] data = { NetworkComProtocol.PAIR_ACCEPT };
                                sock.Send(data);
                                Invoke((MethodInvoker)delegate
                                {
                                    if (_playTimeMin > 0 || _playTimeSec > 0)
                                        timer.Stop();

                                    GameOver = true;

                                    if (WhiteTurn)
                                    {
                                        _gameScores.WriteToFile(DateTime.Now, "WHITE", _blackPoint, _whitePoint);
                                    }
                                    else
                                    {
                                        _gameScores.WriteToFile(DateTime.Now, "BLACK", _blackPoint, _whitePoint);
                                    }

                                    MessageBox.Show("You Win !!!");
                                });
                                break;
                            case DialogResult.No:
                                break;
                        }
                        break;
                    case NetworkComProtocol.PAIR_ACCEPT:
                        Invoke((MethodInvoker)delegate
                        {
                            if (_playTimeMin > 0 || _playTimeSec > 0)
                                timer.Stop();
                            GameOver = true;
                            
                            if (WhiteTurn)
                            {
                                _gameScores.WriteToFile(DateTime.Now, "WHITE", _blackPoint, _whitePoint);
                            }
                            else
                            {
                                _gameScores.WriteToFile(DateTime.Now, "BLACK", _blackPoint, _whitePoint);
                            }

                            MessageBox.Show("You Loose !!!");
                        });
                        break;
                    case NetworkComProtocol.GET_PLAY_TIME:
                        if (_playTimeMin > 0 || _playTimeSec > 0)
                        {
                            byte[] data = { NetworkComProtocol.SET_PLAY_TIME, (byte)_playTimeMin, (byte)_playTimeSec };
                            sock.Send(data);
                            // Start Timer
                            Invoke((MethodInvoker)delegate
                            {
                                timer.Start();
                            });
                        }
                        break;
                    case NetworkComProtocol.SET_PLAY_TIME:
                        _playTimeMin = buffer[1];
                        _playTimeSec = buffer[2];
                        _playTimeSpan = new TimeSpan(0, _playTimeMin, _playTimeSec);
                        timer.SetTime(_playTimeSpan);
                        break;
                }
            }
        }
        //và đây gửi dữ liệu bằng socket
        private void SendMove(int i, int j)
        {
            if (sock == null)
                return;

            // gửi quân cờ đã di chuyển và vị trí của nó, vị trí mới, bộ đếm lượt kiểm tra và giá trị của việc nhập thành, và phong cấp
            //0 loai du lieu
            byte[] datas = { NetworkComProtocol.MOV_CMD, (byte)LastMovedPiece, (byte)BeforeMove_I, (byte)BeforeMove_J, (byte)i, (byte)j, (byte)Moves, (byte)Castling, (byte)Promotionvalue };

            sock.Send(datas);
            //Khong can MessageReceiver_DoWork da duoc dua vao while
            //MessageReceiver.DoWork += MessageReceiver_DoWork;
            //if (!MessageReceiver.IsBusy)
            //{
            //    MessageReceiver.RunWorkerAsync();
            //}
            OtherPlayerTurn = true;

        }

        //đây là nơi người chơi khác có dữ liệu
        private void ReceiveMove(byte[] buffer)
        {

            //khoi tao gia tri undo
            int[,] tmp = (int[,])tableClass.Table.Clone();
            TableHistory.Push(tmp);
            //vị trí trước đây phải là 0
            tableClass.Table[buffer[2], buffer[3]] = 0;
            // Cập nhật điểm số
            UpdatePoints(buffer[4], buffer[5]);
            //vị trí mới với piece
            tableClass.Table[buffer[4], buffer[5]] = buffer[1];
            //nhập thành
            /*if (buffer[7] == 1)
            {
                if (buffer[5] == 2)
                {
                    tableClass.Table[0, 3] = ChessType.BRook1;
                    tableClass.Table[0, 0] = ChessType.Empty;
                }
                if (buffer[5] == 6)
                {
                    tableClass.Table[0, 5] = ChessType.BRook1;
                    tableClass.Table[0, 7] = ChessType.Empty;
                }
            }
            if (buffer[7] == 2)
            {
                if (buffer[5] == 2)
                {
                    tableClass.Table[7, 3] = ChessType.WRook1;
                    tableClass.Table[7, 0] = 0;
                }
                if (buffer[5] == 6)
                {
                    tableClass.Table[7, 5] = ChessType.WRook1;
                    tableClass.Table[7, 7] = ChessType.Empty;
                }
            }*/
            //được thăng chức
            if (buffer[8] > 0)
            {
                tableClass.Table[buffer[4], buffer[5]] = buffer[8];
            }
            //chuyển đổi lần lượt
            WhiteTurn = !WhiteTurn;
            //tôi nghĩ chúng tôi biết điều này
            DrawBoardPieces();
            StaleArrays();
            tableClass.MarkStale(TableBackground, tableClass.Table, WhiteStaleArray, BlackStaleArray);
            OtherPlayerTurn = false;
            //mất màn hình
            if (buffer[6] == 0)
            {
                if (WhiteTurn)
                {
                    MessageBox.Show("You Lost!");
                }
                else
                {
                    MessageBox.Show("You lost!");
                }
            }

        }

        //kết thúc kết nối
        private void InGameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageReceiver.WorkerSupportsCancellation = true;
            MessageReceiver.CancelAsync();
            if (server != null)
                server.Stop();
        }


        //Button UNDO
        private void button1_Click(object sender, EventArgs e)
        {
            if (!_isSingleGame)
            {
                if (sock == null)
                    return;

                MessageBox.Show("Gửi yêu cầu Undo");
                byte[] data = { NetworkComProtocol.UNDO_REQ };
                sock.Send(data);
            }
            else
            {
                this.undo();
            }

        }

        //ham de undo 
        private void undo()
        {
            if (TableHistory.Count == 0)
            {
                return;
            }
            // dung pop de day tu stack ra
            tableClass.Table = TableHistory.Pop();
            DrawBoardPieces();
            StaleArrays();
            tableClass.MarkStale(TableBackground, tableClass.Table, WhiteStaleArray, BlackStaleArray);
            OtherPlayerTurn = !OtherPlayerTurn;
        }

        private void InGameForm_Load(object sender, EventArgs e)
        {

        }

        private void btnDauHang_Click(object sender, EventArgs e)
        {
            if (!_isSingleGame)
            {
                if (GameOver)
                    return;

                if (sock == null)
                    return;

                MessageBox.Show("Gửi yêu cầu đầu hàng");
                byte[] data = { NetworkComProtocol.PAIR_REQ };
                sock.Send(data);
            }
        }

        private void btnLoadHistory_Click(object sender, EventArgs e)
        {
            txtTableHistoryLog.Text = string.Empty;
            foreach (var item in TableHistory)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        txtTableHistoryLog.Text += item[i, j] + "   ";
                    }
                    txtTableHistoryLog.Text += "\r\n";
                }
                txtTableHistoryLog.Text += "=============\r\n";
            }
        }
    }
}

