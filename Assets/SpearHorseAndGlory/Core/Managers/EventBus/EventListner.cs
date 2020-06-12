using System;

namespace SpearHorseAndGlory.EventBusSystem
{
    internal readonly struct EventListner<T> : IEventListner
    {
        internal readonly bool isRemovable;

        private readonly int _listnerHash;
        private readonly Action<T> ListnerAction;

        internal EventListner(Action<T> listnerAction, bool isRemovable = false)
        {
            ListnerAction = listnerAction;
            _listnerHash = listnerAction.Target.GetHashCode();
            this.isRemovable = isRemovable;
        }

        public void PostEvent(object eventObject)
        {
            ListnerAction?.Invoke((T)eventObject);
        }

        public bool CheckListnerByHash(int hash)
        {
            return _listnerHash == hash;
        }

        public bool IsRemovable()
        {
            return this.isRemovable;
        }
    }
}

