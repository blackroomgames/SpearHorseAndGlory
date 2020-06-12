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

        public float frequency;
        public float magnitude;

        public float aimSpeed;


        private Vector3 _currentAngleSpear;
        private float _currentAngle;

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
            _currentAngleSpear = spear.eulerAngles;
            if(_currentAngle >= 0f)
            {
                result = true;
                _currentAngle = 0f;
                //smoth
                spear.localPosition -= Vector3.up * 0.1f;
            }
            else
            {
                _currentAngle += Mathf.Lerp(aimSpeed, 0f, Time.deltaTime);
            }
            _currentAngleSpear.x = 360f + _currentAngle;
            spear.rotation = Quaternion.Euler(_currentAngleSpear);

            return result;
        }
        //invoke in FixedUpdate
        public (float, float) CombatSpearRotation(Transform spear, float timer, float currentAngle = 0f)
        {
            if (timer > 0f)
            {
                currentAngle -= Mathf.Lerp(0.4f, Const.MinSpearAngle, Time.deltaTime);
                
            }
            else
            {
                currentAngle += Mathf.Lerp(0.6f, Const.MinSpearAngle, Time.deltaTime);
            }
            currentAngle = Mathf.Clamp(currentAngle, Const.MinSpearAngle, Const.MaxSpearAngle);
            _currentAngleSpear.x = currentAngle;
            spear.rotation = Quaternion.Euler(_currentAngleSpear);
            return (timer - Time.deltaTime, currentAngle);
        }
    }
}

