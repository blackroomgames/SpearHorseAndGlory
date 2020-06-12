using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpearHorseAndGlory
{
    [AddComponentMenu("SpearHorseAndGlory/MainComponent/CameraMovement")]
    public sealed class CameraMovementComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        private Transform _self;

        private Vector3 _offset;

        private void Start()
        {
            _self = this.gameObject.transform;
            _offset = _target.position - _self.position;
        }

        private void LateUpdate()
        {
            _self.localPosition = _target.localPosition - _offset;
        }
    }
}