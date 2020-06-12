using System;
using UnityEngine;
using SpearHorseAndGlory.EventBusSystem;

namespace SpearHorseAndGlory.UI
{
    internal sealed class GameCanvasContoller : MonoBehaviour
    {
        private void Start()
        {
            EventBus.Instance.RegisterListnerEvent(typeof(ButtonDataEvent), new EventListner<ButtonDataEvent>(ButtonCallBackProcessing));
        }

        private void ButtonCallBackProcessing(ButtonDataEvent buttonData)
        {
            switch (buttonData.buttonType)
            {
                case (ButtonType.StartCombat):
                    var buttonGO = buttonData.button as GameObject;
                    buttonGO.SetActive(false);
                    EventBus.Instance.PostEvent(new MovementDataEvent(true));
                    break;
                case (ButtonType.FakeTouch):
                    if (buttonData.isPressed)
                    {
                        Debug.Log("TEST!");
                        EventBus.Instance.PostEvent(new InputDataEvent());
                    }
                    break;
                default:
                    break;
            }
        }
    }
}