using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpearHorseAndGlory.EventBusSystem;


namespace SpearHorseAndGlory.Controllers
{
    public sealed class PlayerSpear : MonoBehaviour
    {
        private const float MaxSpearAngle = 10f;
        private const float MinSpearAngle = -10f;

        private bool _isMove;
        public float currentAngle;
        public float upForceTimer;

        [SerializeField] private Vector3 _spearRotation;

        private void Start()
        {
            _spearRotation = transform.eulerAngles;
            EventBus.Instance.RegisterListnerEvent(typeof(MovementDataEvent), new EventListner<MovementDataEvent>(MovementAction, true));
            EventBus.Instance.RegisterListnerEvent(typeof(InputDataEvent), new EventListner<InputDataEvent>(AddSpearUpForce, true));
        }

        private void FixedUpdate()
        {
            
            if (_isMove)
            {
                if(upForceTimer > 0f)
                {
                    currentAngle -= Mathf.Lerp(0.4f, MinSpearAngle, Time.fixedDeltaTime);
                    upForceTimer -= Time.fixedDeltaTime;
                }
                else
                {
                    currentAngle += Mathf.Lerp(0.6f, MinSpearAngle, Time.fixedDeltaTime);
                }
                currentAngle = Mathf.Clamp(currentAngle, MinSpearAngle, MaxSpearAngle);
                _spearRotation.z = currentAngle;
                transform.localRotation = Quaternion.Euler(_spearRotation);
            }
        }

        private void AddSpearUpForce(InputDataEvent inputData)
        {
            upForceTimer = Random.Range(0.3f, 0.5f);
        }

        private void MovementAction(MovementDataEvent movementDataEvent)
        {
            _isMove = movementDataEvent.isMove;
        }
    }
}

