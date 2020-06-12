using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SpearHorseAndGlory.EventBusSystem;

namespace SpearHorseAndGlory.Components
{
    [RequireComponent(typeof(Animator))]
    [AddComponentMenu("SpearHorseAndGlory/MainComponent/AnimationComponent")]
    public sealed class AnimationComponent : MonoBehaviour
    {
        private const string IdleAnimationKey = "Idle";
        private const string IdleTypeAnimationKey = "IdleType";
        private const string MovementAnimationKey = "Movement";
        private const float MaxSpeed = 1f;

        private Animator _animator;
        private Coroutine _currentCoroutine;
        private float _flipAnimationTimer = 6f;
        private float _currentTimer;
        private bool _isIdle = true;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _animator.SetBool(IdleAnimationKey, _isIdle);
            _currentTimer = Time.time + _flipAnimationTimer;

            EventBus.Instance.RegisterListnerEvent(typeof(MovementDataEvent), new EventListner<MovementDataEvent>(Movement, true));
        }

        //test
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _isIdle = !_isIdle;
                _animator.SetBool(IdleAnimationKey, _isIdle);
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                StartCoroutine(Charge());
            }
        }

        private void CallAnimationEvent()
        {
            if (Time.time < _currentTimer) return;
            if (_isIdle)
            {
                int rndIdleType = Random.Range(1, 4);
                _animator.SetInteger(IdleTypeAnimationKey, rndIdleType);
            }
            _currentCoroutine = StartCoroutine(LateTimerChange());

        }

        private void Movement(MovementDataEvent movementData)
        {
            StartCoroutine(Charge());
        }

        private IEnumerator LateTimerChange()
        {
            yield return new WaitForSeconds(0.5f);
            _animator.SetInteger(IdleTypeAnimationKey, 0);
            _flipAnimationTimer = Random.Range(6f, 12f);
            _currentTimer = Time.time + _flipAnimationTimer;
        }

        private IEnumerator Charge()
        {
            bool isMove = true;
            _isIdle = false;
            _animator.SetBool(IdleAnimationKey, _isIdle);
            if(_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
            float _currentSpeed = 0f;
            while (isMove)
            {
                yield return new WaitForFixedUpdate();
                _currentSpeed = Mathf.Lerp(_currentSpeed, MaxSpeed, 2.5f * Time.deltaTime);
                if(_currentSpeed >= MaxSpeed * 0.9f)
                {
                    _currentSpeed = MaxSpeed;
                    isMove = false;
                }
                _animator.SetFloat(MovementAnimationKey, _currentSpeed);
            }
        }
    }
}