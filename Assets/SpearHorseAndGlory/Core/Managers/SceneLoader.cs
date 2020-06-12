using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SpearHorseAndGlory.EventBusSystem;

namespace SpearHorseAndGlory.System
{
    internal sealed class SceneLoader
    {
        internal SceneLoader()
        {
            EventBus.Instance.RegisterListnerEvent(typeof(LoadSceneDataEvent), new EventListner<LoadSceneDataEvent>(LoadGameScene));
        }

        private void LoadGameScene(LoadSceneDataEvent sceneData)
        {
            Debug.Log("@@@ SceneLoad");
            EventBus.Instance.CleanRemovableListnerEvent.Invoke();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

