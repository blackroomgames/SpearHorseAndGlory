using UnityEngine;

namespace SpearHorseAndGlory.System
{
    internal sealed class MobileInputSystem : BaseInputSystem<MobileInputSystem>
    {
        private void Update()
        {
            if(Input.touchCount > 0)
            {
                Debug.Log("Touch");
                SendInputData();
            }
        }
    }
}
