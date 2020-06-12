using System;
using System.Collections.Generic;

using UnityEngine;

namespace SpearHorseAndGlory.EventBusSystem
{
    internal sealed class EventBus
    {
        internal static EventBus Instance { get { return _instance ?? (_instance = new EventBus()); } }

        public readonly Action<Type, IEventListner> RegisterListnerEvent;
        public readonly Action<Type, int> UnregisterListnerEvent;
        public readonly Action<object> PostEvent;
        public readonly Action CleanRemovableListnerEvent;

        private static EventBus _instance;
        private Dictionary<Type, List<IEventListner>> _listners;

        private EventBus()
        {
            RegisterListnerEvent = RegisterListner;
            UnregisterListnerEvent = UnregisterListner;
            PostEvent = Post;
            CleanRemovableListnerEvent = CleanRemovableListner;

            _listners = new Dictionary<Type, List<IEventListner>>();
        }

        private void RegisterListner(Type messageType, IEventListner listner)
        {
            List<IEventListner> currentList;

            if (!_listners.TryGetValue(messageType, out currentList))
            {
                currentList = new List<IEventListner>();
                _listners.Add(messageType, currentList);
            }
            if (!currentList.Contains(listner))
            {
                Debug.Log($"@@@ - {messageType}");
                currentList.Add(listner);
            }
        }

        private void UnregisterListner(Type messageType, int listnerHash)
        {
            List<IEventListner> currentList;

            if (_listners.TryGetValue(messageType, out currentList))
            {
                for(int i = 0; i < currentList.Count; ++i)
                {
                    if (currentList[i].CheckListnerByHash(listnerHash))
                    {
                        Debug.Log($"@@@ Unregistr - {listnerHash}");
                        currentList.Remove(currentList[i--]);
                    }
                }
            }
        }

        private void Post(object message)
        {
            List<IEventListner> currentListnersList;

            if (_listners.TryGetValue(message.GetType(), out currentListnersList))
            {
                for (int i = 0; i < currentListnersList.Count; ++i)
                {
                    currentListnersList[i].PostEvent(message);
                }
            }
        }

        private void CleanRemovableListner()
        {
            foreach(var list in _listners)
            {
                for(int i = 0; i < list.Value.Count; ++i)
                {
                    if (list.Value[i].IsRemovable())
                    {
                        list.Value.Remove(list.Value[i--]);
                    }
                }
            }
        }
    }
}

