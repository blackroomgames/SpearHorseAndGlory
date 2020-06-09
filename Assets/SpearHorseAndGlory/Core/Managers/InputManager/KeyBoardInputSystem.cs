using UnityEngine;

namespace SpearHorseAndGlory.System
{
    internal sealed class KeyAndMouseInputSystem: BaseInputSystem<KeyAndMouseInputSystem>
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                SendInputData();
            }
        }
    }
}