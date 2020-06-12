using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpearHorseAndGlory.EventBusSystem;

namespace SpearHorseAndGlory.Components
{
    [RequireComponent(typeof(Rigidbody))]
    [AddComponentMenu("SpearHorseAndGlory/MainComponent/MovementComponent")]
    public class MovementComponent : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private Transform _self;
        private bool _isMove;
        public float moveSpeed;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.useGravity = false;
            _self = this.gameObject.transform;

            EventBus.Instance.RegisterListnerEvent(typeof(MovementDataEvent), new EventListner<MovementDataEvent>(MovementAction, true));
        }

        private void FixedUpdate()
        {
            if (_isMove)
            {
                Movement();
            }
        }

        private void Movement()
        {
            _rigidbody.velocity = _self.forward * moveSpeed;
        }

        private void MovementAction(MovementDataEvent movementComponent)
        {
            _isMove = movementComponent.isMove;
        }
    }

}