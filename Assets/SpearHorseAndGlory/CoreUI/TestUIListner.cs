using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SpearHorseAndGlory.UI;
using SpearHorseAndGlory.EventBusSystem;

public class TestUIListner : MonoBehaviour
{
    public string nameListner;

    private void Start()
    {
        EventBus.Instance.RegisterListnerEvent(typeof(ButtonDataEvent), new EventListner<ButtonDataEvent>(TestCallbackSimpleButton, isRemovable: true));
    }

    private void TestCallbackSimpleButton(ButtonDataEvent buttonEvent)
    {
        switch (buttonEvent.buttonType)
        {
            case (ButtonType.Menu):
                Debug.Log($"Is Menu type - {nameListner} = {buttonEvent.isPressed}");
                Debug.ClearDeveloperConsole();
                EventBus.Instance.PostEvent(new LoadSceneDataEvent());
                break;
            case (ButtonType.Settings):
                Debug.Log($"Is Settings type - {nameListner} = {buttonEvent.isPressed}");
                break;
            default:
                Debug.Log("No have menu or type for button!");
                break;
        }
    }
}
