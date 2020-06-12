using UnityEngine;
using UnityEngine.EventSystems;

using SpearHorseAndGlory.EventBusSystem;

namespace SpearHorseAndGlory.UI
{
    [AddComponentMenu("SpearHorseAndGlory/UI/SimpleButton")]
    public sealed class SimpleUIButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public ButtonType buttonType = ButtonType.NullType;

        public void OnPointerDown(PointerEventData eventData)
        {
            EventBus.Instance.PostEvent(new ButtonDataEvent(gameObject, buttonType, true));
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            EventBus.Instance.PostEvent(new ButtonDataEvent(gameObject, buttonType, false));
        }
    }
}
