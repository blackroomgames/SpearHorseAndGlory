using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpearHorseAndGlory
{
    [CreateAssetMenu(fileName = "SpearSetting", menuName = "SpearHorseAndGlory/SettingsSO/SpearSO")]
    public sealed class SpearSettingSO : ScriptableObject
    {
        public Vector3 startPosition;
        public Vector3 startRotation;
        public Vector3 combatPosition;
        //idle value
        public float frequency;
        [Range(0f, 1f)]
        public float magnitude = 0.15f;
        //combat value
        public float aimSpeed;
        [Range(0f, 1f)]
        public float upForce = 0.5f;
        [Range(0f, 1f)]
        public float downForce = 0.6f;


        [SerializeField]private float _currentAngle;

        public void SpearSetStartPosition(Transform spear)
        {
            spear.localPosition = startPosition;
            spear.localRotation = Quaternion.Euler(startRotation);

            combatPosition = startPosition - Vector3.up * 0.1f;
        }

        public void SpearNoiseInIdlePosition(Transform spear)
        {
            spear.position += Vector3.up * Mathf.Sin(frequency * Time.time) * magnitude * Time.deltaTime;
        }

        public bool SpearTakeAim(Transform spear)
        {
            bool result = false;
            if(_currentAngle == 0f)
            {
                _currentAngle = startRotation.x;
            }
            if(_currentAngle >= 0f)
            {
                result = true;
                _currentAngle = 0f;
                spear.localPosition -= Vector3.up * 0.1f;
            }
            else
            {
                _currentAngle += Mathf.Lerp(aimSpeed, 0f, Time.deltaTime);
            }
            spear.localRotation = Quaternion.Euler(Vector3.right * _currentAngle);

            return result;
        }
        //invoke in FixedUpdate
        public (float, float) CombatSpearRotation(Transform spear, float timer, float angle = 0f)
        {
            if (timer > 0f)
            {
                angle -= Mathf.Lerp(upForce, Const.MinSpearAngle, Time.deltaTime);
                
            }
            else
            {
                angle += Mathf.Lerp(downForce, Const.MinSpearAngle, Time.deltaTime);
            }
            angle = Mathf.Clamp(angle, Const.MinSpearAngle, Const.MaxSpearAngle);
            spear.localRotation = Quaternion.Euler(Vector3.right * angle);
            return (timer - Time.deltaTime, angle);
        }
    }
}

