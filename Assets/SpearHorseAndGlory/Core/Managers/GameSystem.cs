using UnityEngine;

using SpearHorseAndGlory.UI;

namespace SpearHorseAndGlory.System
{
    internal sealed class GameSystem : SingletonMonoBehavior<GameSystem>
    {
        [RuntimeInitializeOnLoadMethod]
        public static void InitializeGameSystem()
        {
            var gameSystem = GameSystem.Instance.gameObject;
            gameSystem.name = "~GAME_SYSTEM~";
            DontDestroyOnLoad(gameSystem);

            //
            var eventBus = EventBusSystem.EventBus.Instance;
            //
            //var inputSystem = KeyAndMouseInputSystem.Instance.gameObject;
            //
            new SceneLoader();
            //
            CreateCanvasController();
        }

        private static void CreateCanvasController()
        {
            var canvasGo = new GameObject("~GAME_CANVAS_CONTROLLER~");
            canvasGo.AddComponent<GameCanvasContoller>();
            DontDestroyOnLoad(canvasGo);
        }
    }
}

