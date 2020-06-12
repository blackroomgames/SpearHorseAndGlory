using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SpearHorseAndGlory.Links;
using SpearHorseAndGlory.EventBusSystem;


namespace SpearHorseAndGlory.Components
{
    [RequireComponent(typeof(MovementComponent))]
    public abstract class HorsemanComponent : MonoBehaviour
    {
        public SpearSettingSO spearSetting;

        private Transform _rider;
        private Transform _riderPoint;
        private bool _isIdle;

        protected Transform spear;
        protected bool isCombat;

        private void Start()
        {
            SetupComponentInEventBus();

            _rider = this.GetComponentInChildren<RiderLink>().transform;
            _riderPoint = this.GetComponentInChildren<RiderPointLink>().transform;
            spear = this.GetComponentInChildren<SpearLink>().transform;

            _rider.SetParent(_riderPoint);
            _rider.localPosition = Vector3.zero;

            spearSetting.SpearSetStartPosition(spear);
            _isIdle = true;
        }

        private void FixedUpdate()
        {
            if (_isIdle)
            {
                spearSetting.SpearNoiseInIdlePosition(spear);
            }
            else
            {
                CombatMove();
            }
        }

        protected virtual void SetupComponentInEventBus()
        {
            EventBus.Instance.RegisterListnerEvent(typeof(MovementDataEvent), new EventListner<MovementDataEvent>(MovementAction, true));
        }


        protected virtual void MovementAction(MovementDataEvent movementData)
        {
            spearSetting.SpearSetStartPosition(spear);
            _isIdle = false;
            //start take aim
            StartCoroutine(HorsemanTakeAim());
        }

        protected virtual void CombatAction()
        {
            isCombat = true;
        }

        protected virtual void CombatMove()
        {
            
        }

        private IEnumerator HorsemanTakeAim()
        {
            bool startTakeAim = true;
            while (startTakeAim)
            {
                yield return new WaitForFixedUpdate();
                startTakeAim = !spearSetting.SpearTakeAim(spear);
            }
            CombatAction();
        }
    }
}