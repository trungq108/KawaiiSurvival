using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CreditScroll : MonoBehaviour
{
    private RectTransform rect;
    [SerializeField] private float scrollSpeed = 50f;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        rect.anchoredPosition = rect.anchoredPosition.With(y: 0);
    }

    private void Update()
    {
        rect.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
    }
}
