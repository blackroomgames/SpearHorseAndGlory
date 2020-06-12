using UnityEngine;
using SpearHorseAndGlory.EventBusSystem;

namespace SpearHorseAndGlory.Components
{
    [AddComponentMenu("SpearHorseAndGlory/MainComponent/HorsemanComponents/PlayerHorseman")]
    public sealed class PlayerHorsemanComponent : HorsemanComponent
    {
        [SerializeField]private float _upForceTimer;
        [SerializeField]private float _currentAngle;

        protected override void SetupComponentInEventBus()
        {
            base.SetupComponentInEventBus();
            EventBus.Instance.RegisterListnerEvent(typeof(InputDataEvent), new EventListner<InputDataEvent>(AddSpearUpForce, true));
        }

        protected override void CombatMove()
        {
            base.CombatMove();
            var map = spearSetting.CombatSpearRotation(spear, _upForceTimer, _currentAngle);
            _upForceTimer = map.Item1;
            _currentAngle = map.Item2;
        }

        private void AddSpearUpForce(InputDataEvent inputData)
        {
            if (!isCombat) return;
            Debug.Log("Is Player!");
            _upForceTimer = Random.Range(0.3f, 0.5f);
        }
    }
}