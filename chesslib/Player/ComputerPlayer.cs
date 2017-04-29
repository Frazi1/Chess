﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using chesslib.Command;
using chesslib.Strategy;
using System.Threading;
using chesslib.Events;

namespace chesslib.Player
{
    public class ComputerPlayer : IPlayer
    {
        public PlayerColor PlayerColor { get; set; }
        public MakeMoveCommand MakeMoveCommand { get; set; }
        public IStrategy Strategy { get; set; }
        public Game Game { get; set; }

        public Thread CurrentThread { get; private set; }

        public event EventsDelegates.MoveDoneEventHandler MoveDone;

        public ComputerPlayer(PlayerColor playerType)
        {
            PlayerColor = playerType;
        }

        private void MakeMove()
        {
            Thread.Sleep(500);
            var move = Strategy.PrepareMove();
            MakeMoveCommand = new MakeMoveCommand(this, move.Item1, move.Item2);
            if (MoveDone != null)
                MoveDone(this, new MoveDoneEventArgs(MakeMoveCommand));

            OnMove();

        }

        private void OnMove()
        {
            MakeMoveCommand = null;
        }

        public void DoTurn()
        {
            Thread CurrentThread = new Thread(MakeMove)
            {
                IsBackground = true
            };
            CurrentThread.Start();
        }

        public void CancelTurn()
        {
            if (CurrentThread!=null && CurrentThread.IsAlive)
                CurrentThread.Abort();
        }
    }
}
