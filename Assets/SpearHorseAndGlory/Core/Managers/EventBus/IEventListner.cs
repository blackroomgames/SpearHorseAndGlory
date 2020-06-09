using System;

namespace SpearHorseAndGlory.EventBusSystem
{
    internal interface IEventListner
    {
        void PostEvent(object eventObject);
        bool CheckListnerByHash(int hash);
        bool IsRemovable();
    }
}