using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeDuration;
    [SerializeField] private float shakeStrength;
    [SerializeField] Camera mainCamera;

    private void OnEnable()
    {
        GameEvent.onRangeWeaponAttack += ShakeCamera;
    }

    private void OnDisable()
    {
        GameEvent.onRangeWeaponAttack -= ShakeCamera;
    }

    private void ShakeCamera(AudioClip clip)
    {
        mainCamera.transform.DOShakePosition(shakeDuration, shakeStrength, 10, 90, false, true);
    }
}
