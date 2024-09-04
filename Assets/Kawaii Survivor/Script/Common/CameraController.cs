using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector2 cameraClamb;
    private PlayerController target;


    private void Awake()
    {
        target = FindObjectOfType<PlayerController>();
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 cameraPosition = target.transform.position;
        cameraPosition.x = Mathf.Clamp(cameraPosition.x, -cameraClamb.x, cameraClamb.x);
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, -cameraClamb.y, cameraClamb.y);
        cameraPosition.z = -10;

        transform.position = cameraPosition;
    }
}
