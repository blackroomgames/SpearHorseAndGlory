using SpearHorseAndGlory.EventBusSystem;

namespace SpearHorseAndGlory.System
{
    internal abstract class BaseInputSystem<T> : SingletonMonoBehavior<T>
        where T: UnityEngine.MonoBehaviour
    {
        protected virtual void SendInputData()
        {
            EventBus.Instance.PostEvent(new InputDataEvent());
        }
    }
}
