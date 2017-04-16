﻿using chesslib.Field;
using chesslib.Player;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace chesslib
{
    public abstract class Piece : IObservable<Piece>
    {
        public Cell CurrentCell { get; set; }
        public PlayerType PlayerType { get; set; }
        public bool IsInGame { get; set; }

        public Piece(Cell currentCell, PlayerType playerType)
        {
            CurrentCell = currentCell;
            if (CurrentCell.Piece == null)
                CurrentCell.Piece = this;
            PlayerType = playerType;
            _observers = new List<IObserver<Piece>>();
            IsInGame = true;
        }

        protected int Direction
        {
            get
            {
                if (PlayerType == PlayerType.White)
                    return 1;
                return -1;
            }
        }

        public virtual bool MoveTo(Cell cell, IPlayer player)
        {
            Update(this);
            return true;
        }

        protected bool CheckPlayer(IPlayer player)
        {
            if (player.PlayerType == PlayerType)
                return true;
            return false;
        }
        public abstract List<Cell> GetAllowedMoves();

        #region IObservable

        private List<IObserver<Piece>> _observers;
        public IDisposable Subscribe(IObserver<Piece> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
            return new Unsubscriber(_observers, observer);
        }

        public void Update(Piece loc)
        {
            foreach (var observer in _observers)
            {
                if (loc == null)
                    observer.OnError(new Exception("Null piece"));
                else
                    observer.OnNext(loc);
            }
        }

        public void EndUpdates()
        {
            foreach (var observer in _observers)
            {
                if (_observers.Contains(observer))
                    observer.OnCompleted();

                _observers.Clear();
            }
        }
        #endregion
    }
}
