using System;
using System.Collections;
using System.Collections.Generic;

namespace chesslib.Command
{
    [Serializable]
    public class MoveCommands : ICollection<MakeMoveCommand>, IEnumerable<MakeMoveCommand>
    {
        public List<MakeMoveCommand> MakeMoveCommands { get; set; }
        public int Count
        {
            get
            {
                return MakeMoveCommands.Count;
            }
        }
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }
        public MakeMoveCommand this[int index]
        {
            get { return MakeMoveCommands[index]; }
        }
        public MoveCommands()
        {
            MakeMoveCommands = new List<MakeMoveCommand>();
        }
        public void Add(MakeMoveCommand item)
        {
            MakeMoveCommands.Add(item);
        }
        public void Clear()
        {
            MakeMoveCommands.Clear();
        }
        public bool Contains(MakeMoveCommand item)
        {
            return MakeMoveCommands.Contains(item);
        }
        public void CopyTo(MakeMoveCommand[] array, int arrayIndex)
        {
            MakeMoveCommands.CopyTo(array, arrayIndex);
        }
        public bool Remove(MakeMoveCommand item)
        {
            return MakeMoveCommands.Remove(item);
        }
        public void RemoveAt(int index)
        {
            MakeMoveCommands.RemoveAt(index);
        }
        public IEnumerator<MakeMoveCommand> GetEnumerator()
        {
            return MakeMoveCommands.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}